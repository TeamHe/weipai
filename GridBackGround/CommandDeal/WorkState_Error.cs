using ResModel;
using System;
using System.Text;
using ResModel.PowerPole;
using cma.service;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 心跳数据报
    /// </summary>
    public class WorkState_Error
    {
        /// <summary>
        /// 心跳数据报解析
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_No"></param>
        /// <param name="data"></param>
        public static void Error(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string psMsg;
            int startNo = 0;
            //设备时间
            DateTime Clocktime_Stamp = Tools.TimeUtil.BytesToDate(data);
            psMsg = "故障时间：" + Clocktime_Stamp.ToString();
            startNo += 4;
            byte[] error = new byte[data.Length - 4];
            Buffer.BlockCopy(data, 4, error, 0, data.Length - 4);
            string ErrorMsg = Encoding.Default.GetString(error);
            
            psMsg = "故障描述：" + ErrorMsg;

            DisPacket.NewRecord(
                   new PackageRecord(
                       PackageRecord_RSType.rec,
                       pole,
                       "装置故障",
                       psMsg));
            ResError(pole.CMD_ID, frame_No);
        }

        public static void ResError(string cmd_ID,
           byte frame_No
          )
        { 
            byte[] data = new byte[1];
            
            data[0] = 0xff;
            //data[1] = 0x00;
           
            var packet =PacketAnaLysis.BuildPacket.PackBuild(
                            cmd_ID,
                            1,                                      //长度
                            PacketAnaLysis.TypeFrame.ResWorkState,  //frameType
                            PacketAnaLysis.PacketType_WorkState.Error,//PacketType
                            frame_No,                                  //FrameNo
                            data
                            );
            string errormsg;
            PackeDeal.SendData(cmd_ID, packet, out errormsg);
        }
    }
}
