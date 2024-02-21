using ResModel;

namespace GridBackGround.PacketAnaLysis
{
    public class PackDeal_Control
    {
        /// <summary>
        /// 工作状态报分发
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="packet_Type"></param>
        /// <param name="frame_No"></param>
        /// <param name="data"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static bool PacketDeivid(IPowerPole pole,
            int packet_Type,
            byte frame_No,
            byte[] data,
            ref int errorCode)
        {
            bool dealState = false;
            switch (packet_Type)
            {
                case PacketType_Control.Model://模型参数0xa7
                    CommandDeal.Comand_Model.Response(pole, frame_No, data);
                    break;
                case PacketType_Control.SoundLightAlarm:
                    CommandDeal.Command_sound_light_alarm.Response(pole, frame_No, data);
                    break;
                //case PacketType_Control.Start_Update:             //补包
                //    CommandDeal.Comand_RemotedUpDate.RemoteUpdateStartResponse(pole, frame_No, data);
                //    break;
                //case PacketType_Control.UpdateBuBao:
                //    CommandDeal.Comand_RemotedUpDate.RemoteUpdateBuBao(pole, frame_No, data);
                //    break;
              
              
                default:
                    errorCode = 0x05;
                    break;

            }
            //if (errorCode == 0x05)
            //    DisPacket.DisComData(DataTurn.StringTurn.ByteToHexString(cmdInfo.Data));
            return dealState;
        }
        /// <summary>
        /// 心跳包
        /// </summary>
        /// <param name="cmdInfo"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static bool WorkStatePacketDeal(Sodao.FastSocket.Server.Command.CommandInfo cmdInfo, out int errorCode)
        {
            errorCode = cmdInfo.ErrorCode;

            //byte[] time = new byte[4];

            //Buffer.BlockCopy(cmdInfo.Data, 23, time, 0, 4);
            //DateTime dt = DataTurn.Time.BytesToDate(time);
            //string str = "设备时间：" + dt.ToString();
            //DataInfo di = new DataInfo(
            //                    DataRecSendState.rec,
            //                    cmdInfo.CMD_ID,
            //                    "心跳",
            //                    str);
            //DisPacket.NewRecord(di);
            return true;
        }
    }
}
