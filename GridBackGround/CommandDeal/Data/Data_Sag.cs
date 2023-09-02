using ResModel;
using System;
using System.Text;
using ResModel.PowerPole;
using cma.service.PowerPole;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 导线弧垂数据报
    /// </summary>
    public class Data_Sag
    {
        private static string CMD_ID;
        private static int PacLength = 17 //被监测设备 ID
           + 4 //采集时间
           + 4 //导线弧垂
           + 4 //导线对地距离
           + 4 //线夹出口处导线切线与水平线夹角 
           + 1;//测量法标识
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
            float Conductor_Sag;        //导线弧垂
            float Toground_Distance;    //导线对地距离 
            float Angle;                //线夹出口处导线切线与水平线夹角
            int Measure_Flag;           //测量法标识
         
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

            //导线弧垂
            Conductor_Sag = BitConverter.ToSingle(data, StartNo);
            pacMsg += "导线弧垂:" + Conductor_Sag.ToString("f3") + "m  ";
            StartNo += 4;

            //导线对地距离
            Toground_Distance = BitConverter.ToSingle(data, StartNo);
            pacMsg += "导线对地距离:" + Toground_Distance.ToString("f3") + "m  ";
            StartNo += 4;

            //线夹出口处导线切线与水平线夹角
            Angle = BitConverter.ToSingle(data, StartNo);
            pacMsg += "横向倾斜度:" + Angle.ToString("f2") + "° ";
            StartNo += 4;

            //测量法标识
            Measure_Flag = (int)data[StartNo];
            pacMsg += "测量法标识:";
            if(Measure_Flag == 0)
                pacMsg += "直接法";
            else
                pacMsg += "间接法";

            //显示发送的数据
            DisPacket.NewRecord(
                new PackageRecord(
                    PackageRecord_RSType.rec,
                    pole,
                    "导线弧垂数据报",
                    pacMsg));
        }
    }
}
