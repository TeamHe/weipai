using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResModel.EQU
{
    public class Tower
    {
        /// <summary>
        /// 所属线路信息
        /// </summary>
        public int LineID { get; set; }
        /// <summary>
        /// 杆塔数据ID
        /// </summary>
        public int TowerNO { get; set; }
        /// <summary>
        /// 杆塔名称
        /// </summary>
        public string TowerName { get; set; }
        /// <summary>
        /// 杆塔装置ID信息
        /// </summary>
        public string TowerID { get; set; }
        /// <summary>
        /// 装置列表
        /// </summary>
        public List<Equ> EquList { get; set; }

        public override string ToString()
        {
            string str = string.Format("编号：{0}\n" +
                                       "线路名称：{1}\n",
                                       this.TowerNO,
                                       this.TowerName
                                       );
            return str;
        }
    }
}
