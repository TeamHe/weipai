using System;
using System.Text;

namespace ResModel.gw
{
    public class gw_stat_base_info
    {
        /// <summary>
        /// 装置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 装置型号
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 规约版本
        /// </summary>
        public string ProtoVersion { get; set; }

        /// <summary>
        /// 监测装置基本信息版本号
        /// </summary>
        public string InfoVersion { get; set; } 

        public gw_stat_base_info() {}

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string Manufactuer { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public string ProductionDate { get; set; }

        /// <summary>
        /// 出厂编号
        /// </summary>
        public string Identifier { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("装置名称:{0} ", this.Name);
            sb.AppendFormat("装置型号:{0} ", this.Model);
            sb.AppendFormat("规约版本:{0} ", this.ProtoVersion);
            sb.AppendFormat("装置版本:{0} ", this.InfoVersion);
            sb.AppendFormat("生产厂家:{0} ", this.Manufactuer);
            sb.AppendFormat("生产日期:{0} ", this.ProductionDate);
            sb.AppendFormat("出厂编号:{0} ", this.Identifier);
            return sb.ToString();
        }
    }
}
