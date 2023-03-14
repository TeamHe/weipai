﻿using GridBackGround.PacketAnaLysis;
using GridBackGround.Termination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBackGround.CommandDeal.nw
{

    /// <summary>
    /// 
    /// </summary>
    internal class nw_cmd_01_timing : nw_cmd_base
    {
        public override int Control { get { return 0x01; } }

        public override string Name { get { return "校时"; } }

        public DateTime Time { get; set; }

        /// <summary>
        /// 接收到的数据包解析
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override int Decode(out string msg)
        {
            msg = null;

            //主站收到对时命令响应包
            if(this.Data != null && this.Data.Length == 0x06)
            {
                Time = new DateTime(Data[0] + 2000, Data[1], Data[2], Data[3], Data[4], Data[5]);
                msg = string.Format("对时成功,装置时间:{0:yyyy-MM-yy HH:mm:ss}", this.Time);
            }
            else
            { //装置请求校时
                this.SendCommand(out msg);
                msg = string.Format("装置请求校时, 当前时间:{0:yyyy-MM-yy HH:mm:ss}", this.Time);
            }
            return 0;
        }

        /// <summary>
        /// 校时数据包，数据域部分构建
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override byte[] Encode(out string msg)
        {
            msg = null;
            if(this.Time ==null || this.Time.Year == 1)
                this.Time = DateTime.Now;
            byte[] data = new byte[0x06];
            data[0] = (byte)(Time.Year - 2000);
            data[1] = (byte)Time.Month;
            data[2] = (byte)Time.Day;
            data[3] = (byte)Time.Hour;
            data[4] = (byte)Time.Minute;
            data[5] = (byte)Time.Second;

            return data;
        }

        /// <summary>
        /// 发送校时指令
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool Timing()
        {
            string msg = null;
            bool res = false;
            try
            {
                this.Decode(out msg);
                if ((res = this.SendCommand(out msg)))
                    msg = string.Format("成功发送校时指令,当前时间: {0:yyyy-MM-yy HH:mm:ss}", this.Time);
                else
                    msg = "校时指令发送失败:" + msg;
            }
            catch (Exception ex)
            {
                msg = "校时指令发送失败:" + ex.Message;
            }
            DisPacket.NewRecord(new DataInfo(DataRecSendState.send, this.Pole, this.Name, msg));
            return res;
        }
    }
}
