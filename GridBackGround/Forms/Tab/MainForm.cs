using System;
using System.Windows.Forms;
using GridBackGround.Forms.Dialogs_nw;
using ResModel;
using GridBackGround.Forms.Tab;
using cma.service;
using GridBackGround.Forms.Dialog;

namespace GridBackGround
{
    public delegate string GetEquNameHandler(string ID);
    public partial class MainForm : Form
    {
        private string CMD_ID = null;
        bool Flag_TabChange = false;

        public MenuItem_nw menu_nw = null;
        public MenuItem_gw menu_gw = null;

        private HTTP.HttpListeners httpListeners = null;

        public event EventHandler<CMDid_Change> OnSelectedEquChanged;

        public MainForm()
        {
            LogHelper.WriteLog("System Start");

            InitializeComponent();
            this.CenterToParent();
            //Forms.EquMan.FormManEquMan fmm= new Forms.EquMan.FormManEquMan();
            //fmm.FormManEquManInit(this.设置ToolStripMenuItem);
        }

        /// <summary>
        /// 菜单栏初始化
        /// </summary>
        private void menu_init()
        {
            this.私有控制ToolStripMenuItem.DropDownItems.Clear();
            this.menu_nw = new MenuItem_nw(this)
            {
                ParentMenu = this.设备控制ToolStripMenuItem,
                PrivateControlMenu = this.私有控制ToolStripMenuItem,
            };
            this.menu_nw.Menuitem_Flush();

            this.menu_gw = new MenuItem_gw(this)
            {
                ParentMenu = this.主站ToolStripMenuItem,
            };
            this.menu_gw.Menuitem_Flush();

            if (Config.SettingsForm.Default.ServiceMode == "nw")
            {
                this.工作模式ToolStripMenuItem.Visible = false;
                this.主站ToolStripMenuItem.Visible = false;
                this.设备控制ToolStripMenuItem.Visible = true;
            }
            else
            {
                this.工作模式ToolStripMenuItem.Visible = false;
                this.主站ToolStripMenuItem.Visible = true;
                this.设备控制ToolStripMenuItem.Visible = false;
            }
        }

        /// <summary>
        /// 界面加载的时候进行的必要的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString()+"系统正在初始化");
            var name = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            this.Text = name.Name + "(V" + name.Version + ")";
            this.notifyIcon1.Text = name.Name + "(V" + name.Version + ")";
            this.Icon = new System.Drawing.Icon("Res\\logo.ico");
            this.notifyIcon1.Icon = new System.Drawing.Icon("Res\\logo.ico");
            //菜单栏初始化
            menu_init();

            //数据库初始化
            try
            {
                DisPacket.Init();
                DB_Operation.DB.Init();

                TabInit();

                //控件初始化
                ControlsInit();
                //终端管理初始化
                PowerPoleManage.GetInstance().PowerPoleManageInit();
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
        private void flush_menuitem(ResModel.EQU.DevFlag flag)
        {
            this.主站ToolStripMenuItem.Visible = false;
            this.设备控制ToolStripMenuItem.Visible=false;
            if(flag == ResModel.EQU.DevFlag.NW)
                this.设备控制ToolStripMenuItem.Visible = true;
            if (flag == ResModel.EQU.DevFlag.GW)
                this.主站ToolStripMenuItem.Visible = true;
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
            flush_menuitem(e.Flag);
            try
            {
                if (OnSelectedEquChanged != null)
                    OnSelectedEquChanged(this, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected equ changed error." + ex.Message);
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
                TabPacket.MainForm = this;
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
                Tab_HisData_nw tab_his_nw = new Tab_HisData_nw();
                tab_his_nw.FormBorderStyle = FormBorderStyle.None;
                this.tabPage_nw_history.Enter += new EventHandler(tabpate_nw_his_enter);
                this.tabPage_nw_history.Controls.Add(tab_his_nw);
                this.tabPage_nw_history.Tag = tabPage_nw_history;
                this.tabControl1.SelectedTab = tabPage_nw_history;
                tab_his_nw.Show();
            }
            if (true)
            {
                TabHisData = new Tab_HisData();
                TabHisData.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                //this.tabPageData.CursorChanged += new EventHandler(tabPageData_CursorChanged);
                this.tabPageData.Enter += new EventHandler(tabPageData_Enter);
                this.tabPageData.Controls.Add(TabHisData);
                this.tabControl1.SelectedTab = tabPageData;
                TabHisData.Show();
            }
            if (true)
            {
                TabOnLineStatus = new Tab_OnlineStatus();
                this.tabPageOnLineStatus.Controls.Add(TabOnLineStatus);
                tabControl1.SelectTab(this.tabPageOnLineStatus);
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

        public Tab_IDs GetTabID() 
        {
            return this.TabID;
        }

        void tabPageData_Enter(object sender, EventArgs e)
        {
            //TabHisData.HisTimeRefresh();
            //throw new NotImplementedException();
        }

        void tabpate_nw_his_enter(object sender, EventArgs e)
        {
            //TabHisData.HisTimeRefresh();
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
                    "NW：" + Config.SettingsForm.Default.nw_port + "  GW：" +
                    Config.SettingsForm.Default.gw_port + "已成功打开";
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

            IPowerPole powerPole = PowerPoleManage.GetInstance().Find(this.CMD_ID);
            if(powerPole == null)
            {
                MessageBox.Show("设备离线");
                return null;
            }
            return powerPole;

        }

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
    }
}
