using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class DonViHanhChanh_BUS
    {
        public static List<DonViHanhChanh> LayDSDonViHanhChanh()
        {
            return DonViHanhChanh_DAO.LayDSDonViHanhChanh();
        }

        public static List<DonViHanhChanh> TraCuuDSDonViHanhChanhTheoMaSuKien(int iMaSuKien)
        {
            return DonViHanhChanh_DAO.TraCuuDSDonViHanhChanhTheoMaSuKien(iMaSuKien);
        }

        public static List<DonViHanhChanh> TraCuuDSDonViHanhChanhTheoCapQuanTriNhoHon(int iMaSuKien, int iCap)
        {
            return DonViHanhChanh_DAO.TraCuuDSDonViHanhChanhTheoCapQuanTriNhoHon(iMaSuKien, iCap);
        }

        public static List<DonViHanhChanh> TraCuuDSDonViHanhChanhTheoCapQuanTriLonHon(int iMaSuKien, int iCap)
        {
            return DonViHanhChanh_DAO.TraCuuDSDonViHanhChanhTheoCapQuanTriLonHon(iMaSuKien, iCap);
        }

        public static List<DonViHanhChanh> TraCuuDSDonViHanhChanhTheoTen(string sTen)
        {
            return DonViHanhChanh_DAO.TraCuuDSDonViHanhChanhTheoTen(sTen);
        }

        public static DonViHanhChanh TraCuuDonViHanhChanhTheoMa(int iMa)
        {
            return DonViHanhChanh_DAO.TraCuuDonViHanhChanhTheoMa(iMa);
        }

        public static DonViHanhChanh TraCuuDonViHanhChanhTheoTen(string sTen)
        {
            return DonViHanhChanh_DAO.TraCuuDonViHanhChanhTheoTen(sTen);
        }

        public static bool Insert(DonViHanhChanh dto)
        {
            return DonViHanhChanh_DAO.Insert(dto);
        }

        public static bool Delete(int iMa)
        {
            return DonViHanhChanh_DAO.Delete(iMa);
        }

        public static bool UpdateDonViHanhChanhInfo(DonViHanhChanh dto)
        {
            return DonViHanhChanh_DAO.UpdateDonViHanhChanhInfo(dto);
        }
    }
}
