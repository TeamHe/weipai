using GridBackGround.CommandDeal.nw;
using System;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialogs_nw
{
    /// <summary>
    /// 图像采集参数设置
    /// </summary>
    public partial class Dialog_nw_image_para : Form
    {

        public Dialog_nw_image_para()
        {
            InitializeComponent();
            this.CenterToParent();
        }
        

        public string Password
        {
            get { return this.textBox_password.Text; }
            set { this.textBox_password.Text = value; }
        }

        public nw_img_para para_ch1
        {
            get { return this.userControl_nw_img_para1.Image_para; }
            set { this.userControl_nw_img_para1.Image_para = value; }
        }

        public nw_img_para para_ch2
        {
            get { return this.userControl_nw_img_para2.Image_para; }
            set { this.userControl_nw_img_para2.Image_para = value; }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
