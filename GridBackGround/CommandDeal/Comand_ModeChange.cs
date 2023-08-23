using System;
using ResModel.PowerPole;
using cma.service.PowerPole;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 修改工作模式
    /// </summary>
    public class Comand_ModeChange
    {
        private static string CMD_ID;
        /// <summary>
        /// 装置复位控制
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="Mode"></param>
        public static void Set(string cmd_ID,byte Mode)
        {
            CMD_ID = cmd_ID;
            byte[] data = new byte[0x1c];
            for (int i = 20; i < 24; i++)
            {
                data[i] =(byte)( Mode+0xf0);
            }
            var time = Tools.TimeUtil.GetBytesTime();
            Buffer.BlockCopy(time, 0, data, 24, 4);
            var packet = BuildPacket(data,FrameNO.GetFrameNO());
            string errorMsg;
            string pacMsg = "";
            switch (Mode)
            { 
                case 0x01:
                    pacMsg = "安全初始化模式";
                    break;
                case 0x02:
                    pacMsg = "密文通讯模式";
                    break;
                case 0x03:
                    pacMsg = "明文通讯模式";
                    break;
                case 0x04:
                    pacMsg = "工厂调测模式";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdInfo"></param>
        public static void ResetResponse(string cmd_ID,
            byte frame_No,
            byte[] data)
        {
            if (data[0] == 0xff)
            {
                //显示发送的数据
                DisPacket.NewRecord(
                    new DataInfo(
                        DataInfoState.rec,
                         Termination.PowerPoleManage.Find(CMD_ID),
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
                data.Length,
                PacketAnaLysis.TypeFrame.Control,
                0xaa,
                frame_No,
                data);
            return Packet;
        }
    }
}
