using ResModel.gw;
using System;
using System.Data;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_a3_period : gw_cmd_base_ctrl
    {
        /// <summary>
        /// 报文名称
        /// </summary>
        public override string Name { get { return "采样周期"; } }

        /// <summary>
        /// 报文类型
        /// </summary>
        public override int PType { get { return 0xa3; } }

        /// <summary>
        /// 数据域内容长度
        /// </summary>
        public override int ValuesLength {  get { return 4; } }

        /// <summary>
        /// 采样周期
        /// </summary>
        public gw_ctrl_period Period { get; set; }

        public void Query(gw_func_code type)
        {
            this.Period = new gw_ctrl_period()
            {
                MainType = type,
            };
            base.Query();
        }


        public void Update(gw_ctrl_period period)
        {
            if (period == null)
                throw new ArgumentNullException(nameof(period));
            this.Period = period;
            this.Update((gw_ctrl)period);
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            int val = 0;
            if(this.Period == null)
                this.Period = new gw_ctrl_period();
            FlushRespStatus(this.Period);

            this.Period.MainType = (gw_func_code)data[offset++];
            offset += gw_coding.GetU16(data, offset, out val);
            this.Period.MainTime = val;
            this.Period.HearTime = (int)data[offset++];
            msg = Period.ToString(this.RequestSetFlag == gw_ctrl.RequestSetFlag.Query);
            return this.ValuesLength;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            if (this.Period == null)
                throw new ArgumentNullException(nameof(this.Period));
            data[offset++] = (byte)this.Period.MainType;
            offset += gw_coding.SetU16(data, offset, this.Period.MainTime);
            data[offset++] = (byte)this.Period.HearTime;
            msg = this.Period.ToString(false);
            return this.ValuesLength;
        }
    }
}
