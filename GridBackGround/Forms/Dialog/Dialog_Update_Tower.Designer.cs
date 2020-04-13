namespace GridBackGround.Forms.Dialog
{
    partial class Dialog_Update_Tower
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
            this.panel_Tower = new System.Windows.Forms.Panel();
            this.label_orgLen = new System.Windows.Forms.Label();
            this.textBox_OrgID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_CompLen = new System.Windows.Forms.Label();
            this.label_CmdLen = new System.Windows.Forms.Label();
            this.comboBox_EquType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_CompID = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.textBox_StationID = new System.Windows.Forms.TextBox();
            this.textBox_T_StationName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_Update = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView_T_Station = new System.Windows.Forms.ListView();
            this.panel_Tower.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Tower
            // 
            this.panel_Tower.Controls.Add(this.label_orgLen);
            this.panel_Tower.Controls.Add(this.textBox_OrgID);
            this.panel_Tower.Controls.Add(this.label1);
            this.panel_Tower.Controls.Add(this.label_CompLen);
            this.panel_Tower.Controls.Add(this.label_CmdLen);
            this.panel_Tower.Controls.Add(this.comboBox_EquType);
            this.panel_Tower.Controls.Add(this.label2);
            this.panel_Tower.Controls.Add(this.textBox_CompID);
            this.panel_Tower.Controls.Add(this.label);
            this.panel_Tower.Controls.Add(this.textBox_StationID);
            this.panel_Tower.Controls.Add(this.textBox_T_StationName);
            this.panel_Tower.Controls.Add(this.label7);
            this.panel_Tower.Controls.Add(this.label8);
            this.panel_Tower.Controls.Add(this.button_Delete);
            this.panel_Tower.Controls.Add(this.button_Update);
            this.panel_Tower.Controls.Add(this.button_Add);
            this.panel_Tower.Controls.Add(this.groupBox1);
            this.panel_Tower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Tower.Location = new System.Drawing.Point(0, 0);
            this.panel_Tower.Name = "panel_Tower";
            this.panel_Tower.Size = new System.Drawing.Size(466, 288);
            this.panel_Tower.TabIndex = 17;
            // 
            // label_orgLen
            // 
            this.label_orgLen.AutoSize = true;
            this.label_orgLen.Location = new System.Drawing.Point(422, 225);
            this.label_orgLen.Name = "label_orgLen";
            this.label_orgLen.Size = new System.Drawing.Size(11, 12);
            this.label_orgLen.TabIndex = 31;
            this.label_orgLen.Text = "0";
            // 
            // textBox_OrgID
            // 
            this.textBox_OrgID.Location = new System.Drawing.Point(300, 222);
            this.textBox_OrgID.Name = "textBox_OrgID";
            this.textBox_OrgID.Size = new System.Drawing.Size(114, 21);
            this.textBox_OrgID.TabIndex = 30;
            this.textBox_OrgID.TextChanged += new System.EventHandler(this.textBox_IDlengChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "原始ID";
            // 
            // label_CompLen
            // 
            this.label_CompLen.AutoSize = true;
            this.label_CompLen.Location = new System.Drawing.Point(421, 190);
            this.label_CompLen.Name = "label_CompLen";
            this.label_CompLen.Size = new System.Drawing.Size(11, 12);
            this.label_CompLen.TabIndex = 28;
            this.label_CompLen.Text = "0";
            // 
            // label_CmdLen
            // 
            this.label_CmdLen.AutoSize = true;
            this.label_CmdLen.Location = new System.Drawing.Point(421, 147);
            this.label_CmdLen.Name = "label_CmdLen";
            this.label_CmdLen.Size = new System.Drawing.Size(11, 12);
            this.label_CmdLen.TabIndex = 27;
            this.label_CmdLen.Text = "0";
            // 
            // comboBox_EquType
            // 
            this.comboBox_EquType.FormattingEnabled = true;
            this.comboBox_EquType.Location = new System.Drawing.Point(75, 188);
            this.comboBox_EquType.Name = "comboBox_EquType";
            this.comboBox_EquType.Size = new System.Drawing.Size(139, 20);
            this.comboBox_EquType.TabIndex = 25;
            this.comboBox_EquType.SelectedIndexChanged += new System.EventHandler(this.comboBox_EquType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "装置类型";
            // 
            // textBox_CompID
            // 
            this.textBox_CompID.Location = new System.Drawing.Point(299, 184);
            this.textBox_CompID.Name = "textBox_CompID";
            this.textBox_CompID.Size = new System.Drawing.Size(114, 21);
            this.textBox_CompID.TabIndex = 19;
            this.textBox_CompID.TextChanged += new System.EventHandler(this.textBox_IDlengChanged);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(243, 189);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(41, 12);
            this.label.TabIndex = 18;
            this.label.Text = "被测ID";
            // 
            // textBox_StationID
            // 
            this.textBox_StationID.Location = new System.Drawing.Point(299, 139);
            this.textBox_StationID.Name = "textBox_StationID";
            this.textBox_StationID.Size = new System.Drawing.Size(114, 21);
            this.textBox_StationID.TabIndex = 11;
            this.textBox_StationID.TextChanged += new System.EventHandler(this.textBox_IDlengChanged);
            // 
            // textBox_T_StationName
            // 
            this.textBox_T_StationName.Location = new System.Drawing.Point(75, 154);
            this.textBox_T_StationName.Name = "textBox_T_StationName";
            this.textBox_T_StationName.Size = new System.Drawing.Size(138, 21);
            this.textBox_T_StationName.TabIndex = 10;
            this.textBox_T_StationName.TextChanged += new System.EventHandler(this.textBox_IDlengChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(243, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "装置ID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "装置名称";
            // 
            // button_Delete
            // 
            this.button_Delete.Location = new System.Drawing.Point(300, 256);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(75, 23);
            this.button_Delete.TabIndex = 7;
            this.button_Delete.Text = "删除";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Del_Click);
            // 
            // button_Update
            // 
            this.button_Update.Location = new System.Drawing.Point(162, 256);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(75, 23);
            this.button_Update.TabIndex = 6;
            this.button_Update.Text = "更新";
            this.button_Update.UseVisualStyleBackColor = true;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(21, 256);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(75, 23);
            this.button_Add.TabIndex = 5;
            this.button_Add.Text = "添加";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView_T_Station);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 116);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "装置列表";
            // 
            // listView_T_Station
            // 
            this.listView_T_Station.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_T_Station.FullRowSelect = true;
            this.listView_T_Station.Location = new System.Drawing.Point(3, 17);
            this.listView_T_Station.MultiSelect = false;
            this.listView_T_Station.Name = "listView_T_Station";
            this.listView_T_Station.Size = new System.Drawing.Size(436, 96);
            this.listView_T_Station.TabIndex = 0;
            this.listView_T_Station.UseCompatibleStateImageBehavior = false;
            this.listView_T_Station.View = System.Windows.Forms.View.Details;
            this.listView_T_Station.SelectedIndexChanged += new System.EventHandler(this.listView_T_Station_SelectedIndexChanged);
            // 
            // Dialog_Update_Tower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 288);
            this.Controls.Add(this.panel_Tower);
            this.Name = "Dialog_Update_Tower";
            this.Text = "Dialog_Update_Tower";
            this.Load += new System.EventHandler(this.Dialog_Update_Tower_Load);
            this.panel_Tower.ResumeLayout(false);
            this.panel_Tower.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Tower;
        private System.Windows.Forms.TextBox textBox_StationID;
        private System.Windows.Forms.TextBox textBox_T_StationName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listView_T_Station;
        private System.Windows.Forms.TextBox textBox_CompID;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label_CompLen;
        private System.Windows.Forms.Label label_CmdLen;
        private System.Windows.Forms.ComboBox comboBox_EquType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_orgLen;
        private System.Windows.Forms.TextBox textBox_OrgID;
        private System.Windows.Forms.Label label1;
    }
}