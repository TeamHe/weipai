using GridBackGround.Termination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBackGround.CommandDeal.nw
{
    /// <summary>
    /// 查询装置时间
    /// </summary>
    public class nw_cmd_0d_time_get : nw_cmd_base
    {
        public nw_cmd_0d_time_get()
        {

        }

        public nw_cmd_0d_time_get(IPowerPole pole) : base(pole)
        {

        }

        public override int Control { get { return 0x0d; } }

        public override string Name { get { return "查询装置时间"; } }

        /// <summary>
        /// 装置时间
        /// </summary>
        public DateTime DevTime { get; set; }

        public override int Decode(out string msg)
        {
            if(this.Data == null || this.Data.Length != 6) 
            {
                throw new Exception(string.Format("数据域长度错误,应为6字节 实际为:{1}",
                                    this.Data != null ? this.Data.Length : 0));

            }

            this.GetDateTime(this.Data, 0, out DateTime time);
            msg = string.Format("获取装置时间成功。时间:{0:G}", time);
            this.DevTime = time;
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            //数据域内容为空
            msg = string.Empty;
            return null;
        }
    }
}
