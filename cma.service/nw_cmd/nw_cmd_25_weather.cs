using System;
using ResModel.nw;
using ResModel;
using DB_Operation.RealData;
using ResModel.PowerPole;

namespace cma.service.nw_cmd
{
    /// <summary>
    /// 气象数据包指令处理
    /// </summary>
    public class nw_cmd_25_weather : nw_cmd_base_data
    {
        public override int Control { get { return 0x25; } }

        public override string Name { get { return "气象数据"; } }

        public nw_cmd_25_weather()
        {

        }

        public nw_cmd_25_weather(IPowerPole pole) : base(pole)
        {
            
        }

        protected override int DecodeValue(byte[] data, int offset)
        {
            nw_data_25_weather weather = new nw_data_25_weather()
            {
                DataTime = this.DataTime,
            };
            int ret = 0;
            if ((ret = weather.Decode(data, offset)) < 0)
                return ret;
            string msg1 = string.Empty;
            try
            {
                db_data_nw_weather db = new db_data_nw_weather(this.Pole);
                db.DataSave(weather);
            }
            catch (Exception ex)
            {
                msg1 = "数据存储失败" + ex.Message;
            }
            NewDataInfo(this.Pole, new PackageRecord(PackageRecord_RSType.rec, this.Pole,
                this.Name, weather.ToString() + msg1));
            return ret;
        }
    }
}
