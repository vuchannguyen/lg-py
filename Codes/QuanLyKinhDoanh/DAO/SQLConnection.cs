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

        private static readonly string CONNECTION_STRING = "Data Source=" + serverName + ";Initial Catalog=VNSC_DB;Integrated Security=True";
        // CRM Data Context
        protected QLKDDataContext dbContext = new QLKDDataContext(CONNECTION_STRING);

        public SQLConnection()
        {
            // set command time out by seconds
            dbContext.CommandTimeout = 300;
        }

        public QLKDDataContext CreateSQlConnection()
        {
            try
            {
                QLKDDataContext Test;
                if (bWindowsAuthentication)
                {
                    Test = new QLKDDataContext("Data Source=" + sServerName + ";Initial Catalog=VNSC_DB;Integrated Security=True");
                }
                else
                {
                    Test = new QLKDDataContext("Data Source=" + sServerName + ";Initial Catalog=VNSC_DB;User ID=" + sUserName + ";Password=" + sPassword + ";");
                }

                //List<HoSo> list = new List<HoSo>();

                //var q = from p in Test.HoSos
                //        select p;

                //foreach (HoSo k in q)
                //{
                //    break;
                //}

                return Test;
            }
            catch
            {
                return null;
            }
        }
    }
}
