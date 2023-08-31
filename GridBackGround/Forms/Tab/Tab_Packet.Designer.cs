﻿namespace GridBackGround
{
    partial class Tab_Packet
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
            this.panelGPRS = new System.Windows.Forms.Panel();
            this.splitContainerGPRS = new System.Windows.Forms.SplitContainer();
            this.label_curdev = new System.Windows.Forms.Label();
            this.button_save = new System.Windows.Forms.Button();
            this.button_his_day = new System.Windows.Forms.Button();
            this.button_his_hour = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
            this.button_his_custom = new System.Windows.Forms.Button();
            this.checkBox_all = new System.Windows.Forms.CheckBox();
            this.checkBox_real = new System.Windows.Forms.CheckBox();
            this.listBox_Packet = new System.Windows.Forms.ListBox();
            this.buttonSendData = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.buttonClearCommand = new System.Windows.Forms.Button();
            this.buttonClearPackage = new System.Windows.Forms.Button();
            this.contextMenuStrip_Node = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除当前节点toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改当前节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelGPRS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGPRS)).BeginInit();
            this.splitContainerGPRS.Panel1.SuspendLayout();
            this.splitContainerGPRS.Panel2.SuspendLayout();
            this.splitContainerGPRS.SuspendLayout();
            this.contextMenuStrip_Node.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGPRS
            // 
            this.panelGPRS.Controls.Add(this.splitContainerGPRS);
            this.panelGPRS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGPRS.Location = new System.Drawing.Point(0, 0);
            this.panelGPRS.Name = "panelGPRS";
            this.panelGPRS.Size = new System.Drawing.Size(863, 487);
            this.panelGPRS.TabIndex = 5;
            // 
            // splitContainerGPRS
            // 
            this.splitContainerGPRS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerGPRS.Location = new System.Drawing.Point(0, 0);
            this.splitContainerGPRS.Name = "splitContainerGPRS";
            this.splitContainerGPRS.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerGPRS.Panel1
            // 
            this.splitContainerGPRS.Panel1.Controls.Add(this.label_curdev);
            this.splitContainerGPRS.Panel1.Controls.Add(this.button_save);
            this.splitContainerGPRS.Panel1.Controls.Add(this.button_his_day);
            this.splitContainerGPRS.Panel1.Controls.Add(this.button_his_hour);
            this.splitContainerGPRS.Panel1.Controls.Add(this.label2);
            this.splitContainerGPRS.Panel1.Controls.Add(this.dateTimePicker_end);
            this.splitContainerGPRS.Panel1.Controls.Add(this.label1);
            this.splitContainerGPRS.Panel1.Controls.Add(this.dateTimePicker_start);
            this.splitContainerGPRS.Panel1.Controls.Add(this.button_his_custom);
            this.splitContainerGPRS.Panel1.Controls.Add(this.checkBox_all);
            this.splitContainerGPRS.Panel1.Controls.Add(this.checkBox_real);
            this.splitContainerGPRS.Panel1.Controls.Add(this.listBox_Packet);
            // 
            // splitContainerGPRS.Panel2
            // 
            this.splitContainerGPRS.Panel2.Controls.Add(this.buttonSendData);
            this.splitContainerGPRS.Panel2.Controls.Add(this.richTextBox2);
            this.splitContainerGPRS.Panel2.Controls.Add(this.buttonClearCommand);
            this.splitContainerGPRS.Panel2.Controls.Add(this.buttonClearPackage);
            this.splitContainerGPRS.Size = new System.Drawing.Size(863, 487);
            this.splitContainerGPRS.SplitterDistance = 405;
            this.splitContainerGPRS.TabIndex = 0;
            // 
            // label_curdev
            // 
            this.label_curdev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_curdev.Location = new System.Drawing.Point(485, 8);
            this.label_curdev.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_curdev.Name = "label_curdev";
            this.label_curdev.Size = new System.Drawing.Size(370, 19);
            this.label_curdev.TabIndex = 16;
            this.label_curdev.Text = "选中设备";
            this.label_curdev.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(485, 30);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(81, 23);
            this.button_save.TabIndex = 10;
            this.button_save.Text = "保存到文件";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_his_day
            // 
            this.button_his_day.Location = new System.Drawing.Point(372, 31);
            this.button_his_day.Name = "button_his_day";
            this.button_his_day.Size = new System.Drawing.Size(107, 23);
            this.button_his_day.TabIndex = 9;
            this.button_his_day.Text = "最近一天记录";
            this.button_his_day.UseVisualStyleBackColor = true;
            this.button_his_day.Click += new System.EventHandler(this.button_his_day_Click);
            // 
            // button_his_hour
            // 
            this.button_his_hour.Location = new System.Drawing.Point(372, 6);
            this.button_his_hour.Name = "button_his_hour";
            this.button_his_hour.Size = new System.Drawing.Size(107, 23);
            this.button_his_hour.TabIndex = 8;
            this.button_his_hour.Text = "最近一小时记录";
            this.button_his_hour.UseVisualStyleBackColor = true;
            this.button_his_hour.Click += new System.EventHandler(this.button_his_hour_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "结束时间";
            // 
            // dateTimePicker_end
            // 
            this.dateTimePicker_end.CustomFormat = " yyyy-MM-dd HH:mm";
            this.dateTimePicker_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_end.Location = new System.Drawing.Point(141, 32);
            this.dateTimePicker_end.Name = "dateTimePicker_end";
            this.dateTimePicker_end.Size = new System.Drawing.Size(141, 21);
            this.dateTimePicker_end.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "开始时间";
            // 
            // dateTimePicker_start
            // 
            this.dateTimePicker_start.CustomFormat = " yyyy-MM-dd HH:mm";
            this.dateTimePicker_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_start.Location = new System.Drawing.Point(141, 5);
            this.dateTimePicker_start.Name = "dateTimePicker_start";
            this.dateTimePicker_start.Size = new System.Drawing.Size(141, 21);
            this.dateTimePicker_start.TabIndex = 4;
            // 
            // button_his_custom
            // 
            this.button_his_custom.Location = new System.Drawing.Point(288, 33);
            this.button_his_custom.Name = "button_his_custom";
            this.button_his_custom.Size = new System.Drawing.Size(75, 23);
            this.button_his_custom.TabIndex = 3;
            this.button_his_custom.Text = "查询";
            this.button_his_custom.UseVisualStyleBackColor = true;
            this.button_his_custom.Click += new System.EventHandler(this.buttonHistoryCustom_Click);
            // 
            // checkBox_all
            // 
            this.checkBox_all.AutoSize = true;
            this.checkBox_all.Location = new System.Drawing.Point(13, 35);
            this.checkBox_all.Name = "checkBox_all";
            this.checkBox_all.Size = new System.Drawing.Size(72, 16);
            this.checkBox_all.TabIndex = 2;
            this.checkBox_all.Text = "显示所有";
            this.checkBox_all.UseVisualStyleBackColor = true;
            // 
            // checkBox_real
            // 
            this.checkBox_real.AutoSize = true;
            this.checkBox_real.Location = new System.Drawing.Point(12, 6);
            this.checkBox_real.Name = "checkBox_real";
            this.checkBox_real.Size = new System.Drawing.Size(72, 16);
            this.checkBox_real.TabIndex = 1;
            this.checkBox_real.Text = "实时报文";
            this.checkBox_real.UseVisualStyleBackColor = true;
            this.checkBox_real.CheckedChanged += new System.EventHandler(this.checkBox_real_CheckedChanged);
            // 
            // listBox_Packet
            // 
            this.listBox_Packet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_Packet.FormattingEnabled = true;
            this.listBox_Packet.HorizontalScrollbar = true;
            this.listBox_Packet.ItemHeight = 12;
            this.listBox_Packet.Location = new System.Drawing.Point(0, 60);
            this.listBox_Packet.Name = "listBox_Packet";
            this.listBox_Packet.Size = new System.Drawing.Size(863, 340);
            this.listBox_Packet.TabIndex = 0;
            this.listBox_Packet.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // buttonSendData
            // 
            this.buttonSendData.Location = new System.Drawing.Point(4, 24);
            this.buttonSendData.Name = "buttonSendData";
            this.buttonSendData.Size = new System.Drawing.Size(75, 23);
            this.buttonSendData.TabIndex = 4;
            this.buttonSendData.Text = "Hex发送";
            this.buttonSendData.UseVisualStyleBackColor = true;
            this.buttonSendData.Visible = false;
            this.buttonSendData.Click += new System.EventHandler(this.buttonSendData_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Location = new System.Drawing.Point(85, 2);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(773, 73);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "";
            // 
            // buttonClearCommand
            // 
            this.buttonClearCommand.Location = new System.Drawing.Point(5, 40);
            this.buttonClearCommand.Name = "buttonClearCommand";
            this.buttonClearCommand.Size = new System.Drawing.Size(75, 23);
            this.buttonClearCommand.TabIndex = 2;
            this.buttonClearCommand.Text = "清空命令";
            this.buttonClearCommand.UseVisualStyleBackColor = true;
            this.buttonClearCommand.Click += new System.EventHandler(this.buttonClearCommand_Click);
            // 
            // buttonClearPackage
            // 
            this.buttonClearPackage.Location = new System.Drawing.Point(4, 2);
            this.buttonClearPackage.Name = "buttonClearPackage";
            this.buttonClearPackage.Size = new System.Drawing.Size(75, 23);
            this.buttonClearPackage.TabIndex = 1;
            this.buttonClearPackage.Text = "清空报文";
            this.buttonClearPackage.UseVisualStyleBackColor = true;
            this.buttonClearPackage.Click += new System.EventHandler(this.buttonClearPackage_Click);
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
            // 
            // 添加节点ToolStripMenuItem
            // 
            this.添加节点ToolStripMenuItem.Name = "添加节点ToolStripMenuItem";
            this.添加节点ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.添加节点ToolStripMenuItem.Text = "添加节点";
            // 
            // 删除当前节点toolStripMenuItem
            // 
            this.删除当前节点toolStripMenuItem.Name = "删除当前节点toolStripMenuItem";
            this.删除当前节点toolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.删除当前节点toolStripMenuItem.Text = "删除当前节点";
            this.删除当前节点toolStripMenuItem.Visible = false;
            // 
            // 修改当前节点ToolStripMenuItem
            // 
            this.修改当前节点ToolStripMenuItem.Name = "修改当前节点ToolStripMenuItem";
            this.修改当前节点ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.修改当前节点ToolStripMenuItem.Text = "修改当前节点";
            // 
            // Tab_Packet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 487);
            this.Controls.Add(this.panelGPRS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Tab_Packet";
            this.Text = "Tab_Packet";
            this.Load += new System.EventHandler(this.Tab_Packet_Load);
            this.panelGPRS.ResumeLayout(false);
            this.splitContainerGPRS.Panel1.ResumeLayout(false);
            this.splitContainerGPRS.Panel1.PerformLayout();
            this.splitContainerGPRS.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGPRS)).EndInit();
            this.splitContainerGPRS.ResumeLayout(false);
            this.contextMenuStrip_Node.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGPRS;
        private System.Windows.Forms.SplitContainer splitContainerGPRS;
        private System.Windows.Forms.ListBox listBox_Packet;
        private System.Windows.Forms.Button buttonSendData;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button buttonClearCommand;
        private System.Windows.Forms.Button buttonClearPackage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Node;
        private System.Windows.Forms.ToolStripMenuItem 添加节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除当前节点toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改当前节点ToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox_real;
        private System.Windows.Forms.CheckBox checkBox_all;
        private System.Windows.Forms.Button button_his_custom;
        private System.Windows.Forms.DateTimePicker dateTimePicker_start;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker_end;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_his_day;
        private System.Windows.Forms.Button button_his_hour;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Label label_curdev;
    }
}