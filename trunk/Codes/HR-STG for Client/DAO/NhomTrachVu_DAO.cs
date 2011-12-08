using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Xml;

namespace DAO
{
    public class NhomTrachVu_DAO
    {
        public static List<NhomTrachVu_DTO> LayDSNhomTrachVu()
        {
            try
            {
                XmlNodeList list_Temp = XMLConnection.CreateXmlDocNTV_TV("NhomTrachVu");
                List<NhomTrachVu_DTO> list_dto = new List<NhomTrachVu_DTO>();

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    NhomTrachVu_DTO dto_Temp = new NhomTrachVu_DTO();

                    dto_Temp.Ma = list_Temp[i].Attributes["Ma"].InnerText;
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

        public static NhomTrachVu_DTO TraCuuNhomTrachVuTheoMa(string sMa)
        {
            try
            {
                XmlNodeList list_Temp = XMLConnection.CreateXmlDocNTV_TV("NhomTrachVu");
                NhomTrachVu_DTO dto = new NhomTrachVu_DTO();

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    if (list_Temp[i].Attributes["Ma"].InnerText == sMa)
                    {
                        NhomTrachVu_DTO dto_Temp = new NhomTrachVu_DTO();

                        dto_Temp.Ma = list_Temp[i].Attributes["Ma"].InnerText;
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

        //public static bool Insert(NhomTrachVu_DTO dto)
        //{
        //    try
        //    {
        //        VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

        //        VNSC.NhomTrachVus.InsertOnSubmit(dto);
        //        VNSC.SubmitChanges();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public static bool Delete(string sMa)
        //{
        //    try
        //    {
        //        VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
        //        NhomTrachVu dto = VNSC.NhomTrachVus.Single(P => P.Ma == sMa);

        //        VNSC.NhomTrachVus.DeleteOnSubmit(dto);
        //        VNSC.SubmitChanges();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public static bool UpdateNhomTrachVuInfo(NhomTrachVu dto)
        //{
        //    try
        //    {
        //        VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
        //        NhomTrachVu sk = VNSC.NhomTrachVus.Single(P => P.Ma == dto.Ma);

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
