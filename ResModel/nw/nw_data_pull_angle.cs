using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResModel.nw
{
    public class nw_data_pull_angle
    {
        /// <summary>
        /// 最大拉力时刻-拉力
        /// </summary>
        public int Pull_max_pull { get; set; }
        /// <summary>
        /// 最大拉力时刻-风偏角
        /// </summary>
        public double AngleDec_max_pull { get; set; }
        /// <summary>
        /// 最大拉力时刻-倾斜角
        /// </summary>
        public double AngleInc_max_pull { get; set; }


        /// <summary>
        /// 最小拉力时刻-拉力
        /// </summary>
        public int Pull_min_pull { get; set; }
        /// <summary>
        /// 最小拉力时刻-风偏角
        /// </summary>
        public double AngleDec_min_pull { get; set; }
        /// <summary>
        /// 最小拉力时刻-倾斜角
        /// </summary>
        public double AngleInc_min_pull { get; set; }



        /// <summary>
        /// 最大风偏角时刻-拉力
        /// </summary>
        public int Pull_max_angle { get; set; }
        /// <summary>
        /// 最大风偏角时刻-风偏角
        /// </summary>
        public double AngleDec_max_angle { get; set; }
        /// <summary>
        /// 最大风偏角时刻-倾斜角
        /// </summary>
        public double AngleInc_max_angle { get; set; }


        /// <summary>
        /// 最小风偏角时刻-拉力
        /// </summary>
        public int Pull_min_angle { get; set; }
        /// <summary>
        /// 最小风偏角时刻-风偏角
        /// </summary>
        public double AngleDec_min_angle { get; set; }
        /// <summary>
        /// 最小风偏角时刻-倾斜角
        /// </summary>
        public double AngleInc_min_angle { get; set; }

        /// <summary>
        /// 数据时间
        /// </summary>
        public DateTime DataTime { get; set; }

        /// <summary>
        /// 功能单元识别码
        /// </summary>
        public int FuncCode { get; set; }

        public override string ToString()
        {
            return string.Format("时间:{12:G} 功能单元识别码：{13} " +
                                 "最大拉力时刻: 拉力:{0} 风偏角:{1} 倾斜角:{2}  " +
                                 "最小拉力时刻: 拉力:{3} 风偏角:{4} 倾斜角:{5}  " +
                                 "最大风偏角时刻: 拉力:{6} 风偏角:{7} 倾斜角:{8}  " +
                                 "最小风偏角时刻: 拉力:{9} 风偏角:{10} 倾斜角:{11}  ",
                                 Pull_max_pull, AngleDec_max_pull, AngleInc_max_pull,
                                 Pull_min_pull, AngleDec_min_pull, AngleInc_min_pull,
                                 Pull_max_angle, AngleDec_max_angle, AngleInc_max_angle,
                                 Pull_min_angle, AngleDec_min_angle, AngleInc_min_angle,
                                 this.DataTime,this.FuncCode);
        }
    }


}
