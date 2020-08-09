using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DB_Operation.RealData;
using System.Data;
using ResModel.EQU;
using System.IO;

namespace GridBackGround.Work
{
    public delegate void PictureCleanCallback(DateTime time, TimeSpan ts, int picture,double size);


    public class PictureClean
    {
        static Timer timer;
        /// <summary>
        /// 图片清理结果
        /// </summary>
        public event PictureCleanCallback CleanCallback;

        public static void Start()
        {
            timer = new Timer(Timer_Elapsed);
            int hour = Config.SettingsForm.Default.PictureCleanTime.Hour;
            int minut = Config.SettingsForm.Default.PictureCleanTime.Minute;
            DateTime now = DateTime.Now;
            DateTime oneClock = DateTime.Today.AddHours(hour);
            oneClock = oneClock.AddMinutes(minut);
            if (DateTime.Now > oneClock)
                oneClock = oneClock.AddDays(1);
            int msUnitl = (int)(oneClock - now).TotalMilliseconds;
            timer.Change(msUnitl,Timeout.Infinite);

            if (Config.SettingsForm.Default.PictureCleanAtStart)
            {
                DateTime end = DateTime.Today;
                int reserveTime = Config.SettingsForm.Default.PictuerCleanReserveTime;
                if (reserveTime <= 0) reserveTime = 1;
                end = end.AddDays(-reserveTime);
                Remove(end);
            }
        }
        /// <summary>
        /// 自动清理定时器执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Elapsed(object state)
        {
            if (!Config.SettingsForm.Default.PictureCleanAuto)
                return;
            DateTime time = DateTime.Today;
            DateTime lastTime = Config.SettingsForm.Default.PictuerCleanLastTime;
            if ((time - lastTime).Days >= Config.SettingsForm.Default.PictureCleanPeriod - 1)
            {
                DateTime end = DateTime.Today;
                int reserveTime = Config.SettingsForm.Default.PictuerCleanReserveTime;
                if (reserveTime <= 0) reserveTime = 1;
                end = end.AddDays(-reserveTime);
                Remove(end);

            }

            int hour = Config.SettingsForm.Default.PictureCleanTime.Hour;
            int minut = Config.SettingsForm.Default.PictureCleanTime.Minute;
            DateTime oneClock = DateTime.Today.AddHours(hour);
            oneClock = oneClock.AddMinutes(minut);
            oneClock = oneClock.AddDays(1);
            int msUnitl = (int)(oneClock - DateTime.Now).TotalMilliseconds;
            timer.Change(msUnitl, Timeout.Infinite);
        }

        private static void clean_start()
        {

        }


        public static void Remove(DateTime end)
        {
            Remove(null,DateTime.MinValue, end);
        }
        public static void Remove(DateTime start,DateTime end)
        {
            Remove(null, DateTime.MinValue, end);
        }

        static void RemoveTask(Equ equ,DateTime start,DateTime end)
        {
           
            List<int> ids = new List<int>();
            DataTable dt = new DB_Real_Picture().GetData(equ, start, end);
            foreach (DataRow row in dt.Rows)
            {
                int pid = (int)row[0];
                string path = (string)row[4];
                ids.Add(pid);
                RemovePicture(path);
            }
            new DB_Real_Picture().RemovePictures(equ, start, end);
            Config.SettingsForm.Default.PictuerCleanLastTime = DateTime.Now;
        }

        public static void Remove(Equ equ,DateTime start,DateTime end)
        {
            System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
            {
                DateTime time = DateTime.Now;
                try
                {
                    RemoveTask(equ, start, end);

                }
                catch(Exception ex)
                {
                    Console.WriteLine("Remove Pictures fail." + ex.Message);
                }
                TimeSpan ts = DateTime.Now.Subtract(time);
                Console.WriteLine("Remove picture takes time." + ts.TotalMilliseconds + "ms");

            });
            task.Start();
        }

        

        public static void RemovePicture(string filepath)
        {
            if (filepath == null) return;
            string dir = Path.GetDirectoryName(filepath);
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
                string name = Path.GetFileNameWithoutExtension(filepath);
                name = name.Substring(0, name.Length - 2);
                name += Path.GetExtension(filepath);
                name = Path.Combine(dir, name);
                File.Delete(name);
            }
            remove_dir(dir);

        }

        static void remove_dir(string dir)
        {
            if (!Directory.Exists(dir)) return;
            if(Directory.GetDirectories(dir).Length >0
                || Directory.GetFiles(dir).Length >0)
                return;
            Directory.Delete(dir, false);
            string parent = Path.GetDirectoryName(dir) ;
            remove_dir(parent);
        }
    }
}
