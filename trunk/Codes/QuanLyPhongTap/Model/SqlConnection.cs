using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SqlConnection
    {
        public static bool WindowsAuthentication;
        public static string ServerName;
        public static string UserName;
        public static string Password;
        protected static DatabaseDataContext dbContext;

        public SqlConnection()
        {
            //Not implement
        }

        public static bool NewConnection()
        {
            try
            {
                dbContext = new DatabaseDataContext();
                dbContext.CommandTimeout = 100;

                if (WindowsAuthentication)
                {
                    dbContext = new DatabaseDataContext(String.Format("Data Source={0};Initial Catalog=QuanLyPhongTap;Integrated Security=True", ServerName));
                }
                else
                {
                    dbContext = new DatabaseDataContext(String.Format("Data Source={0};Initial Catalog=QuanLyPhongTap;User ID={1};Password={2};", ServerName, UserName, Password));
                }

                var query = from data in dbContext.Users
                            select data;

                return query.Count() >= 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void OpenConnection()
        {
            try
            {
                if (dbContext.Connection.State != System.Data.ConnectionState.Open)
                {
                    dbContext.Connection.Open();
                }

                if (dbContext.Transaction == null || dbContext.Transaction.Connection == null)
                {
                    dbContext.Transaction = dbContext.Connection.BeginTransaction();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CloseConnection(bool isSuccess)
        {
            try
            {
                if (dbContext.Transaction != null)
                {
                    if (isSuccess)
                    {
                        dbContext.Transaction.Commit();
                    }
                    else
                    {
                        dbContext.Transaction.Rollback();
                    }

                    dbContext.Transaction.Dispose();
                }

                dbContext.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
