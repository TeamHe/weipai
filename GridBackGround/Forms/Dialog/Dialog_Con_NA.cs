using System;
using System.Windows.Forms;
using System.Net;
using ResModel.gw;
using System.Drawing;

namespace GridBackGround.Forms
{
    public partial class Dialog_Con_NA : Form
    {
        private gw_ctrl_adapter adapter;
        public gw_ctrl_adapter Adapter 
        { 
            get { return this.adapter; }
            set { 
                this.adapter = value;

                this.textBox_IP.Text = string.Empty;
                this.textBox_SubNetMask.Text = string.Empty;
                this.textBox_GateWay.Text = string.Empty;
                this.textBox_DNS.Text = string.Empty;
                this.checkBox_IP.Checked = false;
                this.checkBox_SubNetMask.Checked = false;
                this.checkBox3_GateWay.Checked = false;
                this.checkBox_DNS.Checked = false;

                if (this.adapter != null)
                {
                    this.checkBox_IP.Checked = this.adapter.GetFlag((int)gw_ctrl_adapter.EFlag.IP);
                    this.checkBox_SubNetMask.Checked = this.adapter.GetFlag((int)gw_ctrl_adapter.EFlag.Mask);
                    this.checkBox3_GateWay.Checked = this.adapter.GetFlag((int)gw_ctrl_adapter.EFlag.GateWay);
                    this.checkBox_DNS.Checked = this.adapter.GetFlag((int)gw_ctrl_adapter.EFlag.DNS);

                    if(adapter.IP != null)
                        this.textBox_IP.Text = adapter.IP.ToString();
                    if(adapter.Mask != null)
                        this.textBox_SubNetMask.Text = adapter.Mask.ToString();
                    if(adapter.GateWay != null)
                        this.textBox_GateWay.Text = adapter.GateWay.ToString();
                    if (adapter.DNS != null)
                        this.textBox_DNS.Text = adapter.DNS.ToString();

                }
            } 
        }

        public Dialog_Con_NA()
        {
            InitializeComponent();
            this.CenterToParent();
        }
       
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            IPAddress address;

            if (this.adapter == null)
                this.adapter = new gw_ctrl_adapter();
            this.adapter.Flag = 0;
            this.adapter.SetFlag((int)gw_ctrl_adapter.EFlag.IP,this.checkBox_IP.Checked);
            this.adapter.SetFlag((int)gw_ctrl_adapter.EFlag.Mask,this.checkBox_SubNetMask.Checked);
            this.adapter.SetFlag((int)gw_ctrl_adapter.EFlag.GateWay,this.checkBox3_GateWay.Checked);
            this.adapter.SetFlag((int)gw_ctrl_adapter.EFlag.DNS,this.checkBox_DNS.Checked);

            if (this.checkBox_IP.Checked)
            {
                if(!IPAddress.TryParse(this.textBox_IP.Text,out address))
                {
                    MessageBox.Show("请输入正确的IP地址");
                    return;
                }
                this.adapter.IP = address;
            }

            if (this.checkBox_SubNetMask.Checked)
            {
                if (!IPAddress.TryParse(this.textBox_SubNetMask.Text, out address))
                {
                    MessageBox.Show("请输入正确的子网掩码");
                    return;
                }
                this.adapter.Mask = address;
            }

            if (this.checkBox3_GateWay.Checked)
            {
                if (!IPAddress.TryParse(this.textBox_GateWay.Text, out address))
                {
                    MessageBox.Show("请输入正确的子网掩码");
                    return;
                }
                this.adapter.GateWay = address;
            }

            if (this.checkBox_DNS.Checked)
            {
                if (!IPAddress.TryParse(this.textBox_DNS.Text, out address))
                {
                    MessageBox.Show("请输入正确的DNS服务器");
                    return;
                }
                this.adapter.DNS = address;
            }
            this.DialogResult = DialogResult.OK;
        }

    }
}
