using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResModel.EQU;

namespace ResModel.CollectData
{
    /// <summary>
    /// 杆塔倾斜
    /// </summary>
    public class Inclinations : CollectData
    {
        #region Private Varialble
        
        #endregion

        #region Public Varialble
        /// <summary>
        /// 倾斜度
        /// </summary>
        public float Inclination { get; set; }
        /// <summary>
        /// 顺线倾斜度
        /// </summary>
        public float Inclination_X { get; set; }
        /// <summary>
        /// 横向倾斜度
        /// </summary>
        public float Inclination_Y { get; set; }
        /// <summary>
        /// 顺线倾斜角
        /// </summary>
        public float Angle_X { get; set; }
        /// <summary>
        /// 横向倾斜角
        /// </summary>
        public float Angle_Y { get; set; }
        #endregion

        #region Construction
        /// <summary>
        /// 构造函数
        /// </summary>
        public Inclinations()
        { 
            
        }

        public Inclinations(string name, string cmdID, byte[] data)
            :base(ICMP.Inclination,data)
        {
            this.CMD_ID = cmdID;
            this.CMD_NAME = name;
            if (data == null)
                throw new ArgumentNullException();
            AyanBuf(data);
        }
        #endregion

        #region Private Functions
         /// <summary>
        /// 数据内容解析
        /// </summary>
        /// <param name="data"></param>
        private void AyanBuf(byte[] data)
        {
            Inclination = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Inclination.CloumsName[0] + ":" + Inclination.ToString("f1") + "mm/m  ";
            StartNo += 4;

            Inclination_X = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Inclination.CloumsName[1] + ":" + Inclination_X.ToString("f1") + "mm/m  ";
            StartNo += 4;

            Inclination_Y = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Inclination.CloumsName[2] + ":" + Inclination_Y.ToString("f1") + "mm/m  ";
            StartNo += 4;

            Angle_X = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Inclination.CloumsName[3] + ":" + Angle_X.ToString("f2") + "°  ";
            StartNo += 4;

            Angle_Y = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Inclination.CloumsName[4] + ":" + Angle_Y.ToString("f2") + "°  ";
            StartNo += 4;
        }
        #endregion
    }
}
