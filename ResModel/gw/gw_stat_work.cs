using System;
using System.Text;

namespace ResModel.gw
{
    public class gw_stat_work
    {
        public gw_stat_work() { }

        /// <summary>
        /// 采集时间 Time Stamp
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 电源电压 Battery Voltage
        /// </summary>
        public float Voltage {  get; set; }
        
        /// <summary>
        /// 工作温度 Operation Temprature
        /// </summary>
        public float Temp {  get; set; }
        
        /// <summary>
        /// 电池电量 Battery Capacity
        /// </summary>
        public float Capacity {  get; set; }

        /// <summary>
        /// 浮充状态 Floating Charge 
        /// TRUE 充电   FALSE  放电
        /// </summary>
        public bool FloatingCharge {  get; set; }
        
        /// <summary>
        /// 工作总时间
        /// </summary>
        public UInt32 TotalWrokTime {  get; set; }
        
        /// <summary>
        /// 本次工作时间
        /// </summary>
        public UInt32 WrokTime {  get; set; }

        /// <summary>
        /// 网络连接状态  TRUE 正常   FALSE  断开
        /// </summary>
        public bool ConnectionState { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("采集时间:{0} ", this.Time);
            sb.AppendFormat("电源电压:{0:f2}V ", this.Voltage);
            sb.AppendFormat("工作温度:{0:f1}℃ ", this.Temp);
            sb.AppendFormat("电池电量:{0:f2}Ah ", this.Capacity);
            sb.AppendFormat("浮充状态:{0} ", this.FloatingCharge?"充电":"放电");
            sb.AppendFormat("工作总时间:{0}H ", this.TotalWrokTime);
            sb.AppendFormat("本次工作时间:{0}H ", this.WrokTime);
            sb.AppendFormat("网络连接状态:{0} ", this.ConnectionState? "正常":"断开");
            return sb.ToString();
        }
    }
}
