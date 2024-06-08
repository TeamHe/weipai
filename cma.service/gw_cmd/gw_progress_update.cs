using ResModel;
using System.Collections.Generic;
using System;
using System.Timers;
using ResModel.gw_nw;
using ResModel.gw;

namespace cma.service.gw_cmd
{
    public class gw_progress_update
    {
        private int UpdateEndCount = 0;

        private Timer timer_data;

        public int timer_data_interval = 1000;

        public int timer_end_interval = 2000;


        public IPowerPole pole { get; set; }

        public gn_update_info Info { get; set; }

        public DateTime Time { get; set; }

        public List<int> PacToSend { get; set; }

        public gw_progress_update()
        {
            this.PacToSend = new List<int>();
            this.Time = DateTime.Now;
            this.timer_data = new Timer()
            {
                AutoReset = true,
                Interval = this.timer_data_interval,
            };
            this.timer_data.Elapsed += this.timer_data_callback;
        }

        public gw_progress_update(IPowerPole pole):this()
        {
            this.pole = pole;
        }

        /// <summary>
        /// 升级结束操作
        /// </summary>
        public void UpdateFinish()
        {
            this.timer_data.Stop();
            this.pole.SetProperty("gw_progress_update", null);
        }
        /// <summary>
        /// 获取正在更新的流程
        /// </summary>
        /// <param name="pole"></param>
        /// <returns></returns>
        public static gw_progress_update GetCurrentUpdate(IPowerPole pole)
        {
            return pole.GetProperty("gw_progress_update") as gw_progress_update;
        }

        /// <summary>
        /// 添加待发送数据包编号
        /// </summary>
        /// <param name="pacno"></param>
        public void AddToSendPackage(int pacno)
        {
            if (this.PacToSend == null)
                this.PacToSend = new List<int>();
            if(!this.PacToSend.Contains(pacno))
                this.PacToSend.Add(pacno);
        }

        /// <summary>
        /// 获取下一包待发送的数据包编号
        /// </summary>
        /// <returns></returns>
        public int GetNextToSendPackage()
        {
            if (this.PacToSend.Count == 0)
                return -1;
            return this.PacToSend[0];
        }

        /// <summary>
        /// 移除当前待发送数据包编号
        /// </summary>
        /// <returns></returns>
        public int RemoveFirstToSendPackage()
        {
            int val = -1;
            if (this.PacToSend.Count > 0)
            {
                val = this.PacToSend[0];
                this.PacToSend.RemoveAt(0);
            }
            return val;
        }

        #region 发送更新相关数据包
        /// <summary>
        /// 发送升级文件
        /// </summary>
        /// <param name="pno"></param>
        public bool SendUpdateFile(int pno)
        {
            byte[] data = this.Info.GetPacData(pno);

            return new gw_cmd_ctrl_update_data(this.pole)
            {
                UData = new gw_ctrl_update_data()
                {
                    FileName = this.Info.FileName,
                    PNum = this.Info.PacNum,
                    PNo = pno,
                    Data = data
                }
            }.Execute();
        }

        /// <summary>
        /// 发送升结束包
        /// </summary>
        public bool SendUpdateEnd()
        {
            return new gw_cmd_ctrl_update_end(this.pole)
            {
                UEnd = new gw_ctrl_update_end()
                {
                    FileName = this.Info.FileName,
                    PNum = this.Info.PacNum,
                    Time = this.Time,
                }
            }.Execute();
        }
        #endregion

        /// <summary>
        /// 定时处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_data_callback(object sender, EventArgs e)
        {
            int pno = this.RemoveFirstToSendPackage();
            if (pno >= 0)
            {
                if (!SendUpdateFile(pno))
                {
                    //数据发送失败，退出update
                    this.UpdateFinish();
                }

                //没有待发送数据包之后延时2秒发送结束包
                if (this.GetNextToSendPackage() < 0)
                    this.timer_data.Interval = 2000;
            }
            else
            {   //发送结束包
                this.UpdateEndCount++;
                this.timer_data.Interval = 3000;
                if (this.UpdateEndCount > 5)
                    this.UpdateFinish();
                this.SendUpdateEnd();
            }
        }


        public void StartUpdate()
        {
            gw_progress_update update = GetCurrentUpdate(this.pole);
            if (update != null)
            {
                update.UpdateFinish();
            }

            int pnum = this.Info.GetPacNum();
            if (pnum <= 0)
                throw new Exception("没有待发送数据包");
            for (int i = 0; i < pnum; i++)
                this.AddToSendPackage(i);

            this.pole.SetProperty("gw_progress_update", this);
            Start_DataPackage();
        }

        public void Start_DataPackage()
        {
            this.timer_data.Interval = this.timer_data_interval;
            this.UpdateEndCount = 0;
            if (this.PacToSend.Count > 0)
                this.timer_data.Start();
            else
                this.UpdateFinish();
        }
    }
}
