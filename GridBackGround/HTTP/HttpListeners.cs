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
            try
            {
                _httpListener = new HttpListener { AuthenticationSchemes = AuthenticationSchemes.Anonymous };
                _httpListener.Prefixes.Add(string.Format("http://+:{0}/", Port));
                _httpListener.Start();
                _httpListener.BeginGetContext(new AsyncCallback(GetContextCallBack), _httpListener);
                //LogInfo("开始监听端口:" + Port);
                //while (true)
                //{
                //    try
                //    {
                //        //监听客户端的连接，线程阻塞，直到有客户端连接为止
                //        var client = _httpListener.GetContext();
                //        new Thread(HandleRequest).StartAsync(client);
                //    }
                //    catch (Exception ex)
                //    {
                //        //LogError(ex.Message + ex.StackTrace);
                //    }
                //}
            }
            catch (Exception e)
            {
                //LogError(e.Message + e.StackTrace);
                Environment.Exit(0);
            }
        }

        private void GetContextCallBack(IAsyncResult ar)
        {
            HttpListenerContext context = _httpListener.EndGetContext(ar);
            _httpListener.BeginGetContext(new AsyncCallback(GetContextCallBack), _httpListener);

            //ThreadPool.QueueUserWorkItem(new HttpRequestManager().OnHttpRequest, context); //线程池委托接收对象

            new HttpRequestManager().OnHttpRequest(context);
            //HandleRequest(context);

        }

        //private static void HandleRequest(object obj)
        //{
        //    var client = obj as HttpListenerContext;
        //    if (client == null) return;
        //    try
        //    {
        //        var coding = Encoding.UTF8;
        //        var request = client.Request;
        //        // 取得回应对象
        //        var response = client.Response;
        //        response.StatusCode = 200;
        //        response.ContentEncoding = coding;

        //        Console.WriteLine("{0} {1} HTTP/1.1", request.HttpMethod, request.RawUrl);
        //        Console.WriteLine("Accept: {0}", string.Join(",", request.AcceptTypes));
        //        if(request.UserLanguages != null)
        //        {
        //            Console.WriteLine("Accept-Language: {0}",
        //                string.Join(",", request.UserLanguages));

        //        }
        //        Console.WriteLine("User-Agent: {0}", request.UserAgent);
        //        Console.WriteLine("Accept-Encoding: {0}", request.Headers["Accept-Encoding"]);
        //        Console.WriteLine("Connection: {0}",
        //            request.KeepAlive ? "Keep-Alive" : "close");
        //        Console.WriteLine("Host: {0}", request.UserHostName);
        //        Console.WriteLine("Pragma: {0}", request.Headers["Pragma"]);

        //        // 构造回应内容
        //        string responseString = @"<html><head><title>HttpListener Test</title></head><body><div>Hello, world.</div></body></html>";


        //        byte[] buffer = Encoding.UTF8.GetBytes(responseString);
        //        //对客户端输出相应信息.
        //        response.ContentLength64 = buffer.Length;
        //        Stream output = response.OutputStream;
        //        output.Write(buffer, 0, buffer.Length);
        //        //关闭输出流，释放相应资源
        //        output.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        //LogError(e.Message + e.StackTrace);
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            client.Response.Close();
        //        }
        //        catch (Exception e)
        //        {
        //            //LogError(e.Message);
        //        }
        //    }
        //}
    }

    //public static class ThreadHelper
    //{
    //    /// <summary>
    //    /// 开启同步多线程
    //    /// </summary>
    //    public static void StartSync(this IEnumerable<Thread> threads, object startPara = null, Func<object, object> callback = null)
    //    {
    //        var ts = threads.ToArray();
    //        //启动线程
    //        foreach (var thread in ts)
    //        {
    //            if (!thread.IsBackground)
    //            {
    //                thread.IsBackground = true;
    //            }
    //            var times = 0;
    //            while (thread.ThreadState == (ThreadState.Background | ThreadState.Unstarted) && times < 10)
    //            {
    //                try
    //                {
    //                    if (startPara == null)
    //                    {
    //                        thread.Start();
    //                    }
    //                    else
    //                    {
    //                        thread.Start(startPara);
    //                    }
    //                }
    //                catch (Exception e)
    //                {
    //                    times++;
    //                }
    //                Thread.Sleep(100);
    //            }
    //        }
    //        Thread.Sleep(2000);
    //        //等待全部结束
    //        foreach (var thread in ts)
    //        {
    //            try
    //            {
    //                thread.Join();
    //            }
    //            catch (Exception e)
    //            {
    //            }
    //        }
    //        if (callback != null)
    //        {
    //            callback(startPara);
    //        }
    //    }


    //    /// <summary>
    //    /// 开启多线程
    //    /// </summary>
    //    public static void StartAsync(this IEnumerable<Thread> threads, object startPara = null, Func<object, object> callback = null)
    //    {
    //        var ts = threads.ToArray();
    //        //启动线程
    //        foreach (var thread in ts)
    //        {
    //            if (!thread.IsBackground)
    //            {
    //                thread.IsBackground = true;
    //            }
    //            var times = 0;
    //            while (thread.ThreadState == (ThreadState.Background | ThreadState.Unstarted) && times < 10)
    //            {
    //                try
    //                {
    //                    if (startPara == null)
    //                    {
    //                        thread.Start();
    //                    }
    //                    else
    //                    {
    //                        thread.Start(startPara);
    //                    }
    //                }
    //                catch (Exception e)
    //                {
    //                    times++;
    //                }
    //                Thread.Sleep(100);
    //            }
    //        }
    //        if (callback != null)
    //        {
    //            callback(startPara);
    //        }
    //    }


    //    /// <summary>
    //    /// 开启同步线程
    //    /// </summary>
    //    public static void StartSync(this Thread thread, object parameter = null)
    //    {
    //        try
    //        {
    //            if (!thread.IsBackground)
    //            {
    //                thread.IsBackground = true;
    //            }
    //            if (parameter == null)
    //            {
    //                thread.Start();
    //            }
    //            else
    //            {
    //                thread.Start(parameter);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //        }
    //        Thread.Sleep(1000);
    //        try
    //        {
    //            thread.Join();
    //        }
    //        catch (Exception e)
    //        {
    //        }
    //    }


    //    /// <summary>
    //    /// 开启带超时的同步线程
    //    /// </summary>
    //    public static void StartSyncTimeout(this Thread thread, int timeoutSeconds, object parameter = null)
    //    {
    //        try
    //        {
    //            if (!thread.IsBackground)
    //            {
    //                thread.IsBackground = true;
    //            }
    //            if (parameter == null)
    //            {
    //                thread.Start();
    //            }
    //            else
    //            {
    //                thread.Start(parameter);
    //            }
    //            thread.Join(timeoutSeconds * 1000);
    //        }
    //        catch (Exception e)
    //        {
    //        }
    //    }


    //    /// <summary>
    //    /// 开启异步线程
    //    /// </summary>
    //    public static void StartAsync(this Thread thread, object parameter = null)
    //    {
    //        try
    //        {
    //            if (!thread.IsBackground)
    //            {
    //                thread.IsBackground = true;
    //            }
    //            if (parameter == null)
    //            {
    //                thread.Start();
    //            }
    //            else
    //            {
    //                thread.Start(parameter);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //        }
    //    }
    //}
}
