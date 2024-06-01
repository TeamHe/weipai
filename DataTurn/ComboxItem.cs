using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        public static void Init_items_enum(ComboBox comboBox,Type enumtype)
        {
            if (comboBox == null || enumtype ==null)
                throw new ArgumentNullException("item");
            comboBox.Items.Clear();

            Dictionary<Int32, String> dic = EnumUtil.EnumToDictionary(enumtype, e => e.GetDescription());
            foreach (KeyValuePair<Int32, String> item in dic)
            {
                ComboBoxItem cbxitem = new ComboBoxItem(item.Value, item.Key);//将装置类型绑定到combox中
                comboBox.Items.Add(cbxitem);
            }
            comboBox.SelectedIndex = 0;
        }

        public static void Set_Value(ComboBox comboBox, int value)
        {
            if (comboBox == null )
                throw new ArgumentNullException("item");
         
            foreach(ComboBoxItem cbxitem in comboBox.Items)
            {
                if((int)(cbxitem.Value) == value)
                    comboBox.SelectedItem = cbxitem;
            }
        }



    }
}
