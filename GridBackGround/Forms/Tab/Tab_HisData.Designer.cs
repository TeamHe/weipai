namespace GridBackGround
{
    partial class Tab_HisData
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
            this.button_DataDay = new System.Windows.Forms.Button();
            this.button_DataHour = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_Name = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_Select = new System.Windows.Forms.Button();
            this.dateTimePicker_EndTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_StartTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Type = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.button_DataDay);
            this.panel1.Controls.Add(this.button_DataHour);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.comboBox_Name);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.button_Select);
            this.panel1.Controls.Add(this.dateTimePicker_EndTime);
            this.panel1.Controls.Add(this.dateTimePicker_StartTime);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox_Type);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1044, 594);
            this.panel1.TabIndex = 1;
            // 
            // button_DataDay
            // 
            this.button_DataDay.Location = new System.Drawing.Point(852, 68);
            this.button_DataDay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_DataDay.Name = "button_DataDay";
            this.button_DataDay.Size = new System.Drawing.Size(176, 29);
            this.button_DataDay.TabIndex = 13;
            this.button_DataDay.Text = "最近一天数据";
            this.button_DataDay.UseVisualStyleBackColor = true;
            this.button_DataDay.Click += new System.EventHandler(this.button_DataDay_Click);
            // 
            // button_DataHour
            // 
            this.button_DataHour.Location = new System.Drawing.Point(852, 22);
            this.button_DataHour.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_DataHour.Name = "button_DataHour";
            this.button_DataHour.Size = new System.Drawing.Size(176, 29);
            this.button_DataHour.TabIndex = 12;
            this.button_DataHour.Text = "最近一小时数据";
            this.button_DataHour.UseVisualStyleBackColor = true;
            this.button_DataHour.Click += new System.EventHandler(this.button_DataHour_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "装置名称";
            // 
            // comboBox_Name
            // 
            this.comboBox_Name.AutoCompleteCustomSource.AddRange(new string[] {
            "基本数据类型",
            "导地线微风振动特征量",
            "导地线微风振动波形信号",
            "导地线舞动特征量",
            "导地线舞动轨迹"});
            this.comboBox_Name.FormattingEnabled = true;
            this.comboBox_Name.Items.AddRange(new object[] {
            "气象环境",
            "杆塔倾斜",
            "导地线微风振动特征量",
            "导地线微风振动波形信号",
            "导线弧垂",
            "导线温度",
            "覆冰及不均衡张力差",
            "导线风偏",
            "导地线舞动特征量",
            "导地线舞动轨迹",
            "现场污秽度"});
            this.comboBox_Name.Location = new System.Drawing.Point(107, 22);
            this.comboBox_Name.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Name.Name = "comboBox_Name";
            this.comboBox_Name.Size = new System.Drawing.Size(179, 23);
            this.comboBox_Name.TabIndex = 10;
            this.comboBox_Name.SelectedIndexChanged += new System.EventHandler(this.comboBox_Name_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 102);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1045, 488);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // button_Select
            // 
            this.button_Select.Location = new System.Drawing.Point(707, 22);
            this.button_Select.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Select.Name = "button_Select";
            this.button_Select.Size = new System.Drawing.Size(100, 29);
            this.button_Select.TabIndex = 8;
            this.button_Select.Text = "检索";
            this.button_Select.UseVisualStyleBackColor = true;
            this.button_Select.Click += new System.EventHandler(this.button_Select_Click);
            // 
            // dateTimePicker_EndTime
            // 
            this.dateTimePicker_EndTime.CustomFormat = " yyyy-MM-dd HH:mm";
            this.dateTimePicker_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_EndTime.Location = new System.Drawing.Point(467, 59);
            this.dateTimePicker_EndTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker_EndTime.Name = "dateTimePicker_EndTime";
            this.dateTimePicker_EndTime.Size = new System.Drawing.Size(215, 25);
            this.dateTimePicker_EndTime.TabIndex = 7;
            // 
            // dateTimePicker_StartTime
            // 
            this.dateTimePicker_StartTime.CustomFormat = " yyyy-MM-dd HH:mm";
            this.dateTimePicker_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_StartTime.Location = new System.Drawing.Point(467, 21);
            this.dateTimePicker_StartTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker_StartTime.Name = "dateTimePicker_StartTime";
            this.dateTimePicker_StartTime.Size = new System.Drawing.Size(215, 25);
            this.dateTimePicker_StartTime.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(388, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "结束时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(388, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "开始时间";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "数据类型";
            // 
            // comboBox_Type
            // 
            this.comboBox_Type.AutoCompleteCustomSource.AddRange(new string[] {
            "基本数据类型",
            "导地线微风振动特征量",
            "导地线微风振动波形信号",
            "导地线舞动特征量",
            "导地线舞动轨迹"});
            this.comboBox_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Type.FormattingEnabled = true;
            this.comboBox_Type.Location = new System.Drawing.Point(107, 70);
            this.comboBox_Type.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Type.Name = "comboBox_Type";
            this.comboBox_Type.Size = new System.Drawing.Size(177, 23);
            this.comboBox_Type.TabIndex = 2;
            // 
            // Tab_HisData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 594);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Tab_HisData";
            this.Text = "Tab_HisData";
            this.Load += new System.EventHandler(this.Tab_HisData_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_Name;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_Select;
        private System.Windows.Forms.DateTimePicker dateTimePicker_EndTime;
        private System.Windows.Forms.DateTimePicker dateTimePicker_StartTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Type;
        private System.Windows.Forms.Button button_DataDay;
        private System.Windows.Forms.Button button_DataHour;
    }
}