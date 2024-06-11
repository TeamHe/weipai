namespace ResModel.gw_nw
{
    public class gn_img_cfg
    {
        /// <summary>
        /// 图片上传时对应的设备ID
        /// </summary>
        public string UploadID  { get; set; }

        /// <summary>
        /// 是否添加水印文字
        /// </summary>
        public bool IsMark {  get; set; }

        /// <summary>
        /// 水印文字内容
        /// </summary>
        public string Mark {  get; set; }

        /// <summary>
        /// 是否添加时间水印
        /// </summary>
        public bool IsTime {  get; set; }

        /// <summary>
        /// 图片上传URL地址
        /// </summary>
        public string UploadUrl {  get; set; }
    }
}
