using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonFunction
{
    class MyReturnCode
    {
        private string m_ReturnMesssage;

        public string ReturnMesssage
        {
            get { return m_ReturnMesssage; }
            set { m_ReturnMesssage = value; }
        }
        private bool m_ReturnCode;

        public bool ReturnCode
        {
            get { return m_ReturnCode; }
            set { m_ReturnCode = value; }
        }
        public MyReturnCode(string mess, bool code)
        {
            ReturnMesssage = mess;
            ReturnCode = code;
        }
    }
}
