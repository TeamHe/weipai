using GridBackGround.PacketAnaLysis;
using ResModel;
using Sodao.FastSocket.Server.Command;
using System;
using System.Collections.Generic;
using System.Reflection;
using ResModel.PowerPole;

namespace GridBackGround.CommandDeal.nw
{

    /// <summary>
    /// 南网指令处理handle
    /// </summary>
    public class nw_cmd_handle
    {

        /// <summary>
        /// 南网接收数据帧处理handle 
        /// </summary>
        public class cmd_handle
        {
            public int cid { get; set; }

            public Type type { get; set; }

            public string name { get; set; }

            public cmd_handle(int id, Type type, string name)
            {
                this.type = type;
                this.name = name;
                this.cid = id;
            }
        }

        private static List<Type> subcommands = null;
        private static List<cmd_handle> kps = null;

        public static event EventHandler<nw_cmd_base> OnPackageRecv;

        /// <summary>
        /// 通过反射获取当前指令处理类列表
        /// </summary>
        /// <returns></returns>
        internal static List<Type> GetCmdTypes()
        {
            if (subcommands == null)
            {
                subcommands = new List<Type>();
                Type baseType = typeof(nw_cmd_base);
                foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (type.IsSubclassOf(baseType))
                    {
                        subcommands.Add(type);
                    }
                }
            }
            return subcommands;
        }

        public static List<cmd_handle> GetHandles()
        {
            if (kps == null)
            {
                kps = new List<cmd_handle>();
                List<Type> types = nw_cmd_handle.GetCmdTypes();
                foreach (Type type in types)
                {
                    nw_cmd_base obj = (nw_cmd_base)Activator.CreateInstance(type);
                    kps.Add(new cmd_handle(obj.Control, type, obj.Name));
                }
            }
            return kps;
        }

        /// <summary>
        /// 查找特定指令字对对应的帧处理类
        /// </summary>
        /// <param name="ctl_id"></param>
        /// <returns></returns>
        internal static cmd_handle GetCmdHandle(int ctl_id)
        {
            return GetHandles().Find(ctl => ctl.cid == ctl_id);
        }

        static void _onPackageRecv(IPowerPole pole, nw_cmd_base cmd)
        {
            if(OnPackageRecv != null)
                OnPackageRecv(pole, cmd);
        }

        /// <summary>
        /// 处理接收到的数据帧
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="command"></param>
        public static void Deal(IPowerPole pole, CommandInfo_nw command)
        {
            cmd_handle handle = GetCmdHandle(command.PackageType);
            if (handle == null)
            {
                DisPacket.NewRecord(new DataInfo(DataInfoState.rec, pole,
                    "未知协议", string.Format("不支持的控制字{0:X2}H", command.PackageType)));
            }
            else
            {
                try
                {
                    nw_cmd_base ctl = (nw_cmd_base)Activator.CreateInstance(handle.type);
                    ctl.Pole = pole;
                    ctl.Data = command.Data;
                    ctl.Handle();
                    _onPackageRecv(pole, ctl);
                }
                catch (Exception ex)
                {
                    DisPacket.NewRecord(new DataInfo(DataInfoState.rec, pole,
                        handle.name, string.Format("数据解析处理失败:" + ex.Message )));
                }
            }
        }

        /// <summary>
        /// 打印信息到纪录列表
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="ctl"></param>
        /// <param name="msg"></param>
        public static void LogInfo(IPowerPole pole, nw_cmd_base ctl,string msg)
        {
            DisPacket.NewRecord(new DataInfo(DataInfoState.rec, pole,
                                ctl!=null?ctl.Name: "未知协议", msg));
        }

        /// <summary>
        /// 发送响应帧
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="cmd"></param>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Response(IPowerPole pole, CommandInfo_nw cmd, byte[] data,out string msg)
        {
            int code=0;
            if(pole == null)
            {
                msg = "发送响应包失败:无效的设备handle";
                return false;
            }
            if(cmd == null)
            {
                msg = "发送响应包失败: 无效的数据包handle";
                return false;
            }

            CommandInfo_nw rsp = new CommandInfo_nw();
            rsp.CMD_ID = cmd.CMD_ID; 
            rsp.PackageType = cmd.PackageType; 
            rsp.Data = data;
            rsp.encode();
            bool res = PackeDeal.SendSocket(pole, rsp.Pakcet, out msg, out code);
            if (res == false)
                msg = "发送响应包失败:" + msg;
            return res;
        }

    }

}
