using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class SuKien_BUS
    {
        public static List<SuKien> LayDSSuKien()
        {
            return SuKien_DAO.LayDSSuKien();
        }

        public static List<SuKien> TraCuuDSSuKienTheoIDS(string sIDS)
        {
            return SuKien_DAO.TraCuuDSSuKienTheoIDS(sIDS);
        }

        public static List<SuKien> TraCuuDSSuKienTheoTen(string sTen)
        {
            return SuKien_DAO.TraCuuDSSuKienTheoTen(sTen);
        }

        public static List<SuKien> TraCuuDSSuKienTheoDiaDiem(string sDiaDiem)
        {
            return SuKien_DAO.TraCuuDSSuKienTheoDiaDiem(sDiaDiem);
        }

        public static List<SuKien> TraCuuDSSuKienTheoDonViToChuc(string sDonViToChuc)
        {
            return SuKien_DAO.TraCuuDSSuKienTheoDonViToChuc(sDonViToChuc);
        }

        public static List<SuKien> TraCuuDSSuKienTheoTenNhomLoaiHinh(String sTen)
        {
            return SuKien_DAO.TraCuuDSSuKienTheoTenNhomLoaiHinh(sTen);
        }

        public static List<SuKien> TraCuuDSSuKienTheoTenLoaiHinh(String sTen)
        {
            return SuKien_DAO.TraCuuDSSuKienTheoTenLoaiHinh(sTen);
        }

        public static SuKien TraCuuSuKienTheoMa(int iMa)
        {
            return SuKien_DAO.TraCuuSuKienTheoMa(iMa);
        }

        public static bool Insert(SuKien dto)
        {
            return SuKien_DAO.Insert(dto);
        }

        public static bool Delete(int iMa)
        {
            return SuKien_DAO.Delete(iMa);
        }

        public static bool UpdateSuKienInfo(SuKien dto)
        {
            return SuKien_DAO.UpdateSuKienInfo(dto);
        }
    }
}
