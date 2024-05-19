using ResModel.gw;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tools;


namespace GridBackGround.Forms.Dialog
{
    public partial class Dialog_Con_alarm : Form
    {
        public Dialog_Con_alarm()
        {
            InitializeComponent();
            this.Load += Dialog_Con_alarm_Load;
        }

        List<UC_con_alarm> alarm_values = new List<UC_con_alarm>();

        private void Dialog_Con_alarm_Load(object sender, System.EventArgs e)
        {
            this.alarm_values.Add(this.uC_con_alarm1);
            this.alarm_values.Add(this.uC_con_alarm2);
            this.alarm_values.Add(this.uC_con_alarm3);
            this.alarm_values.Add(this.uC_con_alarm4);
            this.alarm_values.Add(this.uC_con_alarm5);
            this.alarm_values.Add(this.uC_con_alarm6);
            this.alarm_values.Add(this.uC_con_alarm7);
            this.alarm_values.Add(this.uC_con_alarm8);
            this.alarm_values.Add(this.uC_con_alarm9);
            this.alarm_values.Add(this.uC_con_alarm10);

            int no = 1;
            foreach(UC_con_alarm alarm in alarm_values)
            {
                alarm.AlarmValue = new gw_ctrl_alarm_value()
                {
                    Key = string.Format("PARA{0:D2}", no),
                    Name = string.Format("参数{0}", no),
                    Value = 0,
                };
                no ++;
            }

            ComboBoxItem.Init_items_enum(this.comboBox1, typeof(gw_para_type));
            this.comboBox1.SelectedIndex = 0;

        }

        public bool Query { get;set; }

        public gw_ctrl_alarm Alarm { get; set; }

        public gw_para_type Type 
        {
            get
            {
                ComboBoxItem type = this.comboBox1.SelectedItem as ComboBoxItem;
                return (gw_para_type)type.Value;
            }
            set
            {
                ComboBoxItem.Set_Value(this.comboBox1, (int)value);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if(this.Alarm == null)
                this.Alarm = new gw_ctrl_alarm();
            Alarm.Values.Clear();
            Alarm.Type = this.Type;

            this.Query = true;
            this.DialogResult = DialogResult.OK;
        }

        private void button_OK_Click(object sender, System.EventArgs e)
        {
            if (this.Alarm == null)
                this.Alarm = new gw_ctrl_alarm();

            Alarm.Values.Clear();
            Alarm.Type = this.Type;

            foreach(UC_con_alarm alarm in alarm_values)
            {
                if (!alarm.Checked) continue;
                if(!alarm.ValueValid)
                {
                    MessageBox.Show(string.Format("{0} 参数格式化失败", alarm.Text));
                    return;
                }
                Alarm.Values.Add(alarm.AlarmValue);
            }
            if(Alarm.Values.Count == 0)
            {
                MessageBox.Show("没有设定任何参数");
                return;
            }

            this.Query = false;
            this.DialogResult = DialogResult.OK;
        }

        private void button_Cancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
