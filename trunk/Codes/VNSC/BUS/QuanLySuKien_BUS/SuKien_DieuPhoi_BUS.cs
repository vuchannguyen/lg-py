using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class SuKien_DieuPhoi_BUS
    {
        public static List<SuKien_DieuPhoi> LayDSSuKien_DieuPhoi()
        {
            return SuKien_DieuPhoi_DAO.LayDSSuKien_DieuPhoi();
        }

        public static List<SuKien_DieuPhoi> TraCuuDSSuKien_DieuPhoiTheoMaSuKien(int iMa)
        {
            return SuKien_DieuPhoi_DAO.TraCuuDSSuKien_DieuPhoiTheoMaSuKien(iMa);
        }

        public static bool Insert(SuKien_DieuPhoi dto)
        {
            return SuKien_DieuPhoi_DAO.Insert(dto);
        }

        public static bool Delete(int iMaSuKien, int iMaDieuPhoi)
        {
            return SuKien_DieuPhoi_DAO.Delete(iMaSuKien, iMaDieuPhoi);
        }

        public static bool DeleteAll(int iMaSuKien)
        {
            return SuKien_DieuPhoi_DAO.DeleteAll(iMaSuKien);
        }
    }
}
