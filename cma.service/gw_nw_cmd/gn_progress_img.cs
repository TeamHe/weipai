/**
 * 图片上传管理任务
 * 
 * 1， 将图片缓存添加到IPowerpole 的prpoerty(gn_img)中
 * 2， 每一个设备只支持多个图片缓存，图片缓存的key 为 chno *256 + perset
 * 3,  每一个设备有一个图片配置object gn_img_cfg (暂时未启用) TODO
 * 4， 图片存储路径可配置(默认存储路径为可以执行程序所在目录)
 * 5， 图片支持上传到http server, 添加水印文字（暂时注释） TODO
 */
using DB_Operation;
using DB_Operation.RealData;
using ResModel;
using ResModel.PowerPole;
using System;
using System.Collections.Generic;
using System.IO;
using ResModel.gw_nw;
using System.Timers;

namespace cma.service.gw_nw_cmd
{
    public class gn_progress_img
    {
        #region Static Function
        /// <summary>
        /// 获取通道号和预置位号对应的图像handle
        /// 当handle 不存在时创建handle,并返回
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="chno"></param>
        /// <param name="preset"></param>
        /// <returns></returns>
        public static gn_progress_img GetImg(IPowerPole pole, int chno, int preset,bool create = false)
        {
            int key = ((chno & 0xff) << 8) | (preset & 0xff);
            if (pole == null)
                return null;
            Dictionary<int, gn_progress_img> imgs = pole.GetProperty("gn_img") as Dictionary<int, gn_progress_img>;
            if (imgs == null)
            {
                imgs = new Dictionary<int, gn_progress_img>();
                pole.SetProperty("gn_img", imgs);
            }
            if (create == false)
                return imgs[key];
              
            if (!imgs.ContainsKey(key))
            {
                imgs.Add(key, new gn_progress_img()
                {
                    ChNO = chno,
                    Preset = preset,
                    Pole = pole,
                });
            }
            return imgs[key];
        }

        public static void RmImg(IPowerPole pole, int chno,int preset)
        {
            if (pole == null) return;
            Dictionary<int, gn_progress_img> imgs = pole.GetProperty("gn_img") as Dictionary<int, gn_progress_img>;
            if (imgs == null || imgs.Count == 0)
                return;
            int key = ((chno & 0xff) << 8) | (preset & 0xff);
            if (imgs.ContainsKey(key))
                imgs.Remove(key);
        }
        #endregion
        public static string DefaultPath { get; set; }

        public int ChNO { get; set; }

        public int Preset { get; set; }

        public string FileName { get; set; }

        public DateTime Time { get; set; }

        public int Pnum { get; set; }

        public Dictionary<int, byte[]> Packs { get; set; }

        public int StartPNO { get { return 1; } }

        public IPowerPole Pole { get; set; }

        public Timer Timer { get; set; }

        public gn_progress_img()
        {
            this.Timer = new Timer();
            this.Timer.Interval = 5 * 60 * 1000;
            Timer.Elapsed += Timer_Elapsed;
            this.Packs = new Dictionary<int, byte[]>();
        }



        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Stop();
            this.Info("图片接收超时，清除缓存");
        }

        private void TimerRestart()
        {
            if (this.Timer == null) return;
            this.Timer.Stop();
            this.Timer.Start();
        }

        private void Stop()
        {
            this.Timer.Stop();
            gn_progress_img.RmImg(this.Pole, this.ChNO, this.Preset);
        }

        /// <summary>
        /// 获取图片存储路径
        /// </summary>
        /// <returns></returns>
        public string GetSavedPath(string rootPath)
        {
            string fileName = string.Format("{0:yyMMddHHmmss}_{1}_{2}.jpg",
                this.Time, this.ChNO, this.Preset);

            return Path.Combine(
                rootPath,
                "picture",
                Pole.CMD_ID,
                this.Time.ToString("yyyyMM"),
                this.Time.Day.ToString("D2"),
                fileName);
        }

        public virtual string GetRootPath()
        {
            string path = Environment.CurrentDirectory;
            if (!string.IsNullOrEmpty(DefaultPath))
                path = DefaultPath;
            return path;
        }

        public virtual bool SaveToDB()
        {
            db_data_picture db = new db_data_picture(this.Pole);
            if ( db.DataSave(new gn_picture() {
                ChNO = this.ChNO,
                Preset = this.Preset,
                FileName = this.FileName,
                Time = this.Time,
            }) == ErrorCode.NoError)
                return true;
            else
                return false;
        }

        public void AddPac(int pno, byte[] pac)
        {
            if (this.Packs == null)
                this.Packs = new Dictionary<int, byte[]>();
            this.Packs[pno] = pac;
            TimerRestart();
        }

        public List<int> GetRemainPacs()
        {
            return this.GetRemainPacs(this.StartPNO);
        }

        public List<int> GetRemainPacs(int startpno)
        {
            List<int> lost = new List<int>();
            if (this.Packs == null)
                this.Packs = new Dictionary<int, byte[]>();
            for (int i = 0; i < this.Pnum; i++)
            {
                if (!Packs.ContainsKey(i + startpno))
                    lost.Add(i);
            }
            return lost;
        }

        private void CreateDir(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public bool SaveToFile(int startpno, out string msg)
        {
            bool stat = true;
            FileStream fs = null;
            try
            {
                string dir = Path.GetDirectoryName(this.FileName);
                if (!Directory.Exists(dir))
                    CreateDir(dir);

                fs = new FileStream(FileName, FileMode.Create);
                for (int i = 0; i < this.Pnum; i++)
                {
                    if (this.Packs.ContainsKey(i + startpno))
                        fs.Write(this.Packs[i + startpno], 0, this.Packs[i + startpno].Length);
                    else
                        throw new Exception(string.Format("Pack no {0} missesd", i));
                }
                msg = string.Format("图片合成成功 file:///{0}", FileName);
            }
            catch (Exception e)
            {
                stat = false;
                msg = "图片合成失败" + e.Message;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return stat;
        }


        public bool SaveToFile(out string msg)
        {
            return this.SaveToFile(this.StartPNO,out msg);
        }

       

        public void Finish()
        {
            this.FileName = GetSavedPath(GetRootPath());

            string msg = string.Empty;
            bool ret = this.SaveToFile(out msg);
            this.Info("照片合成", msg);
            if (!ret) return;

            //AddWaterMarket();
            //this.Upload();
            this.SaveToDB();
            this.Stop();
        }


        protected void Info(string msg)
        {
            this.Info("图片处理", msg);
        }

        protected void Info(string type, string msg)
        {
            DisPacket.NewRecord(new PackageRecord(
                PackageRecord_RSType.Info,
                this.Pole,
               type, msg));
        }



        ///// <summary>
        ///// 向图片的右下角添加指定文字
        ///// 同时在图片左上角添加指定时间
        ///// </summary>
        ///// <param name="text"></param>
        ///// <param name="time"></param>
        ///// <returns></returns>
        //public void AddWaterMarket(string text, DateTime time, bool isname = true, bool istime = true)
        //{
        //    List<WaterMarkText> WmtList = new List<WaterMarkText>();
        //    //右下角添加的文字
        //    if (isname)
        //        WmtList.Add(
        //            new WaterMarkText(text,
        //                       WaterMarkPosition.WMP_Right_Bottom));
        //    //左上角添加时间
        //    if (istime)
        //        WmtList.Add(
        //            new WaterMarkText(time.ToString("yyyy-MM-dd HH:mm"),
        //                WaterMarkPosition.WMP_Left_Top));

        //    var newFileName = Path.GetFileNameWithoutExtension(this.Picture.FileName) + "-n.jpg";
        //    var newPath = Path.Combine(Path.GetDirectoryName(this.Picture.FileName), newFileName);

        //    new ImageWaterMark().addWaterMark(this.Picture.FileName,
        //                           newPath,
        //                           WaterMarkType.TextMark,
        //                           WmtList);
        //    this.Picture.FileName = newPath;
        //}

        //public void AddWaterMarket(string text, bool isname = true, bool istime = true)
        //{
        //    AddWaterMarket(text, this.Picture.Time, isname, istime);
        //}

        //public bool AddWaterMarket()
        //{
        //    try
        //    {
        //        Equ equ = this.Pole.Equ;
        //        if (!equ.IS_Mark && !equ.Is_Time)
        //            return true ;

        //        string text = equ.MarketText;
        //        if (string.IsNullOrEmpty(text))
        //            text = "测试";

        //        this.AddWaterMarket(text,equ.IS_Mark,equ.Is_Time);
        //        return true ;
        //    }
        //    catch (Exception ex)
        //    {
        //        Info("图片添加水印文字失败:" + ex.Message);
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 将图片上传到指定URL
        ///// </summary>
        ///// <param name="url">图片上传地址</param>
        ///// <param name="id">设备编号</param>
        ///// <param name="time">拍照时间</param>
        ///// <param name="path">图片路径</param>
        ///// <returns>上传状态</returns>
        //public bool Upload(string url, string id, DateTime time, string path)
        //{
        //    bool ret = false;
        //    NameValueCollection collectons = new NameValueCollection();
        //    collectons.Add("user", id);
        //    collectons.Add("chno", this.Picture.ChNO.ToString());
        //    collectons.Add("preset", this.Picture.Preset.ToString());
        //    collectons.Add("ts", time.ToString("yyyyMMddHHmmss"));

        //    if (HttpHelper.HttpUploadFile(url, path, collectons) == "upload-ok")
        //        ret = true;
        //    return ret;
        //}


        ///// <summary>
        ///// 将图片上传到指定URL
        ///// </summary>
        ///// <param name="url"></param>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public bool Upload(string url, string user)
        //{
        //    return Upload(url, user, this.Picture.Time);
        //}

        ///// <summary>
        ///// 将图片上传到指定URL
        ///// </summary>
        ///// <param name="url">图片上传地址</param>
        ///// <param name="id">设备编号</param>
        ///// <param name="time">拍照时间</param>
        ///// <returns>上传状态</returns>
        //public bool Upload(string url, string id, DateTime time)
        //{
        //    return Upload(url, id, time, this.Picture.FileName);
        //}

        //protected virtual string GetUpoloadUrl()
        //{
        //    Equ equ = this.Pole.Equ;
        //    if (equ.UrlID == 0)
        //        return string.Empty;
        //    DB_Url db_url = new DB_Url();
        //    UrlInterFace url = null;
        //    if ((url = db_url.GetUrl(equ.UrlID)) != null)
        //        return url.Url;
        //    else
        //        return string.Empty;
        //}

        //public bool Upload()
        //{
        //    try
        //    {
        //        Equ equ = this.Pole.Equ;
        //        string url = GetUpoloadUrl();
        //        if (string.IsNullOrEmpty(url))
        //            return true;
        //        this.Upload(url,equ.EquNumber);
        //        this.Info("图片上传成功");
        //        return true;
        //    }catch (Exception ex)
        //    {
        //        Info(string.Format("图片上传失败 {0}",ex.Message));
        //        return false;
        //    }
        //}

    }
}