using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class SuKien_DAO
    {
        public static List<SuKien> LayDSSuKien()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien> list = new List<SuKien>();

                var q = from p in VNSC.SuKiens
                        select p;

                foreach (SuKien k in q)
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

        public static List<SuKien> TraCuuDSSuKienTheoIDS(string sIDS)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien> list = new List<SuKien>();

                var q = from p in VNSC.SuKiens
                        where p.IDS == sIDS
                        select p;
                foreach (SuKien k in q)
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

        public static List<SuKien> TraCuuDSSuKienTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien> list = new List<SuKien>();

                var q = from p in VNSC.SuKiens
                        where p.Ten.Contains(sTen)
                        select p;
                foreach (SuKien k in q)
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

        public static List<SuKien> TraCuuDSSuKienTheoDiaDiem(String sDiaDiem)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien> list = new List<SuKien>();

                var q = from p in VNSC.SuKiens
                        where p.DiaDiem == sDiaDiem
                        select p;
                foreach (SuKien k in q)
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

        public static List<SuKien> TraCuuDSSuKienTheoDonViToChuc(String sDonViToChuc)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien> list = new List<SuKien>();

                var q = from p in VNSC.SuKiens
                        where p.DonViToChuc == sDonViToChuc
                        select p;
                foreach (SuKien k in q)
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

        public static List<SuKien> TraCuuDSSuKienTheoTenNhomLoaiHinh(String sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                NhomLoaiHinh NhomLoaiHinh = NhomLoaiHinh_DAO.TraCuuNhomLoaiHinhTheoTen(sTen);
                List<SuKien> list = new List<SuKien>();

                var q = from p in VNSC.SuKiens
                        where p.MaNhomLoaiHinh == NhomLoaiHinh.Ma
                        select p;
                foreach (SuKien k in q)
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

        public static List<SuKien> TraCuuDSSuKienTheoTenLoaiHinh(String sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                LoaiHinh LoaiHinh = LoaiHinh_DAO.TraCuuLoaiHinhTheoTen(sTen);
                List<SuKien> list = new List<SuKien>();

                var q = from p in VNSC.SuKiens
                        where p.MaLoaiHinh == LoaiHinh.Ma
                        select p;
                foreach (SuKien k in q)
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

        public static SuKien TraCuuSuKienTheoMa(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien sk = VNSC.SuKiens.Single(P => P.Ma == iMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(SuKien dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.SuKiens.InsertOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien dto = VNSC.SuKiens.Single(P => P.Ma == iMa);

                VNSC.SuKiens.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateSuKienInfo(SuKien dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien sk = VNSC.SuKiens.Single(P => P.Ma == dto.Ma);

                sk.NgayCapNhatEvatar = dto.NgayCapNhatEvatar;
                sk.IDS = dto.IDS;
                sk.Ten = dto.Ten;
                sk.DiaDiem = dto.DiaDiem;
                sk.DonViToChuc = dto.DonViToChuc;
                sk.MaNhomLoaiHinh = dto.MaNhomLoaiHinh;
                sk.MaLoaiHinh = dto.MaLoaiHinh;
                sk.KhaiMac = dto.KhaiMac;
                sk.BeMac = dto.BeMac;
                sk.MoTa = dto.MoTa;

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
