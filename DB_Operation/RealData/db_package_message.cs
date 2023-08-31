using ResModel;
using ResModel.PowerPole;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DB_Operation.RealData
{
    public class db_package_message : db_base
    {
        protected override string Table_Name { get { return "t_pac_msg"; } }

        private static string sql_save = null;

        private static string[] fileds = new string[]
        {
            "@time",
            "@poleid",
            "@rs_type",
            "@src_type",
            "@src_id",
            "@code",
            "@data",
        };

        public PackageMessage message { get; set; }

        public db_package_message() { }

        public db_package_message(IPowerPole pole)
        {
            this.Pole = pole;
        }

        public db_package_message(IPowerPole pole, PackageMessage message)
            :this(pole)
        {
            this.message = message;
        }

        private string GetSaveSql()
        {
            if (sql_save != null)
                return sql_save;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("INSERT  INTO {0}(", this.Table_Name);
            builder.Append("time, ");
            builder.Append("poleid, ");
            builder.Append("rs_type, ");
            builder.Append("src_type, ");
            builder.Append("src_id, ");
            builder.Append("code, ");
            builder.Append("data");
            builder.Append(")Values(");

            for (int i = 0; i < fileds.Length - 1; i++)
            {
                builder.AppendFormat("{0},", fileds[i]);
            }
            builder.AppendFormat("{0});", fileds[fileds.Length - 1]);
            sql_save = builder.ToString();
            return sql_save;
        }

        public ErrorCode DataSave(PackageMessage msg)
        {
            if (this.Pole == null)
                return ErrorCode.TowerIDError;

            if (this.Pole.Pole_id == 0)
            {
                this.Pole.Pole_id = Real_Data_Op.GetTowerID(this.Pole.CMD_ID);
                if (this.Pole.Pole_id == 0)
                    return ErrorCode.TowerIDError;
            }


            if (msg == null)
                throw new ArgumentNullException(nameof(msg));

            object[] objs = new object[]
            {
                msg.time,
                this.Pole.Pole_id,
                msg.rstype,
                msg.srctype,
                msg.src_id,
                msg.code,
                msg.data,
            };
            return base.DataSave(this.GetSaveSql(), fileds, objs);
        }

        public DataTable DataGet(string cmdid, DateTime start, DateTime end, int limit = 1000)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT d.* FROM {0} as d ", this.Table_Name);
            if(cmdid != null)
                sql.Append      ("left join t_powerpole as pole on d.PoleID = pole.id ");
            sql.Append("where ");
            if(cmdid != null)
                sql.AppendFormat("pole.CMD_ID = '{0}' and ", cmdid);
            sql.AppendFormat("time between  '{0:G}' and '{1:G}' ",start,end);
            sql.Append("order by idt_pac_comm desc");
            if(limit > 0)
                sql.AppendFormat("limit {0}", limit);
            return Connection.GetTable(sql.ToString());
        }

        public PackageMessage GetPackageMessage_from_row(DataRow row)
        {
            if(row == null) 
                return null;

            PackageMessage msg = new PackageMessage();
            if (row["time"] != null)
                msg.time = Convert.ToDateTime(row["time"]);
            if (row["rs_type"] != null)
                msg.rstype = (RSType)Convert.ToInt32(row["rs_type"]);
            if (row["src_type"] != null)
                msg.srctype = (SrcType)Convert.ToInt32(row["rs_type"]);
            if (row["src_id"] != null)
                msg.src_id = Convert.ToString(row["src_id"]);
            if (row["code"] != null)
                msg.code = Convert.ToInt32(row["code"]);
            if (row["data"] != null)
                msg.data = (byte[])row["data"];
            return msg;
        }

        public List<PackageMessage> GetPackageMessage_from_datatable(DataTable dt)
        {
            List<PackageMessage> list= new List<PackageMessage>();
            foreach(DataRow row in dt.Rows)
            {
                PackageMessage msg = GetPackageMessage_from_row (row);
                if (msg != null)
                    list.Insert(0,msg);
            }
            return list;
        }
    }
}
