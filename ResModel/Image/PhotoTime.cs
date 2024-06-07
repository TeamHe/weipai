﻿namespace ResModel.Image
{
    /// <summary>
    /// 拍照时间表
    /// </summary>
    public class PhotoTime
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

        public PhotoTime() { }

        public override string ToString()
        {
            return string.Format("T({0}:{1}) P({2})",
                this.Hour, this.Minute, this.Presetting_No);
        }
    }
}
