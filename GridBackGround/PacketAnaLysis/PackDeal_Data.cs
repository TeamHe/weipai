using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResModel.EQU;


namespace GridBackGround.PacketAnaLysis
{
    public class PackDeal_Data
    {
        /// <summary>
        /// 工作状态报解析
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_Type"></param>
        /// <param name="packet_Type"></param>
        /// <param name="frame_No"></param>
        /// <param name="data"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static bool PacketDeivid(Termination.IPowerPole pole,
            int packet_Type,
            byte frame_No,
            byte[] data,
            ref int errorCode)
        {
            bool dealState = false;
            if (packet_Type == (int)ICMP.Weather ||
                packet_Type == (int)ICMP.Inclination ||
                packet_Type == (int)ICMP.Ice)
            {
                errorCode = 0;
                try
                {
                    var ayan = new CommandDeal.Data.RealDataAyan(pole, packet_Type, data);
                }
                catch{}
            }
            else
                switch (packet_Type)
                {
                    //case PacketType_Monitoring.Weather:
                    //    CommandDeal.Data_Weather.Deal(pole, frame_No, data);
                    //    break;
                    case PacketType_Monitoring.Gradient_Tower:
                        CommandDeal.Data_GTQX.Deal(pole, frame_No, data);
                        break;
                    case PacketType_Monitoring.Vibration_Character:
                        CommandDeal.Data_ZD_Feature.Deal(pole, frame_No, data);
                        break;
                    case PacketType_Monitoring.Vibration_Form:
                        CommandDeal.Data_ZD_Form.Deal(pole, frame_No, data);
                        break;
                    case PacketType_Monitoring.Conductor_Sag:
                        CommandDeal.Data_Sag.Deal(pole, frame_No, data);
                        break;
                    case PacketType_Monitoring.Conductor_Temperature:
                        CommandDeal.Data_Line_Temperature.Deal(pole, frame_No, data);
                        break;
                    case PacketType_Monitoring.Glaciation:
                        CommandDeal.Data_Ice.Deal(pole, frame_No, data);
                        break;
                    case PacketType_Monitoring.Conductor_Monsoon:
                        CommandDeal.Data_FP.Deal(pole, frame_No, data);
                        break;
                    case PacketType_Monitoring.Wave_Character:
                        CommandDeal.Data_WD.Deal(pole, frame_No, data);
                        break;
                    case PacketType_Monitoring.Wave_Trajectory:
                        CommandDeal.Data_WD_Form.Deal(pole, frame_No, data);
                        break;

                    default:
                        errorCode = 0x05;
                        break;

                }
            //显示报文
            dealState = OnDataPacketResPonse(pole.CMD_ID, packet_Type, frame_No);
            return dealState;
        }

        /// <summary>
        /// 数据报响应
        /// </summary>
        /// <param name="cmdInfo"></param>
        /// <returns></returns>
        private static bool OnDataPacketResPonse(string cmd_ID,
            int packet_Type,
            byte frame_No
            )
        {
            byte[] data = new byte[1];
            data[0] = 0xff;
            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
               cmd_ID,
               1,
               PacketAnaLysis.TypeFrame.ResMonitoring,
               packet_Type,
               frame_No,
               data);
            string errorMsg;
            if (PackeDeal.SendData(cmd_ID, Packet, out errorMsg))
            {
                ////显示发送的数据
                //PacketAnaLysis.DisPacket.DisNewPacket(
                //    new PacketAnaLysis.DataInfo(
                //        PacketAnaLysis.DataRecSendState.send,
                //        cmd_ID,
                //        "数据报响应",
                //        ""));
            }
            return false;
        }
        //新数据包显示
        private static bool OnRecNewDataPacket(string cmd_ID, string DataType)
        {
            //PacketAnaLysis.DisPacket.NewRecord(
            //      new PacketAnaLysis.DataInfo(
            //          PacketAnaLysis.DataRecSendState.send,
            //          pole,
            //          "数据报",
            //          DataType));       //数据类型
            return false;
        }
    }
}
