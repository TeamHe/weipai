using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLUtils;

namespace DB_Operation
{
    public class DB
    {
        public static ISQLUtils Connection;
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
           
           Connection = SQLUtils.SQLUtilsFactory.Create();
           DB_Operation.EQUManage.DB_EQU.ClearOnLineState();

        }
        public static DataTable test()
        {
            string a = "select * from Table1;";
            DataTable dt;
            try
            {
                dt = Connection.GetTable(a);
            }
            catch { return null; }
            return dt;
        }

        #region 气象数据存取
        /// <summary>
        /// 气象数据保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int DB_In_Weather(string[] id,DateTime time ,float[] value)
        {
            if (IsExist_Weather(id,time))
                return 0;
            string sql = @"INSERT INTO T_Weather(" +
                    "SmartEquipID," +           //状态监测设备ID
                    "TerminalID," +             //被监测设备ID
                    "dTime," +                  //采样时间
                    "Average_WindSpeed," +    //10分钟平均风速
                    "Average_WindDirection,"+   //10分钟平均风向
                    "Max_WindSpeed," +          //最大风速
                    "Extreme_WindSpeed," +      //极大风速
                    "Standard_WindSpeed," +     //标准风速
                    "Air_Temperature," +        //气温
                    "Humidity," +               //湿度
                    "Air_Pressure,"+            //大气压力
                    "Precipitation,"+           //降雨量
                    "Precipitation_Intensity,"+ //降雨强度
                    "Radiation_Intensity)"+     //光辐射强度
                    "Values(" ;
            sql += "\"" + id[0] + "\",\"" + id[1] + "\",\"" + time.ToString() + "\",";
                    for(int i = 0; i< 10;i++)
                    {
                        sql += "\""+ value[i].ToString()+ "\"," ;
                    }
            sql += "\""+ value[10].ToString()+ "\");" ;

            int m = -1;
            try
            {
                m = Connection.ExecuteNoneQuery(sql);
            }
            catch { return -1; }
            return m;
                   
        }

        /// <summary>
        /// 判断某一条数据是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private static bool IsExist_Weather(string[] id, DateTime time)
        {
            string sql =  string.Format("select * from T_Weather "
                + "where SmartEquipID =  \"{0}\" "
                + "and   TerminalID = \"{1}\" "
                + "and   dTime  = #{2}#;", id[0], id[1], time.ToString());
            try
            {
                var dt = Connection.GetTable(sql);
                if (dt.Rows.Count > 0)
                    return true;
            }
            catch { return false; }
           
            return false;
        }
        
        /// <summary>
        /// 根据id查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DataTable DB_Select_Weather(string id, DateTime start, DateTime end)
        {
            string sql = string.Format("select  SmartEquipID as 设备ID, \n\t"
                    + "TerminalID as 被测设备ID, \n\t"
                    + "dTime as 采集时间, \n\t"
                    + "Average_WindSpeed as 10分钟平均风速,\n\t"     //10分钟平均风速
                    + "Average_WindDirection as 10分钟平均风向,\n\t"   //10分钟平均风向
                    + "Max_WindSpeed as 最大风速,\n\t"          //最大风速
                    + "Extreme_WindSpeed as 极大风速,\n\t"       //极大风速
                    + "Standard_WindSpeed as 标准风速,\n\t"      //标准风速
                    + "Air_Temperature as 气温,\n\t"         //气温
                    + "Humidity as 湿度,\n\t"                //湿度
                    + "Air_Pressure as 大气压力,\n\t"             //大气压力
                    + "Precipitation as 降雨量,\n\t"            //降雨量
                    + "Precipitation_Intensity as 降雨强度,\n\t"  //降雨强度
                    + "Radiation_Intensity as 光辐射强度 \n"      //光辐射强度
                + "from T_Weather \n"
                + "where dTime between #{0}# and #{1}# \n\t"
                    + "and SmartEquipID =  \"{2}\" \n\t"
                + "order by dTime;",start,end,id);

            try
            {
                var dt=  Connection.GetTable(sql);
                return dt;
            }
            catch 
            { 
                return null; 
            }
        }
        /// <summary>
        /// 查询ID
        /// </summary>
        /// <returns></returns>
        public static DataTable DB_Select_ID_Weather()
        {
            string sql = string.Format("select DISTINCT SmartEquipID as 设备ID\n\t"
                + "from T_Weather \n");
            try
            {
                var dt = Connection.GetTable(sql);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 杆塔倾斜数据存取
        /// <summary>
        /// 杆塔倾斜数据保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int DB_In_GTQX(string[] id, DateTime time, float[] value)
        {
            if (IsExist_GTQX(id, time))
                return 0;
            string sql = @"INSERT INTO T_Gtqx(" +
                    "SmartEquipID," +           //状态监测设备ID
                    "TerminalID," +             //被监测设备ID
                    "dTime," +                  //采样时间
                    "Inclination," +            //倾斜度
                    "Inclination_X," +          //顺线倾斜度
                    "Inclination_Y," +          //横向倾斜度
                    "Angle_X," +                //顺线倾斜角
                    "Angle_Y)" +                //横向倾斜角
                    "Values(";
            sql += "\"" + id[0] + "\",\"" + id[1] + "\",\"" + time.ToString() + "\",";
            for (int i = 0; i < 4; i++)
            {
                sql += "\"" + value[i].ToString() + "\",";
            }
            sql += "\"" + value[4].ToString() + "\");";

            int m = -1;
            try
            {
                m = Connection.ExecuteNoneQuery(sql);
            }
            catch { return -1; }
            return m;
        }

        /// 判断某一条数据是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private static bool IsExist_GTQX(string[] id, DateTime time)
        {
            string sql = string.Format("select * from T_Gtqx "
                + "where SmartEquipID =  \"{0}\" "
                + "and   TerminalID = \"{1}\" "
                + "and   dTime  = #{2}#;", id[0], id[1], time.ToString());
            try
            {
                var dt = Connection.GetTable(sql);
                if (dt.Rows.Count > 0)
                    return true;
            }
            catch { return false; }

            return false;
        }

        /// <summary>
        /// 根据id查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DataTable DB_Select_GTQX(string id, DateTime start, DateTime end)
        {
            string sql = string.Format("select  SmartEquipID as 设备ID, \n\t"
                    + "TerminalID as 被测设备ID, \n\t"
                    + "dTime as 采集时间, \n\t"
                    +"Inclination as 倾斜度, "            //倾斜度
                    +"Inclination_X as 顺线倾斜度, "           //顺线倾斜度
                    +"Inclination_Y as 横向倾斜度, "           //横向倾斜度
                    + "Angle_X as 顺线倾斜角 , "                 //顺线倾斜角
                    +"Angle_Y  as 横向倾斜角 "                //横向倾斜角
                + "from T_Gtqx \n"
                + "where dTime between #{0}# and #{1}# \n\t"
                    + "and SmartEquipID =  \"{2}\" \n\t"
                + "order by dTime;", start, end, id);

            try
            {
                var dt = Connection.GetTable(sql);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 查询ID
        /// </summary>
        /// <returns></returns>
        public static DataTable DB_Select_ID_GTQX()
        {
            string sql = string.Format("select DISTINCT SmartEquipID as 设备ID\n\t"
                + "from T_Gtqx \n");
            try
            {
                var dt = Connection.GetTable(sql);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        #endregion
       

        #region 微风振动数据存取
        /// <summary>
        /// 微风振动数据保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int DB_In_ZD_Feature(string[] id, DateTime time, float[] value)
        {
            if (IsExist_ZD_Feature(id,time,value[0]))
                return 0;
            string sql = @"INSERT INTO T_ZD_Feature(" +
                    "SmartEquipID," +           //状态监测设备ID
                    "TerminalID," +             //被监测设备ID
                    "dTime," +                  //采样时间
                    "Unit_No," +                //采集单元序号
                    "Strain_Amplitude," +       //动弯应变幅值
                    "Bending_Amplitude," +      //弯曲振幅
                    "Vibration_Frequency)" +    //微风振动频率
                    "Values(";
            sql += "\"" + id[0] + "\",\"" + id[1] + "\",\"" + time.ToString() + "\",";
            for (int i = 0; i < 3; i++)
            {
                sql += "\"" + value[i].ToString() + "\",";
            }
            sql += "\"" + value[3].ToString() + "\");";

            int m = -1;
            try
            {
                m = Connection.ExecuteNoneQuery(sql);
            }
            catch { return -1; }
            return m;
        }

        /// <summary>
        /// 判断某一条数据是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private static bool IsExist_ZD_Feature(string[] id, DateTime time,float unit_NO)
        {
            string sql = string.Format("select * from T_ZD_Feature "
                + "where SmartEquipID =  \"{0}\" "
                + "and   TerminalID = \"{1}\" "
                + "and   dTime  = #{2}# "
                + "and   Unit_No = {3};", id[0], id[1], time.ToString(),unit_NO);
            try
            {
                var dt = Connection.GetTable(sql);
                if (dt.Rows.Count > 0)
                    return true;
            }
            catch { return false; }

            return false;
        }

        /// <summary>
        /// 根据id查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DataTable DB_Select_ZD_Feature(string id, DateTime start, DateTime end)
        {
            string sql = string.Format("select  SmartEquipID as 设备ID, \n\t"
                    + "TerminalID as 被测设备ID, \n\t"
                    + "dTime as 采集时间, \n\t"+
                    "Unit_No as 采集单元序号," +                //采集单元序号
                    "Strain_Amplitude as 动弯应变幅值, " +       //动弯应变幅值
                    "Bending_Amplitude as 弯曲振幅, " +      //弯曲振幅
                    "Vibration_Frequency as 微风振动频率 "      //微风振动频率
                + "from T_ZD_Feature \n"
                + "where dTime between #{0}# and #{1}# \n\t"
                    + "and SmartEquipID =  \"{2}\" \n\t"
                + "order by dTime;", start, end, id);

            try
            {
                var dt = Connection.GetTable(sql);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 查询ID
        /// </summary>
        /// <returns></returns>
        public static DataTable DB_Select_ID_ZD_Feature()
        {
            string sql = string.Format("select DISTINCT SmartEquipID as 设备ID\n\t"
                + "from T_ZD_Feature \n");
            try
            {
                var dt = Connection.GetTable(sql);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        #endregion
       

        #region 覆冰数据存取
        /// <summary>
        /// 覆冰及不均衡张力差数据保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int DB_In_Ice(string[] id, DateTime time, float[] value)
        {
            if (IsExist_Ice(id, time))
                return 0;
            string sql = string.Format("INSERT INTO T_LeadPull(" +
                    "SmartEquipID," +           //状态监测设备ID
                    "TerminalID," +             //被监测设备ID
                    "DTime," +                  //采样时间
                    "Equal_IceThickness ," +    //等值覆冰厚度
                    "Tension," +                //综合悬挂载荷
                    "Tension_Difference," +     //不均衡张力差
                    "Original_Tension{0},"+
                    "Windage_Yaw_Angle{0},"+
                    "Deflection_Angle{0},"+
                        "Original_Tension{1}," +
                        "Windage_Yaw_Angle{1}," +
                        "Deflection_Angle{1}," +
                    "Original_Tension{2}," +
                    "Windage_Yaw_Angle{2}," +
                    "Deflection_Angle{2})" +
                    "Values(",1,2,3);
            sql += "\"" + id[0] + "\",\"" + id[1] + "\",\"" + time.ToString() + "\",";
            for (int i = 0; i < 11; i++)
            {
                sql += "\"" + value[i].ToString() + "\",";
            }
            sql += "\"" + value[11].ToString() + "\");";

            int m = -1;
            try
            {
                m = Connection.ExecuteNoneQuery(sql);
            }
            catch { return -1; }
            return m;
        }

        /// <summary>
        /// 判断某一条数据是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private static bool IsExist_Ice(string[] id, DateTime time)
        {
            string sql = string.Format("select * from T_LeadPull "
                + "where SmartEquipID =  \"{0}\" "
                + "and   TerminalID = \"{1}\" "
                + "and   dTime  = #{2}#;", id[0], id[1], time.ToString());
            try
            {
                var dt = Connection.GetTable(sql);
                if (dt.Rows.Count > 0)
                    return true;
            }
            catch { return false; }

            return false;
        }

        /// <summary>
        /// 根据id查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DataTable DB_Select_Ice(string id, DateTime start, DateTime end)
        {
            string sql = string.Format("select  SmartEquipID as 设备ID, \n\t"+  //状态监测设备ID
                    "TerminalID as 被监测设备ID," +             //被监测设备ID
                    "DTime as 采样时间," +                      //采样时间
                    "Equal_IceThickness as 等值覆冰厚度," +    //等值覆冰厚度
                    "Tension as 综合悬挂载荷," +                //综合悬挂载荷
                    "Tension_Difference as 不均衡张力差," +     //不均衡张力差
                            "Original_Tension{0} as 原始拉力{0}," +
                            "Windage_Yaw_Angle{0} as 风偏角{0}," +
                            "Deflection_Angle{0} as 偏斜角{0}," +
                        "Original_Tension{1} as 原始拉力{1}," +
                        "Windage_Yaw_Angle{1} as 风偏角{1}," +
                        "Deflection_Angle{1} as 偏斜角{1}," +
                            "Original_Tension{2} as 原始拉力{2}," +
                            "Windage_Yaw_Angle{2} as 风偏角{2}," +
                            "Deflection_Angle{2} as 偏斜角{2} " +
                "from T_LeadPull \n"
                + "where dTime between #{3}# and #{4}# \n\t"
                    + "and SmartEquipID =  \"{5}\" \n\t"
                + "order by dTime;", 1,2,3,start, end, id);

            try
            {
                var dt = Connection.GetTable(sql);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 查询ID
        /// </summary>
        /// <returns></returns>
        public static DataTable DB_Select_ID_Ice()
        {
            string sql = string.Format("select DISTINCT SmartEquipID as 设备ID\n\t"
                + "from T_LeadPull \n");
            try
            {
                var dt = Connection.GetTable(sql);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 线温数据存取
        /// <summary>
        /// 线温数据保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int DB_In_Line_Temperature(string[] id, DateTime time, float[] value)
        {
            string sql = @"INSERT INTO T_Temperature(" +
                    "SmartEquipID," +           //状态监测设备ID
                    "TerminalID," +             //被监测设备ID
                    "DTime," +                  //采样时间
                    "Unit_Sum ," +              //采集单元总数
                    "Unit_No," +                //采集单元序号
                    "Line_Temperature)" +       //线温
                    "Values(";
            sql += "\"" + id[0] + "\",\"" + id[1] + "\",\"" + time.ToString() + "\",";
            for (int i = 0; i < 2; i++)
            {
                sql += "\"" + value[i].ToString() + "\",";
            }
            sql += "\"" + value[2].ToString() + "\");";

            int m = -1;
            try
            {
                m = Connection.ExecuteNoneQuery(sql);
            }
            catch { return -1; }
            return m;
        }
        #endregion
     
        
    }
}
