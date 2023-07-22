using ResModel;
using ResModel.nw;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_cmd_21_data : nw_cmd_base
    {
        public override int Control { get { return 0x21; } }

        public override string Name { get { return "主站请求装置数据"; } }

        /// <summary>
        /// 装置立刻采集所有数据
        /// </summary>
        public bool ImeData { get; set; }


        /// <summary>
        /// 是否为主站请求数据
        /// </summary>
        private bool Response { get; set; }

        public nw_cmd_21_data()
        {

        }

        public nw_cmd_21_data(IPowerPole pole) : base(pole)
        {

        }

        public override int Decode(out string msg)
        {
            msg = string.Empty;
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            if(this.ImeData)
            {
                byte[] data = new byte[2];
                data[0] = 0xbb;
                data[1] = 0xbb;
                msg = "装置立刻采集所有数据";
                return data;
            }else
            {
                msg = "上传未成功上传的历史数据";
                return null;
            }
        }
    }
}
