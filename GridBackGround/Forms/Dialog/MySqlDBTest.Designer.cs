namespace GridBackGround.Forms.Dialog
{
    partial class MySqlDBTest
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Server = new System.Windows.Forms.TextBox();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.label_Port = new System.Windows.Forms.Label();
            this.label_Name = new System.Windows.Forms.Label();
            this.textBox_DBName = new System.Windows.Forms.TextBox();
            this.label_UserName = new System.Windows.Forms.Label();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.textBox_UsNamd = new System.Windows.Forms.TextBox();
            this.label_PassWord = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_PassWord);
            this.panel1.Controls.Add(this.textBox_UsNamd);
            this.panel1.Controls.Add(this.textBox_Password);
            this.panel1.Controls.Add(this.label_UserName);
            this.panel1.Controls.Add(this.textBox_DBName);
            this.panel1.Controls.Add(this.label_Name);
            this.panel1.Controls.Add(this.label_Port);
            this.panel1.Controls.Add(this.textBox_Port);
            this.panel1.Controls.Add(this.textBox_Server);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 231);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器地址";
            // 
            // textBox_Server
            // 
            this.textBox_Server.Location = new System.Drawing.Point(96, 29);
            this.textBox_Server.Name = "textBox_Server";
            this.textBox_Server.Size = new System.Drawing.Size(110, 21);
            this.textBox_Server.TabIndex = 1;
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(96, 69);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(110, 21);
            this.textBox_Port.TabIndex = 2;
            // 
            // label_Port
            // 
            this.label_Port.AutoSize = true;
            this.label_Port.Location = new System.Drawing.Point(25, 73);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(29, 12);
            this.label_Port.TabIndex = 3;
            this.label_Port.Text = "端口";
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(25, 113);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(65, 12);
            this.label_Name.TabIndex = 4;
            this.label_Name.Text = "数据库名称";
            // 
            // textBox_DBName
            // 
            this.textBox_DBName.Location = new System.Drawing.Point(96, 109);
            this.textBox_DBName.Name = "textBox_DBName";
            this.textBox_DBName.Size = new System.Drawing.Size(110, 21);
            this.textBox_DBName.TabIndex = 5;
            // 
            // label_UserName
            // 
            this.label_UserName.AutoSize = true;
            this.label_UserName.Location = new System.Drawing.Point(25, 153);
            this.label_UserName.Name = "label_UserName";
            this.label_UserName.Size = new System.Drawing.Size(41, 12);
            this.label_UserName.TabIndex = 6;
            this.label_UserName.Text = "用户名";
            // 
            // textBox_Password
            // 
            this.textBox_Password.Location = new System.Drawing.Point(96, 189);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(110, 21);
            this.textBox_Password.TabIndex = 7;
            // 
            // textBox_UsNamd
            // 
            this.textBox_UsNamd.Location = new System.Drawing.Point(96, 149);
            this.textBox_UsNamd.Name = "textBox_UsNamd";
            this.textBox_UsNamd.Size = new System.Drawing.Size(110, 21);
            this.textBox_UsNamd.TabIndex = 8;
            // 
            // label_PassWord
            // 
            this.label_PassWord.AutoSize = true;
            this.label_PassWord.Location = new System.Drawing.Point(25, 193);
            this.label_PassWord.Name = "label_PassWord";
            this.label_PassWord.Size = new System.Drawing.Size(29, 12);
            this.label_PassWord.TabIndex = 9;
            this.label_PassWord.Text = "密码";
            // 
            // MySqlDBTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 231);
            this.Controls.Add(this.panel1);
            this.Name = "MySqlDBTest";
            this.Text = "MySqlDBTest";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.TextBox textBox_Server;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.TextBox textBox_DBName;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label_PassWord;
        private System.Windows.Forms.TextBox textBox_UsNamd;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Label label_UserName;
    }
}