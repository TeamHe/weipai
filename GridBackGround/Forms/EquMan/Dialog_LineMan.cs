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
            GetLineList();
        }
        #endregion
        

        /// <summary>
        /// 杆塔列表初始化
        /// </summary>
        /// <param name="tn_line"></param>
        private void GetLineList()
        {
            //杆塔节点生产
            try{
                this.treeView_Nodes.Nodes.Clear();
                var lineList = DB_Operation.EQUManage.DB_Line.List();
                if (lineList == null) return;
                foreach (Line line in lineList )                         //逐个添加装置信息
                {
                    TreeNode tn_line = new TreeNode();                     //生产杆塔节点
                    tn_line.Text = line.Name;
                    tn_line.Tag = line;
                    this.treeView_Nodes.Nodes.Add(tn_line);
                    if (curLine != null)
                    if (line.NO == curLine.NO)
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

            if (curNode != null)
            {
                curNode.ForeColor = Color.Black;
                curNode.BackColor = Color.White;
            }
            curNode = e.Node;
            curNode.ForeColor = Color.White;
            curNode.BackColor = Color.DeepSkyBlue;
            if (e.Node != null)
            {
                Line line = (Line)curNode.Tag;
                this.CurLine = line;
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
                DB_Line.Add(this.textBox_LineName.Text);  //新建杆塔
                GetLineList();
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
            try
            {
                
                string name = this.textBox_LineName.Text;
                if (name.Length == 0)
                {
                    MessageBox.Show("单位名称不能为空");
                }
                DB_Line.Update(curLine, name);
                GetLineList();
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
            try
            {
                string str = "您确定要删除单位：" + curLine.Name + "  ?";
                var result = MessageBox.Show(this, str, "提示", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);
                if (result == System.Windows.Forms.DialogResult.No) return;
                DB_Line.Delete(curLine);
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
            if (this.textBox_LineName.Text != curLine.Name)
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
            if (textBox.Name == "textBox_T_TowerID")
            {
                TowerIDChanged();
            }
            if (textBox.Name == "textBox_LineName")
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
    