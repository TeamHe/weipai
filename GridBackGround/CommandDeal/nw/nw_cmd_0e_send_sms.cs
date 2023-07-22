using ResModel;
using System;
using Tools;
using ResModel.nw;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_cmd_0e_send_sms : nw_cmd_base
    {
        public override int Control { get { return 0x0e; } }

        public override string Name { get { return "发送确认短信"; } }

        public nw_cmd_0e_send_sms() { }

        public nw_cmd_0e_send_sms(IPowerPole pole) : base(pole) { }

        public string Password { get; set; }

        public string PhoneNum { get; set; }


        public override int Decode(out string msg)
        {
            if (Data == null || (Data.Length != 2 && Data.Length != 10))
                throw new Exception(string.Format("数据域长度错误,应为 2或10字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            if (Data.Length == 2)
            {
                if ((Data[0] == 0xff) && (Data[1] == 0xff))
                    msg = "失败. 密码错误";
                else
                    msg = string.Format("失败, 错误代码:{0:X2}{1:X2}H", Data[0], Data[1]);
                return -1;
            }
            int offset = 0;
            offset += this.GetPassword(this.Data, offset, out string password);
            this.Password = password;
            offset += this.GetPhoneNumber(this.Data, offset, out string phone);
            this.PhoneNum = phone;
            msg = string.Format("成功。手机号码:{0}", this.PhoneNum);
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            if(this.Password == null )
                throw new ArgumentNullException("密码");
            if (!MetarnetRegex.IsPhone(this.PhoneNum))
                throw new ArgumentException("无效的手机号码");

            byte[] data = new byte[10];
            int offset = 0;
            offset += this.SetPassword(data,offset,this.Password);
            offset += this.SetPhoneNumber(data, offset, this.PhoneNum);
            msg = string.Format("手机号码:{0}", this.PhoneNum);
            return data;
        }
    }
}
