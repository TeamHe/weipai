using ResModel;
using ResModel.gw;
using System;

namespace cma.service.gw_cmd
{
    public class gw_cmd_img_para : gw_cmd_img_base
    {
        /// <summary>
        /// 控制数据包，payload 有效数据长度
        /// color_select: 1 Reolution: 1 Luminance: 1 Contrast: 1 Saturation:1
        /// </summary>
        public override int ValuesLength {  get { return 5; } }

        public override string Name { get { return "图像采集参数"; } }

        public override int PType {  get { return 0xc9; } }

        /// <summary>
        /// 请求带有参数配置类型标识
        /// </summary>
        protected override bool WithReqSetFlag {  get { return true; } }
        
        /// <summary>
        /// 请求包含标识位字段
        /// </summary>
        protected override bool WithReqFlag {  get { return true; } }

        /// <summary>
        /// 响应带有数据发送状态字段
        /// </summary>
        protected override bool WithRspStatus {  get { return true; } }
        /// <summary>
        /// 响应带有参数配置类型标识
        /// </summary>
        protected override bool WithRspSetFlag { get { return true; } }

        /// <summary>
        /// 响应包带有标识位子弹
        /// </summary>
        protected override bool WithRspFlag {  get { return true; } }

        public gw_img_para Para { get; set; }

        public gw_cmd_img_para() { }

        public gw_cmd_img_para(IPowerPole pole):base(pole) { }

        public void Update(gw_img_para para)
        {
            this.Para = para;
            base.Update(para);
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            if (data.Length - offset < this.ValuesLength)
                throw new ArgumentException("数据缓冲区太小");

            int start = offset;
            this.Para = new gw_img_para();
            this.FlushRespStatus(Para);

            Para.Color = (gw_img_para.EColor)data[offset++];
            Para.Resolution = (gw_img_para.EResolution)data[offset++];
            Para.Luminance = (int)data[offset++];
            Para.Contrast = (int)data[offset++];
            Para.Saturation = (int)data[offset++];
            msg = Para.ToString(this.RequestSetFlag == gw_ctrl.ESetFlag.Query);
            return offset - start;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            msg = string.Empty;
            if (data.Length - offset < this.ValuesLength)
                throw new ArgumentException("数据缓冲区太小");
            if (this.Para != null)
            {
                data[offset++] = (byte)Para.Color;
                data[offset++] = (byte)Para.Resolution;
                data[offset++] = (byte)Para.Luminance;
                data[offset++] = (byte)Para.Contrast;
                data[offset++] = (byte)Para.Saturation;
                msg = Para.ToString(false);
            }
            else
                offset += this.ValuesLength;
            return offset - start;
        }
    }
}
