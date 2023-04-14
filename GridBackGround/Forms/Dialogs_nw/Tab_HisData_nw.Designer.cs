namespace GridBackGround.Forms.Dialogs_nw
{
    partial class Tab_HisData_nw
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl_weather = new System.Windows.Forms.TabControl();
            this.tabPage_pull = new System.Windows.Forms.TabPage();
            this.dataGridView_pull = new System.Windows.Forms.DataGridView();
            this.tabPage_weather = new System.Windows.Forms.TabPage();
            this.dataGridView_weather = new System.Windows.Forms.DataGridView();
            this.tabPage_image = new System.Windows.Forms.TabPage();
            this.dataGridView_image = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_curdev = new System.Windows.Forms.Label();
            this.dateTimePicker_EndTime = new System.Windows.Forms.DateTimePicker();
            this.button_DataDay = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button_DataHour = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button_Select = new System.Windows.Forms.Button();
            this.dateTimePicker_StartTime = new System.Windows.Forms.DateTimePicker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.tabControl_weather.SuspendLayout();
            this.tabPage_pull.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_pull)).BeginInit();
            this.tabPage_weather.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_weather)).BeginInit();
            this.tabPage_image.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_image)).BeginInit();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tabControl_weather);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 475);
            this.panel1.TabIndex = 1;
            // 
            // tabControl_weather
            // 
            this.tabControl_weather.Controls.Add(this.tabPage_pull);
            this.tabControl_weather.Controls.Add(this.tabPage_weather);
            this.tabControl_weather.Controls.Add(this.tabPage_image);
            this.tabControl_weather.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_weather.Location = new System.Drawing.Point(0, 68);
            this.tabControl_weather.Name = "tabControl_weather";
            this.tabControl_weather.SelectedIndex = 0;
            this.tabControl_weather.Size = new System.Drawing.Size(783, 407);
            this.tabControl_weather.TabIndex = 15;
            // 
            // tabPage_pull
            // 
            this.tabPage_pull.Controls.Add(this.dataGridView_pull);
            this.tabPage_pull.Location = new System.Drawing.Point(4, 22);
            this.tabPage_pull.Name = "tabPage_pull";
            this.tabPage_pull.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_pull.Size = new System.Drawing.Size(775, 381);
            this.tabPage_pull.TabIndex = 0;
            this.tabPage_pull.Text = "拉力数据";
            this.tabPage_pull.UseVisualStyleBackColor = true;
            // 
            // dataGridView_pull
            // 
            this.dataGridView_pull.AllowUserToAddRows = false;
            this.dataGridView_pull.AllowUserToDeleteRows = false;
            this.dataGridView_pull.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_pull.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_pull.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_pull.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_pull.Name = "dataGridView_pull";
            this.dataGridView_pull.ReadOnly = true;
            this.dataGridView_pull.RowTemplate.Height = 23;
            this.dataGridView_pull.Size = new System.Drawing.Size(769, 375);
            this.dataGridView_pull.TabIndex = 1;
            this.dataGridView_pull.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
            // 
            // tabPage_weather
            // 
            this.tabPage_weather.Controls.Add(this.dataGridView_weather);
            this.tabPage_weather.Location = new System.Drawing.Point(4, 22);
            this.tabPage_weather.Name = "tabPage_weather";
            this.tabPage_weather.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_weather.Size = new System.Drawing.Size(775, 381);
            this.tabPage_weather.TabIndex = 1;
            this.tabPage_weather.Text = "气象数据";
            this.tabPage_weather.UseVisualStyleBackColor = true;
            // 
            // dataGridView_weather
            // 
            this.dataGridView_weather.AllowUserToAddRows = false;
            this.dataGridView_weather.AllowUserToDeleteRows = false;
            this.dataGridView_weather.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_weather.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_weather.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_weather.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_weather.Name = "dataGridView_weather";
            this.dataGridView_weather.ReadOnly = true;
            this.dataGridView_weather.RowTemplate.Height = 23;
            this.dataGridView_weather.Size = new System.Drawing.Size(769, 375);
            this.dataGridView_weather.TabIndex = 0;
            this.dataGridView_weather.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
            // 
            // tabPage_image
            // 
            this.tabPage_image.Controls.Add(this.dataGridView_image);
            this.tabPage_image.Location = new System.Drawing.Point(4, 22);
            this.tabPage_image.Name = "tabPage_image";
            this.tabPage_image.Size = new System.Drawing.Size(775, 381);
            this.tabPage_image.TabIndex = 2;
            this.tabPage_image.Text = "图像数据";
            this.tabPage_image.UseVisualStyleBackColor = true;
            // 
            // dataGridView_image
            // 
            this.dataGridView_image.AllowUserToAddRows = false;
            this.dataGridView_image.AllowUserToDeleteRows = false;
            this.dataGridView_image.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_image.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_image.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_image.Name = "dataGridView_image";
            this.dataGridView_image.ReadOnly = true;
            this.dataGridView_image.RowTemplate.Height = 23;
            this.dataGridView_image.Size = new System.Drawing.Size(775, 381);
            this.dataGridView_image.TabIndex = 0;
            this.dataGridView_image.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.label_curdev);
            this.panel2.Controls.Add(this.dateTimePicker_EndTime);
            this.panel2.Controls.Add(this.button_DataDay);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.button_DataHour);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.button_Select);
            this.panel2.Controls.Add(this.dateTimePicker_StartTime);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(783, 68);
            this.panel2.TabIndex = 14;
            // 
            // label_curdev
            // 
            this.label_curdev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_curdev.Location = new System.Drawing.Point(235, 8);
            this.label_curdev.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_curdev.Name = "label_curdev";
            this.label_curdev.Size = new System.Drawing.Size(546, 18);
            this.label_curdev.TabIndex = 15;
            this.label_curdev.Text = "选中设备";
            this.label_curdev.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePicker_EndTime
            // 
            this.dateTimePicker_EndTime.CustomFormat = " yyyy-MM-dd HH:mm";
            this.dateTimePicker_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_EndTime.Location = new System.Drawing.Point(63, 35);
            this.dateTimePicker_EndTime.Name = "dateTimePicker_EndTime";
            this.dateTimePicker_EndTime.Size = new System.Drawing.Size(162, 21);
            this.dateTimePicker_EndTime.TabIndex = 7;
            // 
            // button_DataDay
            // 
            this.button_DataDay.Location = new System.Drawing.Point(469, 33);
            this.button_DataDay.Name = "button_DataDay";
            this.button_DataDay.Size = new System.Drawing.Size(132, 23);
            this.button_DataDay.TabIndex = 13;
            this.button_DataDay.Text = "最近一天数据";
            this.button_DataDay.UseVisualStyleBackColor = true;
            this.button_DataDay.Click += new System.EventHandler(this.button_DataDay_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "开始时间";
            // 
            // button_DataHour
            // 
            this.button_DataHour.Location = new System.Drawing.Point(322, 33);
            this.button_DataHour.Name = "button_DataHour";
            this.button_DataHour.Size = new System.Drawing.Size(132, 23);
            this.button_DataHour.TabIndex = 12;
            this.button_DataHour.Text = "最近一小时数据";
            this.button_DataHour.UseVisualStyleBackColor = true;
            this.button_DataHour.Click += new System.EventHandler(this.button_DataHour_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "结束时间";
            // 
            // button_Select
            // 
            this.button_Select.Location = new System.Drawing.Point(234, 33);
            this.button_Select.Name = "button_Select";
            this.button_Select.Size = new System.Drawing.Size(75, 23);
            this.button_Select.TabIndex = 8;
            this.button_Select.Text = "检索";
            this.button_Select.UseVisualStyleBackColor = true;
            this.button_Select.Click += new System.EventHandler(this.button_Select_Click);
            // 
            // dateTimePicker_StartTime
            // 
            this.dateTimePicker_StartTime.CustomFormat = " yyyy-MM-dd HH:mm";
            this.dateTimePicker_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_StartTime.Location = new System.Drawing.Point(63, 8);
            this.dateTimePicker_StartTime.Name = "dateTimePicker_StartTime";
            this.dateTimePicker_StartTime.Size = new System.Drawing.Size(162, 21);
            this.dateTimePicker_StartTime.TabIndex = 6;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 453);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(783, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Tab_HisData_nw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 475);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Tab_HisData_nw";
            this.Text = "Tab_HisData";
            this.Load += new System.EventHandler(this.Tab_HisData_nw_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl_weather.ResumeLayout(false);
            this.tabPage_pull.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_pull)).EndInit();
            this.tabPage_weather.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_weather)).EndInit();
            this.tabPage_image.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_image)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Select;
        private System.Windows.Forms.DateTimePicker dateTimePicker_EndTime;
        private System.Windows.Forms.DateTimePicker dateTimePicker_StartTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_DataDay;
        private System.Windows.Forms.Button button_DataHour;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_curdev;
        private System.Windows.Forms.TabControl tabControl_weather;
        private System.Windows.Forms.TabPage tabPage_pull;
        private System.Windows.Forms.TabPage tabPage_weather;
        private System.Windows.Forms.TabPage tabPage_image;
        private System.Windows.Forms.DataGridView dataGridView_pull;
        private System.Windows.Forms.DataGridView dataGridView_weather;
        private System.Windows.Forms.DataGridView dataGridView_image;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}