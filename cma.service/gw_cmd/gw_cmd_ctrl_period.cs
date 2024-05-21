using ResModel;
using ResModel.gw;
using System;
using Tools;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_period : gw_cmd_base_ctrl
    {
        protected override bool WithReqSetFlag {  get { return true; } }

        protected override bool WithReqType {  get { return true; } }

        protected override bool WithReqFlag {  get { return true; } }

        protected override bool WithRspStatus { get { return true; } }

        protected override bool WithRspType {  get { return true; } }

        protected override bool WithRspFlag {  get { return true; } }

        /// <summary>
        /// 报文名称
        /// </summary>
        public override string Name { get { return "采样参数"; } }

        /// <summary>
        /// 报文类型
        /// </summary>
        public override int PType { get { return 0xa4; } }

        /// <summary>
        /// 数据域内容长度
        /// </summary>
        public override int ValuesLength {  get { return 7; } }

        /// <summary>
        /// 采样周期
        /// </summary>
        public gw_ctrl_period Period { get; set; }

        public gw_cmd_ctrl_period() { }

        public gw_cmd_ctrl_period(IPowerPole pole)
            : base(pole) { }


        public void Update(gw_ctrl_period period)
        {
            if (period == null)
                throw new ArgumentNullException(nameof(period));
            this.Period = period;
            base.Update(period);
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            int val = 0;
            if (data.Length - offset < this.ValuesLength)
                throw new Exception("数据缓冲区长度太小");
            if(this.Period == null)
                this.Period = new gw_ctrl_period();
            FlushRespStatus(this.Period);

            offset += gw_coding.GetU16(data, offset, out val);
            this.Period.MainTime = val;
            
            offset += gw_coding.GetU16(data, offset, out val);
            this.Period.SampleCount = val;
            
            offset += gw_coding.GetU16(data, offset, out val);
            this.Period.SampleFreq = val;

            this.Period.HearTime = (int)data[offset++];
            msg = Period.ToString(false);
            return this.ValuesLength;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            msg = string.Empty;
            if (this.Period != null)
            {
                offset += gw_coding.SetU16(data, offset, this.Period.MainTime);
                offset += gw_coding.SetU16(data, offset, this.Period.SampleCount);
                offset += gw_coding.SetU16(data, offset, this.Period.SampleFreq);
                data[offset++] = (byte)this.Period.HearTime;
                msg = this.Period.ToString(false);

            }
            else
                offset += ValuesLength;

            return offset - start;
        }
    }
}
