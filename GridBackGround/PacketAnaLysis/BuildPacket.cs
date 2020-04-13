using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sodao.FastSocket.Server.Command;

namespace GridBackGround.PacketAnaLysis
{
    class BuildPacket
    {
        public static byte[] PackBuild(string CMD_ID,
                                        int Packet_Length,
                                        int Frame_Type,
                                        int Packet_Type,
                                        byte[] data)
        {
            CommandInfo bci = new CommandInfo(CMD_ID, Packet_Length, Frame_Type, Packet_Type, data, null, 0);
            return Sodao.FastSocket.Server.PacketBuilder.MakePacket(bci);

        }

        public static byte[] PackBuild(string CMD_ID,
                                        int Packet_Length,
                                        int Frame_Type,
                                        int Packet_Type,
                                        byte frame_No,
                                        byte[] data)
        {
            return Sodao.FastSocket.Server.PacketBuilder.MakePacket(
                CMD_ID,
                Packet_Length,
                Frame_Type,
                Packet_Type,
                frame_No,
                data);
        }
        public static byte[] PackBuild(string CMD_ID,
                                       int Frame_Type,
                                       int Packet_Type,
                                       byte frame_No,
                                       byte[] data)
        {
            return Sodao.FastSocket.Server.PacketBuilder.MakePacket(
                CMD_ID,
                data.Length,
                Frame_Type,
                Packet_Type,
                frame_No,
                data);
        }
    }
}
