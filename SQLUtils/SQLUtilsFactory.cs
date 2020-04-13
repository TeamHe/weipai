using System;
namespace SQLUtils
{
   public class SQLUtilsFactory
    {
        /// <summary>
        /// 工厂类创建sql帮助类
        /// </summary>
        /// <returns></returns>
        public static ISQLUtils Create()
        {
            string dbServerName = SettingsDataBase.Default.DBString;
            switch (dbServerName)
            {
                case "SQLServer":
                    return new SQLServerHelper();
                case "Oracle":
                    //return new OracleHelper();
                case "SQLite":
                    return new SQLiteHelper();
                case "OleDb_Access":
                    OleDbHelper oleDbHelper = new OleDbHelper();
                    oleDbHelper.SetConnStr();
                    return oleDbHelper;
                case "OleDb":
                    return new OleDbHelper();
                case "MySql":
                    return new MySqlHelper();
            }
            return new SQLServerHelper();
        }

        public static ISQLUtils Create(SQLConnEnum sqlConnEnum, string conStr)
        {
            switch (sqlConnEnum)
            {
                case SQLConnEnum.SQLServer:
                    return new SQLServerHelper(conStr);
                case SQLConnEnum.OleDb:
                    return new OleDbHelper(conStr);
                case SQLConnEnum.SQLite:
                    return new SQLiteHelper(conStr);
                case SQLConnEnum.Oracle:
                    //return new OracleHelper(conStr);
                case SQLConnEnum.MySql:
                    return new MySqlHelper(conStr);
            }
            return new SQLServerHelper();
        }

        public static ISQLUtils Create(string sqlConnEnum, string conStr)
        {
            switch (sqlConnEnum)
            {
                case "SQLServer":
                    return new SQLServerHelper(conStr);
                case "OleDb":
                    return new OleDbHelper(conStr);
                case "SQLite":
                    return new SQLiteHelper(conStr);
                case "Oracle":
                    //return new OracleHelper(conStr);
                case "MySql":
                    return new MySqlHelper(conStr);
                case "OleDb_Access":
                    OleDbHelper oleDbHelper = new OleDbHelper();
                    oleDbHelper.SetConnStr();
                    return oleDbHelper;
            }
            return new SQLServerHelper();
        }
       /// <summary>
       /// 获取数据库连接
       /// </summary>
       /// <param name="constr"></param>
       /// <returns></returns>
        public static SQLConnEnum GetDBconStr(ref string constr)
        {
            constr = SettingsDataBase.Default.ConnectionString;
            return (SQLConnEnum)Enum.Parse(typeof(SQLConnEnum), SettingsDataBase.Default.DBString);
        }
       /// <summary>
       /// 配置数据库连接
       /// </summary>
       /// <param name="sqlConnEnum"></param>
       /// <param name="conStr"></param>
        public static void SetDBconStr(SQLConnEnum sqlConnEnum, string conStr)
        {
            SettingsDataBase.Default.DBString = sqlConnEnum.ToString();
            SettingsDataBase.Default.ConnectionString = conStr;
            SettingsDataBase.Default.Save();
        }

    }

   public enum SQLConnEnum
   {
       SQLServer = 0,
       Oracle,
       SQLite,
       OleDb,
       MySql
   }
}
