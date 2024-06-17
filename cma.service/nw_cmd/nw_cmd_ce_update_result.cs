using ResModel.nw;
using System;
using System.Text;

namespace cma.service.nw_cmd
{
    public class nw_cmd_ce_update_result : nw_cmd_base
    {
        public override int Control { get { return 0xce; } }

        public override string Name { get { return "升级进度查询"; } }

        public int ChannelNo { get; set; }

        public int Percent { get; set; }

        public nw_UpdateResult Result {  get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string BeforeVersion { get; set; }

        public string AfterVersion { get; set; }
        
        public bool Response { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override int Decode(out string msg)
        {
            nw_progress_update progress = nw_progress_update.GetCurrentUpdate(this.Pole);
            if (this.Data.Length == 2)
            {
                this.ChannelNo = Data[0];
                this.Percent = Data[1];
                msg = string.Format("通道:{0} 升级进度:{1}", this.ChannelNo, this.Percent);

                if (progress != null)
                    progress.UpdateProgress(this.ChannelNo, Percent);
                return 0;
            } 
            else if(Data.Length == 270)
            { 
                int offset = 0;
                this.ChannelNo = Data[offset++];
                this.Result = (nw_UpdateResult)Data[offset++];
                DateTime time = DateTime.Now;
                string str = "";

                offset += GetDateTime(this.Data, offset, out time);
                this.Start = time;

                offset += GetDateTime(this.Data, offset, out time);
                this.End = time;

                offset += GetString(this.Data, offset, 128, out str);
                this.BeforeVersion = str;

                offset += GetString(this.Data, offset, 128, out str);
                this.AfterVersion = str;

                if (progress != null)
                    progress.UpdateProgress(this.ChannelNo, this.Result,
                        this.Start, this.End, this.BeforeVersion, this.AfterVersion);

                this.Response = true;
                this.SendCommand(out string msg1);

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("通道: {0} 升级结果:{1} ", this.ChannelNo, Enum.GetName(typeof(nw_UpdateResult), this.Result));
                sb.AppendFormat("升级开始时间:{0} 升级结束时间:{1}", this.Start, this.End);
                sb.AppendFormat("升级前版本号:{0} 升级后版本号:{1}", this.BeforeVersion, this.AfterVersion);
                sb.Append(msg1);
                msg = sb.ToString();
                return 0;
            }
            else
                throw new ArgumentException("升级结果数据包长度错误");
        }

        public override byte[] Encode(out string msg)
        {
            if(this.Response)
            {
                byte[] data = new byte[270];
                int offset = 0;
                data[offset++] = (byte)this.ChannelNo;
                data[offset++] = (byte)this.Result;
                offset += this.SetDateTime(data, offset, this.Start);
                offset += this.SetDateTime(data, offset, this.End);
                offset += this.SetString(data, offset, 128, this.BeforeVersion);
                offset += this.SetString(data, offset, 128, this.AfterVersion);
                msg = "";
                return data;
            }
            else
            {
                byte[] data = new byte[1];
                data[0] = (byte)ChannelNo;
                msg = string.Format("查询 通道号:{0}", this.ChannelNo);
                return data;
            }
        }
    }
}
