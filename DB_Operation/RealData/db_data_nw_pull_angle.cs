using ResModel.nw;
using ResModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DB_Operation.RealData
{
    public class db_data_nw_pull_angle : db_base
    {
        private static string sql_save = null;

        private static string[] fileds = new string[]
{
             "@time",
             "@poleid",
             "@curtime",
             "@unit",
             "@pull_max_pull",
             "@angleDec_max_pull",
             "@angleInc_max_pull",
             "@pull_min_pull",
             "@angleDec_min_pull",
             "@angleInc_min_pull",
             "@pull_max_angle",
             "@angleDec_max_angle",
             "@angleInc_max_angle",
             "@pull_min_angle",
             "@angleDec_min_angle",
             "@angleInc_min_angle",
        };



        protected override string Table_Name { get { return "t_nw_pull"; } }

        public nw_data_22_pull_angle Data { get; set; }


        public db_data_nw_pull_angle(IPowerPole pole) : base(pole) { }

        public db_data_nw_pull_angle(){ }

        public db_data_nw_pull_angle(IPowerPole pole, nw_data_22_pull_angle data)
            : base(pole)
        {
            this.Data = data;
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
            builder.Append("unit,");
            builder.Append("pull_max_pull,");
            builder.Append("angleDec_max_pull,");
            builder.Append("angleInc_max_pull,");
            builder.Append("pull_min_pull,");
            builder.Append("angleDec_min_pull,");
            builder.Append("angleInc_min_pull,");
            builder.Append("pull_max_angle,");
            builder.Append("angleDec_max_angle,");
            builder.Append("angleInc_max_angle,");
            builder.Append("pull_min_angle,");
            builder.Append("angleDec_min_angle,");
            builder.Append("angleInc_min_angle");

            builder.Append(")Values(");

            for (int i = 0; i < fileds.Length - 1; i++)
            {
                builder.AppendFormat("{0},", fileds[i]);
            }
            builder.AppendFormat("{0});", fileds[fileds.Length - 1]);
            sql_save = builder.ToString();
            return sql_save;
        }

        public ErrorCode DataSave()
        {
            return this.DataSave(this.Data);
        }

        public ErrorCode DataSave(nw_data_22_pull_angle data)
        {
            if (this.Pole == null)
                return ErrorCode.TowerIDError;

            if (this.Pole.Pole_id == 0)
            {
                this.Pole.Pole_id = Real_Data_Op.GetTowerID(this.Pole.CMD_ID);
                if (this.Pole.Pole_id == 0)
                    return ErrorCode.TowerIDError;
            }


            if (data == null)
                throw new ArgumentNullException(nameof(data));

            object[] objs = new object[]
            {
                 data.DataTime,
                 this.Pole.Pole_id,
                 DateTime.Now,
                 data.UnitNo,
                 data.Pull_max_pull,
                 data.AngleDec_max_pull,
                 data.AngleInc_max_pull,
                 data.Pull_min_pull,
                 data.AngleDec_min_pull,
                 data.AngleInc_min_pull,
                 data.Pull_max_angle,
                 data.AngleDec_max_angle,
                 data.AngleInc_max_angle,
                 data.Pull_min_angle,
                 data.AngleDec_min_angle,
                 data.AngleInc_min_angle,
            };
            return base.DataSave(this.GetSaveSql(), fileds, objs);
        }
        public DataTable DataGet(string cmdid, DateTime start, DateTime end)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>
            {
                {"time",              "时间" },
                {"unit",              "单元标识" },

                {"pull_max_pull",     "最大拉力时刻-拉力"},
                {"angleDec_max_pull", "最大拉力时刻-风偏角"},
                {"angleInc_max_pull", "最大拉力时刻-倾斜角"},

                {"pull_min_pull",     "最小拉力时刻-拉力"},
                {"angleDec_min_pull", "最小拉力时刻-风偏角"},
                {"angleInc_min_pull", "最小拉力时刻-倾斜角"},
                
                {"pull_max_angle",     "最大风偏角时刻-拉力"},
                {"angleDec_max_angle", "最大风偏角时刻-风偏角"},
                {"angleInc_max_angle", "最大风偏角时刻-倾斜角"},
                
                {"pull_min_angle",     "最小风偏角时刻-拉力"},
                {"angleDec_min_angle", "最小风偏角时刻-风偏角"},
                {"angleInc_min_angle", "最小风偏角时刻-倾斜角" },
            };
            return base.DataGet(dics, cmdid, start, end);
        }

    }

}
