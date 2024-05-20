using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ResModel.gw
{

    public class gw_ctrl_model
    {
        public enum EType
        {
            [Description("无符号整数")]
            U32 = 0,

            [Description("有符号整数")]
            S32 = 1,

            [Description("单精度浮点数")]
            Single = 2,
        }

        public string Name {  get; set; }

        public string Key {  get; set; }

        public Single Value { get; set; }

        public EType Type { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.IsNullOrEmpty(this.Name) ? this.Key : this.Name);
            switch (this.Type)
            {
                case EType.Single:
                    sb.AppendFormat(":{0}", (float)this.Value);
                    break;
                case EType.S32:
                    sb.AppendFormat(":{0}", (Int32)this.Value);
                    break;
                case EType.U32:
                    sb.AppendFormat(":{0}", (UInt32)this.Value);
                    break;
            }
            return sb.ToString();
        }
    }

    public class gw_ctrl_models: gw_ctrl
    {

        public List<gw_ctrl_model> Models { get; set; } 

        public gw_ctrl_models() 
        {
            this.Models = new List<gw_ctrl_model>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var val in this.Models)
            {
                sb.Append(val);
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }
}
