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
    public partial class Dialog_Con_Model : Form
    {
        #region 初始化
         private CheckBox[] checkBox;
        private TextBox[] textBox;
        private int DataTye;
        public Dialog_Con_Model()
        {
            InitializeComponent();
           
        }
        /// <summary>
        /// 参数模型配置窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dialog_Con_Model_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            InitWindow();
            modelData = new List<CommandDeal.IModel_Data>();
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

        public List<CommandDeal.IModel_Data> modelData { get; private set; }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            modelData.Clear();
            for (int i = 0; i < 20; i++)
            {
                if (checkBox[i].Checked)
                { 
                    string name = "PARA" + (i + 1).ToString("00");
                    try
                    {
                        float value = Convert.ToSingle(this.textBox[i].Text);
                        var model = new CommandDeal.Model_Data(name, value, DataTye);
                        modelData.Add(model);
                    }
                    catch
                    {
                        MessageBox.Show("请输入参数" + (i + 1).ToString() + "正确的配置参数");
                        return;
                    }
                    
                }
            }
            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Dispose();


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
                DataTye = 0x00;
            if (radioButton2.Checked)
                DataTye = 0x01;
            if (radioButton3.Checked)
                DataTye = 0x02;
        }
    }
}
