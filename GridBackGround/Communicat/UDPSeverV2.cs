using System;
using Sodao.FastSocket.Server.Command;

namespace GridBackGround.Communicat
{
    /// <summary>
    /// 国网UDP链接收发处理
    /// </summary>
    public class UdpSeverCMD : Sodao.FastSocket.Server.IUdpService<CommandInfoV2>
    {
        //接收到数据处理
        public void OnReceived(Sodao.FastSocket.Server.UdpSession session, CommandInfoV2 cmdInfo)
        {
            try
            {
                PackeDeal.RecData(session, cmdInfo);
            }
            catch (Exception)
            {
            }
            //string str = UnicodeEncoding.UTF8.GetString(cmdInfo.Data);
        }

        //错误处理
        public void OnError(Sodao.FastSocket.Server.UdpSession session, Exception ex)
        {
            
            //Console.WriteLine(ex.ToString());
        }
    }
}