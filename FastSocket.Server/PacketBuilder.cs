using System;
using System.Text;

namespace Sodao.FastSocket.Server
{
    /// <summary>
    /// <see cref="SocketBase.Packet"/> builder
    /// </summary>
    static public class PacketBuilder
    {
        #region ToAsyncBinary
        ///// <summary>
        ///// to async binary <see cref="SocketBase.Packet"/>
        ///// </summary>
        ///// <param name="responseFlag"></param>
        ///// <param name="seqID"></param>
        ///// <param name="buffer"></param>
        ///// <returns></returns>
        //static public SocketBase.Packet ToAsyncBinary(string responseFlag, int seqID, byte[] buffer)
        //{
        //    var messageLength = (buffer == null ? 0 : buffer.Length) + responseFlag.Length + 6;
        //    var sendBuffer = new byte[messageLength + 4];

           

        //    return new SocketBase.Packet(sendBuffer);
        //}

        //
        /// <summary>
        /// 组装报文
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static byte[] MakePacket(Command.CommandInfo info)
        {
            //byte[] data = ;
            short messageLength = (short)info.Packet_Lenth;
            var sendBuffer = new byte[messageLength +25];

            sendBuffer[0] = 0xa5;
            sendBuffer[1] = 0x5a;
            //包长
            sendBuffer[2] = (byte)((info.Packet_Lenth) % 256);
            sendBuffer[3] = (byte)((info.Packet_Lenth) / 256);
            //CMA_ID
            byte[] cma_ID_Byte = Encoding.UTF8.GetBytes(info.CMD_ID);
            Buffer.BlockCopy(cma_ID_Byte, 0, sendBuffer, 4, 17);

            sendBuffer[21] = (byte)info.Frame_Type;     //帧类型
            sendBuffer[22] = (byte)info.Packet_Type;    //报文类型

            Buffer.BlockCopy(info.Data, 0, sendBuffer, 23, messageLength);  //数据内容

            byte[] crc = Sodao.FastSocket.SocketBase.CRC16.Crc(sendBuffer, messageLength + 23);
            sendBuffer[messageLength + 23] = crc[0];
            sendBuffer[messageLength + 24] = crc[1];

            return sendBuffer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CMD_ID"></param>
        /// <param name="Packet_Length"></param>
        /// <param name="Frame_Type"></param>
        /// <param name="Packet_Type"></param>
        /// <param name="frame_No"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] MakePacket(
            string CMD_ID,
            int Packet_Length,
            int Frame_Type,
            int Packet_Type,
            byte frame_No,
            byte[] data            
            )
        {
            //byte[] data = ;
            short messageLength = (short)Packet_Length;
            var sendBuffer = new byte[messageLength + 27];

            sendBuffer[0] = 0xa5;
            sendBuffer[1] = 0x5a;
            //包长
            sendBuffer[2] = (byte)((messageLength) % 256);
            sendBuffer[3] = (byte)((messageLength) / 256);
            //CMA_ID
            byte[] cma_ID_Byte = Encoding.UTF8.GetBytes(CMD_ID);
            Buffer.BlockCopy(cma_ID_Byte, 0, sendBuffer, 4, 17);

            sendBuffer[21] = (byte)Frame_Type;     //帧类型
            sendBuffer[22] = (byte)Packet_Type;    //报文类型
            sendBuffer[23] = frame_No;

            Buffer.BlockCopy(data, 0, sendBuffer, 24, messageLength);  //数据内容

            byte[] crc = Sodao.FastSocket.SocketBase.CRC16.Crc(sendBuffer, 2,messageLength + 22);
            sendBuffer[messageLength + 24] = crc[0];
            sendBuffer[messageLength + 25] = crc[1];
            sendBuffer[messageLength + 26] = 0x96;
            return sendBuffer;
        }
        #endregion

        #region ToCommandLine
        /// <summary>
        /// to command line <see cref="SocketBase.Packet"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public SocketBase.Packet ToCommandLine(string value)
        {
            return new SocketBase.Packet(Encoding.UTF8.GetBytes(string.Concat(value, Environment.NewLine)));
        }
        #endregion
    }
}