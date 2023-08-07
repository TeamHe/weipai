using System;


namespace Sodao.FastSocket.Server.Protocol
{

    /// <summary>
    /// UDP协议格式
    /// </summary>
    public class UdpProtocol : IUdpProtocol<Command.CommandInfo_gw>
    {
        /// <summary>
        /// 查找对应的命令格式
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public Command.CommandInfo_gw FindCommandInfo(ArraySegment<byte> buffer)
        {
            int readlength;
            return CommandAnalysis.AnalysisPacketV2(buffer, out readlength);
        }
    }
}
