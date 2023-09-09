using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResModel.EQU;

using DB_Operation.EQUManage;
using Tools;

namespace GridBackGround.Forms.EquMan
{
    public enum EQU_Option_Style{
        Add,
        Update,
        Delete,
    }

    public delegate void EQU_Option(EQU_Option_Style style,Equ equ);

    public partial class Dialog_EQU : Form
    {
        #region Public Variable
        private int id_length = 17;
        /// <summary>
        /// 管理按钮点击事件
        /// </summary>
        public event EQU_Option Equ_ManangedEvent;

        private Equ equ = null;
        /// <summary>
        /// 当前管理的设备
        /// </summary>
        public Equ CurrentEqu 
        {
            get
            {
                return GetCurrentEqu();
            }
            set 
            {
                this.equ = value;
                SetCurrentEqu(this.equ);
            } 
        }
        /// <summary>
        /// 当前线路
        /// </summary>
        public Line CurLine 
        {
            get { return (Line)((ComboBoxItem)this.comboBox_DepartMent.SelectedItem).Value; }
            set { 
                var line = value;
                foreach (ComboBoxItem item in this.comboBox_DepartMent.Items)
                {
                    if (((Line)item.Value).NO == line.NO)
                    {
                        this.comboBox_DepartMent.SelectedItem = item;
                        break;
                    }
                }
            } 
        }
        /// <summary>
        /// 当前杆塔信息
        /// </summary>
        public Tower CurTower 
        {
            get { return (Tower)((ComboBoxItem)this.comboBox_Line.SelectedItem).Value; }
            set {
                var tower = value;
                foreach(ComboBoxItem item in this.comboBox_Line.Items)
                {
                    if (((Tower)item.Value).TowerNO == tower.TowerNO)
                    {
                        this.comboBox_Line.SelectedItem = item;
                        break;
                    }
                }
            }
        
        }
        #endregion

        #region Construction
        /// <summary>
        /// Construction
        /// </summary>
        public Dialog_EQU()
        {
            InitializeComponent();
            UserControlInital();
            this.CenterToScreen();
            this.Load += new EventHandler(Dialog_EQU_Load);
        }
        /// <summary>
        /// 用户控件初始化
        void UserControlInital()
        {

            ///ID栏内容变化
            this.textBox_ID.MaxLength                       = 17;
            this.textBox_ID.Tag                             = this.label_IDlength;
            this.textBox_ID.TextChanged                     += new EventHandler(textBox_TextChanged);
            this.label_IDlength.Text                        = "";
            
            //电话栏内容初始化
            this.label_PhoneLength.Text                     = "";
            this.textBox_Phone.Tag                          = this.label_PhoneLength;
            this.textBox_Phone.TextChanged                  += new EventHandler(textBox_TextChanged);
                        
            //杆塔管理按钮
            //this.comboBox_Line.DropDownStyle                = ComboBoxStyle.DropDownList;
            this.linkLabel_Tower_Man.Click                  += new EventHandler(linkLabel_LinkClicked);
            this.linkLabel_Tower_Update.Click               += new EventHandler(linkLabel_LinkClicked);
            //Url管理
            //this.comboBox_Url.DropDownStyle                 = ComboBoxStyle.DropDownList;
            this.comboBox_Url.SelectedIndexChanged          += new EventHandler(comboBox_SelectedIndexChanged);
            this.linkLabel_Url_Update.Click                 += new EventHandler(linkLabel_LinkClicked);
            this.linkLabel_Url_Man.Click                    += new EventHandler(linkLabel_LinkClicked);
           
            //单位管理
            //this.comboBox_DepartMent.DropDownStyle          = ComboBoxStyle.DropDownList;
            this.comboBox_DepartMent.SelectedIndexChanged   += new EventHandler(comboBox_SelectedIndexChanged);
            this.linkLabel_Department_Man.Click             += new EventHandler(linkLabel_LinkClicked);
            this.linkLabel_DepartMent_Update.Click          += new EventHandler(linkLabel_LinkClicked);
            
            //添加删除修改按钮点击事件
            this.button_Add.Tag                             = EQU_Option_Style.Add;
            this.button_Update.Tag                          = EQU_Option_Style.Update;
            this.button_Delete.Tag                          = EQU_Option_Style.Delete;
            this.button_Add.Click                           += new EventHandler(button_Click);
            this.button_Update.Click                        += new EventHandler(button_Click);
            this.button_Delete.Click                        += new EventHandler(button_Click);
            this.CurrentEqu = null;
        }
       
        
        #endregion

        #region 控件相关事件

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Dialog_EQU_Load(object sender, EventArgs e)
        {
            this.linkLabel_LinkClicked(this.linkLabel_Url_Update,           new EventArgs());
            //this.linkLabel_LinkClicked(this.linkLabel_Tower_Update,         new EventArgs());
            this.linkLabel_LinkClicked(this.linkLabel_DepartMent_Update,    new EventArgs());

            //if (this.CurrentEqu == null)
            //    CurrentEqu = new Equ();
            if (Config.SettingsForm.Default.ServiceMode == "nw")
            {
                this.id_length = 6;
                this.label_URL.Visible = false;
                this.comboBox_Url.Visible = false;
                this.linkLabel_Url_Man.Visible = false;
                this.linkLabel_Url_Update.Visible = false;
                this.textBox_ID.Text = "ZT1234";
            }
        }
        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            try
            {   
                //删除按钮点击
                if ((EQU_Option_Style)button.Tag != EQU_Option_Style.Delete)
                    OnEquManangeEvent((EQU_Option_Style)button.Tag, this.CurrentEqu);
                else
                {
                    if (this.CurTower == null)
                    {
                        MessageBox.Show("所属线路不能为空，请先选择所属线路");
                        return;
                    }
                    if (this.textBox_ID.TextLength != this.id_length)
                    {
                        MessageBox.Show(string.Format("设备ID为{0}位，请重新输入设备ID",this.id_length));
                    }
                    //添加更新按钮点击
                    OnEquManangeEvent((EQU_Option_Style)button.Tag, this.equ);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// URL接口选择变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ComboBox)sender == this.comboBox_DepartMent)   //单位选择变化
            {
                UpdateTowerList();
            }
        }

        /// <summary>
        /// 连接按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void linkLabel_LinkClicked(object sender, EventArgs e)
        {
            if ((LinkLabel)sender == this.linkLabel_Url_Update)             //刷新URL列表
                UpdateUrlList();
            if ((LinkLabel)sender == this.linkLabel_Tower_Update)           //刷新所属线路
                UpdateTowerList();
            if ((LinkLabel)sender == this.linkLabel_DepartMent_Update)      //刷新所属单位
                UpdateLineList();


            if ((LinkLabel)sender == this.linkLabel_Url_Man)                //管理Url列表
            {
                Dialog_UrlInterface_Man d_urlMan = new Dialog_UrlInterface_Man();
                d_urlMan.Show();
                this.linkLabel_LinkClicked(this.linkLabel_Url_Update,new EventArgs());
            }
            if ((LinkLabel)sender == this.linkLabel_Tower_Man)              //管理线路列表
            {
                Dialog_Tower_Man d_tower = new Dialog_Tower_Man();
                d_tower.ShowDialog();
                this.linkLabel_LinkClicked(this.linkLabel_Tower_Update, e);
            }
            if ((LinkLabel)sender == this.linkLabel_Department_Man)         //管理所属单位
            {
                Dialog_Line_Man d_line = new Dialog_Line_Man();
                d_line.ShowDialog();
                this.linkLabel_LinkClicked(this.linkLabel_DepartMent_Update, e);
            }
           
        }


        /// <summary>
        /// ID栏文字变化时长度提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textSender = ((TextBox)sender);
            //获取关联lable控件
            if (textSender.Tag == null)
                return;
            Label lable = (Label)textSender.Tag;

            //更新文本框内容长度
            int lenth = textSender.TextLength;
            if (lenth > 0)
                lable.Text = lenth.ToString();
            else
                lable.Text = "";
            //更新文本内容颜色
            if (textSender.Name == "textBox_ID")        //ID栏文字颜色变化
            {
                if (lenth == this.id_length)
                    this.label_IDlength.ForeColor = Color.Green;
                else
                    this.label_IDlength.ForeColor = Color.Red;
            }
        }
        #endregion

        #region 自定义事件处理
        /// <summary>
        /// 自定义事件
        /// </summary>
        /// <param name="style"></param>
        /// <param name="equ"></param>
        public void OnEquManangeEvent(EQU_Option_Style style, Equ equ)
        {
            if (Equ_ManangedEvent != null)
                Equ_ManangedEvent(style, equ);
        }
        #endregion

        #region Private Fuctions
        /// <summary>
        /// 设置当前设备
        /// </summary>
        /// <param name="equ"></param>
        void SetCurrentEqu(Equ equ)
        {
            if (this.equ == null)
            {
                this.textBox1.Text = "";
                this.button_Update.Enabled = false;
                this.button_Delete.Enabled = false;
                this.button_Add.Enabled = true;
                return;
            }
            else
            {
                this.button_Add.Enabled = true;
                this.button_Update.Enabled = true;
                this.button_Delete.Enabled = true;
            }
            this.textBox2.Text = equ.MarketText;
            this.textBox1.Text = equ.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.textBox_Name.Text = equ.Name;
            this.textBox_ID.Text = equ.EquID;
            this.textBox_Phone.Text = equ.Phone;
            this.textBox_EquNumber.Text = equ.EquNumber;
            this.checkBox_isname.Checked = equ.IS_Mark;
            this.checkBox_istime.Checked = equ.Is_Time;
            
            ComboxSeletUrl(equ.UrlID);
            ComboxSelectTower(equ.TowerNO);
            
        }
        /// <summary>
        /// 获取当前设备信息
        /// </summary>
        Equ GetCurrentEqu()
        {
            if (this.textBox_Name.TextLength == 0)
                throw new Exception("设备名称不能为空");
            if (this.comboBox_Line.SelectedIndex == 0)
                throw new Exception("请选择设备所属线路");
            if (this.comboBox_Line.SelectedItem == null)
                throw new Exception("请选择设备所属线路");
            if (this.textBox_EquNumber.TextLength == 0)
                throw new Exception("设备编号不能为空");
            if (this.textBox_ID.TextLength != this.id_length)
                throw new Exception(string.Format("ID长度错误，应该为{0}",this.id_length));
           

            if (equ == null)
                equ = new Equ();
            equ.MarketText = this.textBox2.Text;
            equ.Name = this.textBox_Name.Text;
            equ.EquID = this.textBox_ID.Text;
            equ.EquNumber = this.textBox_EquNumber.Text;
            equ.Phone = this.textBox_Phone.Text;
            equ.TowerNO = ((Tower)((ComboBoxItem)this.comboBox_Line.SelectedItem).Value).TowerNO;
            equ.IS_Mark = this.checkBox_isname.Checked;
            equ.Is_Time = this.checkBox_istime.Checked;
            if(this.comboBox_Url.SelectedValue != null) //选择上传的服务器接口
            {
                equ.UrlID = ((UrlInterFace)((ComboBoxItem)this.comboBox_Url.SelectedItem).Value).ID;
            }
            return equ;
        }

        /// <summary>
        /// 刷新杆塔列表
        /// </summary>
        void UpdateTowerList()
        {
            this.comboBox_Line.Items.Clear();
            if (this.comboBox_DepartMent.SelectedItem == null)
                return;
            var line = (Line)((ComboBoxItem)this.comboBox_DepartMent.SelectedItem).Value;
            UpdateTowerList(line);
        }

        void UpdateTowerList(Line line)
        {
            if(line == null)
                throw new ArgumentNullException("line");
            UpdateTowerList(line.NO);
        }
        List<Tower> TowerList = new List<Tower>();
        void UpdateTowerList(int lineNO)
        {
            //Combox 刷新
            this.comboBox_Line.Items.Clear();

            ComboBoxItem nullitem = new ComboBoxItem();
            nullitem.Text = "";
            nullitem.Value = new Tower() { TowerNO = 0 };
            this.comboBox_Line.Items.Add(nullitem);
           
            this.comboBox_Line.SelectedIndex = 0;
            //获取杆塔列表
            TowerList = DB_Tower.List(lineNO);;
            //this.TowerList.Clear();
            foreach (Tower tower in TowerList)
            {
                ComboBoxItem item = new ComboBoxItem(tower.TowerName, tower);
                this.comboBox_Line.Items.Add(item);
                if (this.equ == null)
                    continue;
                if (this.equ.TowerNO == tower.TowerNO)
                    this.comboBox_Line.SelectedItem = item;
            }
        }
        /// <summary>
        /// 线路列表 模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Line_TextUpdate(object sender, EventArgs e)
        {
            this.comboBox_Line.Items.Clear();

            ComboBoxItem nullitem = new ComboBoxItem();
            nullitem.Text = "";
            nullitem.Value = new Tower() { TowerNO = 0 };
            this.comboBox_Line.Items.Add(nullitem);

            foreach(Tower tower in TowerList)
            {
                if (!tower.TowerName.Contains(this.comboBox_Line.Text))
                    continue;
                ComboBoxItem item = new ComboBoxItem(tower.TowerName, tower);
                this.comboBox_Line.Items.Add(item);
                //if (this.equ == null)
                //    continue;
                //if (this.equ.TowerNO == tower.TowerNO)
                //    this.comboBox_Line.SelectedItem = item;
            }


            //设置光标位置，否则光标位置始终保持在第一列，造成输入关键词的倒序排列

            this.comboBox_Line.SelectionStart = this.comboBox_Line.Text.Length;

            //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置。

            Cursor = Cursors.Default;

            //自动弹出下拉框

            this.comboBox_Line.DroppedDown = true;
        }


        /// <summary>
        /// 刷新URL列表
        /// </summary>
        void UpdateUrlList()
        {
            this.comboBox_Url.Items.Clear();

            DB_Url dburl = new DB_Url();
            var urllist = dburl.GetUrlList();
            this.comboBox_Url.Items.Add(new ComboBoxItem("", null));
            this.comboBox_Url.SelectedIndex = 0;

            foreach (UrlInterFace url in urllist)
            {
                ComboBoxItem item = new ComboBoxItem(url.Nanme, url);
                this.comboBox_Url.Items.Add(item);
                if (equ == null) continue;
                if (equ.UrlID == url.ID)
                    this.comboBox_Url.SelectedItem = item;
            }
        }
        /// <summary>
        /// 刷新单位列表
        /// </summary>
        private void UpdateLineList()
        {
            var lineList = new DB_Line().List();
            this.comboBox_DepartMent.Items.Clear();

            ComboBoxItem nullitem = new ComboBoxItem();
            nullitem.Text = "";
            nullitem.Value = new Line() { NO = 0 };
            this.comboBox_DepartMent.Items.Add(nullitem);
            foreach (Line line in lineList)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = line.Name;
                item.Value = line;
                this.comboBox_DepartMent.Items.Add(item);
            }
        }



        /// <summary>
        ///  根据URL选择Combox选项
        /// </summary>
        /// <param name="url"></param>
        void ComboxSeletUrl(UrlInterFace url)
        {
            ComboxSeletUrl(url.ID);
        }
        /// <summary>
        /// 选择杆塔列表
        /// </summary>
        /// <param name="tower"></param>
        void ComboxSelectTower(Tower tower)
        {
            ComboxSelectTower(tower.TowerNO);
        }

        void ComboxSelectTower(int id)
        {
            this.comboBox_Line.SelectedItem = this.comboBox_Line.Items[0];
            foreach (ComboBoxItem item in comboBox_Line.Items)
            {
                if (item.Value == null) continue;
                Tower tower = (Tower)item.Value;
                if (tower.TowerNO == id)
                    this.comboBox_Line.SelectedItem = item;
            }
        }
        /// <summary>
        /// 根据URL的id选择Combox选项
        /// </summary>
        /// <param name="urlid"></param>
        void ComboxSeletUrl(int urlid)
        {
            this.comboBox_Url.SelectedItem = this.comboBox_Url.Items[0];
            foreach (var comboxitem in comboBox_Url.Items)
            {
                ComboBoxItem item = (ComboBoxItem)comboxitem;
                if (item.Value == null)
                    continue;
                var curUrl = (UrlInterFace)item.Value;
                if (curUrl.ID == urlid)
                    comboBox_Url.SelectedItem = comboxitem;
            }
        }
        #endregion

        private void linkLabel_DepartMent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

    }

 
}
