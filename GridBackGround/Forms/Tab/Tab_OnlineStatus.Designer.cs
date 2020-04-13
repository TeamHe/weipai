namespace GridBackGround.Forms.Tab
{
    partial class Tab_OnlineStatus
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
            this.textBox_UpdateTime = new System.Windows.Forms.TextBox();
            this.label_UpdateTime = new System.Windows.Forms.Label();
            this.comboBox_Line = new System.Windows.Forms.ComboBox();
            this.comboBox_Department = new System.Windows.Forms.ComboBox();
            this.label_Tower = new System.Windows.Forms.Label();
            this.labelDepartment = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView_Display = new Tools.DataGridViewUtil();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Display)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox_UpdateTime);
            this.panel1.Controls.Add(this.label_UpdateTime);
            this.panel1.Controls.Add(this.comboBox_Line);
            this.panel1.Controls.Add(this.comboBox_Department);
            this.panel1.Controls.Add(this.label_Tower);
            this.panel1.Controls.Add(this.labelDepartment);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(828, 42);
            this.panel1.TabIndex = 0;
            // 
            // textBox_UpdateTime
            // 
            this.textBox_UpdateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_UpdateTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_UpdateTime.Enabled = false;
            this.textBox_UpdateTime.Location = new System.Drawing.Point(626, 12);
            this.textBox_UpdateTime.Name = "textBox_UpdateTime";
            this.textBox_UpdateTime.Size = new System.Drawing.Size(178, 14);
            this.textBox_UpdateTime.TabIndex = 6;
            // 
            // label_UpdateTime
            // 
            this.label_UpdateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_UpdateTime.AutoSize = true;
            this.label_UpdateTime.Location = new System.Drawing.Point(566, 13);
            this.label_UpdateTime.Name = "label_UpdateTime";
            this.label_UpdateTime.Size = new System.Drawing.Size(53, 12);
            this.label_UpdateTime.TabIndex = 4;
            this.label_UpdateTime.Text = "更新时间";
            // 
            // comboBox_Line
            // 
            this.comboBox_Line.FormattingEnabled = true;
            this.comboBox_Line.Location = new System.Drawing.Point(249, 9);
            this.comboBox_Line.Name = "comboBox_Line";
            this.comboBox_Line.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Line.TabIndex = 3;
            // 
            // comboBox_Department
            // 
            this.comboBox_Department.FormattingEnabled = true;
            this.comboBox_Department.Location = new System.Drawing.Point(55, 9);
            this.comboBox_Department.Name = "comboBox_Department";
            this.comboBox_Department.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Department.TabIndex = 2;
            // 
            // label_Tower
            // 
            this.label_Tower.AutoSize = true;
            this.label_Tower.Location = new System.Drawing.Point(207, 13);
            this.label_Tower.Name = "label_Tower";
            this.label_Tower.Size = new System.Drawing.Size(35, 12);
            this.label_Tower.TabIndex = 1;
            this.label_Tower.Text = "线路:";
            // 
            // labelDepartment
            // 
            this.labelDepartment.AutoSize = true;
            this.labelDepartment.Location = new System.Drawing.Point(13, 13);
            this.labelDepartment.Name = "labelDepartment";
            this.labelDepartment.Size = new System.Drawing.Size(35, 12);
            this.labelDepartment.TabIndex = 0;
            this.labelDepartment.Text = "单位:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView_Display);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(828, 428);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView_Display
            // 
            this.dataGridView_Display.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Display.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Display.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView_Display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Display.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Display.Name = "dataGridView_Display";
            this.dataGridView_Display.RowTemplate.Height = 23;
            this.dataGridView_Display.Size = new System.Drawing.Size(828, 428);
            this.dataGridView_Display.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Tab_OnlineStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 470);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Tab_OnlineStatus";
            this.Text = "OnlineStatus";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Display)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_UpdateTime;
        private System.Windows.Forms.Label label_UpdateTime;
        private System.Windows.Forms.ComboBox comboBox_Line;
        private System.Windows.Forms.ComboBox comboBox_Department;
        private System.Windows.Forms.Label label_Tower;
        private System.Windows.Forms.Label labelDepartment;
        private System.Windows.Forms.Panel panel2;
        Tools.DataGridViewUtil dataGridView_Display;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}