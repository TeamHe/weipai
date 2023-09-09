using System;
using ResModel.gw;
using DB_Operation;
using DB_Operation.RealData;

namespace cma.service.gw_cmd
{
    public class gw_cmd_01_weather : gw_cmd_base_data
    {
        /// <summary>
        /// 报文类型
        /// </summary>
        public override int PType { get { return 0x01; } }

        public override string Name { get { return "气象数据报"; } }

        public override bool HasUnit { get { return false; } }

        public gw_data_weather Value { get; set; }


        public gw_cmd_01_weather() { }

        public override bool SaveData()
        {
            db_data_gw_weather db = new db_data_gw_weather(this.Pole);
            ErrorCode code = db.DataSave(this.Value);
            if (code == ErrorCode.NoError)
                return true;
            else
                return false;
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            this.Value = new gw_data_weather()
            {
                DataTime = this.DataTime,
                Component_ID = this.Component_ID,
            };
            int ret= Value.Decode(data, offset, out msg); 
            if(ret > 0)
                msg += Value.ToString();
            return ret;
        }
    }
}
