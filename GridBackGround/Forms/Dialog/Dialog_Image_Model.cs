using ResModel.gw;
using System;
using System.Windows.Forms;
using Tools;

namespace GridBackGround.Forms
{
    /// <summary>
    /// 图像采集参数设置
    /// </summary>
    public partial class Dialog_Image_Model : Form
    {
        private gw_img_para para;

        public gw_img_para Para
        {
            get { return para; }
            set
            {
                this.para = value;
                this.Color = para.Color;
                this.Resolution = para.Resolution;
                this.Contrast = para.Contrast;
                this.Luminance = para.Luminance;
                this.Saturation = para.Saturation;

                this.checkBox_色彩选择.Checked = para.GetFlag(gw_img_para.EFlag.Color);
                this.checkBox_图像分辨率.Checked = para.GetFlag(gw_img_para.EFlag.Resolution);
                this.checkBox_亮度.Checked = para.GetFlag(gw_img_para.EFlag.Luminance);
                this.checkBox_对比度.Checked = para.GetFlag(gw_img_para.EFlag.Contrast);
                this.checkBox_饱和度.Checked = para.GetFlag(gw_img_para.EFlag.Saturation);
            }
        }

        /// <summary>
        /// 色彩选择
        /// </summary>
        private gw_img_para.EColor Color
        {
            get { return (gw_img_para.EColor)ComboBoxItem.GetValue(comboBox_Color_Select); }
            set { ComboBoxItem.Set_Value(comboBox_Color_Select, value); }
        }

        private gw_img_para.EResolution Resolution
        {
            get { return (gw_img_para.EResolution)ComboBoxItem.GetValue(comboBox_Resolution); }
            set { ComboBoxItem.Set_Value(comboBox_Resolution, value); }
        }

        private int Contrast
        {
            get
            {
                int val = int.Parse(this.textBox_Contrast.Text);
                if (val < 0 && val > 100)
                    throw new ArgumentOutOfRangeException();
                return val;
            }
            set
            {
                this.textBox_Contrast.Text = value.ToString();
            }
        }

        private int Saturation
        {
            get
            {
                int val = int.Parse(this.textBox_Saturation.Text);
                if (val < 0 && val > 100)
                    throw new ArgumentOutOfRangeException();
                return val;
            }
            set
            {
                this.textBox_Saturation.Text = value.ToString();
            }
        }
        private int Luminance
        {
            get
            {
                int val = int.Parse(this.textBox_Luminance.Text);
                if (val < 0 && val > 100)
                    throw new ArgumentOutOfRangeException();
                return val;
            }
            set
            {
                this.textBox_Luminance.Text = value.ToString();
            }
        }

        public Dialog_Image_Model()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        /// <summary>
        /// 加载时处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_Image_Para_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.button_OK;
            this.CancelButton = this.button_Cancel;
            this.button_Cancel.DialogResult = DialogResult.Cancel;

            ComboBoxItem.Init_items_enum(this.comboBox_Color_Select, typeof(gw_img_para.EColor));
            ComboBoxItem.Init_items_enum(this.comboBox_Resolution, typeof(gw_img_para.EResolution));

            this.Color = gw_img_para.EColor.Colours;
            this.Resolution = gw_img_para.EResolution.R1024_768;
            this.Luminance = 50;
            this.Saturation = 50;
            this.Contrast = 50;

            this.checkBox_色彩选择.Checked = true;
            this.checkBox_图像分辨率.Checked = true;
            this.checkBox_亮度.Checked = true;
            this.checkBox_对比度.Checked = true;
            this.checkBox_饱和度.Checked = true;
        }




        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            if(this.para == null)
                this.para = new gw_img_para();
            para.SetFlag(gw_img_para.EFlag.Color, this.checkBox_色彩选择.Checked);
            para.SetFlag(gw_img_para.EFlag.Resolution, this.checkBox_图像分辨率.Checked);
            para.SetFlag(gw_img_para.EFlag.Luminance,this.checkBox_亮度.Checked);
            para.SetFlag(gw_img_para.EFlag.Contrast,this.checkBox_对比度.Checked) ;
            para.SetFlag(gw_img_para.EFlag.Saturation,this.checkBox_饱和度.Checked);

            if(this.checkBox_色彩选择.Checked)
                para.Color = this.Color;
            if(this.checkBox_图像分辨率.Checked)
                para.Resolution = this.Resolution;
            if (this.checkBox_亮度.Checked)
                try
                {
                    para.Luminance = this.Luminance;
                }
                catch
                {
                    MessageBox.Show("请输入正确的亮度");
                    return;
                }

            if (this.checkBox_对比度.Checked)
                try
                {
                    para.Contrast = this.Contrast;
                }
                catch
                {
                    MessageBox.Show("请输入正确的对比度");
                    return;
                }

            if (this.checkBox_饱和度.Checked)
                try
                {
                    para.Saturation = this.Saturation;
                }
                catch
                {
                    MessageBox.Show("请输入正确的饱和度");
                    return;
                }
            this.DialogResult = DialogResult.OK;
        }
    }
}
