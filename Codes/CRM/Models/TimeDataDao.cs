using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Linq.SqlClient;
using System.Linq;
using CRM.Library.Common;
using CRM.Models.Entities;
using System.Linq.Dynamic;

namespace CRM.Models
{
    public class TimeDataDao : BaseDao
    {
        #region Time_ReaderMaster Table
        public List<Time_ReaderMaster> GetListReaderMaster()
        {
            return dbContext.Time_ReaderMasters.ToList();
        } 
        #endregion

        /// <summary>
        /// Get max transdate value in Time_Data table
        /// </summary>
        public DateTime? GetMaxTransDate()
        {
            // try catch to handle case : Time_Data table doesn't store data
            try
            {
                return (from d in dbContext.Time_Datas select d.TransDate).Max();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get min transdate value in Time_Data table
        /// </summary>
        public DateTime? GetMinTransDate()
        {
            // try catch to handle case : Time_Data table doesn't store data
            try
            {
                return (from d in dbContext.Time_Datas select d.TransDate).Min();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Delete exists by mindate and maxdate before Insert new Data
        /// </summary>
        public Message InsertList(List<Time_Data> records, DateTime minDate, DateTime maxDate)
        {
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                // Delete exist by TransDate
                DeleteByTransDate(minDate, maxDate);

                dbContext.Time_Datas.InsertAllOnSubmit(records);
                dbContext.SubmitChanges();

                trans.Commit();
                return new Message(MessageConstants.I0001, MessageType.Info, "Data", "imported");
            }
            catch (Exception ex)
            {
                trans.Rollback();
                if (ex.GetType() == typeof(ArgumentException))
                    return new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
                return new Message(MessageConstants.E0030, MessageType.Error, "File");
            }
        }

        /// <summary>
        /// Delete all datas have transdate between mindate and maxdate
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns></returns>
        public bool DeleteByTransDate(DateTime minDate, DateTime maxDate)
        {
            try
            {
                IEnumerable<Time_Data> timeDatas = dbContext.Time_Datas.Where(p => p.TransDate >= minDate && p.TransDate <= maxDate);
                dbContext.Time_Datas.DeleteAllOnSubmit(timeDatas);
                dbContext.SubmitChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public int GetReportCount(string keyword, int? deptId, int? intTopN, DateTime? dFrom, DateTime? dTo, int? reportKindId, int comeAfter, int leaveBefore)
        {
            if (reportKindId == Constants.TIME_REPORT_KIND_NOT_CHECK)
            {
                if (dFrom == null || dTo == null)
                    return 0;
            }

            return GetQueryReport(keyword, deptId, intTopN, dFrom, dTo, reportKindId, comeAfter, leaveBefore).Count();
        }

        /// <summary>
        /// Getting report without skip take to export excel file
        /// </summary>
        public List<TimeReport> GetReportToExportExcel(string sortColumn, string sortOrder
            , string keyword, int? deptId, int? intTopN, DateTime? dFrom, DateTime? dTo, int? reportKindId, int comeAfter, int leaveBefore)
        {
            if (reportKindId == Constants.TIME_REPORT_KIND_NOT_CHECK)
            {
                if(dFrom == null || dTo == null)
                    return new List<TimeReport>();
            }

            var sql = GetSortedQueryReport(sortColumn, sortOrder, keyword, deptId, intTopN, dFrom, dTo, reportKindId, comeAfter, leaveBefore);

            return sql.ToList();
        }

        public List<TimeReport> GetReport(string sortColumn, string sortOrder, int skip, int take
            , string keyword, int? deptId, int? intTopN, DateTime? dFrom, DateTime? dTo, int? reportKindId, int comeAfter, int leaveBefore)
        {
            if (reportKindId == Constants.TIME_REPORT_KIND_NOT_CHECK)
            {
                if(dFrom == null || dTo == null)
                    return new List<TimeReport>();
            }

            var sql = GetSortedQueryReport(sortColumn, sortOrder, keyword, deptId, intTopN, dFrom, dTo, reportKindId, comeAfter, leaveBefore);

            return sql.Skip(skip).Take(take).ToList();
        }

        private IQueryable<TimeReport> GetSortedQueryReport(string sortColumn, string sortOrder
            , string keyword, int? deptId, int? intTopN, DateTime? dFrom, DateTime? dTo, int? reportKindId, int comeAfter, int leaveBefore)
        {
            var sql = GetQueryReport(keyword, deptId, intTopN, dFrom, dTo, reportKindId, comeAfter, leaveBefore);

            string orderBy;
            switch (sortColumn)
            {
                case "EmpId":
                    orderBy = "EmpId " + sortOrder;
                    break;
                case "EmpName":
                    orderBy = "EmpName " + sortOrder;
                    break;
                case "JobTitle":
                    orderBy = "JobTitle " + sortOrder;
                    break;
                case "Department":
                    orderBy = "DeptName " + sortOrder;
                    break;
                case "Location":
                    orderBy = "LocationCode " + sortOrder;
                    break;
                case "Manager":
                    orderBy = "ManagerName " + sortOrder;
                    break;
                case "Date":
                    orderBy = "TransDate " + sortOrder;
                    break;
                case "TimeIn":
                    orderBy = "TimeIn " + sortOrder;
                    break;
                case "TimeOut":
                    orderBy = "TimeOut " + sortOrder;
                    break;
                default:
                    orderBy = sortColumn + " " + sortOrder;
                    break;
            }

            if (reportKindId == Constants.TIME_REPORT_KIND_FULL)
            {
                // Preventing to sort by TransDate or Time
                if (sortColumn == "Date" || sortColumn == "TimeIn" || sortColumn == "TimeOut")
                {
                    orderBy = "EmpId " + sortOrder;
                }

                orderBy = orderBy + ", TransDate ASC, TimeIn != null ? TimeIn : TimeOut ASC";
            }
            if (!string.IsNullOrEmpty(orderBy))
                sql = sql.OrderBy(orderBy);

            return sql;
        }

        private IQueryable<TimeReport> GetQueryReport(string keyword, int? deptId, int? intTopN, DateTime? dFrom, DateTime? dTo
            , int? reportKindId, int comeAfter, int leaveBefore)
        {
            #region Handling exceptions
            if (keyword != null)
                keyword = "%" + System.Text.RegularExpressions.Regex.Replace(keyword.Trim(), @"\s+", "%") + "%";

            #endregion

            switch (reportKindId)
            {
                case Constants.TIME_REPORT_KIND_FULL:
                    return GetQueryReportFull(keyword, deptId, intTopN, dFrom, dTo);
                case Constants.TIME_REPORT_KIND_NOT_CHECK:
                    return GetQueryReportNotCheck(keyword, deptId, intTopN, dFrom, dTo);
                default:
                    return GetQueryReportNormal(keyword, deptId, intTopN, dFrom, dTo, reportKindId, comeAfter, leaveBefore);
            }
        }

        /// <summary>
        /// Return query of full report kind
        /// </summary>
        private IQueryable<TimeReport> GetQueryReportFull(string keyword, int? deptId, int? intTopN, DateTime? dFrom, DateTime? dTo)
        {
            var sql =
                from e in dbContext.Employees
                // Left join manager
                join m in dbContext.Employees on e.ManagerId equals m.ID into em
                from mm in em.Where(m => !m.DeleteFlag).DefaultIfEmpty()
                // Left join JobTitleLevel
                join j in dbContext.JobTitleLevels on e.TitleId equals j.ID into emj
                from jj in emj.Where(j => j.IsActive && !j.DeleteFlag).DefaultIfEmpty()
                // Left join department
                join d in dbContext.Departments on e.DepartmentId equals d.DepartmentId into emjd
                from dd in emjd.Where(d => !d.DeleteFlag).DefaultIfEmpty()
                // Join TimeData has been grouped
                join t in dbContext.Time_Datas on e.ID equals t.EmployeeId
                where (
                    !e.DeleteFlag && e.EmpStatusId != Constants.RESIGNED &&
                    (
                        keyword == null
                        || SqlMethods.Like(e.ID + " " + e.FirstName + " " + (e.MiddleName != null ? (e.MiddleName + " ") : "") + e.LastName, keyword)
                        || SqlMethods.Like(e.LastName + " " + e.FirstName, keyword)
                        || SqlMethods.Like(e.OfficeEmail, keyword + "@%")
                    )
                )
                select new TimeReport()
                {
                    EmpId = e.ID,
                    EmpName = e.MiddleName == null ? (e.FirstName + " " + e.LastName) : (e.FirstName + " " + e.MiddleName + " " + e.LastName),
                    JobTitle = jj.DisplayName,
                    DeptId = dd.DepartmentId,
                    DeptName = dd.DepartmentName,
                    LocationCode = e.LocationCode,
                    ManagerName = mm.MiddleName == null ? (mm.FirstName + " " + mm.LastName) : (mm.FirstName + " " + mm.MiddleName + " " + mm.LastName),
                    TransDate = t.TransDate,
                    TimeIn = t.TimeIn,
                    TimeOut = t.TimeOut
                };

            #region Where dFrom, dTo, deptId
            if (dFrom != null)
            {
                sql = sql.Where(p => p.TransDate >= dFrom);
            }
            if (dTo != null)
            {
                sql = sql.Where(p => p.TransDate <= dTo);
            }
            if (deptId != null)
            {
                var listDept = dbContext.sp_GetDepartmentRoot(deptId).ToList();

                List<int> listSubDept = new List<int>();
                foreach (sp_GetDepartmentRootResult dept in listDept)
                {
                    listSubDept.Add(dept.DepartmentId);
                }
                sql = sql.Where(p => listSubDept.Contains(p.DeptId.Value));
            }
            #endregion

            return sql;
        }

        /// <summary>
        /// Return query of not checked in & out report kind
        /// </summary>
        private IQueryable<TimeReport> GetQueryReportNotCheck(string keyword, int? deptId, int? intTopN, DateTime? dFrom, DateTime? dTo)
        {
            List<DateTime> holidays = (from d in dbContext.AnnualHolidays select d.HolidayDate).ToList();
            
            var sql = 
                from ed in (
                    from emp in (
                        from e in dbContext.Employees
                        // Left join manager
                        join m in dbContext.Employees on e.ManagerId equals m.ID into em
                        from mm in em.Where(m => !m.DeleteFlag).DefaultIfEmpty()
                        // Left join JobTitleLevel
                        join j in dbContext.JobTitleLevels on e.TitleId equals j.ID into emj
                        from jj in emj.Where(j => j.IsActive && !j.DeleteFlag).DefaultIfEmpty()
                        // Left join department
                        join d in dbContext.Departments on e.DepartmentId equals d.DepartmentId into emjd
                        from dd in emjd.Where(d => !d.DeleteFlag).DefaultIfEmpty()
                        where (
                            !e.DeleteFlag && e.EmpStatusId != Constants.RESIGNED &&
                            (
                                keyword == null
                                || SqlMethods.Like(e.ID + " " + e.FirstName + " " + (e.MiddleName != null ? (e.MiddleName + " ") : "") + e.LastName, keyword)
                                || SqlMethods.Like(e.LastName + " " + e.FirstName, keyword)
                                || SqlMethods.Like(e.OfficeEmail, keyword + "@%")
                            )
                        )
                        select new TimeReport()
                            {
                                EmpId = e.ID,
                                EmpName = e.MiddleName == null ? (e.FirstName + " " + e.LastName) : (e.FirstName + " " + e.MiddleName + " " + e.LastName),
                                JobTitle = jj.DisplayName,
                                DeptId = dd.DepartmentId,
                                DeptName = dd.DepartmentName,
                                LocationCode = e.LocationCode,
                                ManagerName = mm.MiddleName == null ? (mm.FirstName + " " + mm.LastName) : (mm.FirstName + " " + mm.MiddleName + " " + mm.LastName)
                            }
                    )
                    // Join without conditions with Time_Date
                    from date in dbContext.Time_Dates.Where(
                        // Where dFrom and dTo conditions
                        d => (d.Date >= dFrom && d.Date <= dTo && !holidays.Contains(d.Date))
                    )
                    select new TimeReport()
                        {
                            EmpId = emp.EmpId,
                            EmpName = emp.EmpName,
                            JobTitle = emp.JobTitle,
                            DeptId = emp.DeptId,
                            DeptName = emp.DeptName,
                            LocationCode = emp.LocationCode,
                            ManagerName = emp.ManagerName,
                            TransDate = date.Date
                        }
                )
                // Left join with Time_Data has been grouped
                from t in (
                    from tg in dbContext.Time_Datas
                        group tg by new { tg.EmployeeId, tg.TransDate } into tGr
                        select new
                        {
                            EmployeeId = tGr.Key.EmployeeId,
                            TransDate = tGr.Key.TransDate
                        }
                ).Where(t => (ed.EmpId == t.EmployeeId && ed.TransDate == t.TransDate)).DefaultIfEmpty()
                where (
                    t.EmployeeId == null
                )
                select new TimeReport()
                    {
                        EmpId = ed.EmpId,
                        EmpName = ed.EmpName,
                        JobTitle = ed.JobTitle,
                        DeptId = ed.DeptId,
                        DeptName = ed.DeptName,
                        LocationCode = ed.LocationCode,
                        ManagerName = ed.ManagerName,
                        TransDate = ed.TransDate
                    };

            // Where deptId
            if (deptId != null)
            {
                var listDept = dbContext.sp_GetDepartmentRoot(deptId).ToList();

                List<int> listSubDept = new List<int>();
                foreach (sp_GetDepartmentRootResult dept in listDept)
                {
                    listSubDept.Add(dept.DepartmentId);
                }
                sql = sql.Where(p => listSubDept.Contains(p.DeptId.Value));
            }

            // Handling TopN
            if (intTopN != null)
            {
                // Top N ThenBy p.key to get Top N with EmployeeID ASC
                List<string> ids = sql.GroupBy(p => p.EmpId).OrderByDescending(p => p.Count()).ThenBy(p => p.Key).Select(p => p.Key).Take(intTopN.Value).ToList();
                sql = sql.Where(q => ids.Contains(q.EmpId));
            }

            return sql;
        }

        /// <summary>
        /// Return query of 7 normal reports
        /// </summary>
        private IQueryable<TimeReport> GetQueryReportNormal(string keyword, int? deptId, int? intTopN, DateTime? dFrom, DateTime? dTo
            , int? reportKindId, int comeAfter, int leaveBefore)
        {
            var sql =
                from e in dbContext.Employees
                // Left join manager
                join m in dbContext.Employees on e.ManagerId equals m.ID into em
                from mm in em.Where(m => !m.DeleteFlag).DefaultIfEmpty()
                // Left join JobTitleLevel
                join j in dbContext.JobTitleLevels on e.TitleId equals j.ID into emj
                from jj in emj.Where(j => j.IsActive && !j.DeleteFlag).DefaultIfEmpty()
                // Left join department
                join d in dbContext.Departments on e.DepartmentId equals d.DepartmentId into emjd
                from dd in emjd.Where(d => !d.DeleteFlag).DefaultIfEmpty()
                // Join TimeData has been grouped
                join t in
                    (
                        from tg in dbContext.Time_Datas
                        group tg by new { tg.EmployeeId, tg.TransDate } into tGr
                        select new
                        {
                            EmployeeId = tGr.Key.EmployeeId,
                            TransDate = tGr.Key.TransDate,
                            TimeIn = tGr.Min(p => p.TimeIn),
                            TimeOut = tGr.Max(p => p.TimeOut)
                        }
                    ) on e.ID equals t.EmployeeId
                where (
                    !e.DeleteFlag && e.EmpStatusId != Constants.RESIGNED &&
                    (
                        keyword == null
                        || SqlMethods.Like(e.ID + " " + e.FirstName + " " + (e.MiddleName != null ? (e.MiddleName + " ") : "") + e.LastName, keyword)
                        || SqlMethods.Like(e.LastName + " " + e.FirstName, keyword)
                        || SqlMethods.Like(e.OfficeEmail, keyword + "@%")
                    )
                )
                select new TimeReport()
                {
                    EmpId = e.ID,
                    EmpName = e.MiddleName == null ? (e.FirstName + " " + e.LastName) : (e.FirstName + " " + e.MiddleName + " " + e.LastName),
                    JobTitle = jj.DisplayName,
                    DeptId = dd.DepartmentId,
                    DeptName = dd.DepartmentName,
                    LocationCode = e.LocationCode,
                    ManagerName = mm.MiddleName == null ? (mm.FirstName + " " + mm.LastName) : (mm.FirstName + " " + mm.MiddleName + " " + mm.LastName),
                    TransDate = t.TransDate,
                    TimeIn = t.TimeIn,
                    TimeOut = t.TimeOut
                };

            switch (reportKindId)
            {
                case Constants.TIME_REPORT_KIND_LATE_AND_EARLY:
                    sql = sql.Where(p => p.TimeIn > comeAfter && p.TimeOut < leaveBefore);
                    break;
                case Constants.TIME_REPORT_KIND_LATE_OR_EARLY:
                    sql = sql.Where(p => (p.TimeIn > comeAfter && (p.TimeOut == null || p.TimeOut >= leaveBefore))
                        || (p.TimeOut < leaveBefore && (p.TimeIn == null || p.TimeIn <= comeAfter)));
                    break;
                case Constants.TIME_REPORT_KIND_LATE:
                    sql = sql.Where(p => p.TimeIn > comeAfter);
                    break;
                case Constants.TIME_REPORT_KIND_EARLY:
                    sql = sql.Where(p => p.TimeOut < leaveBefore);
                    break;
                case Constants.TIME_REPORT_KIND_STAY_LATE:
                    sql = sql.Where(p => p.TimeOut > leaveBefore);
                    break;
                case Constants.TIME_REPORT_KIND_NOT_IN:
                    sql = sql.Where(p => p.TimeIn == null);
                    break;
                case Constants.TIME_REPORT_KIND_NOT_OUT:
                    sql = sql.Where(p => p.TimeOut == null);
                    break;
            }

            #region Where dFrom, dTo, deptId
            if (dFrom != null)
            {
                sql = sql.Where(p => p.TransDate >= dFrom);
            }
            if (dTo != null)
            {
                sql = sql.Where(p => p.TransDate <= dTo);
            }
            if (deptId != null)
            {
                var listDept = dbContext.sp_GetDepartmentRoot(deptId).ToList();

                List<int> listSubDept = new List<int>();
                foreach (sp_GetDepartmentRootResult dept in listDept)
                {
                    listSubDept.Add(dept.DepartmentId);
                }
                sql = sql.Where(p => listSubDept.Contains(p.DeptId.Value));
            }
            #endregion
            
            // Handling TopN
            if (intTopN != null)
            {
                // Top N ThenBy p.key to get Top N with EmployeeID ASC
                List<string> ids = sql.GroupBy(p => p.EmpId).OrderByDescending(p => p.Count()).ThenBy(p => p.Key).Select(p => p.Key).Take(intTopN.Value).ToList();
                sql = sql.Where(q => ids.Contains(q.EmpId));
            }

            return sql;
        }
        
        /// <summary>
        /// Clearing data
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public Message ClearData(int month)
        {
            try
            {
                DateTime date = DateTime.Now.AddMonths(-month);

                var records = dbContext.Time_Datas.Where(p => p.TransDate <= date);
                dbContext.Time_Datas.DeleteAllOnSubmit(records);
                dbContext.SubmitChanges();

                return new Message(MessageConstants.I0011, MessageType.Info, "Clearing data");
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ArgumentException))
                    return new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
                return new Message(MessageConstants.E0007, MessageType.Error);

            }
        }
    }
}