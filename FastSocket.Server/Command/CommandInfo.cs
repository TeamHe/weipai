using System;

namespace Sodao.FastSocket.Server.Command
{
    /// <summary>
    /// async binary command info.
    /// </summary>
    public class CommandInfo : ICommandInfo
    {
        #region Constructors
       /// <summary>
       /// new 
       /// </summary>
       /// <param name="CMD_ID">装置ID</param>
       /// <param name="Packet_Length">包长</param>
       /// <param name="Frame_Type">帧类型</param>
       /// <param name="Packet_Type">报文类型</param>
       /// <param name="data">报文内容</param>
       /// <param name="Crc">CRC校验码</param>
        /// <param name="errorCode">错误代码</param>   
        public CommandInfo(string CMD_ID,
                                        int     Packet_Length,
                                        int     Frame_Type,
                                        int     Packet_Type,                                        
                                        byte[]  data,
                                        byte[]  Crc,
                                        int     errorCode)
        {
            this.CMD_ID = CMD_ID;
            this.Packet_Lenth = Packet_Length;
            this.Frame_Type = Frame_Type;
            this.Packet_Type = Packet_Type;
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
        /// 帧类型
        /// </summary>
        public int Packet_Type
        {
            get;
            private set;
        }
        /// <summary>
        /// 主体内容
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