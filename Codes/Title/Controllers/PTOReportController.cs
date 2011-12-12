using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Configuration;
using CRM.Library.Utils;
using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Attributes;
using CRM.Controllers;
using System.Web.UI.WebControls;


namespace CRM.Controllers
{
    public class PTOReportController : BaseController
    {
        private PTOReportDao reportDao = new PTOReportDao();
        //
        // GET: /PTOReport/

        [CrmAuthorizeAttribute(Module = Modules.PTO_Report, Rights = Permissions.Read, ShowAtCurrentPage = true)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.PTO_REPORT_DEFAULT_VALUE] == null ?
                new Hashtable() : (Hashtable)Session[SessionKey.PTO_REPORT_DEFAULT_VALUE];

            ViewData[Constants.PTO_REPORT_TEXT] = hashData[Constants.PTO_REPORT_TEXT] == null ?
                Constants.EMPLOYEE : !string.IsNullOrEmpty((string)hashData[Constants.PTO_REPORT_TEXT]) ?
                hashData[Constants.PTO_REPORT_TEXT] : Constants.EMPLOYEE;
            ViewData[Constants.PTO_REPORT_MONTH] = hashData[Constants.PTO_REPORT_MONTH] == null ?
                DateTime.Now.ToString("MMM-yyyy") : (string)hashData[Constants.PTO_REPORT_MONTH];

            ViewData[Constants.PTO_REPORT_COLUMN] = hashData[Constants.PTO_REPORT_COLUMN] == null ?
                "Name" : hashData[Constants.PTO_REPORT_COLUMN];
            ViewData[Constants.PTO_REPORT_ORDER] = hashData[Constants.PTO_REPORT_ORDER] == null ?
                "asc" : hashData[Constants.PTO_REPORT_ORDER];
            ViewData[Constants.PTO_REPORT_PAGE_INDEX] = hashData[Constants.PTO_REPORT_PAGE_INDEX] == null ?
                "1" : hashData[Constants.PTO_REPORT_PAGE_INDEX].ToString();
            ViewData[Constants.PTO_REPORT_ROW_COUNT] = hashData[Constants.PTO_REPORT_ROW_COUNT] == null ?
                "20" : hashData[Constants.PTO_REPORT_ROW_COUNT].ToString();

            return View();
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.PTO_REPORT_DEFAULT_VALUE);
            return RedirectToAction("Index");
        }
        public ActionResult DateOffTooltip(string id, string month)
        {
            DateTime to = DateTime.Parse(Constants.DATE_LOCK_PTO + "-" + month);
            DateTime from = to.AddMonths(-1).AddDays(1);
            ViewData["dFrom"] = from;
            ViewData["dTo"] = to;
            List<PTO_Detail> listSub = reportDao.GetListPTOByUserID(id, from, to);
            return View(listSub);
        }
        /// <summary>
        /// Get data and bind to grid
        /// </summary>
        /// <param name="empName"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.PTO_Report, Rights = Permissions.Read, ShowAtCurrentPage = true)]
        public ActionResult GetListJQGrid(string empName, string date)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;

            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

            #endregion

            SetSessionFilter(empName, date, sortColumn, sortOrder, pageIndex, rowCount);

            string employeeName = string.Empty;
            DateTime? reportDate = null;
            DateTime? fromDate = null;
            DateTime? to = null;

            //set value for search params
            if (empName != Constants.EMPLOYEE)
                employeeName = empName.Trim();
            if (!string.IsNullOrEmpty(date))
            {
                reportDate = DateTime.Parse(date);
                to = DateTime.Parse(Constants.DATE_LOCK_PTO + "/" + reportDate.Value.Month + "/" + reportDate.Value.Year);
                fromDate = to.Value.AddMonths(-1).AddDays(1);

            }

            // tan.tran 2011.08.11: get the list of managers, don't display them in the report
            string[] ptoManagerList = Constants.PTO_MANAGER_LIST_NOT_DISPLAY_IN_REPORT.Split(',');

            //get list, ignore the manager list
            var reportList = reportDao.GetList(employeeName, fromDate, to).Where(
                p => !ptoManagerList.Contains(p.EmployeeID)).ToList();


            //for paging
            int totalRecords = reportList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            var finalList = reportDao.Sort(reportList, sortColumn, sortOrder)
                                  .Skip((pageIndex - 1) * rowCount)
                                   .Take(rowCount);
            
            //bind to jqGrid
            var jsonData = new
            {
                total = totalPages,
                page = pageIndex,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        i = m.ID,
                        cell = new string[] {
                            m.ID.ToString(),
                            m.EmployeeID,
                            HttpUtility.HtmlEncode(m.DisplayName),
                            m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            m.ContractedDate.HasValue ? m.ContractedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW): string.Empty,
                            m.CarriedForward.ToString(),
                            reportDao.GetCurrentBalanceInMonth(m.EmployeeID,fromDate,to),
                            m.Used.ToString(),
                            SetDateOff(m.EmployeeID,fromDate.Value,to.Value, false),
                            m.EOMBalance.ToString(),
                            m.SubtractedBalance.ToString(),
                            m.UnpaidLeave.ToString(),     
                            HttpUtility.HtmlEncode(m.Comment),
                            "<input type=\"button\" class=\"icon edit\" title=\"Update EOM balance\" onclick=\"CRM.popup('/PTOReport/Update/" + m.ID.ToString() + "', 'Update EOM balance for "+ m.EmployeeID + " - " + m.DisplayName +"', 500)\" />"
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string SetDateOff(string employeeID, DateTime from, DateTime lockedDate, bool exportToExcel)
        {
            var listSub = reportDao.GetListPTOByUserID(employeeID, from, lockedDate);

            string dateOff = string.Empty;
            string dateOffSep = "<br style=\"mso-data-placement:same-cell;\">";
            foreach (var ptoDetail in listSub)
            {
                if (ptoDetail.DateOff.HasValue)
                {
                    if (ptoDetail.DateOff.Value <= lockedDate)
                    {
                        dateOff += ptoDetail.DateOff.Value.ToString(Constants.DATETIME_FORMAT_VIEW) + (exportToExcel ? 
                            " (" + ptoDetail.TimeOff + " - " + ptoDetail.PTO.PTO_Type.Name + ")" + dateOffSep : "<br />");
                    }// else don't display the date > locked date
                }
                else
                {
                    // case Todate > Locked Date --> display the locked date
                    DateTime dFrom = lockedDate.AddMonths(-1).AddDays(1);
                    string toDate = ptoDetail.DateOffTo.Value <= lockedDate ? ptoDetail.DateOffTo.Value.ToString(Constants.DATETIME_FORMAT_VIEW) :
                        lockedDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                    string fromDate = ptoDetail.DateOffFrom >= dFrom ? ptoDetail.DateOffFrom.Value.ToString(Constants.DATETIME_FORMAT_VIEW) :
                        dFrom.ToString(Constants.DATETIME_FORMAT_VIEW);
                    dateOff += fromDate + " -> " + toDate + (exportToExcel ?  " (" + ptoDetail.PTO.PTO_Type.Name + ")" + dateOffSep : "<br />");
                }
            }
            if (!string.IsNullOrEmpty(dateOff) && exportToExcel)
                dateOff = dateOff.Remove(dateOff.Length - dateOffSep.Length);
            return (string.IsNullOrEmpty(dateOff) || exportToExcel) ? dateOff : CommonFunc.Link(employeeID, "#", dateOff, true);
        }
        //
        // GET: /PTOReport/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }


        //
        // GET: /PTOReport/Edit/5

        public ActionResult Update(int id)
        {
            return View(reportDao.GetPTO(id));
        }

        //
        // POST: /PTOReport/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.PTO_Report, Rights = Permissions.Update, ShowAtCurrentPage = true)]
        public ActionResult Update(PTO_Report report)
        {
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                report.UpdatedBy = principal.UserData.UserName;
                Message msg = reportDao.Update(report);

                if (DateTime.Now.Day > int.Parse(Constants.DATE_LOCK_PTO) &&
                    DateTime.Now.Day < (int.Parse(Constants.DATE_LOCK_PTO_HR_VERIFIED) + 1))
                {
                    PTO_Report reportDb = reportDao.GetPTO(report.ID);
                    reportDao.UpdateBalanceForNextMonth(reportDb.EmployeeId, DateTime.Now);
                }

                ShowMessage(msg);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PTOReport/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PTOReport/Delete/5

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.PTO_Report, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [CrmAuthorizeAttribute(Module = Modules.PTO_Report, Rights = Permissions.Export, ShowAtCurrentPage = true)]
        public void ExportToExcel(string empName, string date, string sortColumn, string sortOrder)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;

            string employeeName = string.Empty;
            DateTime? reportDate = null;
            DateTime? from = null;
            DateTime? to = null;

            //set value for search params
            if (empName != Constants.EMPLOYEE)
            {
                employeeName = empName.Trim();
            }

            if (!string.IsNullOrEmpty(date))
            {
                reportDate = DateTime.Parse(date);
                to = DateTime.Parse(Constants.DATE_LOCK_PTO + "/" + reportDate.Value.Month + "/" + reportDate.Value.Year);
                from = to.Value.AddMonths(-1).AddDays(1);

            }

            // tan.tran 2011.08.11: get the list of managers, don't display them in the report
            string[] ptoManagerList = Constants.PTO_MANAGER_LIST_NOT_DISPLAY_IN_REPORT.Split(',');

            //get list, ignore the manager list
            var reportList = reportDao.GetList(employeeName, from, to).Where(
                p => !ptoManagerList.Contains(p.EmployeeID)).ToList();

            reportList = reportDao.Sort(reportList, sortColumn, sortOrder);


            #region Variables
            HtmlTable tbl_Excel = new HtmlTable();
            tbl_Excel.Width = "100%";
            tbl_Excel.CellPadding = 0;
            tbl_Excel.CellSpacing = 0;
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell column1 = new HtmlTableCell();
            HtmlTableCell column2 = new HtmlTableCell();
            HtmlTableCell column3 = new HtmlTableCell();
            HtmlTableCell column4 = new HtmlTableCell();
            HtmlTableCell column5 = new HtmlTableCell();
            HtmlTableCell column6 = new HtmlTableCell();
            HtmlTableCell column7 = new HtmlTableCell();
            HtmlTableCell column8 = new HtmlTableCell();
            HtmlTableCell column9 = new HtmlTableCell();
            HtmlTableCell column10 = new HtmlTableCell();
            HtmlTableCell column11 = new HtmlTableCell();
            HtmlTableCell column12 = new HtmlTableCell();
            #endregion
            #region white space
            row = new HtmlTableRow();
            column1 = new HtmlTableCell();
            column1.InnerHtml = "<br />";
            row.Cells.Add(column1);
            tbl_Excel.Rows.Add(row);
            #endregion
            #region Title
            row = new HtmlTableRow();
            column1 = new HtmlTableCell();
            column1.Align = HorizontalAlign.Left.ToString();
            column1.VAlign = VerticalAlign.Middle.ToString();
            column1.Attributes.Add("style", "color: CornflowerBlue;font-size:13pt;font-family: Arial;font-style: normal;font-weight: 700");
            column1.ColSpan = 11;
            column1.InnerHtml = Constants.PTO_ADMIN_EXPORT_EXCEL_FILE_NAME + DateTime.Now.ToString("dd-MMM-yy") + "_" + DateTime.Now.ToString("hhmmss");
            column1.NoWrap = true;
            row.Cells.Add(column1);
            tbl_Excel.Rows.Add(row);
            #endregion
            #region white space
            row = new HtmlTableRow();
            column1 = new HtmlTableCell();
            column1.InnerHtml = "<br />";
            row.Cells.Add(column1);
            tbl_Excel.Rows.Add(row);
            #endregion
            #region Header
            row = new HtmlTableRow();
            column1 = new HtmlTableCell();
            column2 = new HtmlTableCell();
            column3 = new HtmlTableCell();
            column4 = new HtmlTableCell();
            column5 = new HtmlTableCell();
            column6 = new HtmlTableCell();
            column7 = new HtmlTableCell();
            column8 = new HtmlTableCell();
            column9 = new HtmlTableCell();
            column10 = new HtmlTableCell();
            column11 = new HtmlTableCell();
            column12 = new HtmlTableCell();

            column1.Align = HorizontalAlign.Center.ToString();
            column2.Align = HorizontalAlign.Center.ToString();
            column3.Align = HorizontalAlign.Center.ToString();
            column4.Align = HorizontalAlign.Center.ToString();
            column5.Align = HorizontalAlign.Center.ToString();
            column6.Align = HorizontalAlign.Center.ToString();
            column7.Align = HorizontalAlign.Center.ToString();
            column8.Align = HorizontalAlign.Center.ToString();
            column9.Align = HorizontalAlign.Center.ToString();
            column10.Align = HorizontalAlign.Center.ToString();
            column11.Align = HorizontalAlign.Center.ToString();
            column12.Align = HorizontalAlign.Center.ToString();
            
            column1.InnerHtml = "ID";
            column2.InnerHtml = "Name";
            column3.InnerHtml = "Start Date";
            column4.InnerHtml = "Contract Date";
            column5.InnerHtml = "Carried forward (C/f) (Hours)";
            column6.InnerHtml = "Monthly Vacation (Hours)";
            column7.InnerHtml = "Used (Hours)";
            column8.InnerHtml = "Date Off";
            column9.InnerHtml = "EOM Balance (Hours)";
            column10.InnerHtml = "Paid leave (Hours)";
            column11.InnerHtml = "Unpaid leave (Hours)";
            column12.InnerHtml = "Note";

            column1.Attributes.Add("style", Constants.EXPORT_DETAIL_HEADER_LEFT + "width:90px;");
            column2.Attributes.Add("style", Constants.EXPORT_DETAIL_HEADER_MIDDLE + "width:200px;");
            column3.Attributes.Add("style", Constants.EXPORT_DETAIL_HEADER_MIDDLE + "width:100px");
            column4.Attributes.Add("style", Constants.EXPORT_DETAIL_HEADER_MIDDLE + "width:130px");
            column5.Attributes.Add("style", Constants.EXPORT_DETAIL_HEADER_MIDDLE + "width:140px");
            column6.Attributes.Add("style", Constants.EXPORT_DETAIL_HEADER_MIDDLE + "width:130px");
            column7.Attributes.Add("style", Constants.EXPORT_DETAIL_HEADER_MIDDLE + "width:130px");
            column8.Attributes.Add("style", Constants.EXPORT_DETAIL_HEADER_MIDDLE + "width:300px");
            column9.Attributes.Add("style", Constants.EXPORT_DETAIL_HEADER_MIDDLE + "width:130px");
            column10.Attributes.Add("style", Constants.EXPORT_DETAIL_HEADER_MIDDLE + "width:130px");
            column11.Attributes.Add("style", Constants.EXPORT_DETAIL_HEADER_MIDDLE + "width:130px");
            column12.Attributes.Add("style", Constants.EXPORT_DETAIL_HEADER_MIDDLE + "width:300px");
            
            row.Cells.Add(column1);
            row.Cells.Add(column2);
            row.Cells.Add(column3);
            row.Cells.Add(column4);
            row.Cells.Add(column5);
            row.Cells.Add(column6);
            row.Cells.Add(column7);
            row.Cells.Add(column8);
            row.Cells.Add(column9);
            row.Cells.Add(column10);
            row.Cells.Add(column11);
            row.Cells.Add(column12);
            tbl_Excel.Rows.Add(row);
            #endregion
            #region Data
            foreach (var item in reportList)
            {
                string dateOff = SetDateOff(item.EmployeeID, from.Value, to.Value, true);
                row = new HtmlTableRow();
                column1 = new HtmlTableCell();
                column2 = new HtmlTableCell();
                column3 = new HtmlTableCell();
                column4 = new HtmlTableCell();
                column5 = new HtmlTableCell();
                column6 = new HtmlTableCell();
                column7 = new HtmlTableCell();
                column8 = new HtmlTableCell();
                column9 = new HtmlTableCell();
                column10 = new HtmlTableCell();
                column11 = new HtmlTableCell();
                column12 = new HtmlTableCell();
                
                column1.Align = HorizontalAlign.Center.ToString();
                column2.Align = HorizontalAlign.Left.ToString();
                column3.Align = HorizontalAlign.Center.ToString();
                column4.Align = HorizontalAlign.Center.ToString();
                column5.Align = HorizontalAlign.Center.ToString();
                column6.Align = HorizontalAlign.Center.ToString();
                column7.Align = HorizontalAlign.Center.ToString();
                column8.Align = HorizontalAlign.Left.ToString();
                column9.Align = HorizontalAlign.Center.ToString();
                column10.Align = HorizontalAlign.Center.ToString();
                column11.Align = HorizontalAlign.Center.ToString();
                column12.Align = HorizontalAlign.Center.ToString();
                
                column1.InnerHtml = item.EmployeeID;
                column2.InnerHtml = item.DisplayName;
                column3.InnerHtml = item.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                column4.InnerHtml = item.ContractedDate.HasValue ? item.ContractedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : string.Empty;
                column5.InnerHtml = item.CarriedForward.ToString();
                column6.InnerHtml = reportDao.GetCurrentBalanceInMonth(item.EmployeeID, from, to);
                column7.InnerHtml = item.Used.ToString();
                column8.InnerHtml = dateOff;
                column9.InnerHtml = item.EOMBalance.ToString();
                column10.InnerHtml = item.SubtractedBalance.ToString();
                column11.InnerHtml = item.UnpaidLeave.ToString();
                column12.InnerHtml = item.Comment;
                
                column1.Attributes.Add("style", Constants.EXPORT_DETAIL_DATA_LEFT);
                column2.Attributes.Add("style", Constants.EXPORT_DETAIL_DATA_MIDDLE);
                column3.Attributes.Add("style", Constants.EXPORT_DETAIL_DATA_MIDDLE);
                column4.Attributes.Add("style", Constants.EXPORT_DETAIL_DATA_MIDDLE);
                column5.Attributes.Add("style", Constants.EXPORT_DETAIL_DATA_MIDDLE);
                column6.Attributes.Add("style", Constants.EXPORT_DETAIL_DATA_MIDDLE);
                column7.Attributes.Add("style", Constants.EXPORT_DETAIL_DATA_MIDDLE);
                column8.Attributes.Add("style", Constants.EXPORT_DETAIL_DATA_LEFT);
                column9.Attributes.Add("style", Constants.EXPORT_DETAIL_DATA_MIDDLE);
                column10.Attributes.Add("style", Constants.EXPORT_DETAIL_DATA_MIDDLE);
                column11.Attributes.Add("style", Constants.EXPORT_DETAIL_DATA_MIDDLE);
                column12.Attributes.Add("style", Constants.EXPORT_DETAIL_DATA_MIDDLE);
                
                row.Cells.Add(column1);
                row.Cells.Add(column2);
                row.Cells.Add(column3);
                row.Cells.Add(column4);
                row.Cells.Add(column5);
                row.Cells.Add(column6);
                row.Cells.Add(column7);
                row.Cells.Add(column8);
                row.Cells.Add(column9);
                row.Cells.Add(column10);
                row.Cells.Add(column11);
                row.Cells.Add(column12);
                tbl_Excel.Rows.Add(row);
            }


            #endregion


            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            tbl_Excel.RenderControl(hw);
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", Constants.PTO_ADMIN_EXPORT_EXCEL_FILE_NAME + DateTime.Now.ToString("dd-MMM-yy") + "_" + DateTime.Now.ToString("hhmmss")));
            Response.ContentType = "application/vnd.ms-excel";
            Response.Write(sw.ToString());
            Response.End();

        }

        /// <summary>
        /// Set Session to Filter
        /// </summary>
        /// <param name="filterText"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <param name="month"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilter(string filterText, string month,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.PTO_REPORT_TEXT, filterText);
            hashData.Add(Constants.PTO_REPORT_MONTH, month);

            hashData.Add(Constants.PTO_REPORT_COLUMN, column);
            hashData.Add(Constants.PTO_REPORT_ORDER, order);
            hashData.Add(Constants.PTO_REPORT_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.PTO_REPORT_ROW_COUNT, rowCount);

            Session[SessionKey.PTO_REPORT_DEFAULT_VALUE] = hashData;
        }


    }
}
