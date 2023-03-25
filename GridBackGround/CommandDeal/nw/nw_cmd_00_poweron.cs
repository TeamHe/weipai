﻿using GridBackGround.PacketAnaLysis;
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

        /// <summary>
        /// 接收到的数据帧解析
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override int Decode(out string msg)
        {
            string rsp_msg = string.Empty;
            if (this.Data.Length < 2)
                throw new Exception(string.Format("数据域长度错误,应为{0} 实际为:{1}",2, this.Data.Length));
            int version = this.Data[0] + this.Data[1]*255;
            msg = string.Format("协议版本" + version);

            this.SendCommand(out rsp_msg);
            msg += rsp_msg;
            return 0;
        }

        /// <summary>
        /// 构建响应帧
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override byte[] Encode(out string msg)
        {
            msg = string.Empty;
            return null;
        }
    }
}