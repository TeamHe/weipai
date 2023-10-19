using System;
using System.Drawing;
using System.Windows.Forms;
using ResModel.EQU;
using DB_Operation.EQUManage;

namespace GridBackGround.Forms.EquMan
{
    public partial class Dialog_EQU_Man : Form
    {
        #region Private Variable
        private Dialog_EQU dialog_Equ;

        private TreeNode SelectedNode;

        private Line curLine;

        private Tower curTower;

        private Equ CurEqu;

        private TreeNode treenode_gw;

        private TreeNode treenode_nw;

        private DevFlag flag = DevFlag.NW;
        #endregion

        #region Construction
        /// <summary>
        /// Construction
        /// </summary>
        public Dialog_EQU_Man()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// 初始化用户控件
        /// </summary>
        public void UserControlInit()
        {
           
        }
        #endregion
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_EQU_Man_Load(object sender, EventArgs e)
        {
            
            ShowEquDialog();
            ContextMenu cm = new System.Windows.Forms.ContextMenu();
            ContextMenuStrip cms = new ContextMenuStrip();
            ToolStripMenuItem tsmi = new ToolStripMenuItem("刷新设备列表");
            tsmi.Click += new EventHandler(tsmi_Click);
            cms.Items.Add(tsmi);

            this.treeView_Nodes.ShowNodeToolTips = true;
            this.treeView_Nodes.ContextMenuStrip = cms;
            this.treeView_Nodes.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);
            TreeNodesClear();
            TreeNodesInit();
            this.LineList();
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
            this.treeView_Nodes.Nodes.Add(this.treenode_gw);
            this.treeView_Nodes.Nodes.Add(this.treenode_nw);
        }

        private void TreeNodesClear()
        {
            if(this.treenode_gw != null)
                this.treenode_gw.Nodes.Clear();
            if(this.treenode_nw != null)
                this.treenode_nw.Nodes.Clear();
        }

        /// <summary>
        /// 刷新设备列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsmi_Click(object sender, EventArgs e)
        {
            LineList();
        }


        /// <summary>
        /// 显示设备管理窗体
        /// </summary>
        void ShowEquDialog()
        {
            dialog_Equ = new Dialog_EQU();
            dialog_Equ.TopLevel = false;
            dialog_Equ.Dock = DockStyle.Fill;
            dialog_Equ.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.panel_Center.Controls.Add(dialog_Equ);
            dialog_Equ.Equ_ManangedEvent += new EQU_Option(dialog_Equ_Equ_Mananged);
            this.dialog_Equ.Show();
        }

        private void UpdateDevflag(TreeNode node)
        {
            if(node == null || node.Level != 0 || node.Tag == null)
                return;
            dialog_Equ.Flag = this.flag = (DevFlag)node.Tag;
            
        }

        private void UpdateLine(TreeNode node)
        {
            if(node == null || node.Level != 1 || node.Tag == null)
                return;
            if (node.Parent != null)
                this.UpdateDevflag(node.Parent);
            dialog_Equ.CurLine = this.curLine = (Line)node.Tag;
        }

        private void UpdateTower(TreeNode node)
        {
            if(node == null || node.Level != 2 || node.Tag == null)
                return;
            if (node.Parent != null)
                this.UpdateLine(node.Parent);
            dialog_Equ.CurTower = this.curTower = (Tower)node.Tag;
        }

        private void UpdateEqu(TreeNode node)
        {
            if (node == null || node.Level != 3 || node.Tag == null)
                return;
            dialog_Equ.CurrentEqu = this.CurEqu = (Equ)node.Tag;
            if (node.Parent != null)
                this.UpdateTower(node.Parent);
        }

        /// <summary>
        /// 设备选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (SelectedNode != null)
            {
                SelectedNode.ForeColor = Color.Black;
                SelectedNode.BackColor = Color.White;
            }
            SelectedNode = e.Node;
            SelectedNode.ForeColor = Color.White;
            SelectedNode.BackColor = Color.DeepSkyBlue;

            this.curLine = null;
            this.curTower = null;
            this.CurEqu = null;
            dialog_Equ.CurrentEqu = null;
            switch (e.Node.Level)
            {
                case 0:     //国网南网选择标识
                    this.UpdateDevflag(e.Node);
                    break;
                case 1:     //单位选择标识
                    this.UpdateLine(e.Node);
                    break;
                case 2:     //线路选择标识
                    this.UpdateTower(e.Node);
                    break;
                case 3:     //设备选择标识
                    this.UpdateEqu(e.Node);
                    break;
            }
        }

        
        /// <summary>
        /// 刷新装置列表
        /// </summary>
        private void LineList()
        {
            int curID = 0;
            if (CurEqu != null)
                curID = CurEqu.ID;

            var linelist = new DB_Line().List_LineTowerEqu();
            TreeViewList tree = new TreeViewList()
            {
                ParentNodes = this.treeView_Nodes.Nodes,
                Lines = linelist,
                SelectedEquID = curID,
                HasTypeNode = true,
            };
            this.TreeNodesClear();
            tree.Add_lines();
            if(tree.SelectedTreeNode != null)
                this.treeView_Nodes.SelectedNode = tree.SelectedTreeNode;
        }

        /// <summary>
        /// 装置管理事件
        /// </summary>
        /// <param name="style"></param>
        /// <param name="equ"></param>
        void dialog_Equ_Equ_Mananged(EQU_Option_Style style, Equ equ)
        {
            switch (style)
            { 
                case EQU_Option_Style.Add:
                    try
                    {
                        if (MessageBox.Show(
                                            string.Format("确定要添加设备 {0} 装置ID:{1}吗？", equ.Name,equ.EquID),
                                            "询问",
                                            MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                        equ = DB_EQU.New_EQU(equ);
                        dialog_Equ.CurrentEqu = equ;
                        MessageBox.Show("装置"+equ.Name +"添加成功");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("装置添加失败;" + ex.Message);
                    }
                    break;
                case EQU_Option_Style.Update:
                    try
                    {
                        if (MessageBox.Show(
                                            string.Format("确定要将当前设备更新为:{0} 装置ID:{1}吗？", equ.Name, equ.EquID),
                                            "询问",
                                            MessageBoxButtons.YesNo) == DialogResult.No)
                            return;

                        DB_EQU.Up_Station(equ);
                        MessageBox.Show("装置" + equ.Name + "更新成功");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("装置更新失败:" +ex.Message);
                    }
                    
                    break;
                case EQU_Option_Style.Delete:
                    try
                    {
                        if (MessageBox.Show(
                                            string.Format("确定要将删除设备{0} 装置ID:{1}吗？", equ.Name, equ.EquID),
                                            "询问",
                                            MessageBoxButtons.YesNo) == DialogResult.No)
                            return;

                        DB_EQU.Del_Station(equ);
                        MessageBox.Show("装置" + equ.Name + "删除成功");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("装置删除失败:"+ ex.Message);
                    }
                    break;
            }
            LineList();
        }
    }
}
