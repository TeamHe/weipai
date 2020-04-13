using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.Server.Protocol;
using System.Configuration;


namespace GridBackGround.Communicat
{
    public class Service
    {

        private static Sodao.FastSocket.Server.UdpServer<CommandInfoV2> CMD_UDP;
        private static Sodao.FastSocket.Server.SocketServer<CommandInfoV2> CMD_TCP;
        private static Sodao.FastSocket.Server.SocketServer<CommandInfoV2> WEB_TCP;
       
        
        public static bool CommunicatInit()
        {
            bool state = true;
            string msg = "";
            //装置UDP连接
            CMD_UDP = new Sodao.FastSocket.Server.UdpServer<CommandInfoV2>(
                Config.SettingsForm.Default.CMD_Port, new UdpProtocol(), new UdpSeverCMD());
            if (!CMD_UDP.Start())
            {
                msg += "UDP端口：" + Config.SettingsForm.Default.CMD_Port.ToString() + "被占用\n";
                state = false;
            }
            //装置TCP连接
            CMD_TCP = new Sodao.FastSocket.Server.SocketServer<CommandInfoV2>(new TCPSeverCMD(),
                new Sodao.FastSocket.Server.Protocol.Protocol(),
                8192,
                8192,
                102400,
                20000);
            CMD_TCP.AddListener("binary", new System.Net.IPEndPoint
                (System.Net.IPAddress.Any, Config.SettingsForm.Default.CMD_Port));
            if (!CMD_TCP.Start())
            {
                msg += "TCP端口：" + Config.SettingsForm.Default.CMD_Port.ToString() + "被占用\n";
                state = false;
            }
            //WebTCP连接
            int WEB_Port = Config.SettingsForm.Default.WEB_Port;
            WEB_TCP = new Sodao.FastSocket.Server.SocketServer<CommandInfoV2>(new TCPSeverWeb(),
               new Sodao.FastSocket.Server.Protocol.Protocol(),
               8192,
               8192,
               102400,
               20000);
            WEB_TCP.AddListener("binary", new System.Net.IPEndPoint(System.Net.IPAddress.Any, WEB_Port));
            if (!WEB_TCP.Start())
            {
                msg += "WEB端口：" + WEB_Port.ToString() + "被占用\n";
                state = false;
            }
            if (msg.Length != 0)
                throw new Exception(msg);
            return state;
        }
        public static bool reStartCom()
        {
            try
            {
                if (CMD_UDP != null)
                    CMD_UDP.Stop();
                if (CMD_TCP != null)
                    CMD_TCP.Stop();
                if (WEB_TCP != null)
                    WEB_TCP.Stop();
            }
            catch { }
            return CommunicatInit();
        }
        
    }
}
