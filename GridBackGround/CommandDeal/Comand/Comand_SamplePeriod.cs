using System;
using ResModel;
using ResModel.EQU;
using Tools;
using ResModel.PowerPole;
using cma.service.PowerPole;

//采样参数设置
namespace GridBackGround.CommandDeal
{
    class Comand_SamplePeriod
    {
        private static string CMD_ID;
        private static byte Data_Type;
        private static ushort Main_Time{ get;  set; }
        private static byte  Heart_Time{get; set;}
        //包长：参数配置类型，1
        //      标志位，1
        //      采样的数据类型，1
        //      采集时间周期，2
        //      心跳上送周期,1
        private static int PacLength = 1 + 1 + 1 + 2 + 1;
        #region 公共函数
        /// <summary>
        /// 查询设备采样参数
        /// </summary>
        /// <param name="cmd_ID">设备ID号</param>
        /// <param name="type">数据类型</param>
        public static void Query(string cmd_ID,byte type)
        {
            Con(cmd_ID, 0x00, type, 0x00, 0x00, 0x00);
        }

        public static void Ayanlise(string cmd_id, byte[] data)
        {
            if (data.Length < 6)
                return;
            string pacMsg = "WEB操作：";
            if (data[0] == 0x00)
                pacMsg += "查询";
            else
                pacMsg += "设置";
            int request_Flag = data[1];
            pacMsg += "数据类型:" + ((ICMP)data[2]).GetDescription();
            if ((request_Flag % 2) == 1)
                pacMsg += "采集周期:" + BitConverter.ToUInt16(data, 3) + "分钟 ";
            if ((request_Flag / 2) == 1)
                pacMsg += "心跳周期:" + ((int)data[5]).ToString() + "分钟 ";
            //显示发送的数据
            DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.rec,
                    Termination.PowerPoleManage.Find(cmd_id),
                    "采样周期",
                    pacMsg));
        }
        /// <summary>
        /// 设置设备采样参数
        /// </summary>
        /// <param name="cmd_ID">设备ID</param>
        /// <param name="Data_Type">数据类型</param>
        /// <param name="sample_Time">采样周期</param>
        /// <param name="heart_Time">心跳周期</param>
        public static void Set(string cmd_ID, byte data_Type,int Request_Flag , int sample_Time, int heart_Time)
        {
            Con(cmd_ID,0x01,Request_Flag,data_Type,sample_Time,heart_Time);
        }
        /// <summary>
        /// 响应数据解析
        /// </summary>
        /// <param name="cmd_ID">设备ID</param>
        /// <param name="frame_No"></param>
        /// <param name="data"></param>
        public static void Response(IPowerPole pole, byte frame_No, byte[] data)
        { 
            string pacMsg = "";
            if (data[0] == 0x00)
                pacMsg += "查询";
            else
                pacMsg += "设置";

            if (data[1] == 0xff)
                pacMsg += "成功。";
            else
                pacMsg += "失败。";

           
            pacMsg += "数据类型:" + data[3].ToString("X2")+ " ";
            pacMsg += "采集周期:" + BitConverter.ToUInt16(data,4)+"分钟 ";
            pacMsg += "心跳周期:" + ((int)data[6]).ToString()+ "分钟 ";
            //显示发送的数据
            DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.rec,
                    pole,
                    "采样周期",
                    pacMsg));

        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 配置生成报文
        /// </summary>
        /// <param name="cmd_ID">设备ID</param>
        /// <param name="conMode">操作类型——查询、配置</param>
        /// <param name="request_Flag">标志位</param>
        /// <param name="Data_Type">数据类型</param>
        /// <param name="sample_Time">采样周期</param>
        /// <param name="heart_Time">心跳周期</param>
        private static void Con(string cmd_ID, byte conMode, int request_Flag, byte data_Type, int sample_Time, int heart_Time)
        {
            string pacMsg = "";
            CMD_ID = cmd_ID;
            if (conMode == 0x01)                    //配置时，保存配置信息
            {
                Data_Type = data_Type;
                Main_Time  = (UInt16)sample_Time;
                Heart_Time = (byte)heart_Time;
            }
            #region 报文数据生成
            byte[] data = new byte[PacLength];

            data[0] = conMode;                      //参数配置类型标识
            if (conMode == 0)
                pacMsg = "查询";
            if (conMode == 1)
            {
                pacMsg = "设定";
                if((request_Flag%2)==1)
                    pacMsg+="采样周期：" + Main_Time.ToString() +"分钟";
                if((request_Flag/2)==1) 
                    pacMsg+=" 心跳周期：" + ((int)Heart_Time).ToString() + "分钟";

                data[1] = (byte)request_Flag;
                //data[2] = Data_Type;
                data[2] = (byte)PacketAnaLysis.PacketType_Monitoring.Gradient_Tower;
                data[3] = (byte)(Main_Time & 0xFF);                        //采样周期
                data[4] = (byte)(Main_Time >> 8 & 0xFF);
                data[5] = Heart_Time;
            }
            var packet = BuildPacket(data);     //生成报文                                                            
            #endregion

            string errorMsg;
            if (PackeDeal.SendData(CMD_ID, packet, out errorMsg))
            {
                //显示发送的数据
                DisPacket.NewRecord(
                    new DataInfo(
                        DataInfoState.send,
                         Termination.PowerPoleManage.Find(CMD_ID),
                        "采样周期",
                        pacMsg));
            }
        }
        /// <summary>
        /// 报文生成
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] BuildPacket(byte[] data)
        {

            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                PacLength,
                PacketAnaLysis.TypeFrame.Control,
                PacketAnaLysis.PacketType_Control.MainTime,
                FrameNO.GetFrameNO(),
                data);
            return Packet;
        }
        #endregion
    }
}
