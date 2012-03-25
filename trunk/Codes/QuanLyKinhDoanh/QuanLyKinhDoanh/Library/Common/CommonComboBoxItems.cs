using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class CommonComboBoxItems
    {
        private int _value;
        private string _name;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public CommonComboBoxItems(string name, int value)
        {
            _name = name;
            _value = value;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
