using System;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.SocketBase;

namespace GridBackGround.Communicat
{
    public class TCPSeverWeb : CommandSocketService<CommandInfo_gw>
    {

        //连接事件
        public override void OnConnected(IConnection connection)
        {
            try
            {
                base.OnConnected(connection);
                PackeDeal.Connected(connection, EConnectType.TCP);

            }
            catch (Exception)
            {

            }

        }

        //TCP连接断开
        public override void OnDisconnected(IConnection connection, Exception ex)
        {
            base.OnDisconnected(connection, ex);
        }

        //接收数据事件
        public override void OnReceived(IConnection connection, CommandInfo_gw cmdInfo)
        {
            try
            {
                base.OnReceived(connection, cmdInfo);
                Termination.WebDataDeal.Deal(cmdInfo, connection);
            }
            catch (Exception)
            {

            }
        }

        //异常返回
        public override void OnException(IConnection connection, Exception ex)
        {
            base.OnException(connection, ex);
        }

        //数据发送完成
        public override void OnSendCallback(IConnection connection, SendCallbackEventArgs e)
        {
            base.OnSendCallback(connection, e);
            PackeDeal.SendComplete(connection);
        }

        #region 私有命令处理
        protected override void AddCommand(ICommand<CommandInfo_gw> cmd)
        {
            base.AddCommand(cmd);
        }

       
        //(IConnection connection, Command_CMD commandInfo)
        //{
        //    throw new NotImplementedException();
        //}
        protected override void HandleUnKnowCommand(IConnection connection, CommandInfo_gw commandInfo)
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
