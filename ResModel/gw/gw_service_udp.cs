using System;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.Server.Protocol;

namespace ResModel.gw
{
    public class gw_pack_recv_args
    {
        public gw_pack_recv_args() { }

        public UdpSession Session { get; set; }

        public CommandInfoV2 CmdInfo { get; set; }
    }


    public class gw_service_udp : IUdpService<CommandInfoV2>
    {
        private UdpServer<CommandInfoV2> udp_server_gw;

        public event EventHandler<gw_pack_recv_args> OnPackageRecvd;
        public int Port { get; set; }

        public gw_service_udp()
            : this(6012)
        {

        }

        public gw_service_udp(int port)
        {
            this.Port = port;
        }

        public bool Start()
        {
            if (this.udp_server_gw != null)
            {
                this.udp_server_gw.Stop();
                this.udp_server_gw = null;
            }
            udp_server_gw = new UdpServer<CommandInfoV2>(
                this.Port, new UdpProtocol(), this);
            return this.udp_server_gw.Start();
        }

        public void OnError(UdpSession session, Exception ex)
        {
            Console.WriteLine("Socket error on gw udp socket:" + ex.ToString());
            this.Start();

        }

        public void OnReceived(UdpSession session, CommandInfoV2 cmdInfo)
        {
            try
            {
                if (cmdInfo == null || string.IsNullOrEmpty(cmdInfo.CMD_ID))
                    return;
                if (this.OnPackageRecvd != null)
                {
                    this.OnPackageRecvd(this, new gw_pack_recv_args()
                    {
                        Session = session,
                        CmdInfo = cmdInfo,
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("gw_service_udp package deal error. " + e.Message);
            }
        }
    }
}
