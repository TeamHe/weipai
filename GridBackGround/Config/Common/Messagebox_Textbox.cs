using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridBackGround.Common
{
    public partial class Messagebox_Textbox : Form
    {

        #region Public Variables
        public string FormName
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        /// <summary>
        /// 输入框名称
        /// </summary>
        public string TextName
        {
            get { return this.label1.Text; }
            set { this.label1.Text = value; }
        }
        /// <summary>
        /// 输入框内容
        /// </summary>
        public string Value
        {
            get { return this.textBox1.Text; }
            set { this.textBox1.Text = value; }
        }

        #endregion
        /// <summary>
        /// 初始化
        /// </summary>
        public Messagebox_Textbox()
        {
            Init();
           
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="formName">窗体显示名称</param>
        public Messagebox_Textbox(string formName)
        {
            Init();
            this.Text = FormName;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="formName">窗体显示名称</param>
        /// <param name="name">输入框提示信息</param>
        public Messagebox_Textbox(string formName, string name)
        {
            this.Init();
            this.Text = formName;
            this.TextName = name; 
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="formName">窗体显示名称</param>
        /// <param name="name">输入框提示信息</param>
        /// <param name="value">输入框内容</param>
        public Messagebox_Textbox(string formName,string name,string value)
        {
            this.Init();
            this.Text = formName;
            this.TextName = name;
            this.Value = value;
        }

        private void Init()
        {
            InitializeComponent();
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
