using GridBackGround.CommandDeal;
using System;
using System.Windows.Forms;
using cma.service.gw_cmd;
using ResModel.gw;
using System.Net;

namespace GridBackGround.Forms.Dialog
{
    public class MenuItem_gw : menu_item_control
    {
        public MenuItem_gw(MainForm p_form)
            : base(p_form)
        {
        }

        private void 装置时间_query_click(object sender, EventArgs e)
        {
            gw_cmd_ctrl_time cmd = new gw_cmd_ctrl_time(this.pole);
            cmd.Query();
        }

        private void 装置时间_update_click(object sender, EventArgs e)
        {
            gw_cmd_ctrl_time cmd = new gw_cmd_ctrl_time(this.pole);
            cmd.Update();
        }

        private void 查询网络适配器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gw_cmd_ctrl_adapter cmd = new gw_cmd_ctrl_adapter(this.pole);
            cmd.Query();
        }
        static gw_ctrl_adapter adapter = new gw_ctrl_adapter()
        {
            Flag = 0xff,
            GateWay = IPAddress.Parse("192.168.1.1"),
            Mask = IPAddress.Parse("255.255.255.0"),
            IP = IPAddress.Parse("192.168.1.10"),
            DNS = IPAddress.Parse("114.114.114.114"),
        };

        private void 设置网络适配器ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Dialog_Con_NA na = new Dialog_Con_NA() { Adapter = adapter };
            if (na.ShowDialog() != DialogResult.OK)
                return;

            gw_cmd_ctrl_adapter cmd = new gw_cmd_ctrl_adapter(this.pole);
            cmd.Update(na.Adapter);
            adapter = na.Adapter;
        }
        private void 历史数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Con_HisData dch = new Dialog_Con_HisData()
            {
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now,
            };
            if (dch.ShowDialog() != DialogResult.OK)
                return;
            //当前数据
            gw_ctrl_history history = new gw_ctrl_history()
            {
                Type = (gw_func_code)dch.Data_Type,
                Start = dch.StartTime,
                End = dch.EndTime,
            };
            if (dch.CurrentData)
            {
                history.Start = DateTime.MinValue;
                history.End = DateTime.MinValue;
            }
            gw_cmd_ctrl_history cmd = new gw_cmd_ctrl_history(this.pole);
            cmd.Query(history);
        }

        private void 采样参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Con_MianTime dialog = new Dialog_Con_MianTime();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            gw_cmd_ctrl_period cmd = new gw_cmd_ctrl_period(this.pole);
            if (dialog.Query)
                cmd.Query(dialog.Data_Type);
            else
                cmd.Update(dialog.Period);
        }

        private void 查询上位机信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gw_cmd_ctrl_center cmd = new gw_cmd_ctrl_center(this.pole);
            cmd.Query();
        }

        static gw_ctrl_center gw_ctrl_center = null;

        private void 设定上位机信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Con_IP dialog = new Dialog_Con_IP();
            if(gw_ctrl_center != null)
            {
               dialog.Center = gw_ctrl_center;
            }
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            gw_cmd_ctrl_center cmd = new gw_cmd_ctrl_center(this.pole);
            cmd.Update(dialog.Center);
        }

        private void 查询装置IDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gw_cmd_ctrl_id cmd = new gw_cmd_ctrl_id(this.pole);
            cmd.Query();
        }

        gw_ctrl_id ctrl_id = null;

        private void 设定装置IDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Con_ID dialog = new Dialog_Con_ID();
         
            if(ctrl_id != null)
            {
                dialog.Original_ID = ctrl_id.OriginalID;
                dialog.NEW_CMD_ID = ctrl_id.NEW_CMD_ID;
                dialog.Component_ID = ctrl_id.ComponentID;
            }
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            gw_ctrl_id id = new gw_ctrl_id()
            {
                Flag = dialog.SetFlag,
                ComponentID = dialog.Component_ID,
                OriginalID = dialog.Original_ID,
                NEW_CMD_ID = dialog.NEW_CMD_ID,
            };
            gw_cmd_ctrl_id cmd = new gw_cmd_ctrl_id(this.pole);
            cmd.Update(id);
            ctrl_id = id;
        }
        private void 常规复位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gw_cmd_ctrl_reset cmd = new gw_cmd_ctrl_reset(this.pole);
            cmd.Reset(gw_ctrl_reset.ResetMode.Normal);
        }

        private void 调试模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gw_cmd_ctrl_reset cmd = new gw_cmd_ctrl_reset(this.pole);
            cmd.Reset(gw_ctrl_reset.ResetMode.Debug);
        }

        private void 升级模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gw_cmd_ctrl_reset cmd = new gw_cmd_ctrl_reset(this.pole);
            cmd.Reset(gw_ctrl_reset.ResetMode.Update);
        }

        private void 诊断模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gw_cmd_ctrl_reset cmd = new gw_cmd_ctrl_reset(this.pole);
            cmd.Reset(gw_ctrl_reset.ResetMode.Diagnos);
        }

        private void 查询模型参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Comand_Model.Query(this.pole.CMD_ID);
        }

        private void 设置模型参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Con_Model model = new Dialog_Con_Model();
            if (model.ShowDialog() != DialogResult.OK)
                return;
            Comand_Model.Set(this.pole.CMD_ID, model.modelData);
        }

        private void 查询图像采集参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image_Model.Query(this.pole.CMD_ID);
        }
        private void 设定图像采集参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Image_Model dip = new Dialog_Image_Model();
            if (dip.ShowDialog() != DialogResult.OK)
                return; 
            Image_Model.Set(this.pole.CMD_ID,
                dip.RequestFlag, dip.Color_Select,
                dip.Resolution, dip.Luminance,
                dip.Contrast, dip.Saturation);
        }

        private void 设定拍照时间表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Image_TimeTable dit = new Dialog_Image_TimeTable();
            dit.TimeTable = Image_TimeTable.TimeTable;
            if (dit.ShowDialog() != DialogResult.OK)
                return;
            if (dit.Qurey_State)//查询
                Image_TimeTable.Query(this.pole.CMD_ID, dit.Channel_No);
            else//设定
                Image_TimeTable.Set(this.pole.CMD_ID,
                    dit.Channel_No, dit.TimeTable);
        }

        private void 手动请求照片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Image_Photo dip = new Dialog_Image_Photo();
            if (dip.ShowDialog() != DialogResult.OK)
                return;
            Image_Photo_MAN.Set(this.pole.CMD_ID, 
                dip.Channel_NO, dip.Presetting_No);
        }

        private void 摄像机远程调节ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Image_Adjust dia = new Dialog_Image_Adjust()
            {
                CMD_ID = this.pole.CMD_ID
            };
            dia.Show();
        }

        private void 远程升级装置程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //进行远程升级
            Dialog_Update du = new Dialog_Update();
            if (du.ShowDialog() != DialogResult.OK) 
                return;
            Comand_RemotedUpDate.RunRemotedUpDate(
                du.Factory_Name, du.Model,
                du.Hard_Version, du.Soft_Version,
                du.UpdateTime, du.FileName,
                this.pole.CMD_ID);
        }
        private void 工作模式切换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Reset mode = new Form_Reset();
            if (mode.ShowDialog() != DialogResult.OK)
                return;
            Comand_ModeChange.Set(this.pole.CMD_ID, mode.Mode);
        }
        private void 播放录音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_sound_light_alarm dialog = new Dialog_sound_light_alarm();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            Command_sound_light_alarm.Option1(this.pole.CMD_ID, 
                dialog.Play, dialog.FileNO, dialog.Interval);
        }

        private void 录音文件升级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Update_voice du = new Dialog_Update_voice();
            if (du.ShowDialog() != DialogResult.OK) 
                return;
            Comand_voice_update.StartUpdate(du.FileName, this.pole.CMD_ID);
        }

        private void 录音删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_voice_delete du = new Dialog_voice_delete();
            if (du.ShowDialog() != DialogResult.OK)
                return;
            Comand_voice_update.Remove(this.pole.CMD_ID, du.VoiceType);
        }

        private static gw_ctrl_revival revival = null;
        /// <summary>
        /// 装置唤醒按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuitem_revival_click(object sender, EventArgs e)
        {
            Dialog_revival dialog = new Dialog_revival();
            if(revival != null)
            {
                dialog.Revival = revival;
            }
            if(dialog.ShowDialog() != DialogResult.OK) return;
            gw_cmd_ctrl_revival cmd = new gw_cmd_ctrl_revival(this.pole);
            cmd.Update(dialog.Revival);
            revival = cmd.Revival;
        }

        private void menuitem_baseinfo_base_click(object sender, EventArgs e)
        {
            gw_cmd_ctrl_baseinfo cmd = new gw_cmd_ctrl_baseinfo(this.pole);
            cmd.Query(new gw_ctrl_baseinfo() { 
                Type = gw_ctrl_baseinfo.InfoType.BaseInfo,
                Para_Type = gw_para_type.Weather,
            });
        }

        private void menuitem_baseinfo_status_click(object sender, EventArgs e)
        {
            gw_cmd_ctrl_baseinfo cmd = new gw_cmd_ctrl_baseinfo(this.pole);
            cmd.Query(new gw_ctrl_baseinfo()
            {
                Type = gw_ctrl_baseinfo.InfoType.StatusInfo,
                Para_Type = gw_para_type.Weather,
            });
        }

        private void menuitem_alarm_click(object sender, EventArgs e)
        {
            Dialog_Con_alarm dialog = new Dialog_Con_alarm();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            gw_cmd_ctrl_alarm cmd = new gw_cmd_ctrl_alarm(this.pole);
            if (dialog.Query)
                cmd.Query(dialog.Alarm);
            else
                cmd.Update(dialog.Alarm);
        }

        public void Menuitem_Flush()
        {
            ToolStripMenuItem menu_time;
            menu_time = this.AddDropDownMenuItem("装置时间");
            this.AddDropDownMenuItem(menu_time, "查询", this.装置时间_query_click);
            this.AddDropDownMenuItem(menu_time, "设置", this.装置时间_update_click);

            menu_time = this.AddDropDownMenuItem("网络适配器");
            this.AddDropDownMenuItem(menu_time, "查询", this.查询网络适配器ToolStripMenuItem_Click);
            this.AddDropDownMenuItem(menu_time, "设置", this.设置网络适配器ToolStripMenuItem_Click);

            menu_time = this.AddDropDownMenuItem("请求历史数据", 历史数据ToolStripMenuItem_Click);

            menu_time = this.AddDropDownMenuItem("采样参数", 采样参数ToolStripMenuItem_Click);

            menu_time = this.AddDropDownMenuItem("上位机信息");
            this.AddDropDownMenuItem(menu_time, "查询", this.查询上位机信息ToolStripMenuItem_Click);
            this.AddDropDownMenuItem(menu_time, "设置", this.设定上位机信息ToolStripMenuItem_Click);

            menu_time = this.AddDropDownMenuItem("装置ID");
            this.AddDropDownMenuItem(menu_time, "查询", this.查询装置IDToolStripMenuItem_Click);
            this.AddDropDownMenuItem(menu_time, "设置", this.设定装置IDToolStripMenuItem_Click);

            menu_time = this.AddDropDownMenuItem("装置复位");
            this.AddDropDownMenuItem(menu_time, "常规复位", this.常规复位ToolStripMenuItem_Click);
            this.AddDropDownMenuItem(menu_time, "调试模式", this.调试模式ToolStripMenuItem_Click);
            this.AddDropDownMenuItem(menu_time, "升级模式", this.升级模式ToolStripMenuItem_Click);
            this.AddDropDownMenuItem(menu_time, "诊断模式", this.诊断模式ToolStripMenuItem_Click);

            this.AddDropDownMenuItem("装置唤醒时间", this.menuitem_revival_click);

            menu_time = this.AddDropDownMenuItem("信息上报");
            this.AddDropDownMenuItem(menu_time, "基本信息", this.menuitem_baseinfo_base_click);
            this.AddDropDownMenuItem(menu_time, "状态信息", this.menuitem_baseinfo_status_click);

            menu_time = this.AddDropDownMenuItem("模型参数");
            this.AddDropDownMenuItem(menu_time, "查询", this.查询模型参数ToolStripMenuItem_Click);
            this.AddDropDownMenuItem(menu_time, "设置", this.设置模型参数ToolStripMenuItem_Click);

            menu_time = this.AddDropDownMenuItem("报警阈值", this.menuitem_alarm_click);

            this.ParentMenu.DropDownItems.Add(new ToolStripSeparator());

            menu_time = this.AddDropDownMenuItem("图像采集参数");
            this.AddDropDownMenuItem(menu_time, "查询", this.查询图像采集参数ToolStripMenuItem_Click);
            this.AddDropDownMenuItem(menu_time, "设置", this.设定图像采集参数ToolStripMenuItem_Click);

            menu_time = this.AddDropDownMenuItem("拍照时间表", 设定拍照时间表ToolStripMenuItem_Click);
            menu_time = this.AddDropDownMenuItem("手动请求照片", 手动请求照片ToolStripMenuItem_Click);
            menu_time = this.AddDropDownMenuItem("摄像机远程调节", 摄像机远程调节ToolStripMenuItem_Click);

            this.ParentMenu.DropDownItems.Add(new ToolStripSeparator());

            menu_time = this.AddDropDownMenuItem("远程升级装置程序", 远程升级装置程序ToolStripMenuItem_Click);
            menu_time = this.AddDropDownMenuItem("正式运行升级程序");
            menu_time.Visible = false;
            menu_time = this.AddDropDownMenuItem("切换装置运行程序", 工作模式切换ToolStripMenuItem_Click);

            menu_time = this.AddDropDownMenuItem("录音文件管理");
            this.AddDropDownMenuItem(menu_time, "播放录音", 播放录音ToolStripMenuItem_Click);
            this.AddDropDownMenuItem(menu_time, "录音文件升级", 录音文件升级ToolStripMenuItem_Click);
            this.AddDropDownMenuItem(menu_time, "录音删除");
        }
    }
}
