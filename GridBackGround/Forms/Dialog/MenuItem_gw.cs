using GridBackGround.CommandDeal;
using System;
using System.Windows.Forms;
using cma.service.gw_cmd;
using ResModel.gw;
using static ResModel.gw.gw_ctrl_adapter;
using System.Net;
using System.Threading.Tasks;

namespace GridBackGround.Forms.Dialog
{
    public class MenuItem_gw : menu_item_control
    {
        public MenuItem_gw(MainForm p_form)
            : base(p_form)
        {
        }

        private void 查询网络适配器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gw_cmd_ctrl_a1_adaptercs cmd = new gw_cmd_ctrl_a1_adaptercs()
            {
                Pole = this.pole,
            };
            cmd.Query();
        }
        static gw_ctrl_adapter adapter = new gw_ctrl_adapter()
        {
            Flag = 0xff,
            GateWay = IPAddress.Parse("192.168.1.1"),
            Mask = IPAddress.Parse("255.255.255.0"),
            IP = IPAddress.Parse("192.168.1.10"),
            PhoneNumber = "12345678901234567890"
        };

        private void 设置网络适配器ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Dialog_Con_NA na = new Dialog_Con_NA() { Adapter = adapter };
            if (na.ShowDialog() != DialogResult.OK)
                return;

            gw_cmd_ctrl_a1_adaptercs cmd = new gw_cmd_ctrl_a1_adaptercs()
            {
                Pole = this.pole,
            };
            cmd.Update(na.Adapter);
            adapter = na.Adapter;
        }
        private void 历史数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Con_HisData dch = new Dialog_Con_HisData();
            if (dch.ShowDialog() != DialogResult.OK)
                return;
            //当前数据
            if (dch.CurrentData)                            //请求当前数据
                Comand_History.Current(this.pole.CMD_ID,
                    dch.Data_Type);
            else
                //请求历史数据
                Comand_History.History(this.pole.CMD_ID,
                    dch.Data_Type, dch.StartTime, dch.EndTime);
        }

        private void 采样参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Con_MianTime dialog = new Dialog_Con_MianTime();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            gw_cmd_ctrl_a3_period cmd = new gw_cmd_ctrl_a3_period()
            {
                Pole = this.pole,
            };
            if (dialog.Query)
                cmd.Query((gw_func_code)dialog.Flag);
            else
                cmd.Update(new gw_ctrl_period()
                {
                    Flag = dialog.Flag,
                    MainType = (gw_func_code)dialog.Data_Type,
                    MainTime = dialog.Main_Time,
                    HearTime = dialog.Heart_Time
                });
        }

        private void 查询上位机信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Comand_IP.Query(this.pole.CMD_ID);
        }

        private void 设定上位机信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Con_IP conIP = new Dialog_Con_IP()
            {
                IP = Comand_IP.IP_Address,
                Port = Comand_IP.Port,
                //SetFlag = CommandDeal.Comand_IP,
            };
            if (conIP.ShowDialog() != DialogResult.OK)
                return;
            Comand_IP.Set(this.pole.CMD_ID,
                conIP.IP, conIP.Port, conIP.SetFlag);
        }

        private void 查询装置IDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Comand_ID.Query(this.pole.CMD_ID);
        }

        private void 设定装置IDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_Con_ID conID = new Dialog_Con_ID()
            {
                Original_ID = Comand_ID.Original_ID,
                NEW_CMD_ID = this.pole.CMD_ID,
                Component_ID = Comand_ID.Component_ID,
            };
            if (conID.ShowDialog() != DialogResult.OK)
                return;
            Comand_ID.Set(this.pole.CMD_ID, conID.SetFlag,
                conID.Component_ID, conID.Original_ID, conID.NEW_CMD_ID);
        }
        private void 常规复位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Comand_Reset.Reset(this.pole.CMD_ID, 0x00);
        }

        private void 复位至调试模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Comand_Reset.Reset(this.pole.CMD_ID, 0x01);
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



        public void Menuitem_Flush()
        {
            ToolStripMenuItem menu_time;
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
            this.AddDropDownMenuItem(menu_time, "复位至调试模式", this.复位至调试模式ToolStripMenuItem_Click);

            menu_time = this.AddDropDownMenuItem("模型参数");
            this.AddDropDownMenuItem(menu_time, "查询", this.查询模型参数ToolStripMenuItem_Click);
            this.AddDropDownMenuItem(menu_time, "设置", this.设置模型参数ToolStripMenuItem_Click);

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
