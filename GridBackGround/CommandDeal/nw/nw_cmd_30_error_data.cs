using ResModel;
using System;
using ResModel.PowerPole;
using ResModel.nw;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_cmd_30_error_data : nw_cmd_base_data
    {
        public override int Control { get { return 0x30; } }

        public override string Name { get { return "设备故障信息"; } }

        /// <summary>
        /// 设备状态
        /// </summary>
        private byte DevStatus { get; set; }


        public nw_cmd_30_error_data()
        {

        }

        public nw_cmd_30_error_data(IPowerPole pole) : base(pole)
        {

        }

        protected override int DecodeValue(byte[] data, int offset)
        {
            ///TODO: Save this data to database
            nw_data_30_error value = new nw_data_30_error() { DataTime = this.DataTime };
            int ret = value.Decode(this.Data, offset);
            //显示数据
            NewDataInfo(this.Pole, new PackageRecord(PackageRecord_RSType.rec, this.Pole,
                this.Name, value.ToString()));
            return ret;

        }

        protected override int ExtraDecode(byte[] data, int offset, out string msg)
        {
            this.DevStatus = this.Data[offset++];
            msg = string.Format("当前故障状态:{0}", DevStatus > 0x00 ? "故障" : "正常");
            return 1;
        }
    }
}
