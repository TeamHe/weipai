using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ResModel.EQU;
using SQLUtils;

namespace DB_Operation.EQUManage
{
    public class DB_Url
    {
        private ISQLUtils Connectin = DB.Connection;
        private string tableName = "t_upload";

        private string[] cloums = {"name","url","is_time","is_name"};
        private string[] names = { "名称", "URL","时间标识","名称标识" };

        #region 增加记录
        /// <summary>
        /// 增加URL接口
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        //public UrlInterFace Add(string name, string url)
        //{
        //    //
        //    string sql = string.Format(@"Insert into {0}" +
        //                "({1},{2})Values(\"{3}\",\"{4}\")",
        //                tableName, cloums[0], cloums[1],
        //                name, url);

        //    Connectin.ExecuteNoneQuery(sql);
        //    return GetUrl(name);
            
        //}

        public UrlInterFace Add(string name, string url, bool istime = true, bool isname = true)
        {
            //
            string sql = string.Format(@"Insert into {0}" +
                        "({1},{2},{5},{6})Values(\"{3}\",\"{4}\",{7},{8})",
                        tableName, cloums[0], cloums[1],
                        name, url,
                        cloums[2],cloums[3], Convert.ToInt16(istime), Convert.ToInt16(isname));

            Connectin.ExecuteNoneQuery(sql);
            return GetUrl(name);
        }
        #endregion

        #region 删除记录
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="name">接口名称</param>
        public void Delete(string name)
        {
            string sql = string.Format(
                @"delete from {0} where name = {1}",
                tableName,name);
            Connectin.ExecuteNoneQuery(sql);
        }
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="id">接口ID</param>
        public void Delete(int id)
        { 
            string sql = string.Format(
                @"delete from {0} where id = {1}",
                tableName, id);
            Connectin.ExecuteNoneQuery(sql);
        }
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="url"></param>
        public void Delete(UrlInterFace url)
        {
            Delete(url.ID);
        }
        #endregion

        #region 修改记录        
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        //public void Update(int id, string name, string url)
        //{
        //    string sql = string.Format(
        //        "update {0} set name = \"{1}\", url = \"{2}\" " +
        //        "where id={3}",
        //        tableName, name, url, id);
        //    Connectin.ExecuteNoneQuery(sql);
        //}  
       
        public void Update(int id, string name, string url,bool istime=true,bool isname=true)
        {
            string sql = string.Format(
                "update {0} set name = \"{1}\", url = \"{2}\", is_time={4}, is_name={5} " +
                "where id={3}",
                tableName, name, url, id,Convert.ToInt16(istime),Convert.ToInt16(isname));
            Connectin.ExecuteNoneQuery(sql);
        }
        /// <summary>
        /// 更新接口
        /// </summary>
        /// <param name="url"></param>
        public void Update(UrlInterFace url)
        {
            Update(url.ID, url.Nanme, url.Url);
        }
        #endregion

        #region 查找记录
        /// <summary>
        /// 根据行记录获得值
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected UrlInterFace GetUrl(DataRow row)
        {
            if (row == null)
                return null;
            try
            {
                int id = 0;
                string name;
                string url;
                bool istime = true;
                bool isname = true;

                object[] clos = row.ItemArray;
                if (clos.Length < 3)
                    return null;
                id = (int)clos[0];
                name = clos[1].ToString();
                url = clos[2].ToString();
                if (clos.Length > 3 && !(clos[3] is System.DBNull))
                    istime = Convert.ToBoolean(clos[3]);
                if (clos.Length > 4 && !(clos[4] is System.DBNull))
                    isname = Convert.ToBoolean(clos[4]);

                return new UrlInterFace(id, name, url, istime, isname);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        protected IList<UrlInterFace> GetList(string sql)
        {
            if (sql == null || sql.Length == 0)
                return null;

            IList<UrlInterFace> list = new List<UrlInterFace>();
            try
            {
                DataTable dt =  Connectin.GetTable(sql);
                if (dt == null)
                    return null;
                foreach (DataRow row in dt.Rows)
                {
                    UrlInterFace interFace = GetUrl(row);
                    if (interFace != null)
                        list.Add(GetUrl(row));
                }
                return list;
            }
            catch
            {
                return null;
            }
         
        }

        

        /// <summary>
        /// 获取记录列表
        /// </summary>
        /// <returns>返回值</returns>
        public IList<UrlInterFace> GetUrlList()
        {
            string sql = string.Format(
                @"select * "
                + "from {0} ", tableName);
            return GetList(sql);
        }
        /// <summary>
        /// 根据名称获取接口
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UrlInterFace GetUrl(string name)
        { 
            string sql = string.Format(
            @"select *"
                //+"name as 名称, "
                //+"url as URL "
               + "from {0} where name = \"{1}\"",tableName,name);
            IList<UrlInterFace> interFaces = GetList(sql);
            if (interFaces != null && interFaces.Count > 0)
                return interFaces[0];

            return null;
        }

        public UrlInterFace GetUrl(int id)
        {
            string sql = string.Format(
            @"select * "
                //+ "name as 名称, "
                //+ "url as URL " +
                + "from {0} where id = {1}", tableName, id);
            IList<UrlInterFace> interFaces = GetList(sql);
            if (interFaces != null && interFaces.Count > 0)
                return interFaces[0];
            return null;
        }
        #endregion
    }

    
}
