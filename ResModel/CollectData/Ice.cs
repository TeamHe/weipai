using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResModel.EQU;

namespace ResModel.CollectData
{
    /// <summary>
    /// 覆冰及不均衡张力差数据报
    /// </summary>
    public class Ice:CollectData
    {
        #region Private Variable
        
        #endregion

        #region Public Varialble
      
        /// <summary>
        /// 等值覆冰厚度
        /// </summary>
        public float Equal_IceThickness { get; set; }
        /// <summary>
        /// 综合悬挂载荷
        /// </summary>
        public float Tension { get; set; }
        /// <summary>
        /// 不均衡张力差
        /// </summary>
        public float Tension_Difference { get; set; }
        /// <summary>
        /// 原始拉力1
        /// </summary>
        public float Original_Tension1 { get; set; }
        /// <summary>
        /// 风偏角1
        /// </summary>
        public float Windage_Yaw_Angle1 { get; set; }
        /// <summary>
        /// 偏斜角1
        /// </summary>
        public float Deflection_Angle1 { get; set; }
        /// <summary>
        /// 原始拉力2
        /// </summary>
        public float Original_Tension2 { get; set; }
        /// <summary>
        /// 风偏角2
        /// </summary>
        public float Windage_Yaw_Angle2 { get; set; }
        /// <summary>
        /// 偏斜角2
        /// </summary>
        public float Deflection_Angle2 { get; set; }
        #endregion

        #region Construction
        public Ice() { }

        /// <summary>
        /// 气象构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cmdID"></param>
        /// <param name="data"></param>
        public Ice(string name,string cmdID,byte[] data)
            :base(ICMP.Weather,data)
        {
            this.CMD_ID = cmdID;
            this.CMD_NAME = name;
            if (data == null)
                throw new ArgumentNullException();
            AyanBuf(data);
        }
        
        #endregion

        #region Private Funcition
        
        /// <summary>
        /// 数据内容解析
        /// </summary>
        /// <param name="data"></param>
        private void AyanBuf(byte[] data)
        {
            int SensorNum = 0;

            //等值覆冰厚度
            Equal_IceThickness = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Ice.CloumsName[0] + ":" + Equal_IceThickness.ToString("f1") + "mm  ";
            StartNo += 4;
            //综合悬挂载荷
            Tension = BitConverter.ToSingle(data,StartNo);
            AyanMsg += DataBase.Table_Ice.CloumsName[1] + ":" + Tension.ToString("f1") + "N  ";
            StartNo += 4;
            //不均衡张力差
            Tension_Difference = BitConverter.ToSingle(data, StartNo);
            AyanMsg += DataBase.Table_Ice.CloumsName[2] + ":" + Tension_Difference.ToString("f1") + "N  ";
            StartNo += 4;
            //传感器个数
            SensorNum = (int)data[StartNo++];
            AyanMsg += "传感器个数： " + SensorNum.ToString();
            if (SensorNum >= 1)
            {
                //原始拉力1
                Original_Tension1 = BitConverter.ToSingle(data, StartNo);
                AyanMsg += DataBase.Table_Ice.CloumsName[3] + ":" + Original_Tension1.ToString("f1") + "N  ";
                StartNo += 4;
                //风偏角1
                Windage_Yaw_Angle1 = BitConverter.ToSingle(data, StartNo);
                AyanMsg += DataBase.Table_Ice.CloumsName[4] + ":" + Windage_Yaw_Angle1.ToString("f2") + "°  ";
                StartNo += 4;
                //偏斜角1
                Deflection_Angle1 = BitConverter.ToSingle(data, StartNo);
                AyanMsg += DataBase.Table_Ice.CloumsName[5] + ":" + Deflection_Angle1.ToString("f2") + "°  ";
                StartNo += 4;
            }
            if (SensorNum >= 2)
            {
                Original_Tension2 = BitConverter.ToSingle(data, StartNo);
                AyanMsg += DataBase.Table_Ice.CloumsName[6] + ":" + Original_Tension2.ToString("f1") + "N  ";
                StartNo += 4;
                //风偏角1
                Windage_Yaw_Angle2 = BitConverter.ToSingle(data, StartNo);
                AyanMsg += DataBase.Table_Ice.CloumsName[7] + ":" + Windage_Yaw_Angle2.ToString("f2") + "°  ";
                StartNo += 4;
                //偏斜角1
                Deflection_Angle2= BitConverter.ToSingle(data, StartNo);
                AyanMsg += DataBase.Table_Ice.CloumsName[8] + ":" + Deflection_Angle2.ToString("f2") + "°  ";
                StartNo += 4;
            }
        }
        #endregion
    }
}
