using ResModel.gw;
using System;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialog
{
    public partial class UC_con_alarm : UserControl
    {
        public bool Selected
        {
            get
            {
                return this.checkBox1.Checked;
            }
            set
            {
                this.checkBox1.Checked = value;
            }
        }

        public gw_ctrl_alarm_value alarm_value = null;


        public gw_ctrl_alarm_value AlarmValue
        {
            get 
            { 
                if(this.alarm_value == null)
                {
                    this.alarm_value = new gw_ctrl_alarm_value();
                    alarm_value.Key = this.Key;
                }
                if(Single.TryParse(this.textBox1.Text,out float fval))
                    alarm_value.Value = fval;
                return this.alarm_value;
            }

            set
            {
                this.alarm_value = value;
                this.Text = this.alarm_value.Name;
                this.Key = this.alarm_value.Key;
                this.Value = alarm_value.Value;
            }
        }

        public override string Text
        {
            get { return this.checkBox1.Text; }
            set { this.checkBox1.Text= value; }
        }

        public string Key { get; set; }

        public bool Checked
        {
            get { return this.checkBox1.Checked; }
            set { this.checkBox1.Checked = value; }
        }

        public float Value
        {
            get {
                float val=0;
                float.TryParse(this.textBox1.Text, out val);
                return val;
            }
            set
            {
                this.textBox1.Text = value.ToString();
            }
        }
        public bool ValueValid
        {
            get
            {
                return float.TryParse(this.textBox1.Text, out _);
            }
        }

        public UC_con_alarm()
        {
            InitializeComponent();
        }
    }
}
