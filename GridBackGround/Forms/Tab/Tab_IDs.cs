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

namespace GridBackGround.Forms.Tab
{
    public partial class Tab_IDs : Form
    {
        #region Private Varialbe
        private const string TestLineName = "TestLine";
        private const string TestTowerName = "TestTower";
        //private string CMD_ID = null;

        private int TestEquNum = 0;

        #endregion
        
        
        #region Construction
        public Tab_IDs()
        {
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
            NodeChange(powerPole);
            powerPole = null;
        }
        #region 节点状态变化处理

        public delegate void NodeStateChange(Termination.IPowerPole powerPole);
        /// <summary>
        /// 向添加ID
        /// </summary>
        /// <param name="str"></param>
        private void NodeChange(Termination.IPowerPole powerPole)
        {
            //需要进行委托处理
            if (this.InvokeRequired)
            {
                //产生一个委托调用，然后修改控件的值
                this.Invoke(new NodeStateChange(this.NodeChange), new object[] { powerPole });
            }
            else
            {
                TreeNode Node;
                if (FindItem(powerPole.CMD_ID,out Node))
                {                                                 //查找到相应ID对应的节点
                    UpdateEquMsg(Node,powerPole);               //更新节点提示信息
                }
                else
                {
                    TreeNode node = new TreeNode();
                    if ((powerPole.Name == null) || (powerPole.Name.Length <= 0))
                        node.Text = powerPole.CMD_ID;
                    else
                        node.Text = powerPole.Name;
                    node.Name = powerPole.CMD_ID;
                    AddTestNode().Nodes.Add(node);

                    UpdateEquMsg(node, powerPole);              //更新节点提示信息

                }
            }
        }


        private TreeNode AddTestNode()
        {
            TreeNode towerNode = null;
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Name == TestTowerName)
                {
                    towerNode = node;
                    break;
                }
            }
            if (towerNode ==null)
            {
                towerNode = new TreeNode();
                towerNode.Name = TestTowerName;
                towerNode.Text = "测试被测设备";
                towerNode.ToolTipText = "被测设备ID：";
                this.treeView1.Nodes.Add(towerNode);
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
        public void UpdateEquMsg(TreeNode node, Termination.IPowerPole powerPole)
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
