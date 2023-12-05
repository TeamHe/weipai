using System.Collections.Generic;
using System.Text;
using System.Data;
using ResModel.EQU;
using System;

namespace DB_Operation.EQUManage
{
    public class DB_Line : db_base
    {
        protected override string Table_Name { get { return "t_line"; } }

        /// <summary>
        /// 新建线路
        /// </summary>
        /// <param name="name"></param>
        public bool Add(Line line)
        {
            if (line == null || line.Name ==null) 
                return false;
            string sql = string.Format(@"Insert into {0}"
                + "(Name_Line,ID_Line,flag) "
                + "Values(\"{1}\",\"\",\"{2}\");", 
                this.Table_Name, line.Name, line.Flag);
            if(this.ExecuteNoneQuery(sql.ToString()) ==1)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 删除线路
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool Delete(Line line)
        {
            if(line == null) 
                return false;
            StringBuilder str = new StringBuilder();
            str.AppendFormat("delete from {0} where idt_line = {1}", this.Table_Name, line.NO);
            this.ExecuteNoneQuery(str.ToString());
            return true;
        }
        /// <summary>
        /// 更新线路信息
        /// </summary>
        /// <param name="line"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Update(Line line, string name)
        {
            if (line == null)
                return false;
            StringBuilder strsql = new StringBuilder();
            strsql.AppendFormat("update {0} ", this.Table_Name);
            strsql.Append("set Name_Line=@name where idt_line = @lineid");
            this.ExecuteNoneQuery(strsql.ToString(),
                                  new string[] { "@name", "@lineid" },
                                  new object[] { name, line.NO });
            return true;
        }
        /// <summary>
        /// 获取线路列表
        /// </summary>
        /// <returns></returns>
        public List<Line> List()
        {
            var list = new List<Line>();
            DataTable dt = this.GetTable("SELECT * FROM t_line;");
            if(dt ==null || dt.Rows.Count == 0)
                return list;
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
        public List<Line> List_LineTower()
        {
            var list = new List<Line>();
            string sql = "select * from t_line " +
                         "left join t_tower on  t_line.idt_line = t_tower.LineID";
            DataTable dt = this.GetTable(sql);
            if(dt ==null || dt.Rows.Count == 0)
                return list;
            foreach(DataRow row in dt.Rows)
            {
                int lineID = (int)row["idt_line"];
                Line curLine = null;
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

        public List<Line> List_LineTowerEqu()
        {
            var list = new List<Line>();

            StringBuilder strsql = new StringBuilder();
            strsql.Append("select a.*,b.idt_tower,b.TowerName,b.LineID,c.* from t_line a\n");
            strsql.Append("left join t_tower b on b.LineID = a.idt_line \n");
            strsql.Append("left join t_powerpole c on c.towerID = b.idt_tower \n");

            var dt = Connection.GetTable(strsql.ToString());
            foreach (DataRow row in dt.Rows)
            {
                int lineID = (int)row["idt_line"];
                Line curLine = null;
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
                Tower curtower = null;
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
                    curtower.Flag = curLine.Flag;
                    curLine.TowerList.Add(curtower);
                }
                Equ equ = RowToEqu(row);
                if (equ != null)
                {
                    equ.Flag = curLine.Flag;
                    curtower.EquList.Add(equ);
                }
            }
            return list;
        }
        #region Private Functions

        private static Line RowtoLine(DataRow row)
        {
            var curLine = new Line();
            curLine.NO = (int)row["idt_line"];
            curLine.Name = row["Name_Line"].ToString();
            if (row["ID_Line"] != null)
                curLine.LineID = row["ID_Line"].ToString();

            if (row["flag"] != null )
            if(Enum.TryParse<DevFlag>(row["flag"].ToString(),
                                      out DevFlag flag))
            {
                    curLine.Flag = flag;
            }
            return curLine;
        }

        private static Tower RowtoTower(DataRow row)
        {
            if (row.IsNull("idt_tower"))
                return null;
            Tower tower = new Tower();
            tower.TowerNO = (int)row["idt_tower"];
            tower.TowerName = row["TowerName"].ToString();
            tower.LineID   = (int)row["LineID"];
            tower.EquList = new List<Equ>();
            return tower;
        }

        private static Equ RowToEqu(DataRow row)
        {
            return DB_EQU.GetEqu(row);
        }
        #endregion
    }
}
