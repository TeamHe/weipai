using System.Collections.Generic;
using System.Timers;
using DB_Operation.RealData;
using ResModel;
using ResModel.nw;
using ResModel.PowerPole;

namespace cma.service.PowerPole
{
    public delegate void NewRecordS(List<DataInfo> packets);
    public delegate void NewPacketS(List<string> msgs);

    public class DisPacket
    {
        public static event NewRecordS OnNewRecordS;
        public static event NewPacketS OnNewPacketS;

        private DisPacket packet { get; set; }

        private static Timer timer { get; set; }

        private static List<DataInfo> infos { get; set; }
        private static List<string> msgs { get; set; }

        private static void TimerStart()
        {
            if (timer != null)
                return;
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            infos = new List<DataInfo>();
            msgs = new List<string>();
            timer.Start();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (infos.Count > 0)
            {
                List<DataInfo> info1 = infos;
                infos = new List<DataInfo>();
                if (DisPacket.OnNewRecordS != null)
                    OnNewRecordS(info1);
            }
            if(msgs.Count >0)
            {
                List<string> msg1 = msgs;
                msgs = new List<string>();
                if (DisPacket.OnNewPacketS != null)
                    OnNewPacketS(msg1);
            }
        }

        /// <summary>
        /// 新解析数据显示
        /// </summary>
        /// <param name="packet"></param>
        public static void NewRecord(DataInfo packet)
        {
            TimerStart();
            infos.Add(packet);

        }
        /// <summary>
        /// 新报文显示
        /// </summary>
        /// <param name="data"></param>
        public static void NewPacket(string data)
        {
            //TimerStart();
            //msgs.Add(data);
            //if (DisPacket.OnNewPacket != null)
            //    OnNewPacket(data);
            List<string> msg1 = new List<string>();
            msg1.Add(data);
            if (DisPacket.OnNewPacketS != null)
                OnNewPacketS(msg1);
        }

        /// <summary>
        /// 数据包收发静态处理函数
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="rstype"></param>
        /// <param name="srctype"></param>
        /// <param name="src_name"></param>
        /// <param name="flag"></param>
        /// <param name="data"></param>
        public static void NewPackageMessage(IPowerPole pole, RSType rstype, SrcType srctype, 
            string src_name, int flag, byte[] data)
        {
            PackageMessage msg = new PackageMessage()
            {
                pole = pole,
                rstype = rstype,
                srctype = srctype,
                src_id = src_name,
                code = flag,
                data = data,
            };
            db_package_message db_msg= new db_package_message(pole);
            db_msg.DataSave(msg);
            NewPacket(msg.ToString());
        }

        public static void Init()
        {
            nw_cmd_base.OnNewDataInfo += Nw_cmd_base_NewDataInfo;
        }

        private static void Nw_cmd_base_NewDataInfo(object sender, DataInfo e)
        {
            NewRecord(e);
        }
    }

    
}
