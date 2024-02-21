using System;
using System.Net;
using Newtonsoft.Json.Linq;
using ResModel.PowerPole;
using cma.service.PowerPole;

namespace GridBackGround.HTTP.zlwp
{
    public class CmdPhotoing
    {
        /// <summary>
        /// 请求handle
        /// </summary>
        public HttpListenerContext Context { get; set; }
        /// <summary>
        /// JSON请求内容
        /// </summary>
        public JObject jObject { get; set; }

        public CmdPhotoing()
        {

        }


        public CmdPhotoing(HttpListenerContext context, JObject jobj)
        {
            this.Context = context;
            this.jObject = jobj;
        }


        public void Deal()
        {
            try
            {
                if (jObject["mn"] == null || jObject["channel"] == null
                || jObject["preseting"] == null )
                {
                    Zlwp.SendError(this.Context, Error_Code.InvalidPara);
                    return;
                }
                string cmdid = jObject["mn"].ToString();
                int channel = (int)jObject["channel"];
                int preseting = (int)jObject["preseting"];

            
                PowerPole pole = Termination.PowerPoleManage.Find(cmdid) as PowerPole;
                if (pole == null || pole.is_online() == false)
                {
                    Zlwp.SendError(this.Context, Error_Code.DeviceOffLine);
                    return;
                }
                Error_Code code = Error_Code.UnknownCommand;
                //pole.PhotoingResultEventHanlder += Pole_PhotoingResultEventHanlder;
                //Error_Code code = pole.Photiong(channel, preseting);
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

        private void Pole_PhotoingResultEventHanlder(object sender, PowerPoleEventArgs e)
        {
            Zlwp.SendError(this.Context, e.Code);
            HttpRequestManager.RemoveFromRequestList(this);
            //Termination.PowerPole pole = sender as Termination.PowerPole;
            //pole.PhotoingResultEventHanlder -= Pole_PhotoingResultEventHanlder;

            return;
        }
    }
}
