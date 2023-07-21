using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Sodao.FastSocket.SocketBase;
using Sodao.FastSocket.Server;
using ResModel.EQU;

namespace ResModel
{
    //public delegate void PowerPoleOutLineHandler(string str);
    /// <summary>
    /// 装置状态变化事件
    /// </summary>
    public class PowerPoleStateChange : EventArgs
    {
        /// <summary>
        /// 设备状态变化
        /// </summary>
        /// <param name="powerPole"></param>
        public PowerPoleStateChange(IPowerPole powerPole)
        {
            Power = powerPole;

        }
        /// <summary>
        /// 发送状态变化的节点
        /// </summary>
        public IPowerPole Power { get; set; }
    }


    public interface IPowerPole
    {
        int Pole_id { get; set; }
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
        OnLineStatus OnLine { get; }

        IPEndPoint IP { get; }

        /// <summary>
        /// 设备信息
        /// </summary>
        Equ Equ { get; set; }

        object UserData { get; set; }

        object Lock { get; }

        event EventHandler<PowerPoleStateChange> PowerPoleStateChange;

        bool SendSocket(IPowerPole pole, byte[] data, out string msg);
    }
}
