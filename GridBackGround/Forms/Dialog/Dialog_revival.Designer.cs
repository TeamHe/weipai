namespace GridBackGround.Forms.Dialog
{
    partial class Dialog_revival
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
            this.label_revival_time = new System.Windows.Forms.Label();
            this.textBox_revival_time = new System.Windows.Forms.TextBox();
            this.textBox_revival_cycle = new System.Windows.Forms.TextBox();
            this.label_revival_cycle = new System.Windows.Forms.Label();
            this.textBox_duration_time = new System.Windows.Forms.TextBox();
            this.label_duration_time = new System.Windows.Forms.Label();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_revival_time
            // 
            this.label_revival_time.AutoSize = true;
            this.label_revival_time.Location = new System.Drawing.Point(12, 36);
            this.label_revival_time.Name = "label_revival_time";
            this.label_revival_time.Size = new System.Drawing.Size(77, 12);
            this.label_revival_time.TabIndex = 0;
            this.label_revival_time.Text = "苏醒参考时间";
            // 
            // textBox_revival_time
            // 
            this.textBox_revival_time.Location = new System.Drawing.Point(105, 33);
            this.textBox_revival_time.Name = "textBox_revival_time";
            this.textBox_revival_time.Size = new System.Drawing.Size(100, 21);
            this.textBox_revival_time.TabIndex = 1;
            this.textBox_revival_time.Text = "1200";
            // 
            // textBox_revival_cycle
            // 
            this.textBox_revival_cycle.Location = new System.Drawing.Point(105, 73);
            this.textBox_revival_cycle.Name = "textBox_revival_cycle";
            this.textBox_revival_cycle.Size = new System.Drawing.Size(100, 21);
            this.textBox_revival_cycle.TabIndex = 3;
            this.textBox_revival_cycle.Text = "300";
            // 
            // label_revival_cycle
            // 
            this.label_revival_cycle.AutoSize = true;
            this.label_revival_cycle.Location = new System.Drawing.Point(36, 76);
            this.label_revival_cycle.Name = "label_revival_cycle";
            this.label_revival_cycle.Size = new System.Drawing.Size(53, 12);
            this.label_revival_cycle.TabIndex = 2;
            this.label_revival_cycle.Text = "苏醒周期";
            // 
            // textBox_duration_time
            // 
            this.textBox_duration_time.Location = new System.Drawing.Point(105, 113);
            this.textBox_duration_time.Name = "textBox_duration_time";
            this.textBox_duration_time.Size = new System.Drawing.Size(100, 21);
            this.textBox_duration_time.TabIndex = 5;
            this.textBox_duration_time.Text = "60";
            // 
            // label_duration_time
            // 
            this.label_duration_time.AutoSize = true;
            this.label_duration_time.Location = new System.Drawing.Point(36, 116);
            this.label_duration_time.Name = "label_duration_time";
            this.label_duration_time.Size = new System.Drawing.Size(53, 12);
            this.label_duration_time.TabIndex = 4;
            this.label_duration_time.Text = "苏醒时长";
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(26, 175);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 6;
            this.button_ok.Text = "确定";
            this.button_ok.UseVisualStyleBackColor = true;
            // 
            // button_cancle
            // 
            this.button_cancle.Location = new System.Drawing.Point(129, 175);
            this.button_cancle.Name = "button_cancle";
            this.button_cancle.Size = new System.Drawing.Size(75, 23);
            this.button_cancle.TabIndex = 7;
            this.button_cancle.Text = "取消";
            this.button_cancle.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "s";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "s";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "s";
            // 
            // Dialog_revival
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 219);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_cancle);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.textBox_duration_time);
            this.Controls.Add(this.label_duration_time);
            this.Controls.Add(this.textBox_revival_cycle);
            this.Controls.Add(this.label_revival_cycle);
            this.Controls.Add(this.textBox_revival_time);
            this.Controls.Add(this.label_revival_time);
            this.Name = "Dialog_revival";
            this.Text = "装置苏醒时间";
            this.Load += new System.EventHandler(this.Dialog_revival_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_revival_time;
        private System.Windows.Forms.TextBox textBox_revival_time;
        private System.Windows.Forms.TextBox textBox_revival_cycle;
        private System.Windows.Forms.Label label_revival_cycle;
        private System.Windows.Forms.TextBox textBox_duration_time;
        private System.Windows.Forms.Label label_duration_time;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}