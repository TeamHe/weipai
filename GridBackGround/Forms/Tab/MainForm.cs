using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;
using Sodao.FastSocket.Server.Protocol;
using System.IO;
using GridBackGround.Termination;
using GridBackGround.CommandDeal.nw;
using GridBackGround.Forms.Dialogs_nw;
using GridBackGround.Forms;
using ResModel;

namespace GridBackGround
{
    public delegate string GetEquNameHandler(string ID);
    public partial class MainForm : Form
    {
        private string CMD_ID = null;
        bool Flag_TabChange = false;

        public MenuItem_nw menu_nw = null;

        private HTTP.HttpListeners httpListeners = null;

        public MainForm()
        {
            LogHelper.WriteLog("System Start");

            InitializeComponent();
            this.CenterToParent();
            //Forms.EquMan.FormManEquMan fmm= new Forms.EquMan.FormManEquMan();
            //fmm.FormManEquManInit(this.设置ToolStripMenuItem);
        }
        /// <summary>
        /// 界面加载的时候进行的必要的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString()+"系统正在初始化");
            this.Icon = new System.Drawing.Icon("Res\\logo.ico");
            this.notifyIcon1.Icon = new System.Drawing.Icon("Res\\logo.ico");
            var name = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            this.Text = name.Name;
            this.menu_nw = new MenuItem_nw(this, this.设备控制ToolStripMenuItem);
            this.menu_nw.Menuitem_Flush();

            //数据库初始化
            try
            {

                DB_Operation.DB.Init();

                TabInit();

                

                //控件初始化
                ControlsInit();
                //终端管理初始化
                Termination.PowerPoleManage.PowerPoleManageInit();
                //网络端口初始化
                SocketInit();
                //事件初始化
                EventInit();
                //设备管理菜单按键添加
                Forms.EquMan.FormManEquMan fmm = new Forms.EquMan.FormManEquMan();
                fmm.FormManEquManInit(this.设置ToolStripMenuItem);    //设别管理按钮添加
                fmm.TabID = this.TabID;

                //Work.PictureClean.Start();      //图片清理服务启动

                Console.WriteLine(DateTime.Now.ToString() + "系统初始化完成");

               
            }
            catch(Exception ex)
            {
                MessageBox.Show("系统启动失败。" + ex.Message);
                //LogHelper.WriteLog("系统启动失败", ex);
                //throw ex;
            }
            


        }

     
        /// <summary>
        /// ID选择变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tp_CMD_ID_Change(object sender, CMDid_Change e)
        {
            this.CMD_ID = e.CMD_ID;
            if (e.CMD_ID != null && e.CMD_ID.Length >=6 )
            {
                this.toolStripStatusLabel_ID.Text = "当先选中设备：" + e.CMD_NAME;
            }
            else
            {
                this.toolStripStatusLabel_ID.Text = "当前没有选中设备";
            }
        }

        #region 初始化函数
        private void TabInit()
        {
            this.panel_TabControl.Visible = true;
            this.panel_SerialPort.Visible = false;
            ///串口栏初始化
            if (true)
            {

                Form_SerialPort = new FormSerialPort();
                this.panel_SerialPort.Controls.Add(Form_SerialPort);
                //Form_SerialPort.Owner = this;
                Form_SerialPort.Show();

            }
            //ID显示栏初始化
            if (true)
            {
                TabID = new Forms.Tab.Tab_IDs();
                //TabID.Owner = this;
                this.panel_Left.Controls.Add(TabID);
                TabID.CMD_ID_Change += new EventHandler<CMDid_Change>(tp_CMD_ID_Change);

                try
                {
                    TabID.Show();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("无法连接到数据库,点击确定修改数据库连接" + ex.Message);
                    this.数据库测试ToolStripMenuItem_Click(this.数据库测试ToolStripMenuItem,new EventArgs());
                }

                
            }
            #region tab标签栏初始化
            //报文显示栏初始化
            if (true)
            {
                TabPacket = new Tab_Packet();
                this.tabPageGPRS.Controls.Add(TabPacket);
                TabPacket.Dock = DockStyle.Fill;
                this.tabControl1.SelectedTab = tabControl1.TabPages[0];
                TabPacket.Show();
            }
            //解析数据显示栏初始化
            if (true)
            {
                TabReport = new Tab_Report();
                TabReport.m_GetEquName = GetTowerNameByID;
                this.tabPageReport.Controls.Add(TabReport);
                this.tabControl1.SelectedTab = tabControl1.TabPages[1];
                TabReport.TabID = TabID;
                TabReport.Show();
            }
            //历史数据栏初始化
            if (true)
            {
                TabHisData = new Tab_HisData();
                //TabReport.Owner = this;
                TabHisData.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                //this.tabPageData.CursorChanged += new EventHandler(tabPageData_CursorChanged);
                this.tabPageData.Enter += new EventHandler(tabPageData_Enter);
                this.tabPageData.Controls.Add(TabHisData);
                this.tabControl1.SelectedTab = tabControl1.TabPages[2];
                TabHisData.Show();
            }

            if (true)
            {
                TabOnLineStatus = new Forms.Tab.Tab_OnlineStatus();
                this.tabPageOnLineStatus.Controls.Add(TabOnLineStatus);
                this.tabControl1.SelectedTab = tabControl1.TabPages[3];
                TabOnLineStatus.Show();
            }


            #endregion

            if (Config.SettingsForm.Default.ComMode == "SerialPort")
            {
                this.panel_SerialPort.Visible = true;
                this.工作模式ToolStripMenuItem.Text = "网络工作模式";

            }
            if (Config.SettingsForm.Default.ComMode == "Socket")
            {
                this.panel_TabControl.Visible = true;
                this.工作模式ToolStripMenuItem.Text = "串口工作模式";
            }
            //将标签栏切换到上次关闭时的栏
            this.tabControl1.SelectedIndex = Config.SettingsForm.Default.TabIndex;
            //开始记录标签栏切换
            Flag_TabChange = true;

            #region 进入上次关闭时的工作模式

            #endregion
            //



        }

        void tabPageData_Enter(object sender, EventArgs e)
        {
            TabHisData.HisTimeRefresh();
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 网口初始化
        /// </summary>
        private void SocketInit()
        {
            try
            {

                Communicat.Service.reStartCom();
                this.toolStripStatusLabel_Port.Text =
                    "装置端口：" + Config.SettingsForm.Default.CMD_Port + "  WEB端口：" +
                    Config.SettingsForm.Default.WEB_Port + "已成功打开";
            }
            catch (Exception ex)
            {
                MessageBox.Show("端口打开失败：" + ex.Message);
                this.toolStripStatusLabel_Port.Text = ex.Message;
            }
           
          
        }
        /// <summary>
        /// 事件初始化
        /// </summary>
        private void EventInit()
        { 
            
        }
        /// <summary>
        /// 控件初始化
        /// </summary>
        private void ControlsInit()
        {
            //绑定右击控件
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            //this.WindowState = Config.SettingsForm.Default.WindowState;
            if (Config.SettingsForm.Default.WindowState == "Normal")
                this.WindowState = FormWindowState.Normal;
            if (Config.SettingsForm.Default.WindowState == "Maximized")
            {
                this.WindowState = FormWindowState.Maximized;
            }
            //ToolTip tip = new ToolTip();
            //tip.Tag = this.toolStripStatusLabel_Port.Text;
            this.toolStripStatusLabel_Port.Text = "端口状态";
            toolStripStatusLabel_Port.AutoToolTip = true;

            this.toolStripStatusLabel_Port.Width = 250;
            this.toolStripStatusLabel_ID.Text = "当前没有选中设备";
            this.toolStripStatusLabel_ID.AutoToolTip = false;
            this.toolStripStatusLabel_ID.Width = 250;
        }

        void tip_Popup(object sender, PopupEventArgs e)
        {
            //throw new NotImplementedException();
            ToolTip tip = (ToolTip)sender;
 
        }
        #endregion

        #region 远程升级
        private void RemoteUpdate()
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog.Dialog_Update du = new Forms.Dialog.Dialog_Update();
            if (du.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            string fileName = du.Factory_Name;
            try
            {
                CommandDeal.Comand_RemotedUpDate.RunRemotedUpDate(
                    du.Factory_Name,
                    du.Model,
                    du.Hard_Version,
                    du.Soft_Version,
                    du.UpdateTime,
                    du.FileName, CMD_ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion      

        #region  菜单栏

        #region 程序配置
        private void 本机端口设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Dialog_Config config = new Forms.Dialog_Config();
            if (config.ShowDialog(this) == DialogResult.OK)
            {
                SocketInit();
                //if (Communicat.Service.reStartCom(ref port))
                //    this.toolStripStatusLabel_Port.Text = "端口" + port.ToString() + "已成功打开";
            }
        }
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog(this);
            this.Activate();
        }

        private void 工作模式切换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Form_Reset mode = new Form_Reset();
            if (mode.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CommandDeal.Comand_ModeChange.Set(CMD_ID,
                   mode.Mode);
            }
            this.Activate();
        }
      

        #region Ta切换
        private void 显示通讯报文ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabControl1.TabPages[0];
        }

        private void 显示记录列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabControl1.TabPages[1];
        }
        #endregion

        #endregion

        public IPowerPole GetSeletedPole()
        {
            if(this.CMD_ID == null || CMD_ID.Length <6)
            {
                MessageBox.Show("当前没有选中任何设备");
                return null; 
            }

            IPowerPole powerPole = PowerPoleManage.Find(this.CMD_ID);
            if(powerPole == null)
            {
                MessageBox.Show("设备离线");
                return null;
            }
            return powerPole;

        }

        #region 装置配置

        #region 网络适配器
        private void 查询网络适配器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            CommandDeal.Comand_NA.Query(CMD_ID);
        }
        /// <summary>
        /// 网络适配器设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 设置网络适配器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Con_NA na = new Forms.Dialog_Con_NA();
            na.IP = CommandDeal.Comand_NA.IP;
            na.Subnet_Mask = CommandDeal.Comand_NA.Subnet_Mask;
            na.Gateway = CommandDeal.Comand_NA.Gateway;
            na.PhoneNumber = CommandDeal.Comand_NA.PhoneNumber;
            if (na.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CommandDeal.Comand_NA.Set(CMD_ID,
                    na.Flag,
                    na.IP,
                    na.Subnet_Mask,
                    na.Gateway,
                    na.PhoneNumber);
            }
            this.Activate();
        }
        #endregion

        #region 请求历史数据


        private void 历史数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Con_HisData dch = new Forms.Dialog_Con_HisData();
            if (dch.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                //当前数据
                if (dch.CurrentData)                            //请求当前数据
                    CommandDeal.Comand_History.Current(CMD_ID,
                        dch.Data_Type);
                else
                    //请求历史数据
                    CommandDeal.Comand_History.History(CMD_ID,
                        dch.Data_Type,
                        dch.StartTime,
                        dch.EndTime);
            }
            this.Activate();
        }
        #endregion

        #region 采样参数

        private void 设定采样参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())//判断设备选中状态
                return;

            Forms.Dialog_Con_MianTime miantime = new Forms.Dialog_Con_MianTime();
            if (miantime.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                //查询
                if (miantime.Query)
                    CommandDeal.Comand_SamplePeriod.Query(CMD_ID, miantime.Data_Type);
                else
                    //配置
                    CommandDeal.Comand_SamplePeriod.Set(CMD_ID,
                                                    miantime.Data_Type,
                                                    miantime.Flag,
                                                    miantime.Main_Time,
                                                    miantime.Heart_Time);
            }
            this.Activate();
        }
        #endregion

        #region 上位机信息
        /// <summary>
        /// 上位机信息查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 查询上位机信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            CommandDeal.Comand_IP.Query(CMD_ID);
        }
        /// <summary>
        /// 上位机信息设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 设定上位机信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;

            Forms.Dialog_Con_IP conIP = new Forms.Dialog_Con_IP();
            conIP.IP = CommandDeal.Comand_IP.IP_Address;
            conIP.Port = CommandDeal.Comand_IP.Port;
            //conIP.SetFlag = CommandDeal.Comand_IP
            if (conIP.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CommandDeal.Comand_IP.Set(CMD_ID,
                    conIP.IP,
                    conIP.Port,
                    conIP.SetFlag);
            }
            this.Activate();
        }
        #endregion

        #region 装置ID
        private void 查询装置IDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            CommandDeal.Comand_ID.Query(CMD_ID);
        }

        private void 设定装置IDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Con_ID conID = new Forms.Dialog_Con_ID();
            conID.Original_ID = CommandDeal.Comand_ID.Original_ID;
            conID.NEW_CMD_ID = CMD_ID;
            conID.Component_ID = CommandDeal.Comand_ID.Component_ID;
            if (conID.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CommandDeal.Comand_ID.Set(CMD_ID,
                    conID.SetFlag,
                    conID.Component_ID,
                    conID.Original_ID,
                    conID.NEW_CMD_ID);
            }
            this.Activate();
        }
        #endregion

        #region 复位
        private void 常规复位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            CommandDeal.Comand_Reset.Reset(CMD_ID, 0x00);
        }

        private void 复位至调试模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            CommandDeal.Comand_Reset.Reset(CMD_ID, 0x01);
        }
        #endregion

        #region 模型参数
        private void 查询模型参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            CommandDeal.Comand_Model.Query(CMD_ID);
        }

        private void 设置模型参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Con_Model model = new Forms.Dialog_Con_Model();
            if (model.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CommandDeal.Comand_Model.Set(CMD_ID, model.modelData);
            }
            this.Activate();
        }
        #endregion

        #endregion

        #region 图像

        #region 图像采集参数
        private void 查询图像采集参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            CommandDeal.Image_Model.Query(CMD_ID);
        }
        private void 设定图像采集参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Image_Model dip = new Forms.Dialog_Image_Model();
            if (dip.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CommandDeal.Image_Model.Set(CMD_ID,
                    dip.RequestFlag,
                    dip.Color_Select,
                    dip.Resolution,
                    dip.Luminance,
                    dip.Contrast,
                    dip.Saturation);
            }
            this.Activate();
        }
        #endregion

        #region 手动请求照片
        /// <summary>
        /// 手动请求照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 手动请求照片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Image_Photo dip = new Forms.Dialog_Image_Photo();
            if (dip.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CommandDeal.Image_Photo_MAN.Set(CMD_ID, dip.Channel_NO, dip.Presetting_No);
            }
            //CommandDeal.Image_Photo_MAN.Set(CMD_ID, 1, 1);
        }
        #endregion

        #region 拍照时间表
        private void 设定拍照时间表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Image_TimeTable dit = new Forms.Dialog_Image_TimeTable();
            dit.TimeTable = CommandDeal.Image_TimeTable.TimeTable;
            if (dit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                if (dit.Qurey_State)//查询
                    CommandDeal.Image_TimeTable.Query(CMD_ID, dit.Channel_No);
                else//设定
                    CommandDeal.Image_TimeTable.Set(
                        CMD_ID,
                        dit.Channel_No,
                        dit.TimeTable);
            }
        }
        #endregion

        #region 摄像机远程调节
        private void 摄像机远程调节ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_Image_Adjust dia = new Forms.Dialog_Image_Adjust();
            dia.CMD_ID = CMD_ID;
            dia.Show();
        }
        #endregion


        #endregion

        #region 远程升级
        private void 远程升级装置程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //进行远程升级
            RemoteUpdate();
        }
        #endregion

        #region 曲线部分
        /// <summary>
        /// 显示微风振动波形窗体显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 微风振动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Dialog_Form df = new Forms.Dialog_Form();
            df.Show();
        }

        private void 舞动曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Dialog_FormWD df = new Forms.Dialog_FormWD();
            df.Show();
        }
        #endregion

        #region 获得当前设备ID选中状态
         /// <summary>
        /// 判断当前设备选中状态
        /// </summary>
        /// <returns></returns>
        private bool GetCMD_SelecState()
        {
            if (CMD_ID == null)
            { MessageBox.Show("您没有选择任何装置"); return false; }
            if (CMD_ID.Length == 0)
            {
                MessageBox.Show("您没有选择任何装置"); return false;
            }
            return true;
        }
        #endregion
        
        #region 串口
        private void 打开串口调试工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Config.SettingsForm.Default.ComMode == "SerialPort")
            {
                //this.tabControl1.TabPages[0]
                this.panel_TabControl.Visible = true;
                this.panel_SerialPort.Visible = false;
                Config.SettingsForm.Default.ComMode = "Socket";
                this.工作模式ToolStripMenuItem.Text = "串口工作模式";
                Form_SerialPort.CloseSerialPort();
                return;
            }
            if (Config.SettingsForm.Default.ComMode == "Socket")
            {
                this.panel_SerialPort.Visible = true;
                
                this.panel_TabControl.Visible = false;
                Config.SettingsForm.Default.ComMode = "SerialPort";
                this.工作模式ToolStripMenuItem.Text = "网络工作模式";
                return;
            }

            
        }
        #endregion

        #region 程序管理
               
        #endregion

        #endregion  

        #region 窗体事件
           /// <summary>
        /// 窗体关闭事件
        ///     1，保存修改过的配置文件
        ///    
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Config.SettingsForm.Default.Save();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //this.Hide();
            }
            if(this.WindowState == FormWindowState.Maximized)
            {
                Config.SettingsForm.Default.WindowState = "Maximized";

            }
            if(this.WindowState == FormWindowState.Normal)
            {
                Config.SettingsForm.Default.WindowState = "Normal";
            }
            
        }
        /// <summary>
        /// 从最小化返回到正常模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = true;
                if (Config.SettingsForm.Default.WindowState == "Normal")
                    this.WindowState = FormWindowState.Normal;
                if (Config.SettingsForm.Default.WindowState == "Maximized")
                {
                    this.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //this.WindowState = FormWindowState.Minimized;

            if (MessageBox.Show("是否要关闭", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true; //这里表示取消退出   
            }
            //else
            //{
            //    MessageBox.Show("退出程序");
            //}
        }
        #endregion

        #region 右击事件
            private void 显示主界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Visible = true;
            //this.WindowState =Config .SettingsForm.Default.WindowState.;
            if (Config.SettingsForm.Default.WindowState == "Normal")
                this.WindowState = FormWindowState.Normal;
            if (Config.SettingsForm.Default.WindowState == "Maximized")
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void 播放录音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog.Dialog_sound_light_alarm dialog = new Forms.Dialog.Dialog_sound_light_alarm();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                CommandDeal.Command_sound_light_alarm.Option1(CMD_ID, dialog.Play, dialog.FileNO, dialog.Interval);
            }
        }


        /// <summary>
        /// 保存当前显示Tab标签页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Flag_TabChange)
                Config.SettingsForm.Default.TabIndex = this.tabControl1.SelectedIndex;

        }

        public string GetTowerNameByID(string ID)
        {
            if (TabID == null) return ID;
            return TabID.GetTowerNameByID(ID);
        }

        private void 数据库测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Dialog.DialogTest dbTest = new Forms.Dialog.DialogTest();
            dbTest.ShowDialog();
        }

        private void 记录列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #region 私有控制指令

        private void 用户手机号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.用户手机号ToolStripMenuItem.DropDownItems.Clear();

            ToolStripMenuItem tsmi = new ToolStripMenuItem("查询");
            tsmi.Tag = 0;
            tsmi.Click += new EventHandler(UserPhoneControl);
            tsmi.MouseEnter += new EventHandler(UserPhoneControl);
            this.用户手机号ToolStripMenuItem.DropDownItems.Add(tsmi);

            tsmi = new ToolStripMenuItem("设定");
            tsmi.Tag = 1;
            tsmi.Click += new EventHandler(UserPhoneControl);

            this.用户手机号ToolStripMenuItem.DropDownItems.Add(tsmi);
        }
        /// <summary>
        /// 逆变器使能配置菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserPhoneControl(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if ((int)tsmi.Tag == 1)
            {
                if (!GetCMD_SelecState())
                    return;
                Forms.Dialog.Form_UserCon fuc = new Forms.Dialog.Form_UserCon();
                fuc.UserNO = CommandDeal.Private.UserPhone.UsNO;
                fuc.UserPhone = CommandDeal.Private.UserPhone.PhoneNO;
                if (fuc.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
                CommandDeal.Private.UserPhone.Set(CMD_ID, fuc.UserNO, fuc.UserPhone);
            }
            else
            {
                tsmi.DropDownItems.Clear();
                for (int i = 0; i < 3; i++)
                {
                    ToolStripMenuItem subTsmi = new ToolStripMenuItem("用户" + i.ToString());
                    subTsmi.Tag = i;
                    subTsmi.Click += new EventHandler(UserPhoneQuary_Click);
                    subTsmi.DoubleClick += new EventHandler(UserPhoneQuary_Click);
                    tsmi.DropDownItems.Add(subTsmi);
                }
            }

        }

        void UserPhoneQuary_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            CommandDeal.Private.UserPhone.Query(CMD_ID, (int)tsmi.Tag);
        }
        #endregion

        #region 图片清理
        private void 清理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "确定要清理全部图片信息？", "提示！！！！",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Work.PictureClean.Remove(DateTime.Now);
            }

        }
        #endregion

        private void 参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Dialog.Dialog_PictureClean_cfg dialog = new Forms.Dialog.Dialog_PictureClean_cfg();
            dialog.ShowDialog();
            if (dialog.ShowDialog() == DialogResult.Yes)
            {

            }
        }

        private void 立即清理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show(this, "确定要清理图片信息？", "提示",
            //    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            //{
            //    int days = Config.SettingsForm.Default.PictuerCleanReserveTime;
            //    if (days <= 0) days = 1;
            //    DateTime end = DateTime.Today.AddDays(-days);
            //    Work.PictureClean.Remove(end);
            //}
            throw new Exception("test ui exception");
        }

        private void 录音文件升级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog.Dialog_Update_voice du = new Forms.Dialog.Dialog_Update_voice();
            if (du.ShowDialog() != DialogResult.OK) return;
            string fileName = du.FileName;
            try
            {
                CommandDeal.Comand_voice_update.StartUpdate(du.FileName, CMD_ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void 录音删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCMD_SelecState())
                return;
            Forms.Dialog_voice_delete du = new Forms.Dialog_voice_delete();
            if (du.ShowDialog() != DialogResult.OK) return;
            try
            {
                CommandDeal.Comand_voice_update.Remove(CMD_ID, du.VoiceType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
