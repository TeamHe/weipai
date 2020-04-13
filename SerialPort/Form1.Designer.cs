namespace 国网串口配置工具V2._0
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_Handshake = new System.Windows.Forms.ComboBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_Send = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_Parity = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_StopBits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_DataBits = new System.Windows.Forms.ComboBox();
            this.checkBox_HexDis = new System.Windows.Forms.CheckBox();
            this.checkBox_HexSend = new System.Windows.Forms.CheckBox();
            this.textBox_SendTimer = new System.Windows.Forms.TextBox();
            this.checkBox_TimerSend = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_BaudRate = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.buttonPort = new System.Windows.Forms.Button();
            this.comboBox_Name = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.timer_Send = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.comboBox_Handshake);
            this.panel1.Controls.Add(this.button_Send);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox_Send);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.comboBox_Parity);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.comboBox_StopBits);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBox_DataBits);
            this.panel1.Controls.Add(this.checkBox_HexDis);
            this.panel1.Controls.Add(this.checkBox_HexSend);
            this.panel1.Controls.Add(this.textBox_SendTimer);
            this.panel1.Controls.Add(this.checkBox_TimerSend);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBox_BaudRate);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button_Save);
            this.panel1.Controls.Add(this.buttonPort);
            this.panel1.Controls.Add(this.comboBox_Name);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 448);
            this.panel1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(382, 362);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "ms/次";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 419);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "流控制";
            // 
            // comboBox_Handshake
            // 
            this.comboBox_Handshake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Handshake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Handshake.FormattingEnabled = true;
            this.comboBox_Handshake.Items.AddRange(new object[] {
            "None",
            "XOnXOff",
            "RequestToSend",
            "RequestToSendXOnXOff"});
            this.comboBox_Handshake.Location = new System.Drawing.Point(61, 415);
            this.comboBox_Handshake.Name = "comboBox_Handshake";
            this.comboBox_Handshake.Size = new System.Drawing.Size(69, 20);
            this.comboBox_Handshake.TabIndex = 21;
            this.comboBox_Handshake.SelectedIndexChanged += new System.EventHandler(this.comboBox_Handshake_SelectedIndexChanged);
            // 
            // button_Send
            // 
            this.button_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Send.Location = new System.Drawing.Point(218, 383);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(75, 23);
            this.button_Send.TabIndex = 20;
            this.button_Send.Text = "发送";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(145, 389);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "字符输入框";
            // 
            // textBox_Send
            // 
            this.textBox_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_Send.Location = new System.Drawing.Point(145, 412);
            this.textBox_Send.Name = "textBox_Send";
            this.textBox_Send.Size = new System.Drawing.Size(333, 21);
            this.textBox_Send.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 400);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "校验位";
            // 
            // comboBox_Parity
            // 
            this.comboBox_Parity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Parity.FormattingEnabled = true;
            this.comboBox_Parity.Items.AddRange(new object[] {
            "None",
            "0dd",
            "Even",
            "Mark",
            "Space"});
            this.comboBox_Parity.Location = new System.Drawing.Point(61, 396);
            this.comboBox_Parity.Name = "comboBox_Parity";
            this.comboBox_Parity.Size = new System.Drawing.Size(69, 20);
            this.comboBox_Parity.TabIndex = 16;
            this.comboBox_Parity.SelectedIndexChanged += new System.EventHandler(this.comboBox_Parity_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 381);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "停止位";
            // 
            // comboBox_StopBits
            // 
            this.comboBox_StopBits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_StopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_StopBits.FormattingEnabled = true;
            this.comboBox_StopBits.Items.AddRange(new object[] {
            "0",
            "1",
            "1.5",
            "2"});
            this.comboBox_StopBits.Location = new System.Drawing.Point(61, 377);
            this.comboBox_StopBits.Name = "comboBox_StopBits";
            this.comboBox_StopBits.Size = new System.Drawing.Size(69, 20);
            this.comboBox_StopBits.TabIndex = 14;
            this.comboBox_StopBits.SelectedIndexChanged += new System.EventHandler(this.comboBox_StopBits_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 362);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "数据位";
            // 
            // comboBox_DataBits
            // 
            this.comboBox_DataBits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_DataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DataBits.FormattingEnabled = true;
            this.comboBox_DataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.comboBox_DataBits.Location = new System.Drawing.Point(61, 358);
            this.comboBox_DataBits.Name = "comboBox_DataBits";
            this.comboBox_DataBits.Size = new System.Drawing.Size(69, 20);
            this.comboBox_DataBits.TabIndex = 12;
            this.comboBox_DataBits.SelectedIndexChanged += new System.EventHandler(this.comboBox_DataBits_SelectedIndexChanged);
            // 
            // checkBox_HexDis
            // 
            this.checkBox_HexDis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_HexDis.AutoSize = true;
            this.checkBox_HexDis.Location = new System.Drawing.Point(402, 319);
            this.checkBox_HexDis.Name = "checkBox_HexDis";
            this.checkBox_HexDis.Size = new System.Drawing.Size(66, 16);
            this.checkBox_HexDis.TabIndex = 11;
            this.checkBox_HexDis.Text = "Hex显示";
            this.checkBox_HexDis.UseVisualStyleBackColor = true;
            this.checkBox_HexDis.CheckedChanged += new System.EventHandler(this.checkBox_HexDis_CheckedChanged);
            // 
            // checkBox_HexSend
            // 
            this.checkBox_HexSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_HexSend.AutoSize = true;
            this.checkBox_HexSend.Location = new System.Drawing.Point(145, 357);
            this.checkBox_HexSend.Name = "checkBox_HexSend";
            this.checkBox_HexSend.Size = new System.Drawing.Size(66, 16);
            this.checkBox_HexSend.TabIndex = 10;
            this.checkBox_HexSend.Text = "Hex发送";
            this.checkBox_HexSend.UseVisualStyleBackColor = true;
            this.checkBox_HexSend.CheckedChanged += new System.EventHandler(this.checkBox_HexSend_CheckedChanged);
            // 
            // textBox_SendTimer
            // 
            this.textBox_SendTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_SendTimer.Location = new System.Drawing.Point(300, 357);
            this.textBox_SendTimer.Name = "textBox_SendTimer";
            this.textBox_SendTimer.Size = new System.Drawing.Size(66, 21);
            this.textBox_SendTimer.TabIndex = 9;
            this.textBox_SendTimer.Text = "1000";
            // 
            // checkBox_TimerSend
            // 
            this.checkBox_TimerSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_TimerSend.AutoSize = true;
            this.checkBox_TimerSend.Location = new System.Drawing.Point(221, 357);
            this.checkBox_TimerSend.Name = "checkBox_TimerSend";
            this.checkBox_TimerSend.Size = new System.Drawing.Size(72, 16);
            this.checkBox_TimerSend.TabIndex = 8;
            this.checkBox_TimerSend.Text = "定时发送";
            this.checkBox_TimerSend.UseVisualStyleBackColor = true;
            this.checkBox_TimerSend.CheckedChanged += new System.EventHandler(this.checkBox_TimerSend_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 343);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "波特率";
            // 
            // comboBox_BaudRate
            // 
            this.comboBox_BaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_BaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_BaudRate.FormattingEnabled = true;
            this.comboBox_BaudRate.Items.AddRange(new object[] {
            "110",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200",
            "128000",
            "256000",
            ""});
            this.comboBox_BaudRate.Location = new System.Drawing.Point(61, 339);
            this.comboBox_BaudRate.Name = "comboBox_BaudRate";
            this.comboBox_BaudRate.Size = new System.Drawing.Size(69, 20);
            this.comboBox_BaudRate.TabIndex = 6;
            this.comboBox_BaudRate.SelectedIndexChanged += new System.EventHandler(this.comboBox_BaudRate_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(321, 313);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "清除窗口";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_Save
            // 
            this.button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Save.Location = new System.Drawing.Point(240, 312);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 4;
            this.button_Save.Text = "保存窗口";
            this.button_Save.UseVisualStyleBackColor = true;
            // 
            // buttonPort
            // 
            this.buttonPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPort.Location = new System.Drawing.Point(145, 312);
            this.buttonPort.Name = "buttonPort";
            this.buttonPort.Size = new System.Drawing.Size(75, 23);
            this.buttonPort.TabIndex = 3;
            this.buttonPort.Text = "打开串口";
            this.buttonPort.UseVisualStyleBackColor = true;
            this.buttonPort.Click += new System.EventHandler(this.buttonPort_Click);
            // 
            // comboBox_Name
            // 
            this.comboBox_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Name.FormattingEnabled = true;
            this.comboBox_Name.Location = new System.Drawing.Point(61, 320);
            this.comboBox_Name.Name = "comboBox_Name";
            this.comboBox_Name.Size = new System.Drawing.Size(69, 20);
            this.comboBox_Name.TabIndex = 2;
            this.comboBox_Name.SelectedIndexChanged += new System.EventHandler(this.comboBox_Name_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 324);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "串口号";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(3, -24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(486, 326);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // timer_Send
            // 
            this.timer_Send.Tick += new System.EventHandler(this.timer_Send_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 448);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_Send;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_Parity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_StopBits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_DataBits;
        private System.Windows.Forms.CheckBox checkBox_HexDis;
        private System.Windows.Forms.CheckBox checkBox_HexSend;
        private System.Windows.Forms.TextBox textBox_SendTimer;
        private System.Windows.Forms.CheckBox checkBox_TimerSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_BaudRate;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button buttonPort;
        private System.Windows.Forms.ComboBox comboBox_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_Handshake;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer timer_Send;

    }
}

