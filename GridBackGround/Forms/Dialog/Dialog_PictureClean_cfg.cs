using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GridBackGround.Config;

namespace GridBackGround.Forms.Dialog
{
    public partial class Dialog_PictureClean_cfg : Form
    {
        public Dialog_PictureClean_cfg()
        {
            InitializeComponent();
        }

        private void Dialog_PictureClean_cfg_Load(object sender, EventArgs e)
        {
            this.buttonCancel.DialogResult = DialogResult.Cancel;
            this.checkBox_clean_auto.Checked = SettingsForm.Default.PictureCleanAuto;
            this.checkBox_clean_atstart.Checked = SettingsForm.Default.PictureCleanAtStart;
            this.numericUpDown_cleanperiod.Value = SettingsForm.Default.PictureCleanPeriod;
            this.numericUpDownReserveTime.Value = SettingsForm.Default.PictuerCleanReserveTime;
            this.dateTimePickerCleanTime.Value = SettingsForm.Default.PictureCleanTime;
            this.textBox1.Text = SettingsForm.Default.PictuerCleanLastTime.ToString("yyyy/MM/dd HH:mm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SettingsForm.Default.PictureCleanAuto = this.checkBox_clean_auto.Checked;
            SettingsForm.Default.PictureCleanAtStart = this.checkBox_clean_atstart.Checked;
            SettingsForm.Default.PictureCleanPeriod = (int)this.numericUpDown_cleanperiod.Value;
            SettingsForm.Default.PictuerCleanReserveTime = (int)this.numericUpDownReserveTime.Value;
            SettingsForm.Default.PictureCleanTime = this.dateTimePickerCleanTime.Value;
            this.DialogResult = DialogResult.OK;
        }
    }
}
