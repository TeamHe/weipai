using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GridBackGround.CommandDeal.nw;
using ResModel.EQU;
using static GridBackGround.CommandDeal.nw.Camera_Action;

namespace GridBackGround.Forms.Dialogs_nw
{
    public partial class Dialog_nw_camera_control : Form
    {
        /// <summary>
        /// 装置ID
        /// </summary>
        public string CMD_ID { get; set; }
        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_No { get; set; }

        public nw_cmd_88_remote_camera_control cmd { get; set; }

        public Dialog_nw_camera_control()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_Image_Adjust_Load(object sender, EventArgs e)
        {
            this.button_Open.Tag            = Camera_Action.Actrion.Power_ON;         //打开
            this.buttonClose.Tag            = Camera_Action.Actrion.Power_OFF; ;      //关闭
            this.button_Up.Tag              = Camera_Action.Actrion.Move_UP;          //上
            this.button_Down.Tag            = Camera_Action.Actrion.Move_Down; ;      //下
            this.button_Left.Tag            = Camera_Action.Actrion.Move_Left; ;      //左 
            this.button_Right.Tag           = Camera_Action.Actrion.Move_Right;       //右
            this.button_SaveAsPreset.Tag    = Camera_Action.Actrion.Preset_Set;       //设为预置位
            this.button_TurntoPreset.Tag    = Camera_Action.Actrion.Preset_GoTo;      //转到预置位
            this.button_Near.Tag            = Camera_Action.Actrion.Focal_Near;       //近
            this.button_Far.Tag             = Camera_Action.Actrion.Focal_Far;        //远
            this.button_Aperture_Add.Tag    = Camera_Action.Actrion.Aperture_Add;     //光圈放大
            this.button_Aperture_Reduce.Tag = Camera_Action.Actrion.Aperture_Reduce;  //光圈缩小
            this.button_Focal_Add.Tag       = Camera_Action.Actrion.Focal_Add;        //聚焦增加
            this.button_Focal_Reduce.Tag    = Camera_Action.Actrion.Focal_Reduce;     //聚焦减少

        }


        /// <summary>
        /// 远程调节按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_RemotOption_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int para = 0;
            if (button == button_SaveAsPreset)
                para = int.Parse(this.textBox_preset_save.Text);
            if (button == button_TurntoPreset)
                para = int.Parse(this.textBox_preset_to.Text);

            cmd.Password = this.textBox_password.Text;

            cmd.Action = new Camera_Action()
            {
                actrion = (Camera_Action.Actrion)button.Tag,
                Para = para,
                Channel_no = (int)this.numericUpDown1.Value
            };
            this.cmd.Execute();
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
