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
            try
            {
                base.OnConnected(connection);
                PackeDeal.Connected(connection, EConnectType.TCP);

            }
            catch
            {

            }

        }

        //TCP连接断开
        public override void OnDisconnected(IConnection connection, Exception ex)
        {
            try
            {
                base.OnDisconnected(connection, ex);
                PackeDeal.Disconnected(connection, ex);

            }
            catch
            {

            }

        }

        //接收数据事件
        public override void OnReceived(IConnection connection, CommandInfoV2 cmdInfo)
        {
            try
            {
                base.OnReceived(connection, cmdInfo);
                DateTime tstart = DateTime.Now;
                PackeDeal.RecData(connection, cmdInfo);
                DateTime tend = DateTime.Now;
                TimeSpan timeSpan = tend.Subtract(tstart);
                System.Console.WriteLine("packet deal take time" + timeSpan.TotalMilliseconds.ToString());
            }
            catch
            {

            }
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
            try
            {
                base.OnSendCallback(connection, e);
                PackeDeal.SendComplete(connection);

            }
            catch
            {

            }
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

    public class TCPSeverNW : CommandSocketService<CommandInfo_nw>
    {

        //连接事件
        public override void OnConnected(IConnection connection)
        {
            try
            {
                base.OnConnected(connection);
                PackeDeal.Connected(connection, EConnectType.TCP);

            }
            catch
            {

            }

        }

        //TCP连接断开
        public override void OnDisconnected(IConnection connection, Exception ex)
        {
            try
            {
                base.OnDisconnected(connection, ex);
                PackeDeal.Disconnected(connection, ex);

            }
            catch
            {

            }

        }

        //接收数据事件
        public override void OnReceived(IConnection connection, CommandInfo_nw cmdInfo)
        {
            try
            {
                base.OnReceived(connection, cmdInfo);
                DateTime tstart = DateTime.Now;
                //PackeDeal.RecData(connection, cmdInfo);
                DateTime tend = DateTime.Now;
                TimeSpan timeSpan = tend.Subtract(tstart);
                System.Console.WriteLine("packet deal take time" + timeSpan.TotalMilliseconds.ToString());
            }
            catch
            {

            }
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
            try
            {
                base.OnSendCallback(connection, e);
                //PackeDeal.SendComplete(connection);

            }
            catch
            {

            }
        }

        #region 私有命令处理
        protected override void AddCommand(ICommand<CommandInfo_nw> cmd)
        {
            base.AddCommand(cmd);
        }

        protected override void HandleUnKnowCommand(IConnection connection, CommandInfo_nw commandInfo)
        {
            //throw new NotImplementedException();
        }
        #endregion
    }

}
