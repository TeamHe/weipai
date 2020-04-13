using System;
using ResModel.EQU;

namespace Sodao.FastSocket.Server.Command
{
    /// <summary>
    /// async binary command info.
    /// </summary>
    public class CommandInfoV2 : ICommandInfo
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
        public CommandInfoV2(  string  CMD_ID,
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