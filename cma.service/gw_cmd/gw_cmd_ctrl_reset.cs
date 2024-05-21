using ResModel;
using ResModel.gw;
using System;
using Tools;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_reset : gw_cmd_base
    {

        public override string Name { get { return "装置复位"; } }

        public override int PType {  get { return 0xad; } }

        public override gw_frame_type SendFrameType { get { return gw_frame_type.Control; } }

        public override gw_frame_type RecvFrameType { get { return gw_frame_type.ResControl; } }

        public gw_ctrl_reset CtrlReset { get; set; }

        ///// <summary>
        ///// 结果: 成功，失败
        ///// </summary>
        public gw_ctrl.ESetStatus Status { get; set; }

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

        public override int decode(byte[] data, int offset, out string msg)
        {
            if (data.Length - offset < 1)
                throw new Exception("数据缓冲区长度太小");
            this.Status = (gw_ctrl.ESetStatus)data[offset++];

            msg = this.Name + EnumUtil.GetDescription(this.Status);
            return 1;
        }

        public override byte[] encode(out string msg)
        {
            if(this.CtrlReset == null)
                throw new ArgumentNullException(nameof(this.CtrlReset));
            byte[] data = new byte[1];
            data[0] = (byte)this.CtrlReset.Mode;
            msg = this.CtrlReset.ToString();
            return data;
        }
    }
}
