using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
//using Microsoft.Office.Tools.Excel;
namespace GridBackGround.Config
{
    public class Config
    {
        public static bool ConfigPort(int port)
        {
            SettingsForm.Default.CMD_Port = port;
            return true;
        }

        public static int GetPort()
        {
            return SettingsForm.Default.CMD_Port;
        }
    }
}
