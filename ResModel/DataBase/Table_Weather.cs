using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResModel.DataBase
{
    public class Table_Weather
    {
        public static string TableName 
        {
            get { return "T_Weather"; }
        }
        public static string[] Cloums =
        {
            "Average_WindSpeed",              //10分钟平均风速
            "Average_WindDirection",          //10分钟平均风向
            "Max_WindSpeed",                  //最大风速
            "Extreme_WindSpeed",              //极大风速
            "Standard_WindSpeed",             //标准风速
            "Air_Temperature",                //气温
            "Humidity",                       //湿度
            "Air_Pressure",                   //大气压力
            "Precipitation",                  //降雨量
            "Precipitation_Intensity",        //降雨强度
            "Radiation_Intensity"            //光辐射强度
        };

        public static string[] CloumsName =
         {
            "10分钟平均风速",
            "10分钟平均风向",
            "最大风速",
            "极大风速",
            "标准风速",
            "气温",
            "湿度",
            "大气压力",
            "降雨量",
            "降雨强度",
            "光辐射强度"
         };
    }
}
