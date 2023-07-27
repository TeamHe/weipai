using System;
using System.Collections.Generic;
using ResModel;
using ResModel.nw;
using ResModel.PowerPole;

namespace GridBackGround.CommandDeal.nw
{
    /// <summary>
    /// 南网数据包: 装置流量数据使用情况 处理
    /// </summary>
    public class nw_cmd_40_data_traffic : nw_cmd_base
    {
        public override int Control { get { return 0x40; } }

        public override string Name { get { return "装置流量数据"; } }

        /// <summary>
        /// 帧标识
        /// </summary>
        public int FramFlag { get; set; }

        protected DateTime Time { get; set; }

        protected int Pnum { get; set; }

        protected bool Response { get; set; }

        public List<nw_data_traffic> values { get; set; }

        public nw_cmd_40_data_traffic()
        {
            this.values = new List<nw_data_traffic> { };
        }

        public nw_cmd_40_data_traffic(IPowerPole pole) : base(pole)
        {
            this.values = new List<nw_data_traffic> { };
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
            this.FramFlag = this.Data[offset++];
            this.Pnum = this.Data[offset++];
            if (this.Pnum > 0)
            {
                offset += this.GetDateTime(this.Data, offset, out DateTime datatime);
                for (int i = 0; i < Pnum; i++)
                {
                    nw_data_traffic value = new nw_data_traffic() { DataTime = datatime };
                    if((ret = value.Decode(this.Data, offset)) < 0)
                    {
                        msg = string.Format("第{0}包数据解析失败", i); 
                        return ret;
                    }

                    offset += ret;
                    this.values.Add(value);
                    //显示数据
                    NewDataInfo(this.Pole, new DataInfo(DataInfoState.rec, this.Pole,
                        this.Name, value.ToString()));

                    if (i == this.Pnum - 1)
                        break;
                    if ((this.Data.Length - offset) < 2)
                    {
                        msg = string.Format("第{0}包数据长度错误", i + 1);
                        break;
                    }

                    offset += this.GetU16(this.Data, offset, out int timestramp);
                    datatime = datatime.AddSeconds(timestramp);
                }
            }

            this.Response = true;
            this.SendCommand(out string msg_send);
            msg += msg_send;

            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            msg = string.Empty;
            if(this.Response)
            {
                return this.ResponseData(this.FramFlag);
            }
            else
            {
                msg = "主站请求上传" + this.Name;
                return null;
            }
        }


    }
}
