using ResModel;
using System;
using ResModel.PowerPole;
using cma.service.PowerPole;

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
            CommandDeal.Image_Photo_UP imgUP;
            try
            {
                switch (packet_Type)
                {
                    case PacketType_Image.Image_Data://数报据
                       
                        imgUP = new CommandDeal.Image_Photo_UP();
                        imgUP.Image_Data(pole, frame_No, data);
                        break;
                    case PacketType_Image.Image_Data_End://结束报
                        imgUP = new CommandDeal.Image_Photo_UP();
                        imgUP.Image_Data_End(pole, frame_No, data);
                        break;
                    case PacketType_Image.Image_Data_Start: 
                        imgUP = new CommandDeal.Image_Photo_UP();
                        imgUP.Ask_To_Up(pole, frame_No, data);
                        break;
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
