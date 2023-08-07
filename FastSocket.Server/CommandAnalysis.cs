using System;
using System.Text;

namespace Sodao.FastSocket.Server
{
    /// <summary>
    /// 命令解析部分，TCP/UDP通用
    /// </summary>    
    public static class CommandAnalysis
    {
        /// <summary>
        /// 报文基本规范解析
        /// 协议格式
        /// [Sync][Packet_Lenth][CMD_ID][Frame_Type][Packet_Type][][data][CRC16][End]
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="readlength"></param>
        /// <returns></returns>
        public static Command.CommandInfo AnalysisPacket(ArraySegment<byte> buffer, out int readlength)
        {
            int Packet_Lenth = 0;       //报文长度
            string CMD_ID = "";         //状态监测装置ID
            byte Frame_Type = 0;         //帧类型
            byte Packet_Type = 0;       //报文类型
            byte[] data;                //报文内容
            byte[] CRC = { 0x00 };         //CRC校验码
            int erroCode = 0 ;

            var payload = buffer.Array;
            readlength = buffer.Count;
            #region  数据长度计算
            //接收到的数据小于最小包长25[2 +2+17+1+1+data+2]
            if (buffer.Count < 25)
            {
                erroCode = 0x01;

                //数据长度小于最小包长，将数据提取并出来交给上层处理
                data = new byte[buffer.Count];
                Buffer.BlockCopy(payload, buffer.Offset, data, 0, buffer.Count);
                return new Command.CommandInfo(CMD_ID, Packet_Lenth, Frame_Type, Packet_Type, data, CRC,erroCode);
            }
            //if(buffer.Count)
            #endregion

            #region 寻找包头
            //开始计数
            int startNo = 0;
            int i;
            //找到包的起始位置
            for (i = 0; i < buffer.Count - 25; i++)
            {
                if ((payload[i + buffer.Offset] == 0xa5) && (payload[buffer.Offset + i + 1] == 0x5a))
                {
                    startNo = i + buffer.Offset;
                    break;
                }
            }
            //没找到包头
            if (i == buffer.Count - 1)
            {
                erroCode = 0x02; //错误代码2
                //数据长度小于最小包长，将数据提取并出来交给上层处理
                data = new byte[buffer.Count];
                Buffer.BlockCopy(payload, buffer.Offset, data, 0, buffer.Count);
                return new Command.CommandInfo(CMD_ID, Packet_Lenth, Frame_Type, Packet_Type, data, CRC,erroCode);
            }
            #endregion

            #region CRC校验

            //计算包长
            Packet_Lenth = (int)(payload[startNo + 2]) + (int)(payload[startNo + 3]) * 255;
             data = new byte[Packet_Lenth + 25];
            readlength = startNo - buffer.Offset + 25 + Packet_Lenth;   //读数长度

            if (buffer.Count < readlength)
            {
                readlength = buffer.Count;   //数据长度，小于包长
                erroCode = 0x08; //错误代码8
                return new Command.CommandInfo(CMD_ID, Packet_Lenth, Frame_Type, Packet_Type, data, CRC, erroCode);
            }
           
           
            Buffer.BlockCopy(payload, startNo, data, 0, Packet_Lenth + 25);
           
            CRC = Sodao.FastSocket.SocketBase.CRC16.Crc(buffer.Array, Packet_Lenth + 23);           //计算CRC16         
            if (!((CRC[0] == data[Packet_Lenth + 23]) && CRC[1] == data[Packet_Lenth + 24]))        //CRC校验失败
            {
                erroCode = 0x03; //错误代码3
                //return new Command.AsyncBinaryCommandInfo(CMD_ID, Packet_Lenth, Frame_Type, Packet_Type, data, CRC,erroCode);
            }
            #endregion

            #region 校验通过

            CMD_ID = Encoding.UTF8.GetString(data, 4, 17);          //装置ID
            Frame_Type = data[21];                                  //帧类型
            Packet_Type = data[22];                                 //报文类型
            return new Command.CommandInfo(CMD_ID, Packet_Lenth, Frame_Type, Packet_Type, data, CRC,erroCode);
            #endregion

        }
        /// <summary>
        /// 报文基本规范解析
        /// 协议格式
        /// [Sync][Packet_Lenth][CMD_ID][Frame_Type][Packet_Type][FrameNo][data][CRC16][End]
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="readlength"></param>
        /// <returns></returns>
        public static Command.CommandInfo_gw AnalysisPacketV2(ArraySegment<byte> buffer, out int readlength)
        {
            int Packet_Lenth = 0;       //报文长度
            string CMD_ID = "";         //状态监测装置ID
            byte Frame_Type = 0;         //帧类型
            byte Packet_Type = 0;       //报文类型
            byte Frame_No = 0;
            byte[] Pacekt = null;
            byte[] data = null;                //报文内容
            byte[] CRC = { 0x00 };         //CRC校验码
            int erroCode = 0;

            var payload = buffer.Array;
            readlength = buffer.Count;
 
            #region  数据长度校验
            //[Sync][Packet_Lenth][CMD_ID][Frame_Type][Packet_Type][FrameNo][data][CRC16][End]
            int MinLength =27;      // 2 + 2 + 17 + 1 + 1 +1 + 2 + 1; //不包括报文内容
            if (buffer.Count < MinLength)
            {
                readlength = 0;
                return null;
            }
            #endregion

            #region 寻找包头
            //开始计数
            int startNo = 0;
            int i;
            //找到包的起始位置
            for (i = 0; i < buffer.Count - MinLength; i++)
            {
                if ((payload[i + buffer.Offset] == 0xa5) && (payload[buffer.Offset + i + 1] == 0x5a))
                {
                    startNo = i + buffer.Offset;
                    break;
                }
            }
            //没找到包头
            if (i == buffer.Count - MinLength)
            {
                erroCode = 0x02; //错误代码2
                //数据长度小于最小包长，将数据提取并出来交给上层处理
                Pacekt = new byte[buffer.Count - MinLength];
                readlength = buffer.Count - MinLength;
                return null;
               
            }
            #endregion

            #region 包长校验

            //计算包长
            Packet_Lenth = (int)(payload[startNo + 2]) + (int)(payload[startNo + 3]) * 256;
            if (Packet_Lenth > 2096)
            {
                //readlength = 4;
                try
                {
                    //if(Pacekt == null)
                    //    Pacekt = new byte[readlength];
                    Buffer.BlockCopy(payload, startNo, Pacekt, 0, readlength);
                    data = new byte[1];
                    erroCode = 0x03; //错误代码8
                    return new Command.CommandInfo_gw(CMD_ID,
                        Packet_Lenth,
                        Frame_Type,
                        Packet_Type,
                        Frame_No,
                        Pacekt,
                        data,
                        CRC,
                        erroCode);
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }
            }
            Pacekt = new byte[Packet_Lenth + MinLength];
            readlength = startNo - buffer.Offset + MinLength + Packet_Lenth;   //读数长度

            if (buffer.Count < readlength)
            {
                readlength = 0;
                return null;
            }
            #endregion
            
            #region CRC校验
            Buffer.BlockCopy(payload, startNo, Pacekt, 0, Packet_Lenth + MinLength);
            CRC = Sodao.FastSocket.SocketBase.CRC16.Crc(Pacekt, 2, Packet_Lenth + MinLength - 5);           //计算CRC16
            int CRC_Position = Packet_Lenth + MinLength - 3;
            if (!((CRC[0] == Pacekt[CRC_Position]) && CRC[1] == Pacekt[CRC_Position + 1]))        //CRC校验失败
            {
                erroCode = 0x04; //错误代码3
            }
            #endregion
            if (Pacekt[Packet_Lenth + MinLength - 1] != 0x96)
            {
                erroCode = 0x05;
            }
            #region 校验通过

            CMD_ID = Encoding.UTF8.GetString(Pacekt, 4, 17);          //装置ID
            Frame_Type = Pacekt[21];                                  //帧类型
            Packet_Type = Pacekt[22];                                 //报文类型
            Frame_No = Pacekt[23];                                    //帧序列号
            data = new byte[Packet_Lenth];
            int DataStart = 2+2+17+1+1+1;
            Buffer.BlockCopy(Pacekt, DataStart, data, 0, Packet_Lenth);
            return new Command.CommandInfo_gw(CMD_ID,
                    Packet_Lenth,
                    Frame_Type,
                    Packet_Type,
                    Frame_No,
                    Pacekt,
                    data,
                    CRC,
                    erroCode);
            #endregion
        }
    }
}
