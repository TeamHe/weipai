using System;
using System.Text;

namespace ResModel.gw_nw
{
    public class gn_picture
    {
        public int ChNO {  get; set; }

        public int Preset { get; set; }

        public string FileName { get; set; }

        public DateTime Time {  get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("通道号:{0} ",this.ChNO);
            sb.AppendFormat("预置位号:{0} ", this.Preset);
            sb.AppendFormat("文件路径:{0} ", this.FileName); 
            return base.ToString();
        }
    }
}
