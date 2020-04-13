namespace GridBackGround.Forms
{
    partial class Dialog_Con_ID
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
            this.textBox_Original_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.textBox_NEW_CMD_ID = new System.Windows.Forms.TextBox();
            this.textBox_Component_ID = new System.Windows.Forms.TextBox();
            this.checkBox_NEW_CMD_ID = new System.Windows.Forms.CheckBox();
            this.checkBox_Component_ID = new System.Windows.Forms.CheckBox();
            this.label_Component_ID = new System.Windows.Forms.Label();
            this.label_ID = new System.Windows.Forms.Label();
            this.label_OrgID = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_OrgID);
            this.panel1.Controls.Add(this.label_ID);
            this.panel1.Controls.Add(this.label_Component_ID);
            this.panel1.Controls.Add(this.textBox_Original_ID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button_Cancel);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Controls.Add(this.textBox_NEW_CMD_ID);
            this.panel1.Controls.Add(this.textBox_Component_ID);
            this.panel1.Controls.Add(this.checkBox_NEW_CMD_ID);
            this.panel1.Controls.Add(this.checkBox_Component_ID);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 183);
            this.panel1.TabIndex = 0;
            // 
            // textBox_Original_ID
            // 
            this.textBox_Original_ID.Location = new System.Drawing.Point(109, 111);
            this.textBox_Original_ID.Name = "textBox_Original_ID";
            this.textBox_Original_ID.Size = new System.Drawing.Size(124, 21);
            this.textBox_Original_ID.TabIndex = 7;
            this.textBox_Original_ID.TextChanged += new System.EventHandler(this.textBox_Original_ID_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "原始ID";
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(154, 135);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 5;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(34, 136);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 4;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // textBox_NEW_CMD_ID
            // 
            this.textBox_NEW_CMD_ID.Location = new System.Drawing.Point(109, 79);
            this.textBox_NEW_CMD_ID.Name = "textBox_NEW_CMD_ID";
            this.textBox_NEW_CMD_ID.Size = new System.Drawing.Size(125, 21);
            this.textBox_NEW_CMD_ID.TabIndex = 3;
            this.textBox_NEW_CMD_ID.TextChanged += new System.EventHandler(this.textBox_NEW_CMD_ID_TextChanged);
            // 
            // textBox_Component_ID
            // 
            this.textBox_Component_ID.Location = new System.Drawing.Point(109, 40);
            this.textBox_Component_ID.Name = "textBox_Component_ID";
            this.textBox_Component_ID.Size = new System.Drawing.Size(124, 21);
            this.textBox_Component_ID.TabIndex = 2;
            this.textBox_Component_ID.TextChanged += new System.EventHandler(this.textBox_Component_ID_TextChanged);
            // 
            // checkBox_NEW_CMD_ID
            // 
            this.checkBox_NEW_CMD_ID.AutoSize = true;
            this.checkBox_NEW_CMD_ID.Location = new System.Drawing.Point(30, 81);
            this.checkBox_NEW_CMD_ID.Name = "checkBox_NEW_CMD_ID";
            this.checkBox_NEW_CMD_ID.Size = new System.Drawing.Size(60, 16);
            this.checkBox_NEW_CMD_ID.TabIndex = 1;
            this.checkBox_NEW_CMD_ID.Text = "装置ID";
            this.checkBox_NEW_CMD_ID.UseVisualStyleBackColor = true;
            // 
            // checkBox_Component_ID
            // 
            this.checkBox_Component_ID.AutoSize = true;
            this.checkBox_Component_ID.Location = new System.Drawing.Point(29, 40);
            this.checkBox_Component_ID.Name = "checkBox_Component_ID";
            this.checkBox_Component_ID.Size = new System.Drawing.Size(84, 16);
            this.checkBox_Component_ID.TabIndex = 0;
            this.checkBox_Component_ID.Text = "被测设备ID";
            this.checkBox_Component_ID.UseVisualStyleBackColor = true;
            // 
            // label_Component_ID
            // 
            this.label_Component_ID.AutoSize = true;
            this.label_Component_ID.Location = new System.Drawing.Point(242, 43);
            this.label_Component_ID.Name = "label_Component_ID";
            this.label_Component_ID.Size = new System.Drawing.Size(0, 12);
            this.label_Component_ID.TabIndex = 8;
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Location = new System.Drawing.Point(251, 84);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(0, 12);
            this.label_ID.TabIndex = 9;
            // 
            // label_OrgID
            // 
            this.label_OrgID.AutoSize = true;
            this.label_OrgID.Location = new System.Drawing.Point(251, 120);
            this.label_OrgID.Name = "label_OrgID";
            this.label_OrgID.Size = new System.Drawing.Size(0, 12);
            this.label_OrgID.TabIndex = 10;
            // 
            // Dialog_Con_ID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 183);
            this.Controls.Add(this.panel1);
            this.Name = "Dialog_Con_ID";
            this.Text = "状态监测装置 ID设置";
            this.Load += new System.EventHandler(this.Dialog_ID_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.TextBox textBox_NEW_CMD_ID;
        private System.Windows.Forms.TextBox textBox_Component_ID;
        private System.Windows.Forms.CheckBox checkBox_NEW_CMD_ID;
        private System.Windows.Forms.CheckBox checkBox_Component_ID;
        private System.Windows.Forms.TextBox textBox_Original_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_OrgID;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.Label label_Component_ID;
    }
}