using System;
using System.Text;
using Sodao.FastSocket.SocketBase;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.Server;
using GridBackGround.Communicat;
using GridBackGround.Termination;
using ResModel;
using cma.service.gw_cmd;
using cma.service.PowerPole;

namespace GridBackGround
{
    public class PackeDeal
    {
        public static bool OnSend;
        
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="iconnection"></param>
        public static void Disconnected(IConnection iconnection,Exception ex)
        {

            string str = DateTime.Now.ToShortTimeString()
                + "  " + iconnection.RemoteEndPoint.ToString()+"  DisConnected: " ;
            if(ex != null)
                str +=  ex.Message; 
            DisPacket.NewPacket(str);
        }

        /// <summary>
        /// TCP远程连接响应
        /// </summary>
        /// <param name="iconnection"></param>
        /// <param name="conType"></param>
        /// <returns></returns>
        public static bool Connected(IConnection iconnection, Communicat.EConnectType conType)
        {
            string str = DateTime.Now.ToShortTimeString()
               + "  " + iconnection.RemoteEndPoint.ToString();
            str += "connected";
            DisPacket.NewPacket(str);
            return false;
        }
        #region 发送数据处理
        /// <summary>
        /// 发送字符串
        /// </summary>
        /// <param name="CMD_ID">ID</param>
        /// <param name="data">数据</param>
        /// <param name="errorMsg">错误消息返回</param>
        /// <returns>返回消息</returns>
        public static bool SendData(string CMD_ID,string data, out string errorMsg)
        {
            return SendData(CMD_ID, Encoding.UTF8.GetBytes(data), out errorMsg);
        }
         /// <summary>
         /// 通过串口发送数据
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         public static bool SendData(byte[] data)
         {
             return SerialPortOP.Send(data, 0, data.Length);
         }
        /// <summary>
        /// 发送字符数据
        /// </summary>
        /// <param name="CMD_ID"></param>
        /// <param name="data"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
         public static bool SendData(string CMD_ID,byte[] data,out string errorMsg)
         {
             errorMsg = "";
             //检查是否是串口状态
             if (Config.SettingsForm.Default.ComMode == "SerialPort")
             {
                 return SendData(data);
             }
             if (Config.SettingsForm.Default.ComMode == "Socket")
             {
                 return SendSocket(CMD_ID,data,out errorMsg);
             }
             #region 网络发送数据
             return false;
             #endregion            
         }
         public static bool SendData(string CMD_ID, byte[] data, out string errorMsg, out int errcode)
         {
             errorMsg = "";
             //检查是否是串口状态
             if (Config.SettingsForm.Default.ComMode == "SerialPort")
             {
                 errcode = 15;
                 return SendData(data);
             }
             if (Config.SettingsForm.Default.ComMode == "Socket")
             {

                 return SendSocket(CMD_ID, data, out errorMsg, out  errcode);
             }
             #region 网络发送数据
             errcode = 15;
             return false;
             #endregion
         }

         public static bool SendSocket(IPowerPole powerPole, byte[] data, out string errorMsg, out int errCode)
         {
            errorMsg = "";
            string disData = "发送:";
            errCode = 0;
            if (powerPole == null)
            {
                errorMsg = "无效的设备handle";
                errCode = 11;
                return false;
            }

            if (powerPole.udpSession != null)
            {
                try
                {
                    powerPole.udpSession.SendAsync(data);
                    OnSend = true;
                    System.Threading.Thread.Sleep(5);
                    disData += Tools.StringTurn.ByteToHexString(data); ;
                    DisPacket.NewPacket(disData);                      //显示报文
                    return true;
                }
                catch
                {
                    errCode = 12;
                    return false;
                }
            }
            if (powerPole.Connection != null)
            {
                try
                {
                    powerPole.Connection.BeginSend(new Packet(data));
                    disData += Tools.StringTurn.ByteToHexString(data); ;
                    DisPacket.NewPacket(disData);                      //显示报文
                    return true;
                }
                catch
                {
                    errorMsg = "TCP数据发送失败";
                    errCode = 12;
                    return false;
                }
            }
            errorMsg = "设备离线";
            errCode = 14;
            return false;
         }

        public static bool SendSocket(IPowerPole powerPole, byte[] data, out string errorMsg)
        {
            return SendSocket(powerPole, data, out errorMsg, out _);
        }

        public static bool SendSocket(string CMD_ID, byte[] data, out string errorMsg, out int errCode)
        {
             IPowerPole powerPole = PowerPoleManage.Find(CMD_ID);
             if (powerPole == null)
             {
                 errorMsg = "未找到该设备";
                 errCode = 11;
                 return false;
             }
            return SendSocket(powerPole, data, out errorMsg, out errCode);
        }

         private static bool SendSocket(string CMD_ID, byte[] data, out string errorMsg)
         {
            int errCode = 0;
            return SendSocket(CMD_ID, data,out errorMsg,out errCode);
         }
         public static void SendComplete(IConnection connection)
         {
             OnSend = false;
         }
        #endregion
       
        #region   接收数据处理
        /// <summary>
        /// 接收到UDP数据
        /// </summary>
        /// <param name="session"></param>
        /// <param name="cmdInfo"></param>
        /// <returns></returns>
        public static bool RecData(UdpSession session, CommandInfo cmdInfo)
        {
            if(cmdInfo.CMD_ID.Length == 17)
               Termination.PowerPoleManage.PowerPole(cmdInfo.CMD_ID, session);
            RecDataDeal(cmdInfo,Communicat.EConnectType.UDP);
            return false;
        }
        /// <summary>
        /// 接收到TCP数据
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmdInfo"></param>
        /// <returns></returns>
        public static bool RecData(IConnection connection, CommandInfo cmdInfo)
        {
            if (cmdInfo.CMD_ID.Length == 17)
                PowerPoleManage.PowerPole(cmdInfo.CMD_ID, connection);
            RecDataDeal(cmdInfo, EConnectType.TCP);
            return false;
        }
        /// <summary>
        /// 对接收到的数据进行处理
        /// </summary>
        /// <param name="cmdInfo"></param>
        /// <param name="conType"></param>
        /// <returns></returns>
        public static bool RecDataDeal(CommandInfo cmdInfo, EConnectType conType)
        {
            string data = "接收:　" + "错误代码：" + cmdInfo.ErrorCode.ToString() + "  数据：";
            int errorcode = cmdInfo.ErrorCode;
            data += Tools.StringTurn.ByteToHexString(cmdInfo.Data);
            DisPacket.NewPacket(data);                      //显示报文
            if ((cmdInfo.ErrorCode == 0) || (cmdInfo.ErrorCode == 3))
            {
                //PacketAnaLysis.PackDivid_FrameType.PackDivid(cmdInfo, ref errorcode);
            }
            //if (cmdInfo.Code == 3)
                //PacketAnaLysis.PackDivid_FrameType.PackDivid(cmdInfo, ref errorcode);
            return false;
        }
        #endregion

        #region   接收数据处理V2
        /// <summary>
        /// 接收到UDP数据
        /// </summary>
        /// <param name="session"></param>
        /// <param name="cmdInfo"></param>
        /// <returns></returns>
        public static bool RecData(UdpSession session, CommandInfo_gw cmdInfo)
        {
            IPowerPole pole = null;
            if (cmdInfo.CMD_ID.Length == 17)
            {
                pole = PowerPoleManage.PowerPole(cmdInfo.CMD_ID, session);
                
            }
            RecDataDeal(cmdInfo, EConnectType.UDP, pole);
            return false;
        }
        /// <summary>
        /// 接收到TCP数据
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmdInfo"></param>
        /// <returns></returns>
        public static bool RecData(IConnection connection, CommandInfo_gw cmdInfo)
        {
            IPowerPole pole = null;
            if (cmdInfo.CMD_ID.Length == 17)
            {
                pole = PowerPoleManage.PowerPole(cmdInfo.CMD_ID, connection);
            }
            RecDataDeal(cmdInfo, EConnectType.TCP, pole);
            return false;
        }
        /// <summary>
        /// 对接收到的数据进行处理
        /// </summary>
        /// <param name="cmdInfo"></param>
        /// <param name="conType"></param>
        /// <returns></returns>
        public static bool RecDataDeal(CommandInfo_gw cmdInfo, EConnectType conType,IPowerPole pole)
        {

            int errorcode = cmdInfo.ErrorCode;
            string data = "接收:　" + "错误代码：" + cmdInfo.ErrorCode.ToString() + "  数据：";
            data += Tools.StringTurn.ByteToHexString(cmdInfo.Packet);
            DisPacket.NewPacket(data);
            //显示报文
            if ((cmdInfo.ErrorCode == 0))// || (cmdInfo.Code == 3))
            {
                if(pole != null)
                {
                    if (gw_cmd_handler.Deal(pole, cmdInfo))
                        return true;
                    else
                        PacketAnaLysis.PackDivid_FrameType.PackDivid(
                            pole,
                            cmdInfo.Frame_Type,
                            cmdInfo.Packet_Type,
                            cmdInfo.Frame_No,
                            cmdInfo.Data,
                            ref errorcode);

                }
            }

            //if (cmdInfo.Code == 3)
            //    PacketAnaLysis.DividPack_FrameType.PackDivid(cmdInfo, ref errorcode);
            cmdInfo = null;
            return false;
        }

        /// <summary>
        /// 对接收到的数据进行处理
        /// </summary>
        /// <param name="cmdInfo"></param>
        /// <param name="conType"></param>
        /// <returns></returns>
        public static bool RecDataDeal(CommandInfo_nw cmdInfo, EConnectType conType, IPowerPole pole)
        {

            string data = "接收:　" + "错误代码：" + cmdInfo.ErrorCode.ToString() + "  数据：";
            data += Tools.StringTurn.ByteToHexString(cmdInfo.Pakcet);
            DisPacket.NewPacket(data);
            CommandDeal.nw.nw_cmd_handle.Deal(pole, cmdInfo);
            cmdInfo = null;
            return false;
        }
        #endregion
    }


}
