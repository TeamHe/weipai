namespace GridBackGround.Forms.Dialogs_nw
{
    partial class Dialog_nw_update
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
            this.button_Cancle = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Browse = new System.Windows.Forms.Button();
            this.textBox_File = new System.Windows.Forms.TextBox();
            this.label_FileName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Cancle);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Controls.Add(this.numericUpDown2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button_Browse);
            this.panel1.Controls.Add(this.textBox_File);
            this.panel1.Controls.Add(this.label_FileName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(458, 276);
            this.panel1.TabIndex = 0;
            // 
            // button_Cancle
            // 
            this.button_Cancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancle.Location = new System.Drawing.Point(321, 191);
            this.button_Cancle.Name = "button_Cancle";
            this.button_Cancle.Size = new System.Drawing.Size(75, 23);
            this.button_Cancle.TabIndex = 44;
            this.button_Cancle.Text = "取消";
            this.button_Cancle.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(51, 191);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 43;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(105, 96);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown2.TabIndex = 42;
            this.numericUpDown2.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 41;
            this.label3.Text = "密码";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(107, 138);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(124, 21);
            this.textBox1.TabIndex = 40;
            this.textBox1.Text = "1234";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 39;
            this.label2.Text = "单包数据长度";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(105, 21);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown1.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "通道号";
            // 
            // button_Browse
            // 
            this.button_Browse.Location = new System.Drawing.Point(361, 62);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(35, 23);
            this.button_Browse.TabIndex = 13;
            this.button_Browse.Text = "...";
            this.button_Browse.UseVisualStyleBackColor = true;
            this.button_Browse.Click += new System.EventHandler(this.button_Browse_Click);
            // 
            // textBox_File
            // 
            this.textBox_File.Location = new System.Drawing.Point(105, 62);
            this.textBox_File.Name = "textBox_File";
            this.textBox_File.Size = new System.Drawing.Size(250, 21);
            this.textBox_File.TabIndex = 12;
            this.textBox_File.Text = "D:\\Users\\12345\\Desktop\\160226.bin";
            // 
            // label_FileName
            // 
            this.label_FileName.AutoSize = true;
            this.label_FileName.Location = new System.Drawing.Point(20, 65);
            this.label_FileName.Name = "label_FileName";
            this.label_FileName.Size = new System.Drawing.Size(41, 12);
            this.label_FileName.TabIndex = 11;
            this.label_FileName.Text = "文件名";
            // 
            // Dialog_nw_update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 276);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_nw_update";
            this.Text = "设备升级";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.TextBox textBox_File;
        private System.Windows.Forms.Label label_FileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button button_Cancle;
        private System.Windows.Forms.Button button_OK;
    }
}