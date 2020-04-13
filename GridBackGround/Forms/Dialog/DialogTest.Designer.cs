namespace GridBackGround.Forms.Dialog
{
    partial class DialogTest
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
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.labelDataBaseType = new System.Windows.Forms.Label();
            this.comboBox_DBType = new System.Windows.Forms.ComboBox();
            this.panel_DB = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.panel_DB);
            this.panel1.Controls.Add(this.comboBox_DBType);
            this.panel1.Controls.Add(this.labelDataBaseType);
            this.panel1.Controls.Add(this.button_Cancel);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 344);
            this.panel1.TabIndex = 0;
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.Location = new System.Drawing.Point(211, 309);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 0;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(294, 309);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 1;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // labelDataBaseType
            // 
            this.labelDataBaseType.AutoSize = true;
            this.labelDataBaseType.Location = new System.Drawing.Point(23, 24);
            this.labelDataBaseType.Name = "labelDataBaseType";
            this.labelDataBaseType.Size = new System.Drawing.Size(65, 12);
            this.labelDataBaseType.TabIndex = 2;
            this.labelDataBaseType.Text = "数据库类型";
            // 
            // comboBox_DBType
            // 
            this.comboBox_DBType.FormattingEnabled = true;
            this.comboBox_DBType.Items.AddRange(new object[] {
            "Access",
            "Mysql"});
            this.comboBox_DBType.Location = new System.Drawing.Point(105, 21);
            this.comboBox_DBType.Name = "comboBox_DBType";
            this.comboBox_DBType.Size = new System.Drawing.Size(121, 20);
            this.comboBox_DBType.TabIndex = 3;
            this.comboBox_DBType.SelectedIndexChanged += new System.EventHandler(this.comboBox_DBType_SelectedIndexChanged);
            // 
            // panel_DB
            // 
            this.panel_DB.Location = new System.Drawing.Point(3, 47);
            this.panel_DB.Name = "panel_DB";
            this.panel_DB.Size = new System.Drawing.Size(387, 256);
            this.panel_DB.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "测试连接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonTestCon_Click);
            // 
            // DialogTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 344);
            this.Controls.Add(this.panel1);
            this.Name = "DialogTest";
            this.Text = "数据库测试";
            this.Load += new System.EventHandler(this.DialogTest_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox_DBType;
        private System.Windows.Forms.Label labelDataBaseType;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Panel panel_DB;
        private System.Windows.Forms.Button button1;
    }
}