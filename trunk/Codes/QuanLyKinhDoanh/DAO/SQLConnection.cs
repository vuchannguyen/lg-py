using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;

namespace DAO
{
    public class SQLConnection
    {
        private bool bWindowsAuthentication;

        public bool BWindowsAuthentication
        {
            get { return bWindowsAuthentication; }
            set { bWindowsAuthentication = value; }
        }

        private string sServerName;

        public string SServerName
        {
            get { return sServerName; }
            set { sServerName = value; }
        }

        private string sUserName;

        public string SUserName
        {
            get { return sUserName; }
            set { sUserName = value; }
        }

        private string sPassword;

        public string SPassword
        {
            get { return sPassword; }
            set { sPassword = value; }
        }

        private static readonly string serverName = ".\\SQLEXPRESS";

        private static readonly string CONNECTION_STRING = "Data Source=" + serverName + ";Initial Catalog=QuanLyKinhDoanh;Integrated Security=True";
        // CRM Data Context
        protected static QLKDDataContext dbContext = new QLKDDataContext(CONNECTION_STRING);

        public SQLConnection()
        {
            // set command time out by seconds
            dbContext.CommandTimeout = 300;

            dbContext = new QLKDDataContext(CONNECTION_STRING);
        }

        public static void CreateSQlConnection()
        {
            try
            {
                dbContext = new QLKDDataContext(CONNECTION_STRING);
            }
            catch
            {
                return;
            }
        }
    }
}
