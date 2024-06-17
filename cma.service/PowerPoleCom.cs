using System;
using System.ComponentModel;
using ResModel.nw;
using ResModel;

namespace cma.service
{
    public enum PowerPoleComResult
    {
        [Description("成功")]
        Success = 0,

        [Description("通讯超时")]
        Timeout = 1,

        [Description("设备返回错误")]
        Error = 2,

        [Description("设备离线")]
        Offline = 3,

        [Description("请求取消")]
        RequestCancel = 4,

    }

    public class PowerPoleCom
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public PowerPoleComResult Result { get; set; }

        /// <summary>
        /// 关联设备信息handle
        /// </summary>
        public IPowerPole Pole { get; set; } 

        /// <summary>
        /// 请求数据包
        /// </summary>
        public nw_cmd_base Request { get; set; }

        /// <summary>
        /// 相应数据包
        /// </summary>
        public nw_cmd_base Response { get; set; }

        /// <summary>
        /// 相应数据包指令
        /// </summary>
        public int ResponseCmd { get; set; }

        /// <summary>
        /// 数据包处理完成事件
        /// </summary>
        public event EventHandler OnFinished;

        public event EventHandler OnReady;

        /// <summary>
        /// 系统定时器
        /// </summary>
        private System.Timers.Timer timer;

        /// <summary>
        /// 延时发送时间 单位:秒
        /// </summary>
        public int Delay { get; set; }

        /// <summary>
        /// 标注数据包是否已发送
        /// </summary>
        public  bool Sending { get; set; }

        /// <summary>
        /// 标识当前数据包是否处于就绪(待发送)状态
        /// </summary>
        public bool Ready { get; set; }

        /// <summary>
        /// 发送次数计数器
        /// </summary>
        protected int send_count { get; set; }

        /// <summary>
        /// 最多重发次数
        /// </summary>
        protected int MaxResend { get; set; }

        /// <summary>
        /// 重发间隔
        /// </summary>
        protected int ResendPeriod { get; set; }


        public PowerPoleCom(nw_cmd_base request)
        {
            this.Pole = request.Pole;
            this.Request = request;
            this.timer = new System.Timers.Timer();
            this.timer.Elapsed += Timer_Elapsed;
            this.MaxResend = 1;         //默认重发次数1次
            this.ResendPeriod = 5;      //默认重复发间隔5秒钟
            this.Ready = true;
        }

        public PowerPoleCom(nw_cmd_base request, int delay)
            :this(request)
        {
            if (delay <= 0)
                return;
            this.Ready = false;
            this.Delay = delay;
            timer.Interval = delay * 1000;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //当前处于未就绪状态
            if (this.Ready == false)
            {
                this.Ready = true;
                this.timer.Stop();
                this._Ready();
                return;
            }
            
            //当前重发次数超过最大重发次数
            if (this.send_count >= MaxResend)
            {
                this.timer.Stop();
                this.Finish(PowerPoleComResult.Timeout, null);
                
                return;
            }
            // 指令发送失败，移除当前发送指令
            if (!this.excute_cmd())
            {
                this.timer.Stop();
                this.Finish(PowerPoleComResult.Error, null);
                return;
            }
        }

        public void Finish(PowerPoleComResult result,nw_cmd_base response)
        {
            this.Result = result;
            this.Response = response;
            if(this.OnFinished != null)
            {
                try
                {
                    this.OnFinished(this, new EventArgs());
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Device: {0} OnResponse Exectpion, Request: {1} message:{2}", 
                        this.Pole.CMD_ID, this.Request, e.ToString()));
                }
            }
        }

        private void _Ready()
        {
            if (this.OnReady != null)
                this.OnReady(this, new EventArgs());
        }

        private bool excute_cmd()
        {
            if(!this.Request.Execute())
                return false;
            this.Sending = true;
            this.send_count++;
            timer.Interval = this.ResendPeriod;
            timer.Start();
            return true;
        }

        public bool Execute()
        {
            if (this.Sending) 
                return true;
            return excute_cmd();
        }
    }
}
