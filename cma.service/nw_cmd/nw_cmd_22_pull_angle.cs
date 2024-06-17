using DB_Operation.RealData;
using ResModel;
using ResModel.nw;
using System;
using System.Collections.Generic;
using ResModel.PowerPole;

namespace cma.service.nw_cmd
{
    public class nw_cmd_22_pull_angle : nw_cmd_base_data
    {
        public override int Control { get { return 0x22; } }

        public override string Name { get { return "导地线拉力风偏角数据"; } }

        public List<nw_data_22_pull_angle> values { get; set; }


        public nw_cmd_22_pull_angle()
        {
            this.HasUnitNo = true;
        }

        public nw_cmd_22_pull_angle(IPowerPole pole) : base(pole)
        {
            this.HasUnitNo = true;
        }

        protected override int DecodeValue(byte[] data, int offset)
        {
            nw_data_22_pull_angle angle = new nw_data_22_pull_angle()
            {
                DataTime = this.DataTime,
                UnitNo = this.UnitNO,
            };

            int ret = 0;
            if ((ret = angle.Decode(data, offset)) < 0)
                return ret;
            string msg1 = string.Empty;
            try
            {
                db_data_nw_pull_angle db = new db_data_nw_pull_angle(this.Pole);
                db.DataSave(angle);
            }
            catch (Exception ex)
            {
                msg1 = "数据存储失败" + ex.Message;
            }
            NewDataInfo(this.Pole, new PackageRecord(PackageRecord_RSType.rec, this.Pole,
                this.Name, angle.ToString()+ msg1));
            return ret;
        }
    }
}