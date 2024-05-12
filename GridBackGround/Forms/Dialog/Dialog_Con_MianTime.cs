using ResModel.gw;
using System;
using System.Windows.Forms;
using Tools;

namespace GridBackGround.Forms
{
    public partial class Dialog_Con_MianTime : Form
    {
        private gw_ctrl_period period;

        public Dialog_Con_MianTime()
        {
            InitializeComponent();
            this.CenterToParent();
            
        }
        private void Dialog_Con_MianTime_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.button1;
            this.CancelButton = this.button_Cancel;
            ComboBoxItem.Init_items_enum(this.comboBox1, typeof(gw_func_code));
            this.comboBox1.SelectedIndex = 0;
        }


        #region 公共变量
        /// <summary>
        /// 设备类型
        /// </summary>
        public gw_func_code Data_Type  
        {
            get
            {
                ComboBoxItem color = this.comboBox1.SelectedItem as ComboBoxItem;
                return (gw_func_code)color.Value;
            }
            set
            {
                ComboBoxItem.Set_Value(this.comboBox1, (int)value);
            }
        }
 
        //查询/设定
        public bool Query
        { 
            get; 
            set; 
        }
        public gw_ctrl_period Period
        {
            get { return period; }
            set
            {
                this.period = value;
                this.checkBox_MianTime.Checked = period.GetFlag((int)gw_ctrl_period.EFlag.MainTime);
                this.checkBox_HeartBeat.Checked = period.GetFlag((int)gw_ctrl_period.EFlag.HearTime);
                this.checkBox_samle_freq.Checked = period.GetFlag((int)gw_ctrl_period.EFlag.SampleFreq);
                this.checkBox_samplecount.Checked = period.GetFlag((int)gw_ctrl_period.EFlag.SampleCount);

                this.textBox_HeartTime.Text = period.HearTime.ToString();
                this.textBox_MainTime.Text = period.MainTime.ToString();
                this.textBox_sample_count.Text = period.SampleCount.ToString();
                this.textBox_sample_freq.Text = period.SampleFreq.ToString();
                this.Data_Type = period.MainType;
            }
        }
        #endregion

        private void button_OK_Click(object sender, EventArgs e)
        {
            int  num = 0;
            
            if(this.period == null)
                this.period = new gw_ctrl_period();
            this.period.MainType = this.Data_Type;
            this.period.SetFlag((int)gw_ctrl_period.EFlag.HearTime, this.checkBox_HeartBeat.Checked);
            this.period.SetFlag((int)gw_ctrl_period.EFlag.MainTime, this.checkBox_MianTime.Checked);
            this.period.SetFlag((int)gw_ctrl_period.EFlag.SampleFreq, this.checkBox_samle_freq.Checked);
            this.period.SetFlag((int)gw_ctrl_period.EFlag.SampleCount, this.checkBox_samplecount.Checked);

            if (checkBox_MianTime.Checked)
            {
                if(!int.TryParse(this.textBox_MainTime.Text, out num) || num > 65535)
                {
                    MessageBox.Show("请输入正确采样周期");
                    return;
                }
                this.period.MainTime = num;
            }
            if (checkBox_HeartBeat.Checked)
            {
                if (!int.TryParse(this.textBox_HeartTime.Text, out num) || num >= 256)
                {
                    MessageBox.Show("请输入正确的心跳周期");
                    return;
                }
                this.period.HearTime = num;
            }

            if (this.checkBox_samle_freq.Checked) 
            {
                if (!int.TryParse(this.textBox_sample_freq.Text, out num) || num > 65535)
                {
                    MessageBox.Show("请输入正确的高速采样频率");
                    return;
                }
                this.period.SampleFreq = num;
            }
            if (this.checkBox_samplecount.Checked)
            {
                if (!int.TryParse(this.textBox_sample_count.Text, out num) || num > 65535)
                {
                    MessageBox.Show("请输入正确的高速采样点数");
                    return;
                }
                this.period.SampleCount = num;
            }

            Query = false;
            this.DialogResult = DialogResult.OK;
        }

        private void buttonQuary_Click(object sender, EventArgs e)
        {
            Query = true;
            this.DialogResult = DialogResult.OK;
        }

        private void textBox_Int_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!(e.KeyChar >= 0x30 && e.KeyChar <= 0x39)) && (e.KeyChar!= 0x08))
            { e.Handled = true; }
        }
    }
}
