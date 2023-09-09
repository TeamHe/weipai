using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Tools;
using ResModel.EQU;
using DB_Operation.EQUManage;

namespace GridBackGround.Forms.Tab
{
    public partial class Tab_OnlineStatus : Form
    {
        #region Private Functions
        private Timer timer;
        #endregion

        #region Public Variables
        /// <summary>
        /// 选中的单位ID
        /// </summary>
        public int SelectedDepartmentNo { get; set; }
        /// <summary>
        /// 选中的线路ID
        /// </summary>
        public int SelectedLineNo { get; set; }

        #endregion

        public Tab_OnlineStatus()
        {
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Load += new EventHandler(Tab_OnlineStatus_Load);

            timer = new Timer();
            timer.Interval = 1000 * 10;
            timer.Tick += new EventHandler(timer_Tick);

            this.comboBox_Department.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_Department.DropDown += new EventHandler(comboBox_DropDown);
            this.comboBox_Department.SelectionChangeCommitted += new EventHandler(comboBox_SelectionChangeCommitted);
            this.comboBox_Department.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);

            this.comboBox_Line.DropDownStyle = ComboBoxStyle.DropDownList;
            //this.comboBox_Line.DropDown += new EventHandler(comboBox_DropDown);
            this.comboBox_Line.SelectionChangeCommitted += new EventHandler(comboBox_SelectionChangeCommitted);
            this.comboBox_Line.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
            
            DataGridViewInit();
        }

        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tab_OnlineStatus_Load(object sender, EventArgs e)
        {
            
            timer.Start();

            this.LineInit();
            this.comboBox_Department.SelectedIndex = 0;
            this.TowerInit();
            this.comboBox_Department.SelectedIndex = 0;
        }

        
        #region 控件初始化

        private void DataGridViewInit()
        {
            this.dataGridView_Display.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView_Display.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataGridViewCellStyle headerSytle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            this.dataGridView_Display.ColumnHeadersDefaultCellStyle = headerSytle;
            DataGridViewColumn column = new DataGridViewTextBoxColumn() 
            { 
                Name = "DepartmentName",
                HeaderText = "单位",
            };
            this.dataGridView_Display.Columns.Clear();
            this.dataGridView_Display.Columns.Add(column);
            //this.dataGridView_Display.Columns.Add("DepartmentName", "单位");
            this.dataGridView_Display.Columns.Add("TowerName","线路");
            this.dataGridView_Display.Columns.Add("equName","设备名称");
            this.dataGridView_Display.Columns.Add("equID","设备ID");
            this.dataGridView_Display.Columns.Add("equCode","设备编码");
            this.dataGridView_Display.Columns.Add("phone", "设备手机号");
            this.dataGridView_Display.Columns.Add("equStatus","状态");
        }

        #endregion

        #region 控件事件处理
        /// <summary>
        /// 定时器定时事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            DataGridDisplay();
        }
        
        /// <summary>
        /// 下拉框选择内容变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            if (combobox == this.comboBox_Department)
            {
                Line line = (Line)((ComboBoxItem)combobox.SelectedItem).Value;
                SelectedDepartmentNo = line.NO;
                TowerInit();
            }

            if (combobox == this.comboBox_Line)
            {
                Tower tower = (Tower)((ComboBoxItem)combobox.SelectedItem).Value;
                SelectedLineNo = tower.TowerNO;
                DataGridDisplay();
            }
        }
        
        /// <summary>
        /// combox选中内容提交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
        }
        /// <summary>
        /// combox出现下拉框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comboBox_DropDown(object sender, EventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            if (combobox == comboBox_Department)
            {
                LineInit();
            }
            if (combobox == this.comboBox_Line)
            {
                TowerInit();
            }
        }
        #endregion

        

        #region Private Functions

        /// <summary>
        /// 页面内容初始化
        /// </summary>
        private void Init()
        { 
            
        }
        /// <summary>
        /// 线路初始化
        /// </summary>
        private void LineInit()
        {
            this.comboBox_Department.Items.Clear();

            Line allLine = new Line() { NO = 0,TowerList = new List<Tower>()};
            ComboBoxItem item = new ComboBoxItem()
            {
                Text = "全部",
                Value = allLine,
            };
            this.comboBox_Department.Items.Add(item);

            var lineList = new DB_Line().List_LineTower();
            foreach (Line line in lineList)
            {
                item = new ComboBoxItem()
                {
                    Text = line.Name,
                    Value = line,
                };
                this.comboBox_Department.Items.Add(item);

            }

        }
        /// <summary>
        /// 杆塔列表初始化
        /// </summary>
        private void TowerInit()
        {
            this.comboBox_Line.Items.Clear();

            Tower alltower = new Tower() { TowerNO = 0, EquList = new List<Equ>() };
            ComboBoxItem item = new ComboBoxItem()
            {
                Text = "全部",
                Value = alltower,
            };
            this.comboBox_Line.Items.Add(item);

            var towerList = DB_Tower.List(SelectedDepartmentNo);
            foreach (Tower tower in towerList)
            {
                item = new ComboBoxItem()
                {
                    Text = tower.TowerName,
                    Value = tower,
                };
                this.comboBox_Line.Items.Add(item);
            }
            this.comboBox_Line.SelectedIndex = 0;
        }



        /// <summary>
        /// 刷新显示选中线路的信息
        /// </summary>
        /// <param name="line"></param>
        /// <param name="tower"></param>
        private void DataGridDisplay()
        {
            this.dataGridView_Display.Rows.Clear();
            var linelist = new DB_Line().List_LineTowerEqu();
            linelist.Sort((x,y)=>x.Name.CompareTo(y.Name));
            foreach(Line line in linelist)
            {
                if(SelectedDepartmentNo == 0)
                    DisPlayLine(line);
                else
                    if(line.NO == SelectedDepartmentNo)
                        DisPlayLine(line);
            }

        }

        private void DisPlayLine(Line line)
        {
            line.TowerList.Sort((x,y)=>x.TowerName.CompareTo(y.TowerName));
            foreach(Tower tower in line.TowerList)
            {
                if(SelectedLineNo==0)
                    DisPlayTower(line.Name,tower);
                else
                    if(tower.TowerNO == SelectedLineNo)
                        DisPlayTower(line.Name,tower);
            }
        }

        private void DisPlayTower(string lineName,Tower tower)
        {
            tower.EquList.Sort((x,y)=>x.Name.CompareTo(y.Name));
            foreach (Equ equ in tower.EquList)
            {
                AddRow(lineName,tower.TowerName,equ);
            }
        }

        private void AddRow(string lineName,string towername,Equ equ)
        {
            int rowIndex = this.dataGridView_Display.Rows.Add();
            dataGridView_Display.Rows[rowIndex].Cells[0].Value = lineName;
            dataGridView_Display.Rows[rowIndex].Cells[1].Value = towername;
            dataGridView_Display.Rows[rowIndex].Cells[2].Value = equ.Name;
            dataGridView_Display.Rows[rowIndex].Cells[3].Value = equ.EquID;
            dataGridView_Display.Rows[rowIndex].Cells[4].Value = equ.EquNumber;
            dataGridView_Display.Rows[rowIndex].Cells[5].Value = equ.Phone;
            dataGridView_Display.Rows[rowIndex].Cells[6].Value = equ.Status.GetDescription();
        }

        #endregion


    }
}
