using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SQLUtils;
using Tools;

namespace GridBackGround.Forms.Dialog
{
    public partial class DialogTest : Form
    {
        public DialogTest()
        {
            InitializeComponent();
        }

        private MySqlDBTest dialogMysqlDB;

        private void DialogTest_Load(object sender, EventArgs ea)
        {
            this.StartPosition = FormStartPosition.CenterScreen; 
            InitDBType();
            this.CancelButton = this.button_Cancel;
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AcceptButton = this.button_OK;
        }
        /// <summary>
        /// 数据库连接类型初始化
        /// </summary>
        private void InitDBType()
        {
            this.comboBox_DBType.Items.Clear();
            Dictionary<Int32, String> dic = EnumUtil.EnumToDictionary(typeof(SQLConnEnum), e => e.GetDescription());
            ComboBoxItem cbxitem; //= new ComboBoxItem()

            foreach (KeyValuePair<Int32, String> item in dic)
            {
                cbxitem = new ComboBoxItem(item.Value, item.Key);//将装置类型绑定到combox中
                this.comboBox_DBType.Items.Add(cbxitem);
            }
            string constr = "";
            this.comboBox_DBType.SelectedIndex = (int)SQLUtils.SQLUtilsFactory.GetDBconStr(ref constr);
        }

        /// <summary>
        /// 数据库连接类型改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_DBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panel_DB.Controls.Clear();
            int index = ((ComboBox)sender).SelectedIndex;
           
            switch ((SQLConnEnum)index)
            { 
                case SQLConnEnum.MySql:
                    ShowMysqlDB();
                    string constr = "";
                    if (SQLUtils.SQLUtilsFactory.GetDBconStr(ref constr) == SQLConnEnum.MySql)
                        SetMysqlDBConfig(constr);
                    break;
            }
        }
        /// <summary>
        /// 显示Mysql数据库测试界面
        /// </summary>
        private void ShowMysqlDB()
        {
           
            dialogMysqlDB = new MySqlDBTest();
            dialogMysqlDB.TopLevel = false;
            dialogMysqlDB.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            dialogMysqlDB.Dock = DockStyle.Fill;
            this.panel_DB.Controls.Add(dialogMysqlDB);
            dialogMysqlDB.Show();
        }

        /// <summary>
        /// 获得mysql数据库连接字符串
        /// </summary>
        /// <returns></returns>
        private string GetMysqlConnectString()
        {
            if (this.dialogMysqlDB == null) throw new NoNullAllowedException("当前选中的装置不是mysql");
            try
            {
                return string.Format("Server={0};Port={1};Stmt=;Database={2}; User={3};Password={4};",
               dialogMysqlDB.Server, dialogMysqlDB.Port, dialogMysqlDB.DBName, dialogMysqlDB.UserName, dialogMysqlDB.PassWord);
            }
            catch (Exception ex)
            {

                MessageBox.Show("获取数据库配置信息失败，失败原因：" +ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 根据MySql连接字符串获取连接参数
        /// </summary>
        /// <param name="conStr"></param>
        private void SetMysqlDBConfig(string conStr)
        {
            var values = conStr.Split(new char[] { '=', ';' });
            if (dialogMysqlDB == null) throw new NoNullAllowedException("当前选中设备为非Mysql");
            dialogMysqlDB.Server = values[1];
            dialogMysqlDB.Port = int.Parse(values[3]);
            dialogMysqlDB.DBName = values[7];
            dialogMysqlDB.UserName = values[9];
            dialogMysqlDB.PassWord = values[11];
        }

        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTestCon_Click(object sender, EventArgs e)
        {
            SQLConnEnum dbType = (SQLConnEnum)this.comboBox_DBType.SelectedIndex;
            ISQLUtils isqlU = null;
            try
            {
                switch (dbType)
                {
                    case SQLConnEnum.MySql:
                        isqlU = SQLUtilsFactory.Create(dbType, GetMysqlConnectString());
                        break;
                }
                if (isqlU == null) throw new Exception("暂不支持的数据库类型");
                isqlU.OpenConnection();
                MessageBox.Show("数据库连接成功");
                isqlU.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接失败，失败原因：" + ex.Message);
            }
            
        }
        /// <summary>
        /// 保存数据库连接信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            SQLConnEnum dbType = (SQLConnEnum)this.comboBox_DBType.SelectedIndex;
            string str = "";
            try
            {
                switch (dbType)
                {
                    case SQLConnEnum.MySql:
                        str = GetMysqlConnectString();
                        break;
                }
                if (str.Length == 0)
                {
                    MessageBox.Show("获取连接字符串失败，配置将不再保存");
                    return;
                }
                SQLUtilsFactory.SetDBconStr(dbType,str);
                MessageBox.Show("数据库配置成功，重启之后生效");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                
            }
            catch
            {
            }
        }
    }
}
