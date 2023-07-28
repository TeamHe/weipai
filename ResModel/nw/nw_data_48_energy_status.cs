using System;

namespace ResModel.nw
{
    /// <summary>
    /// 设备工作电能量状态
    /// </summary>
    public class nw_data_48_energy_status : nw_data_base
    {
        public override int PackLength { get { return 17; } }

        /// <summary>
        /// 电池编号
        /// </summary>
        public int BatteryNo { get; set; }

        /// <summary>
        /// 电池电量
        /// </summary>
        public int BatteryLevel { get; set; }

        /// <summary>
        /// 电池电压
        /// </summary>
        public double BatteryVoltage { get; set; }

        /// <summary>
        /// 电池电流
        /// </summary>
        public int BatteryCurrent { get; set; }

        /// <summary>
        /// 电池充电状态
        /// </summary>
        public bool BatteryCharge{ get; set; }

        /// <summary>
        /// 工作温度
        /// </summary>
        public double WorkTemp { get; set; }

        /// <summary>
        /// 光伏/风力阵列电压
        /// </summary>
        public double InputVoltage { get; set; }

        /// <summary>
        /// 光伏/风力阵列电流
        /// </summary>
        public int InputCurrent { get; set; }

        /// <summary>
        /// 负载电压
        /// </summary>
        public double LoadVoltage { get; set; }

        /// <summary>
        ///负载电流
        /// </summary>
        public int LoadCurrent { get; set; }

        public override int Decode(byte[] data, int offset)
        {
            int val;
            if ((data.Length - offset) < this.PackLength)
                return -1;

            this.BatteryNo = data[offset++];
            this.BatteryLevel = data[offset++]; 

            offset += nw_cmd_base.GetU16(data, offset, out val);
            this.BatteryVoltage = val * 0.1;

            offset += nw_cmd_base.GetU16(data, offset, out val);
            this.BatteryCurrent = val;

            if (data[offset++] > 0)
                this.BatteryCharge = true;
            else 
                this.BatteryCharge = false;

            offset += nw_cmd_base.GetU16(data, offset, out val);
            this.WorkTemp = (val - 500) * 0.1;

            offset += nw_cmd_base.GetU16(data, offset, out val);
            this.InputVoltage = val * 0.1;

            offset += nw_cmd_base.GetU16(data, offset, out val);
            this.InputCurrent = val;

            offset += nw_cmd_base.GetU16(data, offset, out val);
            this.LoadVoltage = val * 0.1;

            offset += nw_cmd_base.GetU16(data, offset, out val);
            this.LoadCurrent = val;
            return this.PackLength;
        }

        public override int Encode(byte[] data, int offset)
        {
            if ((data.Length - offset) < this.PackLength)
                return -1;

            data[offset++] = (byte)this.BatteryNo;
            data[offset++] = (byte)this.BatteryLevel;
            offset += nw_cmd_base.SetU16(data, offset, (int)(this.BatteryVoltage * 10));
            offset += nw_cmd_base.SetU16(data, offset, this.BatteryCurrent);

            if (this.BatteryCharge)
                data[offset++] = 1;
            else
                data[offset++] = 0;

            offset += nw_cmd_base.SetU16(data, offset, (int)((this.WorkTemp + 50) * 10));
            offset += nw_cmd_base.SetU16(data, offset, (int)(this.InputVoltage * 10));
            offset += nw_cmd_base.SetU16(data, offset, this.InputCurrent);
            offset += nw_cmd_base.SetU16(data, offset, (int)(this.LoadVoltage * 10));
            nw_cmd_base.SetU16(data, offset, this.LoadCurrent);
            return this.PackLength;
        }

        public override string ToString()
        {
            return string.Format("时间:{0:G} 电池编号:{1} 电池电量:{2}% " +
                                 "电池电压:{3}V 电池电流:{4}mA 电池充电状态:{5} " +
                                 "工作温度:{6}℃ 光伏电压:{7}V 光伏电流:{8}mA " +
                                 "负载电压:{9}V 负载电流:{10}mA",
                                 this.DataTime, this.BatteryNo, this.BatteryLevel,
                                 this.BatteryVoltage, this.BatteryCurrent, this.BatteryCharge ? "充电" : "放电",
                                 this.WorkTemp, this.InputVoltage, this.InputCurrent,
                                 this.LoadVoltage, this.LoadCurrent);
        }
    }
}
