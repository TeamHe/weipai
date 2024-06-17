using ResModel;
using System;
using ResModel.nw;
using cma.service.gw_nw_cmd;

namespace cma.service.nw_cmd
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

        private bool deal(out string msg)
        {
            msg = string.Empty;
            gn_progress_img img;
            if ((img = gn_progress_img.GetImg(this.Pole, this.Channel_NO, this.PresetNo, true)) == null)
            {
                msg = "获取图片缓存失败";
                return false;
            }
            img.Pnum = this.PacNum;
            img.Time = this.photo_time;
            return true;
        }

        public override int Decode(out string msg)
        {
            if (Data == null || Data.Length < 10)
                throw new Exception(string.Format("数据域长度错误,应为10字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            int offset = 0;
            offset += this.GetDateTime(this.Data, offset, out DateTime time);
            this.Channel_NO = Data[offset++];
            this.PresetNo = Data[offset++];
            offset += nw_cmd_base.GetU16(this.Data, offset, out int pnum);

            this.photo_time = time;
            this.PacNum = pnum;

            ///将图片添加到正在上传图片列表
            if (this.deal(out string info))
                this.SendCommand(out string send_msg);

            msg = string.Format("装置请求照片. 拍照时间:{0} 通道号:{1} 预置位号:{2} 总包数:{3} {4}",
                this.photo_time,this.Channel_NO,this.PresetNo,this.PacNum,info);

            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            msg = string.Empty;
            return this.Data;
        }
    }
}
