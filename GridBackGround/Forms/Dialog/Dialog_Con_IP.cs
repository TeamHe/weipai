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
            this.button_Cancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            if (IP != null)
            {
                this.textBox_IP.Text = IP.ToString();
                this.textBox_Port.Text = Port.ToString();
            }
            else
            {
                linkLabel_GetIP_LinkClicked(this,new LinkLabelLinkClickedEventArgs(new LinkLabel.Link()));
            }
        }
        #endregion
        /// <summary>
        
        /// <summary>
        /// 上位机端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 上位机IP
        /// </summary>
        public System.Net.IPAddress IP
        {
            get;
            set;
        }
        /// <summary>
        /// 设定标识位
        /// </summary>
        public int SetFlag{get;private set;}
 
        /// <summary>
        /// 设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            int port = 0 ;
            byte[] ipB = new byte[4];
            System.Net.IPAddress ip= new System.Net.IPAddress(ipB);
            int flag = 0;
            if (checkBox_IP.Checked)    //设置标识位——IP
            { 
                flag += 1;
                try
                {
                    ip = System.Net.IPAddress.Parse(textBox_IP.Text);   //获得IP地址
                }
                catch
                {
                    MessageBox.Show("请输入正确的IP地址！");
                    return;
                }
            }
            if (checkBox_Port.Checked)  //设置标识位——端口号
            {
                flag += 2;
                port = Int32.Parse(this.textBox_Port.Text);         //获得端口号
            }

            Port = port;
            IP = ip;
            SetFlag = flag;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
