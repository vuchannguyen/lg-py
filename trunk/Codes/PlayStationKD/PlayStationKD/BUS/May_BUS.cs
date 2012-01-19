using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class May_BUS
    {
        public static List<May_DTO> SelectMay()
        {
            return May_DAO.SelectMay();
        }

        public static bool AddMay(String sMa, String sGhiChu)
        {
            return May_DAO.AddMay(sMa, sGhiChu);
        }

        public static bool DeleteMay(String sMaMay)
        {
            return May_DAO.DeleteMay(sMaMay);
        }

        public static bool UpdateMay(String sMaMay, String sGhiChu)
        {
            return May_DAO.UpdateMay(sMaMay, sGhiChu);
        }
    }
}
