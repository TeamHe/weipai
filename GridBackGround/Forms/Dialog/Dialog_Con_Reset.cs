using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridBackGround
{
    public partial class Form_Reset : Form
    {
        public Form_Reset()
        {
            InitializeComponent();
            this.CenterToParent();

            this.Mode = 0x00;
            radioButton1.Checked = true;
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        public byte Mode { get; private set; }
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void RadioBution_CheckChangeed(object sender, EventArgs e)
        {
            String answer = ((RadioButton)sender).Text;

            switch (answer)
            {
                case "安全初始化模式":
                    this.Mode = 0x01;
                    break;
                case "密文通讯模式":
                    this.Mode = 0x02;
                    break;
                case "明文通讯模式":
                    this.Mode = 0x03;
                    break;
                case "工厂调测模式":
                    this.Mode = 0x04;
                    break;

            }
        }

        private void Form_Reset_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.button_OK;
            this.CancelButton = this.button_Cancel;
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        
    }
}
