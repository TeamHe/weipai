using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ResModel.PowerPole;
using cma.service.PowerPole;
using System.Diagnostics;
using Tools;
using DB_Operation.RealData;
using System.Data;
using System.IO;
using System.Text;

namespace GridBackGround
{
    public partial class Tab_Report : Form
    {
        private Forms.Tab.Tab_IDs tabid;
        private string CurDeviceID;
        private bool History = false;
        /// <summary>
        /// 显示记录列表的数目
        /// </summary>
        private int ReportNum = Config.SettingsForm.Default.DisReportNum;

        public GetEquNameHandler m_GetEquName { get; set; }//

        public Forms.Tab.Tab_IDs TabID {
            get { return tabid; }
            set {
                if(this.tabid != null)
                {
                    this.tabid.CMD_ID_Change -= Tabid_CMD_ID_Change;
                }
                this.tabid = value;
                if(this.tabid == null)
                {
                    this.CurDeviceID = null;
                }
                else
                {
                    this.tabid.CMD_ID_Change += Tabid_CMD_ID_Change;
                }
            }
        }

        private void Tabid_CMD_ID_Change(object sender, CMDid_Change e)
        {
            if (this.CurDeviceID != e.CMD_ID &&
                this.checkBox_real_record.Checked &&
                ! this.checkBox_all.Checked )
            {
                ClearRecords();
            }
            this.CurDeviceID = e.CMD_ID;
            this.label1.Text = e.CMD_NAME + ":"+this.CurDeviceID;
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        public Tab_Report()
        {
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormRePort_Load(object sender, EventArgs e)
        {
            this.DataGridViewInit();
            this.dateTimePicker_start.Value = DateTime.Now.AddHours(-1);
        }

        /// <summary>
        /// DataGridView初始化
        /// </summary>
        public void DataGridViewInit()
        {
            this.dataGridViewReport.Columns[0].Width = 90;
            this.dataGridViewReport.Columns[1].Width = 90;
            this.dataGridViewReport.Columns[4].Width = 60;
            this.dataGridViewReport.Columns[2].Width = 100;
        }

        private void DisPacket_OnNewPackageInfo(object sender, PackageRecordsEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<PackageRecordsEventArgs>(
                    this.DisPacket_OnNewPackageInfo),
                    new object[] { sender, e });
            }
            else
            {
                if (e == null || e.Infos == null || e.Infos.Count == 0)
                    return;
                if (this.checkBox_all.Checked)
                {
                    this.AddPackageRecords(e.Infos);
                    return;
                }
                List<PackageRecord> list = new List<PackageRecord>();
                foreach (PackageRecord info in e.Infos)
                {
                    if (info.EquName == this.CurDeviceID)
                        list.Add(info);
                }
                this.AddPackageRecords(list);
            }
        }

        #region 添加报文解析
        /// <summary>
        /// 将PakageRecord 转换为 dataGridViewRow
        /// </summary>
        /// <param name="packet"></param>
        public DataGridViewRow PackageRecord_2_DataGridViewRow(PackageRecord packet,DataGridView  gridView)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(gridView); 
            row.Cells[0].Value = packet.Time.ToString();
            try
            {
                row.Cells[1].Value = this.m_GetEquName(packet.EquName);
            }catch (Exception) { }
            row.Cells[2].Value = packet.Command;
            string info = packet.Info;
            if (packet.Command.Contains("照片合成") && packet.Info.Contains("file:///"))
            {
                int fileIndex = packet.Info.IndexOf("file:///");
                string path = packet.Info.Substring(fileIndex, packet.Info.Length - fileIndex);
                info = packet.Info.Substring(0, fileIndex) + "  点击查看";
                row.Cells[3].Tag = path;
                row.Cells[3].ToolTipText = path;
            }
            row.Cells[3].Value = info;
            row.Cells[4].Value = EnumUtil.GetDescription(packet.state);
            row.Tag = packet.EquName;
            return row;
        }

        public void AddPackageRecords(List<PackageRecord> records)
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (PackageRecord record in records)
            {
                var row = PackageRecord_2_DataGridViewRow(record,this.dataGridViewReport);
                if(row != null)
                    rows.Add(row);  
            }
            this.dataGridViewReport.Rows.AddRange(rows.ToArray());

            while (this.dataGridViewReport.Rows.Count > Config.SettingsForm.Default.DisReportNum)
                this.dataGridViewReport.Rows.RemoveAt(0);
            int index = this.dataGridViewReport.Rows.Count - 1;
            if (index < 0)
                return;
            dataGridViewReport.CurrentCell = dataGridViewReport.Rows[index].Cells[0];
            dataGridViewReport.Rows[index].Selected = true;
        }

        private void ClearRecords()
        {
            this.dataGridViewReport.Rows.Clear();
            this.richTextBox1.Text = string.Empty;
        }

        /// <summary>
        /// 从数据库加载记录列表并显示
        /// </summary>
        /// <param name="cmdid">关联的设备ID</param>
        /// <param name="start">起始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="limits">最大数据量</param>
        /// <returns></returns>
        private void LoadHistoryRecords(string cmdid, DateTime start, DateTime end, int limits)
        {
            try
            {
                db_package_record db = new db_package_record();
                DataTable dt = db.DataGet(cmdid, start, end, limits);
                this.AddPackageRecords(db.GetPackageMessage_from_datatable(dt, cmdid));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load package messages failed." + ex.Message);
            }
        }
        private void LoadHistoryRecors(DateTime start, DateTime end, int limits = 0)
        {
            if (this.CurDeviceID == null)
            {
                MessageBox.Show("当前没有选中任何设备，请先选中设备");
                return;
            }
            this.checkBox_real_record.Checked = false;
            this.History = true;
            this.ClearRecords();
            LoadHistoryRecords(this.CurDeviceID, start, end, limits);
        }


        #endregion

        /// <summary>
        /// 清除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClearRecords();
        }

        /// <summary>
        /// 解析数据显示区域添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewReport_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                Convert.ToInt32(e.RowBounds.Location.Y + (e.RowBounds.Height - dataGridViewReport.RowHeadersDefaultCellStyle.Font.Size) / 2),
                dataGridViewReport.RowHeadersWidth - 4,
                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridViewReport.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dataGridViewReport.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.Right);
        }

        /// <summary>
        /// 数据解析区域选择变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewReport_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView view = sender as DataGridView;
            this.richTextBox1.Text = "";
            if (view.CurrentCell == null || view.CurrentCell.Value == null)
                return;

            int rowIndex= view.CurrentCell.RowIndex;
            try 
            {
                this.richTextBox1.Text = 
                    view.Rows[rowIndex].Cells[3].Value.ToString();
            }
            catch { }
        }

        /// <summary>
        /// dataGridView 单元格点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var gridView = (DataGridView)sender;
            if (e.RowIndex == -1) return;
            DataGridViewCell cell = gridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Tag == null)
                return;
            string path = cell.Tag.ToString();
            if (path.Length == 0)
                return;
            try
            {
                Process.Start(path);
            }
            catch(Exception ex)
            {
                MessageBox.Show("图片打开失败，失败原因:" + ex.Message);
            }
        }

        /// <summary>
        /// 实时记录checkbox 选中状态变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_realRecord_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_real_record.Checked)
            {
                DisPacket.OnNewPackageInfo += DisPacket_OnNewPackageInfo;
                this.checkBox_all.Enabled = true;
                this.History = false;
            }
            else
            {
                DisPacket.OnNewPackageInfo -= DisPacket_OnNewPackageInfo;
                this.checkBox_all.Enabled = false;
            }
        }

        private void button_his_custom_Click(object sender, EventArgs e)
        {
            DateTime start = this.dateTimePicker_start.Value;
            DateTime end = this.dateTimePicker_end.Value;
            if (start > end)
            {
                MessageBox.Show("起始时间不能大于结束时间");
                return;
            }
            LoadHistoryRecors(start, end);
        }

        private void button_his_hour_Click(object sender, EventArgs e)
        {
            LoadHistoryRecors(DateTime.Now.AddHours(-1), DateTime.Now);
        }

        private void button_his_day_Click(object sender, EventArgs e)
        {
            LoadHistoryRecors(DateTime.Now.AddDays(-1), DateTime.Now);
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Title = "保存为",
                Filter = "文本文件| *.txt",
                RestoreDirectory = true,
            };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            if (this.dataGridViewReport.Rows.Count == 0)
            {
                MessageBox.Show("没有通讯记录可以存储");
                return;
            }
            StreamWriter stream = null;
            try
            {
                stream = new StreamWriter(dialog.FileName);
                foreach(DataGridViewRow row in this.dataGridViewReport.Rows)
                {
                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("{0} ", row.Cells[0].Value); //DateTime
                    str.AppendFormat("<{0}> [{1}] ", row.Tag.ToString(), row.Cells[4].Value); //CMDID
                    str.AppendFormat("{0}----{1}", row.Cells[2].Value.ToString(),row.Cells[3].Value); //Command
                    stream.WriteLine(str.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("通讯记录保存失败," + ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
    }
}
