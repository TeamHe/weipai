using ResModel.nw;
using System;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialogs_nw
{
    public partial class Dialog_nw_update : Form
    {

        public nw_update_info Update_Info 
        {
            get 
            {
                nw_update_info info = new nw_update_info();
                info.ChannelNO = (int)this.numericUpDown1.Value;
                info.FilePath = (string)this.textBox_File.Text;
                info.Password = this.textBox1.Text;
                info.MaxPacLength = (int)this.numericUpDown2.Value;
                return info;
            }
            set
            {
                nw_update_info info = value;
                this.numericUpDown1.Value = info.ChannelNO;
                this.textBox1.Text = info.Password;
                this.textBox_File.Text = info.FilePath;
                this.numericUpDown2.Value = info.MaxPacLength;
            }
        }
        public Dialog_nw_update()
        {
            InitializeComponent();
        }

        private void button_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                this.textBox_File.Text = ofd.FileName;
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            try
            {
                nw_update_info local_info = this.Update_Info;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
