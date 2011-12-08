using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class LoaiHinh_BUS
    {
        public static List<LoaiHinh> LayDSLoaiHinh()
        {
            return LoaiHinh_DAO.LayDSLoaiHinh();
        }

        public static List<LoaiHinh> TraCuuDSLoaiHinhTheoTen(string sTen)
        {
            return LoaiHinh_DAO.TraCuuDSLoaiHinhTheoTen(sTen);
        }

        public static List<LoaiHinh> TraCuuDSLoaiHinhTheoMaNhomLoaiHinh(String sMa)
        {
            return LoaiHinh_DAO.TraCuuDSLoaiHinhTheoMaNhomLoaiHinh(sMa);
        }

        public static List<LoaiHinh> TraCuuDSLoaiHinhTheoTenNhomLoaiHinh(String sTen)
        {
            return LoaiHinh_DAO.TraCuuDSLoaiHinhTheoTenNhomLoaiHinh(sTen);
        }

        public static LoaiHinh TraCuuLoaiHinhTheoMa(string sMa)
        {
            return LoaiHinh_DAO.TraCuuLoaiHinhTheoMa(sMa);
        }

        public static LoaiHinh TraCuuLoaiHinhTheoTen(string sTen)
        {
            return LoaiHinh_DAO.TraCuuLoaiHinhTheoTen(sTen);
        }

        public static bool Insert(LoaiHinh dto)
        {
            return LoaiHinh_DAO.Insert(dto);
        }

        public static bool Delete(string sMa)
        {
            return LoaiHinh_DAO.Delete(sMa);
        }

        public static bool UpdateLoaiHinhInfo(LoaiHinh dto)
        {
            return LoaiHinh_DAO.UpdateLoaiHinhInfo(dto);
        }
    }
}
