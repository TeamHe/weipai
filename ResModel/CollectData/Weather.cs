using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResModel.EQU;

namespace ResModel.CollectData
{
    /// <summary>
    /// 微气象数据模型
    /// </summary>
    public class Weather:CollectData
    {
        #region Private Variable

        #endregion


        #region Public Variable

       
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
        public Weather()
        { 
        
        }
        /// <summary>
        /// 气象构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cmdID"></param>
        /// <param name="data"></param>
        public Weather(string name,string cmdID,byte[] data)
            :base(ICMP.Weather,data)
        {
            this.CMD_ID = cmdID;
            this.CMD_NAME = name;
            if (data == null)
                throw new ArgumentNullException();
            AyanBuf(data);
        }


        #endregion

        #region Private Mehtod
        /// <summary>
        /// 数据内容解析
        /// </summary>
        /// <param name="data"></param>
        private void AyanBuf(byte[] data)
        {
            Average_WindSpeed_10min = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Weather.CloumsName[0] + ":" + Average_WindSpeed_10min.ToString("f1") + "m/s  ";
            StartNo += 4;

            //10 分钟平均风向
            Average_WindDirection_10min = BitConverter.ToUInt16(data, StartNo);
            AyanMsg += DataBase.Table_Weather.CloumsName[1] + ":" + Average_WindDirection_10min.ToString() + "° ";
            StartNo += 2;

            //最大风速
            Max_WindSpeed = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Weather.CloumsName[2] + ":" + Max_WindSpeed.ToString("f1") + "m/s ";
            StartNo += 4;

            //极大风速
            Extreme_WindSpeed = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Weather.CloumsName[3] + ":" + Extreme_WindSpeed.ToString("f1") + "m/s ";
            StartNo += 4;

            //标准风速
            Standard_WindSpeed = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Weather.CloumsName[4] + ":" + Standard_WindSpeed.ToString("f1") + "m/s ";
            StartNo += 4;

            //气温
            Air_Temperature = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Weather.CloumsName[5] + ":" + Air_Temperature.ToString("f1") + "℃ ";
            StartNo += 4;

            //湿度
            Humidity = BitConverter.ToUInt16(data, StartNo);
            AyanMsg += DataBase.Table_Weather.CloumsName[6] + ":" + Humidity.ToString() + "%RH ";
            StartNo += 2;

            //气压
            Air_Pressure = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Weather.CloumsName[7] + ":" + Air_Pressure.ToString("f1") + "hPa ";
            StartNo += 4;

            //降雨量
            Precipitation = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Weather.CloumsName[8] + ":" + Precipitation.ToString("f1") + "mm ";
            StartNo += 4;

            //降水强度
            Precipitation_Intensity = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Weather.CloumsName[9] + ":" + Precipitation_Intensity.ToString("f1") + " mm/min ";
            StartNo += 4;

            //光辐射强度
            Radiation_Intensity = BitConverter.ToUInt16(data, StartNo);
            AyanMsg += DataBase.Table_Weather.CloumsName[10] + ":" + Radiation_Intensity.ToString() + "W/m2 ";
            StartNo += 4;
        }
        #endregion
        
    }

}
