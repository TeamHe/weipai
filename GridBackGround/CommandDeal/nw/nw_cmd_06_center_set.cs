using ResModel;
using System;
using System.Net;
using ResModel.nw;

namespace GridBackGround.CommandDeal.nw
{
    /// <summary>
    /// 南网更改主站IP地址端口和卡号
    /// </summary>
    public class nw_cmd_06_center_set : nw_cmd_base
    {
        public nw_cmd_06_center_set(IPowerPole pole) : base(pole)
        {
        }

        public nw_cmd_06_center_set()
        {
        }



        public override int Control { get { return 0x06; } }

        public override string Name { get { return "设置主站信息"; } }

        /// <summary>
        /// 南网监控中心相关参数
        /// </summary>
        public nw_center center { get; set; }   

        public override int Decode(out string msg)
        {
            if (Data == null || (Data.Length != 2 && Data.Length != 28))
                throw new Exception(string.Format("数据域长度错误,应为 2或28字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            if(Data.Length == 2)
            {
                if ((Data[0] == 0xff) && (Data[1] == 0xff))
                    msg = "失败，密码错误";
                else if ((Data[0] == 0x00) && (Data[1] == 0x00))
                    msg = "失败，主站 IP、端口号和主站卡号对应字节不完全相同";
                else
                    msg = string.Format("失败, 错误代码:{0:X2}{1:X2}H", Data[0], Data[1]);
                return -1;
            }


            int offset = 0;
            offset += this.GetPassword(this.Data, offset, out string password);
            offset += this.GetIPAddress(this.Data, offset, out IPAddress ip);
            offset += this.GetU16(this.Data, offset, out int port);
            offset += 6; //跳过重复的IP端口
            offset += this.GetPhoneNumber(this.Data, offset, out string phoneno);

            if (this.center == null)
                this.center = new nw_center();
            this.center.IPAddress = ip;
            this.center.Port = port;
            this.center.Password = password;    
            this.center.PhoneNumber = phoneno;
            msg = "成功。" + this.center.ToString();
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            if (this.center  == null)
                throw new Exception("监控中心配置参数未设置");

            byte[] data = new byte[28];
            int offset = 0;
            offset += this.SetPassword(data, offset, this.center.Password);
            offset += this.SetIPAddress(data, offset, this.center.IPAddress);
            offset += this.SetU16(data, offset, this.center.Port);
            offset += this.SetIPAddress(data, offset, this.center.IPAddress);
            offset += this.SetU16(data, offset, this.center.Port);
            offset += this.SetPhoneNumber(data, offset, this.center.PhoneNumber);
            offset += this.SetPhoneNumber(data, offset, this.center.PhoneNumber);
            msg = string.Format("密码:{0} {1}", this.center.Password, this.center.ToString());
            return data;
        }
    }
}
