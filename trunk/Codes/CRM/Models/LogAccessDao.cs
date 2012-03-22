using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Common;
using CRM.Library.Common;

namespace CRM.Models
{
    public class LogAccessDao : BaseDao
    {
        public List<LogAccess> GetList(string userAdmin, string from, string to)
        {
            DateTime? dFrom = null;
            if (!string.IsNullOrEmpty(from) && CheckUtil.IsDate(from))
            {
                dFrom = DateTime.Parse(from);
            }

            DateTime? dTo = null;
            if (!string.IsNullOrEmpty(to) && CheckUtil.IsDate(to))
            {
                dTo = DateTime.Parse(to);
            }

            return dbContext.LogAccesses.Where(p =>
                (string.IsNullOrEmpty(userAdmin) || p.UserAdmin.Contains(userAdmin))
                && (dFrom == null || p.DatetimeAccess >= dFrom)
                && (dTo == null || p.DatetimeAccess.AddDays(-1) <= dTo)
                ).ToList<LogAccess>();
        }

        public List<LogAccess> Sort(List<LogAccess> list, string sortColumn, string sortOrder)
        {
            int order;

            if (sortOrder == "desc")
            {
                order = -1;
            }
            else
            {
                order = 1;
            }
            switch (sortColumn)
            {
                case "UserAdmin":
                    list.Sort(
                         delegate(LogAccess m1, LogAccess m2)
                         { return m1.UserAdmin.CompareTo(m2.UserAdmin) * order; });
                    break;
                case "UserIp":
                    list.Sort(
                         delegate(LogAccess m1, LogAccess m2)
                         { return m1.UserIp.CompareTo(m2.UserIp) * order; });
                    break;
                case "DatetimeAccess":
                    list.Sort(
                         delegate(LogAccess m1, LogAccess m2)
                         { return m1.DatetimeAccess.CompareTo(m2.DatetimeAccess) * order; });
                    break;
                case "DatetimeOut":
                    if (order > 0)
                    {
                        return list.OrderBy(p => p.DatetimeOut).ToList<LogAccess>();
                    }
                    else
                    {
                        return list.OrderByDescending(p => p.DatetimeOut).ToList<LogAccess>();
                    }
            }

            return list;
        }

        /// <summary>
        /// Insert New Access
        /// </summary>
        /// <param name="userAdmin"></param>
        /// <param name="userIp"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Message InsertNewAccess(string stSession, string userAdmin, string userIp)
        {
            Message msg = null;
            try
            {
                LogAccess myLog = new LogAccess();
                myLog.Id = Guid.NewGuid();
                myLog.SessionId = stSession;
                myLog.UserIp = userIp;
                myLog.UserAdmin = userAdmin;
                myLog.DatetimeAccess = DateTime.Now;
                dbContext.LogAccesses.InsertOnSubmit(myLog);
                dbContext.SubmitChanges();

                // success message
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Log access", "added");
            }
            catch
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message SetTimeOut(string stSession)
        {
            Message msg = null;
            try
            {
                LogAccess myLog = dbContext.LogAccesses.OrderByDescending(p=>p.DatetimeAccess).Where(p => p.SessionId.Equals(stSession) && p.DatetimeOut == null).FirstOrDefault<LogAccess>();
                if (myLog != null)
                {
                    myLog.DatetimeOut = DateTime.Now;
                    dbContext.SubmitChanges();
                }
                // success message
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Log access", "update");
            }
            catch
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Count By Date
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public int CountByDateMonthYear(DateTime date)
        {
            var sum = from x in dbContext.LogAccesses
                      where x.DatetimeAccess.Day == date.Day && x.DatetimeAccess.Month == date.Month && x.DatetimeAccess.Year == date.Year
                      select x;
            return sum.Count();
        }

        /// <summary>
        /// Count By Month
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public int CountByMonth(DateTime date)
        {
            var sum = from x in dbContext.LogAccesses
                      where x.DatetimeAccess.Month == date.Month && x.DatetimeAccess.Year == date.Year
                      select x;
            return sum.Count();
        }

        /// <summary>
        /// Count By Year
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public int CountByYear(DateTime date)
        {
            var sum = from x in dbContext.LogAccesses
                      where x.DatetimeAccess.Year == date.Year
                      select x;
            return sum.Count();
        }

        /// <summary>
        /// Count By Year
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public long CountTotal()
        {
            return dbContext.LogAccesses.Count();
        }

        /// <summary>
        /// tan.tran 2011.05.25
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public Message DeleteList(int month)
        {
            Message msg = null;
            DbTransaction trans = null;
            long number = 0;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                List<LogAccess> listMaster = dbContext.LogAccesses.Where(p => p.DatetimeAccess.AddMonths(month) <= DateTime.Now).ToList<LogAccess>();

                foreach (LogAccess log in listMaster)
                {
                    dbContext.LogAccesses.DeleteOnSubmit(log);
                    number++;
                }
                dbContext.SubmitChanges();

                // Show succes message
                if (number > 0)
                {
                    msg = new Message(MessageConstants.I0001, MessageType.Info, number.ToString() + " log(s)", "deleted");
                }
                else
                {
                    msg = new Message(MessageConstants.I0010, MessageType.Info);
                }
                trans.Commit();
            }
            catch (Exception)
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
    }


}