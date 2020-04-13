using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLUtils;
using System.ComponentModel;
using System.Reflection;  
using ResModel.EQU;


namespace DB_Operation.RealData
{
    public interface IRealData_OP
    {

        /// <summary>
        /// 将要存储的数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        ErrorCode DataSave(object data);
        
        /*
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="cmd_ID">装置ID</param>
        /// <param name="time">数据采集时间</param>
        /// <param name="data">采集到的数据</param>
        /// <returns>数据保存结果</returns>
        ErrorCode DataSave(string cmd_ID, DateTime time, object data);

        

        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="equID">装置ID</param>
        /// <param name="time">数据采集时间</param>
        /// <param name="data">采集到的数据</param>
        /// <returns>数据保存结果</returns>
        ErrorCode DataSave(int equID, DateTime time, object data);

        */
        /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="equ">装置</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>获取到的数据</returns>
        DataTable GetData(Equ equ, DateTime startTime, DateTime endTime);
        /*  /// <summary>
         /// 获取历史数据
         /// </summary>
         /// <param name="cmdID">装置ID</param>
         /// <param name="startTime">起始时间</param>
         /// <param name="endTime">结束时间</param>
         /// <returns>获取到的数据</returns>
         object GetData(string cmdID, DateTime startTime, DateTime endTime);
       
         /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="equID">装置ID</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>获取到的数据</returns>
        object GetData(int equID, DateTime startTime, DateTime endTime);
        
        /// <summary>
        /// 获取装置最后一条数据
        /// </summary>
        /// <param name="cmdID"></param>
        /// <returns></returns>
        object GetlastData(string cmdID);

        /// <summary>
        /// 获取装置最后一条数据
        /// </summary>
        /// <param name="cmdID"></param>
        /// <returns></returns>
        object GetlastData(int equID);
        
        /// <summary>
        /// 删除采集数据
        /// </summary>
        /// <param name="cmdID">装置ID</param>
        /// <param name="time">数据采集时间</param>
        /// <returns>删除结果</returns>
        bool DelData(string cmdID,DateTime time);

        /// <summary>
        /// 删除采集数据
        /// </summary>
        /// <param name="equID">装置ID</param>
        /// <param name="time">数据采集时间</param>
        /// <returns>删除结果</returns>
        bool DelData(int equID, DateTime time);

        /// <summary>
        /// 删除采集数据
        /// </summary>
        /// <param name="cmdID">装置ID</param>
        /// <param name="startTime">数据起始时间</param>
        /// <param name="endTime">数据截止时间</param>
        /// <returns></returns>
        bool DelData(string cmdID, DateTime startTime, DateTime endTime);
        
        /// <summary>
        /// 删除采集数据
        /// </summary>
        /// <param name="equID">装置ID</param>
        /// <param name="startTime">数据起始时间</param>
        /// <param name="endTime">数据截止时间</param>
        /// <returns></returns>
        bool DelData(int equID, DateTime startTime, DateTime endTime);
        */
    }

    public enum ErrorCode
    {
        [Description("数据操作成功")]
        NoError = 0,
        
        [Description("装置ID不存在")]
        TowerIDError = -1,    
        
        [Description("数据操作异常")]
        SqlError = -3,

        [Description("数据已存在")]
        DataExist = -2,
    }
}
