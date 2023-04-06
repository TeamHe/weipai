using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ResModel.EQU;
using ResModel.CollectData;
using DB_Operation.EQUManage;
using Tools;
using ResModel;

namespace GridBackGround.Forms.Tab
{
    public partial class Tab_IDs : Form
    {
        #region Private Varialbe
        private const string TestLineName = "TestLine";
        private const string TestTowerName = "TestTower";
        private int lineID = -1;

        public delegate void NodeStateChange(IPowerPole powerPole);

        private NodeStateChange changeDelegate;

        //private string CMD_ID = null;

        private int TestEquNum = 0;

        #endregion
        
        
        #region Construction
        public Tab_IDs()
        {
            changeDelegate = new NodeStateChange(NodeChange);
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
            this.ResumeLayout(false);

        }

        /// <summary>
        /// 窗体load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tab_IDs_Load(object sender, EventArgs e)
        {
            TreeViewUpdate();
            //终端状态变化事件
            Termination.PowerPoleManage.OnStateChange +=
                new Termination.OnLineStateChange(OnLineStateChange);
            treeView1.ShowNodeToolTips = true;
        }
        #endregion
        
        /// <summary>
        /// 设备在线状态更改
        /// </summary>
        /// <param name="powerPole"></param>
        protected void OnLineStateChange(Termination.PowerPole powerPole)
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
                            TreeNode parent = AddTestNode();
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
        
        private TreeNode AddTestLine()
        {
            TreeNode lineNode = null;
            foreach (TreeNode node in treeView1.Nodes)
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
                this.treeView1.Nodes.Add(lineNode);
            }
            return lineNode;
        }

        private TreeNode AddTestNode()
        {
            TreeNode towerNode = null;
            TreeNode linenode = AddTestLine();
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
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                var RootNode = treeView1.Nodes[i];
                TreeNode treeNode = CheckTower(RootNode, ID);
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
                if (node.Level != 2)
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
            var lineList = DB_Line.List_LineTowerEqu();
            TreeNode selectedNode = null;
            TreeViewList.LineList(this.treeView1.Nodes,lineList,out selectedNode);
            Termination.PowerPoleManage.UpdatePolesStation();
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
                if (powerPole.OnLine)
                    nodeequ.Status = OnLineStatus.Online;
                else
                    nodeequ.Status = OnLineStatus.Offline;
                node.Tag = nodeequ;
            }

            Equ equ = DB_EQU.GetEqu(powerPole.CMD_ID);
            if (equ != null)
            {
                node.ToolTipText = equ.ToString();
                node.Tag = equ;
            }
            else
            {
                equ = (Equ)node.Tag;
                if (powerPole.OnLine)
                    equ.Status = OnLineStatus.Online;
                else
                    equ.Status = OnLineStatus.Offline;
                node.ToolTipText = equ.ToString();
            }
            if(powerPole.IP != null)
                node.ToolTipText +=  "\n设备IP：" + powerPole.IP.ToString();
        }

        #endregion

        public event EventHandler<CMDid_Change> CMD_ID_Change;
        
       /// <summary>
       /// 节点选择变化事件
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            EventHandler<CMDid_Change> handler = CMD_ID_Change;
            if (handler == null) return;
            if (e.Node.Level == 2)
            {
                TreeNode nodeEqu = e.Node;
                TreeNode nodeTower = nodeEqu.Parent;
                TreeNode nodeLine = nodeTower.Parent;
                var equ = (Equ)nodeEqu.Tag;
                var tower = (Tower)nodeTower.Tag;
                var line = (Line)nodeLine.Tag;
                this.textBox1.Text = equ.EquID;
                handler(this, new CMDid_Change(equ.EquID, string.Format("{0}->{1}->{2}",line.Name,tower.TowerName,equ.Name)));
            }
            else
                handler(this, new CMDid_Change(null));
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
            if(this.textBox1.Text.Length != 17)
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
