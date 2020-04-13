namespace GridBackGround
{
    partial class Tab_ID
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_OnLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_Node = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除当前节点toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改当前节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.contextMenuStrip_Node.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 479);
            this.panel1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Name,
            this.columnHeader_ID,
            this.columnHeader_IP,
            this.columnHeader_OnLine});
            this.listView1.ContextMenuStrip = this.contextMenuStrip_Node;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listView1.Size = new System.Drawing.Size(288, 479);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView1.MouseHover += new System.EventHandler(this.listView1_MouseHover);
            // 
            // columnHeader_Name
            // 
            this.columnHeader_Name.Text = "装置名称";
            this.columnHeader_Name.Width = 5;
            // 
            // columnHeader_ID
            // 
            this.columnHeader_ID.Text = "装置ID";
            this.columnHeader_ID.Width = 121;
            // 
            // columnHeader_IP
            // 
            this.columnHeader_IP.Text = "装置IP";
            this.columnHeader_IP.Width = 248;
            // 
            // columnHeader_OnLine
            // 
            this.columnHeader_OnLine.Text = "在线状态";
            this.columnHeader_OnLine.Width = 170;
            // 
            // contextMenuStrip_Node
            // 
            this.contextMenuStrip_Node.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加节点ToolStripMenuItem,
            this.删除当前节点toolStripMenuItem,
            this.修改当前节点ToolStripMenuItem});
            this.contextMenuStrip_Node.Name = "contextMenuStrip1";
            this.contextMenuStrip_Node.Size = new System.Drawing.Size(149, 70);
            this.contextMenuStrip_Node.Text = "复制";
            this.contextMenuStrip_Node.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Node_Opening);
            // 
            // 添加节点ToolStripMenuItem
            // 
            this.添加节点ToolStripMenuItem.Name = "添加节点ToolStripMenuItem";
            this.添加节点ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.添加节点ToolStripMenuItem.Text = "添加节点";
            this.添加节点ToolStripMenuItem.Click += new System.EventHandler(this.添加节点ToolStripMenuItem_Click);
            // 
            // 删除当前节点toolStripMenuItem
            // 
            this.删除当前节点toolStripMenuItem.Name = "删除当前节点toolStripMenuItem";
            this.删除当前节点toolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.删除当前节点toolStripMenuItem.Text = "删除当前节点";
            this.删除当前节点toolStripMenuItem.Visible = false;
            this.删除当前节点toolStripMenuItem.Click += new System.EventHandler(this.删除节点toolStripMenuItem_Click);
            // 
            // 修改当前节点ToolStripMenuItem
            // 
            this.修改当前节点ToolStripMenuItem.Name = "修改当前节点ToolStripMenuItem";
            this.修改当前节点ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.修改当前节点ToolStripMenuItem.Text = "修改当前节点";
            this.修改当前节点ToolStripMenuItem.Click += new System.EventHandler(this.修改当前节点ToolStripMenuItem_Click);
            // 
            // Tab_ID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 479);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Tab_ID";
            this.Text = "Tab_I";
            this.Load += new System.EventHandler(this.Tab_ID_Load);
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip_Node.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader_Name;
        private System.Windows.Forms.ColumnHeader columnHeader_ID;
        private System.Windows.Forms.ColumnHeader columnHeader_IP;
        private System.Windows.Forms.ColumnHeader columnHeader_OnLine;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Node;
        private System.Windows.Forms.ToolStripMenuItem 添加节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除当前节点toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改当前节点ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}