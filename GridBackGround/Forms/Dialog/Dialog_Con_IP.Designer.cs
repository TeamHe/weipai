namespace GridBackGround.Forms
{
    partial class Dialog_Con_IP
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
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancle = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox_domain = new System.Windows.Forms.CheckBox();
            this.textBox_domain = new System.Windows.Forms.TextBox();
            this.linkLabel_GetIP = new System.Windows.Forms.LinkLabel();
            this.checkBox_Port = new System.Windows.Forms.CheckBox();
            this.checkBox_IP = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(78, 41);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(124, 21);
            this.textBox_IP.TabIndex = 3;
            this.textBox_IP.Text = "192.168.0.1";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(78, 74);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(124, 21);
            this.textBox_Port.TabIndex = 4;
            this.textBox_Port.Text = "2046";
            this.textBox_Port.TextChanged += new System.EventHandler(this.textBox_Port_TextChanged);
            this.textBox_Port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Port_KeyPress);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(32, 169);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 6;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancle
            // 
            this.button_Cancle.Location = new System.Drawing.Point(147, 169);
            this.button_Cancle.Name = "button_Cancle";
            this.button_Cancle.Size = new System.Drawing.Size(75, 23);
            this.button_Cancle.TabIndex = 7;
            this.button_Cancle.Text = "取消";
            this.button_Cancle.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox_domain);
            this.panel1.Controls.Add(this.textBox_domain);
            this.panel1.Controls.Add(this.linkLabel_GetIP);
            this.panel1.Controls.Add(this.checkBox_Port);
            this.panel1.Controls.Add(this.checkBox_IP);
            this.panel1.Controls.Add(this.button_Cancle);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Controls.Add(this.textBox_Port);
            this.panel1.Controls.Add(this.textBox_IP);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(281, 215);
            this.panel1.TabIndex = 0;
            // 
            // checkBox_domain
            // 
            this.checkBox_domain.AutoSize = true;
            this.checkBox_domain.Location = new System.Drawing.Point(10, 115);
            this.checkBox_domain.Name = "checkBox_domain";
            this.checkBox_domain.Size = new System.Drawing.Size(48, 16);
            this.checkBox_domain.TabIndex = 14;
            this.checkBox_domain.Text = "域名";
            this.checkBox_domain.UseVisualStyleBackColor = true;
            // 
            // textBox_domain
            // 
            this.textBox_domain.Location = new System.Drawing.Point(78, 111);
            this.textBox_domain.Name = "textBox_domain";
            this.textBox_domain.Size = new System.Drawing.Size(124, 21);
            this.textBox_domain.TabIndex = 13;
            // 
            // linkLabel_GetIP
            // 
            this.linkLabel_GetIP.AutoSize = true;
            this.linkLabel_GetIP.Location = new System.Drawing.Point(210, 47);
            this.linkLabel_GetIP.Name = "linkLabel_GetIP";
            this.linkLabel_GetIP.Size = new System.Drawing.Size(65, 12);
            this.linkLabel_GetIP.TabIndex = 12;
            this.linkLabel_GetIP.TabStop = true;
            this.linkLabel_GetIP.Text = "获取外网IP";
            this.linkLabel_GetIP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_GetIP_LinkClicked);
            // 
            // checkBox_Port
            // 
            this.checkBox_Port.AutoSize = true;
            this.checkBox_Port.Checked = true;
            this.checkBox_Port.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Port.Location = new System.Drawing.Point(10, 78);
            this.checkBox_Port.Name = "checkBox_Port";
            this.checkBox_Port.Size = new System.Drawing.Size(60, 16);
            this.checkBox_Port.TabIndex = 11;
            this.checkBox_Port.Text = "端口号";
            this.checkBox_Port.UseVisualStyleBackColor = true;
            // 
            // checkBox_IP
            // 
            this.checkBox_IP.AutoSize = true;
            this.checkBox_IP.Checked = true;
            this.checkBox_IP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_IP.Location = new System.Drawing.Point(10, 42);
            this.checkBox_IP.Name = "checkBox_IP";
            this.checkBox_IP.Size = new System.Drawing.Size(60, 16);
            this.checkBox_IP.TabIndex = 10;
            this.checkBox_IP.Text = "IP地址";
            this.checkBox_IP.UseVisualStyleBackColor = true;
            // 
            // Dialog_Con_IP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 215);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_Con_IP";
            this.Text = "上位机信息查询/设置";
            this.Load += new System.EventHandler(this.Dialog_Con_IP_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox_Port;
        private System.Windows.Forms.CheckBox checkBox_IP;
        private System.Windows.Forms.LinkLabel linkLabel_GetIP;
        private System.Windows.Forms.CheckBox checkBox_domain;
        private System.Windows.Forms.TextBox textBox_domain;
    }
}