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
    public partial class Dialog_Update : Form
    {
        #region Public Variables
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName 
        { 
            get { return this.textBox_File.Text; }
            set { this.textBox_File.Text = value; }
        }
        /// <summary>
        /// 厂商名称
        /// </summary>
        public string Factory_Name 
        { 
            get { return this.textBox_FactoryName.Text; } 
            set { this.textBox_FactoryName.Text = value; } 
        }
        /// <summary>
        /// 厂商型号
        /// </summary>
        public string Model
        {
            get { return this.textBox_Model.Text; }
            set { this.textBox_Model.Text = value; }
        }
        /// <summary>
        /// 硬件版本
        /// </summary>
        public string Hard_Version 
        {
            get { return this.textBox_HardVersion.Text; }
            set { this.textBox_HardVersion.Text = value; }
        }
        /// <summary>
        /// 软件版本
        /// </summary>
        public string Soft_Version
        {
            get { return this.textBox_SoftVersion.Text; }
            set { this.textBox_SoftVersion.Text = value; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime 
        {
            get { return this.dateTimePicker_UpdateTime.Value; }
            set { this.dateTimePicker_UpdateTime.Value = value; }
        }
        #endregion
        
        
        public Dialog_Update()
        {
            InitializeComponent();
            this.CancelButton = this.button_Cancel;
            this.CenterToScreen();
        }

        private void Dialog_Update_Load(object sender, EventArgs e)
        {
            this.button_Browse.Click += new EventHandler(button_Browse_Click);
        }

        void button_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialogRemoteUpdate = new OpenFileDialog();

            OpenFileDialogRemoteUpdate.Filter = "bin文件(*.bin)|*.bin|所有文件(*.*)|*.*";

            if (OpenFileDialogRemoteUpdate.ShowDialog(this) != DialogResult.OK)
                return;
                
            string fullFileName = OpenFileDialogRemoteUpdate.FileName;
            if (fullFileName.IndexOf(".bin") == -1)                             //判断是否为bin文件
            {
                MessageBox.Show("未能识别bin文件");
                return;
            }
            string fileName = System.IO.Path.GetFileName(fullFileName);

            this.textBox_File.Text = fullFileName;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (this.FileName.Length == 0)
            {
                MessageBox.Show("您还未选择文件");
            }
            bool exist = System.IO.File.Exists(this.textBox_File.Text);
            if (!exist)
            {
                MessageBox.Show(this, "文件不存在", "错误", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (System.IO.Path.GetFileName(this.textBox_File.Text).Length > 20)
            {
                MessageBox.Show(this,"文件名长度不能大于20");
                return;
            }
            if (this.textBox_FactoryName.TextLength > 10)
            {
                MessageBox.Show(this, "厂商名称长度不能大于10");
                return;
            }
            if (this.textBox_Model.TextLength > 10)
            {
                MessageBox.Show(this, "设备型号长度不能大于10");
                return;
            }
            if (this.Hard_Version.Length > 4)
            {
                MessageBox.Show(this, "硬件版本长度不能大于4");
                return;
            }
            if (this.Soft_Version.Length > 4)
            {
                MessageBox.Show(this, "软件版本长度不能大于4");
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
