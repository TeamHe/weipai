namespace GridBackGround.Forms
{
    partial class Dialog_Mode
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
            this.radioButton_MiW = new System.Windows.Forms.RadioButton();
            this.radioButton_MingW = new System.Windows.Forms.RadioButton();
            this.radioButton_GCTC = new System.Windows.Forms.RadioButton();
            this.radioButton_AQCSH = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.radioButton_AQCSH);
            this.panel1.Controls.Add(this.radioButton_GCTC);
            this.panel1.Controls.Add(this.radioButton_MingW);
            this.panel1.Controls.Add(this.radioButton_MiW);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 262);
            this.panel1.TabIndex = 0;
            // 
            // radioButton_MiW
            // 
            this.radioButton_MiW.AutoSize = true;
            this.radioButton_MiW.Location = new System.Drawing.Point(42, 75);
            this.radioButton_MiW.Name = "radioButton_MiW";
            this.radioButton_MiW.Size = new System.Drawing.Size(95, 16);
            this.radioButton_MiW.TabIndex = 0;
            this.radioButton_MiW.TabStop = true;
            this.radioButton_MiW.Text = "密文通讯模式";
            this.radioButton_MiW.UseVisualStyleBackColor = true;
            // 
            // radioButton_MingW
            // 
            this.radioButton_MingW.AutoSize = true;
            this.radioButton_MingW.Location = new System.Drawing.Point(42, 114);
            this.radioButton_MingW.Name = "radioButton_MingW";
            this.radioButton_MingW.Size = new System.Drawing.Size(95, 16);
            this.radioButton_MingW.TabIndex = 1;
            this.radioButton_MingW.TabStop = true;
            this.radioButton_MingW.Text = "明文通讯模式";
            this.radioButton_MingW.UseVisualStyleBackColor = true;
            // 
            // radioButton_GCTC
            // 
            this.radioButton_GCTC.AutoSize = true;
            this.radioButton_GCTC.Location = new System.Drawing.Point(42, 153);
            this.radioButton_GCTC.Name = "radioButton_GCTC";
            this.radioButton_GCTC.Size = new System.Drawing.Size(95, 16);
            this.radioButton_GCTC.TabIndex = 2;
            this.radioButton_GCTC.TabStop = true;
            this.radioButton_GCTC.Text = "工厂调测模式";
            this.radioButton_GCTC.UseVisualStyleBackColor = true;
            // 
            // radioButton_AQCSH
            // 
            this.radioButton_AQCSH.AutoSize = true;
            this.radioButton_AQCSH.Location = new System.Drawing.Point(42, 36);
            this.radioButton_AQCSH.Name = "radioButton_AQCSH";
            this.radioButton_AQCSH.Size = new System.Drawing.Size(107, 16);
            this.radioButton_AQCSH.TabIndex = 3;
            this.radioButton_AQCSH.TabStop = true;
            this.radioButton_AQCSH.Text = "安全初始化模式";
            this.radioButton_AQCSH.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(42, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(149, 205);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Dialog_Mode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 262);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_Mode";
            this.Text = "工作模式切换";
            this.Load += new System.EventHandler(this.Dialog_Mode_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButton_AQCSH;
        private System.Windows.Forms.RadioButton radioButton_GCTC;
        private System.Windows.Forms.RadioButton radioButton_MingW;
        private System.Windows.Forms.RadioButton radioButton_MiW;
    }
}