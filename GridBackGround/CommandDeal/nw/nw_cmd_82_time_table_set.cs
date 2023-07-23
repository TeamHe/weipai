using ResModel;
using System;
using System.Collections.Generic;
using ResModel.nw;
using ResModel.Image;

namespace GridBackGround.CommandDeal.nw
{
    /// <summary>
    /// 拍照时间表设置
    /// </summary>
    public class nw_cmd_82_time_table_set : nw_cmd_base
    {
        public override int Control { get { return 0x82; } }

        public override string Name { get { return "拍照时间表设置"; } }

        public string Passowrd { get; set; }

        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_No { get; set; }

        /// <summary>
        /// 拍照时间表
        /// </summary>
        public List<IPhotoTime> TimeTable { get; set; }

        public nw_cmd_82_time_table_set() { }

        public nw_cmd_82_time_table_set(IPowerPole pole):base(pole) { }

        public int Decode_PhotoTime(byte[] data, int offset, out IPhotoTime photoing_time)
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

        public int Encode_PhotoTime(byte[] data, int offset, IPhotoTime photoing_time)
        {
            data[offset++] = (byte)photoing_time.Hour;
            data[offset++] = (byte)photoing_time.Minute;
            data[offset++] = (byte)photoing_time.Presetting_No;
            return 3;
        }

        public override int Decode(out string msg)
        {
            if (Data == null || (Data.Length < 6 && Data.Length !=2))
                throw new Exception(string.Format("数据域长度错误,应为 2或大于6字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            if (Data.Length == 2)
            {
                if ((Data[0] == 0xff) && (Data[1] == 0xff))
                    msg = "设置失败，密码错误";
                else
                    msg = string.Format("设置失败, 错误代码:{0:X2}{1:X2}H", Data[0], Data[1]);
                return -1;
            }

            int offset = 0;
            offset += this.GetPassword(this.Data, offset, out string password);
            this.Channel_No = this.Data[offset++];
            int group = this.Data[offset++];

            if(this.Data.Length < (6+ group*3)) 
                throw new Exception(string.Format("数据域长度错误,应为 {0}字节 实际为:{1}",
                        6 + group * 3, this.Data.Length));

            int ret = 0;
            this.TimeTable = new List<IPhotoTime>();
            msg = string.Format("设置成功. 通道:{0} 共{1}组:", this.Channel_No, group);
            for(int i = 0; i < group; i++)
            {
                offset += this.Decode_PhotoTime(this.Data, offset, out IPhotoTime photo_time);
                this.TimeTable.Add(photo_time);
                msg += string.Format("第{0}组:{1}", i + 1, photo_time);
            }

            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            int group = 0;
            if(this.TimeTable != null) 
                group = this.TimeTable.Count;
            byte[] data = new byte[group*3+6]; 
            int offset = 0;
            offset += this.SetPassword(data, offset, this.Passowrd);
            data[offset++] = (byte)this.Channel_No;
            data[offset++] = (byte)group;
            msg = string.Format("通道:{0} 共{1}组 ", this.Channel_No, group);
            for(int i=0;i < group; i++)
            {
                offset += this.Encode_PhotoTime(data, offset, this.TimeTable[i]);
                msg += string.Format("第{0}组:{1} ", i + 1, this.TimeTable[i]);
            }
            return data;
        }
    }
}
