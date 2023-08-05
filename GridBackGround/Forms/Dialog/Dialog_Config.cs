using System;
using System.Windows.Forms;
using System.IO;

namespace GridBackGround.Forms
{
    public partial class Dialog_Config : Form
    {
        public Dialog_Config()
        {
            InitializeComponent();
            this.CenterToParent();
        }
        //public int CMD_Port { get; private set; }
        //public int WEB_Port { get; private set; }
        //public 

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            int nw_port, webPort, gw_port;
            try
            {
                nw_port = int.Parse(this.textBox_nw_port.Text);
                gw_port = int.Parse (this.textBox_gw_port.Text);
                webPort = int.Parse(this.textBox_Web_Port.Text);

                if(gw_port > 65535)
                    throw new Exception("端口号不能大于65535");
                if (nw_port > 65535)
                    throw new Exception("端口号不能大于65535");
                if (webPort > 65535)
                    throw new Exception("端口号不能大于65535");
                //this.WEB_Port = webPort;
                string str = this.textBox_PicPath.Text;
                try
                {
                    var dir = new DirectoryInfo(str).FullName;
                    Config.SettingsForm.Default.PicturePath = dir;
                }
                catch
                {
                    MessageBox.Show("文件路径不合法,请重新输入文件路径");
                    return;
                }

                Config.SettingsForm.Default.WEB_Port = webPort;
                Config.SettingsForm.Default.nw_port = nw_port;
                Config.SettingsForm.Default.gw_port = gw_port;
                Config.SettingsForm.Default.DisPackNum = int.Parse(this.textBox_PacketNum.Text);
                Config.SettingsForm.Default.DisReportNum = int.Parse(this.textBox_ReportNum.Text);
                //Config.SettingsForm.Default.PicturePath = this.textBox_PicPath.Text;
                Config.SettingsForm.Default.Save();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("端口号格式错误"); 
                return;
            }
        }

       

        private void Dialog_Config_Load(object sender, EventArgs e)
        {
            if(Config.SettingsForm.Default.PicturePath.Length == 0)
            { 
                Config.SettingsForm.Default.PicturePath = Application.StartupPath + "\\Picture\\";
            }

            this.textBox_PicPath.Text = Config.SettingsForm.Default.PicturePath;
            this.textBox_nw_port.Text = Config.SettingsForm.Default.nw_port.ToString();
            this.textBox_Web_Port.Text = Config.SettingsForm.Default.WEB_Port.ToString();
            this.textBox_PacketNum.Text = Config.SettingsForm.Default.DisPackNum.ToString();
            this.textBox_ReportNum.Text = Config.SettingsForm.Default.DisReportNum.ToString();
            this.textBox_gw_port.Text = Config.SettingsForm.Default.gw_port.ToString();

            this.CancelButton = this.button_Cancel;
            this.AcceptButton = this.button_OK;
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            
        }
        /// <summary>
        /// 限制输入为数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar!=(char)13 && e.KeyChar!=(char)8)            
            {                
                e.Handled = true;            
            } 
        }

        /// <summary>
        /// 浏览按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (this.textBox_PicPath.Text.Length >= 2)
                fbd.SelectedPath = this.textBox_PicPath.Text;
            else
                fbd.SelectedPath = Application.StartupPath;
            if(fbd .ShowDialog() == DialogResult.OK)
            {   
                this.textBox_PicPath.Text = fbd.SelectedPath;
            }
        }
    }
}
