using ResModel;
using System;
using System.Text;
using ResModel.nw;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_device_config
    {
        /// <summary>
        /// 心跳间隔
        /// </summary>
        public int Heart { get; set; }

        /// <summary>
        /// 采集间隔
        /// </summary>
        public int ScanInterval { get; set; }

        public int DormancyDuration { get; set; }

        public int OnlineTime { get; set; }

        public int Reboot_day { get; set; }

        public int Reboot_hour { get; set; }

        public int Reboot_min { get; set; } 

        public string Password { get; set; }

        public nw_img_para Img_para_ch1 { get; set; }

        public nw_img_para Img_para_ch2 { get; set; }

        public int[] Extra_paras { get; set; }


        public nw_device_config() { }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("心跳周期:{0}min 采集间隔:{1}min 休眠时长:{2}min 在线时长:{3}min 重启时间:{4}日 {5}时 {6}分 ",
                this.Heart,
                this.ScanInterval,
                this.DormancyDuration,
                this.OnlineTime,
                this.Reboot_day,
                this.Reboot_hour,
                this.Reboot_min);
            if (this.Password != null)
                builder.AppendFormat("认证密码:{0} ",this.Password);
            if (Img_para_ch1 != null || Img_para_ch2 != null)
                builder.Append("图像信息: ");
            if (Img_para_ch1 != null )
                builder.AppendFormat("通道1:{0}",this.Img_para_ch1.ToString());
            if(Img_para_ch2 != null )
                builder.AppendFormat("通道2:{0}", this.Img_para_ch2.ToString());

            if(this.Extra_paras != null)
            {
                builder.Append("有效功能:");
                for(int i=0;i<this.Extra_paras.Length;i++)
                    builder.AppendFormat("{0}:{1} ", i, this.Extra_paras[i]);
            }

            return builder.ToString();
        }
    }
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

                offset += this.GetU16(this.Data, offset, out value);
                Para.ScanInterval = value;

                offset += this.GetU16(this.Data, offset, out value);
                Para.DormancyDuration = value;

                offset += this.GetU16(this.Data, offset, out value);
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
            offset += this.SetU16(data, offset, this.Para.ScanInterval);
            offset += this.SetU16(data, offset, this.Para.DormancyDuration);
            offset += this.SetU16(data, offset, this.Para.OnlineTime);
            data[offset++] = (byte)this.Para.Reboot_day;
            data[offset++] = (byte)this.Para.Reboot_hour;
            data[offset++] = (byte)this.Para.Reboot_min;
            offset += this.SetPassword(data, offset, Para.Password);
            msg = Para.ToString();
            return data;
        }
    }
}
