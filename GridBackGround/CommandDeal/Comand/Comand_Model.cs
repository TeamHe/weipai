using ResModel;
using System;
using System.Collections.Generic;
using System.Text;
using ResModel.PowerPole;
using ResModel.gw;
using cma.service.PowerPole;

namespace GridBackGround.CommandDeal
{
    public class Comand_Model
    {
        private static string CMD_ID;
        private static int PacLength = 1 + 1;

        #region 公共函数
        public static void Query(string cmd_ID)
        {
            Con(cmd_ID, false,null);
        }

        public static void Set(string cmd_ID, List<IModelData> model)
        {
            Con(cmd_ID,true,model);
        }

        public static void Response(IPowerPole pole, byte frame_No, byte[] data)
        {
            string pacMsg = "";
            if (data[0] == 0x00)
                pacMsg += "查询";
            else
                pacMsg += "设置";

            if (data[1] == 0xff)
                pacMsg += "成功。";
            else
                pacMsg += "失败。";

            pacMsg += "参数个数：" + ((int)data[2]).ToString() + "。 ";
            for (int i = 0; i < data[2]; i++)
            {
                try
                {
                    string name = Encoding.Default.GetString(data, 3 + i * 11, 6);
                    pacMsg += name + " ";
                    switch (data[3 + i * 11 + 6])
                    {
                        case 0x00:
                            pacMsg += BitConverter.ToUInt32(data, 3 + i * 11 + 6 + 1).ToString();
                            break;
                        case 0x01:
                            pacMsg += BitConverter.ToInt32(data, 3 + i * 11 + 6 + 1).ToString();
                            break;
                        case 0x02:
                            pacMsg += BitConverter.ToSingle(data, 3 + i * 11 + 6 + 1).ToString("f2");
                            break;
                    }
                    pacMsg += " ";
                }
                catch
                {
                    break;
                }

            }
            //显示发送的数据
            DisPacket.NewRecord(
                new PackageRecord(
                    PackageRecord_RSType.rec,
                    pole,
                    "模型参数",
                    pacMsg));

        }

        public static void Ayanlise(string cmd_id, byte[] data)
        {
            string pacMsg = "WEB操作：";
            if (data[0] == 0x00)
                pacMsg += "查询 ";
            else
                pacMsg += "设置 ";
            pacMsg += "参数个数：" + ((int)data[1]).ToString() + " ";
            for (int i = 0; i < data[1]; i++)
            {
                try
                {
                    string name = Encoding.Default.GetString(data, 2 + i * 11, 6);
                    pacMsg += name + " ";
                    switch (data[2 + i * 11 + 6])
                    {
                        case 0x00:
                            pacMsg += BitConverter.ToUInt32(data, 2 + i * 11 + 6 + 1).ToString();
                            break;
                        case 0x01:
                            pacMsg += BitConverter.ToInt32(data, 2 + i * 11 + 6 + 1).ToString();
                            break;
                        case 0x02:
                            pacMsg += BitConverter.ToSingle(data, 2 + i * 11 + 6 + 1).ToString("f2");
                            break;
                    }
                    pacMsg += " ";
                }
                catch
                {
                    break;
                }

            }

            //显示发送的数据
            DisPacket.NewRecord(
                new PackageRecord(
                    PackageRecord_RSType.rec,
                    Termination.PowerPoleManage.Find(cmd_id),
                    "模型参数",
                    pacMsg));
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 配置生成报文
        /// </summary>
        /// <param name="cmd_ID">设备ID</param>
        /// <param name="conMode">操作类型——查询、配置</param>
        /// <param name="request_Flag">标志位</param>
        /// <param name="Data_Type">数据类型</param>
        /// <param name="sample_Time">采样周期</param>
        /// <param name="heart_Time">心跳周期</param>
        private static void Con(string cmd_ID, bool conMode, List<IModelData> model)
        {
            string pacMsg = "";
            CMD_ID = cmd_ID;
            if(model!= null)
                PacLength = 2 + 11 * model.Count;

            byte[] data = new byte[PacLength];
            if (conMode)
            {
                byte[] value = new byte[4];
                data[0] = 0x01;
                pacMsg += "配置 ";
                data[1] = (byte)(model.Count & 0xff);
                for (int i = 0; i < model.Count; i++)
                {
                    pacMsg += model[i].Name + " ";
                    Buffer.BlockCopy(Encoding.Default.GetBytes(model[i].Name), 0, data, i * 11 + 2, 6);
                    data[i * 11 + 2 + 6] = (byte)(model[i].DataType & 0xff);
                    pacMsg += model[i].Data.ToString("f2") + " ";
                    value = BitConverter.GetBytes(model[i].Data);
                    Buffer.BlockCopy(value, 0, data, i * 11 + 2 +6+1, 4);
                }


            }
            else
            {
                data[0] = 0x00;
                pacMsg += "查询";
            }
            var packet = BuildPacket(data);     //生成报文      
            string errorMsg;
            if (PackeDeal.SendData(CMD_ID, packet, out errorMsg))
            {
                //显示发送的数据
                DisPacket.NewRecord(
                    new PackageRecord(
                        PackageRecord_RSType.send,
                         Termination.PowerPoleManage.Find(CMD_ID),
                        "模型参数",
                        pacMsg));
            }
        }

        /// <summary>
        /// 报文生成
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] BuildPacket(byte[] data)
        {

            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                PacLength,
                PacketAnaLysis.TypeFrame.Control,
                PacketAnaLysis.PacketType_Control.Model,
                FrameNO.GetFrameNO(),
                data);
            return Packet;
        }
        #endregion

    }
}
  
        