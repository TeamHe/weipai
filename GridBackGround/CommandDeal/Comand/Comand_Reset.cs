using ResModel;
using ResModel.PowerPole;
using cma.service.PowerPole;


namespace GridBackGround.CommandDeal
{
    public class Comand_Reset
    {
        private static string CMD_ID;
        /// <summary>
        /// 装置复位控制
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="ResetMode"></param>
        public static void Reset(string cmd_ID,byte ResetMode)
        {
            CMD_ID = cmd_ID;
            byte[] data = new byte[1];
            data[0] = ResetMode;
            var packet = BuildPacket(data,FrameNO.GetFrameNO());
            string errorMsg;
            string pacMsg = "";
            switch (ResetMode)
            { 
                case 0x00:
                    pacMsg = "常规复位";
                    break;
                case 0x01:
                    pacMsg = "复位至调试模式";
                    break;
            }
            
            if (PackeDeal.SendData(CMD_ID, packet, out errorMsg))
            {
                //显示发送的数据
                DisPacket.NewRecord(
                    new DataInfo(
                        DataInfoState.send,
                        Termination.PowerPoleManage.Find(CMD_ID),
                        "装置复位",
                        pacMsg));
            }
        }

        public static void Ayanlise(string cmd_id, byte[] data)
        {
            if (data.Length < 1)
                return;
            string pacMsg = "WEB操作：";
            if (data[0] == 0x00)
                pacMsg += "常规复位";
            else
                pacMsg += "复位至调试模式";
            //显示发送的数据
            DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.rec,
                    Termination.PowerPoleManage.Find(cmd_id),
                    "装置复位",
                    pacMsg));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdInfo"></param>
        public static void ResetResponse(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            if (data[0] == 0xff)
            {
                //显示发送的数据
                DisPacket.NewRecord(
                    new DataInfo(
                        DataInfoState.rec,
                        pole,
                        "装置复位",
                        "装置复位成功")); ;
            }
        }

        private static byte[] BuildPacket(byte[] data)
        {
           
            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                6,
                PacketAnaLysis.TypeFrame.Control,
                PacketAnaLysis.PacketType_Control.Reset,
                data);
            return Packet;
        }

        private static byte[] BuildPacket(byte[] data, byte frame_No)
        {

            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                1,
                PacketAnaLysis.TypeFrame.Control,
                PacketAnaLysis.PacketType_Control.Reset,
                frame_No,
                data);
            return Packet;
        }
    }
}
