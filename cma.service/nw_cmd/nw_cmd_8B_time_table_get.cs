using ResModel;
using System;
using System.Collections.Generic;
using ResModel.nw;
using ResModel.Image;

namespace cma.service.nw_cmd
{

    /// <summary>
    /// 查询拍照时间表
    /// </summary>
    public class nw_cmd_8B_time_table_get : nw_cmd_base
    {
        public override int Control { get { return 0x8b; } }

        public override string Name { get { return "查询拍照时间表"; } }

        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_No { get; set; }

        /// <summary>
        /// 拍照时间表
        /// </summary>
        public List<PhotoTime> TimeTable { get; set; }

        public nw_cmd_8B_time_table_get() { }

        public nw_cmd_8B_time_table_get(IPowerPole pole) : base(pole) { }

        public int Decode_PhotoTime(byte[] data, int offset, out PhotoTime photoing_time)
        {
            photoing_time = null;
            if (data.Length - offset < 3)
                return -1;
            int no = offset;
            int hour = data[no++];
            int minute = data[no++];
            int preset = data[no++];
            photoing_time = new PhotoTime(hour, minute, preset);
            return no - offset;
        }

        public int Encode_PhotoTime(byte[] data, int offset, PhotoTime photoing_time)
        {
            data[offset++] = (byte)photoing_time.Hour;
            data[offset++] = (byte)photoing_time.Minute;
            data[offset++] = (byte)photoing_time.Presetting_No;
            return 3;
        }

        public override int Decode(out string msg)
        {
            if (Data == null || Data.Length < 2 )
                throw new Exception(string.Format("数据域长度错误,应为 大于等于2字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            int offset = 0;
            this.Channel_No = this.Data[offset++];
            int group = this.Data[offset++];

            if (this.Data.Length < (2 + group * 3))
                throw new Exception(string.Format("数据域长度错误,应为 {0}字节 实际为:{1}",
                        6 + group * 3, this.Data.Length));

            this.TimeTable = new List<PhotoTime>();
            msg = string.Format("查询成功. 通道:{0} 共{1}组:", this.Channel_No, group);
            for (int i = 0; i < group; i++)
            {
                offset += this.Decode_PhotoTime(this.Data, offset, out PhotoTime photo_time);
                this.TimeTable.Add(photo_time);
                msg += string.Format("第{0}组:{1} ", i + 1, photo_time);
            }
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            byte[] data = new byte[1];
            msg = string.Empty;
            data[0] = (byte)this.Channel_No;
            return data;
        }
    }

}
