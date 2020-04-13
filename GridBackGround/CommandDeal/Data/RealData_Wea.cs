using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.CommandDeal.Data
{
    class RealData_Wea
    {
        #region Private Variable
        private int datalen = 4 //10分钟平均风速
            + 2 //10 分钟平均风向
            + 4 //最大风速 
            + 4 //极大风速 
            + 4//标准风速
            + 4//气温
            + 2//湿度
            + 4//气压
            + 4//降雨量
            + 4//降水强度
            + 2;//  //光辐射强度

        //private string[] VariableMsg = new string[]{
        //    "10分钟平均风速",
        //    "10 分钟平均风向",
        //    "最大风速",
        //    "极大风速",
        //    "标准风速",
        //    "气温",
        //    "湿度",
        //    "气压",
        //    "降雨量",
        //    "降水强度",
        //    "光辐射强度"
        //};
        #endregion


        #region Public Variable
        /// <summary>
        ///  数据解析文字提示
        /// </summary>
        public string AyanMsg { get; private set; }
        /// <summary>
        /// 10分钟平均风速
        /// </summary>
        public float   Average_WindSpeed_10min{ get; set; }
        /// <summary>
        /// 10 分钟平均风向
        /// </summary>
        public UInt16 Average_WindDirection_10min { get; set; }
        /// <summary>
        /// 最大风速
        /// </summary>
        public float Max_WindSpeed { get; set; }              
        /// <summary>
        /// 极大风速
        /// </summary>
        public float Extreme_WindSpeed { get; set; }  
        /// <summary>
        /// 标准风速
        /// </summary>
        public float Standard_WindSpeed { get; set; }  
        /// <summary>
        /// 气温
        /// </summary>
        public float Air_Temperature { get; set; }  
        /// <summary>
        /// 湿度
        /// </summary>
        public UInt16 Humidity { get; set; }  
        /// <summary>
        /// 大气压力
        /// </summary>
        public float Air_Pressure { get; set; }  
        /// <summary>
        /// 降雨量
        /// </summary>
        public float Precipitation { get; set; }  
		/// <summary>
        /// 降水强度 
		/// </summary>
        public float Precipitation_Intensity { get; set; }  
		/// <summary>
        /// 光辐射强度
		/// </summary>
        public float Radiation_Intensity { get; set; }  
        #endregion

        #region Construction
        public RealData_Wea()
        { 
        
        }

        public RealData_Wea(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException();
            if (data.Length != datalen)
                throw new IndexOutOfRangeException();
            AyanBuf(data);
        }


        #endregion

        #region Private Mehtod
        private void AyanBuf(byte[] data)
        {
            int StartNo = 0;
            Average_WindSpeed_10min = BitConverter.ToSingle(data, StartNo);
            AyanMsg += "10分钟平均风速:" + Average_WindSpeed_10min.ToString("f1") + "m/s  ";
            StartNo += 4;

            //10 分钟平均风向
            Average_WindDirection_10min = BitConverter.ToUInt16(data, StartNo);
            AyanMsg += "10 分钟平均风向:" + Average_WindDirection_10min.ToString() + "° ";
            StartNo += 2;

            //最大风速
            Max_WindSpeed = BitConverter.ToSingle(data, StartNo);
            AyanMsg += "最大风速:" + Max_WindSpeed.ToString("f1") + "m/s ";
            StartNo += 4;

            //极大风速
            Extreme_WindSpeed = BitConverter.ToSingle(data, StartNo);
            AyanMsg += "极大风速:" + Extreme_WindSpeed.ToString("f1") + "m/s ";
            StartNo += 4;

            //标准风速
            Standard_WindSpeed = BitConverter.ToSingle(data, StartNo);
            AyanMsg += "标准风速:" + Standard_WindSpeed.ToString("f1") + "m/s ";
            StartNo += 4;

            //气温
            Air_Temperature = BitConverter.ToSingle(data, StartNo);
            AyanMsg += "气温:" + Air_Temperature.ToString("f1") + "℃ ";
            StartNo += 4;

            //湿度
            Humidity = BitConverter.ToUInt16(data, StartNo);
            AyanMsg += "湿度:" + Humidity.ToString() + "%RH ";
            StartNo += 2;

            //气压
            Air_Pressure = BitConverter.ToSingle(data, StartNo);
            AyanMsg += "气压:" + Air_Pressure.ToString("f1") + "hPa ";
            StartNo += 4;

            //降雨量
            Precipitation = BitConverter.ToSingle(data, StartNo);
            AyanMsg += "降雨量:" + Precipitation.ToString("f1") + "mm ";
            StartNo += 4;

            //降水强度
            Precipitation_Intensity = BitConverter.ToSingle(data, StartNo);
            AyanMsg += "降水强度:" + Precipitation_Intensity.ToString("f1") + " mm/min ";
            StartNo += 4;

            //光辐射强度
            Radiation_Intensity = BitConverter.ToUInt16(data, StartNo);
            AyanMsg += "光辐射强度:" + Radiation_Intensity.ToString() + "W/m2 ";
            StartNo += 4;
        }
        #endregion
        
    }
}
