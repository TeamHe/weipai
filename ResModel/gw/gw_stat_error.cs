using System;
using System.Text;

namespace ResModel.gw
{
    public class gw_stat_error
    {
        public gw_stat_error() { }

        public DateTime Time { get; set; }

        public string Desc {  get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("采集时间:{0}", this.Time);
            sb.AppendFormat("故障描述:{0}", this.Desc);
            return sb.ToString();
        }
    }
}
