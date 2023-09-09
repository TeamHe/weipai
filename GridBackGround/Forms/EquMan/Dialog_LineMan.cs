using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DB_Operation.EQUManage;
using ResModel.EQU;

namespace GridBackGround.Forms.EquMan
{

    public partial class Dialog_Line_Man: Form
    {
        #region Private Variables
        /// <summary>
        /// 当前选中的单位信息
        /// </summary>
        private Line curLine;
        /// <summary>
        /// 当前选中节点信息
        /// </summary>
        private TreeNode curNode;

        private TreeNode treenode_gw;

        private TreeNode treenode_nw;

        private DevFlag flag;
        #endregion
        

        #region  Public Varibales
        /// <summary>
        /// 当前装置信息
        /// </summary>
        public Line CurLine
        {
            get { return this.curLine; }
            set
            {
                this.curLine = value;
               
                if (value != null)
                {
                    this.textBox_LineName.Text = curLine.Name;
                }
                else
                {
                    this.textBox_LineName.Text = "";
                }
                Check_TowerChanged();
            }
        }
        #endregion

        #region Construction
        public Dialog_Line_Man()
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
            TreeNodesInit();
            GetLineList();
        }
        #endregion
        
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
                Tag = DevFlag.GW,
            };
            this.treeView_Nodes.Nodes.Add(this.treenode_gw);
            this.treeView_Nodes.Nodes.Add(this.treenode_nw);
        }

        private void TreeNodesClear()
        {
            this.treenode_gw.Nodes.Clear();
            this.treenode_nw.Nodes.Clear();
        }

        /// <summary>
        /// 杆塔列表初始化
        /// </summary>
        /// <param name="tn_line"></param>
        private void GetLineList()
        {
            this.TreeNodesClear();
            
            //杆塔节点生产
            try{
                List<Line> lines = new DB_Line().List();
                if (lines == null || lines.Count == 0)
                    return;

                foreach (Line line in lines)                         //逐个添加装置信息
                {
                    TreeNode tn_line = new TreeNode()
                    {
                        Text = line.Name,
                        Tag = line
                    };
                    switch (line.Flag)
                    {
                        case DevFlag.GW:
                            this.treenode_gw.Nodes.Add(tn_line);
                            break;
                        case DevFlag.NW:
                            this.treenode_nw.Nodes.Add(tn_line);
                            break;
                    }
                    if (this.CurLine == null || CurLine.NO != line.NO)
                        continue;
                    this.treeView_Nodes.SelectedNode = tn_line;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this,
                    "单位列表初始化失败，错误信息为：" + ex.Message,
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
            if(e.Node == null) 
                return;
            if (curNode != null)
            {
                curNode.ForeColor = Color.Black;
                curNode.BackColor = Color.White;
            }
            curNode = e.Node;
            curNode.ForeColor = Color.White;
            curNode.BackColor = Color.DeepSkyBlue;
            switch (curNode.Level)
            {
                case 0:             //单位类型层级
                    this.CurLine = null;
                    this.flag = (DevFlag)curNode.Tag;
                    break;
                case 1:             //单位名称层级
                    this.CurLine = (Line)curNode.Tag;
                    this.flag = this.CurLine.Flag;
                    break;
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
                if (this.textBox_LineName.TextLength == 0)
                    return;
                Line line = new Line()
                {
                    Name = this.textBox_LineName.Text,
                    Flag = this.flag,
                };

                new DB_Line().Add(line);  //新建杆塔
                GetLineList();
                MessageBox.Show(string.Format("单位“{0}”添加成功", line.Name));
            }
            catch (Exception ex)
            {
                //输出错误信息
                MessageBox.Show(this,
                    "单位添加失败，错误信息为：" + ex.Message,
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
            if(this.CurLine == null)
            {
                MessageBox.Show("当前没有选中单位");
                return;
            }
            if(this.textBox_LineName.Text == string.Empty)
            {
                MessageBox.Show("单位名称不能为空");
                return;
            }
            try
            {
                var oldline = CurLine;
                string old_name = CurLine.Name;
                new DB_Line().Update(CurLine, this.textBox_LineName.Text);
                GetLineList();
                MessageBox.Show(string.Format("单位 “{0}”更名为“{1}”成功",
                    oldline.Name, CurLine.Name));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                   "单位更新失败，错误信息为：" + ex.Message,
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
        private void button_Del_Click(object sender, EventArgs e)
        {
            if (CurLine == null)
            {
                MessageBox.Show("当前没有选中单位");
                return;
            }
            string str = "您确定要删除单位：" + CurLine.Name + "  ?";
            if (MessageBox.Show(this, str, "提示", MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question,
                     MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;
            try
            {
                new DB_Line().Delete(CurLine);
                this.CurLine = null;
                GetLineList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "单位删除失败，错误信息为：" + ex.Message,
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
            button_Add.Enabled = false;
            button_Delete.Enabled = false;
            button_Update.Enabled = false;
        }
        /// <summary>
        /// 检查当前信息是否更改
        /// </summary>
        private void Check_TowerChanged()
        {
            Disabled_TowerOP();
            if (this.curLine == null)  //当前没有选中任何杆塔
            {
                if (textBox_LineName.TextLength != 0)
                    button_Add.Enabled = true;
                return;
            }
            //当前有杆塔被选中
            button_Delete.Enabled = true;
            //杆塔ID长度出错
            //杆塔ID变化了
            //杆塔ID不变，检查名称是否变化
            if (this.textBox_LineName.Text != CurLine.Name)
            {
                this.button_Add.Enabled = true;
                if (this.textBox_LineName.TextLength > 0)
                    this.button_Update.Enabled = true;
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
            if (textBox == this.textBox_LineName)
            {
                Check_TowerChanged();
            }
        }
        #endregion

    }
}
    