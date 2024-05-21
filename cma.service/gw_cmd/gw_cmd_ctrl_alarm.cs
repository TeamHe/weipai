using ResModel;
using ResModel.gw;
using System;
using Tools;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_alarm : gw_cmd_base_ctrl
    {
        protected override bool WithReqSetFlag {  get { return true; } }

        protected override bool WithReqType {  get { return true; } }

        protected override bool WithRspStatus {  get { return true; } }

        protected override bool WithRspType {  get { return true; } }

        public gw_ctrl_alarm Alarm {  get; set; }

        public override int ValuesLength
        {
            get
            {
                if (Alarm == null || Alarm.Values == null)
                    return 1;
                else
                {
                    return 1+ 10*Alarm.Values.Count;
                }
            }
        }

        public override string Name { get { return "报警阈值"; } }

        public override int PType {  get { return 0xa6; } }

        public gw_cmd_ctrl_alarm(IPowerPole pole) : base(pole) { }

        public gw_cmd_ctrl_alarm() { }

        public void Update(gw_ctrl_alarm alarm)
        {
            this.Alarm = alarm;
            base.Update(alarm.ParaType);
        }

        public void Query(gw_ctrl_alarm alarm)
        {
            this.Alarm = alarm;
            base.Query(alarm.ParaType);
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            if(this.Alarm == null)
                this.Alarm = new gw_ctrl_alarm();
            FlushRespStatus(Alarm);
            if (data.Length - offset < 1)
                throw new Exception("数据缓冲区长度太小");
            int num = data[offset++];
            if(data.Length-offset < num*10)
                throw new Exception("数据缓冲区长度太小");
            for(int i=0;i<num;i++)
            {
                gw_ctrl_alarm_value value = new gw_ctrl_alarm_value();
                offset += gw_coding.GetString(data, offset, 6, out string str);
                value.Key = str;

                offset += gw_coding.GetSingle(data, offset, out float fval);
                value.Value = fval;

                Alarm.Values.Add(value);
            }
            msg = Alarm.ToString();
            return offset - start;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            msg = string.Empty;
            if(this.Alarm == null) 
                throw new ArgumentNullException(nameof(this.Alarm));

            data[offset++] = (byte)Alarm.Values.Count;
            if (Alarm.Values != null && Alarm.Values.Count > 0)
            {
                foreach(gw_ctrl_alarm_value val in  Alarm.Values)
                {
                    offset += gw_coding.SetString(data, offset, 6, val.Key);
                    offset += gw_coding.SetSingle(data, offset, val.Value);
                }
                msg = Alarm.ToString();
            }
            return offset - start;
        }
    }
}
