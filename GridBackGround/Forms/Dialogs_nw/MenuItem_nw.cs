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
        /// 图像采集参数配置（控制字：81H）菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_81_img_para(object sender, EventArgs e)
        {
            IPowerPole pole;
            if ((pole = Parent.GetSeletedPole()) == null)
                return;
            Dialog_nw_image_para dialog = new Dialog_nw_image_para();
            dialog.para_ch1 = new nw_img_para() { Color = nw_img_para.EColor.Color, Resolution = nw_img_para.EResolution.R_800_600, Brightness = 20, Contrast = 30, Saturation = 10 };
            dialog.para_ch2 = new nw_img_para() { Color = nw_img_para.EColor.Color, Resolution = nw_img_para.EResolution.R_800_600, Brightness = 20, Contrast = 30, Saturation = 10 };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                nw_cmd_81_img_para cmd = new nw_cmd_81_img_para(pole) { Channel1 = dialog.para_ch1, Channel2 = dialog.para_ch2, Passowrd = "1234" };
                cmd.Execute();
            }
        }


        /// <summary>
        /// 拍照时间表设置 菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_82_time_table_set(object sender, EventArgs e)
        {
            IPowerPole pole;
            if ((pole = Parent.GetSeletedPole()) == null)
                return;
            Dialog_Image_TimeTable dialog = new Dialog_Image_TimeTable();
            dialog.nw_flag = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                nw_cmd_82_time_table_set cmd = new nw_cmd_82_time_table_set(pole);
                cmd.Passowrd = dialog.Password;
                cmd.TimeTable = dialog.TimeTable;
                cmd.Channel_No = dialog.Channel_No;
                cmd.Execute();
            }
        }

        /// <summary>
        /// 主站请求拍摄照片（控制字：83H） 菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_83_photoing(object sender, EventArgs e)
        {
            IPowerPole pole;
            if ((pole = Parent.GetSeletedPole()) == null)
                return;
            Dialog_Image_Photo dialog = new Dialog_Image_Photo();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                nw_cmd_83_photoing cmd = new nw_cmd_83_photoing(pole)
                {
                    Preset_No = dialog.Presetting_No,
                    Channel_No = dialog.Channel_NO,
                };
                cmd.Execute();
            }

        }


        /// <summary>
        /// 摄像机远程调节 菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_88_remote_camera_control(object sender, EventArgs e)
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

        /// <summary>
        /// 查询拍照时间表（控制字：8BH） 菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_8B_time_table_get(object sender, EventArgs e)
        {
            IPowerPole pole;
            if ((pole = Parent.GetSeletedPole()) == null)
                return;
            Dialog_Image_Photo dialog = new Dialog_Image_Photo();
            dialog.nw_get_table = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                nw_cmd_8B_time_table_get cmd = new nw_cmd_8B_time_table_get(pole)
                {
                    Channel_No = dialog.Channel_NO,
                };
                cmd.Execute();
            }
        }

        private void Menuitem_image_flush()
        {
            //图像采集参数配置（控制字：81H）
            this.AddDropDownMenuItem("图像采集参数配置", this.menu_click_81_img_para);

            //主站请求拍摄照片（控制字：83H）
            this.AddDropDownMenuItem("主站请求拍摄照片", this.menu_click_83_photoing);

            //拍照时间表
            ToolStripMenuItem item_time_table = this.AddDropDownMenuItem("拍照时间表");
            //摄像机远程调节
            this.AddDropDownMenuItem("摄像机远程调节", this.menu_click_88_remote_camera_control);

            //查询拍照时间表（控制字：8BH）
            this.AddDropDownMenuItem(item_time_table, "查询", this.menu_click_8B_time_table_get);
            //拍照时间表设置（控制字：82H）
            this.AddDropDownMenuItem(item_time_table, "设置", this.menu_click_82_time_table_set);
        }
        #endregion


        public void Menuitem_Flush()
        {
            Menuitem_image_flush();


        }

    }
}
