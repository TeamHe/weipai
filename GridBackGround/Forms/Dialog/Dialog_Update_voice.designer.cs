namespace GridBackGround.Forms.Dialog
{
    partial class Dialog_Update_voice
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
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Browse = new System.Windows.Forms.Button();
            this.textBox_File = new System.Windows.Forms.TextBox();
            this.label_FileName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Cancel);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Controls.Add(this.button_Browse);
            this.panel1.Controls.Add(this.textBox_File);
            this.panel1.Controls.Add(this.label_FileName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 134);
            this.panel1.TabIndex = 0;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(181, 66);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 11;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(45, 66);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "开始升级";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Browse
            // 
            this.button_Browse.Location = new System.Drawing.Point(327, 21);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(35, 23);
            this.button_Browse.TabIndex = 10;
            this.button_Browse.Text = "...";
            this.button_Browse.UseVisualStyleBackColor = true;
            // 
            // textBox_File
            // 
            this.textBox_File.Location = new System.Drawing.Point(71, 21);
            this.textBox_File.Name = "textBox_File";
            this.textBox_File.Size = new System.Drawing.Size(250, 21);
            this.textBox_File.TabIndex = 9;
            this.textBox_File.Text = "D:\\Users\\12345\\Desktop\\160226.bin";
            // 
            // label_FileName
            // 
            this.label_FileName.AutoSize = true;
            this.label_FileName.Location = new System.Drawing.Point(12, 24);
            this.label_FileName.Name = "label_FileName";
            this.label_FileName.Size = new System.Drawing.Size(41, 12);
            this.label_FileName.TabIndex = 8;
            this.label_FileName.Text = "文件名";
            // 
            // Dialog_Update_voice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 134);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_Update_voice";
            this.Text = "远程升级";
            this.Load += new System.EventHandler(this.Dialog_Update_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_File;
        private System.Windows.Forms.Label label_FileName;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Browse;
    }
}