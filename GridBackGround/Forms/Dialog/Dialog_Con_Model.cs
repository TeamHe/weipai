using System;
using System.Windows.Forms;
using ResModel.gw;

namespace GridBackGround.Forms
{
    public partial class Dialog_Con_Model : Form
    {
        #region 初始化
         private CheckBox[] checkBox;
        private TextBox[] textBox;
        private gw_ctrl_model.EType DataTye;
        public Dialog_Con_Model()
        {
            InitializeComponent();
           
        }

        public gw_ctrl_models Models { get; set; }

        /// <summary>
        /// 参数模型配置窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_Con_Model_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            InitWindow();
            this.Models = new gw_ctrl_models();
            this.AcceptButton = button_OK;
            this.CancelButton = this.button_Cancel;
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

       /// <summary>
       /// 界面初始化
       /// </summary>
        public void InitWindow()
        {
            int xStart;
            //checkBox
            checkBox = new CheckBox[20];
            for (int i = 1; i <= 20; i++)
            {
                if (i <= 10)
                    xStart = 28;
                else
                    xStart = 250;
                checkBox[i - 1] = new CheckBox();
                checkBox[i - 1].AutoSize = true;

                checkBox[i - 1].Location = new System.Drawing.Point(xStart, 12 + (i - 1) % 10 * 30);
                checkBox[i - 1].Name = "checkBox" + i.ToString();
                checkBox[i - 1].Size = new System.Drawing.Size(54, 16);
                checkBox[i - 1].TabIndex = 0;
                checkBox[i - 1].Text = "参数" + i.ToString();
                checkBox[i - 1].UseVisualStyleBackColor = true;

                this.panel1.Controls.Add(checkBox[i - 1]);
            }
            //textBox
            textBox = new TextBox[20];
            for (int i = 1; i <= 20; i++)
            {
                if (i <= 10)
                    xStart = 90;
                else
                    xStart = 312;
                textBox[i - 1] = new TextBox();
                textBox[i - 1].Location = new System.Drawing.Point(xStart, 9 + (i - 1) % 10 * 30);
                textBox[i - 1].Name = "textBox" + i.ToString();
                textBox[i - 1].Size = new System.Drawing.Size(100, 20);
                textBox[i - 1].TabIndex = i;
                textBox[i - 1].KeyPress += new KeyPressEventHandler(textBox_Float_KeyPress);

                this.panel1.Controls.Add(textBox[i - 1]);
            }

            this.radioButton3.Checked = true;
        }
        #endregion

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            this.Models.Models.Clear();
            //modelData.Clear();
            for (int i = 0; i < 20; i++)
            {
                if (!checkBox[i].Checked)
                    continue;
                try
                {
                    float value = Convert.ToSingle(this.textBox[i].Text);
                    var model = new gw_ctrl_model()
                    {
                        Key = string.Format("PARA{0:D2}", i + 1),
                        Value = value,
                        Type = this.DataTye,
                    };
                    Models.Models.Add(model);
                }
                catch
                {
                    MessageBox.Show("请输入参数" + (i + 1).ToString() + "正确的配置参数");
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void textBox_Float_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((!(e.KeyChar >= 0x30 && e.KeyChar <= 0x39)) 
            //    && ((e.KeyChar != 0x08)&&(e.KeyChar!='.')))
            //{ e.Handled = true; }
        }

        /// <summary>
        /// 数据类型选择变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                DataTye = gw_ctrl_model.EType.U32;
            if (radioButton2.Checked)
                DataTye = gw_ctrl_model.EType.S32 ;
            if (radioButton3.Checked)
                DataTye = gw_ctrl_model.EType.Single ;
        }
    }
}
