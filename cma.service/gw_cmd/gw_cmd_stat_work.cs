using ResModel.gw;
using System;

namespace cma.service.gw_cmd
{
    public class gw_cmd_stat_work : gw_cmd_stat_base
    {
        public override string Name { get { return "工作状态报"; } }

        public override int PType {  get { return 0xe8; } }

        /// <summary>
        /// 数据区长度: 采集时间（4） 电源电压(4) 工作温度(4) 电池电量(4) 
        ///             浮充状态 (1)  工作总时间(4)  本次工作时间(4)
        ///             网络连接状态 (1)
        /// </summary>
        private int data_len = 26;
        public gw_stat_work Stat { get; set; }

        public override int decode(byte[] data, int offset, out string msg)
        {
            int start = offset;
            if (data.Length - offset < data_len)
                throw new Exception("数据缓冲区长度太小");
            if (this.Stat == null)
                this.Stat = new gw_stat_work();

            float fval;
            UInt32 uval;
            offset += gw_coding.GetTime(data,offset,out DateTime time);
            Stat.Time = time;
            //电源电压
            offset += gw_coding.GetSingle(data, offset, out fval);
            Stat.Voltage = fval;
            //工作温度
            offset += gw_coding.GetSingle(data, offset, out fval);
            Stat.Temp = fval;
            //电池电量
            offset += gw_coding.GetSingle(data, offset, out fval);
            Stat.Capacity = fval;
            //浮充状态
            Stat.FloatingCharge = data[offset++] > 0 ? false : true;
            //工作总时间
            offset += gw_coding.GetU32(data, offset, out uval);
            Stat.TotalWrokTime = uval;
            //本次工作时间
            offset += gw_coding.GetU32(data, offset, out uval);
            Stat.WrokTime = uval;

            Stat.ConnectionState = data[offset++] > 0 ? false : true;
            msg = Stat.ToString();
            this.Execute();
            return offset - start;
        }

        public override byte[] encode(out string msg)
        {
            msg = string.Empty;
            byte[] data = new byte[1];
            int offset = 0;
            data[offset++] = (byte)gw_ctrl.ESetStatus.Success;  //数据发送状态: 成功
            return data;
        }
    }
}
