using ResModel.gw;
using System;

namespace cma.service.gw_cmd
{
    public class gw_cmd_stat_error : gw_cmd_stat_base
    {
        public override string Name { get { return "工作状态报"; } }

        public override int PType { get { return 0xe9; } }

        public gw_stat_error error { get; set; }

        public override int decode(byte[] data, int offset, out string msg)
        {
            int start = offset;
            if (data.Length - offset < 5)
                throw new Exception("数据缓冲区长度太小");
            this.error = new gw_stat_error();
            offset += gw_coding.GetTime(data,offset,out DateTime time);
            error.Time = time;
            offset += gw_coding.GetString(data, offset, data.Length - offset, out string str);
            error.Desc = str;
            msg = error.ToString();
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
