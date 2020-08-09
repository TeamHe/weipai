using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace GridBackGround.HTTP
{
    public class HttpListeners
    {
        private HttpListener _httpListener;
        public int Port { get; set; }
        /// <summary>
        /// 绑定的IP地址(暂未定定义使用方法)
        /// </summary>
        public IPAddress Address { get; set; }

        public  HttpListeners()
        {
            this.Port = 80;
           
        }

        public HttpListeners(int port)
        {
            this.Port = port;
        }

        public bool ListenerStop()
        {
            try
            {
                if (_httpListener != null)
                {
                    //LogInfo("停止监听端口:" + Port);
                    _httpListener.Stop();
                }
                return true;
            }
            catch (Exception e)
            {
                //LogError(e.Message + e.StackTrace);
                return false;
            }
        }


        /// <summary>
        /// 监听端口
        /// </summary>
        public void ListenerStart()
        {
            //try
            //{
                _httpListener = new HttpListener { AuthenticationSchemes = AuthenticationSchemes.Anonymous };
                _httpListener.Prefixes.Add(string.Format("http://+:{0}/", Port));
                _httpListener.Start();
                _httpListener.BeginGetContext(new AsyncCallback(GetContextCallBack), _httpListener);
            //}
            //catch (Exception e)
            //{
            //    //LogError(e.Message + e.StackTrace);
            //    Environment.Exit(0);
            //}
        }

        private void GetContextCallBack(IAsyncResult ar)
        {
            try
            {
                HttpListenerContext context = _httpListener.EndGetContext(ar);
                _httpListener.BeginGetContext(new AsyncCallback(GetContextCallBack), _httpListener);

                //ThreadPool.QueueUserWorkItem(new HttpRequestManager().OnHttpRequest, context); //线程池委托接收对象

                new HttpRequestManager().OnHttpRequest(context);
                //HandleRequest(context);

            }
            catch
            {

            }

        }
    }
}
