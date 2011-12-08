using System;
using System.Collections.Generic;
using System.Text;
using DAO;
using System.Xml;

namespace BUS
{
    public class XMLConnection_BUS
    {
        public static void SetSuKienPath(string sPath)
        {
            XMLConnection.sPathConfig = sPath;
        }

        public static void SetNTV_TVPath(string sPath)
        {
            XMLConnection.sPathNTV_TV = sPath;
        }

        public static XmlNodeList CreateXmlDocNTV_TV(string sName)
        {
            return XMLConnection.CreateXmlDocNTV_TV(sName);
        }

        public static XmlDocument SelectXmlDocConfig()
        {
            return XMLConnection.SelectXmlDocConfig();
        }
    }
}
