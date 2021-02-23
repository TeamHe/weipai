using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SQLUtils;
using System.Data;
using ResModel.EQU;
using ResModel.DataBase;
using ResModel.CollectData;

namespace DB_Operation.EQUManage
{
    public class DB_EQU
    {
        public static ISQLUtils Connection = DB.Connection;

        private static string TableName = "t_powerpole";

        private static string[] Colums = new string[]{
              "id",
              "name",
              "equNumber",
              "CMD_ID",
              "phone",
              "state",
              "towerID",
              "urlID",
              "marketText",
              "is_time",
              "is_name",
              "updateTime",
            };

        #region Public Funcitons
        /// <summary>
        /// 新建设备
        /// </summary>
        /// <param name="equ"></param>
        public static Equ New_EQU(Equ equ)
        {
            string[] fileds = new string[] { "@name", "@equNumber", "@CMD_ID","@phone",
                "@state","@towerID","@urlID","@marketText","@is_time","@is_name"};
            string sql = string.Format(@"INSERT INTO {0}(", TableName);
            for (int i = 1; i < Colums.Length - 2; i++)
                sql += Colums[i] + ",";
            sql += Colums[Colums.Length - 2] + ")values(";
            for (int i = 0; i < fileds.Length - 1; i++)
                sql += fileds[i] + ",";
            sql += fileds[fileds.Length - 1] + ")";
            object[] obj = new object[fileds.Length];
            obj[0] = equ.Name;
            obj[1] = equ.EquNumber;
            obj[2] = equ.EquID;
            obj[3] = equ.Phone;
            obj[4] = (int)equ.Status;
            obj[5] = equ.TowerNO;
            obj[6] = equ.UrlID;
            obj[7] = equ.MarketText;
            obj[8] = Convert.ToInt16(equ.Is_Time);
            obj[9] = Convert.ToInt16(equ.IS_Mark);
            Connection.ExecuteNoneQuery(sql, CommandType.Text, fileds, obj);
            return GetEqu(equ.EquID);
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <returns></returns>
        public static void Del_Station(Equ equ)
        {
            string sql = string.Format(
                @"Delete from {0} where  id = {1}"
                , TableName, equ.ID);
            Connection.ExecuteNoneQuery(sql);
        }

        /// <summary>
        /// 更新设备数据
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static void Up_Station(Equ equ)
        {
            Up_Station(equ, equ);
        }
        /// <summary>
        /// 更新设备数据
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static void Up_Station(Equ srcequ, Equ desequ)
        {
            string[] fileds = new string[] { "@name", "@equNumber", "@CMD_ID","@phone",
                "@state","@towerID","@urlID","@marketText","@is_time","@is_name","@id"};
            string sql = string.Format("update {0} set ", TableName);
            for (int i = 1; i < Colums.Length - 2; i++)
            {
                sql += Colums[i] + "=" + fileds[i - 1] + " ,";
            }
            sql += Colums[Colums.Length - 2] + "=" + fileds[fileds.Length - 2] + " ";
            sql += "where id=@id ";
            object[] obj = new object[fileds.Length];
            obj[0] = desequ.Name;
            obj[1] = desequ.EquNumber;
            obj[2] = desequ.EquID;
            obj[3] = desequ.Phone;
            obj[4] = (int)desequ.Status;
            obj[5] = desequ.TowerNO;
            obj[6] = desequ.UrlID;
            obj[7] = desequ.MarketText;
            obj[8] = desequ.Is_Time;
            obj[9] = desequ.IS_Mark;
            obj[10] = srcequ.ID;
            Connection.ExecuteNoneQuery(sql, CommandType.Text, fileds, obj);
        }

        /// <summary>
        /// 获取某一杆塔下面的装置列表
        /// </summary>
        /// <param name="tower">杆塔</param>
        /// <returns></returns>
        public static List<Equ> GetEquList(Tower tower)
        {
            return GetEquList(tower.TowerNO);
        }
        /// <summary>
        /// 获取某一杆塔下的装置列表
        /// </summary>
        /// <param name="towerNO">杆塔NO</param>
        /// <returns></returns>
        public static List<Equ> GetEquList(int towerNO)
        {
            List<Equ> equList = new List<Equ>();
            string sql = string.Format(
                "select  * from {0} where towerID = {1} \n", TableName, towerNO);
            var dt = Connection.GetTable(sql);
            if (dt == null) return null;
            foreach (DataRow row in dt.Rows)
            {
                equList.Add(GetEqu(row));
            }
            return equList;
        }

        /// <summary>
        /// 获取某一杆塔下的装置列表
        /// </summary>
        /// <param name="towerNO">杆塔NO</param>
        /// <returns></returns>
        public static List<Equ> GetEquList()
        {
            List<Equ> equList = new List<Equ>();
            string sql = string.Format(
                "select  * from {0} \n", TableName);
            var dt = Connection.GetTable(sql);
            if (dt == null) return null;
            foreach (DataRow row in dt.Rows)
            {
                equList.Add(GetEqu(row));
            }
            return equList;
        }
        /// <summary>
        /// 根据杆塔NO获取装置详细信息
        /// </summary>
        /// <param name="equNO"></param>
        /// <returns></returns>
        public static Equ GetEqu(int equNO)
        {
            string sql = string.Format(
                 "select  * from {0} where id = {1} \n", TableName, equNO);
            var dt = Connection.GetTable(sql);
            if (dt == null) return null;
            if (dt.Rows.Count == 0)
                throw new Exception("您要获取的装置不存在可能在其他地方已经被修改。");
            return GetEqu(dt.Rows[0]);
        }
        /// <summary>
        /// 根据装置ID获取装置详细信息
        /// </summary>
        /// <param name="CmdID"></param>
        /// <returns></returns>
        public static Equ GetEqu(string CmdID)
        {
            string sql = string.Format(
                    "select  * from {0} where CMD_ID = \"{1}\" \n", TableName, CmdID);
            var dt = Connection.GetTable(sql);
            if (dt == null) return null;
            if (dt.Rows.Count == 0)
                return null;
            return GetEqu(dt.Rows[0]);
        }

        public static void ClearOnLineState()
        {
            StringBuilder strsql = new StringBuilder();
            strsql.AppendFormat("update {0} set state = {1} where state={2}",
                TableName,
                (int)OnLineStatus.Offline,
                (int)OnLineStatus.Online );
            Connection.ExecuteNoneQuery(strsql.ToString());
        }

        public static void ChangeOnLineState(OnLineStatus state, int equID)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.AppendFormat("update {0} set state = @state where id = @id",TableName);
            Connection.ExecuteNoneQuery(strsql.ToString(),CommandType.Text,
                new string[]{"@state","@id"},
                new object[]{(int)state,equID});
        }

        #endregion
        
        #region Private Functions
        public static Equ GetEqu(DataRow row)
        {
            //try
            //{
                var equ = new Equ();
                if (row.IsNull(Colums[0]))
                    return null;
                equ.ID = (int)row[Colums[0]];
                equ.Name = row[Colums[1]].ToString();
                equ.EquNumber = row[Colums[2]].ToString(); ;
                equ.EquID = row[Colums[3]].ToString();
                equ.Phone = row[Colums[4]].ToString();
                equ.Status = (OnLineStatus)Convert.ToInt32(row[Colums[5]]);
                equ.TowerNO = (int)row[Colums[6]];
                equ.UrlID = (int)row[Colums[7]];
                equ.MarketText = row[Colums[8]].ToString();
                equ.IS_Mark = true;
                equ.Is_Time = true;
                if (row.ItemArray.Length > 10)
                {
                    if (!(row[Colums[9]] is System.DBNull))
                        equ.Is_Time = Convert.ToBoolean(row[Colums[9]]);
                    if (!(row[Colums[10]] is System.DBNull))
                        equ.IS_Mark = Convert.ToBoolean(row[Colums[10]]);
                }
                equ.UpdateTime = DateTime.Parse(row[Colums[11]].ToString());

                equ.Type = ICMP.Picture;
                return equ;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        #endregion
        


    }
}
