using System;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Protocol;

namespace ResModel.nw
{
    public class nw_pack_recv_args
    {
        public nw_pack_recv_args() { }

        public UdpSession Session { get; set; }

        public CommandInfo_nw CmdInfo { get; set; }
    }

    public class nw_service :  IUdpService<CommandInfo_nw>
    {
        private UdpServer<CommandInfo_nw> udp_server_nw;

        public event EventHandler<nw_pack_recv_args> OnPackageRecvd;

        public int Port { get; set; }


        public nw_service()
            :this(6012)
        { 
        
        } 

        public nw_service(int port)
        {
            this.Port = port;
        }

        public bool Start()
        {
            if(this.udp_server_nw != null)
            {
                this.udp_server_nw.Stop();
                this.udp_server_nw = null;
            }
            udp_server_nw = new UdpServer<CommandInfo_nw>(
                this.Port, new Protocol_udp_nw(), this);
            return this.udp_server_nw.Start();
        }




        public void OnReceived(UdpSession session, CommandInfo_nw cmdInfo)
        {
            try
            {
                if (cmdInfo == null || string.IsNullOrEmpty(cmdInfo.CMD_ID))
                    return;
                if(this.OnPackageRecvd != null)
                {
                    this.OnPackageRecvd(this, new nw_pack_recv_args()
                    {
                        Session = session,
                        CmdInfo = cmdInfo,
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("nw_service package deal error. " + e.Message);
            }
        }

        public void OnError(UdpSession session, Exception ex)
        {
            Console.WriteLine("Socket error on nw udp socket:" + ex.ToString());
            this.Start();
        }
    }

}
