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
using GridBackGround.Forms.Tab;
using System.Windows;
using ResModel.nw;
using System.IO;
using System.Drawing.Imaging;

namespace GridBackGround.Forms.Dialogs_nw
{
    public partial class Tab_HisData_nw : Form
    {
        internal string CurDeviceID { get; set; }

        /// <summary>
        /// 当前功能代码
        /// </summary>
        internal nw_func_code func_Code { get; set; }

        public Tab_HisData_nw()
        {
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
            DateTime start = DateTime.Parse(DateTime.Now.ToShortDateString());
            this.dateTimePicker_StartTime.Value = start;
            this.dateTimePicker_EndTime.Value = start.Add(new TimeSpan(23,59,59));
            this.func_Code = nw_func_code.Pull;

            ///datagridview 图像参数配置
            this.dataGridView_image.CellFormatting += new DataGridViewCellFormattingEventHandler(DataGridView_image_CellFormatting);
            this.dataGridView_image.CellContentClick += DataGridView_image_CellContentClick;
        }

        // <summary>
        /// 数据检索区域添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
               Convert.ToInt32(e.RowBounds.Location.Y + (e.RowBounds.Height - dataGridView.RowHeadersDefaultCellStyle.Font.Size) / 2),
               dataGridView.RowHeadersWidth - 4,
               e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridView.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dataGridView.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.Right);
        }

        /// <summary>
        /// 图像格式化输出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void DataGridView_image_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //图像标签页datagridview 单元格format事件
            if (sender == this.dataGridView_image)
            {
                //图像列数据展示
                if (e != null && e.ColumnIndex == 3)
                {
                    DataGridViewCell cell = dataGridView_image.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Tag = e.Value;
                    cell.ToolTipText = e.Value.ToString();
                    string path = e.Value.ToString();
                    if (File.Exists(path) == false)
                    {
                        path = "Res\\logo.ico";
                        cell.ToolTipText += " (图片不存在)";
                    }
                    byte[] bytes = File.ReadAllBytes(path);
                    using (MemoryStream oldms = new MemoryStream(bytes))
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(oldms);
                        Bitmap bt = new Bitmap(img, new System.Drawing.Size(100, 100));
                        using (MemoryStream newms = new MemoryStream())
                        {
                            bt.Save(newms, ImageFormat.Jpeg);
                            e.Value = newms.ToArray();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// datagridview 单元格单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_image_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //图像标签页datagridview 单击事件
            if (sender == this.dataGridView_image)
            {
                //图片单击事件
                if (e != null && e.ColumnIndex == 3)
                {
                    //显示图片
                    DataGridViewCell cell = dataGridView_image.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string path = cell.Tag.ToString();
                    if (File.Exists(path) == false)
                    {
                        System.Windows.Forms.MessageBox.Show(string.Format("图片: {0} 不存在", path));
                        return;
                    }
                    try
                    {
                        System.Diagnostics.Process.Start(path);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("图片打开失败." + ex.Message);
                    }
                }
            }
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
                System.Windows.MessageBox.Show("起始时间应该小于结束时间");
                return;
            }
            SelectDataByTime(StartTime,EndTime);
            
        }
        private void button_DataHour_Click(object sender, EventArgs e)
        {
            SelectDataByTime(DateTime.Now.AddHours(-1), DateTime.Now);
        }

        private void button_DataDay_Click(object sender, EventArgs e)
        {
            SelectDataByTime(DateTime.Now.AddDays(-1), DateTime.Now);
        }


        void SelectDataByTime(DateTime start, DateTime end)
        {
            if(this.CurDeviceID ==null || this.CurDeviceID==string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("当前未选中任何设备");
                return;
            }
            try
            {
                switch (this.func_Code)
                {
                    case nw_func_code.Pull:
                        get_history_pull(start, end);
                        break;
                    case nw_func_code.Weather:
                        get_history_weather(start, end);
                        break;
                    case nw_func_code.Picture:
                        get_history_image(start,end);
                        break;
                    default:
                        System.Windows.Forms.MessageBox.Show("当前不支持该类型数据检索");
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "数据检索失败");
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tab_HisData_nw_Load(object sender, EventArgs e)
        {
            //获取装置列表变化事件
            MainForm parent = this.ParentForm as MainForm;
            Tab_IDs tab_IDs = parent.GetTabID();
            tab_IDs.CMD_ID_Change += Tab_IDs_CMD_ID_Change;

            this.tabPage_weather.Enter += TabPage_weather_Enter;
            this.tabPage_image.Enter += TabPage_image_Enter;
            this.tabPage_pull.Enter += TabPage_pull_Enter;
        }

        private void TabPage_pull_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "当前选中拉力数据";
            this.func_Code = nw_func_code.Pull;
        }

        private void TabPage_image_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "当前选中图像数据";
            this.func_Code = nw_func_code.Picture;
        }

        private void TabPage_weather_Enter(object sender, EventArgs e)
        {
           this.toolStripStatusLabel1.Text = "当前选中气象数据";
            this.func_Code = nw_func_code.Weather;
        }

        /// <summary>
        /// 选中设备ID变化事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Tab_IDs_CMD_ID_Change(object sender, CMDid_Change e)
        {
            if(e.CMD_ID != null && e.CMD_ID.Length >= 6)
            {
                this.CurDeviceID = e.CMD_ID;
                this.label_curdev.Text = e.CMD_NAME + ":" + this.CurDeviceID;
            }
        }

        private void get_history_weather(DateTime start,DateTime end)
        {
            db_data_nw_weather db_weather = new db_data_nw_weather();
            DataTable dataSet = db_weather.DataGet(this.CurDeviceID,start, end);
            this.dataGridView_weather.DataSource = dataSet;
        }

        private void get_history_pull(DateTime start, DateTime end)
        {
            db_data_nw_pull_angle db = new db_data_nw_pull_angle();
            DataTable dataSet = db.DataGet(this.CurDeviceID, start, end);
            this.dataGridView_pull.DataSource = dataSet;

        }
        private void get_history_image(DateTime start, DateTime end)
        {
            db_data_picture db = new db_data_picture();
            DataTable dt = db.DataGet(this.CurDeviceID, start, end);
            this.dataGridView_image.DataSource = dt;
        }
    }
}
