using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResModel.gw
{
    public class gw_coding
    {
        public static int SetU16(byte[] data,int offset,int value)
        {
            data[offset++] = (byte)(value & 0xFF);
            data[offset++] = (byte)(value >> 8 & 0xFF);
            return 2;
        }

        public static int GetU16(byte[] data, int offset, out int value)
        {
            value = BitConverter.ToUInt16(data, offset);
            return 2;
        }

        public static int GetSingle(byte[] data, int offset, out float value)
        {
            value = BitConverter.ToSingle(data, offset);
            return 4;
        }

        public static int SetSingle(byte[] data, int offset, float value)
        {
            byte[] tmp = BitConverter.GetBytes(value);
            Buffer.BlockCopy(tmp, 0, data, offset, tmp.Length);
            return 4;
        }

    }
}
