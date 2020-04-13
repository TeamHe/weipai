using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResModel.EQU
{
    public class UrlInterFace
    {
        #region Public Variable
        /// <summary>
        /// URL ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// URL名称
        /// </summary>
        public string Nanme { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        public bool IsTime { get; set; }

        public bool IsName { get; set; }
        
        #endregion

        /// <summary>
        /// Construction
        /// </summary>
        public UrlInterFace()
        {

        }
        /// <summary>
        /// UrlInfo
        /// </summary>
        /// <param name="name">添加的文字内容</param>
        /// <param name="url">上传地址</param>
        /// <param name="istime">左上角是否添加时间</param>
        /// <param name="isname">右下角是否添加文字</param>
        public UrlInterFace(string name, string url,bool istime=true,bool isname=true)
        {
            this.Nanme = name;
            this.Url = url;
            this.IsName = isname;
            this.IsTime = istime;
        }

        /// <summary>
        /// URL info
        /// </summary>
        /// <param name="id">杆塔ID</param>
        /// <param name="name">添加的文字内容</param>
        /// <param name="url">上传地址</param>
        /// <param name="istime">左上角是否添加时间</param>
        /// <param name="isname">右下角是否添加文字</param>
        public UrlInterFace(int id, string name, string url,bool istime=true,bool isname=true)

        {
            this.ID = id;

            this.Nanme = name;
            this.Url = url;
            this.IsName = isname;
            this.IsTime = istime;
        }
    }
}
