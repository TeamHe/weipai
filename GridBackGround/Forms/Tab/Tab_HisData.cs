using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResModel.EQU;
using Tools;
using DB_Operation.RealData;

namespace GridBackGround
{
    public partial class Tab_HisData : Form
    {
        private ICMP curType;

        public Tab_HisData()
        {
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
          
        }

        public void HisTimeRefresh()
        {
            this.dateTimePicker_StartTime.Value = DateTime.Now.AddHours(-5);
            this.dateTimePicker_EndTime.Value = DateTime.Now.AddSeconds(0 - DateTime.Now.Second);
        }

        // <summary>
        /// 数据检索区域添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
               Convert.ToInt32(e.RowBounds.Location.Y + (e.RowBounds.Height - dataGridView1.RowHeadersDefaultCellStyle.Font.Size) / 2),
               dataGridView1.RowHeadersWidth - 4,
               e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridView1.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.Right);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Select_Click(object sender, EventArgs e)
        {
            var StartTime = this.dateTimePicker_StartTime.Value;        //开始时间
            var EndTime = this.dateTimePicker_EndTime.Value;            //结束时间
            if (StartTime > EndTime)
            {
                MessageBox.Show("起始时间应该小于结束时间");
                return;
            }
            SelectDataByTime(StartTime,EndTime);
            
        }

        void SelectDataByTime(DateTime start, DateTime end)
        {

            string ID = this.comboBox_Name.SelectedItem.ToString();
            try
            {
                var item = (ComboBoxItem)this.comboBox_Type.SelectedItem;
                if (item.Value == null) return;
                Equ equ = (Equ)item.Value;
                IRealData_OP iRop = Real_Data_Op.Creat(equ.Type);
                if (iRop == null) throw new Exception("不支持的数据类型");
                var dt = iRop.GetData(equ, start, end);
                this.dataGridView1.DataSource = dt;
                curType = equ.Type;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取装置列表失败，失败原因：" + ex.Message);
            }
        }

        private void Tab_HisData_Load(object sender, EventArgs e)
        {
            InitTowerList();
            if(this.comboBox_Type.Items.Count > 0)
                this.comboBox_Type.SelectedIndex = 0;
            HisTimeRefresh();
        }

        /// <summary>
        /// 初始化装置列表
        /// </summary>
        private void InitTowerList()
        {
            //获取装置列表
            this.comboBox_Name.Items.Clear();
            try
            {
                var towerlist = DB_Operation.EQUManage.DB_Tower.List();
                foreach (Tower tower in towerlist)
                {
                    ComboBoxItem cbiItem = new ComboBoxItem(tower.TowerName,tower);
                    this.comboBox_Name.Items.Add(cbiItem);
                }
                if(this.comboBox_Name.Items.Count > 0)
                    this.comboBox_Name.SelectedIndex = 0;
            }
            catch { }
        }
        /// <summary>
        /// 杆塔列表切换，装置类型随之改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox_Type.Items.Clear();                                       //清除原有类型
            if (comboBox_Name.SelectedItem == null) return;                         //检查有无选中项目
            ComboBoxItem nameCBI = (ComboBoxItem)this.comboBox_Name.SelectedItem;   //获取当前选中杆塔
            if (nameCBI.Value == null) return;
            Tower tower = (Tower)nameCBI.Value;                     

            try
            {
                var equList = DB_Operation.EQUManage.DB_EQU.GetEquList(tower);      //获取该装置下的
                if (equList == null) return;
                foreach (Equ equ in equList)
                {
                    ComboBoxItem cbi = new ComboBoxItem(equ.Name,equ);
                    this.comboBox_Type.Items.Add(cbi);
                }
                if (this.comboBox_Type.Items.Count > 0)
                    this.comboBox_Type.SelectedIndex = 0;
            }
            catch{}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (curType != ICMP.Picture) return;
            //this.dataGridView1.d
            var gridView = (DataGridView)sender;
            if (e.ColumnIndex != 3) return;
            DataGridViewCell cell = gridView.Rows[e.RowIndex].Cells[e.ColumnIndex];


            string path = System.IO.Path.Combine(Config.SettingsForm.Default.PicturePath, cell.Value.ToString());
            if (path.Length == 0)
                return;
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("图片打开失败，失败原因:" + ex.Message);
            }
        }

        private void button_DataHour_Click(object sender, EventArgs e)
        {
            SelectDataByTime(DateTime.Now.AddHours(-1), DateTime.Now);
        }

        private void button_DataDay_Click(object sender, EventArgs e)
        {
            SelectDataByTime(DateTime.Now.AddDays(-1), DateTime.Now);
        }

    }
}
