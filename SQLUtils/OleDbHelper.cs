using System;
using System.Linq;
using System.Data.OleDb;
using System.Data;

namespace SQLUtils
{
  public class OleDbHelper:ISQLUtils
    {
        #region Fields

        private OleDbConnection  _conn;

        private string _connStr;

        #endregion Fields

        #region Constructors

        internal OleDbHelper()
        {
            _connStr = GetConnStr();
        }

        public OleDbHelper(string connStr)
        {
            _connStr = connStr;
        }

        #endregion Constructors

        #region Public Methods

        public void SetConnStr()
        {
            _connStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}",
                                     AppDomain.CurrentDomain.BaseDirectory +  _connStr);
        }

        public string GetConnStr()
        {
            //return System.Configuration.ConfigurationSettings.AppSettings["OleConnectionString"];
             _connStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}",
                                     AppDomain.CurrentDomain.BaseDirectory +  SettingsDataBase.Default.OleConnectionString);
             return _connStr;
        }

        public int ExecuteNoneQuery(string strCmd)
        {
            int row = -1;
            
            try
            {
                OpenConnection();
                OleDbCommand cmd = new OleDbCommand(strCmd, _conn);
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
            OpenConnection();
            OleDbCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
            try
            {
              
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
            OleDbCommand cmd = new OleDbCommand(strCmd, _conn);
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
            OleDbCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
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
            //return null;
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
                OleDbCommand cmd = new OleDbCommand(strCmd, _conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
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
                OleDbCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
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

        public OleDbParameter[] GetOleDbParameters(string[] fields, object[] obj)
        {
            try
            {
                OleDbParameter[] parameters = new OleDbParameter[obj.Count()];
                for (int i = 0; i < obj.Count(); i++)
                {
                    parameters[i] = new OleDbParameter(fields[i], obj[i]);
                }


                return parameters;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void OpenConnection()
        {
            try
            {
                if (_conn == null)
                {
                    _conn = new OleDbConnection(_connStr);

                }
                if (_conn.State != ConnectionState.Open)
                {

                    _conn.Open();

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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
        private OleDbCommand GetCommand(string strCmd, CommandType cmdType, string[] fields, object[] obj)
        {
            OleDbParameter[] paras = GetOleDbParameters(fields, obj);
            OleDbCommand cmd = new OleDbCommand(strCmd, _conn);
            cmd.CommandType = cmdType;
            if (paras != null)
            {
                foreach (OleDbParameter para in paras)
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
