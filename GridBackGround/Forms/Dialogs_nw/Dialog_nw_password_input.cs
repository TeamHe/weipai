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
    public partial class Dialog_nw_password_input : Form
    {
        public Dialog_nw_password_input()
        {
            InitializeComponent();
        }

        public string Password 
        {
            get { return this.textBox_password.Text; }  
            set { this.textBox_password.Text = value; } 
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if(this.textBox_password.Text.Length != 4)
            {
                MessageBox.Show("请输入4位装置密码");
                return;

            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
