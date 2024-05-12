using ResModel.gw;

namespace cma.service.gw_cmd
{
    public abstract class gw_cmd_stat_base : gw_cmd_base
    {
        public override gw_frame_type SendFrameType { get { return gw_frame_type.ResWorkState; } }

        public override gw_frame_type RecvFrameType { get { return gw_frame_type.WorkState; } }

    }
}
