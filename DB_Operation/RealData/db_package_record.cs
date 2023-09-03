using ResModel.PowerPole;
using ResModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Operation.RealData
{
    public class db_package_record :db_base
    {
        protected override string Table_Name { get { return "t_pac_record"; } }

        private static string sql_save = null;

        private static string[] fileds = new string[]
        {
            "@time",
            "@poleid",
            "@rs_type",
            "@cmd_type",
            "@info",
        };

        public PackageMessage message { get; set; }

        public db_package_record() { }

        public db_package_record(IPowerPole pole)
        {
            this.Pole = pole;
        }

        public db_package_record(IPowerPole pole, PackageMessage message)
            : this(pole)
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
            builder.Append("cmd_type, ");
            builder.Append("info");
            builder.Append(")Values(");

            for (int i = 0; i < fileds.Length - 1; i++)
            {
                builder.AppendFormat("{0},", fileds[i]);
            }
            builder.AppendFormat("{0});", fileds[fileds.Length - 1]);
            sql_save = builder.ToString();
            return sql_save;
        }

        public ErrorCode DataSave(PackageRecord record)
        {
            if (this.Pole == null)
                return ErrorCode.TowerIDError;

            if (this.Pole.Pole_id == 0)
            {
                this.Pole.Pole_id = Real_Data_Op.GetTowerID(this.Pole.CMD_ID);
                if (this.Pole.Pole_id == 0)
                    return ErrorCode.TowerIDError;
            }


            if (record == null)
                throw new ArgumentNullException(nameof(record));

            object[] objs = new object[]
            {
                record.Time,
                this.Pole.Pole_id,
                record.state,
                record.Command,
                record.Info,
            };
            return base.DataSave(this.GetSaveSql(), fileds, objs);
        }

        public DataTable DataGet(string cmdid, DateTime start, DateTime end, int limit = 1000)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT d.* FROM {0} as d ", this.Table_Name);
            if (cmdid != null)
                sql.Append("left join t_powerpole as pole on d.PoleID = pole.id ");
            sql.Append("where ");
            if (cmdid != null)
                sql.AppendFormat("pole.CMD_ID = '{0}' and ", cmdid);
            sql.AppendFormat("time between  '{0:G}' and '{1:G}' ", start, end);
            sql.Append("order by idt_pac_comm desc");
            if (limit > 0)
                sql.AppendFormat("limit {0}", limit);
            return Connection.GetTable(sql.ToString());
        }

        //public PackageRecord GetPackageMessage_from_row(DataRow row)
        //{
        //    if (row == null)
        //        return null;

        //    PackageRecord record = new PackageRecord();
        //    if (row["time"] != null)
        //        record.time = Convert.ToDateTime(row["time"]);
        //    if (row["rs_type"] != null)
        //        record.rstype = (RSType)Convert.ToInt32(row["rs_type"]);
        //    if (row["src_type"] != null)
        //        record.srctype = (SrcType)Convert.ToInt32(row["rs_type"]);
        //    if (row["src_id"] != null)
        //        record.src_id = Convert.ToString(row["src_id"]);
        //    if (row["code"] != null)
        //        record.code = Convert.ToInt32(row["code"]);
        //    if (row["data"] != null)
        //        record.data = (byte[])row["data"];
        //    return record;
        //}

        //public List<PackageMessage> GetPackageMessage_from_datatable(DataTable dt)
        //{
        //    List<PackageMessage> list = new List<PackageMessage>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        PackageMessage msg = GetPackageMessage_from_row(row);
        //        if (msg != null)
        //            list.Insert(0, msg);
        //    }
        //    return list;
        //}

    }
}
