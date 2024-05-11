using System.ComponentModel;
using System.Net;
using System.Text;

namespace ResModel.gw
{
    public class gw_ctrl_adapter : gw_ctrl
    {
        public enum EFlag
        {
            [Description("IP")]
            IP = 0,

            [Description("Mask")]
            Mask = 1,

            [Description("GateWay")]
            GateWay = 2,

            [Description("DNS")]
            DNS = 3,

            [Description("PhoneNumber")]
            PhoneNumber = 4,
        }

        /// <summary>
        /// 装置IP
        /// </summary>
        public IPAddress IP { get; set; }

        /// <summary>
        /// 子网掩码
        /// </summary>
        public IPAddress Mask { get; set; }

        /// <summary>
        /// 网关
        /// </summary>
        public IPAddress GateWay { get; set; }

        /// <summary>
        /// DNS Server
        /// </summary>
        public IPAddress DNS { get; set; }

        /// <summary>
        /// 手机串号
        /// </summary>
        public string PhoneNumber { get; set; }

        public gw_ctrl_adapter() { }

        public override string ToString(bool flag)
        {
            StringBuilder sb = new StringBuilder();
            if (flag || this.GetFlag((int)EFlag.IP))
                sb.AppendFormat("IP:{0} ", this.IP.ToString());
            if (flag || this.GetFlag((int)EFlag.Mask))
                sb.AppendFormat("子网掩码:{0} ", this.Mask.ToString());
            if (flag || this.GetFlag((int)EFlag.GateWay))
                sb.AppendFormat("网关:{0} ", this.GateWay.ToString());
            if (flag || this.GetFlag((int)EFlag.DNS))
                sb.AppendFormat("DNS:{0} ", this.DNS.ToString());
            if (flag || this.GetFlag((int)EFlag.PhoneNumber))
                sb.AppendFormat("手机串号:{0} ", this.PhoneNumber);
            return sb.ToString();
        }
    }
}
