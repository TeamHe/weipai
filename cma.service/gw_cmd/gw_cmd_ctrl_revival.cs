using ResModel;
using ResModel.gw;
using System;
using Tools;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_revival : gw_cmd_base_ctrl
    {
        public override string Name { get { return "苏醒时间"; } }

        public override int PType { get { return 0xae; } }

        public override int ValuesLength { get { return 0x08; } }

        public gw_ctrl_revival Revival { get; set; }

        public gw_cmd_ctrl_revival() { }

        public gw_cmd_ctrl_revival(IPowerPole pole):base(pole) { }

        public override int decode(byte[] data, int offset, out string msg)
        {
            int start  = offset;
            if(data.Length - offset <1)
                throw new Exception("数据缓冲区长度太小");
            this.Status = (gw_ctrl.Status)data[offset++];
            msg = "设置"  + this.Status.GetDescription();
            return offset - start;
        }

        public void Update(gw_ctrl_revival revival)
        {
            this.Revival = revival;
            base.Update(revival);
        }

        public override byte[] encode(out string msg)
        {
            msg = string.Empty;
            byte[] data = new byte[ValuesLength];
            if(this.Revival != null)
            {
                int offset = 0;
                offset += gw_coding.SetU32(data, offset, this.Revival.RevivalTime);
                offset += gw_coding.SetU16(data, offset, this.Revival.RevivalCycle);
                offset += gw_coding.SetU16(data, offset, this.Revival.DurationTime);
                msg = this.Revival.ToString();
            }
            return data;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            throw new NotImplementedException();
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
