using ResModel.nw;
using System;
using System.Collections.Generic;
using System.Text;

namespace cma.service.nw_cmd
{
    public class nw_cmd_cd_update_bubao : nw_cmd_base
    {
        public override int Control { get { return 0xcd; } }

        public override string Name { get { return "升级文件补包数据上传"; } }

        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannelNo { get; set; }

        /// <summary>
        /// 补包数量
        /// </summary>
        public int PNum { get; set; }

        /// <summary>
        /// 补包数据包列表
        /// </summary>
        public List<int> PList { get; set; }

        public nw_cmd_cd_update_bubao()
        {
            this.PList = new List<int>();
        }

        public override int Decode(out string msg)
        {
            if (this.Data.Length <= 4)
                throw new Exception("无效的数据包内容长度");
            int offset = 0;
            uint pnum = 0;
            this.ChannelNo = Data[offset++];
            offset += GetU32(this.Data, offset, out pnum);
            if(Data.Length !=  pnum *4+5)
                throw new Exception("数据包内容长度错误");

            for(int i=0;i < pnum; i++)
            {
                uint pno = 0;
                offset += GetU32(Data, offset, out pno);
                this.PList.Add((int)pno);
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("通道:{0} 补包数量:{1} 包号:",this.ChannelNo, pnum);
            foreach(int pno in this.PList)
            {
                builder.AppendFormat("{0}, ", pno);
            }
            msg = builder.ToString();

            nw_progress_update progress = nw_progress_update.GetCurrentUpdate(this.Pole);
            if (progress == null)
            {
                msg += "没有找到更新进程, 不处理该请求";
                return 0;
            }
            if(this.PList.Count == 0)
            {   ///没有补包数据，标记当前下载完成
                progress.DownloadFinish();
            }
            else
            {   //有待发送数据，继续发送
                foreach (int pno in this.PList)
                    progress.AddToSendPackage(pno);
                progress.Start_DataPackage(); //开始发送数据包
            }
            return Data.Length;
        }

        public override byte[] Encode(out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
