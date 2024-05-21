using ResModel;
using ResModel.gw;
using System;
using Tools;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_reset : gw_cmd_base_ctrl
    {
        protected override bool WithRspStatus {  get { return true; } }

        public override string Name { get { return "装置复位"; } }

        public override int PType {  get { return 0xad; } }

        public gw_ctrl_reset CtrlReset { get; set; }

        public override int ValuesLength { get { return 1; } }

        public gw_cmd_ctrl_reset() { }

        public gw_cmd_ctrl_reset(IPowerPole pole )
            :base( pole ) { }

        public void Reset(gw_ctrl_reset.ResetMode mode)
        {
            this.CtrlReset = new gw_ctrl_reset()
            {
                Mode = mode,
            };
            this.Execute();
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            msg = string.Empty;
            return 0;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            data[offset++] = (byte)this.CtrlReset.Mode;
            msg = this.CtrlReset.ToString();
            return offset - start;        }
    }
}
