using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Attributes;
using CRM.Models;
using CRM.Library.Common;

namespace CRM.Controllers
{
    /// <summary>
    /// Annual Holiday Controller
    /// </summary>
    public class AnnualHolidayController : BaseController
    {
        /// <summary>
        /// annual holiday dao
        /// </summary>
        private AnnualHolidayDao annualHolidayDao = new AnnualHolidayDao();
        //
        // GET: /AnnualHoliday/
        /// <summary>
        /// Action: Annual Holiday list
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.AnnualHoliday, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.ANNUAL_HOLIDAY_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.ANNUAL_HOLIDAY_DEFAULT_VALUE];

            ViewData[Constants.ANNUAL_HOLIDAY_TEXT] = hashData[Constants.ANNUAL_HOLIDAY_TEXT] == null ? Constants.ANNUAL_HOLIDAY_NAME : !string.IsNullOrEmpty((string)hashData[Constants.ANNUAL_HOLIDAY_TEXT]) ? hashData[Constants.ANNUAL_HOLIDAY_TEXT] : Constants.ANNUAL_HOLIDAY_NAME;            
            ViewData[Constants.ANNUAL_HOLIDAY_YEAR] = new SelectList(annualHolidayDao.GetListOfExistingYear(), "Value", "Value", hashData[Constants.ANNUAL_HOLIDAY_YEAR] == null ? Constants.FIRST_ITEM_YEAR : hashData[Constants.ANNUAL_HOLIDAY_YEAR]);
            
            ViewData[Constants.ANNUAL_HOLIDAY_COLUMN] = hashData[Constants.ANNUAL_HOLIDAY_COLUMN] == null ? "Date" : hashData[Constants.ANNUAL_HOLIDAY_COLUMN];
            ViewData[Constants.ANNUAL_HOLIDAY_ORDER] = hashData[Constants.ANNUAL_HOLIDAY_ORDER] == null ? "asc" : hashData[Constants.ANNUAL_HOLIDAY_ORDER];
            ViewData[Constants.ANNUAL_HOLIDAY_PAGE_INDEX] = hashData[Constants.ANNUAL_HOLIDAY_PAGE_INDEX] == null ? "1" : hashData[Constants.ANNUAL_HOLIDAY_PAGE_INDEX].ToString();
            ViewData[Constants.ANNUAL_HOLIDAY_ROW_COUNT] = hashData[Constants.ANNUAL_HOLIDAY_ROW_COUNT] == null ? "20" : hashData[Constants.ANNUAL_HOLIDAY_ROW_COUNT].ToString();

            return View();
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.ANNUAL_HOLIDAY_DEFAULT_VALUE);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action: GetListJQGrid (Get list of Annual Holiday to show on jqgrid at index page)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.AnnualHoliday, Rights = Permissions.Read, ShowAtCurrentPage=true)]
        public ActionResult GetListJQGrid(string name, string year)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(name, year, sortColumn, sortOrder, pageIndex, rowCount);

            if (name.Trim().ToLower().Equals(Constants.ANNUAL_HOLIDAY_NAME.ToLower()))
            {
                name = string.Empty;
            }

            //Get list of questions, listening topic, comprehension paragraph
            List<AnnualHoliday> holidayList = annualHolidayDao.GetFilterList(name,year);
            int totalRecords = holidayList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            //Sort the list
            var finalList = annualHolidayDao.Sort(holidayList, sortColumn, sortOrder)
                                  .Skip((pageIndex - 1) * rowCount).Take(rowCount);
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
                            HttpUtility.HtmlEncode(m.HolidayName),
                            m.HolidayDate.DayOfWeek.ToString(),
                            m.HolidayDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            HttpUtility.HtmlEncode(m.Description),
                            CommonFunc.Button("edit", "Edit", "CRM.popup('/AnnualHoliday/Edit/" + m.ID + 
                            "', 'Edit Holiday', 500)")
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /AnnualHoliday/Create
        /// <summary>
        /// create (GET)
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.AnnualHoliday, Rights = Permissions.Insert, ShowInPopup =true)]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AnnualHoliday/Create
        /// <summary>
        /// Create (POST)
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.AnnualHoliday, Rights = Permissions.Insert, ShowInPopup = true)]
        public JsonResult Create(AnnualHoliday holiday)
        {
            // TODO: Add insert logic here
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                holiday.CreatedBy = principal.UserData.UserName;
                holiday.UpdatedBy = principal.UserData.UserName;
                result.Data = annualHolidayDao.Insert(holiday);
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }
        
        //
        // GET: /AnnualHoliday/Edit/5
        /// <summary>
        /// Edit (GET)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.AnnualHoliday, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Edit(int id)
        {
            return View(annualHolidayDao.GetByID(id));
        }

        //
        // POST: /AnnualHoliday/Edit/5
        /// <summary>
        /// Edit (Post)
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.AnnualHoliday, Rights = Permissions.Update, ShowInPopup = true)]
        public JsonResult Edit(AnnualHoliday holiday)
        {
             JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                holiday.UpdatedBy = principal.UserData.UserName;
                result.Data = annualHolidayDao.Update(holiday);
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }

        /// <summary>
        /// Delete a list of holiday
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.AnnualHoliday, Rights = Permissions.Delete, ShowInPopup = true)]
        public ActionResult DeleteList(string id)
        {
            Message msg = annualHolidayDao.DeleteList(id);
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }
        /// <summary>
        /// Export Holiday list to excel
        /// </summary>
        /// <param name="name"></param>
        /// <param name="year"></param>
        /// <param name="sortname"></param>
        /// <param name="sortorder"></param>
        [CrmAuthorizeAttribute(Module = Modules.AnnualHoliday, Rights = Permissions.Export, ShowAtCurrentPage = true)]
        public void Export(string name, string year, string sortname, string sortorder)
        {
            List<AnnualHoliday> holidayList = annualHolidayDao.GetFilterList(name, year);
            holidayList = annualHolidayDao.Sort(holidayList, sortname, sortorder);
            ExportExcel exp = new ExportExcel();
            exp.Title = Constants.ANNUAL_HOLIDAY_LIST_TITLE_EXPORT_EXCEL;
            exp.FileName = Constants.ANNUAL_HOLIDAY_LIST_FILE_NAME_PREFIX +
                string.Format("-{0}.xls", DateTime.Now.ToString("dd-MMM-yyyy"));
            exp.ColumnList = new string[] { "HolidayName", "HolidayDate:dayofweek", "HolidayDate:date" };
            exp.HeaderExcel = new string[] { "Holiday Name", "Day", "Date" };
            exp.List = holidayList;
            exp.IsRenderNo = true;
            exp.Execute();
        }

        /// <summary>
        /// Set Session to Filter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="year"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilter(string name, string year,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.ANNUAL_HOLIDAY_TEXT, name);
            hashData.Add(Constants.ANNUAL_HOLIDAY_YEAR, year);
            hashData.Add(Constants.ANNUAL_HOLIDAY_COLUMN, column);
            hashData.Add(Constants.ANNUAL_HOLIDAY_ORDER, order);
            hashData.Add(Constants.ANNUAL_HOLIDAY_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.ANNUAL_HOLIDAY_ROW_COUNT, rowCount);

            Session[SessionKey.ANNUAL_HOLIDAY_DEFAULT_VALUE] = hashData;
        }
    }
}
