using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Text.RegularExpressions;
using CRM.Models.Entities;
using System.Linq.Dynamic;
using System.Data.Linq.SqlClient;


namespace CRM.Models
{
    public class PTOReportDao : BaseDao
    {
        public string GetCurrentBalanceInMonth(string empID, DateTime? from, DateTime? to)
        {
            int? result = dbContext.func_GetCurrentBalanceInMonth(empID, from, to);
            return result.HasValue ? result.Value.ToString() : string.Empty;
        }

        public PTO_Report GetByDate(DateTime date,string employeeID)
        {
            return dbContext.PTO_Reports.Where(q => q.MonthYearReport == date && q.EmployeeId == employeeID).FirstOrDefault();
        }

        public int GetTotalBorrowHours(DateTime curDate, string empID)
        {
            int? hours = dbContext.func_GetTotalBorrowHours(curDate, Constants.DATE_LOCK_PTO, empID);
            if (hours.HasValue)
            {
                return hours.Value;
            }
            return 0;
        }

        public int SetVacationSeniority(string empID, DateTime curDate)
        {
            int? vacationSeniority = dbContext.GetVacationSeniority(empID, curDate);
            int? result = dbContext.GetVacationWithBorrowedHours(vacationSeniority, curDate, empID,
                GetTotalBorrowHours(curDate, empID));
            if (result.HasValue)
            {
                return result.Value;
            }
            return 0;
        }

        public PTO_Detail GetPToDetailByPTOID(string ptoID)
        {
            return dbContext.PTO_Details.Where(q => q.PTO_ID.Equals(ptoID)).FirstOrDefault();
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
                ) && (p.PTO.Status_ID == Constants.PTO_STATUS_CONFIRM || p.PTO.Status_ID == Constants.PTO_STATUS_VERIFIED)

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
                    int? currentBalance = dbContext.GetPaidHourOfEmp(employeeID, from, to);
                    // Get balance of current month
                    int balance = dbContext.GetEOMBalance(employeeID, from, to, currentBalance).Value;

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

        //public Message UpdateBalanceForCurrentMonth(string employeeID, DateTime currentDate, int? borrowedHours)
        //{
        //    Message msg = null;
        //    try
        //    {
        //        // Get PTO of next month
        //        PTO_Report reportDb = dbContext.PTO_Reports.Where(p =>
        //            p.EmployeeId == employeeID &&
        //            (p.MonthYearReport.Month == currentDate.AddMonths(1).Month &&
        //            p.MonthYearReport.Year == currentDate.AddMonths(1).Year)).FirstOrDefault();

        //        if (reportDb != null)
        //        {
        //            DateTime to = DateTime.Parse(Constants.DATE_LOCK_PTO + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
        //            DateTime from = (to.AddMonths(-1)).AddDays(1);
        //            int? currentBalance = dbContext.GetPaidHourOfEmp(employeeID, from, to);
        //            // Get balance of current month
        //            int balance = dbContext.GetEOMBalance(employeeID, from, to, currentBalance).Value;

        //            reportDb.CarriedForward = balance;
        //            reportDb.BorrowedHours = borrowedHours.HasValue?borrowedHours.Value:0;
        //            reportDb.UpdateDate = DateTime.Now;
        //            dbContext.SubmitChanges();
        //            msg = new Message(MessageConstants.I0001, MessageType.Info,
        //                "EOM balance of " + reportDb.EmployeeId + " - " + reportDb.Employee.FirstName + " " + reportDb.Employee.MiddleName + " " + reportDb.Employee.LastName, "updated");

        //        }

        //    }
        //    catch
        //    {
        //        msg = new Message(MessageConstants.E0007, MessageType.Error);
        //    }
        //    return msg;
        //}




        //triet.dinh 13-01-2012
        #region "Get List PTOReport by LINQ"
        /// <summary>
        /// Get query for PTO Report
        /// </summary>
        /// <param name="empName"></param>
        /// <param name="dateOffFrom"></param>
        /// <param name="dateOffTo"></param>
        /// <returns></returns>
        public IQueryable<PTOReportEntity> GetQueryPTOReport(string empName, DateTime? dateOffFrom, DateTime? dateOffTo)
        {
            int[] status = new int[] { Constants.PTO_STATUS_APPROVED, Constants.PTO_STATUS_CONFIRM, Constants.PTO_STATUS_VERIFIED };

            string[] ptoManagerList = Constants.PTO_MANAGER_LIST_NOT_DISPLAY_IN_REPORT.Split(',');

            var sql = from rpt in dbContext.PTO_Reports
                      join emp in dbContext.Employees on rpt.EmployeeId equals emp.ID
                      where emp.DeleteFlag == false && (!ptoManagerList.Contains(rpt.EmployeeId))
                            && (emp.EmpStatusId == null || emp.EmpStatusId !=Constants.RESIGNED)
                            && (rpt.MonthYearReport.Month == dateOffTo.Value.Month)
                            && (rpt.MonthYearReport.Year == dateOffTo.Value.Year)
                      select new PTOReportEntity() 
                      { 
                          ID = rpt.ID,
                          EmployeeID = rpt.EmployeeId,
                          CarriedForward = rpt.CarriedForward,
                          MonthlyVacation = rpt.MonthlyVacation,
                          DisplayName = emp.MiddleName != null ? (emp.FirstName + " " + emp.MiddleName + " " + emp.LastName) : (emp.FirstName + " " + emp.LastName),
                          StartDate = emp.StartDate,
                          ContractedDate = emp.ContractedDate,
                          Comment = rpt.Comment,
                          BorrowedHours = dbContext.func_GetTotalBorrowHours(rpt.MonthYearReport,Constants.DATE_LOCK_PTO,rpt.EmployeeId),
                          OfficeEmail = emp.OfficeEmail,
                          
                      };

            if (!string.IsNullOrEmpty(empName) && empName != Constants.EMPLOYEE)
            {
                empName = CommonFunc.GetFilterText(empName);
                sql = sql.Where(p => SqlMethods.Like(p.DisplayName, empName)
                                  || SqlMethods.Like(p.OfficeEmail, empName + "@%")
                                  || SqlMethods.Like(p.EmployeeID, empName));
            }

            return sql;
        }

        /// <summary>
        /// Get query for PTO Report
        /// </summary>
        /// <param name="empName"></param>
        /// <param name="dateOffFrom"></param>
        /// <param name="dateOffTo"></param>
        /// <returns></returns>
        public IQueryable<PTOReportEntity> GetQueryPortalPTOReport(string managerID, int departmentID,string project, DateTime dateOffFrom, DateTime dateOffTo)
        {
            int[] status = new int[] { Constants.PTO_STATUS_APPROVED, Constants.PTO_STATUS_CONFIRM, Constants.PTO_STATUS_VERIFIED };

            string[] ptoManagerList = Constants.PTO_MANAGER_LIST_NOT_DISPLAY_IN_REPORT.Split(',');

            var sql = from rpt in dbContext.PTO_Reports
                      join emp in dbContext.Employees on rpt.EmployeeId equals emp.ID
                      join man in dbContext.Employees on emp.ManagerId equals man.ID into emps
                      from man in emps.DefaultIfEmpty()
                      where emp.DeleteFlag == false && (!ptoManagerList.Contains(rpt.EmployeeId))
                            && (emp.EmpStatusId == null || emp.EmpStatusId != Constants.RESIGNED)
                            && (rpt.MonthYearReport.Month == dateOffTo.Month)
                            && (rpt.MonthYearReport.Year == dateOffTo.Year)
                            && emp.Department.IsActive && !emp.Department.DeleteFlag
                      select new PTOReportEntity()
                      {
                          ID = rpt.ID,
                          EmployeeID = rpt.EmployeeId,
                          CarriedForward = rpt.CarriedForward,
                          MonthlyVacation = rpt.MonthlyVacation,
                          DisplayName = emp.MiddleName != null ? (emp.FirstName + " " + emp.MiddleName + " " + emp.LastName) : (emp.FirstName + " " + emp.LastName),
                          StartDate = emp.StartDate,
                          ContractedDate = emp.ContractedDate,
                          Comment = rpt.Comment,
                          BorrowedHours = rpt.BorrowedHours,
                          OfficeEmail = emp.OfficeEmail,
                          ManagerID = man.ID,
                          ManagerName = man.MiddleName != null ? (man.FirstName + " " + man.MiddleName + " " + man.LastName) : (man.FirstName + " " + man.LastName),
                          DepartmentID = emp.DepartmentId,
                          DepartmentName = emp.Department.DepartmentName,
                          Project = emp.Project
                      };

            if (!string.IsNullOrEmpty(managerID))
            {
                var listManager = dbContext.sp_GetManagerRoot(managerID).ToList();

                List<string> listSubManager = new List<string>();
                foreach (sp_GetManagerRootResult item in listManager)
                {
                    listSubManager.Add(item.ID);
                }
                sql = sql.Where(p => listSubManager.Contains(p.ManagerID));
           }
            if (departmentID > 0)
            {
                var listDept = dbContext.sp_GetDepartmentRoot(departmentID).ToList();

                List<int> listSubDept = new List<int>();
                foreach (sp_GetDepartmentRootResult dept in listDept)
                {
                    listSubDept.Add(dept.DepartmentId);
                }
                sql = sql.Where(p => listSubDept.Contains(p.DepartmentID));
            }
            if (!string.IsNullOrEmpty(project))
            {
                sql = sql.Where(p => p.Project == project);
            }

            return sql;
        }
            
        /// <summary>
        /// Count total records of PTO Report List
        /// </summary>
        /// <param name="empName"></param>
        /// <param name="dateOffFrom"></param>
        /// <param name="dateOffTo"></param>
        /// <returns></returns>
        public int GetCountPTOReport(string empName, DateTime? dateOffFrom, DateTime? dateOffTo)
        {
            //int[] status = new int[] { Constants.PTO_STATUS_APPROVED, Constants.PTO_STATUS_CONFIRM, Constants.PTO_STATUS_VERIFIED };

            string[] ptoManagerList = Constants.PTO_MANAGER_LIST_NOT_DISPLAY_IN_REPORT.Split(',');

            var sql = from rpt in dbContext.PTO_Reports
                      join emp in dbContext.Employees on rpt.EmployeeId equals emp.ID
                      where emp.DeleteFlag == false && (!ptoManagerList.Contains(rpt.EmployeeId))
                            && (emp.EmpStatusId == null || emp.EmpStatusId != Constants.RESIGNED)
                            && (rpt.MonthYearReport.Month == dateOffTo.Value.Month)
                            && (rpt.MonthYearReport.Year == dateOffTo.Value.Year)
                      select new PTOReportEntity()
                     {
                         ID = rpt.ID,
                         EmployeeID = rpt.EmployeeId,
                         DisplayName = emp.MiddleName != null ? (emp.FirstName + " " + emp.MiddleName + " " + emp.LastName) : (emp.FirstName + " " + emp.LastName),
                         OfficeEmail = emp.OfficeEmail,
                     };


            if (!string.IsNullOrEmpty(empName)  && empName != Constants.EMPLOYEE)
            {
                empName = CommonFunc.GetFilterText(empName);
                sql = sql.Where(p => SqlMethods.Like(p.DisplayName, empName)
                                  || SqlMethods.Like(p.OfficeEmail, empName + "@%")
                                  || SqlMethods.Like(p.EmployeeID, empName));
            }

            return sql.Count();
        }

        public int GetCountPortalPTOReport(string managerID,int departmentID,string project, DateTime dateOffFrom, DateTime dateOffTo)
        {
            //int[] status = new int[] { Constants.PTO_STATUS_APPROVED, Constants.PTO_STATUS_CONFIRM, Constants.PTO_STATUS_VERIFIED };

          string[] ptoManagerList = Constants.PTO_MANAGER_LIST_NOT_DISPLAY_IN_REPORT.Split(',');

            var sql = from rpt in dbContext.PTO_Reports
                      join emp in dbContext.Employees on rpt.EmployeeId equals emp.ID
                      join man in dbContext.Employees on emp.ManagerId equals man.ID into emps
                      from man in emps.DefaultIfEmpty()
                      where emp.DeleteFlag == false && (!ptoManagerList.Contains(rpt.EmployeeId))
                            && (emp.EmpStatusId == null || emp.EmpStatusId != Constants.RESIGNED)
                            && (rpt.MonthYearReport.Month == dateOffTo.Month)
                            && (rpt.MonthYearReport.Year == dateOffTo.Year)
                            && emp.Department.IsActive && !emp.Department.DeleteFlag
                      select new PTOReportEntity()
                      {
                          ID = rpt.ID,
                          EmployeeID = rpt.EmployeeId,
                          DisplayName = emp.MiddleName != null ? (emp.FirstName + " " + emp.MiddleName + " " + emp.LastName) : (emp.FirstName + " " + emp.LastName),
                          OfficeEmail = emp.OfficeEmail,
                          ManagerID = man.ID,
                          ManagerName = man.MiddleName != null ? (man.FirstName + " " + man.MiddleName + " " + man.LastName) : (man.FirstName + " " + man.LastName),
                          DepartmentID = emp.DepartmentId,
                          Project = emp.Project
                      };


            if (!string.IsNullOrEmpty(managerID))
            {
                var listManager = dbContext.sp_GetManagerRoot(managerID).ToList();

                List<string> listSubManager = new List<string>();
                foreach (sp_GetManagerRootResult item in listManager)
                {
                    listSubManager.Add(item.ID);
                }
                sql = sql.Where(p => listSubManager.Contains(p.ManagerID));
            }
            if (departmentID > 0)
            {
                var listDept = dbContext.sp_GetDepartmentRoot(departmentID).ToList();

                List<int> listSubDept = new List<int>();
                foreach (sp_GetDepartmentRootResult dept in listDept)
                {
                    listSubDept.Add(dept.DepartmentId);
                }
                sql = sql.Where(p => listSubDept.Contains(p.DepartmentID));
            }

            if (!string.IsNullOrEmpty(project))
            {
                project = CommonFunc.GetFilterText(project);
                sql = sql.Where(p => SqlMethods.Like(p.Project, project));
            }

            return sql.Count();
        }

        /// <summary>
        /// Get List PTO Report
        /// </summary>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="empName"></param>
        /// <param name="dateOffFrom"></param>
        /// <param name="dateOffTo"></param>
        /// <returns></returns>
        public List<PTOReportEntity> GetListPTOReport(string sortColumn, string sortOrder, int skip, int take,
                                        string empName, DateTime? dateOffFrom, DateTime? dateOffTo)
        {
            dbContext.sp_UpdatePTOReport(dateOffFrom, dateOffTo);

            var sql = GetQueryPTOReport(empName, dateOffFrom, dateOffTo);
            switch (sortColumn)
            {
                case "Name":
                    sql = sql.OrderBy("DisplayName" + " " + sortOrder);
                    break;
                case "UnpaidHour":
                    //sql = sql.OrderBy("SubtractedBalance" + " " + sortOrder);
                    break;
                case "Used": 
                    break;
                case "EOMBalance": 
                    break;
                case "UnpaidLeave": 
                    break;
                default:
                    sql = sql.OrderBy(sortColumn + " " + sortOrder);
                    break;
            }

            List<PTOReportEntity> list;
            if (skip == 0 && take == 0)
                list = sql.ToList();
            else
                list = sql.Skip(skip).Take(take).ToList();

            int[] status = new int[] { Constants.PTO_STATUS_APPROVED, Constants.PTO_STATUS_CONFIRM, Constants.PTO_STATUS_VERIFIED };
            foreach (PTOReportEntity item in list)
            {
                item.SubtractedBalance = dbContext.GetPaidHourOfEmp(item.EmployeeID, dateOffFrom, dateOffTo);
                item.UnpaidLeave = dbContext.GetUnpaidHourOfEmp(item.EmployeeID, dateOffFrom, dateOffTo);
                item.EOMBalance = dbContext.GetEOMBalance(item.EmployeeID, dateOffFrom, dateOffTo, 0);

                item.Used = (from pto in dbContext.PTOs
                             where pto.DeleteFlag == false && pto.Submitter == item.EmployeeID && status.Contains(pto.Status_ID)
                             select dbContext.GetSumHourOfPTO(pto.ID, dateOffFrom, dateOffTo)).Sum();
            }
            return list;
                
        }

        /// <summary>
        /// Get List PTO Report
        /// </summary>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="empName"></param>
        /// <param name="dateOffFrom"></param>
        /// <param name="dateOffTo"></param>
        /// <returns></returns>
        public List<PTOReportEntity> GetListPortalPTOReport(string sortColumn, string sortOrder, int skip, int take,
                                        string empName,int departmentID,string project, DateTime dateOffFrom, DateTime dateOffTo)
        {
            dbContext.sp_UpdatePTOReport(dateOffFrom, dateOffTo);

            var sql = GetQueryPortalPTOReport(empName, departmentID, project, dateOffFrom, dateOffTo);
            sql = sql.OrderBy(sortColumn + " " + sortOrder);
            List<PTOReportEntity> list;
            if (skip == 0 && take == 0)
                list = sql.ToList();
            else
                list = sql.Skip(skip).Take(take).ToList();

            int[] status = new int[] { Constants.PTO_STATUS_APPROVED, Constants.PTO_STATUS_CONFIRM, Constants.PTO_STATUS_VERIFIED };
            foreach (PTOReportEntity item in list)
            {
                item.SubtractedBalance = dbContext.GetPaidHourOfEmp(item.EmployeeID, dateOffFrom, dateOffTo);
                item.UnpaidLeave = dbContext.GetUnpaidHourOfEmp(item.EmployeeID, dateOffFrom, dateOffTo);
                item.EOMBalance = dbContext.GetEOMBalance(item.EmployeeID, dateOffFrom, dateOffTo, 0);

                item.Used = (from pto in dbContext.PTOs
                             where pto.DeleteFlag == false && pto.Submitter == item.EmployeeID && status.Contains(pto.Status_ID)
                             select dbContext.GetSumHourOfPTO(pto.ID, dateOffFrom, dateOffTo)).Sum();
            }
            return list;

        }
        #endregion

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

        public PTO_Report GetByID(int id)
        {
            return dbContext.PTO_Reports.Where(q => q.ID == id).FirstOrDefault();
        }

        public void UpdateVacationSerinority(PTO_Report objUI,int vacationThisMonth)
        {
            PTO_Report objDb = GetByID(objUI.ID);
            if (objDb != null)
            {
                objDb.MonthlyVacation = vacationThisMonth;
                dbContext.SubmitChanges();
            }
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

        public int? GetYTDForDashboard(string employeeID, DateTime fromDate, DateTime toDate)
        {
            return dbContext.GetEOMBalance(employeeID, fromDate, toDate, 0);
        }
    }
}