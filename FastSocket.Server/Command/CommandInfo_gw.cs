using System;
using System.Text;

namespace Sodao.FastSocket.Server.Command
{
    /// <summary>
    /// async binary command info.
    /// </summary>
    public class CommandInfo_gw : ICommandInfo
    {

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
            int startNo = -1;
            var payload = buffer.Array;
            const int mlen = 27;      // 2 + 2 + 17 + 1 + 1 +1 + 2 + 1; //不包括报文内容
            readlength = 0;

            if (buffer.Count - buffer.Offset < mlen)
                return null;

            for (int i = buffer.Offset; i < buffer.Count - mlen; i++)
            {
                if ((payload[i] == 0xa5) && (payload[i + 1] == 0x5a))
                {
                    startNo = i;
                    break;
                }
                readlength++;
            }
            //没找到包头
            if (startNo <0)
                return null;

            //计算包长
            int plen = (payload[startNo + 2]) | ((payload[startNo + 3]) << 8);
            if (plen > 2096) //超长数据包检查
            {
                readlength++;
                return null;
            }
            //缓冲区长度不足，等待下次处理
            if (plen +  mlen > buffer.Count - startNo)  
                return null;

            int pcrc = plen + mlen - 3 + startNo;
            byte[] CRC = CRC16.Crc(payload, 2 + startNo, plen + mlen - 5);           //计算CRC16
            if (!((CRC[0] == payload[pcrc]) && CRC[1] == payload[pcrc + 1]))        //CRC校验失败
            {
                readlength++;
                return null;
            }
            readlength += plen + mlen;   //读数长度

            byte[] Pacekt = new byte[plen + mlen];
            byte[] data = new byte[plen];
            Buffer.BlockCopy(payload, startNo, Pacekt, 0, plen + mlen);
            Buffer.BlockCopy(Pacekt, 24, data, 0, plen);

            return new CommandInfo_gw()
            {
                CMD_ID = Encoding.UTF8.GetString(Pacekt, 4, 17),
                Packet_Lenth = plen,
                Frame_Type = Pacekt[21],
                Packet_Type = Pacekt[22],
                Frame_No = Pacekt[23],
                Packet = Pacekt,
                Data = data,
                Crc = CRC,
            };
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

            byte[] crc = CRC16.Crc(sendBuffer, 2, messageLength + 22);
            sendBuffer[messageLength + 24] = crc[0];
            sendBuffer[messageLength + 25] = crc[1];
            sendBuffer[messageLength + 26] = 0x96;
            this.Packet = sendBuffer;
            return sendBuffer;
        }
        #endregion
    }

    /// <summary>
    /// CRC16校验码计算
    /// </summary>
    public class CRC16
    {
        #region CRC 16 位校验表

        /// <summary> 
        /// 16 位校验表 Upper 表 
        /// </summary> 
        public static byte[] uppercrctab = new byte[]
        {
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
            0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
            0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
            0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
            0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40
        };

        /// <summary> 
        /// 16 位校验表 Lower 表 
        /// </summary> 
        public static byte[] lowercrctab = new byte[]
        {
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06,
            0x07, 0xC7, 0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD,
            0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
            0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A,
            0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC, 0x14, 0xD4,
            0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3,
            0xF2, 0x32, 0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4,
            0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
            0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29,
            0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF, 0x2D, 0xED,
            0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60,
            0x61, 0xA1, 0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67,
            0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
            0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68,
            0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA, 0xBE, 0x7E,
            0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71,
            0x70, 0xB0, 0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92,
            0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
            0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B,
            0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89, 0x4B, 0x8B,
            0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42,
            0x43, 0x83, 0x41, 0x81, 0x80, 0x40
        };
        #endregion
        /// <summary>
        /// CRC16校验码计算
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="usDataLen"></param>
        /// <returns></returns>
        public static byte[] Crc(byte[] buf, int usDataLen)
        {
            return Crc(buf, 0, usDataLen);
        }
        /// <summary>
        /// CRC16校验
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="startNo"></param>
        /// <param name="usDataLen"></param>
        /// <returns></returns>
        public static byte[] Crc(byte[] buf, int startNo, int usDataLen)
        {
            byte uchCRCHi;                // high byte of CRC initialized
            byte uchCRCLo;                // low byte of CRC initialized
            int uIndex;                       // will index into CRC lookup table

            uchCRCHi = 0xFF;
            uchCRCLo = 0xFF;

            int i = startNo;
            while (usDataLen > 0)
            {
                usDataLen--;
                uIndex = uchCRCHi ^ buf[i++];
                uchCRCHi = (byte)(uchCRCLo ^ uppercrctab[uIndex]);
                uchCRCLo = lowercrctab[uIndex];
            }
            byte[] ret = new byte[2];
            ret[0] = uchCRCLo;
            ret[1] = uchCRCHi;
            return ret;
        }
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