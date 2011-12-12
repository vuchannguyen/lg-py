using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Text.RegularExpressions;

namespace CRM.Models
{
    public class PTOReportDao : BaseDao
    {
        public string GetCurrentBalanceInMonth(string empID, DateTime? from, DateTime? to)
        {
            int? result = dbContext.func_GetCurrentBalanceInMonth(empID, from, to);
            return result.HasValue ? result.Value.ToString() : string.Empty;
        }
        public List<sp_GetDateOffPTOResult> GetListPTOByUserID(string empID, DateTime? fromDate)
        {
            return dbContext.sp_GetDateOffPTO(empID, fromDate).ToList();
        }

        public List<PTO_Detail> GetListPTOByUserID(string empID, DateTime fromDate, DateTime toDate)
        {
            return dbContext.PTO_Details.Where(p => !p.PTO.DeleteFlag &&
                p.PTO.Submitter == empID && (
                    (p.PTO.CreateDate.Date <= toDate.Date && p.DateOff >= fromDate && p.DateOff <= toDate) ||
                    (p.PTO.CreateDate.Date <= toDate.Date && p.PTO.CreateDate.Date >= fromDate.Date && p.DateOff < fromDate) ||
                    (p.PTO.CreateDate.Date <= toDate.Date && p.DateOffFrom >= fromDate && p.DateOffFrom <= toDate) ||
                    (p.PTO.CreateDate.Date <= toDate.Date && p.DateOffTo >= fromDate && p.DateOffTo <= toDate) ||
                    (p.PTO.CreateDate.Date <= toDate.Date && p.DateOffFrom < fromDate && p.DateOffTo > toDate) ||
                    (p.PTO.CreateDate.Date >= fromDate.Date && p.PTO.CreateDate.Date <= toDate.Date && p.DateOffTo < fromDate)
                )
            ).ToList();
        }
        /// <summary>
        /// Get PTO by ID
        /// Author: tan.tran
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PTO_Report GetPTO(int id)
        {
            return dbContext.PTO_Reports.Where(p => p.ID == id).FirstOrDefault<PTO_Report>();
        }

        /// <summary>
        /// Get PTO_Report List by month & year
        /// Author: tan.tran
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<PTO_Report> GetList(DateTime date)
        {
            return dbContext.PTO_Reports.Where(p => p.MonthYearReport.Month == date.Month
                && p.MonthYearReport.Year == date.Year).ToList<PTO_Report>();
        }

        /// <summary>
        /// Update a PTO Report
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public Message Update(PTO_Report report)
        {
            Message msg = null;
            try
            {
                PTO_Report reportDb = GetPTO(report.ID);
                //Another user has updated this holiday
                if (!reportDb.UpdateDate.ToString().Trim().Equals(report.UpdateDate.ToString().Trim()))
                {
                    msg = new Message(MessageConstants.E0025, MessageType.Error, "EOM balance");
                }
                //Successful case
                else
                {
                    reportDb.CarriedForward = report.CarriedForward;
                    reportDb.MonthlyVacation = report.MonthlyVacation;
                    reportDb.Comment = report.Comment;
                    reportDb.UpdatedBy = report.UpdatedBy;
                    reportDb.UpdateDate = DateTime.Now;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info,
                        "EOM balance of " + reportDb.EmployeeId + " - " + reportDb.Employee.FirstName + " " + reportDb.Employee.MiddleName + " " + reportDb.Employee.LastName, "updated");
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// UpdateBalanceForNextMonth
        /// add new 2011.10.26
        /// by tan.tran
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public Message UpdateBalanceForNextMonth(string employeeID, DateTime currentDate)
        {
            Message msg = null;
            try
            {
                // Get PTO of next month
                PTO_Report reportDb = dbContext.PTO_Reports.Where(p =>
                    p.EmployeeId == employeeID &&
                    (p.MonthYearReport.Month == currentDate.AddMonths(1).Month &&
                    p.MonthYearReport.Year == currentDate.AddMonths(1).Year)).FirstOrDefault();
                
                if (reportDb != null)
                {
                    DateTime to = DateTime.Parse(Constants.DATE_LOCK_PTO + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                    DateTime from = (to.AddMonths(-1)).AddDays(1);
                    // Get balance of current month
                    int balance = dbContext.GetEOMBalance(employeeID, from, to).Value;

                    reportDb.CarriedForward = balance;
                    reportDb.UpdateDate = DateTime.Now;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info,
                        "EOM balance of " + reportDb.EmployeeId + " - " + reportDb.Employee.FirstName + " " + reportDb.Employee.MiddleName + " " + reportDb.Employee.LastName, "updated");

                }

            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Get list
        /// </summary>
        /// <param name="empName"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<sp_GetPTOReportResult> GetList(string empName, DateTime? dateOffFrom, DateTime? dateOffTo)
        {
            return dbContext.sp_GetPTOReport(empName, dateOffFrom, dateOffTo).ToList();
        }

        /// <summary>
        /// Get PTO Report by Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public PTO_Report GetBalanceByDate(string empID, DateTime date)
        {
            return dbContext.PTO_Reports.Where(q => q.EmployeeId == empID && (q.MonthYearReport.Month == date.Month && q.MonthYearReport.Year == date.Year)).FirstOrDefault<PTO_Report>();
        }

        /// <summary>
        /// Sort Exam
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<sp_GetPTOReportResult> Sort(List<sp_GetPTOReportResult> list, string sortColumn, string sortOrder)
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
                case "EmployeeID":
                    list.Sort(
                         delegate(sp_GetPTOReportResult m1, sp_GetPTOReportResult m2)
                         { return m1.EmployeeID.CompareTo(m2.EmployeeID) * order; });
                    break;
                case "Name":
                    list.Sort(
                         delegate(sp_GetPTOReportResult m1, sp_GetPTOReportResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "StartDate":
                    list.Sort(
                         delegate(sp_GetPTOReportResult m1, sp_GetPTOReportResult m2)
                         { return m1.StartDate.CompareTo(m2.StartDate) * order; });
                    break;
                case "ContractedDate":
                    list.Sort(
                         delegate(sp_GetPTOReportResult m1, sp_GetPTOReportResult m2)
                         {
                             DateTime contractedDate1 = m1.ContractedDate.HasValue ? m1.ContractedDate.Value : DateTime.MinValue;
                             DateTime contractedDate2 = m2.ContractedDate.HasValue ? m2.ContractedDate.Value : DateTime.MinValue;
                             return contractedDate1.CompareTo(contractedDate2) * order;
                         });
                    break;
                case "CarriedForward":
                    list.Sort(
                         delegate(sp_GetPTOReportResult m1, sp_GetPTOReportResult m2)
                         { return m1.CarriedForward.CompareTo(m2.CarriedForward) * order; });
                    break;
                case "Used":
                    list.Sort(
                         delegate(sp_GetPTOReportResult m1, sp_GetPTOReportResult m2)
                         { return m1.Used.Value.CompareTo(m2.Used.Value) * order; });
                    break;
                case "EOMBalance":
                    list.Sort(
                         delegate(sp_GetPTOReportResult m1, sp_GetPTOReportResult m2)
                         { return m1.EOMBalance.Value.CompareTo(m2.EOMBalance.Value) * order; });
                    break;
                case "UnpaidLeave":
                    list.Sort(
                         delegate(sp_GetPTOReportResult m1, sp_GetPTOReportResult m2)
                         { return m1.UnpaidLeave.Value.CompareTo(m2.UnpaidLeave.Value) * order; });
                    break;
                case "Comment":
                    list.Sort(
                         delegate(sp_GetPTOReportResult m1, sp_GetPTOReportResult m2)
                         {

                             m1.Comment = string.IsNullOrEmpty(m1.Comment) ? string.Empty : m1.Comment;
                             m2.Comment = string.IsNullOrEmpty(m2.Comment) ? string.Empty : m2.Comment;
                             return m1.Comment.CompareTo(m2.Comment) * order;
                         });
                    break;
            }

            return list;
        }

        /// <summary>
        /// Update balance for all employee
        /// </summary>
        /// <returns></returns>
        public Message UpdatePTOBalance()
        {
            Message msg = null;
            try
            {
                dbContext.ExecuteCommand(string.Format("exec sp_UpdateBalanceForMonth '{0}', {1}, {2}, {3}", Constants.DATE_LOCK_PTO, 5, 3, 24));
                msg = new Message(MessageConstants.I0001, MessageType.Info, "PTO Balance", "updated");
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
            }
            return msg;
        }

        public void UpdateBalance(string empId, DateTime monthYearReport, int balance)
        {
            try
            {
                var report = dbContext.PTO_Reports.FirstOrDefault(p => p.EmployeeId == empId && p.MonthYearReport.Year == monthYearReport.Year &&
                    p.MonthYearReport.Month == monthYearReport.Month);
                if (report != null)
                {
                    report.CarriedForward = balance;
                    report.UpdateDate = DateTime.Now;
                    report.UpdatedBy = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    //insert balance to the previous month -> carried forward
                    report = new PTO_Report()
                    {
                        EmployeeId = empId,
                        MonthYearReport = monthYearReport,
                        CarriedForward = balance,
                        CreateDate = DateTime.Now,
                        CreatedBy = HttpContext.Current.User.Identity.Name,
                        UpdateDate = DateTime.Now,
                        UpdatedBy = HttpContext.Current.User.Identity.Name
                    };
                    dbContext.PTO_Reports.InsertOnSubmit(report);
                }
                dbContext.SubmitChanges();
            }
            catch { }
        }

        public Message Import(string empId, bool isProbation, DateTime monthYearReport, int balance)
        {
            try
            {
                UpdateBalance(empId, monthYearReport.AddMonths(-1), balance);
                UpdateBalance(empId, monthYearReport, balance + (isProbation ? 0 : 8));
                return new Message(MessageConstants.E0033, MessageType.Info, "Success");
            }
            catch (Exception ex)
            {
                return new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// Get PTO report of employee by ID and month,year
        /// Modified by tan.tran 2011.08.30
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public int GetPTOReportByEmployeeIDAndMonth(string employeeID, DateTime date)
        {
            return dbContext.PTO_Reports.Where(q => q.EmployeeId == employeeID
                && q.MonthYearReport.Month == date.Month
                && q.MonthYearReport.Year == date.Year).Select(p => (int?)p.CarriedForward + p.MonthlyVacation).Sum() ?? 0;
        }
    }
}