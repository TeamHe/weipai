using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResModel.DataBase
{
    public class Table_Ice
    {
        public static string TableName
        {
            get { return "t_data_ice"; }
        }
        public static string[] Cloums =
        {
            "Equal_IceThickness",           //等值覆冰厚度
            "Tension",                      //综合悬挂载荷
            "Tension_Difference",           //不均衡张力差
            "Original_Tension1",            //原始拉力值
            "Windage_Yaw_Angle1",           //风偏角
            "Deflection_Angle1",            //偏斜角
            "Original_Tension2",            //原始拉力2
            "Windage_Yaw_Angle2",           //风偏角2
            "Deflection_Angle2"             //偏斜角2
       
        };

        public static string[] CloumsName =
         {
            "等值覆冰厚度",
            "综合悬挂载荷",
            "不均衡张力差",
            "原始拉力值1",
            "风偏角1",
            "偏斜角2",
            "原始拉力值2",
            "风偏角2",
            "偏斜角2" 
         };
    }
}
