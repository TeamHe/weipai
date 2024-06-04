using ResModel;
using ResModel.gw;

namespace cma.service.gw_cmd
{
    public abstract class gw_cmd_img_base:gw_cmd_base_ctrl
    {

        public override gw_frame_type SendFrameType { get { return gw_frame_type.ControlImage; } }

        public override gw_frame_type RecvFrameType { get { return gw_frame_type.ResControlImage; } }

        public gw_cmd_img_base() { }

        public gw_cmd_img_base(IPowerPole pole):base(pole) { }
    }
}
