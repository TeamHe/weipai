using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialog
{
    public partial class Form_UserCon : Form
    {
        #region Public Variable

        public int UserNO 
        { 
            get { return this.comboBox_User.SelectedIndex; } 
            set { this.comboBox_User.SelectedIndex = value; } 
        }
        public string UserPhone 
        {
            get { return this.textBoxPhone.Text; }
            set { this.textBoxPhone.Text = value; }
        }
        #endregion
        public Form_UserCon()
        {
            InitializeComponent();
            this.CancelButton = button_Cancel;
            for (int i = 0; i < 3; i++ )
                this.comboBox_User.Items.Add("用户" +i.ToString());
        }

        private void Form_UserCon_Load(object sender, EventArgs e)
        {
            this.textBoxPhone.TextChanged += new EventHandler(textBoxPhone_TextChanged);
            this.button_OK.Click += new EventHandler(button_OK_Click);
        }

        void button_OK_Click(object sender, EventArgs e)
        {
            string s = this.textBoxPhone.Text;            
            if (s.Length != 11)
            {
                MessageBox.Show("手机号码长度应该为：11位，当前长度为："+this.textBoxPhone.TextLength);
                return;
            }
            foreach (char c in s)
            {
                if (c < '0' || c > '9')
                {
                    MessageBox.Show("手机号码中含有非数字字符，请重新输入！");
                    return;
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        void textBoxPhone_TextChanged(object sender, EventArgs e)
        {
            this.label_PhoneLen.Text = this.textBoxPhone.TextLength.ToString();
            if (this.textBoxPhone.TextLength == 11)
            { this.label_PhoneLen.ForeColor = Color.Green; }
        }


    }
}
