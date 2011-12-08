using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class SuKien_HoSoThamDu_BUS
    {
        public static List<SuKien_HoSoThamDu> LayDSSuKien_HoSoThamDu()
        {
            return SuKien_HoSoThamDu_DAO.LayDSSuKien_HoSoThamDu();
        }

        public static SuKien_HoSoThamDu TraCuuSuKien_HoSoThamDuTheoMa(int iMaSuKien, string sMaHoSo)
        {
            return SuKien_HoSoThamDu_DAO.TraCuuSuKien_HoSoThamDuTheoMa(iMaSuKien, sMaHoSo);
        }

        public static bool Insert(SuKien_HoSoThamDu dto)
        {
            return SuKien_HoSoThamDu_DAO.Insert(dto);
        }

        public static bool Delete(int iMaSuKien, string sMaHoSo)
        {
            return SuKien_HoSoThamDu_DAO.Delete(iMaSuKien, sMaHoSo);
        }

        public static bool UpdateSuKien_HoSoThamDuInfo(SuKien_HoSoThamDu dto)
        {
            return SuKien_HoSoThamDu_DAO.UpdateSuKien_HoSoThamDuInfo(dto);
        }
    }
}
