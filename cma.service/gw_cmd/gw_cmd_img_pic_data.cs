using ResModel.gw;
using System;
using System.Text;
using cma.service.gw_nw_cmd;

namespace cma.service.gw_cmd
{
    public class gw_cmd_img_pic_data : gw_cmd_img_base
    {
        public override int ValuesLength { get { return 0; } }

        public override string Name { get { return "远程图像数据报"; } }

        public override int PType {  get { return 0xcd; } }

        public int ChNO { get; set; }

        public int Preset { get; set; } 

        public byte[] PData { get; set; }

        public int PNO {  get; set; }

        public int PNUM {  get; set; }

        private bool deal(out string msg)
        {
            msg = string.Empty;
            gn_progress_img img;
            if ((img = gn_progress_img.GetImg(this.Pole, this.ChNO, this.Preset)) == null)
            {
                msg = "获取图片缓存失败";
                return false;
            }
            img.AddPac(this.PNO, this.PData);
            return true;
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            if (data.Length - offset < 6)
                throw new Exception("数据缓冲区太小");

            int start = offset;
            this.ChNO = data[offset++];
            this.Preset = data[offset++];

            offset += gw_coding.GetU16(data, offset,out int pnum);
            this.PNUM = pnum;

            offset += gw_coding.GetU16(data, offset, out int pno);
            this.PNO = pno;

            int plen = this.Data.Length - 6;
            this.PData  = new byte[plen];
            Buffer.BlockCopy(data,offset, PData,0,plen);
            offset += plen;

            this.deal(out string info) ;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("通道号:{0} ", this.ChNO);
            sb.AppendFormat("预置位号:{0} ", this.Preset);
            sb.AppendFormat("总包数:{0} ",this.PNUM);
            sb.AppendFormat("子包包号:{0} ", this.PNO);
            sb.Append(info);
            msg = sb.ToString();
            return offset - start;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
