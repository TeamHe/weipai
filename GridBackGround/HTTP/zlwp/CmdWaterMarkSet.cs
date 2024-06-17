using System;
using System.Net;
using Newtonsoft.Json.Linq;
using ResModel.PowerPole;
using cma.service;

namespace GridBackGround.HTTP.zlwp
{
    /// <summary>
    /// 设置水印叠加信息
    /// </summary>
    public class CmdWaterMarkSet
    {
        /// <summary>
        /// 请求handle
        /// </summary>
        public HttpListenerContext Context { get; set; }
        /// <summary>
        /// JSON请求内容
        /// </summary>
        public JObject jObject { get; set; }

        /// <summary>
        /// 构造内容
        /// </summary>
        public CmdWaterMarkSet()
        {

        }

        public CmdWaterMarkSet(HttpListenerContext ctx,JObject o)
        {
            this.Context = ctx;
            this.jObject = o;
        }


        public void Deal()
        {
            if (jObject["mn"] == null || jObject["label"] == null
                || jObject["is_time"]==null || jObject["is_mark"] == null)
            {
                Zlwp.SendError(this.Context, Error_Code.InvalidPara);
                return;
            }
            string mn = jObject["mn"].ToString();
            string label = jObject["label"].ToString();
            bool is_time = (bool)jObject["is_time"];
            bool is_mark = (bool)jObject["is_mark"];

            try
            {
                ResModel.EQU.Equ equ = DB_Operation.EQUManage.DB_EQU.GetEqu(mn);
                if(equ == null)
                {
                    Zlwp.SendError(this.Context, Error_Code.UnknonwMN);
                    return;
                }

                equ.IS_Mark = is_mark;
                equ.Is_Time = is_time;
                equ.MarketText = label;

                //保存到数据库
                DB_Operation.EQUManage.DB_EQU.Up_Station(equ);
                //刷新内存信息
               PowerPoleManage.GetInstance().UpdatePoleStation(mn);
                Zlwp.SendError(this.Context, Error_Code.Success);
            }catch(Exception ex)
            {
                Zlwp.SendError(this.Context, Error_Code.InternalError, ex.Message);
                LogHelper.WriteLog("httpRequest-Photoing", ex);
                return;
            }

        }

    }
}
