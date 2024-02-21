using ResModel;
using ResModel.PowerPole;
using Sodao.FastSocket.Server.Command;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace cma.service.gw_cmd
{
    public class cmd_type
    {
        public int cid { get; set; }

        public Type type { get; set; }

        public string name { get; set; }

        public cmd_type(int id, Type type, string name)
        {
            this.type = type;
            this.name = name;
            this.cid = id;
        }
    }

    public class gw_cmd_handle
    {
        public Type baseType { get; set; }

        /// <summary>
        /// 扩展类列表
        /// </summary>
        public List<Type> sub_classes { get; set; }    

        public List<cmd_type> cmd_types { get;set; }



        public gw_cmd_handle(Type baseType)
        {
            this.baseType = baseType;
        }

        public gw_cmd_handle():this(typeof(gw_cmd_base))
        {
           
        }


        public List<Type> GetSubClasses()
        {
            List<Type> cmds = new List<Type>(); 
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsAbstract)
                    continue;
                if (type.IsSubclassOf(this.baseType))
                {
                    cmds.Add(type);
                }
            }
            return cmds;
        }

        public List<cmd_type> GetHandles()
        {
            if (this.sub_classes == null)
                this.sub_classes = this.GetSubClasses();

            List<cmd_type> cmds = new List<cmd_type>();
            foreach(Type type in this.sub_classes)
            {
                gw_cmd_base obj = Activator.CreateInstance(type) as gw_cmd_base;
                cmds.Add(new cmd_type(obj.PType, type, obj.Name));
            }
            return cmds;
        }

        public cmd_type GetCmdHandle(int ptype)
        {
            if(this.cmd_types == null)
                this.cmd_types = this.GetHandles();
            return this.cmd_types.Find(ctl => ctl.cid == ptype);
        }

    }

    public class gw_cmd_handler
    {
        public static gw_cmd_handle cmd_handle = null;

        public static event EventHandler<gw_cmd_base> OnPackageRecv;

        public static void gw_cmd_handle_init()
        {
            if (cmd_handle == null)
            {
                cmd_handle = new gw_cmd_handle();
                cmd_handle.sub_classes = cmd_handle.GetSubClasses();
                cmd_handle.GetHandles();
            }
        }



        private static void _onPackageRecv(IPowerPole pole, gw_cmd_base cmd)
        {
            if (OnPackageRecv != null)
                OnPackageRecv(pole, cmd);
        }

        /// <summary>
        /// 打印信息到纪录列表
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="ctl"></param>
        /// <param name="msg"></param>
        public static void LogInfo(IPowerPole pole, gw_cmd_base ctl, string msg)
        {
            DisPacket.NewRecord(new PackageRecord(PackageRecord_RSType.rec, pole,
                                ctl != null ? ctl.Name : "未知协议", msg));
        }



        /// <summary>
        /// 处理接收到的数据帧
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="command"></param>
        public static bool Deal(IPowerPole pole, CommandInfo_gw command)
        {
            cmd_type handle = null;
            if(cmd_handle == null)
                gw_cmd_handle_init();

            if ((handle = cmd_handle.GetCmdHandle(command.Packet_Type)) == null)
            {
                //DisPacket.NewRecord(new DataInfo(DataInfoState.rec, pole,
                //    "未知协议", string.Format("不支持的控制字{0:X2}H", command.Packet_Type)));
                return false;
            }
            else
            {
                try
                {
                    gw_cmd_base ctl = (gw_cmd_base)Activator.CreateInstance(handle.type);
                    ctl.Pole = pole;
                    ctl.FrameNo = (int)command.Frame_No;
                    ctl.FrameType = command.Frame_Type;
                    ctl.Data = command.Data;
                    ctl.Handle();
                    _onPackageRecv(pole, ctl);
                }
                catch (Exception ex)
                {
                    DisPacket.NewRecord(new PackageRecord(PackageRecord_RSType.rec, pole,
                    handle.name, string.Format("数据解析处理失败:" + ex.Message)));
                }
                return true;    
            }
        }
}
}
