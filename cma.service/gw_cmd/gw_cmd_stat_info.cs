using ResModel.gw;
using System;

namespace cma.service.gw_cmd
{
    public class gw_cmd_stat_info : gw_cmd_stat_base
    {
        public override string Name { get { return "基本信息"; } }

        public override int PType { get { return 0xe7; } }

        /// <summary>
        /// 数据区长度: 装置名称（50） 装置型号(10) 规约版本(4) 装置版本(4) 
        ///             生产厂家 (50)  生产日期(4)  出厂编号(20)
        /// </summary>
        private int data_len = 50 + 10 + 4 + 4 + 50 + 4 + 20;

        public gw_stat_base_info info { get; set; }

        public override int decode(byte[] data, int offset, out string msg)
        {
            int start = offset;
            if(data.Length -offset < data_len)
                throw new Exception("数据缓冲区长度太小");
            if(this.info == null)
                this.info = new gw_stat_base_info();
            string str;
            offset += gw_coding.GetString(data,offset,50,out str);
            info.Name = str;

            offset += gw_coding.GetString(data, offset, 10, out str);
            info.Model = str;

            offset += gw_coding.GetString(data, offset, 4, out str);
            info.ProtoVersion = str;

            offset += gw_coding.GetString(data, offset, 4, out str);
            info.InfoVersion = str;

            offset += gw_coding.GetString(data, offset, 50, out str);
            info.Manufactuer = str;

            offset += gw_coding.GetString(data, offset, 4, out str);
            info.ProductionDate = str;

            offset += gw_coding.GetString(data, offset, 20, out str);
            info.Identifier = str;

            msg = info.ToString();
            this.Execute();
            return offset - start;

        }

        public override byte[] encode(out string msg)
        {
            msg = string.Empty;
            byte[] data = new byte[2 ];
            int offset = 0;
            data[offset++] = (byte)gw_ctrl.ESetStatus.Success;  //数据发送状态: 成功
            data[offset++] = 0x00; //注册成功
            return data;
        }
    }
}
