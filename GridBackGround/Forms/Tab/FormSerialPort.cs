using System;
using System.Text;
using System.Windows.Forms;
using Tools;
namespace GridBackGround
{
    public partial class FormSerialPort : Form
    {
        
        #region 私有变量
        private SerialPortOP serialPort;
        private bool HexSendState { get; set; }
        private bool HexDisState { get; set; }
        //byte lastByte;

        //装置ID
        private string CMD_ID = null;
        #endregion

        #region 初始化
        
        public FormSerialPort()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.TopLevel = false;
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
            SerialPortOP.OnSendB += new SerialPortSendB(SerialPortOP_OnSendB);
            SerialPortOP.OnSendS += new SerialPortSendS(SerialPortOP_OnSendS);
        }

        void SerialPortOP_OnSendS(string msg)
        {
            //throw new NotImplementedException();
            //需要进行委托处理
            if (this.InvokeRequired)
            {
                //产生一个委托调用，然后修改控件的值
                this.Invoke(new SetTextS(this.serialPort_OnRecDataS), new object[] { msg });
            }
            else
            {
                this.checkBox_HexSend.Checked = false;
                this.textBox_Send.Text = msg;
            }

        }

        void SerialPortOP_OnSendB(byte[] data, int num)
        {
            //throw new NotImplementedException();
            //需要进行委托处理
            if (this.InvokeRequired)
            {
                //产生一个委托调用，然后修改控件的值
                this.Invoke(new SetTextB(this.SerialPortOP_OnSendB), new object[] { data, num });
            }
            else
            {
                this.checkBox_HexSend.Checked = true;
                 this.textBox_Send.Text = StringTrun.byteToHexStr(data);
            }
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
            if (this.comboBox_Name.Items.Count > 0)
            {
                this.comboBox_Name.SelectedIndex = 0;
            }              

            this.comboBox_BaudRate.SelectedItem = "115200";
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
                    //string temp = this.richTextBox1.Text;
                    //for (int i = 0; i < msg.Length; i++)
                    //{
                    //    temp += msg[i];
                    //}
                    //string temp2 = temp;
                    this.richTextBox1.Text += msg;
                    this.richTextBox1.Focus();
                    //设置光标的位置到文本尾 
                    this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
                    //滚动到控件光标处 
                    this.richTextBox1.ScrollToCaret();
                    //this.richTextBox1.AppendText(msg,msg.Length);
                    //string temp = this.richTextBox1.Text;
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
                this.linkLabel1.Enabled = true;
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
                    this.linkLabel1.Enabled = false;
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

        #region  菜单栏

        #region 程序配置
        private void 本机端口设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Forms.Dialog_Config config = new Forms.Dialog_Config();
            //if (config.ShowDialog(this) == DialogResult.OK)
            //{
            //    int port = config.Port;
            //    if (Config.Config.ConfigPort(port))
            //    {

            //        if (Communicat.Service.reStartCom(ref port))
            //            this.toolStripStatusLabel1.Text = port.ToString() + "已成功打开";
            //    }
            //}
        }
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog(this);
        }
        //#region Ta切换
        //private void 显示通讯报文ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    this.tabControl1.SelectedTab = tabControl1.TabPages[0];
        //}

        //private void 显示记录列表ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    this.tabControl1.SelectedTab = tabControl1.TabPages[1];
        //}
        #endregion

        #region 装置配置

        #region 模型参数
        private void 查询模型参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            CommandDeal.Comand_Model.Query(CMD_ID);
        }

        private void 设置模型参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Con_Model model = new Forms.Dialog_Con_Model();
            if (model.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CommandDeal.Comand_Model.Set(CMD_ID, model.modelData);
            }
        }
        #endregion

        #endregion


        #region 图像

        #region 图像采集参数
        private void 查询图像采集参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            CommandDeal.Image_Model.Query(CMD_ID);
        }
        private void 设定图像采集参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Image_Model dip = new Forms.Dialog_Image_Model();
            if (dip.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CommandDeal.Image_Model.Set(CMD_ID,
                    dip.RequestFlag,
                    dip.Color_Select,
                    dip.Resolution,
                    dip.Luminance,
                    dip.Contrast,
                    dip.Saturation);
            }
        }
        #endregion

        #region 手动请求照片
        /// <summary>
        /// 手动请求照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 手动请求照片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Image_Photo dip = new Forms.Dialog_Image_Photo();
            if (dip.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CommandDeal.Image_Photo_MAN.Set(CMD_ID, dip.Channel_NO, dip.Presetting_No);
            }
        }
        #endregion

        #region 拍照时间表
        private void 设定拍照时间表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Image_TimeTable dit = new Forms.Dialog_Image_TimeTable();
            if (dit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                if (dit.Qurey_State)//查询
                    CommandDeal.Image_TimeTable.Query(CMD_ID, dit.Channel_No);
                else//设定
                    CommandDeal.Image_TimeTable.Set(
                        CMD_ID,
                        dit.Channel_No,
                        dit.TimeTable);
            }
        }
        #endregion

        #region 摄像机远程调节
        private void 摄像机远程调节ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Image_Adjust dia = new Forms.Dialog_Image_Adjust();
            dia.CMD_ID = CMD_ID;
            dia.ShowDialog();
        }
        #endregion


        #endregion

        //#region 远程升级
        //private void 远程升级装置程序ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //进行远程升级
        //    RemoteUpdate();
        //}
        //private void 远程升级装置程序ToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    //进行远程升级
        //    RemoteUpdate();
        //}
        //#endregion

        /// <summary>
        /// 显示微风振动波形窗体显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 微风振动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Dialog_Form df = new Forms.Dialog_Form();
            df.Show();
        }
        /// <summary>
        /// 判断当前设备选中状态
        /// </summary>
        /// <returns></returns>
        private bool GetCMD_SelecState()
        {
            if (serialPort == null)
            {
                MessageBox.Show("串口未进行初始化"); return false; 
            }
            if (!serialPort.IsOpen)
            {
                { MessageBox.Show("串口未打开"); return false; }
            }
            int length = 17;
            if (Config.SettingsForm.Default.ServiceMode == "nw")
                length = 6;
            if (this.textBox_ID.Text.Length != length)
            { MessageBox.Show("装置ID长度错误"); return false; }
            CMD_ID = this.textBox_ID.Text;
            return true;
        }
        #endregion

        public void CloseSerialPort()
        {
            serialPort.Close();
            this.buttonPort.Text = "打开串口";
        }
        private void 工作模式切换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Form_Reset mode = new Form_Reset();
            if (mode.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CommandDeal.Comand_ModeChange.Set(CMD_ID,
                   mode.Mode);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] Names = serialPort.GetPortsName();
            this.comboBox_Name.Items.Clear();
            foreach (string name in Names)
            {
                this.comboBox_Name.Items.Add(name);
            }
            if (this.comboBox_Name.Items.Count > 0)
            {
                this.comboBox_Name.SelectedIndex = 0;
            }              
        }

        private void FormSerialPort_Activated(object sender, EventArgs e)
        {
            //try
            //{ 
            //    this.buttonPort_Click(this,new EventArgs());
            //}
            //catch
            //{
            //}
        }

      
    }

}
