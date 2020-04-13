using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.PacketAnaLysis
{
    public class DataInfo
    {
        /// <summary>
        /// 收发状态
        /// </summary>
        public DataRecSendState state { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquName { get; set; }
        /// <summary>
        /// 命令名称
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// 解析结果
        /// </summary>
        public string AnalyResult { get; set; }
        /// <summary>
        /// 命令发生时间
        /// </summary>
        public DateTime Time { get; set; }
       /// <summary>
       /// 用来存储数据解析结果
       /// </summary>
       /// <param name="dt"></param>
       /// <param name="state"></param>
       /// <param name="pole"></param>
       /// <param name="Command"></param>
       /// <param name="data"></param>
        public DataInfo(
                 DataRecSendState state,
                 Termination.IPowerPole pole,
                 string Command,
                 string data)
        {
            this.state = state;
            this.EquName = "";
            if (pole != null)
            {
                //if (pole.Name != null)
                //    this.EquName = pole.Name + "->";
                this.EquName += pole.CMD_ID;
            }               
            this.Command = Command;
            this.AnalyResult = data;
            this.Time = DateTime.Now;
        }
       
        /// <summary>
        /// 创建数据解析
        /// </summary>
        /// <param name="state"></param>
        /// <param name="equName"></param>
        /// <param name="Command"></param>
        /// <param name="data"></param>
        public DataInfo(
                 DataRecSendState state,
                 string equName,
                 string Command,
                 string data)
        {
            this.state = state;
            this.Command = Command;
            this.EquName = EquName;
            this.AnalyResult = data;
            this.Time = DateTime.Now;
        }
        /// <summary>
        /// 数据解析
        /// </summary>
        /// <param name="state"></param>
        /// <param name="equName"></param>
        /// <param name="cmdID"></param>
        /// <param name="Command"></param>
        /// <param name="data"></param>
        public DataInfo(
                 DataRecSendState state,
                 string equName,
                 string cmdID,
                 string Command,
                 string data)
        {
            this.state = state;
            this.Command = Command;
            if(equName!= null)
                this.EquName = equName + "->";
            this.EquName += cmdID;
            this.AnalyResult = data;
            Time = DateTime.Now;
        }

    }

    /// <summary>
    /// 监测数据报文类型
    /// </summary>
    public enum DataRecSendState
    {
        //
       none = 0,
       //数据接收
       rec = 1,
       //数据发送
       send = 2,
    }
}
