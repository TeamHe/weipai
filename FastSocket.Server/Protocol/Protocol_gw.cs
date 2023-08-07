using System;
using System.Text;
using Sodao.FastSocket.SocketBase;
using Sodao.FastSocket.Server.Command;

namespace Sodao.FastSocket.Server.Protocol
{
    /// <summary>
    /// 异步二进制协议
    /// 协议格式
    /// [Message Length(int32)][SeqID(int32)][Request|Response Flag Length(int16)][Request|Response Flag + Body Buffer]
    /// </summary>

    public sealed class Protocol_tcp_gw : IProtocol<CommandInfo_gw>
    {
        #region IProtocol Members
        /// <summary>
        /// find command
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="buffer"></param>
        /// <param name="maxMessageSize"></param>
        /// <param name="readlength"></param>
        /// <returns></returns>
        /// <exception cref="BadProtocolException">bad async binary protocl</exception>
        public CommandInfo_gw FindCommandInfo(IConnection connection, ArraySegment<byte> buffer,
            int maxMessageSize, out int readlength)
        {
            return CommandInfo_gw.Find_commandinfo(buffer, out readlength);
        }
        #endregion

    }

    /// <summary>
    /// UDP协议格式
    /// </summary>
    public class Protocol_udp_gw : IUdpProtocol<CommandInfo_gw>
    {
        /// <summary>
        /// 查找对应的命令格式
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public CommandInfo_gw FindCommandInfo(ArraySegment<byte> buffer)
        {
            int readlength;
            return CommandInfo_gw.Find_commandinfo(buffer, out readlength);
        }
    }

}