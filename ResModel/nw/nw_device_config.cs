using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResModel.nw
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
                builder.AppendFormat("认证密码:{0} ", this.Password);
            if (Img_para_ch1 != null || Img_para_ch2 != null)
                builder.Append("图像信息: ");
            if (Img_para_ch1 != null)
                builder.AppendFormat("通道1:{0}", this.Img_para_ch1.ToString());
            if (Img_para_ch2 != null)
                builder.AppendFormat("通道2:{0}", this.Img_para_ch2.ToString());

            if (this.Extra_paras != null)
            {
                builder.Append("有效功能:");
                for (int i = 0; i < this.Extra_paras.Length; i++)
                    builder.AppendFormat("{0}:{1} ", i, this.Extra_paras[i]);
            }

            return builder.ToString();
        }
    }

}
