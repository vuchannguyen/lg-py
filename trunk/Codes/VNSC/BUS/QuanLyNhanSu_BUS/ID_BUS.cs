using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class IDV_BUS
    {
        public static List<IDV> LayDSIDV()
        {
            return IDV_DAO.LayDSIDV();
        }

        public static List<IDV> TraCuuDSIDVTheoDienGiai(string sDienGiai)
        {
            return IDV_DAO.TraCuuDSIDVTheoDienGiai(sDienGiai);
        }

        public static IDV TraCuuIDVTheoMa(int iMa)
        {
            return IDV_DAO.TraCuuIDVTheoMa(iMa);
        }

        public static IDV TraCuuIDVTheoDienGiai(string sDienGiai)
        {
            return IDV_DAO.TraCuuIDVTheoDienGiai(sDienGiai);
        }

        public static bool Insert(IDV dto)
        {
            return IDV_DAO.Insert(dto);
        }

        public static bool Delete(int iMa)
        {
            return IDV_DAO.Delete(iMa);
        }

        public static bool UpdateIDVInfo(IDV dto)
        {
            return IDV_DAO.UpdateIDVInfo(dto);
        }
    }
}
