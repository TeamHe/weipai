using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialog
{
    public class MenuItem_gw : menu_item_control
    {
        public MenuItem_gw(MainForm p_form)
            : base(p_form)
        {
        }

        private void Menuitem_control_flush()
        {
            //装置时间查询设置
            ToolStripMenuItem menu_time = this.AddDropDownMenuItem("网络适配器");
            //7.13 	查询装置时间（控制字：0DH）
            this.AddDropDownMenuItem(menu_time, "查询", this.menu_click_cur_time_get);
            //7.2 	校时（控制字：01H）
            this.AddDropDownMenuItem(menu_time, "设置", this.menu_click_cur_time_set);

        }

        public void Menuitem_Flush()
        {
            Menuitem_control_flush();
            //this.ParentMenu.DropDownItems.Add(new ToolStripSeparator());
            //Menuitem_image_flush();

            //private_menu_flush();
        }

    }
}
