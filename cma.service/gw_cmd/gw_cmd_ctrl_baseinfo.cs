using ResModel;
using ResModel.gw;
using System;
using Tools;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_baseinfo : gw_cmd_base_ctrl
    {
        protected override bool WithReqSetFlag {  get { return true; } }

        protected override bool WithReqType {  get { return true; } }

        protected override bool WithRspStatus {  get { return true; } }

        protected override bool WithRspType {  get { return true; } }

        public override int ValuesLength {  get { return 1; } }

        public override string Name { get { return "基本信息"; } }

        public override int PType {  get { return 0xa8; } }

        public gw_ctrl_baseinfo BaseInfo { get; set; }

        public gw_cmd_ctrl_baseinfo() { }

        public gw_cmd_ctrl_baseinfo(IPowerPole pole)
            :base(pole) 
        { 
        }

        public void Query(gw_ctrl_baseinfo info)
        {
            if(info == null)
                throw new ArgumentNullException("info");
            this.BaseInfo = info;
            base.Query(info.ParaType);
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            if (this.BaseInfo == null)
                this.BaseInfo = new gw_ctrl_baseinfo();
            FlushRespStatus(BaseInfo);
            if (data.Length - offset < 1)
                throw new Exception("数据缓冲区长度太小");
            this.BaseInfo.Type = (gw_ctrl_baseinfo.InfoType)data[offset++];
            msg = BaseInfo.ToString();
            return offset - start;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            data[offset++] = (byte)this.BaseInfo.Type;
            msg = BaseInfo.ToString();
            return offset - start;
        }
    }
}
