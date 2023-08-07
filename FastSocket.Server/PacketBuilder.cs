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