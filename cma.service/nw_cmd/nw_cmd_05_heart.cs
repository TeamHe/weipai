using System;
using ResModel.nw;

namespace cma.service.nw_cmd
{
    /// <summary>
    /// 心跳数据包处理
    /// </summary>
    public class nw_cmd_05_heart : nw_cmd_base
    {
        public override int Control { get { return 0x05; } }

        public override string Name { get { return "装置心跳信息"; } }

        /// <summary>
        /// 信号记录时间
        /// </summary>
        public DateTime DevTime { get; set; }

        /// <summary>
        /// 信号强度
        /// </summary>
        public int qsa { get; set; }

        /// <summary>
        /// 电池电压
        /// </summary>
        public double vbat { get; set; }


        /// <summary>
        /// 接收到的数据包解析
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override int Decode(out string msg)
        {
            DateTime time ;
            msg = null;

            if(this.Data == null || this.Data.Length < 8)
            {
                msg = string.Format("数据域长度错误,应为{0} 实际为:{1}", 8, this.Data.Length);
                return -1;
            }

            this.GetDateTime(this.Data, 0,out time);
            this.DevTime = time;
            this.qsa = this.Data[6];
            this.vbat = (int)(this.Data[7])/10.0;
            
            this.SendCommand(out msg);
            msg = string.Format("装置时间：{0:G} 信号强度:{1} 电池电压:{2}V", this.DevTime, this.qsa, this.vbat) + msg;
            return 0;
        }

        /// <summary>
        /// 构建响应包数据域部分
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override byte[] Encode(out string msg)
        {
            msg = null;
            byte[] rsp = new byte[8];
            this.SetDateTime(rsp, 0, this.DevTime);
            rsp[6] = (byte)this.qsa;
            rsp[7] = (byte)(this.vbat * 10);
            return rsp;
        }
    }
}
