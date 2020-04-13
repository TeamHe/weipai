using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.CommandDeal
{
    public interface IModel_Data
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
    public class Model_Data:IModel_Data
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
        public Model_Data(string name, float data, int dataType)
        {
            this.Name = name;
            this.Data = data;
            this.DataType = dataType;
        }
    }
}
