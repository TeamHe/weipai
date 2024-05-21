using ResModel;
using ResModel.gw;
using System;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_id : gw_cmd_base_ctrl
    {
        public override int ValuesLength {  get { return 51; } }

        public override string Name { get { return "装置ID"; } }

        public override int PType { get { return 0xac; } }

        public gw_ctrl_id ID { get; set; }

        public gw_cmd_ctrl_id() { }

        public gw_cmd_ctrl_id(IPowerPole pole)
            : base(pole) { }

        public void Update(gw_ctrl_id id)
        {
            if(id == null)
                 throw new ArgumentNullException("id");
            this.ID = id;
            base.Update(id);
        }
        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            if(this.ID == null)
                this.ID =new gw_ctrl_id();
            this.FlushRespStatus(this.ID);

            string str;
            offset += gw_coding.GetString(data, offset, 17, out str);
            ID.ComponentID = str;

            offset += gw_coding.GetString(data, offset, 17, out str);
            ID.OriginalID = str;

            offset += gw_coding.GetString(data, offset, 17, out str);
            ID.NEW_CMD_ID = str;

            msg = this.ID.ToString(this.RequestSetFlag == gw_ctrl.ESetFlag.Query);

            return this.ValuesLength;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            msg = string.Empty;
            if(this.ID != null)
            {
                offset += gw_coding.SetString(data, offset, 17, ID.ComponentID);
                offset += gw_coding.SetString(data, offset, 17, ID.OriginalID);
                offset += gw_coding.SetString(data, offset, 17, ID.NEW_CMD_ID);
                msg = this.ID.ToString(false);
            }
            return this.ValuesLength;
        }
    }
}
