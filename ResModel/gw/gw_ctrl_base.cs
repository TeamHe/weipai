﻿using System;
using System.ComponentModel;

namespace ResModel.gw
{


    public class gw_ctrl
    {
        public enum RequestSetFlag
        {
            [Description("查询")]
            Query = 0x00,

            [Description("设置")]
            Set = 0x01,
        }

        public enum Status
        {
            [Description("成功")]
            Success = 0xff,

            [Description("失败")]
            Failed = 0x00,
        }

        public RequestSetFlag RSFalg { get; set; }

        public Status Result { get; set; }


        public int Flag { get; set; }

        public int SetFlag(int flag, int offset)
        {
            return flag | (0x01 << offset);
        }

        public void SetFlag(int offset)
        {
            this.Flag = this.SetFlag(this.Flag, offset);
        }

        public void SetFlag(int offset, bool flag)
        {
            if(flag)
                this.Flag = this.SetFlag(this.Flag, offset);
            else
                this.Flag = this.ClearFlag(this.Flag, offset);
        }



        public bool GetFlag(int flag, int offset)
        {
            if((flag & (0x01 << offset)) >0)
                return true;
            else
                return false;
        }

        public bool GetFlag(int offset)
        {
            return this.GetFlag(this.Flag, offset);
        }

        public int ClearFlag(int flag, int offset)
        {
            return flag & ~(0x01 << offset);
        }

        public void ClearFlag(int offset)
        {
            this.Flag = ClearFlag(this.Flag, offset);
        }

        public virtual string ToString(bool flag)
        {
            return base.ToString();
        }

        public override string ToString()
        {
            return this.ToString(true);
        }

}
}