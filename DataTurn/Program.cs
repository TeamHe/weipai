using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Tools
{
    /// <summary>
    /// Byte数组和
    /// </summary>
    public static class StringTurn
    {
        /// <summary>
        /// 将字符数组转化为Hex格式的String
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHexString(byte[] bytes)
        {
            StringBuilder str = new StringBuilder();
            if (bytes != null && bytes.Length > 0)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    str.AppendFormat("{0:X2} ",bytes[i]);
                }
            }
            return str.ToString();


        }
    }

    

    public static class intTurn
    {
        public static byte[] intToByte4(int Num)
        {
            byte[] abyte = new byte[4]; //int为32位除4位，数组为8
            for (int i = 0; i < 4; i++)
            {
                
                Num = Num >> (8 * i);
                abyte[i] = (byte)(Num);
            }

            return abyte;
        }
        public static byte[] intToByte2(int Num)
        {
            byte[] abyte = new byte[2]; //int为32位除4位，数组为8
            for (int i = 0; i < 2; i++)
            {

                Num = Num >> (8 * i);
                abyte[i] = (byte)(Num);
            }

            return abyte;
        }

        /// <summary>
        ///将字符数组转化为int
        /// </summary>
        /// <param name="data"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static int ByteSToInt(byte[] data,int start)
        {
            
            return BitConverter.ToInt32(data,start);
        }

        public static int ByteToUint16(byte[] data,int start)
        {
            return BitConverter.ToUInt16(data, start);
        }
    }

    public static class FloatTurn
    {
        public static byte[] FloatToByte4(float Num)
        {
            byte[] abyte = new byte[4]; //int为32位除4位，数组为8
            return abyte;
        }
        /// <summary>
        ///将字符数组转化为int
        /// </summary>
        /// <param name="data"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static float BytesToFloat(byte[] data, int start)
        {

            return BitConverter.ToSingle(data, start);
        }
    }
}
