using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace GridBackGround.Forms.EquMan
{
    public class FormManEquMan
    {
        public Forms.Tab.Tab_IDs TabID { get; set; }
 
        public FormManEquMan()
        { 
            
        }

        public void FormManEquManInit(ToolStripMenuItem menuItem)
        {
            ToolStripMenuItem item;
            ToolStripSeparator toolStripS = new ToolStripSeparator();
            menuItem.DropDownItems.Add(toolStripS);

            if (Config.SettingsForm.Default.ServiceMode != "nw")
            {
                item = new ToolStripMenuItem("URL接口管理");
                item.Click += new EventHandler(UrlMan_Click);
                menuItem.DropDownItems.Add(item);
            }

            item = new ToolStripMenuItem("单位管理");
            item.Click += new EventHandler(Department_Click);
            menuItem.DropDownItems.Add(item);

            item = new ToolStripMenuItem("线路管理");
            item.Click += new EventHandler(TowerMan_Click);
            menuItem.DropDownItems.Add(item);

             item = new ToolStripMenuItem("设备管理");
            item.Click += new EventHandler(EquMan_Click);
            menuItem.DropDownItems.Add(item);
        }

        void Department_Click(object sender, EventArgs e)
        {
            Dialog_Line_Man form = new Dialog_Line_Man();
            form.ShowDialog();
            if (TabID != null)
                TabID.TreeViewUpdate();
        }
        /// <summary>
        /// 线路管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TowerMan_Click(object sender, EventArgs e)
        {
            Dialog_Tower_Man form = new Dialog_Tower_Man();
            form.ShowDialog();
            if (TabID != null)
                TabID.TreeViewUpdate();
        }

        void EquMan_Click(object sender, EventArgs e)
        {
            Dialog_EQU_Man dEquMan = new Dialog_EQU_Man();
            dEquMan.ShowDialog();
            if (TabID != null)
                TabID.TreeViewUpdate();
        }

        void UrlMan_Click(object sender, EventArgs e)
        {
            Dialog_UrlInterface_Man dum = new Dialog_UrlInterface_Man();
            dum.ShowDialog();
        }


    }
}
