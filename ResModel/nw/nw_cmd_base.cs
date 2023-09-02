using ResModel;
using Sodao.FastSocket.Server.Command;
using System;
using System.Net;
using System.Text;
using ResModel.PowerPole;

namespace ResModel.nw
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
        protected int GetDateTime(byte[] data,int offset,out DateTime time)
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
        protected int SetDateTime(byte[] data,int offset,DateTime time)
        {
            data[offset + 0] = (byte)(time.Year - 2000);
            data[offset + 1] = (byte) time.Month;
            data[offset + 2] = (byte) time.Day;
            data[offset + 3] = (byte) time.Hour;
            data[offset + 4] = (byte) time.Minute;
            data[offset + 5] = (byte) time.Second;
            return 6;
        }

        /// <summary>
        /// 获取大端模式无符号16位整形
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetU16(byte[] data, int offset, out int value)
        {
            value = data[offset + 0] * 256 + data[offset + 1];
            return 2;
        }

        /// <summary>
        /// 设置大端模式无符号整形数组
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int SetU16(byte[] data, int offset, int value)
        {
            data[offset + 0] = (byte)(value / 256);
            data[offset + 1] = (byte)(value % 256);
            return 2;
        }

        public static int GetU32(byte[] data, int offset, out uint value)
        {
            value = (uint)data[offset];
            value = (value << 8) + data[offset + 1] ;
            value = (value << 8) + data[offset + 2] ;
            value = (value << 8) + data[offset + 3] ;
            return 4;
        }

        public static int SetU32(byte[] data, int offset, uint value)
        {
            data[offset++] = (byte)((value & 0xff000000) >> 24);
            data[offset++] = (byte)((value & 0x00ff0000) >> 16);
            data[offset++] = (byte)((value & 0x0000ff00) >> 8);
            data[offset++] = (byte)((value & 0x000000ff));
            return 4;
        }

        /// <summary>
        /// 获取最高位位符号位的16位整形
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetS16(byte[] data, int offset, out int value)
        {
            value = data[offset + 1] + (data[offset + 0]&0x7f) * 256;
            if((data[offset + 0] & 0x80)>0)
                value = -value; 
            return 2;
        }

        /// <summary>
        /// 设置最高位为符号位的16位整形
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <returns></returns>
       public static int SetS16(byte[] data, int offset, int value)
        {
            bool sign = false;
            if(value < 0)
            {
                sign = true;
                value = -value;
            }
            data[offset + 0] = (byte)((value / 256)&0x7f);
            data[offset + 1] = (byte)(value % 256);
            if(sign)
                data[offset + 1] |= 0x80;
            return 2;
        }


        protected int GetPhoneNumber(byte[] data,int offset,out string phone)
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

        protected int SetPhoneNumber(byte[] data, int offset, string phone)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(phone, @"^1[3-9]\d{9}$") == false)
                throw new Exception("请输入正确的11位电话号码");
            byte[] b_phone = Encoding.ASCII.GetBytes(phone);
            data[offset + 0] = (byte)(0xf0 + (b_phone[0] - '0'));
            data[offset + 1] = (byte)(((b_phone[1] - '0') << 4) + (b_phone[2] - '0'));
            data[offset + 2] = (byte)(((b_phone[3] - '0') << 4) + (b_phone[4] - '0'));
            data[offset + 3] = (byte)(((b_phone[5] - '0') << 4) + (b_phone[6] - '0'));
            data[offset + 4] = (byte)(((b_phone[7] - '0') << 4) + (b_phone[8] - '0'));
            data[offset + 5] = (byte)(((b_phone[9] - '0') << 4) + (b_phone[10] - '0'));
            return 6;
        }

        protected int SetPassword(byte[] data,int offset,string password)
        {
            if(password ==null)
                throw new ArgumentNullException("password");
            byte[] b_password = Encoding.ASCII.GetBytes(password);
            Buffer.BlockCopy(b_password,0,data,offset,b_password.Length>=4?4:b_password.Length);
            return 4;
        }

        protected int GetPassword(byte[] data,int offset,out string password)
        {
            password = Encoding.ASCII.GetString(data, offset, 4);
            return 4;
        }

        protected int SetIPAddress(byte[] data,int offset,IPAddress address)
        {
            if (address == null) throw new ArgumentNullException("IP地址");

            byte[] b_ip = address.GetAddressBytes();
            Buffer.BlockCopy(b_ip,0,data,offset,b_ip.Length>=4?4:b_ip.Length);
            return 4;
        }

        protected int GetIPAddress(byte[] data, int offset, out IPAddress address)
        {
            byte[] b_ip = new byte[4];
            Buffer.BlockCopy(data, offset, b_ip, 0, 4);
            address = new IPAddress(b_ip);
            return 4;
        }


        public byte[] ResponseData(int frameflag)
        {
            byte[] data = new byte[3];
            data[0] = (byte)frameflag;
            data[1] = 0xaa;
            data[2] = 0x55;
            return data;
        }

        /// <summary>
        /// 发送数据帧
        /// </summary>
        protected bool SendCommand(out string msg)
        {
            CommandInfo_nw cmd;
            try
            {
                string out_msg;
                if((cmd = GetSendCmd(out out_msg)) == null)
                {
                    msg = "数据包构建失败:" + out_msg;
                    return false;
                }
                if(!this.Pole.SendSocket(this.Pole, cmd.Pakcet, out msg))
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

        public bool Execute ()
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

        /// <summary>
        /// 接收到数据帧处理
        /// </summary>
        public void Handle()
        {
            try
            {
                this.Decode(out string msg);
                if(msg != null && msg.Length > 0)
                    NewDataInfo(this.Pole, new PackageRecord(PackageRecord_RSType.rec, this.Pole,
                        this.Name, msg));
            }
            catch (Exception ex)
            {
                NewDataInfo(this.Pole, new PackageRecord(PackageRecord_RSType.rec, this.Pole,
                    this.Name, string.Format("数据解析处理失败:" + ex.Message)));
            }
        }


        public static event EventHandler<PackageRecord> OnNewDataInfo;

        protected static void NewDataInfo(IPowerPole pole, PackageRecord dataInfo)
        {
            if(OnNewDataInfo != null)
            {
                OnNewDataInfo(pole, dataInfo);
            }
        }
    }
}
