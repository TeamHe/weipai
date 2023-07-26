using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResModel.gw
{
    public interface IModelData
    {
        /// <summary>
        /// 模型数据名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 模型参数数值
        /// </summary>
        float Data { get; set; }

        /// <summary>
        /// 数值类型
        /// </summary>
        int DataType { get; set; }
    }
    public class gw_ModelData:IModelData
    {
        /// <summary>
        /// 模型数据名称
        /// </summary>
        public string Name { get;  set; }
        /// <summary>
        /// 模型参数数值
        /// </summary>
        public float Data { get;  set; }

        /// <summary>
        /// 数值类型
        /// </summary>
        public int DataType { get;  set; }
        public gw_ModelData(string name, float data, int dataType)
        {
            this.Name = name;
            this.Data = data;
            this.DataType = dataType;
        }
    }
}
