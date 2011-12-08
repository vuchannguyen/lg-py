using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class SuKien_HuanLuyen_BUS
    {
        public static List<SuKien_HuanLuyen> LayDSSuKien_HuanLuyen()
        {
            return SuKien_HuanLuyen_DAO.LayDSSuKien_HuanLuyen();
        }

        public static SuKien_HuanLuyen TraCuuSuKien_HuanLuyenTheoMa(int iMa)
        {
            return SuKien_HuanLuyen_DAO.TraCuuSuKien_HuanLuyenTheoMa(iMa);
        }

        public static SuKien_HuanLuyen TraCuuSuKien_HuanLuyenTheoTen(string sTen)
        {
            return SuKien_HuanLuyen_DAO.TraCuuSuKien_HuanLuyenTheoTen(sTen);
        }

        public static bool Insert(SuKien_HuanLuyen dto)
        {
            return SuKien_HuanLuyen_DAO.Insert(dto);
        }

        public static bool Delete(int iMa)
        {
            return SuKien_HuanLuyen_DAO.Delete(iMa);
        }

        public static bool UpdateSuKien_HuanLuyenInfo(SuKien_HuanLuyen dto)
        {
            return SuKien_HuanLuyen_DAO.UpdateSuKien_HuanLuyenInfo(dto);
        }
    }
}
