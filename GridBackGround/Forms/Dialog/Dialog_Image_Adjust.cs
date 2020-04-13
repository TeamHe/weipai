using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResModel.EQU;

namespace GridBackGround.Forms
{
    public partial class Dialog_Image_Adjust : Form
    {
        public Dialog_Image_Adjust()
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
            Channel_No = int.Parse(this.textBox1.Text);
            this.button9.DialogResult = System.Windows.Forms.DialogResult.OK;
            
            this.button_Open.Tag = Pic_Remote_OPtion.Open;      //打开
            this.buttonClose.Tag = Pic_Remote_OPtion.Close;     //关闭
            this.button_Up.Tag = Pic_Remote_OPtion.Up;          //上
            this.button_Down.Tag = Pic_Remote_OPtion.Down;      //下
            this.button_Left.Tag = Pic_Remote_OPtion.Left;      //左
            this.button_Right.Tag = Pic_Remote_OPtion.Right;    //右
            this.button_SaveAsPreset.Tag = Pic_Remote_OPtion.SaveAsPreSet;  //设为预置位
            this.button_TurntoPreset.Tag = Pic_Remote_OPtion.RuntoPreset;   //转到预置位
            this.button_Near.Tag = Pic_Remote_OPtion.Near;                  //近
            this.button_Far.Tag = Pic_Remote_OPtion.Far;                    //远
            this.button1.Tag = Pic_Remote_OPtion.InitPosition;              //设置初始位置
        }

        /// <summary>
        /// 装置ID
        /// </summary>
        public string CMD_ID { get; set; }
        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_No { get; set; }


        /// <summary>
        /// 远程调节按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_RemotOption_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            CommandDeal.Image_Photo_Adjust.Adjust(CMD_ID, Channel_No, 0, (Pic_Remote_OPtion)button.Tag);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
        private void button7_Click(object sender, EventArgs e)
        {
            int PreSeting = int.Parse(this.textBox3.Text);
            Button button = (Button)sender;
            CommandDeal.Image_Photo_Adjust.Adjust(CMD_ID, Channel_No, PreSeting, (Pic_Remote_OPtion)button.Tag);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
