namespace GridBackGround
{
    partial class Tab_Report
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tab_Report));
            this.panel_RePort = new System.Windows.Forms.Panel();
            this.splitContainerReport = new System.Windows.Forms.SplitContainer();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Clear_Record = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_save = new System.Windows.Forms.Button();
            this.button_his_day = new System.Windows.Forms.Button();
            this.button_his_hour = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
            this.button_his_custom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_all = new System.Windows.Forms.CheckBox();
            this.checkBox_real_record = new System.Windows.Forms.CheckBox();
            this.panel_RePort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerReport)).BeginInit();
            this.splitContainerReport.Panel1.SuspendLayout();
            this.splitContainerReport.Panel2.SuspendLayout();
            this.splitContainerReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_RePort
            // 
            this.panel_RePort.Controls.Add(this.splitContainerReport);
            this.panel_RePort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_RePort.Location = new System.Drawing.Point(0, 59);
            this.panel_RePort.Name = "panel_RePort";
            this.panel_RePort.Size = new System.Drawing.Size(834, 329);
            this.panel_RePort.TabIndex = 2;
            // 
            // splitContainerReport
            // 
            this.splitContainerReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerReport.Location = new System.Drawing.Point(0, 0);
            this.splitContainerReport.Name = "splitContainerReport";
            this.splitContainerReport.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerReport.Panel1
            // 
            this.splitContainerReport.Panel1.Controls.Add(this.dataGridViewReport);
            this.splitContainerReport.Panel1.Controls.Add(this.bindingNavigator1);
            // 
            // splitContainerReport.Panel2
            // 
            this.splitContainerReport.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainerReport.Size = new System.Drawing.Size(834, 329);
            this.splitContainerReport.SplitterDistance = 261;
            this.splitContainerReport.TabIndex = 0;
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.AllowUserToAddRows = false;
            this.dataGridViewReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReport.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridViewReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.ID,
            this.Command,
            this.Data,
            this.State});
            this.dataGridViewReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewReport.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewReport.MultiSelect = false;
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.RowTemplate.Height = 23;
            this.dataGridViewReport.Size = new System.Drawing.Size(834, 234);
            this.dataGridViewReport.TabIndex = 1;
            this.dataGridViewReport.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReport_CellContentClick);
            this.dataGridViewReport.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewReport_RowPostPaint);
            this.dataGridViewReport.SelectionChanged += new System.EventHandler(this.dataGridViewReport_SelectionChanged);
            // 
            // Time
            // 
            this.Time.HeaderText = "时间";
            this.Time.Name = "Time";
            // 
            // ID
            // 
            this.ID.HeaderText = "装置ID";
            this.ID.Name = "ID";
            // 
            // Command
            // 
            this.Command.HeaderText = "命令";
            this.Command.Name = "Command";
            // 
            // Data
            // 
            this.Data.HeaderText = "数据";
            this.Data.Name = "Data";
            // 
            // State
            // 
            this.State.HeaderText = "状态";
            this.State.Name = "State";
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.toolStripButton_Clear_Record,
            this.toolStripSeparator1});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 234);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(834, 27);
            this.bindingNavigator1.TabIndex = 0;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(32, 24);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "当前位置";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton_Clear_Record
            // 
            this.toolStripButton_Clear_Record.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Clear_Record.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Clear_Record.Image")));
            this.toolStripButton_Clear_Record.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Clear_Record.Name = "toolStripButton_Clear_Record";
            this.toolStripButton_Clear_Record.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton_Clear_Record.ToolTipText = "清除所有记录";
            this.toolStripButton_Clear_Record.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(834, 64);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_save);
            this.panel1.Controls.Add(this.button_his_day);
            this.panel1.Controls.Add(this.button_his_hour);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dateTimePicker_end);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dateTimePicker_start);
            this.panel1.Controls.Add(this.button_his_custom);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.checkBox_all);
            this.panel1.Controls.Add(this.checkBox_real_record);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(834, 59);
            this.panel1.TabIndex = 2;
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(491, 28);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(81, 23);
            this.button_save.TabIndex = 18;
            this.button_save.Text = "保存到文件";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_his_day
            // 
            this.button_his_day.Location = new System.Drawing.Point(378, 29);
            this.button_his_day.Name = "button_his_day";
            this.button_his_day.Size = new System.Drawing.Size(107, 23);
            this.button_his_day.TabIndex = 17;
            this.button_his_day.Text = "最近一天记录";
            this.button_his_day.UseVisualStyleBackColor = true;
            this.button_his_day.Click += new System.EventHandler(this.button_his_day_Click);
            // 
            // button_his_hour
            // 
            this.button_his_hour.Location = new System.Drawing.Point(378, 4);
            this.button_his_hour.Name = "button_his_hour";
            this.button_his_hour.Size = new System.Drawing.Size(107, 23);
            this.button_his_hour.TabIndex = 16;
            this.button_his_hour.Text = "最近一小时记录";
            this.button_his_hour.UseVisualStyleBackColor = true;
            this.button_his_hour.Click += new System.EventHandler(this.button_his_hour_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "结束时间";
            // 
            // dateTimePicker_end
            // 
            this.dateTimePicker_end.CustomFormat = " yyyy-MM-dd HH:mm";
            this.dateTimePicker_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_end.Location = new System.Drawing.Point(147, 30);
            this.dateTimePicker_end.Name = "dateTimePicker_end";
            this.dateTimePicker_end.Size = new System.Drawing.Size(141, 21);
            this.dateTimePicker_end.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "开始时间";
            // 
            // dateTimePicker_start
            // 
            this.dateTimePicker_start.CustomFormat = " yyyy-MM-dd HH:mm";
            this.dateTimePicker_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_start.Location = new System.Drawing.Point(147, 3);
            this.dateTimePicker_start.Name = "dateTimePicker_start";
            this.dateTimePicker_start.Size = new System.Drawing.Size(141, 21);
            this.dateTimePicker_start.TabIndex = 12;
            // 
            // button_his_custom
            // 
            this.button_his_custom.Location = new System.Drawing.Point(294, 31);
            this.button_his_custom.Name = "button_his_custom";
            this.button_his_custom.Size = new System.Drawing.Size(75, 23);
            this.button_his_custom.TabIndex = 11;
            this.button_his_custom.Text = "查询";
            this.button_his_custom.UseVisualStyleBackColor = true;
            this.button_his_custom.Click += new System.EventHandler(this.button_his_custom_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(547, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "选中设备";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox_all
            // 
            this.checkBox_all.AutoSize = true;
            this.checkBox_all.Enabled = false;
            this.checkBox_all.Location = new System.Drawing.Point(10, 30);
            this.checkBox_all.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_all.Name = "checkBox_all";
            this.checkBox_all.Size = new System.Drawing.Size(72, 16);
            this.checkBox_all.TabIndex = 1;
            this.checkBox_all.Text = "显示全部";
            this.checkBox_all.UseVisualStyleBackColor = true;
            // 
            // checkBox_real_record
            // 
            this.checkBox_real_record.AutoSize = true;
            this.checkBox_real_record.Location = new System.Drawing.Point(10, 10);
            this.checkBox_real_record.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_real_record.Name = "checkBox_real_record";
            this.checkBox_real_record.Size = new System.Drawing.Size(72, 16);
            this.checkBox_real_record.TabIndex = 0;
            this.checkBox_real_record.Text = "实时记录";
            this.checkBox_real_record.UseVisualStyleBackColor = true;
            this.checkBox_real_record.CheckedChanged += new System.EventHandler(this.checkBox_realRecord_CheckedChanged);
            // 
            // Tab_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 388);
            this.Controls.Add(this.panel_RePort);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Tab_Report";
            this.Text = "FormRePort";
            this.Load += new System.EventHandler(this.FormRePort_Load);
            this.panel_RePort.ResumeLayout(false);
            this.splitContainerReport.Panel1.ResumeLayout(false);
            this.splitContainerReport.Panel1.PerformLayout();
            this.splitContainerReport.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerReport)).EndInit();
            this.splitContainerReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_RePort;
        private System.Windows.Forms.SplitContainer splitContainerReport;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Command;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_Clear_Record;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox_real_record;
        private System.Windows.Forms.CheckBox checkBox_all;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_his_day;
        private System.Windows.Forms.Button button_his_hour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker_end;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker_start;
        private System.Windows.Forms.Button button_his_custom;
    }
}