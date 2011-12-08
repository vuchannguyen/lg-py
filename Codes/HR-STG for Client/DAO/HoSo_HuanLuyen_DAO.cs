using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Xml;

namespace DAO
{
    public class HoSo_HuanLuyen_DAO
    {
        public static List<HoSo_HuanLuyen_DTO> LayDSHoSo_HuanLuyen()
        {
            try
            {
                XmlNodeList list_Temp = XMLConnection.CreateXmlDocHoSo("HoSo_HuanLuyen");
                List<HoSo_HuanLuyen_DTO> list_dto = new List<HoSo_HuanLuyen_DTO>();

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    HoSo_HuanLuyen_DTO dto_Temp = new HoSo_HuanLuyen_DTO();

                    dto_Temp.MaHoSo = int.Parse(list_Temp[i]["MaHoSo"].InnerText);
                    dto_Temp.MaHuanLuyen = int.Parse(list_Temp[i]["MaHuanLuyen"].InnerText);

                    list_dto.Add(dto_Temp);
                }

                return list_dto;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo_HuanLuyen_DTO> TraCuuDSHuanLuyenTheoMaHoSo(int iMa)
        {
            try
            {
                XmlNodeList list_Temp = XMLConnection.CreateXmlDocHoSo("HoSo_HuanLuyen");
                List<HoSo_HuanLuyen_DTO> list_dto = new List<HoSo_HuanLuyen_DTO>();

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    if (int.Parse(list_Temp[i]["MaHoSo"].InnerText) == iMa)
                    {
                        HoSo_HuanLuyen_DTO dto_Temp = new HoSo_HuanLuyen_DTO();

                        dto_Temp.MaHoSo = int.Parse(list_Temp[i]["MaHoSo"].InnerText);
                        dto_Temp.MaHuanLuyen = int.Parse(list_Temp[i]["MaHuanLuyen"].InnerText);

                        list_dto.Add(dto_Temp);
                    }
                }

                return list_dto;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static string Insert(HoSo_HuanLuyen_DTO dto)
        {
            try
            {
                XmlDocument XmlDoc = XMLConnection.SelectXmlDocHoSo();
                List<XmlElement> list_dto = new List<XmlElement>();

                XmlElement HoSo = XmlDoc.CreateElement("HoSo_HuanLuyen");



                #region Add content
                list_dto.Add(XmlDoc.CreateElement("MaHoSo"));
                list_dto[list_dto.Count - 1].InnerText = dto.MaHoSo.ToString();

                list_dto.Add(XmlDoc.CreateElement("MaHuanLuyen"));
                list_dto[list_dto.Count - 1].InnerText = dto.MaHuanLuyen.ToString();
                #endregion



                for (int i = 0; i < list_dto.Count; i++)
                {
                    HoSo.AppendChild(list_dto[i]);
                }

                XmlDoc.GetElementsByTagName("HS_HL")[0].AppendChild(HoSo);
                //XmlDoc.Save("HoSo.xml");

                return XmlDoc.OuterXml;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static string Insert2String(HoSo_HuanLuyen_DTO dto)
        {
            try
            {
                XmlDocument XmlDoc = XMLConnection.SelectXmlDocHoSo();
                List<XmlElement> list_dto = new List<XmlElement>();

                XmlElement HoSo = XmlDoc.CreateElement("HoSo_HuanLuyen");



                #region Add content
                list_dto.Add(XmlDoc.CreateElement("MaHoSo"));
                list_dto[list_dto.Count - 1].InnerText = dto.MaHoSo.ToString();

                list_dto.Add(XmlDoc.CreateElement("MaHuanLuyen"));
                list_dto[list_dto.Count - 1].InnerText = dto.MaHuanLuyen.ToString();
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

        public static string Delete(int iMaHoSo, int iMaHuanLuyen)
        {
            try
            {
                XmlDocument XmlDoc = XMLConnection.SelectXmlDocHoSo();
                XmlNodeList dto_Temp = XmlDoc.GetElementsByTagName("HS_HL");
                XmlNodeList list_Temp = XmlDoc.GetElementsByTagName("HoSo_HuanLuyen");

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    if (int.Parse(list_Temp[i]["MaHoSo"].InnerText) == iMaHoSo && int.Parse(list_Temp[i]["MaHuanLuyen"].InnerText) == iMaHuanLuyen)
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

        //public static bool UpdateHoSo_HuanLuyenInfo(HoSo_HuanLuyen dto)
        //{
        //    try
        //    {
        //        VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
        //        HoSo_HuanLuyen sk = VNSC.HoSo_HuanLuyens.Single(P => P.Ma == dto.Ma);

        //        sk.Ten = dto.Ten;
        //        sk.MoTa = dto.MoTa;

        //        VNSC.SubmitChanges();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}
