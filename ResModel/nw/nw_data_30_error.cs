using System;

namespace ResModel.nw
{
    /// <summary>
    /// 故障数据
    /// </summary>
    public class nw_data_30_error : nw_data_base
    {
        /// <summary>
        /// 功能编码
        /// </summary>
        public int FunctionCode { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 故障类型: TRUE 表示故障恢复  FALSE 表示故障
        /// </summary>
        public bool Status { get; set; }

        public override int PackLength { get { return 2; } }

        public nw_data_30_error() { }

        public override string ToString()
        {
            return string.Format("时间:{3} 功能编码:{0} 故障编码:{1} 故障状态:{2}",
                this.FunctionCode,
                this.Code,
                this.Status ? "已恢复" : "故障",
                this.DataTime);
        }

        public override int Decode(byte[] data, int offset)
        {
            if (data.Length - offset < this.PackLength)
                return -1;
            this.FunctionCode = data[offset++];
            byte err = data[offset++];
            this.Code = err & 0x7f;
            if ((err & 0x80) > 0)
                this.Status = true;
            else
                this.Status = false;
            return this.PackLength;
        }

        public override int Encode(byte[] data, int offset)
        {
            if (data.Length - offset < this.PackLength) return -1;
            data[offset++] = (byte)this.FunctionCode;
            data[offset++] = (byte)((this.Status ? 0x80 : 0x00) | this.Code);
            return this.PackLength;
        }
    }
}
