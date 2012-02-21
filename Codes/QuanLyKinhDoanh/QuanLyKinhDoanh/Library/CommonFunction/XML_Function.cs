using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CommonFunction
{
    class XML_Function
    {
        public static string getElementInnerText(XmlDocument xmlDoc, string sTagName, int iIndex)
        {
            XmlElement root = xmlDoc.DocumentElement;
            XmlNodeList xmlNode = root.GetElementsByTagName(sTagName);

            return xmlNode[iIndex].InnerText;
        }
    }
}
