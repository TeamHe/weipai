﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 国网串口配置工具V2._0
{
    public partial class Form1 : Form
    {
        
        #region 私有变量
        private SerialPortOP serialPort;
        private bool HexSendState { get; set; }
        private bool HexDisState { get; set; }
        byte lastByte;
        #endregion

        #region 初始化
        
        public Form1()
        {
            InitializeComponent();
        }
         /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            SerialInit();

            serialPort.OnRecDataS += new SerialPortRecS(serialPort_OnRecDataS);
            serialPort.OnRecDataB += new SerialPortRecB(serialPort_OnRecDataB);
        }
        /// <summary>
        /// 串口初始化
        /// </summary>
        private void SerialInit()
        { 
            serialPort = new SerialPortOP();
            string[] Names  = serialPort.GetPortsName();

            foreach (string name in Names)
            {
                this.comboBox_Name.Items.Add(name);
            }
            if (this.comboBox_Name.Items.Count <= 0)
            {
                MessageBox.Show("在本机上未检测到串口");
            }
            else
                this.comboBox_Name.SelectedIndex = 0;
            this.comboBox_BaudRate.SelectedItem = "9600";
            this.comboBox_DataBits.SelectedItem = "8";
            this.comboBox_StopBits.SelectedItem = "1";
            this.comboBox_Parity.SelectedItem = "None";
            this.comboBox_Handshake.SelectedItem = "None";
        }
        #endregion

        #region 数据接收
        /// <summary>
        /// 线程异步调用委托
        /// </summary>
        /// <param name="data"></param>
        /// <param name="num"></param>
        public delegate void SetTextB(byte[]  data,int num);
        /// <summary>
        /// 数据接收处理
        /// </summary>
        /// <param name="data"></param>
        /// <param name="num"></param>
        void serialPort_OnRecDataB(byte[] data, int num)
        {
            //需要进行委托处理
            if (this.InvokeRequired)
            {
                //产生一个委托调用，然后修改控件的值
                this.Invoke(new SetTextB(this.serialPort_OnRecDataB), new object[] { data,num });
            }
            else
            {
                if (HexDisState)
                {
                    //this.richTextBox1.Text += StringTrun.byteToHexStr(data);
                    this.richTextBox1.Focus();
                    //设置光标的位置到文本尾 
                    this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
                    //滚动到控件光标处 
                    this.richTextBox1.ScrollToCaret();
                    this.richTextBox1.AppendText(StringTrun.byteToHexStr(data));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        public delegate void SetTextS(string str);
        /// <summary>
        /// 显示接收到的数据
        /// </summary>
        /// <param name="msg"></param>
        void serialPort_OnRecDataS(string msg)
        {
            //需要进行委托处理
            if (this.InvokeRequired)
            {
                //产生一个委托调用，然后修改控件的值
                this.Invoke(new SetTextS(this.serialPort_OnRecDataS), new object[] { msg });
            }
            else
            {
                if (!HexDisState)
                {
                    this.richTextBox1.Focus();
                    //设置光标的位置到文本尾 
                    this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
                    //滚动到控件光标处 
                    this.richTextBox1.ScrollToCaret();
                    this.richTextBox1.AppendText(msg);
                }
            }

        }
        #endregion
        
        /// <summary>
        /// 打开关闭串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPort_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)//串口打开，然后关闭串口
            {
                serialPort.Close();
                this.buttonPort.Text = "打开串口";
            }
            else
            {
                if (this.comboBox_Name.Text.Length < 4)
                {
                    MessageBox.Show("您没有选择任何串口");
                    return;
                }

                if (serialPort.Open())
                {
                    this.buttonPort.Text = "关闭串口";
                }
                else
                {
                    MessageBox.Show("串口打开失败");
                }
            }
        }

        #region 串口配置改变

        /// <summary>
        /// 串口号改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.PortNme = this.comboBox_Name.SelectedItem.ToString();
            //serialPort._serialPort.PortName = this.comboBox_Name.SelectedItem.ToString();
        }
        /// <summary>
        /// 波特率选择改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_BaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.BaudRate = int.Parse(this.comboBox_BaudRate.SelectedItem.ToString());
        }
        /// <summary>
        /// 停止位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_StopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = this.comboBox_StopBits.SelectedItem.ToString();

            switch (temp)
            { 
                case "0":
                    serialPort.StopBit = System.IO.Ports.StopBits.None;
                    break;
                case "1":
                    serialPort.StopBit = System.IO.Ports.StopBits.One;
                    break;
                case "1.5":
                    serialPort.StopBit = System.IO.Ports.StopBits.OnePointFive;
                    break;
                case "2":
                    serialPort.StopBit = System.IO.Ports.StopBits.Two;
                    break;
            }
        }
        /// <summary>
        /// 校验位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Parity_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.Parity = this.comboBox_Parity.SelectedItem.ToString();
        }
        /// <summary>
        /// 数据位改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_DataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.DataBits = int.Parse(this.comboBox_DataBits.SelectedItem.ToString());
        }
        /// <summary>
        /// 流控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Handshake_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.Handshake = this.comboBox_Handshake.SelectedItem.ToString();
        }


        #endregion

        
        #region 发送相关
        /// <summary>
        /// 发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Send_Click(object sender, EventArgs e)
        {
            SendData();
        }
        /// <summary>
        /// 定时发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_TimerSend_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_TimerSend.Checked)
            {
                int time;
                try
                {
                    time = int.Parse(this.textBox_SendTimer.Text);
                }
                catch
                {
                    MessageBox.Show("您输入的时间有误，请输入一个整数");
                    return;
                }
                timer_Send.Interval = time;
                timer_Send.Start();
                this.textBox_SendTimer.Enabled = false;
            }
            else
            {
                timer_Send.Stop();
                this.textBox_SendTimer.Enabled = true;
            }
        }
        /// <summary>
        /// Time定时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Send_Tick(object sender, EventArgs e)
        {
            SendData();
        }
        /// <summary>
        /// 发送发送栏里边的数据
        /// </summary>
        private void SendData()
        { 
            if (HexSendState)
            {
                string temp = this.textBox_Send.Text;
                byte[] bytes = StringTrun.StrToHexByte(temp);
                SerialPortOP.Send(bytes);
            }
            else
            {
                SerialPortOP.Send(this.textBox_Send.Text);
            }
        }
        /// <summary>
        /// Hex发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_HexSend_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_HexSend.Checked)
            {
                this.HexSendState = true;
            }
            else
            {
                this.HexSendState = false;
            }
        }

        #endregion

        #region 显示相关
        /// <summary>
        /// Hex显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_HexDis_CheckedChanged(object sender, EventArgs e)
        {
            string temp;
            byte[] tempBytes;
            string turned ;
            if (this.checkBox_HexDis.Checked)
            {
                this.HexDisState = true;
                serialPort.HexSendState = true;
                tempBytes = Encoding.GetEncoding("GB2312").GetBytes(this.richTextBox1.Text);
                turned = StringTrun.byteToHexStr(tempBytes);
                this.richTextBox1.Text = turned;
            }
            else
            {
                this.HexDisState = false;
                serialPort.HexSendState = false;
                temp = this.richTextBox1.Text;
                tempBytes = StringTrun.StrToHexByte(temp);
                this.richTextBox1.Text = Encoding.Default.GetString(tempBytes);
            }
        }
        #endregion

        
        /// <summary>
        /// 清除窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
        }
    }

}
