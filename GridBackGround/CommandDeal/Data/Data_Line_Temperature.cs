using ResModel;
using System;
using System.Text;
using ResModel.PowerPole;
using cma.service;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 线温数据报
    /// </summary>
    public class Data_Line_Temperature
    {
        private static string CMD_ID;
        private static int PacLength = 17 //被监测设备 ID
           + 1 //采集单元总数
           + 1 //采集单元序号
           + 4 //采集时间
           + 4; //线温 

        /// <summary>
        /// 线温数据解析
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_NO"></param>
        /// <param name="data"></param>
        public static void Deal(IPowerPole pole, byte frame_NO, byte[] data)
        {
            CMD_ID = pole.CMD_ID;
            string Component_ID;       //被测设备ID
            uint Unit_Sum;              //采集单元总数
            uint Unit_No;               //采集单元序号
            DateTime Time_Stamp;        //采集时间
            float Line_Temperature;     //线温

            if (data.Length != PacLength)
                return;
            int StartNo = 0;
            string pacMsg = "";
            //被测设备ID
            Component_ID = Encoding.Default.GetString(data, StartNo, 17);
            pacMsg += "被测设备ID:" + Component_ID + " ";
            StartNo += 17;
            //采集单元总数
            Unit_Sum = (uint)data[StartNo];
            pacMsg += "采集单元总数:" + Unit_Sum.ToString() + " ";
            StartNo += 1;
            //采集单元序号
            Unit_No = (uint)data[StartNo];
            pacMsg += "采集单元序号:" + Unit_No.ToString() + " ";
            StartNo += 1;
            //采集时间
            Time_Stamp = Tools.TimeUtil.BytesToDate(data, StartNo);
            pacMsg += "采集时间:" + Time_Stamp.ToString() + " ";
            StartNo += 4;
            //线温
            Line_Temperature = BitConverter.ToSingle(data, StartNo);
            pacMsg += "线温:" + Line_Temperature.ToString("f1") + "℃ ";
            StartNo += 4;
            
            //显示发送的数据
            DisPacket.NewRecord(
                new PackageRecord(
                    PackageRecord_RSType.rec,
                    pole,
                    "导线温度数据报",
                    pacMsg));
        }
    }
}
