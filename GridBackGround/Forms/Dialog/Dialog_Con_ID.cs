using ResModel.gw;
using System;
using System.Windows.Forms;

namespace GridBackGround.Forms
{
    public partial class Dialog_Con_ID : Form
    {
        #region Construction
        /// <summary>
        /// construction
        /// </summary>
        public Dialog_Con_ID()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_ID_Load(object sender, EventArgs e)
        {
            this.CancelButton = button_Cancel;
            this.CenterToParent();
            this.AcceptButton = this.button_OK;
            this.CancelButton = this.button_Cancel;
            this.button_Cancel.DialogResult = DialogResult.Cancel;
        }
        #endregion

        private gw_ctrl_id id;
        public gw_ctrl_id ID 
        {
            get { return id; }
            set { this.id = value;
                if (this.id == null)
                    return;
                this.numericUpDown1.Value = id.NO;
                this.textBox_Component_ID.Text = id.ComponentID;
                this.textBox_NEW_CMD_ID.Text = id.NEW_CMD_ID;
                this.textBox_Original_ID.Text = id.OriginalID;
                this.checkBox_Component_ID.Checked = id.GetFlag((int)gw_ctrl_id.EFlag.ComponentID);
                this.checkBox_NEW_CMD_ID.Checked = id.GetFlag((int)gw_ctrl_id.EFlag.NEW_CMD_ID);
            }
        }

        public int Flag { get; set; }
        


        public bool Query { get; set; }

        /// <summary>
        /// 确定按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            if(id == null)
                id = new gw_ctrl_id();
            id.NO = (int)this.numericUpDown1.Value;
            id.SetFlag((int)gw_ctrl_id.EFlag.NEW_CMD_ID, this.checkBox_NEW_CMD_ID.Checked);
            id.SetFlag((int)gw_ctrl_id.EFlag.ComponentID, this.checkBox_Component_ID.Checked);
            if (checkBox_Component_ID.Checked)    //设置标识位 ____被测设备ID
            {
                if (this.textBox_Component_ID.TextLength == 17)
                    this.id.ComponentID = this.textBox_Component_ID.Text;
                else
                {
                    MessageBox.Show("请输入正确的被测设备ID，长度为17位！");
                    return;
                }
            }
            if (checkBox_NEW_CMD_ID.Checked)  //设置标识位——设备ID
            {
                if (this.textBox_NEW_CMD_ID.TextLength == 17)
                    this.id.NEW_CMD_ID = this.textBox_NEW_CMD_ID.Text;
                else
                {
                    MessageBox.Show("请输入正确的装置ID，长度为17位！");
                    return;
                }
            }
            if (this.textBox_Original_ID.TextLength == 17)
                this.id.OriginalID = this.textBox_Original_ID.Text;
            else
            {
                MessageBox.Show("请输入正确的原始ID，长度为17位！");
                return;
            }
            this.Query = false;
            this.DialogResult = DialogResult.OK;
        }

        private void textBox_Component_ID_TextChanged(object sender, EventArgs e)
        {
            this.label_Component_ID.Text = this.textBox_Component_ID.Text.Length.ToString();
        }

        private void textBox_NEW_CMD_ID_TextChanged(object sender, EventArgs e)
        {
            this.label_ID.Text = this.textBox_NEW_CMD_ID.Text.Length.ToString();
        }

        private void textBox_Original_ID_TextChanged(object sender, EventArgs e)
        {
            this.label_OrgID.Text = this.textBox_Original_ID.Text.Length.ToString();
        }

        private void button_query_Click(object sender, EventArgs e)
        {
            this.Flag = (byte)(1 << ((int)this.numericUpDown1.Value + 2));
            this.Query = true;
            this.DialogResult= DialogResult.OK;
        }
    }
}
