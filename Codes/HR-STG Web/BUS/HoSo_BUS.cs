using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DTO;
using DAO;

namespace BUS
{
    public class HoSo_BUS
    {
        public static List<HoSo_DTO> LayDSHoSo()
        {
            return HoSo_DAO.LayDSHoSo();
        }

        public static HoSo_DTO TraCuuHoSoTheoMa(int iMa)
        {
            return HoSo_DAO.TraCuuHoSoTheoMa(iMa);
        }

        public static string Insert(HoSo_DTO dto)
        {
            return HoSo_DAO.Insert(dto);
        }

        public static string Insert2String(HoSo_DTO dto)
        {
            return HoSo_DAO.Insert2String(dto);
        }

        public static string Delete(int iMa)
        {
            return HoSo_DAO.Delete(iMa);
        }

        public static string UpdateHoSoInfo(HoSo_DTO dto)
        {
            return HoSo_DAO.UpdateHoSoInfo(dto);
        }
    }
}
