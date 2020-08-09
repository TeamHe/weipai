﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            
                Termination.PowerPole pole = Termination.PowerPoleManage.Find(cmdid) as Termination.PowerPole;
                if (pole == null || pole.OnLine == false)
                {
                    Zlwp.SendError(this.Context, Error_Code.DeviceOffLine);
                    return;
                }
                pole.PhotoingResultEventHanlder += Pole_PhotoingResultEventHanlder;
                Error_Code code = pole.Photiong(channel, preseting);
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
                return;
            }
        }

        private void Pole_PhotoingResultEventHanlder(object sender, Termination.PowerPoleEventArgs e)
        {
            Zlwp.SendError(this.Context, e.Code);
            HttpRequestManager.RemoveFromRequestList(this);
            Termination.PowerPole pole = sender as Termination.PowerPole;
            pole.PhotoingResultEventHanlder -= Pole_PhotoingResultEventHanlder;

            return;
        }
    }
}