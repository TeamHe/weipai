using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodao.FastSocket.Server.Protocol
{
    /// <summary>
    /// 南网UDP数据协议处理函数
    /// </summary>
    public class Protocol_udp_nw : IUdpProtocol<Command.CommandInfo_nw>
    {
        CommandInfo_nw IUdpProtocol<CommandInfo_nw>.FindCommandInfo(ArraySegment<byte> buffer)
        {
            CommandInfo_nw info = null;
            byte[] data = buffer.Array;
            int offset = buffer.Offset;
            while (true)
            {
                int read_length = 0;
                ArraySegment<byte> tmp = new ArraySegment<byte>(data, offset, buffer.Count);
                info = CommandInfo_nw.Find_commandinfo_nw(tmp, out read_length);
                if (info != null)
                    break;
                if(read_length == 0) 
                    break; 
               offset += read_length;
            }
            return info;
        }
    }

    /// <summary>
    /// 南网TCP数据协议处理函数
    /// </summary>
    public sealed class Protocol_tcp_nw : IProtocol<Command.CommandInfo_nw>
    {
        CommandInfo_nw IProtocol<CommandInfo_nw>.FindCommandInfo(IConnection connection, 
            ArraySegment<byte> buffer, 
            int maxMessageSize, 
            out int readlength)
        {
            return CommandInfo_nw.Find_commandinfo_nw(buffer, out readlength);
        }
    }

}
