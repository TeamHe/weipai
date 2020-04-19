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
        //int _sDefaultLen = 102400;

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
            //byte[] buffer = new byte[_sDefaultLen];
            //Stream stream = hltc.Request.InputStream;
            //int sLen = 0;
            //int sIndex = 0;

            //while ((sIndex = stream.Read(buffer, sLen, 512)) != 0)
            //    sLen += sIndex;

            //if (sLen < 1)
            //{
            //    //反馈给第三方，并记录本地日志
            //    try
            //    {
            //        ReSendMsgService.SendResponse(hltc, "Post的数据为空.");
            //    }
            //    catch (Exception ex)
            //    {
            //        //GLOBAL.MyLog.WriteLog("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "返回给第三方Post的数据为空失败" + ex.Message);
            //    }
            //    //MyLog.WriteLog("对象：OnReceivPolisy: Post的数据为空.");
            //}
            ////解析、入库
            ////bool jxbl = RelePolicyBuffer(buffer, buffer.Length);
            //if (!jxbl)//XML解析失败
            //{
            //    try
            //    {
            //        //发送指令给第三方
            //        ReSendMsgService.SendResponse(hltc, "XML结构解析失败");
            //    }
            //    catch (Exception ex)
            //    {
            //        //MyLog.WriteLog("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "返回给第三方XML结构解析失败失败" + ex.Message);
            //    }
            //}
            ////否则发送0给第三方
            //try
            //{
            //    ReSendMsgService.SendResponse(hltc, new byte[] { 0x30 });
            //}
            //catch (Exception ex)
            //{
            //    //MyLog.WriteLog("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "返回给第三方 0 失败" + ex.Message);
            //}
        }
        //int r = 1;
        //解析入库方法
        //bool RelePolicyBuffer(byte[] buffer, int bLen)
        //{
        //    //此处为解析xml的脚本，省略，，，，，，，xmltextreader方式解析，单向只读

        //}
    }
}
