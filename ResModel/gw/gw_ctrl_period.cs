﻿using System.ComponentModel;
using System.Text;
using Tools;

namespace ResModel.gw
{
    public class gw_ctrl_period : gw_ctrl
    {
        public enum EFlag
        {
            [Description("采样周期")]
            MainTime = 0,

            [Description("心跳周期")]
            HearTime ,
        }

        /// <summary>
        /// 采样类型
        /// </summary>
        public gw_func_code MainType { get; set; }

        /// <summary>
        /// 采样周期
        /// </summary>
        public int MainTime { get; set; }

        /// <summary>
        /// 心跳周期
        /// </summary>
        public int HearTime { get; set;}

        /// <summary>
        /// 当采样类型为
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public override string ToString(bool flag)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("采样类型:{0} ", EnumUtil.GetDescription(this.MainType));
            if (flag || this.GetFlag((int)EFlag.MainTime))
                sb.AppendFormat("采样周期:{0}min ", this.MainTime);
            if (flag || this.GetFlag((int) EFlag.HearTime))
                sb.AppendFormat("心跳周期:{0}min ", this.HearTime);
            return sb.ToString();
        }

    }
}