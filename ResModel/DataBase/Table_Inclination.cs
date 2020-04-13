using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResModel.DataBase
{
    class Table_Inclination
    {
        public static string TableName
        {
            get { return "t_data_gtqx"; }
        }
        public static string[] Cloums =
        {
            "Inclination",              //倾斜度
            "Inclination_X",            //顺线倾斜度
            "Inclination_Y",            //横向倾斜度
            "Angle_X",                  //顺线倾斜角
            "Angle_Y"                   //横向倾斜角
       
        };

        public static string[] CloumsName =
         {
            "倾斜度",
            "顺线倾斜度",
            "横向倾斜度",
            "顺线倾斜角",
            "横向倾斜角"
         };
    }
}
