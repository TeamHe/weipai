using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridBackGround.Forms
{
    public partial class Dialog_Con_HisData : Form
    {
        #region Construction
        /// <summary>
        /// construction
        /// </summary>
        public Dialog_Con_HisData()
        {
            InitializeComponent();
            this.CenterToParent();

        }
        /// <summary>
        /// 窗体load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_Con_HisData_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.button_OK;
            this.CancelButton = this.button_Cancel;

            this.comboBox1.SelectedIndex = 0;
            this.checkBox1.Checked = true;
            CurrentData = true;
        }
        #endregion
        

        #region Public Variable
        public byte Data_Type { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public bool CurrentData { get; private set; }
        #endregion
        
        private void button_OK_Click(object sender, EventArgs e)
        {
            #region 数据类型
            byte data_type = (byte)((this.comboBox1.SelectedIndex + 1)&0xff);
            Data_Type = data_type;
            #endregion
            if (!CurrentData)
            {
                StartTime = this.dateTimePicker_StartTime.Value;        //开始时间
                EndTime = this.dateTimePicker_EndTime.Value;            //结束时间
                if (StartTime > EndTime)
                {
                    MessageBox.Show("起始时间应该小于结束时间");
                    return;
                }
                if (EndTime > DateTime.Now)
                {
                    MessageBox.Show("结束时间不能大于系统当前时间");
                    return;
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            if(checkbox == checkBox1)
            if (checkBox1.Checked)
            {
                dateTimePicker_StartTime.Enabled = false;
                dateTimePicker_EndTime.Enabled = false;
                CurrentData = true;
            }
            else
            {
                dateTimePicker_StartTime.Enabled = true;
                dateTimePicker_EndTime.Enabled = true;
                CurrentData = false;
            }
        }

      
    }
}
