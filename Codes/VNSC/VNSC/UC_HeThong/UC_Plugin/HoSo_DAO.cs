using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DTO;

namespace DAO
{
    public class HoSo_DAO
    {
        public static List<HoSo_DTO> LayDSHoSo(string sContent)
        {
            try
            {
                XmlDocument xmlTemp = new XmlDocument();
                xmlTemp.LoadXml(sContent);

                XmlNodeList list_Temp = xmlTemp.GetElementsByTagName("HoSo");
                List<HoSo_DTO> list_dto = new List<HoSo_DTO>();

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    HoSo_DTO dto_Temp = new HoSo_DTO();

                    dto_Temp.Ma = int.Parse(list_Temp[i].Attributes["Ma"].InnerText);
                    dto_Temp.NgayCapNhat = list_Temp[i]["NgayCapNhat"].InnerText;

                    dto_Temp.Avatar = list_Temp[i]["Avatar"].InnerText;

                    if ("" == list_Temp[i]["MaIDV"].InnerText)
                    {
                        //
                    }
                    else
                    {
                        dto_Temp.MaIDV = int.Parse(list_Temp[i]["MaIDV"].InnerText);
                    }

                    dto_Temp.MaNhomTrachVu = list_Temp[i]["MaNhomTrachVu"].InnerText;
                    dto_Temp.MaTrachVu = list_Temp[i]["MaTrachVu"].InnerText;

                    dto_Temp.HoTen = list_Temp[i]["HoTen"].InnerText;
                    dto_Temp.NgaySinh = DateTime.Parse(list_Temp[i]["NgaySinh"].InnerText);
                    dto_Temp.GioiTinh = list_Temp[i]["GioiTinh"].InnerText;
                    dto_Temp.QueQuan = list_Temp[i]["QueQuan"].InnerText;
                    dto_Temp.TrinhDoHocVan = list_Temp[i]["TrinhDoHocVan"].InnerText;
                    dto_Temp.TonGiao = list_Temp[i]["TonGiao"].InnerText;
                    dto_Temp.DiaChi = list_Temp[i]["DiaChi"].InnerText;
                    dto_Temp.DienThoaiLienLac = list_Temp[i]["DienThoaiLienLac"].InnerText;
                    dto_Temp.Email = list_Temp[i]["Email"].InnerText;

                    dto_Temp.Nganh = list_Temp[i]["Nganh"].InnerText;
                    dto_Temp.DonVi = list_Temp[i]["DonVi"].InnerText;
                    dto_Temp.LienDoan = list_Temp[i]["LienDoan"].InnerText;
                    dto_Temp.Dao = list_Temp[i]["Dao"].InnerText;
                    dto_Temp.Chau = list_Temp[i]["Chau"].InnerText;
                    dto_Temp.NgayTuyenHua = DateTime.Parse(list_Temp[i]["NgayTuyenHua"].InnerText);
                    dto_Temp.TruongNhanLoiHua = list_Temp[i]["TruongNhanLoiHua"].InnerText;
                    dto_Temp.TrachVuTaiDonVi = list_Temp[i]["TrachVuTaiDonVi"].InnerText;
                    dto_Temp.TrachVuNgoaiDonVi = list_Temp[i]["TrachVuNgoaiDonVi"].InnerText;
                    dto_Temp.TenRung = list_Temp[i]["TenRung"].InnerText;
                    dto_Temp.GhiChu = list_Temp[i]["GhiChu"].InnerText;

                    dto_Temp.NgheNghiep = list_Temp[i]["NgheNghiep"].InnerText;
                    dto_Temp.NutDay = int.Parse(list_Temp[i]["NutDay"].InnerText);
                    dto_Temp.PhuongHuong = int.Parse(list_Temp[i]["PhuongHuong"].InnerText);
                    dto_Temp.CuuThuong = int.Parse(list_Temp[i]["CuuThuong"].InnerText);
                    dto_Temp.TruyenTin = int.Parse(list_Temp[i]["TruyenTin"].InnerText);
                    dto_Temp.TroChoi = int.Parse(list_Temp[i]["TroChoi"].InnerText);
                    dto_Temp.LuaTrai = int.Parse(list_Temp[i]["LuaTrai"].InnerText);
                    dto_Temp.SoTruong = list_Temp[i]["SoTruong"].InnerText;

                    list_dto.Add(dto_Temp);
                }

                return list_dto;
            }
            catch
            {
                return null;
            }
        }

        public static string Insert2String(HoSo_DTO dto)
        {
            try
            {
                XmlDocument XmlDoc = new XmlDocument();
                List<XmlElement> list_dto = new List<XmlElement>();

                XmlElement HoSo = XmlDoc.CreateElement("HoSo");
                HoSo.SetAttribute("Ma", dto.Ma.ToString());



                #region Add content
                list_dto.Add(XmlDoc.CreateElement("MaIDV"));
                list_dto[list_dto.Count - 1].InnerText = dto.MaIDV.ToString();

                list_dto.Add(XmlDoc.CreateElement("MaNhomTrachVu"));
                list_dto[list_dto.Count - 1].InnerText = dto.MaNhomTrachVu;

                list_dto.Add(XmlDoc.CreateElement("MaTrachVu"));
                list_dto[list_dto.Count - 1].InnerText = dto.MaTrachVu;

                list_dto.Add(XmlDoc.CreateElement("Avatar"));
                list_dto[list_dto.Count - 1].InnerText = dto.Avatar;

                list_dto.Add(XmlDoc.CreateElement("NgayCapNhat"));
                list_dto[list_dto.Count - 1].InnerText = dto.NgayCapNhat;



                list_dto.Add(XmlDoc.CreateElement("HoTen"));
                list_dto[list_dto.Count - 1].InnerText = dto.HoTen;

                list_dto.Add(XmlDoc.CreateElement("NgaySinh"));
                list_dto[list_dto.Count - 1].InnerText = dto.NgaySinh.ToString();

                list_dto.Add(XmlDoc.CreateElement("GioiTinh"));
                list_dto[list_dto.Count - 1].InnerText = dto.GioiTinh;

                list_dto.Add(XmlDoc.CreateElement("QueQuan"));
                list_dto[list_dto.Count - 1].InnerText = dto.QueQuan;

                list_dto.Add(XmlDoc.CreateElement("TrinhDoHocVan"));
                list_dto[list_dto.Count - 1].InnerText = dto.TrinhDoHocVan;

                list_dto.Add(XmlDoc.CreateElement("TonGiao"));
                list_dto[list_dto.Count - 1].InnerText = dto.TonGiao;

                list_dto.Add(XmlDoc.CreateElement("DiaChi"));
                list_dto[list_dto.Count - 1].InnerText = dto.DiaChi;

                list_dto.Add(XmlDoc.CreateElement("DienThoaiLienLac"));
                list_dto[list_dto.Count - 1].InnerText = dto.DienThoaiLienLac;

                list_dto.Add(XmlDoc.CreateElement("Email"));
                list_dto[list_dto.Count - 1].InnerText = dto.Email;



                list_dto.Add(XmlDoc.CreateElement("Nganh"));
                list_dto[list_dto.Count - 1].InnerText = dto.Nganh;

                list_dto.Add(XmlDoc.CreateElement("DonVi"));
                list_dto[list_dto.Count - 1].InnerText = dto.DonVi;

                list_dto.Add(XmlDoc.CreateElement("LienDoan"));
                list_dto[list_dto.Count - 1].InnerText = dto.LienDoan;

                list_dto.Add(XmlDoc.CreateElement("Dao"));
                list_dto[list_dto.Count - 1].InnerText = dto.Dao;

                list_dto.Add(XmlDoc.CreateElement("Chau"));
                list_dto[list_dto.Count - 1].InnerText = dto.Chau;

                list_dto.Add(XmlDoc.CreateElement("NgayTuyenHua"));
                list_dto[list_dto.Count - 1].InnerText = dto.NgayTuyenHua.ToString();

                list_dto.Add(XmlDoc.CreateElement("TruongNhanLoiHua"));
                list_dto[list_dto.Count - 1].InnerText = dto.TruongNhanLoiHua;

                list_dto.Add(XmlDoc.CreateElement("TrachVuTaiDonVi"));
                list_dto[list_dto.Count - 1].InnerText = dto.TrachVuTaiDonVi;

                list_dto.Add(XmlDoc.CreateElement("TrachVuNgoaiDonVi"));
                list_dto[list_dto.Count - 1].InnerText = dto.TrachVuNgoaiDonVi;

                list_dto.Add(XmlDoc.CreateElement("TenRung"));
                list_dto[list_dto.Count - 1].InnerText = dto.TenRung;

                list_dto.Add(XmlDoc.CreateElement("GhiChu"));
                list_dto[list_dto.Count - 1].InnerText = dto.GhiChu;



                list_dto.Add(XmlDoc.CreateElement("NgheNghiep"));
                list_dto[list_dto.Count - 1].InnerText = dto.NgheNghiep;

                list_dto.Add(XmlDoc.CreateElement("NutDay"));
                list_dto[list_dto.Count - 1].InnerText = dto.NutDay.ToString();

                list_dto.Add(XmlDoc.CreateElement("PhuongHuong"));
                list_dto[list_dto.Count - 1].InnerText = dto.PhuongHuong.ToString();

                list_dto.Add(XmlDoc.CreateElement("CuuThuong"));
                list_dto[list_dto.Count - 1].InnerText = dto.CuuThuong.ToString();

                list_dto.Add(XmlDoc.CreateElement("TruyenTin"));
                list_dto[list_dto.Count - 1].InnerText = dto.TruyenTin.ToString();

                list_dto.Add(XmlDoc.CreateElement("TroChoi"));
                list_dto[list_dto.Count - 1].InnerText = dto.TroChoi.ToString();

                list_dto.Add(XmlDoc.CreateElement("LuaTrai"));
                list_dto[list_dto.Count - 1].InnerText = dto.LuaTrai.ToString();

                list_dto.Add(XmlDoc.CreateElement("SoTruong"));
                list_dto[list_dto.Count - 1].InnerText = dto.SoTruong;
                #endregion



                for (int i = 0; i < list_dto.Count; i++)
                {
                    HoSo.AppendChild(list_dto[i]);
                }

                return HoSo.OuterXml;
            }
            catch
            {
                return null;
            }
        }
    }
}
