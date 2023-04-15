using GridBackGround.PacketAnaLysis;
using GridBackGround.Termination;
using System;
using System.Collections.Generic;
using ResModel.nw;
using ResModel;
using DB_Operation.RealData;

namespace GridBackGround.CommandDeal.nw
{
    /// <summary>
    /// 气象数据包指令处理
    /// </summary>
    public class nw_cmd_25_weather : nw_cmd_base
    {
        public override int Control { get { return 0x25; } }

        public override string Name { get { return "气象数据"; } }

        public string Password { get; set; }

        /// <summary>
        /// 是否为主站请求数据
        /// </summary>
        private bool Response { get; set; }

        /// <summary>
        /// 帧标识
        /// </summary>
        private byte FrameFlag { get; set; }

        /// <summary>
        /// 本次接收到的数据列表
        /// </summary>
        public List<nw_data_weather> Weathers { get; set; }

        public nw_cmd_25_weather()
        {

        }

        public nw_cmd_25_weather(IPowerPole pole) : base(pole)
        {
            
        }

        /// <summary>
        /// 微气象数据内容解析
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="weather"></param>
        /// <returns></returns>
        public int Decode_Weather(byte[] data, int offset, out nw_data_weather weather)
        {
            weather = null;
            //温度:上送值减去 500 除以 10 即为实际环境温度，如 450 =（450 - 500）/ 10 = -5.0 度；
            //风速：风速为 3 秒平均风速，结果除以 10即为实际风速，如风速上送 89 即为 8.9 米 / 秒；
            //风向：风速为 3 秒平均风向，结果为与正北方向的夹角；
            //降雨量：降雨量为采样时间前一小时的累计雨量，数据除以 100 计算得出的数值为每小时降雨量。
            //1 分钟、10 分钟平均风速：采用滑动平均算法计算的采样时间前 10 分钟的平均风速；
            //1 分钟、10 分钟平均风向：采用滑动平均算法计算的采样时间前 10 分钟的平均风向

            //温度（2 字节）+湿度（1 字节）+瞬时风速（2 字节） +瞬时风向（2 字节）+雨量（2 字节）+ (9字节)
            //气压（2 字节）+日照（2 字节）+1 分钟平均风速（2 字节）+1分钟平均风向（2 字节）+      (8字节)
            //10 分钟平均风速（2 字节）+10 分钟平均风向（2 字节）+10 分钟最大风速。                (6字节)
            if (data.Length - offset < 23)
                return -1;
            int no = offset;

            int u16 = 0;
            weather = new nw_data_weather();
            //温度
            no += this.GetU16(data, no, out u16);
            weather.Temp = (u16 - 500) / 10.0;

            //湿度
            weather.Humidity = (int)data[no++];

            //风速
            no += this.GetU16(data, no, out u16);
            weather.Speed = u16 / 10.0;

            //风向
            no += this.GetU16(data, no, out u16);
            weather.Direction = u16;

            //雨量
            no += this.GetU16(data, no, out u16);
            weather.Rain = u16 / 100.0;

            //气压
            no += this.GetU16(data, no, out u16);
            weather.Pressure = u16;

            //日照
            no += this.GetU16(data, no, out u16);
            weather.Sun = u16;

            //1 分钟平均风速
            no += this.GetU16(data, no, out u16);
            weather.Speed_1_min = u16 / 10.0;

            //1 分钟平均风向
            no += this.GetU16(data, no, out u16);
            weather.Direction_1_min = u16;

            //10 分钟平均风速
            no += this.GetU16(data, no, out u16);
            weather.Speed_10_min = u16 / 10.0;

            //10 分钟平均风向
            no += this.GetU16(data, no, out u16);
            weather.Direction_10_min = u16;

            //10 分钟最大风速
            no += this.GetU16(data, no, out u16);
            weather.Speed_max = u16 / 10.0;

            return no-offset;
        }

        public override int Decode(out string msg)
        {
            msg = null;
            if(this.Data ==null || this.Data.Length == 0)
            {
                msg = "装置无未上送数据";
                return 0;
            }

            int offset = 4; //不验证密文信息，以及帧标识
            int ret = 0;
            this.FrameFlag = this.Data[offset++];
            int pnum = this.Data[offset++];
            this.Weathers = new List<nw_data_weather>();
            db_data_nw_weather db = new db_data_nw_weather(this.Pole);
            
            offset += this.GetDateTime(this.Data, offset, out DateTime datatime);
            for(int i = 0; i < pnum; i++)
            {
                if((ret = this.Decode_Weather(this.Data,offset,out nw_data_weather weather)) < 0)
                {
                    msg = string.Format("第{0}包数据解析失败", i);
                    break;
                }

                offset += ret;
                weather.DataTime = datatime;
                this.Weathers.Add(weather);

                string msg1 = string.Empty;
                try
                {
                    db.DataSave(weather);
                }
                catch
                {
                    msg1 = "数据存储失败";
                }

                //显示数据
                DisPacket.NewRecord(new DataInfo(DataRecSendState.rec, this.Pole,
                    this.Name, weather.ToString() + msg1));
                if (i == pnum - 1)
                    break;
                if((this.Data.Length - offset) < 2)
                {
                    msg = string.Format("第{0}包数据长度错误",i+1);
                    break;
                }
                    
                offset += this.GetU16(this.Data, offset, out int period);
                datatime = datatime.AddSeconds(period);
            }
            this.Response = true;
            this.SendCommand( out string msg_send);
            msg += msg_send;
            return ret;
        }

        public override byte[] Encode(out string msg)
        {
            msg = string.Empty;
            if (this.Response)
            {
                byte[] data = new byte[3];
                data[0] = this.FrameFlag;
                data[1] = 0xaa;
                data[2] = 0x55;
                return data;
            }
                        msg = "主站请求上传气象数据";
            return null;
        }
    }
}
