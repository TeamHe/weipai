using ResModel;
using ResModel.gw;
using System;
using System.Net;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_center : gw_cmd_base_ctrl
    {
        public override string Name { get { return "上位机信息"; } }

        public override int PType { get { return 0xa7; } }

        public override int ValuesLength { get { return 70; } }

        public gw_ctrl_center Center { get; set; }

        public gw_cmd_ctrl_center() { }

        public gw_cmd_ctrl_center(IPowerPole pole)
            :base(pole) { }

        public void Update(gw_ctrl_center center)
        {
            if(center == null) 
                throw new ArgumentNullException(nameof(Center));
            this.Center = center;
            base.Update(center);
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            if(data.Length - offset < this.ValuesLength)
                throw new ArgumentException("数据缓冲区长度太小");

            if (this.Center == null)
                this.Center = new gw_ctrl_center();
            this.FlushRespStatus(Center);

            int port;
            IPAddress ip;
            offset += gw_coding.GetIPAddress(data,offset,out ip);
            this.Center.IP = ip;
            offset += gw_coding.GetU16(data, offset, out port);
            this.Center.Port = port;
            offset += gw_coding.GetString(data, offset, 64, out string str);
            this.Center.Domain = str;

            msg = this.Center.ToString(this.RequestSetFlag == gw_ctrl.ESetFlag.Query);
            return offset - start;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            msg = string.Empty;
            if(this.Center != null)
            {
                offset += gw_coding.SetIPAddress(data, offset, this.Center.IP);
                offset += gw_coding.SetU16(data, offset, this.Center.Port);
                offset += gw_coding.SetString(data, offset, 64,this.Center.Domain);
                msg = this.Center.ToString(false);
            }
            return this.ValuesLength;
        }
    }
}
