using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace GridBackGround.HTTP
{
    /// <summary>
    /// /将响应结果反馈第三方，否则第三方默认失败，如此将延迟发送时间
    /// </summary>
    public class ReSendMsgService
    {

        #region SendResponse 给请求发发送应答
        public static bool SendResponse(HttpListenerContext ctx, string sErr)
        {
            byte[] buf = Encoding.Default.GetBytes(sErr);
            return SendResponse(ctx, 200, buf);
        }

        public static bool SendResponse(HttpListenerContext ctx, byte[] buf)
        {
            return SendResponse(ctx, 200, buf);
        }

        public static bool SendResponse(HttpListenerContext ctx, int nStatusCode, byte[] buf)
        {
            try
            {
                ctx.Response.StatusCode = nStatusCode;
                ctx.Response.ContentLength64 = buf.Length;
                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        #endregion


        public static string GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (System.IO.Stream body = request.InputStream) // here we have data
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }


        }
    }
 }
