using ResModel.gw;
using System;
using System.Collections.Generic;
using System.Text;
using cma.service.gw_nw_cmd;

namespace cma.service.gw_cmd
{
    public class gw_cmd_img_pic_end : gw_cmd_img_base
    {
        public override int ValuesLength { get { return 0; } }

        public override string Name { get { return "远程图像结束包"; } }

        public override int PType { get { return 0xce; } }

        public int ChNO { get; set; }

        public int Preset {  get; set; }

        public DateTime Time { get; set; }

        private void SendBubao(List<int> pacs)
        {
            gw_cmd_img_bubao cmd = new gw_cmd_img_bubao(this.Pole)
            {
                ChNO = this.ChNO,
                Preset = this.Preset,
                Pnos = pacs,
            };
            cmd.Execute();
        }

        private bool  deal(out string msg)
        {
            msg = string.Empty;
            gn_progress_img img;
            if ((img = gn_progress_img.GetImg(this.Pole, this.ChNO, this.Preset)) == null)
            {
                msg = "获取图片缓存失败";
                return false;
            }

            List<int> pacs = img.GetRemainPacs();
            this.SendBubao(pacs);
            if (pacs == null || pacs.Count == 0)
            {
                img.Time = Time;
                img.Finish();
            }
            return true;
        }


        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            if (data.Length - offset < 6)
                throw new Exception("数据缓冲区太小");
            int start = offset;
            this.ChNO  = data[offset++];
            this.Preset = data[offset++];
            offset += gw_coding.GetTime(data, offset, out DateTime time);
            this.Time = time;

            this.deal(out string info);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("通道号:{0} ", this.ChNO);
            sb.AppendFormat("预置位号:{0} ", this.Preset);
            sb.AppendFormat("时间:{0} " ,this.Time.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append(info);
            msg  = sb.ToString();
            return offset - start;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
