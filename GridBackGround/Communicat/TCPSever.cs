using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.SocketBase;
using GridBackGround.Communicat;

namespace GridBackGround.Communicat
{
    public class TCPSeverCMD : CommandSocketService<CommandInfoV2>
    {

        //连接事件
        public override void OnConnected(IConnection connection)
        {
            base.OnConnected(connection);
            PackeDeal.Connected(connection, EConnectType.TCP);

        }

        //TCP连接断开
        public override void OnDisconnected(IConnection connection, Exception ex)
        {
            base.OnDisconnected(connection, ex);
            PackeDeal.Disconnected(connection,ex);

        }

        //接收数据事件
        public override void OnReceived(IConnection connection, CommandInfoV2 cmdInfo)
        {
            base.OnReceived(connection, cmdInfo);
            PackeDeal.RecData(connection, cmdInfo);
        }

        //异常返回
        public override void OnException(IConnection connection, Exception ex)
        {
            base.OnException(connection, ex);
        }

        /// <summary>
        /// 数据发送完成
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="e"></param>
        public override void OnSendCallback(IConnection connection, SendCallbackEventArgs e)
        {
            base.OnSendCallback(connection, e);
            PackeDeal.SendComplete(connection);
        }

        #region 私有命令处理
        protected override void AddCommand(ICommand<CommandInfoV2> cmd)
        {
            base.AddCommand(cmd);
        }

        protected override void HandleUnKnowCommand(IConnection connection, CommandInfoV2 commandInfo)
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}
