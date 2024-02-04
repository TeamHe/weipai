using System.ComponentModel;
using System.Text;

namespace ResModel.gw
{
    public class gw_ctrl_id : gw_ctrl
    {
        public enum EFlag
        {
            [Description("新装置ID")]
            NEW_CMD_ID = 0,

            [Description("被测设备ID")]
            ComponentID = 1,
        }

        public string ComponentID {  get; set; }

        public string NEW_CMD_ID {  get; set; }

        public string OriginalID {  get; set; }

        public override string ToString(bool flag)
        {
            StringBuilder sb = new StringBuilder();
            if (flag || this.GetFlag((int)EFlag.NEW_CMD_ID))
                sb.AppendFormat("新装置ID:{0} ", this.NEW_CMD_ID);
            if (flag || this.GetFlag((int)EFlag.ComponentID))
                sb.AppendFormat("被测装置ID:{0} ", this.ComponentID);
            sb.AppendFormat("原始ID:{0} ", this.OriginalID);
            return sb.ToString();
        }
    }
}
