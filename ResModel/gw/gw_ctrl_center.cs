using System.ComponentModel;
using System.Net;
using System.Text;

namespace ResModel.gw
{
    public class gw_ctrl_center : gw_ctrl
    {
        public enum EFlag
        {
            [Description("IP地址")]
            IP = 0,

            [Description("端口号")]
            Port,
        }

        public IPAddress IP { get; set; }

        public int Port { get; set; }

        public override string ToString(bool flag)
        {
            StringBuilder sb = new StringBuilder();
            if(flag || this.GetFlag((int)EFlag.IP))
                sb.AppendFormat("IP地址:{0} ",this.IP.ToString());
            if (flag || this.GetFlag((int)EFlag.Port))
                sb.AppendFormat("端口号:{0} ", this.Port);
            return sb.ToString();
        }
    }
}
