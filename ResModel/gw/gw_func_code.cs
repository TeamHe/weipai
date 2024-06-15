using System.ComponentModel;

namespace ResModel.gw
{
    /// <summary>
    /// 国网功能代码枚举
    /// </summary>
    public enum gw_func_code
    {
        [Description("气象数据")]
        Weather = 0x01,

        [Description("覆冰数据")]
        ICE=0x22,

        [Description("杆塔倾斜数据")]
        Inclination = 0xc,
    }

    /// <summary>
    /// 国网参数类型枚举
    /// </summary>
    public enum gw_para_type
    {
        [Description("气象")]
        Weather = 0xaf,

        [Description("杆塔倾斜")]
        Inclination = 0xb0,

        [Description("导地线微风振动")]
        Vibration = 0xb1,

        [Description("导线弧垂")]
        ConductorSag = 0xb2,

        [Description("导线温度")]
        LineTemperature = 0xb3,

        [Description("覆冰")]
        Ice = 0xb4,

        [Description("导线风偏")]
        WindageYaw = 0xb5,

        [Description("导地线舞动")]
        UGallop = 0xb6,

        [Description("现场污秽度")]
        Pollution = 0xb7,
    }
}
