using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class SuKien_DonViHanhChanh_BUS
    {
        public static List<SuKien_DonViHanhChanh> LayDSSuKien_DonViHanhChanh()
        {
            return SuKien_DonViHanhChanh_DAO.LayDSSuKien_DonViHanhChanh();
        }

        public static List<SuKien_DonViHanhChanh> TraCuuDSSuKien_DonViHanhChanhTheoMaSuKien(int iMa)
        {
            return SuKien_DonViHanhChanh_DAO.TraCuuDSSuKien_DonViHanhChanhTheoMaSuKien(iMa);
        }

        public static bool Insert(SuKien_DonViHanhChanh dto)
        {
            return SuKien_DonViHanhChanh_DAO.Insert(dto);
        }

        public static bool Delete(int iMaSuKien, int iMaDonViHanhChanh)
        {
            return SuKien_DonViHanhChanh_DAO.Delete(iMaSuKien, iMaDonViHanhChanh);
        }

        public static bool DeleteAll(int iMaSuKien)
        {
            return SuKien_DonViHanhChanh_DAO.DeleteAll(iMaSuKien);
        }
    }
}
