using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GridBackGround
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm()); 
            //DB_Operation.DB.Init();
            //Application.Run(new Forms.Dialog.Dialog_EquManage());
        }
    }
}
