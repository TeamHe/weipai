using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using cma.service;
using DB_Operation.RealData;
using ResModel.PowerPole;

namespace GridBackGround
{
    public partial class Tab_Packet : Form
    {
        private string CurDeviceID { get; set; }

        private bool history = false;

        private MainForm mainForm;

        /// <summary>
        /// 报文数目计数器
        /// </summary>
        private int PacketCounter = 0;
        /// <summary>
        /// 显示的报文数目
        /// </summary>
        private int PacketDisNum = Config.SettingsForm.Default.DisPackNum;

        public MainForm MainForm
        {
            get { return this.mainForm; }
            set
            {
                if (mainForm != null)
                    mainForm.OnSelectedEquChanged -= Main_OnSelectedEquChanged;
                this.mainForm = value;
                if (mainForm != null)
                    mainForm.OnSelectedEquChanged += Main_OnSelectedEquChanged;
            }
        }

        #region 初始化
        public Tab_Packet()
        {
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
        }

        private void Tab_Packet_Load(object sender, EventArgs e)
        {
            this.dateTimePicker_start.Value = DateTime.Now.AddHours(-1);
            this.dateTimePicker_end.Value = DateTime.Now;
            this.checkBox_real.Checked = true;
        }
        #endregion

        #region 通讯记录列表处理
        /// <summary>
        /// 选中通讯记录变化事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.listBox_Packet.SelectedIndex;
            this.richTextBox2.Text = listBox_Packet.Items[index].ToString();
        }

        /// <summary>
        ///  添加一条通讯记录
        /// </summary>
        /// <param name="str"></param>
        private void DisplayPackageMessage(string str)
        {

            this.listBox_Packet.Items.Insert(this.listBox_Packet.Items.Count,
                "(" + (PacketCounter++).ToString().PadLeft(5) + ") "
                //+  DateTime.Now.ToShortTimeString() 
                + " " + str);
        }

        /// <summary>
        /// 显示多条通讯记录
        /// </summary>
        /// <param name="msgs"></param>
        private void DisplayPackageMessages(List<PackageMessage> msgs)
        {
            if (msgs == null || msgs.Count == 0)
                return;
            this.listBox_Packet.BeginUpdate();
            foreach (PackageMessage msg in msgs)
                DisplayPackageMessage(msg.ToString());

            while (listBox_Packet.Items.Count > PacketDisNum)
                listBox_Packet.Items.RemoveAt(0);
            this.listBox_Packet.SetSelected(this.listBox_Packet.Items.Count - 1, true);
            this.listBox_Packet.EndUpdate();
        }

        /// <summary>
        /// 清除所有通讯记录
        /// </summary>
        private void ClearPackageMessages()
        {
            this.PacketCounter = 1;
            this.listBox_Packet.Items.Clear();
        }

        /// <summary>
        /// 从数据库加载记录列表并显示
        /// </summary>
        /// <param name="cmdid">关联的设备ID</param>
        /// <param name="start">起始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="limits">最大数据量</param>
        /// <returns></returns>
        private void LoadHistoryPackageMessages(string cmdid, DateTime start, DateTime end, int limits)
        {
            try {
                db_package_message db = new db_package_message();
                DataTable dt = db.DataGet(cmdid, start, end, limits);
               var msgs = db.GetPackageMessage_from_datatable(dt);
                this.DisplayPackageMessages(msgs);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Load package messages failed." + ex.Message);
            }
        }
        private void LoadHistoryPackageMessages(DateTime start, DateTime end, int limits=0)
        {
            if(this.CurDeviceID == null)
            {
                MessageBox.Show("当前没有选中任何设备，请先选中设备");
                return;
            }
            LoadHistoryPackageMessages(this.CurDeviceID,start,end,limits);
        }

        #endregion

        #region  事件处理
        /// <summary>
        /// 实时通讯记录列表更新事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNewPakageMessages(object sender, PackageMessageEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<PackageMessageEventArgs>(this.OnNewPakageMessages),
                    new object[] { sender, e });
            }
            else
            {
                if (e.Msgs == null || e.Msgs.Count == 0)
                    return;
                if (this.checkBox_all.Checked)
                {
                    DisplayPackageMessages(e.Msgs);
                    return;
                }
                if (this.CurDeviceID == null)
                    return;
                List<PackageMessage> cur_used = new List<PackageMessage>();
                foreach (PackageMessage msg in e.Msgs)
                {
                    if (msg.pole == null || msg.pole.CMD_ID != this.CurDeviceID)
                        continue;
                    cur_used.Add(msg);
                }
                DisplayPackageMessages(cur_used);
            }
        }

        /// <summary>
        /// 当前选中的设备变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_OnSelectedEquChanged(object sender, CMDid_Change e)
        {
            if (e == null || e.CMD_ID == null) return;
            if (e.CMD_ID == this.CurDeviceID) return;

            this.CurDeviceID = e.CMD_ID;
            this.label_curdev.Text = e.CMD_NAME + ":" + e.CMD_ID;

            if (this.checkBox_real.Checked)
            if (!this.checkBox_all.Checked)
            { //显示实时数据，但是不显示所有设备的数据，清空列表
                this.ClearPackageMessages();
                ///TODO: 
                ///    主动加载当前设备一段时间内的记录列表
            }
        }

        /// <summary>
        /// 清空通讯记录列表事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearPackage_Click(object sender, EventArgs e)
        {
            this.ClearPackageMessages();
        }

        /// <summary>
        /// 清空报文命令栏内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearCommand_Click(object sender, EventArgs e)
        {
            this.richTextBox2.Text = "";
        }
        
        private void buttonSendData_Click(object sender, EventArgs e)
        {
            string temp = this.richTextBox2.Text;//this.textBox_Send.Text;
            byte[] bytes = Tools.StringTrun.StrToHexByte(temp);
            PackeDeal.SendData(bytes);
        }

        private void checkBox_real_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_real.Checked)
            {
                if (this.history)
                    this.ClearPackageMessages();
                DisPacket.OnNewPakageMessage += OnNewPakageMessages;
            }
            else
                DisPacket.OnNewPakageMessage -= OnNewPakageMessages;
        }

        /// <summary>
        /// 手动查询历史报文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHistoryCustom_Click(object sender, EventArgs e)
        {
            DateTime start = this.dateTimePicker_start.Value;
            DateTime end = this.dateTimePicker_end.Value;
            if(start.CompareTo(end) > 0)
            {
                MessageBox.Show("起始时间不能大于结束时间,请重新选择");
                return;
            }
            this.checkBox_real.Checked = false;
            this.ClearPackageMessages();
            this.LoadHistoryPackageMessages(start, end);
            this.history = true;
        }

        /// <summary>
        /// 查询最近一小时内的报文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_his_hour_Click(object sender, EventArgs e)
        {
            this.checkBox_real.Checked = false;
            this.ClearPackageMessages();
            this.LoadHistoryPackageMessages(DateTime.Now.AddHours(-1), DateTime.Now);
            this.history = true;
        }

        /// <summary>
        /// 查询最近一天内的记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_his_day_Click(object sender, EventArgs e)
        {
            this.checkBox_real.Checked = false;
            this.ClearPackageMessages();
            this.LoadHistoryPackageMessages(DateTime.Now.AddDays(-1), DateTime.Now);
            this.history = true;
        }

        /// <summary>
        /// 保存记录列表按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Title = "保存为",
                Filter = "文本文件| *.txt",
                RestoreDirectory = true,
            };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            if(this.listBox_Packet.Items.Count == 0)
            {
                MessageBox.Show("没有通讯记录可以存储");
                return;
            }

            try
            {
                StreamWriter stream = new StreamWriter(dialog.FileName);
                foreach(var item in this.listBox_Packet.Items)
                {
                    stream.WriteLine(item.ToString());  
                }
                stream.Close();
            }catch(Exception ex) 
            {
                MessageBox.Show("通讯记录保存失败," + ex.Message);
            }
        }
        #endregion

    }
}
