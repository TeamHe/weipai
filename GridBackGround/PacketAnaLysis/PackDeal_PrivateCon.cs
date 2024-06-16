using ResModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.PacketAnaLysis
{
    class PackDeal_PrivateCon
    {
        /// <summary>
        /// 工作状态报分发
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="packet_Type"></param>
        /// <param name="frame_No"></param>
        /// <param name="data"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static bool PacketDeivid(IPowerPole pole,
            int packet_Type,
            byte frame_No,
            byte[] data,
            ref int errorCode)
        {
            bool dealState = false;
            switch (packet_Type)
            {

                case PrivatControl.UserPhone:    //用户手机号
                    CommandDeal.Private.UserPhone.Response(pole, frame_No, data);
                    break;

                default:
                    errorCode = 0x05;
                    break;

            }
            return dealState;
        }
    }

    class PackDeal_VoiceRes
    {
        /// <summary>
        /// 扩展语音播放协议
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="packet_Type"></param>
        /// <param name="frame_No"></param>
        /// <param name="data"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static bool PacketDeivid(IPowerPole pole,
            int packet_Type,
            byte frame_No,
            byte[] data,
            ref int errorCode)
        {
            bool dealState = false;
            switch (packet_Type)
            {

                default:
                    errorCode = 0x05;
                    break;

            }
            return dealState;
        }

    }

}
