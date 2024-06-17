using System;
using System.Collections.Generic;
using ResModel;
using ResModel.nw;
using ResModel.PowerPole;

namespace cma.service.nw_cmd
{
    /// <summary>
    /// 南网数据包: 装置流量数据使用情况 处理
    /// </summary>
    public class nw_cmd_40_data_traffic : nw_cmd_base_data
    {
        public override int Control { get { return 0x40; } }

        public override string Name { get { return "装置流量数据"; } }


        protected DateTime Time { get; set; }

        public List<nw_data_40_traffic> values { get; set; }

        public nw_cmd_40_data_traffic()
        {
            this.values = new List<nw_data_40_traffic> { };
        }

        public nw_cmd_40_data_traffic(IPowerPole pole) : base(pole)
        {
            this.values = new List<nw_data_40_traffic> { };
        }


        protected override int DecodeValue(byte[] data, int offset)
        {
            ///TODO: Save this data to database
            nw_data_40_traffic value = new nw_data_40_traffic() { DataTime = this.DataTime };
            int ret= value.Decode(this.Data, offset);
            //显示数据
            NewDataInfo(this.Pole, new PackageRecord(PackageRecord_RSType.rec, this.Pole,
                this.Name, value.ToString()));
            return ret;
        }
    }
}
