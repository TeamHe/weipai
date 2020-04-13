using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    public class ComboBoxItem
    {
        private string _text = null;
        private object _value = null;
        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }
        public object Value
        {
            get { return this._value; }
            set { this._value = value; }
        }
        public override string ToString()
        {
            return this._text;
        }
        #region Construction
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComboBoxItem()
        { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        public ComboBoxItem(string text)
        {
            this._text = text;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text">需要显示的字符</param>
        /// <param name="value">需要保存的值</param>
        public ComboBoxItem(string text,object value)
        {
            this._text = text;
            this._value = value;
        }
        #endregion
        
    }
}
