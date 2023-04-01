using GridBackGround.CommandDeal.nw;
using GridBackGround.Termination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridBackGround.Forms.Dialogs_nw
{
    public class MenuItem_nw
    {
        #region Base
        public ToolStripMenuItem ParentMenu { get; set; }

        public MainForm Parent { get; set; }

        public MenuItem_nw() { }

        public MenuItem_nw(MainForm p_form, ToolStripMenuItem p_menu)
        { 
            ParentMenu = p_menu;
            Parent = p_form;
        }

        public MenuItem_nw(ToolStripMenuItem p_menu) 
        {  
            ParentMenu = p_menu; 
        }

        public ToolStripMenuItem AddDropDownMenuItem(string text)
        {
            return AddDropDownMenuItem(this.ParentMenu, text);
        }

        public ToolStripMenuItem AddDropDownMenuItem(string text,
                                                     EventHandler handler)
        {
            ToolStripMenuItem item = AddDropDownMenuItem(text);
            item.Click += handler;
            return item;
        }


        public ToolStripMenuItem AddDropDownMenuItem(ToolStripMenuItem parent, 
                                                     string text)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text);
            parent.DropDownItems.Add(item);
           
            return item;
        }

        public ToolStripMenuItem AddDropDownMenuItem(ToolStripMenuItem parent, 
                                                     string text,
                                                     EventHandler handler)
        {
            ToolStripMenuItem item = AddDropDownMenuItem(parent, text);
            item.Click += handler;
            return item;
        }
        #endregion

        #region 图像菜单相关操作
        /// <summary>
        /// 摄像机远程调节 菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_88_remote_camera_control_Click(object sender, EventArgs e)
        {
            IPowerPole pole;
            if ((pole = Parent.GetSeletedPole()) == null)
                return;
            nw_cmd_88_remote_camera_control cmd = new nw_cmd_88_remote_camera_control(pole);
            cmd.Password = "1234";
            cmd.Action = new Camera_Action()
            {
                Channel_no = 1,
                actrion = Camera_Action.Actrion.Power_OFF,
                Para = 0x34,
            };
            cmd.Execute();
        }


        private void Menuitem_image_flush()
        {
            //摄像机远程调节
            this.AddDropDownMenuItem("摄像机远程调节", this.menu_88_remote_camera_control_Click);

        }
        #endregion


        public void Menuitem_Flush()
        {
            Menuitem_image_flush();


        }

    }
}
