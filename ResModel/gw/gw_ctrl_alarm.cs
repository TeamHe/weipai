using System.Collections.Generic;
using System.Text;
using Tools;

namespace ResModel.gw
{
    public class gw_ctrl_alarm_value
    {
        public string Name { get; set; }

        public string Key { get; set; } 

        public float Value { get; set; }
    }

    public class gw_ctrl_alarm : gw_ctrl
    {
        /// <summary>
        /// 报警参数
        /// </summary>
        public List<gw_ctrl_alarm_value> Values { get; set; }

        public gw_ctrl_alarm()
        {
            this.Values = new List<gw_ctrl_alarm_value>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("报警阈值:");
            foreach (var val in Values)
            {
                sb.AppendFormat("{0}:{1} ", string.IsNullOrEmpty(val.Name) ? val.Key : val.Name,val.Value);
            }
            return sb.ToString();
        }
    }
}
