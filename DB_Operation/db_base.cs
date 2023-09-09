using SQLUtils;
using System;
using System.Collections.Generic;
using System.Text;
using ResModel;
using System.Data;
using System.ComponentModel;

namespace DB_Operation
{
    public enum ErrorCode
    {
        [Description("数据操作成功")]
        NoError = 0,

        [Description("装置ID不存在")]
        TowerIDError = -1,

        [Description("数据操作异常")]
        SqlError = -3,

        [Description("数据已存在")]
        DataExist = -2,
    }

    public abstract class db_base
    {
        protected ISQLUtils Connection = DB.Connection;
        protected abstract string Table_Name { get; }

        public IPowerPole Pole { get; set; }

        public db_base(IPowerPole pole) 
        { 
            this.Pole = pole;
        }

        public db_base() { }


        protected int ExecuteNoneQuery(string strCmd)
        {
            return Connection.ExecuteNoneQuery(strCmd);
        }

        /// <summary>
        /// 执行带参数的sql语句或存储过程，返回事务执行结果
        /// </summary>
        /// <param name="strCmd">sql语句或存储过程名</param>
        /// <param name="cmdType">说明strCmd是sql语句或存储过程名</param>
        /// <param name="fields">参数名</param>
        /// <param name="obj">参数值</param>
        /// <returns>事务执行结果</returns>
        protected int ExecuteNoneQuery(string strCmd, CommandType cmdType, string[] fields, object[] obj)
        {
            return Connection.ExecuteNoneQuery(strCmd, cmdType, fields, obj);
        }

        /// <summary>
        /// 执行带参数的sql语句或存储过程，返回事务执行结果
        /// </summary>
        /// <param name="strCmd">sql语句或存储过程名</param>
        /// <param name="cmdType">说明strCmd是sql语句或存储过程名</param>
        /// <param name="fields">参数名</param>
        /// <param name="obj">参数值</param>
        /// <returns>事务执行结果</returns>
        protected int ExecuteNoneQuery(string strCmd, string[] fields, object[] obj)
        {
            return Connection.ExecuteNoneQuery(strCmd, CommandType.Text, fields, obj);
        }

        /// <summary>
        /// 执行sql语句，返回数据表
        /// </summary>
        /// <param name="strCmd">sql语句</param>
        /// <returns>数据表</returns>
        protected DataTable GetTable(string strCmd)
        {
            return this.Connection.GetTable(strCmd);
        }

        protected ErrorCode DataSave(string strCmd, string[] fields, object[] obj)
        {
            int m = this.ExecuteNoneQuery(strCmd, CommandType.Text, fields, obj);
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
            sb.AppendFormat("where d.time between '{0:G}' and '{1:G}' and pole.CMD_ID = '{2}' ",
                        start, end, cmdid);
            sb.AppendFormat("order by d.time desc",
                        start, end, cmdid);
            return sb.ToString();
        }

    }
}
