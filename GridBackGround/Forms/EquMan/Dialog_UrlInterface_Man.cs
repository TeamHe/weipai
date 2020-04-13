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

namespace GridBackGround.Forms.EquMan
{
    public partial class Dialog_UrlInterface_Man : Form
    {
        #region Private Variable
        private UrlInterFace urlInterFace = null;

        private TreeNode SelectedNode;
        #endregion
        #region Construction
        /// <summary>
        /// Construction
        /// </summary>
        public Dialog_UrlInterface_Man()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.Load += new EventHandler(Dialog_UrlInterface_Man_Load);
            this.button_Update.Enabled = false;
            this.button_Delete.Enabled = false;
            this.treeView1.ShowNodeToolTips = true;
            
        }
        #endregion
       
        #region ControlInit
        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Dialog_UrlInterface_Man_Load(object sender, EventArgs e)
        {
            TreeViewInit();
            this.button_Add.Click += new EventHandler(button_Add_Click);
            this.button_Update.Click += new EventHandler(button_Update_Click);
            this.button_Delete.Click += new EventHandler(button_Delete_Click);
        }

        /// <summary>
        /// 接口列表初始化
        /// </summary>
        void TreeViewInit()
        {
            //node 选择事件
            
            this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            UpdateUrlList();
            //this.treeView1.HideSelection = false;

        }
        #endregion

        #region 接口管理按钮
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void button_Add_Click(object sender, EventArgs e)
        {
            if (this.textBox1.TextLength == 0)
            {
                MessageBox.Show("请输入接口名称！");
                return;
            }
            if (this.textBox2.TextLength == 0)
            {
                MessageBox.Show("请输入接口URL!");
                return;
            }
            try
            {
                DB_Url URL = new DB_Url();
                var url = URL.Add(this.textBox1.Text, this.textBox2.Text);
                var node = TreeviewAddUrl(url);
                this.treeView1.SelectedNode = node;
                MessageBox.Show("URL接口"+this.urlInterFace.Nanme+"添加成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("URL 添加失败，失败原因:" + ex.Message);
            }
        }

        /// <summary>
        /// 更新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void button_Update_Click(object sender, EventArgs e)
        {
            if (this.textBox1.TextLength == 0)
            {
                MessageBox.Show("请输入接口名称！");
                return;
            }
            if (this.textBox2.TextLength == 0)
            {
                MessageBox.Show("请输入接口URL!");
                return;
            }
            try
            {
                DB_Url URL = new DB_Url();
                URL.Update(this.urlInterFace.ID, this.textBox1.Text, this.textBox2.Text);
                urlInterFace.Nanme = this.textBox1.Text;
                urlInterFace.Url = this.textBox2.Text;
                this.treeView1.SelectedNode.Tag = urlInterFace;
                this.treeView1.SelectedNode.Text = urlInterFace.Nanme;
                this.treeView1.SelectedNode.ToolTipText = urlInterFace.Url;
                MessageBox.Show("URL更新成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("URL 更新失败，失败原因:" + ex.Message);
            }
        }
        
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void button_Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this,
                                "您确定要删除URL接口:" + this.urlInterFace.Nanme + "?",
                                "",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2
                                )
                == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    DB_Url URL = new DB_Url();
                    URL.Delete(this.urlInterFace);
                    MessageBox.Show("接口" + urlInterFace.Nanme + "删除成功");
                    this.treeView1.Nodes.Remove(this.treeView1.SelectedNode);
                    if (this.treeView1.Nodes.Count == 0)
                    {
                        this.urlInterFace = null;
                        this.button_Delete.Enabled = false;
                        this.button_Update.Enabled = false;

                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("URL接口删除失败，失败原因:" + ex.Message);
                }
            }
        }
        #endregion
        
        
        /// <summary>
        /// 列表中控件选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (SelectedNode != null)
            {
                SelectedNode.ForeColor = Color.Black;
                SelectedNode.BackColor = Color.White;
            }

            SelectedNode = this.treeView1.SelectedNode;
            SelectedNode.BackColor = Color.DeepSkyBlue;
            SelectedNode.ForeColor = Color.White;

            this.button_Update.Enabled = true;
            this.button_Delete.Enabled = true;
            TreeNode tn = this.treeView1.SelectedNode;
            this.urlInterFace = (UrlInterFace)tn.Tag;
            this.textBox1.Text = urlInterFace.Nanme;
            this.textBox2.Text = urlInterFace.Url;
        }

        #region Private Functions
        /// <summary>
        /// 更新列表
        /// </summary>
        private void UpdateUrlList()
        {
            this.treeView1.Nodes.Clear();
            try
            {
                DB_Url db_url = new DB_Url();
                var urlList = db_url.GetUrlList();
                foreach (UrlInterFace url in urlList)
                {
                    var tn = TreeviewAddUrl(url);
                    if (urlInterFace != null)
                        if (url.ID == urlInterFace.ID)
                        {
                            this.treeView1.SelectedNode = tn;
                        }
                }
                if (this.urlInterFace == null)
                    if (this.treeView1.Nodes.Count > 0)
                        this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取接口列表失败，失败原因:" + ex.Message);
            }

        }
        /// <summary>
        /// 向列表控件增加节点
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        TreeNode TreeviewAddUrl(UrlInterFace url)
        {
            TreeNode tn = new TreeNode();
            tn.Tag = url;
            tn.Text = url.Nanme;
            tn.ToolTipText = url.Url;
            this.treeView1.Nodes.Add(tn);
            this.button_Delete.Enabled = false;
            return tn;
        }
        #endregion


        
    }
}
