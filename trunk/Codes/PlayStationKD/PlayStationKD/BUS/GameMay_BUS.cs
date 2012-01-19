using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class GameMay_BUS
    {
        public static bool AddGameMay(String sMaGame, String sMaMay)
        {
            return GameMay_DAO.AddGameMay(sMaGame, sMaMay);
        }

        public static bool DeleteGameMay(String sMaGame, String sMaMay)
        {
            return GameMay_DAO.DeleteGameMay(sMaGame, sMaMay);
        }
    }
}
