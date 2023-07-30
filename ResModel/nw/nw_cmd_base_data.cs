using System;

namespace ResModel.nw
{
    public abstract class nw_cmd_base_data:nw_cmd_base
    {
        public nw_cmd_base_data(IPowerPole pole) : base(pole)
        {
        }
        public nw_cmd_base_data() : base()
        {
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// 帧标识
        /// </summary>
        public int FramFlag { get; set; }

        /// <summary>
        /// 包数
        /// </summary>
        protected int Pnum { get; set; }

        /// <summary>
        /// 功能单元识别码
        /// </summary>
        protected int UnitNO { get; set; }

        /// <summary>
        /// 是否包含功能单元识别码
        /// </summary>
        protected bool HasUnitNo { get; set; }

        /// <summary>
        /// 数据时间
        /// </summary>
        protected DateTime DataTime { get; set; }

        /// <summary>
        /// 是否为响应包
        /// </summary>
        protected bool IsResponse { get; set; }

        protected abstract int DecodeValue(byte[] data, int offset);

        public override int Decode(out string msg)
        {
            msg = null;
            if (this.Data == null || this.Data.Length == 0)
            {
                msg = "装置无未上送数据";
                return 0;
            }

            int offset = 0;
            offset += this.GetPassword(this.Data, offset, out string password);
            this.Password = password;

            this.FramFlag = this.Data[offset++];
            this.Pnum = this.Data[offset++];
            if(HasUnitNo)
                this.UnitNO = this.Data[offset++];

            if(this.Pnum == 0)
                return this.Response(out msg);

            offset += this.GetDateTime(this.Data, offset, out DateTime time);
            this.DataTime = time;

            for(int i = 0; i < Pnum; i++)
            {
                int ret = 0;
                try
                {
                    if ((ret = this.DecodeValue(Data, offset)) < 0)
                    {
                        msg = string.Format("第{0}包数据解析失败", i + 1);
                        return ret;
                    }
                }
                catch (Exception ex)
                {
                    msg = string.Format("第{0}包数据解析失败.{1}", i + 1, ex.Message);
                    return ret;
                }
                offset += ret;
                if (i == this.Pnum - 1)
                    break;
                if ((this.Data.Length - offset) < 2)
                {
                    msg = string.Format("第{0}包数据长度错误", i + 1);
                    return -1;
                }
                offset += nw_cmd_base.GetU16(this.Data, offset, out int timestramp);
                this.DataTime = this.DataTime.AddSeconds(timestramp);
            }
            return this.Response(out msg);
        }

        protected int Response(out string msg_send)
        {
            this.IsResponse = true;
            this.SendCommand(out msg_send);
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            msg = string.Empty;
            if (this.IsResponse)
                return this.ResponseData(this.FramFlag);
            
            msg = "主站请求上传" + this.Name;
            return null;
        }
    }
}
