﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ResModel;
using ResModel.nw;
using SQLUtils;
using System.Data;

namespace DB_Operation.RealData
{
    public class db_data_nw_weather : db_base
    {
        private static string sql_save = null;

        private static string[] fileds = new string[]
        {
             "@time",
             "@poleid",
             "@curtime",
             "@temp",
             "@humidity",
             "@speed",
             "@direction",
             "@rain",
             "@pressure",
             "@sun",
             "@speed_1_min",
             "@direction_1_min",
             "@speed_10_min",
             "@direction_10_min",
             "@speed_max",
        };

        nw_data_25_weather Weather { get; set; }

        protected override string Table_Name { get { return "t_nw_weather"; } }

        public db_data_nw_weather(){ }

        public db_data_nw_weather(IPowerPole pole):base(pole) { }

        public db_data_nw_weather(IPowerPole pole,nw_data_25_weather weather) 
            :base(pole)
        {
            this.Weather = weather;
        }


        private string GetSaveSql()
        {
            if (sql_save != null)
                return sql_save;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("INSERT  INTO {0}(", this.Table_Name);
            builder.Append("time,");
            builder.Append("poleid,");
            builder.Append("up_time,");
            builder.Append("temp,");
            builder.Append("humidity,");
            builder.Append("speed,");
            builder.Append("direction,");
            builder.Append("rain,");
            builder.Append("pressure,");
            builder.Append("sun,");
            builder.Append("speed_1_min,");
            builder.Append("direction_1_min,");
            builder.Append("speed_10_min,");
            builder.Append("direction_10_min,");
            builder.Append("speed_max");

            builder.Append(")Values(");

            for (int i = 0; i < fileds.Length - 1; i++)
            {
                builder.AppendFormat("{0},", fileds[i]);
            }
            builder.AppendFormat("{0});", fileds[fileds.Length-1]);
            sql_save = builder.ToString();
            return sql_save;
        }

        public ErrorCode DataSave()
        {
            return this.DataSave(this.Weather);
        }

        public ErrorCode DataSave(nw_data_25_weather weather)
        {
            if (this.Pole == null)
                return ErrorCode.TowerIDError;

            if(this.Pole.Pole_id == 0)
            {
                this.Pole.Pole_id = Real_Data_Op.GetTowerID(this.Pole.CMD_ID);
                if(this.Pole.Pole_id == 0)
                    return ErrorCode.TowerIDError;
            }
                

            if (weather == null)
                throw new ArgumentNullException(nameof(weather));

            object[] objs = new object[]
            {
                 weather.DataTime,
                 this.Pole.Pole_id,
                 DateTime.Now,
                 weather.Temp,
                 weather.Humidity,
                 weather.Speed,
                 weather.Direction,
                 weather.Rain,
                 weather.Pressure,
                 weather.Sun,
                 weather.Speed_1_min,
                 weather.Direction_1_min,
                 weather.Speed_10_min,
                 weather.Direction_10_min,
                 weather.Speed_max,
            };
            return base.DataSave(this.GetSaveSql(), fileds, objs);
        }


        public DataTable DataGet(string cmdid,DateTime start, DateTime end)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>
            {
                {"time",                "时间"},
                {"temp",                "温度"},
                {"humidity",            "湿度"},
                {"speed",               "瞬时风速"},
                {"direction",           "瞬时风向"},
                {"rain",                "降雨量"},
                {"pressure",            "大气压力"},
                {"sun",                 "日照"},
                {"speed_1_min",         "1分钟平均风速"},
                {"direction_1_min",     "1分钟平均风向"},
                {"speed_10_min",        "10分钟平均风速"},
                {"direction_10_min",    "10分钟平均风向"},
                {" speed_max",          "10分钟最大风速"},
            };
            return base.DataGet(dics, cmdid, start, end);
        }
    }
}
