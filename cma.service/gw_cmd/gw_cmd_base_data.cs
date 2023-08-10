using System;
using System.Text;
using Tools;
using ResModel.gw;

namespace cma.service.gw_cmd
{
    public abstract class gw_cmd_base_data:gw_cmd_base
    {
        /// <summary>
        /// 被测设备ID
        /// </summary>
        public string Component_ID { get; set; }


        public override gw_frame_type SendFrameType { get { return gw_frame_type.ResMonitoring; } }

        public override gw_frame_type RecvFrameType { get { return gw_frame_type.Monitoring; } }


        /// <summary>
        /// 数据时间
        /// </summary>
        public DateTime DataTime { get; set; }

        /// <summary>
        /// 采集单元总数
        /// </summary>
        public int UnitNum { get; set; }

        /// <summary>
        /// 采集单元序号
        /// </summary>
        public int UnitNo { get; set; }

        public abstract bool HasUnit { get; }

        public bool Data_Status { get; set; }

        public virtual bool SaveData()
        {
            throw new Exception("当前数据类型不支持数据存储");
        }

        public abstract int DecodeData(byte[] data, int offset, out string msg);

        public override int decode(byte[] data, int offset,out string msg)
        {
            msg = null;
            int min_length = 20;
            int start = offset;
            if (this.HasUnit) min_length += 2;
            if (data == null || (data.Length-offset) < min_length)
                throw new Exception("数据内容长度错误");

            this.Component_ID = Encoding.Default.GetString(data, offset, 17);
            offset += 17;

            if(this.HasUnit)
            {
                this.UnitNum = data[offset++];
                this.UnitNo = data[offset++];
            }
            this.DataTime = TimeUtil.BytesToDate(data, offset);
            offset += 4;

            
            int ret = 0;
            if ((ret = this.DecodeData(data, offset, out string msg_data)) < 0)
                return -1;

            this.Data_Status = true;
            msg = msg_data;
            try
            {
                this.SaveData();
                this.Data_Status = true;
            }
            catch (Exception ex)
            {
               msg += " 数据存储错误:" + ex.Message;
            }
            this.Execute();
            return 0;
        }

        public override byte[] encode(out string msg)
        {
            msg = null;
            byte[] rsp = new byte[1];
            if (this.Data_Status)
                rsp[0] = 0xff;
            else
                rsp[0] = 0x00;
            return rsp;
        }
    }
}
