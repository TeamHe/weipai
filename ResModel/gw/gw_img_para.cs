using System.ComponentModel;
using System.Text;
using Tools;

namespace ResModel.gw
{
    public class gw_img_para:gw_ctrl
    {
        /// <summary>
        /// 设置标识位
        /// </summary>
        public enum EFlag
        {
            [Description("色彩选择")]
            Color = 0,

            [Description("图像分辨率")]
            Resolution = 1,

            [Description("亮度")]
            Luminance = 2,

            [Description("对比度")]
            Contrast = 3,

            [Description("饱和度")]
            Saturation = 4,
        }

        public enum EColor
        {
            [Description("黑白")]
            BlackWhite = 0,

            [Description("彩色")]
            Colours = 1,
        }

        public enum EResolution
        {
            [Description("320*240")]
            R320_240 = 1,

            [Description("640*480")]
            R640_480 = 2,

            [Description("704*576")]
            R704_576 = 3,

            [Description("800*600")]
            R800_600 = 4,

            [Description("1024*768")]
            R1024_768 = 5,

            [Description("1080*1024")]
            R1280_1024 = 6,

            [Description("1280*720")]
            R1280_720 = 7,

            [Description("1920*1080")]
            R1920_1080 = 8,

            [Description("2560*1440")]
            R2560_1440 = 9,

            [Description("3840*2160")]
            R3840_2160 = 10,
        }

        /// <summary>
        /// 色彩选择类型
        /// </summary>
        public EColor Color { get; set; }

        /// <summary>
        /// 图像分辨率
        /// </summary>
        public EResolution Resolution { get; set; }

        /// <summary>
        /// 亮度
        /// </summary>
        public int Luminance {  get; set; }

        /// <summary>
        /// 对比度
        /// </summary>
        public int Contrast { get; set; }

        /// <summary>
        /// 饱和度
        /// </summary>
        public int Saturation { get; set; }


        public override string ToString(bool flag)
        {
            StringBuilder sb = new StringBuilder();
            if(flag || this.GetFlag(EFlag.Color))
                sb.AppendFormat("色彩选择:{0} ",this.Color.GetDescription());
            if (flag || this.GetFlag(EFlag.Resolution))
                sb.AppendFormat("图像分辨率:{0} ",this.Resolution.GetDescription());
            if (flag || this.GetFlag(EFlag.Luminance))
                sb.AppendFormat("亮度:{0} ", this.Luminance);
            if (flag || this.GetFlag(EFlag.Contrast))
                sb.AppendFormat("对比度:{0} ", this.Contrast);
            if (flag || this.GetFlag(EFlag.Saturation))
                sb.AppendFormat("饱和度:{0} ", this.Saturation);
            return sb.ToString();
        }
    }
}
