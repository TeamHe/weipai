using ResModel;
using System;

namespace GridBackGround.CommandDeal.nw
{

    /// <summary>
    /// 
    /// </summary>
    public class nw_cmd_01_timing : nw_cmd_base
    {
        public nw_cmd_01_timing() 
        {
            
        }

        public nw_cmd_01_timing(IPowerPole pole) : base(pole)
        {

        }

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
            msg = string.Format("当前系统时间: {0:G}", this.Time);
            return data;
        }
    }
}
