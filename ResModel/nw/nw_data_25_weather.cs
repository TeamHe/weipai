namespace ResModel.nw
{
    /// <summary>
    /// 气象数据handle
    /// </summary>
    public class nw_data_25_weather : nw_data_base
    {
        /// <summary>
        /// 温度
        /// </summary>
        public double Temp { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public int Humidity { get; set; }

        /// <summary>
        /// 瞬时风速
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// 瞬时风向
        /// </summary>
        public int Direction { get; set; }

        /// <summary>
        /// 降雨量
        /// </summary>
        public double Rain { get; set; }

        /// <summary>
        /// 大气压力
        /// </summary>
        public int Pressure { get; set; }

        /// <summary>
        /// 日照
        /// </summary>
        public int Sun { get; set; }
        /// <summary>
        /// 1分钟平均风速
        /// </summary>
        public double Speed_1_min { get; set; }

        /// <summary>
        /// 1分钟平均风向
        /// </summary>
        public int Direction_1_min { get; set; }

        /// <summary>
        /// 10分钟平均风速
        /// </summary>
        public double Speed_10_min { get; set; }

        /// <summary>
        /// 10分钟平均风向
        /// </summary>
        public int Direction_10_min { get; set; }

        /// <summary>
        /// 10分钟平均风速
        /// </summary>
        public double Speed_max { get; set; }

        public override int PackLength { get { return 23; } }

        public override int Decode(byte[] data, int offset)
        {
            //温度:上送值减去 500 除以 10 即为实际环境温度，如 450 =（450 - 500）/ 10 = -5.0 度；
            //风速：风速为 3 秒平均风速，结果除以 10即为实际风速，如风速上送 89 即为 8.9 米 / 秒；
            //风向：风速为 3 秒平均风向，结果为与正北方向的夹角；
            //降雨量：降雨量为采样时间前一小时的累计雨量，数据除以 100 计算得出的数值为每小时降雨量。
            //1 分钟、10 分钟平均风速：采用滑动平均算法计算的采样时间前 10 分钟的平均风速；
            //1 分钟、10 分钟平均风向：采用滑动平均算法计算的采样时间前 10 分钟的平均风向

            //温度（2 字节）+湿度（1 字节）+瞬时风速（2 字节） +瞬时风向（2 字节）+雨量（2 字节）+ (9字节)
            //气压（2 字节）+日照（2 字节）+1 分钟平均风速（2 字节）+1分钟平均风向（2 字节）+      (8字节)
            //10 分钟平均风速（2 字节）+10 分钟平均风向（2 字节）+10 分钟最大风速。                (6字节)

            if (data.Length - offset < this.PackLength)
                return -1;
            int no = offset;
            int u16 = 0;

            //温度
            no += nw_cmd_base.GetU16(data, no, out u16);
            this.Temp = (u16 - 500) / 10.0;

            //湿度
            this.Humidity = (int)data[no++];

            //风速
            no += nw_cmd_base.GetU16(data, no, out u16);
            this.Speed = u16 / 10.0;

            //风向
            no += nw_cmd_base.GetU16(data, no, out u16);
            this.Direction = u16;

            //雨量
            no += nw_cmd_base.GetU16(data, no, out u16);
            this.Rain = u16 / 100.0;

            //气压
            no += nw_cmd_base.GetU16(data, no, out u16);
            this.Pressure = u16;

            //日照
            no += nw_cmd_base.GetU16(data, no, out u16);
            this.Sun = u16;

            //1 分钟平均风速
            no += nw_cmd_base.GetU16(data, no, out u16);
            this.Speed_1_min = u16 / 10.0;

            //1 分钟平均风向
            no += nw_cmd_base.GetU16(data, no, out u16);
            this.Direction_1_min = u16;

            //10 分钟平均风速
            no += nw_cmd_base.GetU16(data, no, out u16);
            this.Speed_10_min = u16 / 10.0;

            //10 分钟平均风向
            no += nw_cmd_base.GetU16(data, no, out u16);
            this.Direction_10_min = u16;

            //10 分钟最大风速
            no += nw_cmd_base.GetU16(data, no, out u16);
            this.Speed_max = u16 / 10.0;

            return no - offset;

        }

        public override int Encode(byte[] data, int offset)
        {
            //温度:上送值减去 500 除以 10 即为实际环境温度，如 450 =（450 - 500）/ 10 = -5.0 度；
            //风速：风速为 3 秒平均风速，结果除以 10即为实际风速，如风速上送 89 即为 8.9 米 / 秒；
            //风向：风速为 3 秒平均风向，结果为与正北方向的夹角；
            //降雨量：降雨量为采样时间前一小时的累计雨量，数据除以 100 计算得出的数值为每小时降雨量。
            //1 分钟、10 分钟平均风速：采用滑动平均算法计算的采样时间前 10 分钟的平均风速；
            //1 分钟、10 分钟平均风向：采用滑动平均算法计算的采样时间前 10 分钟的平均风向

            //温度（2 字节）+湿度（1 字节）+瞬时风速（2 字节） +瞬时风向（2 字节）+雨量（2 字节）+ (9字节)
            //气压（2 字节）+日照（2 字节）+1 分钟平均风速（2 字节）+1分钟平均风向（2 字节）+      (8字节)
            //10 分钟平均风速（2 字节）+10 分钟平均风向（2 字节）+10 分钟最大风速。                (6字节)
            if (data.Length - offset < this.PackLength)
                return -1;

            int no = offset;

            no += nw_cmd_base.SetU16(data, no, (int)((this.Temp + 50) * 10));         //温度
            data[no++] = (byte)this.Humidity;                            //湿度
            no += nw_cmd_base.SetU16(data, no, (int)(this.Speed * 10));         //风速
            no += nw_cmd_base.SetU16(data, no, (int)(this.Direction));            //风向
            no += nw_cmd_base.SetU16(data, no, (int)(this.Rain * 100));             //雨量
            no += nw_cmd_base.SetU16(data, no, (int)(this.Pressure));             //气压
            no += nw_cmd_base.SetU16(data, no, (int)(this.Sun));                  //日照
            no += nw_cmd_base.SetU16(data, no, (int)(this.Speed_1_min * 10));     //1 分钟平均风速
            no += nw_cmd_base.SetU16(data, no, (int)(this.Direction_1_min));      //1 分钟平均风向
            no += nw_cmd_base.SetU16(data, no, (int)(this.Speed_10_min * 10));    //10 分钟平均风速
            no += nw_cmd_base.SetU16(data, no, (int)(this.Direction_10_min));     //10 分钟平均风向
            no += nw_cmd_base.SetU16(data, no, (int)(this.Speed_max * 10));       //10 分钟最大风速
            return no - offset;

        }

        public override string ToString()
        {
            return string.Format("时间:{12:G} " +
                "温度:{0} 湿度:{1} 瞬时风速:{2} 瞬时风向:{3} " +
                "雨量:{4} 气压:{5} 日照:{6} 1分钟平均风速:{7} " +
                "1分钟平均风向:{8} 10分钟平均风速:{9} " +
                "10分钟平均风向:{10} 10分钟最大风速:{11}",
                this.Temp, this.Humidity, this.Speed, this.Direction,
                this.Rain, this.Pressure, this.Sun, this.Speed_1_min,
                this.Direction_1_min, this.Speed_10_min,
                this.Direction_10_min, this.Speed_max,
                this.DataTime);
        }
    }


}
