using ResModel;
using ResModel.gw;
using System;

namespace cma.service.gw_cmd
{
    /// <summary>
    /// 监测装置时间查询/设置
    /// </summary>
    public class gw_cmd_ctrl_time : gw_cmd_base_ctrl
    {
        protected override bool WithReqSetFlag {  get { return true; } }

        protected override bool WithRspStatus { get { return true; } }

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
            int start = offset;
            if (data.Length - offset < this.ValuesLength)
                throw new Exception("数据缓冲区长度太小");

            offset += gw_coding.GetTime(data, offset, out DateTime time);
            this.Time = time;
            msg = string.Format("装置时间:{0}", this.Time);
            return offset -start;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            msg = string.Empty;
            if(this.RequestSetFlag == gw_ctrl.ESetFlag.Set)
                msg = string.Format("当前时间: {0}", this.Time);
            offset += gw_coding.SetTime(data, offset, this.Time);
            return offset - start;
        }
    }
}
