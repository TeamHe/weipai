using GridBackGround.Termination;
using Sodao.FastSocket.Server.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBackGround.CommandDeal.nw
{
    /// <summary>
    /// 南网接收数据帧处理基类
    /// </summary>
    public abstract class nw_cmd_base
    {
        /// <summary>
        /// 控制字ID
        /// </summary>
        public abstract int Control { get; }

        /// <summary>
        /// 指令名称
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 数据帧解析处理虚函数
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="cmd"></param>
        /// <param name="msg"></param>
        public abstract void Handle(IPowerPole pole, CommandInfo_nw cmd, out string msg);
    }
}
