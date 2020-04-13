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
namespace GridBackGround.Termination
{
    public class PowerPole : IPowerPole
    {
        static System.Timers.Timer timer;

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
            get;
            private set;
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
                Change = true;
                
            }
            this.Connection = iconnection;
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


    }
    /// <summary>
    /// 装置状态变化事件
    /// </summary>
    public class PowerPoleStateChange : EventArgs
    {
        /// <summary>
        /// 设备状态变化
        /// </summary>
        /// <param name="powerPole"></param>
        public PowerPoleStateChange(PowerPole powerPole)
        {
            Power = powerPole;

        }
        /// <summary>
        /// 发送状态变化的节点
        /// </summary>
        public PowerPole Power{get;set;}
    
    }
}
