using System;
using System.Windows.Forms;
using GridBackGround.Forms.Dialog;
using ResModel.gw;

namespace GridBackGround.Forms
{
    public partial class Dialog_Con_Model : Form
    {

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
            this.button_Cancel.DialogResult = DialogResult.Cancel;
        }

       /// <summary>
       /// 界面初始化
       /// </summary>
        public void InitWindow()
        {
            for(int i=0;i<20;i++)
            {
                UC_con_model uc = new UC_con_model()
                {
                    MKey = string.Format("PARA{0:D2}", i + 1),
                    MName = string.Format("参数{0:D2}", i + 1),
                    MType = gw_ctrl_model.EType.F32,
                    MChecked = false,
                };
                this.flowLayoutPanel1.Controls.Add(uc);
            }
        }

        private bool  GetModelFromUC(UC_con_model uc,out gw_ctrl_model model)
        {
            if(uc == null)
                throw new ArgumentNullException(uc.ToString());
            model = null;
            if (uc.MChecked == false)
                return true;
            Single value = 0;
            try
            {
                switch (uc.MType)
                {
                    case gw_ctrl_model.EType.S32:
                        value = Int32.Parse(uc.Value);
                        break;
                    case gw_ctrl_model.EType.U32:
                        value = UInt32.Parse(uc.Value);
                        break;
                    case gw_ctrl_model.EType.F32:
                        value = Single.Parse(uc.Value);
                        break;
                }
            }
            catch
            {
                MessageBox.Show(string.Format("配置参数:{0} 格式化失败，请输入正确的参数", uc.MName));
                return false;
            }

            model = new gw_ctrl_model()
            {
                Name = uc.MName,
                Key = uc.MKey,
                Type = uc.MType,
                Value = value,
            };
            return true;
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            if (this.Models == null)
                this.Models = new gw_ctrl_models();
            this.Models.Models.Clear();

            foreach(UC_con_model uc in this.flowLayoutPanel1.Controls)
            {
                gw_ctrl_model model;
                if (GetModelFromUC(uc, out model) == false)
                    return;
                if (model == null)
                    continue;
                this.Models.Models.Add(model);
            }

            if(this.Models.Models.Count == 0)
            {
                MessageBox.Show("没有选择任何参数进行设置");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
