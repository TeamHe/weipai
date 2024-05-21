using System;
using Tools;
using ResModel;
using ResModel.gw;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_history : gw_cmd_base_ctrl
    {
        protected override bool WithRspStatus {  get { return true; } }
        public override string Name { get { return "请求历史数据"; } }

        public override int PType { get { return 0xa3; } }

        public override int ValuesLength { get { return 9; } }

        public gw_ctrl_history History { get; set; }

        public gw_cmd_ctrl_history() { }

        public gw_cmd_ctrl_history(IPowerPole pole)
            :base(pole) { }

        public void Query(gw_ctrl_history history)
        {
            if(history == null)
                throw new ArgumentNullException(nameof(history));
            this.History = history;
            this.Execute();
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            if (data.Length - offset < 1)
                throw new Exception("数据缓冲区长度太小");
            if (this.History == null)
                History = new gw_ctrl_history();
            FlushRespStatus(History);
            History.Type = (gw_func_code)data[offset++];
            msg = string.Format("数据类型:{0}", History.Type.GetDescription());
            return 1;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            if(this.History == null)
                throw new ArgumentNullException(nameof(this.History));
            if (History.Start > History.End)
                throw new Exception("起始时间不能大于结束时间");

            data[offset++] = (byte)this.History.Type;
            if (History.End > History.Start)
            {
                offset += gw_coding.SetTime(data, offset, History.Start);
                offset += gw_coding.SetTime(data, offset, History.End);
            }
            else
                offset += 8;

            msg = History.ToString();
            return offset-start;
        }
    }
}
