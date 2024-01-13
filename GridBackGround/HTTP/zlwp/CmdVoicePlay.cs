using System;
using System.Net;
using Newtonsoft.Json.Linq;
using ResModel.PowerPole;

namespace GridBackGround.HTTP.zlwp
{
    /// <summary>
    /// 
    /// </summary>
    public class CmdVoicePlay
    {
        /// <summary>
        /// 请求handle
        /// </summary>
        public HttpListenerContext Context { get; set; }
        /// <summary>
        /// JSON请求内容
        /// </summary>
        public JObject jObject { get; set; }

        public CmdVoicePlay()
        {

        }


        public CmdVoicePlay(HttpListenerContext context, JObject jobj)
        {
            this.Context = context;
            this.jObject = jobj;
        }


        public void Deal()
        {
            try
            {
                if (jObject["mn"] == null || jObject["index"] == null
                || jObject["status"] == null)
                {
                    Zlwp.SendError(this.Context, Error_Code.InvalidPara);
                    return;
                }
                string cmdid = jObject["mn"].ToString();
                int index = (int)jObject["index"];
                int status = (int)jObject["status"];
                int interval = 0;
                if(jObject["interval"] != null)
                    interval = (int)jObject["interval"];
                if (status != 0x01)
                    status = 0x02;
                if(status == 0x01 && jObject["interval"] == null)
                {
                    Zlwp.SendError(this.Context, Error_Code.InvalidPara);
                    return;
                }


                Termination.PowerPole pole = Termination.PowerPoleManage.Find(cmdid) as Termination.PowerPole;
                if (pole == null || pole.is_online() == false)
                {
                    Zlwp.SendError(this.Context, Error_Code.DeviceOffLine);
                    return;
                }
                Error_Code code = Error_Code.UnknownCommand;
                //pole.VoiceLightAlarmEventHanlder += ResultEventHanlder;
                //Error_Code code = pole.VoicePlay(index, status,interval);
                if(code != Error_Code.Success)
                {
                    Zlwp.SendError(this.Context, code);
                    return;
                }
                HttpRequestManager.AddToRequestList(this);
            }
            catch (Exception ex)
            {
                Zlwp.SendError(this.Context, Error_Code.InternalError, ex.Message);
                LogHelper.WriteLog("httpRequest-Photoing", ex);
                return;
            }
        }

        private void ResultEventHanlder(object sender, PowerPoleEventArgs e)
        {
            Zlwp.SendError(this.Context, e.Code);
            HttpRequestManager.RemoveFromRequestList(this);
            //Termination.PowerPole pole = sender as Termination.PowerPole;
            //pole.VoiceLightAlarmEventHanlder -= ResultEventHanlder;

            return;
        }
    }
}
