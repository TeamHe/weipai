using ResModel;
using System;
using System.Text;
using ResModel.PowerPole;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 导线风偏数据报
    /// </summary>
    public class Data_FP
    {
        private static string CMD_ID;
        private static int PacLength = 17 //被监测设备 ID
           + 4 //采集时间
           + 4 //风偏角
           + 4 //偏斜角
           + 4 //最小电气间隙 
          ;//
        /// <summary>
        /// ΢微气象数据解析
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_NO"></param>
        /// <param name="data"></param>
        public static void Deal(IPowerPole pole, byte frame_NO, byte[] data)
        {
            CMD_ID = pole.CMD_ID;
            string Component_ID;        //被监测设备 ID
            DateTime Time_Stamp;        //采集时间
            float Windage_Yaw_Angle;    //风偏角
            float Deflection_Angle;     //偏斜角
            float Least_Clearance;      //最小电气间隙

            if (data.Length != PacLength)
                return;
            string pacMsg = "";
            int StartNo = 0;
            //被测设备ID
            Component_ID = Encoding.Default.GetString(data, StartNo, 17);
            pacMsg += "被测设备ID:" + Component_ID + " ";
            StartNo += 17;

            //采集时间
            Time_Stamp = Tools.TimeUtil.BytesToDate(data, StartNo);
            pacMsg += "采集时间:" + Time_Stamp.ToString() + " ";
            StartNo += 4;

            //风偏角
            Windage_Yaw_Angle = BitConverter.ToSingle(data, StartNo);
            pacMsg += "风偏角:" + Windage_Yaw_Angle.ToString("f2") + "° ";
            StartNo += 4;

            //偏斜角
            Deflection_Angle = BitConverter.ToSingle(data, StartNo);
            pacMsg += "偏斜角:" + Deflection_Angle.ToString("f2") + "°  ";
            StartNo += 4;

            //最小电气间隙
            Least_Clearance = BitConverter.ToSingle(data, StartNo);
            pacMsg += "最小电气间隙:" + Least_Clearance.ToString("f3") + "m  ";
            StartNo += 4;            

            //显示发送的数据
            DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.rec,
                    pole,
                    "风偏数据报",
                    pacMsg));
        }
    }
}
