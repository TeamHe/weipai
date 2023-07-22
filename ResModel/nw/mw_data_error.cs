using System;

namespace ResModel.nw
{
    /// <summary>
    /// 故障数据
    /// </summary>
    public class mw_data_error
    {
        public DateTime DataTime { get; set; }

        public int FunctionCode { get; set; }

        public int Code { get; set; }

        /// <summary>
        /// 故障类型: TRUE 表示故障恢复  FALSE 表示故障
        /// </summary>
        public bool Status { get; set; }

        public mw_data_error() { }

        public mw_data_error(DateTime dataTime, int functionCode, int errorCode)
        {
            DataTime = dataTime;
            FunctionCode = functionCode;
            Code = errorCode;
        }

        public override string ToString()
        {
            return string.Format("时间:{3} 功能编码:{0} 故障编码:{1} 故障状态:{2}",
                this.FunctionCode,
                this.Code,
                this.Status ? "已恢复" : "故障",
                this.DataTime);
        }
    }
}
