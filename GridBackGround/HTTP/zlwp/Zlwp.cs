using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Tools;
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
                SendError(context,Error_Code.RequestMethodError);
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
                SendError(context, Error_Code.JSONFormatError);
                context.Response.Close();
            }
            try
            {
                string command = context.Request.RawUrl;
                if (command.StartsWith("/"))
                    command = command.Substring(1);
                command = command.Substring(command.IndexOf('/') + 1);
                if (command.ToLower() == "watermarkset")
                {
                    zlwp.CmdWaterMarkSet deal = new CmdWaterMarkSet(context, o);
                    deal.Deal();
                }
                else if(command.ToLower() == "photoing")
                {
                    new CmdPhotoing(context, o).Deal();
                }
                else if(command.ToLower() == "phototableset")
                {
                    new CmdPhotoTableSet(context, o).Deal();
                }
                else if(command.ToLower() == "voiceplay")
                {
                    new CmdVoicePlay(context, o).Deal();
                }
                else
                {
                    SendError(context, Error_Code.UnknownCommand);
                }
            }
            catch(Exception ex)
            {
                SendError(context, Error_Code.InternalError, ex.Message);
            }
            return true;
        }

      
        public static bool SendError(HttpListenerContext context,Error_Code code,string message= null)
        {
            string str = EnumUtil.GetDescription(code)+" "+message;
            return SendError(context, (int)code, str);
        }

        public static bool SendError(HttpListenerContext context,int code, string str)
        {
            Error err = new Error()
            {
                Code = code,
                Message = str
            };
            string json = JsonConvert.SerializeObject(err);
            return Response(context, json);
        }

        public static bool Response(HttpListenerContext context, string message)
        {
            bool ret =  ReSendMsgService.SendResponse(context, message);
            context.Response.Close();
            return ret;
        }
    }


    public class Error{

        public int Code { get; set; }

        public string Message { get; set; }
    }

 


    public class Packet
    {

    }
}
