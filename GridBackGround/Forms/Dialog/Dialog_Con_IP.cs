using ResModel.gw;
using System;
using System.Net;
using System.Windows.Forms;

namespace GridBackGround.Forms
{
    /// <summary>
    /// IP配置界面
    /// </summary>
    public partial class Dialog_Con_IP : Form
    {
        #region construction
        /// construction
        /// </summary>
        public Dialog_Con_IP()
        {
            InitializeComponent();
            this.CenterToParent();
        }
        /// <summary>
        /// 窗体加载时初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_Con_IP_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.button_OK;
            this.CancelButton = this.button_Cancle;
            this.button_Cancle.DialogResult = DialogResult.Cancel;
        }
        #endregion

        public gw_ctrl_center center;

        public gw_ctrl_center Center
        {
            get{ return this.center; }
            set
            {
                this.center = value;
                if (center == null) return;

                this.checkBox_domain.Checked = center.GetFlag((int)gw_ctrl_center.EFlag.Domain);
                this.checkBox_IP.Checked = center.GetFlag((int)gw_ctrl_center.EFlag.IP);
                this.checkBox_Port.Checked = center.GetFlag((int)gw_ctrl_center.EFlag.Port);

                this.textBox_IP.Text = this.center.IP.ToString();
                this.textBox_Port.Text = this.center.Port.ToString();
                this.textBox_domain.Text = this.center.Domain.ToString();
            }
        }

        /// <summary>
        /// 设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            if(this.center == null)
                this.center = new gw_ctrl_center();
            this.center.Flag = 0;
            this.center.SetFlag((int)gw_ctrl_center.EFlag.Port, this.checkBox_Port.Checked);
            this.center.SetFlag((int)gw_ctrl_center.EFlag.IP, this.checkBox_IP.Checked);
            this.center.SetFlag((int)gw_ctrl_center.EFlag.Domain, this.checkBox_domain.Checked);

            if(this.center.Flag == 0)
            {
                MessageBox.Show("您没有选中任何参数");
                return;
            }

            if (this.checkBox_IP.Checked)
            {
                if(!IPAddress.TryParse(this.textBox_IP.Text,out IPAddress ip))
                {
                    MessageBox.Show("请输入正确的IP地址！");
                    return;

                }
                this.center.IP = ip;
            }

            if (this.checkBox_Port.Checked)
            {
                if (!int.TryParse(this.textBox_Port.Text, out int port) || port > 65535)
                {
                    MessageBox.Show("请输入正确的端口号");
                    return;
                }
                this.center.Port = port;
            }
            if (this.checkBox_domain.Checked)
            {
                if(this.textBox_domain.TextLength > gw_ctrl_center.Domain_Max_Length)
                {
                    MessageBox.Show("域名最大长度为64,当前为" + this.textBox_domain.TextLength.ToString());
                    return;
                }
                this.center.Domain = this.textBox_domain.Text;
            }
            this.DialogResult = DialogResult.OK;
        }
        
        /// <summary>
        /// 连接按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_GetIP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string str = PublicIP.MyPublicIP();
            this.textBox_IP.Text = str;
        }
       
        /// <summary>
        /// 端口号输入限制为数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Port_KeyPress(object sender, KeyPressEventArgs e)
        {   
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8) 
            { 
                e.Handled = true; 
            } 
        }
        /// <summary>
        /// 端口号限制输入最大为65535
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Port_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(this.textBox_Port.Text) > 65535)
                {
                    MessageBox.Show("端口号不能大于65535！");
                }
            }
            catch { }
            

        }
        class PublicIP
        {
            public static string MyPublicIP()
            {
                try
                {
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        string html = wc.DownloadString("http://ip.qq.com");
                        System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(html, "<span class=\"red\">([^<]+)</span>");
                        if (m.Success) return m.Groups[1].Value;

                        return "0.0.0.0";
                    }
                }
                catch
                {
                    return "0.0.0.0";
                }
            }
        }
    }

    
}
