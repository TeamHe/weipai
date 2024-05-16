using ResModel.gw;
using System;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialog
{
    public partial class Dialog_revival : Form
    {
        private gw_ctrl_revival revival;

        public gw_ctrl_revival Revival
        {
            get { return revival; }
            set { revival = value; 
                if (revival != null)
                {
                    this.textBox_duration_time.Text = revival.DurationTime.ToString();
                    this.textBox_revival_cycle.Text = revival.RevivalCycle.ToString();
                    this.textBox_revival_time.Text = revival.RevivalTime.ToString();
                }
            }
        }

        public Dialog_revival()
        {
            InitializeComponent();
        }



        private void Dialog_revival_Load(object sender, EventArgs e)
        {
            this.button_cancle.DialogResult = DialogResult.Cancel;
            this.button_ok.Click += Button_ok_Click;
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            UInt32 val = 0;
            if(this.revival == null)
                this.revival = new gw_ctrl_revival();
            if( !UInt32.TryParse(this.textBox_revival_time.Text, out val) ) {
                MessageBox.Show("请输入正确的唤醒参考时间");
                return;
            }
            this.revival.RevivalTime = val;
              
            UInt16 val16 = 0;
            if(!UInt16.TryParse(this.textBox_revival_cycle.Text, out val16))
            {
                MessageBox.Show("请输入正确的唤醒周期");
                return;

            }
            revival.RevivalCycle = val16;

            if(!UInt16.TryParse(this.textBox_duration_time.Text, out val16))
            {
                MessageBox.Show("请输入正确的唤醒时长");
                return;
            }
            revival.DurationTime = val16;
            this.DialogResult = DialogResult.OK;
        }
    }
}
