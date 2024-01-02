using System;
using System.Collections.Generic;
using System.Timers;
using System.ComponentModel;
using ResModel;
using ResModel.nw;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_progress_update
    {
        private int UpdateEndCount = 0;

        private int UpdateStartCount = 0;

        private Timer timer_start;

        private Timer timer_data;

        private Timer timer_result;

        private int timer_start_interval = 5;

        public int timer_data_interval = 1;

        private int timer_result_interval = 3;

        private int ResultCount;

        public int MaxResultTime { get; set; }


        public IPowerPole pole {  get; set; }

        public nw_update_info Info { get; set; }


        public List<int> PacToSend { get; set; }

        public int Percent { get; set; }

        public nw_UpdateResult Result { get; set; }

        public nw_progress_update()
        {
            this.MaxResultTime = 5 * 60; //默认最大进度申请时间5分钟
            this.PacToSend = new List<int>();

            this.timer_start = new Timer()
            {
                Interval = this.timer_start_interval * 1000,
                AutoReset = true,
            };
            timer_start.Elapsed += timer_start_elapsed;

            this.timer_data = new Timer()
            {
                AutoReset = true,
                Interval = this.timer_data_interval*1000,
            };
            this.timer_data.Elapsed += this.timer_data_callback;

            this.timer_result = new Timer()
            {
                AutoReset = true,
                Interval = this.timer_result_interval * 1000,
            };
            this.timer_result.Elapsed += this.timer_result_elasped;
        }

        /// <summary>
        /// 升级结束操作
        /// </summary>
        public void UpdateFinish()
        {
            this.pole.SetProperty("nw_progress_update", null);
            this.timer_start.Stop();
            this.timer_data.Stop();
            this.timer_result.Stop();
        }
        /// <summary>
        /// 获取正在更新的流程
        /// </summary>
        /// <param name="pole"></param>
        /// <returns></returns>
        public static nw_progress_update GetCurrentUpdate(IPowerPole pole)
        {
            return pole.GetProperty("nw_progress_update") as nw_progress_update;
        }

        /// <summary>
        /// 添加待发送数据包编号
        /// </summary>
        /// <param name="pacno"></param>
        public void AddToSendPackage(int pacno)
        {
            if (this.PacToSend == null)
                this.PacToSend = new List<int>();
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
            if(this.PacToSend.Count > 0)
            {
                val = this.PacToSend[0];
                this.PacToSend.RemoveAt(0);
            }
            return val;
        }

        #region 发送更新相关数据包
        public bool SendUpdateStart()
        {
            nw_cmd_ca_request_update cmd = new nw_cmd_ca_request_update()
            {
                Pole = this.pole,
                Password = this.Info.Password,
                ChannoNo = this.Info.ChannelNO,
                FileName = this.Info.FileName,
                PacNum = this.Info.PacNum,
                UpdateType = (int)this.Info.Type,
            };
            return cmd.Execute();
        }

        /// <summary>
        /// 发送升级文件
        /// </summary>
        /// <param name="pno"></param>
        public bool SendUpdateFile(int pno)
        {
            byte[] data = this.Info.GetPacData(pno);

            nw_cmd_cb_update_data cmd = new nw_cmd_cb_update_data()
            {
                Pole = this.pole,
                ChannoNo = this.Info.ChannelNO,
                PNO = pno,
                UpdateData = data,
            };
            return cmd.Execute();
        }

        /// <summary>
        /// 发送升结束包
        /// </summary>
        public bool SendUpdateEnd()
        {
            nw_cmd_cc_update_end cmd = new nw_cmd_cc_update_end()
            {
                Pole = this.pole,
                ChannelNo = this.Info.ChannelNO,
            };
            return cmd.Execute();
        }

        public bool SendUpdateResult()
        {
            nw_cmd_ce_update_result cmd = new nw_cmd_ce_update_result()
            {
                Pole = this.pole,
                ChannelNo = this.Info.ChannelNO,
            };
            return cmd.Execute();
        }
        #endregion

        protected void timer_start_elapsed(object sender, EventArgs e)
        {
            this.UpdateStartCount++;
            ///启动更新超过次数，退出更新
            if (UpdateStartCount > 4)
                this.UpdateFinish();
            else
            {
                //数据包发送失败退出更新
                if (!this.SendUpdateStart())
                    UpdateFinish();
            }
        }

        /// <summary>
        /// 定时处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_data_callback(object sender, EventArgs e)
        {
            int pno = this.RemoveFirstToSendPackage();
            if(pno >= 0)
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

        public void timer_result_elasped(object sender, EventArgs e)
        {
            this.ResultCount++;
            if (this.ResultCount > this.MaxResultTime / this.timer_result_interval)
                this.UpdateFinish();
            else
                this.SendUpdateResult();
        }

        public void StartUpdate()
        {
            nw_progress_update update = GetCurrentUpdate(this.pole);
            if (update != null)
            {
                update.UpdateFinish();
            }

            int pnum = this.Info.GetPacNum();
            if (pnum <= 0)
                throw new Exception("没有待发送数据包");
            for (int i = 0; i < pnum; i++)
                this.AddToSendPackage(i);

            this.pole.SetProperty("nw_progress_update", this);
            timer_start.Start();
            if (this.SendUpdateStart())
                this.UpdateStartCount = 0;
            else
                this.UpdateFinish();
        }

        public void Start_DataPackage()
        {
            this.timer_start.Stop();
            this.timer_data.Interval = this.timer_data_interval * 1000;
            this.UpdateEndCount = 0;
            if (this.PacToSend.Count > 0)
                this.timer_data.Start();
            else
                this.UpdateFinish();
        }

        /// <summary>
        /// 下载完成处理函数式
        /// </summary>
        public void DownloadFinish()
        {
            this.timer_data.Stop();
            this.timer_result.Start();
            this.ResultCount = 0;
        }

        /// <summary>
        /// 更新升级进度
        /// </summary>
        /// <param name="chno"></param>
        /// <param name="percent"></param>
        public void UpdateProgress(int chno, int percent)
        {
            this.Percent = percent;
        }

        /// <summary>
        /// 更新升级结果
        /// </summary>
        /// <param name="chno"></param>
        /// <param name="result"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="beforeVersion"></param>
        /// <param name="afterVersion"></param>
        public void UpdateProgress(int chno, nw_UpdateResult result, 
            DateTime start, DateTime end, 
            string beforeVersion, string afterVersion)
        {
            this.UpdateFinish();
        }
    }
}
