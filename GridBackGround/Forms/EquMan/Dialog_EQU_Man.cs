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

        private Equ CurEqu;

        //private Equ SelectedEqu;
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
            
            this.treeView1.Nodes.Clear();
            this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);

            this.treeView1.ShowNodeToolTips = true;
            this.LineList();

            this.treeView1.ContextMenuStrip = cms;
           
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
        /// <summary>
        /// 设备选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (SelectedNode != null)
            {
                SelectedNode.ForeColor = Color.Black;
                SelectedNode.BackColor = Color.White;
            }
            SelectedNode = e.Node;
            SelectedNode.ForeColor = Color.White;
            SelectedNode.BackColor = Color.DeepSkyBlue;

            if (SelectedNode.Level == 2)
            {
                //设置选中设备信息
                TreeNode nodeEqu        = e.Node;
                TreeNode nodeTower      = nodeEqu.Parent;
                TreeNode nodeLine       = nodeTower.Parent;
                dialog_Equ.CurLine      = (Line)nodeLine.Tag;
                dialog_Equ.CurTower     = (Tower)nodeTower.Tag;
                dialog_Equ.CurrentEqu   = (Equ)nodeEqu.Tag;
                CurEqu = (Equ)nodeEqu.Tag;
            }
        }

        
        /// <summary>
        /// 刷新装置列表
        /// </summary>
        private void LineList()
        {
            this.treeView1.Nodes.Clear();          
            var linelist = new DB_Line().List_LineTowerEqu();
            int curID = 0;
            if(CurEqu != null) curID = CurEqu.ID;
            TreeNode selectedNode = null;
            TreeViewList.LineList(this.treeView1.Nodes,linelist,out selectedNode,curID);
            if (selectedNode != null)
            {
                this.treeView1.SelectedNode = selectedNode;
            }
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
                        equ = DB_Operation.EQUManage.DB_EQU.New_EQU(equ);
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
                        DB_Operation.EQUManage.DB_EQU.Up_Station(equ);
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
                        DB_Operation.EQUManage.DB_EQU.Del_Station(equ);
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
