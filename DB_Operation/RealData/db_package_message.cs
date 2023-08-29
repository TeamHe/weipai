using ResModel;
using ResModel.PowerPole;
using System;
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

        public DataTable DataGet(IPowerPole pole, DateTime start, DateTime end,int limit=1000)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from {0} ",this.Table_Name);
            sql.AppendFormat("where time between  '{0:G}' and '{1:G}' and poleid = {2} limit ",
                start, end, pole.Pole_id,limit);

            return Connection.GetTable(sql.ToString());
        }

    }
}
