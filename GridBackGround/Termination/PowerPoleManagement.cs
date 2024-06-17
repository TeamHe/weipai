using System;
using Sodao.FastSocket.SocketBase;
using Sodao.FastSocket.Server;
using ResModel;
using System.Collections;
using cma.service;
using cma.service.nw_cmd;

namespace GridBackGround.Termination
{
    public delegate void OnLineStateChange(PowerPole powerPole);
    public static class PowerPoleManage
    {
        public static event OnLineStateChange OnStateChange;
        public static event EventHandler<PowerPole> OnPoleAdded;
        public static event EventHandler<PowerPole> OnPoleRemoved;
        //public static List<Termination.PowerPole> PowerPoleList;
        public static Hashtable PowerPoleList;
        /// <summary>
        /// 初始化函数
        /// </summary>
        public static void PowerPoleManageInit()
        { 
            PowerPoleList = new Hashtable();
            PowerPoleComMan.Init();
            PowerPoleStateMan.Init();
            nw_cmd_handle.GetHandles();
        }

        public static void UpdatePolesStation()
        {
            if(PowerPoleList!= null)
            foreach (PowerPole pole in PowerPoleList.Values)
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

        private static void _AddPowerPole(PowerPole pole)
        {
            if (OnPoleAdded != null)
            {
                OnPoleAdded(null,pole);
            }
        }

        private static void _OnPoleRemove(PowerPole pole)
        {
            if(OnPoleRemoved != null)
            {
                OnPoleRemoved(null,pole);
            }
        }

        #region  private member
        
        #region  新建终端
        #endregion
        /// <summary>
        /// 终端状态变化事件接收处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PoleStateChange(object sender, PowerPoleStateChange e)
        {
            if (OnStateChange != null)
                OnStateChange((PowerPole)e.Power);     //触发终端变化事件，通知界面更新状态
        }
        

        /// <summary>
        /// 更新终端在线列表
        /// </summary>
        /// <param name="CMD_ID"></param>
        /// <param name="iconnection"></param>
        /// <param name="UdpSession"></param>
        private static IPowerPole PowerPoleDeal(string name,string CMD_ID, IConnection iconnection, UdpSession UdpSession)
        {
            bool add = false;
            PowerPole powerPole = (PowerPole)Find(CMD_ID);
            if (powerPole == null)
            {
                powerPole = new PowerPole(CMD_ID);
                powerPole.UpstateEqu();
                powerPole.PowerPoleStateChange += new EventHandler<PowerPoleStateChange>(PoleStateChange);
                PowerPoleList.Add(CMD_ID,powerPole);
                if (OnStateChange != null)              //触发终端状态事件，主界面显示新加入的终端
                    OnStateChange(powerPole);
                add = true;
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
            if (add)
                _AddPowerPole(powerPole);
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
            if(PowerPoleList == null || !PowerPoleList.Contains(CMD_ID))
                return null;
            return PowerPoleList[CMD_ID] as IPowerPole;
        }
        #endregion
    }
}
