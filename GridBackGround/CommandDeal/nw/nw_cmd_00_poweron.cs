using GridBackGround.PacketAnaLysis;
using GridBackGround.Termination;
using Sodao.FastSocket.Server.Command;
using System;

namespace GridBackGround.CommandDeal.nw
{
    /// <summary>
    /// 南网00指令字，开机联络信息帧处理
    /// </summary>
    internal class nw_cmd_00_poweron : nw_cmd_base
    {
        /// <summary>
        /// 控制字ID
        /// </summary>
        public override int Control { get { return 0x00; } }

        public override string Name { get { return "开机联络信息"; } }

        public override void Handle(IPowerPole pole, CommandInfo_nw cmd,out string msg)
        {
            if (cmd.Data.Length < 2)
                throw new Exception(string.Format("数据域长度错误,应为{0} 实际为:{1}",2, cmd.Data.Length));
            int version = cmd.Data[0] + cmd.Data[1]*255;
            msg = string.Format("协议版本" + version);

            nw_cmd_handle.Response(pole, cmd, null, out string rsp_msg);
            msg += rsp_msg;
        }
    }
}
