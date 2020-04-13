using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Sodao.FastSocket.SocketBase;
using Sodao.FastSocket.Server;

using ResModel.EQU;

namespace GridBackGround.Termination
{
    //public delegate void PowerPoleOutLineHandler(string str);
   
     public interface IPowerPole
    {
        /// <summary>
        /// 装置名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 装置ID
        /// </summary>
        string CMD_ID { get; }

        //TCP终端
        IConnection Connection { get; }

        //UDP终端
        UdpSession udpSession { get; }

        //在线状态
        bool OnLine { get; }

        IPEndPoint IP { get; }


        Equ Equ { get; set; }

        object UserData { get; set; }

        event EventHandler<PowerPoleStateChange> PowerPoleStateChange;
    }
}
