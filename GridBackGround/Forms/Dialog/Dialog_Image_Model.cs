using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridBackGround.Forms
{
    /// <summary>
    /// 图像采集参数设置
    /// </summary>
    public partial class Dialog_Image_Model : Form
    {
        #region 初始化
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
            this.comboBox_Color_Select.SelectedIndex = 1;   //彩色
            this.comboBox_Resolution.SelectedIndex = 2;     //720P
            this.AcceptButton = this.button_OK;
            this.CancelButton = this.button_Cancel;
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        #endregion
        
        #region 公共变量
        /// <summary>
        /// 标识
        /// </summary>
        public int RequestFlag { get; set; }
        /// <summary>
        /// 色彩选择
        /// </summary>
        public int Color_Select
        { 
            get { return this.comboBox_Color_Select.SelectedIndex + 1; }
            set { this.comboBox_Color_Select.SelectedIndex = value - 1; }
        }
        /// <summary>
        /// 自定义图像分辨率
        /// </summary>
        public int Resolution
        { 
            get {
                if (this.comboBox_Resolution.SelectedIndex == 5)
                    return 3;
                return this.comboBox_Resolution.SelectedIndex + 1; 
            }
            set { this.comboBox_Resolution.SelectedIndex = value - 1; }
        }
        /// <summary>
        /// 亮度
        /// </summary>
        public UInt16 Luminance { get; set; }
        /// <summary>
        /// 对比度
        /// </summary>
        public UInt16 Contrast { get; set; }
        /// <summary>
        /// 饱和度
        /// </summary>
        public UInt16 Saturation { get; set; }
        #endregion

        #region 私有函数
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            RequestFlag = 0;
            if (this.checkBox_色彩选择.Checked)
            {
                //色彩选择
                RequestFlag += 1;
            }
            if (this.checkBox_图像分辨率.Checked)
            {
                //自定义图像分辨率
                Resolution = this.comboBox_Resolution.SelectedIndex + 1;
                RequestFlag += 2;
            }
            if (this.checkBox_亮度.Checked)
            {
                try
                {
                    Luminance = UInt16.Parse(this.textBox_Luminance.Text);      //亮度
                }
                catch
                {
                    MessageBox.Show("请输入正确的数字");
                    return;
                }
                if (Luminance > 100 || Luminance == 0)
                {
                    MessageBox.Show("亮度的取值范围为1～100。");
                    return;
                }
                RequestFlag += 4;
            }
            //对比度
            if (this.checkBox_对比度.Checked)
            {
                try
                {
                    Contrast = UInt16.Parse(this.textBox_Contrast.Text);        //对比度
                }
                catch
                {
                    MessageBox.Show("请输入正确的数字");
                    return;
                }
                if (Contrast > 100 || Contrast == 0)
                {
                    MessageBox.Show("对比度的取值范围为1～100。");
                    return;
                }
                RequestFlag += 8;
            }
            if (this.checkBox_饱和度.Checked)
            {
                try
                {
                    Saturation = UInt16.Parse(this.textBox_Saturation.Text);    //饱和度
                }
                catch
                {
                    MessageBox.Show("请输入正确的数字");
                    return;
                }
                if (Saturation > 100 || Saturation == 0)
                {
                    MessageBox.Show("饱和度的取值范围为1～100。");
                    return;
                }
                RequestFlag += 16;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Dispose();
        }

        private void Integer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!(e.KeyChar >= 0x30 && e.KeyChar <= 0x39)) && (e.KeyChar != 0x08))
            { e.Handled = true; }
        }

        private void OnTextChaned(object sender,EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            try
            {
                var value = int.Parse(textbox.Text);
                if (value > 100)
                    textbox.Text = "100";
            }
            catch 
            {
                MessageBox.Show("请输入正确的数字为1-100");
            }
        }
        #endregion
    }
}
