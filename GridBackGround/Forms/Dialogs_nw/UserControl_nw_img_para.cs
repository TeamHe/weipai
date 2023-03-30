using GridBackGround.CommandDeal.nw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;

namespace GridBackGround.Forms.Dialogs_nw
{
    public partial class UserControl_nw_img_para : UserControl
    {
        public UserControl_nw_img_para()
        {
            InitializeComponent();
            ComboBoxItem.Init_items_enum(this.comboBox_Color_Select, typeof(nw_img_para.EColor));
            ComboBoxItem.Init_items_enum(this.comboBox_Resolution, typeof(nw_img_para.EResolution));
        }



        public nw_img_para Image_para 
        {
            get { return Get_Img_Para(); }
            set { Set_Img_Para(value); }
        }

        public nw_img_para Get_Img_Para()
        {
            nw_img_para para = new nw_img_para();
            para.Brightness = int.Parse(this.textBox_Luminance.Text);
            para.Saturation = int.Parse(this.textBox_Saturation.Text);
            para.Contrast = int.Parse(this.textBox_Contrast.Text);
            ComboBoxItem color = this.comboBox_Color_Select.SelectedItem as ComboBoxItem;
            para.Color = (nw_img_para.EColor)(color.Value);
            ComboBoxItem resolution = this.comboBox_Resolution.SelectedItem as ComboBoxItem;
            para.Resolution = (nw_img_para.EResolution)(resolution.Value);
            return para;
        }

        public void Set_Img_Para(nw_img_para para)
        {
            if (para == null)
                throw new ArgumentNullException("para");

            this.textBox_Luminance.Text = para.Brightness.ToString();
            this.textBox_Saturation.Text = para.Saturation.ToString();
            this.textBox_Contrast.Text = para.Contrast.ToString();
            ComboBoxItem.Set_Value(comboBox_Color_Select, (int)para.Color);
            ComboBoxItem.Set_Value(comboBox_Resolution, (int)para.Resolution);
        }

        private void Integer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!(e.KeyChar >= 0x30 && e.KeyChar <= 0x39)) && (e.KeyChar != 0x08))
            { e.Handled = true; }
        }

        private void OnTextChaned(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            try
            {
                var value = int.Parse(textbox.Text);
                if (value > 100)
                    textbox.Text = "100";
            }
            catch
            {
                MessageBox.Show("请输入正确的数字为1-100");
            }
        }

    }
}
