using ResModel;
using ResModel.PowerPole;
using cma.service.PowerPole;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 手动请求拍照片
    /// </summary>
    public class Image_Photo_MAN
    {
        private static string CMD_ID;
        private static int PacLength = 1 +1;//通道号，预置位号
           
        private static int RecLength = 1 ;//数据发送状态
        
        #region 公共函数

        /// <summary>
        /// 手动请求拍照片
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="Channel_No"></param>
        /// <param name="Presetting_No"></param>
        public static bool Set(string cmd_ID, int Channel_No, int Presetting_No)
        {
            return Con(cmd_ID, Channel_No,Presetting_No);
        }

        /// <summary>
        /// 拍照命令响应
        /// </summary>
        /// <param name="cmd_ID">设备ID </param>
        /// <param name="frame_No"></param>
        /// <param name="data">数据</param>
        public static void Response(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string pacMsg = "拍照";
            if (data.Length != RecLength)
                return;
            Error_Code code = Error_Code.Success;
            if (data[0] == 0xff)
                pacMsg += "成功。";
            else
            {
                pacMsg += "失败。";
                code = Error_Code.DeviceError;
            }
            //Termination.PowerPole powerPole = pole as Termination.PowerPole;
            //powerPole.OnPhotiongFinish(code);


            DisPacket.NewRecord(
                new PackageRecord(
                    PackageRecord_RSType.rec,
                    pole,
                    "手动拍照片",
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
        private static bool Con(string cmd_ID, int Channel_No, int Presetting_No) 
        {
            string pacMsg = "";
            CMD_ID = cmd_ID;

            #region 报文数据生成
            byte[] data = new byte[PacLength];
            pacMsg = "通道号：" + Channel_No.ToString() + " ";
            data[0] = (byte)(Channel_No & 0xff);                      //参数配置类型标识
            pacMsg += "预置位号：" + Presetting_No.ToString() + " ";
            data[1] = (byte)(Presetting_No & 0xff);

            var packet = BuildPacket(data);     //生成报文                                                            
            #endregion

            string errorMsg;
            if (PackeDeal.SendData(CMD_ID, packet, out errorMsg))
            {
                //显示发送的数据
                DisPacket.NewRecord(
                    new PackageRecord(
                        PackageRecord_RSType.send,
                         Termination.PowerPoleManage.Find(CMD_ID),
                        "手动拍照片",
                        pacMsg));
                return true;
            }
            else
            {
                return false;
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
                PacketAnaLysis.PacketType_Image.Take_Photo,
                FrameNO.GetFrameNO(),
                data);
            return Packet;
        }

        private static string GetImageDPI(int value)
        {
            string msg = "";
            switch (value)
            {
                case 1:
                    msg += "320 X 240 ";
                    break;
                case 2:
                    msg += "640 X 480 ";
                    break;
                case 3:
                    msg += "704 X 576 ";
                    break;
                case 4:
                    msg += "720 X 480（标清）  ";
                    break;
                case 5:
                    msg += "1280 X 720（720P）  ";
                    break;
                case 6:
                    msg += "1920 X 1080（1080P） ";
                    break;
                case 7:
                    msg += "1280P 或者更高 ";
                    break;
                default:
                    msg += "未识别";
                    break;
            }
            return msg;
        }
        #endregion
    }
}
