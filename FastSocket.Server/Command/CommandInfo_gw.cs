using System;
using System.Text;

namespace Sodao.FastSocket.Server.Command
{
    /// <summary>
    /// async binary command info.
    /// </summary>
    public class CommandInfo_gw : ICommandInfo
    {
        #region Constructors
        /// <summary>
        /// 报文内容
        /// </summary>
        /// <param name="CMD_ID">装置ID</param>
        /// <param name="packet_Length">包长</param>
        /// <param name="frame_Type">帧类型</param>
        /// <param name="packet_Type">报文类型</param>
        /// <param name="frame_No">帧序列号</param>
        /// <param name="packet">通讯报文</param>
        /// <param name="data">数据内容</param>
        /// <param name="Crc">CRC校验码</param>
        /// <param name="errorCode">错误代码</param>
        public CommandInfo_gw(  string  CMD_ID,
                                        int     packet_Length,
                                        int     frame_Type,
                                        int     packet_Type,
                                        byte    frame_No,
                                        byte[]  packet,                
                                        byte[]  data,
                                        byte[]  Crc,
                                        int     errorCode)
        {
            this.CMD_ID = CMD_ID;
            this.Packet_Lenth = packet_Length;
            this.Frame_Type = frame_Type;
            this.Packet_Type = packet_Type;
            this.Frame_No = frame_No;
            this.Packet = packet;
            this.Data = data;
            this.Crc = Crc;
            this.ErrorCode = errorCode;
        }

        public CommandInfo_gw() 
        {

        }

        #endregion

        #region Public Properties
        /// <summary>
        /// 装置ID
        /// </summary>
        public string CMD_ID { get; set; }
        /// <summary>
        /// 包长
        /// </summary>
        public int Packet_Lenth { get; set; }
        /// <summary>
        /// 帧类型
        /// </summary>
        public int Frame_Type { get; set; }
        /// <summary>
        /// 报文类型
        /// </summary>
        public int Packet_Type { get; set; }
        /// <summary>
        /// 帧序列号
        /// </summary>
        public byte Frame_No { get; set; }
        /// <summary>
        /// 报文内容
        /// </summary>
        public byte[] Packet { get; set; }
        /// <summary>
        /// 数据内容
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 校验码
        /// </summary>
        public byte[] Crc { get; set; }

        /// <summary>
        /// 校验码
        /// </summary>
        public int ErrorCode
        {
            get;
            private set;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// reply
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="payload"></param>
        public void Reply(SocketBase.IConnection connection, byte[] payload)
        {
            //var packet = PacketBuilder.ToAsyncBinary(this.CMD_ID, this.Packet_Lenth, payload);
            //connection.BeginSend(packet);
        }

        /// <summary>
        /// 报文基本规范解析
        /// 协议格式
        /// [Sync][Packet_Lenth][CMD_ID][Frame_Type][Packet_Type][FrameNo][data][CRC16][End]
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="readlength"></param>
        /// <returns></returns>
        public static CommandInfo_gw Find_commandinfo(ArraySegment<byte> buffer, out int readlength)
        {
            int Packet_Lenth = 0;       //报文长度
            string CMD_ID = "";         //状态监测装置ID
            byte Frame_Type = 0;         //帧类型
            byte Packet_Type = 0;       //报文类型
            byte Frame_No = 0;
            byte[] Pacekt = null;
            byte[] data = null;                //报文内容
            byte[] CRC = { 0x00 };         //CRC校验码
            int erroCode = 0;

            var payload = buffer.Array;
            readlength = buffer.Count;

            #region  数据长度校验
            //[Sync][Packet_Lenth][CMD_ID][Frame_Type][Packet_Type][FrameNo][data][CRC16][End]
            int MinLength = 27;      // 2 + 2 + 17 + 1 + 1 +1 + 2 + 1; //不包括报文内容
            if (buffer.Count < MinLength)
            {
                readlength = 0;
                return null;
            }
            #endregion

            #region 寻找包头
            //开始计数
            int startNo = 0;
            int i;
            //找到包的起始位置
            for (i = 0; i < buffer.Count - MinLength; i++)
            {
                if ((payload[i + buffer.Offset] == 0xa5) && (payload[buffer.Offset + i + 1] == 0x5a))
                {
                    startNo = i + buffer.Offset;
                    break;
                }
            }
            //没找到包头
            if (i == buffer.Count - MinLength)
            {
                erroCode = 0x02; //错误代码2
                //数据长度小于最小包长，将数据提取并出来交给上层处理
                Pacekt = new byte[buffer.Count - MinLength];
                readlength = buffer.Count - MinLength;
                return null;

            }
            #endregion

            #region 包长校验

            //计算包长
            Packet_Lenth = (int)(payload[startNo + 2]) + (int)(payload[startNo + 3]) * 256;
            if (Packet_Lenth > 2096)
            {
                //readlength = 4;
                try
                {
                    //if(Pacekt == null)
                    //    Pacekt = new byte[readlength];
                    Buffer.BlockCopy(payload, startNo, Pacekt, 0, readlength);
                    data = new byte[1];
                    erroCode = 0x03; //错误代码8
                    return new CommandInfo_gw(CMD_ID,
                        Packet_Lenth,
                        Frame_Type,
                        Packet_Type,
                        Frame_No,
                        Pacekt,
                        data,
                        CRC,
                        erroCode);
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }
            }
            Pacekt = new byte[Packet_Lenth + MinLength];
            readlength = startNo - buffer.Offset + MinLength + Packet_Lenth;   //读数长度

            if (buffer.Count < readlength)
            {
                readlength = 0;
                return null;
            }
            #endregion

            #region CRC校验
            Buffer.BlockCopy(payload, startNo, Pacekt, 0, Packet_Lenth + MinLength);
            CRC = SocketBase.CRC16.Crc(Pacekt, 2, Packet_Lenth + MinLength - 5);           //计算CRC16
            int CRC_Position = Packet_Lenth + MinLength - 3;
            if (!((CRC[0] == Pacekt[CRC_Position]) && CRC[1] == Pacekt[CRC_Position + 1]))        //CRC校验失败
            {
                erroCode = 0x04; //错误代码3
            }
            #endregion
            if (Pacekt[Packet_Lenth + MinLength - 1] != 0x96)
            {
                erroCode = 0x05;
            }
            #region 校验通过

            CMD_ID = Encoding.UTF8.GetString(Pacekt, 4, 17);          //装置ID
            Frame_Type = Pacekt[21];                                  //帧类型
            Packet_Type = Pacekt[22];                                 //报文类型
            Frame_No = Pacekt[23];                                    //帧序列号
            data = new byte[Packet_Lenth];
            int DataStart = 2 + 2 + 17 + 1 + 1 + 1;
            Buffer.BlockCopy(Pacekt, DataStart, data, 0, Packet_Lenth);
            return new CommandInfo_gw(CMD_ID,
                    Packet_Lenth,
                    Frame_Type,
                    Packet_Type,
                    Frame_No,
                    Pacekt,
                    data,
                    CRC,
                    erroCode);
            #endregion
        }

        /// <summary>
        /// 数据编码
        /// </summary>
        /// <returns></returns>
        public byte[] encode()
        {
            //byte[] data = ;
            short messageLength = (short)this.Packet_Lenth;
            var sendBuffer = new byte[messageLength + 27];

            sendBuffer[0] = 0xa5;
            sendBuffer[1] = 0x5a;
            //包长
            sendBuffer[2] = (byte)((messageLength) % 256);
            sendBuffer[3] = (byte)((messageLength) / 256);
            //CMA_ID
            byte[] cma_ID_Byte = Encoding.UTF8.GetBytes(CMD_ID);
            Buffer.BlockCopy(cma_ID_Byte, 0, sendBuffer, 4, 17);

            sendBuffer[21] = (byte)this.Frame_Type;     //帧类型
            sendBuffer[22] = (byte)this.Packet_Type;    //报文类型
            sendBuffer[23] = this.Frame_No;

            Buffer.BlockCopy(this.Data, 0, sendBuffer, 24, messageLength);  //数据内容

            byte[] crc = SocketBase.CRC16.Crc(sendBuffer, 2, messageLength + 22);
            sendBuffer[messageLength + 24] = crc[0];
            sendBuffer[messageLength + 25] = crc[1];
            sendBuffer[messageLength + 26] = 0x96;
            this.Packet = sendBuffer;
            return sendBuffer;
        }
        #endregion
    }

    /// <summary>
    /// async binary command info.
    /// </summary>
    public class Command_CMD : ICommandInfo
    {
        #region Constructors
        /// <summary>
        /// 报文内容
        /// </summary>
        /// <param name="CMD_ID">装置ID</param>
        /// <param name="packet_Length">包长</param>
        /// <param name="frame_Type">帧类型</param>
        /// <param name="packet_Type">报文类型</param>
        /// <param name="frame_No">帧序列号</param>
        /// <param name="packet">通讯报文</param>
        /// <param name="data">数据内容</param>
        /// <param name="Crc">CRC校验码</param>
        /// <param name="errorCode">错误代码</param>
        public Command_CMD(string CMD_ID,
                                        int packet_Length,
                                        int frame_Type,
                                        int packet_Type,
                                        byte frame_No,
                                        byte[] packet,
                                        byte[] data,
                                        byte[] Crc,
                                        int errorCode)
        {
            this.CMD_ID = CMD_ID;
            this.Packet_Lenth = packet_Length;
            this.Frame_Type = frame_Type;
            this.Packet_Type = packet_Type;
            this.Frame_No = frame_No;
            this.Packet = packet;
            this.Data = data;
            this.Crc = Crc;
            this.ErrorCode = errorCode;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// 装置ID
        /// </summary>
        public string CMD_ID
        {
            get;
            private set;
        }
        /// <summary>
        /// 包长
        /// </summary>
        public int Packet_Lenth
        {
            get;
            private set;
        }
        /// <summary>
        /// 帧类型
        /// </summary>
        public int Frame_Type
        {
            get;
            private set;
        }

        /// <summary>
        /// 报文类型
        /// </summary>
        public int Packet_Type
        {
            get;
            private set;
        }
        /// <summary>
        /// 帧序列号
        /// </summary>
        public byte Frame_No
        {
            get;
            private set;
        }
        /// <summary>
        /// 报文内容
        /// </summary>
        public byte[] Packet
        {
            get;
            private set;
        }
        /// <summary>
        /// 数据内容
        /// </summary>
        public byte[] Data
        {
            get;
            private set;
        }

        /// <summary>
        /// 校验码
        /// </summary>
        public byte[] Crc
        {
            get;
            private set;
        }

        /// <summary>
        /// 校验码
        /// </summary>
        public int ErrorCode
        {
            get;
            private set;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// reply
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="payload"></param>
        public void Reply(SocketBase.IConnection connection, byte[] payload)
        {
            //var packet = PacketBuilder.ToAsyncBinary(this.CMD_ID, this.Packet_Lenth, payload);
            //connection.BeginSend(packet);
        }
        #endregion
    }
}