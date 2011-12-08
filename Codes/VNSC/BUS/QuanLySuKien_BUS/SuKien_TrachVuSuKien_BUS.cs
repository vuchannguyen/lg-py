using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class SuKien_TrachVuSuKien_BUS
    {
        public static List<SuKien_TrachVuSuKien> LayDSSuKien_TrachVuSuKien()
        {
            return SuKien_TrachVuSuKien_DAO.LayDSSuKien_TrachVuSuKien();
        }

        public static List<SuKien_TrachVuSuKien> TraCuuDSSuKien_TrachVuSuKienTheoMaSuKien(int iMa)
        {
            return SuKien_TrachVuSuKien_DAO.TraCuuDSSuKien_TrachVuSuKienTheoMaSuKien(iMa);
        }

        public static bool Insert(SuKien_TrachVuSuKien dto)
        {
            return SuKien_TrachVuSuKien_DAO.Insert(dto);
        }

        public static bool Delete(int iMaSuKien, int iMaTrachVuSuKien)
        {
            return SuKien_TrachVuSuKien_DAO.Delete(iMaSuKien, iMaTrachVuSuKien);
        }

        public static bool DeleteAll(int iMaSuKien)
        {
            return SuKien_TrachVuSuKien_DAO.DeleteAll(iMaSuKien);
        }
    }
}
