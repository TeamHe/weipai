﻿using System.ComponentModel;

namespace ResModel.gw
{
    /// <summary>
    /// 南网功能代码枚举
    /// </summary>
    public enum gw_func_code
    {
        [Description("气象数据")]
        Weather = 0x01,

        [Description("覆冰数据")]
        ICE=0x07,

        [Description("图像监测")]
        Picture = 0x84,
    }
}