using GridBackGround.CommandDeal.Image;
using log4net.Appender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBackGround.CommandDeal.nw
{
    public class nw_cmd_86_photo_up_end : nw_cmd_base
    {
        public override int Control { get { return 0x86; } }

        public override string Name { get { return "图像数据上送结束标记"; } }

        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_NO { get; set; }

        /// <summary>
        /// 预置位号
        /// </summary>
        public int PresetNo { get; set; }

        public override int Decode(out string msg)
        {
            if (Data == null || Data.Length < 2)
                throw new Exception(string.Format("数据域长度错误,应为2字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            int offset = 0;
            this.Channel_NO = Data[offset++];
            this.PresetNo = Data[offset++];

            msg = string.Format("图像上传结束. 通道号:{0} 预置位号:{1}",this.Channel_NO,this.PresetNo);

            Photo_man photo = new Photo_man(this.Pole, this.Channel_NO, this.PresetNo);
            List<int> remains = photo.Picture_End(DateTime.MinValue, out string msg_end);
            nw_cmd_87_photo_up_bubao cmd = new nw_cmd_87_photo_up_bubao(Pole) 
            { 
                Channel_NO =this.Channel_NO, 
                PresetNo =this.PresetNo,
                List_Pac = remains,
            };
            cmd.Execute();

            if(remains == null || remains.Count == 0)   
            {
                photo.Picture_Save();   //图片处理
            }
            return 0;
        }

        public override byte[] Encode(out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
