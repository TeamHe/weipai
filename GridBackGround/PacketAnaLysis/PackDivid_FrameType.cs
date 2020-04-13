using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sodao.FastSocket.Server.Command;

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
        public static bool PackDivid(Termination.IPowerPole pole, 
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
                //监测数据报
                case TypeFrame.Monitoring:
                    errorCode = 0;
                    workState = PackDeal_Data.PacketDeivid(
                        pole,
                        packet_Type,
                        frame_No,
                        data,
                        ref errorCode);
                    break;
                //控制数据报
                case TypeFrame.ResControl:
                    workState = PackDeal_Control.PacketDeivid(
                        pole,
                        packet_Type,
                        frame_No,
                        data,
                        ref errorCode);
                    break;
                //远程图像数据报
                case TypeFrame.Image:
                    PackDeal_Image.PacketDeivid(
                        pole,
                        packet_Type,
                        frame_No,
                        data,
                        ref errorCode);
                    break;
                 //图像控制响应报
                case TypeFrame.ResControlImage:
                    PackDeal_Image_Control.PacketDeivid(
                        pole,
                        packet_Type,
                        frame_No,
                        data,
                        ref errorCode);
                    break;
                //工作状态报
                case TypeFrame.WorkState:
                    workState = PackDeal_WorkState.PacketDeivid(
                        pole,
                        packet_Type,
                        frame_No,
                        data,
                        ref errorCode);
                    break;
                case TypeFrame.PrivateRes:
                    workState = PackDeal_PrivateCon.PacketDeivid(
                        pole,
                        packet_Type,
                        frame_No,
                        data,
                        ref errorCode);
                    break;
                default:
                    errorCode = 0x04;
                    break;
            }
            return workState;
        }        
    }
}
