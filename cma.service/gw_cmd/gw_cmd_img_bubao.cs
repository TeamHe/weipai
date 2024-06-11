using ResModel;
using ResModel.gw;
using System;
using System.Collections.Generic;
using System.Text;

namespace cma.service.gw_cmd
{
    public class gw_cmd_img_bubao : gw_cmd_img_base
    {
        public override int ValuesLength 
        {  
            get { 
                int len = 4;
                if (this.Pnos != null && this.Pnos.Count > 0)
                    len += this.Pnos.Count * 2;
                return len;
            }
        }

        public override string Name { get { return "远程图像补包数据"; } }

        public override int PType {  get { return 0xcf; } }

        public int ChNO {  get; set; }

        public int Preset {  get; set; }

        public List<int> Pnos { get; set; }

        public gw_cmd_img_bubao() { }

        public gw_cmd_img_bubao(IPowerPole pole) : base(pole) { }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            throw new NotImplementedException();
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            StringBuilder sb = new StringBuilder();

            data[offset++] = (byte)this.ChNO;
            data[offset++] = (byte)this.Preset;
            int num = 0;
            if(this.Pnos != null && this.Pnos.Count >0)
                num = this.Pnos.Count * 2;
            sb.AppendFormat("通道号:{0} ", this.ChNO);
            sb.AppendFormat("预置位号:{0} ", this.Preset);
            sb.AppendFormat("补包包数:{0} ", num);
            offset += gw_coding.SetU16(data, offset, num);
            if (num > 0)
            {
                sb.Append("分别为:");
                foreach(int pno in this.Pnos)
                {
                    offset += gw_coding.SetU16(data, offset, pno);
                    sb.AppendFormat("{0} ", pno);
                }
            }
            msg = sb.ToString();
            return offset - start;
        }
    }
}
