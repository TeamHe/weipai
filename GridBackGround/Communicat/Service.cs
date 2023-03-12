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
        private static Sodao.FastSocket.Server.UdpServer<CommandInfo_nw> udp_server_nw;
        private static Sodao.FastSocket.Server.SocketServer<CommandInfoV2> CMD_TCP;
        private static Sodao.FastSocket.Server.SocketServer<CommandInfoV2> WEB_TCP;
        private static HTTP.HttpListeners httpListeners = null;


        public static bool CommunicatInit()
        {
            bool state = true;
            string msg = "";
            //service mode 配置为南网模式
            if(Config.SettingsForm.Default.ServiceMode == "nw")
            {
                //装置UDP连接
                udp_server_nw = new Sodao.FastSocket.Server.UdpServer<CommandInfo_nw>(
                    Config.SettingsForm.Default.CMD_Port,
                    new Protocol_udp_nw(),
                    new UdpSeverNW());
                if (!udp_server_nw.Start())
                {
                    msg += "UDP端口：" + Config.SettingsForm.Default.CMD_Port.ToString() + "被占用\n";
                    state = false;
                }
            }
            else
            {
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

                httpListeners = new HTTP.HttpListeners(Config.SettingsForm.Default.WEB_Port);
                httpListeners.ListenerStart();

            }

            //WebTCP连接
            //int WEB_Port = Config.SettingsForm.Default.WEB_Port;
            //try
            //{
            //    if (httpListeners == null)
            //        httpListeners = new HTTP.HttpListeners();
            //    httpListeners.ListenerStart();


            //}
            //catch (Exception ex)
            //{
            //    msg += "WEB端口：" + WEB_Port.ToString() + "被占用\n";
            //    state = false;
            //}

            return state;
        }
        public static bool reStartCom()
        {
            try
            {
                if (Config.SettingsForm.Default.ServiceMode == "nw")
                {
                    if (udp_server_nw != null)
                        udp_server_nw.Stop();
                }
                else
                {
                    if (CMD_UDP != null)
                        CMD_UDP.Stop();
                    if (CMD_TCP != null)
                        CMD_TCP.Stop();
                    if (httpListeners != null)
                        httpListeners.ListenerStop();
                }
            }
            catch { }
            return CommunicatInit();
        }
        
    }
}
