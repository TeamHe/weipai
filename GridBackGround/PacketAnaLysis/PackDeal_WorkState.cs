using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.PacketAnaLysis
{
    public class PackDeal_WorkState
    {
        /// <summary>
        /// 工作状态报分发
        /// </summary>
        /// <param name="cmdInfo"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static bool PacketDeivid(
            Termination.IPowerPole pole,
            int packet_Type,
            byte frame_No,
            byte[] data,
            ref int errorCode)
        {
            bool dealState = false;
            switch (packet_Type)
            {
                case PacketType_WorkState.Heart:
                    DateTime start = DateTime.Now;
                    CommandDeal.WorkState_Heart.Heart(pole,frame_No,data);
                    TimeSpan span = DateTime.Now.Subtract(start);
                    Console.WriteLine("data  :{0} ms", span.TotalMilliseconds);
                    break;
                case PacketType_WorkState.Error:
                    CommandDeal.WorkState_Error.Error(pole, frame_No, data);
                    break;
                default:
                    errorCode = 0x05;
                    break;

            }
            return dealState;
        }
      
    }
}
