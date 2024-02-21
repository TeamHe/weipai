using ResModel;
using System;
using ResModel.PowerPole;
using cma.service;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 心跳数据报
    /// </summary>
    public class WorkState_Heart
    {
        /// <summary>
        /// 心跳数据报解析
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_No"></param>
        /// <param name="data"></param>
        public static void Heart(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string psMsg;
            int startNo = 0;
            //设备时间
            DateTime Clocktime_Stamp = Tools.TimeUtil.BytesToDate(data);
            psMsg = "设备时间：" + Clocktime_Stamp.ToString();
            startNo += 4;
            
            //电源电压
            float Battery_Voltage = Tools.FloatTurn.BytesToFloat(data, startNo);
            psMsg += " 电源电压：" + Battery_Voltage.ToString("0.0") + "V";
            startNo += 4;

            //工作温度
            float Operation_Temperature = Tools.FloatTurn.BytesToFloat(data, startNo);
            psMsg += " 工作温度：" + Operation_Temperature.ToString("0.0") + "℃";
            startNo += 4;

            //剩余电量
            float Battery_Capacity = Tools.FloatTurn.BytesToFloat(data, startNo);
            psMsg += " 剩余电量：" + Battery_Capacity.ToString("0.0") + "Ah";
            startNo += 4;

            //浮充状态
            byte FloatingCharge = data[startNo];
            string chargeState = "";
            if (FloatingCharge == 0x00) chargeState = "充电";
            if (FloatingCharge == 0x01) chargeState = "放电";
            psMsg += " 浮充状态：" + chargeState;
            startNo += 1;

            //工作总时间
            UInt32 Total_Working_Time = BitConverter.ToUInt32(data,startNo);
            psMsg += " 工作总时间：" + Total_Working_Time.ToString() + "小时";
            startNo += 4;

            //本次连续工作总时间
            UInt32 Working_Time = BitConverter.ToUInt32(data,startNo);
            psMsg += " 本次连续工作时间：" + Working_Time.ToString() + "小时";
            startNo += 4;

            //连接状态
            string Connection_State="";
            if (data[startNo] == 0x00) Connection_State = "所有传感器连接正常";
            if (data[startNo] == 0x01) Connection_State = "有一个或多个传感器断开连接";
            psMsg += " 连接状态：" + Connection_State;
            startNo += 1;

            //发送流量
            UInt32 Send_Flow = BitConverter.ToUInt32(data, startNo);
            psMsg += " 当月发送流量：" + Send_Flow.ToString() +"字节";
            startNo += 4;

            //接收流量
            UInt32 Receive_Flow = BitConverter.ToUInt32(data, startNo);
            psMsg += " 当月接收流量：" + Receive_Flow.ToString() + "字节";
            startNo += 4;

            //通信协议版本号
            string Protocol_Version = "";
            for (int i = 0; i < 3; i++) 
                Protocol_Version += data[startNo++].ToString() + '.';
            Protocol_Version += data[startNo].ToString();
            psMsg += " 通信协议版本号：" + Protocol_Version;

            DisPacket.NewRecord(
                   new PackageRecord(
                       PackageRecord_RSType.rec,
                       pole,
                       "心跳",
                       psMsg)); 
            TimeSpan time =DateTime.Now.Subtract(Clocktime_Stamp);
            bool timing = false;
            //心跳校时取值差为1分钟
            if ((time.TotalSeconds >60)||(time.TotalSeconds < -60))
                timing = true;
            ResHeart(pole.CMD_ID, frame_No, timing);
        }

        public static void ResHeart(string cmd_ID,
           byte frame_No,
            bool Timing
          )
        { 
            byte[] data = new byte[6];
            
            data[0] = 0xff;
            data[1] = 0x00;
            if(Timing)
            {
                var time = Tools.TimeUtil.GetBytesTime();
                Buffer.BlockCopy(time, 0, data, 2, 4);
            }
            var packet = PacketAnaLysis.BuildPacket.PackBuild(
                            cmd_ID,
                            6,                                         //长度
                            PacketAnaLysis.TypeFrame.ResWorkState,    //frameType
                            PacketAnaLysis.PacketType_WorkState.Heart,//PacketType
                            frame_No,                                  //FrameNo
                            data
                            );
            string errormsg;
            PackeDeal.SendData(cmd_ID, packet, out errormsg);
        }
    }
}
