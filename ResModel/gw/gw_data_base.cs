using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ResModel.gw
{
    public abstract class gw_data_base
    {
        public gw_data_base() { }

        /// <summary>
        /// 被测设备ID
        /// </summary>
        public string Component_ID { get; set; }

        public int UnitNum { get; set;}

        public int UnitNo { get; set; }

        public DateTime DataTime { get; set; }

        public abstract int Decode(byte[] data, int offset, out string msg);

        public abstract byte[] Encode(out string msg);

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("被测设备ID:{0} 采集时间:{1} ", this.Component_ID, this.DataTime);
            if (this.UnitNum>0)
            {
                str.AppendFormat("采集单元总数:{0} 采集单元编号{1}", this.UnitNum, this.UnitNo);
            }
            return str.ToString();

        }
    }
}
