using System;

namespace ResModel.nw
{
    public abstract class nw_data_base
    {
        public DateTime DataTime { get; set; }

        public abstract int PackLength { get; }

        public nw_data_base() { }

        public abstract int Decode(byte[] data, int offset);

        public abstract int Encode(byte[] data, int offset);
    }
}
