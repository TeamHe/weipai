using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace GridBackGround
{
    public delegate void SerialPortRecS(string msg);
    public delegate void SerialPortSendS(string msg);
    public delegate void SerialPortRecB(byte[] data,int num);
    public delegate void SerialPortSendB(byte[] data,int num);
    public class SerialPortOP
    {
     
        private static SerialPort _serialPort;
        
        private static byte lastByte;
        private Thread readThread ;

        #region 公共变量
        /// <summary>
        /// 串口接收到数据事件，同时以字符串形式上传
        /// </summary>
        public event SerialPortRecS OnRecDataS; 
        /// <summary>
        ///  串口接收到数据事件，同时以byte数组形式上传
        /// </summary>
        public event SerialPortRecB OnRecDataB;
        
        public static event SerialPortSendS OnSendS;

        public static event SerialPortSendB OnSendB;
       
        /// <summary>
        /// 接收到的数据上传格式
        /// </summary>
        public bool HexSendState { get; set; }
        /// <summary>
        /// 串口打开状态
        /// </summary>
        public bool IsOpen
        {
            get { return _serialPort.IsOpen; }
        }
         /// <summary>
        /// 串口名称
        /// </summary>
        public string PortNme
        {
            get { return _serialPort.PortName; }
            set { _serialPort.PortName = value; }
        }
        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate
        {
            get { return _serialPort.BaudRate; }
            set { _serialPort.BaudRate = value; }
        }
        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBit
        {
            get { return _serialPort.StopBits; }
            //set { string tempValue = value;
            //_serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), tempValue);

            set{_serialPort.StopBits = value; }
        }
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits
        {
            get { return _serialPort.DataBits; }
            set { _serialPort.DataBits = value; }
        }
        /// <summary>
        /// 校验位
        /// </summary>
        public string Parity
        {
            get { return _serialPort.Parity.ToString(); }
            set
            {
                string values = value;
                _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), values);
            }

        }
        /// <summary>
        /// 流控制
        /// </summary>
        public string Handshake
        {
           get{return _serialPort.Handshake.ToString();}
           set{string tempvalue = value;
            _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), tempvalue);}
        }        
        #endregion
        /// <summary>
        /// 串口操作类初始化
        /// </summary>
        public SerialPortOP()
        {
            //串口初始化
            _serialPort = new SerialPort();                                        
            _serialPort.Encoding = System.Text.Encoding.GetEncoding("GB2312"); 
            //数据接收线程
            readThread = new Thread(new ThreadStart(Read));                         
            readThread.Name = "串口接收线程";
            readThread.IsBackground = true;
            //数据接收事件
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived);
            
            //默认数据上传形式为字符串
            HexSendState = false;
        }

        #region 串口操作
        /// <summary>
        /// 串口关闭
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                _serialPort.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                _serialPort.Open();
                // readThread.Start();
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获得计算机上可用串口名称
        /// </summary>
        /// <returns></returns>
        public string[] GetPortsName()
        {
            return SerialPort.GetPortNames();
        }
        #endregion

        #region 数据接收

        /// <summary>
        /// 串口数据接收线程
        /// </summary>
        private void Read()
        {
            while (true)  
            {
                //串口关闭
                if (!_serialPort.IsOpen)
                { 
                    TimeSpan waitTime = new TimeSpan(0, 0, 0, 0, 50);  
                    Thread.Sleep(waitTime);
                    continue;
                }
                 //串口打开 
                try  
                {
                    //接收数据
                    byte[] readBuffer = new byte[_serialPort.BytesToRead];
                    _serialPort.Read(readBuffer, 0, readBuffer.Length);
                    if(readBuffer.Length >0)
                        RecDataAyanlise(readBuffer, readBuffer.Length);
                }  
                catch (TimeoutException) { }  
            }  


        }
        /// <summary>
        /// 数据接收事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] readBuffer = new byte[_serialPort.BytesToRead];
            _serialPort.Read(readBuffer, 0, readBuffer.Length);
            RecDataAyanlise(readBuffer, readBuffer.Length);
        }
        /// <summary>
        /// 接收到的数据解析
        /// </summary>
        /// <param name="data">数据解析内容</param>
        /// <param name="count">数据个数</param>
        private void RecDataAyanlise(byte[] data, int count)
        {
            string Data = "";
            //以byte数组形式上传数据
            if (HexSendState)
            {
                if (count > 0)
                    RecData(data, count);
                return;
            }
            //以字符串形式上传数据
            for (int i = 0; i < count; i++)
            { 
                //非汉字字符
                if (data[i] < 0xa1)
                {
                    Data += (char)data[i];
                    lastByte = 0;
                    continue;
                }
                //汉字字符解析
                if (lastByte == 0x00) //汉字第一字节
                {
                    lastByte = data[i];
                    continue;
                }
                //汉字第二字节
                byte[] HexChinese = new byte[2] { lastByte, data[i] };
                lastByte = 0x00;
                //生成汉字
                Data += Encoding.GetEncoding("GB2312").GetString(HexChinese);
            }
            if (Data.Length != 0)
                RecData(Data);
        }
        /// <summary>
        /// 触发接收数据事件
        /// </summary>
        /// <param name="data"></param>
        private void RecData(string data)
        {
            if(data.Length >0 )
                OnRecDataS(data);
        }
        //触发接收数据事件
        private void RecData(byte[] data, int num)
        {
            if (num > 0)
            {
                OnRecDataB(data,num);
            }
        }
        #endregion

        #region 数据发送
        /// <summary>
        /// 发送字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Send(string data)
        {
            if (_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Write(data);
                    //var send = new Send(_serialPort, data);
                    if (OnSendS != null)
                    {
                        OnSendS(data);
                    }
                    return true;
                }
                catch { return false; }
            }
            return false;
        }
        /// <summary>
        /// 发送byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Send(byte[] data)
        {
            return Send(data, 0, data.Length);
        }
        /// <summary>
        /// 发送指定长度的byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool Send(byte[] data, int offset, int count)
        {
            if (_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Write(data, offset, count);
                    //var send = new Send(_serialPort,data);
                    if (OnSendB != null)
                    {
                        OnSendB(data, data.Length);
                    }
                    return true;
                }
                catch { return false; }
            }
            return false;
        }
        #endregion
       
    }

    public class Send
    {
        public event SerialPortSendS OnSendS;

        public event SerialPortSendB OnSendB;
 

        public Send(SerialPort sp,byte[] data)
        {
            sp.Write(data,0,data.Length);
            OnSendB(data,data.Length);
        }
        public Send(SerialPort sp, string data)
        {
            sp.Write(data);
            OnSendS(data);
        }
    }
}
