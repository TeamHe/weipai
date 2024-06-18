using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.Server;
using ResModel;
using ResModel.nw;
using ResModel.gw;
using cma.service;
using cma.service.gw_cmd;

namespace GridBackGround.Communicat
{
    public class Service
    {
        //private static SocketServer<CommandInfo_gw> CMD_TCP;
        private static gw_tcp_service gw_Tcp_Service = null;
        private static HTTP.HttpListeners httpListeners = null;

        private static gw_service_udp service_gw_udp = null;

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
            IPowerPole pole = PowerPoleManage.GetInstance().PowerPole(cmdInfo.CMD_ID, session);
            PackeDeal.RecDataDeal(cmdInfo, EConnectType.UDP, pole);
        }
        #endregion

        public static bool PowerPoleMan_gw_udp_init(int port)
        {
            if (service_gw_udp == null)
            {
                service_gw_udp = new gw_service_udp(port);
                service_gw_udp.OnPackageRecvd += Service_gw_udp_OnPackageRecvd; ;
            }
            else
            {
                service_gw_udp.Port = port;
            }
            return service_gw_udp.Start();
        }

        private static void Service_gw_udp_OnPackageRecvd(object sender, gw_pack_recv_args e)
        {
            UdpSession session = e.Session;
            CommandInfo_gw cmdInfo = e.CmdInfo;
            IPowerPole pole = PowerPoleManage.GetInstance().PowerPole(cmdInfo.CMD_ID, session);
            PackeDeal.RecDataDeal(cmdInfo, EConnectType.UDP, pole);
        }

        public static bool CommunicatInit()
        {
            bool state = true;
            string msg = "";
            if (!PowerPoleMan_nw_init(Config.SettingsForm.Default.nw_port))
            {
                msg += "南网UDP端口：" + Config.SettingsForm.Default.nw_port.ToString() + "被占用\n";
                state = false;
            }
            if (!PowerPoleMan_gw_udp_init(Config.SettingsForm.Default.gw_port))
            {
                msg += "国网UDP端口：" + Config.SettingsForm.Default.gw_port.ToString() + "被占用\n";
                state = false;
            }

            try
            {
                gw_Tcp_Service = new gw_tcp_service() { Port = Config.SettingsForm.Default.gw_port, };
                gw_Tcp_Service.Start();
            }
            catch
            {
                msg += "TCP端口：" + Config.SettingsForm.Default.gw_port.ToString() + "被占用\n";
                state = false;
            }

            //httpListeners = new HTTP.HttpListeners(Config.SettingsForm.Default.WEB_Port);
            //httpListeners.ListenerStart();

            //}

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
                if (gw_Tcp_Service != null)
                    gw_Tcp_Service.Stop();
                if (httpListeners != null)
                    httpListeners.ListenerStop();
            }
            catch { }
            return CommunicatInit();
        }
        
    }
}
