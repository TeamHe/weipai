using ResModel;
using ResModel.gw_nw;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DB_Operation.RealData
{
    public class db_data_picture : db_base
    {
        private static string sql_save = null;

        protected override string Table_Name { get { return "t_data_picture"; } }

        private static string[] fileds = new string[] 
        { 
            "@id", 
            "@time", 
            "@Path", 
            "@ChannalNO",
            "@Presetting_No"
        };

        public db_data_picture() { }

        public db_data_picture(IPowerPole pole):base(pole) { }

        private string GetSaveSql()
        {
            if (sql_save != null)
                return sql_save;
            StringBuilder sb = new StringBuilder();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("INSERT  INTO {0}(", this.Table_Name);
            builder.Append("PoleID, ");
            builder.Append("Time, ");
            builder.Append("Path, ");
            builder.Append("ChannalNO, ");
            builder.Append("Presetting_No");

            builder.Append(")Values(");

            for (int i = 0; i < fileds.Length - 1; i++)
            {
                builder.AppendFormat("{0},", fileds[i]);
            }
            builder.AppendFormat("{0});", fileds[fileds.Length - 1]);
            sql_save = builder.ToString();
            return sql_save;
        }

        public DataTable DataGet(string cmdid, DateTime start, DateTime end)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>
            {
                {"time",                "时间"},
                {"ChannalNO",           "通道"},
                {"Presetting_No",       "预置位"},
                {"Path",                "图片路径"},
            };
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("from {0} as d ", this.Table_Name);
            sb.Append("left join t_powerpole as pole on d.PoleID = pole.id ");
            sb.AppendFormat("where d.time between '{0:G}' and '{1:G}' and pole.CMD_ID = '{2}'",
                        start, end, cmdid);
            return base.DataGet(dics, sb.ToString());
        }

        public ErrorCode DataSave(gn_picture picture)
        {
            if (this.Pole == null)
                return ErrorCode.TowerIDError;

            if (this.Pole.Pole_id == 0)
            {
                this.Pole.Pole_id = Real_Data_Op.GetTowerID(this.Pole.CMD_ID);
                if (this.Pole.Pole_id == 0)
                    return ErrorCode.TowerIDError;
            }


            if (picture == null)
                throw new ArgumentNullException(nameof(picture));

            object[] objs = new object[]
            {
                 this.Pole.Pole_id,
                 picture.Time,
                 picture.FileName,
                 picture.ChNO,
                 picture.Preset,
            };
            return base.DataSave(this.GetSaveSql(), fileds, objs);
        }
    }
}
