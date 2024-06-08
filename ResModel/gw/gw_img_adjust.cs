using System.ComponentModel;
using System.Text;
using Tools;

namespace ResModel.gw
{
    public class gw_img_adjust : gw_ctrl
    {
        public enum EAction
        {
            [Description("打开电源")]
            PowerOn = 1,
            [Description("摄像机调节到指定预置点")]
            Preset,
            [Description("向上调节一个单位")]
            UP,
            [Description("向下调节一个单位")]
            DOWN,
            [Description("向左调节一个单位")]
            LEFT,
            [Description("向右调节一个单位")]
            RIGHT,
            [Description("焦距向远调节一个单位")]
            FAR,
            [Description("焦距向近调节一个单位")]
            NEAR,
            [Description("保存当前位置为预置点")]
            SAVE,
            [Description("关闭摄像机电源")]
            PowerOff,
        }

        /// <summary>
        /// 通道号
        /// </summary>
        public int ChNO {  get; set; }

        /// <summary>
        /// 预置位号
        /// </summary>
        public int Preset {  get; set; }

        /// <summary>
        /// 动作指令
        /// </summary>
        public EAction Action { get; set; }


        public gw_img_adjust() { }

        public override string ToString(bool flag)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("通道号:{0} ",this.ChNO);
            sb.AppendFormat("动作指令:{0} ", this.Action.GetDescription());
            if (Action == EAction.Preset || Action == EAction.SAVE)
                sb.AppendFormat("预置位号:{0} ", this.Preset);
            return sb.ToString();
        }
    }
}
