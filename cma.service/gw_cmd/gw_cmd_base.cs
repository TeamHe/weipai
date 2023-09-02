using ResModel.PowerPole;
using ResModel;
using System;
using Sodao.FastSocket.Server.Command;
using ResModel.gw;
using cma.service.PowerPole;

namespace cma.service.gw_cmd
{
    public abstract class gw_cmd_base
    {
        /// <summary>
        /// 关联设备handle
        /// </summary>
        public IPowerPole Pole { get; set; }

        /// <summary>
        /// 数据包中文名称
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 帧类型
        /// </summary>
        public abstract int PType { get; }

        /// <summary>
        /// 帧类型
        /// </summary>
        public int FrameType { get; set; }

        /// <summary>
        /// 帧序列号
        /// </summary>
        public int FrameNo { get; set; }

        public abstract ResModel.gw.gw_frame_type SendFrameType { get; }

        public abstract ResModel.gw.gw_frame_type RecvFrameType { get; }

        /// <summary>
        /// 接收到的数据帧数据域部分
        /// </summary>
        public byte[] Data { get; set; }

        public abstract byte[] encode(out string msg);

        public abstract int decode(byte[] data, int offset, out string msg);


        /// <summary>
        /// 获取待发送数据包handle
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public CommandInfo_gw GetSendCmd(out string msg)
        {
            msg = string.Empty;
            if (this.Pole == null)
            {
                msg = "无效的设备handle";
                return null;
            }

            CommandInfo_gw cmd = new CommandInfo_gw();
            cmd.CMD_ID = this.Pole.CMD_ID;
            cmd.Frame_No = (byte)this.FrameNo;
            cmd.Packet_Type = this.PType;
            cmd.Frame_Type = (int)this.SendFrameType;
            cmd.Data = this.encode(out msg);
            cmd.Packet_Lenth = cmd.Data.Length;
            return cmd;
        }

        /// <summary>
        /// 发送数据帧
        /// </summary>
        protected bool SendCommand(out string msg)
        {
            CommandInfo_gw cmd;
            try
            {
                string out_msg;
                if ((cmd = GetSendCmd(out out_msg)) == null)
                {
                    msg = "数据包构建失败:" + out_msg;
                    return false;
                }
                if (!this.Pole.SendSocket(this.Pole, cmd.encode(), out msg))
                {
                    msg = "数据包发送失败:" + out_msg;
                    return false;
                }
                else
                {
                    msg = out_msg;
                    return true;
                }
            }
            catch (Exception ex)
            {
                msg = "发送响应包失败:" + ex.Message;
                return false;
            }
        }

        public bool Execute()
        {
            bool res = false;
            string msg;
            try
            {
                res = this.SendCommand(out msg);
                msg = string.Format("指令发送{0}.{1}", res ? "成功" : "失败", msg);
            }
            catch (Exception ex)
            {
                msg = string.Format("指令发送{0}.{1}", "失败", ex.Message);
            }
            NewDataInfo(this.Pole, new PackageRecord(PackageRecord_RSType.send, this.Pole, this.Name, msg));
            return res;
        }





        public bool Handle()
        {
            try
            {
                this.decode(this.Data,0,out string msg);
                if (msg != null && msg.Length > 0)
                    NewDataInfo(this.Pole, new PackageRecord(PackageRecord_RSType.rec, this.Pole,
                        this.Name, msg));
            }
            catch (Exception ex)
            {
                NewDataInfo(this.Pole, new PackageRecord(PackageRecord_RSType.rec, this.Pole,
                    this.Name, string.Format("数据解析处理失败:" + ex.Message)));
            }
            return true;
        }

        public static event EventHandler<PackageRecord> OnNewDataInfo;

        protected static void NewDataInfo(IPowerPole pole, PackageRecord dataInfo)
        {
            //if (OnNewDataInfo != null)
            //{
            //    OnNewDataInfo(pole, dataInfo);
            //}

            DisPacket.NewRecord(dataInfo);

        }

    }
}
