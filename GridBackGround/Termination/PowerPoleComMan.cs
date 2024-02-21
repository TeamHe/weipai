using GridBackGround.CommandDeal.nw;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResModel.nw;
using ResModel;
using cma.service;

namespace GridBackGround.Termination
{
    /// <summary>
    /// 监控设备通讯管理
    /// </summary>
    public class PowerPoleComMan
    {
        private Hashtable _pole_list;

        //TODO: 
        //     1, 平台发送控制报文时自动添加到对应的设备待发送指令列表
        //     2, 收到报文后检查报文列表中是否包含回调函数， 有回调函数时调用回调函数

        // _pole_lis is the hashtable which deal for the packages need to send.
        //      Each hash item contains two PowerPole_com list,
        //      The first list is the com ready to send,
        //      And the second is the coms which are not ready

        public PowerPoleComMan()
        {
            this._pole_list = new Hashtable();
            PowerPoleManage.OnPoleAdded += PowerPoleManage_PoleAdded;
            PowerPoleManage.OnPoleRemoved += PowerPoleManage_PoleRemoved;
            nw_cmd_handle.OnPackageRecv += OnPackageRecv_nw;
        }

        /// <summary>
        /// 获取PowerPole 对应的PowerPole_com list
        /// </summary>
        /// <param name="pole"></param>
        /// <returns></returns>
        private List<PowerPoleCom>[] GetEntryLists(IPowerPole pole)
        {
            if (_pole_list == null
                || pole == null
                || _pole_list.ContainsKey(pole) == false)
                return null;
            var entry = _pole_list[pole];
            //DictionaryEntry entry = (DictionaryEntry)_pole_list[pole];
            return entry as List<PowerPoleCom>[];
        }

        /// <summary>
        //  获取PowerPol 对应的Ready PowerPoleCom list
        /// </summary>
        /// <param name="pole"></param>
        /// <returns></returns>
        private List<PowerPoleCom> GetReadyList(IPowerPole pole)
        {
            List<PowerPoleCom>[] lists = GetEntryLists(pole);
            if (lists == null) return null;
            return lists[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pole"></param>
        /// <returns>-1  当前没有可以发送的数据包
        ///          0   数据包发送失败
        ///          1   发送成功
        ///</returns>
        private  void SendNext(IPowerPole pole)
        {
            List<PowerPoleCom> list = this.GetReadyList(pole);
            //没有待发送数据包，立即返回
            if (list == null || list.Count == 0)
                return;
            //当前数据包发送失败，继续发送下一包
            while (list.Count > 0)
            {
                //发送当前数据包
                PowerPoleCom com = list[0];
                if (com.Sending == true) //当前数据包已发送，退出循环
                    break;
                if (com.Execute())      //当前数据包发送成功，退出循环
                    break;
                //数据包发送失败
                com.OnFinished -= PowerPoleCom_OnFinished;
                list.RemoveAt(0);
                com.Finish(PowerPoleComResult.Error, null); //触发发送完成事件
            }
        }

        public async void SendNextAsync(IPowerPole pole)
        {
            await Task.Run(() => SendNext(pole));
        }

        private void PowerPoleManage_PoleRemoved(object sender, PowerPole e)
        {
            List<PowerPoleCom>[] lists = GetEntryLists(e);
            foreach (PowerPoleCom com in lists[0])
            {
                com.OnFinished -= PowerPoleCom_OnFinished;
                com.Finish(PowerPoleComResult.RequestCancel, null);
            }
            foreach(PowerPoleCom com in lists[1])
            {
                com.OnFinished -= PowerPoleCom_OnFinished;
                com.Finish(PowerPoleComResult.RequestCancel, null);
            }

            lists[0].Clear();
            lists[1].Clear();
            _pole_list.Remove(e);
        }

        private void PowerPoleManage_PoleAdded(object sender, PowerPole e)
        {
            if (this._pole_list.ContainsKey(e))
                return;
            var lists = new List<PowerPoleCom>[2];
            lists[0] = new List<PowerPoleCom>();
            lists[1] = new List<PowerPoleCom>();
            this._pole_list.Add(e as IPowerPole, lists);
        }

        public bool Add(IPowerPole pole, PowerPoleCom com)
        {
            List<PowerPoleCom>[] lists = GetEntryLists(pole);
            if(lists ==null)
                return false;
            com.OnFinished += PowerPoleCom_OnFinished;
            if (com.Ready)
            {
                lists[0].Add(com);
                this.SendNextAsync(pole);
            }
            else
            {
                lists[1].Add(com);
                com.OnReady += PowerPoleCom_OnReady;
            }
            return true;
        }

        private void PowerPoleCom_OnFinished(object sender, EventArgs e)
        {
            PowerPoleCom com = (PowerPoleCom)sender;
            List<PowerPoleCom>[] lists = GetEntryLists(com.Pole);
            if (lists[0].Remove(com) == false)
                lists[1].Remove(com);
            this.SendNextAsync(com.Pole);
        }

        private void PowerPoleCom_OnReady(object sender, EventArgs e)
        {
            PowerPoleCom com = (PowerPoleCom)sender;
            List<PowerPoleCom>[] lists = GetEntryLists(com.Pole);
            if (lists != null && lists[1].Contains(com))
            {
                lists[1].Remove(com);
                lists[0].Add(com);
            }
            this.SendNextAsync(com.Pole);
        }

        private void OnPackageRecv_nw(object sender, nw_cmd_base e)
        {
            PowerPole pole = (PowerPole)sender;
            if (pole.Flag == PowerPoleFlag.NW) 
                return;
            List<PowerPoleCom> list = GetReadyList(pole);
            if (list == null || list.Count == 0)
                return;
            PowerPoleCom com = (PowerPoleCom)list[0];
            if (e.Control != com.ResponseCmd)
                return;
            com.OnFinished -= PowerPoleCom_OnFinished;
            list.RemoveAt(0);
            com.Finish(PowerPoleComResult.Success, null);
            SendNextAsync(pole);
        }

        private static PowerPoleComMan _manager;

        public static void Init()
        {
            _manager = new PowerPoleComMan();
        }

        /// <summary>
        /// 注册一个通讯请求包
        /// </summary>
        /// <param name="com"></param>
        public static bool Register(PowerPoleCom com)
        {
            return _manager.Add(com.Pole, com);
        }


        public static PowerPoleCom CreateNewCom(nw_cmd_base request)
        {
            PowerPoleCom com = new PowerPoleCom(request);
            Register(com);
            return com;
        }
        public static PowerPoleCom CreateNewCom(nw_cmd_base request, int delay)
        {
            PowerPoleCom com = new PowerPoleCom(request, delay);
            Register(com);
            return com;
        }

    }
}
