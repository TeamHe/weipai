using System.Collections.Generic;
using System.Timers;
using ResModel.nw;
using ResModel.PowerPole;

namespace cma.service.PowerPole
{
    public delegate void NewRecord(DataInfo packet);
    public delegate void NewPacket(string msg);

    public delegate void NewRecordS(List<DataInfo> packets);
    public delegate void NewPacketS(List<string> msgs);

    public class DisPacket
    {
        //public static event NewRecord OnNewRecord;
        //public static event NewPacket OnNewPacket;

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
