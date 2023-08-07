using Sodao.FastSocket.Server.Command;

namespace GridBackGround.PacketAnaLysis
{
    class BuildPacket
    {
        public static byte[] PackBuild(string CMD_ID,
                                        int Packet_Length,
                                        int Frame_Type,
                                        int Packet_Type,
                                        byte frame_No,
                                        byte[] data)
        {
            return new CommandInfo_gw()
            {
                CMD_ID = CMD_ID,
                Packet_Lenth = Packet_Length,
                Frame_Type = Frame_Type,
                Packet_Type = Packet_Type,
                Frame_No = frame_No,
                Data = data
            }.encode();
        }
        public static byte[] PackBuild(string CMD_ID,
                                       int Frame_Type,
                                       int Packet_Type,
                                       byte frame_No,
                                       byte[] data)
        {
            return new CommandInfo_gw()
            {
                CMD_ID = CMD_ID,
                Packet_Lenth = data.Length,
                Frame_Type = Frame_Type,
                Packet_Type = Packet_Type,
                Frame_No = frame_No,
                Data = data
            }.encode();
        }
    }
}
