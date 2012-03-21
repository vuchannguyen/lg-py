using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;

namespace DAO
{
    public class SQLConnection
    {
        public static bool bWindowsAuthentication;
        public static string sServerName;
        public static string sUserName;
        public static string sPassword;

        public static QLKDDataContext CreateSQlConnection()
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
