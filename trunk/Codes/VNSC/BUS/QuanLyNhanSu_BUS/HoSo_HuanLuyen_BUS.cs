using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class HoSo_HuanLuyen_BUS
    {
        public static List<HoSo_HuanLuyen> LayDSHoSo_HuanLuyen()
        {
            return HoSo_HuanLuyen_DAO.LayDSHoSo_HuanLuyen();
        }

        public static List<HoSo_HuanLuyen> TraCuuDSHuanLuyenTheoMaHoSo(string sMa)
        {
            return HoSo_HuanLuyen_DAO.TraCuuDSHuanLuyenTheoMaHoSo(sMa);
        }

        public static bool Insert(HoSo_HuanLuyen dto)
        {
            return HoSo_HuanLuyen_DAO.Insert(dto);
        }

        public static bool Delete(string sMaHoSo, int iMaHuanLuyen)
        {
            return HoSo_HuanLuyen_DAO.Delete(sMaHoSo, iMaHuanLuyen);
        }

        public static bool DeleteAll(string sMaHoSo)
        {
            return HoSo_HuanLuyen_DAO.DeleteAll(sMaHoSo);
        }
    }
}
