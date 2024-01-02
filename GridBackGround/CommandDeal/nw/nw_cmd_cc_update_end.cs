using ResModel.nw;
using System;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_cmd_cc_update_end : nw_cmd_base
    {
        public override int Control { get { return 0xcc; } }

        public override string Name { get { return "升级结束包"; } }

        public int ChannelNo { get; set; }

        public override int Decode(out string msg)
        {
            throw new NotImplementedException();
        }

        public override byte[] Encode(out string msg)
        {
            byte[] data = new byte[1];
            data[0] = (byte)ChannelNo;
            msg = string.Format("通道号:{0}", this.ChannelNo);
            return data;
        }
    }
}
