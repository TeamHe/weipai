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
            this.dateTimePicker_UpdateTime = new System.Windows.Forms.DateTimePicker();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Browse = new System.Windows.Forms.Button();
            this.textBox_File = new System.Windows.Forms.TextBox();
            this.label_FileName = new System.Windows.Forms.Label();
            this.textBox_SoftVersion = new System.Windows.Forms.TextBox();
            this.label_UpdateTime = new System.Windows.Forms.Label();
            this.label_SoftVersion = new System.Windows.Forms.Label();
            this.textBox_HardVersion = new System.Windows.Forms.TextBox();
            this.label_HardVersion = new System.Windows.Forms.Label();
            this.textBox_Model = new System.Windows.Forms.TextBox();
            this.label_Model = new System.Windows.Forms.Label();
            this.textBox_FactoryName = new System.Windows.Forms.TextBox();
            this.label_Fatory_Name = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dateTimePicker_UpdateTime);
            this.panel1.Controls.Add(this.button_Cancel);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Controls.Add(this.button_Browse);
            this.panel1.Controls.Add(this.textBox_File);
            this.panel1.Controls.Add(this.label_FileName);
            this.panel1.Controls.Add(this.textBox_SoftVersion);
            this.panel1.Controls.Add(this.label_UpdateTime);
            this.panel1.Controls.Add(this.label_SoftVersion);
            this.panel1.Controls.Add(this.textBox_HardVersion);
            this.panel1.Controls.Add(this.label_HardVersion);
            this.panel1.Controls.Add(this.textBox_Model);
            this.panel1.Controls.Add(this.label_Model);
            this.panel1.Controls.Add(this.textBox_FactoryName);
            this.panel1.Controls.Add(this.label_Fatory_Name);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 289);
            this.panel1.TabIndex = 0;
            // 
            // dateTimePicker_UpdateTime
            // 
            this.dateTimePicker_UpdateTime.Location = new System.Drawing.Point(72, 140);
            this.dateTimePicker_UpdateTime.Name = "dateTimePicker_UpdateTime";
            this.dateTimePicker_UpdateTime.Size = new System.Drawing.Size(141, 21);
            this.dateTimePicker_UpdateTime.TabIndex = 12;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(181, 248);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 11;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(45, 248);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "开始升级";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Browse
            // 
            this.button_Browse.Location = new System.Drawing.Point(327, 194);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(35, 23);
            this.button_Browse.TabIndex = 10;
            this.button_Browse.Text = "...";
            this.button_Browse.UseVisualStyleBackColor = true;
            // 
            // textBox_File
            // 
            this.textBox_File.Location = new System.Drawing.Point(71, 194);
            this.textBox_File.Name = "textBox_File";
            this.textBox_File.Size = new System.Drawing.Size(250, 21);
            this.textBox_File.TabIndex = 9;
            this.textBox_File.Text = "D:\\Users\\12345\\Desktop\\160226.bin";
            // 
            // label_FileName
            // 
            this.label_FileName.AutoSize = true;
            this.label_FileName.Location = new System.Drawing.Point(12, 197);
            this.label_FileName.Name = "label_FileName";
            this.label_FileName.Size = new System.Drawing.Size(41, 12);
            this.label_FileName.TabIndex = 8;
            this.label_FileName.Text = "文件名";
            // 
            // textBox_SoftVersion
            // 
            this.textBox_SoftVersion.Location = new System.Drawing.Point(262, 92);
            this.textBox_SoftVersion.Name = "textBox_SoftVersion";
            this.textBox_SoftVersion.Size = new System.Drawing.Size(100, 21);
            this.textBox_SoftVersion.TabIndex = 7;
            this.textBox_SoftVersion.Text = "V1.0";
            // 
            // label_UpdateTime
            // 
            this.label_UpdateTime.AutoSize = true;
            this.label_UpdateTime.Location = new System.Drawing.Point(12, 140);
            this.label_UpdateTime.Name = "label_UpdateTime";
            this.label_UpdateTime.Size = new System.Drawing.Size(53, 12);
            this.label_UpdateTime.TabIndex = 2;
            this.label_UpdateTime.Text = "修改日期";
            // 
            // label_SoftVersion
            // 
            this.label_SoftVersion.AutoSize = true;
            this.label_SoftVersion.Location = new System.Drawing.Point(203, 95);
            this.label_SoftVersion.Name = "label_SoftVersion";
            this.label_SoftVersion.Size = new System.Drawing.Size(53, 12);
            this.label_SoftVersion.TabIndex = 6;
            this.label_SoftVersion.Text = "软件版本";
            // 
            // textBox_HardVersion
            // 
            this.textBox_HardVersion.Location = new System.Drawing.Point(262, 34);
            this.textBox_HardVersion.Name = "textBox_HardVersion";
            this.textBox_HardVersion.Size = new System.Drawing.Size(100, 21);
            this.textBox_HardVersion.TabIndex = 5;
            this.textBox_HardVersion.Text = "V1.0";
            // 
            // label_HardVersion
            // 
            this.label_HardVersion.AutoSize = true;
            this.label_HardVersion.Location = new System.Drawing.Point(203, 37);
            this.label_HardVersion.Name = "label_HardVersion";
            this.label_HardVersion.Size = new System.Drawing.Size(53, 12);
            this.label_HardVersion.TabIndex = 4;
            this.label_HardVersion.Text = "硬件版本";
            // 
            // textBox_Model
            // 
            this.textBox_Model.Location = new System.Drawing.Point(71, 89);
            this.textBox_Model.Name = "textBox_Model";
            this.textBox_Model.Size = new System.Drawing.Size(100, 21);
            this.textBox_Model.TabIndex = 3;
            this.textBox_Model.Text = "ZLWP001";
            // 
            // label_Model
            // 
            this.label_Model.AutoSize = true;
            this.label_Model.Location = new System.Drawing.Point(12, 92);
            this.label_Model.Name = "label_Model";
            this.label_Model.Size = new System.Drawing.Size(53, 12);
            this.label_Model.TabIndex = 2;
            this.label_Model.Text = "设备型号";
            // 
            // textBox_FactoryName
            // 
            this.textBox_FactoryName.Location = new System.Drawing.Point(71, 34);
            this.textBox_FactoryName.Name = "textBox_FactoryName";
            this.textBox_FactoryName.Size = new System.Drawing.Size(100, 21);
            this.textBox_FactoryName.TabIndex = 1;
            this.textBox_FactoryName.Text = "Zhongdian";
            // 
            // label_Fatory_Name
            // 
            this.label_Fatory_Name.AutoSize = true;
            this.label_Fatory_Name.Location = new System.Drawing.Point(12, 37);
            this.label_Fatory_Name.Name = "label_Fatory_Name";
            this.label_Fatory_Name.Size = new System.Drawing.Size(53, 12);
            this.label_Fatory_Name.TabIndex = 0;
            this.label_Fatory_Name.Text = "厂商名称";
            // 
            // Dialog_Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 289);
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
        private System.Windows.Forms.TextBox textBox_Model;
        private System.Windows.Forms.Label label_Model;
        private System.Windows.Forms.TextBox textBox_FactoryName;
        private System.Windows.Forms.Label label_Fatory_Name;
        private System.Windows.Forms.TextBox textBox_HardVersion;
        private System.Windows.Forms.Label label_HardVersion;
        private System.Windows.Forms.TextBox textBox_SoftVersion;
        private System.Windows.Forms.Label label_UpdateTime;
        private System.Windows.Forms.Label label_SoftVersion;
        private System.Windows.Forms.TextBox textBox_File;
        private System.Windows.Forms.Label label_FileName;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.DateTimePicker dateTimePicker_UpdateTime;
    }
}