using System;
using System.Collections.Generic;
using System.Text;

namespace ResModel.gw
{
    public class gw_data_ice_pull
    {
        public gw_data_ice_pull() { }

        /// <summary>
        /// 原始拉力值
        /// </summary>
        public float Original_Tension {  get; set; }

        /// <summary>
        /// 风偏角
        /// </summary>
        public float Windage_Yaw_Angle {  get; set; }

        /// <summary>
        /// 偏斜角
        /// </summary>
        public float Deflection_Angle { get; set; }
    }

    public class gw_data_ice : gw_data_base
    {
        public gw_data_ice() 
        {
            this.Pulls = new List<gw_data_ice_pull>();
        }

        /// <summary>
        /// 等值覆冰厚度
        /// </summary>
        public float Equal_IceThicknes { get; set; }

        /// <summary>
        /// 综合悬挂载荷
        /// </summary>
        public float Tension { get; set; }

        /// <summary>
        /// 不均衡张力差
        /// </summary>
        public float Tension_Difference { get; set; }

        public List<gw_data_ice_pull> Pulls { get; set; }

        public override int Decode(byte[] data, int offset, out string msg)
        {
            int start = offset;
            msg = string.Empty;
            if (data.Length - offset < 13)
                throw new Exception("数据内容长度错误");

            float fvalue;
            //等值覆冰厚度
            offset += gw_coding.GetSingle(data, offset, out fvalue);
            this.Equal_IceThicknes = fvalue;

            //综合悬挂载荷
            offset += gw_coding.GetSingle(data, offset, out fvalue);
            this.Tension = fvalue;
            
            // 不均衡张力差
            offset += gw_coding.GetSingle(data, offset, out fvalue);
            this.Tension_Difference = fvalue;

            int sensor_num = data[offset++];
            if(data.Length - offset < sensor_num*12)
                throw new Exception("数据内容长度错误");

            for(int i=0;i<sensor_num; i++)
            {
                gw_data_ice_pull pull = new gw_data_ice_pull();
                //原始拉力值
                offset += gw_coding.GetSingle(data, offset, out fvalue);
                pull.Original_Tension = fvalue;

                //风偏角
                offset += gw_coding.GetSingle(data, offset, out fvalue);
                pull.Windage_Yaw_Angle = fvalue;

                // 偏斜角
                offset += gw_coding.GetSingle(data, offset, out fvalue);
                pull.Deflection_Angle = fvalue;

                this.Pulls.Add(pull);
            }
            return offset - start;
        }
        public override byte[] Encode(out string msg)
        {
            byte[] data = new byte[13+this.Pulls.Count*12];
            int offset = 0;
            msg = string.Empty;

            offset += gw_coding.SetSingle(data, offset, this.Equal_IceThicknes);
            offset += gw_coding.SetSingle(data, offset, this.Tension);
            offset += gw_coding.SetSingle(data, offset, this.Tension_Difference);
            data[offset++] = (byte)this.Pulls.Count; 
            foreach(gw_data_ice_pull pull in this.Pulls) 
            {
                offset += gw_coding.SetSingle(data, offset, pull.Original_Tension);
                offset += gw_coding.SetSingle(data, offset, pull.Windage_Yaw_Angle);
                offset += gw_coding.SetSingle(data, offset, pull.Deflection_Angle);
            }
            return data;

        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("覆冰厚度:{0:f1}mm 悬挂载荷:{1:f1}N 不均衡张力差:{2:f1}N ",
                this.Equal_IceThicknes, this.Tension, this.Tension_Difference);
            int no = 1;
            foreach(var pull in this.Pulls)
            {
                builder.AppendFormat("拉力传感器{0}: 原始拉力值{1:f1}N 风偏角:{2:f2}° 偏斜角:{3:f2}° ",
                    no++,pull.Original_Tension, pull.Windage_Yaw_Angle, pull.Deflection_Angle);
            }
            return base.ToString() + builder.ToString();
        }
    }
}
