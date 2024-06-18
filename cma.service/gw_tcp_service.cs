using System;
using System.Net;
using ResModel;
using ResModel.PowerPole;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.Server.Protocol;
using Sodao.FastSocket.SocketBase;

namespace cma.service.gw_cmd
{

    public class gw_tcp_service : CommandSocketService<CommandInfo_gw>
    {
        public int Port { get; set; }
        public string Name { get { return "gw-tcp"; } }
        //public bool IsActive { get { if(this.Service == null || this.Service.) }

        private SocketServer<CommandInfo_gw> Service = null;

        public void Start()
        {
            this.Stop();
            this.Service = new SocketServer<CommandInfo_gw>(new gw_tcp_service(),
                new Protocol_tcp_gw(),
                8192,
                8192,
                102400,
                20000);
            this.Service.AddListener("gw-tcp", new IPEndPoint(IPAddress.Any, this.Port));
            this.Service.Start();
        }

        public void Stop()
        {
            try
            {
                if (Service != null)
                {
                    this.Service.Stop();
                    this.Service = null;
                }
            }
            catch { }
        }

        public void Restart()
        {
            if (this.Service != null)
                this.Stop();
            this.Start();
        }

        protected override void HandleUnKnowCommand(IConnection connection, CommandInfo_gw commandInfo)
        {
            //throw new NotImplementedException();
        }

        public override void OnConnected(IConnection connection)
        {
            DisPacket.NewPacket(string.Format("{0}  {1} connected",
                DateTime.Now.ToShortTimeString(),
                connection.RemoteEndPoint));
            base.OnConnected(connection);

        }

        public override void OnDisconnected(IConnection connection, Exception ex)
        {
            DisPacket.NewPacket(string.Format("{0}  {1}  DisConnected. {2}",
                DateTime.Now.ToShortTimeString(),
                connection.RemoteEndPoint.ToString(),
                ex != null ? ex.Message : string.Empty));

            base.OnDisconnected(connection, ex);
        }

        public override void OnException(IConnection connection, Exception ex)
        {
            base.OnException(connection, ex);
        }

        public override void OnReceived(IConnection connection, CommandInfo_gw cmdInfo)
        {
            base.OnReceived(connection, cmdInfo);
            
            try 
            {
                IPowerPole pole = null;
                if ((pole = PowerPoleManage.GetInstance().PowerPole(cmdInfo.CMD_ID,connection)) != null)
                {
                    DisPacket.NewPackageMessage(pole, RSType.Recv, SrcType.GW_TCP,
                        pole.IP != null ? pole.IP.ToString() : "unknown", 0, cmdInfo.Packet);
                    gw_cmd_handler.Deal(pole, cmdInfo);
                }
            }
            catch (Exception ex) 
            {
                
                Console.WriteLine(string.Format("Service:{0} Source:{0} FrameType:{1} PackageType:{2} Data:{3} exception:{4}",
                    connection.RemoteEndPoint,
                    cmdInfo.Frame_Type,
                    cmdInfo.Packet_Type,
                    BitConverter.ToString(cmdInfo.Data),
                    ex.Message));
            }
        }

        public override void OnSendCallback(IConnection connection, SendCallbackEventArgs e)
        {
            base.OnSendCallback(connection, e);
        }
    }
}
