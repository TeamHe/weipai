namespace GridBackGround.Forms.Dialog
{
    partial class Dialog_Update
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_UpdateTime = new System.Windows.Forms.DateTimePicker();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Browse = new System.Windows.Forms.Button();
            this.textBox_File = new System.Windows.Forms.TextBox();
            this.label_FileName = new System.Windows.Forms.Label();
            this.label_UpdateTime = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePicker_UpdateTime);
            this.panel1.Controls.Add(this.button_Cancel);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Controls.Add(this.button_Browse);
            this.panel1.Controls.Add(this.textBox_File);
            this.panel1.Controls.Add(this.label_FileName);
            this.panel1.Controls.Add(this.label_UpdateTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 162);
            this.panel1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "512",
            "1024",
            "2048"});
            this.comboBox1.Location = new System.Drawing.Point(72, 85);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "包长度";
            // 
            // dateTimePicker_UpdateTime
            // 
            this.dateTimePicker_UpdateTime.Location = new System.Drawing.Point(72, 48);
            this.dateTimePicker_UpdateTime.Name = "dateTimePicker_UpdateTime";
            this.dateTimePicker_UpdateTime.Size = new System.Drawing.Size(141, 21);
            this.dateTimePicker_UpdateTime.TabIndex = 12;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(186, 123);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 11;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(50, 123);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "开始升级";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Browse
            // 
            this.button_Browse.Location = new System.Drawing.Point(328, 10);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(35, 23);
            this.button_Browse.TabIndex = 10;
            this.button_Browse.Text = "...";
            this.button_Browse.UseVisualStyleBackColor = true;
            // 
            // textBox_File
            // 
            this.textBox_File.Location = new System.Drawing.Point(72, 11);
            this.textBox_File.Name = "textBox_File";
            this.textBox_File.Size = new System.Drawing.Size(250, 21);
            this.textBox_File.TabIndex = 9;
            this.textBox_File.Text = "D:\\Users\\12345\\Desktop\\160226.bin";
            // 
            // label_FileName
            // 
            this.label_FileName.AutoSize = true;
            this.label_FileName.Location = new System.Drawing.Point(13, 15);
            this.label_FileName.Name = "label_FileName";
            this.label_FileName.Size = new System.Drawing.Size(41, 12);
            this.label_FileName.TabIndex = 8;
            this.label_FileName.Text = "文件名";
            // 
            // label_UpdateTime
            // 
            this.label_UpdateTime.AutoSize = true;
            this.label_UpdateTime.Location = new System.Drawing.Point(7, 52);
            this.label_UpdateTime.Name = "label_UpdateTime";
            this.label_UpdateTime.Size = new System.Drawing.Size(53, 12);
            this.label_UpdateTime.TabIndex = 2;
            this.label_UpdateTime.Text = "修改日期";
            // 
            // Dialog_Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 162);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_Update";
            this.Text = "远程升级";
            this.Load += new System.EventHandler(this.Dialog_Update_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_UpdateTime;
        private System.Windows.Forms.TextBox textBox_File;
        private System.Windows.Forms.Label label_FileName;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.DateTimePicker dateTimePicker_UpdateTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}