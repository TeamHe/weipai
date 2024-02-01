using ResModel.gw;
using System;
using Tools;

namespace cma.service.gw_cmd
{

    public abstract class gw_cmd_base_ctrl : gw_cmd_base
    {
        /// <summary>
        /// 被测设备ID
        /// </summary>
        public string Component_ID { get; set; }


        public override gw_frame_type SendFrameType { get { return gw_frame_type.Control; } }

        public override gw_frame_type RecvFrameType { get { return gw_frame_type.ResControl; } }


        /// <summary>
        /// 参数配置类型标识: 查询，设置
        /// </summary>
        public gw_ctrl.RequestSetFlag RequestSetFlag { get; set; }


        /// <summary>
        /// 查询/设置 结果: 成功，失败
        /// </summary>
        public gw_ctrl.Status Status { get; set; }

        /// <summary>
        /// 设置标识位(每一个参数一个Bit)
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 数据区域长度
        /// </summary>
        public abstract int ValuesLength { get; }

        /// <summary>
        /// 数据区域解析
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public abstract int DecodeData(byte[] data, int offset, out string msg);

        /// <summary>
        /// 数据部分编码
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public abstract int EncodeData(byte[] data, int offset, out string msg);

        public virtual void Query()
        {
            this.RequestSetFlag = gw_ctrl.RequestSetFlag.Query;
            this.Execute();
        }

        private void Update()
        {
            this.RequestSetFlag = gw_ctrl.RequestSetFlag.Set;
            this.Execute();
        }

        public void Update(int flag)
        {
            this.Flag = flag;
            this.Update();
        }

        public virtual void Update(gw_ctrl ctrl)
        {
            if(ctrl != null)
                this.Flag = ctrl.Flag;
            this.Update();
        }

        public override int decode(byte[] data, int offset, out string msg)
        {
            if (data.Length - offset < 3)
                throw new Exception("数据缓冲区长度太小");
            this.RequestSetFlag = (gw_ctrl.RequestSetFlag)data[offset++];
            this.Status = (gw_ctrl.Status)data[offset++];
            this.Flag = (int)data[offset++];

            int ret = this.DecodeData(data, offset, out string str);
            msg = EnumUtil.GetDescription(this.RequestSetFlag) +
                  this.Name +
                  EnumUtil.GetDescription(this.Status) + 
                  ". " + str;
            return ret;
        }

        public override byte[] encode(out string msg)
        {
            byte[] data = new byte[2+this.ValuesLength];
            int offset = 0;
            data[offset++] = (byte)this.RequestSetFlag;
            data[offset++] = (byte)this.Flag;

            this.EncodeData(data,offset, out string str);
            msg = EnumUtil.GetDescription(this.RequestSetFlag) +
                 this.Name + ". " + str;
            return data;
        }

        public void FlushRespStatus(in gw_ctrl ctrl)
        {
            if (ctrl == null)
                return;
            ctrl.Flag = this.Flag;
            ctrl.RSFalg = this.RequestSetFlag;
            ctrl.Result = this.Status;
        }
    }
}
