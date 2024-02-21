using ResModel;
using System;
using System.Text;
using ResModel.PowerPole;
using cma.service;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 覆冰数据报
    /// </summary>
    public class Data_Ice
    {
        private static string CMD_ID;
        /// <summary>
        /// 覆冰数据解析
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_NO"></param>
        /// <param name="data"></param>
        public static void Deal(IPowerPole pole, byte frame_NO, byte[] data)
        {
            CMD_ID = pole.CMD_ID;
            string Component_ID;        //被监测设备 ID
            DateTime Time_Stamp;        //采集时间
            float Equal_IceThickness;   //等值覆冰厚度
            float Tension;              //综合悬挂载荷
            float Tension_Difference;   //不均衡张力差
           
            
            float[] value = new float[12];
            string[] id = new string[2];
            id[0] = pole.CMD_ID; ;
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
            //等值覆冰厚度
            Equal_IceThickness = BitConverter.ToSingle(data, StartNo);
            value[0] = Equal_IceThickness;
            pacMsg += "等值覆冰厚度:" + Equal_IceThickness.ToString("f1") + "mm  ";
            StartNo += 4;

            //综合悬挂载荷
            Tension = BitConverter.ToSingle(data, StartNo);
            value[1] = Tension;
            pacMsg += "综合悬挂载荷:" + Tension.ToString("f1") + "N  ";
            StartNo += 4;

            //不均衡张力差
            Tension_Difference = BitConverter.ToSingle(data, StartNo);
            value[2] = Tension_Difference;
            pacMsg += "不均衡张力差:" + Tension_Difference.ToString("f1") + "N  ";
            StartNo += 4;
            int ValueCounter = 3;
            int m =(int)data[StartNo];
            StartNo++;
            for(int i= 0; i < m; i++)
            {
                 //原始拉力
                value[ValueCounter] = BitConverter.ToSingle(data, StartNo);
                value[ValueCounter] = (float)((int)(value[ValueCounter] * 100) / 100.0);
                pacMsg += string.Format("原始拉力{0}: {1} N  ",i, value[ValueCounter++].ToString("f2"));
                StartNo += 4;
                //ValueCounter++;
                
                 //风偏角
                value[ValueCounter] = BitConverter.ToSingle(data, StartNo);
                value[ValueCounter] = (float)((int)(value[ValueCounter] * 100) / 100.0);
                pacMsg += string.Format("风偏角{0}: {1} °  ",i, value[ValueCounter++].ToString("f2"));
                StartNo += 4;
                //ValueCounter++;
                //偏斜角
                value[ValueCounter] = BitConverter.ToSingle(data, StartNo);
                value[ValueCounter] = (float)((int)(value[ValueCounter] * 100) / 100.0);
                pacMsg += string.Format("偏斜角{0}: {1} °  ",i, value[ValueCounter++].ToString("f2"));
                //ValueCounter++;
                StartNo += 4;
            }
            int temp = DB_Operation.DB.DB_In_Ice(id, Time_Stamp, value);
            switch (temp)
            { 
                case 0:
                    pacMsg += "数据已存在";
                    break;
                case 1:
                    pacMsg += "数据保存成功";
                    break;
                default:
                    pacMsg += "数据保存失败";
                    break;
            }
            //显示发送的数据
            DisPacket.NewRecord(
                new PackageRecord(
                    PackageRecord_RSType.rec,
                    pole,
                    "覆冰数据报",
                    pacMsg));
        }
    }
}
