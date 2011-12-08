using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using CryptoFunction;

namespace DAO
{
    public class XMLConnection
    {
        public static string sPathConfig;
        public static string sPathNTV_TV;
        public static string sPathHoSo;



        public static XmlNodeList CreateXmlDocNTV_TV(string sName)
        {
            if (File.Exists(sPathNTV_TV))
            {
                string sContent = Crypto.DecryptData(sPathNTV_TV);
                if (sContent == null)
                {
                    return null;
                }

                XmlDocument xmlTemp = new XmlDocument();
                xmlTemp.LoadXml(sContent);

                return xmlTemp.GetElementsByTagName(sName);
            }
            else
            {
                return null;
            }
        }

        public static XmlNodeList CreateXmlDocHoSo(string sName)
        {
            if (File.Exists(sPathHoSo))
            {
                string sContent = Crypto.DecryptData(sPathHoSo);
                if (sContent == null)
                {
                    return null;
                }

                XmlDocument xmlTemp = new XmlDocument();
                xmlTemp.LoadXml(sContent);

                return xmlTemp.GetElementsByTagName(sName);
            }
            else
            {
                return null;
            }
        }

        public static XmlDocument SelectXmlDocHoSo()
        {
            if (File.Exists(sPathHoSo))
            {
                string sContent = Crypto.DecryptData(sPathHoSo);
                if (sContent == null)
                {
                    return null;
                }

                XmlDocument xmlTemp = new XmlDocument();
                xmlTemp.LoadXml(sContent);

                return xmlTemp;
            }
            else
            {
                return null;
            }
        }

        public static XmlDocument SelectXmlDocConfig()
        {
            if (File.Exists(sPathConfig))
            {
                string sContent = Crypto.DecryptData(sPathConfig);
                if (sContent == null)
                {
                    return null;
                }

                XmlDocument xmlTemp = new XmlDocument();
                xmlTemp.LoadXml(sContent);

                return xmlTemp;
            }
            else
            {
                return null;
            }
        }
    }
}
