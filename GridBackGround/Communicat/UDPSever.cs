using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;
using Sodao.FastSocket.Server.Protocol;

namespace GridBackGround.Communicat
{
    public class UdpService : Sodao.FastSocket.Server.IUdpService<CommandInfo>
    {
        //接收到数据处理
        public void OnReceived(Sodao.FastSocket.Server.UdpSession session, CommandInfo cmdInfo)
        {
            string str = UnicodeEncoding.UTF8.GetString(cmdInfo.Data);
            PackeDeal.RecData(session,cmdInfo);        
        }

        //错误处理
        public void OnError(Sodao.FastSocket.Server.UdpSession session, Exception ex)
        {
            //Console.WriteLine(ex.ToString());
        }
    }
}