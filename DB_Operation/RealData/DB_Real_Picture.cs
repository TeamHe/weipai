using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResModel.EQU;
using ResModel.CollectData;
using SQLUtils;
using System.Data;


namespace DB_Operation.RealData
{
    public class DB_Real_Picture:IRealData_OP
    {
        private static Object thisLock = new Object();
        public ISQLUtils Connection { get; set; }
        private string tableName = "t_data_picture";
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ErrorCode DataSave(object data)
        {
            Picture wea = (Picture)data;
            int towerID = Real_Data_Op.GetTowerID(wea.CMD_ID,Connection,ICMP.Picture);
            lock (thisLock)
            {
                string[] fileds = new string[] { "@id", "@time", "@Path", "@ChannalNO",
                "@Presetting_No"
                };

                string sql = @"INSERT  INTO t_data_picture(" +
                        "PoleID," +                    //被测设备编号
                        "Time," +                       //采集时间
                        "Path," +                       //文件路径
                        "ChannalNO," +                  //通道号
                        "Presetting_No)" +              //预置位号           
                        "Values(";
                for (int i = 0; i < fileds.Length - 1; i++)
                {
                    sql += fileds[i] + ",";
                }
                sql += fileds[fileds.Length - 1] + ")";

                object[] obj = new object[fileds.Length];
                obj[0] = towerID;
                obj[1] = wea.Maintime;
                obj[2] = wea.PicPath;
                obj[3] = wea.ChannalNO;
                obj[4] = wea.Presetting_No;

                int m = Connection.ExecuteNoneQuery(sql, CommandType.Text, fileds, obj);
                if (m == 0)
                    return ErrorCode.DataExist;
            }
            return ErrorCode.NoError;

        }

        
        /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="equ">装置</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>获取到的数据</returns>
        public DataTable GetData(Equ equ, DateTime startTime, DateTime endTime)
        {
            if (Connection == null)
                Connection = DB.Connection;
            lock (thisLock)
            {
                string sql = "";
                if(equ == null)
                    sql = string.Format("select "
                        + "{0}.idt_data_picture as pid,"
                        + "{0}.Time as 采集时间,"
                        + "{0}.ChannalNO as 通道号,"
                        + "{0}.Presetting_No as 预置位号,"
                        + "{0}.Path as 路径  "
                        + "from {0} where Time between '{1}' and '{2}'",
                        tableName, startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                else
                    sql = string.Format("select "
                      + "{0}.idt_data_picture as pid,"
                      + "{0}.Time as 采集时间,"
                      + "{0}.ChannalNO as 通道号,"
                      + "{0}.Presetting_No as 预置位号,"
                      + "{0}.Path as 路径  "
                      + "from {0} where PoleID ={3} Time between '{1}' and '{2}'",
                      tableName, startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                      endTime.ToString("yyyy-MM-dd HH:mm:ss"),equ.ID);
                return Connection.GetTable(sql);
            }
        }

        /// <summary>
        /// 删除图片数据
        /// </summary>
        /// <param name="equ">装置</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>删除行数</returns>
        public int RemovePictures(Equ equ,DateTime start,DateTime end)
        {
            if (Connection == null)
                Connection = DB.Connection;
            string sql = "";
            if (equ == null)
                sql = string.Format("delete from {2} where time between '{0}' and '{1}'", 
                    start.ToString(), end.ToString(), tableName);
            else
                sql = string.Format("delete from {3} where PoleID = {0} and time between '{1}' and '{2}'",
                    equ.ID, start.ToString(), end.ToString(),tableName);
            return Connection.ExecuteNoneQuery(sql);
        }

        #region Private Method

        
        #endregion
    }
}
