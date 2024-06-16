using ResModel;
using ResModel.gw;
using System;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_workmode : gw_cmd_base_ctrl
    {

        protected override bool WithRspStatus {  get { return true; } }

        public override int ValuesLength { get { return 0x1c; } }

        public override string Name { get { return "装置复位"; } }

        public override int PType {  get { return 0x0c8; } }

        public gw_ctrl_workmode Mode { get; set; }

        public gw_cmd_ctrl_workmode(){ }

        public gw_cmd_ctrl_workmode(IPowerPole pole) :base(pole){ }

        public void Update(gw_ctrl_workmode mode)
        {
            this.Mode = mode;
            base.Update();
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            msg = string.Empty;
            return 0;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            if (this.Mode == null)
                throw new ArgumentNullException("Mode");
            int start = offset;
            offset += 20;
            for (int i = 0; i < 4; i++)
                data[offset++] = (byte)(this.Mode.Mode + 0xf0);
            offset += gw_coding.SetTime(data, offset, Mode.Time);
            msg = Mode.ToString();
            return offset - offset;
        }
    }
}
