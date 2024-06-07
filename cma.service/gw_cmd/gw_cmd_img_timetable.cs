using ResModel;
using ResModel.Image;
using System;
using System.Collections.Generic;
using System.Text;

namespace cma.service.gw_cmd
{
    public class gw_cmd_img_timetable : gw_cmd_img_base
    {
        public override int ValuesLength
        {
            get
            {
                int num = 2;
                if (this.Times != null && this.Times.Count > 0)
                    num += Times.Count * 3;
                return num;
            }
        }

        public override string Name { get { return "拍照时间表"; } }

        public override int PType { get { return 0xca; } }

        /// <summary>
        /// 请求带有参数配置类型标识
        /// </summary>
        protected override bool WithReqSetFlag { get { return true; } }

        /// <summary>
        /// 响应带有数据发送状态
        /// </summary>
        protected override bool WithRspStatus {  get { return true; } }

        /// <summary>
        /// 响应带有参数类型标识
        /// </summary>
        protected override bool WithRspSetFlag {  get { return true; } }

        /// <summary>
        /// 时间表列表
        /// </summary>
        public List<PhotoTime> Times { get; set; }

        /// <summary>
        /// 通道号
        /// </summary>
        public int ChNO { get; set; }

        public gw_cmd_img_timetable() 
        { 
            this.Times = new List<PhotoTime>();
        }

        public gw_cmd_img_timetable(IPowerPole pole):base(pole)
        {
            this.Times = new List<PhotoTime>();
        }

        public void Query(int chno)
        {
            this.ChNO = chno;
            base.Query();
        }

        public void Update(int chno,List<PhotoTime> times)
        {
            this.ChNO = chno;
            this.Times = times;
            base.Update();
        }

        public override int DecodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            if (this.Data.Length - offset < 2)
                throw new Exception("数据缓冲区太小");

            StringBuilder sb = new StringBuilder();
            this.ChNO = data[offset++];
            int num = data[offset++];
            sb.AppendFormat("通道号:{0} ",this.ChNO);
              sb.AppendFormat("共{0}组。 ", num);
            if (data.Length - offset < num * 3)
                throw new Exception("数据缓冲区太小");

            this.Times.Clear();
            for(int i = 0; i < num; i++)
            {
                PhotoTime ptime = new PhotoTime()
                {
                    Hour = data[offset++],
                    Minute = data[offset++],
                    Presetting_No = data[offset++],
                };
                this.Times.Add(ptime);
                sb.AppendFormat("第{0}组:{1}", i + 1, ptime);
                if (i < num - 1)
                    sb.Append(", ");

            }
            msg = sb.ToString();
            return offset - start;
        }

        public override int EncodeData(byte[] data, int offset, out string msg)
        {
            int start = offset;
            if (data.Length - offset < this.ValuesLength)
                throw new Exception("数据缓冲区太小");
            
            int num = 0;
            if(this.Times != null && this.Times.Count > 0) 
                num = Times.Count;
            data[offset++] = (byte)this.ChNO;
            data[offset++] = (byte)num;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("通道号:{0} ", this.ChNO);
            if(this.RequestSetFlag == ResModel.gw.gw_ctrl.ESetFlag.Set)
            {
                sb.AppendFormat("共{0}组。 ", num);
                for (int i = 0; i < num; i++)
                {
                    PhotoTime ptime = (PhotoTime)this.Times[i];
                    data[offset++] = (byte)ptime.Hour;
                    data[offset++] = (byte)ptime.Minute;
                    data[offset++] = (byte)ptime.Presetting_No;

                    sb.AppendFormat("第{0}组:{1}", i + 1, ptime);
                    if (i < num - 1)
                        sb.Append(", ");
                }
            }
            msg = sb.ToString();
            return offset - start;
        }
    }
}
