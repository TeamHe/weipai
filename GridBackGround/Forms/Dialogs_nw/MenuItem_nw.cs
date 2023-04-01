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


        #region 控制菜单相关操作
        /// <summary>
        /// 7.2 	校时（控制字：01H）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_cur_time_set(object sender, EventArgs e)
        {
            IPowerPole pole = Parent.GetSeletedPole();
            if (pole != null)
            {
                nw_cmd_01_timing timing = new nw_cmd_01_timing(pole);
                timing.Execute();
            }
        }
        /// <summary>
        /// 7.13 	查询装置时间（控制字：0DH）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_cur_time_get(object sender, EventArgs e)
        {
            IPowerPole pole = Parent.GetSeletedPole();
            if (pole != null)
            {
                nw_cmd_0d_time_get timing = new nw_cmd_0d_time_get(pole);
                timing.Execute();
            }
        }


        /// <summary>
        /// 7.7 	查询主站IP地址、端口号和卡号（控制字：07H）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_center_get(object sender, EventArgs e)
        {
            IPowerPole pole = Parent.GetSeletedPole();
            if (pole != null)
            {
                nw_cmd_07_center_get center = new nw_cmd_07_center_get(pole);
                center.Execute();
            }
        }

        /// <summary>
        /// 7.6 	更改主站IP地址、端口号和卡号（控制字：06H）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_center_set(object sender, EventArgs e)
        {
            IPowerPole pole;
            if ((pole = Parent.GetSeletedPole()) == null)
                return;
            Forms.Dialogs_nw.Dialog_nw_ip dialog = new Forms.Dialogs_nw.Dialog_nw_ip();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                nw_cmd_06_center_set cmd = new nw_cmd_06_center_set(pole);
                cmd.center = dialog.center;
                cmd.Execute();
            }
        }

        /// <summary>
        /// 开始上传所有未上传数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_data_get(object sender, EventArgs e)
        {
            IPowerPole pole = Parent.GetSeletedPole();
            if (pole != null)
            {
                nw_cmd_21_data cmd = new nw_cmd_21_data(pole);
                cmd.Execute();
            }
        }

        /// <summary>
        /// 立即采集所有数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_data_get_imediately(object sender, EventArgs e)
        {
            IPowerPole pole = Parent.GetSeletedPole();
            if (pole != null)
            {
                nw_cmd_21_data cmd = new nw_cmd_21_data(pole);
                cmd.ImeData = true;
                cmd.Execute();
            }
        }

        /// <summary>
        /// 7.16 	上传导地线拉力及偏角数据（控制字：22H）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_22_data_pull_get(object sender, EventArgs e)
        {
            IPowerPole pole = Parent.GetSeletedPole();
            if (pole != null)
            {
                nw_cmd_22_pull_angle cmd = new nw_cmd_22_pull_angle(pole);
                cmd.Execute();
            }
        }


        /// <summary>
        /// 7.17 	上传气象数据（控制字：25H）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_data_weather_get(object sender, EventArgs e)
        {
            IPowerPole pole = Parent.GetSeletedPole();
            if (pole != null)
            {
                nw_cmd_25_weather cmd = new nw_cmd_25_weather(pole);
                cmd.Execute();
            }
        }

        /// <summary>
        /// 7.3 	设置装置密码（控制字：02H）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_set_password(object sender, EventArgs e)
        {
            IPowerPole pole;
            if ((pole = Parent.GetSeletedPole()) == null)
                return;
            Forms.Dialogs_nw.Dialog_nw_password dialog = new Forms.Dialogs_nw.Dialog_nw_password();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                nw_cmd_02_password cmd = new nw_cmd_02_password(pole);
                cmd.Password_old = dialog.Password_old;
                cmd.Password_new = dialog.Password_new;
                cmd.Execute();
            }

        }

        /// <summary>
        /// 7.8 	装置重启（控制字：08H）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_click_reboot(object sender, EventArgs e)
        {
            IPowerPole pole;
            if ((pole = Parent.GetSeletedPole()) == null)
                return;
            Forms.Dialogs_nw.Dialog_nw_password_input dialog = new Forms.Dialogs_nw.Dialog_nw_password_input();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                nw_cmd_08_rest cmd = new nw_cmd_08_rest(pole);
                cmd.Password = dialog.Password;
                cmd.Execute();
            }
        }


        private void Menuitem_control_flush()
        {
            //装置时间查询设置
            ToolStripMenuItem menu_time = this.AddDropDownMenuItem("装置时间");
            //7.13 	查询装置时间（控制字：0DH）
            this.AddDropDownMenuItem(menu_time, "查询", this.menu_click_cur_time_get);
            //7.2 	校时（控制字：01H）
            this.AddDropDownMenuItem(menu_time, "设置", this.menu_click_cur_time_set);

            //主站信息查询设置
            ToolStripMenuItem menu_center = this.AddDropDownMenuItem("主站信息");
            //7.7 	查询主站IP地址、端口号和卡号（控制字：07H）
            this.AddDropDownMenuItem(menu_center, "查询", this.menu_click_center_get);
            //7.6 	更改主站IP地址、端口号和卡号（控制字：06H）
            this.AddDropDownMenuItem(menu_center, "设置", this.menu_click_center_set);

            //主站请求装置数据
            ToolStripMenuItem menu_data = this.AddDropDownMenuItem("请求上传数据");
            this.AddDropDownMenuItem(menu_data, "未上传装置数据", this.menu_click_data_get);
            this.AddDropDownMenuItem(menu_data, "立即采集所有数据", this.menu_click_data_get_imediately);
            this.AddDropDownMenuItem(menu_data, "导地线拉力及偏角数据", this.menu_click_22_data_pull_get);
            this.AddDropDownMenuItem(menu_data, "气象数据", this.menu_click_data_weather_get);

            //设置装置密码
            this.AddDropDownMenuItem("设置装置密码", menu_click_set_password);
            this.AddDropDownMenuItem("装置重启", menu_click_reboot);
        }
        #endregion


        public void Menuitem_Flush()
        {
            Menuitem_control_flush();
            this.ParentMenu.DropDownItems.Add(new ToolStripSeparator());
            Menuitem_image_flush();


        }

    }
}
