using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GridBackGround
{
    static class Program
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {   //检查程序列表中有没有同名进程.
                Process process = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName(process.ProcessName);
                foreach(Process pr in processes)
                {   //如果有同名进程且已经启动界面，则启动另外一个同名进程，然后本进程退出
                    if (pr.MainWindowHandle != IntPtr.Zero) 
                    {
                        SwitchToThisWindow(processes[0].MainWindowHandle, true);
                        return;
                    }
                }
            }
            catch {  }

            try
            {
                //系统定时器
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 1000 * 60;
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
                //处理未捕获的异常 
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                ////处理UI线程异常 
                Application.ThreadException += Application_ThreadException;
                ////处理非UI线程异常  
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("main", ex);
                MessageBox.Show("系统出现异常:" + (ex.Message +" " + (ex.InnerException != null && ex.InnerException.Message != null && ex.Message != ex.InnerException.Message ? ex.InnerException.Message : ""))+",请重启程序。");
            }
        }
        #region 强制GC
        /// <summary>
        /// 刷新内存
        /// </summary>
        public static void FlushMemory()
        {
            GarbageCollect();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        /// <summary>
        /// 主动通知系统进行垃圾回收    
        /// </summary>
        public static void GarbageCollect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        /// <summary>
        /// 把不频繁执行或者已经很久没有执行的代码,没有必要留在物理内存中,只会造成浪费;放在虚拟内存中,等执行这部分代码的时候,再调出来
        /// </summary>
        /// <param name="process"></param>
        /// <param name="minSize"></param>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        #endregion
        /// <summary>
        /// 可读的文件大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static string HumanReadableFilesize(double size)
        {
            String[] units = new String[] { "B", "KB", "MB", "GB", "TB", "PB" };
            double mod = 1024.0;
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];

        }
        /// <summary>
        /// 系统定时器触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            FlushMemory();  //强制GC

            //记录当前内存使用量
            Process process = Process.GetCurrentProcess();
            LogHelper.WriteLog(string.Format("Current Memory: WorkingSet:{0}  PrivateMemorySize:{1} PeakWorkingSet: {2} MaxWorkingSet:{3}",
                                process.WorkingSet64,
                                process.PrivateMemorySize64,
                                process.PeakWorkingSet64,
                                process.MaxWorkingSet));

        }

        /// <summary>
        /// 处理非UI线程异常  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            
            if (ex != null)
            {
                if(ex is OutOfMemoryException)
                {
                    Process process = Process.GetCurrentProcess();
                    LogHelper.WriteLog(string.Format("OutOfMemoryException: WorkingSet:{0}  PrivateMemorySize:{1} PeakWorkingSet: {2} MaxWorkingSet:{3}",
                            process.WorkingSet64,
                            process.PrivateMemorySize64,
                            process.PeakWorkingSet64,
                            process.MaxWorkingSet
                            ), ex);
                }
                else
                    LogHelper.WriteLog("CurrentDomain_UnhandledException", ex);
            }
            MessageBox.Show("系统出现异常：" + (ex.Message + " " + (ex.InnerException != null && ex.InnerException.Message != null && ex.Message != ex.InnerException.Message ? ex.InnerException.Message : "")));
        }
        /// <summary>
        /// /处理UI线程异常 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            if (e.Exception != null)
            {
                if (ex is OutOfMemoryException)
                {
                    Process process = Process.GetCurrentProcess();
                    LogHelper.WriteLog(string.Format("OutOfMemoryException: WorkingSet:{0}  PrivateMemorySize:{1} PeakWorkingSet: {2} MaxWorkingSet:{3}",
                            process.WorkingSet64,
                            process.PrivateMemorySize64,
                            process.PeakWorkingSet64,
                            process.MaxWorkingSet
                            ), ex);
                }
                else

                    LogHelper.WriteLog("Application_ThreadException", e.Exception);
            }
            MessageBox.Show("系统出现异常：" + (ex.Message + " " + (ex.InnerException != null && ex.InnerException.Message != null && ex.Message != ex.InnerException.Message ? ex.InnerException.Message : "")));
        }
    }


}
