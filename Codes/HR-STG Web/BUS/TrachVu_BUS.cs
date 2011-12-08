using System;
using System.Collections.Generic;
using System.Text;
using DAO;
using DTO;

namespace BUS
{
    public class TrachVu_BUS
    {
        public static List<TrachVu_DTO> LayDSTrachVu()
        {
            return TrachVu_DAO.LayDSTrachVu();
        }

        public static TrachVu_DTO TraCuuTrachVuTheoMa(string sMa)
        {
            return TrachVu_DAO.TraCuuTrachVuTheoMa(sMa);
        }
    }
}
