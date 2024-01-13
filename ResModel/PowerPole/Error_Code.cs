using System.ComponentModel;

namespace ResModel.PowerPole
{
    public enum Error_Code
    {
        [Description("设置成功")]
        Success = 0,
        [Description("无效的参数")]
        InvalidPara,
        [Description("未知的MN")]
        UnknonwMN,
        [Description("设备离线")]
        DeviceOffLine,
        [Description("设备返回错误")]
        DeviceError,
        //5
        [Description("通讯超时")]
        ResponseOverTime,
        [Description("Http请求方法错误,只接受Post")]
        RequestMethodError,
        [Description("JSON 格式化错误")]
        JSONFormatError,
        [Description("未识别的指令")]
        UnknownCommand,
        [Description("内部错误")]
        InternalError,
        [Description("设备正在等待设备响应")]
        DeviceBusy,
    }
}
