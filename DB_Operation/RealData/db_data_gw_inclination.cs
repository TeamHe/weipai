using ResModel;
using ResModel.gw;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DB_Operation.RealData
{
    public class db_data_gw_inclination : db_base
    {
        private static string sql_save = null;

        protected override string Table_Name { get { return "t_gw_inclination"; } }

        protected string CreateTable = "CREATE TABLE `t_gw_inclination` (\r\n  " +
            "`idt_gw_indination` INT NOT NULL AUTO_INCREMENT,\r\n  " +
            "`time` DATETIME NOT NULL,\r\n  " +
            "`poleid` INT NOT NULL,\r\n  " +
            "`cid` INT NULL DEFAULT 0 COMMENT '被测设备ID编号',\r\n  " +
            "`up_time` DATETIME NULL DEFAULT CURRENT_TIMESTAMP,\r\n  " +
            "`inclination` FLOAT NULL COMMENT '倾斜度',\r\n  "+
            "`inclination_x` FLOAT NULL COMMENT '顺线倾斜度',\r\n  " +
            "`inclination_y` FLOAT NULL COMMENT '横向倾斜度',\r\n  " +
            "`angle_x` FLOAT NULL COMMENT '顺线倾斜角',\r\n  " +
            "`angle_y` FLOAT NULL COMMENT '横向倾斜角',\r\n  " +
            "PRIMARY KEY (`idt_gw_indination`))\r\n " +
            "COMMENT = '国网杆塔倾斜数据';\r\n";

        private static string[] fileds = new string[]
        {
             "@time",
             "@poleid",
             "@curtime",
             "@cid",
             "@inclination",
             "@inclination_x",
             "@inclination_y",
             "@angle_x",
             "@angle_y",
        };

        private string GetSaveSql()
        {
            if (sql_save != null)
                return sql_save;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("INSERT  INTO {0}(", this.Table_Name);
            builder.Append("time, ");
            builder.Append("poleid, ");
            builder.Append("up_time, ");
            builder.Append("cid, ");
            builder.Append("inclination, ");
            builder.Append("inclination_x, ");
            builder.Append("inclination_y, ");
            builder.Append("angle_x, ");
            builder.Append("angle_y ");
            builder.Append(")Values(");

            for (int i = 0; i < fileds.Length - 1; i++)
            {
                builder.AppendFormat("{0},", fileds[i]);
            }
            builder.AppendFormat("{0});", fileds[fileds.Length - 1]);
            sql_save = builder.ToString();
            return sql_save;
        }

        public gw_data_inclination Data { get; set; }

        public db_data_gw_inclination() { }
        public db_data_gw_inclination(IPowerPole pole):base(pole) { }

        public db_data_gw_inclination(IPowerPole pole,gw_data_inclination data) 
            : base(pole) 
        {
            this.Data = data;
        }
        public ErrorCode DataSave(gw_data_inclination data)
        {
            if (this.Pole == null)
                return ErrorCode.TowerIDError;

            if (this.Pole.Pole_id == 0)
            {
                this.Pole.Pole_id = Real_Data_Op.GetTowerID(this.Pole.CMD_ID);
                if (this.Pole.Pole_id == 0)
                    return ErrorCode.TowerIDError;
            }

            if (data  == null)
                throw new ArgumentNullException(nameof(data));

            object[] objs = new object[]
            {
                data.DataTime,
                this.Pole.Pole_id,
                DateTime.Now,
                data.cno,
                data.Inclination,
                data.Inclination_x,
                data.Inclination_y,
                data.Angle_x,
                data.Angle_y,
            };            
            return base.DataSave(this.GetSaveSql(), fileds, objs);
        }

        public DataTable DataGet(string cmdid, DateTime start, DateTime end)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>
            {
                {"cid",             "编号" },
                {"time",            "时间"},
                {"inclination",     "倾斜度" },
                {"inclination_x",   "顺线倾斜度"},
                {"inclination_y",   "横向倾斜度"},
                {"angle_x",         "顺线倾斜角"},
                {"angle_y",         "横向倾斜角"},
            };
            return base.DataGet(dics, cmdid, start, end);
        }

    }
}
