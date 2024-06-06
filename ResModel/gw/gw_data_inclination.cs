using System;
using System.Text;

namespace ResModel.gw
{
    public class gw_data_inclination : gw_data_base
    {
        public gw_data_inclination() { }

        public float Inclination {  get; set; }

        public float Inclination_x {  get; set; }

        public float Inclination_y { get; set; }

        public float Angle_x {  get; set; }

        public float Angle_y { get; set;}

        public override int Decode(byte[] data, int offset, out string msg)
        {
            float fval;
            int start = offset;

            if(data.Length - offset < 20)
                throw new Exception("数据内容长度错误");

            msg = string. Empty;
            offset += gw_coding.GetSingle(data, offset, out fval);
            this.Inclination = fval;

            offset += gw_coding.GetSingle(data, offset, out fval);
            this.Inclination_x = fval;

            offset += gw_coding.GetSingle(data, offset, out fval);
            this.Inclination_y = fval;
            
            offset += gw_coding.GetSingle(data, offset, out fval);
            this.Angle_x = fval;

            offset += gw_coding.GetSingle(data, offset, out fval);
            this.Angle_y = fval;
            return offset - start;
        }

        public override byte[] Encode(out string msg)
        {
            int offset = 0;
            byte[] data = new byte[20];

            msg = string.Empty;
            offset += gw_coding.SetSingle(data, offset, this.Inclination);
            offset += gw_coding.SetSingle(data, offset, this.Inclination_x);
            offset += gw_coding.SetSingle(data, offset, this.Inclination_y);
            offset += gw_coding.SetSingle(data, offset, this.Angle_x);
            offset += gw_coding.SetSingle(data, offset, this.Angle_y);
            return data;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("倾斜度:{0:f1} ", this.Inclination);
            sb.AppendFormat("顺线倾斜度:{0:f1} ", this.Inclination_x);
            sb.AppendFormat("横向倾斜度:{0:f1} ", this.Inclination_y);
            sb.AppendFormat("顺线倾斜角:{0:f2} ", this.Angle_x);
            sb.AppendFormat("横向倾斜角:{0:f2} ", this.Angle_y);
            return base.ToString() + sb.ToString();
        }
    }
}
