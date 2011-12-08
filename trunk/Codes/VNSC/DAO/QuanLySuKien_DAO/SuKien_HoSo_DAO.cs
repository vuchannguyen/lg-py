using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class SuKien_HoSo_DAO
    {
        public static List<SuKien_HoSo> LayDSSuKien_HoSo()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_HoSo> list = new List<SuKien_HoSo>();

                var q = from p in VNSC.SuKien_HoSos
                        select p;

                foreach (SuKien_HoSo k in q)
                {
                    list.Add(k);
                }

                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<SuKien_HoSo> TraCuuDSSuKien_HoSoTheoMaSuKien(int iMaSuKien)
        {
            try
            {
                List<SuKien_HoSo> list = new List<SuKien_HoSo>();
                List<SuKien_HoSoThamDu> q = SuKien_HoSoThamDu_DAO.TraCuuDSHoSoThamDuTheoMaSuKien(iMaSuKien);

                foreach (SuKien_HoSoThamDu k in q)
                {
                    list.Add(TraCuuSuKien_HoSoTheoMa(k.MaSuKien_HoSo));
                }

                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<SuKien_HoSo> TraCuuDSSuKien_HoSoTheoTen(int iMaSuKien, string sTen)
        {
            try
            {
                //VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_HoSo> list = new List<SuKien_HoSo>();
                List<SuKien_HoSo> list_SuKien = TraCuuDSSuKien_HoSoTheoMaSuKien(iMaSuKien);

                var q = from p in list_SuKien
                        where p.HoTen.Contains(sTen)
                        select p;

                foreach (SuKien_HoSo k in q)
                {
                    list.Add(k);
                }

                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static SuKien_HoSo TraCuuSuKien_HoSoTheoMa(string sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien_HoSo sk = VNSC.SuKien_HoSos.Single(P => P.Ma == sMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(SuKien_HoSo dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.SuKien_HoSos.InsertOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(string sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien_HoSo dto = VNSC.SuKien_HoSos.Single(P => P.Ma == sMa);

                VNSC.SuKien_HoSos.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateSuKien_HoSoInfo(SuKien_HoSo dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien_HoSo sk = VNSC.SuKien_HoSos.Single(P => P.Ma == dto.Ma);
                sk.NgayCapNhat = dto.NgayCapNhat;

                sk.MaIDV = dto.MaIDV;
                sk.MaNhomTrachVu = dto.MaNhomTrachVu;
                sk.MaTrachVu = dto.MaTrachVu;
                sk.HoTen = dto.HoTen;
                sk.NgaySinh = dto.NgaySinh;
                sk.GioiTinh = dto.GioiTinh;
                sk.QueQuan = dto.QueQuan;
                sk.TrinhDoHocVan = dto.TrinhDoHocVan;
                sk.TonGiao = dto.TonGiao;
                sk.DiaChi = dto.DiaChi;
                sk.DienThoaiLienLac = dto.DienThoaiLienLac;
                sk.Email = dto.Email;

                sk.Nganh = dto.Nganh;
                sk.DonVi = dto.DonVi;
                sk.LienDoan = dto.LienDoan;
                sk.Dao = dto.Dao;
                sk.Chau = dto.Chau;
                sk.NgayTuyenHua = dto.NgayTuyenHua;
                sk.TruongNhanLoiHua = dto.TruongNhanLoiHua;
                sk.TrachVuTaiDonVi = dto.TrachVuTaiDonVi;
                sk.TrachVuNgoaiDonVi = dto.TrachVuNgoaiDonVi;
                sk.TenRung = dto.TenRung;
                sk.GhiChu = dto.GhiChu;

                sk.NgheNghiep = dto.NgheNghiep;
                sk.NutDay = dto.NutDay;
                sk.PhuongHuong = dto.PhuongHuong;
                sk.CuuThuong = dto.CuuThuong;
                sk.TruyenTin = dto.TruyenTin;
                sk.TroChoi = dto.TroChoi;
                sk.LuaTrai = dto.LuaTrai;
                sk.SoTruong = dto.SoTruong;

                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
