using ResModel;
using ResModel.gw;
using System;
using System.Collections.Generic;
using System.Text;

namespace cma.service.gw_cmd
{
    public class gw_cmd_ctrl_update_data : gw_cmd_base_ctrl
    {
        public override int ValuesLength {  
            get {
                int len = 28;
                if (this.UData != null && this.UData.Data != null)
                    len += UData.Data.Length;
                return len;
            }
        }

        public override string Name { get { return "软件升级数据报"; } }

        public override int PType { get { return 0xa9; } }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            throw new NotImplementedException();
        }

        public gw_ctrl_update_data UData { get; set; }


        public gw_cmd_ctrl_update_data() { }

        public gw_cmd_ctrl_update_data(IPowerPole pole) : base(pole) { }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            if (this.UData == null || this.UData.Data == null || this.UData.Data.Length == 0)
                throw new Exception("没有可发送的数据");
            int start = offset;
            msg = string.Empty;

            offset += gw_coding.SetString(data, offset, 20, this.UData.FileName);
            offset += gw_coding.SetS32(data, offset, this.UData.PNum);
            offset += gw_coding.SetS32(data, offset, this.UData.PNo);
            offset += gw_coding.SetBytes(data, offset, this.UData.Data.Length, this.UData.Data);
            msg = this.UData.ToString();
            return offset - start;
        }
    }

    public class gw_cmd_ctrl_update_end : gw_cmd_base_ctrl
    {
        public override int ValuesLength
        {
            get { return 28; }
        }

        public override string Name { get { return "软件升级结束报"; } }

        public override int PType { get { return 0xaa; } }
        
        public gw_ctrl_update_end UEnd { get; set; }

        public gw_cmd_ctrl_update_end() { }

        public gw_cmd_ctrl_update_end(IPowerPole pole) : base(pole) { }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            throw new NotImplementedException();
        }


        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            if (this.UEnd == null)
                throw new ArgumentNullException(nameof(this.UEnd));
            int start = offset;
            msg = string.Empty;

            offset += gw_coding.SetString(data, offset, 20, this.UEnd.FileName);
            offset += gw_coding.SetS32(data, offset, this.UEnd.PNum);
            offset += gw_coding.SetTime(data, offset, this.UEnd.Time);
            msg = this.UEnd.ToString();
            return offset - start;
        }
    }

    public class gw_cmd_ctrl_update_comp : gw_cmd_base_ctrl
    {
        public override int ValuesLength {  get { return 1; } }

        public override string Name { get { return "软件升级补包"; } }

        public override int PType { get {  return 0xab; } }

        public string FileName { get; set; }

        public List<int> PList { get; set; }

        public gw_cmd_ctrl_update_comp()
        {
            this.PList = new List<int>();
        }

        public void comp_hanlde(out string msg)
        {
            msg = string.Empty;
            gw_progress_update progress = gw_progress_update.GetCurrentUpdate(this.Pole);
            if (progress == null)
            {
                msg += "没有找到更新进程, 不处理该请求";
                return;
            }
            if (this.PList.Count == 0)
            {
                //没有补包数据，标记当前下载完成
                progress.UpdateFinish();
            }
            else
            {   //有待发送数据，继续发送
                foreach (int pno in this.PList)
                    progress.AddToSendPackage(pno);
                progress.Start_DataPackage(); //开始发送数据包
            }
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            if (data.Length - offset < 24)
                throw new Exception("数据缓冲区太小");

           int start = offset;
            offset += gw_coding.GetString(data, offset, 20, out string name);
            this.FileName = name;
            offset += gw_coding.GetS32(data, offset, out int num);
            if(num >0 && data.Length - offset < num)
                throw new Exception("数据缓冲区太小");
            for (int i = 0; i < num; i++)
            {
                offset += gw_coding.GetS32(data, offset, out int pno);
                this.PList.Add(pno);
            }
            comp_hanlde(out string str);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("文件名:{0} ", this.FileName);
            sb.AppendFormat("补包包数:{0} ", this.PList.Count);
            if (this.PList.Count == 0)
            {
                sb.Append("远程升级文件下发完成. ");
            }
            else
            {
                sb.AppendFormat("补包包号:");
                foreach (int pno in this.PList)
                {
                    sb.AppendFormat("{0} ", pno);
                }
            }
            sb.Append(str);
            msg = sb.ToString();
            return offset - start;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            throw new NotImplementedException();
        }
    }


}
