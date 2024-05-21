using System.ComponentModel;
using System.Text;
using Tools;

namespace ResModel.gw
{
    public class gw_ctrl_baseinfo:gw_ctrl
    {
        public enum InfoType {

            [Description("基本信息")]
            BaseInfo = 0x01,

            [Description("状态信息")]
            StatusInfo = 0x02,
        }

        public InfoType Type { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("信息类型:{0} ", this.Type.GetDescription());
            return sb.ToString();
        }
    }
}
