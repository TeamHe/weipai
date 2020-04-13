using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Data.OleDb;
using System.IO;
using System.Data;

namespace SQLUtils
{
   public class UtilsMethods
    {
        /// <summary>
        ///通过读取注册表判断当前office版本号，根据安装office版本得到Excel ole连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        public static String GetExcelConnStr(string filePath)
        {
            string provider = "Microsoft.Jet.OLEDB.4.0";
            string properties = "Excel 8.0";
            RegistryKey hkml = Registry.CurrentUser;
            RegistryKey software = hkml.OpenSubKey(@"Software\Microsoft\Office", true);
            string[] versions = software.GetSubKeyNames();
            string versionStr = versions.ToString();
            if (versionStr.Contains("14.0") || versionStr.Contains("12.0"))
            {
                provider = "Microsoft.ACE.OLEDB.12.0";
                properties = "Excel 12.0";
            }
            return String.Format(" Provider = {0} ; Data Source ={1};Extended Properties='{2};IMEX=1'", provider, filePath, properties);
        }

        public static List<string> GetExcelTableNames(string excelFileName)
        {
            List<string> tableNameList = new List<string>();
            if (File.Exists(excelFileName))
            {
                using (OleDbConnection conn = new OleDbConnection(GetExcelConnStr(excelFileName)))
                {
                    conn.Open();
                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string tableName = dt.Rows[i][2].ToString();
                        tableName = tableName.Substring(1, tableName.Length - 2);
                        tableNameList.Add(tableName);
                    }
                }
            }
            return tableNameList;
        }
    }
}
