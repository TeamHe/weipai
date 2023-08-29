using System;
using System.ComponentModel;
using System.Text;

namespace ResModel.PowerPole
{
    /// <summary>
    /// 数据来源标识
    /// </summary>
    public enum SrcType
    {
        [Description("TCP")]
        GW_TCP,
        [Description("UDP")]
        GW_UDP,
        [Description("UDP")]
        NW_UDP,
        [Description("Serial")]
        Serial,
        [Description("Webservice")]
        WebSocket,
    }

    /// <summary>
    /// 数据收发标识
    /// </summary>
    public enum RSType
    {
        Notice,
        Recv,
        Send,
    }

    public class PackageMessage
    {
        /// <summary>
        /// 关联的设备handle
        /// </summary>
        public IPowerPole pole { get; set; }

        /// <summary>
        /// 数据收发时间
        /// </summary>
        public DateTime time { get; set; }

        /// <summary>
        /// 收发数据类型
        /// </summary>
        public byte[] data { get; set; }    

        /// <summary>
        /// 数据收发类型
        /// </summary>
        public RSType rstype { get; set; }

        /// <summary>
        /// 操作代码(错误码)
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 数据来源类型
        /// </summary>
        public SrcType srctype { get; set; }

        /// <summary>
        /// 数据接口ID，
        /// 当接口为socket时是目标IP端口，
        /// 当接口为串口时是关联的串口号
        /// </summary>
        public string src_id { get; set; }

        /// <summary>
        /// 数据描述
        /// </summary>
        public string Description { get; set; }

        public PackageMessage()
        {
            time = DateTime.Now;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("{0:s} ", this.time);
            str.AppendFormat("[{0}] ",this.rstype.ToString());
            if (this.data != null && this.data.Length > 0)
                for (int i = 0; i < this.data.Length; i++)
                    str.AppendFormat("{0:X2} ", data[i]);
            if (this.Description != null)
                str.Append(this.Description);
            return str.ToString();
        }
    }
}
