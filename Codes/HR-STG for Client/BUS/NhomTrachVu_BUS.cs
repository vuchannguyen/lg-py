using System;
using System.Collections.Generic;
using System.Text;
using DAO;
using DTO;

namespace BUS
{
    public class NhomTrachVu_BUS
    {
        public static List<NhomTrachVu_DTO> LayDSNhomTrachVu()
        {
            return NhomTrachVu_DAO.LayDSNhomTrachVu();
        }

        public static NhomTrachVu_DTO TraCuuNhomTrachVuTheoMa(string sMa)
        {
            return NhomTrachVu_DAO.TraCuuNhomTrachVuTheoMa(sMa);
        }
    }
}
