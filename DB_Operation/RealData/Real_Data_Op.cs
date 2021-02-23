using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLUtils;
using ResModel.EQU;

namespace DB_Operation.RealData
{

    /// <summary>
    /// 数据保存操作
    /// </summary>
    public class Real_Data_Op
    {
        private static ISQLUtils Connection = DB.Connection;
        /*
          #region 判断记录是否存在
                /// 判断某一条数据是否存在
                /// </summary>
                /// <param name="id"></param>
                /// <param name="time"></param>
                /// <returns></returns>
                public static bool IsExistData(int rowNO, string TablenName)
                {
                    string sql = string.Format("select * from {1} "
                        + "where ID_Num =  {0} ", rowNO.ToString(), TablenName);
                    try
                    {
                        var dt = Connection.GetTable(sql);
                        if (dt.Rows.Count > 0)
                            return true;
                    }
                    catch { return false; }

                    return false;
                }
                /// <summary>
                /// 该条记录是否存在
                /// </summary>
                /// <param name="id"></param>
                /// <param name="dateTime"></param>
                /// <returns></returns>
                public static bool IsExistData(string id, DateTime dateTime, string TablenName)
                {
                    string sql = string.Format("select * from {0} "
                        + "where  Time = {1} and TowerID =  " +
                        "(select idt_tower from t_tower where TowerID = \"{2}\")",
                        TablenName, dateTime, id);
                    try
                    {
                        var dt = Connection.GetTable(sql);
                        if (dt.Rows.Count > 0)
                            return true;
                    }
                    catch { return false; }

                    return false;
                }
                public static ErrorCode IsExistData(int id, DateTime dateTime, string TablenName)
                {
                    string sql = string.Format("select * from {0} "
                        + "where  Time = \"{1}\" and TowerID =  {2}"
                       , TablenName, dateTime, id);
                    try
                    {
                        var dt = Connection.GetTable(sql);
                        if (dt.Rows.Count > 0)
                            return ErrorCode.DataExist;
                        else
                            return ErrorCode.NoError;
                    }
                    catch { return ErrorCode.SqlError; }
                }
                /// <summary>
                /// 该条记录是否存在
                /// </summary>
                /// <param name="id"></param>
                /// <param name="dateTime"></param>
                /// <returns></returns>
                public static ErrorCode IsExistData(string id, DateTime dateTime, string TablenName, ref int idt_tower)
                {
                    idt_tower = -1;
                    string sql = string.Format("select idt_tower from t_tower where TowerID = \"{0}\"", id);
                    try
                    {
                        var dt = Connection.GetTable(sql);
                        if (dt.Rows.Count > 0)
                            idt_tower = (int)dt.Rows[0][0];
                        else
                            return ErrorCode.TowerIDError;
                    }
                    catch (Exception ex)
                    {
                        string temp = ex.Message;
                        return ErrorCode.SqlError;
                    }
                    sql = string.Format("select * from {0} "
                        + "where  Time = \"{1}\" and TowerID =  {2}"
                       , TablenName, dateTime, idt_tower);
                    try
                    {
                        var dt = Connection.GetTable(sql);
                        if (dt.Rows.Count > 0)
                            return ErrorCode.DataExist;
                        else
                            return ErrorCode.NoError;
                    }
                    catch { return ErrorCode.SqlError; }
                }
                #endregion

                #region 删除实时数据
                /// <summary>
                /// 删除单条气象数据
                /// </summary>
                /// <param name="id"></param>
                /// <param name="time"></param>
                /// <returns></returns>
                //public static int DB_Real_Del(string TablenName, int id, DateTime time)
                //{
                //    string sql = string.Format(
                //     "Delete from {2} where TowerID = \"{0}\" and Time=\"{1}\" ;", id, time, TablenName);
                //    int num = -1;
                //    try
                //    {
                //        num = Connection.ExecuteNoneQuery(sql);
                //        return num;
                //    }
                //    catch (Exception ex)
                //    {
                //        throw ex;
                //    }
                //}
                /// <summary>
                /// 删除数据
                /// </summary>
                /// <param name="id"></param>
                /// <param name="time"></param>
                /// <returns></returns>
                //public static int DB_Real_Del(string TablenName, int id, DateTime[] time)
                //{
                //    string sql = string.Format("Delete from {3} where TowerID = \"{0}\" " +
                //         "and Time between \"{1}\" and \"{2}\";", id, time[0], time[1], TablenName);
                //    int num = -1;
                //    try
                //    {
                //        num = Connection.ExecuteNoneQuery(sql);
                //        return num;
                //    }
                //    catch (Exception ex)
                //    {
                //        throw ex;
                //    }
                //}
                #endregion

                public static string Get_Sel_Real_Head()
                {
                    return "select  "
                            + "T_Tower.TowerName as 被测设备名称,\n\t "
                            + "T_Tower.TowerID as 被测设备ID,\n\t";
                }

                #region 数据查询
                /// <summary>
                /// 获取最后一条记录
                /// </summary>
                /// <param name="TableName"></param>
                /// <param name="towerID"></param>
                /// <param name="selData"></param>
                /// <returns></returns>
                public static DataTable GetLastData(string TableName, int towerID, string selData)
                {
                    string sql = selData +
                       String.Format("where {0}.TowerID = {1} order by {0}.Time  desc limit 1 ",
                       TableName, towerID.ToString());
                    try
                    {
                        var dt = Connection.GetTable(sql);
                        return dt;
                    }
                    catch
                    {
                        return null;
                    }
                }

                public static DataTable GetData(string TableName, string id, DateTime start, DateTime end, string selData)
                {
                    string sql = selData +
                        string.Format(
                              "where {3}.Time between \"{0}\" and \"{1}\" \n\t"
                            + "and {3}.TowerID =  "
                            + "(select T_Tower.idt_tower from T_Tower where TowerID = \"{2}\") \n\t"
                        + "order by Time;", start, end, id, TableName);

                    try
                    {
                        var dt = Connection.GetTable(sql);
                        return dt;
                    }
                    catch
                    {
                        return null;
                    }
                }
                #endregion

                //public static int DealError(ErrorCode err)
                //{
                //    switch (err)
                //    {
                //        case ErrorCode.DataExist:
                //            return 0;
                //        case ErrorCode.SqlError:
                //            return -1;
                //        case ErrorCode.TowerIDError:
                //            return -2;
                //        case ErrorCode.NoError:
                //            return 1;
                //    }
                //    return 1;
                //}

         */

        /// <summary>
        /// 创建待保存的数据接口
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="">遇到不支持的数据类型</exception>
        /// <returns></returns>
        public static IRealData_OP Creat(ICMP type)
        {
            //IRealData_OP op;
            switch (type)
            { 
                //case ICMP.Weather:
                //    var op = new DB_Real_Weather();
                //    op.Connection = Connection;
                //    return op;
                case ICMP.Picture:
                    var pic = new DB_Real_Picture();
                    pic.Connection = Connection;
                    return pic;
                //case ICMP.Inclination:
                //    var inc = new DB_Real_Inidication();
                //    inc.Connection = Connection;
                //    return inc;
                //case ICMP.Ice:
                //    var ice = new DB_Real_Ice();
                //    ice.Connection = Connection;
                //    return ice;
            }
            throw new Exception("不支持的数据保存类型");
        }

        /// <summary>
        /// 
        /// </summary>
        public Real_Data_Op()
        { 
        
        }

         /// <summary>
        /// 根据装置ID获取装置序列号
        /// </summary>
        /// <param name="CMD_ID"></param>
        /// <returns></returns>
        public static int GetTowerID(string CMD_ID,ISQLUtils connection,ICMP EquType)
        {
            string sql = "SELECT id,type FROM t_powerpole where CMD_ID = @id  LIMIT 1";
            string[] files = new string[] { "@id" };
            object[] obj = new object[files.Length];
            obj[0] = CMD_ID;
            DataRow row = Connection.GetFirstRow(sql, CommandType.Text, files, obj);
            if (row == null)
                throw new Exception("该装置ID不存在");
            //try
            //{
                //ICMP type = (ICMP)int.Parse(row[1].ToString());
                //if (type != EquType)
                //    throw new Exception("数据类型错误");
                int id = (int)row["id"];
                return id;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
    
}
