using System;
using System.Net;
using System.Text;
using Tools;

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

        public static int GetU32(byte[] data, int offset, out UInt32 value)
        {
            value = BitConverter.ToUInt32(data, offset);
            return 4;
        }

        public static int SetU32(byte[] data, int offset, UInt32 value)
        {
            byte[] tmp = BitConverter.GetBytes(value);
            Buffer.BlockCopy(tmp, 0, data, offset, tmp.Length);
            return 4;
        }

        public static int GetS32(byte[] data, int offset, out Int32 value)
        {
            value = BitConverter.ToInt32(data, offset);
            return 4;
        }

        public static int SetS32(byte[] data, int offset, Int32 value)
        {
            byte[] tmp = BitConverter.GetBytes(value);
            Buffer.BlockCopy(tmp, 0, data, offset, tmp.Length);
            return 4;
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

        public static int GetIPAddress(byte[] data, int offset, out IPAddress address)
        {
            if (data.Length - offset < 4)
                throw new Exception("GetIPAddress:Buffer Length too small");
            byte[] bytes = new byte[4];
            Buffer.BlockCopy(data,offset, bytes, 0, 4);
            address = new IPAddress(bytes);
            return 4;
        }

        public static int SetIPAddress(byte[] data, int offset, IPAddress address)
        {
            if (data.Length - offset < 4)
                throw new Exception("SetIPAddress:Buffer Length too small");
            if(address != null)
            {
                byte[] bytes = address.GetAddressBytes();
                Buffer.BlockCopy(bytes, 0, data, offset, 4);
            }
            return 4;
        }

        public static int GetString(byte[] data, int offset, int len,out string str)
        {
            if (data.Length < offset + len)
                throw new Exception("GetString:数据长度错误");
            str = Encoding.Default.GetString(data, offset, len).TrimEnd('\0');
            return len;
        }

        public static int SetString(byte[] data, int offset, int len, string str)
        {
            if (data.Length < offset + len)
                throw new Exception("SetString:数据长度错误");

            if (str != null)
            {
                byte[] buff = Encoding.Default.GetBytes(str);
                int tmp_len = len;
                if (buff.Length < len)
                    tmp_len = buff.Length;
                Buffer.BlockCopy(buff, 0, data, offset, tmp_len);
            }
            return len;
        }

        public static int SetTime(byte[] data, int offset, DateTime time) 
        {
            if (data.Length - offset < 4)
                throw new Exception("SetTime:数据长度错误");
            byte[] bytes = TimeUtil.GetBytesTime(time);
            Buffer.BlockCopy(bytes,0, data, offset, 4);
            return 4;
        }

        public static int GetTime(byte[] data, int offset, out DateTime time)
        {
            if (data.Length - offset < 4)
                throw new Exception("GetTime:数据长度错误");
            time = TimeUtil.BytesToDate(data, offset);
            return 4;
        }
    }
}
