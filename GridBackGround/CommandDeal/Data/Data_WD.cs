using ResModel;
using System;
using System.Text;
using ResModel.PowerPole;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 导线舞动特征数据报
    /// </summary>
    public class Data_WD
    {
        private static string CMD_ID;
        private static int PacLength = 17  //被测设备ID
            + 1     //采集单元总数
            + 1     //采集单元序列号
            + 4     //采集时间
            + 4     //舞动幅值
            + 4     //垂直舞动幅值
            + 4     //水平舞动幅值
            + 4     //舞动椭圆倾斜角
            + 4;    //舞动频率
        /// <summary>
        /// 微风振动数据解析
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_NO"></param>
        /// <param name="data"></param>
        public static void Deal(IPowerPole pole, byte frame_NO, byte[] data)
        {
            CMD_ID = pole.CMD_ID;
            string Component_ID;            //被测设备ID
            uint Unit_Sum;                  //采集单元总数
            uint Unit_No;                   //采集单元序号
            DateTime Time_Stamp;            //采集时间
            float U_Gallop_Amplitude;       //舞动幅值
            float U_Vertical_Amplitude;     //垂直舞动幅值
            float U_Horizontal_Amplitude;   //水平舞动幅值
            float U_AngleToVertical;        //舞动椭圆倾斜角
            float U_Gallop_Frequency;       //舞动频率
            if (data.Length != PacLength)
                return;
            string pacMsg = "";
            int StartNo = 0;
            //被测设备ID
            Component_ID = Encoding.Default.GetString(data, StartNo, StartNo);
            pacMsg += "被测设备ID:" + Component_ID + " ";
            StartNo += 17;

            //采集单元总数
            Unit_Sum = (uint)data[ StartNo];
            pacMsg += "采集单元总数:" + Unit_Sum.ToString() + " ";
            StartNo += 1;
            //采集单元序号
            Unit_No = (uint)data[ StartNo];
            pacMsg += "采集单元序号:" + Unit_No.ToString() + " ";
            StartNo += 1;

            //采集时间
            Time_Stamp = Tools.TimeUtil.BytesToDate(data,  StartNo);
            pacMsg += "采集时间:" + Time_Stamp.ToString() + " ";
            StartNo += 4;

            //舞动幅值
            U_Gallop_Amplitude = BitConverter.ToSingle(data, StartNo);
            pacMsg += "舞动幅值:" + U_Gallop_Amplitude.ToString("f3") + "m ";
            StartNo += 4;
            //垂直舞动幅值
            U_Vertical_Amplitude = BitConverter.ToSingle(data, StartNo);
            pacMsg += "垂直舞动幅值:" + U_Vertical_Amplitude.ToString("f3") + "m ";
            StartNo += 4;
            //水平舞动幅值
            U_Horizontal_Amplitude = BitConverter.ToSingle(data, StartNo);
            pacMsg += "水平舞动幅值:" + U_Horizontal_Amplitude.ToString("f3") + "m ";
            StartNo += 4;

            //舞动椭圆倾斜角
            U_AngleToVertical = BitConverter.ToSingle(data, StartNo);
            pacMsg += "舞动椭圆倾斜角:" + U_AngleToVertical.ToString("f2") + "° ";
            StartNo += 4;


            //舞动频率
            U_Gallop_Frequency = BitConverter.ToSingle(data, StartNo);
            pacMsg += "舞动频率:" + U_Gallop_Frequency.ToString("f2") + "Hz ";
            StartNo += 4;

            //显示发送的数据
            PacketAnaLysis.DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.rec,
                    pole,
                    "舞动特征数据报",
                    pacMsg));
        }
    }
}
