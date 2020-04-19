using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GridBackGround.HTTP.zlwp
{
    public class Zlwp
    {
        public Zlwp() { }

        public bool Deal(HttpListenerContext context)
        {
            //http Method 方法检查
            if(context.Request.HttpMethod != "POST")
            {
                SendError(context, "Only Accept Post");
                context.Response.Close();
                return true;
            }
            //数据报文格式化
            JObject o = null;
            try
            {
                string content = ReSendMsgService.GetRequestPostData(context.Request);
                o = JObject.Parse(content);
            }
            catch (Exception ex)
            {
                SendError(context, "Resquest format error. " + ex.Message);
                context.Response.Close();
            }
            //检查是否存在指令字符串
            if (!o.ContainsKey("root"))
            {
                SendError(context, "Cannot find command feild");
                context.Response.Close();
            }

            try
            {
                JToken token = o.GetValue("root");
                string command = token.ToString();
                if(command == "morning")
                {
                    Response(context, "receive morning command");
                    context.Response.Close();
                }
                else
                {
                    SendError(context, "unsuported command");
                    context.Response.Close();
                }

            }catch(Exception ex)
            {
                SendError(context, "internal error");
                context.Response.Close();
            }
            return true;
        }


        public bool SendError(HttpListenerContext context, string message)
        {
            Error err = new Error();
            err.Message = message; ;
            string json = JsonConvert.SerializeObject(err);
            return Response(context, json);
        }

        public static bool Response(HttpListenerContext context, string message)
        {
            context.Response.AddHeader("Content-Type", "application/json");
            return ReSendMsgService.SendResponse(context, message);
        }
    }


    public class Error{
        //public string MN { get; set; }

        //public int Code { get; set; }

        public string Message { get; set; }
    }


    public class Packet
    {

    }
}
