using ResModel;
using ResModel.gw;
using System;

namespace cma.service.gw_cmd
{
    public class gw_cmd_img_adjust : gw_cmd_img_base
    {
        public override int ValuesLength {  get { return 3; } }

        public override string Name { get { return "摄像头远程调节"; } }

        public override int PType {  get { return 0xd0; } }

        protected override bool WithRspStatus {  get { return true; } }

        public gw_img_adjust Adjust { get; set; }

        public gw_cmd_img_adjust() { }

        public gw_cmd_img_adjust(IPowerPole pole):base(pole) { }

        public void Action(gw_img_adjust adjust)
        {
            this.Adjust = adjust;
            this.Execute();
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            msg = string.Empty;
            return 0;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            int statrt = offset;
            if(this.Adjust == null)
                throw new ArgumentNullException(nameof(Adjust));
            data[offset++] = (byte)this.Adjust.ChNO;
            data[offset++] = (byte)this.Adjust.Preset;
            data[offset++] = (byte)this.Adjust.Action;
            msg = this.Adjust.ToString();
            return offset - statrt;
        }
    }
}
