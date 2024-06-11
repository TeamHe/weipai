using System;
using System.Collections.Generic;
using ResModel.nw;
using cma.service.gw_nw_cmd;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_cmd_86_photo_up_end : nw_cmd_base
    {
        public override int Control { get { return 0x86; } }

        public override string Name { get { return "图像数据上送结束标记"; } }

        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_NO { get; set; }

        /// <summary>
        /// 预置位号
        /// </summary>
        public int PresetNo { get; set; }

        private void SendBubao(List<int> pacs)
        {
            nw_cmd_87_photo_up_bubao cmd = new nw_cmd_87_photo_up_bubao(Pole)
            {
                Channel_NO = this.Channel_NO,
                PresetNo = this.PresetNo,
                List_Pac = pacs,
            };
            cmd.Execute();
        }

        private void deal(out string msg)
        {
            msg = string.Empty;
            gn_progress_img img;
            if ((img = gn_progress_img.GetImg(this.Pole, this.Channel_NO, this.PresetNo)) == null)
            {
                msg = "获取图片缓存失败";
                return;
            }

            List<int> pacs = img.GetRemainPacs();
            this.SendBubao(pacs);
            if (pacs == null || pacs.Count == 0)
                img.Finish();
        }

        public override int Decode(out string msg)
        {
            if (Data == null || Data.Length < 2)
                throw new Exception(string.Format("数据域长度错误,应为2字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            int offset = 0;
            this.Channel_NO = Data[offset++];
            this.PresetNo = Data[offset++];

            this.deal(out string info);
            msg = string.Format("图像上传结束. 通道号:{0} 预置位号:{1} {2}",this.Channel_NO,this.PresetNo,info);
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
