using DB_Operation.RealData;
using ResModel;
using ResModel.nw;
using System;
using System.Collections.Generic;
using ResModel.PowerPole;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_cmd_22_pull_angle : nw_cmd_base
    {
        public override int Control { get { return 0x22; } }

        public override string Name { get { return "导地线拉力风偏角数据"; } }

        public List<nw_data_pull_angle> values { get; set; }
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



        public int Decode_pull_angle(byte[] data, int offset, out nw_data_pull_angle pull)
        {
            pull = null;
            if (data.Length - offset < 24)
                return -1;
            int no = offset;
            int value = 0;
            double fvale = 0;
            pull = new nw_data_pull_angle();

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

            no += GetU16(data, no, out value);
            pull.Pull_max_angle = value;
            no += GetAngle(data, no, out fvale);
            pull.AngleDec_max_angle = fvale;
            no += GetAngle(data, no, out fvale);
            pull.AngleInc_max_angle = fvale;

            no += GetU16(data, no, out value);
            pull.Pull_min_angle = value;
            no += GetAngle(data, no, out fvale); 
            pull.AngleDec_min_angle = fvale;
            no += GetAngle(data, no, out fvale); 
            pull.AngleInc_min_angle = fvale;

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
            db_data_nw_pull_angle db = new db_data_nw_pull_angle(this.Pole);
            if(pnum > 0 )
            {
                offset += this.GetDateTime(this.Data, offset, out DateTime datatime);
                for (int i = 0; i < pnum; i++)
                {
                    if ((ret = this.Decode_pull_angle(this.Data, offset, out nw_data_pull_angle data)) < 0)
                    {
                        msg = string.Format("第{0}包数据解析失败", i);
                        break;
                    }

                    offset += ret;
                    data.DataTime = datatime;
                    data.FuncCode = this.FuncCode;
                    string msg1 = string.Empty;
                    try
                    {
                       db.DataSave(data);
                    }catch(Exception e)
                    {
                        msg1 = "数据存储失败";
                    }
                    //显示数据
                    NewDataInfo(this.Pole,new DataInfo(DataInfoState.rec, this.Pole,
                        this.Name, data.ToString() + " " + msg1));

                    if (i == pnum - 1)
                        break;
                    if ((this.Data.Length - offset) < 2)
                    {
                        msg = string.Format("第{0}包数据长度错误", i + 1);
                        break;
                    }

                    offset += nw_cmd_base.GetU16(this.Data, offset, out int period);
                    datatime = datatime.AddSeconds(period);
                }
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