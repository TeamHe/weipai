using GridBackGround.Termination;
using System;
using System.ComponentModel;
using Tools;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_img_para
    {
        /// <summary>
        /// 色彩原则枚举
        /// </summary>
        public enum EColor
        {
            [Description("黑白")]
            Black = 0,

            [Description("彩色")]
            Color = 1,
        }

        /// <summary>
        /// 图像分辨率
        /// </summary>
        public enum EResolution
        {
            [Description("320 X 240")]
            R_320_240 = 1,

            [Description("640 X 480")]
            R_640_480 = 2,

            [Description("704 X 576")]
            R_704_576 = 3,

            [Description("800 X 600")]
            R_800_600 = 4,              


            [Description("1024 X 768")]
            R_1024_768 =5,

            [Description("1280 X 1024")]
            R_1280_1024 =6,

            [Description("1280 X 720")]
            R_1280_720 = 7,

            [Description("920 X 1080")]
            R_1920_1080 = 8,

            [Description("960H(960 x 576)")]
            R_960_576 = 9,

            [Description("960P(1280 x 960)")]
            R_1280_960 = 10,

            [Description("1200P(1600 x 1200)")]
            R_1600_1200 = 11,

            [Description("QXGA(2048 x 1536)")]
            R_2048_1536 = 12,

            [Description("400W(2592 x 1520)")]
            R_2592_1520 = 13,

            [Description("500W(2592 x 1944)")]
            R_2592_1944 = 14,

            [Description("600W(3072*2048)")]
            R_3072_2048 = 15,

            [Description("800W(4K)(3840 x 2160)")]
            R_3840_2160 = 16,

            [Description("1200W(4000 x 3000)")]
            R_4000_3000 = 17,

            [Description("1600W(4608 x 3456)")]
            R_4608_3456 = 18,

            [Description("QUXGA(3200x2400)")]
            R_3200_2400 = 19,

            [Description("4224 x 3136")]
            R_4224_3136 = 20,
        }

        /// <summary>
        /// 色彩选择
        /// </summary>
        public EColor Color { get; set; }

        /// <summary>
        /// 图像大小-分辨率
        /// </summary>
        public EResolution Resolution { get; set; }

        /// <summary>
        /// 亮度
        /// </summary>
        public int Brightness { get; set; }

        /// <summary>
        /// 对比度
        /// </summary>
        public int Contrast { get; set; }      

        /// <summary>
        /// 饱和度
        /// </summary>
        public int Saturation { get; set; }

        public override string ToString()
        {
            return string.Format("色彩选择:{0} 图像大小:{1} 亮度:{2} 对比度:{3} 饱和度:{4}",
                this.Color.GetDescription(),
                this.Resolution.GetDescription(),
                this.Brightness,
                this.Contrast,
                this.Saturation
                );
        }
    }


    /// <summary>
    /// 南网图像采集参数配置
    /// </summary>
    public class nw_cmd_81_img_para : nw_cmd_base
    {
    
        public override int Control { get { return 0x81; } }

        public override string Name { get { return "图像采集参数配置"; } }

        public nw_img_para Channel1 { get; set; }

        public nw_img_para Channel2 { get; set; }


        public string Passowrd { get; set; }

        public nw_cmd_81_img_para()
        {

        }

        public nw_cmd_81_img_para(IPowerPole pole) : base(pole)
        {

        }


        public int Decode_img_para(byte[] data, int offset, out nw_img_para para)
        {
            para = null;
            if (data.Length - offset < 5)
                return -1;
            int no = offset;
            int value = 0;
            para = new nw_img_para();

            para.Color = (nw_img_para.EColor)data[no++];
            para.Resolution = (nw_img_para.EResolution)data[no++];
            para.Brightness = data[no++];
            para.Contrast = data[no++];
            para.Saturation = data[no++];

            return no - offset;
        }

        public int Encode_img_para(byte[] data, int offset, nw_img_para para)
        {
            int no = offset;
            data[no++] = (byte)para.Color; 
            data[no++] = (byte)para.Resolution;
            data[no++] = (byte)para.Brightness;
            data[no++] = (byte)para.Contrast;
            data[no++] = (byte)para.Saturation;
            return no - offset;
        }


        public override int Decode(out string msg)
        {
            if (Data == null || (Data.Length != 2 && Data.Length != 14))
                throw new Exception(string.Format("数据域长度错误,应为 2或14字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            if (Data.Length == 2)
            {
                if ((Data[0] == 0xff) && (Data[1] == 0xff))
                    msg = "设置失败，密码错误";
                else
                    msg = string.Format("设置失败, 错误代码:{0:X2}{1:X2}H", Data[0], Data[1]);
                return -1;
            }

            int offset = 0;
            offset += this.GetPassword(this.Data, offset, out string password);

            offset += Decode_img_para(this.Data, offset, out nw_img_para channel);
            this.Channel1 = channel;
            offset += Decode_img_para(this.Data, offset, out channel);
            this.Channel2 = channel;

            msg = string.Format("设置成功. 通道1: {0} 通道2:{1}",
                this.Channel1.ToString(),this.Channel2.ToString());
            return 0;

        }

        public override byte[] Encode(out string msg)
        {
            if(this.Channel1 == null || this.Channel2 == null)
                throw new ArgumentNullException("通道参数不能为空");

            msg = string.Empty;
            byte[] data = new byte[14];
            int offset = 0;
            offset += this.SetPassword(data, offset, this.Passowrd);
            offset += this.Encode_img_para(data, offset, this.Channel1);
            offset += this.Encode_img_para(data, offset, this.Channel2);

            msg = string.Format("设置图像采集参数. 通道1:{0} 通道2:{1}",
                this.Channel1.ToString() ,this.Channel2.ToString());    
            return data;
        }
    }
}
