using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.CommandDeal
{
    class Comand_Timing
    {
        private static string CMD_ID;
        public static void Timing(string cmd_ID, byte ResetMode)
        {
            CMD_ID = cmd_ID;
            byte[] data = new byte[5];
            data[0] = ResetMode;
            var time = DataTurn.Time.GetBytesTime();
            Buffer.BlockCopy(time, 0, data, 1, 4);
            var packet = BuildPacket(data);
            string errorMsg;
            string pacMsg = "";
            switch (ResetMode)
            {
                case 0x00:
                    pacMsg = "查询时间" ;//+ DateTime.Now.ToString();
                    break;
                case 0x01:
                    pacMsg = "设置时间" + DateTime.Now.ToString();
                    break;
            }
            if (PackeDeal.SendData(CMD_ID, packet, out errorMsg))
            {
                //显示发送的数据
                PacketAnaLysis.DisPacket.DisNewPacket(
                    new PacketAnaLysis.DataInfo(
                        PacketAnaLysis.DataRecSendState.send,
                        CMD_ID,
                        "时间设定",
                        pacMsg));
            }
        }
        private static byte[] BuildPacket(byte[] data)
        {
            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                5,
                PacketAnaLysis.TypeFrame.Control,
                PacketAnaLysis.PacketType_Control.Timing,
                data);
            return Packet;
        }
        public static void TimingResponse(Sodao.FastSocket.Server.Command.CommandInfo cmdInfo)
        {
            if (cmdInfo.Data[0x17] == 0xff)
            {
                DateTime time = DataTurn.Time.BytesToDate(cmdInfo.Data,0x18);
                //显示发送的数据
                PacketAnaLysis.DisPacket.DisNewPacket(
                    new PacketAnaLysis.DataInfo(
                        PacketAnaLysis.DataRecSendState.rec,
                        CMD_ID,
                        "时间设定",
                        "装置时间" + time.ToString())); ;
            }
        }
    }
}
