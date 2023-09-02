using System;
using System.Collections.Generic;
using ResModel.EQU;
using DB_Operation.EQUManage;
using ResModel;
using ResModel.PowerPole;
using DB_Operation;
using cma.service.PowerPole;

namespace GridBackGround.CommandDeal
{

    /// <summary>
    /// 上传照片
    /// </summary>
    public class Image_Photo_UP
    {
        private string CMD_ID;
        /// <summary>
        /// 获取图片handle
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="chno"></param>
        /// <param name="preno"></param>
        /// <returns></returns>
        private Picture GetPicture(IPowerPole pole,int chno,int preno)
        {
            if (pole == null) return null;
            if (pole.UserData == null)
                pole.UserData = new List<Picture>();
            Picture picture = null;
            lock (pole.Lock)
            {
                List<Picture> pics = pole.UserData as List<Picture>;
                foreach (Picture pic in pics)
                {
                    if (pic.ChannalNO == chno && pic.Presetting_No == preno)
                    {
                        picture = pic;
                        break;
                    }
                }
            }
            return picture;
        }

        private Picture NewPicture(IPowerPole pole,int chno,int preno,int pacnum)
        {
            Picture picture = new Picture(chno, preno);
            picture.Equ = pole.Equ;
            picture.CMD_ID = pole.CMD_ID;
            picture.Img = new ImgMsg(pole.CMD_ID, pacnum);
            lock (pole.Lock)
            {
                List<Picture> list = pole.UserData as List<Picture>;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].ChannalNO == chno && list[i].Presetting_No == preno)
                    {
                        list[i] = picture;
                        return picture;
                    }
                }
                list.Add(picture);
            }
            return picture;
        }
        private void RemovePicture(IPowerPole pole,Picture pic)
        {
            lock (pole.Lock)
            {
                List<Picture> pics = pole.UserData as List<Picture>;
                pics.Remove(pic);
            }
        }

        #region 报文管理
        /// <summary>
        /// 请求上送照片
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_No"></param>
        /// <param name="data"></param>
        public  void Ask_To_Up(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string pacMsg = "";
            try
            {
                CMD_ID = pole.CMD_ID;
                if (data.Length != 4)
                    return;
                //报文解析
                int Channel_No = data[0];
                pacMsg += "通道号：" + Channel_No.ToString() + " ";

                int Presetting_No = data[1];
                pacMsg += "预置位号：" + Presetting_No.ToString() + " ";

                int PacLength = data[2] * 256 + data[3];
                pacMsg += "总包数：" + PacLength.ToString();

                Picture pic = GetPicture(pole, Channel_No, Presetting_No);
                if(pic != null && pic.UploadingState)
                { //判断上传依据:   1,有缓存图片;2,上传标识为真
                    pacMsg += "正在上传数据，暂不响应！";
                }
                else
                {   //新的图片_1,创建图片缓存;2, 发送响应包
                    try
                    {
                        NewPicture(pole,Channel_No,Presetting_No,PacLength);
                    }
                    catch (Exception ex)
                    {
                        pacMsg += "创建图片缓存失败:" + ex.Message;
                    }
                    //生成响应报文
                    var packet = PacketAnaLysis.BuildPacket.PackBuild(
                                  CMD_ID,
                                  data.Length,
                                  PacketAnaLysis.TypeFrame.Image,
                                  PacketAnaLysis.PacketType_Image.Image_Data_Start,
                                  FrameNO.GetFrameNO(),
                                  data);

                    string errorMsg;        //请求上传响应
                    PackeDeal.SendData(CMD_ID, packet, out errorMsg);
                }
            }
            catch (Exception ex)
            {
                  pacMsg += "数据异常："+ex.Message;
            }
            DisPacket.NewRecord(
                   new PackageRecord(
                       PackageRecord_RSType.rec,
                       pole,
                       "装置请求上送照片",
                       pacMsg));
        }

        /// <summary>
        /// 图像数据报
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_No"></param>
        /// <param name="data"></param>
        public  void Image_Data(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            CMD_ID = pole.CMD_ID;
            string pacMsg = "";
            //报文解析
            int Channel_No = data[0];
            pacMsg += "通道号：" + Channel_No.ToString() + " ";

            int Presetting_No = data[1];
            pacMsg += "预置位号：" + Presetting_No.ToString() + " ";

            int PacLength = BitConverter.ToUInt16(data, 2);
            pacMsg += "总包数：" + PacLength.ToString();

            int Subpacket_No = BitConverter.ToUInt16(data, 4);
            pacMsg += "子包包号：" + Subpacket_No.ToString();
          
            //数据长度
            try 
            {
                Picture pic = GetPicture(pole,Channel_No,Presetting_No);
                if(pic == null)
                {   //当前设备没有图片缓存
                    pacMsg += " 添加缓存失败,没有图片缓存";
                }
                else
                {//保存图片缓存
                    int PhotoLength = data.Length - 6;
                    byte[] packet = new byte[PhotoLength];
                    Buffer.BlockCopy(data, 6, packet, 0, PhotoLength);
                    lock (pic.Lock)
                    {
                        pic.AddPicData(Subpacket_No, packet);
                    }
                }              
            }
            catch (Exception ex)
            {
                pacMsg += "添加数据失败："+ ex.Message;
                //触发图片解析事件
                DisPacket.NewRecord(
                      new PackageRecord(
                          PackageRecord_RSType.rec,
                          pole,
                          "远程图像数据",
                          pacMsg));
            }
            //PacketAnaLysis.DisPacket.NewRecord(
            //        new PacketAnaLysis.DataInfo(
            //            PacketAnaLysis.DataRecSendState.rec,
            //            pole,
            //            "远程图像数据",
            //            pacMsg));

        }

        /// <summary>
        /// 图像数据报结束包
        /// </summary>
        /// <param name="cmd_ID"></param>
        /// <param name="frame_No"></param>
        /// <param name="data"></param>
        public  void Image_Data_End(IPowerPole pole,
            byte frame_No,
            byte[] data)
        {
            string pacMsg = "";
            if (data.Length != 6)
                return;
            //报文解析
            int channal  = data[0];
            pacMsg += "通道号：" + channal.ToString() + " ";

            int preset = data[1];
            pacMsg += "预置位号：" + preset.ToString() + " ";
            //采集时间
            DateTime time = Tools.TimeUtil.BytesToDate(data, 2);
            pacMsg += "采集时间：" + time.ToString("yyyy-MM-dd HH:mm:ss");

            Picture pic = GetPicture(pole,channal,preset);
            if(pic == null || pic.UploadingState == true)
            {   //没有图片缓存,或正在上传图片不处理
                pacMsg += " 没有图片缓存，或正在上传图片，不响应";
                DisPacket.NewRecord(
                   new PackageRecord(
                       PackageRecord_RSType.rec,
                       pole,
                       "远程图像数据上送结束标记",
                       pacMsg));
                return;
            }
            pic.Maintime = time;

            DisPacket.NewRecord(
              new PackageRecord(
                  PackageRecord_RSType.rec,
                  pole,
                  "远程图像数据上送结束标记",
                  pacMsg));

            //查找待上传数据包列表
            int[] stil = null;
            lock (pic.Lock)
            {
                stil = pic.GetStilPac().ToArray();
                if (stil == null || stil.Length == 0)
                    pic.UploadState = true;
            }

            //发送数据包结束包
            SendStilPac(pole, pic.ChannalNO, pic.Presetting_No, stil);
            if (stil != null && stil.Length > 0)
                return;

            //if (pole.UserData != null)
            //{
            //    
            //}
            //else
            //{
            //    SendStilPac(pole,channal,preset,null);
            //    PacketAnaLysis.DisPacket.NewRecord(
            //        new PacketAnaLysis.DataInfo(
            //            PacketAnaLysis.DataRecSendState.rec,
            //            pole,
            //            "远程图像数据上送结束标记",
            //            pacMsg));
            //    return;
            //}
            //结束包解析完成，显示

            //获取缺包数目：若缺包数目为0，则保存并上传图片
            //防止多个结束包同时上传
            //int m =GetStilPac(pole); 
            //if (m != 0) 
            //    return;
        
            try
            {
                SavePhoto(pole,pic);
                //PictuerOperation(pole);
            }
            catch (Exception ex)
            {
                DisPacket.NewRecord(
                new PackageRecord(
                    PackageRecord_RSType.rec,
                    pole,
                    "图片合成上传异常：",
                    ex.Message));
            }
            RemovePicture(pole, pic);
        }
        #endregion
        
        /// <summary>
        /// 图片合成、保存、上传
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="picture"></param>
        //private  void PictuerOperation(Termination.IPowerPole pole)
        //{
        //    //验证图片是否存在
        //    try
        //    {
        //        SavePhoto(pole);
        //    }
        //    catch (Exception ex)
        //    {
        //        PacketAnaLysis.DisPacket.NewRecord(
        //            new PacketAnaLysis.DataInfo(
        //            PacketAnaLysis.DataRecSendState.rec,
        //            pole,
        //            "图片合成上传异常1：",
        //            ex.Message));
        //    }
        //}

        /// <summary>
        /// 获取缺少的包数以及包号
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="Channel_No"></param>
        /// <param name="Presetting_No"></param>
        /// <returns></returns>
        //private  int GetStilPac(Termination.IPowerPole pole)
        //{
        //    Picture picture = (Picture)pole.UserData;
        //    if (picture == null) return 0;
        //    var stilPacket =  picture.GetStilPac();
        //    SendStilPac(pole, 
        //                picture.ChannalNO,
        //                picture.Presetting_No,
        //                stilPacket);
        //    if (stilPacket == null)
        //        return 0;
        //    return stilPacket.Length;
            
        //}

        public int SendStilPac(IPowerPole pole,int channal,int preset,int [] packets)
        {
            string pacMsg = "";
            int StilRecCount = 0;
            //int[] NO_To_Rec = null;
            byte[] Rec_Data = null;


            if (packets != null)
                StilRecCount = packets.Length;
            //生成报文

            Rec_Data = new byte[StilRecCount * 2 + 4];
            Rec_Data[0] = (byte)(channal & 0xff);
            pacMsg += "通道号：" + channal.ToString() + " ";

            Rec_Data[1] = (byte)(preset & 0xff);
            pacMsg += "预置位号：" + preset.ToString() + " ";

            Rec_Data[2] = (byte)(StilRecCount & 0xff);
            Rec_Data[3] = (byte)((StilRecCount & 0xff00) >> 8);
            pacMsg += "补包数目：" + StilRecCount.ToString() + ", ";

            //需要补报
            if (StilRecCount != 0)
            {
                pacMsg += "分别为：";
                for (int i = 0; i < StilRecCount; i++)
                {
                    Rec_Data[i * 2 + 4] = (byte)(packets[i] & 0xff);
                    Rec_Data[i * 2 + 5] = (byte)((packets[i] & 0xff00) >> 8);
                    pacMsg += packets[i].ToString() + ", ";
                }
            }

            try
            {
                var packet = PacketAnaLysis.BuildPacket.PackBuild(
                    pole.CMD_ID,
                    StilRecCount * 2 + 4,
                    PacketAnaLysis.TypeFrame.ControlImage,
                    PacketAnaLysis.PacketType_Image.Image_Data_Compen,
                    FrameNO.GetFrameNO(),
                    Rec_Data);
                string errorMsg;        //请求上传响应
                PackeDeal.SendData(pole.CMD_ID, packet, out errorMsg);
                
            }
            catch
            { 
            
            }
            //显示请求上传报文
            DisPacket.NewRecord(
                new PackageRecord(
                    PackageRecord_RSType.rec,
                    pole,
                    "补包数据下发",
                    pacMsg));
            

            return StilRecCount;
        }
        /// <summary>
        /// 图片保存
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="time"></param>
        /// <param name="Presetting_No"></param>
        public  void SavePhoto(IPowerPole pole,Picture picture)
        {
            string saveMsg = "";
            bool state = true;
          
            try 
            {
                string rootPath = Config.SettingsForm.Default.PicturePath;
                if (rootPath == null || rootPath.Length == 0)
                    rootPath = System.Environment.CurrentDirectory;
                picture.SaveToFile(rootPath);
                saveMsg += "图片合成成功";
                saveMsg += "file:///" + picture.PicPath;
            }
            catch(Exception ex) 
            {
                saveMsg += "图片合成失败:" + ex.Message;
                state = false;
            } 
            DisPacket.NewRecord(                         //输出保存信息
                    new PackageRecord(
                        PackageRecord_RSType.none,
                        pole,
                        "照片合成",
                        saveMsg));
            saveMsg = "";
            try
            {
                if (state)
                {
                    //给图片添加水印文字
                    if ((pole != null)&&(pole.Equ != null) && (pole.Equ.MarketText != null) && (pole.Equ.MarketText.Length != 0))
                        picture.AddWaterMarket(pole.Equ.MarketText,pole.Equ.IS_Mark,pole.Equ.Is_Time);
                    else
                        picture.AddWaterMarket("杆塔号:BF001");
                }

            }
            catch (Exception ex)
            {
                saveMsg += "图片添加水印文字失败:" + ex.Message;
                state = false;
            }
            if (pole.Equ == null) return;
            try
            {
                if (state)
                {
                    Equ equ = pole.Equ;
                    if(equ.UrlID != 0)
                    {
                        DB_Url db_url = new DB_Url();
                        var url = db_url.GetUrl(equ.UrlID);
                        picture.Upload(url.Url, equ.EquNumber);
                            saveMsg += "图片上传成功";
                        //if (picture.Upload(url.Url, equ.EquNumber))
                        //    saveMsg += "图片上传成功";
                        //else
                        //{
                        //    saveMsg += "图片上传失败";
                        //    picture.UploadState = false;
                        //}
                    }
                    else
                    {
                        saveMsg += "图片上传失败，未设置上传URL";
                        picture.UploadState = false;
                    }
                   
                }
            }
            catch (Exception ex)
            {
                saveMsg = "图片上传失败：" + ex.Message;
                picture.UploadState = false;
            }



            try
            {
                if (state)
                {
                    picture.SaveToDB();
                    //saveMsg += "图片保存成功";
                    RemovePicture(pole, picture);
                }
            }
            catch (Exception ex)
            {
                saveMsg += "图片保到数据库存失败:" + ex.Message;
                state = false;
            }
            //图片上传到服务器


            DisPacket.NewRecord(                         //输出保存信息
                    new PackageRecord(
                        PackageRecord_RSType.none,
                        pole,
                        "图片上传",
                        saveMsg));
        }
    }

    
}
