using ResModel.nw;
using System;
using System.ComponentModel;
using System.Text;

namespace cma.service.nw_cmd
{
    /// <summary>
    /// 南网控制指令: 0xca主站请求设备升级更新
    /// </summary>
    public class nw_cmd_ca_request_update : nw_cmd_base
    {
        public override int Control { get { return 0xca; } }

        public override string Name { get { return "请求设备升级"; } }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannoNo { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 总包数量
        /// </summary>
        public int PacNum {  get; set; }

        /// <summary>
        /// 升级类型
        /// </summary>
        public int UpdateType {  get; set; }

        public enum UpdateErrorCode
        {
            [Description("当前设备正在升级")]
            Updating = 1,

            [Description("当前电量不够")]
            LowPower = 2,

            [Description("当前流量不够")]
            LowFlow = 3,

            [Description("参数不对")]
            InvalidPara = 4,

            [Description("未接收到该升级文件")]
            NoUpdateData = 5,
        }

        public override int Decode(out string msg)
        {
            nw_progress_update progress = nw_progress_update.GetCurrentUpdate(this.Pole);
            if (this.Data == null)
                throw new Exception("无效的数据域");
            if (Data.Length == 2)
            {
                msg = "密码出错";
                return -1;
            }
            else if(Data.Length == 3)
            {
                this.ChannoNo = Data[0];
                int code = 0;
                GetU16(this.Data,1,out code);
                string errmsg = Enum.GetName(typeof(UpdateErrorCode), code);
                msg = string.Format("通道:{0} 错误信息:{1}-{2}",
                    this.ChannoNo, code, Enum.GetName(typeof(UpdateErrorCode), code));
                if(progress != null)
                    progress.UpdateFinish();
                return -1;
            }
            else if(Data.Length == 74)
            {
                string str = "";
                int offset = GetPassword(Data, 0, out str);
                this.Password = str;
                this.ChannoNo = Data[offset++];
                offset += this.GetString(Data, offset, 64, out str);
                this.FileName = str;
                uint pnum = 0;
                offset += GetU32(this.Data,offset, out pnum);
                this.PacNum = (int)pnum;
                this.UpdateType = Data[offset++];
                msg = string.Format("通道:{0} 文件名:{1} 总包数:{2} 升级类型:{3}",
                    this.ChannoNo, this.FileName, this.PacNum,
                    this.UpdateType == 0 ? "普通升级" : "断点续传");
                
                if(progress != null) 
                    progress.Start_DataPackage();  //开始发送数据包
                else
                {
                    msg += "没有找到更新进程, 不处理该请求";
                    return 0;
                }
                return 0;
            }
            else
            {
                throw new Exception("数据长度错误");
            }
        }

        public override byte[] Encode(out string msg)
        {
            if (string.IsNullOrEmpty(FileName))
                throw new ArgumentNullException("文件名");

            byte[] data = new byte[74];  //4+1+64+4+1
            int offset = this.SetPassword(data, 0, this.Password);
            data[offset++] = (byte)this.ChannoNo; //通道号
            byte[] b_id = Encoding.ASCII.GetBytes(this.FileName);
            Buffer.BlockCopy(b_id, 0, data, offset, b_id.Length >= 64 ? 64 : b_id.Length);
            offset += 64;
            offset += SetU32(data, offset, (uint)this.PacNum);
            data[offset++] = (byte)this.UpdateType;
            msg = string.Format("通道:{0} 文件名:{1} 总包数:{2} 升级类型:{3}",
                this.ChannoNo, this.FileName, this.PacNum,
                this.UpdateType == 0?"普通升级":"断点续传");
            return data;
        }
    }
}
