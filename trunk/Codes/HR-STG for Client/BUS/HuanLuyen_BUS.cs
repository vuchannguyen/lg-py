using System;
using System.Collections.Generic;
using System.Text;
using DAO;
using DTO;

namespace BUS
{
    public class HuanLuyen_BUS
    {
        public static List<HuanLuyen_DTO> LayDSHuanLuyen()
        {
            return HuanLuyen_DAO.LayDSHuanLuyen();
        }

        public static HuanLuyen_DTO TraCuuHuanLuyenTheoMa(int iMa)
        {
            return HuanLuyen_DAO.TraCuuHuanLuyenTheoMa(iMa);
        }

        public static string Insert(HuanLuyen_DTO dto)
        {
            return HuanLuyen_DAO.Insert(dto);
        }

        public static string Insert2String(HuanLuyen_DTO dto)
        {
            return HuanLuyen_DAO.Insert2String(dto);
        }

        public static string Delete(int iMa)
        {
            return HuanLuyen_DAO.Delete(iMa);
        }

        public static string UpdateHuanLuyenInfo(HuanLuyen_DTO dto)
        {
            return HuanLuyen_DAO.UpdateHuanLuyenInfo(dto);
        }
    }
}
