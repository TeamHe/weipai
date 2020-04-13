using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.PacketAnaLysis
{
    /// <summary>
    /// 监测数据报文类型
    /// </summary>
    static public class PacketType_Monitoring
    {
        #region 标准字段
        /// <summary>
        /// 气象环境类数据报
        /// </summary>
        public const int Weather = 0x01;
        /// <summary>
        /// 杆塔倾斜数据报
        /// </summary>
        public const int Gradient_Tower  = 0x02;
        /// <summary>
        /// 导地线微风振动特征量数据报
        /// </summary>
        public const int Vibration_Character = 0x03;
        /// <summary>
        /// 导地线微风振动波形信号数据报
        /// </summary>
        public const int Vibration_Form = 0x04;
        /// <summary>
        /// 导线弧垂数据报
        /// </summary>
        public const int Conductor_Sag = 0x05;
        /// <summary>
        /// 导线温度
        /// </summary>
        public const int Conductor_Temperature = 0x06;
        /// <summary>
        /// 覆冰及不均衡张力差数据报
        /// </summary>
        public const int Glaciation = 0x07;
        /// <summary>
        /// 导线风偏
        /// </summary>
        public const int Conductor_Monsoon = 0x08;
        /// <summary>
        /// 导地线舞动特征量数据报
        /// </summary>
        public const int Wave_Character = 0x09;
        /// <summary>
        /// 导地线舞动轨迹数据报
        /// </summary>
        public const int Wave_Trajectory = 0x0a;
        /// <summary>
        /// 现场污秽度数据报
        /// </summary>
        public const int Filthy_Degree = 0x0b;

        #endregion

        #region 预留字段
        
        #endregion

    }
    /// <summary>
    /// 控制报文数据类型
    /// </summary>
    static public class PacketType_Control
    {
        #region 标准字段
        /// <summary>
        /// 监测装置时间查询/设置
        /// </summary>
        public const int Timing = 0xa1; 
        /// <summary>
        /// 状态监测装置网络适配器查询/设置
        /// </summary>
        public const int NIA = 0xa1;
        /// <summary>
        /// 上级设备请求状态监测装置历史数据
        /// </summary>
        public const int HisData = 0xa2;
        /// <summary>
        /// 状态监测装置采样周期查询/设置
        /// </summary>
        public const int MainTime = 0xa3;
        /// <summary>
        /// 监测装置指向上位机的信息查询/设置
        /// </summary>
        public const int HostComputer = 0xa4;
        /// <summary>
        /// 状态监测装置 ID 查询/设置
        /// </summary>
        public const int ID = 0xa5;
        /// <summary>
        /// 装置复位
        /// </summary>
        public const int Reset = 0xa6;
         /// <summary>
        /// 状态监测装置模型参数配置信息查询/设置
        /// </summary>
        public const int Model = 0xa7;
        #endregion

        #region 预留字段
        /// <summary>
        /// 开始远程升级
        /// </summary>
        public const int Start_Update = 0xa9;
        /// <summary>
        /// 远程升级数据报
        /// </summary>
        public const int UpdateData = 0xaa;
        /// <summary>
        /// 远程升级结束标记
        /// </summary>
        public const int UpdateEnd = 0xab;
        /// <summary>
        /// 远程升级补包
        /// </summary>
        public const int UpdateBuBao = 0xac;
        #endregion
    }
    /// <summary>
    /// 远程图像数据类型
    /// </summary>
    static public class PacketType_Image
    {
        #region 标准字段
        /// <summary>
        /// 图像采集参数设置
        /// </summary>
        public const int Model = 0xb1;
        /// <summary>
        /// 拍照时间表设置
        /// </summary>
        public const int Photo_TimeTable = 0xb2;
        /// <summary>
        /// 手动请求拍摄照片
        /// </summary>
        public const int Take_Photo = 0xb3;
        /// <summary>
        /// 采集装置请求上送照片
        /// </summary>
        public const int Image_Data_Start = 0xb4;
        /// <summary>
        /// 远程图像数据报
        /// </summary>
        public const int  Image_Data= 0xb5;
        /// <summary>
        /// 远程图像数据上送结束标记
        /// </summary>
        public const int Image_Data_End = 0xb6;
        /// <summary>
        /// 远程图像补包数据下发
        /// </summary>
        public const int Image_Data_Compen = 0xb7;
        /// <summary>
        /// 摄像机远程调节
        /// </summary>
        public const int Camera_Adjust = 0xb8;
        #endregion

        #region 预留字段
        
        #endregion
    }

    /// <summary>
    ///工作状态数据类型
    /// </summary>
    static public class PacketType_WorkState
    {
        #region 标准字段
         /// <summary>
        /// 心跳数据报
        /// </summary>
        public const int Heart = 0xc1;
        /// <summary>
        /// 故障信息报
        /// </summary>
        public const int  Error= 0xc2;
        #endregion
       
        #region 预留字段
		 
	    #endregion     
    }

    static public class PrivatControl
    {
        /// <summary>
        /// 用户手机号码配置
        /// </summary>
        public const int UserPhone = 0x01;
    }
}
