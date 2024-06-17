using ResModel;
using System;
using System.Net;
using ResModel.nw;

namespace cma.service.nw_cmd
{

    /// <summary>
    /// 查询主站信息
    /// </summary>
    public class nw_cmd_07_center_get : nw_cmd_base
    {
        /// <summary>
        /// 监控中心信息
        /// </summary>
        public nw_center Center { get; set; }

        public nw_cmd_07_center_get()
        {

        }

        public nw_cmd_07_center_get(IPowerPole pole) : base(pole)
        {

        }

        public override int Control { get { return 0x07; } }

        public override string Name { get { return "查询主站信息"; } }

        public override int Decode(out string msg)
        {
            if(this.Data == null || this.Data.Length < 12)
            {
                msg = string.Format("数据域长度错误,应为{0} 实际为:{1}", 12, this.Data == null?0:this.Data.Length);
                return -1;
            }

            byte[] ipaddress = new byte[4];
            Buffer.BlockCopy(this.Data,0,ipaddress,0,4);
            nw_cmd_base.GetU16(this.Data, 4, out int port);
            this.GetPhoneNumber(this.Data, 6, out string phone);

            if (this.Center == null)
                this.Center = new nw_center();
            this.Center.IPAddress = new IPAddress(ipaddress);
            this.Center.Port = port;
            this.Center.PhoneNumber = phone;
            msg = "成功。" + this.Center.ToString();
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            msg = string.Empty;
            return null;
        }

    }
}
