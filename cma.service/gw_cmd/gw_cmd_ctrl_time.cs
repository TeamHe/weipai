using ResModel;
using ResModel.gw;
using System;
using Tools;

namespace cma.service.gw_cmd
{
    /// <summary>
    /// 监测装置时间查询/设置
    /// </summary>
    public class gw_cmd_ctrl_time : gw_cmd_base_ctrl
    {
        public override int ValuesLength { get { return 0x04; } }

        public override string Name { get { return "装置时间"; } }

        public override int PType { get { return 0xa1; } }

        public DateTime Time { get; set; }

        public gw_cmd_ctrl_time()
        {
            this.Time = DateTime.Now;
        }

        public gw_cmd_ctrl_time(IPowerPole pole)
            : base(pole){
            this.Time = DateTime.Now;
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            DateTime time = DateTime.MinValue;
            if (data.Length - offset < 4)
                throw new Exception("数据缓冲区长度太小");

            offset += gw_coding.GetTime(data, offset, out time);
            this.Time = time;
            msg = string.Format("装置时间:{0}", this.Time);
            return this.ValuesLength;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            msg = string.Empty;
            if(this.RequestSetFlag == gw_ctrl.RequestSetFlag.Set)
                msg = string.Format("当前时间: {0}", this.Time);
            offset += gw_coding.SetTime(data, offset, this.Time);
            return this.ValuesLength;
        }

        public override byte[] encode(out string msg)
        {
            byte[] data = new byte[1 + this.ValuesLength];
            int offset = 0;
            data[offset++] = (byte)this.RequestSetFlag;

            this.EncodeData(data, offset, out string str);
            msg = EnumUtil.GetDescription(this.RequestSetFlag) +
                 this.Name + ". " + str;
            return data;
        }

        public override int decode(byte[] data, int offset, out string msg)
        {
            if (data.Length - offset < 1)
                throw new Exception("数据缓冲区长度太小");
            this.Status = (gw_ctrl.Status)data[offset++];

            int ret = this.DecodeData(data, offset, out string str);
            msg = EnumUtil.GetDescription(this.Status) +
                  ". " + str;
            return ret;
        }
    }
}
