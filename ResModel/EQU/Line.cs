using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResModel.EQU
{
    public class Line
    {
        /// <summary>
        /// 类型标识
        /// </summary>
        public DevFlag Flag { get; set; }
        /// <summary>
        /// 线路数据库编号
        /// </summary>
        public int NO { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 线路ID
        /// </summary>
        public string LineID { get; set; }
        /// <summary>
        /// 下属杆塔装置
        /// </summary>
        public List<Tower> TowerList { get; set; }

        public Line() 
        {
            this.Flag = DevFlag.NW;
            this.TowerList = new List<Tower>();
        }

        public override string ToString()
        {
            return string.Format("单位编号:{0}\n单位名称:{1}",NO,Name);
        }
    }
}
