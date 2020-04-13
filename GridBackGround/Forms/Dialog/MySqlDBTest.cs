using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace GridBackGround.Forms.Dialog
{
    public partial class MySqlDBTest : Form
    {
        public MySqlDBTest()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 服务器
        /// </summary>
        public string Server 
        { 
            get { return this.textBox_Server.Text; }
            set { this.textBox_Server.Text = value; }
        }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port
        {
            get { try { return int.Parse(this.textBox_Port.Text); } catch { return 0; } }
            set { this.textBox_Port.Text = value.ToString(); }
        }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName 
        {
            get { return this.textBox_DBName.Text; }
            set { this.textBox_DBName.Text = value; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName 
        {
            get { return this.textBox_UsNamd.Text; }
            set { this.textBox_UsNamd.Text = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            get { return this.textBox_Password.Text; }
            set { this.textBox_Password.Text = value; }
        }
    }
}
