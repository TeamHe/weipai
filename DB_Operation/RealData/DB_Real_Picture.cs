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

        /*
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="cmd_ID">装置ID</param>
        /// <param name="time">数据采集时间</param>
        /// <param name="data">数据内容</param>
        /// <returns></returns>
        public ErrorCode DataSave(string cmd_ID, DateTime time, object data)
        {

            return ErrorCode.SqlError;
        }

        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="equID">装置ID</param>
        /// <param name="time">采集时间</param>
        /// <param name="data">数据内容</param>
        /// <returns></returns>
        public ErrorCode DataSave(int equID, DateTime time, object data)
        {
            return ErrorCode.SqlError;
        }

        /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="cmdID">装置ID</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>获取到的数据</returns>
        public object GetData(string cmdID, DateTime startTime, DateTime endTime)
        {
            return null;
        }
        */
        /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="equ">装置</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>获取到的数据</returns>
        public DataTable GetData(Equ equ, DateTime startTime, DateTime endTime)
        {
            //if (equ.Type != ICMP.Picture)
            //    throw new Exception("装置类型异常");
            lock (thisLock)
            {
                string[] fileds = new string[] { "@id", "@startTime", "@endTime" };


                string sql = string.Format("select "
                    + "{0}.Time as 采集时间, \n\t"
                    + "{0}.ChannalNO as 通道号,"
                    + "{0}.Presetting_No as 预置位号,"
                    + "{0}.Path as 路径  "
                    + "from {0} \n"
                    + "where {0}.Time between @startTime and @endTime \n\t"
                    + "and {0}.PoleID =  @id ", tableName);
                object[] obj = new object[fileds.Length];
                obj[0] = equ.ID;
                obj[1] = startTime;
                obj[2] = endTime;

                return Connection.GetTable(sql, CommandType.Text, fileds, obj);
            }
        }


        #region Private Method

        
        #endregion
    }
}
