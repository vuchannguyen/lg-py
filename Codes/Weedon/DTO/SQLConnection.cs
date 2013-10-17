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

        //private static readonly string CONNECTION_STRING = "Data Source=" + serverName + ";Initial Catalog=Weedon;Integrated Security=True";

        //Weedon Data Context
        protected static WeedonDataContext dbContext;
        public SQLConnection()
        {
            // set command time out by seconds
            dbContext = new WeedonDataContext();
            dbContext.CommandTimeout = 100;
        }

        public static bool CreateSQlConnection()
        {
            try
            {
                dbContext = new WeedonDataContext();
                dbContext.CommandTimeout = 100;

                if (windowsAuthentication)
                {
                    dbContext = new WeedonDataContext("Data Source=" + serverName + ";Initial Catalog=Weedon;Integrated Security=True");
                }
                else
                {
                    dbContext = new WeedonDataContext("Data Source=" + serverName + ";Initial Catalog=Weedon;User ID=" + userName + ";Password=" + password + ";");
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
