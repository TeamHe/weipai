using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Operation.RealData
{
    public class db_data_picture : db_base
    {
        protected override string Table_Name { get { return "t_data_picture"; } }

        public DataTable DataGet(string cmdid, DateTime start, DateTime end)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>
            {
                {"time",                "时间"},
                {"ChannalNO",           "通道"},
                {"Presetting_No",       "预置位"},
                {"Path",                "图片路径"},
            };
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("from {0} as d ", this.Table_Name);
            sb.Append("left join t_powerpole as pole on d.PoleID = pole.id ");
            sb.AppendFormat("where d.time between '{0:G}' and '{1:G}' and pole.CMD_ID = '{2}'",
                        start, end, cmdid);
            return base.DataGet(dics, sb.ToString());
        }

    }
}
