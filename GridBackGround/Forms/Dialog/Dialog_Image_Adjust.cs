using System;
using System.Windows.Forms;
using cma.service.gw_cmd;
using ResModel;
using ResModel.EQU;
using ResModel.gw;

namespace GridBackGround.Forms
{
    internal struct AdjustTag
    {
        public gw_img_adjust.EAction Action { get; set; }
        public TextBox TextBox { get; set; }
    }
    public partial class Dialog_Image_Adjust : Form
    {
        public Dialog_Image_Adjust()
        {
            InitializeComponent();
            CenterToParent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_Image_Adjust_Load(object sender, EventArgs e)
        {
            Channel_No = int.Parse(this.textBox1.Text);

            this.button_Open.Tag = gw_img_adjust.EAction.PowerOn;      //打开
            this.buttonClose.Tag = gw_img_adjust.EAction.PowerOff;     //关闭
            this.button_Up.Tag = gw_img_adjust.EAction.UP;          //上
            this.button_Down.Tag = gw_img_adjust.EAction.DOWN;      //下
            this.button_Left.Tag = gw_img_adjust.EAction.LEFT;      //左
            this.button_Right.Tag = gw_img_adjust.EAction.RIGHT;    //右
            this.button_Near.Tag = gw_img_adjust.EAction.NEAR;                  //近
            this.button_Far.Tag = gw_img_adjust.EAction.FAR;                    //远

            this.button_SaveAsPreset.Tag = new AdjustTag()
            {
                Action = gw_img_adjust.EAction.SAVE,  //设为预置位
                TextBox = this.textBox_save,
            };
            this.button_TurntoPreset.Tag = new AdjustTag(){
                Action = gw_img_adjust.EAction.Preset,   //转到预置位
                TextBox = this.textBox_preset,
            };
        }

        public IPowerPole Pole { get; set; }

        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_No { get; set; }

        private void adjust(gw_img_adjust.EAction action, int preset)
        {
            if (this.Pole.OnLine != OnLineStatus.Online)
            {
                MessageBox.Show("设备离线");
                return ;
            }

            gw_cmd_img_adjust cmd = new gw_cmd_img_adjust(this.Pole);
            cmd.Action(new gw_img_adjust()
            {
                ChNO = Channel_No,
                Preset = preset,
                Action = action,
            });
        }

        /// <summary>
        /// 远程调节按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_RemotOption_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            adjust((gw_img_adjust.EAction)button.Tag, 0);
        }

        private void textBox_chno_TextChanged(object sender, EventArgs e)
        {
            int NO = int.Parse(this.textBox1.Text);
            if (NO >= 0 && NO < 3)
                Channel_No = NO;
        }
        /// <summary>
        /// 保存预置位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_preset_Click(object sender, EventArgs e)
        {
            int preset = 0;
            AdjustTag tag = (AdjustTag)((Button)sender).Tag;
            if(!int.TryParse(tag.TextBox.Text,out preset))
            {
                MessageBox.Show("请输入正确的预置位号");
                return;
            }
            adjust(tag.Action, preset);
        }
    }
}
