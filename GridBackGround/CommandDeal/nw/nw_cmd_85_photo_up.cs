using GridBackGround.CommandDeal.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridBackGround.CommandDeal.nw
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

        public override int Decode(out string msg)
        {
            if (Data == null || Data.Length < 4)
                throw new Exception(string.Format("数据域长度错误,应大于4字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            int offset = 0;
            this.Channel_NO = Data[offset++];
            this.PresetNo = Data[offset++];
            offset += this.GetU16(this.Data, offset, out int pno);
            this.PacNO = pno;

            this.PhotoData = new byte[this.Data.Length - 4];
            Buffer.BlockCopy(this.Data,4,this.PhotoData,0, this.Data.Length - 4);

            ///TODO: 
            ///     将图像数据添加到图像缓存
            Photo_man photo = new Photo_man(this.Pole, Channel_NO, this.PresetNo);
            photo.PictureData(this.PacNO, PhotoData,  out string msg_photo);

            msg = string.Format("图像数据包上传: 通道号:{0} 预置位:{1} 包号:{2}",this.Channel_NO,this.PresetNo,this.PacNO);
            msg += msg_photo;
            return 0;

        }

        public override byte[] Encode(out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
