﻿using System;
using System.Collections.Generic;
using System.Text;
using ResModel.gw;
using ResModel;
using System.Data;

namespace DB_Operation.RealData
{
    public class db_data_gw_ice : db_base
    {
        private static string sql_save = null;

        private static string[] fileds = new string[]
{
             "@time",
             "@poleid",
             "@curtime",
             "@ice_thickness",
             "@tension",
             "@tension_diff",
             "@pull1",
             "@yaw_angle1",
             "@del_angle1",
             "@pull2",
             "@yaw_angle2",
             "@del_angle2",
             "@pull3",
             "@yaw_angle3",
             "@del_angle3",
             "@cid",
             "@speed",
             "@dir",
             "@temp",
             "@hum",
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
            builder.Append("ice_thickness, ");
            builder.Append("tension, ");
            builder.Append("tension_diff, ");
            builder.Append("pull1, ");
            builder.Append("yaw_angle1, ");
            builder.Append("del_angle1, ");
            builder.Append("pull2, ");
            builder.Append("yaw_angle2, ");
            builder.Append("del_angle2, ");
            builder.Append("pull3, ");
            builder.Append("yaw_angle3,");
            builder.Append("del_angle3,");
            builder.Append("cid, ");
            builder.Append("speed, ");
            builder.Append("dir, ");
            builder.Append("temp,");
            builder.Append("hum");

            builder.Append(")Values(");

            for (int i = 0; i < fileds.Length - 1; i++)
            {
                builder.AppendFormat("{0},", fileds[i]);
            }
            builder.AppendFormat("{0});", fileds[fileds.Length - 1]);
            sql_save = builder.ToString();
            return sql_save;
        }


        protected override string Table_Name { get { return "t_gw_ice"; } }

        public gw_data_ice Ice { get; set; }

        public db_data_gw_ice() { }

        public db_data_gw_ice(IPowerPole pole) : base(pole) { }

        public db_data_gw_ice(IPowerPole pole, gw_data_ice ice)
            : base(pole)
        {
            this.Ice = ice;
        }


        public ErrorCode DataSave(gw_data_ice ice)
        {
            if (this.Pole == null)
                return ErrorCode.TowerIDError;

            if (this.Pole.Pole_id == 0)
            {
                this.Pole.Pole_id = Real_Data_Op.GetTowerID(this.Pole.CMD_ID);
                if (this.Pole.Pole_id == 0)
                    return ErrorCode.TowerIDError;
            }


            if (ice == null)
                throw new ArgumentNullException(nameof(ice));

            object[] objs = new object[20];
            int no = 0,cnum=0;
            objs[no++] = ice.DataTime;
            objs[no++] = this.Pole.Pole_id;
            objs[no++] = DateTime.Now;
            objs[no++] = ice.Equal_IceThicknes ;
            objs[no++] = ice.Tension ;
            objs[no++] = ice.Tension_Difference ;
            foreach(gw_data_ice_pull pull in ice.Pulls)
            {
                objs[no++] = pull.Original_Tension ;
                objs[no++] = pull.Windage_Yaw_Angle ;
                objs[no++] = pull.Deflection_Angle ;
                cnum++;
                if(cnum >=3 ) { break; }
            }
            no = 15;
            objs[no++] = ice.cno;
            objs[no++] = ice.Speed ;
            objs[no++] = ice.Direction ;
            objs[no++] = ice.Temp;
            objs[no++] = ice.Humidity;
            return base.DataSave(this.GetSaveSql(), fileds, objs);
        }
        public DataTable DataGet(string cmdid, DateTime start, DateTime end)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>
            {
                {"cid",             "编号" },
                {"time",            "时间"},
                {"speed",           "风速"},
                {"dir",             "风向"},
                {"temp",            "气温"},
                {"hum",             "湿度"},
                {"ice_thickness",   "等值覆冰厚度"},
                {"tension",         "综合悬挂载荷"},
                {"tension_diff",    "不均衡张力差"},
                {"pull1",           "拉力1"},
                {"yaw_angle1",      "风偏角1"},
                {"del_angle1",      "偏斜角1"},
                {"pull2",           "拉力2"},
                {"yaw_angle2",      "风偏角2"},
                {"del_angle2",      "偏斜角2"},
                {"pull3",           "拉力3"},
                {"yaw_angle3",      "风偏角3"},
                {"del_angle3",      "偏斜角3"},
            };
            return base.DataGet(dics, cmdid, start, end);
        }
    }
}
