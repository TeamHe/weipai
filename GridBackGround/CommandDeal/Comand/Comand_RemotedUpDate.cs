using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ResModel;
using ResModel.PowerPole;
using Sodao.FastSocket.Server.Command;
using cma.service.PowerPole;

namespace GridBackGround.CommandDeal
{
    public class Comand_RemotedUpDate
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
        /// 厂商名称
        /// </summary>
        public static string Factory_Name { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public static string Model { get; set; }
        /// <summary>
        /// 硬件版本
        /// </summary>
        public static string HardVersion { get; set; }
        /// <summary>
        /// 软件版本
        /// </summary>
        public static string SoftVersion { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public static string FilePath { get; set; }
        /// <summary>
        /// 软件更新时间
        /// </summary>
        public static DateTime Time { get; set; }
        /// <summary>
        /// 更新状态标志
        /// </summary>
        private static bool UpdateState { get; set; }

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

        #region   远程升级公共处理函数——主动升级、补包数据
        /// <summary>
        /// 远程升级开始报文
        /// </summary>
        /// <param name="factory_Name"></param>
        /// <param name="model"></param>
        /// <param name="hardVersion"></param>
        /// <param name="SoftVersion"></param>
        /// <param name="time"></param>
        /// <param name="file"></param>
        /// <param name="cmd_ID"></param>
        public static void RunRemotedUpDate(
            string factory_Name,
            string model,
            string hardVersion,
            string softVersion,
            DateTime time,
            string file,
            string cmd_ID)
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

            Factory_Name = factory_Name;
            Model = model;
            HardVersion = hardVersion;
            SoftVersion = softVersion;
            Time = time;
            FilePath = file;
            CMD_ID = cmd_ID;
            BuBaoState = false;                                                 //正常数据包

            FileName = fileName;
            //获得文件名
            if (timerRemotUpdate != null)
                timerRemotUpdate.Stop();

            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);
            PacketNum = (int)(reader.BaseStream.Length / 512) + 1;
            ListPacketToSend.Clear();
            for (int i = 0; i < PacketNum; i++)                                 //整理好将要发送数据的列表
                ListPacketToSend.Add(i);
            reader.Close();

            BuildStartUpdateData();
            DisPacket.NewRecord(
                   new DataInfo(
                       DataInfoState.send,
                        Termination.PowerPoleManage.Find(CMD_ID),
                       "开始远程升级",
                       "开始远程升级"));
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

        public static void RemoteUpdateStartResponse(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string pacMsg = "开始远程升级响应：";
            if (data[0] == 0xff)
            {
                pacMsg += "开始远程升级成功";
                //FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                //BinaryReader reader = new BinaryReader(fs);
                //PacketNum = (int)(reader.BaseStream.Length / 512) + 1;
                //ListPacketToSend.Clear();
                //for (int i = 1; i <= PacketNum; i++)                                 //整理好将要发送数据的列表
                //    ListPacketToSend.Add(i);
                //reader.Close();
                //启动定时器，每隔2s发送一包升级数据直到结束为止
                StartTimer();
            }
            else
            {
                pacMsg += "开始远程升级失败，错误代码： " + ((int)data[1]).ToString();
                UpdateState = false;
            }

            //显示解析结果
            DisPacket.NewRecord(
                new DataInfo(
                    DataInfoState.rec,
                    pole,
                    "开始远程升级响应",
                    pacMsg));
        }

        /// <summary>
        /// 远程升级补包处理
        /// </summary>
        public static void RemoteUpdateBuBao(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string pacMsg = "远程升级补包";
            if (data[0] == 0xff)
            {
                pacMsg += "成功 ";
            }
            else
            {
                pacMsg += "失败,错误代码：" + ((int)data[1]).ToString();
            }
            pacMsg += "文件名" + Encoding.Default.GetString(data, 2, 20);
            int BubaoNum = Tools.intTurn.ByteSToInt(data, 22);

            pacMsg += "补包包数为：" + BubaoNum.ToString() + "   ";

            if (BubaoNum == 0)             //结束补包
            {
                pacMsg += "补包结束";
                UpdateState = false;
                //显示发送的数据
                DisPacket.NewRecord(
                    new DataInfo(
                        DataInfoState.send,
                         Termination.PowerPoleManage.Find(CMD_ID),
                        "远程升级补包数据上传",
                        pacMsg));
                return;
            }
            if (ListPacketToSend.Count == 0)
            {
                for (int i = 0; i < BubaoNum; i++)
                {
                    int temp = Tools.intTurn.ByteSToInt(data, 26 + i * 4);
                    ListPacketToSend.Add(temp);
                    pacMsg += temp.ToString() + " ";
                }
                BuBaoState = true;
                //显示发送的数据
                DisPacket.NewRecord(
                    new DataInfo(
                        DataInfoState.send,
                         pole,
                        "远程升级补包数据上传",
                        pacMsg));

                //软件数据报补包数据上传
                StartTimer();
            }
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
                pacMsg = "发送远程升级结束包";
                SendRemoteUpdatePacket(BuildRemotUpdateEnd(), pacMsg);
                timerRemotUpdate.Stop();
                return;
            }
            //timerRemotUpdate.Stop();
            int pacNum = 0;
            int pacNO = ListPacketToSend[0];
            ListPacketToSend.RemoveAt(0);   //数据发送完成，移除发送列表
            var UpdataData = BuildRemotUpdateData(ReadFile(pacNO, ref pacNum), pacNum, pacNO);

            if (BuBaoState == false)
                pacMsg = "发送远程升级文件" + FileName + ",总共" + PacketNum.ToString() + "包，第" + pacNO.ToString() + "包";
            else
                pacMsg = "发送远程升级补包数据" + FileName + ",总共" + PacketNum.ToString() + "包，第" + pacNO.ToString() + "包";
            //发送远程升级数据
            SendRemoteUpdatePacket(UpdataData, pacMsg);

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
                byte[] tempData = reader.ReadBytes(length);
                for (int i = 0; i < 512; i++)
                {
                    if (i < tempData.Length) ReadData[i] = tempData[i];
                    else { ReadData[i] = 0xff; }
                }
            }
            else
                ReadData = reader.ReadBytes(512);
            reader.Close();
            return ReadData;
        }
        /// <summary>
        /// 生成开始升级报文
        /// </summary>
        /// <returns></returns>
        private static byte[] BuildStartUpdateData()
        {
            byte[] packet = new byte[56];

            //厂商名称
            Buffer.BlockCopy(Encoding.Default.GetBytes(Factory_Name), 0,
                packet, 0, Factory_Name.Length);
            //设备型号
            Buffer.BlockCopy(Encoding.Default.GetBytes(Model), 0,
                packet, 10, Model.Length);
            //硬件版本
            Buffer.BlockCopy(Encoding.Default.GetBytes(HardVersion), 0,
                packet, 20, HardVersion.Length);
            //软件版本
            Buffer.BlockCopy(Encoding.Default.GetBytes(SoftVersion), 0,
                packet, 24, SoftVersion.Length);
            //软件修改日期
            packet[28] = (byte)(Time.Year / 256);
            packet[29] = (byte)(Time.Year % 256);
            packet[30] = (byte)(Time.Month);
            packet[31] = (byte)(Time.Day);
            //文件名
            Buffer.BlockCopy(Encoding.Default.GetBytes(FileName), 0,
                packet, 32, FileName.Length);
            //总包数
            byte[] num = Tools.intTurn.intToByte4(PacketNum);
            Buffer.BlockCopy(num, 0, packet, 52, 4);
            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
               CMD_ID,
               56,
               PacketAnaLysis.TypeFrame.Control,
               PacketAnaLysis.PacketType_Control.Start_Update,
               FrameNO.GetFrameNO(),
               packet);


            string errorMsg;
            PackeDeal.SendData(CMD_ID, Packet, out errorMsg);
            return Packet;
        }


        /// <summary>
        /// 远程升级数据包报文生成
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] BuildRemotUpdateData(byte[] data, int pacNum, int pacNO)
        {

            byte[] tempByte = new byte[] { };
            byte[] packet = new byte[data.Length + 30];

            tempByte = Encoding.ASCII.GetBytes(FileName);           //文件名
            Buffer.BlockCopy(tempByte, 0, packet, 0, tempByte.Length);
            byte[] num = Tools.intTurn.intToByte4(pacNum);    //包数
            Buffer.BlockCopy(num, 0, packet, 20, 4);
            num = Tools.intTurn.intToByte4(pacNO);            //包号
            Buffer.BlockCopy(num, 0, packet, 24, 4);
            Buffer.BlockCopy(data, 0, packet, 28, data.Length);             //包内容
            byte[] crc = CRC16.Crc(data, data.Length);
            Buffer.BlockCopy(crc, 0, packet, data.Length + 28, 2);
            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                data.Length + 30,
                PacketAnaLysis.TypeFrame.Control,
                PacketAnaLysis.PacketType_Control.UpdateData,
                FrameNO.GetFrameNO(),
                packet);
            return Packet;
        }


        /// <summary>
        /// 远程升级结束包报文生成
        /// </summary>
        /// <returns></returns>
        private static byte[] BuildRemotUpdateEnd()
        {
            byte[] packet = new byte[60];
            Buffer.BlockCopy(Encoding.Default.GetBytes(Factory_Name), 0,
                packet, 0, Factory_Name.Length);
            //设备型号
            Buffer.BlockCopy(Encoding.Default.GetBytes(Model), 0,
                packet, 10, Model.Length);
            //硬件版本
            Buffer.BlockCopy(Encoding.Default.GetBytes(HardVersion), 0,
                packet, 20, HardVersion.Length);
            //软件版本
            Buffer.BlockCopy(Encoding.Default.GetBytes(SoftVersion), 0,
                packet, 24, SoftVersion.Length);
            //软件修改日期
            packet[28] = (byte)(Time.Year / 256);
            packet[29] = (byte)(Time.Year % 256);
            packet[30] = (byte)(Time.Month);
            packet[31] = (byte)(Time.Day);
            //文件名
            Buffer.BlockCopy(Encoding.Default.GetBytes(FileName), 0,
                packet, 32, FileName.Length);
            //总包数
            byte[] num = Tools.intTurn.intToByte4(PacketNum);
            Buffer.BlockCopy(num, 0, packet, 52, 4);

            Buffer.BlockCopy(Tools.TimeUtil.GetBytesTime(), 0, packet, 56, 4);

            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                CMD_ID,
                60,
                PacketAnaLysis.TypeFrame.Control,
                PacketAnaLysis.PacketType_Control.UpdateEnd,
                 FrameNO.GetFrameNO(),
                packet);
            return Packet;
        }

        /// <summary>
        /// 发送远程升级数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pacMsg"></param>
        private static void SendRemoteUpdatePacket(byte[] data, string pacMsg)
        {

            string errorMsg;
            if (PackeDeal.SendData(CMD_ID, data, out errorMsg))
            {
                //显示发送的数据
                DisPacket.NewRecord(
                    new DataInfo(
                        DataInfoState.send,
                         Termination.PowerPoleManage.Find(CMD_ID),
                        "远程升级数据报",
                        pacMsg));
            }

        }
    }
}
