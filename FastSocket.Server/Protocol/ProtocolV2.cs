using System;
using System.Text;
using Sodao.FastSocket.SocketBase;

namespace Sodao.FastSocket.Server.Protocol
{
    /// <summary>
    /// 异步二进制协议
    /// 协议格式
    /// [Message Length(int32)][SeqID(int32)][Request|Response Flag Length(int16)][Request|Response Flag + Body Buffer]
    /// </summary>

    public sealed class Protocol : IProtocol<Command.CommandInfo_gw>
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
        public Command.CommandInfo_gw FindCommandInfo(IConnection connection, ArraySegment<byte> buffer,
            int maxMessageSize, out int readlength)
        {
            return CommandAnalysis.AnalysisPacketV2(buffer, out readlength);
        }
        #endregion

    }

}