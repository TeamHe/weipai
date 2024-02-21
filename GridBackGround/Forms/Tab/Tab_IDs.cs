using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ResModel.EQU;
using DB_Operation.EQUManage;
using ResModel;
using System.Text;
using Tools;
using cma.service;

namespace GridBackGround.Forms.Tab
{
    public partial class Tab_IDs : Form
    {
        enum image_index
        {
            [Description("线路名称")]
            Line = 0,

            [Description("杆塔名称")]
            Tower = 1,

            [Description("设备未注册")]
            Device_init,

            [Description("设备离线")]
            Device_offline,

            [Description("设备在线")]
            Device_online,

            [Description("设备休眠")]
            Device_sleep,
        }
        #region Private Varialbe
        private const string TestLineName = "TestLine";
        private const string TestTowerName = "TestTower";
        private int lineID = -1;

        public delegate void NodeStateChange(IPowerPole powerPole);

        private NodeStateChange changeDelegate;

        //private string CMD_ID = null;

        private int TestEquNum = 0;

        private TreeNode treenode_gw;

        private TreeNode treenode_nw;

        private Equ CurEqu;
        #endregion


        #region Construction
        public Tab_IDs()
        {
            changeDelegate = new NodeStateChange(NodeChange);
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
            this.ResumeLayout(false);
            this.treeView1.ImageList = this.imageList1;

        }

        /// <summary>
        /// 窗体load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tab_IDs_Load(object sender, EventArgs e)
        {
            TreeNodesClear();
            TreeNodesInit();
            TreeViewUpdate();

            //终端状态变化事件
            Termination.PowerPoleManage.OnStateChange +=
                new Termination.OnLineStateChange(OnLineStateChange);
            treeView1.ShowNodeToolTips = true;
        }

        private void TreeNodesInit()
        {
            this.treenode_gw = new TreeNode()
            {
                Text = "国网",
                Tag = DevFlag.GW,
            };
            this.treenode_nw = new TreeNode()
            {
                Text = "南网",
                Tag = DevFlag.NW,
            };
            this.treeView1.Nodes.Add(this.treenode_gw);
            this.treeView1.Nodes.Add(this.treenode_nw);
        }

        private void TreeNodesClear()
        {
            if (this.treenode_gw != null)
                this.treenode_gw.Nodes.Clear();
            if (this.treenode_nw != null)
                this.treenode_nw.Nodes.Clear();
        }
        #endregion

        /// <summary>
        /// 设备在线状态更改
        /// </summary>
        /// <param name="powerPole"></param>
        protected void OnLineStateChange(PowerPole powerPole)
        {
            if (powerPole != null)
            {
                try
                {
                    NodeChange(powerPole);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog("OnLineStateChange ID:" + powerPole.CMD_ID,ex);
                }
            }
        }
        #region 节点状态变化处理

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
                this.Invoke(changeDelegate, new object[] { powerPole });
            }
            else
            {
                try
                {

                    if (powerPole == null || powerPole.CMD_ID==null) return;
                    TreeNode Node;
                    if (FindItem(powerPole.CMD_ID,out Node))
                    {
                        try
                        {
                            //查找到相应ID对应的节点
                            UpdateEquMsg(Node,powerPole);                 //更新节点提示信息
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog("OnLineStateChange ID-2:" + powerPole.CMD_ID, ex);
                        }

                    }
                    else
                    {
                        try
                        {

                            TreeNode node = new TreeNode();
                            if ((powerPole.Name == null) || (powerPole.Name.Length <= 0))
                                node.Text = powerPole.CMD_ID;
                            else
                                node.Text = powerPole.Name;
                            node.Name = powerPole.CMD_ID;
                            UpdateEquMsg(node, powerPole);              //更新节点提示信息
                            TreeNode parent = AddTestNode(powerPole);
                            if(parent != null)
                                parent.Nodes.Add(node);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog("OnLineStateChange ID-3:" + powerPole.CMD_ID, ex);
                        }

                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog("OnLineStateChange ID-1:" + powerPole.CMD_ID, ex);
                }

            }
        }
        
        private TreeNode AddTestLine(IPowerPole powerPole)
        {
            TreeNode lineNode = null;
            TreeNode typenode = treenode_nw;
            if (powerPole.CMD_ID.Length == 17)
                typenode = treenode_gw;
            foreach (TreeNode node in typenode.Nodes)
            {
                if (node.Name == TestLineName)
                {
                    lineNode = node;
                    break;
                }
            }
            if(lineNode == null)
            {
                lineNode = new TreeNode();
                lineNode.Name = TestLineName;
                lineNode.Text = "测试单位";
                lineNode.ToolTipText = "测试单位";
                lineNode.Tag = new Line() { Name = "测试单位", LineID = "00000000000000000",NO =-1 };
                lineNode.ImageIndex = (int)image_index.Line;
                lineNode.SelectedImageIndex = (int)image_index.Line;
                typenode.Nodes.Add(lineNode);
            }
            return lineNode;
        }

        private TreeNode AddTestNode(IPowerPole powerPole)
        {
            TreeNode towerNode = null;
            TreeNode linenode = AddTestLine(powerPole);
            if (linenode.Nodes.Count == 0)
            {
                towerNode = new TreeNode();
                towerNode.Name = TestTowerName;
                towerNode.Text = "测试线路";
                towerNode.ToolTipText = "测试线路：";
                towerNode.Tag = new Tower()
                {
                    LineID = -1,
                    TowerName = "测试线路",
                    TowerID = "00000000000000000",
                    TowerNO = -1
                };
                towerNode.ImageIndex = (int)image_index.Tower;
                towerNode.SelectedImageIndex = (int)image_index.Tower;
                linenode.Nodes.Add(towerNode);
            }
            else
            {
                towerNode = linenode.Nodes[0];
            }
            return towerNode;    
        }


        /// <summary>
        /// 查找有没有指定ID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool FindItem(string ID, out TreeNode findNode)
        { 
            foreach(TreeNode node in this.treenode_gw.Nodes)
            {
                TreeNode treeNode = CheckTower(node, ID);
                if (treeNode != null)
                {
                    findNode = treeNode;
                    return true;
                }
            }
            foreach (TreeNode node in this.treenode_nw.Nodes)
            {
                TreeNode treeNode = CheckTower(node, ID);
                if (treeNode != null)
                {
                    findNode = treeNode;
                    return true;
                }
            }
            findNode = null;
            return false;
        }
        /// <summary>
        /// 递归查找指定ID的节点
        /// </summary>
        /// <param name="RootNode"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        private TreeNode CheckTower(TreeNode RootNode, string ID)
        {
            foreach (TreeNode node in RootNode.Nodes)
            {
                if (node.Level != 3)
                {
                    var checkedNode = CheckTower(node,ID);
                    if (checkedNode != null)
                        return checkedNode;
                }
                else
                    if (((Equ)node.Tag).EquID == ID)
                        return node;
            }
            return null;
        }
        #endregion

        #region  Tree控件刷新
        public void TreeViewUpdate()
        {
            var linelist = new DB_Line().List_LineTowerEqu();
            TreeViewList tree = new TreeViewList()
            {
                ParentNodes = this.treeView1.Nodes,
                Lines = linelist,
                SelectedEquID = this.CurEqu != null ? this.CurEqu.ID : 0,
                HasTypeNode = true,
            };
            this.TreeNodesClear();
            tree.Add_lines();
            tree_lines_image_flush(this.treenode_gw.Nodes);
            tree_lines_image_flush(this.treenode_nw.Nodes);
            Termination.PowerPoleManage.UpdatePolesStation();
        }

        private void tree_lines_image_flush(TreeNodeCollection nodes)
        {
            if(nodes == null) 
                return;
            foreach(TreeNode node in nodes)
            {
                node.ImageIndex = (int)image_index.Line;
                node.SelectedImageIndex = (int)image_index.Line;
                tree_towers_image_flush(node.Nodes);
            }
        }

        private void tree_towers_image_flush(TreeNodeCollection nodes)
        {
            if (nodes == null)
                return;
            foreach (TreeNode node in nodes)
            {
                node.ImageIndex = (int)image_index.Tower;
                node.SelectedImageIndex = (int)image_index.Tower;
                tree_equs_image_flush(node.Nodes);
            }
        }

        private void tree_equs_image_flush(TreeNodeCollection nodes)
        {
            if (nodes == null)
                return;
            foreach (TreeNode node in nodes)
            {
                tree_equ_image_flush(node, node.Tag as Equ);
            }
        }

        private void tree_equ_image_flush(TreeNode node, Equ equ)
        {
            if (node == null) 
                return;
           node.ImageIndex = (int)image_index.Device_init;
            if (equ == null)
                return;
            switch (equ.Status)
            {
                case OnLineStatus.None:
                    node.ImageIndex = (int)image_index.Device_init;
                    node.SelectedImageIndex = (int)image_index.Device_init;
                    break;
                case OnLineStatus.Online:
                    node.ImageIndex = (int)image_index.Device_online;
                    node.SelectedImageIndex = (int)image_index.Device_online;
                    break;
                case OnLineStatus.Offline:
                    node.ImageIndex = (int)image_index.Device_offline;
                    node.SelectedImageIndex = (int)image_index.Device_offline;
                    break;
                case OnLineStatus.Sleep:
                    node.ImageIndex = (int)image_index.Device_sleep;
                    node.SelectedImageIndex = (int)image_index.Device_sleep;
                    break;
            }
        }

        /// <summary>
        /// 杆塔列表节点初始化
        /// </summary>
        private void TreeTowerInit(TreeView rootNode)
        { 
           
        }

      
        /// <summary>
        /// 更新节点提示信息
        /// </summary>
        /// <param name="powerPole"></param>
        /// <param name="node"></param>
        public void UpdateEquMsg(TreeNode node, IPowerPole powerPole)
        {
            if (node == null) return;
            if (node.Tag == null)
            {
                Equ nodeequ = new Equ();
                nodeequ.Name = "测试装置" + (TestEquNum++).ToString();
                //nodeequ.EquID = node.Name;
                nodeequ.EquID = powerPole.CMD_ID;
                nodeequ.Type = ICMP.UnKonwn;
                nodeequ.Status = powerPole.OnLine;
                node.Tag = nodeequ;
            }

            Equ equ = DB_EQU.GetEqu(powerPole.CMD_ID);
            if (equ != null)
            {
                equ.Status = powerPole.OnLine;
                node.ToolTipText = equ.ToString();
                node.Tag = equ;
            }
            else
            {
                equ = (Equ)node.Tag;
                equ.Status = powerPole.OnLine;
                node.ToolTipText = equ.ToString();
            }
            if(powerPole.IP != null)
                node.ToolTipText +=  "\n设备IP：" + powerPole.IP.ToString();
            tree_equ_image_flush(node, equ);
        }

        #endregion

        public event EventHandler<CMDid_Change> CMD_ID_Change;

        public void SelectedEquChange(CMDid_Change change)
        {
            if(this.CMD_ID_Change != null)
                this.CMD_ID_Change(this, change);
            this.CurEqu = change.equ;
        }

        /// <summary>
        /// 节点选择变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DevFlag flag = DevFlag.Unknown;
            Equ equ = null;
            Tower tower = null;
            Line line = null;
            TreeNode node = e.Node;
            if (node.Level == 3)
            {
                equ = (Equ)node.Tag;
                node = node.Parent;
            }
            if(node.Level == 2)
            {
                tower = (Tower)node.Tag;
                node = node.Parent;
            }
            if(node.Level == 1)
            {
                line = (Line)node.Tag;
                node = node.Parent;
            }
            if(node.Level == 0)
            {
                flag = (DevFlag)node.Tag;
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(EnumUtil.GetDescription(flag));
            if(line != null)
                builder.AppendFormat(":{0}",line.Name);
            if(tower != null)
                builder.AppendFormat("->{0}", tower.TowerName);
            if(equ != null)
                builder.AppendFormat("->{0}", equ.Name);

            this.SelectedEquChange(new CMDid_Change(equ != null ? equ.EquID : null)
            {
                Flag = flag,
                CMD_NAME = builder.ToString(),
            });
        }

        /// <summary>
        /// 根据装置ID获取装置名称提示信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetTowerNameByID(string ID)
        {
            if (ID == null) throw new NoNullAllowedException("装置ID不能为空");
            TreeNode node;
            if (FindItem(ID, out node))
            {
                TreeNode tower = node.Parent;
                TreeNode line = tower.Parent;
                return line.Text + "->"+tower.Text+ "->" + node.Text;
            }
            return ID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int length = 17;
            if (Config.SettingsForm.Default.ServiceMode == "nw")
                length = 6;
            if(this.textBox1.Text.Length != length)
            {
                MessageBox.Show("请输入正确长度的设备ID");
                return;
            }
            string id = this.textBox1.Text;
            foreach (TreeNode nodeLine in this.treeView1.Nodes)
            {
                foreach(TreeNode nodeTower in nodeLine.Nodes)
                {
                    foreach(TreeNode nodeEqu in nodeTower.Nodes)
                    {
                        Equ equ = (Equ)nodeEqu.Tag;
                        if(equ.EquID == id)
                        {
                            this.treeView1.SelectedNode = nodeEqu;
                            nodeTower.Expand();
                            nodeLine.Expand();
                            return;
                        }
                    }
                }
            }
            MessageBox.Show("没有找到ID为:" + id + "的节点");
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true; //我这里用默认颜色即可，只需要在TreeView失去焦点时选中节点仍然突显
            return;
        }
    }
}
