using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ResModel.PowerPole;
using cma.service.PowerPole;
using System.Security.Policy;

namespace GridBackGround
{
    public partial class Tab_Report : Form
    {
        private Forms.Tab.Tab_IDs tabid;
        private string cmdid;
        private bool DispalyAll = false;
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
                    this.cmdid = null;
                }
                else
                {
                    this.tabid.CMD_ID_Change += Tabid_CMD_ID_Change;
                }
            }
        }

        private void Tabid_CMD_ID_Change(object sender, CMDid_Change e)
        {
            this.cmdid = e.CMD_ID;
            this.label1.Text = e.CMD_NAME + ":"+this.cmdid;
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        public Tab_Report()
        {
            InitializeComponent();
            //this.IsMdiContainer = true;
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
            //接收数据解析事件
            //PacketAnaLysis.DisPacket.OnNewRecord += new PacketAnaLysis.NewRecord(DisNewPacked);
            //////新报文显示
            ////PacketAnaLysis.DisPacket.OnNewPacket += new PacketAnaLysis.NewPacket(UserListChanged);
            //////终端状态变化事件
            //Termination.PowerPoleManage.OnStateChange += new Termination.OnLineStateChange(PowerPoleManage_OnStateChange);
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
                if (this.DispalyAll)
                {
                    this.AddPackageRecords(e.Infos);
                    return;
                }
                List<PackageRecord> list = new List<PackageRecord>();
                foreach (PackageRecord info in e.Infos)
                {
                    if (info.EquName == this.cmdid)
                        list.Add(info);
                }
                this.AddPackageRecords(list);
            }
        }

        #region 添加报文解析

        private DataTable source;

        private void DataTable_init()
        {
            source = new DataTable();
            source.Columns.Add("时间",typeof(string));
            source.Columns.Add("装置ID", typeof(string));
            source.Columns.Add("命令", typeof(string));
            source.Columns.Add("数据", typeof(string));
            source.Columns.Add("状态", typeof(string));
            this.dataGridViewReport.Columns.Clear();
        }

        //public delegate void GridViewAddRow(PacketAnaLysis.DataInfo packet);
        /// <summary>
        /// 显示新解析数据
        /// </summary>
        /// <param name="packet"></param>
        public void AddPackageRecord(PackageRecord packet)
        {

            int index = this.dataGridViewReport.Rows.Add();

            dataGridViewReport.Rows[index].Cells[0].Value =packet.Time.ToString();
            try
            {
                string name = this.m_GetEquName(packet.EquName);
                dataGridViewReport.Rows[index].Cells[1].Value = name;// father.GetTowerNameByID(packet.EquName);
            }
            catch { }
            //MainForm father = (MainForm)this.Parent;

            dataGridViewReport.Rows[index].Cells[2].Value = packet.Command;
            if (packet.Command.Contains("照片合成"))
            {
                if (packet.Info.Contains("file:///"))
                {
                    int fileIndex = packet.Info.IndexOf("file:///");
                    dataGridViewReport.Rows[index].Cells[3].Tag = packet.Info.Substring(fileIndex, packet.Info.Length - fileIndex);
                    dataGridViewReport.Rows[index].Cells[3].Value = packet.Info.Substring(0, fileIndex) + "  点击查看"; ;
                }
                else
                {
                    dataGridViewReport.Rows[index].Cells[3].Value = packet.Info;
                }
            }
            else
                dataGridViewReport.Rows[index].Cells[3].Value = packet.Info;
            dataGridViewReport.Rows[index].Cells[4].Value = (packet.state == PackageRecord_RSType.rec) ? "接收" : "发送";
            //dataGridViewReport.CurrentCell = dataGridViewReport.Rows[index].Cells[0];
            //dataGridViewReport.Rows[index].Selected = true;

        }

        public void AddPackageRecords(List<PackageRecord> records)
        {
            foreach (PackageRecord record in records)
            {
                AddPackageRecord(record);
            }
            while (this.dataGridViewReport.Rows.Count > Config.SettingsForm.Default.DisReportNum)
                this.dataGridViewReport.Rows.RemoveAt(0);
            int index = this.dataGridViewReport.Rows.Count - 1;
            if (index < 0)
                return;
            dataGridViewReport.CurrentCell = dataGridViewReport.Rows[index].Cells[0];
            dataGridViewReport.Rows[index].Selected = true;
        }

        #endregion

        /// <summary>
        /// 清除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.dataGridViewReport.Rows.Clear();
            //source.Clear();
            //this.dataGridViewReport.DataSource = source;
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
            if (this.dataGridViewReport.CurrentCell == null)
            {
                this.richTextBox1.Text = "";
                return;
            }

            int index = this.dataGridViewReport.CurrentCell.RowIndex;
            try
            {
                if (dataGridViewReport.CurrentCell.Value != null)
                {
                    string text = dataGridViewReport.Rows[this.dataGridViewReport.CurrentCell.RowIndex].Cells[3].Value.ToString();
                    this.richTextBox1.Text = text;
                }
            }
            catch { }
        }

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
                System.Diagnostics.Process.Start(path);
            }
            catch(Exception ex)
            {
                MessageBox.Show("图片打开失败，失败原因:" + ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                DisPacket.OnNewPackageInfo += DisPacket_OnNewPackageInfo;
                this.checkBox2.Enabled = true;
            }
            else
            {
                DisPacket.OnNewPackageInfo -= DisPacket_OnNewPackageInfo;
                this.checkBox2.Enabled = false;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.DispalyAll = this.checkBox2.Checked;
        }
    }
}
