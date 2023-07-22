using System.ComponentModel;
using Tools;

namespace ResModel.nw
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
            R_1024_768 = 5,

            [Description("1280 X 1024")]
            R_1280_1024 = 6,

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


}
