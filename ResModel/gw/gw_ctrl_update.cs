using System;
using System.Text;

namespace ResModel.gw
{
    public class gw_ctrl_update_data:gw_ctrl
    {
        public string FileName { get; set; }

        public int PNum {  get; set; }

        public int PNo {  get; set; }

        public byte[] Data { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("文件名:{0} ", this.FileName);
            sb.AppendFormat("总包数:{0} ", this.PNum);
            sb.AppendFormat("子包包号:{0} ",this.PNo);
            return sb.ToString();
        }
    }

    public class gw_ctrl_update_end
    {
        public string FileName { get; set; }

        public int PNum { get; set; }

        public DateTime Time { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("文件名:{0} ", this.FileName);
            sb.AppendFormat("总包数:{0} ", this.PNum);
            sb.AppendFormat("时间:{0}", this.Time);
            return sb.ToString();
        }
    }
}
