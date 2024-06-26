﻿using System;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Configuration;

namespace SQLUtils
{
   public class SQLiteHelper:ISQLUtils
    {
        #region Fields

        private static SQLiteConnection _conn;

        private string _connStr;

        #endregion Fields

        #region Constructors

        public SQLiteHelper()
        {
            _connStr = string.Format("Data Source={0};Pooling=true;FailIfMissing=false", AppDomain.CurrentDomain.BaseDirectory + "Data\\" + GetConnStr());
        }

        public SQLiteHelper(string connName)
        {
            _connStr = string.Format("Data Source={0};Pooling=true;FailIfMissing=false", AppDomain.CurrentDomain.BaseDirectory + "Data\\" + connName);
        }

        #endregion Constructors

        #region Public Methods

        public string GetConnStr()
        {
            return ConfigurationManager.AppSettings["SQLiteConnectionString"];
        }

        public void SetConnStr(string connName)
        {
            _connStr = string.Format("Data Source={0};Pooling=true;FailIfMissing=false", AppDomain.CurrentDomain.BaseDirectory + "Data\\" + connName);
        }


        public int ExecuteNoneQuery(string strCmd)
        {
            int row = -1;  
            try
            {
                OpenConnection();
                SQLiteCommand cmd = new SQLiteCommand(strCmd, _conn);
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

            return row;
        }

        public int ExecuteNoneQuery(string strCmd, System.Data.CommandType cmdType, string[] fields, object[] obj)
        {
            int row = -1;
           
            try
            {
                OpenConnection();
                SQLiteCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
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

            return row;
        }

        public object ExecuteScalar(string strCmd)
        {
            OpenConnection();
            SQLiteCommand cmd = new SQLiteCommand(strCmd, _conn);
            object result = null;
            try
            {
               
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
            return result;
        }

        public object ExecuteScalar(string strCmd, System.Data.CommandType cmdType, string[] fields, object[] obj)
        {
            SQLiteCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
            object result = null;
            try
            {
                OpenConnection();
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
           
            try
            {
                OpenConnection();
                SQLiteCommand cmd = new SQLiteCommand(strCmd, _conn);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
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
            return result;
        }

        public DataSet GetSet(string strCmd, CommandType cmdType, string[] fields, object[] obj)
        {
            
            DataSet result = new DataSet();
            try
            {
                OpenConnection();
                SQLiteCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
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

        public SQLiteParameter[] GetSqlParameters(string[] fields, object[] obj)
        {
            try
            {
                SQLiteParameter[] parameters = new SQLiteParameter[obj.Count()];
                for (int i = 0; i < obj.Count(); i++)
                {
                    parameters[i] = new SQLiteParameter(fields[i], obj[i]);
                }


                return parameters;
            }
            catch
            {
                return new SQLiteParameter[] { };
            }
        }

        public void OpenConnection()
        {
            if (_conn == null)
            {
                _conn = new SQLiteConnection(_connStr);

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
        private SQLiteCommand GetCommand(string strCmd, CommandType cmdType, string[] fields, object[] obj)
        {
            SQLiteParameter[] paras = GetSqlParameters(fields, obj);
            SQLiteCommand cmd = new SQLiteCommand(strCmd, _conn);
            cmd.CommandType = cmdType;
            if (paras != null)
            {
                foreach (SQLiteParameter para in paras)
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
