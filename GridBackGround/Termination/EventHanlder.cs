using System;
using ResModel.PowerPole;

namespace GridBackGround.Termination
{
    public class PowerPoleEventArgs : EventArgs
    {
        public Error_Code Code { get; set; }

        public string Message { get; set; }
    }
}
