using GridBackGround.Termination;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBackGround.CommandDeal.nw
{
    internal class nw_cmd_0a_device_config_get : nw_cmd_base
    {
        public override int Control { get { return 0x0a; } }

        public override string Name { get { return "查询装置配置参数"; } }

        public nw_device_config Para { get; set; }
        public nw_cmd_0a_device_config_get() { }

        public nw_cmd_0a_device_config_get(IPowerPole pole) : base(pole) { }


        public override int Decode(out string msg)
        {
            if (Data == null || (Data.Length != 2 && Data.Length <10))
                throw new Exception(string.Format("数据域长度错误,应为 2或大于10字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));
            if (this.Data.Length == 2)
            {
                if (Data[0] == 0xff && Data[1] == 0xff)
                    msg = "查询失败。原密码错误";
                else
                    msg = string.Format("设置失败。错误码:{0:X2}{1:X2}H", Data[0], Data[1]);
                return 0;
            }

            if (this.Para == null)
                this.Para = new nw_device_config();
            int value;
            int offset = 0;
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

            if(Data.Length>= 15)
            {
                nw_img_para para_ch1 = new nw_img_para();
                para_ch1.Color = (nw_img_para.EColor)this.Data[offset++];
                para_ch1.Resolution = (nw_img_para.EResolution)this.Data[offset++];
                para_ch1.Brightness = this.Data[offset++];
                para_ch1.Contrast = this.Data[offset++];
                para_ch1.Saturation = this.Data[offset++];
                Para.Img_para_ch1 = para_ch1;
            }
            if(Data.Length >= 20)
            {
                nw_img_para para_ch2 = new nw_img_para();
                para_ch2.Color = (nw_img_para.EColor)this.Data[offset++];
                para_ch2.Resolution = (nw_img_para.EResolution)this.Data[offset++];
                para_ch2.Brightness = this.Data[offset++];
                para_ch2.Contrast = this.Data[offset++];
                para_ch2.Saturation = this.Data[offset++];
                Para.Img_para_ch2 = para_ch2;
            }
            
            if( Data.Length > 20)
            {
                int[] extra_paras = new int[this.Data.Length-20];
                for(int i = 0; i < this.Data.Length - 20; i++)
                {
                    extra_paras[i] = this.Data[offset++];
                }
                Para.Extra_paras = extra_paras;
            }
            msg = this.Para.ToString();
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            //数据域长度为0，数据域为空
            msg = string.Empty;
            return null;
        }
    }
}
