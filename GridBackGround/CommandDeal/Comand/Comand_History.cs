using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResModel;
using ResModel.EQU;
using Tools;

namespace GridBackGround.CommandDeal
{
    class Comand_History
    {
        private static string CMD_ID;
        private static byte Data_Type;
        private static DateTime Start_Time { get; set; }
        private static DateTime End_Time { get; set; }
        //包长：数据类型，1
        //      开始时间，4
        //      结束时间, 4
        private static int PacLength = 1 + 4 + 4;
        //private static int RecLength = 1 + 2;

        #region 公共函数
        /// <summary>
        /// 查询设备采样参数
        /// </summary>
        /// <param name="cmd_ID">设备ID号</param>
        public static void Current(string cmd_ID,byte dataType)
        {
            Con(cmd_ID, dataType, DateTime.Now, DateTime.Now, true);
        }
        /// <summary>
        /// 申请历史数据
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="data_Type"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void History(string cmd_ID, byte data_Type, DateTime start, DateTime end)
        {
            Con(cmd_ID, data_Type, start,end,false);
        }
        /// <summary>
        /// 响应数据解析
        /// </summary>
        /// <param name="cmd_ID">设备ID</param>
        /// <param name="frame_No"></param>
        /// <param name="data"></param>
        public static void Response(IPowerPole pole, byte frame_No, byte[] data)
        {
            string pacMsg = "";
            if (data[0] == 0xff)
                pacMsg += "申请历史数据成功";
            else
                pacMsg += "申请历史数据失败";
            //显示发送的数据
            PacketAnaLysis.DisPacket.NewRecord(
                new PacketAnaLysis.DataInfo(
                    PacketAnaLysis.DataRecSendState.rec,
                     Termination.PowerPoleManage.Find(CMD_ID),
                    "请求历史数据",
                    pacMsg));

        }

        public static void Ayanlise(string cmd_id, byte[] data)
        {
            string pacMsg = "WEB操作：";

            pacMsg += "数据类型：" + ((ICMP)data[0]).GetDescription() + " ";
            if (data[1] == 0 && data[2] == 0 && data[3] == 0 && data[4] == 0)
                pacMsg += "申请当前数据";
            Start_Time = TimeUtil.BytesToDate(data, 1);
            End_Time = TimeUtil.BytesToDate(data, 5);
            pacMsg += "申请历史数据:";
            pacMsg += "起始时间" + Start_Time.ToString();
            pacMsg += "结束时间" + End_Time.ToString();


            //显示发送的数据
            PacketAnaLysis.DisPacket.NewRecord(
                new PacketAnaLysis.DataInfo(
                    PacketAnaLysis.DataRecSendState.rec,
                    Termination.PowerPoleManage.Find(cmd_id),
                    "请求装置历史数据",
                    pacMsg));
        }
        #endregion

        #region 私有函数
         private static void Con(string cmd_ID, byte data_Type,DateTime start,DateTime end,bool Current)
        {
            string pacMsg = "";
            CMD_ID = cmd_ID;
            if (!Current)                    //配置时，保存配置信息
            {
                Data_Type = data_Type;
                Start_Time = start;
                End_Time = end;
            }
            #region 报文数据生成
            byte[] data = new byte[PacLength];

            data[0] = data_Type;                      //参数配置类型标识
            if (!Current)
            {
                pacMsg += "申请历史数据，";
                byte[] tempData;
                pacMsg += "起始时间" + Start_Time.ToString();
                tempData = Tools.TimeUtil.GetBytesTime(Start_Time);
                pacMsg += "结束时间" + End_Time.ToString();
                Buffer.BlockCopy(tempData, 0, data, 1, 4);
                tempData = Tools.TimeUtil.GetBytesTime(End_Time);
                Buffer.BlockCopy(tempData, 0, data, 5, 4);
            }
            else
                pacMsg += "申请当前数据";
            pacMsg += " 数据类型： " + data[0].ToString("X2")+ " ";;
             //switch (data_Type)
             //{
             //    case PacketAnaLysis.PacketType_Monitoring.Vibration_Character:
             //    pacMsg += "微风振动特征量数据";
             //    break;
             //    case PacketAnaLysis.PacketType_Monitoring.Vibration_Form:
             //    pacMsg += "微风振动波形信号数据";
             //    break;
             //    case PacketAnaLysis.PacketType_Monitoring.Wave_Character:
             //    pacMsg += "舞动特征量数据";
             //    break;
             //    case PacketAnaLysis.PacketType_Monitoring.Wave_Trajectory:
             //    pacMsg += "舞动轨迹数据";
             //    break;
             //    default:
             //    pacMsg += "基本数据类型";
             //    break;
             //}
            var packet = BuildPacket(data);     //生成报文                                                            
            #endregion

            string errorMsg;

            if (PackeDeal.SendData(CMD_ID, packet, out errorMsg))
            {
                //显示发送的数据
                PacketAnaLysis.DisPacket.NewRecord(
                    new PacketAnaLysis.DataInfo(
                        PacketAnaLysis.DataRecSendState.send,
                         Termination.PowerPoleManage.Find(CMD_ID),
                        "请求装置历史数据",
                        pacMsg));
            }
        }

         private static byte[] BuildPacket(byte[] data)
         {

             var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                 CMD_ID,
                 PacLength,
                 PacketAnaLysis.TypeFrame.Control,
                 PacketAnaLysis.PacketType_Control.HisData,
                 FrameNO.GetFrameNO(),
                 data);
             return Packet;
         }
        #endregion

    }
}
