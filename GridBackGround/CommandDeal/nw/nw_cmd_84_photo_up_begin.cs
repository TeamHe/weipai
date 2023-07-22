using GridBackGround.CommandDeal.Image;
using ResModel;
using ResModel.CollectData;
using System;
using ResModel.nw;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_cmd_84_photo_up_begin : nw_cmd_base
    {
        public override int Control { get { return 0x84; } }

        public override string Name { get { return "采集终端请求上送照片"; } }

        /// <summary>
        /// 拍照时间
        /// </summary>
        public DateTime photo_time { get; set; }

        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_NO { get; set; }

        /// <summary>
        /// 预置位号
        /// </summary>
        public int PresetNo { get; set; }

        /// <summary>
        /// 报数
        /// </summary>
        public int PacNum { get; set; }

        public nw_cmd_84_photo_up_begin() { }

        public nw_cmd_84_photo_up_begin(IPowerPole pole) : base(pole) { }


        public override int Decode(out string msg)
        {
            if (Data == null || Data.Length < 10)
                throw new Exception(string.Format("数据域长度错误,应为10字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            int offset = 0;
            offset += this.GetDateTime(this.Data, offset, out DateTime time);
            this.Channel_NO = Data[offset++];
            this.PresetNo = Data[offset++];
            offset += this.GetU16(this.Data, offset, out int pnum);

            this.photo_time = time;
            this.PacNum = pnum;

            ///将图片添加到正在上传图片列表
            Photo_man photo = new Photo_man(this.Pole, this.Channel_NO, this.PresetNo);
            Picture picture = photo.Picture_StartUp(this.PacNum, out string start_msg);
            
            msg = string.Format("装置请求照片. 拍照时间:{0} 通道号:{1} 预置位号:{2} 总包数:{3}",
                this.photo_time,this.Channel_NO,this.PresetNo,this.PacNum);

            msg += start_msg;
            if(picture != null)     //图片信息添加到缓存成功才会发送响应包
            {
                picture.Maintime = time;
                this.SendCommand(out string send_msg);
            }
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            msg = string.Empty;
            return this.Data;
        }
    }
}
