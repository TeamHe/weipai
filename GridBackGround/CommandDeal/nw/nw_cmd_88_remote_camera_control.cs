using GridBackGround.Termination;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tools;

namespace GridBackGround.CommandDeal.nw
{
    public class Camera_Action
    {
        public enum Actrion
        {
            [Description("打开摄像机电源")]
            Power_ON = 1,

            /// <summary>
            /// 摄像机调节到指定预置点 
            /// 
            /// Para: Preset 所需预置点
            /// </summary>
            [Description("摄像机调节到指定预置点")]
            Preset_GoTo,

            [Description("向上调节1个单位")]
            Move_UP,

            [Description("向下调节1个单位")]
            Move_Down,

            [Description("向左调节1个单位")]
            Move_Left,



            [Description("向右调节1个单位")]
            Move_Right = 6,

            /// <summary>
            /// 焦距向远方调节1个单位（镜头变倍放大）
            /// </summary>
            [Description("焦距向远方调节1个单位")]
            Focal_Far,

            [Description("焦距向近处调节1个单位")]
            Focal_Near,

            /// <summary>
            /// 摄像机调节到指定预置点 
            /// 
            /// Para: Preset 所需设置预置点
            /// </summary>
            [Description("保存当前位置为某预置点")]
            Preset_Set,

            [Description("关闭摄像机电源")]
            Power_OFF,

            [Description("光圈放大1个单位")]
            Aperture_Add = 11,

            [Description("光圈缩小1个单位")]
            Aperture_Reduce,

            [Description("聚焦增加1个单位")]
            Focal_Add ,

            [Description("聚焦减少1个单位")]
            Focal_Reduce,

            /// <summary>
            /// 开始巡航 
            /// 
            /// Para: NO 巡航号
            /// </summary>
            [Description("开始巡航")]
            Cruise_ON,

            /// <summary>
            /// 停止巡航 
            /// 
            /// Para: NO 巡航号
            /// </summary>
            [Description("停止巡航")]
            Cruise_OFF = 16,

            /// <summary>
            /// 打开辅助开关 
            /// 
            /// Para: NO 辅助开关号
            /// </summary>
            [Description("打开辅助开关")]
            Switch_ON,

            /// <summary>
            /// 关闭辅助开关 
            /// 
            /// Para: NO 辅助开关号
            /// </summary>
            [Description("关闭辅助开关")]
            Switch_OFF,

            [Description("开始自动扫描")]
            AutoScan_ON,

            [Description("停止自动扫描")]
            AutoScan_OFF,

            [Description("开始随机扫描")]
            RandomScan_ON = 21,

            [Description("停止随机扫描")]
            RandomScan_OFF,

            [Description("红外灯全开")]
            Infrared_ON,

            [Description("红外灯半开")]
            Infrared_Half,

            [Description("红外灯关闭")]
            Infrared_OFF,

            /// <summary>
            /// 删除预置位号 
            /// 
            /// Para: Preset 所需预置位号
            /// </summary>
            [Description("删除预置位号")]
            Prest_Del = 26,

            [Description("设置自动扫描左边界")]
            AutoScan_Left,

            [Description("设置自动扫描右边界")]
            AutoScan_Right,
            /// <summary>
            /// 设置自动扫描速度 
            /// 
            /// Para: speed 速度
            /// </summary>
            [Description("设置自动扫描速度")]
            AutoScan_Speed,

            /// <summary>
            /// 设置云台上下左右调节时 
            /// 
            /// Para: step 每单位的步长 每单位步长值为1~100。 
            /// </summary>
            [Description("设置自动扫描速度")]
            Move_Step,

            /// <summary>
            /// 开始巡检 
            /// 
            /// Para: no 巡检组号
            /// </summary>
            [Description("开始巡检")]
            Inspection_ON = 31,

            /// <summary>
            /// 停止巡检 
            /// 
            /// Para: no 巡检组号
            /// </summary>
            [Description("停止巡检")]
            Inspection_OFF,


            [Description("停止摄像机动作")]
            Move_Stop = 48,

            /// <summary>
            /// 摄像机向上运动速度
            /// 
            /// Para: speed 速度 速度范围1~100
            /// </summary>
            [Description("开始摄像机向上运动")]
            Move_Speed_UP,

            /// <summary>
            /// 开始摄像机向下运动
            /// 
            /// Para: speed 速度 速度范围1~100
            /// </summary>
            [Description("开始摄像机向下运动")]
            Move_Speed_Down,


            /// <summary>
            /// 开始摄像机向左运动
            /// 
            /// Para: speed 速度 速度范围1~100
            /// </summary>
            [Description("开始摄像机向左运动")]
            Move_Speed_Left = 51,


            /// <summary>
            /// 开始摄像机向右运动
            /// 
            /// Para: speed 速度 速度范围1~100
            /// </summary>
            [Description("开始摄像机向右运动")]
            Move_Speed_Right,

            /// <summary>
            /// 开始摄像机向左上运动
            /// 
            /// Para: speed 速度 速度范围1~100
            /// </summary>
            [Description("开始摄像机向左上运动")]
            Move_Speed_Left_UP,

            /// <summary>
            /// 开始摄像机向右上运动
            /// 
            /// Para: speed 速度 速度范围1~100
            /// </summary>
            [Description("开始摄像机向右上运动")]
            Move_Speed_Right_UP,

            /// <summary>
            /// 开始摄像机向左运动
            /// 
            /// Para: speed 速度 速度范围1~100
            /// </summary>
            [Description("开始摄像机向左运动")]
            Move_Speed_Left_Down ,

            /// <summary>
            /// 开始摄像机向右下运动
            /// 
            /// Para: speed 速度 速度范围1~100
            /// </summary>
            [Description("开始摄像机向右下运动")]
            Move_Speed_Right_Down = 56,

            [Description("开始摄像机焦距向远方调节")]
            Move_Focal_Far,

            [Description("开始摄像机焦距向远方调节")]
            Move_Focal_Near,



            [Description("开始摄像机光圈放大")]
            Move_Aperture_Add ,

            [Description("开始摄像机光圈缩小")]
            Move_Aperture_Reduce,

            [Description("开始摄像机聚焦增加")]
            Move_Focal_Add = 61,

            [Description("开始摄像机聚焦减少")]
            Move_Focal_Reduce,
        }

        public Actrion actrion { get; set; }

        public int Para { get; set; }

        public int Channel_no { get; set; }

        public Camera_Action() { }

        public Camera_Action(Actrion actrion, int actionPara):this()
        {
            this.actrion = actrion;
            this.Para = actionPara;
        }

        public override string ToString()
        {
            return string.Format("通道:{2} 动作:{0} 参数:{1}", this.actrion.GetDescription(), this.Para,this.Channel_no);
        }
    }

    public class nw_cmd_88_remote_camera_control : nw_cmd_base
    {
        public override int Control { get { return 0x88; } }

        public override string Name { get { return "摄像机远程调节"; } }

        public string Password { get; set; }


        public Camera_Action Action { get; set; }

        public nw_cmd_88_remote_camera_control() { }

        public nw_cmd_88_remote_camera_control(IPowerPole pole):base(pole) { }



        public override int Decode(out string msg)
        {
            if (Data == null || (Data.Length != 2 && Data.Length != 7))
                throw new Exception(string.Format("数据域长度错误,应为7 或2字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            if (this.Data.Length == 2)
            {
                if (Data[0] == 0xff && Data[1] == 0xff)
                    msg = " 失败。原密码错误";
                else
                    msg = string.Format("失败。错误码:{0:X2}{1:X2}H", Data[0], Data[1]);
                return 0;
            }

            int offset = 0;
            offset += this.GetPassword(this.Data, offset, out string password); 
            this.Password = password;

            if(this.Action == null)
                this.Action = new Camera_Action();
            this.Action.Channel_no = this.Data[offset++];
            this.Action.actrion = (Camera_Action.Actrion)this.Data[offset++];
            this.Action.Para = this.Data[offset++];

            msg = "成功。" + this.Action.ToString();
            return 0;

        }

        public override byte[] Encode(out string msg)
        {
            if (this.Action == null || Action.actrion == 0)
                throw new ArgumentNullException("Action");

            byte[] data = new byte[7];
            int offset = 0;
            offset += this.SetPassword(data, offset, this.Password);
            data[offset++] = (byte)this.Action.Channel_no;
            data[offset++] = (byte)this.Action.actrion;
            data[offset++] = (byte)this.Action.Para;
            msg = this.Action.ToString();
            return data;
        }
    }
}
