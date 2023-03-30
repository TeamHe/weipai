namespace GridBackGround.Forms.Dialogs_nw
{
    partial class UserControl_nw_img_para
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_saturation = new System.Windows.Forms.Label();
            this.label_Contrast = new System.Windows.Forms.Label();
            this.label_brightness = new System.Windows.Forms.Label();
            this.label_Resolution = new System.Windows.Forms.Label();
            this.label_color = new System.Windows.Forms.Label();
            this.textBox_Contrast = new System.Windows.Forms.TextBox();
            this.textBox_Saturation = new System.Windows.Forms.TextBox();
            this.textBox_Luminance = new System.Windows.Forms.TextBox();
            this.comboBox_Resolution = new System.Windows.Forms.ComboBox();
            this.comboBox_Color_Select = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label_saturation
            // 
            this.label_saturation.AutoSize = true;
            this.label_saturation.Location = new System.Drawing.Point(14, 160);
            this.label_saturation.Name = "label_saturation";
            this.label_saturation.Size = new System.Drawing.Size(41, 12);
            this.label_saturation.TabIndex = 34;
            this.label_saturation.Text = "饱和度";
            // 
            // label_Contrast
            // 
            this.label_Contrast.AutoSize = true;
            this.label_Contrast.Location = new System.Drawing.Point(14, 123);
            this.label_Contrast.Name = "label_Contrast";
            this.label_Contrast.Size = new System.Drawing.Size(41, 12);
            this.label_Contrast.TabIndex = 33;
            this.label_Contrast.Text = "对比度";
            // 
            // label_brightness
            // 
            this.label_brightness.AutoSize = true;
            this.label_brightness.Location = new System.Drawing.Point(16, 87);
            this.label_brightness.Name = "label_brightness";
            this.label_brightness.Size = new System.Drawing.Size(29, 12);
            this.label_brightness.TabIndex = 32;
            this.label_brightness.Text = "亮度";
            // 
            // label_Resolution
            // 
            this.label_Resolution.AutoSize = true;
            this.label_Resolution.Location = new System.Drawing.Point(14, 51);
            this.label_Resolution.Name = "label_Resolution";
            this.label_Resolution.Size = new System.Drawing.Size(65, 12);
            this.label_Resolution.TabIndex = 31;
            this.label_Resolution.Text = "图像分辨率";
            // 
            // label_color
            // 
            this.label_color.AutoSize = true;
            this.label_color.Location = new System.Drawing.Point(14, 15);
            this.label_color.Name = "label_color";
            this.label_color.Size = new System.Drawing.Size(53, 12);
            this.label_color.TabIndex = 30;
            this.label_color.Text = "色彩选择";
            // 
            // textBox_Contrast
            // 
            this.textBox_Contrast.Location = new System.Drawing.Point(85, 116);
            this.textBox_Contrast.Name = "textBox_Contrast";
            this.textBox_Contrast.Size = new System.Drawing.Size(161, 21);
            this.textBox_Contrast.TabIndex = 29;
            this.textBox_Contrast.Text = "50";
            this.textBox_Contrast.TextChanged += new System.EventHandler(this.OnTextChaned);
            this.textBox_Contrast.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Integer_KeyPress);
            // 
            // textBox_Saturation
            // 
            this.textBox_Saturation.Location = new System.Drawing.Point(85, 152);
            this.textBox_Saturation.Name = "textBox_Saturation";
            this.textBox_Saturation.Size = new System.Drawing.Size(161, 21);
            this.textBox_Saturation.TabIndex = 28;
            this.textBox_Saturation.Text = "50";
            this.textBox_Saturation.TextChanged += new System.EventHandler(this.OnTextChaned);
            this.textBox_Saturation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Integer_KeyPress);
            // 
            // textBox_Luminance
            // 
            this.textBox_Luminance.Location = new System.Drawing.Point(85, 80);
            this.textBox_Luminance.Name = "textBox_Luminance";
            this.textBox_Luminance.Size = new System.Drawing.Size(161, 21);
            this.textBox_Luminance.TabIndex = 27;
            this.textBox_Luminance.Text = "50";
            this.textBox_Luminance.TextChanged += new System.EventHandler(this.OnTextChaned);
            this.textBox_Luminance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Integer_KeyPress);
            // 
            // comboBox_Resolution
            // 
            this.comboBox_Resolution.FormattingEnabled = true;
            this.comboBox_Resolution.Location = new System.Drawing.Point(85, 45);
            this.comboBox_Resolution.Name = "comboBox_Resolution";
            this.comboBox_Resolution.Size = new System.Drawing.Size(161, 20);
            this.comboBox_Resolution.TabIndex = 26;
            // 
            // comboBox_Color_Select
            // 
            this.comboBox_Color_Select.FormattingEnabled = true;
            this.comboBox_Color_Select.Location = new System.Drawing.Point(85, 10);
            this.comboBox_Color_Select.Name = "comboBox_Color_Select";
            this.comboBox_Color_Select.Size = new System.Drawing.Size(161, 20);
            this.comboBox_Color_Select.TabIndex = 25;
            // 
            // UserControl_nw_img_para
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_saturation);
            this.Controls.Add(this.label_Contrast);
            this.Controls.Add(this.label_brightness);
            this.Controls.Add(this.label_Resolution);
            this.Controls.Add(this.label_color);
            this.Controls.Add(this.textBox_Contrast);
            this.Controls.Add(this.textBox_Saturation);
            this.Controls.Add(this.textBox_Luminance);
            this.Controls.Add(this.comboBox_Resolution);
            this.Controls.Add(this.comboBox_Color_Select);
            this.Name = "UserControl_nw_img_para";
            this.Size = new System.Drawing.Size(268, 191);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_saturation;
        private System.Windows.Forms.Label label_Contrast;
        private System.Windows.Forms.Label label_brightness;
        private System.Windows.Forms.Label label_Resolution;
        private System.Windows.Forms.Label label_color;
        private System.Windows.Forms.TextBox textBox_Contrast;
        private System.Windows.Forms.TextBox textBox_Saturation;
        private System.Windows.Forms.TextBox textBox_Luminance;
        private System.Windows.Forms.ComboBox comboBox_Resolution;
        private System.Windows.Forms.ComboBox comboBox_Color_Select;
    }
}
