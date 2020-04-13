using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB_Operation;
using ResModel;

namespace Wintech.BLL
{
    public class Line
    {
        public static int Create(string lineName)
        { 
            return 0;
        }

        public static void Update(int lineID,string linename) 
        { 
        
        }

        public static ResModel.EQU.Line Get(int lineID)
        {
            return null;
        }

        //public static ResModel.EQU.Line Get(string name)
        //{ 
        
        //}

        public static List<ResModel.EQU.Line> List()
        {
            return null;  
        }
    }
}
