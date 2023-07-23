using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.Server.Protocol;
using Sodao.FastSocket.Server;
using GridBackGround.Termination;
using ResModel;
using ResModel.nw;

namespace GridBackGround.Communicat
{
    public class Service
    {

        private static UdpServer<CommandInfoV2> CMD_UDP;
        private static SocketServer<CommandInfoV2> CMD_TCP;
        private static SocketServer<CommandInfoV2> WEB_TCP;
        private static HTTP.HttpListeners httpListeners = null;

        #region 南网Service 处理
        private static nw_service powerPoleMan_Nw = null;

        private static bool PowerPoleMan_nw_init(int port)
        {
            if (powerPoleMan_Nw == null)
            {
                powerPoleMan_Nw = new nw_service(port);
                powerPoleMan_Nw.OnPackageRecvd += PowerPoleMan_Nw_OnPackageRecvd;
            }
            else
            {
                powerPoleMan_Nw.Port = port;
            }
            return powerPoleMan_Nw.Start();
        }

        private static void PowerPoleMan_Nw_OnPackageRecvd(object sender, nw_pack_recv_args e)
        {
            UdpSession session = e.Session;
            CommandInfo_nw cmdInfo = e.CmdInfo;
            IPowerPole pole = PowerPoleManage.PowerPole(cmdInfo.CMD_ID, session);
            PackeDeal.RecDataDeal(cmdInfo, EConnectType.UDP, pole);
        }
        #endregion

        public static bool CommunicatInit()
        {
            bool state = true;
            string msg = "";
            //service mode 配置为南网模式
            if(Config.SettingsForm.Default.ServiceMode == "nw")
            {

                if (!PowerPoleMan_nw_init(Config.SettingsForm.Default.CMD_Port))
                {
                    msg += "UDP端口：" + Config.SettingsForm.Default.CMD_Port.ToString() + "被占用\n";
                    state = false;
                }
            }
            else
            {
                //装置UDP连接
                CMD_UDP = new UdpServer<CommandInfoV2>(
                    Config.SettingsForm.Default.CMD_Port, new UdpProtocol(), new UdpSeverCMD());
                if (!CMD_UDP.Start())
                {
                    msg += "UDP端口：" + Config.SettingsForm.Default.CMD_Port.ToString() + "被占用\n";
                    state = false;
                }
                //装置TCP连接
                CMD_TCP = new SocketServer<CommandInfoV2>(new TCPSeverCMD(),
                    new Protocol(),
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
                if (CMD_UDP != null)
                    CMD_UDP.Stop();
                if (CMD_TCP != null)
                    CMD_TCP.Stop();
                if (httpListeners != null)
                    httpListeners.ListenerStop();
            }
            catch { }
            return CommunicatInit();
        }
        
    }
}
