using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using GridBackGround.CommandDeal.nw;
using ResModel.EQU;
using Tools;
using static GridBackGround.CommandDeal.nw.Camera_Action;

namespace GridBackGround.Forms.Dialogs_nw
{
    public partial class Dialog_nw_function : Form
    {

        public List<System.Windows.Forms.CheckBox> Checkbox_list { get; set; }

        public string Password
        {
            get { return this.textBox_password.Text; }
            set { this.textBox_password.Text = value; }
        }

        public List<e_nw_function> Functions 
        { 
            get { return GetFunctions(); }
            set { SetFunctions(value); }
        }

        public Dialog_nw_function()
        {
            InitializeComponent();
            this.CenterToParent();

            Function_Checkbox_init(this.checkBox1, e_nw_function.Pull_angle);
            Function_Checkbox_init(this.checkBox2, e_nw_function.Weather);
            Function_Checkbox_init(this.checkBox3, e_nw_function.Image_Tranfer);
            Function_Checkbox_init(this.checkBox4, e_nw_function.Device_Error);

        }

        public List<e_nw_function> GetFunctions()
        {
            List<e_nw_function> funcs= new List<e_nw_function>();
            foreach(System.Windows.Forms.CheckBox checkbox in this.Checkbox_list)
            {
                if (checkbox.Checked)
                    funcs.Add((e_nw_function)checkbox.Tag);
            }
            return funcs;
        }

        public void SetFunctions(List<e_nw_function> funcs)
        {
            foreach (System.Windows.Forms.CheckBox checkbox in this.Checkbox_list)
                checkbox.Checked = false;
            foreach(e_nw_function func in funcs)
            {
                foreach (System.Windows.Forms.CheckBox checkbox in this.Checkbox_list)
                {
                    if((e_nw_function)checkbox.Tag == func)
                    {
                        checkbox.Checked= true;
                        break;
                    }
                }
            }
        }

        private void Function_Checkbox_init(System.Windows.Forms.CheckBox checkBox,e_nw_function fuction)
        {
            if(Checkbox_list == null)
                Checkbox_list = new List<System.Windows.Forms.CheckBox>();
            checkBox.Text = fuction.GetDescription();
            checkBox.Tag = fuction;
            checkBox.Checked = true;
            Checkbox_list.Add(checkBox);
        }

    }
}
