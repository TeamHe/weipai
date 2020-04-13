using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sodao.FastSocket.SocketBase;


namespace Sodao.FastSocket.Server.Protocol
{

    /// <summary>
    /// UDP协议格式
    /// </summary>
    public class UdpProtocol : Sodao.FastSocket.Server.Protocol.IUdpProtocol<Command.CommandInfoV2>
    {
        /// <summary>
        /// 查找对应的命令格式
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public Command.CommandInfoV2 FindCommandInfo(ArraySegment<byte> buffer)
        {
            int readlength;
            return CommandAnalysis.AnalysisPacketV2(buffer, out readlength);
        }
    }
}
