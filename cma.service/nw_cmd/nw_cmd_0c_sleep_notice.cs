using System;
using ResModel.nw;

namespace cma.service.nw_cmd
{
    public class nw_cmd_0c_sleep_notice : nw_cmd_base
    {
        public override int Control { get { return 0x0c; } }

        public override string Name { get { return "装置休眠通知"; } }

        public override int Decode(out string msg)
        {
            msg = "装置进入休眠状态"; 
            PowerPole pole = this.Pole as PowerPole;
            pole.SetOnlineState(ResModel.EQU.OnLineStatus.Sleep);
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
