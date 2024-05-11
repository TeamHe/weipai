using System;
using Tools;
using ResModel;
using ResModel.gw;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_history : gw_cmd_base
    {
        public override string Name { get { return "请求历史数据"; } }

        public override int PType { get { return 0xa3; } }

        public override gw_frame_type SendFrameType { get { return gw_frame_type.Control; } }

        public override gw_frame_type RecvFrameType { get { return gw_frame_type.ResControl; } }

        public int ValuesLength { get { return 9; } }

        /// <summary>
        /// 查询/设置 结果: 成功，失败
        /// </summary>
        public gw_ctrl.Status Status { get; set; }


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

        public override int decode(byte[] data, int offset, out string msg)
        {
            if(data.Length - offset < 2)
                throw new Exception("数据缓冲区长度太小");
            if (this.History == null)
                History = new gw_ctrl_history();
            this.Status = (gw_ctrl.Status)data[offset++];
            History.Result = this.Status;
            History.Type = (gw_func_code)data[offset++];
            msg = string.Format("请求历史数据{1} 数据类型:{0}",
                EnumUtil.GetDescription(History.Type),
                EnumUtil.GetDescription(History.Result));
            return 2;
            throw new NotImplementedException();
        }

        public override byte[] encode(out string msg)
        {
            if(this.History == null)
                throw new ArgumentNullException(nameof(this.History));
            if (History.Start > History.End)
                throw new Exception("起始时间不能大于结束时间");

            byte[] data = new byte[this.ValuesLength];
            int offset = 0;
            data[offset++] = (byte)this.History.Type;
            offset += gw_coding.SetTime(data, offset, History.Start);
            offset += gw_coding.SetTime(data, offset, History.End);
            
            msg = History.ToString();
            return data;
        }
    }
}
