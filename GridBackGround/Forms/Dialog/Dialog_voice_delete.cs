using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridBackGround.Forms
{
    public partial class Dialog_voice_delete : Form
    {
        public Dialog_voice_delete()
        {
            InitializeComponent();
            this.CenterToParent();
            
        }
        private void Dialog_Con_MianTime_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.button_OK;
            this.CancelButton = this.button_Cancel;

            VoiceType = 0;
        }


        #region 公共变量
        /// <summary>
        /// 数据采集周期
        /// </summary>
        public int  VoiceType  { get; set; }
        #endregion

        private void button_OK_Click(object sender, EventArgs e)
        {
            VoiceType = (int)this.numericUpDown1.Value;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Dispose();
        }      
       
    }
}
