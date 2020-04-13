using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection; 
 
namespace ResModel.EQU
{
    public enum Pic_Remote_OPtion
    {
        [Description("打开摄像机电源 ")]
        Open = 1,

        [Description("摄像机调节到指定预置点")]
        RuntoPreset,

        [Description("向上调节1个单位")]
        Up,

        [Description("向下调节1个单位")]
        Down,
        //5
        [Description("向左调节1个单位")]
        Left,

        [Description("向右调节1个单位")]
        Right,
        //
        [Description("焦距向远方调节1个单位")]
        Far,

        [Description("焦距向近处调节1个单位")]
        Near,

        [Description("保存当前位置为某预置点")]
        SaveAsPreSet,
        //10
        [Description("关闭摄像机电源")]
        Close,

        [Description("设置初始位置")]
        InitPosition,

    }
}
