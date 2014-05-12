using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class CommonComboBoxItems
    {
        private object _value;
        private object _name;

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public object Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public CommonComboBoxItems(object name, object value)
        {
            _name = name;
            _value = value;
        }

        public override string ToString()
        {
            return _name.ToString();
        }
    }
}
