using ResModel;
using ResModel.gw;
using System;
using Tools;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_alarm : gw_cmd_base_ctrl
    {
        public gw_ctrl_alarm Alarm {  get; set; }

        public override int ValuesLength
        {
            get
            {
                if (Alarm == null || Alarm.Values == null)
                    return 2;
                else
                {
                    return 2+ 10*Alarm.Values.Count;
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
            base.Update();
        }

        public void Query(gw_ctrl_alarm alarm)
        {
            this.Alarm = alarm;
            base.Query();
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            if(this.Alarm == null)
                this.Alarm = new gw_ctrl_alarm();
            if (data.Length - offset <2)
                throw new Exception("数据缓冲区长度太小");
            Alarm.Type = (gw_para_type)data[offset++];
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
            if(this.Alarm == null) 
                throw new ArgumentNullException(nameof(this.Alarm));
            msg = string.Empty;
            int start = offset;

            data[offset++] = (byte)Alarm.Type;
            data[offset++] = (byte)Alarm.Values.Count;
            if (Alarm.Values != null && Alarm.Values.Count > 0)
            {
                foreach(gw_ctrl_alarm_value val in  Alarm.Values)
                {
                    offset += gw_coding.SetString(data, offset, 6, val.Key);
                    offset += gw_coding.SetSingle(data, offset, val.Value);
                }
            }
            msg = Alarm.ToString();
            return offset - start;
        }

        public override int decode(byte[] data, int offset, out string msg)
        {
            if (data.Length - offset < 1)
                throw new Exception("数据缓冲区长度太小");

            this.Status = (gw_ctrl.ESetStatus)data[offset++];
            int ret = this.DecodeData(data, offset, out string str);
            msg = EnumUtil.GetDescription(this.Status) + ". " + str;
            return ret;
        }

        public override byte[] encode(out string msg)
        {
            int offset = 0;
            byte[] data = new byte[1 + this.ValuesLength];
            data[offset++] = (byte)this.RequestSetFlag;
            this.EncodeData(data, offset, out string str);
            msg = EnumUtil.GetDescription(this.RequestSetFlag) + this.Name + ". " + str;
            return data;
        }
    }
}
