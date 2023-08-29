using System;
using System.Collections.Generic;
using System.Windows.Forms;
using cma.service.PowerPole;
using ResModel.PowerPole;

namespace GridBackGround
{
    public partial class Tab_Packet : Form
    {
        private string CMD_ID { get; set; }

        private MainForm mainForm;

        public MainForm MainForm { get { return this.mainForm; }
            set {
                if (mainForm != null)
                    mainForm.OnSelectedEquChanged -= Main_OnSelectedEquChanged;
                this.mainForm = value;
                if(mainForm != null)
                    mainForm.OnSelectedEquChanged += Main_OnSelectedEquChanged;
            }
        }
        ////装置ID
        //private string CMD_ID = null;
        /// <summary>
        /// 报文数目计数器
        /// </summary>
        private int PacketCounter = 0;
        /// <summary>
        /// 显示的报文数目
        /// </summary>
        private int PacketDisNum = Config.SettingsForm.Default.DisPackNum;
        
        #region 初始化
        public Tab_Packet()
        {
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
        }

        private void Tab_Packet_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 当前选中的设备变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_OnSelectedEquChanged(object sender, CMDid_Change e)
        {
            if (e == null || e.CMD_ID == null)
                return;
            this.CMD_ID = e.CMD_ID;
        }

        private void DisPacket_OnNewPakageMessage(object sender, PackageMessageEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<PackageMessageEventArgs>(this.DisPacket_OnNewPakageMessage), 
                    new object[] { sender, e});
            }
            else
            {
                if (e.Msgs == null || e.Msgs.Count == 0)
                    return;
                foreach (PackageMessage msg in e.Msgs)
                {
                    if(this.checkBox_display_all.Checked)
                    {
                        listViewPackageAdd(msg.ToString());
                        continue;
                    }
                    if (msg.pole == null || msg.pole.CMD_ID != this.CMD_ID)
                        continue;
                    listViewPackageAdd(msg.ToString());
                }
                while (listBox_Packet.Items.Count > PacketDisNum)
                {
                    listBox_Packet.Items.RemoveAt(0);
                }
            }
        }

        private void OnNewPackageMessage(List<PackageMessage> msgs)
        {
        }
        #endregion

        #region  事件处理
        /// <summary>
        /// 新的报文数据显示
        /// </summary>
        /// <param name="str"></param>

        protected void UserListChanged(string str)
        {
            listViewPackageAdd(str);
            str = null;
        }
        #region 添加报文
        /// <summary>
        /// 界面控件赋值委托
        /// </summary>
        /// <param name="str"></param>
        public delegate void SetText(string str);
        /// <summary>
        /// 界面控件赋值委托处理
        /// </summary>
        /// <param name="str"></param>
        private void listViewPackageAdd(string str)
        {

                this.listBox_Packet.Items.Insert(this.listBox_Packet.Items.Count,
                    "(" + (PacketCounter++).ToString() + ") "
                    //+  DateTime.Now.ToShortTimeString() 
                    + " " + str);
                this.listBox_Packet.SetSelected(this.listBox_Packet.Items.Count - 1, true);
        }

        #endregion

        #endregion

        /// <summary>
        /// 在报文显示区域显示完整报文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.listBox_Packet.SelectedIndex;
            this.richTextBox2.Text = listBox_Packet.Items[index].ToString();
        }

        /// <summary>
        /// 清空报文栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearPackage_Click(object sender, EventArgs e)
        {
            this.PacketCounter = 1;
            this.listBox_Packet.Items.Clear();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox1.Checked)
                DisPacket.OnNewPakageMessage += DisPacket_OnNewPakageMessage;
            else
                DisPacket.OnNewPakageMessage -= DisPacket_OnNewPakageMessage;
        }

    }
}
