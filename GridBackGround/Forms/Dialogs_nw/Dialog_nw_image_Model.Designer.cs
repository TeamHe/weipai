using ResModel.nw;

namespace GridBackGround.Forms.Dialogs_nw
{
    partial class Dialog_nw_image_para
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
            nw_img_para nw_img_para1 = new nw_img_para();
           nw_img_para nw_img_para2 = new nw_img_para();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.userControl_nw_img_para1 = new GridBackGround.Forms.Dialogs_nw.UserControl_nw_img_para();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.userControl_nw_img_para2 = new GridBackGround.Forms.Dialogs_nw.UserControl_nw_img_para();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label_password = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 296);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(277, 219);
            this.tabControl1.TabIndex = 28;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.userControl_nw_img_para1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(269, 193);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "通道1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // userControl_nw_img_para1
            // 
            this.userControl_nw_img_para1.Dock = System.Windows.Forms.DockStyle.Fill;
            nw_img_para1.Brightness = 50;
            nw_img_para1.Color =nw_img_para.EColor.Black;
            nw_img_para1.Contrast = 50;
            nw_img_para1.Resolution =nw_img_para.EResolution.R_320_240;
            nw_img_para1.Saturation = 50;
            this.userControl_nw_img_para1.Image_para = nw_img_para1;
            this.userControl_nw_img_para1.Location = new System.Drawing.Point(3, 3);
            this.userControl_nw_img_para1.Name = "userControl_nw_img_para1";
            this.userControl_nw_img_para1.Size = new System.Drawing.Size(263, 187);
            this.userControl_nw_img_para1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.userControl_nw_img_para2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(269, 193);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "通道2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // userControl_nw_img_para2
            // 
            this.userControl_nw_img_para2.Dock = System.Windows.Forms.DockStyle.Fill;
            nw_img_para2.Brightness = 50;
            nw_img_para2.Color = nw_img_para.EColor.Black;
            nw_img_para2.Contrast = 50;
            nw_img_para2.Resolution = nw_img_para.EResolution.R_320_240;
            nw_img_para2.Saturation = 50;
            this.userControl_nw_img_para2.Image_para = nw_img_para2;
            this.userControl_nw_img_para2.Location = new System.Drawing.Point(3, 3);
            this.userControl_nw_img_para2.Name = "userControl_nw_img_para2";
            this.userControl_nw_img_para2.Size = new System.Drawing.Size(263, 187);
            this.userControl_nw_img_para2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox_password);
            this.panel2.Controls.Add(this.button_Cancel);
            this.panel2.Controls.Add(this.label_password);
            this.panel2.Controls.Add(this.button_OK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 219);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(277, 77);
            this.panel2.TabIndex = 27;
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(93, 6);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(160, 21);
            this.textBox_password.TabIndex = 25;
            this.textBox_password.Text = "1234";
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(144, 39);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 11;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(23, 12);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(29, 12);
            this.label_password.TabIndex = 26;
            this.label_password.Text = "密码";
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(25, 39);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 10;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // Dialog_nw_image_para
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 296);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_nw_image_para";
            this.Text = "图像采集参数设置";
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private UserControl_nw_img_para userControl_nw_img_para1;
        private UserControl_nw_img_para userControl_nw_img_para2;
    }
}