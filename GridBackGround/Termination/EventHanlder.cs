using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.Termination
{
    public class PowerPoleEventArgs : EventArgs
    {
        public Error_Code Code { get; set; }

        public string Message { get; set; }
    }
}
