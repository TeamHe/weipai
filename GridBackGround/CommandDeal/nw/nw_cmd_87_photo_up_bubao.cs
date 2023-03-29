using GridBackGround.Termination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_cmd_87_photo_up_bubao : nw_cmd_base
    {
        public override int Control { get { return 0x87; } }

        public override string Name { get { return "补包数据下发"; } }

        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_NO { get; set; }

        /// <summary>
        /// 预置位号
        /// </summary>
        public int PresetNo { get; set; }

        /// <summary>
        /// 补报列表
        /// </summary>
        public List<int> List_Pac {  get; set; }
        public nw_cmd_87_photo_up_bubao()
        {

        }

        public nw_cmd_87_photo_up_bubao(IPowerPole pole) : base(pole)
        {

        }

        public override int Decode(out string msg)
        {
            throw new NotImplementedException();
        }

        public override byte[] Encode(out string msg)
        {
            int num = 0;
            if (List_Pac != null) { num = this.List_Pac.Count; }
            byte[] data = new byte[num * 2 + 3];
            int offset = 0;
            data[offset++] = (byte)this.Channel_NO;
            data[offset++] = (byte)this.PresetNo;
            data[offset++] = (byte)num;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("补报收据下发. 通道号:{0} 预置位号:{1} 补包报数:{2} 包号:",
                this.Channel_NO,this.PresetNo,num);
            for(int i = 0; i < num;i++)
            {
                offset += this.SetU16(data, offset, List_Pac[i]);
                stringBuilder.AppendFormat("{0},", List_Pac[i]);
            }
            msg = stringBuilder.ToString();
            return data;
        }
    }
}
