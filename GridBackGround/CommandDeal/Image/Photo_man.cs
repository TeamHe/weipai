using DB_Operation.EQUManage;
using GridBackGround.PacketAnaLysis;
using ResModel;
using ResModel.CollectData;
using ResModel.EQU;
using System;
using System.Collections.Generic;
using ResModel.PowerPole;

namespace GridBackGround.CommandDeal.Image
{
    public class Photo_man
    {
        public IPowerPole pole { get; set; }
        
        public List<Picture> Pictures
        {
            get { return this.pole.UserData as List<Picture>; }
            set { this.pole.UserData = value; }
        }

        /// <summary>
        /// 通道号
        /// </summary>
        public int Channel_NO { get; set; }

        /// <summary>
        /// 预置位号
        /// </summary>
        public int Preset_NO { get; set; }

        public Photo_man(IPowerPole pole, int ch_no,int preset_no) 
        {
            if (pole == null)
                throw new ArgumentNullException();
            this.pole = pole;
            this.Channel_NO = ch_no;
            this.Preset_NO = preset_no;
        }

        /// <summary>
        /// 从当前设备待上传图片列表中获取正在上传图片信息
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="chno"></param>
        /// <param name="preno"></param>
        /// <returns></returns>
        public Picture GetPicture()
        {
            Picture picture = null;

            if(this.Pictures == null)
                this.Pictures = new List<Picture>();

            lock (pole.Lock)
            {
                foreach (Picture pic in this.Pictures)
                {
                    if (pic.ChannalNO == this.Channel_NO && pic.Presetting_No == this.Preset_NO)
                    {
                        picture = pic;
                        break;
                    }
                }
            }
            return picture;
        }

        private Picture NewPicture(int pacnum)
        {
            Picture picture = new Picture(this.Channel_NO, this.Preset_NO);
            picture.Equ = pole.Equ;
            picture.CMD_ID = pole.CMD_ID;
            picture.Img = new ImgMsg(pole.CMD_ID, pacnum);

            lock (pole.Lock)
            {
                List<Picture> list = this.Pictures;
                for (int i = 0; i < list.Count; i++)
                {
                    //如果当前通道号，预置位号有数据上传刷新缓存
                    if (list[i].ChannalNO == this.Channel_NO && 
                        list[i].Presetting_No == this.Preset_NO)
                    {
                        list[i] = picture;
                        return picture;
                    }
                }
                list.Add(picture);
            }
            return picture;
        }

        private void RemovePicture(Picture pic)
        {
            lock (pole.Lock)
            {
                List<Picture> pics = this.Pictures;
                pics.Remove(pic);
            }
        }

        /// <summary>
        /// 请求上传图片处理
        /// </summary>
        /// <param name="pnum"></param>
        /// <param name="msg"></param>
        /// <returns></returns>

        public Picture Picture_StartUp(int pnum, out string msg)
        {
            msg = string.Empty;
            Picture pic = this.GetPicture();
            if (pic != null && pic.UploadingState)
            { //判断上传依据:   1,有缓存图片;2,上传标识为真
                msg += "正在上传数据，暂不响应！";
                return null;
            }
            else
            {   //新的图片_1,创建图片缓存;2, 发送响应包
                try
                {
                    return NewPicture(pnum);
                }
                catch (Exception ex)
                {
                    msg += "创建图片缓存失败:" + ex.Message;
                    return null;
                }
            }
        }

        /// <summary>
        /// 接收到新的数据包
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="data"></param>
        /// <param name="pacMsg"></param>
        /// <returns></returns>
        public bool PictureData(int pno, byte[] data,out string pacMsg)
        {
            pacMsg = string.Empty;
            try
            {
                Picture pic = GetPicture();
                if (pic == null)
                {   //当前设备没有图片缓存
                    pacMsg += " 添加缓存失败,没有图片缓存";
                    return false;
                }
                else
                {//保存图片缓存
                    lock (pic.Lock)
                    {
                        pic.AddPicData(pno, data);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                pacMsg += "添加数据失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 图片上传结束包处理
        /// </summary>
        /// <returns>带补包数据包列表</returns>
        public List<int> Picture_End(DateTime photo_time, out string pacMsg) 
        {
            pacMsg = string.Empty;
            Picture pic = GetPicture();
            if (pic == null || pic.UploadingState == true)
            {   //没有图片缓存,或正在上传图片不处理
                pacMsg += " 没有图片缓存，或正在上传图片，不响应";
                return null;
            }
            if(photo_time > DateTime.MinValue)
                pic.Maintime = photo_time;

            //查找待上传数据包列表
            List<int> remains = null;
            lock (pic.Lock)
            {
                remains = pic.GetStilPac();
                if (remains == null || remains.Count == 0)
                    pic.UploadState = true;
            }
            return remains;
        }

        /// <summary>
        /// 将图片保存到文件中
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        private bool SaveToFile(Picture picture,out string msg)
        {
            msg = string.Empty;
            try
            {
                string rootPath = Config.SettingsForm.Default.PicturePath;
                if (rootPath == null || rootPath.Length == 0)
                    rootPath = System.Environment.CurrentDirectory;
                picture.SaveToFile(rootPath);
                msg += "图片合成成功";
                msg += "file:///" + picture.PicPath;
                return true;
            }
            catch (Exception ex)
            {
                msg += "图片合成失败:" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 给图片添加水印文字
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool AddWaterMarket(Picture picture, out string msg)
        {
            msg = string.Empty;
            try
            {
                if ((pole != null) && (pole.Equ != null) && (pole.Equ.MarketText != null) && (pole.Equ.MarketText.Length != 0))
                    picture.AddWaterMarket(pole.Equ.MarketText, pole.Equ.IS_Mark, pole.Equ.Is_Time);
                else
                    picture.AddWaterMarket("杆塔号:BF001");
                return true;

            }
            catch (Exception ex)
            {
                msg = "图片添加水印文字失败:" + ex.Message;
                return false;
            }

        }

        private void  PictureUploade(Picture picture, out string msg)
        {
            msg = string.Empty;
            try
            {
                Equ equ = pole.Equ;
                if (equ != null && equ.UrlID != 0)
                {
                    DB_Url db_url = new DB_Url();
                    var url = db_url.GetUrl(equ.UrlID);
                    picture.Upload(url.Url, equ.EquNumber);
                    msg += "图片上传成功";
                }
            }
            catch (Exception ex)
            {
                msg = "图片上传失败：" + ex.Message;
                picture.UploadState = false;
                return;
            }
        }

        /// <summary>
        /// 图片保存
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="time"></param>
        /// <param name="Presetting_No"></param>
        public void Picture_Save()
        {
            bool state = true;
            Picture picture = this.GetPicture();

            state = this.SaveToFile(picture, out string saveMsg);  //图像缓存保存到文件

            //输出保存信息
            if(saveMsg!=null && saveMsg != string.Empty)
            {
                DisPacket.NewRecord(new DataInfo(DataInfoState.send, this.pole, "照片合成", saveMsg));
            }

            saveMsg = string.Empty;
            //添加水银信息
            if (state && (state = AddWaterMarket(picture, out string msg_add_mask)))
            {
                PictureUploade(picture, out string msg_upload);
                saveMsg += msg_add_mask;
                saveMsg += msg_upload;
            }

            try
            {
                if (state)
                {
                    picture.SaveToDB();
                    //saveMsg += "图片保存成功";
                    RemovePicture(picture);
                }
            }
            catch (Exception ex)
            {
                saveMsg += "图片保到数据库存失败:" + ex.Message;
                state = false;
            }
            //图片上传到服务器
            if(saveMsg!=null && saveMsg.Length > 0) 
                DisPacket.NewRecord(new DataInfo(DataInfoState.send, this.pole, "图片存储", saveMsg));
            
        }
    }
}
