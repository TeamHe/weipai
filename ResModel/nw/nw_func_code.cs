using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResModel.nw
{
    /// <summary>
    /// 南网功能代码枚举
    /// </summary>
    public enum nw_func_code
    {
        [Description("导地线拉力及倾角监测")]
        Pull = 0x22,

        [Description("气象数据监测")]
        Weather = 0x25,

        [Description("设备故障自检")]
        Fault_Detect = 0x30,

        [Description("图像监测")]
        Picture = 0x84,
    }
}
