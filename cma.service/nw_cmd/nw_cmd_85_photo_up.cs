using System;
using ResModel.nw;
using cma.service.gw_nw_cmd;

namespace cma.service.nw_cmd
{
    internal class nw_cmd_85_photo_up : nw_cmd_base
    {
        public override int Control { get { return 0x85; } }

        public override string Name { get { return "图像数据上送"; } }

        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_NO { get; set; }

        /// <summary>
        /// 预置位号
        /// </summary>
        public int PresetNo { get; set; }

        /// <summary>
        /// 包号
        /// </summary>
        public int PacNO { get; set; }

        /// <summary>
        /// 数据包内容
        /// </summary>
        public byte[] PhotoData { get; set; }

        private void deal(out string msg)
        {
            msg = string.Empty;
            gn_progress_img img;
            if ((img = gn_progress_img.GetImg(this.Pole, this.Channel_NO, this.PresetNo)) == null)
            {
                msg = "获取图片缓存失败";
                return;
            }
            img.AddPac(this.PacNO, this.PhotoData);
        }

        public override int Decode(out string msg)
        {
            if (Data == null || Data.Length < 4)
                throw new Exception(string.Format("数据域长度错误,应大于4字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            int offset = 0;
            this.Channel_NO = Data[offset++];
            this.PresetNo = Data[offset++];
            offset += nw_cmd_base.GetU16(this.Data, offset, out int pno);
            this.PacNO = pno;

            this.PhotoData = new byte[this.Data.Length - 4];
            Buffer.BlockCopy(this.Data,4,this.PhotoData,0, this.Data.Length - 4);

            this.deal(out string info);

            msg = string.Format("图像数据包上传: 通道号:{0} 预置位:{1} 包号:{2} {3}",this.Channel_NO,this.PresetNo,this.PacNO, info);
            return 0;

        }

        public override byte[] Encode(out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
