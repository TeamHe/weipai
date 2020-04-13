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
using ResModel.CollectData;
using System.Collections.Specialized;
using Tools;

namespace GridBackGround.Forms.Dialog
{
    /// <summary>
    /// 装置管理页面
    /// </summary>
    public partial class Dialog_Update_Tower : Form
    {
        #region Private Variable
        private Equ curEqu;
        private Tower curTower;
        #endregion
        
        #region Public Variable
        /// <summary>
        /// 当前装置所在杆塔信息
        /// </summary>
        public Tower CurTower
        {
            get { return this.curTower; }
            set
            {
                this.curTower = value;
                ClearEquMsg();
                if (curTower == null) return;
                this.textBox_CompID.Text = curTower.TowerID;
                GetEquList();
            }
        }
        /// <summary>
        /// 当前装置信息
        /// </summary>
        private Equ CurEQU
        {
            get { return curEqu; }
            set { 
                curEqu = value;
                if (value == null) 
                {
                    ClearEquMsg();
                    return; }
                this.textBox_CompID.Text = curTower.TowerID;
                this.textBox_StationID.Text = curEqu.EquID;
                this.textBox_OrgID.Text = curEqu.EquOrgID;
                this.textBox_T_StationName.Text = curEqu.Name;
               
                this.comboBox_EquType.SelectedIndex = (int)this.curEqu.Type - 1;
            }
        }
        #endregion

        #region Construction
        /// <summary>
        /// Construction
        /// </summary>
        public Dialog_Update_Tower()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Dialog Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_Update_Tower_Load(object sender, EventArgs ex)
        {
            this.textBox_CompID.Enabled = false;
            this.listView_T_Station.Columns.Add("序号", 50, HorizontalAlignment.Center);
            this.listView_T_Station.Columns.Add("装置名称", 120, HorizontalAlignment.Center);
            this.listView_T_Station.Columns.Add("装置ID", 120, HorizontalAlignment.Center);
            this.listView_T_Station.Columns.Add("装置类型", 120, HorizontalAlignment.Center);
            this.textBox_StationID.Tag = label_CmdLen;
            this.textBox_CompID.Tag = label_CompLen;
            this.textBox_OrgID.Tag = label_orgLen;

            Combox_Enum_Init();
            ClearEquMsg();
        }
        /// <summary>
        /// 装置类型Combox选框初始化
        /// </summary>
        private void Combox_Enum_Init()
        {
            this.comboBox_EquType.Items.Clear();

            ComboBoxItem cbxitem; //= new ComboBoxItem()

            //获取装置类型描述
            Dictionary<Int32, String> dic = EnumUtil.EnumToDictionary(typeof(ICMP), e => e.GetDescription());
            foreach (KeyValuePair<Int32, String> item in dic)
            {
                cbxitem = new ComboBoxItem(item.Value, item.Key);//将装置类型绑定到combox中
                this.comboBox_EquType.Items.Add(cbxitem);
            }
            this.comboBox_EquType.SelectedIndex = 0;
        }

        #endregion

        /// <summary>
        /// 获取杆塔下装置列表
        /// </summary>
        public void GetEquList()
        {
            try
            {
                this.listView_T_Station.Items.Clear();
                List<Equ> equlist =  DB_EQU.GetEquList(curTower.TowerNO);
                if (equlist == null) return;
                foreach (Equ equ in equlist)
                {
                    if (equ == null) return;
                    ListViewItem lvi = new ListViewItem((this.listView_T_Station.Items.Count+1).ToString());
                    lvi.Tag = equ;
                    lvi.SubItems.Add(equ.Name);
                    lvi.SubItems.Add(equ.EquID);
                    lvi.SubItems.Add(equ.Type.GetDescription());
                    this.listView_T_Station.Items.Add(lvi);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this,
                   "获取装置列表失败，错误信息为：" + ex.Message,
                   "错误",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error,
                   MessageBoxDefaultButton.Button1
                   );
            }
        }

        /// <summary>
        /// 选中的装置变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_T_Station_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearEquMsg();
            if (this.listView_T_Station.SelectedItems == null) return;
            if (listView_T_Station.SelectedItems.Count == 0) return;
            this.CurEQU = (Equ)listView_T_Station.SelectedItems[0].Tag;
        }
        /// <summary>
        /// 清除装置信息
        /// </summary>
        private void ClearEquMsg()
        {
            this.textBox_CompID.Text = "";
            this.textBox_OrgID.Text = "";
            this.textBox_StationID.Text = "";
            this.textBox_T_StationName.Text = "";
            this.comboBox_EquType.SelectedIndex = 0;
        }
        /// <summary>
        /// 禁止使用所有按钮
        /// </summary>
        private void DisabledButton()
        {
            this.button_Add.Enabled = false;
            this.button_Delete.Enabled = false;
            this.button_Update.Enabled = false;
        }
        /// <summary>
        /// 检查装置变化状态
        /// </summary>
        /// <returns></returns>
        private bool CheckChanged()
        {
            
            if (CurEQU != null)
            {
                this.button_Delete.Enabled = true;
            }
            
            if (this.label_CmdLen.ForeColor != Color.Green)     //装置ID检验
                return false;
            if (this.label_CompLen.ForeColor != Color.Green)     //装置被测ID长度检验
                return false;
            if (this.label_orgLen.ForeColor != Color.Green)     //装置原始ID检验
                return false;
            if (this.textBox_T_StationName.TextLength == 0)     //装置名称检验
                return false;
            //长度验证通过
            if (this.CurEQU == null)            //当前装置没有，装置操作只允许添加
            {
                this.button_Add.Enabled = true;
                return true;
            }
            ComboBoxItem cbi = (ComboBoxItem)this.comboBox_EquType.SelectedItem;
            var type = (ICMP)cbi.Value;
            if (type != this.CurEQU.Type)
            {
                this.button_Update.Enabled = true;
            }
            if (this.textBox_OrgID.Text != this.CurEQU.EquOrgID)//验证装置ID
            {
                this.button_Update.Enabled = true;
            }
            if (this.textBox_StationID.Text != this.CurEQU.EquID)//验证装置ID
            {
                this.button_Update.Enabled = true;
                this.button_Add.Enabled = true;
            }
            
            if (this.textBox_T_StationName.Text != this.CurEQU.Name)
            {
                this.button_Update.Enabled = true;
            }

            return true;
        }

        /// <summary>
        /// 装置ID长度变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_IDlengChanged(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox != this.textBox_T_StationName)
            {
                Label label = (Label)textbox.Tag;
                int len = textbox.TextLength;
                label.Text = len.ToString();
                if (len == 0) label.Text = "";
                label.ForeColor = Color.Red;
                if (len == 17) label.ForeColor = Color.Green;
                if (textbox == this.textBox_StationID)
                {
                    this.textBox_OrgID.Text = this.textBox_StationID.Text;
                }
            }
            DisabledButton();
            CheckChanged();
        }
        #region Station Operation
        /// <summary>
        /// 添加装置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Add_Click(object sender, EventArgs e)
        {
            try
            {
                var equ = GetEditedEqu();
                DB_EQU.New_EQU(equ);
                this.GetEquList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "装置添加失败,失败原因：" + ex.Message,
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }
        /// <summary>
        /// 更新装置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Update_Click(object sender, EventArgs e)
        {
            try
            {
                var equ = GetEditedEqu();
                DB_EQU.Up_Station(curEqu,equ);
                this.GetEquList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "装置更新失败,失败原因：" + ex.Message,
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }
        /// <summary>
        /// 删除装置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Del_Click(object sender, EventArgs e)
        {
            try
            {
                DB_EQU.Del_Station(curEqu);
                this.GetEquList();
                this.CurEQU = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "装置删除失败,失败原因：" + ex.Message,
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }
        /// <summary>
        /// 获取编辑后的装置
        /// </summary>
        /// <returns></returns>
        private Equ GetEditedEqu()
        {
            Equ equ = new Equ();
            equ.Name = this.textBox_T_StationName.Text;
            equ.EquID = this.textBox_StationID.Text;
            equ.EquOrgID = this.textBox_OrgID.Text;
            equ.TowerNO = curTower.TowerNO;
            ComboBoxItem cbi = (ComboBoxItem)this.comboBox_EquType.SelectedItem;
            equ.Type = (ICMP)cbi.Value;
            return equ;
        }
        #endregion

        private void comboBox_EquType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisabledButton();
            CheckChanged();
        }

        
 
    }
}
