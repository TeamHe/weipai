using GridBackGround.CommandDeal.nw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ResModel.PowerPole;

namespace GridBackGround.Termination
{
    public class PowerPoleStateMan
    {

        private Dictionary<PowerPole, PowerPoleState> _poles;

        private Timer _timer;

        public PowerPoleStateMan()
        {
            this._timer = new Timer()
            {
                AutoReset = true,
                Interval = 1000,
                Enabled = true,
            };
            this._timer.Elapsed += _timer_Elapsed;
            this._poles = new Dictionary<PowerPole, PowerPoleState>();
            PowerPoleManage.OnPoleAdded += PowerPoleManage_PoleAdded;
            PowerPoleManage.OnPoleRemoved += PowerPoleManage_PoleRemoved;
        }

        /// <summary>
        /// 触发秒处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var item in _poles)
            {
                PowerPoleState online = item.Value;
                online.OnSec();
            }
        }

        /// <summary>
        /// 新增一个新设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PowerPoleManage_PoleRemoved(object sender, PowerPole e)
        {
            this.UnResgister(e);
        }

        private void PowerPoleManage_PoleAdded(object sender, PowerPole e)
        {
            this.Resgister(e, e.State);
        }

        public bool Resgister(PowerPole pole, PowerPoleState online)
        {
            if (_poles.ContainsKey(pole))
                _poles[pole] = online;
            else
                _poles.Add(pole, online);

            //南网设备
            if (pole.Flag == PowerPoleFlag.NW)
            {
                var cmd = new nw_cmd_0a_device_config_get(pole);
                var com = PowerPoleComMan.CreateNewCom(cmd,2);
                //com.OnFinished += Com_Finish;
                //Console.WriteLine("{0:yyyy-MM-dd hh:mm:ss fff} {1} 请求获取配置信息完成. Result:{2}", DateTime.Now, com.Pole.CMD_ID, com.Result);
            }

            return true;
        }

        public bool UnResgister(PowerPole pole)
        {
            return this._poles.Remove(pole);
        }


        private void Com_Finish(object sender, EventArgs e)
        {
            //PowerPoleCom com = sender as PowerPoleCom;
            //nw_cmd_0a_device_config_get response = com.Response as nw_cmd_0a_device_config_get;
            //Console.WriteLine("{0:yyyy-MM-dd hh:mm:ss fff} {1} 获取装置配置信息完成. Result:{1}", DateTime.Now, com.Pole.CMD_ID, com.Result);
            //if (com.Result == PowerPoleComResult.Success)
            //{
            //    Console.WriteLine("设备配置信息:{0}", response);
            //}
        }

        private static PowerPoleStateMan _manager;

        public static void Init()
        {
            _manager = new PowerPoleStateMan();
        }

        /// <summary>
        /// 注册一个online处理
        /// </summary>
        /// <param name="powerPole"></param>
        /// <param name="online"></param>
        public static bool Register(PowerPole powerPole, PowerPoleState online)
        {
            return _manager.Resgister(powerPole, online);
        }

        /// <summary>
        /// 取消注册Online 处理
        /// </summary>
        /// <param name="powerPole"></param>
        /// <returns></returns>
        public static bool UnRegister(PowerPole powerPole)
        {
            return _manager.UnResgister(powerPole);
        }
    }
}
