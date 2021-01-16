﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridBackGround.Forms
{
    public partial class Dialog_Image_Photo : Form
    {
        public Dialog_Image_Photo()
        {
            InitializeComponent();
            this.CenterToParent();
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_NO { get; set; }
        /// <summary>
        /// 预置位号
        /// </summary>
        public int Presetting_No { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            Channel_NO = (int)this.numericUpDown1.Value; ;
            try
            {
                Presetting_No = int.Parse(this.textBox1.Text);
            }
            catch
            {
                MessageBox.Show("请输入正确的预置位号");
                return;
            }
            if (Presetting_No > 255 || Presetting_No == 0)
            {
                MessageBox.Show("请输入正确的预置位号");
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Dispose();
        }

        private void Dialog_Image_Photo_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.button_OK;
            this.CancelButton = this.button_Cancel;
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
