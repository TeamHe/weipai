using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sodao.FastSocket.SocketBase;
using Sodao.FastSocket.Server;

namespace GridBackGround.Termination
{
    public delegate void OnLineStateChange(Termination.PowerPole powerPole);
    public static class PowerPoleManage
    {
        public static event OnLineStateChange OnStateChange;
        public static List<Termination.PowerPole> PowerPoleList;
        /// <summary>
        /// 初始化函数
        /// </summary>
        public static void PowerPoleManageInit()
        { 
            PowerPoleList = new List<Termination.PowerPole>();
        }

        public static void UpdatePolesStation()
        {
            if(PowerPoleList!= null)
            foreach (PowerPole pole in PowerPoleList)
            {
                pole.UpstateEqu();
            }
        }

        public static void UpdatePoleStation(string CMD_ID)
        {
            foreach (PowerPole pole in PowerPoleList)
            {
                if(pole.CMD_ID == CMD_ID)
                    pole.UpstateEqu();
            }
        }


        #region  private member
        
        #region  新建终端
        /// <summary>
        /// 创建新的终端节点并添加到列表中
        /// </summary>
        /// <param name="CMD_ID"></param>
        /// <returns></returns>
        private static PowerPole NewPowerPole(string name,string CMD_ID)
        {
            PowerPole powerPole = new PowerPole(CMD_ID);
            //接收节点变化事件
            powerPole.PowerPoleStateChange += new EventHandler<PowerPoleStateChange>(PoleStateChange);
            powerPole.UpstateEqu();
            PowerPoleList.Add(powerPole);
            return powerPole;
        }

        #endregion
        /// <summary>
        /// 终端状态变化事件接收处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PoleStateChange(object sender, Termination.PowerPoleStateChange e)
        {
            if (OnStateChange != null)
                OnStateChange(e.Power);     //触发终端变化事件，通知界面更新状态
        }
        
        /// <summary>
        /// 更新终端在线列表
        /// </summary>
        /// <param name="CMD_ID"></param>
        /// <param name="iconnection"></param>
        /// <param name="UdpSession"></param>
        private static IPowerPole PowerPoleDeal(string name,string CMD_ID, IConnection iconnection, UdpSession UdpSession)
        {
            PowerPole powerPole = (PowerPole)Find(CMD_ID);
            if (powerPole == null)
            {
                powerPole = NewPowerPole(name,CMD_ID);
                if (OnStateChange != null)              //触发终端状态事件，主界面显示新加入的终端
                    OnStateChange(powerPole);
            }
           //更新在线状态
            if (iconnection != null)
                powerPole.UpdatePowerPole(iconnection);
            if (UdpSession != null)
                powerPole.UpdatePowerPole(UdpSession);
            //更新设备名称
            if (name.Length != 0)
            {
                powerPole.Name = name;
                if (OnStateChange != null)              //触发终端状态事件，主界面显示新加入的终端
                    OnStateChange(powerPole);
            }
            return powerPole;
           
        }
        #endregion

        #region    公共函数
        /// <summary>
        /// 创建新节点带装置ID
        /// </summary>
        /// <param name="Name">装置名称</param>
        /// <param name="CMD_ID">装置ID</param>
        public static IPowerPole PowerPole(string Name, string CMD_ID)
        {
           return PowerPoleDeal(Name,CMD_ID, null, null);
        }
        /// <summary>
        /// 创建新节点装置ID
        /// </summary>
        /// <param name="CMD_ID">装置ID</param>
        public static IPowerPole PowerPole(string CMD_ID)
        {
           return PowerPoleDeal("",CMD_ID, null, null);
        }
        /// <summary>
        /// 更TCP新节点信息
        /// </summary>
        /// <param name="CMD_ID"></param>
        /// <param name="iconnection"></param>
        public static IPowerPole PowerPole(string CMD_ID, IConnection iconnection)
        {
           return PowerPoleDeal("",CMD_ID, iconnection, null);
        }
        /// <summary>
        /// 更新UDP节点信息
        /// </summary>
        /// <param name="CMD_ID"></param>
        /// <param name="udpSession"></param>
        public static IPowerPole PowerPole(string CMD_ID, UdpSession udpSession)
        {
           return  PowerPoleDeal("",CMD_ID, null, udpSession);
        }
        


        //查找指定的节点
        public static IPowerPole Find(string CMD_ID)
        {
            if (PowerPoleList != null)
                foreach (PowerPole powerPole in PowerPoleList)
                {
                    if (powerPole.CMD_ID == CMD_ID)
                        return powerPole;
                }
            return null;
        }
        #endregion
    }
}
