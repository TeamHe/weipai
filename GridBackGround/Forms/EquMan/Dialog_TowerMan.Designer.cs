namespace GridBackGround.Forms.EquMan
{
    partial class Dialog_Tower_Man
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Del_Tower = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_T_TowerName = new System.Windows.Forms.TextBox();
            this.button_Update_Tower = new System.Windows.Forms.Button();
            this.button_Add_Tower = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView_Nodes = new System.Windows.Forms.TreeView();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
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
            this.panel_Background.Size = new System.Drawing.Size(519, 472);
            this.panel_Background.TabIndex = 0;
            // 
            // panel_Right
            // 
            this.panel_Right.Controls.Add(this.panel2);
            this.panel_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Right.Location = new System.Drawing.Point(193, 0);
            this.panel_Right.Name = "panel_Right";
            this.panel_Right.Size = new System.Drawing.Size(326, 472);
            this.panel_Right.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.linkLabel1);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button_Del_Tower);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.textBox_T_TowerName);
            this.panel2.Controls.Add(this.button_Update_Tower);
            this.panel2.Controls.Add(this.button_Add_Tower);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(326, 472);
            this.panel2.TabIndex = 30;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(119, 171);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(117, 20);
            this.comboBox1.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "所属单位";
            // 
            // button_Del_Tower
            // 
            this.button_Del_Tower.Enabled = false;
            this.button_Del_Tower.Location = new System.Drawing.Point(189, 247);
            this.button_Del_Tower.Name = "button_Del_Tower";
            this.button_Del_Tower.Size = new System.Drawing.Size(65, 23);
            this.button_Del_Tower.TabIndex = 17;
            this.button_Del_Tower.Text = "删除";
            this.button_Del_Tower.UseVisualStyleBackColor = true;
            this.button_Del_Tower.Click += new System.EventHandler(this.button_Del_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(62, 205);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "线路名称";
            // 
            // textBox_T_TowerName
            // 
            this.textBox_T_TowerName.Location = new System.Drawing.Point(119, 202);
            this.textBox_T_TowerName.Name = "textBox_T_TowerName";
            this.textBox_T_TowerName.Size = new System.Drawing.Size(117, 21);
            this.textBox_T_TowerName.TabIndex = 2;
            this.textBox_T_TowerName.TextChanged += new System.EventHandler(this.textBox_T_TowerID_TextChanged);
            // 
            // button_Update_Tower
            // 
            this.button_Update_Tower.Enabled = false;
            this.button_Update_Tower.Location = new System.Drawing.Point(119, 247);
            this.button_Update_Tower.Name = "button_Update_Tower";
            this.button_Update_Tower.Size = new System.Drawing.Size(62, 23);
            this.button_Update_Tower.TabIndex = 16;
            this.button_Update_Tower.Text = "更新";
            this.button_Update_Tower.UseVisualStyleBackColor = true;
            this.button_Update_Tower.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // button_Add_Tower
            // 
            this.button_Add_Tower.Enabled = false;
            this.button_Add_Tower.Location = new System.Drawing.Point(49, 247);
            this.button_Add_Tower.Name = "button_Add_Tower";
            this.button_Add_Tower.Size = new System.Drawing.Size(65, 23);
            this.button_Add_Tower.TabIndex = 20;
            this.button_Add_Tower.Text = "添加";
            this.button_Add_Tower.UseVisualStyleBackColor = true;
            this.button_Add_Tower.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(190, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 472);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView_Nodes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 472);
            this.panel1.TabIndex = 0;
            // 
            // treeView_Nodes
            // 
            this.treeView_Nodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Nodes.Location = new System.Drawing.Point(0, 0);
            this.treeView_Nodes.Name = "treeView_Nodes";
            this.treeView_Nodes.ShowNodeToolTips = true;
            this.treeView_Nodes.Size = new System.Drawing.Size(190, 472);
            this.treeView_Nodes.TabIndex = 1;
            this.treeView_Nodes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Nodes_AfterSelect);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(243, 178);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(29, 12);
            this.linkLabel1.TabIndex = 23;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "刷新";
            // 
            // Dialog_Tower_Man
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 472);
            this.Controls.Add(this.panel_Background);
            this.Name = "Dialog_Tower_Man";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "线路管理";
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
        private System.Windows.Forms.TextBox textBox_T_TowerName;
        private System.Windows.Forms.Button button_Update_Tower;
        private System.Windows.Forms.Button button_Add_Tower;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;


    }
}