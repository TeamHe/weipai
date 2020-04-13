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
    public partial class Dialog_Con_ID : Form
    {
        #region Construction
        /// <summary>
        /// construction
        /// </summary>
        public Dialog_Con_ID()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_ID_Load(object sender, EventArgs e)
        {
            this.CancelButton = button_Cancel;
            this.CenterToParent();
            this.AcceptButton = this.button_OK;
            this.CancelButton = this.button_Cancel;
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.SetFlag = 0;
        }
        #endregion

        #region Public Variable
        /// <summary>
        /// 被测设备ID
        /// </summary>
        public string Component_ID 
        {
            get { return this.textBox_Component_ID.Text; }
            set { this.textBox_Component_ID.Text = value; } 
        }    //被测设备ID
        /// <summary>
        /// 新的装置ID
        /// </summary>
        public string NEW_CMD_ID
        {
            get { return this.textBox_NEW_CMD_ID.Text; }
            set { this.textBox_NEW_CMD_ID.Text = value; }
        }      //设备ID
        /// <summary>
        /// 原始ID
        /// </summary>
        public string Original_ID
        {
            get { return this.textBox_Original_ID.Text; }
            set { this.textBox_Original_ID.Text = value; }
        }      
        
        /// <summary>
        /// 设置标识
        /// </summary>
        public int SetFlag
        {
            get;
            private set;
        }            //设置标识

        #endregion

        /// <summary>
        /// 确定按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            int flag = 0;
            if (checkBox_Component_ID.Checked)    //设置标识位 ____被测设备ID
            {
                flag += 2;
                if(this.textBox_Component_ID.TextLength!=17)
                {
                    MessageBox.Show("请输入正确的被测设备ID，长度为17位！"); 
                    return;
                }    
                this.Component_ID = this.textBox_Component_ID.Text;
            }
            if (checkBox_NEW_CMD_ID.Checked)  //设置标识位——设备ID
            {
                flag += 1;
                if (this.textBox_NEW_CMD_ID.TextLength != 17)
                {
                    MessageBox.Show("请输入正确的装置ID，长度为17位！");
                    return;
                }
                this.NEW_CMD_ID = this.textBox_NEW_CMD_ID.Text;
            }
            if(this.textBox_Original_ID.TextLength != 17)
            {
                MessageBox.Show("请输入正确的原始ID，长度为17位！");
                return;
            }            
            SetFlag = flag;
            this.Original_ID = this.textBox_Original_ID.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void textBox_Component_ID_TextChanged(object sender, EventArgs e)
        {
            this.label_Component_ID.Text = this.textBox_Component_ID.Text.Length.ToString();
        }

        private void textBox_NEW_CMD_ID_TextChanged(object sender, EventArgs e)
        {
            this.label_ID.Text = this.textBox_NEW_CMD_ID.Text.Length.ToString();
        }

        private void textBox_Original_ID_TextChanged(object sender, EventArgs e)
        {
            this.label_OrgID.Text = this.textBox_Original_ID.Text.Length.ToString();
        }
        

        
    }
}
