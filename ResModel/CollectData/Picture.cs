using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using System.Collections.Specialized;
using DB_Operation.RealData;
using DB_Operation.EQUManage;
using System.IO;

namespace ResModel.CollectData
{
    /// <summary>
    /// 图片保存
    /// </summary>
    public class Picture : CollectData
    {
        #region Public Variable
        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannalNO { get; set; }
        /// <summary>
        /// 预置位号
        /// </summary>
        public int Presetting_No { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicPath { get; set; }
        /// <summary>
        /// 是否已经上传完毕
        /// </summary>
        public bool UploadState { get; set; }
        /// <summary>
        /// 是否处于上传中
        /// </summary>
        public bool UploadingState { get; set; }

        /// <summary>
        /// 是否添加图片时间标记
        /// </summary>
        public bool Is_Time { get; set; }

        /// <summary>
        /// 是否添加杆塔名称标记
        /// </summary>
        public bool Is_Name { get; set; }
        /// <summary>
        /// 图片缓存
        /// </summary>
        public ImgMsg Img { get; set; }

        public object Lock { get;private  set; }

        #endregion

        #region Construction
        /// <summary>
        /// Construction
        /// </summary>
        public Picture()
        {
             Lock = new object();
            this.UploadState = false;
            UploadingState = false;
        }
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="Channal">通道号</param>
        public Picture(int Channal)
        {
            Lock = new object();
            this.ChannalNO = Channal;
            this.UploadState = false;
            UploadingState = false;
        }
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="channal">通道号</param>
        /// <param name="presset">预置位号</param>
        public Picture(int channal,int presset)
        {
            Lock = new object();
            this.ChannalNO = channal;
            this.Presetting_No = presset;
            this.UploadState = false;
            UploadingState = false;
        }
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="channal">通道号</param>
        /// <param name="presset">预置位号</param>
        /// <param name="time">拍照时间</param>
        public Picture(int channal, int presset, DateTime time)
        {
            Lock = new object();
            this.ChannalNO = channal;
            this.Presetting_No = presset;
            this.Maintime = time;
            this.UploadState = false;
            UploadingState = false;
        }

        public Picture(int channal,int presset,DateTime time,string path)
        {
            Lock = new object();
            this.ChannalNO = channal;
            this.Presetting_No = presset;
            this.Maintime = time;
            this.PicPath = path;
            this.UploadState = false;
            UploadingState = false;
        }
        #endregion

        #region Public Function
        /// <summary>
        /// 在图片左上角添加图片拍摄时间
        /// 在图片右下角添加众力科技字样
        /// </summary>
        /// <returns></returns>
        public TimeSpan AddWaterMarket()
        {
            return AddWaterMarket("众力科技");
        }
        /// <summary>
        /// 向图片的右下角添加指定文字
        /// 同时在图片左上角添加采集时间
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public TimeSpan AddWaterMarket(string text,bool isname=true,bool istime=true)
        {
            return AddWaterMarket(text, Maintime, isname, istime);
        }
        /// <summary>
        /// 向图片的右下角添加指定文字
        /// 同时在图片左上角添加指定时间
        /// </summary>
        /// <param name="text"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public TimeSpan AddWaterMarket(string text, DateTime time,bool isname=true,bool istime=true)
        {
            //try
            //{

                List<WaterMarkText> WmtList = new List<WaterMarkText>();
                //右下角添加的文字
                if(isname)
                    WmtList.Add(
                        new WaterMarkText(text,
                                   WaterMarkPosition.WMP_Right_Bottom));
                //左上角添加时间
                if(istime)
                    WmtList.Add(
                        new WaterMarkText(time.ToString("yyyy-MM-dd HH:mm"),
                            WaterMarkPosition.WMP_Left_Top));
                DateTime start = DateTime.Now;
                ImageWaterMark waterMark = new ImageWaterMark();

                var newFileName = Path.GetFileNameWithoutExtension(PicPath) + "-n.jpg";
                var newPath = Path.Combine(Path.GetDirectoryName(PicPath), newFileName);


                //string desPath = Path.GetDirectoryName(PicPath) + "\\" +
                //Path.GetFileNameWithoutExtension(PicPath) + ".png";
                waterMark.addWaterMark(this.PicPath,
                                       newPath,
                                       WaterMarkType.TextMark,
                                       WmtList);
                DateTime end = DateTime.Now;
                TimeSpan ts = end.Subtract(start);

                this.PicPath = newPath;
                return ts;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        /// <summary>
        /// 将图片上传到指定URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Upload(string url, string user)
        {
            return Upload(url, user, Maintime);
        }

        /// <summary>
        /// 将图片上传到指定URL
        /// </summary>
        /// <param name="url">图片上传地址</param>
        /// <param name="user">设备编号</param>
        /// <param name="time">拍照时间</param>
        /// <returns>上传状态</returns>
        public bool Upload(string url, string user, DateTime time)
        {
            return Upload(url, user, time, this.PicPath);
        }
        /// <summary>
        /// 将图片上传到指定URL
        /// </summary>
        /// <param name="url">图片上传地址</param>
        /// <param name="user">设备编号</param>
        /// <param name="time">拍照时间</param>
        /// <param name="path">图片路径</param>
        /// <returns>上传状态</returns>
        public bool Upload(string url, string user, DateTime time, string path)
        {
            bool ret = false;
            //try
            //{
                UploadingState = true;
                NameValueCollection collectons = new NameValueCollection();
                collectons.Add("user", user);
                collectons.Add("chno", this.ChannalNO.ToString());
                collectons.Add("preset", this.Presetting_No.ToString());
                collectons.Add("ts", time.ToString("yyyyMMddHHmmss"));

                string res = HttpHelper.HttpUploadFile(url, path, collectons);
            
                if (res == "upload-ok")
                {
                    this.UploadState = true;

                    ret = true;
                }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return ret;
        }

        /// <summary>
        /// 获取缺少的数据包包号
        /// </summary>
        /// <returns></returns>
        public int[] GetStilPac()
        {
            return this.Img.GetStilNo();
        }
        /// <summary>
        /// 添加上传的数据包
        /// </summary>
        /// <param name="SubPacNo"></param>
        /// <param name="data"></param>
        public void AddPicData(int SubPacNo, byte[] data)
        {
                this.Img.AddSubPac(SubPacNo, data);           
        }

        /// <summary>
        /// 将数据保存到数据库中
        /// </summary>
        public override void SaveToDB()
        {
            IRealData_OP iRdo = Real_Data_Op.Creat(EQU.ICMP.Picture);   //获取图片操作接口
            if (iRdo == null) throw new Exception("不支持的数据类型");
            if (this.PicPath == null) throw new ArgumentNullException("图片路径为空");
            //try                  //保存到数据库中
            //{
                iRdo.DataSave(this);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            this.UploadState = true;
        }
        #endregion
        /// <summary>
        /// 获取图片存储路径
        /// </summary>
        /// <returns></returns>
        public string GetSavedPath(string rootPath)
        {
            string filePath = null, fileName, fullPath, DirPath;
            try
            {
                filePath = Path.Combine(this.CMD_ID, 
                                        this.Maintime.ToString("yyyyMM"),
                                        this.Maintime.Day.ToString("D2"));
                DirPath = Path.Combine(rootPath, "picture",filePath);
                string number = "";
                if (Equ != null && Equ.EquNumber != null)
                    number = Equ.EquNumber;

                fileName = string.Format("{1}_{0:yyMMddHHmmss}_{2}_{3}.jpg", this.Maintime,number,this.ChannalNO,this.Presetting_No);
                fullPath = Path.Combine(DirPath,fileName);
                System.IO.Directory.CreateDirectory(DirPath);
                return fullPath;
            }
            catch
            {
                throw new Exception("路径不合法");
            }
        }
    
        /// <summary>
        /// 将图片保存到数据库中
        /// </summary>
        /// <param name="rootPath"></param>
        public void SaveToFile(string rootPath)
        {
            this.PicPath = this.GetSavedPath(rootPath);
            this.Img.SaveToFile(this.PicPath);
        }

    }
    //    /// <summary>
    ///// 包状态保存
    ///// </summary>
    //public class PackMsg
    //{
    //    /// <summary>
    //    /// 包在list中的位置
    //    /// </summary>
    //    public int PacPosition { get; set; }

    //    public int PacNo { get; set; }

    //    public PackMsg()
    //    {
    //        PacPosition = 0;
    //    }
    //    public PackMsg(int pacLen, int pacPosi)
    //    {
    //        PacPosition = pacPosi;
    //    }

    //    public PackMsg(int pacLen, int pacPosi, int PacNo)
    //    {
    //        this.PacNo = PacNo;
    //        this.PacPosition = pacPosi;
    //    }
    //}

    /// <summary>
    /// 数据包内容
    /// </summary>
    public class ImgPac
    {
        /// <summary>
        /// 数据包包号
        /// </summary>
        public int PNO { get; set; }

        /// <summary>
        /// 数据包内容
        /// </summary>
        public byte[] Data { get; set; }

        public ImgPac(int pno, byte[] data)
        {
            this.PNO = pno;
            this.Data = data;
        }
    }

    /// <summary>
    /// 图像数据保存
    /// </summary>
    public class ImgMsg
    {
        #region Public Variable

        /// <summary>
        /// 图片状态缓存
        /// </summary>
        //public PackMsg[] Paket { get; set; }
        ///// <summary>
        ///// 图片缓存
        ///// </summary>
        //public List<byte[]> PackList { get; set; }

        public int PNum { get; set; }

        private List<ImgPac> list_pac { get; set; }



        #endregion

        #region Construction
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="name"></param>
        /// <param name="PackNum"></param>
        public ImgMsg(string name, int PackNum)
        {
          
            this.PNum = PackNum;
            this.list_pac = new List<ImgPac>();
            //Paket = new PackMsg[PackNum];
            //PackList = new List<byte[]>();
        }
        #endregion

        #region Public Functions
        /// <summary>
        /// 添加图片数据
        /// </summary>
        /// <param name="SubNO">子包包号</param>
        /// <param name="buff">图片内容</param>
        public void AddSubPac(int SubNO, byte[] buff)
        {
            this.list_pac.Add(new ImgPac(SubNO, buff));
            //PackList.Add(buff);
            //PackMsg pm = new PackMsg(buff.Length, PackList.Count - 1,SubNO);
            //Paket[SubNO - 1] = pm;
        }
        /// <summary>
        /// 检查缺报数目
        /// </summary>
        /// <returns></returns>
        public int[] GetStilNo()
        {
            List<int> list_stil = new List<int>();
            this.list_pac.Sort((a, b) => { if (a.PNO > b.PNO) return 1; if (a.PNO < b.PNO) return -1; return 0; });

            for(int i = 0; i < list_pac.Count-1; i++)
            {
                if (list_pac[i].PNO == list_pac[i + 1].PNO)
                {
                    list_pac.RemoveAt(i + 1);
                    i--;
                }
            }
            int pno = 1;
            foreach (ImgPac pac in list_pac)
            {
                while (pno < pac.PNO)
                {
                    list_stil.Add(pno);
                    pno++;
                }
                pno++;    
            }
            while(pno <= this.PNum)
            {
                list_stil.Add(pno);
                pno++;
            }
            return list_stil.ToArray();
            //int StilRecCount = 0;
            //int[] NO_To_Rec = new int[Paket.Length];
            //for (int i = 0; i < Paket.Length; i++)
            //{
            //    if (Paket[i] == null)
            //    {
            //        NO_To_Rec[StilRecCount] = i + 1;
            //        StilRecCount++;
            //    }
            //}
            //if (StilRecCount == 0) return null;
            //int[] stilPacs = new int[StilRecCount];
            //for (int i = 0; i < StilRecCount; i++)
            //    stilPacs[i] = NO_To_Rec[i];
            //return stilPacs;
        }
        /// <summary>
        /// 图片保存到文件
        /// </summary>
        /// <param name="path"></param>
        public void SaveToFile(string path)
        {
            FileStream fs=null;
            try
            {
                //var packetpath = Path.GetDirectoryName(path);
                //var fileName = Path.GetFileNameWithoutExtension(path) + ".txt";
                //var fullPacketPath = Path.Combine(packetpath,fileName);
                //var arrayfs = new FileStream(fullPacketPath,FileMode.Create);

                fs = new FileStream(path, FileMode.Create);
                foreach(ImgPac pac in list_pac)
                {
                    fs.Write(pac.Data, 0, pac.Data.Length);
                }
                //for (int i = 0; i < Paket.Length; i++)
                //{
                //    byte[] bytesToWrite = PackList[Paket[i].PacPosition];
                //    fs.Write(bytesToWrite, 0, bytesToWrite.Length);
                //    var str = string.Format("序号：{0}  子包包号：{1}  数据内容：", i, Paket[i].PacNo);
                //    str += Tools.StringTurn.ByteToHexString(bytesToWrite);
                //    str += "\r\n";
                //     byte[] buffer = Encoding.UTF8.GetBytes(str);
                //    arrayfs.Write(buffer,0,buffer.Length);
                //}
                fs.Close();
                //arrayfs.Close() ;

            }
            catch (Exception ex)
            {
                if (fs != null)
                    try { fs.Close(); }
                    catch { }
                throw ex;
            }            
        }
        #endregion

    }
}
