using System.Net;

namespace ResModel.nw
{
    /// <summary>
    /// 南网监控中心信息
    /// </summary>
    public class nw_center
    {
        public IPAddress IPAddress { get; set; }

        public int Port { get; set; }

        public string PhoneNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("主站IP:{0}  端口号:{1}  主站卡号:{2}",
                IPAddress.ToString(),
                Port.ToString(),
                PhoneNumber);

        }
    }

}
