using System;
using System.ComponentModel;

namespace ResModel.gw
{


    public class gw_ctrl
    {
        public enum ESetFlag
        {
            [Description("查询")]
            Query = 0x00,

            [Description("设置")]
            Set = 0x01,
        }

        public enum ESetStatus
        {
            [Description("成功")]
            Success = 0xff,

            [Description("失败")]
            Failed = 0x00,
        }

        public ESetFlag RSFalg { get; set; }

        public ESetStatus Result { get; set; }


        public int Flag { get; set; }

        public gw_para_type ParaType { get; set; }

        public int SetFlag(int flag, int offset)
        {
            return flag | (0x01 << offset);
        }

        public void SetFlag(Enum flag)
        {
            this.SetFlag(Convert.ToInt32(flag));
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

        public void SetFlag(Enum e, bool flag)
        {
            SetFlag(Convert.ToInt32(e), flag);
        }


        public bool GetFlag(int flag, int offset)
        {
            if((flag & (0x01 << offset)) >0)
                return true;
            else
                return false;
        }

        public bool GetFlag(Enum flag)
        {
            return GetFlag(Convert.ToInt32(flag));
        }

        public bool GetFlag(int offset)
        {
            return this.GetFlag(this.Flag, offset);
        }

        public int ClearFlag(int flag, int offset)
        {
            return flag & ~(0x01 << offset);
        }

        public int ClearFlags(int flag, int mask)
        {
            return flag & ~(mask);
        }

        public void ClearFlags(int mask)
        {
            this.Flag = ClearFlags(this.Flag, mask);
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
