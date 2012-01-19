using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class GameMay_DAO
    {
        public static bool AddGameMay(String sMaGame, String sMaMay)
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();
                String cmdStr = "INSERT INTO Game_May (MaGame, MaMay) values (" + "@MaGame, @MaMay)";
                SqlCommand cmd = new SqlCommand(cmdStr, connection);

                cmd.Parameters.AddWithValue("@MaGame", sMaGame);
                cmd.Parameters.AddWithValue("@MaMay", sMaMay);
                cmd.ExecuteNonQuery();

                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteGameMay(String sMaGame, String sMaMay)
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();
                String cmdStr = String.Format("DELETE FROM GAME_MAY WHERE MAGAME = '{0}' AND MAMAY = '{1}'", sMaGame, sMaMay);
                SqlCommand cmd = new SqlCommand(cmdStr, connection);
                cmd.ExecuteNonQuery();

                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
