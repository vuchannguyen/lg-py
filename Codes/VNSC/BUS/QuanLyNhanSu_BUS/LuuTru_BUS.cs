using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class LuuTru_BUS
    {
        public static List<LuuTru> LayDSLuuTru()
        {
            return LuuTru_DAO.LayDSLuuTru();
        }

        public static LuuTru TraCuuLuuTruTheoMa(int iMa)
        {
            return LuuTru_DAO.TraCuuLuuTruTheoMa(iMa);
        }

        public static LuuTru TraCuuLuuTruTheoNgay(string sNgayCapNhat)
        {
            return LuuTru_DAO.TraCuuLuuTruTheoNgay(sNgayCapNhat);
        }

        public static bool Insert(LuuTru dto)
        {
            return LuuTru_DAO.Insert(dto);
        }

        public static bool Delete(int iMa)
        {
            return LuuTru_DAO.Delete(iMa);
        }
    }
}
