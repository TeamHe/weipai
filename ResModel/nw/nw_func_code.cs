using System.ComponentModel;

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

    /// <summary>
    /// TODO:  replace e_nw_function with nw_func_code
    /// </summary>
    public enum e_nw_function
    {
        [Description("导地线拉力及倾角监测功能")]
        Pull_angle = 0x22,

        [Description("绝缘子泄漏电流监测功能")]
        Leak_Current = 0x24,

        [Description("气象数据监测功能")]
        Weather = 0x25,

        [Description("导线温度、电流数据监测功能")]
        Conductor_Temp_Current = 0x26,

        [Description("杆塔振动数据监测功能")]
        Pole_Vibration = 0x027,

        [Description("导线侧倾角监测功能")]
        Line_Angle = 0x28,

        [Description("舞动振幅频率监测功能")]
        Conductor_Galloping = 0x29,

        [Description("杆塔倾斜数据监测功能")]
        Pole_Leaning = 0x2a,

        [Description("导线微风震动数据监测功能")]
        Conductor_Vibration = 0x2b,

        [Description("综合防盗功能")]
        Anti_theft = 0x2c,

        [Description("山火报警功能")]
        Fire_Alarm = 0x2d,

        [Description("大风舞动报警功能")]
        Wind_Galloping_Alarm = 0x2e,

        [Description("设备故障自检功能")]
        Device_Error = 0x30,

        [Description("微风振动动态数据监测功能")]
        Conductor_Vibration_Dynamic = 0x32,

        [Description("舞动动态数据监测功能")]
        Conductor_Galloping_Dynamic = 0x36,

        [Description("污秽数据监测功能")]
        Contamination = 0x41,

        [Description("导线弧垂数据监测功能")]
        Conductor_Droop = 0x42,

        [Description("电缆温度数据监测功能")]
        Conductor_Temp = 0x43,

        [Description("图像监测功能")]
        Image_Tranfer = 0x84,

        [Description("视频监测功能")]
        Video_Tranfer = 0x85,
    }

}
