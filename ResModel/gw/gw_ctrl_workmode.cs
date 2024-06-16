using System;

namespace ResModel.gw
{
    public class gw_ctrl_workmode : gw_ctrl
    {
        public enum EMode
        {
            安全初始化模式 = 1,
            密文通讯模式 = 2,
            明文通讯模式 = 3,
            工厂调测模式 = 4,
        }

        public EMode Mode { get; set; }

        public DateTime Time { get; set; }

        public gw_ctrl_workmode()
        {
            this.Time = DateTime.Now;
        }

        public override string ToString(bool flag)
        {
            return this.Mode.ToString();
        }
    }
}
