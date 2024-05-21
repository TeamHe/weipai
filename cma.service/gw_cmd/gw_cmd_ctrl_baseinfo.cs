using ResModel;
using ResModel.gw;
using System;
using Tools;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_baseinfo : gw_cmd_base_ctrl
    {
        public override int ValuesLength {  get { return 1; } }

        public override string Name { get { return "基本信息上报"; } }

        public override int PType {  get { return 0xa8; } }

        public gw_ctrl_baseinfo BaseInfo { get; set; }

        public gw_cmd_ctrl_baseinfo() { }

        public gw_cmd_ctrl_baseinfo(IPowerPole pole)
            :base(pole) 
        { 
        }

        public void Query(gw_ctrl_baseinfo info)
        {
            this.BaseInfo = info;
            base.Query();
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            throw new NotImplementedException();
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            throw new NotImplementedException();
        }

        public override int decode(byte[] data, int offset, out string msg)
        {
            int start = offset;
            if(this.BaseInfo == null)
                this.BaseInfo = new gw_ctrl_baseinfo();
            if (data.Length - offset < 3)
                throw new Exception("数据缓冲区长度太小");

            this.Status = (gw_ctrl.ESetStatus)data[offset++];
            this.BaseInfo.Para_Type = (gw_para_type)data[offset++];
            this.BaseInfo.Type = (gw_ctrl_baseinfo.InfoType)data[offset++];

            msg = EnumUtil.GetDescription(this.Status) +
                  ". " + this.BaseInfo.ToString();
            return offset-start;
        }

        public override byte[] encode(out string msg)
        {
            byte[] data = new byte[3];
            int offset = 0;
            data[offset++] = (byte)this.RequestSetFlag;
            data[offset++] = (byte)this.BaseInfo.Para_Type;
            data[offset++] = (byte)this.BaseInfo.Type;

            msg = this.Name + ". " + this.BaseInfo.ToString();
            return data;
        }
    }
}
