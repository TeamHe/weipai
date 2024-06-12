using System.Collections.Generic;
using System.Text;
using SQLUtils;
using ResModel.EQU;
using System.Data;

namespace DB_Operation.EQUManage
{
    public class DB_Tower
    {
        public static ISQLUtils Connection = DB.Connection;
        private static string TableName = "t_tower";

        #region 新建杆塔

        public static void NewTower(Tower tower)
        {
            NewTower(tower.TowerName,tower.TowerID);  
        }


        /// <summary>
        /// 创建杆塔
        /// </summary>
        /// <param name="tower_Name">杆塔名称</param>
        /// <param name="tower_ID">杆塔ID</param>
        /// <param name="IDline">线路ID</param>
        /// <param name="cableID">线缆ID</param>
        /// <param name="radition">辐射系数</param>
        /// <param name="Ass">吸收系数</param>
        /// <returns>操作状态</returns>
        public static int NewTower(string tower_Name, string tower_ID)
        {
            //if (Is_Line_Exist(tower_ID))
            //    throw new Exception("该装置ID已存在");
            string sql = string.Format(@"INSERT INTO {0}(" +
                   "TowerName, " +           //杆塔名称
                    "TowerID" +             //杆塔ID
                  ")" +
                   "Values(\"{1}\", \"{2}\");"
                   , TableName, tower_Name, tower_ID);
            return Connection.ExecuteNoneQuery(sql);
        }
        /// <summary>
        /// 添加杆塔信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lineNO"></param>
        /// <returns></returns>
        public static int Add(string name, int lineNO)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.AppendFormat("INSERT INTO {0}(TowerName,LineID) ",TableName);
            strsql.Append("Values(@name,@lineID);");
            strsql.Append("select last_insert_id();");
            var row = Connection.GetFirstRow(strsql.ToString(), CommandType.Text,
                new string[] { "@name", "@lineID" },
                new object[] { name,lineNO});

            return int.Parse(row[0].ToString());
        }

        #endregion

        #region 删除杆塔
        /// <summary>
        /// 删除杆塔信息
        /// </summary>
        /// <param name="tower"></param>
        /// <returns></returns>
        public static bool DelTower(Tower tower)
        {
            return DelTower(tower.TowerNO);
        }
        /// <summary>
        /// 删除杆塔
        /// </summary>
        /// <param name="Tower_ID"></param>
        /// <returns></returns>
        public static bool DelTower(string Tower_ID)
        {
            string sql = string.Format(
                @"Delete from {1} where  TowerID = ""{0}"";"
                , Tower_ID, TableName);
            int num = Connection.ExecuteNoneQuery(sql);
            if (num >= 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除杆塔
        /// </summary>
        /// <param name="IDTower"></param>
        /// <returns></returns>
        public static bool DelTower(int IDTower)
        {
            string sql = string.Format(
                @"Delete from {1} where  idt_tower = {0};"
                , IDTower, TableName);
            int num = Connection.ExecuteNoneQuery(sql);
            if (num >= 1)
                return true;
            else
                return false;
        }
        #endregion

        #region 查询杆塔
        /// <summary>
        /// 获取杆塔列表
        /// </summary>
        /// <returns></returns>
        //public static List<Tower> GetTowerList()
        //{
        //    List<Tower> towerList = new List<Tower>();
        //    string sql = string.Format("select  idt_tower as ID,\n " +
        //           "TowerName as 杆塔名称,\n" +  // 杆塔名称
        //           "TowerID as 杆塔ID\n" +   //杆塔ID  
        //          "from {0}\n", TableName);
        //    var dt = Connection.GetTable(sql);
        //    if (dt == null) return null;
        //    foreach(DataRow row in dt.Rows )
        //    {
        //        Tower tower = new Tower();
        //        tower.TowerName = row["杆塔名称"].ToString();
        //        tower.TowerID = row["杆塔ID"].ToString();
        //        tower.TowerNO = (int)row["ID"];
        //        towerList.Add(tower);
        //    }
        //    return towerList;
        //}

        public static List<Tower> List()
        {
            List<Tower> list = new List<Tower>();
            StringBuilder strsql = new StringBuilder();
            strsql.AppendFormat("select * from {0} ",TableName);
            var dt = Connection.GetTable(strsql.ToString());
            if (dt == null) return list;
            foreach (DataRow row in dt.Rows)
            {
                Tower tower = new Tower();
                tower.TowerName = row["TowerName"].ToString();
                tower.TowerNO = (int)row["idt_tower"];
                if (!row.IsNull("LineID"))
                    tower.LineID = (int)row["LineID"];
                list.Add(tower);
            }
            return list;
        }

        public static List<Tower> List(int? lineID)
        {
            List<Tower> list = new List<Tower>();
            StringBuilder strsql = new StringBuilder();
            strsql.AppendFormat("select * from {0} ", TableName);
            if (lineID != null)
                strsql.Append("where LineID=@lineID;");
            else
                strsql.Append("where isnull(LineID)");
            var dt = Connection.GetTable(strsql.ToString(),CommandType.Text,
                new string[]{"@lineID"},
                new object[]{lineID});
            if (dt == null) return list;
            foreach (DataRow row in dt.Rows)
            {
                Tower tower = new Tower();
                tower.TowerName = row["TowerName"].ToString();
                tower.TowerNO = (int)row["idt_tower"];
                if (!row.IsNull("LineID"))
                    tower.LineID = (int)row["LineID"];
                list.Add(tower);
            }
            return list;
        }

        /// <summary>
        /// 查询杆塔ID对应的行号
        /// </summary>
        /// <param name="Tower_ID">杆塔ID</param>
        /// <returns>杆塔表中ID</returns>
        //public static Tower GetTower(string Tower_ID)
        //{
        //    string sql = string.Format(
        //         "select  idt_tower as ID,\n " + //
        //         "TowerName as 杆塔名称,\n" +  // 杆塔名称
        //         "TowerID as 杆塔ID\n" +   //杆塔ID 
        //         "from {0}\n " +
        //         "where TowerID = \"{1}\"\n", TableName, Tower_ID);
        //    var dt = Connection.GetTable(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        DataRow row = dt.Rows[0];
        //        Tower tower = new Tower();
        //        tower.TowerName = row["杆塔名称"].ToString();
        //        tower.TowerID = row["杆塔ID"].ToString();
        //        tower.TowerNO = (int)row["ID"];
        //        return tower;
        //    }
        //    return null;

        //}
        /// <summary>
        /// 获取杆塔信息
        /// </summary>
        /// <param name="id_tower"></param>
        /// <returns></returns>
        //public static Tower GetTower(int id_tower)
        //{
        //    string sql = string.Format(
        //         "select  idt_tower as ID " + //
        //         "TowerName as 杆塔名称," +  // 杆塔名称
        //         "TowerID as 杆塔ID,\n\t" +   //杆塔ID 
        //         "from {0} " +
        //         "where t_tower.idt_tower = {0} \n", TableName, id_tower);
        //    var dt = Connection.GetTable(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        DataRow row = dt.Rows[0];
        //        Tower tower = new Tower();
        //        tower.TowerName = row["杆塔名称"].ToString();
        //        tower.TowerID = row["杆塔ID"].ToString();
        //        tower.TowerNO = (int)row["ID"];
        //        return tower;
        //    }
        //    return null;
        //}
        #endregion

        #region 更新杆塔信息

        /// <summary>
        /// 更新杆塔参数信息
        /// </summary>
        /// <param name="IDTower"></param>
        /// <param name="tower"></param>
        /// <returns></returns>
        public static int UpTower(int IDTower, Tower tower)
        {
            //if (Is_Line_Exist(tower.TowerID)) throw new Exception("该装置ID已存在");
            string sql = string.Format("update  {0} " + // 更新杆塔信息
                   "set TowerName = \"{1}\", " +    //杆塔名称
                   "TowerID = \"{2}\"\n" +
                   "where idt_tower = {3}; \n", TableName, tower.TowerName, tower.TowerID, IDTower);
            return Connection.ExecuteNoneQuery(sql);
        }
        /// <summary>
        /// 更新杆塔参数信息
        /// </summary>
        /// <param name="srcTower"></param>
        /// <param name="desTower"></param>
        /// <returns></returns>
        public static int UpTower(Tower srcTower, Tower desTower)
        {
            return UpTower(srcTower.TowerNO,desTower);
        }

        public static bool Update(int towerID, Tower tower)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.AppendFormat("Update {0} ",TableName);
            strsql.Append("set TowerName=@name,LineID=@lineID \n");
            strsql.Append("where idt_tower=@id;\n");

            Connection.ExecuteNoneQuery(strsql.ToString(), CommandType.Text,
                new string[] { "@name", "@lineID", "@id" },
                new object[] { tower.TowerName, tower.LineID, towerID });
            return true;
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 判断杆塔ID是否存在
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool Is_Line_Exist(string ID)
        {
            string sql = string.Format("select  TowerName as 杆塔名称," + // 装置名称
                   "TowerID as 杆塔ID\n\t" +  //状态监测设备ID
                   "from {1}  \n" +
                   "where TowerID = \"{0}\";", ID, TableName);

            try
            {
                var dt = Connection.GetTable(sql);
                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;

            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
