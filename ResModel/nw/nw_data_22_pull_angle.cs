﻿using System;

namespace ResModel.nw
{
    public class nw_data_22_pull_angle : nw_data_base
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

        public override int PackLength { get { return 24; } }

        private int GetAngle(byte[] data, int offset, out double value)
        {
            nw_cmd_base.GetS16(data, offset, out int val);
            value = val / 100.0;
            return 2;
        }

        private int SetAngle(byte[] data, int offset, double value)
        {
            int val = (int)(value * 100);
            nw_cmd_base.SetS16(data, offset, val);
            return 2;
        }


        public override int Decode(byte[] data, int offset)
        {
            int no = offset;
            double fvale = 0;
            int value;

            if (data.Length - offset < this.PackLength)
                return -1;

            no += nw_cmd_base.GetU16(data, no, out value);
            this.Pull_max_pull = value;
            no += GetAngle(data, no, out fvale);
            this.AngleDec_max_pull = fvale;
            no += GetAngle(data, no, out fvale);
            this.AngleInc_max_pull = fvale;


            no += nw_cmd_base.GetU16(data, no, out value);
            this.Pull_min_pull = value;
            no += GetAngle(data, no, out fvale);
            this.AngleDec_min_pull = fvale;
            no += GetAngle(data, no, out fvale);
            this.AngleInc_min_pull = fvale;

            no += nw_cmd_base.GetU16(data, no, out value);
            this.Pull_max_angle = value;
            no += GetAngle(data, no, out fvale);
            this.AngleDec_max_angle = fvale;
            no += GetAngle(data, no, out fvale);
            this.AngleInc_max_angle = fvale;

            no += nw_cmd_base.GetU16(data, no, out value);
            this.Pull_min_angle = value;
            no += GetAngle(data, no, out fvale);
            this.AngleDec_min_angle = fvale;
            no += GetAngle(data, no, out fvale);
            this.AngleInc_min_angle = fvale;

            return no - offset;

        }

        public override int Encode(byte[] data, int offset)
        {
            int no = offset;
            if (data.Length - offset < PackLength)
                return -1;

            no += nw_cmd_base.SetU16(data, no, this.Pull_max_pull);            //拉力
            no += this.SetAngle(data, no, this.AngleDec_max_pull);         //
            no += this.SetAngle(data, no, this.AngleInc_max_pull);         //

            no += nw_cmd_base.SetU16(data, no, this.Pull_min_pull);            //拉力
            no += this.SetAngle(data, no, this.AngleDec_min_pull);         //
            no += this.SetAngle(data, no, this.AngleInc_min_pull);         //

            no += nw_cmd_base.SetU16(data, no, this.Pull_max_angle);            //拉力
            no += this.SetAngle(data, no, this.AngleDec_max_angle);         //
            no += this.SetAngle(data, no, this.AngleInc_max_angle);         //

            no += nw_cmd_base.SetU16(data, no, this.Pull_min_angle);            //拉力
            no += this.SetAngle(data, no, this.AngleDec_min_angle);         //
            no += this.SetAngle(data, no, this.AngleInc_min_angle);         //

            return PackLength;

        }

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
                                 this.DataTime,this.UnitNo);
        }
    }


}
