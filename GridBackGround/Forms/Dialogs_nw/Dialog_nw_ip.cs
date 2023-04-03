using GridBackGround.CommandDeal.nw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialogs_nw
{
    public partial class Dialog_nw_ip : Form
    {
        /// <summary>
        /// 监控中心信息
        /// </summary>
        public nw_center center {  get; set; }

        public Dialog_nw_ip()
        {
            InitializeComponent();
            this.CenterToParent();
        }


        private void Dialog_nw_ip_Load(object sender, EventArgs e)
        {
            if(this.center != null)
            {
                this.textBox_IP.Text = center.IPAddress.ToString();
                this.textBox_Port.Text = center.Port.ToString();
                this.textBox_phoneno.Text = center.PhoneNumber;
                this.textBox_password.Text = center.Password;
            }
        }


        private void button_OK_Click(object sender, EventArgs e)
        {
            IPAddress ip;
            int port;
            if(IPAddress.TryParse(this.textBox_IP.Text,out ip) == false)
            {
                MessageBox.Show("请输入正确的IP地址");
                return;
            }

            if(int.TryParse(this.textBox_Port.Text,out port) == false
                || port <=0 || port > 65535)
            {
                MessageBox.Show("请输入正确的端口号");
                return;
            }

            string phoneNum = this.textBox_phoneno.Text;
            if (System.Text.RegularExpressions.Regex.IsMatch(phoneNum, @"^1[3-9]\d{9}$") == false)
            {
                MessageBox.Show("请输入正确的11位电话号码");
                return;
            }

            if(this.textBox_password.Text.Length != 4)
            {
                MessageBox.Show("请输入4位密码");
            }

            if(this.center == null)
                this.center = new nw_center();
            this.center.IPAddress = ip;
            this.center.Password = this.textBox_password.Text;
            this.center.Port = port;
            this.center.PhoneNumber = phoneNum;

            this.DialogResult = DialogResult.OK;
        }
    }
}
