using ResModel.nw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResModel.gw;
using ResModel;
using System.Data;

namespace DB_Operation.RealData
{
    public class db_data_gw_weather : db_base
    {
        private static string sql_save = null;

        private static string[] fileds = new string[]
{
             "@time",
             "@poleid",
             "@curtime",
             "@temp",
             "@humidity",
             "@speed_avg",
             "@speed_max",
             "@speed_ext",
             "@speed_std",
             "@direction_avg",
             "@pressure",
             "@rain",
             "@rainIntensity",
             "@sun",
};


        protected override string Table_Name { get { return "t_gw_weather"; } }

        public gw_data_weather Weather { get; set; }

        public db_data_gw_weather() { }

        public db_data_gw_weather(IPowerPole pole) : base(pole) { }

        public db_data_gw_weather(IPowerPole pole, gw_data_weather weather)
            : base(pole)
        {
            this.Weather = weather;
        }

        private string GetSaveSql()
        {
            if (sql_save != null)
                return sql_save;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("INSERT  INTO {0}(", this.Table_Name);
            builder.Append("time, ");
            builder.Append("poleid, ");
            builder.Append("up_time, ");
            builder.Append("temp, ");
            builder.Append("humidity, ");
            builder.Append("speed_avg, ");
            builder.Append("speed_max, ");
            builder.Append("speed_ext, ");
            builder.Append("speed_std, ");
            builder.Append("direction_avg, ");
            builder.Append("pressure, ");
            builder.Append("rain, ");
            builder.Append("rainIntensity, ");
            builder.Append("sun");

            builder.Append(")Values(");

            for (int i = 0; i < fileds.Length - 1; i++)
            {
                builder.AppendFormat("{0},", fileds[i]);
            }
            builder.AppendFormat("{0});", fileds[fileds.Length - 1]);
            sql_save = builder.ToString();
            return sql_save;
        }

        public ErrorCode DataSave(gw_data_weather weather)
        {
            if (this.Pole == null)
                return ErrorCode.TowerIDError;

            if (this.Pole.Pole_id == 0)
            {
                this.Pole.Pole_id = Real_Data_Op.GetTowerID(this.Pole.CMD_ID);
                if (this.Pole.Pole_id == 0)
                    return ErrorCode.TowerIDError;
            }


            if (weather == null)
                throw new ArgumentNullException(nameof(weather));

            object[] objs = new object[]
            {
                 weather.DataTime,
                 this.Pole.Pole_id,
                 DateTime.Now,
                 weather.Temperature,
                 weather.Humidity,
                 weather.AvgSpeed,
                 weather.MaxSpeed,
                 weather.ExtSpeed,
                 weather.StdSpeed,
                 weather.AvgDir,
                 weather.Air_Pressure,
                 weather.Rain,
                 weather.Rain_Intensity,
                 weather.Sun_Intensity,
            };
            return base.DataSave(this.GetSaveSql(), fileds, objs);
        }
        public DataTable DataGet(string cmdid, DateTime start, DateTime end)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>
            {
                {"time",            "时间"},
                {"temp",            "温度"},
                {"humidity",        "湿度"},
                {"speed_avg",       "10分钟平均风速"},
                {"speed_max",       "最大风速"},
                {"speed_ext",       "极大风速"},
                {"speed_std",       "标准风速"},
                {"direction_avg",   "10分钟平均风向"},
                {"pressure",        "气压"},
                {"rain",            "降雨量"},
                {"rainIntensity",   "降雨强度"},
                { "sun",            "光辐射强度"},
            };
            return base.DataGet(dics, cmdid, start, end);
        }
    }
}
