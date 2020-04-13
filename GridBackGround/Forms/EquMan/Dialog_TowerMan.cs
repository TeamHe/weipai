using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DB_Operation.EQUManage;
using ResModel.EQU;
using SQLUtils;
using Tools;
namespace GridBackGround.Forms.EquMan
{

    public partial class Dialog_Tower_Man: Form
    {
        #region Private Variables
        /// <summary>
        /// 当前选择的杆塔信息
        /// </summary>
        private Tower curTower;
        /// <summary>
        /// 当前选择的节点
        /// </summary>
        private TreeNode curNode;
        #endregion

        #region  Public Varibale
        /// <summary>
        /// 当前装置信息
        /// </summary>
        public Tower CurTower
        {
            get { return this.curTower; }
            set
            {
                this.curTower = value;
               
                if (value != null)
                {
                    this.textBox_T_TowerName.Text = curTower.TowerName;
                }
                else
                {
                    this.textBox_T_TowerName.Text = "";
                }
            }
        }

        /// <summary>
        /// Tower属性设置操作
        /// </summary>
        /// <param name="tower"></param>
        private void SetTower(Tower tower)
        {
           
        }
        #endregion

        #region Construction
        public Dialog_Tower_Man()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectionChangeCommitted += new EventHandler(comboBox1_SelectionChangeCommitted);
            GetTowerList();
            this.linkLabel1.Click += new EventHandler(linkLabeRefresh_Click);
            this.linkLabeRefresh_Click(this.linkLabel1,new EventArgs());
           
            
        }

        void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Check_TowerChanged();
            if (this.comboBox1.SelectedIndex == 0)
            {
                this.button_Add_Tower.Enabled = false;
                this.button_Update_Tower.Enabled = false;
            }
        }

        void linkLabeRefresh_Click(object sender, EventArgs e)
        {
            LineListInit();
           
        }
        #endregion

        private void TreeViewAddTowers(TreeNode parentnode, List<Tower> towerlist)
        {
            if (towerlist == null) return;
            towerlist.Sort((x,y)=>(x.TowerName.CompareTo(y.TowerName)));
            foreach (Tower tower in towerlist)
            {
                TreeNode towerNode = new TreeNode();
                towerNode.Text = tower.TowerName;
                towerNode.Name = tower.TowerNO.ToString();
                towerNode.Tag = tower;
                towerNode.ToolTipText = string.Format("线路编号:{0}\n线路名称:{1}", tower.TowerNO, tower.TowerName);
                parentnode.Nodes.Add(towerNode);
                if (curTower != null && tower.TowerNO == curTower.TowerNO)
                {
                    this.treeView_Nodes.SelectedNode = towerNode;
                    this.treeView_Nodes_AfterSelect(this.treeView_Nodes, new TreeViewEventArgs(towerNode));
                    parentnode.Expand();
                }
            }
        }

        /// <summary>
        /// 杆塔列表初始化
        /// </summary>
        /// <param name="tn_line"></param>
        private void GetTowerList()
        {
            //杆塔节点生产
            try{
                this.treeView_Nodes.Nodes.Clear();

                var lineTowerList = DB_Operation.EQUManage.DB_Line.List_LineTower();
                if (lineTowerList == null) return;
                foreach(Line line in lineTowerList)
                {
                    TreeNode node = new TreeNode();
                    node.Text = line.Name;
                    node.Name = line.NO.ToString();
                    node.ToolTipText = string.Format("单位编号:{0}\n单位名称:{1}", line.NO, line.Name);
                    node.Tag = line;
                    this.treeView_Nodes.Nodes.Add(node);
                    TreeViewAddTowers(node,line.TowerList);
                }

                var towerLit = DB_Tower.List(null);
                if (towerLit.Count == 0) return;
                TreeNode testNode = new TreeNode();
                testNode.Text = "测试单位";
                testNode.Name = "0";
                testNode.ToolTipText = string.Format("单位编号:无\n单位名称:测试单位");
                this.treeView_Nodes.Nodes.Add(testNode);
                TreeViewAddTowers(testNode,towerLit);


            }
            catch(Exception ex)
            {
                MessageBox.Show(this,
                    "杆塔列表初始化失败，错误信息为：" + ex.Message,
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1
                    );
            }
            
        }

        /// <summary>
        /// 节点选中变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_Nodes_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (curNode != null)
            {
                curNode.ForeColor = Color.Black;
                curNode.BackColor = Color.White;
            }
            curNode = e.Node;
            curNode.ForeColor = Color.White;
            curNode.BackColor = Color.DeepSkyBlue;

            if (curNode.Level == 1)
            {
               
                Tower tower = (Tower)curNode.Tag;
                foreach (var item in comboBox1.Items)
                {
                    ComboBoxItem comitem = (ComboBoxItem)item;
                    if ((int)comitem.Value == tower.LineID)
                    {
                        this.comboBox1.SelectedItem = item;
                    }
                }
                this.CurTower = tower;
                
               
            }
            if (curNode.Level == 0)
            {
                curNode.Expand();
                //if (curNode.Nodes.Count > 0)
                //    this.treeView_Nodes.SelectedNode = curNode.Nodes[0];
            }

        }

        private void LineListInit()
        {
            this.comboBox1.Items.Clear();
            var linelist = DB_Line.List();
            this.comboBox1.Items.Add(new ComboBoxItem("", 0));
            this.comboBox1.SelectedIndex = 0;

            foreach (Line line in linelist)
            {
                ComboBoxItem item = new ComboBoxItem(line.Name,line.NO);
                this.comboBox1.Items.Add(item);
                if (curTower!= null && curTower.LineID == line.NO)
                    this.comboBox1.SelectedItem = item;
            }
        }

        #region 杆塔装置操作
        /// <summary>
        /// 添加杆塔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox_T_TowerName.TextLength == 0)
                    return;
                //DB_Tower.NewTower(this.textBox_T_TowerName.Text,  //新建杆塔
                //                    "");
                int towerID = DB_Tower.Add(this.textBox_T_TowerName.Text,(int)((ComboBoxItem)this.comboBox1.SelectedItem).Value);
                CurTower = new Tower() { 
                    TowerNO = towerID,
                    TowerName=this.textBox_T_TowerName.Text,
                    LineID = (int)((ComboBoxItem)this.comboBox1.SelectedItem).Value,
                };

                GetTowerList();
                
            }
            catch (Exception ex)
            {
                //输出杆塔错误信息
                MessageBox.Show(this,
                    "杆塔添加失败，错误信息为：" + ex.Message,
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1
                    );
            }


        }
        /// <summary>
        /// 更新杆塔信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Update_Click(object sender, EventArgs e)
        {
            try
            {
                Tower tower = new Tower();
                tower.TowerName = this.textBox_T_TowerName.Text;
                tower.LineID = (int)((ComboBoxItem)this.comboBox1.SelectedItem).Value;
                DB_Tower.Update(curTower.TowerNO, tower);
                GetTowerList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                   "杆塔更新失败，错误信息为：" + ex.Message,
                   "错误",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error,
                   MessageBoxDefaultButton.Button1
                   );
            }

        }
        /// <summary>
        /// 删除杆塔信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Del_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "您确定要删除线路：" + curTower.TowerName + "  线路编号为：" + curTower.TowerNO + "  ?";
                var result = MessageBox.Show(this, str, "提示", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);
                if (result == System.Windows.Forms.DialogResult.No) return;
                DB_Tower.DelTower(curTower);
                this.CurTower = null;
                GetTowerList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "杆塔删除失败，错误信息为：" + ex.Message,
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1
                    );
            }
        }

        

        /// <summary>
        /// 禁止使用杆塔ID的所有按钮
        /// </summary>
        private void Disabled_TowerOP()
        {
            button_Add_Tower.Enabled = false;
            button_Del_Tower.Enabled = false;
            button_Update_Tower.Enabled = false;
        }
        /// <summary>
        /// 检查当前信息是否更改
        /// </summary>
        private void Check_TowerChanged()
        {
            if (this.curTower == null)  //当前没有选中任何杆塔
            {
                if (textBox_T_TowerName.TextLength != 0)
                    button_Add_Tower.Enabled = true;
                return;
            }
            //当前有杆塔被选中
            button_Del_Tower.Enabled = true;
            //杆塔ID长度出错
            //杆塔ID变化了
            //杆塔ID不变，检查名称是否变化
            if (this.textBox_T_TowerName.Text != curTower.TowerName || (int)((ComboBoxItem)this.comboBox1.SelectedItem).Value != curTower.LineID)
            {
                this.button_Add_Tower.Enabled = true;
                if (this.textBox_T_TowerName.TextLength > 0)
                    this.button_Update_Tower.Enabled = true;
            }
        }

        #endregion

        #region TextBoxTextChange
        /// <summary>
        /// TextBoxTextChange事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_T_TowerID_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Name == "textBox_T_TowerID")
            {
                TowerIDChanged();
            }
            if (textBox.Name == "textBox_T_TowerName")
            {
                Disabled_TowerOP();
                Check_TowerChanged();
            }

        }
        /// <summary>
        /// 杆塔ID变化更新对应的
        /// </summary>
        private void TowerIDChanged()
        {
          
            Disabled_TowerOP();
           
            Check_TowerChanged();
        }
        #endregion

    }
}
    