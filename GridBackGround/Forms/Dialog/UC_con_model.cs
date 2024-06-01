using ResModel.gw;
using System.Windows.Forms;
using Tools;

namespace GridBackGround.Forms.Dialog
{
    public partial class UC_con_model : UserControl
    {
        public UC_con_model()
        {
            InitializeComponent();
            ComboBoxItem.Init_items_enum(this.comboBox1, typeof(gw_ctrl_model.EType));
        }

        public bool MChecked 
        {
            get {  return this.checkBox1.Checked; }
            set {  this.checkBox1.Checked = value; }
        }

        public string MKey { get; set; }


        public string MName
        {
            get { return this.checkBox1.Text; }

            set { this.checkBox1.Text = value; }
        }

        public gw_ctrl_model.EType MType
        {
            get {
                var item = this.comboBox1.SelectedItem as ComboBoxItem;
                return (gw_ctrl_model.EType)item.Value;
            }
            set
            {
                ComboBoxItem.Set_Value(this.comboBox1, (int)value);
            }
        }

        public string Value
        {
            get { return this.textBox1.Text; }
            set { this.textBox1.Text = value; }
        }
    }
}
