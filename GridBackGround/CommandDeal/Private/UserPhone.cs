using ResModel;
using System;
using System.Text;
using ResModel.PowerPole;

namespace GridBackGround.CommandDeal.Private
{
    class UserPhone
    {
        private static string CMD_ID;
        /// <summary>
        /// 上次操作IP地址
        /// </summary>
        //用户编号
        public static int UsNO { get; set; }
        //用户手机号
        public static string PhoneNO { get; set; }

        #region 公共函数
        /// <summary>
        /// 查询设备上位机IP地址
        /// </summary>
        /// <param name="cmd_ID">设备ID号</param>
        public static void Query(string cmd_ID, int usNO)
        {
            Con(cmd_ID, 0x00, usNO, "");
        }

        /// <summary>
        /// 设置设备上位机IP地址信息
        /// </summary>
        /// <param name="cmd_ID">设备ID</param>
        /// <param name="ip">设定的IP端口号</param>
        /// <param name="port">端口号</param>
        /// <param name="request_Flag">设置类型</param>
        public static void Set(string cmd_ID, int userNo, string PhoneNO)
        {
            Con(cmd_ID, 0x01, userNo, PhoneNO);
        }

        /// <summary>
        /// IP配置响应
        /// </summary>
        /// <param name="cmd_ID">设备ID </param>
        /// <param name="frame_No"></param>
        /// <param name="data">数据</param>
        public static void Response(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string pacMsg = "";
            //if (data.Length != PacLength + 1)
            //    return;
            if (data[0] == 0x00)
                pacMsg += "查询";
            else
                pacMsg += "设置";

            if (data[1] == 0xff)
                pacMsg += "成功。";
            else
                pacMsg += "失败。";

            int usNO = data[2];
            UsNO = usNO;
            pacMsg += "用户编号：" + usNO.ToString();
            string phone = Encoding.Default.GetString(data, 3, 11);
            PhoneNO = phone;
            pacMsg += "  用户手机号码：" + phone;
            //显示数据响应解析结果
            DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.send,
                    pole,
                    "用户手机号",
                    pacMsg)); ;
        }
        #endregion


        #region 私有函数
        /// <summary>
        /// 终端上位机IP配置
        /// </summary>
        /// <param name="cmd_ID">设备ID</param>
        /// <param name="ConMode">配置类型——查询，配置</param>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="request_Flag">设置标识</param>
        private static void Con(string cmd_ID, byte ConMode, int usNO, string phoneNO)
        {
            string pacMsg = "";
            CMD_ID = cmd_ID;
            if (ConMode == 0x01)                    //配置时，保存配置信息
            {
                UsNO = usNO;
                PhoneNO = phoneNO;
            }
            #region 报文数据生成
            byte[] data = new byte[13];

            data[0] = ConMode;                      //参数配置类型标识
            if (ConMode == 0)
                pacMsg = "查询";
            else
                pacMsg = "设定";
            data[1] = (byte)usNO;

            pacMsg += "用户" + usNO.ToString() + "的手机号";

            if (ConMode == 1)
            {
                var phone = Encoding.ASCII.GetBytes(phoneNO);
                if (phone.Length <= 11)
                    Buffer.BlockCopy(phone, 0, data, 2, phone.Length);
                else
                    Buffer.BlockCopy(phone, 0, data, 8, 11);
                pacMsg += " 为： " + phoneNO;
            }
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
                        "用户手机号",
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
                13,
                PacketAnaLysis.TypeFrame.PrivateCon,
                PacketAnaLysis.PrivatControl.UserPhone,
                FrameNO.GetFrameNO(),
                data);
            return Packet;
        }
        #endregion
    }
}
