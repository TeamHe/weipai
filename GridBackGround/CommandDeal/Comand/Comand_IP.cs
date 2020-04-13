using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.CommandDeal
{
    public class Comand_IP
    {
        private static  string  CMD_ID;
        /// <summary>
        /// 上次操作IP地址
        /// </summary>
        public static   System.Net.IPAddress IP_Address { get; set; }
        public static   UInt16  Port { get; set; }
        private static  int     PacLength = 1 + 1 + 4 + 2;//配置类型、标识位、上位机IP地址、上位机端口号

        #region 公共函数
        /// <summary>
        /// 查询设备上位机IP地址
        /// </summary>
        /// <param name="cmd_ID">设备ID号</param>
        public static void Query(string cmd_ID)
        {
            Con(cmd_ID, 0x00, null, 0, 0);
        }

        /// <summary>
        /// 设置设备上位机IP地址信息
        /// </summary>
        /// <param name="cmd_ID">设备ID</param>
        /// <param name="ip">设定的IP端口号</param>
        /// <param name="port">端口号</param>
        /// <param name="request_Flag">设置类型</param>
        public static void Set(string cmd_ID, System.Net.IPAddress ip, int port, int request_Flag)
        {
            Con(cmd_ID, 0x01, ip, port, request_Flag);
        }  
        
        /// <summary>
        /// IP配置响应
        /// </summary>
        /// <param name="cmd_ID">设备ID </param>
        /// <param name="frame_No"></param>
        /// <param name="data">数据</param>
        public static void Response(Termination.IPowerPole pole,
            byte frame_No,
            byte[] data)
        {    
            string pacMsg = "";
            if(data.Length != PacLength+1)
                return;
            if (data[0] == 0x00)
                pacMsg += "查询";
            else
                pacMsg += "设置";

            if (data[1] == 0xff)
                pacMsg += "成功。";
            else
                pacMsg += "失败。";
         
            byte[] ip = new byte[4];
            Buffer.BlockCopy(data, 0x3, ip, 0, 4);
            IP_Address = new System.Net.IPAddress(ip);
            pacMsg += "IP地址：" + IP_Address.ToString();
            Port = BitConverter.ToUInt16(data, 0x07);
            pacMsg +=  " 端口号：" + Port.ToString();
            //显示数据响应解析结果
            PacketAnaLysis.DisPacket.NewRecord(
                new PacketAnaLysis.DataInfo(
                    PacketAnaLysis.DataRecSendState.send,
                    pole,
                    "上位机信息",
                    pacMsg)); ;
        }

        public static void Ayanlise(string cmd_id, byte[] data)
        {
            if (data.Length < 8)
                return;
            string pacMsg = "WEB操作：";
            if (data[0] == 0x00)
                pacMsg += "查询 ";
            else
                pacMsg += "设置 ";
            int request_Flag = data[1];
            if ((request_Flag & 0x01) == 1)
            {
                byte[] ip = new byte[4];
                Buffer.BlockCopy(data, 2, ip, 0, 4);
                IP_Address = new System.Net.IPAddress(ip);
                pacMsg += "IP地址：" + IP_Address.ToString();
            }
            if ((request_Flag & 0x02) == 0x02)
            {
                Port = BitConverter.ToUInt16(data, 0x06);
                pacMsg += " 端口号：" + Port.ToString();
            }

            //显示发送的数据
            PacketAnaLysis.DisPacket.NewRecord(
                new PacketAnaLysis.DataInfo(
                    PacketAnaLysis.DataRecSendState.rec,
                    Termination.PowerPoleManage.Find(cmd_id),
                    "上位机信息",
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
        private static void Con(string cmd_ID, byte ConMode, System.Net.IPAddress ip, int port, int request_Flag)
        {
            string pacMsg = "";
            CMD_ID = cmd_ID;
            if (ConMode == 0x01)                    //配置时，保存配置信息
            {
                IP_Address = ip;
                Port = (UInt16)port;
            }
            #region 报文数据生成
            byte[] data = new byte[PacLength];

            data[0] = ConMode;                      //参数配置类型标识
            if(ConMode == 0)
                pacMsg = "查询";
            if (ConMode == 1)
            {
                data[1] = (byte)request_Flag;
                if ((request_Flag & 0x01) == 0x01)
                {
                    if (ip != null)
                        Buffer.BlockCopy(ip.GetAddressBytes(), 0, data, 2, 4);  //IP
                    pacMsg += "设定,上位机IP:" + ip.ToString() +" ";
                }
                if ((request_Flag & 0x02) == 0x02)
                {
                    data[6] = (byte)(port & 0xFF);                        //端口号
                    data[7] = (byte)(port >> 8 & 0xFF);
                    pacMsg += "端口号:" + port.ToString();

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
                        "上位机信息",
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
                PacketAnaLysis.PacketType_Control.HostComputer,
                FrameNO.GetFrameNO(),
                data);
            return Packet;
        }
        #endregion
    }
}
