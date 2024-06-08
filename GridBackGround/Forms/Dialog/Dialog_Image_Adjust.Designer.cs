namespace GridBackGround.Forms
{
    partial class Dialog_Image_Adjust
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
            this.label5 = new System.Windows.Forms.Label();
            this.button_Open = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBox_save = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_TurntoPreset = new System.Windows.Forms.Button();
            this.button_SaveAsPreset = new System.Windows.Forms.Button();
            this.textBox_preset = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Far = new System.Windows.Forms.Button();
            this.button_Near = new System.Windows.Forms.Button();
            this.button_Left = new System.Windows.Forms.Button();
            this.button_Down = new System.Windows.Forms.Button();
            this.button_Right = new System.Windows.Forms.Button();
            this.button_Up = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.button_Open);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.textBox_save);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.button_TurntoPreset);
            this.panel1.Controls.Add(this.button_SaveAsPreset);
            this.panel1.Controls.Add(this.textBox_preset);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button_Far);
            this.panel1.Controls.Add(this.button_Near);
            this.panel1.Controls.Add(this.button_Left);
            this.panel1.Controls.Add(this.button_Down);
            this.panel1.Controls.Add(this.button_Right);
            this.panel1.Controls.Add(this.button_Up);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 262);
            this.panel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "电源";
            // 
            // button_Open
            // 
            this.button_Open.Location = new System.Drawing.Point(59, 111);
            this.button_Open.Name = "button_Open";
            this.button_Open.Size = new System.Drawing.Size(38, 23);
            this.button_Open.TabIndex = 17;
            this.button_Open.Text = "打开";
            this.button_Open.UseVisualStyleBackColor = true;
            this.button_Open.Click += new System.EventHandler(this.button_RemotOption_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(120, 111);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(39, 23);
            this.buttonClose.TabIndex = 16;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.button_RemotOption_Click);
            // 
            // textBox_save
            // 
            this.textBox_save.Location = new System.Drawing.Point(69, 75);
            this.textBox_save.Name = "textBox_save";
            this.textBox_save.Size = new System.Drawing.Size(100, 21);
            this.textBox_save.TabIndex = 14;
            this.textBox_save.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "预置位";
            // 
            // button_TurntoPreset
            // 
            this.button_TurntoPreset.Location = new System.Drawing.Point(184, 41);
            this.button_TurntoPreset.Name = "button_TurntoPreset";
            this.button_TurntoPreset.Size = new System.Drawing.Size(75, 23);
            this.button_TurntoPreset.TabIndex = 12;
            this.button_TurntoPreset.Text = "转到预置位";
            this.button_TurntoPreset.UseVisualStyleBackColor = true;
            this.button_TurntoPreset.Click += new System.EventHandler(this.button_preset_Click);
            // 
            // button_SaveAsPreset
            // 
            this.button_SaveAsPreset.Location = new System.Drawing.Point(184, 75);
            this.button_SaveAsPreset.Name = "button_SaveAsPreset";
            this.button_SaveAsPreset.Size = new System.Drawing.Size(75, 23);
            this.button_SaveAsPreset.TabIndex = 11;
            this.button_SaveAsPreset.Text = "保存预置位";
            this.button_SaveAsPreset.UseVisualStyleBackColor = true;
            this.button_SaveAsPreset.Click += new System.EventHandler(this.button_preset_Click);
            // 
            // textBox_preset
            // 
            this.textBox_preset.Location = new System.Drawing.Point(69, 38);
            this.textBox_preset.Name = "textBox_preset";
            this.textBox_preset.Size = new System.Drawing.Size(100, 21);
            this.textBox_preset.TabIndex = 10;
            this.textBox_preset.Text = "1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(68, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox_chno_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "调焦";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "预置位";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "通道号";
            // 
            // button_Far
            // 
            this.button_Far.Location = new System.Drawing.Point(149, 177);
            this.button_Far.Name = "button_Far";
            this.button_Far.Size = new System.Drawing.Size(23, 23);
            this.button_Far.TabIndex = 5;
            this.button_Far.Text = "+";
            this.button_Far.UseVisualStyleBackColor = true;
            this.button_Far.Click += new System.EventHandler(this.button_RemotOption_Click);
            // 
            // button_Near
            // 
            this.button_Near.Location = new System.Drawing.Point(218, 177);
            this.button_Near.Name = "button_Near";
            this.button_Near.Size = new System.Drawing.Size(23, 23);
            this.button_Near.TabIndex = 4;
            this.button_Near.Text = "－";
            this.button_Near.UseVisualStyleBackColor = true;
            this.button_Near.Click += new System.EventHandler(this.button_RemotOption_Click);
            // 
            // button_Left
            // 
            this.button_Left.Location = new System.Drawing.Point(23, 179);
            this.button_Left.Name = "button_Left";
            this.button_Left.Size = new System.Drawing.Size(23, 23);
            this.button_Left.TabIndex = 3;
            this.button_Left.Text = "←";
            this.button_Left.UseVisualStyleBackColor = true;
            this.button_Left.Click += new System.EventHandler(this.button_RemotOption_Click);
            // 
            // button_Down
            // 
            this.button_Down.Location = new System.Drawing.Point(52, 208);
            this.button_Down.Name = "button_Down";
            this.button_Down.Size = new System.Drawing.Size(23, 23);
            this.button_Down.TabIndex = 2;
            this.button_Down.Text = "↓";
            this.button_Down.UseVisualStyleBackColor = true;
            this.button_Down.Click += new System.EventHandler(this.button_RemotOption_Click);
            // 
            // button_Right
            // 
            this.button_Right.Location = new System.Drawing.Point(80, 180);
            this.button_Right.Name = "button_Right";
            this.button_Right.Size = new System.Drawing.Size(23, 23);
            this.button_Right.TabIndex = 1;
            this.button_Right.Text = "→";
            this.button_Right.UseVisualStyleBackColor = true;
            this.button_Right.Click += new System.EventHandler(this.button_RemotOption_Click);
            // 
            // button_Up
            // 
            this.button_Up.Location = new System.Drawing.Point(52, 152);
            this.button_Up.Name = "button_Up";
            this.button_Up.Size = new System.Drawing.Size(23, 23);
            this.button_Up.TabIndex = 0;
            this.button_Up.Text = "↑";
            this.button_Up.UseVisualStyleBackColor = true;
            this.button_Up.Click += new System.EventHandler(this.button_RemotOption_Click);
            // 
            // Dialog_Image_Adjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_Image_Adjust";
            this.Text = "摄像头远程调节";
            this.Load += new System.EventHandler(this.Dialog_Image_Adjust_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Left;
        private System.Windows.Forms.Button button_Down;
        private System.Windows.Forms.Button button_Right;
        private System.Windows.Forms.Button button_Up;
        private System.Windows.Forms.Button button_Far;
        private System.Windows.Forms.Button button_Near;
        private System.Windows.Forms.TextBox textBox_save;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_TurntoPreset;
        private System.Windows.Forms.Button button_SaveAsPreset;
        private System.Windows.Forms.TextBox textBox_preset;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Open;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label5;
    }
}