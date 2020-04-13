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
    public partial class Dialog_FormWD : Form
    {
        public Dialog_FormWD()
        {
            InitializeComponent();
        }
        List<CommandDeal.WD_Form> Wd_Form;
        ISeries SeriesX,SeriesY,SeriesZ;
        IAnnotationTool annotion;
        private void Dialog_Form_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.button_Update;
            IToolList toolList = this.axTChart1.Tools;
            ITools tools = toolList.Items[3];

            annotion = tools.asAnnotation;
            annotion.Text = "坐标值";
            SeriesX = this.axTChart1.Series(0);     //获得曲线句柄
            SeriesY = this.axTChart1.Series(1);     
            SeriesZ = this.axTChart1.Series(2);

            Wd_Form = CommandDeal.Data_WD_Form.List_WD; //获得数据源
          
            if (Wd_Form == null)
                return;
            if (Wd_Form.Count == 0)
                return;
            for (int i = 0; i < Wd_Form.Count; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Text = Wd_Form[i].CMD_ID;
                cbi.Value = i;
                this.comboBox1.Items.Add(cbi);
            }
            this.comboBox1.SelectedIndex = 0;
        }
        /// <summary>
        /// 选择站点ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox2.Items.Clear();
            int no = (int)((ComboBoxItem)this.comboBox1.SelectedItem).Value;
            for (int i = 0; i < Wd_Form[no].Forms.Count; i++)
            {
                var form = Wd_Form[no].Forms[i];
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Text = form.Unit_No.ToString();
                cbi.Value = i;
                this.comboBox2.Items.Add(cbi);
            }
            if (comboBox2.Items.Count > 0)
                this.comboBox2.SelectedIndex = 0;
        }
        /// <summary>
        /// 生成曲线按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            SeriesX.Clear();
            SeriesY.Clear();
            SeriesZ.Clear();

            float[] data = Wd_Form[EquNO].Forms[UnitNO].Data;
            for (int i = 0; i < data.Length / 3; i++)
            {
                SeriesX.Add(data[i * 3], i.ToString(), (uint)i);
                SeriesY.Add(data[i * 3 + 1], i.ToString(), (uint)i);
                SeriesZ.Add(data[i * 3 + 2], i.ToString(), (uint)i);
            }
            this.textBox1.Text = Wd_Form[EquNO].Forms[UnitNO].Time.ToString();
        }
        /// <summary>
        /// 刷新数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Wd_Form = CommandDeal.Data_WD_Form.List_WD;
            if (Wd_Form == null)
                return;
            if (Wd_Form.Count == 0)
                return;
            this.comboBox1.Items.Clear();
            this.comboBox2.Items.Clear();
            for (int i = 0; i < Wd_Form.Count; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Text = Wd_Form[i].CMD_ID;
                cbi.Value = i;
                this.comboBox1.Items.Add(cbi);
            }
            this.comboBox1.SelectedIndex = 0;
        }
        /// <summary>
        /// 鼠标移动显示点坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axTChart1_OnMouseMove(object sender, AxTeeChart.ITChartEvents_OnMouseMoveEvent e)
        {
            int TEMPX, TEMPY;
            double MINX, MAXX, XVALUE;
            TEMPX = e.x; TEMPY = e.y;
            MINX = this.axTChart1.Axis.Bottom.MinXValue();//X轴最小值             
            MAXX = this.axTChart1.Axis.Bottom.MaxXValue();//X轴最大值 
            XVALUE = this.axTChart1.Axis.Bottom.CalcPosPoint(TEMPX);//X轴坐标     
            if (XVALUE > MINX && XVALUE <= MAXX)  //显示坐标轴范围内数据     
            {
                double seriesX = SeriesX.YValues.get_Value((int)XVALUE);
                double seriesY = SeriesY.YValues.get_Value((int)XVALUE);
                double seriesZ = SeriesX.YValues.get_Value((int)XVALUE);
                annotion.Text = "点号：" + ((int)XVALUE).ToString() + " 值：X方向位移:" + seriesX.ToString("f2")
                    + "   Y方向位移:" + seriesY.ToString("f2")
                    + "   Z方向位移:" + seriesZ.ToString("f2");
            }
        }

       
    }
}
