using System;
using System.Collections.Generic;
using System.Text;
using DAO;
using DTO;

namespace BUS
{
    public class SuKien_BUS
    {
        public static SuKien_DTO LaySuKien()
        {
            return SuKien_DAO.LaySuKien();
        }
    }
}
