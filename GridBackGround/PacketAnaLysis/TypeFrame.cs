using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.PacketAnaLysis
{
    /// <summary>
    /// 帧类型
    /// </summary>
    static public class TypeFrame
    {
        /// <summary>
        /// 监测数据报（监测装置->上位机）
        /// </summary>
        public const int Monitoring = 0x01;
        /// <summary>
        /// 数据报相应(上位机->监测装置)
        /// </summary>
        public const int ResMonitoring = 0x02;
        /// <summary>
        /// 控制数据报(上位机->监测装置)
        /// </summary>
        public const int Control = 0x03;
        /// <summary>
        /// 控制响应报（监测装置->上位机）
        /// </summary>
        public const int ResControl = 0x04;
        /// <summary>
        /// 图像数据报（监测装置->上位机）
        /// </summary>
        public const int Image = 0x05;
        /// <summary>
        /// 图像数据响应报（上位机->监测装置）
        /// </summary>
        public const int ResImage = 0x06;
        /// <summary>
        /// 远程图像控制报(上位机->监测装置)
        /// </summary>
        public const int ControlImage = 0x07;
        /// <summary>
        /// 图像控制响应报（监测装置->上位机）
        /// </summary>
        public const int ResControlImage = 0x08;
        /// <summary>
        /// 工作状态报（监测装置->上位机）
        /// </summary>
        public const int WorkState = 0x09;
        /// <summary>
        /// 工作状态响应(上位机->监测装置)
        /// </summary>
        public const int ResWorkState = 0x0a;
        /// <summary>
        /// 扩展语音播放协议(上位机->监测装置)
        /// </summary>
        public const int Voice = 0x0b;
        /// <summary>
        /// 扩展语音播放协议响应
        /// </summary>
        public const int VoiceRes = 0x0c;
        /// <summary>
        /// 私有控制(上位机—>检测装置)
        /// </summary>
        public const int PrivateCon = 0xf1;
        /// <summary>
        /// 私有控制响应(检测装置->上位机)
        /// </summary>
        public const int PrivateRes = 0xf2;
    }
}
