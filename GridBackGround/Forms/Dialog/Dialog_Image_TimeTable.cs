using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ResModel.Image;

namespace GridBackGround.Forms
{
    public partial class Dialog_Image_TimeTable : Form
    {
        public Dialog_Image_TimeTable()
        {
            this.CenterToParent();
            InitializeComponent();
            this.radioButton2.Checked = true;
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_Image_TimeTable_Load(object sender, EventArgs e)
        {
            if (TimeTable == null)
            {
                TimeTable = new List<PhotoTime>();
            }
            else
            {
                foreach (PhotoTime ptt in TimeTable)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = ptt.Hour.ToString();
                    lvi.SubItems.Add(ptt.Minute.ToString());
                    lvi.SubItems.Add(ptt.Presetting_No.ToString());
                    this.listView1.Items.Add(lvi);
                }
            }
           
            button_Cancel.DialogResult = DialogResult.Cancel;
        }

        #region 公共变量
        /// <summary>
        /// 通道号
        /// </summary>
        /// 
        public int Channel_No { get; set; }
        private bool query = false;
        public bool Qurey_State 
        {
            get { return query; }
            set { 
                this.query =  value;
                if (this.query)
                {
                    this.radioButton1.Checked = true; 
                    this.radioButton2.Checked = false;
                }
                else
                {
                    this.radioButton1.Checked = false;
                    this.radioButton2.Checked = true;
                }
            }
        }
        /// <summary>
        /// 时间表链表
        /// </summary>
        public List<PhotoTime> TimeTable { get; set; }

        private bool nw = false;
        public bool nw_flag
        {
            get { return nw; } 
            set { 
                this.nw = value;
                if(nw)
                {
                    this.textBox_passwd.Visible = true;
                    this.label_passwd.Visible = true;
                    this.radioButton1.Visible = false;
                    this.radioButton2.Visible = false;
                }
                else
                {
                    this.textBox_passwd.Visible = false; 
                    this.label_passwd.Visible = false; 
                    this.radioButton1.Visible = true;
                    this.radioButton2.Visible = true;
                }
            }
        }

        public string Password
        {
            get { return this.textBox_passwd.Text; }
            set { this.textBox_passwd.Text = value; }
        }
        #endregion


        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DateTime time = dateTimePicker1.Value;
            int hour = time.Hour;
            int minute = time.Minute;
            UInt16 Presetting_No;

            ListViewItem lvi = new ListViewItem();
            lvi.Text = hour.ToString();
            lvi.SubItems.Add(minute.ToString());
            try
            {
                Presetting_No = UInt16.Parse(this.textBox1.Text);
            }
            catch
            {
                MessageBox.Show("请输入正确的通道号");
                return;
            }
            if (Presetting_No > 255 || Presetting_No == 0)
            {
                MessageBox.Show("请输入正确的通道号");
                return;
            }
            lvi.SubItems.Add(Presetting_No.ToString());

            this.listView1.Items.Add(lvi);
        }
        
        /// <summary>
        /// 删除按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)  //选中项遍历  
            {  
                listView1.Items.RemoveAt(lvi.Index); // 按索引移除  
            } 
        }
        /// <summary>
        /// 确定按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.TimeTable.Clear();
            Channel_No = (int)this.numericUpDown1.Value;
            if (this.listView1.Items.Count > 0)
            {
                foreach (ListViewItem lvi in listView1.Items)
                {
                    int hour = int.Parse(lvi.SubItems[0].Text);
                    int minute = int.Parse(lvi.SubItems[1].Text);
                    int preset_No = int.Parse(lvi.SubItems[2].Text);
                    TimeTable.Add(new PhotoTime(hour, minute, preset_No));
                }
            }
            else
            {
                if(this.Qurey_State == false)
                {
                    MessageBox.Show("您没有添加任何时间，请先添加时间");
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
                Qurey_State = true;
            else
                Qurey_State = false;
        }
    }
}
