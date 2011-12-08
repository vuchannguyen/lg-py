using System;
using System.Collections.Generic;
using System.Text;
using DAO;
using DTO;

namespace BUS
{
    public class IDV_BUS
    {
        public static List<IDV_DTO> LayDSIDV()
        {
            return IDV_DAO.LayDSIDV();
        }
    }
}
