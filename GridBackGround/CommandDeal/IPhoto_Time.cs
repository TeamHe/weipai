using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 拍照时间表
    /// </summary>
    public interface IPhoto_Time
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
    public class PhotoTime : IPhoto_Time
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

        public PhotoTime(int hour, int minute, int preset_No)
        {
            this.Hour = hour;
            this.Minute = minute;
            this.Presetting_No = preset_No;
        }

        public override string ToString()
        {
            return string.Format("时:{0} 分:{1} 预置位:{2}",
                this.Hour, this.Minute, this.Presetting_No);
        }
    }
}
