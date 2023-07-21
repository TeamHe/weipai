using System;
using System.Text;
using System.Net;
using ResModel;
using ResModel.PowerPole;

namespace GridBackGround.CommandDeal
{
    public class Comand_NA
    {
       

        #region 公共变量
        /// <summary>
        /// 装置ID
        /// </summary>
        public static string CMD_ID { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public static IPAddress IP { get; set; }       //IP地址
        /// <summary>
        /// 子网掩码
        /// </summary>
        public static IPAddress Subnet_Mask { get; set; }       //
        /// <summary>
        /// /网关
        /// </summary>
        public static IPAddress Gateway { get; set; }
        /// <summary>
        /// 手机串号
        /// </summary>
        public static string PhoneNumber { get; set; }
        /// <summary>
        /// 设置标识位
        /// </summary>
        public int Flag { get; set; }
        #endregion
 
        #region 私有变量
       
        /// <summary>
        /// 发送的数据长度
        /// </summary>
        private static int PacLength = 1 + 1 + 4 + 4 + 4 +20;//配置类型、标识位、被测设备新ID，原始ID，装置新ID
        /// <summary>
        /// 接收数据的数据长度
        /// </summary>
        private static int RecLength = 1 + 1 + 1 + 4 + 4 + 4 + 20;//配置类型、发送状态、标识位、被测设备ID，原始ID
        #endregion
      

        #region 公共函数
       
        /// <summary>
        /// 查询装置适配器信息
        /// </summary>
        /// <param name="cmd_ID"></param>
        public static void Query(string cmd_ID)
        {
            Con(cmd_ID, 0x00, 0x00, null, null, null,"");
        }

        /// <summary>
        /// 装置适配器信息设置
        /// </summary>
        /// <param name="cmd_ID">装置ID</param>
        /// <param name="request_Flag">设置标识位</param>
        /// <param name="ip">装置IP</param>
        /// <param name="subnetMask">子网掩码</param>
        /// <param name="gateWay">网关</param>
        /// <param name="phoneNumber">手机串号</param>
        public static void Set(string cmd_ID, 
            int request_Flag, 
            IPAddress ip, 
            IPAddress subnetMask, 
            IPAddress gateWay,
            string phoneNumber)
        {
            Con(cmd_ID, 0x01, request_Flag, ip, subnetMask, gateWay,phoneNumber);
        }
        public static void Ayanlise(string cmd_id, byte[] data)
        {
            if (data.Length < 34)
                return;
            string pacMsg = "WEB操作：";
            if (data[0] == 0x00)
                pacMsg += "查询 ";
            else
                pacMsg += "设置 ";
            int request_Flag = data[1];
            byte[] ip = new byte[4];
            if ((request_Flag & 0x01) == 1)
            {
                Buffer.BlockCopy(data, 0x2, ip, 0, 4);
                IP = new IPAddress(ip);
                pacMsg += "IP地址：" + IP.ToString() + " ";


            }
            if ((request_Flag & 0x02) == 0x02)
            {
                Buffer.BlockCopy(data, 2 + 4, ip, 0, 4);
                Subnet_Mask = new IPAddress(ip);
                pacMsg += "子网掩码：" + Subnet_Mask.ToString() + " ";
            }
            if ((request_Flag & 0x04) == 0x04)
            {
                Buffer.BlockCopy(data, 2 + 4 + 4, ip, 0, 4);
                Gateway = new IPAddress(ip);
                pacMsg += "网关：" + Gateway.ToString() + " ";
            }
            PhoneNumber = Encoding.Default.GetString(data, 2 + 4 + 4 + 4, 20);
            pacMsg += "手机串号：" + PhoneNumber + " ";

            //显示发送的数据
            PacketAnaLysis.DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.rec,
                    Termination.PowerPoleManage.Find(cmd_id),
                    "网络适配器",
                    pacMsg));
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
            byte[] ip = new byte[4];
            Buffer.BlockCopy(data, 0x3, ip, 0, 4);
            IP = new IPAddress(ip);
            pacMsg += "IP地址：" + IP.ToString() + " ";

            Buffer.BlockCopy(data, 3+4, ip, 0, 4);
            Subnet_Mask = new IPAddress(ip);
            pacMsg += "子网掩码：" + Subnet_Mask.ToString() + " ";

            Buffer.BlockCopy(data, 3 + 4 +4, ip, 0, 4);
            Gateway = new IPAddress(ip);
            pacMsg += "网关：" + Gateway.ToString() + " ";

            PhoneNumber = Encoding.Default.GetString(data, 3 + 4 + 4 + 4, 20);
            pacMsg += "手机串号：" + PhoneNumber + " ";
            PacketAnaLysis.DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.rec,
                    pole,
                    "网络适配器",
                    pacMsg));
        }
        #endregion

        #region 私有函数
     
        /// <summary>
        /// 装置适配器信息配置
        /// </summary>
        /// <param name="cmd_ID">装置ID</param>
        /// <param name="ConMode">配置标识</param>
        /// <param name="request_Flag">设置标识位</param>
        /// <param name="ip">装置IP</param>
        /// <param name="subnetMask">子网掩码</param>
        /// <param name="gateWay">网关</param>
        /// <param name="phoneNumber">手机串号</param>
        private static void Con(string cmd_ID, byte ConMode,
             int request_Flag,
            IPAddress ip,
            IPAddress subnetMask,
            IPAddress gateWay,
            string phoneNumber)
        {
            string pacMsg = "";
            CMD_ID = cmd_ID;

            #region 报文生成
            byte[] data = new byte[PacLength];
            data[0] = ConMode;
            if (ConMode == 0x00)
            {
                pacMsg = "查询";
                data[1] = 0x08;
            }
            if (ConMode == 0x01)
            {
                pacMsg = "设置";
                data[1] = (byte)(request_Flag & 0xff);
                //IP配置
                if ((request_Flag & 0x01) == 1)
                {
                    if (ip != null)
                    {
                        IP = ip;
                        Buffer.BlockCopy(ip.GetAddressBytes(), 0, data, 2, 4);
                        pacMsg += "IP地址：" + ip.ToString() +" ";
                    }
                    else
                        return;
                }
                //子网掩码
                if ((request_Flag & 0x02) == 0x02)
                {
                    if (subnetMask != null)
                    {
                        Subnet_Mask = subnetMask;
                        Buffer.BlockCopy(subnetMask.GetAddressBytes(), 0, data, 2+4, 4);
                        pacMsg += "子网掩码：" + subnetMask.ToString() +" ";
                    }
                    else
                        return; 
                }
                //网关
                if ((request_Flag & 0x04) == 0x04)
                {
                    if (gateWay != null)
                    {
                        Gateway = gateWay;
                        Buffer.BlockCopy(gateWay.GetAddressBytes(), 0, data, 2 + 4+4, 4);
                        pacMsg += "网关：" + gateWay.ToString() + " ";
                    }
                    else
                        return; 
                }
                //手机串号
                if ((request_Flag & 0x08) == 0x08)
                {
                    if (phoneNumber.Length== 20)
                    {
                        PhoneNumber = phoneNumber;
                        Buffer.BlockCopy(Encoding.Default.GetBytes(phoneNumber), 0, data, 2 + 4 + 4+4, 20);
                        pacMsg += "手机串号：" + phoneNumber + " ";
                    }
                    else
                        return;
                }
            }
            var packet = BuildPacket(data);     //生成报文                                                            
            #endregion

            string errorMsg;
            if (PackeDeal.SendData(CMD_ID, packet, out errorMsg))
            {
                //显示发送的数据
                PacketAnaLysis.DisPacket.NewRecord(
                    new DataInfo(
                        DataInfoState.send,
                         Termination.PowerPoleManage.Find(CMD_ID),
                        "网络适配器",
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
                PacketAnaLysis.PacketType_Control.NIA,
                FrameNO.GetFrameNO(),
                data);
            return Packet;
        }
        #endregion
    }
}
