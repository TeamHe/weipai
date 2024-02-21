using System;
using System.Collections.Generic;
using System.Timers;
using DB_Operation.RealData;
using ResModel;
using ResModel.nw;
using ResModel.PowerPole;

namespace cma.service
{
    public class PackageMessageEventArgs:EventArgs
    {
        public List<PackageMessage> Msgs { get; set; }

        public PackageMessageEventArgs(List<PackageMessage> msgs)
        {
            this.Msgs = msgs;
        }
    }

    public class PackageRecordsEventArgs : EventArgs
    {
        public List<PackageRecord> Infos { get; set; }

        public PackageRecordsEventArgs(List<PackageRecord> infos)
        {
            this.Infos = infos;
        }
    }

    public class DisPacket
    {
        public static event EventHandler<PackageRecordsEventArgs> OnNewPackageInfo;

        public static event EventHandler<PackageMessageEventArgs> OnNewPakageMessage;

        private static object obj_message=new object();

        private static object obj_record = new object();

        private DisPacket packet { get; set; }

        private static Timer timer { get; set; }

        private static List<PackageRecord> infos { get; set; }
        private static List<PackageMessage> msgs { get; set; }

        private static void TimerStart()
        {
            if (timer != null)
                return;
            timer = new Timer(500);
            timer.Elapsed += Timer_Elapsed;
            infos = new List<PackageRecord>();
            msgs = new List<PackageMessage>();
            timer.Start();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (infos.Count > 0)
            {
                List<PackageRecord> info1 = null;
                lock (obj_record)
                {
                    info1 = infos;
                    infos = new List<PackageRecord>();
                }
                if (OnNewPackageInfo != null)
                    OnNewPackageInfo(null,new PackageRecordsEventArgs(info1));
            }
            if(msgs.Count >0)
            {
                List<PackageMessage> msg1 = null;
                lock (obj_message)
                {
                    msg1 = msgs;
                    msgs = new List<PackageMessage>();
                }
                if (OnNewPakageMessage != null)
                    OnNewPakageMessage(null,new PackageMessageEventArgs(msg1));
            }
        }

        /// <summary>
        /// 新解析数据显示
        /// </summary>
        /// <param name="packet"></param>
        public static void NewRecord(PackageRecord packet)
        {
            TimerStart();
            lock(obj_record)
                infos.Add(packet);

            if (packet.Pole == null)
                return;
            try
            {
                db_package_record db = new db_package_record(packet.Pole);
                db.DataSave(packet);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("PackageRecord save failed. " + ex.Message.ToString());
            }
        }

        public static void NewPackageMessage(PackageMessage msg)
        {
            if (msg == null)
                return;
            TimerStart();
            lock(obj_message)
                msgs.Add(msg);

            if (msg.pole == null)
                return;
            try
            {
                db_package_message db_msg = new db_package_message(msg.pole);
                db_msg.DataSave(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine("PackageMessage save failed. " + e.ToString());
            }
        }

        /// <summary>
        /// 新报文显示
        /// </summary>
        /// <param name="data"></param>
        public static void NewPacket(string data)
        {
            NewPackageMessage(new PackageMessage()
            {
                Description = data
            });
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
            NewPackageMessage(new PackageMessage()
            {
                pole = pole,
                rstype = rstype,
                srctype = srctype,
                src_id = src_name,
                code = flag,
                data = data,
            });
        }

        public static void Init()
        {
            nw_cmd_base.OnNewDataInfo += Nw_cmd_base_NewDataInfo;
        }

        private static void Nw_cmd_base_NewDataInfo(object sender, PackageRecord e)
        {
            NewRecord(e);
        }
    }

    
}
