using DB_Operation;
using DB_Operation.RealData;
using ResModel.gw;

namespace cma.service.gw_cmd
{
    public class gw_cmd_data_inclination : gw_cmd_base_data
    {
        public override bool HasUnit {  get { return false; } }

        public override string Name { get { return "杆塔倾斜数据报"; } }

        public override int PType {  get { return 0x0c; } }

        public gw_data_inclination Value { get; set; }


        public override bool SaveData()
        {
            db_data_gw_inclination db = new db_data_gw_inclination(this.Pole);
            ErrorCode code = db.DataSave(this.Value);
            if (code == ErrorCode.NoError)
                return true;
            else
                return false;
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            this.Value = new gw_data_inclination()
            {
                DataTime = this.DataTime,
                Component_ID = this.Component_ID,
            };
            int ret = Value.Decode(data, offset, out msg);
            if (ret > 0)
                msg += Value.ToString();
            return ret;
        }
    }
}
