using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResModel.nw
{
    /// <summary>
    /// 气象数据handle
    /// </summary>
    public class nw_data_weather
    {
        /// <summary>
        /// 数据时间
        /// </summary>
        public DateTime DataTime { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public double Temp { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public int Humidity { get; set; }

        /// <summary>
        /// 瞬时风速
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// 瞬时风向
        /// </summary>
        public int Direction { get; set; }

        /// <summary>
        /// 降雨量
        /// </summary>
        public double Rain { get; set; }

        /// <summary>
        /// 大气压力
        /// </summary>
        public int Pressure { get; set; }

        /// <summary>
        /// 日照
        /// </summary>
        public int Sun { get; set; }
        /// <summary>
        /// 1分钟平均风速
        /// </summary>
        public double Speed_1_min { get; set; }

        /// <summary>
        /// 1分钟平均风向
        /// </summary>
        public int Direction_1_min { get; set; }

        /// <summary>
        /// 10分钟平均风速
        /// </summary>
        public double Speed_10_min { get; set; }

        /// <summary>
        /// 10分钟平均风向
        /// </summary>
        public int Direction_10_min { get; set; }

        /// <summary>
        /// 10分钟平均风速
        /// </summary>
        public double Speed_max { get; set; }

        public override string ToString()
        {
            return string.Format("时间:{12:G} 温度:{0} 湿度:{1} 瞬时风速:{2} 瞬时风向:{3} 雨量:{4} 气压:{5} 日照:{6} 1分钟平均风速:{7} 1分钟平均风向:{8} 10分钟平均风速:{9} 10 分钟平均风向:{10} 10分钟最大风速:{11}",
                     this.Temp,
                     this.Humidity,
                     this.Speed,
                     this.Direction,
                     this.Rain,
                     this.Pressure,
                     this.Sun,
                     this.Speed_1_min,
                     this.Direction_1_min,
                     this.Speed_10_min,
                     this.Direction_10_min,
                     this.Speed_max,
                     this.DataTime);
        }
    }


}
