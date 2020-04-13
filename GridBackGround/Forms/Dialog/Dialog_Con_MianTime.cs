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
    public partial class Dialog_Con_MianTime : Form
    {
        public Dialog_Con_MianTime()
        {
            InitializeComponent();
            this.CenterToParent();
            
        }
        private void Dialog_Con_MianTime_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.button1;
            this.CancelButton = this.button_Cancel;


            this.comboBox1.SelectedIndex = 0;
            Data_Type = 0;
            Main_Time = 0;
            Heart_Time = 0;
        }


        #region 公共变量
        /// <summary>
        /// 设备类型
        /// </summary>
        public byte Data_Type  
        {
            get { return (byte)(this.comboBox1.SelectedIndex + 1); }
            set
            {
                if (value >= 1 && value <= 10)
                    this.comboBox1.SelectedIndex = value - 1;
                else
                    this.comboBox1.SelectedIndex = 0;
                }
        }
        /// <summary>
        /// 数据采集周期
        /// </summary>
        public int  Main_Time  { get; set; }
        /// <summary>
        /// 心跳周期
        /// </summary>
        public int  Heart_Time { get; set; }
        /// <summary>
        /// 配置标致位
        /// </summary>
        public int  Flag       { get; set; }
        //查询/设定
        public bool Query      
        { 
            get; 
            set; 
        }
        #endregion

        private void button_OK_Click(object sender, EventArgs e)
        {
            byte data_type = 0;
            int  main_time = 0;
            int heart_time = 0;
            int flag = 0;
            
            #region 采样周期
            if (checkBox_MianTime.Checked)    //设置标识位——采样周期
            {
                flag += 1;
                try
                {
                    main_time = Int16.Parse(this.textBox_MainTime.Text);         //获得端口号
                }
                catch
                {
                    MessageBox.Show("请输入正确采样周期");
                    return;
                }
                if(main_time >65535)
                {
                    MessageBox.Show("请输入正确采样周期");
                    return ;
                }
            }
            #endregion
            
            #region 心跳周期
            if (checkBox_HeartBeat.Checked)  //设置标识位——心跳周期
            {
                flag += 2;
                try
                {
                    heart_time =Int32.Parse(this.textBox_HeartTime.Text);   //获得IP地址
                    if (heart_time > 512)
                    {
                        MessageBox.Show("请输入正确的心跳周期！");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("请输入正确的心跳周期！");
                    return;
                }
                
            }
	        #endregion
            data_type = (byte)((this.comboBox1.SelectedIndex + 1) & 0xff);
           
            Main_Time = main_time;
            Heart_Time = heart_time;
            Data_Type = data_type;
            Flag = flag;

            Query = false;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Dispose();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Dispose();
        }

        private void buttonQuary_Click(object sender, EventArgs e)
        {
            byte data_type  = (byte)((this.comboBox1.SelectedIndex + 1) & 0xff);
            Query = true;
            Data_Type = data_type;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Dispose();
        }

        private void textBox_Int_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!(e.KeyChar >= 0x30 && e.KeyChar <= 0x39)) && (e.KeyChar!= 0x08))
            { e.Handled = true; }
        }

        
       
    }
}
