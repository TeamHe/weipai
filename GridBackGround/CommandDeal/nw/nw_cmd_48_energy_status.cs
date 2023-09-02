using System.Collections.Generic;
using ResModel;
using ResModel.nw;
using ResModel.PowerPole;

namespace GridBackGround.CommandDeal.nw
{
    /// <summary>
    /// 南网数据包: 工作电能量状态数据 处理
    /// </summary>
    public class nw_cmd_48_energy_status : nw_cmd_base_data
    {
        public override int Control { get { return 0x48; } }

        public override string Name { get { return "工作电能量状态数据"; } }

        public nw_cmd_48_energy_status()
        {
        }

        public nw_cmd_48_energy_status(IPowerPole pole) : base(pole)
        {
        }

        protected override int DecodeValue(byte[] data, int offset)
        {
            nw_data_48_energy_status value = new nw_data_48_energy_status() { DataTime = this.DataTime };
            int ret = value.Decode(this.Data, offset);
            //显示数据
            NewDataInfo(this.Pole, new PackageRecord(PackageRecord_RSType.rec, this.Pole,
                this.Name, value.ToString()));
            return ret;
        }
    }
}
