using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Xml;

namespace DAO
{
    public class TrachVu_DAO
    {
        public static List<TrachVu_DTO> LayDSTrachVu()
        {
            try
            {
                XmlNodeList list_Temp = XMLConnection.CreateXmlDocNTV_TV("TrachVu");
                List<TrachVu_DTO> list_dto = new List<TrachVu_DTO>();

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    TrachVu_DTO dto_Temp = new TrachVu_DTO();

                    dto_Temp.Ma = list_Temp[i].Attributes["Ma"].InnerText;
                    dto_Temp.MaDoiTuong = list_Temp[i]["MaNhomTrachVu"].InnerText;
                    dto_Temp.Ten = list_Temp[i]["Ten"].InnerText;

                    list_dto.Add(dto_Temp);
                }

                return list_dto;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static TrachVu_DTO TraCuuTrachVuTheoMa(string sMa)
        {
            try
            {
                XmlNodeList list_Temp = XMLConnection.CreateXmlDocNTV_TV("TrachVu");
                TrachVu_DTO dto = new TrachVu_DTO();

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    if (list_Temp[i].Attributes["Ma"].InnerText == sMa)
                    {
                        TrachVu_DTO dto_Temp = new TrachVu_DTO();

                        dto_Temp.Ma = list_Temp[i].Attributes["Ma"].InnerText;
                        dto_Temp.MaDoiTuong = list_Temp[i]["MaNhomTrachVu"].InnerText;
                        dto_Temp.Ten = list_Temp[i]["Ten"].InnerText;

                        dto = dto_Temp;
                        break;
                    }
                }

                return dto;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
