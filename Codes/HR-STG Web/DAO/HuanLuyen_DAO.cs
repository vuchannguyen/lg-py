using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Xml;

namespace DAO
{
    public class HuanLuyen_DAO
    {
        public static List<HuanLuyen_DTO> LayDSHuanLuyen()
        {
            try
            {
                XmlNodeList list_Temp = XMLConnection.CreateXmlDocHoSo("HuanLuyen");
                List<HuanLuyen_DTO> list_dto = new List<HuanLuyen_DTO>();

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    HuanLuyen_DTO dto_Temp = new HuanLuyen_DTO();

                    dto_Temp.Ma = int.Parse(list_Temp[i].Attributes["Ma"].InnerText);
                    dto_Temp.Nganh = list_Temp[i]["Nganh"].InnerText;
                    dto_Temp.Khoa = list_Temp[i]["Khoa"].InnerText;
                    dto_Temp.TenKhoa = list_Temp[i]["TenKhoa"].InnerText;
                    dto_Temp.KhoaTruong = list_Temp[i]["KhoaTruong"].InnerText;
                    dto_Temp.Nam = DateTime.Parse(list_Temp[i]["Nam"].InnerText);
                    dto_Temp.MHL = list_Temp[i]["MHL"].InnerText;
                    dto_Temp.TinhTrang = list_Temp[i]["TinhTrang"].InnerText;

                    list_dto.Add(dto_Temp);
                }

                return list_dto;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static HuanLuyen_DTO TraCuuHuanLuyenTheoMa(int iMa)
        {
            try
            {
                XmlNodeList list_Temp = XMLConnection.CreateXmlDocHoSo("HuanLuyen");
                HuanLuyen_DTO dto = new HuanLuyen_DTO();

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    if (int.Parse(list_Temp[i].Attributes["Ma"].InnerText) == iMa)
                    {
                        HuanLuyen_DTO dto_Temp = new HuanLuyen_DTO();

                        dto_Temp.Ma = int.Parse(list_Temp[i].Attributes["Ma"].InnerText);
                        dto_Temp.Nganh = list_Temp[i]["Nganh"].InnerText;
                        dto_Temp.Khoa = list_Temp[i]["Khoa"].InnerText;
                        dto_Temp.TenKhoa = list_Temp[i]["TenKhoa"].InnerText;
                        dto_Temp.KhoaTruong = list_Temp[i]["KhoaTruong"].InnerText;
                        dto_Temp.Nam = DateTime.Parse(list_Temp[i]["Nam"].InnerText);
                        dto_Temp.MHL = list_Temp[i]["MHL"].InnerText;
                        dto_Temp.TinhTrang = list_Temp[i]["TinhTrang"].InnerText;

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

        public static string Insert(HuanLuyen_DTO dto)
        {
            try
            {
                XmlDocument XmlDoc = XMLConnection.SelectXmlDocHoSo();
                List<XmlElement> list_dto = new List<XmlElement>();

                XmlElement HoSo = XmlDoc.CreateElement("HuanLuyen");
                HoSo.SetAttribute("Ma", dto.Ma.ToString());



                #region Add content
                list_dto.Add(XmlDoc.CreateElement("Nganh"));
                list_dto[list_dto.Count - 1].InnerText = dto.Nganh;

                list_dto.Add(XmlDoc.CreateElement("Khoa"));
                list_dto[list_dto.Count - 1].InnerText = dto.Khoa;

                list_dto.Add(XmlDoc.CreateElement("TenKhoa"));
                list_dto[list_dto.Count - 1].InnerText = dto.TenKhoa;

                list_dto.Add(XmlDoc.CreateElement("KhoaTruong"));
                list_dto[list_dto.Count - 1].InnerText = dto.KhoaTruong;

                list_dto.Add(XmlDoc.CreateElement("Nam"));
                list_dto[list_dto.Count - 1].InnerText = dto.Nam.ToString();

                list_dto.Add(XmlDoc.CreateElement("MHL"));
                list_dto[list_dto.Count - 1].InnerText = dto.MHL;

                list_dto.Add(XmlDoc.CreateElement("TinhTrang"));
                list_dto[list_dto.Count - 1].InnerText = dto.TinhTrang;
                #endregion



                for (int i = 0; i < list_dto.Count; i++)
                {
                    HoSo.AppendChild(list_dto[i]);
                }

                XmlDoc.GetElementsByTagName("HL")[0].AppendChild(HoSo);
                //XmlDoc.Save("HoSo.xml");

                return XmlDoc.OuterXml;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static string Insert2String(HuanLuyen_DTO dto)
        {
            try
            {
                XmlDocument XmlDoc = XMLConnection.SelectXmlDocHoSo();
                List<XmlElement> list_dto = new List<XmlElement>();

                XmlElement HoSo = XmlDoc.CreateElement("HuanLuyen");
                HoSo.SetAttribute("Ma", dto.Ma.ToString());



                #region Add content
                list_dto.Add(XmlDoc.CreateElement("Nganh"));
                list_dto[list_dto.Count - 1].InnerText = dto.Nganh;

                list_dto.Add(XmlDoc.CreateElement("Khoa"));
                list_dto[list_dto.Count - 1].InnerText = dto.Khoa;

                list_dto.Add(XmlDoc.CreateElement("TenKhoa"));
                list_dto[list_dto.Count - 1].InnerText = dto.TenKhoa;

                list_dto.Add(XmlDoc.CreateElement("KhoaTruong"));
                list_dto[list_dto.Count - 1].InnerText = dto.KhoaTruong;

                list_dto.Add(XmlDoc.CreateElement("Nam"));
                list_dto[list_dto.Count - 1].InnerText = dto.Nam.ToString();

                list_dto.Add(XmlDoc.CreateElement("MHL"));
                list_dto[list_dto.Count - 1].InnerText = dto.MHL;

                list_dto.Add(XmlDoc.CreateElement("TinhTrang"));
                list_dto[list_dto.Count - 1].InnerText = dto.TinhTrang;
                #endregion



                for (int i = 0; i < list_dto.Count; i++)
                {
                    HoSo.AppendChild(list_dto[i]);
                }

                return HoSo.OuterXml;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static string Delete(int iMa)
        {
            try
            {
                XmlDocument XmlDoc = XMLConnection.SelectXmlDocHoSo();
                XmlNodeList dto_Temp = XmlDoc.GetElementsByTagName("HL");
                XmlNodeList list_Temp = XmlDoc.GetElementsByTagName("HuanLuyen");

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    if (int.Parse(list_Temp[i].Attributes["Ma"].InnerText) == iMa)
                    {
                        dto_Temp[0].RemoveChild(list_Temp[i]);
                    }
                }

                //XmlDoc.Save("HoSo.xml");

                return XmlDoc.OuterXml;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static string UpdateHuanLuyenInfo(HuanLuyen_DTO dto)
        {
            try
            {
                XmlDocument XmlDoc = XMLConnection.SelectXmlDocHoSo();
                XmlNodeList list_Temp = XmlDoc.GetElementsByTagName("HuanLuyen");

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    if (int.Parse(list_Temp[i].Attributes["Ma"].InnerText) == dto.Ma)
                    {
                        list_Temp[i]["Nganh"].InnerText = dto.Nganh;
                        list_Temp[i]["Khoa"].InnerText = dto.Khoa;
                        list_Temp[i]["TenKhoa"].InnerText = dto.TenKhoa;
                        list_Temp[i]["KhoaTruong"].InnerText = dto.KhoaTruong;
                        list_Temp[i]["Nam"].InnerText = dto.Nam.ToString();
                        list_Temp[i]["MHL"].InnerText = dto.MHL;
                        list_Temp[i]["TinhTrang"].InnerText = dto.TinhTrang;
                    }
                }

                //XmlDoc.Save("HoSo.xml");

                return XmlDoc.OuterXml;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
