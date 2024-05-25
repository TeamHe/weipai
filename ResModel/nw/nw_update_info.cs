using System.ComponentModel;
using ResModel.gw_nw;

namespace ResModel.nw
{
    public enum nw_UpdateResult
    {
        [Description("升级成功")]
        Success = 0,
        [Description("文件下载失败")]
        DownloadFail = 1,
        [Description("升级文件存储空间不足")]
        LowMemory = 2,
        [Description("文件格式错误")]
        FormatError = 3,
        [Description("文件校验出错")]
        CheckCodeErr = 4,
        [Description("固件版本与当前硬件不匹配")]
        UnCompatible = 5,
        [Description("电量过低")]
        LowPower = 6,
        [Description("升级失败：其他原因")]
        UpdateFaile = 7,
    }

    public class nw_update_info : gn_update_info
    {
        public nw_update_info() { 
            Password = "1234";
        }

        public string Password { get;set; }

        public int ChannelNO { get; set; }

        public enum UpdateType
        {
            /// <summary>
            /// 普通升级
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 断点续传
            /// </summary>
            Resum = 1,
        }

        public UpdateType Type { get; set; }

    }
}
