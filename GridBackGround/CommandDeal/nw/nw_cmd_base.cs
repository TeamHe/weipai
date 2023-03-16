using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using GridBackGround.PacketAnaLysis;
using GridBackGround.Termination;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

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
        /// 接收到的数据帧数据域部分
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 关联设备handle
        /// </summary>
        public IPowerPole Pole { get; set; }

        /// <summary>
        /// 关联南网接收数据帧Handle
        /// </summary>
        //public CommandInfo_nw cmd_rec { get; set; }

        /// <summary>
        /// 关联数据帧发送帧handle
        /// </summary>
        //public CommandInfo_nw cmd_send { get; set; }

        public nw_cmd_base()
        {

        }

        public nw_cmd_base(IPowerPole pole)
        {
            this.Pole = pole;
        }


        public nw_cmd_base(IPowerPole pole, byte[]  data)
        {
            this.Pole = pole;
            this.Data = data;
        }


        /// <summary>
        /// 数据帧解析处理虚函数
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="cmd"></param>
        /// <param name="msg"></param>
        public abstract int Decode(out string msg);
        //{
        //    msg = null;
        //    return 0;
        //}

        public abstract byte[] Encode(out string msg);
        //{
        //    msg = null;
        //    return null;
        //}

        /// <summary>
        /// 获取待发送数据包handle
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public CommandInfo_nw GetSendCmd(out string msg)
        {
            msg = string.Empty;
            if(this.Pole == null)
            {
                msg = "无效的设备handle";
                return null;
            }

            CommandInfo_nw cmd = new CommandInfo_nw();
            cmd.CMD_ID = this.Pole.CMD_ID;
            cmd.PackageType = this.Control;
            cmd.Data = this.Encode(out msg);
            cmd.encode();
            return cmd;             
        }


        /// <summary>
        /// 从报文缓冲区中获取时间
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="time"></param>
        /// <returns>时间数据占用字节数</returns>
        internal int GetDateTime(byte[] data,int offset,out DateTime time)
        {
            time = new DateTime(Data[offset + 0] + 2000, 
                                Data[offset + 1], 
                                Data[offset + 2], 
                                Data[offset + 3], 
                                Data[offset + 4], 
                                Data[offset + 5]);
            return 6;
        }

        /// <summary>
        /// 将时间添加到报文缓冲区
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="time"></param>
        /// <returns>时间数据占用字节数</returns>
        internal int SetDateTime(byte[] data,int offset,DateTime time)
        {
            data[offset + 0] = (byte)(time.Year - 2000);
            data[offset + 1] = (byte) time.Month;
            data[offset + 2] = (byte) time.Day;
            data[offset + 3] = (byte) time.Hour;
            data[offset + 4] = (byte) time.Minute;
            data[offset + 5] = (byte) time.Second;
            return 6;
        }

        internal int GetU16(byte[] data,int offset,out int value)
        {
            value = data[offset + 0] + data[offset + 1] * 256;
            return 2;
        }

        internal int GetPhoneNumber(byte[] data,int offset,out string phone)
        {
            phone = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                (char)((data[offset + 0] & 0x0f) + 0x30),
                (char)((data[offset + 1] / 0x10) + 0x30),
                (char)((data[offset + 1] & 0x0f) + 0x30),
                (char)((data[offset + 2] / 0x10) + 0x30),
                (char)((data[offset + 2] & 0x0f) + 0x30),
                (char)((data[offset + 3] / 0x10) + 0x30),
                (char)((data[offset + 3] & 0x0f) + 0x30),
                (char)((data[offset + 4] / 0x10) + 0x30),
                (char)((data[offset + 4] & 0x0f) + 0x30),
                (char)((data[offset + 5] / 0x10) + 0x30),
                (char)((data[offset + 5] & 0x0f) + 0x30));
            return 6;
        }

        /// <summary>
        /// 发送数据帧
        /// </summary>
        public bool SendCommand(out string msg)
        {
            CommandInfo_nw cmd;
            try
            {
                if((cmd = GetSendCmd(out msg)) == null)
                {
                    msg = "数据包构建失败:" + msg;
                    return false;
                }
                if(!PackeDeal.SendSocket(this.Pole, cmd.Pakcet, out msg))
                {
                    msg = "数据包发送失败:" + msg;
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                msg = "发送响应包失败:" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 接收到数据帧处理
        /// </summary>
        public void Handle()
        {
            try
            {
                string msg =null;
                this.Decode(out msg);
                DisPacket.NewRecord(new DataInfo(DataRecSendState.rec, this.Pole,
                    this.Name, msg));
            }
            catch (Exception ex)
            {
                DisPacket.NewRecord(new DataInfo(DataRecSendState.rec, this.Pole,
                    this.Name, string.Format("数据解析处理失败:" + ex.Message)));
            }

        }
    }
}
