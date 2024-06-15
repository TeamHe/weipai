using ResModel;
using ResModel.gw;
using System;
using System.Text;
using Tools;

namespace cma.service.gw_cmd
{

    public abstract class gw_cmd_base_ctrl : gw_cmd_base
    {

        public override gw_frame_type SendFrameType { get { return gw_frame_type.Control; } }

        public override gw_frame_type RecvFrameType { get { return gw_frame_type.ResControl; } }

        /// <summary>
        /// 请求报文是否包含参数配置类型标识字段(查询，设置)
        /// </summary>
        protected virtual bool WithReqSetFlag { get { return false; } }

        /// <summary>
        /// 请求是否包含标识位字段（请求或设置的参数项）
        /// </summary>
        protected virtual bool WithReqFlag { get { return false; } }

        /// <summary>
        /// 请求是否包含参数类型字段
        /// </summary>
        protected virtual bool WithReqType { get { return false; } }

        /// <summary>
        /// 响应包是否包含数据发送状态标识 (成功，失败)
        /// </summary>
        protected virtual bool WithRspStatus { get { return false; } }

        /// <summary>
        /// 响应包是否包含参数类型字段
        /// </summary>
        protected virtual bool WithRspType { get { return false; } }

        /// <summary>
        /// 响应包是否包含标识位字段
        /// </summary>
        protected virtual bool WithRspFlag { get { return false; } }

        /// <summary>
        /// 响应包是否包含参数配置类型字段
        /// </summary>
        protected virtual bool WithRspSetFlag { get { return false; } }

        /// <summary>
        /// 参数配置类型标识: 查询，设置
        /// </summary>
        public gw_ctrl.ESetFlag RequestSetFlag { get; set; }

        public gw_para_type ParaType { get; set; }

        /// <summary>
        /// 查询/设置 结果: 成功，失败
        /// </summary>
        public gw_ctrl.ESetStatus Status { get; set; }

        /// <summary>
        /// 设置标识位(每一个参数一个Bit)
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 数据区域长度
        /// </summary>
        public abstract int ValuesLength { get; }

        public gw_cmd_base_ctrl() { }

        public gw_cmd_base_ctrl(IPowerPole pole)
            :base(pole) 
        { }

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
            this.RequestSetFlag = gw_ctrl.ESetFlag.Query;
            if (this.WithReqFlag && this.Flag == 0x00)
                this.Flag = 0xff;
            this.Execute();
        }

        public void Query(gw_para_type type)
        {
            this.ParaType = type;
            this.Query();
        }

        public void Update()
        {
            this.RequestSetFlag = gw_ctrl.ESetFlag.Set;
            this.Execute();
        }

        public void Update(int flag)
        {
            this.Flag = flag;
            this.Update();
        }

        public void Update(gw_para_type paraType)
        {
            this.ParaType = paraType;
            this.Update();
        }

        public void Update(gw_para_type paraType, int flag)
        {
            this.Flag = flag;
            this.ParaType = paraType;
            this.Update();
        }

        public virtual void Update(gw_ctrl ctrl)
        {
            if (ctrl != null)
                this.Update(ctrl.ParaType, ctrl.Flag);
            else
                this.Update();
        }

        public override int decode(byte[] data, int offset, out string msg)
        {
            int num = 0,start = offset;
            if (WithRspStatus) num++;
            if (WithRspSetFlag) num++;
            if (WithReqType) num++;
            if (WithRspFlag) num++;
            if (data.Length - offset < num)
                throw new Exception("数据缓冲区长度太小");

            if (WithRspStatus)
                this.Status = (gw_ctrl.ESetStatus)data[offset++];
            if(WithRspSetFlag)
                this.RequestSetFlag = (gw_ctrl.ESetFlag)data[offset++];
            if(WithRspType)
                this.ParaType = (gw_para_type)data[offset++];
            if (WithRspFlag)
                this.Flag = (int)data[offset++];

            offset += this.DecodeData(data, offset, out string str);
            StringBuilder sb = new StringBuilder();
            if (this.WithRspSetFlag)
                sb.AppendFormat("{0}{1}",this.RequestSetFlag.GetDescription(),this.Name);
            if(this.WithRspStatus)
                sb.AppendFormat("{0}. ",this.Status.GetDescription());
            if(this.WithRspType)
                sb.AppendFormat("参数类型:{0} ",this.ParaType.GetDescription());
            sb.Append(str);
            msg = sb.ToString();
            return offset - start;
        }

        public override byte[] encode(out string msg)
        {
            int no = 0;
            if (this.WithReqSetFlag)
                no++;
            if (this.WithReqFlag)
                no++;
            if (this.WithReqType)
                no++;
            byte[] data = new byte[no + this.ValuesLength];
            int offset = 0;
            if (this.WithReqSetFlag)
                data[offset++] = (byte)this.RequestSetFlag;
            if (this.WithReqType)
                data[offset++] = (byte)this.ParaType;
            if (this.WithReqFlag)
                data[offset++] = (byte)this.Flag;

            this.EncodeData(data,offset, out string str);
            StringBuilder sb = new StringBuilder();
            if (this.WithReqSetFlag)
                sb.AppendFormat("{0}{1}. ", this.RequestSetFlag.GetDescription(), this.Name);
            if (this.WithReqType)
                sb.AppendFormat("参数类型:{0} ", this.ParaType.GetDescription());
            sb.Append(str);
            msg = sb.ToString();
            return data;
        }

        public void FlushRespStatus(in gw_ctrl ctrl)
        {
            if (ctrl == null)
                return;
            ctrl.Flag = this.Flag;
            ctrl.RSFalg = this.RequestSetFlag;
            ctrl.Result = this.Status;
            ctrl.ParaType = this.ParaType;
        }
    }
}
