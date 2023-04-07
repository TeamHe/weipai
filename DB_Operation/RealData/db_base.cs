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

        //protected abstract string GetSql();

        //protected abstract object[] GetFileds();


        protected ErrorCode DataSave(string strCmd, string[] fields, object[] obj)
        {
            int m = Connection.ExecuteNoneQuery(strCmd, CommandType.Text, fields, obj);
            if (m == 0)
                return ErrorCode.DataExist;
            return ErrorCode.NoError;

        }

    }
}
