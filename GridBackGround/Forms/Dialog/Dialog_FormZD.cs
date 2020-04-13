using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TeeChart;
using Tools;


namespace GridBackGround.Forms
{
    public partial class Dialog_Form : Form
    {
        public Dialog_Form()
        {
            InitializeComponent();
        }
        #region 私有变量
        List<CommandDeal.WFZD_Forms> Wfzd_Form;
        ISeries Series1;
        IAnnotationTool annotion;
        #endregion
        
        private void Dialog_Form_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button_Update;
            IToolList toolList = this.axTChart1.Tools;
            ITools tools = toolList.Items[3];

            annotion = tools.asAnnotation;
            annotion.Text = "坐标值";
            Series1 = this.axTChart1.Series(0);
            Wfzd_Form = CommandDeal.Data_ZD_Form.List_WFZD;
            if (Wfzd_Form == null)
                return;
            if (Wfzd_Form.Count == 0)
                return;
            for (int i = 0; i < Wfzd_Form.Count; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Text = Wfzd_Form[i].CMD_ID;
                cbi.Value = i;
                this.comboBox1.Items.Add(cbi);
            }
            this.comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox2.Items.Clear();
            if (this.comboBox1.SelectedItem == null)
            {
                MessageBox.Show("您没有选择任何装置!");
                return;
            }
            int no = (int)((ComboBoxItem)this.comboBox1.SelectedItem).Value;
            for (int i = 0; i < Wfzd_Form[no].Forms.Count; i++)
            {
                var form = Wfzd_Form[no].Forms[i];
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Text = form.Unit_No.ToString();
                cbi.Value = i;
                this.comboBox2.Items.Add(cbi);
            }
            if(comboBox2.Items.Count>0)
                this.comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.comboBox1.SelectedItem == null)
            {
                MessageBox.Show("您没有选择任何装置!");
                return;
            }
            if (this.comboBox2.SelectedItem == null)
            {
                MessageBox.Show("您没有选择任何采集单元!");
                return;
            }
            int EquNO = (int)((ComboBoxItem)this.comboBox1.SelectedItem).Value;
            int UnitNO = (int)((ComboBoxItem)this.comboBox2.SelectedItem).Value;
            
            Series1.Clear();

            float[] data = Wfzd_Form[EquNO].Forms[UnitNO].Data;
            for (int i = 0; i < data.Length; i++)
            {
                Series1.Add(data[i], i.ToString(), (uint)i);
            }
            this.textBox1.Text = Wfzd_Form[EquNO].Forms[UnitNO].Time.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Wfzd_Form = CommandDeal.Data_ZD_Form.List_WFZD;
            if (Wfzd_Form == null)
                return;
            if (Wfzd_Form.Count == 0)
                return;
            this.comboBox1.Items.Clear();
            this.comboBox2.Items.Clear();
            for (int i = 0; i < Wfzd_Form.Count; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Text = Wfzd_Form[i].CMD_ID;
                cbi.Value = i;
                this.comboBox1.Items.Add(cbi);
            }
            this.comboBox1.SelectedIndex = 0;
        }

        private void axTChart1_OnMouseMove(object sender, AxTeeChart.ITChartEvents_OnMouseMoveEvent e)
        {
            int TEMPX, TEMPY;
            double MINX, MAXX, XVALUE, YVALUE;
            TEMPX = e.x; TEMPY = e.y;
            MINX = this.axTChart1.Axis.Bottom.MinXValue();//X轴最小值             
            MAXX = this.axTChart1.Axis.Bottom.MaxXValue();//X轴最大值 
            XVALUE = this.axTChart1.Axis.Bottom.CalcPosPoint(TEMPX);//X轴坐标     
            if (XVALUE > MINX && XVALUE <= MAXX)  //显示坐标轴范围内数据     
            {
                YVALUE = Series1.YValues.get_Value((int)XVALUE);
                annotion.Text = "点号：" + ((int)XVALUE).ToString() + " 值：" + YVALUE.ToString();
            }
        } 
    }

    
}
