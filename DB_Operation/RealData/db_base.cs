using SQLUtils;
using ResModel.EQU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResModel;
using System.Data;

namespace DB_Operation.RealData
{
    public abstract class db_base
    {
        private ISQLUtils Connection = DB.Connection;
        protected abstract string Table_Name { get; }

        public IPowerPole Pole { get; set; }

        public db_base(IPowerPole pole) 
        { 
            this.Pole = pole;
        }

        public db_base() { }

        //protected abstract string GetSql();

        //protected abstract object[] GetFileds();


        protected ErrorCode DataSave(string strCmd, string[] fields, object[] obj)
        {
            int m = Connection.ExecuteNoneQuery(strCmd, CommandType.Text, fields, obj);
            if (m == 0)
                return ErrorCode.DataExist;
            return ErrorCode.NoError;

        }

        protected DataTable DataGet(Dictionary<string,string> dics,
                                    string cmdid, 
                                    DateTime start, 
                                    DateTime end)
        {
            return DataGet(dics, GetSql_data_nw(cmdid, start, end));
        }

        protected DataTable DataGet(Dictionary<string, string> dics,
                            string condition)
        {
            int count = 0;
            StringBuilder sb = new StringBuilder("select ");
            foreach (KeyValuePair<string, string> dic in dics)
            {
                count++;
                if (count < dics.Count)
                    sb.AppendFormat("d.{0} as '{1}', \n", dic.Key, dic.Value);
                else
                    sb.AppendFormat("d.{0} as '{1}' \n", dic.Key, dic.Value);
            }
            sb.Append(condition);
            return Connection.GetTable(sb.ToString());
        }



        protected string GetSql_data_nw(string cmdid,DateTime start, DateTime end)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("from {0} as d ", this.Table_Name);
            sb.Append("left join t_powerpole as pole on d.poleid = pole.id ");
            sb.AppendFormat("where d.time between '{0:G}' and '{1:G}' and pole.CMD_ID = '{2}'",
                        start, end, cmdid);
            return sb.ToString();
        }

    }
}
