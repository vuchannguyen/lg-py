using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class NhomTrachVu_BUS
    {
        public static List<NhomTrachVu> LayDSNhomTrachVu()
        {
            return NhomTrachVu_DAO.LayDSNhomTrachVu();
        }

        public static List<NhomTrachVu> TraCuuDSNhomTrachVuTheoTen(string sTen)
        {
            return NhomTrachVu_DAO.TraCuuDSNhomTrachVuTheoTen(sTen);
        }

        public static NhomTrachVu TraCuuNhomTrachVuTheoMa(string sMa)
        {
            return NhomTrachVu_DAO.TraCuuNhomTrachVuTheoMa(sMa);
        }

        public static NhomTrachVu TraCuuNhomTrachVuTheoTen(string sTen)
        {
            return NhomTrachVu_DAO.TraCuuNhomTrachVuTheoTen(sTen);
        }

        public static bool Insert(NhomTrachVu dto)
        {
            return NhomTrachVu_DAO.Insert(dto);
        }

        public static bool Delete(string sMa)
        {
            return NhomTrachVu_DAO.Delete(sMa);
        }

        public static bool UpdateNhomTrachVuInfo(NhomTrachVu dto)
        {
            return NhomTrachVu_DAO.UpdateNhomTrachVuInfo(dto);
        }
    }
}
