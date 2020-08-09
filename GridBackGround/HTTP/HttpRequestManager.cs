using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace GridBackGround.HTTP
{
    public class HttpRequestManager
    {

        public virtual void OnHttpRequest(object context)
        {
            HttpListenerContext client = context as HttpListenerContext;
            if (client == null) return;
            try
            {
                var coding = Encoding.UTF8;
                var request = client.Request;
                // 取得回应对象
                var response = client.Response;
                response.StatusCode = 200;
                response.ContentEncoding = coding;

                Console.WriteLine("{0} {1} HTTP/1.1", request.HttpMethod, request.RawUrl);
                Console.WriteLine("Accept: {0}", string.Join(",", request.AcceptTypes));
                if (request.UserLanguages != null)
                {
                    Console.WriteLine("Accept-Language: {0}",
                        string.Join(",", request.UserLanguages));

                }
                Console.WriteLine("User-Agent: {0}", request.UserAgent);
                Console.WriteLine("Accept-Encoding: {0}", request.Headers["Accept-Encoding"]);
                Console.WriteLine("Connection: {0}",
                    request.KeepAlive ? "Keep-Alive" : "close");
                Console.WriteLine("Host: {0}", request.UserHostName);
                Console.WriteLine("Pragma: {0}", request.Headers["Pragma"]);

                if (request.RawUrl.StartsWith("/zlwp"))
                {
                    new zlwp.Zlwp().Deal(client);
                }
                else
                {
                    string content = ReSendMsgService.GetRequestPostData(request);
                    string responseString = String.Format(@"<html><head><title>HttpListener Test</title></head><body><div>Hello, world.--{0}</div></body></html>", content);
                    ReSendMsgService.SendResponse(client, responseString);
                }

               

                //byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                ////对客户端输出相应信息.
                //response.ContentLength64 = buffer.Length;
                //Stream output = response.OutputStream;
                //output.Write(buffer, 0, buffer.Length);
                //关闭输出流，释放相应资源
                //output.Close();
            }
            catch (Exception e)
            {
                //LogError(e.Message + e.StackTrace);
                client.Response.Close();
            }
        }



        //接收，解析方法
        void OnReceivPolisy(HttpListenerContext hltc)
        {
        }

        private static List<object> RequestList = new List<object>();

        public static void AddToRequestList(object obj)
        {
            RequestList.Add(obj);
        }

        public static void RemoveFromRequestList(object obj)
        {
            RequestList.Remove(obj);
        }
    }
}
