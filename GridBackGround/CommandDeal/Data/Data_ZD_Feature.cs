using ResModel;
using System;
using System.Text;
using ResModel.PowerPole;
using cma.service.PowerPole;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 微风振动特征数据报
    /// </summary>
    public class Data_ZD_Feature
    {
        private static string CMD_ID;
        //private static string Component_ID;
        private static int PacLength = 17 + 1 + 1 + 4 + 2 + 4 + 4;  //被测设备ID，采集单元总数，采集单元序列号，采集时间，动弯应变幅值，弯曲振幅，微分振动频率
        /// <summary>
        /// 微风振动数据解析
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_NO"></param>
        /// <param name="data"></param>
        public static void Deal(IPowerPole pole,
            byte frame_NO, byte[] data)
        {
            CMD_ID = pole.CMD_ID;
            string Component_ID ;       //被测设备ID
            uint Unit_Sum;              //采集单元总数
            uint Unit_No;               //采集单元序号
            DateTime Time_Stamp;        //采集时间
            uint Strain_Amplitude;      //动弯应变幅值
            float Bending_Amplitude;    //弯曲振幅
            float Vibration_Frequency ; //微风振动频率

            if (data.Length != PacLength)
                return;

            float[] value = new float[11];
            string[] id = new string[2];
            id[0] = pole.CMD_ID;

            string pacMsg = "";
            //int StartNo = 0;
            //被测设备ID
            Component_ID = Encoding.Default.GetString(data, 0, 17);
            id[1] = Component_ID;
            pacMsg += "被测设备ID:" + Component_ID + " ";

            //采集单元总数
            Unit_Sum = (uint)data[17];
            pacMsg += "采集单元总数:" + Unit_Sum.ToString() + " ";

            //采集单元序号
            Unit_No = (uint)data[18];
            value[0] = Unit_No;
            pacMsg += "采集单元序号:" + Unit_No.ToString() + " ";

            //采集时间
            Time_Stamp = Tools.TimeUtil.BytesToDate(data,19);
            pacMsg += "采集时间:" + Time_Stamp.ToString() + " ";

            //动弯应变幅值
            Strain_Amplitude = BitConverter.ToUInt16(data,23);
            value[1] = Strain_Amplitude;
            pacMsg += "动弯应变幅值:" + Strain_Amplitude.ToString() + "µε ";

            //弯曲振幅
            Bending_Amplitude = BitConverter.ToSingle(data,25);
            value[2] = Bending_Amplitude;
            pacMsg += "弯曲振幅:" + Bending_Amplitude.ToString("f2") + "mm ";

            //微风振动频率
            Vibration_Frequency = BitConverter.ToSingle(data, 29);
            value[3] = Vibration_Frequency;
            pacMsg += "微风振动频率:" + Vibration_Frequency.ToString("f2") + "Hz ";

            int temp = DB_Operation.DB.DB_In_ZD_Feature(id, Time_Stamp, value);
            if (temp == 1)
                pacMsg += "数据保存成功";
            else
                if (temp == 0)
                    pacMsg += "数据已存在";
                else
                    pacMsg += "数据保存失败";

            //显示发送的数据
            DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.rec,
                    pole,
                    "微风振动特征量数据报",
                    pacMsg));
        }
    }
}
