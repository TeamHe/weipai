using GridBackGround.Termination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBackGround.CommandDeal.nw
{
    /// <summary>
    /// 下发设置装置密码指令
    /// </summary>
    public class nw_cmd_02_password : nw_cmd_base
    {
        public override int Control { get { return 0x02; } }

        public override string Name { get{ return "设置装置密码"; } }

        /// <summary>
        /// 原密码
        /// </summary>
        public string Password_old { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string Password_new { get; set; }

        public nw_cmd_02_password(IPowerPole pole) : base(pole)
        {
        }

        public nw_cmd_02_password()
        {
        }



        public override int Decode(out string msg)
        {
            if(Data == null || (Data.Length != 2 && Data.Length != 8))
                throw new Exception(string.Format("数据域长度错误,应为 2或8字节 实际为:{1}", 
                    this.Data != null ? this.Data.Length : 0));

            if(this.Data.Length == 2)
            {
                if (Data[0] ==0xff && Data[1] == 0xff)
                    msg = "设置失败。原密码错误";
                else
                    msg = string.Format("设置失败。错误码:{0:X2}{1:X2}H", Data[0], Data[1]);
                return 0;
            }
            else
            {
                this.Password_old = Encoding.ASCII.GetString(this.Data, 0, 4);
                this.Password_new = Encoding.ASCII.GetString(this.Data, 4, 4);
                msg = string.Format("设置成功. 原密码:{0} 新密码:{1}", this.Password_old, this.Password_new);
            }
            return 0;
                
        }

        public override byte[] Encode(out string msg)
        {
            if(this.Password_old == null || this.Password_new == null) 
                throw new Exception("新旧密码均不能问为空");

            byte[] data = new byte[8];
            byte[] p_old = Encoding.Default.GetBytes(this.Password_old);
            byte[] p_new = Encoding.Default.GetBytes(this.Password_new);
            Buffer.BlockCopy(p_old, 0, data, 0, p_old.Length >= 4 ? 4 : p_old.Length);
            Buffer.BlockCopy(p_new, 0, data, 4, p_new.Length >= 4 ? 4 : p_new.Length);
            msg = string.Format("旧密码:{0},新密码{1}",Password_old, this.Password_new);
            return data;
        }


    }
}
