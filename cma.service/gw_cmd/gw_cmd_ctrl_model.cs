using ResModel;
using ResModel.gw;
using System;
using Tools;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_model : gw_cmd_base_ctrl
    {
        protected override bool WithReqSetFlag {  get { return true; } }

        protected override bool WithRspStatus {  get { return true; } }

        protected override bool WithRspSetFlag {  get { return true; } }

        public override int ValuesLength { 
            get
            {
                if(this.Models == null || this.Models.Models.Count == 0)
                    return 1;
                return 1 + 11 * this.Models.Models.Count;
            }
        }

        public override string Name { get { return "配置信息"; } }

        public override int PType {  get { return 0xa5; } }

        public gw_ctrl_models Models { get; set; }

        public gw_cmd_ctrl_model() { }

        public gw_cmd_ctrl_model(IPowerPole pole):base(pole) { }

        public void Update(gw_ctrl_models models)
        {
            this.Models = models;
            this.Update();
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            int num = (int)data[offset++];
            if(this.Models == null)
                this.Models = new gw_ctrl_models();
            if(data.Length - offset < num*11)
                throw new Exception("数据缓冲区长度太小");
            for(int i=0;i < num;i++)
            {
                gw_ctrl_model model = new gw_ctrl_model();
                offset += gw_coding.GetString(data, offset, 6, out string key);
                model.Key = key;

                model.Type = (gw_ctrl_model.EType)data[offset++];
                switch(model.Type)
                {
                    case gw_ctrl_model.EType.S32:
                        offset += gw_coding.GetS32(data, offset, out int val);
                        model.Value = val;
                        break;
                    case gw_ctrl_model.EType.U32:
                        offset += gw_coding.GetU32(data, offset, out UInt32 uval);
                        model.Value = uval;
                        break;
                    case gw_ctrl_model.EType.Single:
                        offset += gw_coding.GetSingle(data, offset, out float fval);
                        model.Value = fval;
                        break;
                }
                this.Models.Models.Add(model);
            }
            msg = this.Models.ToString();
            return offset - start;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            int start= offset;
            int num = 0;
            msg = string.Empty;
            if(Models != null && this.Models.Models.Count > 0)
                num = this.Models.Models.Count;
            data[offset++] = (byte)num;
            if(num > 0)
            {
                foreach (var model in Models.Models)
                {
                    offset += gw_coding.SetString(data, offset, 6, model.Key);
                    data[offset++] = (byte)model.Type;
                    switch (model.Type)
                    {
                        case gw_ctrl_model.EType.S32:
                            offset += gw_coding.SetS32(data, offset, (int)model.Value);
                            break;
                        case gw_ctrl_model.EType.U32:
                            offset += gw_coding.SetU32(data, offset, (UInt32)model.Value);
                            break;
                        case gw_ctrl_model.EType.Single:
                            offset += gw_coding.SetSingle(data, offset, (float)model.Value);
                            break;
                    }
                }
                if (num > 0)
                    msg = Models.ToString();
            }
            return offset - start;
        }
    }
}
