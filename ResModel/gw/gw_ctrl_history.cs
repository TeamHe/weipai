using System;
using System.Text;
using Tools;

namespace ResModel.gw
{
    public class gw_ctrl_history:gw_ctrl
    {
        public gw_func_code Type { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("数据类型:{0} ", EnumUtil.GetDescription(this.Type));
            if(this.Start != DateTime.MinValue)
                sb.AppendFormat("起始时间:{0} ", this.Start);
            if(this.End != DateTime.MinValue)
                sb.AppendFormat("结束时间:{0} ", this.End);
            return sb.ToString();
        }
    }
}
