using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class TrachVu_BUS
    {
        public static List<TrachVu> LayDSTrachVu()
        {
            return TrachVu_DAO.LayDSTrachVu();
        }

        public static List<TrachVu> TraCuuDSTrachVuTheoTen(string sTen)
        {
            return TrachVu_DAO.TraCuuDSTrachVuTheoTen(sTen);
        }

        public static List<TrachVu> TraCuuDSTrachVuTheoMaNhomTrachVu(String sMa)
        {
            return TrachVu_DAO.TraCuuDSTrachVuTheoMaNhomTrachVu(sMa);
        }

        public static List<TrachVu> TraCuuDSTrachVuTheoTenNhomTrachVu(String sTen)
        {
            return TrachVu_DAO.TraCuuDSTrachVuTheoTenNhomTrachVu(sTen);
        }

        public static TrachVu TraCuuTrachVuTheoMa(string sMa)
        {
            return TrachVu_DAO.TraCuuTrachVuTheoMa(sMa);
        }

        public static TrachVu TraCuuTrachVuTheoTen(string sTen)
        {
            return TrachVu_DAO.TraCuuTrachVuTheoTen(sTen);
        }

        public static bool Insert(TrachVu dto)
        {
            return TrachVu_DAO.Insert(dto);
        }

        public static bool Delete(string sMa)
        {
            return TrachVu_DAO.Delete(sMa);
        }

        public static bool UpdateTrachVuInfo(TrachVu dto)
        {
            return TrachVu_DAO.UpdateTrachVuInfo(dto);
        }
    }
}
