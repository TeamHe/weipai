using ResModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.PacketAnaLysis
{
    /// <summary>
    ///  图像控制响应报
    /// </summary>
    public class PackDeal_Image_Control
    {
        /// <summary> 
        /// 图像控制响应报分发
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
            switch (packet_Type)
            {
                //时间表响应
                case PacketType_Image.Photo_TimeTable:
                    CommandDeal.Image_TimeTable.Response(pole, frame_No, data);
                    break;
                    //手动拍照片
                case PacketType_Image.Take_Photo:
                    CommandDeal.Image_Photo_MAN.Response(pole, frame_No, data);
                    break;
                    //开始标记
                case PacketType_Image.Image_Data_Start:
                    imgUP = new CommandDeal.Image_Photo_UP();
                    imgUP.Ask_To_Up(pole, frame_No, data);
                    break;
                    //结束标记
                case PacketType_Image.Image_Data_End:
                    imgUP = new CommandDeal.Image_Photo_UP();
                    imgUP.Image_Data_End(pole, frame_No, data);
                    break;
                    //摄像机远程调节响应
                case PacketType_Image.Camera_Adjust:
                    CommandDeal.Image_Photo_Adjust.Response(pole, frame_No, data);
                    break;
                default:
                    errorCode = 0x05;
                    break;

            }
            return dealState;
        }
        
    }
}
