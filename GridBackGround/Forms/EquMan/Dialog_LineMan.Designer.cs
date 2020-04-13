namespace GridBackGround.Forms.EquMan
{
    partial class Dialog_Line_Man
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
            this.button_Delete = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_LineName = new System.Windows.Forms.TextBox();
            this.button_Update = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView_Nodes = new System.Windows.Forms.TreeView();
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
            this.panel_Background.Size = new System.Drawing.Size(339, 172);
            this.panel_Background.TabIndex = 0;
            // 
            // panel_Right
            // 
            this.panel_Right.Controls.Add(this.panel2);
            this.panel_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Right.Location = new System.Drawing.Point(112, 0);
            this.panel_Right.Name = "panel_Right";
            this.panel_Right.Size = new System.Drawing.Size(227, 172);
            this.panel_Right.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_Delete);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.textBox_LineName);
            this.panel2.Controls.Add(this.button_Update);
            this.panel2.Controls.Add(this.button_Add);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(227, 172);
            this.panel2.TabIndex = 30;
            // 
            // button_Delete
            // 
            this.button_Delete.Location = new System.Drawing.Point(148, 83);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(65, 23);
            this.button_Delete.TabIndex = 17;
            this.button_Delete.Text = "删除";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Del_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "单位名称";
            // 
            // textBox_LineName
            // 
            this.textBox_LineName.Location = new System.Drawing.Point(75, 48);
            this.textBox_LineName.Name = "textBox_LineName";
            this.textBox_LineName.Size = new System.Drawing.Size(117, 21);
            this.textBox_LineName.TabIndex = 2;
            this.textBox_LineName.TextChanged += new System.EventHandler(this.textBox_T_TowerID_TextChanged);
            // 
            // button_Update
            // 
            this.button_Update.Enabled = false;
            this.button_Update.Location = new System.Drawing.Point(78, 83);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(62, 23);
            this.button_Update.TabIndex = 16;
            this.button_Update.Text = "更新";
            this.button_Update.UseVisualStyleBackColor = true;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // button_Add
            // 
            this.button_Add.Enabled = false;
            this.button_Add.Location = new System.Drawing.Point(8, 83);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(65, 23);
            this.button_Add.TabIndex = 20;
            this.button_Add.Text = "添加";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(109, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 172);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView_Nodes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(109, 172);
            this.panel1.TabIndex = 0;
            // 
            // treeView_Nodes
            // 
            this.treeView_Nodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Nodes.Location = new System.Drawing.Point(0, 0);
            this.treeView_Nodes.Name = "treeView_Nodes";
            this.treeView_Nodes.ShowNodeToolTips = true;
            this.treeView_Nodes.Size = new System.Drawing.Size(109, 172);
            this.treeView_Nodes.TabIndex = 1;
            this.treeView_Nodes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Nodes_AfterSelect);
            // 
            // Dialog_Line_Man
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 172);
            this.Controls.Add(this.panel_Background);
            this.Name = "Dialog_Line_Man";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单位管理";
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
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_LineName;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.Button button_Add;


    }
}