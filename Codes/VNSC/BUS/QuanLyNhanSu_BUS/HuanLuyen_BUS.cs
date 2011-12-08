using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class HuanLuyen_BUS
    {
        public static List<HuanLuyen> LayDSHuanLuyen()
        {
            return HuanLuyen_DAO.LayDSHuanLuyen();
        }

        public static HuanLuyen TraCuuHuanLuyenTheoMa(int iMa)
        {
            return HuanLuyen_DAO.TraCuuHuanLuyenTheoMa(iMa);
        }

        public static HuanLuyen TraCuuHuanLuyenTheoTen(string sTen)
        {
            return HuanLuyen_DAO.TraCuuHuanLuyenTheoTen(sTen);
        }

        public static bool Insert(HuanLuyen dto)
        {
            return HuanLuyen_DAO.Insert(dto);
        }

        public static bool Delete(int iMa)
        {
            return HuanLuyen_DAO.Delete(iMa);
        }

        public static bool UpdateHuanLuyenInfo(HuanLuyen dto)
        {
            return HuanLuyen_DAO.UpdateHuanLuyenInfo(dto);
        }
    }
}
