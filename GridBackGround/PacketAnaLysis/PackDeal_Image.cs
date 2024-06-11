using ResModel;
using System;
using ResModel.PowerPole;
using cma.service;

namespace GridBackGround.PacketAnaLysis
{
    /// <summary>
    ///  图像数据报
    /// </summary>
    public class PackDeal_Image
    {
        /// <summary> 
        /// 图像数据报分发
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
            try
            {
                switch (packet_Type)
                {
                    default:
                        errorCode = 0x06;
                        break;

                }
            }
            catch(Exception ex) 
            {
                DisPacket.NewRecord(
                   new PackageRecord(
                       PackageRecord_RSType.rec,
                       pole,
                       "远程图像数据",
                       "数据异常:" + ex.Message));
            }
            return dealState;
        }
        
    }
}
