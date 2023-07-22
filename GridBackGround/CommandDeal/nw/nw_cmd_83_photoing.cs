using ResModel;
using System;
using ResModel.nw;

namespace GridBackGround.CommandDeal.nw
{

    /// <summary>
    /// 主站请求拍摄照片
    /// </summary>
    public class nw_cmd_83_photoing : nw_cmd_base
    {
        public override int Control { get { return 0x83; } }

        public override string Name { get { return "主站请求拍摄照片"; } }

        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_No { get; set; }

        /// <summary>
        /// 预置位号
        /// </summary>
        public int Preset_No { get; set; }

        public nw_cmd_83_photoing() { }

        public nw_cmd_83_photoing(IPowerPole pole) : base(pole) { }


        public override int Decode(out string msg)
        {
            if (Data == null || Data.Length < 2)
                throw new Exception(string.Format("数据域长度错误,应为2字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            int offset = 0;
            this.Channel_No = this.Data[offset++];
            this.Preset_No = this.Data[offset++];
            msg = string.Format("拍照成功. 通道号:{0} 预置位号:{1}",
                this.Channel_No,this.Preset_No);
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            byte[] data = new byte[2];
            data[0] = (byte)this.Channel_No;
            data[1] = (byte)this.Preset_No;
            msg = string.Format("请求拍照. 通道号:{0} 预置位号:{1}",
                this.Channel_No, this.Preset_No);
            return data;
        }
    }
}
