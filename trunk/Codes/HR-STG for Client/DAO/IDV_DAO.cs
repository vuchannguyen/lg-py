using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Xml;

namespace DAO
{
    public class IDV_DAO
    {
        public static List<IDV_DTO> LayDSIDV()
        {
            try
            {
                XmlDocument xmlDoc = XMLConnection.SelectXmlDocConfig();
                XmlNodeList list_Temp = xmlDoc.GetElementsByTagName("IDV");
                List<IDV_DTO> list_dto = new List<IDV_DTO>();

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    IDV_DTO dto_Temp = new IDV_DTO();

                    dto_Temp.Ma = list_Temp[i].Attributes["Ma"].InnerText;
                    dto_Temp.IDV = list_Temp[i]["IDV1"].InnerText;
                    dto_Temp.DienGiai = list_Temp[i]["DienGiai"].InnerText;
                    dto_Temp.MatKhau = list_Temp[i]["MatKhau"].InnerText;

                    list_dto.Add(dto_Temp);
                }

                return list_dto;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
