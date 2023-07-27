using ResModel;
using System;
using System.Collections.Generic;
using Tools;
using ResModel.nw;

namespace GridBackGround.CommandDeal.nw
{

    public class nw_cmd_0b_function_config : nw_cmd_base
    {
        public override int Control { get { return 0x0b; } }

        public override string Name { get { return "装置功能配置"; } }

        public string Password { get; set; }

        public List<nw_func_code> Functions { get; set; }


        public nw_cmd_0b_function_config() { }
        public nw_cmd_0b_function_config(IPowerPole pole):base(pole) { }

        public override int Decode(out string msg)
        {
            throw new NotImplementedException();
        }

        public override byte[] Encode(out string msg)
        {
            if(this.Password == null || this.Password == string.Empty)
                throw new ArgumentNullException("密码");
            int length = 4;
            if (this.Functions != null)
                length += this.Functions.Count;

            byte[] data = new byte[length];
            int offset = 0;
            msg = "功能配置为:";
            offset += this.SetPassword(data, offset, this.Password);
            if(this.Functions != null)
            {
                foreach (nw_func_code function in this.Functions)
                {
                    data[offset++] = (byte)function;
                    msg += " " + function.GetDescription();
                }
            }
            return data;
        }
    }
}
