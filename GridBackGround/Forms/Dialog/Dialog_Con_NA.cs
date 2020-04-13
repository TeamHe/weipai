using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace GridBackGround.Forms
{
    public partial class Dialog_Con_NA : Form
    {
        #region 公共变量
        /// <summary>
        /// IP地址
        /// </summary>
        public IPAddress IP { get; set; }       //IP地址
        /// <summary>
        /// 子网掩码
        /// </summary>
        public IPAddress Subnet_Mask { get; set; }       //
        /// <summary>
        /// /网关
        /// </summary>
        public IPAddress Gateway { get; set; }
        /// <summary>
        /// 手机串号
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 设置标识位
        /// </summary>
        public int Flag { get; set; }
        #endregion

        #region Private Veriable
        //Tool.TextBoxIP textBoxIP;
        //ToolTip tip;
        #endregion

        #region construction
        public Dialog_Con_NA()
        {
            InitializeComponent();
            this.CenterToParent();
            ControlInit();
            Flag = 0;
        }
        /// <summary>
        /// 加载时的初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_Con_NA_Load(object sender, EventArgs e)
        {
            this.CancelButton = this.button_Cancel;
            this.AcceptButton = this.button_OK;
            if (IP != null)
            {
                this.textBox_IP.Text = IP.ToString();
            }
            if (Subnet_Mask != null)
            {
                this.textBox_SubNetMask.Text = Subnet_Mask.ToString();
            }
            if (Gateway != null)
            {
                this.textBox_GateWay.Text = Gateway.ToString();
            }
            if (PhoneNumber != null)
            {
                this.textBox_IMEI.Text = PhoneNumber;
            }
        }
        #endregion

        #region 未使用
        private void ControlInit()
        {
            //textBoxIP = new Tool.TextBoxIP();

            //textBoxIP.Location = new System.Drawing.Point(121, 134);
            //textBoxIP.Size = new Size(300, 21);
            //textBoxIP.TabIndex = 10;
            //this.panel1.Controls.Add(textBoxIP);
            //textBoxIP.Show();
            //tip = new ToolTip();
            //tip.SetToolTip(maskedTextBox1,"IP地址");
            //maskedTextBox1.ValidatingType = typeof(System.Net.IPAddress);
            //maskedTextBox1.TypeValidationCompleted += new TypeValidationEventHandler(maskedTextBox1_TypeValidationCompleted);
        }
        /*
         *  说明：
         *         IP输入框文本验证结果
         */
        //void maskedTextBox1_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    if (!e.IsValidInput)
        //    {
        //        tip.ToolTipTitle = "Invalid IP Address";
        //        tip.Show("The data you supplied must be a valid date in the format mm/dd/yyyy.", maskedTextBox1, 0, -20, 5000);
        //    }
        //}
        //private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyValue == 110 || e.KeyValue == 190)//小键盘的.是110，主键盘的.是190,按.后跳到下一段
        //        switch (maskedTextBox1.SelectionStart)
        //        {
        //            case 0:
        //            case 1:
        //            case 2:
        //            case 3:
        //                //Text.Split
        //                maskedTextBox1.SelectionStart = 4;
        //                break;
        //            case 4:
        //            case 5:
        //            case 6:
        //            case 7:
        //                maskedTextBox1.SelectionStart = 8;
        //                break;
        //            case 8:
        //            case 9:
        //            case 10:
        //            case 11:
        //                maskedTextBox1.SelectionStart = 12;
        //                break;
        //            default:
        //                break;
        //        }
        //}
        #endregion
       
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            int flag = 0;
            #region IP地址
            if (this.checkBox_IP.Checked)
            {
                flag += 1;
                try
                {
                    IP = IPAddress.Parse(this.textBox_IP.Text);
                }
                catch
                {
                    MessageBox.Show("请输入正确的IP地址");
                }
            }
            #endregion

            #region 子网掩码
            if (this.checkBox_SubNetMask.Checked)
            {
                flag += 2;
                try
                {
                    Subnet_Mask = IPAddress.Parse(this.textBox_SubNetMask.Text);
                }
                catch
                {
                    MessageBox.Show("请输入正确的子网掩码");
                }
            }
            #endregion

            #region 网关
            if (this.checkBox3_GateWay.Checked)
            {
                flag += 4;
                try
                {
                    Gateway = IPAddress.Parse(this.textBox_GateWay.Text);
                }
                catch
                {
                    MessageBox.Show("请输入正确的网关");
                }
            }
            #endregion

            #region 手机串号
            if (this.checkBox_IMEI.Checked)
            {
                flag += 8;
                if (this.textBox_IMEI.Text.Length != 20)
                {
                    MessageBox.Show("请输入正确的手机串号长度为20"); return ;
                }
                PhoneNumber = this.textBox_IMEI.Text;

            }
            #endregion

            Flag = flag;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Dispose();
        }

       
    }
}
