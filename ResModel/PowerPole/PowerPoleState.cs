using ResModel.EQU;
using System;

namespace ResModel.PowerPole
{
    public class PowerPoleState
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

        public event EventHandler<OnLineStatus> StateChagned;

        public PowerPoleState()
        {
            this.HeartPeriod = 300;     //默认心跳周期5分钟
            this.SleepPeriod = 600;     //默认装置休眠10分钟
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
            if (this.StateChagned != null)
            {
                try
                {
                    EventHandler<OnLineStatus> handler = this.StateChagned;
                    handler(this, this.OnLine_State);
                }
                catch (Exception e)
                {
                    Console.WriteLine(this.ToString() + "OnStateChange() fail." + e.ToString());
                }
            }
        }

        /// <summary>
        /// 设置通讯周期
        /// </summary>
        /// <param name="heartPeriod"></param>
        /// <param name="sleepPeriod"></param>
        public void SetPeriod(int heartPeriod,int sleepPeriod)
        {
            this.HeartPeriod = heartPeriod;
            this.SleepPeriod = sleepPeriod;
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

}
