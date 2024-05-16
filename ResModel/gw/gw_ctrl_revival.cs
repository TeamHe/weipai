using System;
using System.Text;

namespace ResModel.gw
{
    public class gw_ctrl_revival : gw_ctrl
    {
        /// <summary>
        /// 装置苏醒参考时间
        /// </summary>
        public UInt32 RevivalTime { get; set; } 

        /// <summary>
        /// 装置苏醒周期
        /// </summary>
        public UInt16 RevivalCycle {  get; set; }

        /// <summary>
        /// 装置苏醒时间长度
        /// </summary>
        public UInt16 DurationTime {  get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("苏醒参考时间:{0}s ", this.RevivalTime);
            sb.AppendFormat("苏醒周期:{0}s ", this.RevivalCycle);
            sb.AppendFormat("苏醒时间长度:{0}s ", this.DurationTime);
            return sb.ToString();
        }
    }
}
