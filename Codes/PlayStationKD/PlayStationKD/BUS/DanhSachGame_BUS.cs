using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class DanhSachGame_BUS
    {
        public static List<DanhSachGame_DTO> SelectDanhSachGame()
        {
            return DanhSachGame_DAO.SelectDanhSachGame();
        }

        public static List<DanhSachGame_DTO> SelectDanhSachGame_May(String sMaMay)
        {
            return DanhSachGame_DAO.SelectDanhSachGame_May(sMaMay);
        }

        public static bool AddGame(String sMa, String sTen, String sNguoiChoi, String sTheLoai, String sGhiChu)
        {
            return DanhSachGame_DAO.AddGame(sMa, sTen, sNguoiChoi, sTheLoai, sGhiChu);
        }

        public static bool DeleteGame(String sMaGame)
        {
            return DanhSachGame_DAO.DeleteGame(sMaGame);
        }

        public static bool UpdateGame(String sMa, String sTen, String sNguoiChoi, String sTheLoai, String sGhiChu)
        {
            return DanhSachGame_DAO.UpdateGame(sMa, sTen, sNguoiChoi, sTheLoai, sGhiChu);
        }

        public static List<DanhSachGame_DTO> FindGame(String sTen)
        {
            return DanhSachGame_DAO.FindGame(sTen);
        }

        public static List<DanhSachGame_DTO> FindGameMay(String sTen, String sMaMay)
        {
            return DanhSachGame_DAO.FindGameMay(sTen, sMaMay);
        }
    }
}
