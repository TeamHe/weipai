namespace GridBackGround.Forms
{
    partial class Dialog_Con_NA
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
            this.textBox_IMEI = new System.Windows.Forms.TextBox();
            this.textBox_GateWay = new System.Windows.Forms.TextBox();
            this.textBox_SubNetMask = new System.Windows.Forms.TextBox();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.checkBox_IMEI = new System.Windows.Forms.CheckBox();
            this.checkBox3_GateWay = new System.Windows.Forms.CheckBox();
            this.checkBox_SubNetMask = new System.Windows.Forms.CheckBox();
            this.checkBox_IP = new System.Windows.Forms.CheckBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox_IMEI);
            this.panel1.Controls.Add(this.textBox_GateWay);
            this.panel1.Controls.Add(this.textBox_SubNetMask);
            this.panel1.Controls.Add(this.textBox_IP);
            this.panel1.Controls.Add(this.checkBox_IMEI);
            this.panel1.Controls.Add(this.checkBox3_GateWay);
            this.panel1.Controls.Add(this.checkBox_SubNetMask);
            this.panel1.Controls.Add(this.checkBox_IP);
            this.panel1.Controls.Add(this.button_Cancel);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 202);
            this.panel1.TabIndex = 0;
            // 
            // textBox_IMEI
            // 
            this.textBox_IMEI.Location = new System.Drawing.Point(121, 106);
            this.textBox_IMEI.Name = "textBox_IMEI";
            this.textBox_IMEI.Size = new System.Drawing.Size(131, 21);
            this.textBox_IMEI.TabIndex = 9;
            this.textBox_IMEI.TextChanged += new System.EventHandler(this.textBox_IMEI_TextChanged);
            // 
            // textBox_GateWay
            // 
            this.textBox_GateWay.Location = new System.Drawing.Point(121, 78);
            this.textBox_GateWay.Name = "textBox_GateWay";
            this.textBox_GateWay.Size = new System.Drawing.Size(131, 21);
            this.textBox_GateWay.TabIndex = 8;
            // 
            // textBox_SubNetMask
            // 
            this.textBox_SubNetMask.Location = new System.Drawing.Point(121, 50);
            this.textBox_SubNetMask.Name = "textBox_SubNetMask";
            this.textBox_SubNetMask.Size = new System.Drawing.Size(131, 21);
            this.textBox_SubNetMask.TabIndex = 7;
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(121, 22);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(131, 21);
            this.textBox_IP.TabIndex = 6;
            // 
            // checkBox_IMEI
            // 
            this.checkBox_IMEI.AutoSize = true;
            this.checkBox_IMEI.Location = new System.Drawing.Point(33, 106);
            this.checkBox_IMEI.Name = "checkBox_IMEI";
            this.checkBox_IMEI.Size = new System.Drawing.Size(72, 16);
            this.checkBox_IMEI.TabIndex = 5;
            this.checkBox_IMEI.Text = "手机串号";
            this.checkBox_IMEI.UseVisualStyleBackColor = true;
            // 
            // checkBox3_GateWay
            // 
            this.checkBox3_GateWay.AutoSize = true;
            this.checkBox3_GateWay.Location = new System.Drawing.Point(33, 79);
            this.checkBox3_GateWay.Name = "checkBox3_GateWay";
            this.checkBox3_GateWay.Size = new System.Drawing.Size(48, 16);
            this.checkBox3_GateWay.TabIndex = 4;
            this.checkBox3_GateWay.Text = "网关";
            this.checkBox3_GateWay.UseVisualStyleBackColor = true;
            // 
            // checkBox_SubNetMask
            // 
            this.checkBox_SubNetMask.AutoSize = true;
            this.checkBox_SubNetMask.Location = new System.Drawing.Point(33, 52);
            this.checkBox_SubNetMask.Name = "checkBox_SubNetMask";
            this.checkBox_SubNetMask.Size = new System.Drawing.Size(72, 16);
            this.checkBox_SubNetMask.TabIndex = 3;
            this.checkBox_SubNetMask.Text = "子网掩码";
            this.checkBox_SubNetMask.UseVisualStyleBackColor = true;
            // 
            // checkBox_IP
            // 
            this.checkBox_IP.AutoSize = true;
            this.checkBox_IP.Location = new System.Drawing.Point(33, 25);
            this.checkBox_IP.Name = "checkBox_IP";
            this.checkBox_IP.Size = new System.Drawing.Size(60, 16);
            this.checkBox_IP.TabIndex = 2;
            this.checkBox_IP.Text = "IP地址";
            this.checkBox_IP.UseVisualStyleBackColor = true;
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(149, 160);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 1;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(26, 160);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 0;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 10;
            // 
            // Dialog_Con_NA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 202);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_Con_NA";
            this.Text = "状态监测装置网络适配器设置";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_IMEI;
        private System.Windows.Forms.TextBox textBox_GateWay;
        private System.Windows.Forms.TextBox textBox_SubNetMask;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.CheckBox checkBox_IMEI;
        private System.Windows.Forms.CheckBox checkBox3_GateWay;
        private System.Windows.Forms.CheckBox checkBox_SubNetMask;
        private System.Windows.Forms.CheckBox checkBox_IP;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label1;
    }
}