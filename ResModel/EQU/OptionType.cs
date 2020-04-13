using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;  
using System.Reflection;  
//using System.Collections.Specialized; 

namespace ResModel.EQU
{
    /// <summary>
    /// 监测数据报文类型
    /// </summary>
    public enum ICMP
    {
        #region 标准字段
        /// <summary>
        /// 微气象数据报
        /// </summary>
        [Description("微气象")]
        Weather = 0x01,
        /// <summary>
        /// 杆塔倾斜数据报
        /// </summary>
        [Description("杆塔倾斜")]
        Inclination,
        /// <summary>
        /// 导地线微风振动特征量数据报
        /// </summary>
        [Description("微风振动特征量")]
        Vibration_Character,
        /// <summary>
        /// 导地线微风振动波形信号数据报
        /// </summary>
        [Description("微风振动波形信号")]
        Vibration_Form,
        /// <summary>
        /// 导线弧垂数据报
        /// </summary>
        [Description("导线弧垂数据报")]
        Conductor_Sag,
        /// <summary>
        /// 导线温度
        /// </summary>
        [Description("导线温度")]
        Line_Temperature,
        /// <summary>
        /// 覆冰及不均衡张力差数据报
        /// </summary>
        [Description("覆冰")]
        Ice,
        /// <summary>
        /// 导线风偏
        /// </summary>
        [Description("导线风偏")]
        Windage_Yaw,
        /// <summary>
        /// 导地线舞动特征量数据报
        /// </summary>
        [Description("导地线舞动特征量")]
        Wave_Character,
        /// <summary>
        /// 导地线舞动轨迹数据报
        /// </summary>
        [Description("导地线舞动轨迹")]
        Wave_Trajectory,
        /// <summary>
        /// 现场污秽度数据报
        /// </summary>
        [Description("现场污秽度")]
        Filthy_Degree,
        /// <summary>
        /// 图像
        /// </summary>
        [Description("图像")]
        Picture,
        #endregion

        #region 预留字段
        [Description("未知类型")]
        UnKonwn,
        #endregion
    }

    /// <summary>
    /// 装置控制
    /// </summary>
    public enum EQU_Control
    { 
       #region 标准字段
        /// <summary>
        /// 状态监测装置网络适配器查询/设置
        /// </summary>
        [Description("状态监测装置网络适配器")]
        NIA =0xa1,
        /// <summary>
        /// 上级设备请求状态监测装置历史数据
        /// </summary>
        [Description("上级设备请求状态监测装置历史数据")]
        HisData,
        /// <summary>
        /// 状态监测装置采样周期查询/设置
        /// </summary>
        [Description("状态监测装置采样周期")]
        MainTime,
        /// <summary>
        /// 监测装置指向上位机的信息查询/设置
        /// </summary>
        [Description("监测装置指向上位机的信息")]
        HostComputer,
        /// <summary>
        /// 状态监测装置 ID 查询/设置
        /// </summary>
        [Description("状态监测装置ID")]
        ID,
        /// <summary>
        /// 装置复位
        /// </summary>
        [Description("装置复位ID")]
        Reset,
         /// <summary>
        /// 状态监测装置模型参数配置信息查询/设置 0xa7
        /// </summary>
        [Description("状态监测装置模型参数配置信息")]
        Model,
        #endregion
    }

    /// <summary>
    /// 远程图像数据类型
    /// </summary>
    public enum Pic_Control
    {
        #region 标准字段
        /// <summary>
        /// 图像采集参数设置
        /// </summary>
        [Description("图像采集参数")]
        Model = 0xb1,
        /// <summary>
        /// 拍照时间表设置
        /// </summary>
        [Description("拍照时间表")]
        Photo_TimeTable,
        /// <summary>
        /// 手动请求拍摄照片
        /// </summary>
        [Description("手动请求拍摄照片")]
        Take_Photo,
        /// <summary>
        /// 采集装置请求上送照片
        /// </summary>
        [Description("采集装置请求上送照片")]
        Image_Data_Start,
        /// <summary>
        /// 远程图像数据报
        /// </summary>
        [Description("远程图像数据报")]
        Image_Data,
        /// <summary>
        /// 远程图像数据上送结束标记
        /// </summary>
        [Description("远程图像数据上送结束标记")]
        Image_Data_End,
        /// <summary>
        /// 远程图像补包数据下发
        /// </summary>
        [Description("远程图像补包数据下发")]
        Image_Data_Compen,
        /// <summary>
        /// 摄像机远程调节     0xb8
        /// </summary>
        [Description("摄像机远程调节")]
        Camera_Adjust,
        #endregion

        #region 预留字段

        #endregion
    }

    /// <summary>
    /// 工作状态数据类型
    /// </summary>
    public enum Work_State
    { 
       #region 标准字段
        /// <summary>
        /// 心跳数据报
        /// </summary>
        [Description("心跳数据报")]
        Heart = 0xc1,
        /// <summary>
        /// 故障信息报
        /// </summary>
        [Description("故障信息报")]
        Error,
        #endregion

        #region 预留字段

        #endregion
    }
}
