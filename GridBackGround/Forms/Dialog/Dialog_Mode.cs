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
    public partial class Dialog_Mode : Form
    {
        public Dialog_Mode()
        {
            InitializeComponent();
        }

        private void Dialog_Mode_Load(object sender, EventArgs e)
        {
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.radioButton_GCTC.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

    }
}
