using System;
using System.Text;
using cma.service.gw_nw_cmd;

namespace cma.service.gw_cmd
{
    public class gw_cmd_img_pic_start : gw_cmd_img_base
    {
        public override int ValuesLength { get { return 4; } }

        public override string Name { get { return "装置请求上送图片"; } }

        public override int PType {  get { return 0xcc; } }

        public int ChNO { get; set; }
        
        public int Preset {  get; set; }

        public int PNum {  get; set; }

        private bool deal(out string msg)
        {
            msg = string.Empty;
            gn_progress_img img;
            if ((img = gn_progress_img.GetImg(this.Pole, this.ChNO, this.Preset,true)) == null)
            {
                msg = "获取图片缓存失败";
                return false;
            }
            img.Pnum = this.PNum;
            return true;
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            if (data.Length - offset < 4)
                throw new Exception("数据缓冲区太小");

            int start = offset;
            this.ChNO = data[offset++];
            this.Preset = data[offset++];
            this.PNum = data[offset++] * 0x100 + data[offset++] ;

            var ret = this.deal(out string info);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("通道号:{0} ", this.ChNO);
            sb.AppendFormat("预置位号:{0} ", this.Preset);
            sb.AppendFormat("总包数:{0} ", this.PNum) ;
            sb.Append(info);

            msg = sb.ToString();

            if(ret == true)
                this.Execute();
            return offset - start;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            msg = string.Empty;
            int start = offset;
            data[offset++] = (byte)this.ChNO;
            data[offset++] = (byte)this.Preset;
            data[offset++] = (byte)((this.PNum & 0xff00)>>8);
            data[offset++] = (byte)(this.PNum & 0xff);
            return offset - start;
        }
    }
}
