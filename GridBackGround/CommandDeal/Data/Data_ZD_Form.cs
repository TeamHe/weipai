using System;
using System.Collections.Generic;

namespace GridBackGround.CommandDeal
{

    public class WFZD_Forms
    {
        public List<Form> Forms { get; set; }
        public string CMD_ID { get; set; }
        
        public int Unit_Num { get; set; }

        public WFZD_Forms(string cmd_ID)
        {
            CMD_ID = cmd_ID;
            Forms = new List<Form>();           
        }
    }
    public class Form
    {
        public DateTime Time { get; set; }
        public int Unit_No { get; set; }
        public float[] Data { get; set; }

        public Form(DateTime time,int no,int length)
        {
            this.Time = time;
            Unit_No = no;
            Data = new float[length];
        }

    }
}
