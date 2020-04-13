using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
  
    public static class TimeUtil
    {
        public static DateTime BytesToDate(byte[] data, int start)
        {

            double timeL = 0;
            //if (data[start] == 0xff)                    //验证装置时间不为全0xff
            //    if (data[start + 1] == 0xff)
            //        if (data[start + 1] == 0xff)
            //            if (data[start + 1] == 0xff)
            //                throw new Exception("装置时间错误，为0xffffffff");
            for (int i = start + 3; i >= start; i--)
            {
                timeL *= 256;
                timeL += ((double)data[i]);
            };
            DateTime time = new DateTime(1970, 1, 1);
            time = time.AddSeconds(timeL);
            return time;
        }
        public static DateTime BytesToDate(byte[] data)
        {
            return BytesToDate(data, 0);
        }

        public static byte[] GetBytesTime()
        {
            return GetBytesTime(DateTime.Now);
        }

        public static byte[] GetBytesTime(DateTime time)
        {
            DateTime t1 = new DateTime(1970, 1, 1);
            TimeSpan timeSpan = time.Subtract(t1);
            ulong timeL = (ulong)timeSpan.TotalSeconds;

            return BitConverter.GetBytes(timeL);
        }
    }
}
