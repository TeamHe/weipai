namespace GridBackGround
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.常规ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择通讯方式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示通讯报文ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示记录列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.记录列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存到excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.从Excel中导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.私有控制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户手机号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.主站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设备控制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.微风振动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.舞动曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.本机端口设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工作模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据库测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图片清理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.立即清理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Port = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_ID = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGPRS = new System.Windows.Forms.TabPage();
            this.tabPageReport = new System.Windows.Forms.TabPage();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.tabPage_nw_history = new System.Windows.Forms.TabPage();
            this.tabPageOnLineStatus = new System.Windows.Forms.TabPage();
            this.panel = new System.Windows.Forms.Panel();
            this.panel_SerialPort = new System.Windows.Forms.Panel();
            this.panel_TabControl = new System.Windows.Forms.Panel();
            this.splitter_Bottom = new System.Windows.Forms.Splitter();
            this.splitter_Left = new System.Windows.Forms.Splitter();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.panel_Left = new System.Windows.Forms.Panel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示主界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出程序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel.SuspendLayout();
            this.panel_TabControl.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.常规ToolStripMenuItem,
            this.记录列表ToolStripMenuItem,
            this.私有控制ToolStripMenuItem,
            this.主站ToolStripMenuItem,
            this.设备控制ToolStripMenuItem,
            this.数据曲线ToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1059, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 常规ToolStripMenuItem
            // 
            this.常规ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择通讯方式ToolStripMenuItem,
            this.显示通讯报文ToolStripMenuItem,
            this.显示记录列表ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.常规ToolStripMenuItem.Name = "常规ToolStripMenuItem";
            this.常规ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.常规ToolStripMenuItem.Text = "常规";
            // 
            // 选择通讯方式ToolStripMenuItem
            // 
            this.选择通讯方式ToolStripMenuItem.Name = "选择通讯方式ToolStripMenuItem";
            this.选择通讯方式ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.选择通讯方式ToolStripMenuItem.Text = "选择通讯方式";
            this.选择通讯方式ToolStripMenuItem.Visible = false;
            // 
            // 显示通讯报文ToolStripMenuItem
            // 
            this.显示通讯报文ToolStripMenuItem.Name = "显示通讯报文ToolStripMenuItem";
            this.显示通讯报文ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示通讯报文ToolStripMenuItem.Text = "显示通讯报文";
            this.显示通讯报文ToolStripMenuItem.Click += new System.EventHandler(this.显示通讯报文ToolStripMenuItem_Click);
            // 
            // 显示记录列表ToolStripMenuItem
            // 
            this.显示记录列表ToolStripMenuItem.Name = "显示记录列表ToolStripMenuItem";
            this.显示记录列表ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示记录列表ToolStripMenuItem.Text = "显示记录列表";
            this.显示记录列表ToolStripMenuItem.Click += new System.EventHandler(this.显示记录列表ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // 记录列表ToolStripMenuItem
            // 
            this.记录列表ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存到excelToolStripMenuItem,
            this.从Excel中导入ToolStripMenuItem});
            this.记录列表ToolStripMenuItem.Name = "记录列表ToolStripMenuItem";
            this.记录列表ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.记录列表ToolStripMenuItem.Text = "记录列表";
            this.记录列表ToolStripMenuItem.Visible = false;
            this.记录列表ToolStripMenuItem.Click += new System.EventHandler(this.记录列表ToolStripMenuItem_Click);
            // 
            // 保存到excelToolStripMenuItem
            // 
            this.保存到excelToolStripMenuItem.Name = "保存到excelToolStripMenuItem";
            this.保存到excelToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.保存到excelToolStripMenuItem.Text = "保存到excel";
            // 
            // 从Excel中导入ToolStripMenuItem
            // 
            this.从Excel中导入ToolStripMenuItem.Name = "从Excel中导入ToolStripMenuItem";
            this.从Excel中导入ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.从Excel中导入ToolStripMenuItem.Text = "从Excel中导入";
            // 
            // 私有控制ToolStripMenuItem
            // 
            this.私有控制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.用户手机号ToolStripMenuItem});
            this.私有控制ToolStripMenuItem.Name = "私有控制ToolStripMenuItem";
            this.私有控制ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.私有控制ToolStripMenuItem.Text = "私有控制";
            // 
            // 用户手机号ToolStripMenuItem
            // 
            this.用户手机号ToolStripMenuItem.Name = "用户手机号ToolStripMenuItem";
            this.用户手机号ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.用户手机号ToolStripMenuItem.Text = "用户手机号";
            // 
            // 主站ToolStripMenuItem
            // 
            this.主站ToolStripMenuItem.Name = "主站ToolStripMenuItem";
            this.主站ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.主站ToolStripMenuItem.Text = "设备控制";
            // 
            // 设备控制ToolStripMenuItem
            // 
            this.设备控制ToolStripMenuItem.Name = "设备控制ToolStripMenuItem";
            this.设备控制ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.设备控制ToolStripMenuItem.Text = "设备控制";
            // 
            // 数据曲线ToolStripMenuItem
            // 
            this.数据曲线ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.微风振动ToolStripMenuItem,
            this.舞动曲线ToolStripMenuItem});
            this.数据曲线ToolStripMenuItem.Name = "数据曲线ToolStripMenuItem";
            this.数据曲线ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.数据曲线ToolStripMenuItem.Text = "数据曲线";
            this.数据曲线ToolStripMenuItem.Visible = false;
            // 
            // 微风振动ToolStripMenuItem
            // 
            this.微风振动ToolStripMenuItem.Name = "微风振动ToolStripMenuItem";
            this.微风振动ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.微风振动ToolStripMenuItem.Text = "微风振动曲线";
            this.微风振动ToolStripMenuItem.Click += new System.EventHandler(this.微风振动ToolStripMenuItem_Click);
            // 
            // 舞动曲线ToolStripMenuItem
            // 
            this.舞动曲线ToolStripMenuItem.Name = "舞动曲线ToolStripMenuItem";
            this.舞动曲线ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.舞动曲线ToolStripMenuItem.Text = "舞动曲线";
            this.舞动曲线ToolStripMenuItem.Click += new System.EventHandler(this.舞动曲线ToolStripMenuItem_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.本机端口设置ToolStripMenuItem,
            this.工作模式ToolStripMenuItem,
            this.数据库测试ToolStripMenuItem,
            this.图片清理ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 本机端口设置ToolStripMenuItem
            // 
            this.本机端口设置ToolStripMenuItem.Name = "本机端口设置ToolStripMenuItem";
            this.本机端口设置ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.本机端口设置ToolStripMenuItem.Text = "本机端口设置";
            this.本机端口设置ToolStripMenuItem.Click += new System.EventHandler(this.本机端口设置ToolStripMenuItem_Click);
            // 
            // 工作模式ToolStripMenuItem
            // 
            this.工作模式ToolStripMenuItem.Name = "工作模式ToolStripMenuItem";
            this.工作模式ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.工作模式ToolStripMenuItem.Text = "串口模式";
            this.工作模式ToolStripMenuItem.Click += new System.EventHandler(this.打开串口调试工具ToolStripMenuItem_Click);
            // 
            // 数据库测试ToolStripMenuItem
            // 
            this.数据库测试ToolStripMenuItem.Name = "数据库测试ToolStripMenuItem";
            this.数据库测试ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.数据库测试ToolStripMenuItem.Text = "数据库测试";
            this.数据库测试ToolStripMenuItem.Click += new System.EventHandler(this.数据库测试ToolStripMenuItem_Click);
            // 
            // 图片清理ToolStripMenuItem
            // 
            this.图片清理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.立即清理ToolStripMenuItem,
            this.清理ToolStripMenuItem,
            this.参数设置ToolStripMenuItem});
            this.图片清理ToolStripMenuItem.Name = "图片清理ToolStripMenuItem";
            this.图片清理ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.图片清理ToolStripMenuItem.Text = "图片清理";
            this.图片清理ToolStripMenuItem.Visible = false;
            // 
            // 立即清理ToolStripMenuItem
            // 
            this.立即清理ToolStripMenuItem.Name = "立即清理ToolStripMenuItem";
            this.立即清理ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.立即清理ToolStripMenuItem.Text = "立即清理";
            this.立即清理ToolStripMenuItem.Click += new System.EventHandler(this.立即清理ToolStripMenuItem_Click);
            // 
            // 清理ToolStripMenuItem
            // 
            this.清理ToolStripMenuItem.Name = "清理ToolStripMenuItem";
            this.清理ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.清理ToolStripMenuItem.Text = "清理全部";
            this.清理ToolStripMenuItem.Click += new System.EventHandler(this.清理ToolStripMenuItem_Click);
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            this.参数设置ToolStripMenuItem.Click += new System.EventHandler(this.参数设置ToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Port,
            this.toolStripStatusLabel_ID});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 491);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(1059, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "状态栏";
            // 
            // toolStripStatusLabel_Port
            // 
            this.toolStripStatusLabel_Port.AutoSize = false;
            this.toolStripStatusLabel_Port.Name = "toolStripStatusLabel_Port";
            this.toolStripStatusLabel_Port.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel_Port.Spring = true;
            this.toolStripStatusLabel_Port.Text = "端口状态";
            this.toolStripStatusLabel_Port.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel_ID
            // 
            this.toolStripStatusLabel_ID.AutoSize = false;
            this.toolStripStatusLabel_ID.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabel_ID.Name = "toolStripStatusLabel_ID";
            this.toolStripStatusLabel_ID.Size = new System.Drawing.Size(4, 17);
            this.toolStripStatusLabel_ID.Spring = true;
            this.toolStripStatusLabel_ID.Text = "状态";
            this.toolStripStatusLabel_ID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGPRS);
            this.tabControl1.Controls.Add(this.tabPageReport);
            this.tabControl1.Controls.Add(this.tabPageData);
            this.tabControl1.Controls.Add(this.tabPage_nw_history);
            this.tabControl1.Controls.Add(this.tabPageOnLineStatus);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(878, 453);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageGPRS
            // 
            this.tabPageGPRS.Location = new System.Drawing.Point(4, 22);
            this.tabPageGPRS.Name = "tabPageGPRS";
            this.tabPageGPRS.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGPRS.Size = new System.Drawing.Size(870, 427);
            this.tabPageGPRS.TabIndex = 0;
            this.tabPageGPRS.Text = "通信报文";
            this.tabPageGPRS.UseVisualStyleBackColor = true;
            // 
            // tabPageReport
            // 
            this.tabPageReport.Location = new System.Drawing.Point(4, 22);
            this.tabPageReport.Name = "tabPageReport";
            this.tabPageReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReport.Size = new System.Drawing.Size(870, 427);
            this.tabPageReport.TabIndex = 1;
            this.tabPageReport.Text = "记录列表";
            this.tabPageReport.UseVisualStyleBackColor = true;
            // 
            // tabPageData
            // 
            this.tabPageData.Location = new System.Drawing.Point(4, 22);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Size = new System.Drawing.Size(870, 427);
            this.tabPageData.TabIndex = 2;
            this.tabPageData.Text = "国网历史数据";
            this.tabPageData.UseVisualStyleBackColor = true;
            // 
            // tabPage_nw_history
            // 
            this.tabPage_nw_history.Location = new System.Drawing.Point(4, 22);
            this.tabPage_nw_history.Name = "tabPage_nw_history";
            this.tabPage_nw_history.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_nw_history.Size = new System.Drawing.Size(870, 427);
            this.tabPage_nw_history.TabIndex = 4;
            this.tabPage_nw_history.Text = "南网历史数据";
            this.tabPage_nw_history.UseVisualStyleBackColor = true;
            // 
            // tabPageOnLineStatus
            // 
            this.tabPageOnLineStatus.Location = new System.Drawing.Point(4, 22);
            this.tabPageOnLineStatus.Name = "tabPageOnLineStatus";
            this.tabPageOnLineStatus.Size = new System.Drawing.Size(870, 427);
            this.tabPageOnLineStatus.TabIndex = 3;
            this.tabPageOnLineStatus.Text = "在线状态";
            this.tabPageOnLineStatus.UseVisualStyleBackColor = true;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.panel_SerialPort);
            this.panel.Controls.Add(this.panel_TabControl);
            this.panel.Controls.Add(this.splitter_Bottom);
            this.panel.Controls.Add(this.splitter_Left);
            this.panel.Controls.Add(this.panel_Bottom);
            this.panel.Controls.Add(this.panel_Left);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 25);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1059, 466);
            this.panel.TabIndex = 6;
            // 
            // panel_SerialPort
            // 
            this.panel_SerialPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_SerialPort.Location = new System.Drawing.Point(181, 0);
            this.panel_SerialPort.Name = "panel_SerialPort";
            this.panel_SerialPort.Size = new System.Drawing.Size(878, 453);
            this.panel_SerialPort.TabIndex = 0;
            this.panel_SerialPort.Visible = false;
            // 
            // panel_TabControl
            // 
            this.panel_TabControl.Controls.Add(this.tabControl1);
            this.panel_TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_TabControl.Location = new System.Drawing.Point(181, 0);
            this.panel_TabControl.Name = "panel_TabControl";
            this.panel_TabControl.Size = new System.Drawing.Size(878, 453);
            this.panel_TabControl.TabIndex = 10;
            // 
            // splitter_Bottom
            // 
            this.splitter_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter_Bottom.Location = new System.Drawing.Point(181, 453);
            this.splitter_Bottom.Name = "splitter_Bottom";
            this.splitter_Bottom.Size = new System.Drawing.Size(878, 3);
            this.splitter_Bottom.TabIndex = 9;
            this.splitter_Bottom.TabStop = false;
            // 
            // splitter_Left
            // 
            this.splitter_Left.Location = new System.Drawing.Point(178, 0);
            this.splitter_Left.Name = "splitter_Left";
            this.splitter_Left.Size = new System.Drawing.Size(3, 456);
            this.splitter_Left.TabIndex = 8;
            this.splitter_Left.TabStop = false;
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Bottom.Location = new System.Drawing.Point(178, 456);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(881, 10);
            this.panel_Bottom.TabIndex = 7;
            // 
            // panel_Left
            // 
            this.panel_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Left.Location = new System.Drawing.Point(0, 0);
            this.panel_Left.Name = "panel_Left";
            this.panel_Left.Size = new System.Drawing.Size(178, 466);
            this.panel_Left.TabIndex = 6;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "微拍后台";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示主界面ToolStripMenuItem,
            this.退出程序ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            // 
            // 显示主界面ToolStripMenuItem
            // 
            this.显示主界面ToolStripMenuItem.Name = "显示主界面ToolStripMenuItem";
            this.显示主界面ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.显示主界面ToolStripMenuItem.Text = "显示主界面";
            this.显示主界面ToolStripMenuItem.Click += new System.EventHandler(this.显示主界面ToolStripMenuItem_Click);
            // 
            // 退出程序ToolStripMenuItem
            // 
            this.退出程序ToolStripMenuItem.Name = "退出程序ToolStripMenuItem";
            this.退出程序ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.退出程序ToolStripMenuItem.Text = "退出程序";
            this.退出程序ToolStripMenuItem.Click += new System.EventHandler(this.退出程序ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 513);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "输电线路状态监测代理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel_TabControl.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem 常规ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择通讯方式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示通讯报文ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示记录列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 记录列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 主站ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGPRS;
        private System.Windows.Forms.TabPage tabPageReport;
        private System.Windows.Forms.ToolStripMenuItem 保存到excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 从Excel中导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 本机端口设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Port;
        private System.Windows.Forms.ToolStripMenuItem 数据曲线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 微风振动ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageData;
        private System.Windows.Forms.ToolStripMenuItem 工作模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ID;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Panel panel_Left;
        private System.Windows.Forms.Panel panel_TabControl;
        private System.Windows.Forms.Splitter splitter_Bottom;
        private System.Windows.Forms.Splitter splitter_Left;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel panel_SerialPort;


        private FormSerialPort Form_SerialPort;
        private Forms.Tab.Tab_IDs TabID;
        Tab_Packet TabPacket;
        Tab_Report TabReport;
        Tab_HisData TabHisData;
        Forms.Tab.Tab_OnlineStatus TabOnLineStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示主界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出程序ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 舞动曲线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据库测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 私有控制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户手机号ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageOnLineStatus;
        private System.Windows.Forms.ToolStripMenuItem 图片清理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 立即清理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设备控制ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage_nw_history;
    }
}

