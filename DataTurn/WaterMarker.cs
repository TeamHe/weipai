﻿using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Tools
{
    using d = System.Drawing;

    /// <summary> 
    /// 水印的类型 
    /// </summary> 
    public enum WaterMarkType
    {
        /// <summary> 
        /// 文字水印 
        /// </summary> 
        TextMark,
        /// <summary> 
        /// 图片水印 
        /// </summary> 
        //ImageMark // 暂时只能添加文字水印 
    };
    /// <summary> 
    /// 水印的位置 
    /// </summary> 
    public enum WaterMarkPosition
    {
        /// <summary> 
        /// 左上角 
        /// </summary> 
        WMP_Left_Top,
        /// <summary> 
        /// 左下角 
        /// </summary> 
        WMP_Left_Bottom,
        /// <summary> 
        /// 右上角 
        /// </summary> 
        WMP_Right_Top,
        /// <summary> 
        /// 右下角 
        /// </summary> 
        WMP_Right_Bottom
    };

    public class WaterMarkText
    {
        /// <summary>
        /// 要设置的文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public WaterMarkPosition Position { get; set; }

        #region Construction
        /// <summary>
        /// Construction
        /// </summary>
        public WaterMarkText()
        {

        }
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="text">要设置的文字</param>
        /// <param name="postion">位置</param>
        public WaterMarkText(string text, WaterMarkPosition postion)
        {
            this.Text = text;
            this.Position = postion;
        }
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="text">要设置的文字内容</param>
        public WaterMarkText(string text)
        {
            this.Text = text;
            this.Position = WaterMarkPosition.WMP_Right_Bottom;
        }
        #endregion


    }
    /// <summary> 
    /// 处理图片的类（包括加水印，生成缩略图） 
    /// </summary> 
    public class ImageWaterMark
    {
        public ImageWaterMark()
        {
            // 
            // TODO: 在此处添加构造函数逻辑 
            // 
        }
        #region 给图片加水印
        /// <summary>
        /// 添加水印文字
        /// </summary>
        /// <param name="oldpath">图片所在位置</param>
        /// <param name="newpath">图片保存位置</param>
        /// <param name="wmtType">水印类型</param>
        /// <param name="sWaterMarkContent">水印内容</param>
        /// <param name="positon">文字保存位置</param>
        public void addWaterMark(string oldpath,
                                 string newpath,
                                 WaterMarkType wmtType,
                                 string sWaterMarkContent,
                                 WaterMarkPosition positon)
        {
            try
            {
                d.Image image = d.Image.FromFile(oldpath);
                Bitmap b = new Bitmap(image.Width, image.Height,
                 PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.High;
                g.DrawImage(image, 0, 0, image.Width, image.Height);
                switch (wmtType)
                {
                    case WaterMarkType.TextMark:
                        //文字水印 
                        this.addWatermarkText(g, sWaterMarkContent, positon,
                         image.Width, image.Height);
                        break;
                }
                image.Dispose();
                string dirPath = Path.GetDirectoryName(newpath);
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                if (File.Exists(newpath))
                    File.Delete(newpath);
                b.Save(newpath);
                b.Dispose();
                image.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
                //if (File.Exists(oldpath))
                //{
                //    File.Delete(oldpath);
                //}
            }
            finally
            {
                //if (File.Exists(oldpath))
                //{
                //    File.Delete(oldpath);
                //}
            }
        }

        /// <summary>
        /// 添加水印文字
        /// </summary>
        /// <param name="oldpath">图片所在位置</param>
        /// <param name="newpath">图片保存位置</param>
        /// <param name="wmtType">水印类型</param>
        /// <param name="sWaterMarkContent">水印内容</param>
        /// <param name="positon">文字保存位置</param>
        public void addWaterMark(string oldpath,
                                 string newpath,
                                 WaterMarkType wmtType,
                                 List<WaterMarkText> texts)
        {
            try
            {
                d.Image image = d.Image.FromFile(oldpath);
                Bitmap b = new Bitmap(image.Width, image.Height,
                 PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.High;
                g.DrawImage(image, 0, 0, image.Width, image.Height);
                //添加水印文字
                switch (wmtType)
                {
                    case WaterMarkType.TextMark:
                        //文字水印 
                        foreach (WaterMarkText wmt in texts)
                        {
                            this.addWatermarkText(g, wmt.Text, wmt.Position,
                             image.Width, image.Height);
                        }
                        break;
                }
                image.Dispose();
                //保存新图片
                //if (File.Exists(newpath))
                //    File.Delete(newpath);
                b.Save(newpath,ImageFormat.Jpeg);
                //销毁资源
                b.Dispose();
            }
            catch (Exception ex)
            {
                if(oldpath != newpath)
                if (File.Exists(oldpath))
                {
                    File.Delete(oldpath);
                }
                throw ex;

            }
            finally
            {
                //删除旧图片
                //if (oldpath != newpath)
                //if (File.Exists(oldpath))
                //{
                //    File.Delete(oldpath);
                //}
            }
        }

        /// <summary> 
        /// 添加水印(分图片水印与文字水印两种) 
        /// </summary> 
        /// <param name="oldpath">原图片绝对地址</param> 
        /// <param name="newpath">新图片放置的绝对地址</param> 
        /// <param name="wmtType">要添加的水印的类型</param> 
        /// <param name="sWaterMarkContent">水印内容，若添加文字水印，此即为要添加的文字； 
        /// 若要添加图片水印，此为图片的路径</param> 
        public void addWaterMark(string oldpath, string newpath,
         WaterMarkType wmtType, string sWaterMarkContent)
        {

        }
        /// <summary> 
        ///　　 加水印文字 
        /// </summary> 
        /// <param name="picture">imge 对象</param> 
        /// <param name="_watermarkText">水印文字内容</param> 
        /// <param name="_watermarkPosition">水印位置</param> 
        /// <param name="_width">被加水印图片的宽</param> 
        /// <param name="_height">被加水印图片的高</param> 
        private void addWatermarkText(Graphics picture, string _watermarkText,
         WaterMarkPosition _watermarkPosition, int _width, int _height)
        {
            // 确定水印文字的字体大小 
            int[] sizes = new int[] {32, 30, 28, 26, 24, 22, 20, 18, 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0; i < sizes.Length; i++)
            {
                crFont = new Font("Arial Black", sizes[i], FontStyle.Bold);
                crSize = picture.MeasureString(_watermarkText, crFont);
                if ((ushort)crSize.Width < (ushort)_width)
                {
                    break;
                }
            }
            // 生成水印图片（将文字写到图片中） 
            Bitmap floatBmp = new Bitmap((int)crSize.Width + 3,
                  (int)crSize.Height + 3, PixelFormat.Format32bppArgb);

            Graphics fg = Graphics.FromImage(floatBmp);
            //填充背景色
            fg.FillRectangle(Brushes.Black, new Rectangle(0, 0, (int)crSize.Width+3, (int)crSize.Height + 3));   
            PointF pt = new PointF(0, 0);
            // 画阴影文字 
            //Brush TransparentBrush0 = new SolidBrush(Color.FromArgb(255, Color.Black));
            //Brush TransparentBrush1 = new SolidBrush(Color.FromArgb(255, Color.Black));
            //fg.DrawString(_watermarkText, crFont, TransparentBrush0, pt.X, pt.Y + 1);
            //fg.DrawString(_watermarkText, crFont, TransparentBrush0, pt.X + 1, pt.Y);
            //fg.DrawString(_watermarkText, crFont, TransparentBrush1, pt.X + 1, pt.Y + 1);
            //fg.DrawString(_watermarkText, crFont, TransparentBrush1, pt.X, pt.Y + 2);
            //fg.DrawString(_watermarkText, crFont, TransparentBrush1, pt.X + 2, pt.Y);
            //TransparentBrush0.Dispose();
            //TransparentBrush1.Dispose();
            // 画文字 
            fg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fg.DrawString(_watermarkText,
                        crFont, 
                        new SolidBrush(Color.White),
                        pt.X, pt.Y, StringFormat.GenericDefault);
                        // 保存刚才的操作 
            fg.Save();
            fg.Dispose();
            // floatBmp.Save("d:\\WebSite\\DIGITALKM\\ttt.jpg"); 
            // 将水印图片加到原图中 
            this.addWatermarkImage(
                                    picture,
                                    new Bitmap(floatBmp),
                                    _watermarkPosition,
                                    _width,
                                    _height);
        }
        /// <summary> 
        ///　　 加水印图片 
        /// </summary> 
        /// <param name="picture">imge 对象</param> 
        /// <param name="iTheImage">Image对象（以此图片为水印）</param> 
        /// <param name="_watermarkPosition">水印位置</param> 
        /// <param name="_width">被加水印图片的宽</param> 
        /// <param name="_height">被加水印图片的高</param> 
        private void addWatermarkImage(Graphics picture, d.Image iTheImage,
         WaterMarkPosition _watermarkPosition, int _width, int _height)
        {
            d.Image watermark = new Bitmap(iTheImage);
            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            //设置叠加图片的清晰度
            //float[][] colorMatrixElements = { 
            // new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f}, 
            // new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f}, 
            // new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f}, 
            // new float[] {0.0f, 0.0f, 0.0f, alpha/255f, 0.0f},    //alpha 要设置的清晰度的值 0-255 
            // new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f} 
            //};
            float[][] colorMatrixElements = { 
　　　　　　　　　　　　 new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f}, 
　　　　　　　　　　　　 new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f}, 
　　　　　　　　　　　　 new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f}, 
　　　　　　　　　　　　 new float[] {0.0f, 0.0f, 0.0f, 0.75f, 0.0f}, 
　　　　　　　　　　　　 new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f} 
　　　　　　　　　　　 };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            int xpos = 0;
            int ypos = 0;
            int WatermarkWidth = 0;
            int WatermarkHeight = 0;
            double bl = 1d;
            //计算水印图片的比率 
            //取背景的1/4宽度来比较 
            //if ((_width > watermark.Width * 4) && (_height > watermark.Height * 4))
            //{
            //    bl = 1;
            //}
            //else if ((_width > watermark.Width * 4) && (_height < watermark.Height * 4))
            //{
            //    bl = Convert.ToDouble(_height / 4) / Convert.ToDouble(watermark.Height);
            //}
            //else if ((_width < watermark.Width * 4) && (_height > watermark.Height * 4))
            //{
            //    bl = Convert.ToDouble(_width / 4) / Convert.ToDouble(watermark.Width);
            //}
            //else
            //{
            //    if ((_width * watermark.Height) > (_height * watermark.Width))
            //    {
            //        bl = Convert.ToDouble(_height / 4) / Convert.ToDouble(watermark.Height);
            //    }
            //    else
            //    {
            //        bl = Convert.ToDouble(_width / 4) / Convert.ToDouble(watermark.Width);
            //    }
            //}
            bl = Convert.ToDouble(_height / 16) / Convert.ToDouble(watermark.Height);
           

            //取得叠加图片所处的位置
            WatermarkWidth = Convert.ToInt32(watermark.Width * bl);
            WatermarkHeight = Convert.ToInt32(watermark.Height * bl);
            switch (_watermarkPosition)
            {
                case WaterMarkPosition.WMP_Left_Top:
                    xpos = 10;
                    ypos = 10;
                    break;
                case WaterMarkPosition.WMP_Right_Top:
                    xpos = _width - WatermarkWidth - 10;
                    ypos = 10;
                    break;
                case WaterMarkPosition.WMP_Right_Bottom:
                    xpos = _width - WatermarkWidth - 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;
                case WaterMarkPosition.WMP_Left_Bottom:
                    xpos = 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;
            }
            //图片叠加
            picture.DrawImage(
                                 watermark,
                                 new Rectangle(xpos, ypos, WatermarkWidth, WatermarkHeight),
                                 0,
                                 0,
                                 watermark.Width,
                                 watermark.Height,
                                 GraphicsUnit.Pixel,
                                 imageAttributes);
            watermark.Dispose();
            imageAttributes.Dispose();
        }
        /// <summary> 
        ///　　 加水印图片 
        /// </summary> 
        /// <param name="picture">imge 对象</param> 
        /// <param name="WaterMarkPicPath">水印图片的地址</param> 
        /// <param name="_watermarkPosition">水印位置</param> 
        /// <param name="_width">被加水印图片的宽</param> 
        /// <param name="_height">被加水印图片的高</param> 
        private void addWatermarkImage(Graphics picture, string WaterMarkPicPath,
         WaterMarkPosition _watermarkPosition, int _width, int _height)
        {
            d.Image watermark = new Bitmap(WaterMarkPicPath);
            this.addWatermarkImage(picture, watermark, _watermarkPosition, _width,
             _height);
        }
        #endregion
        #region 生成缩略图
        /// <summary> 
        /// 保存图片 
        /// </summary> 
        /// <param name="image">Image 对象</param> 
        /// <param name="savePath">保存路径</param> 
        /// <param name="ici">指定格式的编解码参数</param> 
        private void SaveImage(d.Image image, string savePath, ImageCodecInfo ici)
        {
            //设置 原图片 对象的 EncoderParameters 对象 
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(
             System.Drawing.Imaging.Encoder.Quality, ((long)90));
            image.Save(savePath, ici, parameters);
            parameters.Dispose();
        }
        /// <summary> 
        /// 获取图像编码解码器的所有相关信息 
        /// </summary> 
        /// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param> 
        /// <returns>返回图像编码解码器的所有相关信息</returns> 
        private ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType)
                    return ici;
            }
            return null;
        }
        /// <summary> 
        /// 生成缩略图 
        /// </summary> 
        /// <param name="sourceImagePath">原图片路径(相对路径)</param> 
        /// <param name="thumbnailImagePath">生成的缩略图路径,如果为空则保存为原图片路径(相对路径)</param> 
        /// <param name="thumbnailImageWidth">缩略图的宽度（高度与按源图片比例自动生成）</param> 
        public void ToThumbnailImages(
         string SourceImagePath,
         string ThumbnailImagePath,
         int ThumbnailImageWidth)
        {
            Hashtable htmimes = new Hashtable();
            htmimes[".jpeg"] = "image/jpeg";
            htmimes[".jpg"] = "image/jpeg";
            htmimes[".png"] = "image/png";
            htmimes[".tif"] = "image/tiff";
            htmimes[".tiff"] = "image/tiff";
            htmimes[".bmp"] = "image/bmp";
            htmimes[".gif"] = "image/gif";
            // 取得原图片的后缀 
            string sExt = SourceImagePath.Substring(
             SourceImagePath.LastIndexOf(".")).ToLower();
            //从 原图片创建 Image 对象 
            d.Image image = d.Image.FromFile(SourceImagePath);
            int num = ((ThumbnailImageWidth / 4) * 3);
            int width = image.Width;
            int height = image.Height;
            //计算图片的比例 
            if ((((double)width) / ((double)height)) >= 1.3333333333333333f)
            {
                num = ((height * ThumbnailImageWidth) / width);
            }
            else
            {
                ThumbnailImageWidth = ((width * num) / height);
            }
            if ((ThumbnailImageWidth < 1) || (num < 1))
            {
                return;
            }
            //用指定的大小和格式初始化 Bitmap 类的新实例 
            Bitmap bitmap = new Bitmap(ThumbnailImageWidth, num,
             PixelFormat.Format32bppArgb);
            //从指定的 Image 对象创建新 Graphics 对象 
            Graphics graphics = Graphics.FromImage(bitmap);
            //清除整个绘图面并以透明背景色填充 
            graphics.Clear(Color.Transparent);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.High;
            //在指定位置并且按指定大小绘制 原图片 对象 
            graphics.DrawImage(image, new Rectangle(0, 0, ThumbnailImageWidth, num));
            image.Dispose();
            try
            {
                //将此 原图片 以指定格式并用指定的编解码参数保存到指定文件 
                SaveImage(bitmap, ThumbnailImagePath,
                 GetCodecInfo((string)htmimes[sExt]));
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
        #endregion
    } 
}
