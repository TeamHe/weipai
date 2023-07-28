using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResModel.nw
{
    /// <summary>
    /// 南网实时数据: 装置流量数据类
    /// </summary>
    public class nw_data_40_traffic : nw_data_base
    {
        /// <summary>
        /// 今日已用流量
        /// </summary>
        public uint today_used { get; set; }

        /// <summary>
        /// 当月已用流量
        /// </summary>
        public uint mouth_used { get; set; }

        /// <summary>
        /// 当月剩余流量
        /// </summary>
        public uint mouth_left { get; set; }

        /// <summary>
        /// 数据部分最小长度
        /// </summary>
        public override int PackLength { get { return 12; } }

        /// <summary>
        /// 数据包解析
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public override int Decode(byte[] data, int offset)
        {
            int no = offset;
            uint traffic = 0;
            //今日已用流量
            no += nw_cmd_base.GetU32(data,offset,out traffic);
            this.today_used = traffic;

            no += nw_cmd_base.GetU32(data, no, out traffic);
            this.mouth_used = traffic;

            no += nw_cmd_base.GetU32(data, no, out traffic);
            this.mouth_left = traffic;
            return no - offset;
        }

        public override int Encode(byte[] data, int offset)
        {
            offset += nw_cmd_base.SetU32(data, offset, this.today_used);
            offset += nw_cmd_base.SetU32(data, offset, this.mouth_used);
            nw_cmd_base.SetU32(data, offset, this.mouth_left);
            return 12;
        }

        public override string ToString()
        {
            return string.Format("时间:{0:G} 今日已用流量:{1}MB " +
                     "当月已用流量{2}MB  当月剩余流量{3}MB",
                     this.DataTime, this.today_used,
                     this.mouth_used, this.mouth_left);
        }
    }
}
