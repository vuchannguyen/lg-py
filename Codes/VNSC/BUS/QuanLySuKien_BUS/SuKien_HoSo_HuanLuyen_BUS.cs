using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class SuKien_HoSo_HuanLuyen_BUS
    {
        public static List<SuKien_HoSo_HuanLuyen> LayDSSuKien_HoSo_HuanLuyen()
        {
            return SuKien_HoSo_HuanLuyen_DAO.LayDSSuKien_HoSo_HuanLuyen();
        }

        public static List<SuKien_HoSo_HuanLuyen> TraCuuDSSuKien_HuanLuyenTheoMaSuKien_HoSo(string sMa)
        {
            return SuKien_HoSo_HuanLuyen_DAO.TraCuuDSSuKien_HuanLuyenTheoMaSuKien_HoSo(sMa);
        }

        public static bool Insert(SuKien_HoSo_HuanLuyen dto)
        {
            return SuKien_HoSo_HuanLuyen_DAO.Insert(dto);
        }

        public static bool Delete(string sMaHoSo, int iMaHuanLuyen)
        {
            return SuKien_HoSo_HuanLuyen_DAO.Delete(sMaHoSo, iMaHuanLuyen);
        }

        public static bool DeleteAll(string sMaHoSo)
        {
            return SuKien_HoSo_HuanLuyen_DAO.DeleteAll(sMaHoSo);
        }
    }
}
