using GridBackGround.PacketAnaLysis;
using GridBackGround.Termination;
using ResModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static log4net.Appender.RollingFileAppender;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_pull_ange
    {
        /// <summary>
        /// 最大拉力时刻-拉力
        /// </summary>
        public int Pull_max_pull { get; set; }
        /// <summary>
        /// 最大拉力时刻-风偏角
        /// </summary>
        public double AngleDec_max_pull { get; set; }
        /// <summary>
        /// 最大拉力时刻-倾斜角
        /// </summary>
        public double AngleInc_max_pull { get; set; }


        /// <summary>
        /// 最小拉力时刻-拉力
        /// </summary>
        public int Pull_min_pull { get; set; }
        /// <summary>
        /// 最小拉力时刻-风偏角
        /// </summary>
        public double AngleDec_min_pull { get; set; }
        /// <summary>
        /// 最小拉力时刻-倾斜角
        /// </summary>
        public double AngleInc_min_pull { get; set; }



        /// <summary>
        /// 最大风偏角时刻-拉力
        /// </summary>
        public int Pull_max_angle { get; set; }
        /// <summary>
        /// 最大风偏角时刻-风偏角
        /// </summary>
        public double AngleDec_max_angle { get; set; }
        /// <summary>
        /// 最大风偏角时刻-倾斜角
        /// </summary>
        public double AngleInc_max_angle { get; set; }


        /// <summary>
        /// 最小风偏角时刻-拉力
        /// </summary>
        public int Pull_min_angle { get; set; }
        /// <summary>
        /// 最小风偏角时刻-风偏角
        /// </summary>
        public double AngleDec_min_angle { get; set;}
        /// <summary>
        /// 最小风偏角时刻-倾斜角
        /// </summary>
        public double AngleInc_min_angle { get; set; }

        /// <summary>
        /// 数据时间
        /// </summary>
        public DateTime DataTime { get; set; }

        public override string ToString()
        {
            return string.Format("最大拉力时刻: 拉力:{0} 风偏角:{1} 倾斜角:{2}  " +
                                 "最小拉力时刻: 拉力:{3} 风偏角:{4} 倾斜角:{5}  " +
                                 "最大风偏角时刻: 拉力:{6} 风偏角:{7} 倾斜角:{8}  " +
                                 "最小风偏角时刻: 拉力:{9} 风偏角:{10} 倾斜角:{11}  ",
                                 Pull_max_pull, AngleDec_max_pull, AngleInc_max_pull,
                                 Pull_min_pull, AngleDec_min_pull, AngleInc_min_pull,
                                 Pull_max_angle, AngleDec_max_angle, AngleInc_max_angle,
                                 Pull_min_angle, AngleDec_min_angle, AngleInc_min_angle);
        }
    }

    public class nw_cmd_22_pull_angle : nw_cmd_base
    {
        public override int Control { get { return 0x22; } }

        public override string Name { get { return "导地线拉力风偏角数据"; } }

        public List<nw_pull_ange> values { get; set; }
        /// <summary>
        /// 帧标识
        /// </summary>
        private byte FrameFlag { get; set; }

        /// <summary>
        /// 功能单元识别码
        /// </summary>
        public int FuncCode { get; set; }

        /// <summary>
        /// 是否为主站请求数据
        /// </summary>
        private bool Response { get; set; }

        public nw_cmd_22_pull_angle()
        {

        }

        public nw_cmd_22_pull_angle(IPowerPole pole) : base(pole)
        {

        }

        private int GetAngle(byte[] data, int offset, out double value)
        {
            this.GetS16(data, offset, out int val);
            value = val / 100.0;
            return 2;
        }



        public int Decode_pull_angle(byte[] data, int offset, out nw_pull_ange pull)
        {
            pull = null;
            if (data.Length - offset < 24)
                return -1;
            int no = offset;
            int value = 0;
            double fvale = 0;
            pull = new nw_pull_ange();

            no += GetU16(data, no, out value);
            pull.Pull_max_pull = value;
            
            no += GetAngle(data, no, out fvale);
            pull.AngleDec_max_pull = fvale;
            
            no += GetAngle(data, no, out fvale);
            pull.AngleInc_max_pull = fvale;


            no += GetU16(data, no, out value);
            pull.Pull_min_pull = value;
            
            no += GetAngle(data, no, out fvale);
            pull.AngleDec_min_pull = fvale;
            
            no += GetAngle(data, no, out fvale);
            pull.AngleInc_min_pull = fvale;


            no += GetAngle(data, no, out fvale);
            pull.AngleDec_max_angle = fvale;
            
            no += GetAngle(data, no, out fvale);
            pull.AngleInc_max_angle = fvale;
            
            no += GetU16(data, no, out value);
            pull.Pull_max_angle = value;


            no += GetAngle(data, no, out fvale); 
            pull.AngleDec_min_angle = fvale;

            no += GetAngle(data, no, out fvale); 
            pull.AngleInc_min_angle = fvale;

            no += GetU16(data, no, out value);
            pull.Pull_min_angle = value;


            return no - offset;
        }

        public override int Decode(out string msg)
        {
            msg = null;
            if (this.Data == null || this.Data.Length == 0)
            {
                msg = "装置无未上送数据";
                return 0;
            }

            int offset = 4; //不验证密文信息，以及帧标识
            int ret = 0;
            this.FrameFlag = this.Data[offset++];
            int pnum = this.Data[offset++];
            this.FuncCode = this.Data[offset++];

            offset += this.GetDateTime(this.Data, offset, out DateTime datatime);
            for (int i = 0; i < pnum; i++)
            {
                if ((ret = this.Decode_pull_angle(this.Data, offset, out nw_pull_ange weather)) < 0)
                {
                    msg = string.Format("第{0}包数据解析失败", i);
                    break;
                }

                offset += ret;
                weather.DataTime = datatime;
                //显示数据
                DisPacket.NewRecord(new DataInfo(DataRecSendState.rec, this.Pole,
                    this.Name, weather.ToString()));

                if (i == pnum - 1)
                    break;
                if ((this.Data.Length - offset) < 2)
                {
                    msg = string.Format("第{0}包数据长度错误", i + 1);
                    break;
                }

                offset += this.GetU16(this.Data, offset, out int period);
                datatime = datatime.AddSeconds(period);
            }
            this.Response = true;
            this.SendCommand(out string msg_send);
            msg += msg_send;
            return ret;
        }

        public override byte[] Encode(out string msg)
        {
            msg = string.Empty;
            if (this.Response)
            {
                byte[] data = new byte[3];
                data[0] = this.FrameFlag;
                data[1] = 0xaa;
                data[2] = 0x55;
                return data;
            }
            msg = "主站请求上传"+this.Name;
            return null;

        }
    }
}