using System.ComponentModel;
using Tools;

namespace ResModel.gw
{
    public    class gw_ctrl_reset:gw_ctrl
    {
        public enum ResetMode
        {
            [Description("常规复位")]
            Normal = 0,

            [Description("调试模式")]
            Debug,
        }

        public ResetMode Mode {  get; set; }

        public override string ToString()
        {
            return string.Format("复位模式:{0}",EnumUtil.GetDescription(Mode));
        }
    }
}
