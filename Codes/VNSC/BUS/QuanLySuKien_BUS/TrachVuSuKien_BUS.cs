using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class TrachVuSuKien_BUS
    {
        public static List<TrachVuSuKien> LayDSTrachVuSuKien()
        {
            return TrachVuSuKien_DAO.LayDSTrachVuSuKien();
        }

        public static List<TrachVuSuKien> TraCuuDSTrachVuSuKienTheoMaSuKien(int iMaSuKien)
        {
            return TrachVuSuKien_DAO.TraCuuDSTrachVuSuKienTheoMaSuKien(iMaSuKien);
        }

        public static List<TrachVuSuKien> TraCuuDSTrachVuSuKienTheoTen(string sTen)
        {
            return TrachVuSuKien_DAO.TraCuuDSTrachVuSuKienTheoTen(sTen);
        }

        public static List<TrachVuSuKien> TraCuuDSTrachVuSuKienTheoMaDonViHanhChanh(int iMa)
        {
            return TrachVuSuKien_DAO.TraCuuDSTrachVuSuKienTheoMaDonViHanhChanh(iMa);
        }

        public static TrachVuSuKien TraCuuTrachVuSuKienTheoMa(int iMa)
        {
            return TrachVuSuKien_DAO.TraCuuTrachVuSuKienTheoMa(iMa);
        }

        public static TrachVuSuKien TraCuuTrachVuSuKienTheoTen(string sTen)
        {
            return TrachVuSuKien_DAO.TraCuuTrachVuSuKienTheoTen(sTen);
        }

        public static bool Insert(TrachVuSuKien dto)
        {
            return TrachVuSuKien_DAO.Insert(dto);
        }

        public static bool Delete(int iMa)
        {
            return TrachVuSuKien_DAO.Delete(iMa);
        }

        public static bool UpdateTrachVuSuKienInfo(TrachVuSuKien dto)
        {
            return TrachVuSuKien_DAO.UpdateTrachVuSuKienInfo(dto);
        }
    }
}
