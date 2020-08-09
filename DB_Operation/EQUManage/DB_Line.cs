using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLUtils;
using ResModel;
using System.Data;
using ResModel.EQU;

namespace DB_Operation.EQUManage
{
    public class DB_Line
    {
        public static ISQLUtils Connection = DB.Connection;
        private static string tableName = "t_line";
        /// <summary>
        /// 新建线路
        /// </summary>
        /// <param name="name"></param>
        public static bool Add(string name)
        {
            string sql = string.Format(@"Insert into {0}"
                + "(Name_Line,ID_Line) "
                +"Values(\"{1}\",\"\");",
                tableName,name);
            if (Connection.ExecuteNoneQuery(sql) == 1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除指定名称的
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Delete(string name)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.AppendFormat("delete from {0} where Name_Line= {1}", tableName, name);
            int rows = Connection.ExecuteNoneQuery(strsql.ToString());
            return true;    
        }
        /// <summary>
        /// 删除线路信息
        /// </summary>
        /// <param name="lineID"></param>
        /// <returns></returns>
        public static bool Delete(int lineID)
        {

            StringBuilder strsql = new StringBuilder();
            strsql.AppendFormat("delete from {0} where idt_line = {1}",tableName,lineID);
            int rows = Connection.ExecuteNoneQuery(strsql.ToString());
            return true;
        }
        /// <summary>
        /// 删除线路
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool Delete(ResModel.EQU.Line line)
        {
            return Delete(line.NO);
        }
        /// <summary>
        /// 更新线路信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Update(int id, string name)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.AppendFormat("update {0} ",tableName);
            strsql.Append("set Name_Line=@name where idt_line = @lineid");
            int rows = Connection.ExecuteNoneQuery(strsql.ToString(),System.Data.CommandType.Text,
                new string[] { "@name", "@lineid" },
                new object[]{name,id});
            return true;
        }
        /// <summary>
        /// 更新线路信息
        /// </summary>
        /// <param name="line"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Update(ResModel.EQU.Line line, string name)
        {
            return Update(line.NO,name);
        }
        /// <summary>
        /// 获取线路列表
        /// </summary>
        /// <returns></returns>
        public static List<ResModel.EQU.Line> List()
        {
            var list = new List<ResModel.EQU.Line>();

            StringBuilder strsql = new StringBuilder();
            strsql.Append("SELECT * FROM t_line;");
            var dt = Connection.GetTable(strsql.ToString());
            foreach (DataRow row in dt.Rows)
            {

                list.Add(RowtoLine(row));
            }
            return list;
        }
        /// <summary>
        /// 获取线路杆塔列表
        /// </summary>
        /// <returns></returns>
        public static List<ResModel.EQU.Line> List_LineTower()
        {
            var list = new List<ResModel.EQU.Line>();

            StringBuilder strsql = new StringBuilder();
            strsql.Append("select * from t_line ");
            strsql.Append("left join t_tower on  t_line.idt_line = t_tower.LineID");

            var dt = Connection.GetTable(strsql.ToString());
            foreach(DataRow row in dt.Rows)
            {
                int lineID = (int)row["idt_line"];
                ResModel.EQU.Line curLine = null;
                foreach (var line in list)
                {
                    if (line.NO == lineID)
                        curLine = line;
                }
                if (curLine == null)
                {
                    curLine = RowtoLine(row);
                    list.Add(curLine);
                }
                var tower = RowtoTower(row);
                if(tower != null)
                    curLine.TowerList.Add(tower);
            }

            return list;
        }

        public static List<ResModel.EQU.Line> List_LineTowerEqu()
        {
            var list = new List<ResModel.EQU.Line>();

            StringBuilder strsql = new StringBuilder();
            strsql.Append("select a.*,b.idt_tower,b.TowerName,b.LineID,c.* from t_line a\n");
            strsql.Append("left join t_tower b on b.LineID = a.idt_line \n");
            strsql.Append("left join t_powerpole c on c.towerID = b.idt_tower \n");

            var dt = Connection.GetTable(strsql.ToString());
            foreach (DataRow row in dt.Rows)
            {
                int lineID = (int)row["idt_line"];
                ResModel.EQU.Line curLine = null;
                foreach (var line in list)
                {
                    if (line.NO == lineID)
                        curLine = line;
                }
                if (curLine == null)
                {
                    curLine = RowtoLine(row);
                    list.Add(curLine);
                }
                ResModel.EQU.Tower curtower = null;
                var tower = RowtoTower(row);
                if (tower == null) continue;
                foreach (var ectower in curLine.TowerList)
                {
                    if (ectower.TowerNO == tower.TowerNO)
                        curtower = ectower ;
                }
                if (curtower == null)
                {
                    curtower = tower;
                    curLine.TowerList.Add(curtower);
                }
                Equ equ = RowToEqu(row);
                if (equ != null)
                    curtower.EquList.Add(equ);

            }

            return list;
        }

        public static ResModel.EQU.Line Get(int lineID)
        {
            return new ResModel.EQU.Line();
        }

        public static ResModel.EQU.Line Get(string name)
        {
            return new ResModel.EQU.Line();
        }


        #region Private Functions

        private static ResModel.EQU.Line RowtoLine(DataRow row)
        {
            var curLine = new ResModel.EQU.Line();
            curLine.NO = (int)row["idt_line"];
            curLine.Name = row["Name_Line"].ToString();
            if (row["ID_Line"] != null)
                curLine.LineID = row["ID_Line"].ToString();
            curLine.TowerList = new List<ResModel.EQU.Tower>();
            return curLine;
        }

        private static ResModel.EQU.Tower RowtoTower(DataRow row)
        {
            if (row.IsNull("idt_tower"))
                return null;
            ResModel.EQU.Tower tower = new ResModel.EQU.Tower();
            tower.TowerNO = (int)row["idt_tower"];
            tower.TowerName = row["TowerName"].ToString();
            tower.LineID   = (int)row["LineID"];
            tower.EquList = new List<Equ>();
            return tower;
        }

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
              "updateTime"
            };

        private static ResModel.EQU.Equ RowToEqu(DataRow row)
        {
            return DB_EQU.GetEqu(row);
        }
        #endregion
    }
}
