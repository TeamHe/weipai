using ResModel;

namespace cma.service.gw_cmd
{
    public class gw_cmd_img_photo : gw_cmd_img_base
    {
        public override string Name { get { return "手动请求照片"; } }

        public override int PType { get { return 0xcb; } }

        public override int ValuesLength { get { return 0x02; } }

        protected override bool WithRspStatus {  get { return true; } }

        /// <summary>
        /// 通道号
        /// </summary>
        public int ChNO { get; set; }

        public int Preset {  get; set; }

        public gw_cmd_img_photo() { }

        public gw_cmd_img_photo(IPowerPole pole):base(pole) { }

        /// <summary>
        /// 拍摄照片
        /// </summary>
        /// <param name="chno"></param>
        /// <param name="preset"></param>
        public void Photo(int chno, int preset)
        {
            this.ChNO = chno;
            this.Preset = preset;
            this.Execute();
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            msg = string.Empty;
            return 0;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            data[offset++] = (byte)this.ChNO;
            data[offset++] = (byte)this.Preset;
            msg = string.Format("通道号:{0} 预置位号:{1} ",this.ChNO,this.Preset);
            return this.ValuesLength;
        }
    }
}
