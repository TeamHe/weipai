using GridBackGround.CommandDeal.nw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;

namespace GridBackGround.Forms.Dialogs_nw
{
    public partial class Dialog_nw_send_sms : Form
    {
        /// <summary>
        /// 监控中心信息
        /// </summary>
        public nw_center center {  get; set; }

        public string Password
        {
            get { return this.textBox_password.Text; }
            set { this.textBox_password.Text = value; }
        }

        public string PhoneNumber
        {
            get { return this.textBox_phoneno.Text; }
            set { this.textBox_phoneno.Text = value; }
        }

        public Dialog_nw_send_sms()
        {
            InitializeComponent();
            this.CenterToParent();
        }


        private void button_OK_Click(object sender, EventArgs e)
        {
            string phoneNum = this.textBox_phoneno.Text;
            if (!MetarnetRegex.IsPhone(phoneNum))
            {
                MessageBox.Show("请输入正确的11位电话号码");
                return;
            }

            if(this.textBox_password.Text.Length != 4)
            {
                MessageBox.Show("请输入4位密码");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
