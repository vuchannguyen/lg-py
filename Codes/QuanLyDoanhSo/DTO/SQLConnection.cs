using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class SQLConnection
    {
        public static bool windowsAuthentication;
        public static string serverName;
        public static string userName;
        public static string password;

        //private static readonly string serverName = @".\";
        //private static readonly string serverName = @".\SQLEXPRESS";

        //private static readonly string CONNECTION_STRING = "Data Source=" + serverName + ";Initial Catalog=QuanLyDoanhSo;Integrated Security=True";

        //QuanLyDoanhSo Data Context
        protected static DoanhSoDataContext dbContext;
        public SQLConnection()
        {
            // set command time out by seconds
            dbContext = new DoanhSoDataContext();
            dbContext.CommandTimeout = 100;
        }

        public static bool CreateSQlConnection()
        {
            try
            {
                dbContext = new DoanhSoDataContext();
                dbContext.CommandTimeout = 100;

                if (windowsAuthentication)
                {
                    dbContext = new DoanhSoDataContext("Data Source=" + serverName + ";Initial Catalog=QuanLyDoanhSo;Integrated Security=True");
                }
                else
                {
                    dbContext = new DoanhSoDataContext("Data Source=" + serverName + ";Initial Catalog=QuanLyDoanhSo;User ID=" + userName + ";Password=" + password + ";");
                }

                var sql = from data in dbContext.Users
                          select data;

                return sql.Count() >= 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
