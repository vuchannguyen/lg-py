using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CRM.Library.Attributes;
using CRM.Library.Common;
using CRM.Models;

namespace CRM.Controllers
{
    public class ServiceRequestAdminController : BaseController
    {
        #region Variables
        SRStatusDao statusDao = new SRStatusDao();
        SRCategoryDao catDao = new SRCategoryDao();
        UserAdminDao userDao = new UserAdminDao();
        ServiceRequestDao srDao = new ServiceRequestDao();
        EmployeeDao empDao = new EmployeeDao();
        SRCommentDao commentDao = new SRCommentDao();
        SRActivityDao activityDao = new SRActivityDao();
        GroupDao groupDao = new GroupDao();
        #endregion

        #region Methods

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            
            Hashtable hashData = Session[SessionKey.SR_LIST_ADMIN_SEARCH_VALUES] == null ? new Hashtable() : (Hashtable)Session[SessionKey.SR_LIST_ADMIN_SEARCH_VALUES];

            ViewData[Constants.SR_LIST_ADMIN_TITLE] = hashData[Constants.SR_LIST_ADMIN_TITLE] == null ? Constants.SR_FIRST_KEY_WORD : hashData[Constants.SR_LIST_ADMIN_TITLE];
            ViewData[Constants.SR_ADMIN_STATUS_LIST] = new SelectList(statusDao.GetAll(), "ID", "Name", hashData[Constants.SR_ADMIN_STATUS_LIST] == null ? 
                Constants.SR_FIRST_STATUS : hashData[Constants.SR_ADMIN_STATUS_LIST]);
            ViewData[Constants.SR_ADMIN_CATEGORY_LIST] = new SelectList(catDao.GetList(), "ID", "Name", hashData[Constants.SR_ADMIN_CATEGORY_LIST] == null ?
                Constants.SR_FIRST_CATEGORY : hashData[Constants.SR_ADMIN_CATEGORY_LIST]);
            ViewData[Constants.SR_ADMIN_SUBCATEGORY_LIST] = new SelectList(catDao.GetSubList(), "ID", "Name", hashData[Constants.SR_ADMIN_SUBCATEGORY_LIST] == null ?
                Constants.SR_FIRST_SUBCATEGORY : hashData[Constants.SR_ADMIN_SUBCATEGORY_LIST]);
            ViewData[Constants.SR_ADMIN_ASSIGNTO_LIST] = new SelectList(srDao.GetListAssign(), "Text", "Value", 
                hashData[Constants.SR_ADMIN_ASSIGNTO_LIST] == null ? principal.UserData.UserName : hashData[Constants.SR_ADMIN_ASSIGNTO_LIST]);
            ViewData[Constants.SR_ADMIN_REQUESTOR_LIST] = new SelectList(srDao.GetListRequestor(), "Text", "Value",
                hashData[Constants.SR_ADMIN_REQUESTOR_LIST] == null ? "" : hashData[Constants.SR_ADMIN_REQUESTOR_LIST]);

            ViewData[Constants.SR_ADMIN_START_DATE] = hashData[Constants.SR_ADMIN_START_DATE] == null ? "" : hashData[Constants.SR_ADMIN_START_DATE] ;
            ViewData[Constants.SR_ADMIN_END_DATE] = hashData[Constants.SR_ADMIN_END_DATE] == null ? "" : hashData[Constants.SR_ADMIN_END_DATE];
            
            ViewData[Constants.SR_LIST_ADMIN_COLUMN] = hashData[Constants.SR_LIST_ADMIN_COLUMN] == null ? "ID" : hashData[Constants.SR_LIST_ADMIN_COLUMN];
            ViewData[Constants.SR_LIST_ADMIN_ORDER] = hashData[Constants.SR_LIST_ADMIN_ORDER] == null ? "asc" : hashData[Constants.SR_LIST_ADMIN_ORDER];
            ViewData[Constants.SR_LIST_ADMIN_PAGE_INDEX] = hashData[Constants.SR_LIST_ADMIN_PAGE_INDEX] == null ? "1" : hashData[Constants.SR_LIST_ADMIN_PAGE_INDEX].ToString();
            ViewData[Constants.SR_LIST_ADMIN_ROW_COUNT] = hashData[Constants.SR_LIST_ADMIN_ROW_COUNT] == null ? "20" : hashData[Constants.SR_LIST_ADMIN_ROW_COUNT].ToString();
            return View();
        }

        /// <summary>
        /// Get list JQGrid
        /// </summary>
        /// <param name="name">title or description</param>
        /// <param name="status">status id</param>
        /// <param name="category">category id</param>
        /// <param name="subcate">sub category id</param>
        /// <param name="assignto">assign to id</param>
        /// <param name="role">login role</param>
        /// <returns>ActionResult</returns>
        public ActionResult GetListJQGrid(string name, string status, string category, string subcate, string assignto, 
            string startdate, string enddate, string requestor)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            
            SetSessionFilterSRList(name, status, category, subcate, assignto, sortColumn, sortOrder, pageIndex, rowCount, startdate, enddate, requestor);

            #region search
            string title = string.Empty;
            int subcat = 0;
            int categoryId = 0;
            int statusId = 0;
            string assignName = null;
            string requestUser = null;
            DateTime? fromDate = null;
            DateTime? toDate = null;
            if (name != Constants.SR_FIRST_KEY_WORD)
            {
                title = name;
            }
            if (!string.IsNullOrEmpty(subcate))
            {
                subcat = int.Parse(subcate);
            }
            if (!string.IsNullOrEmpty(status))
            {
                statusId = int.Parse(status);
            }
            if (!string.IsNullOrEmpty(category))
            {
                categoryId = int.Parse(category);
            }
            if (!string.IsNullOrEmpty(assignto))
            {
                assignName = assignto;
            }
            if (!string.IsNullOrEmpty(requestor))
            {
                requestUser = requestor;
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                fromDate = DateTime.Parse(startdate);
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                toDate = DateTime.Parse(enddate);
            }
            #endregion

            List<sp_SR_GetServiceRequest4AdminResult> empList = srDao.GetList4Admin(title, subcat, categoryId, statusId, assignName, fromDate, toDate, requestUser);
            
            int totalRecords = empList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            List<sp_SR_GetServiceRequest4AdminResult> finalList = srDao.SortAdmin(empList, sortColumn, sortOrder)
                                 .Skip((currentPage - 1) * rowCount)
                                  .Take(rowCount).ToList<sp_SR_GetServiceRequest4AdminResult>();

            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        i = m.ID,
                        cell = new string[] {                   
                            m.ID.ToString(),         
                            "<span class='"+ m.Icon + "' title='" + m.Urgency + "'></span>" ,
                            Constants.SR_SERVICE_REQUEST_PREFIX + m.ID.ToString(),
                            CommonFunc.Link(m.ID.ToString(), "/ServiceRequestAdmin/Detail/"  + m.ID+ "", (m.Title.Length > Constants.SR_MAX_LENGTH_TITLE?
                                Server.HtmlEncode(m.Title.Substring(0, Constants.SR_MAX_LENGTH_TITLE))+ "..." : Server.HtmlEncode(m.Title)), true),
                            m.Category,
                            m.SubCategory,                            
                            m.Status,                            
                            m.Requestor,
                            m.AssginName,    
                            m.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            SetAction(m.ID.ToString(), m.StatusID, false, m.AssginName)
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Set action
        /// </summary>
        /// <param name="ID">service request id</param>
        /// <param name="statusID">status id</param>
        /// <param name="role">role</param>
        /// <returns>string</returns>
        private string SetAction(string ID, int statusID, bool isDetailPage, string assignName)
        {
            string action = string.Empty;
            string sDisplayText = "";
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            
            bool plus1 = false, plus2 = false;
            if (statusID != Constants.SR_STATUS_CLOSED)
            {
                bool hasEdit = false;
                if (groupDao.HasPermisionOnModule(principal.UserData.UserID, (int)Permissions.ForceEdit, (int)Modules.ServiceRequestAdmin))
                {
                    sDisplayText = isDetailPage ? "Edit" : string.Empty;
                    action += CommonFunc.ButtonWithParams("edit", "Edit", "navigateWithReferrer('/ServiceRequestAdmin/ForceEdit/" + ID + "')", sDisplayText);
                    hasEdit = true;
                }
                if (statusID != Constants.SR_STATUS_TO_BE_APPROVED && principal.UserData.UserName == assignName)
                {
                    if (!hasEdit)
                    {
                        sDisplayText = isDetailPage ? "Edit" : string.Empty;
                        action += CommonFunc.ButtonWithParams("edit", "Edit", "navigateWithReferrer('/ServiceRequestAdmin/Edit/" + ID + "')", sDisplayText);
                    }
                    if (statusID == Constants.SR_STATUS_NEW)
                    {
                        sDisplayText = isDetailPage ? "Get Approval" : string.Empty;
                        action += CommonFunc.ButtonWithParams("approve", "Get the Approval from Manager for this Request", "CRM.popup('/ServiceRequestAdmin/GetApproval/" + ID + "', 'Get approval for service request " +
                            Constants.SR_SERVICE_REQUEST_PREFIX + ID + "', 400)", sDisplayText);
                    }
                    else
                    {
                        plus1 = true;
                    }
                }
            }
            else
            {
                plus2 = true;
            }
            if (statusID != Constants.SR_STATUS_CLOSED && principal.UserData.UserName == assignName)
            {

                sDisplayText = isDetailPage ? "Close" : string.Empty;
                action += CommonFunc.ButtonWithParams("closesr", "Close this Request", "CRM.popup('/ServiceRequestAdmin/Close/" + ID + "', 'Close service request " + 
                    Constants.SR_SERVICE_REQUEST_PREFIX + ID + "', 400)", sDisplayText);
            }
            else
            {
                action += CommonFunc.Button("clpointer", "", "");
            }
            if (plus2)
            {
                action += CommonFunc.Button("clpointer", "", "");
                action += CommonFunc.Button("clpointer", "", "");
            } else if (plus1)
            {
                action += CommonFunc.Button("clpointer", "", "");
            }
            return action;
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.SR_LIST_ADMIN_SEARCH_VALUES);
            return RedirectToAction("Index");
        }

        private Message CheckFileUpload()
        {
            Message msg = null;
            bool invalidExtension = false;
            bool invalidSize = false;
            bool invalidName = false;
            string errorExtension = string.Empty;
            string errorFileName = string.Empty;
            string duplicateName = string.Empty;
            int i = 0;
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[i] as HttpPostedFileBase;
                if (hpf.ContentLength != 0)
                {
                    int maxSize = 1024 * 1024 * Constants.CV_MAX_SIZE;
                    float sizeFile = float.Parse(hpf.ContentLength.ToString("N"));
                    string extension = Path.GetExtension(hpf.FileName);
                    string[] extNotAllowList = Constants.CONTRACT_EXT_NOT_ALLOW.Split(',');
                    if (extNotAllowList.Contains(extension.ToLower())) //check extension file is valid
                    {
                        invalidExtension = true;
                        errorExtension += extension + ",";
                        break;
                    }
                    else if (sizeFile > maxSize) //check maxlength of uploaded file
                    {
                        invalidSize = true;
                        break;
                    }
                    else if (duplicateName.Contains(Path.GetFileName(hpf.FileName)))
                    {
                        errorFileName = Path.GetFileName(hpf.FileName);
                        invalidName = true;
                        break;
                    }
                }
                i++;
                duplicateName += Path.GetFileName(hpf.FileName) + ",";
            }
            if (invalidExtension == true)
            {
                msg = new Message(MessageConstants.E0043, MessageType.Error, Constants.CONTRACT_EXT_NOT_ALLOW, Constants.CV_MAX_SIZE.ToString());
            }
            else if (invalidSize == true)
            {
                msg = new Message(MessageConstants.E0012, MessageType.Error, Constants.CV_MAX_SIZE.ToString());
            }
            else if (invalidName == true)
            {
                msg = new Message(MessageConstants.E0017, MessageType.Error, errorFileName);
            }
            return msg;
        }

        /// <summary>
        /// Add Service Request Comment
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddComment(SR_Comment obj)
        {
            Message msg = null;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            obj.PostTime = DateTime.Now;
            obj.Poster = principal.UserData.UserName;
            string serverPath = Server.MapPath(Constants.SR_UPLOAD_PATH);
            //Check File Valid
            msg = CheckFileUpload();
            if (msg == null) //case sussessfully
            {
                int y = 0;
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[y] as HttpPostedFileBase;
                    if (!string.IsNullOrEmpty(Path.GetExtension(hpf.FileName)))
                    {
                        string strReplaceUserName = principal.UserData.UserName.Replace(".", "_");
                        string extension = Path.GetExtension(hpf.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(hpf.FileName);
                        string contractName = principal.UserData.UserID + "_" + strReplaceUserName + "_" + fileName +
                             "." + DateTime.Now.ToString(Constants.UNIQUE_TIME) + extension;
                        contractName = ConvertUtil.FormatFileName(contractName);
                        string strPath = serverPath + "\\" + contractName;
                        hpf.SaveAs(strPath);
                        obj.Files += contractName + Constants.FILE_STRING_PREFIX;
                    }
                    y++;
                }
                msg = commentDao.Insert(obj);
            }
            SendSRMail(obj.ServiceRequestID, Constants.SR_SEND_MAIL_COMMENT);
            ShowMessage(msg);

            return RedirectToAction("Detail/" + int.Parse(obj.ServiceRequestID.ToString()));
        }

        /// <summary>
        /// Refresh weekly report
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult RefreshWeeklyReport()
        {
            Session.Remove(SessionKey.SR_REPORT_WEEKLY_LIST_ADMIN_SEARCH_VALUES);

            return RedirectToAction("WeeklyReport");
        }

        /// <summary>
        /// Set session filter service request list
        /// </summary>
        /// <param name="text">title or description</param>
        /// <param name="statusId">status id</param>
        /// <param name="categoryId">category id</param>
        /// <param name="subCategoryId">sub category id</param>
        /// <param name="assignId">assign id</param>
        /// <param name="column">column</param>
        /// <param name="order">order</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="rowCount">row count</param>
        private void SetSessionFilterSRList(string text, string statusId, string categoryId, string subCategoryId, string assignId,
            string column, string order, int pageIndex, int rowCount, string startDate, string endDate, string requestor)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.SR_LIST_ADMIN_TITLE, text);
            hashData.Add(Constants.SR_ADMIN_STATUS_LIST, statusId);
            hashData.Add(Constants.SR_ADMIN_CATEGORY_LIST, categoryId);
            hashData.Add(Constants.SR_ADMIN_SUBCATEGORY_LIST, subCategoryId);
            hashData.Add(Constants.SR_ADMIN_ASSIGNTO_LIST, assignId);
            hashData.Add(Constants.SR_ADMIN_REQUESTOR_LIST, requestor);
            hashData.Add(Constants.SR_ADMIN_START_DATE, startDate);
            hashData.Add(Constants.SR_ADMIN_END_DATE, endDate);
            hashData.Add(Constants.SR_LIST_ADMIN_COLUMN, column);
            hashData.Add(Constants.SR_LIST_ADMIN_ORDER, order);
            hashData.Add(Constants.SR_LIST_ADMIN_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.SR_LIST_ADMIN_ROW_COUNT, rowCount);

            Session[SessionKey.SR_LIST_ADMIN_SEARCH_VALUES] = hashData;
        }

        /// <summary>
        /// Refresh open closed report
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult RefreshOCReport()
        {
            Session.Remove(SessionKey.SR_REPORT_OC_LIST_ADMIN_SEARCH_VALUES);

            return RedirectToAction("ReportOpenCloseRequests");
        }

        /// <summary>
        /// Set session filter for sr report open closed status
        /// </summary>
        /// <param name="column">string</param>
        /// <param name="order">string</param>
        /// <param name="pageIndex">int</param>
        /// <param name="rowCount">int</param>
        /// <param name="startDate">string</param>
        /// <param name="endDate">string</param>
        private void SetSessionFilterSRReportOCList(string column, string order, int pageIndex, int rowCount, string startDate, string endDate)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.SR_REPORT_OC_ADMIN_START_DATE, startDate);
            hashData.Add(Constants.SR_REPORT_OC_ADMIN_END_DATE, endDate);
            hashData.Add(Constants.SR_REPORT_OC_LIST_ADMIN_COLUMN, column);
            hashData.Add(Constants.SR_REPORT_OC_LIST_ADMIN_ORDER, order);
            hashData.Add(Constants.SR_REPORT_OC_LIST_ADMIN_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.SR_REPORT_OC_LIST_ADMIN_ROW_COUNT, rowCount);

            Session[SessionKey.SR_REPORT_OC_LIST_ADMIN_SEARCH_VALUES] = hashData;
        }

        /// <summary>
        /// Refresh activiry report
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult RefreshActivityReport()
        {
            Session.Remove(SessionKey.SR_REPORT_ACTIVITY_LIST_ADMIN_SEARCH_VALUES);

            return RedirectToAction("ReportActivity");
        }

        /// <summary>
        /// Set session filter for activity report
        /// </summary>
        /// <param name="column">string</param>
        /// <param name="order">string</param>
        /// <param name="pageIndex">int</param>
        /// <param name="rowCount">int</param>
        /// <param name="startDate">string</param>
        /// <param name="endDate">string</param>
        private void SetSessionFilterSRReportActivityList(string column, string order, int pageIndex, int rowCount, string startDate, string endDate)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.SR_REPORT_ACTIVITY_ADMIN_START_DATE, startDate);
            hashData.Add(Constants.SR_REPORT_ACTIVITY_ADMIN_END_DATE, endDate);
            hashData.Add(Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_COLUMN, column);
            hashData.Add(Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_ORDER, order);
            hashData.Add(Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_ROW_COUNT, rowCount);

            Session[SessionKey.SR_REPORT_ACTIVITY_LIST_ADMIN_SEARCH_VALUES] = hashData;
        }       
        
        /// <summary>
        /// Set session filter for weekly report
        /// </summary>
        /// <param name="column">string</param>
        /// <param name="order">string</param>
        /// <param name="pageIndex">int</param>
        /// <param name="rowCount">int</param>
        /// <param name="startDate">string</param>
        /// <param name="endDate">string</param>
        /// <param name="type">string</param>
        private void SetSessionFilterWeeklyReportList(string column, string order, int pageIndex, int rowCount, string startDate, string endDate, string type)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.SR_REPORT_WEEKLY_ADMIN_START_DATE, startDate);
            hashData.Add(Constants.SR_REPORT_WEEKLY_ADMIN_END_DATE, endDate);
            if (type == "it")
            {
                hashData.Add(Constants.SR_REPORT_ITEAM_LIST_ADMIN_COLUMN, column);
                hashData.Add(Constants.SR_REPORT_ITEAM_LIST_ADMIN_ORDER, order);
            }
            else if(type == "active")
            {
                hashData.Add(Constants.SR_REPORT_ACTIVE_LIST_ADMIN_COLUMN, column);
                hashData.Add(Constants.SR_REPORT_ACTIVE_LIST_ADMIN_ORDER, order);
            }
            Session[SessionKey.SR_REPORT_WEEKLY_LIST_ADMIN_SEARCH_VALUES] = hashData;
        }

        /// <summary>
        /// Detail
        /// </summary>
        /// <param name="id">service request id</param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.Read)]
        public ActionResult Detail(string id)
        {
            try
            {
                SR_ServiceRequest obj = srDao.GetById(ConvertUtil.ConvertToInt(id));
                if (obj != null)
                {
                    List<sp_SR_GetServiceRequest4AdminResult> list = GetListSRForNavigation();
                    ViewData["listSR"] = list;
                    string flow = string.Empty;
                    string[] arrIds = obj.InvolveUser.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string[] arrStatus = obj.InvolveStatus.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string[] arrDate = obj.InvolveDate.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    for (int i = 0; i < arrIds.Length - 1; i++)
                    {
                        //check duplicate person on user name and role.
                        SR_Status objStatus = statusDao.GetByID(arrStatus[i] != null ? int.Parse(arrStatus[i]) : 0);
                        if (i == arrIds.Length - 1)  // Last Role in WorkFlow
                        {
                            flow += arrIds[i];
                        }
                        else
                        {
                            flow += arrIds[i] + ";" + (objStatus != null ? objStatus.Name : string.Empty) + ";" + arrDate[i] + ",";
                        }

                    }
                    ViewData["WorkFlow"] = flow;
                    List<SR_Comment> listComment = commentDao.GetList(obj.ID);
                    if (listComment.Count > 0)
                    {
                        ViewData["Comment"] = listComment;
                    }
                    var principal = HttpContext.User as AuthenticationProjectPrincipal;
                    ViewData[Constants.SR_ACTION] = SetAction(obj.ID.ToString(), obj.StatusID, true , obj.AssignUser);
                    return View(obj);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        
        /// <summary>
        /// Show tooltip for title
        /// </summary>
        /// <param name="id">service request id</param>
        /// <returns>ActionResult</returns>
        public ActionResult ShowTitleTooltip(string id)
        {
            SR_ServiceRequest emp = srDao.GetById(ConvertUtil.ConvertToInt(id));
            return View(emp);
        }

        /// <summary>
        /// Delete list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>  
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.Delete)]
        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = srDao.DeleteList(id, principal.UserData.UserName);
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        /// <summary>
        /// Open close request report action
        /// </summary>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestReport, Rights = Permissions.OpenedClosedReport)]
        public ActionResult ReportOpenCloseRequests()
        {
            Hashtable hashData = Session[SessionKey.SR_REPORT_OC_LIST_ADMIN_SEARCH_VALUES] == null ? new Hashtable() :
                (Hashtable)Session[SessionKey.SR_REPORT_OC_LIST_ADMIN_SEARCH_VALUES];
            // Get date now
            DateTime date1 = DateTime.Now;
            DateTime date2 = date1.AddDays(-7);

            ViewData[Constants.SR_REPORT_OC_ADMIN_START_DATE] = hashData[Constants.SR_REPORT_OC_ADMIN_START_DATE] == null ? date2.ToString(Constants.DATETIME_FORMAT) :
                hashData[Constants.SR_REPORT_OC_ADMIN_START_DATE];
            ViewData[Constants.SR_REPORT_OC_ADMIN_END_DATE] = hashData[Constants.SR_REPORT_OC_ADMIN_END_DATE] == null ? date1.ToString(Constants.DATETIME_FORMAT) :
                hashData[Constants.SR_REPORT_OC_ADMIN_END_DATE];
            ViewData[Constants.SR_REPORT_OC_LIST_ADMIN_COLUMN] = hashData[Constants.SR_REPORT_OC_LIST_ADMIN_COLUMN] == null ? "ID" :
                hashData[Constants.SR_REPORT_OC_LIST_ADMIN_COLUMN];
            ViewData[Constants.SR_REPORT_OC_LIST_ADMIN_ORDER] = hashData[Constants.SR_REPORT_OC_LIST_ADMIN_ORDER] == null ? "asc" :
                hashData[Constants.SR_REPORT_OC_LIST_ADMIN_ORDER];
            ViewData[Constants.SR_REPORT_OC_LIST_ADMIN_PAGE_INDEX] = hashData[Constants.SR_REPORT_OC_LIST_ADMIN_PAGE_INDEX] == null ? "1" :
                hashData[Constants.SR_REPORT_OC_LIST_ADMIN_PAGE_INDEX].ToString();
            ViewData[Constants.SR_REPORT_OC_LIST_ADMIN_ROW_COUNT] = hashData[Constants.SR_REPORT_OC_LIST_ADMIN_ROW_COUNT] == null ? "20" :
                hashData[Constants.SR_REPORT_OC_LIST_ADMIN_ROW_COUNT].ToString();

            return View();
        }

        /// <summary>
        /// Activity report
        /// </summary>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestReport, Rights = Permissions.RequestsActivityReport)]
        public ActionResult ReportActivity()
        {
            Hashtable hashData = Session[SessionKey.SR_REPORT_ACTIVITY_LIST_ADMIN_SEARCH_VALUES] == null ? new Hashtable() : 
                (Hashtable)Session[SessionKey.SR_REPORT_ACTIVITY_LIST_ADMIN_SEARCH_VALUES];

            // Get date now
            DateTime date1 = DateTime.Now;
            DateTime date2 = date1.AddDays(-7);

            ViewData[Constants.SR_REPORT_ACTIVITY_ADMIN_START_DATE] = hashData[Constants.SR_REPORT_ACTIVITY_ADMIN_START_DATE] == null ? date2.ToString(Constants.DATETIME_FORMAT) :
                hashData[Constants.SR_REPORT_ACTIVITY_ADMIN_START_DATE];
            ViewData[Constants.SR_REPORT_ACTIVITY_ADMIN_END_DATE] = hashData[Constants.SR_REPORT_ACTIVITY_ADMIN_END_DATE] == null ? date1.ToString(Constants.DATETIME_FORMAT) :
                hashData[Constants.SR_REPORT_ACTIVITY_ADMIN_END_DATE];

            ViewData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_COLUMN] = hashData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_COLUMN] == null ? "ID" :
                hashData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_COLUMN];
            ViewData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_ORDER] = hashData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_ORDER] == null ? "asc" :
                hashData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_ORDER];
            ViewData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_PAGE_INDEX] = hashData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_PAGE_INDEX] == null ? "1" :
                hashData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_PAGE_INDEX].ToString();
            ViewData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_ROW_COUNT] = hashData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_ROW_COUNT] == null ? "20" :
                hashData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_ROW_COUNT].ToString();

            return View();
        }

        /// <summary>
        /// Get list for report open close service request
        /// </summary>
        /// <param name="startdate">request date from </param>
        /// <param name="enddate">request date to</param>
        /// <returns>ActionResult</returns>
        public ActionResult GetListJQGridReportOCSR(string startdate, string enddate)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilterSRReportOCList(sortColumn, sortOrder, pageIndex, rowCount, startdate, enddate);

            #region search
            DateTime? fromDate = null;
            DateTime? toDate = null;
            string sFrom = null;
            string sEnd = null;
            if (!string.IsNullOrEmpty(startdate))
            {
                fromDate = DateTime.Parse(startdate);
                sFrom = ConvertUtil.ConvertDateTimeToTicks(fromDate.Value).ToString();
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                toDate = DateTime.Parse(enddate);
                sEnd = ConvertUtil.ConvertDateTimeToTicks(toDate.Value).ToString();
            }

            #endregion

            List<sp_SR_GetOpenCloseRequestResult> empList = srDao.GetListReportStatus(fromDate, toDate);
            
            int totalRecords = empList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            List<sp_SR_GetOpenCloseRequestResult> finalList = srDao.SortReportStatus(empList, sortColumn, sortOrder)
                                 .Skip((currentPage - 1) * rowCount)
                                  .Take(rowCount).ToList<sp_SR_GetOpenCloseRequestResult>();

            var sum_open = from p in finalList
                           select p.TotalOpened;
            var sum_close = from p in finalList
                            select p.TotalClosed;

            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                userdata = new
                {
                    Category = "Total",
                    Open = sum_open.Sum().ToString(),
                    Close = sum_close.Sum().ToString(),
                    Total = (sum_open.Sum() - sum_close.Sum()).ToString()
                },
                rows = (
                    from m in finalList
                    select new
                    {
                        i = m.Name,
                        cell = new string[] {                   
                            m.Name,
                            CommonFunc.Link(m.CategoryId.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListOpenClose/?id=" +
                                m.CategoryId.ToString() + "&type=1&from=" + sFrom +"&to=" + sEnd + "\",\"Open close request list\", 860);", m.TotalOpened.ToString(), false),                            
                            CommonFunc.Link(m.CategoryId.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListOpenClose/?id=" +
                                m.CategoryId.ToString() + "&type=2&from=" + sFrom +"&to=" + sEnd + "\",\"Open close request list\", 860);", m.TotalClosed.ToString(), false),                            
                            m.Total.ToString()
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Export open close status to excel
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult ExportOCStatusExcel()
        {
            string columnName = "ID";
            string order = "desc";
            Hashtable hashData = (Hashtable)Session[SessionKey.SR_REPORT_OC_LIST_ADMIN_SEARCH_VALUES];
            DateTime? fromDate = null;
            DateTime? toDate = null;
            if (hashData != null)
            {
                if (!string.IsNullOrEmpty((string)hashData[Constants.SR_REPORT_OC_ADMIN_START_DATE]))
                    fromDate = DateTime.Parse((string)hashData[Constants.SR_REPORT_OC_ADMIN_START_DATE]);
                if (!string.IsNullOrEmpty((string)hashData[Constants.SR_REPORT_OC_ADMIN_END_DATE]))
                    toDate = DateTime.Parse((string)hashData[Constants.SR_REPORT_OC_ADMIN_END_DATE]);

                columnName = !string.IsNullOrEmpty((string)hashData[Constants.SR_REPORT_OC_LIST_ADMIN_COLUMN]) ? (string)hashData[Constants.SR_REPORT_OC_LIST_ADMIN_COLUMN] : "ID";
                order = !string.IsNullOrEmpty((string)hashData[Constants.SR_REPORT_OC_LIST_ADMIN_ORDER]) ? (string)hashData[Constants.SR_REPORT_OC_LIST_ADMIN_ORDER] : "desc";
            }
            
            List<sp_SR_GetOpenCloseRequestResult> empList = srDao.GetListReportStatus(fromDate, toDate);
            empList = srDao.SortReportStatus(empList, columnName, order);
            
            ExportExcel exp = new ExportExcel();
            
            string[] column = new string[] { "Name", "TotalOpened:text", "TotalClosed:text", "Total:text" };
            string[] header = new string[] { "Category", "Opened SRs", "Closed SRs", "Total (opened - closed)" };
            var sum_open = from p in empList
                           select p.TotalOpened;
            var sum_close = from p in empList
                            select p.TotalClosed;
            string[] footer = new string[] {"", "Total:left", sum_open.Sum().ToString() + ":center",
                sum_close.Sum().ToString() + ":center", (sum_open.Sum() - sum_close.Sum()).ToString() + ":center" };

            exp.Title = Constants.SR_REPORT_OC_TILE_EXPORT_EXCEL;
            exp.FileName = Constants.SR_REPORT_OC_EXPORT_EXCEL_NAME;
            exp.ColumnList = column;
            exp.HeaderExcel = header;
            exp.FooterExcel = footer;
            exp.List = empList;
            exp.IsRenderNo = true;
            exp.Execute();

            return View();
        }
        
        /// <summary>
        /// Export activity report to excel
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult ExportActivityExcel()
        {
            string columnName = "ID";
            string order = "desc";
            Hashtable hashData = (Hashtable)Session[SessionKey.SR_REPORT_ACTIVITY_LIST_ADMIN_SEARCH_VALUES];
            DateTime? fromDate = null;
            DateTime? toDate = null;
            if (hashData != null)
            {
                if (!string.IsNullOrEmpty((string)hashData[Constants.SR_REPORT_ACTIVITY_ADMIN_START_DATE]))
                    fromDate = DateTime.Parse((string)hashData[Constants.SR_REPORT_ACTIVITY_ADMIN_START_DATE]);
                if (!string.IsNullOrEmpty((string)hashData[Constants.SR_REPORT_ACTIVITY_ADMIN_END_DATE]))
                    toDate = DateTime.Parse((string)hashData[Constants.SR_REPORT_ACTIVITY_ADMIN_END_DATE]);

                columnName = !string.IsNullOrEmpty((string)hashData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_COLUMN]) ? (string)hashData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_COLUMN] : "ID";
                order = !string.IsNullOrEmpty((string)hashData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_ORDER]) ? (string)hashData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_ORDER] : "desc";
            }
            List<sp_SR_GetSRActivityResult> empList = srDao.GetListReportActivity(fromDate, toDate);
            empList = srDao.SortReportActivity(empList, columnName, order);
            TableItemStyle footerStyle = new TableItemStyle();
            footerStyle.Font.Size = 10;
            footerStyle.Font.Bold = true;
            footerStyle.HorizontalAlign = HorizontalAlign.Right;
            footerStyle.BorderStyle = BorderStyle.Groove;
            footerStyle.BorderWidth = Unit.Parse(".5pt");
            
            ExportExcel exp = new ExportExcel(null, null, null, null, footerStyle);
            
            string[] column = new string[] { "Department", "SR_Count:number", "TotalTime:hhMM"};
            string[] header = new string[] { "Department", "SR Count", "Total Time" };
            var sum_sr = from p in empList
                         select p.SR_Count;
            var sum_total = from p in empList
                            select p.TotalTime;
            string[] footer = new string[] { "", "Grand Total:left", sum_sr.Sum().ToString() + ":right", sum_total.Sum().ToString() + ":hhmm" };
            exp.Title = Constants.SR_REPORT_ACTIVITY_TILE_EXPORT_EXCEL;
            exp.FileName = Constants.SR_REPORT_ACTIVITY_EXPORT_EXCEL_NAME;
            exp.ColumnList = column;
            exp.HeaderExcel = header;
            exp.FooterExcel = footer;
            exp.List = empList;
            exp.IsRenderNo = true;
            exp.Execute();

            return View();
        }

        /// <summary>
        /// Export weekly report to excel
        /// </summary>
        /// <param name="startDate">request date from</param>
        /// <param name="endDate">request date to</param>
        /// <returns>ExcelResult</returns>
        public ExcelResult  ExportWeeklyReport(string startDate, string endDate)
        {
            string columnName_act = "ID";
            string order_act = "desc";
            string columnName_it = "ID";
            string order_it = "desc";
            Hashtable hashData = (Hashtable)Session[SessionKey.SR_REPORT_WEEKLY_LIST_ADMIN_SEARCH_VALUES];
            DateTime? fromDate = null;
            DateTime? toDate = null;
            if (!string.IsNullOrEmpty(startDate))
                fromDate = DateTime.Parse(startDate);
            if (!string.IsNullOrEmpty(endDate))
                toDate = DateTime.Parse(endDate);

            if (hashData != null)
            {                
                columnName_act = !string.IsNullOrEmpty((string)hashData[Constants.SR_REPORT_ACTIVE_LIST_ADMIN_COLUMN]) ? (string)hashData[Constants.SR_REPORT_ACTIVE_LIST_ADMIN_COLUMN] : "ID";
                order_act = !string.IsNullOrEmpty((string)hashData[Constants.SR_REPORT_ACTIVE_LIST_ADMIN_ORDER]) ? (string)hashData[Constants.SR_REPORT_ACTIVE_LIST_ADMIN_ORDER] : "desc";
                columnName_it= !string.IsNullOrEmpty((string)hashData[Constants.SR_REPORT_ITEAM_LIST_ADMIN_COLUMN]) ? (string)hashData[Constants.SR_REPORT_ITEAM_LIST_ADMIN_COLUMN] : "ID";
                order_it = !string.IsNullOrEmpty((string)hashData[Constants.SR_REPORT_ITEAM_LIST_ADMIN_ORDER]) ? (string)hashData[Constants.SR_REPORT_ITEAM_LIST_ADMIN_ORDER] : "desc";
            }
            // title
            string subTit = string.Empty;
            if (fromDate.HasValue)
                subTit += " From: " + fromDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW);
            if (toDate.HasValue)
                subTit += " To: " + toDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW);
            if(!string.IsNullOrEmpty(subTit))
                subTit = "(" + subTit + ")";
            // list of all status
            List<sp_SR_GetReportAllStatusResult> actList = srDao.GetListReportActive(fromDate, toDate);
            actList = srDao.SortReportActive(actList, columnName_act, order_act);
            // list of it team
            List<sp_SR_GetReportITTeamResult> itList = srDao.GetListReportITTeam(fromDate, toDate);
            itList = srDao.SortReportITTeam(itList, columnName_it, order_it);
            // list request closed
            int max_row = 0;
            List<RequestClosed> empList = srDao.GetListEmpClosed(fromDate, toDate, ref max_row);
            string[][] emp = GetSeperateListEmpClosed(empList, max_row);
            string[] header_closed = (from p in empList
                                     select p.emp_name).ToArray();
            string[] column_closed = (from p in empList
                                      select p.emp_name).ToArray();
            string[] footer_closed = (from p in empList
                                     select p.arrID.Count().ToString()).ToArray();
            // end closed
            string[] column_act = new string[] { "DisplayName", "TotalActive:number", "TotalNew:number", "TotalOpen:number", "TotalToBeApprove:number",
                    "TotalClosed:number", "TotalVerifiedClosed:number", "TotalPending:number", "TotalPostponed:number", "TotalApproved:number", "TotalRejected:number"};
            
            string[] header_act = new string[] { "Assigned to / Status", "Active", "New", "Open", "To be Approved", "Closed", "Verified closed", "Pending", "Postponed"
                , "Approved", "Rejected"};
            
            string[] column_it = new string[] { "DisplayName", "TotalTime:hhMM", "TotalOpened:number" ,"TotalClosed:number"};            
            string[] header_it = new string[] { "Name Helpdesk", "Amount time work for 1 week", "Total request opened", "Total request closed" };
            
            ExportExcelAdvance exp = new ExportExcelAdvance();                        
            
            exp.Sheets = new List<CExcelSheet>{ new CExcelSheet{ Name="ID-Request Active", 
                                                    List= actList,
                                                    Header = header_act, 
                                                    ColumnList= column_act,
                                                    Title = Constants.SR_REPORT_ACTIVITY_TILE_EXPORT_EXCEL + subTit,                
                                                    IsRenderNo = true},
                                                new CExcelSheet{ Name="Request Closed", 
                                                    List= emp,
                                                    Header = header_closed, 
                                                    Footer = footer_closed,
                                                    ColumnList= column_closed,
                                                    Title = Constants.SR_REPORT_CLOSED_TILE_EXPORT_EXCEL  + subTit,
                                                    IsRenderNo = true},
                                                new CExcelSheet{ Name="Request Detail IT Team", 
                                                    List= itList,
                                                    Header = header_it, 
                                                    ColumnList= column_it,
                                                    Title = Constants.SR_REPORT_ITEAM_TILE_EXPORT_EXCEL  + subTit,
                                                    IsRenderNo = true}                                                
                                                };
            
            string filepath = Server.MapPath("~/Export/") + "reportweekly" + DateTime.Now.Ticks.ToString() + ".xlsx";            
            string filename = exp.ExportExcelMultiSheet(filepath);
            filename = Constants.SR_REPORT_WEEKLY_EXPORT_EXCEL_NAME;

            return new ExcelResult { FileName = filename, Path = filepath };
        }

        /// <summary>
        /// Get list jq grid for activity report
        /// </summary>
        /// <param name="startdate">string</param>
        /// <param name="enddate">string</param>
        /// <returns></returns>
        public ActionResult GetListJQGridReportActivity(string startdate, string enddate)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilterSRReportActivityList(sortColumn, sortOrder, pageIndex, rowCount, startdate, enddate);

            #region search
            DateTime? fromDate = null;
            DateTime? toDate = null;
            string sFrom = null;
            string sEnd = null;
            if (!string.IsNullOrEmpty(startdate))
            {
                fromDate = DateTime.Parse(startdate);
                sFrom = ConvertUtil.ConvertDateTimeToTicks(fromDate.Value).ToString();
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                toDate = DateTime.Parse(enddate);
                sEnd = ConvertUtil.ConvertDateTimeToTicks(toDate.Value).ToString();
            }
            #endregion

            List<sp_SR_GetSRActivityResult> empList = srDao.GetListReportActivity(fromDate, toDate);

            int totalRecords = empList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            List<sp_SR_GetSRActivityResult> finalList = srDao.SortReportActivity(empList, sortColumn, sortOrder)
                                 .Skip((currentPage - 1) * rowCount)
                                  .Take(rowCount).ToList<sp_SR_GetSRActivityResult>();

            var sum_sr  = from  p in finalList   
                          select p.SR_Count;
            var sum_total = from p in finalList
                            select p.TotalTime;

            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                userdata = new
                {
                    Department = "Grand Total",
                    SR_Count = sum_sr.Sum().ToString(),
                    TotalTime = sum_total.Sum().HasValue ? CommonFunc.FormatTime(sum_total.Sum().Value) :""
                },
                rows = (
                    from m in finalList
                    select new
                    {
                        i = m.Department,
                        cell = new string[] {                   
                            m.SubDepartment,                            
                            CommonFunc.Link(m.DepartmentId.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListActivities/?id=" + m.DepartmentId.ToString() + 
                            "&from=" + sFrom +"&to="+ sEnd + "\",\"activities list\", 860);", m.SR_Count.ToString(), false),
                            m.TotalTime.HasValue ? CommonFunc.FormatTime(m.TotalTime.Value):""
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get list jq grid for iteam report
        /// </summary>
        /// <param name="startdate">string</param>
        /// <param name="enddate">string</param>
        /// <returns>ActionResult</returns>
        public ActionResult GetListJQGridReportITeam(string startdate, string enddate)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilterWeeklyReportList(sortColumn, sortOrder, pageIndex, rowCount, startdate, enddate, "it");

            #region search
            DateTime? fromDate = null;
            DateTime? toDate = null;
            string sFrom = null;
            string sEnd = null;
            if (!string.IsNullOrEmpty(startdate))
            {
                fromDate = DateTime.Parse(startdate);
                sFrom = ConvertUtil.ConvertDateTimeToTicks(fromDate.Value).ToString();
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                toDate = DateTime.Parse(enddate);
                sEnd = ConvertUtil.ConvertDateTimeToTicks(toDate.Value).ToString();
            }
            #endregion

            List<sp_SR_GetReportITTeamResult> empList = srDao.GetListReportITTeam(fromDate, toDate);
            
            int totalRecords = empList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            List<sp_SR_GetReportITTeamResult> finalList = srDao.SortReportITTeam(empList, sortColumn, sortOrder)
                                 .Skip((currentPage - 1) * rowCount)
                                  .Take(rowCount).ToList<sp_SR_GetReportITTeamResult>();
                        
            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,            
                rows = (
                    from m in finalList
                    select new
                    { 
                        i = m.DisplayName,
                        cell = new string[] {                   
                            m.DisplayName,
                            m.TotalTime.HasValue ? CommonFunc.FormatTime(m.TotalTime.Value) : "",
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListITeam/?id=" +
                                m.DisplayName.ToString() + "&type=3&from=" + sFrom + "&to=" + sEnd + "\",\"IT helpdesk list\", 860);", m.TotalOpened.ToString(), false),
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListITeam/?id=" +
                                m.DisplayName.ToString() + "&type=2&from=" + sFrom + "&to=" + sEnd + "\",\"IT helpdesk list\", 860);", m.TotalClosed.ToString(), false),
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListITeam/?id=" +
                                m.DisplayName.ToString() + "&type=1&from=" + sFrom + "&to=" + sEnd + "\",\"IT helpdesk list\", 860);", m.TotalDoing.ToString(), false)
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get list jq grid for active report
        /// </summary>
        /// <param name="startdate">string</param>
        /// <param name="enddate">string</param>
        /// <returns>ActionResult</returns>
        public ActionResult GetListJQGridReportActive(string startdate, string enddate)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilterWeeklyReportList(sortColumn, sortOrder, pageIndex, rowCount, startdate, enddate, "active");

            #region search
            DateTime? fromDate = null;
            DateTime? toDate = null;
            string sFrom = null;
            string sEnd = null;
            if (!string.IsNullOrEmpty(startdate))
            {
                fromDate = DateTime.Parse(startdate);
                sFrom = ConvertUtil.ConvertDateTimeToTicks(fromDate.Value).ToString();
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                toDate = DateTime.Parse(enddate);
                sEnd = ConvertUtil.ConvertDateTimeToTicks(toDate.Value).ToString();
            }

            #endregion

            List<sp_SR_GetReportAllStatusResult> empList = srDao.GetListReportActive(fromDate, toDate);

            int totalRecords = empList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            List<sp_SR_GetReportAllStatusResult> finalList = srDao.SortReportActive(empList, sortColumn, sortOrder)
                                 .Skip((currentPage - 1) * rowCount)
                                  .Take(rowCount).ToList<sp_SR_GetReportAllStatusResult>();
            
            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        i = m.DisplayName,
                        cell = new string[] {                   
                            m.DisplayName,        
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListActive/?id=" +
                                m.DisplayName.ToString() + "&type=10&from=" + sFrom + "&to=" + sEnd +"\",\"Request active list\", 860);", m.TotalActive.ToString(), false),
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListActive/?id=" +
                                m.DisplayName.ToString() + "&type="+ Constants.SR_STATUS_NEW + "&from=" + sFrom + "&to=" + sEnd + "\",\"Request active list\", 860);", m.TotalNew.ToString(), false),
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListActive/?id=" +
                                m.DisplayName.ToString() + "&type="+ Constants.SR_STATUS_OPEN + "&from=" + sFrom + "&to=" + sEnd + "\",\"Request active list\", 860);", m.TotalOpen.ToString(), false),
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListActive/?id=" +
                                m.DisplayName.ToString() + "&type="+ Constants.SR_STATUS_TO_BE_APPROVED + "&from=" + sFrom + "&to=" + sEnd + "\",\"Request active list\", 860);", m.TotalToBeApprove.ToString(), false),
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListActive/?id=" +
                                m.DisplayName.ToString() + "&type="+ Constants.SR_STATUS_CLOSED + "&from=" + sFrom + "&to=" + sEnd + "\",\"Request active list\", 860);", m.TotalClosed.ToString(), false),
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListActive/?id=" +
                                m.DisplayName.ToString() + "&type="+ Constants.SR_STATUS_VERIFIED_CLOSED + "&from=" + sFrom + "&to=" + sEnd + "\",\"Request active list\", 860);", m.TotalVerifiedClosed.ToString(), false),
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListActive/?id=" +
                                m.DisplayName.ToString() + "&type="+ Constants.SR_STATUS_PENDING + "&from=" + sFrom + "&to=" + sEnd + "\",\"Request active list\", 860);", m.TotalPending.ToString(), false),
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListActive/?id=" +
                                m.DisplayName.ToString() + "&type="+ Constants.SR_STATUS_POSTPONED + "&from=" + sFrom + "&to=" + sEnd + "\",\"Request active list\", 860);", m.TotalPostponed.ToString(), false),
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListActive/?id=" +
                                m.DisplayName.ToString() + "&type="+ Constants.SR_STATUS_APPROVED + "&from=" + sFrom + "&to=" + sEnd + "\",\"Request active list\", 860);", m.TotalApproved.ToString(), false),
                            CommonFunc.Link(m.DisplayName.ToString(), "javascript:CRM.popup(\"/ServiceRequestAdmin/ListActive/?id=" +
                                m.DisplayName.ToString() + "&type="+ Constants.SR_STATUS_REJECTED + "&from=" + sFrom + "&to=" + sEnd + "\",\"Request active list\", 860);", m.TotalRejected.ToString(), false)
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get list jq grid for closed report
        /// </summary>
        /// <param name="startdate">request date from</param>
        /// <param name="enddate">request date to</param>
        /// <returns>List<RequestClosed></returns>
        public List<RequestClosed> GetListJQGridReportClosed(string startdate, string enddate)
        {
            
            SetSessionFilterWeeklyReportList("", "", 1, 20, startdate, enddate, "closed");

            #region search
            DateTime? fromDate = null;
            DateTime? toDate = null;

            if (!string.IsNullOrEmpty(startdate))
            {
                fromDate = DateTime.Parse(startdate);
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                toDate = DateTime.Parse(enddate);
            }
            #endregion
            int max_row = 0;
            List<RequestClosed> empList = srDao.GetListEmpClosed(fromDate, toDate, ref max_row);
            string[][] emp = GetSeperateListEmpClosed(empList, max_row);
            ViewData[Constants.SR_REPORT_WEEKLY_REQUEST_CLOSED] = emp;
           
            return empList;
        }

        /// <summary>
        /// Get seperate list
        /// </summary>
        /// <param name="empList">List<RequestClosed></param>
        /// <param name="max_row">int</param>
        /// <returns>string[][]</returns>
        private string[][] GetSeperateListEmpClosed(List<RequestClosed> empList, int max_row)
        {
            // Amount emp is amount columns too
            int max_col = empList != null ? empList.Count() : 0;
            string[][] rows = new string[max_row][];
            for (int i = 0; i < max_row; i++)
            {
                rows[i] = new string[max_col];
                for (int j = 0; j < max_col; j++)
                {
                    if (empList[j].arrID.Count() > i)
                    {
                        rows[i][j] = empList[j].arrID[i] != null ? empList[j].arrID[i].ToString() : string.Empty;
                    }
                }

            }

            return rows;
        }

        /// <summary>
        /// Activities list
        /// </summary>
        /// <param name="id">department id</param>
        /// <returns>ActionResult</returns>
        public ActionResult ListActivities(string id, string from, string to)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(from))
                startDate = ConvertUtil.ConvertTicksToDateTime(long.Parse(from));
            if (!string.IsNullOrEmpty(to))
                endDate = ConvertUtil.ConvertTicksToDateTime(long.Parse(to));

            List<sp_SR_GetListActivityResult> list = srDao.GetListActivities(startDate, endDate, ConvertUtil.ConvertToInt(id));
            ArrayList arr = new ArrayList();
            if (list != null && list.Count() > 0)
            { 
                foreach(sp_SR_GetListActivityResult item in list)
                {
                    string[] items = new string[] {item.ID.ToString(), item.Title, item.RequestUser, item.CreateDate.ToString(Constants.DATETIME_FORMAT_TIME), 
                            item.StatusName, item.TotalTime.HasValue ? CommonFunc.FormatTime(item.TotalTime.Value):""}; 
                    arr.Add(items);
                }
            }
            Session[SessionKey.SR_REPORT_LIST] = arr;
            return View("ListDetail", arr);
        }

        public ExcelResult ExportListDetail()
        {
            
            ArrayList arr = (ArrayList)Session[SessionKey.SR_REPORT_LIST];
            string[] column = new string[] { "ID:sr", "Title:text", "RequestUser", "requestDate", "status", "totalTime" };
            string[] header = new string[] { "ID", "Title", "Request User", "Request Date", "Status", "Total Time" };

            ExportExcelAdvance exp = new ExportExcelAdvance();

            exp.Sheets = new List<CExcelSheet>{ new CExcelSheet{ Name="ID-Request Active", 
                                                    List= arr,
                                                    Header = header, 
                                                    ColumnList= column,
                                                    Title = "Detail list",
                                                    IsRenderNo = true}
                                                };

            string filepath = Server.MapPath("~/Export/") + "reportListDetail" + DateTime.Now.Ticks.ToString() + ".xlsx";
            string filename = exp.ExportExcelMultiSheet(filepath);
            filename = Constants.SR_REPORT_LIST_DETAIL_EXCEL_NAME;

            return new ExcelResult { FileName = filename, Path = filepath };
        }

        /// <summary>
        /// List open close status
        /// </summary>
        /// <param name="id">category Id</param>
        /// <param name="type">1:open; 2:close</param>
        /// <returns>ActionResult</returns>
        public ActionResult ListOpenClose(string id, string type, string from, string to)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(from))
                startDate = ConvertUtil.ConvertTicksToDateTime(long.Parse(from));
            if (!string.IsNullOrEmpty(to))
                endDate = ConvertUtil.ConvertTicksToDateTime(long.Parse(to));

            List<sp_SR_GetListOpenCloseRequestResult> list = srDao.GetListReportOpenClose(startDate, endDate, ConvertUtil.ConvertToInt(id), ConvertUtil.ConvertToInt(type));
            ArrayList arr = new ArrayList();
            if (list != null && list.Count() > 0)
            {
                foreach (sp_SR_GetListOpenCloseRequestResult item in list)
                {
                    string[] items = new string[] {item.ID.ToString(), item.Title, item.RequestUser, item.CreateDate.ToString(Constants.DATETIME_FORMAT_TIME), 
                            item.StatusName, item.TotalTime.HasValue ? CommonFunc.FormatTime(item.TotalTime.Value):""};
                    arr.Add(items);
                }
            }
            Session[SessionKey.SR_REPORT_LIST] = arr;
            return View("ListDetail", arr);
        }

        /// <summary>
        /// List ITeam 
        /// </summary>
        /// <param name="id">assign user</param>
        /// <param name="type">1:open; 2:close</param>
        /// <returns>ActionResult</returns>
        public ActionResult ListITeam(string id, string type, string from, string to)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(from))
                startDate = ConvertUtil.ConvertTicksToDateTime(long.Parse(from));
            if (!string.IsNullOrEmpty(to))
                endDate = ConvertUtil.ConvertTicksToDateTime(long.Parse(to));

            List<sp_SR_GetListReportITTeamResult> list = srDao.GetListITeam(startDate, endDate, id, ConvertUtil.ConvertToInt(type));
            ArrayList arr = new ArrayList();
            if (list != null && list.Count() > 0)
            {
                foreach (sp_SR_GetListReportITTeamResult item in list)
                {
                    string[] items = new string[] {item.ID.ToString(), item.Title, item.RequestUser, item.CreateDate.ToString(Constants.DATETIME_FORMAT_TIME), 
                            item.StatusName, item.TotalTime.HasValue ? CommonFunc.FormatTime(item.TotalTime.Value):""};
                    arr.Add(items);
                }
            }
            Session[SessionKey.SR_REPORT_LIST] = arr;
            return View("ListDetail", arr);

        }

        /// <summary>
        /// Active list
        /// </summary>
        /// <param name="id">assign user</param>
        /// <param name="type">status id</param>
        /// <returns>ActionResult</returns>
        public ActionResult ListActive(string id, string type,string from, string to)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            
            if(!string.IsNullOrEmpty(from))
                startDate = ConvertUtil.ConvertTicksToDateTime(long.Parse(from));
            if (!string.IsNullOrEmpty(to))
                endDate = ConvertUtil.ConvertTicksToDateTime(long.Parse(to));

            List<sp_SR_GetListReportStatusResult> list = srDao.GetListAllStatus(startDate, endDate, id, ConvertUtil.ConvertToInt(type));
            ArrayList arr = new ArrayList();
            if (list != null && list.Count() > 0)
            {
                foreach (sp_SR_GetListReportStatusResult item in list)
                {
                    string[] items = new string[] {item.ID.ToString(), item.Title, item.RequestUser, item.CreateDate.ToString(Constants.DATETIME_FORMAT_TIME), 
                            item.StatusName, item.TotalTime.HasValue ? CommonFunc.FormatTime(item.TotalTime.Value):""};
                    arr.Add(items);
                }
            }
            Session[SessionKey.SR_REPORT_LIST] = arr;
            return View("ListDetail", arr);

        }

        /// <summary>
        /// Weekly report action
        /// </summary>
        /// <returns>ActionResult</returns>
         [CrmAuthorizeAttribute(Module = Modules.ServiceRequestReport, Rights = Permissions.WeeklyReport)]
        public ActionResult WeeklyReport()
        {
            Hashtable hashData = Session[SessionKey.SR_REPORT_WEEKLY_LIST_ADMIN_SEARCH_VALUES] == null ? new Hashtable() :
             (Hashtable)Session[SessionKey.SR_REPORT_WEEKLY_LIST_ADMIN_SEARCH_VALUES];
            // Get date now
            DateTime date1 = DateTime.Now;
            DateTime date2 = date1.AddDays(-7);

            ViewData[Constants.SR_REPORT_WEEKLY_ADMIN_START_DATE] = hashData[Constants.SR_REPORT_WEEKLY_ADMIN_START_DATE] == null ? date2.ToString(Constants.DATETIME_FORMAT) :
                hashData[Constants.SR_REPORT_WEEKLY_ADMIN_START_DATE];
            ViewData[Constants.SR_REPORT_WEEKLY_ADMIN_END_DATE] = hashData[Constants.SR_REPORT_WEEKLY_ADMIN_END_DATE] == null ? date1.ToString(Constants.DATETIME_FORMAT) :
                hashData[Constants.SR_REPORT_WEEKLY_ADMIN_END_DATE];

            ViewData[Constants.SR_REPORT_ACTIVE_LIST_ADMIN_COLUMN] = hashData[Constants.SR_REPORT_ACTIVE_LIST_ADMIN_COLUMN] == null ? "ID" :
                hashData[Constants.SR_REPORT_ACTIVE_LIST_ADMIN_COLUMN];
            ViewData[Constants.SR_REPORT_ACTIVE_LIST_ADMIN_ORDER] = hashData[Constants.SR_REPORT_ACTIVE_LIST_ADMIN_ORDER] == null ? "asc" :
                hashData[Constants.SR_REPORT_ACTIVE_LIST_ADMIN_ORDER];

            ViewData[Constants.SR_REPORT_ITEAM_LIST_ADMIN_COLUMN] = hashData[Constants.SR_REPORT_ITEAM_LIST_ADMIN_COLUMN] == null ? "ID" :
                hashData[Constants.SR_REPORT_ITEAM_LIST_ADMIN_COLUMN];
            ViewData[Constants.SR_REPORT_ITEAM_LIST_ADMIN_ORDER] = hashData[Constants.SR_REPORT_ITEAM_LIST_ADMIN_ORDER] == null ? "asc" :
                hashData[Constants.SR_REPORT_ITEAM_LIST_ADMIN_ORDER];

            return View();
        }

        /// <summary>
        /// Get tab of weekly report
        /// </summary>
        /// <param name="id">tab id</param>
        /// <param name="startDate">request date from</param>
        /// <param name="endDate">request date to</param>
        /// <returns>ActionResult</returns>
        public ActionResult getAjaxTab(int id, string startDate, string endDate)
        {
            string viewName = string.Empty;
            List<RequestClosed> model = new List<RequestClosed>();
            switch (id)
            {
                case 1:
                    viewName = "UCReportActive";
                    break;
                case 2:
                    model = GetListJQGridReportClosed(startDate, endDate);
                    viewName = "UCReportClosed";
                    break;
                case 3:
                    viewName = "UCReportITeam";
                    break;
                case 4:
                    viewName = "_error";
                    break;
            }

            return PartialView(viewName, model);
        }
        
        /// <summary>
        /// Get list for navigation
        /// </summary>
        /// <returns>List<sp_SR_GetServiceRequest4AdminResult></returns>
        public List<sp_SR_GetServiceRequest4AdminResult> GetListSRForNavigation()
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string textSearch = string.Empty;
            int subcat = 0;
            int categoryId = 0;
            int statusId = 0;
            string assignName = null;
            string requestor = null;
            DateTime? fromDate = null;
            DateTime? toDate = null;
            string column = "ID";
            string order = "desc";
            List<sp_SR_GetServiceRequest4AdminResult> list = null;

            if (Session[SessionKey.SR_LIST_ADMIN_SEARCH_VALUES] != null)
            {
                Hashtable hashData = (Hashtable)Session[SessionKey.SR_LIST_ADMIN_SEARCH_VALUES];
                textSearch = (string)hashData[Constants.SR_LIST_ADMIN_TITLE];
                textSearch = textSearch.Trim();
                if (textSearch == Constants.SR_FIRST_KEY_WORD)
                {
                    textSearch = string.Empty;
                }

                statusId = !string.IsNullOrEmpty((string)hashData[Constants.SR_ADMIN_STATUS_LIST]) ? int.Parse((string)hashData[Constants.SR_ADMIN_STATUS_LIST]) : 0;
                categoryId = !string.IsNullOrEmpty((string)hashData[Constants.SR_ADMIN_CATEGORY_LIST]) ? int.Parse((string)hashData[Constants.SR_ADMIN_CATEGORY_LIST]) : 0;
                subcat = !string.IsNullOrEmpty((string)hashData[Constants.SR_ADMIN_SUBCATEGORY_LIST]) ? int.Parse((string)hashData[Constants.SR_ADMIN_SUBCATEGORY_LIST]) : 0;
                assignName = !string.IsNullOrEmpty((string)hashData[Constants.SR_ADMIN_ASSIGNTO_LIST]) ? (string)hashData[Constants.SR_ADMIN_ASSIGNTO_LIST] : null;
                requestor = !string.IsNullOrEmpty((string)hashData[Constants.SR_ADMIN_REQUESTOR_LIST]) ? (string)hashData[Constants.SR_ADMIN_REQUESTOR_LIST] : null;

                if (!string.IsNullOrEmpty((string)hashData[Constants.SR_ADMIN_START_DATE]))
                {
                    fromDate = DateTime.Parse((string)hashData[Constants.SR_ADMIN_START_DATE]);
                }
                if (!string.IsNullOrEmpty((string)hashData[Constants.SR_ADMIN_END_DATE]))
                {
                    toDate = DateTime.Parse((string)hashData[Constants.SR_ADMIN_END_DATE]);
                }

                column = !string.IsNullOrEmpty((string)hashData[Constants.SR_LIST_ADMIN_COLUMN]) ? (string)hashData[Constants.SR_LIST_ADMIN_COLUMN] : "ID";
                order = !string.IsNullOrEmpty((string)hashData[Constants.SR_LIST_ADMIN_ORDER]) ? (string)hashData[Constants.SR_LIST_ADMIN_ORDER] : "desc";

            }

            list = srDao.GetList4Admin(textSearch, subcat, categoryId, statusId, assignName, fromDate, toDate, requestor);
            list = srDao.SortAdmin(list, column, order);

            return list;
        }

        public ActionResult Navigation(string name, string id)
        {
            List<sp_SR_GetServiceRequest4AdminResult> srList;
            srList = GetListSRForNavigation();
            string testID = string.Empty;
            int index = 0;
            string url = string.Empty;
            switch (name)
            {
                case "First":
                    testID = srList[0].ID.ToString();
                    break;
                case "Prev":
                    index = srList.IndexOf(srList.Where(p => p.ID.ToString() == id).FirstOrDefault<sp_SR_GetServiceRequest4AdminResult>());
                    if (index != 0)
                    {
                        testID = srList[index - 1].ID.ToString();
                    }
                    else
                    {
                        testID = id;
                    }
                    break;
                case "Next":
                    index = srList.IndexOf(srList.Where(p => p.ID.ToString() == id).FirstOrDefault<sp_SR_GetServiceRequest4AdminResult>());
                    if (index != srList.Count - 1)
                    {
                        testID = srList[index + 1].ID.ToString();
                    }
                    else
                    {
                        testID = id;
                    }
                    break;
                case "Last":
                    testID = srList[srList.Count - 1].ID.ToString();
                    break;
            }
            return RedirectToAction("Detail/" + testID);
        }

        #endregion

        #region Methods (Tai Nguyen: Submit, Edit, Close SR, GetApproval)
        /// <summary>
        /// Handle the Category change event
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult CategoryOnChange(string id)
        {
            int? cateId = -1;//Get Sub category
            int tmpId = ConvertUtil.ConvertToInt(id);
            if (!string.IsNullOrEmpty(id) && tmpId != 0 && tmpId != -1)
                cateId = tmpId;
            var subCateList = catDao.GetList(null, cateId, true);
            subCateList = subCateList.Where(p => p.IsParentActive.HasValue && p.IsParentActive.Value).ToList();
            return Json(new { subCategories = subCateList.ToArray() }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get the assign user list
        /// </summary>
        /// <param name="srStatusId"></param>
        /// <param name="requestor"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public List<string> GetAssignList(int srStatusId, string requestor, ref string selected)
        {
            selected = "";
            int statusId = ConvertUtil.ConvertToInt(srStatusId);
            if (statusId == Constants.SR_STATUS_TO_BE_APPROVED)
            {
                var resultList = empDao.GetListManagerWithAllAttr(null).
                    Select(p => CommonFunc.GetUserNameLoginByEmpID(p.ID)).Where(p => !string.IsNullOrEmpty(p));
                selected = string.IsNullOrEmpty(requestor) ? "" :
                    CommonFunc.GetUserNameLoginByEmpID(CommonFunc.GetEmployeeByUserName(requestor).ManagerId);
                return resultList.ToList();
            }
            else
            {
                var resultList = userDao.GetListITHelpDesk((int)Modules.ServiceRequestAdmin, (int)Permissions.ItHelpDesk).Select(p => p.Name);
                //string directManager = CommonFunc.GetUserNameLoginByEmpID(CommonFunc.GetEmployeeByUserName(requestor).ManagerId);
                return resultList.ToList();
            }
        }
        /// <summary>
        /// Handle the change event of status dropdown list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestor"></param>
        /// <returns></returns>
        public JsonResult StatusOnChange(string id, string requestor)
        {
            int statusId = ConvertUtil.ConvertToInt(id);
            string selectedUser = "";
            var resultList = GetAssignList(statusId, requestor, ref selectedUser);
            return Json(new
            {
                users = resultList.ToArray(),
                selected = selectedUser
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="srId"></param>
        [NonAction]
        private void SendSRMail(int srId,int type)
        {
            //Add send mail logic here
            CommonFunc.SendSRMail(srId, type);
        }
        /// <summary>
        /// GET: submit new SR
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.Insert, ShowAtCurrentPage = true)]
        public ActionResult Create()
        {
            ViewData[CommonDataKey.SR_CATEGORY_LIST] = new SelectList(catDao.GetList(null, 0, true), "ID", "Name");
            ViewData[CommonDataKey.SR_SUB_CATEGORY_LIST] = new SelectList(
                catDao.GetList(null, -1, true).Where(p => p.IsParentActive.HasValue && p.IsParentActive.Value), "ID", "Name");
            ViewData[CommonDataKey.SR_URGENCY_LIST] = new SelectList(srDao.GetUrgencyList(), "ID", "Name");
            ViewData[CommonDataKey.SR_STATUS_LIST] = new SelectList(statusDao.GetAll(), "ID", "Name");
            ViewData[CommonDataKey.SR_ASSIGNED_TO_LIST] = new SelectList(
                userDao.GetListITHelpDesk((int)Modules.ServiceRequestAdmin, (int)Permissions.ItHelpDesk), "Name", "name");
            return View();
        }
        /// <summary>
        /// POST: submit new SR
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.Insert, ShowAtCurrentPage = true)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(SR_ServiceRequest sr)
        {
            Message msg = null;
            try
            {
                sr.CategoryID = ConvertUtil.ConvertToInt(Request[CommonDataKey.SR_SUB_CATEGORY_LIST]);
                if (!string.IsNullOrEmpty(Request[CommonDataKey.SR_URGENCY_LIST]))
                    sr.UrgencyID = ConvertUtil.ConvertToInt(Request[CommonDataKey.SR_URGENCY_LIST]);
                else
                    sr.UrgencyID = Constants.SR_URGENCY_NORMAL_ID;
                if (string.IsNullOrEmpty(sr.RequestUser))
                    sr.RequestUser = sr.SubmitUser;
                if(sr.DueDate.HasValue)
                    sr.DueDate = DateTime.Parse(sr.DueDate.Value.ToString("dd/MM/yyyy") + " " + Request[CommonDataKey.SR_HOURS_LIST]);
                sr.StatusID = ConvertUtil.ConvertToInt(Request[CommonDataKey.SR_STATUS_LIST]);
                sr.AssignUser = Request[CommonDataKey.SR_ASSIGNED_TO_LIST] ?? HttpContext.User.Identity.Name ?? "";
                msg = CheckValidSR(sr);
                if (msg == null)
                    msg = CommonFunc.SR_CheckUploadedFiles(Request.Files);
                if (msg == null)
                {
                    string[] fileNameArr = new string[0];
                    int count = 0;
                    foreach (string fileName in Request.Files)
                    {
                        //Save File
                        HttpPostedFileBase file = Request.Files[fileName];
                        if (file.ContentLength > 0)
                        {
                            int startIndex = file.FileName.LastIndexOf('\\');
                            string newFileName = GetFilePrefix(count) + file.FileName.Substring(startIndex < 0 ? 0 : startIndex + 1);
                            sr.Files += newFileName + Constants.SR_FILE_SEPARATE_SIGN;
                            file.SaveAs(Server.MapPath(Constants.SR_UPLOAD_PATH) + newFileName);
                            count++;
                        }
                    }
                    string sParent = Request["ParentID"];
                    if (!string.IsNullOrEmpty(sParent))
                        sr.ParentID = ConvertUtil.ConvertToInt(sParent.Replace(Constants.SR_SERVICE_REQUEST_PREFIX, ""));
                    msg = srDao.Insert(sr);
                    if (msg.MsgType != MessageType.Error)
                    {
                        //SendMail
                        SendSRMail(sr.ID, Constants.SR_SEND_MAIL_DEFAULT);
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            if (msg.MsgType == MessageType.Error)
                return RedirectToAction("Create");
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Get the prefix of file upload
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [NonAction]
        private string GetFilePrefix(int count)
        {
            return DateTime.Now.ToString(Constants.SR_FILE_NAME_PREFIX_FORMAT) + count + Constants.SR_FILENAME_SEPARATE_SIGN;
        }
        /// <summary>
        /// Check valid SR
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        public Message CheckValidSR(SR_ServiceRequest sr)
        {
            try
            {
                if (sr == null)
                    return new Message(MessageConstants.E0030, MessageType.Error, "Service Request");
                //Check for sub-category
                if (sr.CategoryID == 0)
                    return new Message(MessageConstants.E0030, MessageType.Error, "Sub-Category");
                SR_Category category = catDao.GetById(sr.CategoryID, true);
                if (category == null || !category.SR_Category1.IsActive)//Parent-category must be active
                    return new Message(MessageConstants.E0030, MessageType.Error, "Sub-Category");
                //Check for default assign User
                if (string.IsNullOrEmpty(sr.AssignUser) && sr.StatusID == Constants.SR_STATUS_NEW) // tan.tran 2011.07.04: only check in case Add New
                    return new Message(MessageConstants.E0007, MessageType.Error);
                //Check for Requestor -> if it's not empty, it must exist in database
                var requestor = CommonFunc.GetEmployeeByUserName(sr.RequestUser);
                if (!string.IsNullOrEmpty(sr.RequestUser) && requestor == null)
                    return new Message(MessageConstants.E0030, MessageType.Error, "Requestor");
                if (string.IsNullOrEmpty(requestor.LocationCode))
                    return new Message(MessageConstants.E0046, MessageType.Error, "submit service request", "requestor " + sr.RequestUser + " has no location");
                return null;
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }
        /// <summary>
        /// GET:edit SR
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.Update, ShowAtCurrentPage = true)]
        public ActionResult Edit(string id)
        {
            return EditActionGet(id);
        }
        /// <summary>
        /// POST: edit SR
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.Update, ShowAtCurrentPage = true)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(SR_ServiceRequest sr)
        {
            return EditActionPost(sr);
        }
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestAdmin, Rights = Permissions.ForceEdit, ShowAtCurrentPage = true)]
        public ActionResult ForceEdit(string id)
        {
            return EditActionGet(id);
        }
        /// <summary>
        /// POST: edit SR
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestAdmin, Rights = Permissions.ForceEdit, ShowAtCurrentPage = true)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ForceEdit(SR_ServiceRequest sr)
        {
            return EditActionPost(sr);
        }
        [NonAction]
        public ActionResult EditActionGet(string id)
        {
            SR_ServiceRequest sr = srDao.GetById(ConvertUtil.ConvertToInt(id));
            if (sr != null && sr.StatusID != Constants.SR_STATUS_CLOSED)
            {
                ViewData[CommonDataKey.SR_CATEGORY_LIST] = new SelectList(catDao.GetList(null, 0, true), "ID", "Name", sr.SR_Category.ParentId);
                ViewData[CommonDataKey.SR_SUB_CATEGORY_LIST] = new SelectList(catDao.GetList(null, sr.SR_Category.ParentId, true).
                    Where(p => p.IsParentActive.HasValue && p.IsParentActive.Value), "ID", "Name", sr.CategoryID);
                ViewData[CommonDataKey.SR_URGENCY_LIST] = new SelectList(srDao.GetUrgencyList(), "ID", "Name", sr.UrgencyID);
                ViewData[CommonDataKey.SR_STATUS_LIST] = new SelectList(statusDao.GetAll(), "ID", "Name", sr.StatusID);
                string tmpString = "";
                ViewData[CommonDataKey.SR_ASSIGNED_TO_LIST] = new SelectList(
                    GetAssignList(sr.StatusID, sr.RequestUser, ref tmpString), sr.AssignUser);
                return View("Edit", sr);
            }
            ShowMessage(new Message(MessageConstants.E0005, MessageType.Error, "Service Request", "database or it can't be edited"));
            return RedirectToAction("Index");
        }
        [NonAction]
        public ActionResult EditActionPost(SR_ServiceRequest sr)
        {
            Message msg = null;
            try
            {
                SR_ServiceRequest objDb = srDao.GetById(sr.ID);
                string preAssignUser = objDb.AssignUser;
                int preStatusId = objDb.StatusID;
                //Show error if sr does not exist
                if (objDb == null)
                    msg = new Message(MessageConstants.E0005, MessageType.Error, "Service Request", "database");
                //Show error if sr is not in new status
                else if (objDb.StatusID == Constants.SR_STATUS_CLOSED)
                    msg = new Message(MessageConstants.E0046, MessageType.Error, "edit",
                        Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID + " is closed");
                //Show error if sr has been edited by other user
                else if (!objDb.UpdateDate.ToString().Equals(sr.UpdateDate.ToString()))
                    msg = new Message(MessageConstants.E0025, MessageType.Error, "This Service Request");
                if (msg != null)
                {
                    ShowMessage(msg);
                    return RedirectToAction("Index");
                }
                if (sr.DueDate.HasValue)
                    sr.DueDate = DateTime.Parse(sr.DueDate.Value.ToString("dd/MM/yyyy") + " " + Request[CommonDataKey.SR_HOURS_LIST]);
                sr.StatusID = ConvertUtil.ConvertToInt(Request[CommonDataKey.SR_STATUS_LIST]);
                sr.AssignUser = Request[CommonDataKey.SR_ASSIGNED_TO_LIST];
                sr.CategoryID = ConvertUtil.ConvertToInt(Request[CommonDataKey.SR_SUB_CATEGORY_LIST]);
                //Set urgency to Normal if it isn't selected
                if (!string.IsNullOrEmpty(Request[CommonDataKey.SR_URGENCY_LIST]))
                    sr.UrgencyID = ConvertUtil.ConvertToInt(Request[CommonDataKey.SR_URGENCY_LIST]);
                else
                    sr.UrgencyID = Constants.SR_URGENCY_NORMAL_ID;
                //Set RequestUser to SubmitUser if RequestUser field is empty
                if (string.IsNullOrEmpty(sr.RequestUser))
                    sr.RequestUser = sr.SubmitUser;
                //Check for valid SR
                msg = CheckValidSR(sr);
                if (msg == null)
                    //Check for uploaded file(s)
                    msg = CommonFunc.SR_CheckUploadedFiles(Request.Files);
                if (msg == null)
                {
                    sr.Files = objDb.Files;
                    string[] fileNameArr = new string[0];
                    int count = 0;
                    foreach (string fileName in Request.Files)
                    {
                        //Save File
                        HttpPostedFileBase file = Request.Files[fileName];
                        if (file.ContentLength > 0)
                        {
                            int startIndex = file.FileName.LastIndexOf('\\');
                            string newFileName = GetFilePrefix(count) + file.FileName.Substring(startIndex < 0 ? 0 : startIndex + 1);
                            //Update the Files field of sr
                            sr.Files += newFileName + Constants.SR_FILE_SEPARATE_SIGN;
                            file.SaveAs(Server.MapPath(Constants.SR_UPLOAD_PATH) + newFileName);
                            count++;
                        }
                    }
                    //Remove Deleted files
                    string deletedFiles = Request["hidRemovedFiles"];
                    if (!string.IsNullOrEmpty(deletedFiles))
                    {
                        CommonFunc.SR_RemoveFiles(deletedFiles);
                        sr.Files = CommonFunc.SR_RemoveDeletedFiles(sr.Files, deletedFiles);
                    }
                    string sParent = Request["ParentID"];
                    if (!string.IsNullOrEmpty(sParent))
                        sr.ParentID = ConvertUtil.ConvertToInt(sParent.Replace(Constants.SR_SERVICE_REQUEST_PREFIX, ""));
                    msg = srDao.Update(sr);
                }
                //Send mail if update successfully and the status or assign user has changed
                if (msg.MsgType != MessageType.Error && (sr.StatusID != preStatusId || sr.AssignUser != preAssignUser))
                    SendSRMail(sr.ID, Constants.SR_SEND_MAIL_DEFAULT);
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            //If has error -> redirect to Edit page, else -> redirect to Index page
            if (msg.MsgType == MessageType.Error)
                return RedirectToAction("Edit", new { @id = sr.ID });
            return Redirect(Request["hidCallerPage"] ?? Url.Action("Index"));
        }
        /// <summary>
        /// GET:Close SR
        /// </summary>
        /// <param name="id">SR id</param>
        /// <param name="r">r is null or empty -> redirect to detail page</param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Close(string id)
        {
            try
            {
                SR_ServiceRequest sr = srDao.GetById(ConvertUtil.ConvertToInt(id));
                if (sr == null)
                    return Content(string.Format(Resources.Message.E0005, "Service Request", "database"));
                if (sr.StatusID == Constants.SR_STATUS_CLOSED)
                    return Content(string.Format(Resources.Message.E0047, Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID, "has been closed"));
                return View();
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        }
        /// <summary>
        /// POST: Close SR
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.Update, ShowInPopup = true)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Close()
        {
            Message msg = null;
            try
            {
                int srId = ConvertUtil.ConvertToInt(Request["srId"]);
                SR_ServiceRequest sr = srDao.GetById(srId);
                if (sr == null)
                    msg = new Message(MessageConstants.E0005, MessageType.Error, "Service Request", "database");
                else if (sr.StatusID == Constants.SR_STATUS_CLOSED)
                    msg = new Message(MessageConstants.E0005, MessageType.Error, Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID, "has been closed");
                else
                {
                    SR_Comment comment = new SR_Comment()
                    {
                        Contents = Request["Comment"],
                        Poster = HttpContext.User.Identity.Name,
                        PostTime = DateTime.Now,
                        ServiceRequestID = srId
                    };
                    msg = srDao.ChangeStatus(srId, comment, Constants.SR_STATUS_CLOSED);
                    if (msg.MsgType != MessageType.Error)
                        SendSRMail(sr.ID, Constants.SR_SEND_MAIL_DEFAULT);
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return Redirect(Request["hidCallerPage"]);
        }
        /// <summary>
        /// Check manager
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckMangerExisted(string name)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Message msg = null;
            Employee manager = CommonFunc.GetEmployeeByUserName(name);
            
            if (manager == null || !manager.JobTitleLevel.JobTitle.IsManager)
            {
                msg = new Message(MessageConstants.E0030, MessageType.Error, "Manager " + name);
                result.Data = msg.ToString();
            }
            else
            {
                result.Data = true;
            }

            return result;
        }
        /// <summary>
        /// GET: Get approval
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.GetApproval, ShowInPopup = true)]
        public ActionResult GetApproval(string id)
        {
            try
            {
                SR_ServiceRequest sr = srDao.GetById(ConvertUtil.ConvertToInt(id));
                if (sr == null)
                    return Content(string.Format(Resources.Message.E0005, "Service Request", "database"));
                if (sr.StatusID != Constants.SR_STATUS_NEW)
                {
                    return Content(string.Format(Resources.Message.E0046,
                        "get approval for " + Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID, "the status is not 'New'"));
                }
                return View(sr);
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        }
        /// <summary>
        /// POST: Get Approval
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.GetApproval, ShowInPopup = true)]
        public ActionResult GetApproval()
        {
            Message msg = null;
            try
            {
                int srId = ConvertUtil.ConvertToInt(Request["srId"]);
                SR_ServiceRequest sr = srDao.GetById(srId);
                if (sr == null)
                    msg = new Message(MessageConstants.E0005, MessageType.Error, "Service Request", "database");
                else if (sr.StatusID != Constants.SR_STATUS_NEW)
                {
                    msg = new Message(MessageConstants.E0046, MessageType.Error,
                        "get approval for " + Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID, "the status is not 'New'");
                }
                else
                {
                    string managerName = Request["Manager"];
                    Employee manager = CommonFunc.GetEmployeeByUserName(managerName);
                    if (manager == null || !manager.JobTitleLevel.JobTitle.IsManager)
                    {
                        msg = new Message(MessageConstants.E0030, MessageType.Error, "Manager " + managerName);
                    }
                    else
                    {
                        SR_Comment comment = new SR_Comment()
                        {
                            Contents = Request["Comment"],
                            Poster = HttpContext.User.Identity.Name,
                            PostTime = DateTime.Now,
                            ServiceRequestID = srId
                        };
                        msg = srDao.GetApproval(srId, managerName, comment);
                        if (msg.MsgType != MessageType.Error)
                            SendSRMail(sr.ID, Constants.SR_SEND_MAIL_DEFAULT);
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return Redirect(Request["hidCallerPage"]);
        }
        /// <summary>
        /// GET: Add Solution
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.UpdateSolution, ShowInPopup = true)]
        public ActionResult AddSolution(string id)
        {
            try
            {
                SR_ServiceRequest sr = srDao.GetById(ConvertUtil.ConvertToInt(id));
                if (sr == null)
                    return Content(string.Format(Resources.Message.E0005, "Service Request", "database"));
                else if (sr.StatusID == Constants.SR_STATUS_CLOSED)
                    return Content(string.Format(Resources.Message.E0046, "edit solution", Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID + " is closed"));
                return View(sr);
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        }
        /// <summary>
        /// POST: Add solution
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.UpdateSolution, ShowInPopup = true)]
        public ActionResult AddSolution()
        {
            Message msg = null;
            int srId = 0;
            try
            {
                srId = ConvertUtil.ConvertToInt(Request["srId"]);
                SR_ServiceRequest sr = srDao.GetById(srId);
                if (sr == null)
                    msg = new Message(MessageConstants.E0005, MessageType.Error, "Service Request", "database");
                else if (sr.StatusID == Constants.SR_STATUS_CLOSED)
                    msg = new Message(MessageConstants.E0046, MessageType.Error, "edit solution",Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID + " is closed");
                else
                {
                    string solution = Request["Solution"];
                    msg = srDao.UpdateSolution(srId, solution);
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return RedirectToAction("Detail", new { @id = srId});
        }
        /// <summary>
        /// GET: add activity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.AddActivity, ShowInPopup = true)]
        public ActionResult AddActivity(string id)
        {
            try
            {
                SR_ServiceRequest sr = srDao.GetById(ConvertUtil.ConvertToInt(id));
                if (sr == null)
                    return Content(string.Format(Resources.Message.E0005, "Service Request", "database"));
                else if(sr.StatusID == Constants.SR_STATUS_CLOSED)
                    return Content(string.Format(Resources.Message.E0046, "add activity", Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID + " is closed"));
                return View();
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        }
        /// <summary>
        /// POST: Add activity
        /// </summary>
        /// <param name="srActivity"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.AddActivity, ShowInPopup = true)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddActivity(SR_Activity srActivity)
        {
            Message msg = null;
            int srId = 0;
            try
            {
                string dateFormat = "dd/MM/yyyy hh:mm tt";
                srId = ConvertUtil.ConvertToInt(Request["srId"]);
                SR_ServiceRequest sr = srDao.GetById(srId);
                if (sr == null)
                    msg = new Message(MessageConstants.E0005, MessageType.Error, "Service Request", "database");
                else if(CommonFunc.GetEmployeeByUserName(srActivity.UserName) == null)
                    msg = new Message(MessageConstants.E0030, MessageType.Error, "User" + srActivity.UserName);
                else if(sr.StatusID == Constants.SR_STATUS_CLOSED)
                    msg = new Message(MessageConstants.E0046, MessageType.Error, "add activity",Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID + " is closed");
                else
                {
                    srActivity.StartTime = DateTime.ParseExact(
                        srActivity.StartTime.ToString(Constants.DATETIME_FORMAT) + " " + Request[CommonDataKey.SR_HOURS_LIST_START_TIME], 
                        dateFormat, CultureInfo.InvariantCulture);
                    srActivity.EndTime = DateTime.ParseExact(
                        srActivity.StartTime.ToString(Constants.DATETIME_FORMAT) + " " + Request[CommonDataKey.SR_HOURS_LIST_END_TIME],
                        dateFormat, CultureInfo.InvariantCulture);
                    var activityList = activityDao.GetList(srId);
                    bool isOverlapped = false;
                    foreach (var act in activityList)
                    {
                        if(CommonFunc.IsOverlappedPeriod(act.StartTime, act.EndTime, srActivity.StartTime, srActivity.EndTime))
                        {
                            msg = new Message(MessageConstants.E0046, MessageType.Error, "add activity", "the time is overlapped");
                            isOverlapped = true;
                        }
                    }
                    if (!isOverlapped)
                    {
                        srActivity.ServiceRequestID = srId;
                        int iHour = ConvertUtil.ConvertToInt(Request["Total"].Split(Constants.SR_ACTIVITY_TOTAL_SEPARATE)[0]);
                        int iMinute = ConvertUtil.ConvertToInt(Request["Total"].Split(Constants.SR_ACTIVITY_TOTAL_SEPARATE)[1]);
                        srActivity.Total = 60 * iHour + iMinute;
                        msg = activityDao.Insert(srActivity);
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return RedirectToAction("Detail", new { @id = srId });
        }

        /// <summary>
        /// Delete Activity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequest, Rights = Permissions.DeleteActivity)]
        public ActionResult DeleteActivity(string id)
        {
            Message msg = null;
            string srId = "";
            try
            {
                int actId = ConvertUtil.ConvertToInt(id);
                SR_Activity activity = activityDao.GetById(actId);
                srId = activity.ServiceRequestID.ToString();
                if (activity == null)
                    msg = new Message(MessageConstants.E0005, MessageType.Error,"Activity", "database");
                else
                {
                    msg = activityDao.Delete(actId);
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            //return RedirectToAction("Detail", new { @id = srId });
            JsonResult result = new JsonResult();
            return Json(msg);
        }
        #endregion
        
        #region tan.tran SR dashboard

        public ActionResult DashBoard()
        {
            // Statistic
            List<sp_SR_GetServiceRequestResult> list = srDao.GetList(null, 0, 0, 0, null, null, null, null);
            if (list.Count > 0)
            {
                ViewData["SRStatistic_Count"] = list.Where(p => p.AssginName == principal.UserData.UserName && p.StatusID != Constants.SR_STATUS_CLOSED).Count().ToString();
                ViewData["SRStatistic_List_New"] = list.Where(q => q.StatusID == Constants.SR_STATUS_NEW).Count().ToString();
                ViewData["SRStatistic_List_Open"] = list.Where(q => q.StatusID == Constants.SR_STATUS_OPEN).Count().ToString();
            }
            ViewData["LoginStatistic_Title"] = GetUserloginStatisticTitle();
            ViewData["LoginStatistic_Value"] = GetUserloginStatisticValue(list);
            ViewData["LoginStatistic_CloseSR"] = GetUserloginStatisticSR(list.Where(q => q.StatusID == Constants.SR_STATUS_CLOSED).ToList());

            return View();
        }

        public JsonResult GetStatusStatic()
        {
            List<sp_SR_GetServiceRequestResult> list = srDao.GetList(null, 0, 0, 0, null, null, null, null);
            Dictionary<string, double> retVal = new Dictionary<string, double>();
            retVal.Add("New(" + CountSRByStatus(list, Constants.SR_STATUS_NEW) + ")", CountSRPercentageByStatus(list, Constants.SR_STATUS_NEW));
            retVal.Add("Closed(" + CountSRByStatus(list, Constants.SR_STATUS_CLOSED) + ")", CountSRPercentageByStatus(list, Constants.SR_STATUS_CLOSED));
            retVal.Add("Open(" + CountSRByStatus(list, Constants.SR_STATUS_OPEN) + ")", CountSRPercentageByStatus(list, Constants.SR_STATUS_OPEN));
            retVal.Add("To be Approved(" + CountSRByStatus(list, Constants.SR_STATUS_TO_BE_APPROVED) + ")", CountSRPercentageByStatus(list, Constants.SR_STATUS_TO_BE_APPROVED));
            retVal.Add("Verified Closed(" + CountSRByStatus(list, Constants.SR_STATUS_VERIFIED_CLOSED) + ")", CountSRPercentageByStatus(list, Constants.SR_STATUS_VERIFIED_CLOSED));
            retVal.Add("Pending(" + CountSRByStatus(list, Constants.SR_STATUS_PENDING) + ")", CountSRPercentageByStatus(list, Constants.SR_STATUS_PENDING));
            retVal.Add("Postponed(" + CountSRByStatus(list, Constants.SR_STATUS_POSTPONED) + ")", CountSRPercentageByStatus(list, Constants.SR_STATUS_POSTPONED));
            retVal.Add("Approved(" + CountSRByStatus(list, Constants.SR_STATUS_APPROVED) + ")", CountSRPercentageByStatus(list, Constants.SR_STATUS_APPROVED));
            retVal.Add("Rejected(" + CountSRByStatus(list, Constants.SR_STATUS_REJECTED)+")", CountSRPercentageByStatus(list, Constants.SR_STATUS_REJECTED));
            return Json(retVal.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private double CountSRPercentageByStatus(List<sp_SR_GetServiceRequestResult> list, int status)
        {
            double result;
            List<sp_SR_GetServiceRequestResult> filterList = list.Where(q => q.StatusID == status).ToList();
            result = Math.Round((double)((double)filterList.Count() / list.Count()) * 100);
            return result;
        }

        private int CountSRByStatus(List<sp_SR_GetServiceRequestResult> list, int status)
        {
            List<sp_SR_GetServiceRequestResult> filterList = list.Where(q => q.StatusID == status).ToList();
            return filterList.Count(); 
        }

        private string GetUserloginStatisticTitle()
        {
            string result = string.Empty;
            DateTime currDate = DateTime.Now;
            result += DateTimeUtil.GetDateInWeek(currDate.AddDays(-6)) + ",";
            result += DateTimeUtil.GetDateInWeek(currDate.AddDays(-5)) + ",";
            result += DateTimeUtil.GetDateInWeek(currDate.AddDays(-4)) + ",";
            result += DateTimeUtil.GetDateInWeek(currDate.AddDays(-3)) + ",";
            result += DateTimeUtil.GetDateInWeek(currDate.AddDays(-2)) + ",";
            result += DateTimeUtil.GetDateInWeek(currDate.AddDays(-1)) + ",";
            result += DateTimeUtil.GetDateInWeek(currDate);
            return result;
        }

        private string GetUserloginStatisticValue(List<sp_SR_GetServiceRequestResult> list)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string result = string.Empty;
            DateTime currDate = DateTime.Now;
            result += CountSRByDate(currDate.AddDays(-6), list) + ",";
            result += CountSRByDate(currDate.AddDays(-5), list) + ",";
            result += CountSRByDate(currDate.AddDays(-4), list) + ",";
            result += CountSRByDate(currDate.AddDays(-3), list) + ",";
            result += CountSRByDate(currDate.AddDays(-2), list) + ",";
            result += CountSRByDate(currDate.AddDays(-1), list) + ",";
            result += CountSRByDate(currDate, list);
            return result;
        }

        private string GetUserloginStatisticSR(List<sp_SR_GetServiceRequestResult> list)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string result = string.Empty;
            DateTime currDate = DateTime.Now;
            result += CountSRByCloseDate(currDate.AddDays(-6), list) + ",";
            result += CountSRByCloseDate(currDate.AddDays(-5), list) + ",";
            result += CountSRByCloseDate(currDate.AddDays(-4), list) + ",";
            result += CountSRByCloseDate(currDate.AddDays(-3), list) + ",";
            result += CountSRByCloseDate(currDate.AddDays(-2), list) + ",";
            result += CountSRByCloseDate(currDate.AddDays(-1), list) + ",";
            result += CountSRByCloseDate(currDate, list);
            return result;
        }

        private string CountSRByDate(DateTime date, List<sp_SR_GetServiceRequestResult> list)
        {
            return list.Where(q => q.CreateDate.ToString(Constants.DATETIME_FORMAT) == date.ToString(Constants.DATETIME_FORMAT)).ToList().Count.ToString();
        }

        private string CountSRByCloseDate(DateTime date, List<sp_SR_GetServiceRequestResult> list)
        {
            return list.Where(q => (q.CloseDate.HasValue?q.CloseDate.Value.ToString(Constants.DATETIME_FORMAT):"") == date.ToString(Constants.DATETIME_FORMAT)).ToList().Count.ToString();
        }

        #endregion

        public ActionResult Import(string totaltime)
        {
            return View(totaltime);
        }

        public ActionResult UpdateComment(string totaltime)
        {
            return View(totaltime);
        }

        [HttpPost]
        public ActionResult UpdateComment(FormCollection form)
        {
            DateTime date1 = DateTime.Now;
            List<sp_SR_GetFilesOfService_reqResult> list = srDao.GetListFilesSR().ToList<sp_SR_GetFilesOfService_reqResult>();

            List<int> lstId = list.Select(p => p.Id).Distinct().ToList();
            long notSuccess = 0;
            long success = 0;
            if (list != null)
            {
                foreach (int id in lstId)
                {
                    List<sp_SR_GetFilesOfService_reqResult> item = list.Where(p => p.Id == id).ToList();
                    if (item != null)
                    {
                        string str = string.Empty;
                        foreach (sp_SR_GetFilesOfService_reqResult s in item)
                        {
                            str = s.file_name + Constants.FILE_STRING_PREFIX;
                        }
                        SR_Comment sCom = new SR_Comment();
                        sCom = commentDao.GetCommentByServiceRequestID(id);
                        sCom.Files = str;
                        //listCom.Add(sCom);
                        //Message msg = commentDao.InsertList(listCom);
                        Message msg = commentDao.Update(sCom);

                        if (msg.MsgType == MessageType.Error)
                            notSuccess++;
                        else
                            success++;
                    }
                }
            }            
        

            DateTime date2 = DateTime.Now;
            TimeSpan totaltime = date2 - date1;
            ViewData["success"] = success.ToString();
            ViewData["notsuccess"] = notSuccess.ToString();
            //string total = "Total {0} hours, {1} minutes, {2} seconds, {3} miliseconds";
            //total = string.Format(total, totaltime.TotalHours, totaltime.TotalMinutes, totaltime.TotalSeconds, totaltime.TotalMilliseconds);

            //ShowMessage(msg);
            return View("ResultImport");
        }

        [HttpPost]
        public ActionResult Import(FormCollection form)
        {
            DateTime date1 = DateTime.Now;
            List<sp_SR_GetNotesOfService_reqResult> list = srDao.GetListServiceReq().ToList<sp_SR_GetNotesOfService_reqResult>();
            long notSuccess = 0;
            long success = 0;
            if (list != null)
            {
                foreach (sp_SR_GetNotesOfService_reqResult item in list)
                {
                    if (!string.IsNullOrEmpty(item.notes))
                    {
                        char[] delimiters = new char[] { '\r', '\n' ,'\n'};
                        string[] str = item.notes.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                        int id = srDao.GetIdFromSysAid(item.Id);
                        SR_ServiceRequest sr = srDao.GetById(id);
                        if (sr != null)
                        {
                            if (str.Length > 0)
                            {
                                //List<SR_Comment> listCom = new List<SR_Comment>();
                                foreach (string s in str)
                                {
                                    if (!string.IsNullOrEmpty(s))
                                    {
                                        SR_Comment sCom = new SR_Comment();
                                        sCom.ServiceRequestID = id;
                                        sCom.PostTime = DateTime.Now;
                                        sCom.Poster = string.Empty;
                                        sCom.Contents = s;
                                        //listCom.Add(sCom);
                                        //Message msg = commentDao.InsertList(listCom);
                                        Message msg = commentDao.Insert(sCom);
                                        
                                        if (msg.MsgType == MessageType.Error)
                                            notSuccess++;
                                        else
                                            success++;
                                    }
                                }

                            }
                            
                        }
                    }
                }
            }

            DateTime date2 = DateTime.Now;
            TimeSpan totaltime = date2 - date1;
            ViewData["success"] = success.ToString();
            ViewData["notsuccess"] = notSuccess.ToString();
            //string total = "Total {0} hours, {1} minutes, {2} seconds, {3} miliseconds";
            //total = string.Format(total, totaltime.TotalHours, totaltime.TotalMinutes, totaltime.TotalSeconds, totaltime.TotalMilliseconds);
            
            //ShowMessage(msg);
            return View("ResultImport");
        }
    }
}
