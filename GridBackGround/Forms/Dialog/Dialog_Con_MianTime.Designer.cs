﻿namespace GridBackGround.Forms
{
    partial class Dialog_Con_MianTime
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
            this.panel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_HeartTime = new System.Windows.Forms.TextBox();
            this.textBox_MainTime = new System.Windows.Forms.TextBox();
            this.checkBox_HeartBeat = new System.Windows.Forms.CheckBox();
            this.checkBox_MianTime = new System.Windows.Forms.CheckBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.button1);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.textBox_HeartTime);
            this.panel.Controls.Add(this.textBox_MainTime);
            this.panel.Controls.Add(this.checkBox_HeartBeat);
            this.panel.Controls.Add(this.checkBox_MianTime);
            this.panel.Controls.Add(this.button_Cancel);
            this.panel.Controls.Add(this.button_OK);
            this.panel.Controls.Add(this.label1);
            this.panel.Controls.Add(this.comboBox1);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(294, 173);
            this.panel.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonQuary_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "分钟";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "分钟";
            // 
            // textBox_HeartTime
            // 
            this.textBox_HeartTime.Location = new System.Drawing.Point(106, 93);
            this.textBox_HeartTime.Name = "textBox_HeartTime";
            this.textBox_HeartTime.Size = new System.Drawing.Size(69, 21);
            this.textBox_HeartTime.TabIndex = 7;
            this.textBox_HeartTime.Text = "2";
            this.textBox_HeartTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Int_KeyPress);
            // 
            // textBox_MainTime
            // 
            this.textBox_MainTime.Location = new System.Drawing.Point(106, 57);
            this.textBox_MainTime.Name = "textBox_MainTime";
            this.textBox_MainTime.Size = new System.Drawing.Size(69, 21);
            this.textBox_MainTime.TabIndex = 6;
            this.textBox_MainTime.Text = "10";
            this.textBox_MainTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Int_KeyPress);
            // 
            // checkBox_HeartBeat
            // 
            this.checkBox_HeartBeat.AutoSize = true;
            this.checkBox_HeartBeat.Location = new System.Drawing.Point(29, 95);
            this.checkBox_HeartBeat.Name = "checkBox_HeartBeat";
            this.checkBox_HeartBeat.Size = new System.Drawing.Size(72, 16);
            this.checkBox_HeartBeat.TabIndex = 5;
            this.checkBox_HeartBeat.Text = "心跳周期";
            this.checkBox_HeartBeat.UseVisualStyleBackColor = true;
            // 
            // checkBox_MianTime
            // 
            this.checkBox_MianTime.AutoSize = true;
            this.checkBox_MianTime.Checked = true;
            this.checkBox_MianTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_MianTime.Location = new System.Drawing.Point(29, 59);
            this.checkBox_MianTime.Name = "checkBox_MianTime";
            this.checkBox_MianTime.Size = new System.Drawing.Size(72, 16);
            this.checkBox_MianTime.TabIndex = 4;
            this.checkBox_MianTime.Text = "采集时间";
            this.checkBox_MianTime.UseVisualStyleBackColor = true;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(193, 131);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "返回";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(100, 131);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "设定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据类型";
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "基本数据类型",
            "导地线微风振动特征量",
            "导地线微风振动波形信号",
            "导地线舞动特征量",
            "导地线舞动轨迹"});
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "气象环境",
            "杆塔倾斜",
            "导地线微风振动特征量",
            "导地线微风振动波形信号",
            "导线弧垂",
            "导线温度",
            "覆冰及不均衡张力差",
            "导线风偏",
            "导地线舞动特征量",
            "导地线舞动轨迹",
            "现场污秽度"});
            this.comboBox1.Location = new System.Drawing.Point(88, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 0;
            // 
            // Dialog_Con_MianTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 173);
            this.Controls.Add(this.panel);
            this.Name = "Dialog_Con_MianTime";
            this.Text = "状态监测装置采样周期设置";
            this.Load += new System.EventHandler(this.Dialog_Con_MianTime_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TextBox textBox_HeartTime;
        private System.Windows.Forms.TextBox textBox_MainTime;
        private System.Windows.Forms.CheckBox checkBox_HeartBeat;
        private System.Windows.Forms.CheckBox checkBox_MianTime;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}