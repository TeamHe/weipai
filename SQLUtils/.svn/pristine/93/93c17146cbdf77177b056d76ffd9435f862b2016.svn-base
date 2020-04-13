using System;
using System.Data;

namespace SQLUtils
{
   public interface ISQLUtils:IDisposable
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        string GetConnStr();

        /// <summary>
        /// 打开连接
        /// </summary>
        void OpenConnection();

        /// <summary>
        /// 关闭连接
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// 执行一条sql语句，返回事务执行结果
        /// </summary>
        /// <param name="strCmd">ql语句</param>
        /// <returns>受影响的记录数</returns>
        int ExecuteNoneQuery(string strCmd);

        /// <summary>
        /// 执行带参数的sql语句或存储过程，返回事务执行结果
        /// </summary>
        /// <param name="strCmd">sql语句或存储过程名</param>
        /// <param name="cmdType">说明strCmd是sql语句或存储过程名</param>
        /// <param name="fields">参数名</param>
        /// <param name="obj">参数值</param>
        /// <returns>事务执行结果</returns>
        int ExecuteNoneQuery(string strCmd, CommandType cmdType, string[] fields, object[] obj);

        /// <summary>
        /// 执行sql语句，返回首记录
        /// </summary>
        /// <param name="strCmd">sql语句</param>
        /// <returns>首记录</returns>
        object ExecuteScalar(string strCmd);

        /// <summary>
        /// 执行sql语句或存储过程，返回首记录
        /// </summary>
        /// <param name="strCmd">sql语句或存储过程名</param>
        /// <param name="cmdType">说明strCmd是sql语句或存储过程名</param>
        /// <param name="fields">参数名</param>
        /// <param name="obj">参数值</param>
        /// <returns>首记录</returns>
        object ExecuteScalar(string strCmd, CommandType cmdType, string[] fields, object[] obj);

        /// <summary>
        /// 执行sql语句，返回数据表
        /// </summary>
        /// <param name="strCmd">sql语句</param>
        /// <returns>数据表</returns>
        DataTable GetTable(string strCmd);

        /// <summary>
        /// 执行sql语句或存储过程，返回数据表
        /// </summary>
        /// <param name="strCmd">sql语句或存储过程名</param>
        /// <param name="cmdType">说明strCmd是sql语句或存储过程名</param>
        /// <param name="fields">参数名</param>
        /// <param name="obj">参数值</param>
        /// <returns>数据表</returns>
        DataTable GetTable(string strCmd, CommandType cmdType, string[] fields, object[] obj);

        /// <summary>
        /// 执行sql语句返回数据表首行
        /// </summary>
        /// <param name="strCmd">sql语句</param>
        /// <returns>数据表首行</returns>
        DataRow GetFirstRow(string strCmd);

        /// <summary>
        /// 执行sql语句或存储过程，返回数据表首行
        /// </summary>
        /// <param name="strCmd">sql语句或存储过程名</param>
        /// <param name="cmdType">说明strCmd是sql语句或存储过程名</param>
        /// <param name="fields">参数名</param>
        /// <param name="obj">参数值</param>
        /// <returns>数据表首行</returns>
        DataRow GetFirstRow(string strCmd, CommandType cmdType, string[] fields, object[] obj);

        /// <summary>
        /// 执行sql语句，返回数据集
        /// </summary>
        /// <param name="strCmd">sql语句</param>
        /// <returns>返回数据集</returns>
        DataSet GetSet(string strCmd);

        /// <summary>
        /// 执行sql语句或存储过程，返回数据集
        /// </summary>
        /// <param name="strCmd">sql语句或存储过程名</param>
        /// <param name="cmdType">说明strCmd是sql语句或存储过程名</param>
        /// <param name="fields">参数名</param>
        /// <param name="obj">参数值</param>
        /// <returns>数据集</returns>
        DataSet GetSet(string strCmd, CommandType cmdType, string[] fields, object[] obj);


    }
}
