using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialogs_nw
{
    public partial class Dialog_nw_set_cmdid : Form
    {
        public Dialog_nw_set_cmdid()
        {
            InitializeComponent();
        }

        public string Password
        {
            get { return this.textBox_password.Text; }
            set { this.textBox_password.Text = value; }
        }

        public string CMD_ID
        {
            get { return this.textBox_cmdid.Text; }
            set { this.textBox_cmdid.Text = value; }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if(this.textBox_cmdid.TextLength != 6) 
                MessageBox.Show("请输入6位新装置ID");

            if (this.textBox_password.TextLength != 4)
                MessageBox.Show("请输入4位密码");

            this.DialogResult = DialogResult.OK;
        }
    }
}
