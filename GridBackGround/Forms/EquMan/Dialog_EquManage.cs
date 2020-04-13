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

namespace GridBackGround.Forms.Dialog
{

    public partial class Dialog_EquManage : Form
    {
        
        Dialog_Update_Tower Dialog_Tower;
        private Tower curTower;

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
                if (Dialog_Tower != null)
                    Dialog_Tower.CurTower = value;
                if (value != null)
                {
                    this.textBox_T_TowerID.Text = curTower.TowerID;
                    this.textBox_T_TowerName.Text = curTower.TowerName;
                }
                else
                {
                    this.textBox_T_TowerID.Text = "";
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
        public Dialog_EquManage()
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
            Dialog_Tower = new Dialog_Update_Tower();
            Dialog_Tower.TopLevel = false;
            Dialog_Tower.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Dialog_Tower.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(Dialog_Tower);
            Dialog_Tower.Show();
            GetTowerList();
        }
        #endregion
        

        /// <summary>
        /// 杆塔列表初始化
        /// </summary>
        /// <param name="tn_line"></param>
        private void GetTowerList()
        {
            //杆塔节点生产
            try{
                this.treeView_Nodes.Nodes.Clear();
                var towerList = DB_Operation.EQUManage.DB_Tower.GetTowerList();
                if (towerList == null) return;
                foreach (Tower tower in towerList )                         //逐个添加装置信息
                {
                    TreeNode tn_tower = new TreeNode();                     //生产杆塔节点
                    tn_tower.Text = tower.TowerName;
                    tn_tower.Name = tower.TowerID;
                    tn_tower.ToolTipText = "杆塔(被测装置)名称：" + tn_tower.Text +
                                           "\n杆塔(被测装置)ID：" + tn_tower.Name;
                    tn_tower.Tag = tower;
                    this.treeView_Nodes.Nodes.Add(tn_tower);
                }
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
            TreeNode tn = e.Node;
            Tower tower = (Tower)tn.Tag;
            //if(Dialog_Tower!=null)
            //    Dialog_Tower.CurTower = tower;
            this.CurTower = tower;
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
                //创建杆塔前验证
                if (this.label_TowerID_len.ForeColor != Color.Green)
                    return;
                if (this.textBox_T_TowerName.TextLength == 0)
                    return;
                DB_Tower.NewTower(this.textBox_T_TowerName.Text,  //新建杆塔
                                    this.textBox_T_TowerID.Text);
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
                tower.TowerID = this.textBox_T_TowerID.Text;
                tower.TowerName = this.textBox_T_TowerName.Text;
                DB_Tower.UpTower(curTower, tower);
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
        /// 更新杆塔信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Del_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "您确定要删除杆塔：" + curTower.TowerName + "  杆塔ID为：" + curTower.TowerID + "  ?"
                    + "\n该ID下所有设备将全部被删除";
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
                if (label_TowerID_len.ForeColor == Color.Green)
                    if (textBox_T_TowerName.TextLength != 0)
                        button_Add_Tower.Enabled = true;
                return;
            }
            //当前有杆塔被选中
            button_Del_Tower.Enabled = true;
            //杆塔ID长度出错
            if (label_TowerID_len.ForeColor != Color.Green)
                return;
            //杆塔ID变化了
            if (this.textBox_T_TowerID.Text != curTower.TowerID)
            {
                this.button_Add_Tower.Enabled = true;
                this.button_Update_Tower.Enabled = true;
                return;
            }
            //杆塔ID不变，检查名称是否变化
            if (this.textBox_T_TowerName.Text != curTower.TowerName)
            {
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
            int len = textBox_T_TowerID.TextLength;
            label_TowerID_len.Text = len.ToString();
            if (len == 0)
            {
                label_TowerID_len.Text = "";
            }
            label_TowerID_len.ForeColor = Color.Red;
            Disabled_TowerOP();
            if (len == 17)
            {
                label_TowerID_len.ForeColor = Color.Green;
            }
            Check_TowerChanged();
        }
        #endregion

    }
}
    