using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.CommandDeal
{
    public class Image_TimeTable
    {
        private static string CMD_ID;
        private static int PacLength = 1 + 1;
        public static List<CommandDeal.IPhoto_Time> TimeTable { get; set; }
        #region 公共函数
        public static void Query(string cmd_ID, int Channel_No)
        {
            Con(cmd_ID, false,Channel_No,null);
        }

        public static bool Set(string cmd_ID, int Channel_No, List<IPhoto_Time> model)
        {
            return Con(cmd_ID, true, Channel_No, model);
        }

        public static void Response(Termination.IPowerPole pole, byte frame_No, byte[] data)
        {
            if (data.Length < 3) return;
            string pacMsg = "";
            if (data[0] == 0x00)
                pacMsg += "查询";
            else
                pacMsg += "设置";
          
            pacMsg += "通道" + ((int)data[1]).ToString();
            Error_Code code = Error_Code.Success;
            if (data[2] == 0xff)
                pacMsg += "成功。";
            else
            {
                pacMsg += "失败。";
                code = Error_Code.DeviceError;
            }
            if(data.Length > 3)
            {
                pacMsg += "一共：" + ((int)data[3]).ToString() + "组，为：（时，分，预置位号）";
                for (int i = 0; i < data[3]; i++)
                {
                    pacMsg += "(" + data[i * 3 + 4].ToString() + ",";
                    pacMsg += data[i * 3 + 4 + 1].ToString() + "，";
                    pacMsg += data[i * 3 + 4 + 2].ToString() + "），";
                }
            }
           
            try
            {
                Termination.PowerPole powerPole = pole as Termination.PowerPole;
                powerPole.OnTimeTableFinish(code);
            }
            catch
            {

            }
            //显示发送的数据
            PacketAnaLysis.DisPacket.NewRecord(
                new PacketAnaLysis.DataInfo(
                    PacketAnaLysis.DataRecSendState.rec,
                    pole,
                    "拍照时间表",
                    pacMsg));

        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 配置生成报文
        /// </summary>
        /// <param name="cmd_ID">设备ID</param>
        /// <param name="conMode">操作类型——查询、配置</param>
        /// <param name="request_Flag">标志位</param>
        /// <param name="Data_Type">数据类型</param>
        /// <param name="sample_Time">采样周期</param>
        /// <param name="heart_Time">心跳周期</param>
        private static bool Con(string cmd_ID, bool conMode,int Channel_No,List<IPhoto_Time> timeTable)
        {
            string pacMsg = "";
            CMD_ID = cmd_ID;
            if(timeTable!= null)
                PacLength = 3 + 3 * timeTable.Count;

            byte[] data = new byte[PacLength];
            if (conMode)
            {
                TimeTable = timeTable;
                byte[] value = new byte[4];
                data[0] = 0x01;
                pacMsg += "配置 通道号：" + Channel_No.ToString();
                //通道号
                data[1] = (byte)(Channel_No & 0xff);
                //组数
                data[2] = (byte)(timeTable.Count & 0xff);
                pacMsg += "共" + timeTable.Count.ToString() + "组,分别为（时，分，预置位号）：";
                for (int i = 0; i < timeTable.Count; i++)
                {
                    pacMsg += "(";
                    data[i * 3 + 3] = (byte)(timeTable[i].Hour);
                    pacMsg += timeTable[i].Hour.ToString() + "，";

                    data[i * 3 + 3 + 1] = (byte)(timeTable[i].Minute);
                    pacMsg += timeTable[i].Minute.ToString() + "，";

                    data[i * 3 + 3 + 2] = (byte)(timeTable[i].Presetting_No);
                    pacMsg += timeTable[i].Presetting_No.ToString() + "），";
                }


            }
            else
            {
                data[0] = 0x00;
                pacMsg += "查询 通道号：" +Channel_No.ToString();
                //通道号
                data[1] = (byte)(Channel_No & 0xff);

            }
            var packet = BuildPacket(data);     //生成报文      
            string errorMsg;
            if (PackeDeal.SendData(CMD_ID, packet, out errorMsg))
            {
                
                //显示发送的数据
                PacketAnaLysis.DisPacket.NewRecord(
                    new PacketAnaLysis.DataInfo(
                        PacketAnaLysis.DataRecSendState.send,
                         Termination.PowerPoleManage.Find(CMD_ID),
                        "拍照时间表",
                        pacMsg));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 报文生成
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] BuildPacket(byte[] data)
        {

            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                PacLength,
                PacketAnaLysis.TypeFrame.ControlImage,
                PacketAnaLysis.PacketType_Image.Photo_TimeTable,
                FrameNO.GetFrameNO(),
                data);
            return Packet;
        }
        #endregion

    }
}
  
        