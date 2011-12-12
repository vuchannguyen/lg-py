using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Attributes;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using CRM.Areas.Portal.Models;

namespace CRM.Controllers
{
    public class HomeController : BaseController
    {
        private ContractRenewalDao dao = new ContractRenewalDao();
        private ServiceRequestDao srDao = new ServiceRequestDao();
        private GroupDao groupDao = new GroupDao();
        private PTODao ptoDao = new PTODao();
        public ActionResult Index()
        {
            // Set Session Time out
            string stTimeout = System.Configuration.ConfigurationManager.AppSettings["SESSION_TIME_OUT"];
            if (!string.IsNullOrEmpty(stTimeout) && CheckUtil.IsInteger(stTimeout))
            {
                SessionManager.SetTimeout(Session, int.Parse(stTimeout));
            } // else default is 20 mins            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            bool isAuthorize = CommonFunc.CheckAuthorized(principal.UserData.UserID,(int)Modules.ContractRenewal,(int)Permissions.ContractNotification);
            if (isAuthorize == true)
            {
                List<Contract> noticList = GetContractNotification();
                if (noticList.Count > 0)
                {
                    ViewData["NotificationList"] = noticList;
                }
                else
                {
                    ViewData["NotificationList"] = null;
                }
            }

            // Statistic
            ViewData["LoginStatistic_Title"] = GetUserloginStatisticTitle();
            ViewData["LoginStatistic_Value"] = GetUserloginStatisticValue();
            LogAccessDao logDao = new LogAccessDao();
            ViewData["LoginStatistic_Today"] = logDao.CountByDateMonthYear(DateTime.Now);
            ViewData["LoginStatistic_Yesterday"] = logDao.CountByDateMonthYear(DateTime.Now.AddDays(-1));

            ViewData["LoginStatistic_ThisMonth"] = logDao.CountByMonth(DateTime.Now);
            ViewData["LoginStatistic_LastMonth"] = logDao.CountByMonth(DateTime.Now.AddMonths(-1));

            ViewData["LoginStatistic_ThisYear"] = logDao.CountByYear(DateTime.Now);
            ViewData["LoginStatistic_LastYear"] = logDao.CountByYear(DateTime.Now.AddYears(-1));

            // Check user has inputted annual holiday for this year
            isAuthorize = CommonFunc.CheckAuthorized(principal.UserData.UserID,(int)Modules.PTO_Admin,(int)Permissions.PTONotification);
            if (isAuthorize == true)
            {
                if (!CheckInputAnnualHolidayThisYear())
                {
                    ViewData["NoticeInputAnnualHolliday"] = "true";
                }
                // Check the PTO balance of has been update
                if (!CheckUpdatePTOBalance())
                {
                    ViewData["NoticeUpdatePTOBalance"] = "true";
                }

                var ptoListNeedToBeConfirmed = ptoDao.GetListOfPtoNeedToBeConfirmed(DateTime.Now, null);
                int iNumDaysToCome = CommonFunc.GetPtoDateTo(DateTime.Now).Subtract(DateTime.Now).Days;
                if (ptoListNeedToBeConfirmed.Count > 0 &&
                     iNumDaysToCome <= Constants.PTO_NOTIFICATION_DAYS)
                {
                    ViewData[CommonDataKey.PTO_NOTICE_CONFIRM] = ptoListNeedToBeConfirmed.Count;
                }
            }
            //Service Request Notification
            if (groupDao.HasPermisionOnModule(principal.UserData.UserID, (int)Permissions.Notification, (int)Modules.ServiceRequestAdmin))
                ViewData["SrUndoneList"] = srDao.GetUndoneListByDate(DateTime.Now);
            //End Service Request
            return View();    
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.HOME_STATISTIC_LIST);
            return RedirectToAction("UserLoginStatistic");
        }

        #region Notification
        public List<Contract> GetContractNotification()
        {
            return dao.GetList().Where(p=>p.NotificationClosed == false
                && p.Employee.DeleteFlag == false
                && p.Employee.EmpStatusId != Constants.RESIGNED
                && p.EndDate != null
                && p.EndDate.Value.AddDays(-Constants.NOTIFICATION_DAYS) <= DateTime.Now
                ).OrderByDescending(p=>p.EndDate).ToList<Contract>();
           
        }

        public string GetUserloginStatisticTitle()
        {
            LogAccessDao dao = new LogAccessDao();
            
            string result = string.Empty;
            DateTime currDate = DateTime.Now;
            result += DateTimeUtil.GetDateInWeek(currDate.AddDays(-6))+ ",";
            result += DateTimeUtil.GetDateInWeek(currDate.AddDays(-5)) + ",";
            result += DateTimeUtil.GetDateInWeek(currDate.AddDays(-4)) + ",";
            result += DateTimeUtil.GetDateInWeek(currDate.AddDays(-3)) + ",";
            result += DateTimeUtil.GetDateInWeek(currDate.AddDays(-2)) + ",";
            result += DateTimeUtil.GetDateInWeek(currDate.AddDays(-1)) + ",";
            result += DateTimeUtil.GetDateInWeek(currDate) ;          


            return result;
        }

        public string GetUserloginStatisticValue()
        {
            LogAccessDao dao = new LogAccessDao();
            string result = string.Empty;
            DateTime currDate = DateTime.Now;
            result += dao.CountByDateMonthYear(currDate.AddDays(-6)) + ",";
            result += dao.CountByDateMonthYear(currDate.AddDays(-5)) + ",";
            result += dao.CountByDateMonthYear(currDate.AddDays(-4)) + ",";
            result += dao.CountByDateMonthYear(currDate.AddDays(-3)) + ",";
            result += dao.CountByDateMonthYear(currDate.AddDays(-2)) + ",";
            result += dao.CountByDateMonthYear(currDate.AddDays(-1)) + ",";
            result += dao.CountByDateMonthYear(currDate);
            
            return result;
        }


         [CrmAuthorizeAttribute(Module = Modules.UserLog, Rights = Permissions.Read)]
        public ActionResult UserLoginStatistic()
        {
            Hashtable hashData = Session[SessionKey.HOME_STATISTIC_LIST] == null ? new Hashtable() : (Hashtable)Session[SessionKey.HOME_STATISTIC_LIST];
            ViewData[Constants.HOME_STATISTIC_USER_ADMIN] = hashData[Constants.HOME_STATISTIC_USER_ADMIN] == null ? Constants.SELECT_USER_ADMIN : !string.IsNullOrEmpty((string)hashData[Constants.HOME_STATISTIC_USER_ADMIN]) ? hashData[Constants.HOME_STATISTIC_USER_ADMIN] : Constants.SELECT_USER_ADMIN;
            ViewData[Constants.HOME_STATISTIC_FROM_DATE] = hashData[Constants.HOME_STATISTIC_FROM_DATE] == null ? "" : hashData[Constants.HOME_STATISTIC_FROM_DATE];
            ViewData[Constants.HOME_STATISTIC_TO_DATE] = hashData[Constants.HOME_STATISTIC_TO_DATE] == null ? "" : hashData[Constants.HOME_STATISTIC_TO_DATE];
            ViewData[Constants.HOME_STATISTIC_COLUMN] = hashData[Constants.HOME_STATISTIC_COLUMN] == null ? "DatetimeAccess" : hashData[Constants.HOME_STATISTIC_COLUMN].ToString();
            ViewData[Constants.HOME_STATISTIC_ORDER] = hashData[Constants.HOME_STATISTIC_ORDER] == null ? "desc" : hashData[Constants.HOME_STATISTIC_ORDER].ToString();
            ViewData[Constants.HOME_STATISTIC_PAGE_INDEX] = hashData[Constants.HOME_STATISTIC_PAGE_INDEX] == null ? "1" : hashData[Constants.HOME_STATISTIC_PAGE_INDEX].ToString();
            ViewData[Constants.HOME_STATISTIC_ROW_COUNT] = hashData[Constants.HOME_STATISTIC_ROW_COUNT] == null ? "20" : hashData[Constants.HOME_STATISTIC_ROW_COUNT].ToString();

            UserAdminDao userDao = new UserAdminDao();
            List<UserAdmin> listUsers = userDao.GetList();
            ViewData["UserAdmin"] = new SelectList(listUsers, "UserName", "UserName", ViewData[Constants.HOME_STATISTIC_USER_ADMIN]);

            return View();
        }

        public ActionResult GetListJQGrid(string userAdmin, string dateFrom, string dateTo)
        {
            LogAccessDao logDao = new LogAccessDao();

            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(userAdmin, dateFrom, dateTo, sortColumn, sortOrder, pageIndex, rowCount);
            List<LogAccess> logList = logDao.GetList(userAdmin, dateFrom, dateTo);

            int totalRecords = logList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

            // Sort
            List<LogAccess> logListFinal = logDao.Sort(logList, sortColumn, sortOrder);

            var jsonData = new
            {
                total = totalPages,
                page = pageIndex,
                records = totalRecords,
                rows = (
                    from m in logListFinal
                    select new
                    {
                        i = m.Id,
                        cell = new string[] {
                           m.UserAdmin,
                           m.UserIp,
                           m.DatetimeAccess.ToString(Constants.DATETIME_FORMAT_FULL),
                           m.DatetimeOut != null? m.DatetimeOut.Value.ToString(Constants.DATETIME_FORMAT_FULL): string.Empty
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private void SetSessionFilter(string userAdmin, string dateFrom, string dateTo, string column, string order, int pageIndex, int rowCount)
        {
            Hashtable statisticState = new Hashtable();
            statisticState.Add(Constants.HOME_STATISTIC_USER_ADMIN, userAdmin);
            statisticState.Add(Constants.HOME_STATISTIC_FROM_DATE, dateFrom);
            statisticState.Add(Constants.HOME_STATISTIC_TO_DATE, dateTo);
            statisticState.Add(Constants.CANDIDATE_LIST_COLUMN, column);
            statisticState.Add(Constants.CANDIDATE_LIST_ORDER, order);
            statisticState.Add(Constants.CANDIDATE_LIST_PAGE_INDEX, pageIndex);
            statisticState.Add(Constants.CANDIDATE_LIST_ROW_COUNT, rowCount);
            Session[SessionKey.HOME_STATISTIC_LIST] = statisticState;
        }
        #endregion

        public List<Contract> GetContractList(bool hasExpired)
        {
            List<Contract> list =  dao.GetList().Where(p => p.NotificationClosed == false
                && p.Employee.DeleteFlag == false
                && p.Employee.EmpStatusId != Constants.RESIGNED
                && p.EndDate != null
                && p.EndDate.Value.AddDays(-Constants.NOTIFICATION_DAYS) <= DateTime.Now
                ).OrderByDescending(p => p.EndDate).ToList<Contract>();
            if (hasExpired)
            {
                list = list.Where(p => p.EndDate <= DateTime.Now).ToList<Contract>();
            }
            else
            {
                list = list.Where(p => p.EndDate > DateTime.Now).ToList<Contract>();
            }
            return list;
        }

        public ActionResult ExportContractList()
        {
            List<Contract> expiredList = GetContractList(true);
            List<Contract> commingList = GetContractList(false);
            #region Comming List
            #region variable
            HtmlTable tbl_Excel = new HtmlTable();
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell colID = new HtmlTableCell();
            HtmlTableCell colName = new HtmlTableCell();
            HtmlTableCell colDepartment = new HtmlTableCell();
            HtmlTableCell colSubDepartment = new HtmlTableCell();
            HtmlTableCell colContractType = new HtmlTableCell();
            HtmlTableCell colStartDate = new HtmlTableCell();
            HtmlTableCell colEndDate = new HtmlTableCell();
            HtmlTableCell col = new HtmlTableCell();
            HtmlTableCell colHospital = new HtmlTableCell();
            #endregion
            #region Title
            HtmlTableCell colHeader = new HtmlTableCell();
            colHeader.Align = HorizontalAlign.Left.ToString();
            colHeader.VAlign = VerticalAlign.Middle.ToString();
            colHeader.ColSpan = 4;
            colHeader.RowSpan = 2;
            colHeader.Height = "15";
            colHeader.Attributes.Add("style", Constants.ROW_HEADER);
            colHeader.InnerHtml = "<font size=5><b>Contract Renewal</font>";
            colHeader.NoWrap = true;
            row.Cells.Add(colHeader);
            tbl_Excel.Rows.Add(row);
            #endregion
            #region Space
            row = new HtmlTableRow();
            tbl_Excel.Rows.Add(row);
            row = new HtmlTableRow();
            tbl_Excel.Rows.Add(row);
            row = new HtmlTableRow();
            tbl_Excel.Rows.Add(row);
            #endregion
            #region Name List
            row = new HtmlTableRow();
            HtmlTableCell colComming = new HtmlTableCell();
            colComming.ColSpan = 4;
            colComming.Attributes.Add("style", Constants.ROW_SUB_HEADER);
            colComming.InnerHtml = "Contract is comming in next 7 day(s)";
            row.Cells.Add(colComming);
            tbl_Excel.Rows.Add(row);  
            #endregion
            #region Grid Title
            row = new HtmlTableRow();
            colID = new HtmlTableCell();
            colName = new HtmlTableCell();
            colDepartment = new HtmlTableCell();
            colSubDepartment = new HtmlTableCell();
            colContractType = new HtmlTableCell();
            colStartDate = new HtmlTableCell();
            colEndDate = new HtmlTableCell();
            colID.InnerHtml = "ID";
            colName.InnerHtml = "Name";
            colDepartment.InnerHtml = "Department";
            colSubDepartment.InnerHtml = "Sub Department";
            colContractType.InnerHtml = "Contract Type";
            colEndDate.InnerHtml = "End Date";
            colStartDate.InnerHtml = "Start Date";
            row.Cells.Add(colID);
            row.Cells.Add(colName);
            row.Cells.Add(colDepartment);
            row.Cells.Add(colSubDepartment);
            row.Cells.Add(colContractType);
            row.Cells.Add(colStartDate);
            row.Cells.Add(colEndDate);
            #region CSS for Title
            colID.Attributes.Add("style", Constants.ROW_TITLE);
            colName.Attributes.Add("style", Constants.ROW_TITLE);
            colDepartment.Attributes.Add("style", Constants.ROW_TITLE);
            colSubDepartment.Attributes.Add("style", Constants.ROW_TITLE);
            colContractType.Attributes.Add("style", Constants.ROW_TITLE);
            colStartDate.Attributes.Add("style", Constants.ROW_TITLE);
            colEndDate.Attributes.Add("style", Constants.ROW_TITLE);
            tbl_Excel.Rows.Add(row);
            #endregion
            #endregion
            #region Row
            foreach (Contract item in commingList)
            {
                Employee emp = new EmployeeDao().GetById(item.EmployeeId);
                if (emp != null)
                {
                    row = new HtmlTableRow();
                    colID = new HtmlTableCell();
                    colName = new HtmlTableCell();
                    colDepartment = new HtmlTableCell();
                    colSubDepartment = new HtmlTableCell();
                    colContractType = new HtmlTableCell();
                    colStartDate = new HtmlTableCell();
                    colEndDate = new HtmlTableCell();
                    colID.InnerHtml = item.EmployeeId;
                    colName.InnerHtml = emp.LastName + " " + emp.MiddleName + " " + emp.FirstName;
                    colDepartment.InnerHtml = new DepartmentDao().GetDepartmentNameBySub(emp.DepartmentId);
                    colSubDepartment.InnerHtml = emp.Department.DepartmentName;
                    colContractType.InnerHtml = item.ContractType1.ContractTypeName;
                    colStartDate.InnerHtml = item.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                    colEndDate.InnerHtml = item.EndDate.HasValue?item.EndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"";
                    colID.Attributes.Add("style", Constants.ROW_CSS);
                    colName.Attributes.Add("style", Constants.ROW_CSS);
                    colDepartment.Attributes.Add("style", Constants.ROW_CSS);
                    colSubDepartment.Attributes.Add("style", Constants.ROW_CSS);
                    colContractType.Attributes.Add("style", Constants.ROW_CSS);
                    colStartDate.Attributes.Add("style", Constants.ROW_CSS);
                    colEndDate.Attributes.Add("style", Constants.ROW_CSS);
                    row.Cells.Add(colID);
                    row.Cells.Add(colName);
                    row.Cells.Add(colDepartment);
                    row.Cells.Add(colSubDepartment);
                    row.Cells.Add(colContractType);
                    row.Cells.Add(colStartDate);
                    row.Cells.Add(colEndDate);
                    tbl_Excel.Rows.Add(row);
                }
            }
            #endregion
            #endregion
            #region Expired List
            #region Space
            row = new HtmlTableRow();
            tbl_Excel.Rows.Add(row);
            row = new HtmlTableRow();
            tbl_Excel.Rows.Add(row);
            #endregion
            #region Name List
            row = new HtmlTableRow();
            HtmlTableCell colExpired = new HtmlTableCell();
            colExpired.ColSpan = 4;
            colExpired.Attributes.Add("style", Constants.ROW_SUB_HEADER);
            colExpired.InnerHtml = "Contract has been expired";
            row.Cells.Add(colExpired);
            tbl_Excel.Rows.Add(row);
            #endregion
            #region Grid Title
            row = new HtmlTableRow();
            colID = new HtmlTableCell();
            colName = new HtmlTableCell();
            colDepartment = new HtmlTableCell();
            colSubDepartment = new HtmlTableCell();
            colContractType = new HtmlTableCell();
            colStartDate = new HtmlTableCell();
            colEndDate = new HtmlTableCell();
            colID.InnerHtml = "ID";
            colName.InnerHtml = "Name";
            colDepartment.InnerHtml = "Department";
            colSubDepartment.InnerHtml = "Sub Department";
            colContractType.InnerHtml = "Contract Type";
            colEndDate.InnerHtml = "End Date";
            colStartDate.InnerHtml = "Start Date";
            row.Cells.Add(colID);
            row.Cells.Add(colName);
            row.Cells.Add(colDepartment);
            row.Cells.Add(colSubDepartment);
            row.Cells.Add(colContractType);
            row.Cells.Add(colStartDate);
            row.Cells.Add(colEndDate);
            #region CSS for Title
            colID.Attributes.Add("style", Constants.ROW_TITLE);
            colName.Attributes.Add("style", Constants.ROW_TITLE);
            colDepartment.Attributes.Add("style", Constants.ROW_TITLE);
            colSubDepartment.Attributes.Add("style", Constants.ROW_TITLE);
            colContractType.Attributes.Add("style", Constants.ROW_TITLE);
            colStartDate.Attributes.Add("style", Constants.ROW_TITLE);
            colEndDate.Attributes.Add("style", Constants.ROW_TITLE);
            tbl_Excel.Rows.Add(row);
            #endregion
            #endregion
            #region Row
            foreach (Contract item in expiredList)
            {
                Employee emp = new EmployeeDao().GetById(item.EmployeeId);
                if (emp != null)
                {
                    row = new HtmlTableRow();
                    colID = new HtmlTableCell();
                    colName = new HtmlTableCell();
                    colDepartment = new HtmlTableCell();
                    colSubDepartment = new HtmlTableCell();
                    colContractType = new HtmlTableCell();
                    colStartDate = new HtmlTableCell();
                    colEndDate = new HtmlTableCell();
                    colID.InnerHtml = item.EmployeeId;
                    colName.InnerHtml = emp.LastName + " " + emp.MiddleName + " " + emp.FirstName;
                    colDepartment.InnerHtml = new DepartmentDao().GetDepartmentNameBySub(emp.DepartmentId);
                    colSubDepartment.InnerHtml = emp.Department.DepartmentName;
                    colContractType.InnerHtml = item.ContractType1.ContractTypeName;
                    colStartDate.InnerHtml = item.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                    colEndDate.InnerHtml = item.EndDate.HasValue ? item.EndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "";
                    colID.Attributes.Add("style", Constants.ROW_CSS);
                    colName.Attributes.Add("style", Constants.ROW_CSS);
                    colDepartment.Attributes.Add("style", Constants.ROW_CSS);
                    colSubDepartment.Attributes.Add("style", Constants.ROW_CSS);
                    colContractType.Attributes.Add("style", Constants.ROW_CSS);
                    colStartDate.Attributes.Add("style", Constants.ROW_CSS);
                    colEndDate.Attributes.Add("style", Constants.ROW_CSS);
                    row.Cells.Add(colID);
                    row.Cells.Add(colName);
                    row.Cells.Add(colDepartment);
                    row.Cells.Add(colSubDepartment);
                    row.Cells.Add(colContractType);
                    row.Cells.Add(colStartDate);
                    row.Cells.Add(colEndDate);
                    tbl_Excel.Rows.Add(row);
                }
            }
            #endregion
            #endregion
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "ContractList"));
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            tbl_Excel.RenderControl(hw);
            Response.Write("<meta http-equiv=Content-Type content='text/html; charset=utf-8' />");
            Response.Write(sw.ToString());
            Response.End();
            return View();
        }

        /// <summary>
        /// Check user input Annual Holiday for current year
        /// </summary>
        /// <returns></returns>
        private bool CheckInputAnnualHolidayThisYear()
        {
            AnnualHolidayDao annDao = new AnnualHolidayDao();
            int count = annDao.GetFilterList(string.Empty, DateTime.Now.Year.ToString()).Count();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckNewCommingPTO()
        {
            return true;
        }

        private bool CheckUpdatePTOBalance()
        {
            PTOReportDao reportDao = new PTOReportDao();
            int count = reportDao.GetList(DateTime.Now).Count();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult UpdateBalance()
        {
            PTOReportDao ptoReportDao = new PTOReportDao();
            Message msg = ptoReportDao.UpdatePTOBalance();
            ShowMessage(msg);
            return RedirectToAction("/");            
        }
        
        [ChildActionOnly]
        public ActionResult UndoneServiceRequest()
        {
            var undoneSrList = srDao.GetUndoneListByDate(DateTime.Now);
            return View(undoneSrList);
        }
    }
}
