﻿using ResModel;

namespace GridBackGround.PacketAnaLysis
{
    /// <summary>
    /// 报文详细内容解析
    /// </summary>
    public class PackDivid_FrameType
    {
        
        /// <summary>
        /// 根据报文类型数据分发
        /// </summary>
        /// <param name="cmdInfo"></param>
        /// <returns></returns>
        public static bool PackDivid(IPowerPole pole, 
            int frame_Type,
            int packet_Type,
            byte frame_No,
            byte[] data,
            ref int errorCode)
        {
            bool workState = false;
            errorCode = 0;
            switch (frame_Type)
            {
                default:
                    errorCode = 0x04;
                    break;
            }
            return workState;
        }        
    }
}
