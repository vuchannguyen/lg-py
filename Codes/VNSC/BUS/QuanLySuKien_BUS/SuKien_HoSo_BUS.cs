using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class SuKien_HoSo_BUS
    {
        public static List<SuKien_HoSo> LayDSSuKien_HoSo()
        {
            return SuKien_HoSo_DAO.LayDSSuKien_HoSo();
        }

        public static List<SuKien_HoSo> TraCuuDSSuKien_HoSoTheoMaSuKien(int iMaSuKien)
        {
            return SuKien_HoSo_DAO.TraCuuDSSuKien_HoSoTheoMaSuKien(iMaSuKien);
        }

        public static List<SuKien_HoSo> TraCuuDSSuKien_HoSoTheoTen(int iMaSuKien, string sTen)
        {
            return SuKien_HoSo_DAO.TraCuuDSSuKien_HoSoTheoTen(iMaSuKien, sTen);
        }

        public static SuKien_HoSo TraCuuSuKien_HoSoTheoMa(string sMa)
        {
            return SuKien_HoSo_DAO.TraCuuSuKien_HoSoTheoMa(sMa);
        }

        public static bool Insert(SuKien_HoSo dto)
        {
            return SuKien_HoSo_DAO.Insert(dto);
        }

        public static bool Delete(string sMa)
        {
            return SuKien_HoSo_DAO.Delete(sMa);
        }

        public static bool UpdateSuKien_HoSoInfo(SuKien_HoSo dto)
        {
            return SuKien_HoSo_DAO.UpdateSuKien_HoSoInfo(dto);
        }
    }
}
