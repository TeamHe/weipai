using System;

namespace ResModel.gw
{
    public class gw_data_weather : gw_data_base
    {
        public gw_data_weather() { }

        /// <summary>
        /// 10分钟平均风速
        /// </summary>
        public float AvgSpeed { get; set; }
        
        /// <summary>
        /// 10 分钟平均风向
        /// </summary>
        public int AvgDir { get; set; }
        
        /// <summary>
        /// 最大风速
        /// </summary>
        public float MaxSpeed { get; set; }
        
        /// <summary>
        /// 极大风速
        /// </summary>
        public float ExtSpeed { get; set; }
        
        /// <summary>
        /// 标准风速
        /// </summary>
        public float StdSpeed { get; set; }
        
        /// <summary>
        /// 气温
        /// </summary>
        public float Temperature { get; set; }
        
        /// <summary>
        /// 湿度
        /// </summary>
        public int Humidity { get; set; }
        
        /// <summary>
        /// 大气压力
        /// </summary>
        public float Air_Pressure { get; set; }
        
        /// <summary>
        /// 降雨量
        /// </summary>
        public float Rain { get; set; }
        
        /// <summary>
        /// 降水强度 
        /// </summary>
        public float Rain_Intensity { get; set; }

        /// <summary>
        /// 光辐射强度
        /// </summary>
        public int Sun_Intensity { get; set; }

        public override int Decode(byte[] data, int offset, out string msg)
        {
            int start = offset;
            msg = string.Empty;
            if (data.Length - offset < 38)
                throw new Exception("数据内容长度错误");
            float fvalue;
            int ivalue;
            //10分钟平均风速
            offset += gw_coding.GetSingle(data, offset, out fvalue);
            this.AvgSpeed = fvalue;

            //10分钟平均风向
            offset += gw_coding.GetU16(data, offset, out ivalue);
            this.AvgDir = ivalue;

            //最大风速
            offset += gw_coding.GetSingle(data, offset, out fvalue);
            this.MaxSpeed = fvalue;

            //极大风速
            offset += gw_coding.GetSingle(data, offset, out fvalue);
            this.ExtSpeed = fvalue;

            //标准风速
            offset += gw_coding.GetSingle(data, offset, out fvalue);
            this.StdSpeed = fvalue;

            //气温
            offset += gw_coding.GetSingle(data, offset, out fvalue);
            this.Temperature = fvalue;

            //湿度
            offset += gw_coding.GetU16(data, offset, out ivalue);
            this.Humidity = ivalue;

            //气压
            offset += gw_coding.GetSingle(data, offset, out fvalue);
            this.Air_Pressure = fvalue;

            //降雨量
            offset += gw_coding.GetSingle(data, offset, out fvalue);
            this.Rain = fvalue;

            //降水强度
            offset += gw_coding.GetSingle(data, offset, out fvalue);
            this.Rain_Intensity = fvalue;

            //光辐射强度
            offset += gw_coding.GetU16(data, offset, out ivalue);
            this.Sun_Intensity = ivalue;
            
            return offset - start;
        }
        public override byte[] Encode(out string msg)
        {
            byte[] data = new byte[38];
            int offset = 0;
            msg = string.Empty;

            offset += gw_coding.SetSingle(data, offset, this.AvgSpeed);
            offset += gw_coding.SetU16(data, offset, this.AvgDir);
            offset += gw_coding.SetSingle(data, offset, this.MaxSpeed);
            offset += gw_coding.SetSingle(data, offset, this.ExtSpeed);
            offset += gw_coding.SetSingle(data, offset, this.StdSpeed);
            offset += gw_coding.SetSingle(data, offset, this.Temperature);
            offset += gw_coding.SetU16(data, offset, this.Humidity);
            offset += gw_coding.SetSingle(data, offset, this.Air_Pressure);
            offset += gw_coding.SetSingle(data, offset, this.Rain);
            offset += gw_coding.SetSingle(data, offset, this.Rain_Intensity);
            offset += gw_coding.SetSingle(data, offset, this.Sun_Intensity);
            return data;
            
        }

        public override string ToString() 
        {
            return base.ToString() + string.Format("10分钟平均风速:{0:f1}m/s 10分钟平均风向:{1}° " +
                "最大风速:{2:f1}m/s 极大风速:{3:f1}m/s " +
                "标准风速:{4:f1}m/s 气温:{5:f1}℃ " +
                "湿度:{6}%RH 大气压力:{7:f1}hPa " +
                "降雨量:{8:f1}mm 降雨强度:{9:f1}mm/min " +
                "光辐射强度:{10}W/m2",
                this.AvgSpeed, this.AvgDir,
                this.MaxSpeed, this.ExtSpeed,
                this.StdSpeed, this.Temperature,
                this.Humidity, this.Air_Pressure,
                this.Rain, this.Rain_Intensity,
                this.Sun_Intensity);
        }
    }
}
