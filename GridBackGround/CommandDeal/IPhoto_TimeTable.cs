using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 拍照时间表
    /// </summary>
    public interface IPhoto_TimeTable
    {
        /// <summary>
        /// 时
        /// </summary>
        int Hour { get; set; }
        /// <summary>
        /// 分
        /// </summary>
        int Minute { get; set; }
        /// <summary>
        /// 预置位号
        /// </summary>
        int Presetting_No { get; set; }
        
    }

    /// <summary>
    /// 拍照时间表
    /// </summary>
    public class PhotoTimeTable : IPhoto_TimeTable
    {
        /// <summary>
        /// 时
        /// </summary>
        public int Hour { get; set; }
        /// <summary>
        /// 分
        /// </summary>
        public int Minute { get; set; }
        /// <summary>
        /// 预置位号
        /// </summary>
        public int Presetting_No { get; set; }

        public PhotoTimeTable(int hour, int minute, int preset_No)
        {
            this.Hour = hour;
            this.Minute = minute;
            this.Presetting_No = preset_No;
        }
    }
}
