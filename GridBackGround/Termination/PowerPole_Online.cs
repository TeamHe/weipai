using ResModel.EQU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Web.UI.HtmlControls;

namespace GridBackGround.Termination
{
    public class PowerPole_Online
    {
        /// <summary>
        /// 通讯终端时长
        /// </summary>
        protected int counter_uncom = 0;

        public int OnLineTime { get; set; }

        public int SleepTime { get; set; }

        /// <summary>
        /// 上次通讯时间
        /// </summary>
        public DateTime Time_last_com { get; set; }

        /// <summary>
        /// 当前在线状态
        /// </summary>
        public OnLineStatus OnLine_State { get; private set; }

        /// <summary>
        /// 心跳周期
        /// </summary>
        public int HeartPeriod { get; set; }

        /// <summary>
        /// 休眠时长
        /// </summary>
        public int SleepPeriod { get; set; }

        public event EventHandler<OnLineStatus> OnStateChagne;

        public PowerPole_Online()
        {
            this.HeartPeriod = 10;     //默认心跳周期5分钟
            this.SleepPeriod = 30;     //默认装置休眠10分钟
        }

        public void SetState(OnLineStatus state)
        {
            this.SetState(state, string.Empty);
        }

        /// <summary>
        /// 触发状态切换
        /// </summary>
        /// <param name="state"></param>
        /// <param name="reason"></param>
        public void SetState(OnLineStatus state, string reason)
        {
            if (this.OnLine_State == state)
                return;
            //更新相关计数器
            this.OnLine_State = state;
            switch (state)
            {
                case OnLineStatus.Offline:
                case OnLineStatus.None:
                    this.OnLineTime = 0;
                    this.SleepTime = 0;
                    break;
                case OnLineStatus.Online:
                    this.SleepTime = 0;
                    break;
                case OnLineStatus.Sleep:
                    this.OnLineTime = 0;
                    break;
            }
            //触发状态变化事件
            if (this.OnStateChagne != null)
            {
                try
                {
                    EventHandler<OnLineStatus> handler = this.OnStateChagne;
                    handler(this, this.OnLine_State);
                }
                catch (Exception e)
                {
                    Console.WriteLine(this.ToString() + "OnStateChange() fail." + e.ToString());
                }
            }
        }

        /// <summary>
        /// 秒处理函数
        /// </summary>
        public void OnSec()
        {
            if (this.OnLine_State != OnLineStatus.Offline
                && this.OnLine_State != OnLineStatus.None)
                counter_uncom++;

            switch (this.OnLine_State)
            {
                case OnLineStatus.Offline:
                case OnLineStatus.None:
                    this.OnLineTime = 0;
                    this.SleepTime = 0;
                    break;
                case OnLineStatus.Online:
                    this.OnLineTime++;
                    this.SleepTime = 0;
                    //超过3次心跳又30秒，切换到offline 状态
                    if (this.counter_uncom > this.HeartPeriod * 3 + 30)
                    {
                        this.SetState(OnLineStatus.Offline, DateTime.Now.ToString() + "通讯超时("+this.counter_uncom+")，在线状态由Online切换为Offline");
                    }
                    break;
                case OnLineStatus.Sleep:
                    this.SleepTime++;
                    this.OnLineTime = 0;
                    //超过休眠周期又60秒，切换到offline状态
                    if (this.counter_uncom > this.SleepPeriod + 60)
                    {
                        this.SetState(OnLineStatus.Offline, DateTime.Now.ToString() + "通讯超时(" + this.counter_uncom + ")，在线状态由Sleep切换为Offline");
                    }
                    break;
            }
        }

        public void OnCommunication()
        {
            this.Time_last_com = DateTime.Now;
            this.counter_uncom = 0;
            if (this.OnLine_State == OnLineStatus.Online)
                return;

            // TODO:
            // when receive package the status is Sleep what the status need to be, sleep or online ？
            // 1, stay with sleep update the last comm_time, when outtime check last_comm_time,
            //    if last_comm_time less than comm period,set to be online
            //
            // 2, set it to be  online
            //if(this.OnLine_State != OnLineStatus.Sleep)

            this.SetState(OnLineStatus.Online);
        }
    }

    public class PowerPole_Online_Manager
    {
        private static Dictionary<PowerPole, PowerPole_Online> online_list;

        private static Timer _timer;

        /// <summary>
        /// 注册一个online处理
        /// </summary>
        /// <param name="powerPole"></param>
        /// <param name="online"></param>
        public static void Register(PowerPole powerPole,PowerPole_Online online)
        {
            if (online_list == null)
            {
                online_list = new Dictionary<PowerPole, PowerPole_Online>();
                _timer = new Timer()
                {
                    AutoReset = true,
                    Interval = 1000,
                    Enabled = true,
                };
                _timer.Elapsed += _timer_Elapsed;
            }

            if(online_list.ContainsKey(powerPole))
                online_list[powerPole] = online;
            else
                online_list.Add(powerPole, online);
        }

        /// <summary>
        /// 取消注册Online 处理
        /// </summary>
        /// <param name="powerPole"></param>
        /// <returns></returns>
        public static bool UnRegister(PowerPole powerPole)
        {
            if(online_list ==null)
                return false;
            if(!online_list.ContainsKey(powerPole))
                return false;
            online_list.Remove(powerPole);
            return true;
        }


        /// <summary>
        /// 触发秒处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (online_list == null)
                return;
            foreach(var item in online_list)
            {
                PowerPole_Online online = item.Value;
                online.OnSec();
            }
        }

        private static void TimerProc(object state)
        {
            // The state object is the Timer object.
            Timer t = (Timer)state;
            t.Dispose();
            Console.WriteLine("The timer callback executes.");
        }
    }
}
