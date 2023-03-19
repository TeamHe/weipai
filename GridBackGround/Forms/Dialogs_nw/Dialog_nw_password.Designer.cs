namespace GridBackGround.Forms.Dialogs_nw
{
    partial class Dialog_nw_password
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
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_password_old = new System.Windows.Forms.TextBox();
            this.button_Cancle = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_password_new = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "原密码";
            // 
            // textBox_password_old
            // 
            this.textBox_password_old.Location = new System.Drawing.Point(92, 20);
            this.textBox_password_old.Name = "textBox_password_old";
            this.textBox_password_old.Size = new System.Drawing.Size(124, 21);
            this.textBox_password_old.TabIndex = 28;
            this.textBox_password_old.Text = "1234";
            // 
            // button_Cancle
            // 
            this.button_Cancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancle.Location = new System.Drawing.Point(127, 101);
            this.button_Cancle.Name = "button_Cancle";
            this.button_Cancle.Size = new System.Drawing.Size(75, 23);
            this.button_Cancle.TabIndex = 23;
            this.button_Cancle.Text = "取消";
            this.button_Cancle.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(12, 101);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 22;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "新密码";
            // 
            // textBox_password_new
            // 
            this.textBox_password_new.Location = new System.Drawing.Point(92, 54);
            this.textBox_password_new.Name = "textBox_password_new";
            this.textBox_password_new.Size = new System.Drawing.Size(124, 21);
            this.textBox_password_new.TabIndex = 30;
            this.textBox_password_new.Text = "1234";
            // 
            // Dialog_nw_password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 155);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_password_new);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_password_old);
            this.Controls.Add(this.button_Cancle);
            this.Controls.Add(this.button_OK);
            this.Name = "Dialog_nw_password";
            this.Text = "设置装置密码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_password_old;
        private System.Windows.Forms.Button button_Cancle;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_password_new;
    }
}