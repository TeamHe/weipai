using DB_Operation.RealData;
using DB_Operation;
using ResModel.gw;

namespace cma.service.gw_cmd
{
    public class gw_cmd_07_ice : gw_cmd_base_data
    {
        /// <summary>
        /// 报文类型
        /// </summary>
        public override int PType { get { return 0x07; } }

        public override string Name { get { return "覆冰数据报"; } }

        public override bool HasUnit { get { return false; } }

        public gw_data_ice Value { get; set; }


        public gw_cmd_07_ice() { }

        public override bool SaveData()
        {
            db_data_gw_ice db = new db_data_gw_ice(this.Pole);
            ErrorCode code = db.DataSave(this.Value);
            if (code == ErrorCode.NoError)
                return true;
            else
                return false;
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            this.Value = new gw_data_ice()
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
