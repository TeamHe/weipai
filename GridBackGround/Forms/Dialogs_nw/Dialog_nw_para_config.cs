using System;
using System.Windows.Forms;
using ResModel.nw;

namespace GridBackGround.Forms.Dialogs_nw
{
    public partial class Dialog_nw_para_config : Form
    {
        public nw_device_config Config 
        {
            get { return this.ConfigGet(); }
            set { this.ConfigSet(value); }
        }

        public string Password
        {
            get { return this.textBox_password.Text; }
            set { this.textBox_password.Text = value; }
        }

        public Dialog_nw_para_config()
        {
            InitializeComponent();
            this.CenterToParent();
            
        }

        public void ConfigSet(nw_device_config config)
        {
            this.Config = config;
            this.textBox_auth_paasword.Text = Config.Password;
            this.textBox_heart_time.Text = (this.Config.Heart).ToString();
            this.textBox_ScanInterval.Text = (this.Config.ScanInterval).ToString();
            this.textBox_DormancyDuration.Text = (this.Config.DormancyDuration ).ToString();
            this.textBox_OnlineTime.Text = (this.Config.OnlineTime).ToString();
            this.numericUpDown_reboot_time_day.Value = this.Config.Reboot_day;
            this.numericUpDown_reboot_time_hour.Value = this.Config.Reboot_hour;
            this.numericUpDown_reboot_time_min.Value = this.Config.Reboot_min;
        }

        public nw_device_config ConfigGet()
        {
            nw_device_config config = new nw_device_config();
            int val = 0;

            if (int.TryParse(this.textBox_heart_time.Text, out val) && val <= 6)
                config.Heart = val;
            else
                throw new ArgumentException("请输入正确的心跳周期,小于6分钟");

            if (int.TryParse(this.textBox_ScanInterval.Text, out val))
                config.ScanInterval = val;
            else
                throw new ArgumentException("请输入正确的采集间隔");

            if (int.TryParse(this.textBox_DormancyDuration.Text, out val))
                config.DormancyDuration = val;
            else
                throw new ArgumentException("请输入正确的休眠时长");

            if (int.TryParse(this.textBox_OnlineTime.Text, out val))
                config.OnlineTime = val;
            else
                throw new ArgumentException("请输入正确在线时长");

            config.Reboot_day = (int)this.numericUpDown_reboot_time_day.Value;
            config.Reboot_hour = (int)this.numericUpDown_reboot_time_hour.Value;
            config.Reboot_min = (int)this.numericUpDown_reboot_time_min.Value;
            config.Password = this.textBox_auth_paasword.Text;
            return config;

        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            try
            {
                this.ConfigGet();
                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void textBox_Int_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!(e.KeyChar >= 0x30 && e.KeyChar <= 0x39)) && (e.KeyChar!= 0x08))
            { e.Handled = true; }
        }
    }
}
