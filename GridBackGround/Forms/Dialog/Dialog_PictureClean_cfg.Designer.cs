namespace GridBackGround.Forms.Dialog
{
    partial class Dialog_PictureClean_cfg
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
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_clean_auto = new System.Windows.Forms.CheckBox();
            this.numericUpDown_cleanperiod = new System.Windows.Forms.NumericUpDown();
            this.dateTimePickerCleanTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownReserveTime = new System.Windows.Forms.NumericUpDown();
            this.checkBox_clean_atstart = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_cleanperiod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReserveTime)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图片删除周期(天)";
            // 
            // checkBox_clean_auto
            // 
            this.checkBox_clean_auto.AutoSize = true;
            this.checkBox_clean_auto.Location = new System.Drawing.Point(12, 12);
            this.checkBox_clean_auto.Name = "checkBox_clean_auto";
            this.checkBox_clean_auto.Size = new System.Drawing.Size(96, 16);
            this.checkBox_clean_auto.TabIndex = 1;
            this.checkBox_clean_auto.Text = "自动删除图片";
            this.checkBox_clean_auto.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_cleanperiod
            // 
            this.numericUpDown_cleanperiod.Location = new System.Drawing.Point(170, 47);
            this.numericUpDown_cleanperiod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_cleanperiod.Name = "numericUpDown_cleanperiod";
            this.numericUpDown_cleanperiod.Size = new System.Drawing.Size(72, 21);
            this.numericUpDown_cleanperiod.TabIndex = 5;
            this.numericUpDown_cleanperiod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dateTimePickerCleanTime
            // 
            this.dateTimePickerCleanTime.CustomFormat = "HH:mm";
            this.dateTimePickerCleanTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerCleanTime.Location = new System.Drawing.Point(171, 91);
            this.dateTimePickerCleanTime.Name = "dateTimePickerCleanTime";
            this.dateTimePickerCleanTime.ShowUpDown = true;
            this.dateTimePickerCleanTime.Size = new System.Drawing.Size(72, 21);
            this.dateTimePickerCleanTime.TabIndex = 6;
            this.dateTimePickerCleanTime.Value = new System.DateTime(2020, 8, 8, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "自动清理时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "图片保留时间(天)";
            // 
            // numericUpDownReserveTime
            // 
            this.numericUpDownReserveTime.Location = new System.Drawing.Point(173, 130);
            this.numericUpDownReserveTime.Name = "numericUpDownReserveTime";
            this.numericUpDownReserveTime.Size = new System.Drawing.Size(69, 21);
            this.numericUpDownReserveTime.TabIndex = 9;
            // 
            // checkBox_clean_atstart
            // 
            this.checkBox_clean_atstart.AutoSize = true;
            this.checkBox_clean_atstart.Location = new System.Drawing.Point(170, 12);
            this.checkBox_clean_atstart.Name = "checkBox_clean_atstart";
            this.checkBox_clean_atstart.Size = new System.Drawing.Size(96, 16);
            this.checkBox_clean_atstart.TabIndex = 10;
            this.checkBox_clean_atstart.Text = "启动自动清理";
            this.checkBox_clean_atstart.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(17, 225);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 11;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(182, 225);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "上次清理时间";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(171, 181);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(117, 21);
            this.textBox1.TabIndex = 15;
            // 
            // Dialog_PictureClean_cfg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 260);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.checkBox_clean_atstart);
            this.Controls.Add(this.numericUpDownReserveTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePickerCleanTime);
            this.Controls.Add(this.numericUpDown_cleanperiod);
            this.Controls.Add(this.checkBox_clean_auto);
            this.Controls.Add(this.label1);
            this.Name = "Dialog_PictureClean_cfg";
            this.Text = "图片清理选项配置";
            this.Load += new System.EventHandler(this.Dialog_PictureClean_cfg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_cleanperiod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReserveTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_clean_auto;
        private System.Windows.Forms.NumericUpDown numericUpDown_cleanperiod;
        private System.Windows.Forms.DateTimePicker dateTimePickerCleanTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownReserveTime;
        private System.Windows.Forms.CheckBox checkBox_clean_atstart;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
    }
}