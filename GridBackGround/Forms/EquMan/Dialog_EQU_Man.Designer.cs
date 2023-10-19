namespace GridBackGround.Forms.EquMan
{
    partial class Dialog_EQU_Man
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
            this.components = new System.ComponentModel.Container();
            this.panel_Backgroound = new System.Windows.Forms.Panel();
            this.panel_Center = new System.Windows.Forms.Panel();
            this.panel_Left = new System.Windows.Forms.Panel();
            this.treeView_Nodes = new System.Windows.Forms.TreeView();
            this.contextMenuStrip_TreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.你好ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_Backgroound.SuspendLayout();
            this.panel_Left.SuspendLayout();
            this.contextMenuStrip_TreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Backgroound
            // 
            this.panel_Backgroound.Controls.Add(this.panel_Center);
            this.panel_Backgroound.Controls.Add(this.panel_Left);
            this.panel_Backgroound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Backgroound.Location = new System.Drawing.Point(0, 0);
            this.panel_Backgroound.Name = "panel_Backgroound";
            this.panel_Backgroound.Size = new System.Drawing.Size(648, 450);
            this.panel_Backgroound.TabIndex = 0;
            // 
            // panel_Center
            // 
            this.panel_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Center.Location = new System.Drawing.Point(164, 0);
            this.panel_Center.Name = "panel_Center";
            this.panel_Center.Size = new System.Drawing.Size(484, 450);
            this.panel_Center.TabIndex = 3;
            // 
            // panel_Left
            // 
            this.panel_Left.Controls.Add(this.treeView_Nodes);
            this.panel_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Left.Location = new System.Drawing.Point(0, 0);
            this.panel_Left.Name = "panel_Left";
            this.panel_Left.Size = new System.Drawing.Size(164, 450);
            this.panel_Left.TabIndex = 2;
            // 
            // treeView_Nodes
            // 
            this.treeView_Nodes.ContextMenuStrip = this.contextMenuStrip_TreeView;
            this.treeView_Nodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Nodes.Location = new System.Drawing.Point(0, 0);
            this.treeView_Nodes.Name = "treeView_Nodes";
            this.treeView_Nodes.Size = new System.Drawing.Size(164, 450);
            this.treeView_Nodes.TabIndex = 1;
            // 
            // contextMenuStrip_TreeView
            // 
            this.contextMenuStrip_TreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.你好ToolStripMenuItem});
            this.contextMenuStrip_TreeView.Name = "contextMenuStrip_TreeView";
            this.contextMenuStrip_TreeView.Size = new System.Drawing.Size(101, 26);
            // 
            // 你好ToolStripMenuItem
            // 
            this.你好ToolStripMenuItem.Name = "你好ToolStripMenuItem";
            this.你好ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.你好ToolStripMenuItem.Text = "你好";
            // 
            // Dialog_EQU_Man
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 450);
            this.Controls.Add(this.panel_Backgroound);
            this.MaximizeBox = false;
            this.Name = "Dialog_EQU_Man";
            this.Text = "设备管理";
            this.Load += new System.EventHandler(this.Dialog_EQU_Man_Load);
            this.panel_Backgroound.ResumeLayout(false);
            this.panel_Left.ResumeLayout(false);
            this.contextMenuStrip_TreeView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Backgroound;
        private System.Windows.Forms.TreeView treeView_Nodes;
        private System.Windows.Forms.Panel panel_Center;
        private System.Windows.Forms.Panel panel_Left;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_TreeView;
        private System.Windows.Forms.ToolStripMenuItem 你好ToolStripMenuItem;
    }
}