using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class DieuPhoi_BUS
    {
        public static List<DieuPhoi> LayDSDieuPhoi()
        {
            return DieuPhoi_DAO.LayDSDieuPhoi();
        }

        public static List<DieuPhoi> TraCuuDSDieuPhoiTheoMaSuKien(int iMaSuKien)
        {
            return DieuPhoi_DAO.TraCuuDSDieuPhoiTheoMaSuKien(iMaSuKien);
        }

        public static List<DieuPhoi> TraCuuDSDieuPhoiTheoMaSuKien_HoSo(int iMaSuKien, string sMaSuKien_HoSo)
        {
            return DieuPhoi_DAO.TraCuuDSDieuPhoiTheoMaSuKien_HoSo(iMaSuKien, sMaSuKien_HoSo);
        }

        public static List<DieuPhoi> TraCuuDSDieuPhoiTheoMaSuKien_DonViHanhChanh_TrachVuSuKien(int iMa, int iMaDonViHanhChanh, int iMaTrachVuSuKien)
        {
            return DieuPhoi_DAO.TraCuuDSDieuPhoiTheoMaSuKien_DonViHanhChanh_TrachVuSuKien(iMa, iMaDonViHanhChanh, iMaTrachVuSuKien);
        }

        public static List<DieuPhoi> TraCuuDSDieuPhoiTheoTen(string sTen)
        {
            return DieuPhoi_DAO.TraCuuDSDieuPhoiTheoTen(sTen);
        }

        public static DieuPhoi TraCuuDieuPhoiTheoMa(int iMa)
        {
            return DieuPhoi_DAO.TraCuuDieuPhoiTheoMa(iMa);
        }

        //public static DieuPhoi TraCuuDieuPhoiTheoTen(string sTen)
        //{
        //    return DieuPhoi_DAO.TraCuuDieuPhoiTheoTen(sTen);
        //}

        public static bool Insert(DieuPhoi dto)
        {
            return DieuPhoi_DAO.Insert(dto);
        }

        public static bool Delete(int iMa)
        {
            return DieuPhoi_DAO.Delete(iMa);
        }

        public static bool UpdateDieuPhoiInfo(DieuPhoi dto)
        {
            return DieuPhoi_DAO.UpdateDieuPhoiInfo(dto);
        }
    }
}
