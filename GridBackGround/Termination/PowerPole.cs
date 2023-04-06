using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sodao.FastSocket.SocketBase;
using Sodao.FastSocket.Server;
using System.Timers;
using System.Net;

using ResModel.EQU;
using DB_Operation.EQUManage;
using ResModel;

namespace GridBackGround.Termination
{
    

    public class PowerPole : IPowerPole
    {
        /// <summary>
        /// 指令超时时间
        /// </summary>
        public int OverTime { get; set; } = 3;

        private Timer timer;

        private IConnection connection;

        public object Lock { get; private set; }

        #region Constructors
        /// <summary>
        /// 装置初始化
        /// </summary>
        /// <param name="CMD_ID"></param>
        public PowerPole(string CMD_ID)
        {
            
            if (CMD_ID == null) throw new ArgumentNullException("装置ID");
            this.CMD_ID = CMD_ID;
            this.OnLine = false;
            this.Lock = new object();
            UpstateEqu();
            timer = new System.Timers.Timer(30 * 60 * 1000);
            timer.Elapsed += new ElapsedEventHandler(OutLine);
            timer.AutoReset = true;
        }
        
        #endregion

        #region   IPowerPole Members
        /// <summary>
        /// 装置名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 装置ID
        /// </summary>
        public String CMD_ID
        {
            get;
            private set;
        }

        /// <summary>
        /// TCP终端
        /// </summary>
        public IConnection Connection
        {
            get { return this.connection; }
            private set {
                if(this.connection != null)     //处理连接断开事件
                    this.connection.Disconnected -= Connection_Disconnected;
                this.connection = value;
                if (this.connection != null)
                    this.connection.Disconnected += Connection_Disconnected;

            }
        }

        private void Connection_Disconnected(IConnection connection, Exception ex)
        {
            this.OnLine = false;
            OnLineStateChange();
            this.Connection = null;

        }

        /// <summary>
        /// Udp终端
        /// </summary>
        public UdpSession udpSession
        {
            get;
            private set;
        }
        /// <summary>
        /// 在线状态
        /// </summary>
        public bool OnLine
        {
            get;
            private set;
        }
        /// <summary>
        /// 装置IP
        /// </summary>
        public IPEndPoint IP
        {
            get;
            private set;
        }
        /// <summary>
        /// 装置状态变化事件
        /// </summary>
        public event EventHandler<PowerPoleStateChange> PowerPoleStateChange;

        //public event EventHandler<>
        /// <summary>
        /// 装置信息
        /// </summary>
        public Equ Equ { get; set; }
        /// <summary>
        /// 用户数据
        /// </summary>
        public object UserData { get; set; }
        #endregion


        #region 刷新设备信息
        /// <summary>
        /// 刷新设备信息
        /// </summary>
        public void UpstateEqu()
        {
            try
            {
                //在数据库中查找相关ID
                this.Equ = DB_EQU.GetEqu(CMD_ID);
            }
            catch { }
        }
        #endregion

        #region 更新设备参数
        /// <summary>
        /// 更新设备在线状态
        /// </summary>
        /// <param name="udpSession"></param>
        /// <returns></returns>
        public bool UpdatePowerPole(UdpSession udpSession)
        {
            bool Change = false;
            if (this.Connection != null)
            {
                try
                {
                    this.Connection.BeginDisconnect();
                }
                catch { }
            }            
            if (this.IP != (IPEndPoint)udpSession.RemoteEndPoint)
            {
                //IP端口变化
                IP = (IPEndPoint)udpSession.RemoteEndPoint;
                Change = true;  
            }
            this.udpSession = udpSession;
            Online(Change);
            return false;
        }
        /// <summary>
        /// 更新设备在线状态
        /// </summary>
        /// <param name="iconnection"></param>
        /// <returns></returns>
        public bool UpdatePowerPole(IConnection iconnection)
        {
            bool Change = false;
            this.udpSession = null;
            if(this.Connection != iconnection)
            {
                try
                {
                    if (this.Connection != null)
                        this.Connection.BeginDisconnect();
                }
                catch { }
                this.Connection = iconnection;
                this.IP = (IPEndPoint)iconnection.RemoteEndPoint;
            }
            if(this.OnLine == false)
            {
                this.OnLine = true;
                OnLineStateChange();
            }
            Online(Change);
            return false;
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 定时器超时，设备更新状态下线。并触发设备下线事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutLine(object sender, ElapsedEventArgs e)
        {
            if (OnLine == true)
            {
                this.OnLine = false;
                OnLineStateChange();
            }
           
        }
        /// <summary>
        /// 更新设备定时
        /// </summary>
        /// <param name="state">设备状态变化</param>
        private void Online(bool state)
        {
            if (this.OnLine == false)
            {
                this.OnLine = true;
                state = true;
            }
            timer.Close();
            timer.Start();
            if (state)
                OnLineStateChange();
        }
        /// <summary>
        /// 触发设备更新事件
        /// </summary>
        private void OnLineStateChange()
        {
            if (this.Equ != null)
            {
                if (this.OnLine)
                    Equ.Status = OnLineStatus.Online;
                else
                    Equ.Status = OnLineStatus.Offline;
                DB_EQU.ChangeOnLineState(Equ.Status, Equ.ID);
            }            
            EventHandler<PowerPoleStateChange> handler = PowerPoleStateChange;
            if (handler != null)
            {
                handler(this, new PowerPoleStateChange(this));
            }
        }
        #endregion

        public override string ToString()
        {
            string str = "";
            if (OnLine)
                str += "在线";
            else
                str += "离线";
            str += "\n设备IP:";
            if (udpSession != null)
                str += udpSession.RemoteEndPoint.ToString();
            if (Connection != null)
                str += Connection.RemoteEndPoint.ToString();

            return this.Equ.ToString() + "\n" + str;
        }

        #region 手动请求拍照处理
        /// <summary>
        /// 拍照请求事件
        /// </summary>
        public event EventHandler<PowerPoleEventArgs> PhotoingResultEventHanlder;

        private bool busy_photoing { get; set; }

        private Timer timer_photoing { get; set; }

        public Error_Code Photiong(int channel,int preseting)
        {
            if (preseting > 255)
                return Error_Code.InvalidPara;
            if (this.OnLine == false)
                return Error_Code.DeviceOffLine;
            if (this.busy_photoing)
                return Error_Code.DeviceBusy;
            if(this.timer_photoing == null)
            {
                this.timer_photoing = new Timer(this.OverTime * 1000);
                this.timer_photoing.AutoReset = false;
                this.timer_photoing.Elapsed += Timer_Photoing_Elapsed;
            }
            this.timer_photoing.Start();

            if (!CommandDeal.Image_Photo_MAN.Set(this.CMD_ID, channel, preseting))
                return Error_Code.DeviceOffLine;

            return Error_Code.Success;
        }

        private void Timer_Photoing_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.OnPhotiongFinish(Error_Code.ResponseOverTime);
        }

        public void OnPhotiongFinish(Error_Code code, string message = null)
        {
            this.busy_photoing = false ;
            PowerPoleEventArgs args = new PowerPoleEventArgs()
            {
                Code = code,
                Message = message
            };
            try
            {
                if (PhotoingResultEventHanlder != null)
                    this.PhotoingResultEventHanlder(this, args);
            }
            catch
            {

            }
        }
        #endregion

        #region 设置拍照时间表
        /// <summary>
        /// 拍照请求事件
        /// </summary>
        public event EventHandler<PowerPoleEventArgs> SetTimeTableResultEventHanlder;

        private bool busy_settimetable { get; set; }

        private Timer timer_settimetable { get; set; }

        public Error_Code SetTimeTable(int channel, List<CommandDeal.IPhoto_Time> table)
        {
            if (this.OnLine == false)
                return Error_Code.DeviceOffLine;
            if (this.busy_settimetable)
                return Error_Code.DeviceBusy;
            if (this.timer_settimetable == null)
            {
                this.timer_settimetable = new Timer(this.OverTime * 1000);
                this.timer_settimetable.AutoReset = false;
                this.timer_settimetable.Elapsed += Timer_TimeTable_Elapsed;
            }
            this.timer_settimetable.Start();

            if (!CommandDeal.Image_TimeTable.Set(this.CMD_ID, channel, table))
                return Error_Code.DeviceOffLine;

            return Error_Code.Success;
        }

        private void Timer_TimeTable_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.OnTimeTableFinish(Error_Code.ResponseOverTime);
        }

        public void OnTimeTableFinish(Error_Code code, string message = null)
        {
            this.busy_settimetable = false;
            if (timer_settimetable != null) {
                this.timer_settimetable.Stop();
                this.timer_settimetable = null;
            }
            PowerPoleEventArgs args = new PowerPoleEventArgs()
            {
                Code = code,
                Message = message
            };
            //try
            //{
                if (SetTimeTableResultEventHanlder != null)
                    this.SetTimeTableResultEventHanlder(this, args);
            //}
            //catch(Exception ex)
            //{

            //}
        }
        #endregion

        #region 打开声光报警
        public event EventHandler<PowerPoleEventArgs> VoiceLightAlarmEventHanlder;
        /// <summary>
        /// 声光报警busy标记
        /// </summary>
        private bool busy_VoiceLightAlarm { get; set; }
        /// <summary>
        /// 声光报警指令定时器
        /// </summary>
        private Timer timer_VoiceLightAlarm { get; set; }

        /// <summary>
        /// 发送声光报警指令
        /// </summary>
        /// <param name="index">录音文件索引，从0x01开始计数</param>
        /// <param name="status">控制播放状态：①0x01：开始播放②0x02：停止播放</param>
        /// <param name="interval">持续播放时间(单位秒)</param>
        /// <returns>错误码</returns>
        public Error_Code VoicePlay(int index, int status,int interval)
        {
            if (status != 0x02)
                status = 0x01;
            if (this.OnLine == false)
                return Error_Code.DeviceOffLine;
            if (this.busy_VoiceLightAlarm)
                return Error_Code.DeviceBusy;
            if (this.timer_VoiceLightAlarm == null)
            {
                this.timer_VoiceLightAlarm = new Timer(this.OverTime * 1000);
                this.timer_VoiceLightAlarm.AutoReset = false;
                this.timer_VoiceLightAlarm.Elapsed += Timer_VoiceLightAlarm_Elapsed;
            }
            this.timer_VoiceLightAlarm.Start();

            if (!CommandDeal.Command_sound_light_alarm.Option1(this.CMD_ID,(CommandDeal.Command_sound_light_alarm.Play)status,index,interval))
                return Error_Code.DeviceOffLine;

            return Error_Code.Success;
        }

        private void Timer_VoiceLightAlarm_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.OnVoiceLightAlarmFinish(Error_Code.ResponseOverTime);
        }

        public void OnVoiceLightAlarmFinish(Error_Code code, string message = null)
        {
            this.busy_VoiceLightAlarm = false;
            if (timer_VoiceLightAlarm != null)
            {
                this.timer_VoiceLightAlarm.Stop();
                this.timer_VoiceLightAlarm = null;
            }
            PowerPoleEventArgs args = new PowerPoleEventArgs()
            {
                Code = code,
                Message = message
            };
            try
            {
                if (VoiceLightAlarmEventHanlder != null)
                    this.VoiceLightAlarmEventHanlder(this, args);
            }
            catch
            {

            }
        }

        #endregion
    }
}
