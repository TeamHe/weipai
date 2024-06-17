using ResModel;
using System;
using ResModel.nw;

namespace cma.service.nw_cmd
{
    public class nw_cmd_03_device_config_set : nw_cmd_base
    {
        public override int Control { get { return 0x03; } }

        public override string Name { get { return "下发参数配置"; } }

        public nw_device_config Para { get; set; }

        public string Password { get; set; }
        
        public nw_cmd_03_device_config_set() { }

        public nw_cmd_03_device_config_set(IPowerPole pole) : base(pole) { }

        public override int Decode(out string msg)
        {
            if (Data == null || (Data.Length != 2 && Data.Length != 18))
                throw new Exception(string.Format("数据域长度错误,应为 2或18字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));
            if (this.Data.Length == 2)
            {
                if (Data[0] == 0xff && Data[1] == 0xff)
                    msg = "设置失败。原密码错误";
                else
                    msg = string.Format("设置失败。错误码:{0:X2}{1:X2}H", Data[0], Data[1]);
                return 0;
            }
            else
            {
                if (this.Para == null)
                    this.Para = new nw_device_config();
                int offset = 0;
                int value;
                offset += this.GetPassword(this.Data, offset, out string password);
                this.Password = password;
                Para.Heart = Data[offset++];

                offset += nw_cmd_base.GetU16(this.Data, offset, out value);
                Para.ScanInterval = value;

                offset += nw_cmd_base.GetU16(this.Data, offset, out value);
                Para.DormancyDuration = value;

                offset += nw_cmd_base.GetU16(this.Data, offset, out value);
                Para.OnlineTime = value;

                Para.Reboot_day = Data[offset++];
                Para.Reboot_hour = Data[offset++];
                Para.Reboot_min = Data[offset++];

                offset += this.GetPassword(this.Data, offset, out password);
                Para.Password = password;

                msg = Para.ToString();
            }
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            int offset = 0;
            byte[] data = new byte[18];

            if(this.Password == null) 
                throw new ArgumentNullException("密码");
            if (this.Para == null || this.Para.Password ==null)
                throw new ArgumentNullException("认证密码");

            offset += this.SetPassword(data, offset, this.Password);
            data[offset++] = (byte)Para.Heart;
            offset += nw_cmd_base.SetU16(data, offset, this.Para.ScanInterval);
            offset += nw_cmd_base.SetU16(data, offset, this.Para.DormancyDuration);
            offset += nw_cmd_base.SetU16(data, offset, this.Para.OnlineTime);
            data[offset++] = (byte)this.Para.Reboot_day;
            data[offset++] = (byte)this.Para.Reboot_hour;
            data[offset++] = (byte)this.Para.Reboot_min;
            offset += this.SetPassword(data, offset, Para.Password);
            msg = Para.ToString();
            return data;
        }
    }
}
