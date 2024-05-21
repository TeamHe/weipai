using ResModel;
using ResModel.gw;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_revival : gw_cmd_base_ctrl
    {
        protected override bool WithRspStatus {  get { return true; } }

        public override string Name { get { return "苏醒时间"; } }

        public override int PType { get { return 0xae; } }

        public override int ValuesLength { get { return 0x08; } }

        public gw_ctrl_revival Revival { get; set; }

        public gw_cmd_ctrl_revival() { }

        public gw_cmd_ctrl_revival(IPowerPole pole):base(pole) { }

        public void Update(gw_ctrl_revival revival)
        {
            this.Revival = revival;
            base.Update(revival);
        }


        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            msg = string.Empty;
            if (this.Revival != null)
            {
                offset += gw_coding.SetU32(data, offset, this.Revival.RevivalTime);
                offset += gw_coding.SetU16(data, offset, this.Revival.RevivalCycle);
                offset += gw_coding.SetU16(data, offset, this.Revival.DurationTime);
                msg = this.Revival.ToString();
            }
            else
                offset += this.ValuesLength;
            return offset - start;
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            msg = string.Empty;
            return 0;
        }
    }
}
