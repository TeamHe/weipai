﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GridBackGround.HTTP.zlwp
{
    public class CmdPhotoTableSet
    {
        /// <summary>
        /// 请求handle
        /// </summary>
        public HttpListenerContext Context { get; set; }
        /// <summary>
        /// JSON请求内容
        /// </summary>
        public JObject jObject { get; set; }


        public CmdPhotoTableSet()
        {

        }

        public CmdPhotoTableSet(HttpListenerContext context,JObject obj)
        {
            this.Context = context;
            this.jObject = obj;
        }

        public void Deal()
        {
            try
            {

                if (jObject["mn"] == null || jObject["tables"] == null || jObject["channel"]==null)
                {
                    Zlwp.SendError(this.Context, Error_Code.InvalidPara);
                    return;
                }
                string mn = jObject["mn"].ToString();
                int channel = (int)jObject["channel"];
                List<CommandDeal.IPhoto_TimeTable> list = new List<CommandDeal.IPhoto_TimeTable>();
                JArray jArray = jObject["tables"] as JArray;
                foreach(JObject jObject in jArray)
                {
                    if(jObject["hour"] == null || jObject["minute"] == null || jObject["preseting"] == null)
                    {
                        Zlwp.SendError(this.Context, Error_Code.InvalidPara);
                        return;
                    }
                    int hour = (int )jObject["hour"];
                    int minute = (int)jObject["minute"];
                    int preseting = (int)jObject["preseting"];
                    CommandDeal.PhotoTimeTable time = new CommandDeal.PhotoTimeTable(hour,minute,preseting);
                    list.Add(time);
                }

                Termination.PowerPole pole = Termination.PowerPoleManage.Find(mn) as Termination.PowerPole;
                if (pole == null || pole.OnLine == false)
                {
                    Zlwp.SendError(this.Context, Error_Code.DeviceOffLine);
                    return;
                }
                pole.SetTimeTableResultEventHanlder += Pole_TimeTableResultEventHanlder;
                Error_Code code = pole.SetTimeTable(channel, list);
                if (code != Error_Code.Success)
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

        private void Pole_TimeTableResultEventHanlder(object sender, Termination.PowerPoleEventArgs e)
        {
            HttpRequestManager.RemoveFromRequestList(this);

            Zlwp.SendError(this.Context, e.Code);
            Termination.PowerPole pole = sender as Termination.PowerPole;
            pole.SetTimeTableResultEventHanlder -= Pole_TimeTableResultEventHanlder;
            return;
        }
    }
}