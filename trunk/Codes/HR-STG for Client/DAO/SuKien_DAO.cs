using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Xml;

namespace DAO
{
    public class SuKien_DAO
    {
        public static SuKien_DTO LaySuKien()
        {
            try
            {
                XmlDocument xmlDoc = XMLConnection.SelectXmlDocConfig();
                XmlNodeList list_Temp = xmlDoc.GetElementsByTagName("SuKien");
                //List<SuKien_DTO> list_dto = new List<SuKien_DTO>();

                SuKien_DTO dto_Temp = new SuKien_DTO();

                dto_Temp.Ma = list_Temp[0].Attributes["Ma"].InnerText;
                dto_Temp.Avatar = list_Temp[0]["Avatar"].InnerText;
                dto_Temp.IDS = list_Temp[0]["IDS"].InnerText;
                dto_Temp.Ten = list_Temp[0]["Ten"].InnerText;
                dto_Temp.DiaDiem = list_Temp[0]["DiaDiem"].InnerText;
                dto_Temp.DonViToChuc = list_Temp[0]["DonViToChuc"].InnerText;
                dto_Temp.NhomLoaiHinh = list_Temp[0]["NhomLoaiHinh"].InnerText;
                dto_Temp.LoaiHinh = list_Temp[0]["LoaiHinh"].InnerText;
                dto_Temp.Nganh = list_Temp[0]["Nganh"].InnerText;
                dto_Temp.KhaiMac = DateTime.Parse(list_Temp[0]["KhaiMac"].InnerText);
                dto_Temp.BeMac = DateTime.Parse(list_Temp[0]["BeMac"].InnerText);
                dto_Temp.MoTa = list_Temp[0]["MoTa"].InnerText;

                return dto_Temp;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
