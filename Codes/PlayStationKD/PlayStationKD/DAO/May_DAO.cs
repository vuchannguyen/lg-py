using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class May_DAO
    {
        public static List<May_DTO> SelectMay()
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM MAY";
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                connection.Close();

                List<May_DTO> lMay = new List<May_DTO>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    May_DTO Game = new May_DTO();
                    Game.Ma = dt.Rows[i]["Ma"].ToString();
                    Game.GhiChu = dt.Rows[i]["GhiChu"].ToString();

                    lMay.Add(Game);
                }
                return lMay;
            }
            catch
            {
                return null;
            }
        }

        public static bool AddMay(String sMa, String sGhiChu)
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();
                String cmdStr = "INSERT INTO MAY (Ma, GhiChu) values (" + "@Ma, @GhiChu)";
                SqlCommand cmd = new SqlCommand(cmdStr, connection);

                cmd.Parameters.AddWithValue("@Ma", sMa);
                //cmd.Parameters.AddWithValue("@Ten", sTen);
                //cmd.Parameters.AddWithValue("@NguoiChoi", int.Parse(sNguoiChoi));
                //cmd.Parameters.AddWithValue("@TheLoai", sTheLoai);
                cmd.Parameters.AddWithValue("@GhiChu", sGhiChu);
                cmd.ExecuteNonQuery();

                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteMay(String sMaMay)
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();
                String cmdStr = String.Format("DELETE FROM MAY WHERE MA = '{0}'", sMaMay);
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

        public static bool UpdateMay(String sMaMay, String sGhiChu)
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();
                String cmdStr = String.Format(@"UPDATE MAY SET GHICHU = N'{0}' WHERE MA = '{1}'", sGhiChu, sMaMay);
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
