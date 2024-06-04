namespace GridBackGround.Forms
{
    partial class Dialog_Image_Model
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
            this.checkBox_亮度 = new System.Windows.Forms.CheckBox();
            this.checkBox_对比度 = new System.Windows.Forms.CheckBox();
            this.checkBox_饱和度 = new System.Windows.Forms.CheckBox();
            this.checkBox_图像分辨率 = new System.Windows.Forms.CheckBox();
            this.checkBox_色彩选择 = new System.Windows.Forms.CheckBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.textBox_Contrast = new System.Windows.Forms.TextBox();
            this.textBox_Saturation = new System.Windows.Forms.TextBox();
            this.textBox_Luminance = new System.Windows.Forms.TextBox();
            this.comboBox_Resolution = new System.Windows.Forms.ComboBox();
            this.comboBox_Color_Select = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox_亮度);
            this.panel1.Controls.Add(this.checkBox_对比度);
            this.panel1.Controls.Add(this.checkBox_饱和度);
            this.panel1.Controls.Add(this.checkBox_图像分辨率);
            this.panel1.Controls.Add(this.checkBox_色彩选择);
            this.panel1.Controls.Add(this.button_Cancel);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Controls.Add(this.textBox_Contrast);
            this.panel1.Controls.Add(this.textBox_Saturation);
            this.panel1.Controls.Add(this.textBox_Luminance);
            this.panel1.Controls.Add(this.comboBox_Resolution);
            this.panel1.Controls.Add(this.comboBox_Color_Select);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(275, 254);
            this.panel1.TabIndex = 1;
            // 
            // checkBox_亮度
            // 
            this.checkBox_亮度.AutoSize = true;
            this.checkBox_亮度.Location = new System.Drawing.Point(12, 97);
            this.checkBox_亮度.Name = "checkBox_亮度";
            this.checkBox_亮度.Size = new System.Drawing.Size(48, 16);
            this.checkBox_亮度.TabIndex = 19;
            this.checkBox_亮度.Text = "亮度";
            this.checkBox_亮度.UseVisualStyleBackColor = true;
            // 
            // checkBox_对比度
            // 
            this.checkBox_对比度.AutoSize = true;
            this.checkBox_对比度.Location = new System.Drawing.Point(12, 133);
            this.checkBox_对比度.Name = "checkBox_对比度";
            this.checkBox_对比度.Size = new System.Drawing.Size(60, 16);
            this.checkBox_对比度.TabIndex = 18;
            this.checkBox_对比度.Text = "对比度";
            this.checkBox_对比度.UseVisualStyleBackColor = true;
            // 
            // checkBox_饱和度
            // 
            this.checkBox_饱和度.AutoSize = true;
            this.checkBox_饱和度.Location = new System.Drawing.Point(12, 169);
            this.checkBox_饱和度.Name = "checkBox_饱和度";
            this.checkBox_饱和度.Size = new System.Drawing.Size(60, 16);
            this.checkBox_饱和度.TabIndex = 17;
            this.checkBox_饱和度.Text = "饱和度";
            this.checkBox_饱和度.UseVisualStyleBackColor = true;
            // 
            // checkBox_图像分辨率
            // 
            this.checkBox_图像分辨率.AutoSize = true;
            this.checkBox_图像分辨率.Location = new System.Drawing.Point(12, 61);
            this.checkBox_图像分辨率.Name = "checkBox_图像分辨率";
            this.checkBox_图像分辨率.Size = new System.Drawing.Size(84, 16);
            this.checkBox_图像分辨率.TabIndex = 13;
            this.checkBox_图像分辨率.Text = "图像分辨率";
            this.checkBox_图像分辨率.UseVisualStyleBackColor = true;
            // 
            // checkBox_色彩选择
            // 
            this.checkBox_色彩选择.AutoSize = true;
            this.checkBox_色彩选择.Location = new System.Drawing.Point(12, 25);
            this.checkBox_色彩选择.Name = "checkBox_色彩选择";
            this.checkBox_色彩选择.Size = new System.Drawing.Size(72, 16);
            this.checkBox_色彩选择.TabIndex = 12;
            this.checkBox_色彩选择.Text = "色彩选择";
            this.checkBox_色彩选择.UseVisualStyleBackColor = true;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(167, 215);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 11;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(35, 216);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 10;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // textBox_Contrast
            // 
            this.textBox_Contrast.Location = new System.Drawing.Point(102, 127);
            this.textBox_Contrast.Name = "textBox_Contrast";
            this.textBox_Contrast.Size = new System.Drawing.Size(100, 21);
            this.textBox_Contrast.TabIndex = 9;
            this.textBox_Contrast.Text = "50";
            // 
            // textBox_Saturation
            // 
            this.textBox_Saturation.Location = new System.Drawing.Point(102, 163);
            this.textBox_Saturation.Name = "textBox_Saturation";
            this.textBox_Saturation.Size = new System.Drawing.Size(100, 21);
            this.textBox_Saturation.TabIndex = 8;
            this.textBox_Saturation.Text = "50";
            // 
            // textBox_Luminance
            // 
            this.textBox_Luminance.Location = new System.Drawing.Point(102, 91);
            this.textBox_Luminance.Name = "textBox_Luminance";
            this.textBox_Luminance.Size = new System.Drawing.Size(100, 21);
            this.textBox_Luminance.TabIndex = 7;
            this.textBox_Luminance.Text = "50";
            // 
            // comboBox_Resolution
            // 
            this.comboBox_Resolution.FormattingEnabled = true;
            this.comboBox_Resolution.Items.AddRange(new object[] {
            "①320 X 240 ",
            "②640 X 480 ",
            "③704 X 576",
            "④720 X 480（标清）",
            "⑤1280 X 720（720P）",
            "⑥1920 X 1080（1080P）",
            "⑦2592 × 1944"});
            this.comboBox_Resolution.Location = new System.Drawing.Point(102, 56);
            this.comboBox_Resolution.Name = "comboBox_Resolution";
            this.comboBox_Resolution.Size = new System.Drawing.Size(161, 20);
            this.comboBox_Resolution.TabIndex = 3;
            // 
            // comboBox_Color_Select
            // 
            this.comboBox_Color_Select.FormattingEnabled = true;
            this.comboBox_Color_Select.Items.AddRange(new object[] {
            "黑白",
            "彩色"});
            this.comboBox_Color_Select.Location = new System.Drawing.Point(102, 21);
            this.comboBox_Color_Select.Name = "comboBox_Color_Select";
            this.comboBox_Color_Select.Size = new System.Drawing.Size(161, 20);
            this.comboBox_Color_Select.TabIndex = 1;
            // 
            // Dialog_Image_Model
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 254);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_Image_Model";
            this.Text = "图像采集参数设置";
            this.Load += new System.EventHandler(this.Dialog_Image_Para_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.TextBox textBox_Contrast;
        private System.Windows.Forms.TextBox textBox_Saturation;
        private System.Windows.Forms.TextBox textBox_Luminance;
        private System.Windows.Forms.ComboBox comboBox_Resolution;
        private System.Windows.Forms.ComboBox comboBox_Color_Select;
        private System.Windows.Forms.CheckBox checkBox_亮度;
        private System.Windows.Forms.CheckBox checkBox_对比度;
        private System.Windows.Forms.CheckBox checkBox_饱和度;
        private System.Windows.Forms.CheckBox checkBox_图像分辨率;
        private System.Windows.Forms.CheckBox checkBox_色彩选择;
    }
}