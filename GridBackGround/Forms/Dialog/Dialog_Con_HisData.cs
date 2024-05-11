using ResModel.gw;
using System;
using System.Windows.Forms;
using Tools;

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

            ComboBoxItem.Init_items_enum(this.comboBox1, typeof(gw_func_code));
            this.comboBox1.SelectedIndex = 0;
            this.checkBox1.Checked = true;
            CurrentData = true;

            this.EndTime = DateTime.Now;
            this.StartTime = DateTime.Now.AddHours(-1);
        }
        #endregion
        

        #region Public Variable
        public gw_func_code Data_Type { get;  set; }
        public DateTime StartTime 
        { 
            get { return this.dateTimePicker_StartTime.Value; }  
            set { this.dateTimePicker_StartTime.Value = value; } 
        }
        public DateTime EndTime 
        {
            get { return this.dateTimePicker_EndTime.Value; }
            set { this.dateTimePicker_EndTime.Value = value; }
        }
        public bool CurrentData 
        {
            get { return this.checkBox1.Checked; }
            set { this.checkBox1.Checked = value; } 
        }
        #endregion
        
        private void button_OK_Click(object sender, EventArgs e)
        {
            #region 数据类型
            ComboBoxItem color = this.comboBox1.SelectedItem as ComboBoxItem;
            Data_Type = (gw_func_code)color.Value;
            #endregion
            if (!CurrentData)
            {
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
            this.DialogResult = DialogResult.OK;
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
            }
            else
            {
                dateTimePicker_StartTime.Enabled = true;
                dateTimePicker_EndTime.Enabled = true;
            }
        }

      
    }
}
