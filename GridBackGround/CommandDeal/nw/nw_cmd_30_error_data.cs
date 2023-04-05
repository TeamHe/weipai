using GridBackGround.PacketAnaLysis;
using GridBackGround.Termination;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace GridBackGround.CommandDeal.nw
{
    /// <summary>
    /// 故障数据
    /// </summary>
    public class mw_error_data
    {
        public DateTime DataTime { get; set; }

        public int FunctionCode { get; set; }

        public int Code { get; set; }

        /// <summary>
        /// 故障类型: TRUE 表示故障恢复  FALSE 表示故障
        /// </summary>
        public bool Status { get; set; }

        public mw_error_data() { }

        public mw_error_data(DateTime dataTime, int functionCode, int errorCode)
        {
            DataTime = dataTime;
            FunctionCode = functionCode;
            Code = errorCode;
        }

        public override string ToString()
        {
            return string.Format("时间:{3} 功能编码:{0} 故障编码:{1} 故障状态:{2}",
                this.FunctionCode,
                this.Code,
                this.Status?"已恢复":"故障",
                this.DataTime);
        }
    }

    public class nw_cmd_30_error_data : nw_cmd_base
    {
        public override int Control { get { return 0x30; } }

        public override string Name { get { return "设备故障信息"; } }

        /// <summary>
        /// 帧标识
        /// </summary>
        private byte FrameFlag { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        private byte DevStatus { get; set; }

        /// <summary>
        /// 是否为主站请求数据
        /// </summary>
        private bool Response { get; set; }

        public nw_cmd_30_error_data()
        {

        }

        public nw_cmd_30_error_data(IPowerPole pole) : base(pole)
        {

        }


        /// <summary>
        /// 微气象数据内容解析
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="weather"></param>
        /// <returns></returns>
        public int Decode_Error(byte[] data, int offset, out mw_error_data error)
        {
            error = null;
            if (data.Length - offset < 2)
                return -1;
            int no = offset;
            error = new mw_error_data();
            error.FunctionCode = data[no++];
            byte err = data[no++];
            error.Code = err & 0x7f;
            if ((err & 0x80) > 0)
                error.Status = true;
            else
               error.Status = false;
            return no - offset;
        }

        public override int Decode(out string msg)
        {
            msg = null;
            if (this.Data == null || this.Data.Length == 0)
            {
                msg = "装置无未上送数据";
                return 0;
            }
            int offset = 4; //不验证密文信息，以及帧标识
            int ret = 0;
            this.FrameFlag = this.Data[offset++];
            int pnum = this.Data[offset++];
            this.DevStatus = this.Data[offset++];
            msg += string.Format("当前故障状态:{0}", DevStatus > 0x00 ? "故障" : "正常");
            offset += this.GetDateTime(this.Data, offset, out DateTime datatime);
            for (int i = 0; i < pnum; i++)
            {
                if ((ret = this.Decode_Error(this.Data, offset, out mw_error_data error)) < 0)
                {
                    msg = string.Format("第{0}包数据解析失败", i);
                    break;
                }

                offset += ret;
                error.DataTime = datatime;
                //显示数据
                DisPacket.NewRecord(new DataInfo(DataRecSendState.rec, this.Pole,
                    this.Name, error.ToString()));

                if (i == pnum - 1)
                    break;
                if ((this.Data.Length - offset) < 2)
                {
                    msg = string.Format("第{0}包数据长度错误", i + 1);
                    break;
                }

                offset += this.GetU16(this.Data, offset, out int period);
                datatime = datatime.AddSeconds(period);
            }
            this.Response = true;
            this.SendCommand(out string msg_send);
            msg += msg_send;
            return ret;

        }

        public override byte[] Encode(out string msg)
        {
            msg = string.Empty;
            if (this.Response)
            {
                byte[] data = new byte[3];
                data[0] = this.FrameFlag;
                data[1] = 0xaa;
                data[2] = 0x55;
                return data;
            }
            msg = "主站请求上传设备故障信息";
            return null;
        }
    }
}
