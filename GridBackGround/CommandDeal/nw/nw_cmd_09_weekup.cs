using GridBackGround.Termination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_cmd_09_weekup : nw_cmd_base
    {
        public override int Control { get { return 0x09; } }

        public override string Name { get { return "唤醒终端"; } }

        public string Password { get; set; }

        public nw_cmd_09_weekup()
        {

        }

        public nw_cmd_09_weekup(IPowerPole pole) : base(pole)
        {

        }


        public override int Decode(out string msg)
        {
            if (Data == null || (Data.Length != 2 && Data.Length != 4))
                throw new Exception(string.Format("数据域长度错误,应为 2或4字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));
            if (this.Data.Length == 2)
            {
                if (Data[0] == 0xff && Data[1] == 0xff)
                    msg = "唤醒失败。原密码错误";
                else
                    msg = string.Format("唤醒失败。错误码:{0:X2}{1:X2}H", Data[0], Data[1]);
                return 0;
            }
            else
            {
                msg = string.Format("唤醒成功. ");
            }
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            byte[] data = new byte[4];
            this.SetPassword(data, 0, this.Password);
            msg = string.Empty;
            return data;
        }
    }
}
