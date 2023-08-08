using System.Collections.Generic;
using System.Windows.Forms;
using Tools;
using ResModel.nw;

namespace GridBackGround.Forms.Dialogs_nw
{
    public partial class Dialog_nw_function : Form
    {

        public List<CheckBox> Checkbox_list { get; set; }

        public string Password
        {
            get { return this.textBox_password.Text; }
            set { this.textBox_password.Text = value; }
        }

        public List<nw_func_code> Functions 
        { 
            get { return GetFunctions(); }
            set { SetFunctions(value); }
        }

        public Dialog_nw_function()
        {
            InitializeComponent();
            this.CenterToParent();

            Function_Checkbox_init(this.checkBox1, nw_func_code.Pull);
            Function_Checkbox_init(this.checkBox2, nw_func_code.Weather);
            Function_Checkbox_init(this.checkBox3, nw_func_code.Picture);
            Function_Checkbox_init(this.checkBox4, nw_func_code.Fault_Detect);
            Function_Checkbox_init(this.checkBox5, nw_func_code.Traffic);
            Function_Checkbox_init(this.checkBox6, nw_func_code.EnergySatus);
        }

        public List<nw_func_code> GetFunctions()
        {
            List<nw_func_code> funcs= new List<nw_func_code>();
            foreach(System.Windows.Forms.CheckBox checkbox in this.Checkbox_list)
            {
                if (checkbox.Checked)
                    funcs.Add((nw_func_code)checkbox.Tag);
            }
            return funcs;
        }

        public void SetFunctions(List<nw_func_code> funcs)
        {
            foreach (CheckBox checkbox in this.Checkbox_list)
                checkbox.Checked = false;
            foreach(nw_func_code func in funcs)
            {
                foreach (CheckBox checkbox in this.Checkbox_list)
                {
                    if((nw_func_code)checkbox.Tag == func)
                    {
                        checkbox.Checked= true;
                        break;
                    }
                }
            }
        }

        private void Function_Checkbox_init(CheckBox checkBox, nw_func_code fuction)
        {
            if(Checkbox_list == null)
                Checkbox_list = new List<CheckBox>();
            checkBox.Text = fuction.GetDescription();
            checkBox.Tag = fuction;
            checkBox.Checked = true;
            Checkbox_list.Add(checkBox);
        }

    }
}
