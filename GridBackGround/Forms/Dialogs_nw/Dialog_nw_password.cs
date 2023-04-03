using System;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialogs_nw
{
    public partial class Dialog_nw_password : Form
    {
        public string Password_old { 
            get { return this.textBox_password_old.Text; } 
            set { this.textBox_password_old.Text = value; } 
        }

        public string Password_new {
            get { return this.textBox_password_new.Text; }
            set { this.textBox_password_new.Text = value; }
        }
        public Dialog_nw_password()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (textBox_password_old.Text.Length != 4)
            {
                MessageBox.Show("请输入4位原密码");
                return;
            }
            if(this.textBox_password_new.Text.Length  != 4) 
            {
                MessageBox.Show("请输入4位新密码");
                return;

            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
