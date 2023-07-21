using ResModel;
using ResModel.PowerPole;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 图形参数配置
    /// </summary>
    public class Image_Model
    {
        private static string CMD_ID;
        private static int PacLength = 1 //配置类型
            + 1     //标志位
            + 1     //色彩选择
            + 1     //图像分辨率
            + 1     //亮度
            + 1     //对比度
            + 1     //饱和度
            ;
        private static int RecLength = 1 + 1 +1 //类型标志，配置状态，标志位
            + 1 + 1 //色彩选择+ 图像分辨率
            + 1 + 1 //亮度、对比度
            +1;//饱和度
        
        #region 公共函数
        /// <summary>
        /// 状态监测装置 ID 查询
        /// </summary>
        /// <param name="cmd_ID">设备ID号</param>
        public static void Query(string cmd_ID)
        {
            Con(cmd_ID, 0x00, 0,0x00,0,0,0,0);
        }

        /// <summary>
        /// 图像采集参数配置
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="Color_Select">色彩选择</param>
        /// <param name="Resolution">图像分辨率</param>
        /// <param name="Luminance">亮度</param>
        /// <param name="Contrast">对比度</param>
        /// <param name="Saturation">饱和度</param>
        public static void Set(string cmd_ID,int request_flag,
            int Color_Select,
            int Resolution,
            int Luminance,
            int Contrast,
            int Saturation)
        {
            Con(cmd_ID, 0x01,request_flag,Color_Select,Resolution, 
                Luminance,Contrast,Saturation);
        }

        /// <summary>
        /// 图像采集参数响应
        /// </summary>
        /// <param name="cmd_ID">设备ID </param>
        /// <param name="frame_No"></param>
        /// <param name="data">数据</param>
        public static void Response(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string pacMsg = "";
            int StartNO = 0;
            if (data.Length != RecLength)
                return;
            //参数配置类型标识
            if (data[StartNO] == 0x00)
                pacMsg += "查询";
            else
                pacMsg += "设置";
            StartNO++;
            //数据发送状态
            if (data[StartNO] == 0xff)
                pacMsg += "成功，";
            else
                pacMsg += "失败,";
            StartNO++;
            //标志位
            StartNO++;

            //色彩
            pacMsg += "色彩:";
            if (data[StartNO] == 0)
                pacMsg += "黑白 ";
            else
                pacMsg += "彩色 ";
            StartNO++;

            //图像分辨率
            pacMsg += "图像分辨率:" + GetImageDPI(data[StartNO++]) + " ";
            //亮度
            pacMsg += "亮度:" + ((int)data[StartNO++]).ToString() + " ";

            //对比度
            pacMsg += "对比度:" + ((int)data[StartNO++]).ToString() + " ";

            //饱和度
            pacMsg += "饱和度:" + ((int)data[StartNO++]).ToString() + " ";

            PacketAnaLysis.DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.rec,
                    pole,
                    "图像采集参数",
                    pacMsg)); 
        }
        #endregion


        #region 私有函数
        /// <summary>
        /// 图像采集参数配置
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="request_Set_Flag">参数配置类型标识</param>
        /// <param name="Color_Select">色彩选择</param>
        /// <param name="Resolution">图像分辨率</param>
        /// <param name="Luminance">亮度</param>
        /// <param name="Contrast">对比度</param>
        /// <param name="Saturation">饱和度</param>
        private static void Con(string cmd_ID, byte request_Set_Flag, int request_Flag,
            int Color_Select ,
            int Resolution, 
            int Luminance,
            int Contrast,
            int Saturation)
        {
   
            string pacMsg = "";
            CMD_ID = cmd_ID;

            #region 报文数据生成
            byte[] data = new byte[PacLength];
            int StartNo = 0;
            data[StartNo] = request_Set_Flag;                      //参数配置类型标识
            StartNo++;
            if (request_Set_Flag == 0)
                pacMsg = "查询。";
            if (request_Set_Flag == 1)       //设置
            {

                data[StartNo++] = (byte)request_Flag;
                pacMsg = "设定，";
                if((request_Flag & 0x01) == 0x01 )
                {
                    //色彩选择
                    data[StartNo] = (byte)(Color_Select & 0xff);
                    pacMsg += "色彩:";
                    if (Color_Select == 0)
                        pacMsg += "黑白 ";
                    else
                        pacMsg += "彩色 ";
                }
                StartNo++;
                //图像分辨率
                if ((request_Flag & 0x02) == 0x02)
                {
                    data[StartNo] = (byte)(Resolution & 0xff);
                    pacMsg += "图像分辨率:" + GetImageDPI(Resolution) + " ";
                }
                StartNo++;
                //亮度
                if ((request_Flag & 0x04) == 0x04)
                {
                    data[StartNo] = (byte)(Luminance & 0xff);
                    pacMsg += "亮度:" + Luminance.ToString() + " ";
                }
                StartNo++;
                //对比度
                if ((request_Flag & 0x08) == 0x08)
                {
                    data[StartNo] = (byte)(Contrast & 0xff);
                    pacMsg += "对比度:" + Contrast.ToString() + " ";
                }
                StartNo++;
                //饱和度
                if ((request_Flag & 0x10) == 0x10)
                {
                    data[StartNo] = (byte)(Saturation & 0xff);
                    pacMsg += "饱和度:" + Saturation.ToString() + " ";
                }
                StartNo++;
            }
            var packet = BuildPacket(data);     //生成报文                                                            
            #endregion

            string errorMsg;
            if (PackeDeal.SendData(CMD_ID, packet, out errorMsg))
            {
                //显示发送的数据
                PacketAnaLysis.DisPacket.NewRecord(
                    new DataInfo(
                        DataInfoState.send,
                         Termination.PowerPoleManage.Find(CMD_ID),
                        "图像采集参数",
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
                PacketAnaLysis.PacketType_Image.Model,
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
                    msg += "①320 * 240 ";
                    break;
                case 2:
                    msg += "②640 * 480 ";
                    break;
                case 3:
                    msg += "③704 X 576 ";
                    break;
                case 4:
                    msg += "④720 X 480（720P） ";
                    break;
                case 5:
                    msg += "⑤1280 X 720（720P）  ";
                    break;
                case 6:
                    msg += "⑥1920 X 1080（1080P） ";
                    break;
                case 7:
                    msg += "⑦2592 X 1944 ";
                    break;
                default:
                    msg += "未识别分辨率" + value.ToString();
                    break;
            }
            return msg;
        }
        #endregion
    }
}
