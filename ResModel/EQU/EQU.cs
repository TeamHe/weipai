using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Tools;

namespace ResModel.EQU
{

    /// <summary>
    /// 在线状态
    /// </summary>
    public enum OnLineStatus
    {
        [Description("未注册")]
        None = 0,
        [Description("在线")]
        Online,
        [Description("离线")]
        Offline,
    }

    public class Equ
    {
        

        #region Public Variable
        /// <summary>
        /// 装置编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 装置名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 装置ID
        /// </summary>
        public string EquID { get; set; }
        /// <summary>
        /// 装置原始ID
        /// </summary>
        public string EquOrgID { get; set; }
        /// <summary>
        /// 装置心跳周期
        /// </summary>
        public int HeartTime { get; set; }
        /// <summary>
        /// 采集周期
        /// </summary>
        public int MainTime { get; set; }
        /// <summary>
        /// 所在杆塔
        /// </summary>
        public int TowerNO { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public ICMP Type { get; set; }

       
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquNumber { get; set; }
       
        /// <summary>
        /// Url接口
        /// </summary>
        public int UrlID { get; set; }
        /// <summary>
        /// 设备手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 在线状态
        /// </summary>
        public OnLineStatus Status { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 图片水印文字
        /// </summary>
        public string MarketText { get; set; }

        public bool Is_Time { get; set; }

        public bool Is_Name { get; set; }
        
        #endregion

        #region Construction
        /// <summary>
        /// 构造函数
        /// </summary>
        public Equ()
        {

        }

        /// <summary>
        /// 设备编号
        /// </summary>
        /// <param name="name"></param>
        public Equ(string name)
        {
            this.Name = name;
        }

        public Equ(string name, string number)
        {
            this.Name = name;
            this.EquNumber = number;
        }

        public Equ(string name, string number, string cmdid)
        {
            this.Name = name;
            this.EquNumber = number;
            this.EquID = cmdid;
        }

        public UrlInterFace GetUrl()
        {
            return null;
        }

        public override string ToString()
        {
            string str = string.Format(
                "ID:{4}\n"+
                "装置名称:{0}\n"+
                "装置编号:{1}\n"+
                "装置ID  :{2}\n"+
                "手机号码:{3}\n"+
                "在线状态:{5}\n",
                this.Name,
                this.EquNumber,
                this.EquID,
                this.Phone,
                this.ID,
                this.Status.GetDescription());
            return str;
        }
        
        #endregion
    }
}
