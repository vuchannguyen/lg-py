using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DanhSachGame_DAO
    {
        public static List<DanhSachGame_DTO> SelectDanhSachGame()
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();

                SqlCommand command = connection.CreateCommand();
                //int MaMay = int.Parse(this.cbMay.SelectedValue.ToString());
                command.CommandText = "SELECT * FROM TROCHOI";
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                connection.Close();

                List<DanhSachGame_DTO> lGame = new List<DanhSachGame_DTO>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DanhSachGame_DTO Game = new DanhSachGame_DTO();
                    Game.Ma = dt.Rows[i]["Ma"].ToString();
                    Game.Ten = dt.Rows[i]["Ten"].ToString();
                    Game.NguoiChoi = int.Parse(dt.Rows[i]["NguoiChoi"].ToString());
                    Game.TheLoai = dt.Rows[i]["TheLoai"].ToString();
                    Game.GhiChu = dt.Rows[i]["GhiChu"].ToString();

                    lGame.Add(Game);
                }
                return lGame;
            }
            catch
            {
                return null;
            }
        }

        public static List<DanhSachGame_DTO> SelectDanhSachGame_May(String sMaMay)
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = String.Format("SELECT * FROM TROCHOI G, GAME_MAY GM WHERE G.MA = GM.MAGAME AND GM.MAMAY = {0}", sMaMay);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                connection.Close();

                List<DanhSachGame_DTO> lGame = new List<DanhSachGame_DTO>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DanhSachGame_DTO Game = new DanhSachGame_DTO();
                    Game.Ma = dt.Rows[i]["Ma"].ToString();
                    Game.Ten = dt.Rows[i]["Ten"].ToString();
                    Game.NguoiChoi = int.Parse(dt.Rows[i]["NguoiChoi"].ToString());
                    Game.TheLoai = dt.Rows[i]["TheLoai"].ToString();
                    Game.GhiChu = dt.Rows[i]["GhiChu"].ToString();

                    lGame.Add(Game);
                }
                return lGame;
            }
            catch
            {
                return null;
            }
        }

        public static bool AddGame(String sMa, String sTen, String sNguoiChoi, String sTheLoai, String sGhiChu)
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();
                String cmdStr = "INSERT INTO TROCHOI (Ma, Ten, NguoiChoi, TheLoai, GhiChu) values (" + "@Ma, @Ten, @NguoiChoi, @TheLoai, @GhiChu)";
                SqlCommand cmd = new SqlCommand(cmdStr, connection);

                cmd.Parameters.AddWithValue("@Ma", sMa);
                cmd.Parameters.AddWithValue("@Ten", sTen);
                cmd.Parameters.AddWithValue("@NguoiChoi", int.Parse(sNguoiChoi));
                cmd.Parameters.AddWithValue("@TheLoai", sTheLoai);
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

        public static bool DeleteGame(String sMaGame)
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();
                String cmdStr = String.Format("DELETE FROM TROCHOI WHERE MA = '{0}'", sMaGame);
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

        public static bool UpdateGame(String sMa, String sTen, String sNguoiChoi, String sTheLoai, String sGhiChu)
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();
                String cmdStr = String.Format(@"UPDATE TROCHOI SET TEN = N'{0}', NGUOICHOI = {1}, THELOAI = N'{2}', GHICHU = N'{3}' WHERE MA = '{4}'", sTen, sNguoiChoi, sTheLoai, sGhiChu, sMa);
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

        public static List<DanhSachGame_DTO> FindGame(String sTen)
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();
                String cmdStr = String.Format(@"SELECT * FROM TROCHOI WHERE TEN LIKE N'%{0}%'", sTen);
                SqlCommand cmd = new SqlCommand(cmdStr, connection);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                connection.Close();

                List<DanhSachGame_DTO> lGame = new List<DanhSachGame_DTO>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DanhSachGame_DTO Game = new DanhSachGame_DTO();
                    Game.Ma = dt.Rows[i]["Ma"].ToString();
                    Game.Ten = dt.Rows[i]["Ten"].ToString();
                    Game.NguoiChoi = int.Parse(dt.Rows[i]["NguoiChoi"].ToString());
                    Game.TheLoai = dt.Rows[i]["TheLoai"].ToString();
                    Game.GhiChu = dt.Rows[i]["GhiChu"].ToString();

                    lGame.Add(Game);
                }
                return lGame;
            }
            catch
            {
                return null;
            }
        }

        public static List<DanhSachGame_DTO> FindGameMay(String sTen, String sMaMay)
        {
            try
            {
                SqlConnection connection = SQLDBConnection.CreateSQlConnection();
                String cmdStr = String.Empty;
                if (sMaMay == "Tất cả")
                {
                    cmdStr = String.Format(@"SELECT * FROM TROCHOI TC, GAME_MAY GM WHERE TC.TEN LIKE N'%{0}%' AND GM.MAGAME = TC.MA", sTen);
                }
                else
                {
                    cmdStr = String.Format(@"SELECT * FROM TROCHOI TC, GAME_MAY GM WHERE TC.TEN LIKE N'%{0}%' AND GM.MAMAY = N'{1}' AND GM.MAGAME = TC.MA", sTen, sMaMay);
                }
                SqlCommand cmd = new SqlCommand(cmdStr, connection);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                connection.Close();

                List<DanhSachGame_DTO> lGame = new List<DanhSachGame_DTO>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DanhSachGame_DTO Game = new DanhSachGame_DTO();
                    Game.Ten = dt.Rows[i]["Ten"].ToString();
                    Game.NguoiChoi = int.Parse(dt.Rows[i]["NguoiChoi"].ToString());
                    Game.TheLoai = dt.Rows[i]["TheLoai"].ToString();
                    Game.MaMay = dt.Rows[i]["MaMay"].ToString();
                    Game.GhiChu = dt.Rows[i]["GhiChu"].ToString();

                    lGame.Add(Game);
                }
                return lGame;
            }
            catch (Exception EX)
            {
                throw EX;
                return null;
            }
        }
    }
}
