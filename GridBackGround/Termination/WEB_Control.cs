using System;
using System.Collections.Generic;
using System.Text;
using Sodao.FastSocket.SocketBase;
using Sodao.FastSocket.Server.Command;
using System.Timers;
using ResModel.EQU;
using cma.service;

namespace GridBackGround.Termination
{
    public interface IWeb_Control
    {
        /// <summary>
        /// 装置ID
        /// </summary>
        string CMD_ID { get; set; }

        string BACK_CMDID { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        int PacLength { get; set; }
        /// <summary>
        /// TCP连接
        /// </summary>
        IConnection iconnection { get; set; }
        /// <summary>
        /// 配置帧类型
        /// </summary>
        int Frame_Type { get; set; }
        /// <summary>
        /// 配置报文类型
        /// </summary>
        int Pack_Type { get; set; }
        /// <summary>
        /// 配置报文内容
        /// </summary>
        byte[] Pack { get; set; }

        void Stop();
    }
    public class WEB_Control : IWeb_Control
    {
        public System.Timers.Timer timer { get; set; }
        #region Constructors
        /// <summary>
        /// 装置初始化
        /// </summary>
        /// <param name="CMD_ID"></param>
        public WEB_Control(CommandInfo_gw cmdInfo, IConnection connection)
        {
            //new PowerPole("", CMD_ID);
            if (cmdInfo == null) throw new ArgumentNullException("配置内容");
            Frame_Type = cmdInfo.Frame_Type;
            Pack_Type = cmdInfo.Packet_Type;
            Pack = cmdInfo.Packet;
            CMD_ID = cmdInfo.CMD_ID;
            BACK_CMDID = cmdInfo.CMD_ID;
            PacLength = cmdInfo.Packet_Lenth;
            iconnection = connection;

            timer = new System.Timers.Timer(5 * 1000);
            timer.AutoReset = false;
            timer.Elapsed += new ElapsedEventHandler(OutLine);
            timer.Start();
        }


        #endregion

        #region   IWeb_Control Members
        // <summary>
        /// 装置ID
        /// </summary>
        public string CMD_ID { get; set; }
        /// <summary>
        /// 备用装置ID
        /// </summary>
        public string BACK_CMDID { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int PacLength { get; set; }
        /// <summary>
        /// TCP连接
        /// </summary>
        public IConnection iconnection { get; set; }
        /// <summary>
        /// 配置帧类型
        /// </summary>
        public int Frame_Type { get; set; }
        /// <summary>
        /// 配置报文类型
        /// </summary>
        public int Pack_Type { get; set; }
        /// <summary>
        /// 配置报文内容
        /// </summary>
        public byte[] Pack { get; set; }

        public void Stop()
        {
            this.timer.Stop();
        }
        #endregion

        #region 私有函数
        //定时器超时，设备更新状态下线。并触发设备下线事件
        private void OutLine(object sender, ElapsedEventArgs e)
        {
            Web_DataSend.MakeResPonse(this, 13);
        }
        #endregion
    }

    public class Web_Control_Mang
    {
        private static List<IWeb_Control> list_WebControl;

        public static IWeb_Control New(CommandInfo_gw cmdinfo, IConnection connection)
        {
            var web = new WEB_Control(cmdinfo, connection);
            if (list_WebControl == null)
            {
                list_WebControl = new List<IWeb_Control>();
            }
            list_WebControl.Add(web);
            return web;
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="cmdinfo"></param>
        /// <returns></returns>
        public static IWeb_Control Find(Command_CMD cmdinfo)
        {
            if (list_WebControl == null)
                return null;
            foreach (IWeb_Control web in list_WebControl)
            {
                if (String.Compare(web.CMD_ID, cmdinfo.CMD_ID) == 0
                    && (web.Pack_Type == cmdinfo.Packet_Type))
                    return web;
                if ((EQU_Control)cmdinfo.Packet_Type == (EQU_Control.ID))
                {
                    if (String.Compare(web.BACK_CMDID, cmdinfo.CMD_ID) == 0)
                        return web;
                }
            }
            return null;
        }
        /// <summary>
        /// 删除web控制
        /// </summary>
        /// <param name="web"></param>
        public static void Delate(IWeb_Control web)
        {
            try
            {
                web.Stop();
                list_WebControl.Remove(web);
            }
            catch
            {
                return;
            }
        }
    }

    public class Web_DataSend
    {
        public static void SendData(IWeb_Control web)
        {
            string errorMsg;
            int errCode = 0;
            try
            {
                byte[] crc = CRC16.Crc(web.Pack, 2, web.PacLength + 22);
                web.Pack[web.PacLength + 24] = crc[0];
                web.Pack[web.PacLength + 25] = crc[1];
                web.Pack[web.PacLength + 26] = 0x96;
                PackeDeal.SendData(web.CMD_ID, web.Pack, out errorMsg, out errCode);
                if (errCode != 0)
                {
                    MakeResPonse(web, errCode);
                }
            }
            catch
            {
                MakeResPonse(web, 12);
            }


        }


        public static void MakeResPonse(IWeb_Control web, int errCode)
        {
            int PacLength = 0;
            byte[] data;
            switch ((EQU_Control)web.Pack_Type)
            { //网络适配器
                case EQU_Control.NIA:
                    PacLength = 34;
                    data = new byte[PacLength];
                    data[0] = web.Pack[24];
                    data[1] = (byte)errCode;
                    break;
                case EQU_Control.HisData://历史数据
                    PacLength = 2;
                    data = new byte[PacLength];
                    data[0] = (byte)errCode;
                    //data[1] = (byte)errCode;
                    break;
                case EQU_Control.MainTime://采样参数
                    PacLength = 7;
                    data = new byte[PacLength];
                    data[0] = web.Pack[24];
                    data[1] = (byte)errCode;
                    break;
                case EQU_Control.HostComputer://上位机
                    PacLength = 9;
                    data = new byte[PacLength];
                    data[0] = web.Pack[24];
                    data[1] = (byte)errCode;
                    break;
                case EQU_Control.ID://ID
                    PacLength = 37;
                    data = new byte[PacLength];
                    data[0] = web.Pack[24];
                    data[1] = (byte)errCode;
                    break;
                case EQU_Control.Reset://复位
                    PacLength = 1;
                    data = new byte[PacLength];
                    data[0] = (byte)errCode;
                    break;
                case EQU_Control.Model://模型参数
                    PacLength = 3;
                    data = new byte[PacLength];
                    data[1] = (byte)errCode;
                    break;
                default:
                    web.Stop();
                    Web_Control_Mang.Delate(web);
                    return;
            }

            var pac = PacketAnaLysis.BuildPacket.PackBuild(
                web.CMD_ID,
                PacLength,
                web.Frame_Type + 1,
                web.Pack_Type,
                CommandDeal.FrameNO.GetFrameNO(),
                data);
            ResPonse(web, pac);

        }

        public static void ResPonse(IWeb_Control web, byte[] data)
        {
            if (web == null)
                return;
            if (web.iconnection == null)
                return;
            try
            {
                web.iconnection.BeginSend(new Packet(data));
                //PackeDeal.DisPac(cmdinfo);
                string disData = "WEB发送:";
                disData += Tools.StringTurn.ByteToHexString(data);
                DisPacket.NewPacket(disData);                      //显示报文
                web.Stop();
                Web_Control_Mang.Delate(web);

            }
            catch
            {
                web.Stop();
                Web_Control_Mang.Delate(web);
            }
        }
    }

    public class WebDataDeal
    {
        public static void Deal(CommandInfo_gw cmdinfo, IConnection connection)
        {

            string data = "WEB接收:　" + "错误代码：" + cmdinfo.ErrorCode.ToString() + "  数据：";
            int errorcode = cmdinfo.ErrorCode;
            data += Tools.StringTurn.ByteToHexString(cmdinfo.Packet);
            DisPacket.NewPacket(data);                      //显示报文

            if (cmdinfo.ErrorCode != 0 && cmdinfo.ErrorCode < 4)
                return;
            var web = Web_Control_Mang.New(cmdinfo, connection);
            if ((EQU_Control)cmdinfo.Packet_Type == EQU_Control.ID)
            {
                if ((cmdinfo.Data[1] & 0x01) == 1)
                {
                    web.BACK_CMDID = Encoding.Default.GetString(cmdinfo.Data, 2 + 17 * 2, 17);
                    //pacMsg += "装置ID:" + new_cmd_id + " ";
                }
            }
            Web_DataSend.SendData(web);
            switch ((EQU_Control)cmdinfo.Packet_Type)
            {

                //网络适配器
                case EQU_Control.Model://模型参数
                    CommandDeal.Comand_Model.Ayanlise(cmdinfo.CMD_ID, cmdinfo.Data);
                    break;
            }
        }
    }
}