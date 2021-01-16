using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 扩展语音播放协议
    /// </summary>
    public class Comand_voice_update
    {
        #region Public Varialbes
        /// <summary>
        /// 要升级的文件名称
        /// </summary>
        public static string FileName { get; set; }                  //远程升级文件名称
        /// <summary>
        /// 正在升级的装置
        /// </summary>
        public static string CMD_ID { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public static string FilePath { get; set; }
        /// <summary>
        /// 更新状态标志
        /// </summary>
        private static bool UpdateState { get; set; }
        /// <summary>
        /// 文件长度
        /// </summary>
        private static int file_length { get; set; }
        #endregion


        #region 私有成员

        private static int PacketNum = 0;
        public static bool BuBaoState = false;
        /// <summary>
        /// 将要发送的包号列表
        /// </summary>
        private static List<int> ListPacketToSend = new List<int>();
        /// <summary>
        /// 发送数据定时器
        /// </summary>
        private static System.Timers.Timer timerRemotUpdate;

        #endregion

        /// <summary>
        /// 删除语音文件
        /// </summary>
        /// <param name="cmdid"></param>
        /// <param name="voice_type"></param>
        public static void Remove(string cmdid,int voice_type)
        {
            string pacMsg = "";
            CMD_ID = cmdid;
            byte[] data = new byte[2];
            data[0] = (byte)voice_type;
            data[1] = (byte)(voice_type >> 8);
            string msg = string.Format("删除语音文件:{0}", voice_type);
            var packet =  PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                2,
                PacketAnaLysis.TypeFrame.Voice,
                PacketAnaLysis.PacketType_Voice.Delete,
                FrameNO.GetFrameNO(),
                data);
            pack_send(packet, msg);

        }

        #region   远程升级公共处理函数——主动升级、补包数据
        /// <summary>
        /// 远程升级开始报文
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="cmd_ID">设备ID</param>
        public static void StartUpdate(string file, string cmd_ID)
        {
            string fileName;

            //if (UpdateState)
            //    throw new Exception("正在进行远程升级，请等待当前升级完成之后再次开始升级！");
            if (file == null)
                throw new Exception("文件名为空，请重新升级");
            if (!System.IO.Path.IsPathRooted(file))
                throw new Exception("非法路径，请重新升级");
            if (!System.IO.File.Exists(file))
                throw new Exception("文件不存在，请重新升级");
            fileName = System.IO.Path.GetFileName(file);
            if (fileName.Length >= 20)
                throw new Exception("文件名长度不能大于20个字符");

            if (cmd_ID == null)
                throw new Exception("装置名称为空，请重新升级");

            UpdateState = true;

            FilePath = file;
            CMD_ID = cmd_ID;
            BuBaoState = false;                                                 //正常数据包

            FileName = fileName;
            //获得文件名
            if (timerRemotUpdate != null)
                timerRemotUpdate.Stop();

            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);
            file_length = (int)reader.BaseStream.Length;
            PacketNum = (int)(reader.BaseStream.Length / 512) + 1;
            ListPacketToSend.Clear();
            for (int i = 0; i < PacketNum; i++)                                 //整理好将要发送数据的列表
                ListPacketToSend.Add(i);
            reader.Close();

            pack_update_start();
            PacketAnaLysis.DisPacket.NewRecord(
                   new PacketAnaLysis.DataInfo(
                       PacketAnaLysis.DataRecSendState.send,
                        Termination.PowerPoleManage.Find(CMD_ID),
                       "扩展语音播放协议",
                       string.Format("语音文件开始更新，文件名:\"{2}\"，总共{0}字节，{1}包", file_length,PacketNum, fileName)));
            if (Config.SettingsForm.Default.ComMode == "SerialPort")
            {
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 3000;
                timer.Enabled = true;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
                timer.Start();
            }
        }

        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = (System.Timers.Timer)sender;
            timer.Stop();
            StartTimer();
            //throw new NotImplementedException();
        }

        public static void rsp_update_start(Termination.IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string pacMsg = "语音文件开始更新成功";
            StartTimer();

            //显示解析结果
            PacketAnaLysis.DisPacket.NewRecord(
                new PacketAnaLysis.DataInfo(
                    PacketAnaLysis.DataRecSendState.rec,
                    pole,
                    "扩展语音播放协议",
                    pacMsg));
        }

        /// <summary>
        /// 语音文件补报处理
        /// </summary>
        /// <param name="pole">设备handle</param>
        /// <param name="frame_No">帧序号</param>
        /// <param name="data">数据内容</param>
        public static void rsp_update_bubao(Termination.IPowerPole pole,
            byte frame_No, byte[] data)
        {
            string pacMsg = "语音文件补包";
            int BubaoNum = Tools.intTurn.ByteToUint16(data, 0);

            pacMsg += "补包包数为：" + BubaoNum.ToString() + "，分别为:";

                for (int i = 0; i < BubaoNum; i++)
                {
                    int temp = Tools.intTurn.ByteToUint16(data, 2 + i * 2);
                    ListPacketToSend.Add(temp-1);
                    pacMsg += temp.ToString() + " ";
                }
                BuBaoState = true;
                //显示发送的数据
                PacketAnaLysis.DisPacket.NewRecord(
                    new PacketAnaLysis.DataInfo(
                        PacketAnaLysis.DataRecSendState.send,
                         pole,
                        "扩展语音播放协议",
                        pacMsg));

                //软件数据报补包数据上传
                StartTimer();
        }

        /// <summary>
        ///  语音文件下发成功回复处理
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="frame_No"></param>
        /// <param name="data">升级包</param>
        public static void rsp_update_finish(Termination.IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string pacMsg = "语音文件发送完成，";
            
            int BubaoNum = Tools.intTurn.ByteToUint16(data, 24);

            pacMsg += "语音类型：" + BubaoNum.ToString() + "   ";

            //显示发送的数据
            PacketAnaLysis.DisPacket.NewRecord(
                new PacketAnaLysis.DataInfo(
                    PacketAnaLysis.DataRecSendState.send,
                     pole,
                    "扩展语音播放协议",
                    pacMsg));
        }
        /// <summary>
        /// 语音文件删除相应
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="fram_no"></param>
        /// <param name="data"></param>
        public static void rsp_delete(Termination.IPowerPole pole,byte fram_no,byte[] data)
        {
            int BubaoNum = Tools.intTurn.ByteToUint16(data, 0);
            string msg = string.Format("语音文件:{0}， 删除完成", BubaoNum);

            //显示发送的数据
            PacketAnaLysis.DisPacket.NewRecord(
                new PacketAnaLysis.DataInfo(
                    PacketAnaLysis.DataRecSendState.send,
                     pole,
                    "扩展语音播放协议",
                    msg));

        }

        #endregion

        #region  定时发送处理数据
        /// <summary>
        /// 设置并启动定时器
        /// </summary>
        private static void StartTimer()
        {
            if (timerRemotUpdate == null)
            {
                timerRemotUpdate = new System.Timers.Timer(1.5 * 1000);
                timerRemotUpdate.AutoReset = true;
                timerRemotUpdate.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerRemotUpdate);
            }
            else
            {
                timerRemotUpdate.Stop();
            }
            timerRemotUpdate.Start();
        }
        /// <summary>
        /// 定时发送远程升级数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnTimerRemotUpdate(object sender, System.Timers.ElapsedEventArgs e)
        {
            string pacMsg = "";
            if (ListPacketToSend.Count == 0)
            {
                pacMsg = "语音文件发送完成";
                pack_send(pack_update_finish(), pacMsg);
                timerRemotUpdate.Stop();
                return;
            }
            //timerRemotUpdate.Stop();
            int pacNum = 0;
            int pacNO = ListPacketToSend[0];
            ListPacketToSend.RemoveAt(0);   //数据发送完成，移除发送列表
            var UpdataData = pack_update_data(ReadFile(pacNO, ref pacNum), pacNum, pacNO+1);

            if (BuBaoState == false)
                pacMsg = "发送语音文件\"" + FileName + "\",总共" + PacketNum.ToString() + "包，第" + (pacNO+1).ToString() + "包";
            else
                pacMsg = "发送语音文件补包\"" + FileName + "\",总共" + PacketNum.ToString() + "包，第" + (pacNO+1).ToString() + "包";
            //发送远程升级数据
            pack_send(UpdataData, pacMsg);

            //Resend = 0;
        }

        #endregion
        /// <summary>
        /// 从文件中读取远程升级数据
        /// </summary>
        /// <returns></returns>
        private static byte[] ReadFile(int PacketNO, ref int pacNum)
        {
            byte[] ReadData = new byte[512];
            //读取远程升级数据
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);
            pacNum = (int)(reader.BaseStream.Length / 512) + 1;
            reader.BaseStream.Position = (PacketNO) * 512;
            if (((PacketNO + 1) * 512) > reader.BaseStream.Length)    //最后一页数据——不足部分补齐0xff
            {
                int length = (int)reader.BaseStream.Length - ((PacketNO) * 512);
                ReadData = reader.ReadBytes(length);
            }
            else
                ReadData = reader.ReadBytes(512);
            reader.Close();
            return ReadData;
        }
        /// <summary>
        /// 创建语音文件开始更新数据吧
        /// </summary>
        /// <returns></returns>
        private static byte[] pack_update_start()
        {
            byte[] packet = new byte[28];

            //文件名
            Buffer.BlockCopy(Encoding.Default.GetBytes(FileName), 0,
                packet, 0, FileName.Length);
            byte[] len = Tools.intTurn.intToByte4(file_length);
            Buffer.BlockCopy(len, 0, packet, 20, 4);    //文件长度
            //总包数
            byte[] num = Tools.intTurn.intToByte2(PacketNum);
            Buffer.BlockCopy(num, 0, packet, 24, 2);
            //包长度
            byte[] plen = Tools.intTurn.intToByte2(512);
            Buffer.BlockCopy(plen, 0, packet, 26, 2);

            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
               CMD_ID, 28, PacketAnaLysis.TypeFrame.Voice,
               PacketAnaLysis.PacketType_Voice.StartUpdate,
               FrameNO.GetFrameNO(),
               packet);

            string errorMsg;
            PackeDeal.SendData(CMD_ID, Packet, out errorMsg);
            return Packet;
        }


        /// <summary>
        /// 创建语音升级数据包
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pacNum"></param>
        /// <param name="pacNO"></param>
        /// <returns></returns>
        private static byte[] pack_update_data(byte[] data, int pacNum, int pacNO)
        {

            byte[] tempByte = new byte[] { };
            byte[] packet = new byte[data.Length + 6];

            byte[] num = Tools.intTurn.intToByte2(pacNum);          //包数
            Buffer.BlockCopy(num, 0, packet, 0, 2);
            num = Tools.intTurn.intToByte2(pacNO);                  //包号
            Buffer.BlockCopy(num, 0, packet, 2, 2);
            num = Tools.intTurn.intToByte2(data.Length);            //数据长度
            Buffer.BlockCopy(num, 0, packet, 4, 2);
            Buffer.BlockCopy(data, 0, packet, 6, data.Length);     //包内容
            return PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                data.Length + 6,
                PacketAnaLysis.TypeFrame.Voice,
                PacketAnaLysis.PacketType_Voice.UpdateData,
                FrameNO.GetFrameNO(),
                packet);
        }


        /// <summary>
        /// 创建语音升级结束数据包
        /// </summary>
        /// <returns></returns>
        private static byte[] pack_update_finish()
        {
            byte[] packet = new byte[60];
            //文件名
            Buffer.BlockCopy(Encoding.Default.GetBytes(FileName), 0,
                packet, 0, FileName.Length);
            byte[] len = Tools.intTurn.intToByte4(file_length);
            Buffer.BlockCopy(len, 0, packet, 20, 4);    //文件长度

            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                24,
                PacketAnaLysis.TypeFrame.Voice,
                PacketAnaLysis.PacketType_Voice.UpdateEnd,
                FrameNO.GetFrameNO(),
                packet);
            return Packet;
        }

        /// <summary>
        /// 发送远程升级数据
        /// </summary>
        /// <param name="data">数据包呢日用</param>
        /// <param name="pacMsg">提示信息</param>
        private static void pack_send(byte[] data, string pacMsg)
        {

            string errorMsg;
            if (PackeDeal.SendData(CMD_ID, data, out errorMsg))
            {
                //显示发送的数据
                PacketAnaLysis.DisPacket.NewRecord(
                    new PacketAnaLysis.DataInfo(
                        PacketAnaLysis.DataRecSendState.send,
                         Termination.PowerPoleManage.Find(CMD_ID),
                        "扩展语音播放协议",
                        pacMsg));
            }

        }
    }
}
