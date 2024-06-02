using System;
using System.IO;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialog
{
    public partial class Dialog_Update : Form
    {
        #region Public Variables
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName 
        { 
            get { return this.textBox_File.Text; }
            set { this.textBox_File.Text = value; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime 
        {
            get { return this.dateTimePicker_UpdateTime.Value; }
            set { this.dateTimePicker_UpdateTime.Value = value; }
        }

        public int PacLength { get; set; }
        #endregion
        
        
        public Dialog_Update()
        {
            InitializeComponent();
            this.CancelButton = this.button_Cancel;
            this.CenterToScreen();
        }

        private void Dialog_Update_Load(object sender, EventArgs e)
        {
            this.button_Browse.Click += new EventHandler(button_Browse_Click);
            this.comboBox1.SelectedIndex = 1;
        }

        void button_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialogRemoteUpdate = new OpenFileDialog();

            OpenFileDialogRemoteUpdate.Filter = "bin文件(*.bin)|*.bin|所有文件(*.*)|*.*";

            if (OpenFileDialogRemoteUpdate.ShowDialog(this) != DialogResult.OK)
                return;
                
            string fullFileName = OpenFileDialogRemoteUpdate.FileName;
            this.textBox_File.Text = fullFileName;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (this.FileName.Length == 0)
            {
                MessageBox.Show("您还未选择文件");
            }
            bool exist = File.Exists(this.textBox_File.Text);
            if (!exist)
            {
                MessageBox.Show(this, "文件不存在", "错误", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (Path.GetFileName(this.textBox_File.Text).Length > 20)
            {
                MessageBox.Show(this,"文件名长度不能大于20");
                return;
            }

            if (int.TryParse(this.comboBox1.Text, out int leng) == true)
                this.PacLength = leng;
            else
            {
                MessageBox.Show("无效的包长度");
                return ;
            }
            this.DialogResult = DialogResult.OK;
        }

    }
}
