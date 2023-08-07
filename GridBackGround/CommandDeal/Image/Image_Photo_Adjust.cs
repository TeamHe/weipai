using ResModel;
using ResModel.EQU;
using Tools;
using ResModel.PowerPole;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 摄像头远程调节
    /// </summary>
    public class Image_Photo_Adjust
    {
        private static string CMD_ID;
        private static int PacLength = 1 +1 +1;//通道号，预置位号，调节命令
           
        private static int RecLength = 1 ;//数据发送状态
        
        #region 公共函数

        /// <summary>
        /// 摄像头调整
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="Channel_No"></param>
        /// <param name="Presetting_No"></param>
        public static void Adjust(string cmd_ID, int Channel_No, int Presetting_No,int Command)
        {
            Con(cmd_ID, Channel_No,Presetting_No,(Pic_Remote_OPtion)Command);
        }

        /// <summary>
        /// 摄像头远程调节
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="Channel_No"></param>
        /// <param name="Presetting_No"></param>
        /// <param name="option"></param>
        public static void Adjust(string cmd_ID, int Channel_No, int Presetting_No, Pic_Remote_OPtion option)
        {
            Con(cmd_ID, Channel_No, Presetting_No, option);
        }

        /// <summary>
        /// 摄像头远程调节响应
        /// </summary>
        /// <param name="cmd_ID">设备ID </param>
        /// <param name="frame_No"></param>
        /// <param name="data">数据</param>
        public static void Response(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string pacMsg = "调节";
            if (data.Length != RecLength)
                return;
 
            if (data[0] == 0xff)
                pacMsg += "成功。";
            else
                pacMsg += "失败。";

            DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.rec,
                    pole,
                    "摄像头远程调节",
                    pacMsg)); 
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 手动请求拍照片
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="Channel_No"></param>
        /// <param name="Presetting_No"></param>
        private static void Con(string cmd_ID, int Channel_No, int Presetting_No,Pic_Remote_OPtion option) 
        {
            string pacMsg = "";
            CMD_ID = cmd_ID;

            #region 报文数据生成
            byte[] data = new byte[PacLength];
            pacMsg = "通道号：" + Channel_No.ToString() + " ";
            data[0] = (byte)(Channel_No & 0xff);                      //参数配置类型标识
            pacMsg += "预置位号：" + Presetting_No.ToString() + " ";
            data[1] = (byte)(Presetting_No & 0xff);
            pacMsg += "命令：" + option.GetDescription() + " ";
            data[2] = (byte)((int)option);

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
                        "摄像头远程调节",
                        pacMsg));
            }

        }

        /// <summary>
        /// 创建报文
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] BuildPacket(byte[] data)
        {

            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                PacLength,
                PacketAnaLysis.TypeFrame.ControlImage,
                PacketAnaLysis.PacketType_Image.Camera_Adjust,
                FrameNO.GetFrameNO(),
                data);
            return Packet;
        }

       
        #endregion
    }
}
