using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResModel.EQU;
using Tools;
using ResModel.CollectData;
using System.Data;

namespace ResModel.CollectData
{
    /// <summary>
    /// 采集数据基类
    /// </summary>
    public class CollectData
    {

        #region Public Variable
        /// <summary>
        /// 设备
        /// </summary>
        public Equ Equ { get; set; }
        /// <summary>
        /// 装置名称
        /// </summary>
        public string CMD_NAME { get; set; }

        /// <summary>
        /// 装置ID
        /// </summary>
        public string CMD_ID { get; set; }

        /// <summary>
        /// 被测ID
        /// </summary>
        public string COMP_ID { get; set; }

        /// <summary>
        /// 数据采集时间
        /// </summary>
        public DateTime Maintime { get; set; }

        

        /// <summary>
        ///  数据解析文字提示
        /// </summary>
        public string AyanMsg { get; protected set; }
        #endregion

        #region Protected Variable
        /// <summary>
        /// 装置类型
        /// </summary>
        public ICMP Type { get; protected set; }
        /// 采集单元数
        /// </summary>
        protected uint Unit_Sum { get; private set; }
        /// <summary>
        /// 采集单元号
        /// </summary>
        protected uint Unit_No { get; private set; }

        protected int StartNo;
        #endregion
        
        #region Construction
        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectData()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        public CollectData(ICMP type, byte[] data)
        {
            this.Type = type;
            this.CollectDataAyan(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public CollectData(byte[] data)
        {
            this.CollectDataAyan(data);
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 数据报文头解析
        /// </summary>
        /// <param name="data"></param>
        private void CollectDataAyan(byte[] data)
        {
            if (data.Length <= (17 + 4))
                throw new IndexOutOfRangeException();
            StartNo = 0;
            //获取数据报包头部分
            this.COMP_ID = Encoding.Default.GetString(data, StartNo, 17);
            AyanMsg += "被测设备ID:" + COMP_ID + " ";
            StartNo += 17;
            //采集单元内容
            switch ((ICMP)Type)      //线上单元添加采集单元数目和采集单元序号
            {
                case ICMP.Windage_Yaw:          //导线风偏
                case ICMP.Line_Temperature:     //线温
                case ICMP.Vibration_Character:  //振动
                case ICMP.Vibration_Form:
                case ICMP.Wave_Character:       //舞动
                case ICMP.Wave_Trajectory:
                    Unit_Sum = (uint)data[StartNo];
                    AyanMsg += "采集单元总数:" + Unit_Sum.ToString() + " ";
                    StartNo += 1;
                    Unit_No = (uint)data[StartNo];
                    AyanMsg += "采集单元序号:" + Unit_No.ToString() + " ";
                    StartNo += 1;
                    break;
            }
            //采集时间
            this.Maintime = Tools.TimeUtil.BytesToDate(data, StartNo);
            AyanMsg += "采集时间:" + this.Maintime.ToString() + " ";
            StartNo += 4;
        }

        #endregion

        public virtual void SaveToDB()
        { 
        
        }
    }
}
