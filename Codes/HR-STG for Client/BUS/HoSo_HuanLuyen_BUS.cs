using System;
using System.Collections.Generic;
using System.Text;
using DAO;
using DTO;

namespace BUS
{
    public class HoSo_HuanLuyen_BUS
    {
        public static List<HoSo_HuanLuyen_DTO> LayDSHoSo_HuanLuyen()
        {
            return HoSo_HuanLuyen_DAO.LayDSHoSo_HuanLuyen();
        }

        public static List<HoSo_HuanLuyen_DTO> TraCuuDSHuanLuyenTheoMaHoSo(int iMa)
        {
            return HoSo_HuanLuyen_DAO.TraCuuDSHuanLuyenTheoMaHoSo(iMa);
        }

        public static string Insert(HoSo_HuanLuyen_DTO dto)
        {
            return HoSo_HuanLuyen_DAO.Insert(dto);
        }

        public static string Insert2String(HoSo_HuanLuyen_DTO dto)
        {
            return HoSo_HuanLuyen_DAO.Insert2String(dto);
        }

        public static string Delete(int iMaHoSo, int iMaHuanLuyen)
        {
            return HoSo_HuanLuyen_DAO.Delete(iMaHoSo, iMaHuanLuyen);
        }
    }
}
