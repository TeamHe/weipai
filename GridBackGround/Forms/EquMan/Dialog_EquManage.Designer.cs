namespace GridBackGround.Forms.Dialog
{
    partial class Dialog_EquManage
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
            this.panel_Background = new System.Windows.Forms.Panel();
            this.panel_Right = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_Del_Tower = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_T_TowerName = new System.Windows.Forms.TextBox();
            this.textBox_T_TowerID = new System.Windows.Forms.TextBox();
            this.button_Update_Tower = new System.Windows.Forms.Button();
            this.button_Add_Tower = new System.Windows.Forms.Button();
            this.label_TowerID_len = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView_Nodes = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel_Background.SuspendLayout();
            this.panel_Right.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Background
            // 
            this.panel_Background.Controls.Add(this.panel_Right);
            this.panel_Background.Controls.Add(this.splitter1);
            this.panel_Background.Controls.Add(this.panel1);
            this.panel_Background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Background.Location = new System.Drawing.Point(0, 0);
            this.panel_Background.Name = "panel_Background";
            this.panel_Background.Size = new System.Drawing.Size(671, 388);
            this.panel_Background.TabIndex = 0;
            // 
            // panel_Right
            // 
            this.panel_Right.Controls.Add(this.panel3);
            this.panel_Right.Controls.Add(this.panel2);
            this.panel_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Right.Location = new System.Drawing.Point(203, 0);
            this.panel_Right.Name = "panel_Right";
            this.panel_Right.Size = new System.Drawing.Size(468, 388);
            this.panel_Right.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_Del_Tower);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textBox_T_TowerName);
            this.panel2.Controls.Add(this.textBox_T_TowerID);
            this.panel2.Controls.Add(this.button_Update_Tower);
            this.panel2.Controls.Add(this.button_Add_Tower);
            this.panel2.Controls.Add(this.label_TowerID_len);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(468, 88);
            this.panel2.TabIndex = 30;
            // 
            // button_Del_Tower
            // 
            this.button_Del_Tower.Location = new System.Drawing.Point(323, 63);
            this.button_Del_Tower.Name = "button_Del_Tower";
            this.button_Del_Tower.Size = new System.Drawing.Size(121, 23);
            this.button_Del_Tower.TabIndex = 17;
            this.button_Del_Tower.Text = "删除杆塔(被测装置)";
            this.button_Del_Tower.UseVisualStyleBackColor = true;
            this.button_Del_Tower.Click += new System.EventHandler(this.button_Del_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "杆塔(被测装置)名称";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "杆塔(被测装置)ID";
            // 
            // textBox_T_TowerName
            // 
            this.textBox_T_TowerName.Location = new System.Drawing.Point(153, 16);
            this.textBox_T_TowerName.Name = "textBox_T_TowerName";
            this.textBox_T_TowerName.Size = new System.Drawing.Size(117, 21);
            this.textBox_T_TowerName.TabIndex = 2;
            this.textBox_T_TowerName.TextChanged += new System.EventHandler(this.textBox_T_TowerID_TextChanged);
            // 
            // textBox_T_TowerID
            // 
            this.textBox_T_TowerID.Location = new System.Drawing.Point(153, 65);
            this.textBox_T_TowerID.Name = "textBox_T_TowerID";
            this.textBox_T_TowerID.Size = new System.Drawing.Size(117, 21);
            this.textBox_T_TowerID.TabIndex = 3;
            this.textBox_T_TowerID.TextChanged += new System.EventHandler(this.textBox_T_TowerID_TextChanged);
            // 
            // button_Update_Tower
            // 
            this.button_Update_Tower.Location = new System.Drawing.Point(322, 33);
            this.button_Update_Tower.Name = "button_Update_Tower";
            this.button_Update_Tower.Size = new System.Drawing.Size(121, 23);
            this.button_Update_Tower.TabIndex = 16;
            this.button_Update_Tower.Text = "更新杆塔(被测装置)";
            this.button_Update_Tower.UseVisualStyleBackColor = true;
            this.button_Update_Tower.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // button_Add_Tower
            // 
            this.button_Add_Tower.Location = new System.Drawing.Point(322, 5);
            this.button_Add_Tower.Name = "button_Add_Tower";
            this.button_Add_Tower.Size = new System.Drawing.Size(121, 23);
            this.button_Add_Tower.TabIndex = 20;
            this.button_Add_Tower.Text = "添加杆塔(被测装置)";
            this.button_Add_Tower.UseVisualStyleBackColor = true;
            this.button_Add_Tower.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // label_TowerID_len
            // 
            this.label_TowerID_len.AutoSize = true;
            this.label_TowerID_len.Location = new System.Drawing.Point(278, 69);
            this.label_TowerID_len.Name = "label_TowerID_len";
            this.label_TowerID_len.Size = new System.Drawing.Size(11, 12);
            this.label_TowerID_len.TabIndex = 21;
            this.label_TowerID_len.Text = "0";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 388);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView_Nodes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 388);
            this.panel1.TabIndex = 0;
            // 
            // treeView_Nodes
            // 
            this.treeView_Nodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Nodes.Location = new System.Drawing.Point(0, 0);
            this.treeView_Nodes.Name = "treeView_Nodes";
            this.treeView_Nodes.ShowNodeToolTips = true;
            this.treeView_Nodes.Size = new System.Drawing.Size(200, 388);
            this.treeView_Nodes.TabIndex = 1;
            this.treeView_Nodes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Nodes_AfterSelect);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 88);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(468, 300);
            this.panel3.TabIndex = 31;
            // 
            // Dialog_EquManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 388);
            this.Controls.Add(this.panel_Background);
            this.Name = "Dialog_EquManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备管理";
            this.Load += new System.EventHandler(this.Dialog_Load);
            this.panel_Background.ResumeLayout(false);
            this.panel_Right.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Background;
        private System.Windows.Forms.Panel panel_Right;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView_Nodes;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_Del_Tower;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_T_TowerName;
        private System.Windows.Forms.TextBox textBox_T_TowerID;
        private System.Windows.Forms.Button button_Update_Tower;
        private System.Windows.Forms.Button button_Add_Tower;
        private System.Windows.Forms.Label label_TowerID_len;
        private System.Windows.Forms.Panel panel3;


    }
}