using System.Collections;
using System.Collections.Generic;
using ResModel.EQU;

namespace cma.service.PowerPole
{
    public class Equs
    {
        public List<Line> Lines { get; set; }

        //public List<Tower> Towers { get; set; }

        public Hashtable Equlist{  get; set; }

        public Equs() 
        { 
            this.Lines = new List<Line>();
            //this.Towers = new List<Tower>();

        }

        

        public void load()
        {
            this.Lines = new DB_Operation.EQUManage.DB_Line().List();
            

        }


    }
}
