using ResModel.nw;
using System;

namespace cma.service.nw_cmd
{
    internal class nw_cmd_cb_update_data : nw_cmd_base
    {
        public override int Control { get { return 0xcb; } }

        public override string Name { get { return "下发升级文件"; } }

        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannoNo { get; set; }

        /// <summary>
        /// 子包包号
        /// </summary>
        public int PNO { get; set; }


        /// <summary>
        /// 数据
        /// </summary>
        public byte[] UpdateData { get; set; }

        public override int Decode(out string msg)
        {
            throw new NotImplementedException();
        }

        public override byte[] Encode(out string msg)
        {
            if(this.UpdateData == null || this.UpdateData.Length == 0)
                throw new ArgumentNullException();
            byte[] data = new byte[5+ this.UpdateData.Length];
            int offset = 0;
            data[offset++] = (byte)(this.ChannoNo);
            offset += SetU32(data, offset, (uint)this.PNO);
            Buffer.BlockCopy(this.UpdateData, 0, data,offset, this.UpdateData.Length);
            msg = string.Format("通道号:{0} 子包包号:{1}", this.ChannoNo, this.PNO);
            return data;
        }
    }
}
