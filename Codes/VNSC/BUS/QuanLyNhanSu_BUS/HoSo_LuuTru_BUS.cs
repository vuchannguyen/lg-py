using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class HoSo_LuuTru_BUS
    {
        public static List<HoSo_LuuTru> LayDSHoSo_LuuTru()
        {
            return HoSo_LuuTru_DAO.LayDSHoSo_LuuTru();
        }

        public static List<HoSo_LuuTru> TraCuuDSLuuTruTheoMaHoSo(string sMa)
        {
            return HoSo_LuuTru_DAO.TraCuuDSLuuTruTheoMaHoSo(sMa);
        }

        public static bool Insert(HoSo_LuuTru dto)
        {
            return HoSo_LuuTru_DAO.Insert(dto);
        }

        public static bool Delete(string sMaHoSo, int iMaLuuTru)
        {
            return HoSo_LuuTru_DAO.Delete(sMaHoSo, iMaLuuTru);
        }

        public static bool DeleteAll(string sMaHoSo)
        {
            return HoSo_LuuTru_DAO.DeleteAll(sMaHoSo);
        }
    }
}
