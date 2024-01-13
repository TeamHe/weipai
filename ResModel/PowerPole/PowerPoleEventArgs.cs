using System;

namespace ResModel.PowerPole
{
    public class PowerPoleEventArgs : EventArgs
    {
        public Error_Code Code { get; set; }

        public string Message { get; set; }
    }
}
