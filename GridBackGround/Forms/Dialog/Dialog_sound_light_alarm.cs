using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialog
{
    public partial class Dialog_sound_light_alarm : Form
    {
        /// <summary>
        /// 播放状态描述
        /// </summary>
        public CommandDeal.Command_sound_light_alarm.Play Play { get; set; }
        /// <summary>
        /// 播放时长
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 文件编号
        /// </summary>
        public int FileNO { get; set; }

        public Dialog_sound_light_alarm()
        {
            InitializeComponent();
            this.button1.Tag = CommandDeal.Command_sound_light_alarm.Play.Start;
            this.button2.Tag = CommandDeal.Command_sound_light_alarm.Play.Stop;
            this.button3.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Play = (CommandDeal.Command_sound_light_alarm.Play)((Button)sender).Tag;
            this.FileNO = (int)this.numericUpDown1.Value;
            this.Interval = (int)this.numericUpDown2.Value;
            this.DialogResult = DialogResult.OK;
        }
    }
}
