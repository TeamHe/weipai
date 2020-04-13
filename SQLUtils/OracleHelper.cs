using System;
using System.Data;
using System.Linq;
//using System.Data.OracleClient;

namespace SQLUtils
{
    //class OracleHelper:ISQLUtils
    //{
    //    #region Fields

    //    private OracleConnection _conn;

    //    private string _connStr;

    //    #endregion Fields

    //    #region Constructors

    //    public OracleHelper()
    //    {
    //        _connStr = GetConnStr();
    //    }

    //    public OracleHelper(string conStr)
    //    {
    //        _connStr = conStr;
    //    }

    //    #endregion Constructors

    //    #region Public Methods

    //    public string GetConnStr()
    //    {
    //        return System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
    //    }

    //    public void SetConnStr(string connStr)
    //    {
    //        _connStr = connStr;
    //    }

    //    public int ExecuteNoneQuery(string strCmd)
    //    {
    //        int row = -1;
    //        OracleCommand cmd = new OracleCommand(strCmd, _conn);
    //        try
    //        {
    //            OpenConnection();
    //            row = cmd.ExecuteNonQuery();
    //        }
    //        catch (Exception ex)
    //        {
    //            return -1;
    //        }
    //        finally
    //        {
    //            CloseConnection();
    //        }

    //        return row;
    //    }

    //    public int ExecuteNoneQuery(string strCmd, System.Data.CommandType cmdType, string[] fields, object[] obj)
    //    {
    //        int row = -1;
    //        OracleCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
    //        try
    //        {
    //            OpenConnection();
    //            row = cmd.ExecuteNonQuery();
    //        }
    //        catch (Exception ex)
    //        {
    //            return -1;
    //        }
    //        finally
    //        {
    //            CloseConnection();
    //        }

    //        return row;
    //    }

    //    public object ExecuteScalar(string strCmd)
    //    {
    //        OracleCommand cmd = new OracleCommand(strCmd, _conn);
    //        object result = null;
    //        try
    //        {
    //            OpenConnection();
    //            result = cmd.ExecuteScalar();

    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //            throw new Exception(ex.Message);
    //        }
    //        finally
    //        {
    //            CloseConnection();
    //        }
    //        return result;
    //    }

    //    public object ExecuteScalar(string strCmd, System.Data.CommandType cmdType, string[] fields, object[] obj)
    //    {
    //        OracleCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
    //        object result = null;
    //        try
    //        {
    //            OpenConnection();
    //            result = cmd.ExecuteScalar();
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //            throw new Exception(ex.Message);
    //        }
    //        finally
    //        {
    //            CloseConnection();
    //        }
    //        return result;
    //    }

    //    public DataTable GetTable(string strCmd)
    //    {
    //        return GetSet(strCmd).Tables[0];
    //    }

    //    public DataTable GetTable(string strCmd, CommandType cmdType, string[] fields, object[] obj)
    //    {
    //        return GetSet(strCmd, cmdType, fields, obj).Tables[0];
    //    }

    //    public DataSet GetSet(string strCmd)
    //    {
    //        DataSet result = new DataSet();
    //        try
    //        {
    //            OpenConnection();
    //            OracleCommand cmd = new OracleCommand(strCmd, _conn);
    //            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
    //            adapter.Fill(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //            throw new Exception(ex.Message);
    //        }
    //        finally
    //        {
    //            CloseConnection();
    //        }
    //        return result;
    //    }

    //    public DataSet GetSet(string strCmd, CommandType cmdType, string[] fields, object[] obj)
    //    {
          
    //        DataSet result = new DataSet();
    //        try
    //        {
    //            OpenConnection();
    //            OracleCommand cmd = GetCommand(strCmd, cmdType, fields, obj);
    //            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
    //            adapter.Fill(result);
    //        }
    //        catch (Exception ex)
    //        {
    //           // return null;
    //            throw new Exception(ex.Message);
    //        }
    //        finally
    //        {
    //            CloseConnection();
    //        }
    //        return result;
    //    }

    //    public DataRow GetFirstRow(string strCmd)
    //    {
    //        DataTable dataTable = GetTable(strCmd);
    //        if (dataTable.Rows.Count > 0)
    //        {
    //            return dataTable.Rows[0];
    //        }
    //        return null;
    //    }

    //    public DataRow GetFirstRow(string strCmd, CommandType cmdType, string[] fields, object[] obj)
    //    {
    //        DataTable dataTable = GetTable(strCmd, cmdType, fields, obj);
    //        if (dataTable.Rows.Count > 0)
    //        {
    //            return dataTable.Rows[0];
    //        }
    //        return null;
    //    }

    //    #endregion Public Methods

    //    #region Private Methods

    //    public OracleParameter[] GetSqlParameters(string[] fields, object[] obj)
    //    {
    //        try
    //        {
    //            OracleParameter[] parameters = new OracleParameter[obj.Count()];
    //            for (int i = 0; i < obj.Count(); i++)
    //            {
    //                parameters[i] = new OracleParameter(fields[i], obj[i]);
    //            }


    //            return parameters;
    //        }
    //        catch
    //        {
    //            return new OracleParameter[] { };
    //        }
    //    }

    //    public void OpenConnection()
    //    {
    //        if (_conn == null)
    //        {
    //            _conn = new OracleConnection(_connStr);

    //        }
    //        if (_conn.State != ConnectionState.Open)
    //        {

    //            _conn.Open();

    //        }
    //    }

    //    public void CloseConnection()
    //    {
    //        if (_conn.State != ConnectionState.Closed)
    //        {
    //            _conn.Close();
    //        }
    //    }

    //    /// <summary>
    //    /// <param name="strCmd">sql语句或存储过程名</param>
    //    /// <param name="cmdType">存储过程类型</param>
    //    /// <param name="paras">需要添加的参数</param>
    //    /// </summary>
    //    private OracleCommand GetCommand(string strCmd, CommandType cmdType, string[] fields, object[] obj)
    //    {
    //        OracleParameter[] paras = GetSqlParameters(fields, obj);
    //        OracleCommand cmd = new OracleCommand(strCmd, _conn);
    //        cmd.CommandType = cmdType;
    //        if (paras != null)
    //        {
    //            foreach (OracleParameter para in paras)
    //                cmd.Parameters.Add(para);
    //        }
    //        return cmd;
    //    }

    //    #endregion Private Methods

    //    #region IDisposable 成员

    //    public void Dispose()
    //    {
    //        GC.SuppressFinalize(true);
    //    }

    //    #endregion
    //}
}
