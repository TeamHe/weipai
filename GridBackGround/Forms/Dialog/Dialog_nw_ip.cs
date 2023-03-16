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

namespace GridBackGround.Forms.Dialog
{
    public partial class Dialog_nw_ip : Form
    {
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 主站IP
        /// </summary>
        public IPAddress IP { get; set; }   

        /// <summary>
        /// 主站卡号
        /// </summary>
        public string SIM_Number { get; set; }

        public Dialog_nw_ip()
        {
            InitializeComponent();
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

            string phoneNum = this.textBox1.Text;
            if (System.Text.RegularExpressions.Regex.IsMatch(phoneNum, @"^1[3-9]\d{9}$") == false)
            {
                MessageBox.Show("请输入正确的11位电话号码");
                return;
            }

            this.IP = ip;
            this.Port = port;
            this.SIM_Number = phoneNum;
            this.DialogResult = DialogResult.OK;
        }
    }
}
