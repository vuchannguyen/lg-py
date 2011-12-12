using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using System.Collections;
using CRM.Library.Common;
using CRM.Areas.Portal.Models;
using CRM.Library.Attributes;
using System.Configuration;
using CRM.Library.Utils;

namespace CRM.Controllers
{
    /// <summary>
    /// Controller: PTOAdmin
    /// </summary>
    public class PTOAdminController : BaseController
    {
        //
        // GET: /PTOAdmin/
        #region variables
        /// <summary>
        /// DAO: PTO
        /// </summary>
        private PTODao ptoDao = new PTODao();
        private LogDao logDao = new LogDao();
        /// <summary>
        /// DAO: PTO_Status
        /// </summary>
        private PTOStatusDao ptoStatusDao = new PTOStatusDao();
        /// <summary>
        /// DAO:PTO_Type
        /// </summary>
        private PTOTypeDao ptoTypeDao = new PTOTypeDao();
        /// <summary>
        /// DAO: Employee
        /// </summary>
        private EmployeeDao empDao = new EmployeeDao();
        private PTOReportDao ptoReportDao = new PTOReportDao();
        private string sDateOffKeyPrefix = "txtDateOff_";
        private string sHoursKeyPrefix = "txtHours_";
        private string sFromHrsInputNamePrefix = "ddl_From_";
        private string sToHrsInputNamePrefix = "ddl_To_";
        private string sRemindEmailTemplate = "";
        #endregion
        /// <summary>
        /// Index page of PTO Admin
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.PTO_Admin, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.PTO_ADMIN_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.PTO_ADMIN_DEFAULT_VALUE];

            ViewData[Constants.PTO_ADMIN_TEXT] = hashData[Constants.PTO_ADMIN_TEXT] == null ? Constants.PTO_ADMIN_EMPLOYEE_NAME : !string.IsNullOrEmpty((string)hashData[Constants.PTO_ADMIN_TEXT]) ? hashData[Constants.PTO_ADMIN_TEXT] : Constants.PTO_ADMIN_EMPLOYEE_NAME;
            ViewData[Constants.PTO_ADMIN_STATUS] = new SelectList(ptoStatusDao.GetList(), "id", "name", hashData[Constants.PTO_ADMIN_STATUS] == null ? Constants.PTO_FIRST_STATUS : hashData[Constants.PTO_ADMIN_STATUS]);
            if (hashData[Constants.PTO_ADMIN_TYPE] != null)
            {
                if (!string.IsNullOrEmpty((string)hashData[Constants.PTO_ADMIN_TYPE]))
                {
                    int typeID = int.Parse((string)hashData[Constants.PTO_ADMIN_TYPE]);
                    int parentTypeID = ptoTypeDao.GetParentIDByTypeID(typeID);
                    ViewData[CommonDataKey.PTO_ADMIN_TYPE_PARENT_ID] = new SelectList(ptoTypeDao.GetParentTypeList(), "ID", "Name", parentTypeID);
                    ViewData[CommonDataKey.PTO_ADMIN_TYPE_ID] = new SelectList(ptoTypeDao.GetTypeListByParentID(parentTypeID), "ID", "Name", typeID);
                }
                else
                {
                    ViewData[CommonDataKey.PTO_ADMIN_TYPE_PARENT_ID] = new SelectList(ptoTypeDao.GetParentTypeList(), "ID", "Name", string.Empty);
                    List<PTO_Type> list = new List<PTO_Type>();
                    ViewData[CommonDataKey.PTO_ADMIN_TYPE_ID] = new SelectList(list, "id", "name", string.Empty);
                }
            }
            else
            {
                ViewData[CommonDataKey.PTO_ADMIN_TYPE_PARENT_ID] = new SelectList(ptoTypeDao.GetParentTypeList(), "ID", "Name", string.Empty);
                List<PTO_Type> list = new List<PTO_Type>();
                ViewData[CommonDataKey.PTO_ADMIN_TYPE_ID] = new SelectList(list, "id", "name", string.Empty);
            }

            ViewData[Constants.PTO_ADMIN_MONTH] = hashData[Constants.PTO_ADMIN_MONTH] == null ? DateTime.Now.ToString("MMM-yyyy") : (string)hashData[Constants.PTO_ADMIN_MONTH];

            ViewData[Constants.PTO_ADMIN_COLUMN] = hashData[Constants.PTO_ADMIN_COLUMN] == null ? "Employee" : hashData[Constants.PTO_ADMIN_COLUMN];
            ViewData[Constants.PTO_ADMIN_ORDER] = hashData[Constants.PTO_ADMIN_ORDER] == null ? "asc" : hashData[Constants.PTO_ADMIN_ORDER];
            ViewData[Constants.PTO_ADMIN_PAGE_INDEX] = hashData[Constants.PTO_ADMIN_PAGE_INDEX] == null ? "1" : hashData[Constants.PTO_ADMIN_PAGE_INDEX].ToString();
            ViewData[Constants.PTO_ADMIN_ROW_COUNT] = hashData[Constants.PTO_ADMIN_ROW_COUNT] == null ? "20" : hashData[Constants.PTO_ADMIN_ROW_COUNT].ToString();

            return View();
        }

        public ActionResult Refresh(string id)
        {
            string view = string.Empty;
            switch (id)
            {
                case "2":
                    Session.Remove(SessionKey.PTO_CONFIRM_DEFAULT_VALUE);
                    view = "PtoToConfirm";
                    break;
                default:
                    Session.Remove(SessionKey.PTO_ADMIN_DEFAULT_VALUE);
                    view = "Index";
                    break;
            }
            return RedirectToAction(view);
        }

        /// <summary>
        /// Get PTO list to show on index page
        /// </summary>
        /// <param name="filterText"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.PTO_Admin, Rights = Permissions.Read, ShowAtCurrentPage = true)]
        public ActionResult GetListJQGrid(string filterText, string status, string type, string month)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(filterText, status, type, month, sortColumn, sortOrder, pageIndex, rowCount);
            
            #region search
            
            List<sp_GetPTOListForAdminResult> empList = GetPTOList(filterText, status, type, month);
            int totalRecords = empList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = ptoDao.Sort(empList, sortColumn, sortOrder).
                Skip((currentPage - 1) * rowCount).Take(rowCount);

            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        i = finalList.ToList().IndexOf(m),
                        cell = new string[] {        
                                CommonFunc.PtoIcon(m.StatusID),
                                CommonFunc.Link(m.ID, "ptoTooltip", "#", m.ID),
                                CommonFunc.Link(GetEmpoyeeIDFromPTO_ID(m.ID), "empTooltip", "/Employee/Detail/" + 
                                    GetEmpoyeeIDFromPTO_ID(m.ID), m.Submitter),
                                m.Hours.Value.ToString(),
                                m.CreateDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                                m.StatusName,                                
                                m.TypeName,                                    
                                m.Reason == null ? "" : HttpUtility.HtmlEncode(m.Reason),
                                SetAction(m.ID, m.Key_ID, month)                                
                            }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string SetAction(string ID, int key_id, string month)
        {
            DateTime monthYear = DateTime.Parse(Constants.DATE_LOCK_PTO + month);
            DateTime lockedVerifyTo = DateTime.Parse(Constants.DATE_LOCK_PTO_HR_VERIFIED + month);
            DateTime lockedVerifyFrom = DateTime.Parse(Constants.DATE_LOCK_PTO_HR_VERIFIED + month).AddMonths(-1).AddDays(1);
            string action = string.Empty;
            var pto = ptoDao.GetPTOByKey(key_id);
            if ((pto.Status_ID == Constants.PTO_STATUS_CONFIRM || pto.Status_ID == Constants.PTO_STATUS_VERIFIED)
                && DateTime.Now.Date <= lockedVerifyTo.Date && DateTime.Now.Date >= lockedVerifyFrom.Date)
            {
                action = CommonFunc.Button("edit", "Verify", "CRM.popup('/PTOAdmin/Verify/" +
                                    ID + "','Verify " + ID + "',600)");
            }
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            if(CommonFunc.CheckAuthorized(principal.UserData.UserID, (int) Modules.PTO_Admin, (int) Permissions.Delete))
            {
                string funcname = string.Format("CRM.deleteItemConfirm('{0}','{1}','{2}')", "/PTOAdmin/DeletePTO", key_id, "/PTOAdmin");
                //action += CommonFunc.Button("delete", "Delete PTO",
                //"CRM.msgConfirmBox('Are you sure you want to delete this PTO?','450','javascript:DeletePTO(" + key_id + ");')");
                action += CommonFunc.Button("delete", "Delete PTO", funcname);
            }

            return action;
        }

        /// <summary>
        /// List of PTOs need to be confirmed by managers
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.PTO_Admin, Rights = Permissions.Read)]
        public ActionResult PtoToConfirm()
        {
            Hashtable hashData = Session[SessionKey.PTO_CONFIRM_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.PTO_CONFIRM_DEFAULT_VALUE];

            ViewData[Constants.PTO_CONFIRM_TEXT] = hashData[Constants.PTO_CONFIRM_TEXT] == null ? Constants.PTO_ADMIN_EMPLOYEE_NAME : !string.IsNullOrEmpty((string)hashData[Constants.PTO_CONFIRM_TEXT]) ? hashData[Constants.PTO_CONFIRM_TEXT] : Constants.PTO_ADMIN_EMPLOYEE_NAME;
            ViewData[Constants.PTO_CONFIRM_MANAGER] = new SelectList(empDao.GetManager(null, 0, 0), "id", "displayname", hashData[Constants.PTO_CONFIRM_MANAGER] == null ? Constants.FIRST_MANAGER : hashData[Constants.PTO_CONFIRM_MANAGER]);
            if (hashData[Constants.PTO_CONFIRM_TYPE] != null)
            {
                if (!string.IsNullOrEmpty((string)hashData[Constants.PTO_CONFIRM_TYPE]))
                {
                    int typeID = int.Parse((string)hashData[Constants.PTO_CONFIRM_TYPE]);
                    int parentTypeID = ptoTypeDao.GetParentIDByTypeID(typeID);
                    ViewData[CommonDataKey.PTO_TYPE_PARENT_ID] = new SelectList(ptoTypeDao.GetParentTypeList(), "ID", "Name", parentTypeID);
                    ViewData[CommonDataKey.PTO_TYPE_LIST] = new SelectList(ptoTypeDao.GetTypeListByParentID(parentTypeID), "ID", "Name", typeID);
                }
                else
                {
                    ViewData[CommonDataKey.PTO_TYPE_PARENT_ID] = new SelectList(ptoTypeDao.GetParentTypeList(), "ID", "Name", string.Empty);
                    List<PTO_Type> list = new List<PTO_Type>();
                    ViewData[CommonDataKey.PTO_TYPE_LIST] = new SelectList(list, "id", "name", string.Empty);
                }
            }
            else
            {
                ViewData[CommonDataKey.PTO_TYPE_PARENT_ID] = new SelectList(ptoTypeDao.GetParentTypeList(), "ID", "Name", string.Empty);
                List<PTO_Type> list = new List<PTO_Type>();
                ViewData[CommonDataKey.PTO_TYPE_LIST] = new SelectList(list, "id", "name", string.Empty);
            }
            ViewData[Constants.PTO_CONFIRM_COLUMN] = hashData[Constants.PTO_CONFIRM_COLUMN] == null ? "Employee" : hashData[Constants.PTO_CONFIRM_COLUMN];
            ViewData[Constants.PTO_CONFIRM_ORDER] = hashData[Constants.PTO_CONFIRM_ORDER] == null ? "asc" : hashData[Constants.PTO_CONFIRM_ORDER];
            ViewData[Constants.PTO_CONFIRM_PAGE_INDEX] = hashData[Constants.PTO_CONFIRM_PAGE_INDEX] == null ? "1" : hashData[Constants.PTO_CONFIRM_PAGE_INDEX].ToString();
            ViewData[Constants.PTO_CONFIRM_ROW_COUNT] = hashData[Constants.PTO_CONFIRM_ROW_COUNT] == null ? "20" : hashData[Constants.PTO_CONFIRM_ROW_COUNT].ToString();

            //List<PTO_Type> lType = ptoTypeDao.GetList();
            //var lManager = empDao.GetManager( null, 0, 0);
            //ViewData[CommonDataKey.PTO_MANAGER_LIST] = new SelectList(lManager,
            //    "id", "displayname", Constants.PTO_DEFAULT_FILTER_STATUS);
            //ViewData[CommonDataKey.PTO_TYPE_LIST] = new SelectList(lType, "id", "name");
            return View();
        }


        /// <summary>
        /// Get PTO list to show in Reminder page
        /// </summary>
        /// <param name="filterText"></param>
        /// <param name="managerId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.PTO_Admin, Rights = Permissions.Read, ShowAtCurrentPage = true)]
        public ActionResult GetListPtoToConfirmJQGrid(string filterText, string managerId, string type)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilterPTOConfirm(filterText, managerId, type, sortColumn, sortOrder, pageIndex, rowCount);

            string sMonth = "" + DateTime.Now.Month + "-" + DateTime.Now.Year;
            List<sp_GetPTOListForAdminResult> empList = GetPTOList(filterText, "", type, sMonth);
            empList = empList.Where(p=>p.StatusID == Constants.PTO_STATUS_APPROVED ||
                p.StatusID == Constants.PTO_STATUS_NEW).ToList();
            string[] sManagerIds = GetManagerIdArr(empList);
            if (!string.IsNullOrEmpty(managerId))
            {
                empList = empList.Where(p=> new PTODao().GetPTOById(p.ID).SubmitTo.Equals(managerId)).ToList();
            }
            int totalRecords = empList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = ptoDao.Sort(empList, sortColumn, sortOrder).
                Skip((currentPage - 1) * rowCount).Take(rowCount);
            
            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        i = finalList.ToList().IndexOf(m),
                        cell = new string[] {        
                                m.ID,
                                CommonFunc.Link(GetEmpoyeeIDFromPTO_ID(m.ID), "empTooltip", "/Employee/Detail/" + 
                                    GetEmpoyeeIDFromPTO_ID(m.ID), m.Submitter),
                                CommonFunc.GetEmployeeFullName(ptoDao.GetPTOById(m.ID).Employee1, 
                                    Constants.FullNameFormat.FirstMiddleLast) + " - Department: " + 
                                    ptoDao.GetPTOById(m.ID).Employee1.Department.DepartmentName.Split('-')[0],
                                m.Hours.Value.ToString(),
                                m.TypeName,
                                m.Balance + "",
                                m.Reason == null ? "" : HttpUtility.HtmlEncode(m.Reason),
                                String.Join(",", sManagerIds)
                            }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Send notification email about PTOs need to be confirmed to a manager
        /// </summary>
        /// <param name="managerId"></param>
        [NonAction]
        public void SendRemindEmailToManager(string managerId)
        {
            if (string.IsNullOrEmpty(sRemindEmailTemplate))
            {
                string path = Server.MapPath(Constants.PTO_REMIND_EMAIL_PATH);
                sRemindEmailTemplate = System.IO.File.ReadAllText(path);
            }
            string emailBody = sRemindEmailTemplate;
            string sRowFormat = "<tr>" +
                                    "<td>{0}</td>" + 
                                    "<td>{1}</td>" +
                                    "<td align='center'>{2}</td>" +
                                    "<td>{3}</td>" +
                                    "<td align='center'>{4}</td>" +
                                    "<td>{5}</td>" +
                                "</tr>";
            Employee manager = empDao.GetById(managerId);
            var ptoList = ptoDao.GetListOfPtoNeedToBeConfirmed(DateTime.Now, managerId);
            string sRows = "";
            foreach( var tmpPto in ptoList)
            {
                string sNewRow = String.Format(sRowFormat, tmpPto.ID, tmpPto.Submitter, tmpPto.Hours, 
                    tmpPto.TypeName, tmpPto.Balance, HttpUtility.HtmlEncode(tmpPto.Reason));
                sRows += sNewRow;
            }
            emailBody = emailBody.Replace(Constants.PTO_REMIND_MANAGER_NAME, 
                CommonFunc.GetEmployeeFullName(manager, Constants.FullNameFormat.FirstMiddleLast));
            emailBody = emailBody.Replace(Constants.PTO_REMIND_MANAGER_DEAD_LINE, 
                CommonFunc.GetPtoDateTo(DateTime.Now).ToString(Constants.DATETIME_FORMAT_VIEW));
            emailBody = emailBody.Replace(Constants.PTO_REMIND_MANAGER_ITEMS, 
                sRows);
            string host = ConfigurationManager.AppSettings["mailserver_host"];
            string port = ConfigurationManager.AppSettings["mailserver_port"];

            string from = Constants.CRM_MAIL_FROM_ADDRESS;
            string fromName = Constants.CRM_MAIL_FROM_NAME;
            string toEmail = manager.OfficeEmail;
            string sEmailSubject = Constants.PTO_REMIND_EMAIL_SUBJECT;
            string ccMail = "";
            WebUtils.SendMail(host, port, from, fromName, toEmail, ccMail, sEmailSubject, emailBody);    
        }
        /// <summary>
        /// Send Remider Email to managers
        /// </summary>
        /// <param name="managerIds"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.PTO_Admin, Rights = Permissions.Read)]
        public JsonResult SendRemindEmail(string managerIds)
        {
            JsonResult result = new JsonResult();
            //managerIds = "1632";//Remove this when finish testing.
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                string[] managerIdArr = managerIds.Split(',');
                foreach (string tmpId in managerIdArr)
                {
                    SendRemindEmailToManager(tmpId);
                }
                result.Data = new Message(MessageConstants.I0001, MessageType.Info, "Email(s)", "sent");
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }
        /// <summary>
        /// Get the array of manager id in the pto list
        /// </summary>
        /// <param name="ptoList"></param>
        /// <returns></returns>
        [NonAction]
        public string[] GetManagerIdArr(List<sp_GetPTOListForAdminResult> ptoList)
        {
            List<string> listResult = new List<string>();
            foreach (var item in ptoList)
            { 
                PTO tempPto = ptoDao.GetPTOById(item.ID);
                if (!listResult.Contains(tempPto.SubmitTo))
                {
                    listResult.Add(tempPto.SubmitTo);
                }
            }
            return listResult.ToArray();
        }
        /// <summary>
        /// Get PTO list filter by name, status, type, month
        /// </summary>
        /// <param name="empName"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [NonAction]
        public List<sp_GetPTOListForAdminResult> GetPTOList(string empName, string status, string type, string month)
        {
            int iStatus = 0;
            int iResult = 0;
            DateTime? tfrom = null;
            DateTime? tto = null;
            if (!string.IsNullOrEmpty(status))
            {
                iStatus = int.Parse(status);
            }
            if (!string.IsNullOrEmpty(type))
            {
                iResult = int.Parse(type);
            }
            if (!string.IsNullOrEmpty(month))
            {
                try
                {
                    // filter month is date end of month and this date dependent on configuration
                    // ex: month:Feb-2011 => from: 25-Feb-2011, to: 26-Feb-2011
                    tto = DateTime.Parse(Constants.DATE_LOCK_PTO + month);
                    tfrom = tto.Value.AddMonths(-1).AddDays(1);
                }
                catch
                {
                    // get mon by curent date
                    month = DateTime.Now.ToString(Constants.PTO_MANAGER_DATE_FORMAT);
                    tto = DateTime.Parse(Constants.DATE_LOCK_PTO + month);
                    tfrom = tto.Value.AddMonths(-1).AddDays(1);      
                }
            }
            #endregion
            if (empName == Constants.PTO_ADMIN_EMPLOYEE_NAME)
            {
                empName = string.Empty;
            }
            return ptoDao.GetPTOListForAdmin(empName, iStatus, iResult, tfrom, tto);
        }
        /// <summary>
        /// Export PTO list to excel
        /// </summary>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <param name="month"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        [CrmAuthorizeAttribute(Module = Modules.PTO_Admin, Rights = Permissions.Export, ShowAtCurrentPage = true)]
        public void ExportToExcel(string name, string status, string type, string month, 
            string sortColumn, string sortOrder)
        {
            List<sp_GetPTOListForAdminResult> empList = GetPTOList(name, status, type, month);
            var finalList = ptoDao.Sort(empList, sortColumn, sortOrder);

            ExportExcel exp = new ExportExcel();
            string[] column = new string[] { "ID", "Submitter", "Hours", "StatusName", 
                "TypeName", "Balance", "Reason" };
            string[] header = new string[] { "ID", "Employee", "Hour(s)", "Status", 
                "PTO Type", "Balance", "Reason" };

            exp.Title = Constants.PTO_ADMIN_TITLE_EXPORT_EXCEL;
            exp.FileName = Constants.PTO_ADMIN_EXPORT_EXCEL_FILE_NAME + string.Format("{0}_{1}.xls",
                DateTime.Now.ToString("ddMMMyy"), DateTime.Now.ToString("hhmmss"));
            exp.ColumnList = column;
            exp.HeaderExcel = header;
            exp.List = finalList;
            exp.IsRenderNo = true;
            exp.Execute();
        }
        /// <summary>
        /// Return the EmployeeID from PTO ID
        /// </summary>
        /// <param name="ptoID"></param>
        /// <returns></returns>
        [NonAction]
        public string GetEmpoyeeIDFromPTO_ID(string ptoID)
        {
            return ptoID.Substring(4,4);
        }
        /// <summary>
        /// GET: Create new PTO for employee
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.PTO_Admin, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create()
        {
            List<PTO_Type> lType = ptoTypeDao.GetList();
            ViewData[CommonDataKey.PTO_TYPE_PARENT_ID] = new SelectList(ptoTypeDao.GetParentTypeList(), "ID", "Name", string.Empty);
            List<PTO_Type> list = new List<PTO_Type>();
            ViewData[CommonDataKey.PTO_TYPE_LIST] = new SelectList(list, "id", "name", string.Empty);
            string[] sArrTypeIDs_isHourType = lType.Where(p => p.IsHourType).
                Select(p => p.ID.ToString()).ToArray();
            ViewData[CommonDataKey.PTO_IDS_IS_HOUR_TYPE] = String.Join(",", sArrTypeIDs_isHourType);
            return View();
        }
        /// <summary>
        /// POST: Create new PTO
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.PTO_Admin, Rights = Permissions.Insert, ShowInPopup = true)]
        public JsonResult Create(FormCollection collection)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            PTO objUI = new PTO();
            objUI.Submitter = collection["Submitter"];
            objUI.SubmitTo = collection["SubmitTo"];
            if (string.IsNullOrEmpty(objUI.Submitter))
            {
                result.Data = new Message(MessageConstants.E0030, MessageType.Error, "Employee");
            }
            else if (string.IsNullOrEmpty(objUI.SubmitTo))
            {
                result.Data = new Message(MessageConstants.E0030, MessageType.Error, "Manager");
            }
            //Successful case
            else
            {
                objUI.ID = CommonFunc.SetPTO_ID(objUI.Submitter);
                objUI.Status_ID = Constants.PTO_STATUS_CONFIRM;
                objUI.CreateDate = DateTime.Now;
                objUI.CreatedBy = principal.UserData.UserName;
                objUI.UpdateDate = DateTime.Now;
                objUI.UpdatedBy = principal.UserData.UserName;
                objUI.PTOType_ID = int.Parse(collection.Get(CommonDataKey.PTO_TYPE_LIST));
                objUI.Reason = collection.Get("reason");
                objUI.HRComment = collection.Get("HRComment");
                PTO_Type objPTO_Type = ptoTypeDao.GetByID(objUI.PTOType_ID);
                List<PTO_Detail> listObjPTO_Detail = new List<PTO_Detail>();
                PTO_Detail objPTO_Detail = new PTO_Detail();
                //if PTO is hour type
                if (objPTO_Type.IsHourType)
                {
                    string sDateOffKeyPrefix = Constants.PTO_DATE_OFF_KEY_PREFIX;
                    string sFromHrsInputNamePrefix = Constants.PTO_FROM_HRS_INPUT_NAME_PREFIX;
                    string sToHrsInputNamePrefix = Constants.PTO_TO_HRS_INPUT_NAME_PREFIX;
                    var dateOffList = Request.Form.AllKeys.Where(p => p.Contains(sDateOffKeyPrefix)).ToList();
                    foreach (var key in dateOffList)
                    {
                        PTO_Detail objDetail = new PTO_Detail();
                        int index = ConvertUtil.ConvertToInt(key.Replace(sDateOffKeyPrefix, ""));
                        objDetail.DateOff = DateTime.Parse(Request[sDateOffKeyPrefix + index]);
                        objDetail.IsCompanyPay = objPTO_Type.IsCompanyPay && objPTO_Type.PayHour > 0;
                        objDetail.PTO_ID = objUI.ID;
                        objDetail.HourFrom = ConvertUtil.ConvertToInt(Request[sFromHrsInputNamePrefix + index]);
                        objDetail.HourTo = ConvertUtil.ConvertToInt(Request[sToHrsInputNamePrefix + index]);
                        objDetail.TimeOff = CommonFunc.GetWorkingHours(objDetail.HourFrom, objDetail.HourTo);
                        objDetail.DateOffFrom = null;
                        objDetail.DateOffTo = null;
                        objDetail.CreateDate = DateTime.Now;
                        objDetail.CreatedBy = HttpContext.User.Identity.Name;
                        objDetail.UpdateDate = DateTime.Now;
                        objDetail.UpdatedBy = HttpContext.User.Identity.Name;
                        listObjPTO_Detail.Add(objDetail);
                    }
                }
                //PTO is not hour type
                else
                {
                    objPTO_Detail.DateOff = null;
                    objPTO_Detail.TimeOff = null;
                    objPTO_Detail.IsCompanyPay = objPTO_Type.IsCompanyPay && objPTO_Type.PayHour > 0;
                    objPTO_Detail.PTO_ID = objUI.ID;
                    objPTO_Detail.DateOffFrom = DateTime.Parse(collection.Get("txtDateOffFrom"));
                    objPTO_Detail.DateOffTo = DateTime.Parse(collection.Get("txtDateOffTo"));
                    objPTO_Detail.CreateDate = DateTime.Now;
                    objPTO_Detail.CreatedBy = principal.UserData.UserName;
                    objPTO_Detail.UpdateDate = DateTime.Now;
                    objPTO_Detail.UpdatedBy = principal.UserData.UserName;
                }

                result.Data = ptoDao.Insert(objUI, listObjPTO_Detail, objPTO_Detail);
                
            }
            
            return result;
        }
        /// <summary>
        /// GET: Verify PTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.PTO_Admin, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Verify(string id)
        {
            PTO pto = ptoDao.GetPTOById(id);
            if (pto.Status_ID != Constants.PTO_STATUS_VERIFIED && pto.Status_ID != Constants.PTO_STATUS_CONFIRM)
                return Content(string.Format(Constants.DIV_MESSAGE_FORMAT, "msgError", "", Resources.Message.E0007));
            int date_lock_PTO = int.Parse(Constants.DATE_LOCK_PTO);
            DateTime currDate = DateTime.Now;
            if (currDate.Day > date_lock_PTO)
            {
                // make sure the current date is in new month
                // example: current date is 30-August --> new date is 9-Sep
                currDate = currDate.AddDays(10);
            }
            DateTime fromCurrentMonth = new DateTime(currDate.Year, currDate.AddMonths(-1).Month, date_lock_PTO + 1);
            DateTime toCurrentMonth = new DateTime(currDate.Year, currDate.Month, date_lock_PTO);
            string employeeID = pto.Submitter;
            int? currentTimeOff = (int?)ptoDao.GetPTOEmpList(0, 0, fromCurrentMonth, toCurrentMonth, employeeID).Sum(q => q.Hours);
            ViewData[CommonDataKey.PTO_VACATION_BALANCE] = ptoReportDao.GetPTOReportByEmployeeIDAndMonth(employeeID, currDate);
            ViewData[CommonDataKey.PTO_USED_HOURS] = currentTimeOff.HasValue ? currentTimeOff.Value : 0;

            int parentTypeID = ptoTypeDao.GetParentIDByTypeID(pto.PTOType_ID);
            ViewData[CommonDataKey.PTO_TYPE_PARENT_ID] = new SelectList(ptoTypeDao.GetParentTypeList(), "ID", "Name", parentTypeID);
            ViewData[CommonDataKey.PTO_TYPE_LIST] = new SelectList(ptoTypeDao.GetTypeListByParentID(parentTypeID), "ID", "Name", pto.PTOType_ID);
            //List<PTO_Type> lType = ptoTypeDao.GetList().Where(p=>p.ID==pto.PTOType_ID).ToList();
            //ViewData[CommonDataKey.PTO_TYPE_LIST] = new SelectList(lType, "id", "name", pto.PTOType_ID);


            List<PTO_Type> lType = ptoTypeDao.GetList();
            //ViewData[CommonDataKey.PTO_TYPE_PARENT_ID] = new SelectList(ptoTypeDao.GetParentTypeList(), "ID", "Name", string.Empty);
            //List<PTO_Type> list = new List<PTO_Type>();
            //ViewData[CommonDataKey.PTO_TYPE_LIST] = new SelectList(list, "id", "name", string.Empty);
            string[] sArrTypeIDs_isHourType = lType.Where(p => p.IsHourType).
                Select(p => p.ID.ToString()).ToArray();
            ViewData[CommonDataKey.PTO_IDS_IS_HOUR_TYPE] = String.Join(",", sArrTypeIDs_isHourType);


            List<PTO_Detail> listPTO_Detail = ptoDao.GetPTO_DetailByPTO_ID(pto.ID);
            ViewData[CommonDataKey.PTO_DETAILS] = listPTO_Detail;
            ViewData["ptoDateOff"] = string.Join(",", listPTO_Detail.Where(p => p.DateOff.HasValue).Select(p => p.DateOff.HasValue ? p.DateOff.Value.ToString(Constants.DATETIME_FORMAT) : ""));
            ViewData["ptoHourFrom"] = string.Join(",", listPTO_Detail.Where(p => p.DateOff.HasValue).Select(p => p.HourFrom));
            ViewData["ptoHourTo"] = string.Join(",", listPTO_Detail.Where(p => p.DateOff.HasValue).Select(p => p.HourTo));
            ViewData["ptoIsPay"] = string.Join(",", listPTO_Detail.Where(p => p.DateOff.HasValue).Select(p => Convert.ToInt32(p.IsCompanyPay)));
            return View(pto);
        }

        /// <summary>
        /// Delete PTO
        /// </summary>
        /// <param name="id">PTO id</param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.PTO_Admin, Rights = Permissions.Delete)]
        public ActionResult DeletePTO(string id)
        {
            Message msg = null;
            PTO pto = ptoDao.GetPTOByKey(ConvertUtil.ConvertToInt(id));
            if (pto != null)
            {                
                //bool result = ptoDao.DeleteAllDetails(pto.ID);
                //if (result)
                msg = ptoDao.Delete(pto.Key_ID, pto.ID);
            }
            if (msg != null && msg.MsgType != MessageType.Error)
            {
                pto.DeleteFlag = true;
                CommonFunc.SendEmailToEmployee(ptoDao, pto, pto.PTO_Details.ToList(), false);
                CommonFunc.SendEmailToEmployee(ptoDao, pto, pto.PTO_Details.ToList(), true);
                CommonFunc.SendEmailToEmployee(ptoDao, pto, pto.PTO_Details.ToList(), null);
            }
            ShowMessage(msg);            
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        /// <summary>
        /// POST: Verify PTO
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.PTO_Admin, Rights = Permissions.Update, ShowInPopup = true)]
        public JsonResult Verify(FormCollection collection)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                PTO objUI = new PTO();

                string sPTO_ID = collection.Get("hidPTO_ID");
                PTO ptoUI = ptoDao.GetPTOById(sPTO_ID);
                string sUpdateDate = collection.Get("hidUpdateDate");
                int ptoTypeID = ConvertUtil.ConvertToInt(collection.Get(CommonDataKey.PTO_TYPE_LIST));
                int ptoOldType = ptoUI.PTOType_ID;
                if (!sUpdateDate.Equals(ptoUI.UpdateDate.ToString()))
                {
                    result.Data = new Message(MessageConstants.E0025, MessageType.Error, "This PTO");
                    return result;
                }
                string sHR_Comment = collection.Get("HRComment");
                if (bool.Parse(collection.GetValues("ckbVerified")[0]))
                    ptoUI.Status_ID = Constants.PTO_STATUS_VERIFIED;
                else
                    ptoUI.Status_ID = Constants.PTO_STATUS_CONFIRM;
                ptoUI.HRComment = sHR_Comment;
                ptoUI.UpdatedBy = principal.UserData.UserName;
                ptoUI.PTOType_ID = ptoTypeID;
                ptoUI.UpdateDate = DateTime.Now;
                bool isHoursType = ptoTypeDao.GetByID(ptoTypeID).IsHourType;
                List<PTO_Detail> listPTO_Detail = new List<PTO_Detail>();
                //if pto is hour type
                if (isHoursType)
                {
                    string sDateOffKeyPrefix = Constants.PTO_DATE_OFF_KEY_PREFIX;
                    string sFromHrsInputNamePrefix = Constants.PTO_FROM_HRS_INPUT_NAME_PREFIX;
                    string sToHrsInputNamePrefix = Constants.PTO_TO_HRS_INPUT_NAME_PREFIX;
                    string sIsCompanypay = Constants.PTO_IS_COMPANY_PAY_CHECKBOX;
                    var dateOffList = Request.Form.AllKeys.Where(p => p.Contains(sDateOffKeyPrefix)).ToList();
                    foreach (var key in dateOffList)
                    {
                        PTO_Detail ptoDetail = new PTO_Detail();
                        int index = ConvertUtil.ConvertToInt(key.Replace(sDateOffKeyPrefix, ""));
                        
                        ptoDetail.CreateDate = DateTime.Now;
                        ptoDetail.CreatedBy = principal.UserData.UserName;
                        ptoDetail.UpdateDate = DateTime.Now;
                        ptoDetail.UpdatedBy = principal.UserData.UserName;
                        ptoDetail.DateOff = DateTime.Parse(Request[sDateOffKeyPrefix + index]);
                        ptoDetail.HourFrom = ConvertUtil.ConvertToInt(Request[sFromHrsInputNamePrefix + index]);
                        ptoDetail.HourTo = ConvertUtil.ConvertToInt(Request[sToHrsInputNamePrefix + index]);
                        ptoDetail.TimeOff = CommonFunc.GetWorkingHours(ptoDetail.HourFrom, ptoDetail.HourTo);
                        ptoDetail.IsCompanyPay = collection[sIsCompanypay + index] != null;
                        //Get the right value of checkbox: checked(true,false), uncheck(false)
                        ptoDetail.PTO_ID = sPTO_ID;
                        listPTO_Detail.Add(ptoDetail);
                    }
                }
                //pto is not hour type
                else
                {
                    DateTime tFrom = DateTime.Parse(collection.Get("txtDateOffFrom"));
                    DateTime tTo = DateTime.Parse(collection.Get("txtDateOffTo"));
                    PTO_Detail ptoDetail = new PTO_Detail();
                    //int index = ConvertUtil.ConvertToInt(key.Replace(sDateOffKeyPrefix, ""));
                    ptoDetail.CreateDate = DateTime.Now;
                    ptoDetail.CreatedBy = principal.UserData.UserName;
                    ptoDetail.UpdateDate = DateTime.Now;
                    ptoDetail.UpdatedBy = principal.UserData.UserName;
                    ptoDetail.IsCompanyPay = bool.Parse(collection.GetValues("ckbIsCompanyPay_Date")[0]);
                    ptoDetail.DateOffFrom = tFrom;
                    ptoDetail.DateOffTo = tTo;
                    //Get the right value of checkbox: checked(true,false), uncheck(false)
                    ptoDetail.PTO_ID = sPTO_ID;
                    listPTO_Detail.Add(ptoDetail);
                }
                bool isPTOChanged = IsPTOChanged(ptoUI, ptoOldType, listPTO_Detail);
                bool sendMail = ptoUI.Status_ID == Constants.PTO_STATUS_VERIFIED && isPTOChanged;
                if (!ptoUI.IsChanged && isPTOChanged)
                    ptoUI.IsChanged = true;

                result.Data = ptoDao.Verify(ptoUI,   listPTO_Detail);

                if (DateTime.Now.Day > int.Parse(Constants.DATE_LOCK_PTO) &&
                    DateTime.Now.Day < (int.Parse(Constants.DATE_LOCK_PTO_HR_VERIFIED) + 1))
                {
                    PTOReportDao reportDao = new PTOReportDao();
                    reportDao.UpdateBalanceForNextMonth(ptoUI.Submitter, DateTime.Now);
                }

                //Send email to Employee if HR verified and change PTO (type/dateoff/hour...)
                if (sendMail)
                    CommonFunc.SendEmailToEmployee(ptoDao, ptoUI, listPTO_Detail, false);
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }

        [NonAction]
        private bool IsPTOChanged(PTO pto, int ptoOldType, List<PTO_Detail> details)
        {
            //var ptoDB = ptoDao.GetPTOByKey(pto.Key_ID);
            if (pto.PTOType_ID != ptoOldType || pto.PTO_Details.Count != details.Count)
                return true;
            if (pto.PTO_Type.IsHourType)
            {
                List<PTO_Detail> detailsDB = pto.PTO_Details.OrderBy(p => p.DateOff).ToList();
                details = details.OrderBy(p => p.DateOff).ToList();
                for (int i = 0; i < details.Count; i++)
                    if (details[i].DateOff.Value.Date != detailsDB[i].DateOff.Value.Date ||
                        details[i].HourFrom != detailsDB[i].HourFrom || details[i].HourTo != detailsDB[i].HourTo)
                        return true;
            }
            else
            { 
                PTO_Detail detailsDB = pto.PTO_Details.FirstOrDefault();
                PTO_Detail detailsUI = details.FirstOrDefault();
                if (detailsUI.DateOffFrom.Value.Date != detailsDB.DateOffFrom.Value.Date ||
                    detailsUI.DateOffTo.Value.Date != detailsDB.DateOffTo.Value.Date)
                    return true;
            }
            return false;
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
        private void SetSessionFilter(string filterText, string status, string type, string month,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.PTO_ADMIN_TEXT, filterText);
            hashData.Add(Constants.PTO_ADMIN_STATUS, status);
            hashData.Add(Constants.PTO_ADMIN_TYPE, type);
            hashData.Add(Constants.PTO_ADMIN_MONTH, month);

            hashData.Add(Constants.PTO_ADMIN_COLUMN, column);
            hashData.Add(Constants.PTO_ADMIN_ORDER, order);
            hashData.Add(Constants.PTO_ADMIN_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.PTO_ADMIN_ROW_COUNT, rowCount);

            Session[SessionKey.PTO_ADMIN_DEFAULT_VALUE] = hashData;
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
        private void SetSessionFilterPTOConfirm(string filterText, string manager, string type,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.PTO_CONFIRM_TEXT, filterText);
            hashData.Add(Constants.PTO_CONFIRM_MANAGER, manager);
            hashData.Add(Constants.PTO_CONFIRM_TYPE, type);            

            hashData.Add(Constants.PTO_CONFIRM_COLUMN, column);
            hashData.Add(Constants.PTO_CONFIRM_ORDER, order);
            hashData.Add(Constants.PTO_CONFIRM_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.PTO_CONFIRM_ROW_COUNT, rowCount);

            Session[SessionKey.PTO_CONFIRM_DEFAULT_VALUE] = hashData;
        }
        public ActionResult GetEmployeeInfo(string empId)
        {
            PTOReportDao ptoReportDao = new PTOReportDao();
            int date_lock_PTO = int.Parse(Constants.DATE_LOCK_PTO);
            DateTime currDate = DateTime.Now;
            if (currDate.Day > date_lock_PTO)
            {
                // make sure the current date is in new month
                // example: current date is 30-August --> new date is 9-Sep
                currDate = currDate.AddDays(10);
            }
            DateTime fromCurrentMonth = new DateTime(currDate.Year, currDate.AddMonths(-1).Month, date_lock_PTO + 1);
            DateTime toCurrentMonth = new DateTime(currDate.Year, currDate.Month, date_lock_PTO);

            int? currentTimeOff = (int?)ptoDao.GetPTOEmpList(0, 0, fromCurrentMonth, toCurrentMonth, empId).Sum(q => q.Hours);

            var userName = CommonFunc.GetUserNameLoginByEmpID(empId);
            return Json(new
            {
                UserName = userName,
                BalanceYTD = ptoReportDao.GetPTOReportByEmployeeIDAndMonth(empId, currDate),
                UsedYTD = currentTimeOff.HasValue ? currentTimeOff.Value : 0
            }, JsonRequestBehavior.AllowGet);
        }
        public string GetEmployeeLoginName(string empId)
        {
            return CommonFunc.GetUserNameLoginByEmpID(empId);
        }

        public ActionResult RefreshLog()
        {
            string view = string.Empty;
                
            Session.Remove(SessionKey.PTO_USER_LOG_DEFAULT_VALUE);
            view = "UserLog";
                
            return RedirectToAction(view);
        }

        [CrmAuthorizeAttribute(Module = Modules.PTO_Admin, Rights = Permissions.Read)]
        public ActionResult UserLog()
        {
            Hashtable hashData = Session[SessionKey.PTO_USER_LOG_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.PTO_USER_LOG_DEFAULT_VALUE];

            ViewData[Constants.PTO_USER_LOG_NAME] = hashData[Constants.PTO_USER_LOG_NAME] == null ? Constants.USERNAME : !string.IsNullOrEmpty((string)hashData[Constants.PTO_USER_LOG_NAME]) ? hashData[Constants.PTO_USER_LOG_NAME] : Constants.USERNAME;
            ViewData[Constants.PTO_USER_LOG_DATE] = hashData[Constants.PTO_USER_LOG_DATE] == null ? string.Empty : hashData[Constants.PTO_USER_LOG_DATE];

            ViewData[Constants.PTO_USER_LOG_COLUMN] = hashData[Constants.PTO_USER_LOG_COLUMN] == null ? "Date" : hashData[Constants.PTO_USER_LOG_COLUMN];
            ViewData[Constants.PTO_USER_LOG_ORDER] = hashData[Constants.PTO_USER_LOG_ORDER] == null ? "desc" : hashData[Constants.PTO_USER_LOG_ORDER];
            ViewData[Constants.PTO_USER_LOG_PAGE_INDEX] = hashData[Constants.PTO_USER_LOG_PAGE_INDEX] == null ? "1" : hashData[Constants.PTO_USER_LOG_PAGE_INDEX].ToString();
            ViewData[Constants.PTO_USER_LOG_ROW_COUNT] = hashData[Constants.PTO_USER_LOG_ROW_COUNT] == null ? "20" : hashData[Constants.PTO_USER_LOG_ROW_COUNT].ToString();

            return View();
        }
        

        public ActionResult GetListJQGrid4Log(string name, string date)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter4Log(name, date, sortColumn, sortOrder, pageIndex, rowCount);

            #region search
            string userName = string.Empty;
            string dateSort = string.Empty;
            DateTime dt = new DateTime();

            if (name.Trim().ToLower().Equals(Constants.USERNAME.ToLower()))
            {
                name = string.Empty;
            }

            if (!string.IsNullOrEmpty(name))
            {
                userName = name;
            }

            if (!string.IsNullOrEmpty(date))
            {
                bool isValid = DateTime.TryParse(date, out dt);
                if (isValid)
                {
                    dateSort = dt.ToString(Constants.DATETIME_FORMAT_DB);
                }
            }

            #endregion

            List<sp_LogMasterResult> logList = logDao.GetList(userName, dateSort, (int) LogType.Pto);

            int totalRecords = logList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = logDao.Sort(logList, sortColumn, sortOrder)
                                  .Skip((currentPage - 1) * rowCount)
                                   .Take(rowCount);

            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        cell = new string[] {
                            m.UserName,
                            m.LogDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW),
                            GetAction(m.UserName,m.LogDate.Value)
                            
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string GetAction(string userName, DateTime date)
        {
            string stAction = string.Empty;
            List<sp_LogMasterGroupResult> listdetail = logDao.GetDetailOnList(userName, date, (int) LogType.Pto);
            for (int j = 0; j < listdetail.Count; j++)
            {
                sp_LogMasterGroupResult sub = (sp_LogMasterGroupResult)listdetail[j];
                stAction += " - <a style=\"padding-top:3px;padding-bottom:2px\" onclick=\"CRM.popup('/UserLog/Detail/?UserName=" + userName + 
                    "&Date=" + date.ToString(Constants.DATETIME_FORMAT) + "&TableName=" + sub.TableName + "&ActionName=" + sub.ActionName +
                    "&Count=" + sub.Count + "&Type=" + (int) LogType.Pto + "', 'User Log Details', 800)\"'>" + sub.ActionName + " " + HttpUtility.HtmlEncode(sub.TableName) + 
                    "</a> (" + sub.Count + ")";
                if (j < listdetail.Count - 1)
                {
                    stAction += "<br />";
                }

            }
            return stAction;
        }
        /// <summary>
        /// Set Session to Filter
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="groupId"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilter4Log(string accountName, string date,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.PTO_USER_LOG_NAME, accountName);
            hashData.Add(Constants.PTO_USER_LOG_DATE, date);
            hashData.Add(Constants.PTO_USER_LOG_COLUMN, column);
            hashData.Add(Constants.PTO_USER_LOG_ORDER, order);
            hashData.Add(Constants.PTO_USER_LOG_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.PTO_USER_LOG_ROW_COUNT, rowCount);

            Session[SessionKey.PTO_USER_LOG_DEFAULT_VALUE] = hashData;
        }
    }
}
