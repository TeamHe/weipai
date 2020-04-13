namespace GridBackGround.Forms
{
    partial class Dialog_Config
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
            this.button_Browse = new System.Windows.Forms.Button();
            this.textBox_PicPath = new System.Windows.Forms.TextBox();
            this.label_PicPath = new System.Windows.Forms.Label();
            this.textBox_ReportNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_PacketNum = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.textBox_Web_Port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_CMD_Port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Browse);
            this.panel1.Controls.Add(this.textBox_PicPath);
            this.panel1.Controls.Add(this.label_PicPath);
            this.panel1.Controls.Add(this.textBox_ReportNum);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox_PacketNum);
            this.panel1.Controls.Add(this.label);
            this.panel1.Controls.Add(this.textBox_Web_Port);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_CMD_Port);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Controls.Add(this.button_Cancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 271);
            this.panel1.TabIndex = 0;
            // 
            // button_Browse
            // 
            this.button_Browse.Location = new System.Drawing.Point(354, 144);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(31, 23);
            this.button_Browse.TabIndex = 18;
            this.button_Browse.Text = "...";
            this.button_Browse.UseVisualStyleBackColor = true;
            this.button_Browse.Click += new System.EventHandler(this.button_Browse_Click);
            // 
            // textBox_PicPath
            // 
            this.textBox_PicPath.Location = new System.Drawing.Point(82, 144);
            this.textBox_PicPath.Name = "textBox_PicPath";
            this.textBox_PicPath.Size = new System.Drawing.Size(266, 21);
            this.textBox_PicPath.TabIndex = 17;
            // 
            // label_PicPath
            // 
            this.label_PicPath.AutoSize = true;
            this.label_PicPath.Location = new System.Drawing.Point(23, 148);
            this.label_PicPath.Name = "label_PicPath";
            this.label_PicPath.Size = new System.Drawing.Size(53, 12);
            this.label_PicPath.TabIndex = 16;
            this.label_PicPath.Text = "图片路径";
            // 
            // textBox_ReportNum
            // 
            this.textBox_ReportNum.Location = new System.Drawing.Point(281, 75);
            this.textBox_ReportNum.Name = "textBox_ReportNum";
            this.textBox_ReportNum.Size = new System.Drawing.Size(100, 21);
            this.textBox_ReportNum.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "记录数目";
            // 
            // textBox_PacketNum
            // 
            this.textBox_PacketNum.Location = new System.Drawing.Point(280, 37);
            this.textBox_PacketNum.Name = "textBox_PacketNum";
            this.textBox_PacketNum.Size = new System.Drawing.Size(100, 21);
            this.textBox_PacketNum.TabIndex = 13;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(212, 41);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(53, 12);
            this.label.TabIndex = 12;
            this.label.Text = "报文数目";
            // 
            // textBox_Web_Port
            // 
            this.textBox_Web_Port.Location = new System.Drawing.Point(82, 75);
            this.textBox_Web_Port.Name = "textBox_Web_Port";
            this.textBox_Web_Port.Size = new System.Drawing.Size(100, 21);
            this.textBox_Web_Port.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "WEB端口";
            // 
            // textBox_CMD_Port
            // 
            this.textBox_CMD_Port.Location = new System.Drawing.Point(82, 37);
            this.textBox_CMD_Port.Name = "textBox_CMD_Port";
            this.textBox_CMD_Port.Size = new System.Drawing.Size(100, 21);
            this.textBox_CMD_Port.TabIndex = 9;
            this.textBox_CMD_Port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "装置端口";
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(35, 219);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 7;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(135, 219);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 6;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // Dialog_Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 271);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_Config";
            this.Text = "程序配置";
            this.Load += new System.EventHandler(this.Dialog_Config_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_ReportNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_PacketNum;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox textBox_Web_Port;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_CMD_Port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.TextBox textBox_PicPath;
        private System.Windows.Forms.Label label_PicPath;
    }
}