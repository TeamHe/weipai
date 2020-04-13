using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridBackGround.Forms
{
    public partial class Dialog_Image_TimeTable : Form
    {
        public Dialog_Image_TimeTable()
        {
            this.CenterToParent();
            InitializeComponent();
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
                TimeTable = new List<CommandDeal.IPhoto_TimeTable>();
            }
            else
            {
                foreach (CommandDeal.IPhoto_TimeTable ptt in TimeTable)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = ptt.Hour.ToString();
                    lvi.SubItems.Add(ptt.Minute.ToString());
                    lvi.SubItems.Add(ptt.Presetting_No.ToString());
                    this.listView1.Items.Add(lvi);
                }
            }
           
            this.comboBox1.SelectedIndex = 0;
            button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Qurey_State = true;
            radioButton1.Checked = true;
        }

        #region 公共变量
        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_No { get; set; }
        public bool Qurey_State { get; set; }
        /// <summary>
        /// 时间表链表
        /// </summary>
        public List<CommandDeal.IPhoto_TimeTable> TimeTable { get; set; }
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
            Channel_No = this.comboBox1.SelectedIndex + 1;
            if (!Qurey_State)
            {    
                if (this.listView1.Items.Count > 0)
                {
                    foreach (ListViewItem lvi in listView1.Items)
                    {
                        int hour = int.Parse(lvi.SubItems[0].Text);
                        int minute = int.Parse(lvi.SubItems[1].Text);
                        int preset_No = int.Parse(lvi.SubItems[2].Text);
                        var tt = (CommandDeal.IPhoto_TimeTable)new CommandDeal.PhotoTimeTable(hour, minute, preset_No);
                        TimeTable.Add(tt);
                    }
                }
                else
                {
                    MessageBox.Show("您没有添加任何时间，请先添加时间");
                    return;
                }

            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Dispose();
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
