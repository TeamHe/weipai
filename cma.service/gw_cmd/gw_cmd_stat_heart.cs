using ResModel.gw;
using System;

namespace cma.service.gw_cmd
{
    public class gw_cmd_stat_heart : gw_cmd_stat_base
    {
        public override string Name { get { return "心跳数据报"; } }

        public override int PType { get { return 0xe6; } }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime Time { get; set; }

        public override int decode(byte[] data, int offset, out string msg)
        {
            int start = offset;
            if(data.Length -offset < 4)
                throw new Exception("数据缓冲区长度太小");

            offset += gw_coding.GetTime(data, offset, out DateTime time);
            this.Time = time;
            msg = string.Format("当前时间:{0}", this.Time);
            return offset - start;
        }

        public override byte[] encode(out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
