using ResModel;

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
        public static bool PacketDeivid(IPowerPole pole,
            int packet_Type,
            byte frame_No,
            byte[] data,
            ref int errorCode)
        {
            switch (packet_Type)
            {
                default:
                    errorCode = 0x05;
                    break;

            }
            //显示报文
            return OnDataPacketResPonse(pole.CMD_ID, packet_Type, frame_No);
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
