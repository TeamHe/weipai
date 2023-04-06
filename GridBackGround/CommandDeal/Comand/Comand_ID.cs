using ResModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.CommandDeal
{
    public class Comand_ID
    {
        private static string CMD_ID;
        public static string Component_ID { get; set; }
        public static string NEW_CMD_ID { get; set; }
        public static string Original_ID { get; set; }
        private static int PacLength = 1 + 1 + 17 + 17+17;//配置类型、标识位、被测设备新ID，原始ID，装置新ID
        private static int RecLength = 1 + 1 + 1 + 17 + 17;//配置类型、发送状态、标识位、被测设备ID，原始ID
        #region 公共函数
        /// <summary>
        /// 状态监测装置 ID 查询
        /// </summary>
        /// <param name="cmd_ID">设备ID号</param>
        public static void Query(string cmd_ID)
        {
            Con(cmd_ID, 0x00, 0x00,"","","");
        }
        public static void Ayanlise(string cmd_id, byte[] data)
        {
            if (data.Length < 2 + 17 * 3)
                return;
            string pacMsg = "WEB操作：";
            if (data[0] == 0x00)
                pacMsg += "查询 ";
            else
                pacMsg += "设置 ";
            int request_Flag = data[1];
            if ((request_Flag & 0x02) == 0x02)
            {
                var component_ID = Encoding.Default.GetString(data, 2, 17);
                pacMsg += "被测设备ID:" + component_ID + " ";
            }
            var orig_id = Encoding.Default.GetString(data, 2 + 17, 17);
            pacMsg += "原始ID：" + orig_id + " ";
            if ((request_Flag & 0x01) == 1)
            {
                var new_cmd_id = Encoding.Default.GetString(data, 2 + 17 * 2, 17);
                pacMsg += "装置ID:" + new_cmd_id + " ";
            }

            //显示发送的数据
            PacketAnaLysis.DisPacket.NewRecord(
                new PacketAnaLysis.DataInfo(
                    PacketAnaLysis.DataRecSendState.rec,
                    Termination.PowerPoleManage.Find(cmd_id),
                    "状态监测装置ID",
                    pacMsg));
        }

        public static void Set(string cmd_ID, int request_Flag, string component_ID,string original_ID, string new_CMD_ID)
        {
            Con(cmd_ID, 0x01, request_Flag,component_ID, new_CMD_ID,original_ID);
        }

        /// <summary>
        /// IP配置响应
        /// </summary>
        /// <param name="cmd_ID">设备ID </param>
        /// <param name="frame_No"></param>
        /// <param name="data">数据</param>
        public static void Response(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string pacMsg = "";
            if (data.Length != RecLength)
                return;
            if (data[0] == 0x00)
                pacMsg += "查询";
            else
                pacMsg += "设置";

            if (data[1] == 0xff)
                pacMsg += "成功，";
            else
                pacMsg += "失败,";
            Component_ID = Encoding.Default.GetString(data, 3, 17);
            pacMsg += "被测设备ID:" + Component_ID +" ";
            Original_ID = Encoding.Default.GetString(data, 3 + 17, 17);
            pacMsg += "原始ID：" +  Original_ID +" ";
            PacketAnaLysis.DisPacket.NewRecord(
                new PacketAnaLysis.DataInfo(
                    PacketAnaLysis.DataRecSendState.rec,
                    pole,
                    "状态监测装置ID",
                    pacMsg)); 
        }
        #endregion


        #region 私有函数
        /// <summary>
        /// 终端上位机IP配置
        /// </summary>
        /// <param name="cmd_ID">设备ID</param>
        /// <param name="ConMode">配置类型——查询，配置</param>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="request_Flag">设置标识</param>
        private static void Con(string cmd_ID, byte ConMode, int request_Flag,string component_ID, string new_CMD_ID,string original_ID)
        {
            string pacMsg = "";
            CMD_ID = cmd_ID;

            #region 报文数据生成
            byte[] data = new byte[PacLength];

            data[0] = ConMode;                      //参数配置类型标识
            if (ConMode == 0)
                pacMsg = "查询";
            if (ConMode == 1)       //设置
            { 
                data[1] = (byte)(request_Flag&0xff);
                pacMsg = "设定";
                if ((request_Flag & 0x02) == 0x02)
                {
                     if (component_ID.Length == 17)
                        Component_ID = component_ID;
                    else
                        return;
                    Buffer.BlockCopy(Encoding.Default.GetBytes(Component_ID), 0, data, 2, 17);
                    pacMsg += "被测设备ID:" + Component_ID + " ";
                }
                //原始ID
                if (original_ID.Length == 17)
                    Original_ID = original_ID;
                if(Original_ID.Length ==17)
                    Buffer.BlockCopy(Encoding.Default.GetBytes(Original_ID), 0, data, 2 + 17, 17);

                if ((request_Flag &0x01) == 1)
                {
                    if (new_CMD_ID.Length == 17)
                        NEW_CMD_ID = new_CMD_ID;
                    else
                    return;
                    Buffer.BlockCopy(Encoding.Default.GetBytes(NEW_CMD_ID), 0, data, 2+17*2, 17);
                    pacMsg += "装置ID:" + NEW_CMD_ID + " ";
                }
                
            }
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
                        "状态监测装置ID",
                        pacMsg));
            }
        }

        /// <summary>
        /// 创建报文
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] BuildPacket(byte[] data)
        {

            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                PacLength,
                PacketAnaLysis.TypeFrame.Control,
                PacketAnaLysis.PacketType_Control.ID,
                FrameNO.GetFrameNO(),
                data);
            return Packet;
        }
        #endregion
    }
}
