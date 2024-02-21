using ResModel;
using ResModel.EQU;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using cma.service;
using GridBackGround.Termination;

namespace GridBackGround
{
    public partial class Tab_ID : Form
    {
        //装置ID
        private string CMD_ID = null;
        public Tab_ID()
        {
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
        }

        private void Tab_ID_Load(object sender, EventArgs e)
        {
            //不显示装置名称
            this.listView1.Columns[0].Width = 60;
            //终端状态变化事件
            PowerPoleManage.OnStateChange += 
                new OnLineStateChange(OnLineStateChange);


            this.listView1.Columns[3].Width = 0;
            this.listView1.Columns[2].Width = 130;
        }
        /// <summary>
        /// 设备在线状态更改
        /// </summary>
        /// <param name="powerPole"></param>
        protected void OnLineStateChange(PowerPole powerPole)
        {
            NodeChange(powerPole);
            powerPole = null;
        }

        #region 节点状态变化处理

        public delegate void NodeStateChange(IPowerPole powerPole);
        /// <summary>
        /// 向添加ID
        /// </summary>
        /// <param name="str"></param>
        private void NodeChange(IPowerPole powerPole)
        {
            //需要进行委托处理
            if (this.InvokeRequired)
            {
                //产生一个委托调用，然后修改控件的值
                this.Invoke(new NodeStateChange(this.NodeChange), new object[] { powerPole });
            }
            else
            {
                int ItemNO;
                if (!FindItem(powerPole.CMD_ID, out ItemNO))
                {
                    ListViewItem lvi = new ListViewItem();
                    try
                    {
                        if (powerPole.Name != null)
                            lvi.Text = powerPole.Name;
                        if (powerPole.CMD_ID != null)
                            lvi.SubItems.Add(powerPole.CMD_ID);
                        else
                            lvi.SubItems.Add("");
                        if (powerPole.IP != null)

                            lvi.SubItems.Add(powerPole.IP.ToString());
                        else
                            lvi.SubItems.Add("");
                        lvi.SubItems.Add(powerPole.OnLine.ToString());
                        this.listView1.Items.Add(lvi);
                    }
                    catch { };
                }
                else
                {
                    if(powerPole.Name != null)
                    listView1.Items[ItemNO].Text = powerPole.Name;
                    if(powerPole.CMD_ID!= null)
                    listView1.Items[ItemNO].SubItems[1].Text = powerPole.CMD_ID;
                    if(powerPole.IP != null)
                        listView1.Items[ItemNO].SubItems[2].Text = powerPole.IP.ToString();
                    //if (powerPole.OnLine)
                    //    listView1.Items[ItemNO].SubItems[3].Text = "在线";
                    //else
                    //    listView1.Items[ItemNO].SubItems[3].Text = "离线";
                }
                //默认选中第一行的ID
                if (this.CMD_ID == null)
                {
                    if (listView1.Items.Count > 0)
                    {
                        this.listView1.Items[0].Selected = true;
                        this.listView1.Items[0].BackColor = Color.BurlyWood;
                    }
                }

            }
        }



        /// <summary>
        /// 查找有没有指定ID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool FindItem(string str, out int No)
        {
            No = 0;
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                ListViewItem lvi = this.listView1.Items[i];
                if (lvi.SubItems[1].Text == str)
                {
                    No = i;
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 获得CMD_ID
        /// <summary>
        /// ID选择变化事件
        /// </summary>
        public event EventHandler<CMDid_Change> CMD_ID_Change;

        /// <summary>
        /// 选择装置，对应装置ID改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                int index = this.listView1.SelectedItems[0].Index;
                CMD_ID = this.listView1.Items[index].SubItems[1].Text;
                this.listView1.SelectedItems[0].BackColor = Color.BurlyWood;
            }
            else
            {
                CMD_ID = "";
            }

            EventHandler<CMDid_Change> handler = CMD_ID_Change;

            if (handler != null)
            {
                handler(this, new CMDid_Change(CMD_ID));
            }
        }

        #endregion

        #region 右击
        /// <summary>
        /// 节点管理右击菜单显示控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip_Node_Opening(object sender, CancelEventArgs e)
        {
            //this.删除当前节点toolStripMenuItem.Visible = false;
            //this.修改当前节点ToolStripMenuItem.Visible = false;
            //this.添加节点ToolStripMenuItem.Visible = true;
            if (this.listView1.SelectedItems.Count == 0)
            {
                this.删除当前节点toolStripMenuItem.Visible = false;
                this.修改当前节点ToolStripMenuItem.Visible = false;
            }
            else
            {
                this.删除当前节点toolStripMenuItem.Visible = true;
                this.修改当前节点ToolStripMenuItem.Visible = true;
            }
        }

        /// <summary>
        /// 右击节点管理添加节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 添加节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Forms.Dialog_AddNode fan = new Forms.Dialog_AddNode();
            //fan.ShowDialog();
        }

        /// <summary>
        /// 删除设备列表中的当前选中行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除节点toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                ListViewItem lvi = this.listView1.SelectedItems[0];
                this.listView1.Items.Remove(this.listView1.SelectedItems[0]);
            }
        }
       

        private void 修改当前节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Forms.Dialog_AddNode fan = new Forms.Dialog_AddNode();
           
            //fan.ADD = false;
            
            //ListViewItem lvi = this.listView1.SelectedItems[0];
            //fan.CMD_ID = lvi.SubItems[1].Text;
            //fan.CMD_Name = lvi.SubItems[0].Text;
            //fan.ShowDialog(); 
            
            
        }
        #endregion

        private void listView1_MouseHover(object sender, EventArgs e)
        {
            
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.toolTip1.Hide(this.listView1);
        }
    }

    /// <summary>
    /// ID变化事件载体
    /// </summary>
    public class CMDid_Change : EventArgs
    {
        /// <summary>
        /// 设备状态变化
        /// </summary>
        /// <param name="powerPole"></param>
        public CMDid_Change(string cmd_id)
        {
            this.CMD_ID = cmd_id;
            this.CMD_NAME = "";
        }
        public CMDid_Change(string cmd_id, string cmd_name)
        {
            this.CMD_ID = cmd_id;
            this.CMD_NAME = cmd_name;
        }
        /// <summary>
        /// 发送状态变化的节点
        /// </summary>
        public string CMD_ID { get; set; }

        public string CMD_NAME { get; set; }

        public DevFlag Flag { get; set; }

        public Equ equ { get; set; }
    }
}
