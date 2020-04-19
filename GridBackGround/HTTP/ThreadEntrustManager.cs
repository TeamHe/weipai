using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

namespace GridBackGround.HTTP
{
    /// <summary>
    /// 委托方法类
    /// </summary>
    //public class ThreadEntrustManager
    //{
    //    protected HttpListener _listener;
    //    Thread _ListenerThread;
    //    bool _bThreadLoop;
    //    string url;
    //    static string _ListenerUrls = XmlHelp.GetXmlNode("LocalListenUrl").InnerText;
    //    string[] _ListenerUrlsArray = _ListenerUrls.Split(';');

    //    public void ListenerStart()
    //    {
    //        if (_ListenerUrlsArray.Length > 0)
    //        {
    //            _listener = new HttpListener();
    //            _bThreadLoop = true;
    //            foreach (string strUrl in _ListenerUrlsArray)
    //            {
    //                url = strUrl;
    //                _listener.Prefixes.Add(url);//添加监听前缀对象
    //            }
    //            _listener.Start();
    //            //MyLog.WriteLog("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  start listening...");
    //            _ListenerThread = new Thread(new ThreadStart(ThreadFunction));
    //            _ListenerThread.Start();

    //        }
    //        else
    //        {
    //            _bThreadLoop = false;
    //            //日志
    //        }
    //    }

    //    void ThreadFunction()
    //    {
    //        while (_bThreadLoop)
    //        {
    //            try
    //            {
    //                HttpListenerContext hltc = _listener.GetContext();
    //                ThreadPool.QueueUserWorkItem(new HttpRequestManager().OnHttpRequest, hltc); //线程池委托接收对象
    //            }
    //            catch (Exception ex)
    //            {
    //                //GLOBAL.MyLog.WriteLog(ex);
    //                //Trace.Fail("对象：ThreadFunction ：An error occured in database access, details: " + ex.Message);
    //            }
    //        }
    //    }

    //    public void ListenerClose()
    //    {
    //        _ListenerThread.Abort();
    //        _bThreadLoop = false;
    //        _listener.Close();
    //    }

    //}
}
