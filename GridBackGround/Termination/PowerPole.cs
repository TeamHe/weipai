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
using System.Security.RightsManagement;
using System.ComponentModel;

namespace GridBackGround.Termination
{
    public enum PowerPoleFlag
    {
        None = 0,
        [Description("南网")]
        NW,
        [Description("国网")]
        GW,
    }

    public class PowerPole : IPowerPole
    {
        /// <summary>
        /// 设备标识
        /// </summary>
        public PowerPoleFlag Flag { get; set; }
        /// <summary>
        /// 指令超时时间
        /// </summary>
        public int OverTime { get; set; } = 3;

        private Timer timer;

        private IConnection connection;

        public OnLineStatus OnLine {get;private set;}

        public PowerPoleState State { get; set;}

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
            this.Lock = new object();
            if (CMD_ID.Length == 6)
                this.Flag = PowerPoleFlag.NW;
            if(CMD_ID.Length == 17)
                this.Flag = PowerPoleFlag.GW;
            UpstateEqu();

            ///在线状态模块初始化
            this.State = new PowerPoleState();
            this.State.StateChagned += OnStateChagne;

            Console.WriteLine(DateTime.Now.ToString() + string.Format("Device {0} Created", this.CMD_ID));
        }
        #endregion

        #region  在线状态处理
        /// <summary>
        /// 设备在线状态更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStateChagne(object sender, OnLineStatus e)
        {
            OnLineStatus old = this.OnLine;
            this.OnLine = e;
            if(this.Equ != null && this.Equ.Status != e) {
                this.Equ.Status = e;
                if(e == OnLineStatus.Online || e == OnLineStatus.Offline)
                    DB_EQU.ChangeOnLineState(Equ.Status, Equ.ID);
            }
            EventHandler<PowerPoleStateChange> handler = PowerPoleStateChange;
            if (handler != null)
            {
                handler(this, new PowerPoleStateChange(this));
            }
            Console.WriteLine(DateTime.Now.ToString() + string.Format(" Device {0} 在线状态Change: from {1} to {2}",this.CMD_ID,old,this.OnLine));
        }

        /// <summary>
        /// 设置在线状态
        /// </summary>
        /// <param name="status"></param>
        public void SetOnlineState(OnLineStatus status)
        {
            if(this.State != null)
                this.State.SetState(status);
            else
                this.OnStateChagne(null,status);
        }

        /// <summary>
        /// 获取当前是否在线
        /// </summary>
        /// <returns></returns>
        public bool is_online()
        {
            return (this.OnLine == OnLineStatus.Online || this.OnLine == OnLineStatus.Sleep);
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
                    this.connection.Disconnected -= OnDisconnected;
                this.connection = value;
                if (this.connection != null)
                    this.connection.Disconnected += OnDisconnected;
            }
        }

        /// <summary>
        /// TCP 链接断开事件处理
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="ex"></param>
        private void OnDisconnected(IConnection connection, Exception ex)
        {
            this.Connection = null;
            this.SetOnlineState(OnLineStatus.Offline);
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

        public bool SendSocket(IPowerPole pole, byte[] data, out string msg)
        {
            return PackeDeal.SendSocket(pole, data, out msg);
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
            }
            this.udpSession = udpSession;
            this.State.OnCommunication();
            return false;
        }
        /// <summary>
        /// 更新设备在线状态
        /// </summary>
        /// <param name="iconnection"></param>
        /// <returns></returns>
        public bool UpdatePowerPole(IConnection iconnection)
        {
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
            this.State.OnCommunication();
            return false;
        }
        #endregion

        #region 私有函数
        /// <summary>
        #endregion

        public override string ToString()
        {
            string str = "";
            str += this.OnLine.ToString();
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
            if (!is_online())
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
            if (!is_online())
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
        public int Pole_id { get ; set ; }

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
            if (!is_online())
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
