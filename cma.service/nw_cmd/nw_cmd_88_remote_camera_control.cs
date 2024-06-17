using ResModel;
using System;
using ResModel.nw;

namespace cma.service.nw_cmd
{
    public class nw_cmd_88_remote_camera_control : nw_cmd_base
    {
        public override int Control { get { return 0x88; } }

        public override string Name { get { return "摄像机远程调节"; } }

        public string Password { get; set; }


        public nw_camera_action Action { get; set; }

        public nw_cmd_88_remote_camera_control() { }

        public nw_cmd_88_remote_camera_control(IPowerPole pole):base(pole) { }



        public override int Decode(out string msg)
        {
            if (Data == null || (Data.Length != 2 && Data.Length != 7))
                throw new Exception(string.Format("数据域长度错误,应为7 或2字节 实际为:{1}",
                    this.Data != null ? this.Data.Length : 0));

            if (this.Data.Length == 2)
            {
                if (Data[0] == 0xff && Data[1] == 0xff)
                    msg = " 失败。原密码错误";
                else
                    msg = string.Format("失败。错误码:{0:X2}{1:X2}H", Data[0], Data[1]);
                return 0;
            }

            int offset = 0;
            offset += this.GetPassword(this.Data, offset, out string password); 
            this.Password = password;

            if(this.Action == null)
                this.Action = new nw_camera_action();
            this.Action.Channel_no = this.Data[offset++];
            this.Action.actrion = (nw_camera_action.Actrion)this.Data[offset++];
            this.Action.Para = this.Data[offset++];

            msg = "成功。" + this.Action.ToString();
            return 0;

        }

        public override byte[] Encode(out string msg)
        {
            if (this.Action == null || Action.actrion == 0)
                throw new ArgumentNullException("Action");

            byte[] data = new byte[7];
            int offset = 0;
            offset += this.SetPassword(data, offset, this.Password);
            data[offset++] = (byte)this.Action.Channel_no;
            data[offset++] = (byte)this.Action.actrion;
            data[offset++] = (byte)this.Action.Para;
            msg = this.Action.ToString();
            return data;
        }
    }
}
