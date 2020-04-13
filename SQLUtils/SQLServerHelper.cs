using System;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Configuration;

namespace SQLUtils
{
  public class SQLServerHelper:ISQLUtils
    {
        #region Fields

        private static SqlConnection _conn;

        private string _connStr;

        #endregion Fields

        #region Constructors
        
        public SQLServerHelper()
        {
            _connStr = GetConnStr();
        }
        
        public SQLServerHelper(string connStr)
        {
            _connStr = connStr;
        }


        #endregion Constructors

        #region Public Methods

        public void SetConnStr(string connName)
        {
            //_connStr = System.Configuration.ConfigurationSettings.AppSettings[connName];
            _connStr = ConfigurationManager.AppSettings[connName];
        }

        public string GetConnStr()
        {
            return _connStr = ConfigurationManager.AppSettings["ConnectionString"];
        }
       
        public int ExecuteNoneQuery(string strCmd)
        {
            int row = -1;
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand(strCmd, _conn);
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
            SqlCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
            try
            {
                OpenConnection();
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
            SqlCommand cmd = new SqlCommand(strCmd, _conn);
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

        public object ExecuteScalar(string strCmd, System.Data.CommandType cmdType, string[] fields, object[] obj)
        {
            SqlCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
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
                SqlCommand cmd = new SqlCommand(strCmd, _conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
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
            SqlCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
            DataSet result = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            try
            {
                OpenConnection();
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

        public SqlParameter[] GetSqlParameters(string[] fields, object[] obj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[obj.Count()];
                for (int i = 0; i < obj.Count(); i++)
                {
                    parameters[i] = new SqlParameter(fields[i], obj[i]);
                }


                return parameters;
            }
            catch
            {
                return new SqlParameter[] { };
            }
        }

        public void OpenConnection()
        {
            if (_conn == null)
            {
                _conn = new SqlConnection(_connStr);

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
        private SqlCommand GetCommand(string strCmd, CommandType cmdType, string[] fields, object[] obj)
        {
            SqlParameter[] paras = GetSqlParameters(fields, obj);
            SqlCommand cmd = new SqlCommand(strCmd, _conn);
            cmd.CommandType = cmdType;
            if (paras != null)
            {
                foreach (SqlParameter para in paras)
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
