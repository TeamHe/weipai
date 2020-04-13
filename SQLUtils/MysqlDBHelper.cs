﻿using System;
using System.Linq;
using System.Data;

using MySql.Data.MySqlClient;
using System.Configuration;

namespace SQLUtils
{
    public class MySqlHelper : ISQLUtils
    {
        #region Fields

        private MySqlConnection _conn;

        private Object thisLock = new Object();

        private string _connStr;

        //private string _connStr;
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.		
        //public string _connStr = ConfigurationManager.ConnectionStrings["DB_WeiPai"].ConnectionString;

        #endregion Fields


        #region Constructors
        public MySqlHelper()
        {
            _connStr = GetConnStr();
        }


        public MySqlHelper(string conStr)
        {
            _connStr = conStr;
        }

        #endregion Constructors
        

     


        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetConnStr()
        {
            string str = SettingsDataBase.Default.ConnectionString;
            return str;
        }

        public void SetConnStr(string connStr)
        {
            _connStr = connStr;
        }

        //public bool Exists(string strSql)
        //{
        //    return 
        //}
        public bool Exists(string strSql, CommandType type, string[] fields, object[] obj)
        {
            var row = GetFirstRow(strSql,type,fields,obj );
            int cmdresult;
            if ((Object.Equals(row, null)) || (Object.Equals(row, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(row[0].ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int ExecuteNoneQuery(string strCmd)
        {
            int row = -1;
            lock (thisLock)
            {  //数据操作语句  
                try
                {
                    OpenConnection();
                    MySqlCommand cmd = new MySqlCommand(strCmd, _conn);
                    row = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CloseConnection();
                }
            }
            return row;
        }

        public int ExecuteNoneQuery(string strCmd, System.Data.CommandType cmdType, string[] fields, object[] obj)
        {
            int row = -1;
            lock (this.thisLock)
            {
                try
                {
                    OpenConnection();
                    MySqlCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
                    row = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CloseConnection();
                }
            }
            return row;
        }

        public object ExecuteScalar(string strCmd)
        {
            object result = null;
            lock (thisLock)
            {

                try
                {
                    OpenConnection();
                    MySqlCommand cmd = new MySqlCommand(strCmd, _conn);
                    result = cmd.ExecuteScalar();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CloseConnection();
                }
            }
            return result;
        }

        public object ExecuteScalar(string strCmd, System.Data.CommandType cmdType, string[] fields, object[] obj)
        {
            object result = null;
            lock (thisLock)
            {

                try
                {
                    OpenConnection();
                    MySqlCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
                    result = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CloseConnection();
                }
            }
            return result;
        }

        public DataTable GetTable(string strCmd)
        {
            return GetSet(strCmd).Tables[0];
        }

        public DataTable GetTable(string strCmd, CommandType cmdType, string[] fields, object[] obj)
        {
            return GetSet(strCmd, cmdType, fields, obj).Tables[0];
        }

        public DataSet GetSet(string strCmd)
        {
            DataSet result = new DataSet();
            lock (thisLock)
            {
                try
                {
                    OpenConnection();
                    MySqlCommand cmd = new MySqlCommand(strCmd, _conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CloseConnection();
                }
            }
            return result;
        }

        public DataSet GetSet(string strCmd, CommandType cmdType, string[] fields, object[] obj)
        {

            DataSet result = new DataSet();
            lock (thisLock)
            {
                try
                {
                    OpenConnection();
                    MySqlCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CloseConnection();
                }
            }
            return result;
        }

        public DataRow GetFirstRow(string strCmd)
        {
            DataTable dataTable = GetTable(strCmd);
            if (dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0];
            }
            return null;
        }

        public DataRow GetFirstRow(string strCmd, CommandType cmdType, string[] fields, object[] obj)
        {
            DataTable dataTable = GetTable(strCmd, cmdType, fields, obj);
            if (dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0];
            }
            return null;
        }

        #endregion Public Methods

        #region Private Methods

        private MySqlParameter[] GetSqlParameters(string[] fields, object[] obj)
        {
            try
            {
                MySqlParameter[] parameters = new MySqlParameter[obj.Count()];
                for (int i = 0; i < obj.Count(); i++)
                {
                    parameters[i] = new MySqlParameter(fields[i], obj[i]);
                }


                return parameters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void OpenConnection()
        {
            if (_conn == null)
            {
                _conn = new MySqlConnection(_connStr);

            }
            if (_conn.State != ConnectionState.Open)
            {

                _conn.Open();

            }
        }

        public void CloseConnection()
        {
            if (_conn.State != ConnectionState.Closed)
            {
                _conn.Close();
            }
        }

        /// <summary>
        /// <param name="strCmd">sql语句或存储过程名</param>
        /// <param name="cmdType">存储过程类型</param>
        /// <param name="paras">需要添加的参数</param>
        /// </summary>
        private MySqlCommand GetCommand(string strCmd, CommandType cmdType, string[] fields, object[] obj)
        {
            MySqlParameter[] paras = GetSqlParameters(fields, obj);
            MySqlCommand cmd = new MySqlCommand(strCmd, _conn);
            cmd.CommandType = cmdType;
            if (paras != null)
            {
                foreach (MySqlParameter para in paras)
                    cmd.Parameters.Add(para);
            }
            return cmd;
        }

        #endregion Private Methods

        #region IDisposable 成员

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        #endregion
    }
}
