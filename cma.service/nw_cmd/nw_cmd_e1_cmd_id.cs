using ResModel;
using System;
using System.Text;
using ResModel.nw;

namespace cma.service.nw_cmd
{
    public class nw_cmd_e1_cmd_id : nw_cmd_base
    {
        public override int Control { get { return 0xe1; } }

        public override string Name { get { return "设置装置ID"; } }

        public string Password { get; set; }

        /// <summary>
        /// 新装置ID
        /// </summary>
        public string NewID { get; set; }

        public nw_cmd_e1_cmd_id()
        {

        }

        public nw_cmd_e1_cmd_id(IPowerPole pole) : base(pole)
        {

        }


        public override int Decode(out string msg)
        {
            throw new NotImplementedException();
        }

        public override byte[] Encode(out string msg)
        {
            if (Password == null || this.Password.Length != 4)
                throw new ArgumentNullException("密码");
            if (NewID == null || this.NewID.Length != 6)
                throw new ArgumentNullException("新装置ID");
            byte[] data = new byte[10];
            int offset = this.SetPassword(data, 0, this.Password);
            byte[] b_id = Encoding.ASCII.GetBytes(NewID);
            Buffer.BlockCopy(b_id, 0, data, offset, b_id.Length >= 6 ? 6 : b_id.Length);
            msg = string.Format("新装置ID:" + this.NewID);
            return data;
        }
    }
}
