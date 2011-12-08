using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class NhomLoaiHinh_BUS
    {
        public static List<NhomLoaiHinh> LayDSNhomLoaiHinh()
        {
            return NhomLoaiHinh_DAO.LayDSNhomLoaiHinh();
        }

        public static List<NhomLoaiHinh> TraCuuDSNhomLoaiHinhTheoTen(string sTen)
        {
            return NhomLoaiHinh_DAO.TraCuuDSNhomLoaiHinhTheoTen(sTen);
        }

        public static NhomLoaiHinh TraCuuNhomLoaiHinhTheoMa(string sMa)
        {
            return NhomLoaiHinh_DAO.TraCuuNhomLoaiHinhTheoMa(sMa);
        }

        public static NhomLoaiHinh TraCuuNhomLoaiHinhTheoTen(string sTen)
        {
            return NhomLoaiHinh_DAO.TraCuuNhomLoaiHinhTheoTen(sTen);
        }

        public static bool Insert(NhomLoaiHinh dto)
        {
            return NhomLoaiHinh_DAO.Insert(dto);
        }

        public static bool Delete(string sMa)
        {
            return NhomLoaiHinh_DAO.Delete(sMa);
        }

        public static bool UpdateNhomLoaiHinhInfo(NhomLoaiHinh dto)
        {
            return NhomLoaiHinh_DAO.UpdateNhomLoaiHinhInfo(dto);
        }
    }
}
