using ResModel;
using ResModel.gw;
using System;
using System.Net;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_adapter : gw_cmd_base_ctrl
    {
        public override string Name { get { return "网络适配器"; } }

        public override int PType { get { return 0xa2; } }

        public gw_ctrl_adapter Adapter { get; set; }

        /// <summary>
        /// 数据区域长度
        /// IP: 4 子网掩码:4 网关:4,DNS:4 ,手机串号:20
        /// </summary>
        public override int ValuesLength { get { return 36; } }

        public gw_cmd_ctrl_adapter() { }

        public gw_cmd_ctrl_adapter(IPowerPole pole)
            :base(pole) { }

        public void Update(gw_ctrl_adapter adapter)
        {
            if(adapter == null)
                throw new ArgumentNullException("Adapter 不能为空");
            this.Adapter = adapter;
            this.Update(adapter as gw_ctrl);
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            if (this.Adapter == null)
                this.Adapter = new gw_ctrl_adapter();
            FlushRespStatus(this.Adapter);

            IPAddress address;
            offset += gw_coding.GetIPAddress(data, offset, out address);
            Adapter.IP = address;

            offset += gw_coding.GetIPAddress(data, offset, out address);
            Adapter.Mask = address;

            offset += gw_coding.GetIPAddress(data, offset, out address);
            Adapter.GateWay = address;

            offset += gw_coding.GetIPAddress(data, offset, out address);
            Adapter.DNS = address;

            offset += gw_coding.GetString(data, offset, 20, out string str);
            Adapter.PhoneNumber = str;

            msg = Adapter.ToString(this.RequestSetFlag == gw_ctrl.ESetFlag.Query);
            return 0;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            msg = "";
            if (this.Adapter != null) {
                offset += gw_coding.SetIPAddress(data, offset, Adapter.IP);
                offset += gw_coding.SetIPAddress(data, offset, Adapter.Mask);
                offset += gw_coding.SetIPAddress(data, offset, Adapter.GateWay);
                offset += gw_coding.SetIPAddress(data, offset, Adapter.DNS);
                offset += gw_coding.SetString(data, offset, 20, Adapter.PhoneNumber);
                if (this.RequestSetFlag == gw_ctrl.ESetFlag.Set)
                    msg = this.Adapter.ToString(false);

            }
            return this.ValuesLength;
        }
    }
}
