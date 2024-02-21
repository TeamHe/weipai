using System.ComponentModel;
using Tools;
using ResModel;
using ResModel.PowerPole;
using cma.service;

namespace GridBackGround.CommandDeal
{
    /// <summary>
    /// 设备端声光报警
    /// </summary>
    public class Command_sound_light_alarm
    {
        
        /// <summary>
        /// 声光报警播放模式枚举
        /// </summary>
        public enum Play{
            [Description("开始播放")]
            Start=1,
            [Description("停止播放")]
            Stop,
        }
        /// <summary>
        /// 播放设备端录音文件
        /// </summary>
        /// <param name="cmd_id"></param>
        public static bool Option1(string cmd_id,Play status,int fileno,int interval)
        {
            byte[] data = new byte[4];
            data[0] = (byte)fileno;
            string msg = "文件编号：" + fileno.ToString() + " ";
            data[1] = (byte)status;
            msg += "播放状态:" + EnumUtil.GetDescription(status) + " ";
            if (status == Play.Start) {
                data[2] = (byte)(interval & 0xFF);                        //端口号
                data[3] = (byte)(interval >> 8 & 0xFF);
                msg += "播放时长:" + interval.ToString() + "s ";
            }
            var Packet = PacketAnaLysis.BuildPacket.PackBuild(
                cmd_id,
                4,
                PacketAnaLysis.TypeFrame.Control,
                PacketAnaLysis.PacketType_Control.SoundLightAlarm,
                FrameNO.GetFrameNO(),
                data);
            string errorMsg;
            bool ret = PackeDeal.SendData(cmd_id, Packet, out errorMsg);
            if (ret)
            {
                //显示发送的数据
                DisPacket.NewRecord(
                    new PackageRecord(
                        PackageRecord_RSType.send,
                         Termination.PowerPoleManage.Find(cmd_id),
                        "播放设备端录音文件",
                        msg));
            }
            return ret;
        }


        public static void Response(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            if (data.Length < 1)
                return;
            string pacMsg = "播放设备端录音文件:";
            Error_Code code = Error_Code.Success;
            if (data[0] == 0xff)
                pacMsg += "成功 ";
            else {
                code = Error_Code.DeviceError;
                pacMsg += "失败 ";
            }
               
            //显示发送的数据
            DisPacket.NewRecord(
                new PackageRecord(
                    PackageRecord_RSType.rec,
                    Termination.PowerPoleManage.Find(pole.CMD_ID),
                    "播放设备端录音文件",
                    pacMsg));
            //try
            //{
            //    Termination.PowerPole powerPole = pole as Termination.PowerPole;
            //    powerPole.OnVoiceLightAlarmFinish(code);
            //}
            //catch
            //{

            //}

        }


    }
}
