using System.ComponentModel;

namespace ResModel.gw
{
    public enum gw_frame_type
    {
        /// <summary>
        /// 监测数据报（监测装置->上位机）
        /// </summary>

        [Description("监测数据报")]
        Monitoring = 0x01,
        /// <summary>
        /// 数据报相应(上位机->监测装置)
        /// </summary>
        [Description("数据响应报")]
        ResMonitoring = 0x02,
        /// <summary>
        /// 控制数据报(上位机->监测装置)
        /// </summary>
        [Description("控制数据报")]
        Control = 0x03,

        /// <summary>
        /// 控制响应报（监测装置->上位机）
        /// </summary>
        [Description("控制响应报")]
        ResControl = 0x04,


        /// <summary>
        /// 图像数据报（监测装置->上位机）
        /// </summary>
        [Description("图像数据报")]
        Image = 0x05,

        /// <summary>
        /// 图像数据响应报（上位机->监测装置）
        /// </summary>
        [Description("图像数据响应报")]
        ResImage = 0x06,


        /// <summary>
        /// 远程图像控制报(上位机->监测装置)
        /// </summary>
        [Description("图像控制报")]
        ControlImage = 0x07,

        /// <summary>
        /// 图像控制响应报（监测装置->上位机）
        /// </summary>
        [Description("图像控制响应报")]
        ResControlImage = 0x08,
        /// <summary>
        /// 工作状态报（监测装置->上位机）
        /// </summary>
        [Description("工作状态报")]
        WorkState = 0x09,

        /// <summary>
        /// 工作状态响应(上位机->监测装置)
        /// </summary>
        [Description("工作状态响应")]
        ResWorkState = 0x0a,
        /// <summary>
        /// 扩展语音播放协议(上位机->监测装置)
        /// </summary>
        [Description("扩展语音播放协议")]
        Voice = 0x0b,
        /// <summary>
        /// 扩展语音播放协议响应
        /// </summary>
        [Description("扩展语音播放协议响应")]
        VoiceRes = 0x0c,
        /// <summary>
        /// 私有控制(上位机—>检测装置)
        /// </summary>
        [Description("私有控制")]
        PrivateCon = 0xf1,
        /// <summary>
        /// 私有控制响应(检测装置->上位机)
        /// </summary>
        [Description("私有控制响应")]
        PrivateRes = 0xf2,

    }
}
