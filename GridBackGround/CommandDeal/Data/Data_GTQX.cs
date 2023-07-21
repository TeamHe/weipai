using ResModel;
using System;
using System.Text;
using ResModel.PowerPole;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 杆塔倾斜数据报
    /// </summary>
    public class Data_GTQX
    {
         private static string CMD_ID;
         private static int PacLength = 17 //被监测设备 ID
            + 4 //采集时间
            + 4 //倾斜度
            + 4 //横向倾斜度
            + 4 //顺线倾斜角 
            + 4//横向倾斜角
            + 4;//
        /// <summary>
        /// 杆塔倾斜数据解析
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_NO"></param>
        /// <param name="data"></param>
         public static void Deal(IPowerPole pole, byte frame_NO, byte[] data)
         {
             CMD_ID = pole.CMD_ID;
             string Component_ID;   //被监测设备 ID
             DateTime Time_Stamp;   //采集时间
             float Inclination;     //倾斜度
             float Inclination_X;   //顺线倾斜度
             float Inclination_Y;   //横向倾斜度
             float Angle_X;         //顺线倾斜角
             float Angle_Y;         //横向倾斜角

             if (data.Length != PacLength)
                 return;
             float[] value = new float[5];
             string[] id = new string[2];
             id[0] = CMD_ID;

             string pacMsg = "";
             int StartNo = 0;
             //被测设备ID
             Component_ID = Encoding.Default.GetString(data, StartNo, 17);
             id[1] = Component_ID;
             pacMsg += "被测设备ID:" + Component_ID + " ";
             StartNo += 17;

             //采集时间
             Time_Stamp = Tools.TimeUtil.BytesToDate(data, StartNo);
             pacMsg += "采集时间:" + Time_Stamp.ToString() + " ";
             StartNo += 4;

             //倾斜度
             Inclination = BitConverter.ToSingle(data, StartNo);
             Inclination = (float)((int)(Inclination * 10) / 10.0);
             value[0] = Inclination;
             pacMsg += "倾斜度:" + Inclination.ToString("f1") + "mm/m  ";
             StartNo += 4;
           
             //顺线倾斜度
             Inclination_X = BitConverter.ToSingle(data, StartNo);
             Inclination_X = (float)((int)(Inclination_X * 10) / 10.0);
             value[1] = Inclination_X; 
             pacMsg += "倾斜度:" + Inclination_X.ToString("f1") + "mm/m  ";
             StartNo += 4;

             //横向倾斜度
             Inclination_Y = BitConverter.ToSingle(data, StartNo);
             Inclination_Y = (float)((int)(Inclination_Y * 10) / 10.0);
             value[2] = Inclination_Y;
             pacMsg += "横向倾斜度:" + Inclination_Y.ToString("f1") + "mm/m  ";
             StartNo += 4;

             //顺线倾斜角
             Angle_X = BitConverter.ToSingle(data, StartNo);
             Angle_X = (float)((int)(Angle_X * 100) / 100.0);
             value[3] = Angle_X;
             pacMsg += "顺线倾斜角:" + Angle_X.ToString("f2") + "°  ";
             StartNo += 4;

             //横向倾斜角
             Angle_Y = BitConverter.ToSingle(data, StartNo);
             Angle_Y = (float)((int)(Angle_Y * 100) / 100.0);
             value[4] = Angle_Y;
             pacMsg += "横向倾斜角:" + Angle_Y.ToString("f2") + "°  ";
             StartNo += 4;

             int temp = DB_Operation.DB.DB_In_GTQX(id, Time_Stamp, value);
             if (temp == 1)
                 pacMsg += "数据保存成功";
             else
                 if (temp == 0)
                     pacMsg += "数据已存在";
                 else
                     pacMsg += "数据保存失败";

             //显示发送的数据
             PacketAnaLysis.DisPacket.NewRecord(
                 new DataInfo(
                     DataInfoState.rec,
                      pole,
                     "杆塔倾斜数据报",
                     pacMsg));
         }
    }
}
