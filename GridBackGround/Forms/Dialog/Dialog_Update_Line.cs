using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialog
{
    public partial class Dialog_Update_Line : Form
    {
        public Dialog_Update_Line()
        {
            InitializeComponent();
        }

        private void Dialog_Update_Line_Load(object sender, EventArgs e)
        {
            //ListView显示内容
            this.listView_Tower.Columns.Add("序号", 25, HorizontalAlignment.Center);
            this.listView_Tower.Columns.Add("名称", 60, HorizontalAlignment.Center);
            this.listView_Tower.Columns.Add("ID", 120, HorizontalAlignment.Center);
            
            CableInit();
        }
        /// <summary>
        /// 线缆初始化
        /// </summary>
        private void CableInit()
        {
            //var dt = null;//DB_Operation.DB_Cable.GetCableType();
            DataTable dt = null;
            if (dt == null) return;
            if (dt.Rows.Count == 0) return;
            this.comboBox_CableType.Items.Add("");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.comboBox_CableType.Items.Add(dt.Rows[i]["线缆类型"].ToString());
            }
            this.comboBox_CableType.SelectedIndex = 0;
        }
        /// <summary>
        /// 更新杆塔ListView
        /// </summary>
        private void Update_ListView_Tower()
        {
            this.listView_Tower.Items.Clear();
            //var dt = DB_Operation.DB_Tower.GetTowers(IDCurrentLine);
            DataTable dt = null;
            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem lvi = new ListViewItem((this.listView_Tower.Items.Count + 1).ToString());
                lvi.SubItems.Add(dr["杆塔名称"].ToString());
                lvi.SubItems.Add(dr["杆塔ID"].ToString());
                lvi.Tag = dr["ID"];
                this.listView_Tower.Items.Add(lvi);
            }

        }

        #region 线路信息管理
        /// <summary>
        /// 更新线路信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUp_Line_Click(object sender, EventArgs e)
        {
            //int m = DB_Operation.DB_Line.UpdateLine(IDCurrentLine,
            //    this.textBox_LineID.Text,
            //    this.textBox_LineName.Text);
            //if (m >= 1)                             //更新成功
            //{
            //    TreeViewInit();                     //更新站点列表
            //    foreach (TreeNode tn in this.treeView_Nodes.Nodes)      //恢复选择线路
            //        if (tn.Name == this.textBox_LineID.Text)
            //            this.treeView_Nodes.SelectedNode = tn;
            //    MessageBox.Show("更新成功！");
            //}
            //else                                    //更新失败
            //{
            //    this.textBox_LineName.Text = this.treeView_Nodes.SelectedNode.Text;
            //    MessageBox.Show("更新失败！");
            //}
        }

        /// <summary>
        /// 删除线路信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_DelLine_Click(object sender, EventArgs e)
        {
            ////执行删除线路操作
            //int m = DB_Operation.DB_Line.DelLine(this.textBox_LineID.Text);
            //if (m >= 1)
            //{
            //    TreeViewInit();
            //    this.textBox_LineID.Text = "";
            //    this.textBox_LineName.Text = "";
            //    MessageBox.Show("线路删除成功！");
            //}
            //else
            //{
            //    this.textBox_LineName.Text = this.treeView_Nodes.SelectedNode.Text;
            //    MessageBox.Show("线路删除失败,该线路不存在或已经被删除！");
            //}
        }

        #endregion
        #region 杆塔信息管理
        /// <summary>
        /// 添加杆塔信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Add_LTower_Click(object sender, EventArgs e)
        {
            //int m = -1;
            //string[] Towermsg = new string[4];
            //float[] values = new float[2];
            //if (GetTowerMsg(ref Towermsg, ref values))  //获取杆塔信息
            //{
            //    m = DB_Operation.DB_Tower.NewTower(Towermsg[0], Towermsg[1], IDCurrentLine, Towermsg[2], Towermsg[3], values[0], values[1]);
            //}
            //else
            //    return;
            //if (m >= 1)
            //{
            //    Update_ListView_Tower();
            //    if (this.listView_Tower.Items.Count > 0)
            //        this.listView_Tower.Items[0].Selected = true;
            //    MessageBox.Show("杆塔(被测装置)信息添加成功");
            //}
            //else
            //{
            //    MessageBox.Show("杆塔(被测装置)信息添加失败，请查看改ID是否已存在");
            //}
            //this.UpdateTreeView();
        }
        /// <summary>
        /// 更新杆塔信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Update_LTower_Click(object sender, EventArgs e)
        {
            //int m = -1;
            //string[] Towermsg = new string[4];
            //float[] values = new float[2];
            //if (this.listView_Tower.SelectedItems.Count == 0)
            //{
            //    MessageBox.Show("您没有选择被测设备");
            //    return;
            //}
            //int towerIndex = (int)this.listView_Tower.SelectedItems[0].Tag;

            //if (GetTowerMsg(ref Towermsg, ref values))
            //    m = DB_Operation.DB_Tower.UpTower(towerIndex, Towermsg[0], Towermsg[1], Towermsg[2], Towermsg[3], values[0], values[1]);
            //else
            //    return;
            //if (m >= 1)
            //{
            //    Update_ListView_Tower();
            //    this.listView_Tower.Items[0].Selected = true;
            //    MessageBox.Show("杆塔(被测装置)信息更新成功");
            //}
            //else
            //    MessageBox.Show("杆塔(被测装置)信息更新失败，请检查杆塔ID正确与否");
            //this.UpdateTreeView();
        }
        /// <summary>
        /// 删除杆塔信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Del_LTower_Click(object sender, EventArgs e)
        {
            //string TowerID = this.textBox_L_towerID.Text;
            //int m = DB_Operation.DB_Tower.DelTower(TowerID);
            //if (m >= 1)
            //{
            //    Update_ListView_Tower();
            //    MessageBox.Show("杆塔(被测装置)删除成功");
            //}
            //else
            //{
            //    MessageBox.Show("杆塔(被测装置)删除失败，请重新修改将要删除的杆塔ID");
            //}
        }

        #endregion

        /// <summary>
        /// 杆塔ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Tower_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.listView_Tower.SelectedItems.Count == 0)
            //{
            //    ClearTextbox();
            //    return;
            //}

            //int index = this.listView_Tower.SelectedItems[0].Index;            
            //int towerID = (int)this.listView_Tower.Items[index].Tag;
            //var dt = DB_Operation.DB_Tower.GetTowerEx(towerID);

            //if (dt == null) return;
            //if (dt.Rows.Count == 0) return;
            //this.textBox_L_towerID.Text = dt.Rows[0]["杆塔ID"].ToString();
            //this.textBox_L_TowerName.Text = dt.Rows[0]["杆塔名称"].ToString();
            //this.textBox_Radition.Text = dt.Rows[0]["表面辐射系数"].ToString();
            //this.textBox_ass.Text = dt.Rows[0]["表面吸收系数"].ToString();

            //SetCombox(this.comboBox_CableType, dt.Rows[0]["线缆类型"].ToString());
            //SetCombox(this.comboBox_CableSection, dt.Rows[0]["线缆标准截面"].ToString());
        }
        /// <summary>
        /// 线缆类型框选择变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_CableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.comboBox_CableSection.Items.Count > 0)
            //{
            //    this.comboBox_CableSection.Items.Clear();
            //    this.comboBox_CableSection.Items.Add("");
            //}
            //string type = this.comboBox_CableType.SelectedItem.ToString();
            //var dt = DB_Operation.DB_Cable.GetCableSection(type);
            //if (dt == null) return;
            //if (dt.Rows.Count == 0) return;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    this.comboBox_CableSection.Items.Add(dt.Rows[i]["线缆标准截面"].ToString());
            //}
        }
        /// <summary>
        /// 清空选项
        /// </summary>
        private void ClearTextbox()
        {
            //this.textBox_T_StationName.Text = "";
            //this.textBox_T_StationID.Text = "";
            this.textBox_L_towerID.Text = "";
            this.textBox_L_TowerName.Text = "";
            this.textBox_ass.Text = "";
            this.textBox_Radition.Text = "";
            this.comboBox_CableSection.Text = "";
            this.comboBox_CableSection.Text = "";
        }
    }
}
