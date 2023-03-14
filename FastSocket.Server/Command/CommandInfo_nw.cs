using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodao.FastSocket.Server.Command
{
    /// <summary>
    /// 南网数据包handle
    /// </summary>
    public sealed class CommandInfo_nw : ICommandInfo
    {
        /// <summary>
        /// 南网数据包构造函数
        /// </summary>
        public CommandInfo_nw()
        {

        }

        #region Public Properties

        /// <summary>
        /// 装置号码
        /// 
        /// a)	在线监测装置的装置号码长度为6个字节。前两字节表示厂家代码，采用大写字母。后四字节
        ///     表示厂家对每套在线监测装置的识别码，采用大写字母及数字，优先使用数字。
        /// b)	厂家代码由南方电网公司统一分配。厂家赋予每套在线监测装置的装置号码应在南方电网范围
        ///     内具备唯一性
        /// </summary>
        public string CMD_ID { get;  set; }

        /// <summary>
        /// 控制字 单字节
        /// </summary>
        public int PackageType {
            get;
            set;
        }

        /// <summary>
        /// 报文内容
        /// </summary>
        public byte[] Pakcet {
            get;set;
        }

        /// <summary>
        /// 数据域内容
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 校验码
        /// </summary>
        public byte CheckCode { get; set; }

        /// <summary>
        /// 数据解析错误代码
        /// </summary>
        public int ErrorCode { get; set; }

        #endregion

        /// <summary>
        /// 数据包构建函数
        /// </summary>
        public void encode()
        {
            int len = 12;
            if(this.Data != null)
            {
                len += Data.Length;
            }            

            this.Pakcet = new byte[len];
            Pakcet[0] = 0x68;
            if(this.CMD_ID != null)
            {
                byte[] b_cmd = Encoding.ASCII.GetBytes(this.CMD_ID);
                int cp_len = 6;
                if (b_cmd.Length < 6) cp_len = b_cmd.Length;
                Buffer.BlockCopy(b_cmd, 0, this.Pakcet, 1,cp_len);
            }

            this.Pakcet[7] = (byte)this.PackageType;
            if(this.Data != null)
            {
                this.Pakcet[8] = (byte)(this.Data.Length % 256);
                this.Pakcet[9] = (byte)(this.Data.Length / 256);
                Buffer.BlockCopy(this.Data, 0, this.Pakcet, 10, this.Data.Length);
            }
            byte check_code = commandinfo_nw_check(Pakcet, 1, len - 3);
            this.Pakcet[len - 2] = check_code;
            this.Pakcet[len - 1] = 0x16;
        }

        /// <summary>
        /// 南网数据包校验码计算函数
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte commandinfo_nw_check(byte[] buffer, int offset,int len)
        {
            byte check = 0;
            for (int i = 0; i < len; i++) {
                check += buffer[offset + i];
            }
            return (byte)(check^0xff) ;
        }

        /// <summary>
        /// 南网数据包流式解析
        /// </summary>
        /// <param name="buffer">数据接收缓冲区</param>
        /// <param name="readlength">从缓冲区读取数据个数</param>
        /// <returns></returns>
        public static CommandInfo_nw Find_commandinfo_nw(ArraySegment<byte> buffer, out int readlength)
        {
            //数据包帧结构如下所示:
            //  起始码  	装置号码	控制字	 数据域长度	数据域	校验码	结束码
            //   1字节       6字节      1字节     2字节       边长   1字节   1字节

            
            //最小数据包长度验证(1+6+1+2+1+1)=12
            if (buffer.Count < 12)
            {
                readlength = 0;
                return null;
            }

            //寻找数据包起始位置
            int startno = 0,i;
            
            for(i=buffer.Offset;i<buffer.Count-12;i++)
            {
                if (buffer.Array[i] == 0x68)
                {
                    startno = i;
                    break;
                }
            }
            if (i == (buffer.Count - 12) && buffer.Array[i]!=0x68) 
            { //没有找到包头，返回错误信息
                readlength = i - buffer.Offset;
                return null;
            }

            //数据帧总长度
            int p_len = (int)(buffer.Array[startno + 8]) + (int)(buffer.Array[startno + 9]) * 256 + 12;
            if(p_len > 4000) //超长数据包检查
            {
                readlength = startno + 1 - buffer.Offset;
                return null;
            }

            //数据缓冲区数据长度检查处理
            int read_len = startno + p_len;
            if(buffer.Count < read_len)
            {
                readlength = startno - buffer.Offset;
                return null;
            }

            int check_start = startno + 1;
            int check_len = p_len - 3;
            byte check_code = buffer.Array[startno + p_len - 2];
            byte check_code_cal = commandinfo_nw_check(buffer.Array, check_start, check_len);
#if !DEBUG  //调试模式不计算校验码
            //校验码验证
            if (check_code != check_code_cal)
            { //校验出错
                readlength = startno + 1 - buffer.Offset;
                return null;
            }
#endif
            //copy 数据包内容
            CommandInfo_nw command = new CommandInfo_nw();
            command.PackageType = buffer.Array[startno + 7];
            command.Pakcet = new byte[p_len];
            Buffer.BlockCopy(buffer.Array, startno, command.Pakcet, 0, p_len);
            command.CMD_ID = Encoding.ASCII.GetString(command.Pakcet, 1, 6);
            if(p_len > 12)
            {
                command.Data = new byte[p_len - 12];
                Buffer.BlockCopy(buffer.Array, startno + 10, command.Data, 0, p_len - 12);
            }
            command.CheckCode = check_code;
            readlength = startno + p_len - buffer.Offset;
            return command;
        }
    }
}
