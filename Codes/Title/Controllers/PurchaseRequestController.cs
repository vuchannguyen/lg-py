using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using CRM.Library.Attributes;
using CRM.Library.Common;
using CRM.Library.Exceptions;
using CRM.Library.Utils;
using CRM.Models;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Routing;
using System.Text;
using Microsoft.Win32;
using System.Net;
using System.Globalization;

namespace CRM.Controllers
{
    /// <summary>
    /// Purchase request controller
    /// </summary>
    public class PurchaseRequestController : BaseController
    {
        #region variables

        private DepartmentDao deptDao = new DepartmentDao();
        private ResolutionDao resDao = new ResolutionDao();
        private StatusDao staDao = new StatusDao();
        private PurchaseRequestDao dao = new PurchaseRequestDao();
        private JRAdminDao jrAdminDao = new JRAdminDao();
        private WFStatusDao wfStatusdao = new WFStatusDao();
        private CommonDao cmDao = new CommonDao();
        private UserAdminDao userAdminDao = new UserAdminDao();
        private RoleDao roleDao = new RoleDao();
        private PRCommentDao prCommentDao = new PRCommentDao();
        
        
        #endregion

        #region Methods

        #region son.vuong
        /// <summary>
        /// List
        /// </summary>
        /// <returns>ActionResult</returns>
        /// 
        //[CrmAuthorizeAttribute(Module = Modules.PurchaseRequest, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            int currentRole = 0;
            List<WFRole> list = cmDao.GetRoleList(principal.UserData.UserID, Constants.WORK_FLOW_PURCHASE_REQUEST);
            if (list.Count == 0)
                return RedirectToAction("NotPermission", "Common");
            //handle case user link to this page from email
            string roleParam = Request["role"];

            try
            {
                if (!string.IsNullOrEmpty(roleParam))
                {
                    TempData[CommonDataKey.PR_ROLE_ID] = roleParam;
                }

                if (TempData[CommonDataKey.PR_ROLE_ID] != null)
                {
                    principal.UserData.Role = int.Parse(TempData[CommonDataKey.PR_ROLE_ID].ToString());
                }
                //End
                if (list.Count == 0) //if does
                {
                    ViewData[CommonDataKey.PR_ROLE] = null;

                }
                else if (principal.UserData.Role != 0 && list.Count >= 1) //Check for delete case in JRAdmin
                {
                    WFRole exist = list.Where(c => c.ID == principal.UserData.Role).FirstOrDefault<WFRole>();
                    if (exist == null) //if does not exist, choose the first role in list
                    {
                        if (list.Count > 1)
                        {
                            //Display login role dropdownlist for choosing
                            ViewData[CommonDataKey.PR_ROLE] = new SelectList(list, "ID", "Name", "");
                        }
                        AssignRole(list[0].ID);
                        currentRole = list[0].ID;
                    }
                    else
                    {
                        if (list.Count > 1)
                        {
                            //Display login role dropdownlist for choosing
                            ViewData[CommonDataKey.PR_ROLE] = new SelectList(list, "ID", "Name", principal.UserData.Role);
                        }

                        if (TempData[CommonDataKey.PR_ROLE_ID] != null)
                        {
                            AssignRole(principal.UserData.Role);
                        }

                        currentRole = principal.UserData.Role;
                    }
                }
                else
                {
                    if (principal.UserData.Role == 0 && list.Count >= 1)
                    {
                        if (list.Count > 1)
                        {
                            //Display login role dropdownlist for choosing
                            ViewData[CommonDataKey.PR_ROLE] = new SelectList(list, "ID", "Name", "");
                        }
                        //assign the first role in list (default)
                        AssignRole(list[0].ID);
                        currentRole = list[0].ID;
                    }
                }
            }
            catch (Exception)
            {

                Message msg = new Message(MessageConstants.E0007, MessageType.Error);
                ShowMessage(msg);
                return View(currentRole);
            }

            Hashtable hashData = Session[SessionKey.PURCHASE_FILTER] == null ? new Hashtable() : (Hashtable)Session[SessionKey.PURCHASE_FILTER];

            ViewData[Constants.PURCHASE_REQUEST_KEYWORD] = hashData[Constants.PURCHASE_REQUEST_KEYWORD] == null ? Constants.PURCHASE_REQUEST : hashData[Constants.PURCHASE_REQUEST_KEYWORD];
            ViewData[Constants.PURCHASE_REQUEST_DEPARTMENT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", hashData[Constants.PURCHASE_REQUEST_DEPARTMENT] == null ?
                Constants.FIRST_ITEM_DEPARTMENT : hashData[Constants.PURCHASE_REQUEST_DEPARTMENT].ToString());
            if (hashData[Constants.PURCHASE_REQUEST_DEPARTMENT] == null)
            {
                ViewData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] == null ?
                Constants.FIRST_ITEM_SUB_DEPARTMENT : hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT].ToString());
            }
            else if (!String.IsNullOrEmpty(hashData[Constants.PURCHASE_REQUEST_DEPARTMENT].ToString()))
            {
                ViewData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] = new SelectList(deptDao.GetSubListByParent(ConvertUtil.ConvertToInt(hashData[Constants.PURCHASE_REQUEST_DEPARTMENT])), "DepartmentId", "DepartmentName", hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] == null ?
                    Constants.FIRST_ITEM_SUB_DEPARTMENT : hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT].ToString());
            }
            else
            {
                ViewData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] == null ?
                Constants.FIRST_ITEM_SUB_DEPARTMENT : hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT].ToString());
            }
            ViewData[Constants.PURCHASE_REQUEST_REQUESTOR_ID] = new SelectList(jrAdminDao.GetUserInPurchaseRequest(false), "UserAdminId", "UserName", hashData[Constants.PURCHASE_REQUEST_REQUESTOR_ID] == null ?
                Constants.PURCHASE_REQUEST_REQUESTOR_FIRST_ITEM : hashData[Constants.PURCHASE_REQUEST_REQUESTOR_ID].ToString());
            ViewData[Constants.PURCHASE_REQUEST_ASSIGN_ID] = new SelectList(jrAdminDao.GetUserInPurchaseRequest(true), "UserAdminId", "UserName", hashData[Constants.PURCHASE_REQUEST_ASSIGN_ID] == null ?
                Constants.PURCHASE_REQUEST_ASSIGN_FIRST_ITEM : hashData[Constants.PURCHASE_REQUEST_ASSIGN_ID].ToString());

            //ViewData[Constants.PURCHASE_REQUEST_STATUS_ID] = new SelectList(wfStatusdao.GetList(), "ID", "Name", hashData[Constants.PURCHASE_REQUEST_STATUS_ID] == null ?
            //    Constants.STATUS_OPEN.ToString() : hashData[Constants.PURCHASE_REQUEST_STATUS_ID].ToString());

            ViewData[Constants.PURCHASE_REQUEST_RESOLUTION_ID] = new SelectList(GetListResolutionName(), "Text", "Value", hashData[Constants.PURCHASE_REQUEST_RESOLUTION_ID] == null ?
                Constants.FIRST_ITEM_RESOLUTION : hashData[Constants.PURCHASE_REQUEST_RESOLUTION_ID].ToString());

            ViewData[Constants.PURCHASE_REQUEST_COLUMN] = hashData[Constants.PURCHASE_REQUEST_COLUMN] == null ? "ID" : hashData[Constants.PURCHASE_REQUEST_COLUMN];
            ViewData[Constants.PURCHASE_REQUEST_ORDER] = hashData[Constants.PURCHASE_REQUEST_ORDER] == null ? "desc" : hashData[Constants.PURCHASE_REQUEST_ORDER];
            ViewData[Constants.PURCHASE_REQUEST_PAGE_INDEX] = hashData[Constants.PURCHASE_REQUEST_PAGE_INDEX] == null ? "1" : hashData[Constants.PURCHASE_REQUEST_PAGE_INDEX].ToString();
            ViewData[Constants.PURCHASE_REQUEST_ROW_COUNT] = hashData[Constants.PURCHASE_REQUEST_ROW_COUNT] == null ? "20" : hashData[Constants.PURCHASE_REQUEST_ROW_COUNT].ToString();

            bool isViewAll = HasViewAllPR();
            ViewData[CommonDataKey.PR_IS_VIEW_ALL] = isViewAll;
            ViewData[CommonDataKey.PR_USER_LOGIN_ID] = principal.UserData.UserID;
            ViewData[CommonDataKey.PR_USER_LOGIN_NAME] = principal.UserData.UserName;
            ViewData[CommonDataKey.PR_LOGIN_ROLE] = principal.UserData.Role;
            return View(currentRole);
        }

        private List<ListItem> GetListResolutionName()
        {
            List<string> list = resDao.GetListResolutionName(Constants.WORK_FLOW_PURCHASE_REQUEST);
            List<ListItem> items = new List<ListItem>();
            foreach (string item in list)
            {
                items.Add(new ListItem
                {
                    Text = item,
                    Value = item
                });
            }

            return items;
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.PURCHASE_FILTER);
            return RedirectToAction("Index");
        }
        public ActionResult RefreshReport()
        {
            Session.Remove(SessionKey.PURCHASE_FILTER_REPORT);
            return RedirectToAction("Report");
        }
        public List<sp_GetPurchaseRequestResult> GetListPRForNavigation()
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string textSearch = null;
            int department = 0;
            int sub_Department = 0;
            int requestorId = 0;
            string resolutionId = null;
            int assignID = 0;
            string column = "ID";
            string order = "desc";
            List<sp_GetPurchaseRequestResult> list = null;
            if (Session[SessionKey.PURCHASE_FILTER] != null)
            {
                Hashtable hashData = (Hashtable)Session[SessionKey.PURCHASE_FILTER];
                textSearch = (string)hashData[Constants.PURCHASE_REQUEST_KEYWORD];
                textSearch = textSearch.Trim();
                if (textSearch == Constants.PURCHASE_REQUEST)
                {
                    textSearch = "";
                }
                else
                {
                    if (textSearch.ToLower().StartsWith(Constants.PR_REQUEST_PREFIX.ToLower()))
                    {
                        textSearch = textSearch.Substring(3);
                    }
                    
                }

                if (textSearch == Constants.FULLNAME_OR_USERID)
                {
                    textSearch = string.Empty;
                }

                department = !string.IsNullOrEmpty((string)hashData[Constants.PURCHASE_REQUEST_DEPARTMENT]) ? int.Parse((string)hashData[Constants.PURCHASE_REQUEST_DEPARTMENT]) : 0;
                sub_Department = !string.IsNullOrEmpty((string)hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT]) ? int.Parse((string)hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT]) : 0;
                requestorId = !string.IsNullOrEmpty((string)hashData[Constants.PURCHASE_REQUEST_REQUESTOR_ID]) ? int.Parse((string)hashData[Constants.PURCHASE_REQUEST_REQUESTOR_ID]) : 0;
                resolutionId = !string.IsNullOrEmpty((string)hashData[Constants.PURCHASE_REQUEST_RESOLUTION_ID]) ? (string)hashData[Constants.PURCHASE_REQUEST_RESOLUTION_ID] : null;
                assignID = !string.IsNullOrEmpty((string)hashData[Constants.PURCHASE_REQUEST_ASSIGN_ID]) ? int.Parse((string)hashData[Constants.PURCHASE_REQUEST_ASSIGN_ID]) : 0;

                column = !string.IsNullOrEmpty((string)hashData[Constants.PURCHASE_REQUEST_COLUMN]) ? (string)hashData[Constants.PURCHASE_REQUEST_COLUMN] : "ID";
                order = !string.IsNullOrEmpty((string)hashData[Constants.PURCHASE_REQUEST_ORDER]) ? (string)hashData[Constants.PURCHASE_REQUEST_ORDER] : "desc";

                list = dao.GetList(textSearch, department, sub_Department, requestorId, assignID,
                resolutionId, principal.UserData.UserID, principal.UserData.Role, 0, false);

            }
            else
            {
                list = dao.GetList(textSearch, department, sub_Department, requestorId, assignID,
                null, principal.UserData.UserID, principal.UserData.Role, Constants.STATUS_OPEN, false);
            }

            return list;
        }

        /// <summary>
        /// Do action change role
        /// </summary>
        /// <param name="roleId">string</param>
        /// <returns>ActionResult</returns>
        public ActionResult ChangeRole(string roleId)
        {
            if (!string.IsNullOrEmpty(roleId))
            {
                int role = int.Parse(roleId);
                AssignRole(role);
            }
            string controllerName = RouteData.Values["controller"].ToString();
            return Redirect(Request.UrlReferrer == null ?
                "/" + controllerName : Request.UrlReferrer.AbsolutePath);
        }

        /// <summary>
        /// Read in cookie and re-assign choosing role into cookie
        /// </summary>
        /// <param name="role">role id</param>
        private void AssignRole(int role)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            var timeOut = 400;
            UserData ud = new UserData(principal.UserData.UserID, principal.UserData.UserName, role);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, principal.UserData.UserName, DateTime.Now, DateTime.Now.AddMinutes(timeOut), false, ud.ToXml());
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(Constants.COOKIE_CRM, encryptedTicket);

            Hashtable hashData = Session[SessionKey.PURCHASE_FILTER] == null ? new Hashtable() :
                (Hashtable)Session[SessionKey.PURCHASE_FILTER];
            hashData[Constants.PURCHASE_REQUEST_PAGE_INDEX] = "1";
            Response.Cookies.Add(authCookie);
        }

        public string GetMimeType(FileInfo fileInfo)
        {
            string mimeType = "application/unknown";

            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(
                fileInfo.Extension.ToLower()
                );

            if (regKey != null)
            {
                object contentType = regKey.GetValue("Content Type");

                if (contentType != null)
                    mimeType = contentType.ToString();
            }

            return mimeType;
        }
        private string GetContentType(string fileName)
        {

            string contentType = "application/octetstream";

            string ext = System.IO.Path.GetExtension(fileName).ToLower();

            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);

            if (registryKey != null && registryKey.GetValue("Content Type") != null)

                contentType = registryKey.GetValue("Content Type").ToString();

            return contentType;

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

                    ////HttpPostedFile myPostedFile = fileUpload.PostedFile;
                    //if (hpf != null && hpf.ContentLength > 0)
                    //{
                    //    FileInfo finfo = new FileInfo(hpf.FileName);
                    //    FileStream fs = new FileStream("D:\\test\\test.xlsx", FileMode.Open, FileAccess.Read);
                    //    BinaryReader br = new BinaryReader(fs);
                    //    string[] buff = br.ReadBytes(512).Select(p=>p.ToString("X")).ToArray();

                    //    string fileExtension = string.Join(" ", buff);
                    //    //if (fileExtension != ".gif" && fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                    //    //{
                    //    //    invalidExtension = true;
                    //    //    errorExtension += fileExtension + ",";
                    //    //    break;
                    //    //}
                    //    invalidExtension = true;
                    //    errorExtension += fileExtension + ",";
                    //    break;
                    //}
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
        /// Add purchase comment
        /// </summary>
        /// <param name="obj">PurchaseComment</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddComment(PurchaseComment obj)
        {
            Message msg = null;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            obj.PostTime = DateTime.Now;
            obj.Poster = principal.UserData.UserName;
            string serverPath = Server.MapPath(Constants.PERFORMANCE_REVIEW_PATH);
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
                msg = prCommentDao.Insert(obj);
            }
            ShowMessage(msg);

            return RedirectToAction("Detail/" + int.Parse(obj.RequestID.ToString()));
        }

        /// <summary>
        /// Export to Excel
        /// </summary>
        /// <param name="text">keyword</param>
        /// <param name="department">string</param>
        /// <param name="subdepartment">string</param>
        /// <param name="requestorId">string</param>
        /// <param name="statusId">string</param>
        /// <returns>ActionResult</returns>
        public ActionResult ExportToExcel(string purchaseId, string department, string subdepartment, string requestorId, string assignId, string statusId,string fromdate="",string todate="",string page="")
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;

            //List<sp_GetPurchaseRequestResult> list = TempData[CommonDataKey.PR_LIST] != null ? (List<sp_GetPurchaseRequestResult>)TempData[CommonDataKey.PR_LIST] : dao.GetList("", 0, 0, 0, 0, 0, principal.UserData.Role.ToString());
            string sessionKey=SessionKey.PURCHASE_FILTER;
            string Title = Constants.PR_TILE_EXPORT_EXCEL;
            
            string FileName = Constants.PR_EXPORT_EXCEL_NAME;

            string[] ColumnList = new string[] { "ID:PR", "RequestDate:Date", "ExpectedDate:Date", "RequestorName:text-left", "Department:text-left", "SubDepartmentName:text-left", "Justification:text-left", 
                "ResolutionName:text-left", "StatusName:text-left", "AssignName:text-left", "VendorName:text-left", "VendorPhone:text-left", "VendorEmail:text-left", "VendorAddress:text-left", "Total:currency", "PurchaseAppoval:text-left", "PaymentAppoval:text-left"};
            string[] HeaderExcel = new string[] { "Request #", "Request Date", "Expected Date", "Requestor", "Department", "Sub Department", "Justification",
                "Resolution", "Status", "Forwarded To", "Vendor/Supplier", "Vendor/Supplier Phone", "Vendor/Supplier Email", "Vendor/Supplier Address", "Total", 
                "PurchaseApproval#", "PaymentApproval#"};
            
            //using for report page
            if (!String.IsNullOrEmpty(page))
            {
                sessionKey = SessionKey.PURCHASE_FILTER_REPORT;
                Title = Constants.PR_REPORT_TILE_EXPORT_EXCEL;
                FileName = Constants.PR_REPORT_EXPORT_EXCEL_NAME;

                ColumnList = new string[] { "ID:PR", "RequestDate:Date", "ExpectedDate:Date", "RequestorName:text-left", "Department:text-left", "SubDepartmentName:text-left", "Justification:text-left", 
                "ResolutionName:text-left", "StatusName:text-left", "AssignName:text-left", "Total:currency"};
                
                HeaderExcel = new string[] { "Request #", "Request Date", "Expected Date", "Requestor", "Department", "Sub Department", "Justification",
                "Resolution", "Status", "Forwarded To","Total"};
            }               
            Hashtable hashData = (Hashtable)Session[sessionKey];
            
            bool isViewAll = HasViewAllPR();
            if (principal.UserData.Role != Constants.PR_REQUESTOR_ID)
                isViewAll = true;
            // If user have authenticated viewAllPR then get requestorId, Otherwise get principal. userlogin
            requestorId = isViewAll ? requestorId : principal.UserData.UserID.ToString();

            purchaseId = purchaseId.Trim();

            if (purchaseId == Constants.PURCHASE_REQUEST)
            {
                purchaseId = "";
            }
            else
            {
                if (purchaseId.ToLower().StartsWith(Constants.PR_REQUEST_PREFIX.ToLower()))
                {
                    purchaseId = purchaseId.Substring(3);
                }
                //string containsJR = Constants.PR_REQUEST_PREFIX;
                //if (containsJR.Contains(purchaseId))
                //{
                //    purchaseId = string.Empty;
                //}
                //else
                //{
                //    if (purchaseId.Length >= 4)
                //    {
                //        purchaseId = purchaseId.Substring(3);
                //    }
                //}
            }
            List<sp_GetPurchaseRequestResult> list = GetListByFilter(purchaseId, department, subdepartment, requestorId, assignId, statusId,
                principal.UserData.UserID, principal.UserData.Role,fromdate,todate);
            string column = !string.IsNullOrEmpty((string)hashData[Constants.PURCHASE_REQUEST_COLUMN]) ?
                (string)hashData[Constants.PURCHASE_REQUEST_COLUMN] : "ID";
            string order = !string.IsNullOrEmpty((string)hashData[Constants.PURCHASE_REQUEST_ORDER]) ?
                (string)hashData[Constants.PURCHASE_REQUEST_ORDER] : "desc";

            var finalList = dao.Sort(list, column, order);

            ExportExcel exp = new ExportExcel();
            exp.Title = Title;
            exp.FileName = FileName;
            exp.ColumnList = ColumnList;
            exp.HeaderExcel = HeaderExcel;
            exp.List = list;
            exp.IsRenderNo = true;
            exp.Execute();

            return View();
        }


        /// <summary>
        /// Delete list of purchase request
        /// </summary>
        /// <param name="id">list of ids</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.PurchaseRequest, Rights = Permissions.Delete)]
        public ActionResult DeleteList(string id)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                if (principal.IsInRole(Constants.PR_REQUESTOR_ID.ToString()))
                {
                    msg = dao.DeleteList(id, principal.UserData.UserName);
                }
                else
                {
                    msg = new Message(MessageConstants.E0002, MessageType.Error);
                }
                //ShowMessage(msg);
            }
            catch (ForbiddenExceptionOnCurrentPage)
            {
                //ShowMessage(msg);
                msg = new Message(MessageConstants.E0002, MessageType.Error);
            }
            //return RedirectToAction("/Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        /// <summary>
        /// Check view permission
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="loginId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        private bool HasViewPermission(sp_GetPurchaseRequestResult sp, int loginId, string loginName)
        {
            if (sp.AssignID == loginId ||
                sp.Requestor == loginId ||
                (sp.CCList != null ? sp.CCList.Split(';').Contains(loginName) : false) ||
                sp.InvolveID.Split(',').Contains(loginId.ToString()))

                return true;
            else
                return false;
        }

        public ActionResult Navigation(string name, string id, string page)
        {

            List<sp_GetPurchaseRequestResult> listPR = GetListPRForNavigation();

            string testID = string.Empty;
            int index = 0;
            string url = string.Empty;
            switch (name)
            {
                case "First":
                    testID = listPR[0].ID.ToString();
                    break;
                case "Prev":
                    index = listPR.IndexOf(listPR.Where(p => p.ID.ToString() == id).FirstOrDefault<sp_GetPurchaseRequestResult>());
                    if (index != 0)
                    {
                        testID = listPR[index - 1].ID.ToString();
                    }
                    else
                    {
                        testID = id;
                    }
                    break;
                case "Next":
                    index = listPR.IndexOf(listPR.Where(p => p.ID.ToString() == id).FirstOrDefault<sp_GetPurchaseRequestResult>());
                    if (index != listPR.Count - 1)
                    {
                        testID = listPR[index + 1].ID.ToString();
                    }
                    else
                    {
                        testID = id;
                    }
                    break;
                case "Last":
                    testID = listPR[listPR.Count - 1].ID.ToString();
                    break;
            }
            switch (page)
            {
                case "Detail":
                    url = "Detail/" + testID;
                    break;
            }
            return RedirectToAction(url);
        }

        /// <summary>
        /// Detail
        /// </summary>
        /// <param name="id">purchase request id</param>
        /// <returns>ActionResult</returns>
        public ActionResult Detail(int id)
        {
            sp_GetPurchaseRequestResult pr = null;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;

            pr = dao.GetPurchaseRequestByID(id.ToString());
            //For navigation
            List<sp_GetPurchaseRequestResult> listPR = GetListPRForNavigation();
            ViewData["ListPR"] = listPR;
            //Get comment of purchase request                        
            ViewData[CommonDataKey.PR_COMMENT] = prCommentDao.GetList(id);
            List<WFRole> list = cmDao.GetRoleList(principal.UserData.UserID, Constants.WORK_FLOW_PURCHASE_REQUEST);
            if (list.Count > 1)
                ViewData[CommonDataKey.PR_ROLE] = new SelectList(list, "ID", "Name", principal.UserData.Role);
            if (prCommentDao.GetList(id).Count > 0)
            {
                ViewData[CommonDataKey.PR_COMMENT_COUNT] = prCommentDao.GetList(id).Count;
            }
            if (pr != null)
            {
                //if (!HasViewAllPR() &&  !HasViewPermission(pr, principal.UserData.UserID, principal.UserData.UserName))
                if (!dao.HasViewPermision(pr.ID, principal.UserData.UserName, principal.UserData.Role))
                {
                    ViewData[CommonDataKey.IS_ACCESSIBLE] = false;
                    return View(pr);
                }
                ViewData[CommonDataKey.IS_ACCESSIBLE] = true;
                try
                {
                    List<PurchaseItem> itemList;
                    itemList = dao.GetPurchaseRequestItems(id);
                    List<PurchaseInvoice> invoiceList = dao.GetPurchaseInvoice(id);
                    ViewData[CommonDataKey.LIST_INVOICE] = invoiceList;
                    ViewData[CommonDataKey.LIST_PURCHASE_ITEM] = itemList;

                    string flow = string.Empty;
                    string[] arrIds = pr.InvolveID.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string[] arrRoles = pr.InvolveRole.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string[] arrResolution = pr.InvolveResolution.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string[] arrDate = pr.InvolveDate.Split(Constants.SEPARATE_INVOLVE_CHAR);

                    int maxLength = new int[] { arrIds.Length, arrDate.Length, arrResolution.Length, arrRoles.Length }.Min();

                    for (int i = 0; i < maxLength - 1; i++)//last item is empty
                    {
                        //check duplicate person on user name and role.
                        UserAdmin userAdmin = userAdminDao.GetById(int.Parse(arrIds[i]));
                        WFRole role = roleDao.GetByID(int.Parse(arrRoles[i]));
                        flow += userAdmin.UserName + " (" + role.Name + ");" + arrResolution[i] + ";" + arrDate[i] + ",";
                    }
                    ViewData[CommonDataKey.PR_WORK_FLOW] = flow;
                }
                catch (Exception)
                {
                    Message msg = new Message(MessageConstants.E0007, MessageType.Error);
                    ShowMessage(msg);
                    return RedirectToAction("Index");
                }
                ViewData[CommonDataKey.PURCHASE_REQUEST_ACTIONS] =
                    SetAction(pr.AssignRole.Value, pr.AssignID.Value, pr.WFStatusID, pr.WFResolutionID, pr.ID, true);
                return View(pr);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Setup approval 
        /// </summary>
        /// <param name="forms">FormCollection</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult SetupApproval(FormCollection forms)
        {
            JsonResult result = new JsonResult();
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = null;
            int prId = int.Parse(forms["ReqID"]);

            #region Check permission
            bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                    forms["ReqID"], Constants.ActionType.Update, principal.UserData.Role);
            if (!check)
                return RedirectToAction("NotPermission", "Common");
            #endregion

            try
            {
                int count = int.Parse(forms["hidValue"]);

                List<PurchaseRequestApproval> listApproval = new List<PurchaseRequestApproval>();
                PurchaseRequestApprovalDao appDao = new PurchaseRequestApprovalDao();
                PurchaseRequest purchase = dao.GetPurchaseRequestByID(prId);
                if (purchase.UpdateDate.ToString() == forms["UpdateDate"])
                {
                    // Delete old purchase request approval
                    appDao.DeleteByPurchaseId(prId);
                    string assignId = "";
                    string assignRole = "";
                    for (int i = 1; i <= count; i++)
                    {
                        PurchaseRequestApproval approval = new PurchaseRequestApproval();
                        approval.RequestID = prId;
                        approval.ApproverId = int.Parse(forms["Assign" + i.ToString()]);
                        approval.ApproverGroup = int.Parse(forms["Approval" + i.ToString()]);
                        approval.IsImmediate = bool.Parse(forms["isImmediate" + i.ToString()].Split(Convert.ToChar(","))[0]);
                        listApproval.Add(approval);
                        //msg = appDao.Insert(approval);
                    }
                    PurchaseRequestApproval oldCorp = new PurchaseRequestApproval();
                    oldCorp.RequestID = prId;
                    oldCorp.ApproverId = principal.UserData.UserID;
                    oldCorp.ApproverGroup = principal.UserData.Role;
                    listApproval.Add(oldCorp);
                    //msg = appDao.Insert(oldCorp);

                    string forwardto = forms["Forward"];

                    string[] arr = null;
                    if (forwardto != null & forwardto != "")
                        arr = forwardto.Split(Constants.SEPARATE_USER_ADMIN_ID);
                    if (count > 0)
                    {
                        assignId = forms["Assign1"];
                        assignRole = forms["Approval1"];
                    }
                    else
                    {
                        if (arr != null)
                        {
                            assignId = arr[0];
                            assignRole = arr[1];
                        }
                    }
                    if (arr != null)
                    {
                        PurchaseRequestApproval approval = new PurchaseRequestApproval();
                        approval.RequestID = prId;
                        approval.ApproverId = int.Parse(arr[0]);
                        approval.ApproverGroup = int.Parse(arr[1]);

                        listApproval.Add(approval);
                        //appDao.Insert(approval);
                    }
                    if (msg == null)
                    {
                        List<sp_GetResolutionByRoleResult> resolutionList = dao.GetResolutionByRole(int.Parse(assignRole));
                        resolutionList = resolutionList.Where(p => p.WFResolutionID != Constants.PR_RESOLUTION_TO_BE_APPROVAL_APPROVE &&
                            p.WFResolutionID != Constants.PR_RESOLUTION_TO_BE_APPROVAL_REJECT).ToList<sp_GetResolutionByRoleResult>();
                        if (count == 0)
                        {
                            purchase.WFResolutionID = Constants.PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING;
                        }
                        else
                        {
                            purchase.WFResolutionID = resolutionList[0].WFResolutionID;
                        }

                        purchase.InvolveResolution += Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                        purchase.AssignID = int.Parse(assignId);
                        purchase.AssignRole = int.Parse(assignRole);
                        purchase.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                        purchase.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                        purchase.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                        if (purchase.BillableToClient)
                        {
                            purchase.USAccounting = int.Parse(forms["USAccounting"]);
                        }
                        purchase.UpdatedBy = principal.UserData.UserName;
                        purchase.UpdateDate = DateTime.Parse(forms["UpdateDate"]);
                        msg = dao.UpdateForSetupApproval(purchase, listApproval, true);
                        if (msg.MsgType == MessageType.Info)
                        {
                            SendPRMail(purchase.ID);
                        }
                    }
                }
                else
                {
                    msg = new Message(MessageConstants.E0025, MessageType.Error, "Purchase Request '" + Constants.PR_REQUEST_PREFIX + prId + "'");
                }
                ShowMessage(msg);
                result.Data = msg;
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                ShowMessage(msg);
            }
            return GotoCallerPage();
        }

        /// <summary>
        /// Setup approval
        /// </summary>
        /// <param name="id">id of purchase request</param>
        /// <returns>ActionResult</returns>
        public ActionResult SetupApproval(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region Check permission
            bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                    id, Constants.ActionType.Update, principal.UserData.Role);
            if (!check)
                return RedirectToAction("NotPermission", "Common");
            #endregion
            sp_GetPurchaseRequestResult pr = dao.GetPurchaseRequestByID(id.ToString());
            List<sp_GetListAssignByResolutionIdResult> listAssign = dao.GetListAssign(Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING, Constants.WORK_FLOW_PURCHASE_REQUEST);

            List<WFRole> list = cmDao.GetRoleListForApproval(Constants.WORK_FLOW_PURCHASE_REQUEST);
            list = list.Where(q => q.ID != Constants.PR_US_ACCOUNTING).ToList();
            List<sp_GetListApprovalAssignResult> appList = dao.GetListApprovalAssign(int.Parse(id));
            appList = appList.Where(p => p.ApproverGroup != Constants.PR_SR_MANAGER_ID).ToList<sp_GetListApprovalAssignResult>();
            ViewData[CommonDataKey.LIST_APPROVAL] = appList;
            ViewData[CommonDataKey.LIST_US_ACCOUNTING] = new SelectList(dao.GetSelectedListUSAccounting(), "Value", "Text", pr.USAccounting.HasValue ? pr.USAccounting.Value.ToString() : string.Empty);
            ViewData[CommonDataKey.PR_LIST_COUNT] = (appList != null && appList.Count > 0) ? (appList.Count - 1) : 0;
            if (appList != null && appList.Count > 0)
            {
                ViewData[CommonDataKey.PR_FORWARD] = new SelectList(listAssign,
                    "UserAdminRole", "DisplayName", appList[appList.Count - 1].UserAdminRole);
            }
            else
            {
                ViewData[CommonDataKey.PR_FORWARD] = new SelectList(listAssign,
                    "UserAdminRole", "DisplayName", principal.UserData.UserID + Constants.SEPARATE_USER_ADMIN_ID_STRING + Constants.PR_PURCHASING_ID.ToString());
            }

            if (list.Count == 0) //if does
            {
                ViewData[CommonDataKey.PR_ROLE] = null;
            }
            else
            {
                //Display login role dropdownlist for choosing
                ViewData[CommonDataKey.PR_ROLE] = new SelectList(list, "ID", "Name");
            }


            //Get comment of request            
            if (pr != null)
            {
                if (pr.AssignID != principal.UserData.UserID || !principal.IsInRole(pr.AssignRole.ToString()))
                {
                    return RedirectToAction("Index");
                }

                List<PurchaseItem> itemList;
                itemList = dao.GetPurchaseRequestItems(Convert.ToInt32(id));
                ViewData[CommonDataKey.LIST_PURCHASE_ITEM] = itemList;
                ViewData[CommonDataKey.PR_MAX_APPROVAL] = ConfigurationManager.AppSettings["PR_MAX_APPROVAL"];
                return View(pr);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Set filter arguments to session
        /// </summary>
        /// <param name="keyword">string</param>
        /// <param name="department">string</param>
        /// <param name="subDepartment">string</param>
        /// <param name="positionId">string</param>
        /// <param name="requestorId">string</param>
        /// <param name="statusId">string</param>
        /// <param name="column">string</param>
        /// <param name="order">string</param>
        /// <param name="pageIndex">string</param>
        /// <param name="rowCount">string</param>
        private void SetSessionFilter(string keyword, string department, string subDepartment, string requestorId, string assignId, 
            string resolutionId,string column, string order, int pageIndex, int rowCount, string page = "", 
            string fromDate = Constants.PURCHASE_REQUEST_SUBMIT_FROM_DATE_KEY,string toDate = Constants.PURCHASE_REQUEST_SUBMIT_TO_DATE_KEY)
        {
            string sessionKey=SessionKey.PURCHASE_FILTER;
            
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.PURCHASE_REQUEST_KEYWORD, keyword);
            hashData.Add(Constants.PURCHASE_REQUEST_DEPARTMENT, department);
            hashData.Add(Constants.PURCHASE_REQUEST_SUB_DEPARTMENT, subDepartment);
            hashData.Add(Constants.PURCHASE_REQUEST_REQUESTOR_ID, requestorId);
            hashData.Add(Constants.PURCHASE_REQUEST_ASSIGN_ID, assignId);
            hashData.Add(Constants.PURCHASE_REQUEST_RESOLUTION_ID, resolutionId);

            hashData.Add(Constants.PURCHASE_REQUEST_COLUMN, column);
            hashData.Add(Constants.PURCHASE_REQUEST_ORDER, order);
            hashData.Add(Constants.PURCHASE_REQUEST_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.PURCHASE_REQUEST_ROW_COUNT, rowCount);
            //use for view report page
            if (page == "Report")
            {
                sessionKey = SessionKey.PURCHASE_FILTER_REPORT;
                hashData.Add(Constants.PURCHASE_REQUEST_REPORT_FROM_DATE, fromDate);
                hashData.Add(Constants.PURCHASE_REQUEST_REPORT_TO_DATE, toDate);
            }
            Session[sessionKey] = hashData;
        }

        private bool HasViewAllPR()
        {
            var moduleId = (byte)Modules.PurchaseRequest;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            ArrayList permissionIds = new ArrayList();
            //loop all permission to check the permission is allowed and add to array 
            permissionIds.Add((int)Permissions.ViewAllPR);
            bool result = PermissionCommon.IsUserAuthorized(principal.UserData.UserID, moduleId, Array.ConvertAll(permissionIds.ToArray(), arr => (int)arr));

            return result;
        }


        /// <summary>
        /// Get list jquery grid
        /// </summary>
        /// <param name="purchaseId">string</param>
        /// <param name="department">string</param>
        /// <param name="subdepartment">string</param>
        /// <param name="requestorId">string</param>
        /// <param name="statusId">string</param>
        /// <returns>ActionResult</returns>
        [ValidateInput(false)]
        public ActionResult GetListJQGrid(string purchaseId, string department, string subdepartment, string requestorId, string assignId, string resolutionId)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;

            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

            #endregion

            SetSessionFilter(purchaseId, department, subdepartment, requestorId, assignId, resolutionId, sortColumn, sortOrder, pageIndex, rowCount);

            //bool isViewAll = HasViewAllPR();
            //if (principal.UserData.Role != Constants.PR_REQUESTOR_ID)
            //    isViewAll = true;
            // //If user have authenticated viewAllPR then get requestorId, Otherwise get principal. userlogin
            //requestorId = isViewAll ? requestorId : string.Empty;

            purchaseId = purchaseId.Trim();

            if (purchaseId == Constants.PURCHASE_REQUEST)
            {
                purchaseId = "";
            }
            else
            {
                if (purchaseId.ToLower().StartsWith(Constants.PR_REQUEST_PREFIX.ToLower()))
                {
                    purchaseId = purchaseId.Substring(3);
                }
                //string containsJR = Constants.PR_REQUEST_PREFIX;
                //if (containsJR.Contains(purchaseId))
                //{
                //    purchaseId = string.Empty;
                //}
                //else
                //{
                //    if (purchaseId.Length >= 4)
                //    {
                //        purchaseId = purchaseId.Substring(3);
                //    }
                //}
            }
            List<sp_GetPurchaseRequestResult> list = GetListByFilter(purchaseId, department, subdepartment, requestorId, assignId, resolutionId,
                principal.UserData.UserID, principal.UserData.Role);
            //List<WFResolution> resList = resDao.GetListResolutionChangeByRole(principal.UserData.Role);

            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = dao.Sort(list, sortColumn, sortOrder)
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
                        i = m.ID,
                        cell = new string[] {
                            m.ID.ToString(),
                            m.WFStatusID != Constants.STATUS_CLOSE ? m.Priority.HasValue ? 
                                m.Priority.Value.ToString() : string.Empty : string.Empty,
                            CommonFunc.Link(m.ID.ToString(),"/PurchaseRequest/Detail/"  + m.ID.ToString() + "", "PR-" +  m.ID.ToString(),false),
                            m.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW),                                                        
                            m.ExpectedDate.HasValue ? m.ExpectedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "",
                            Server.HtmlEncode(m.RequestorName),
                            Server.HtmlEncode(m.Department),                            
                            Server.HtmlEncode(m.ResolutionName),                            
                            SetAssignName(m.WFResolutionID,m.WFStatusID,m.AssignName),  
                            Server.HtmlEncode(m.PurchaseAppoval),
                            //CommonFunc.Link(m.ID.ToString(), "justification", "#", CommonFunc.SubStringRoundWord(Server.HtmlEncode(m.Justification), 
                            //    Constants.PURCHASE_REQUEST_JUSTIFICATION_MAX_LENGTH)),
                            CommonFunc.Link(m.ID.ToString(), "justification", "#", CommonFunc.SubStringRoundWordToOneRow(m.Justification, 
                                Constants.PURCHASE_REQUEST_JUSTIFICATION_MAX_LENGTH)),
                            SetAction(m.AssignRole.Value,m.AssignID.Value,m.WFStatusID,m.WFResolutionID,m.ID, false)
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JustificationTooltip(int id)
        {
            PurchaseRequest pr = dao.GetByID(id);
            //string result = Server.HtmlEncode(pr.Justification);
            //return result.Replace("\\r\\n", "<br/>");
            return View(pr);
        }

        /// <summary>
        /// Set button action
        /// </summary>
        /// <param name="assignRole">int</param>
        /// <param name="assignID">int</param>
        /// <param name="status">int</param>
        /// <param name="resolutionID">int</param>
        /// <param name="id">int</param>
        /// <returns>string</returns>
        private string SetAction(int assignRole, int assignID, int status, int resolutionID, int id, bool isDetailPage)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string value = string.Empty;
            string editButton = CommonFunc.ButtonWithParams("edit", "Edit", "navigateWithReferrer('/PurchaseRequest/Edit/" + id.ToString() + "')", isDetailPage ? "Edit" : string.Empty);
            bool hasEditButton = false;
            if (dao.HasEditPermision(id, principal.UserData.UserName, principal.UserData.Role))
            {
                value += editButton;
                hasEditButton = true;
            }
            string sDisplayText = "";
            if (assignID == principal.UserData.UserID && assignRole == principal.UserData.Role && status != Constants.STATUS_CLOSE)
            {
                if (principal.UserData.Role == Constants.PR_REQUESTOR_ID && !hasEditButton)
                {
                    value += editButton;
                }
                else if (principal.UserData.Role == Constants.PR_PURCHASING_ID)
                {
                    if (resolutionID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING || resolutionID == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING
                        || resolutionID == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR)
                    {
                        sDisplayText = isDetailPage ? "Review" : string.Empty;
                        value += CommonFunc.ButtonWithParams("pr_review", "Review", "navigateWithReferrer('/PurchaseRequest/PurchasingConFirm/" + id.ToString() + "')", sDisplayText);
                    }
                    else if (resolutionID == Constants.PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING || resolutionID == Constants.PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING_IMMEDIATELY ||
                        resolutionID == Constants.PR_RESOLUTION_COMPLETE_WAITING_CLOSE_ID)
                    {
                        sDisplayText = isDetailPage ? "Fill Data" : string.Empty;
                        value += CommonFunc.ButtonWithParams("filldata", "Fill Data", "navigateWithReferrer('/PurchaseRequest/FillData/" + id.ToString() + "')", sDisplayText);
                    }
                }
                else if (principal.UserData.Role == Constants.PR_SR_MANAGER_ID)
                {
                    sDisplayText = isDetailPage ? "Set Up Approval" : string.Empty;
                    value += CommonFunc.ButtonWithParams("approve", "Set Up Approval", "navigateWithReferrer('/PurchaseRequest/SetupApproval/" + id.ToString() + "')", sDisplayText);
                    sDisplayText = isDetailPage ? "Reject" : string.Empty;
                    value += CommonFunc.ButtonWithParams("reject", "Reject", "CRM.popup('/PurchaseRequest/SrManagerReject/" + id.ToString() + "', 'Reject " + Constants.PR_REQUEST_PREFIX + id.ToString() + "', 500)", sDisplayText);
                }
                else if ((principal.UserData.Role == Constants.PR_VP_OF_SALES_ID || principal.UserData.Role == Constants.PR_MANAGER_ID
                    || principal.UserData.Role == Constants.PR_PRESIDENT_CEO_ID))
                {
                    List<WFResolution> resList = resDao.GetListResolutionChangeByRole(principal.UserData.Role);
                    if (principal.UserData.Role == Constants.PR_MANAGER_ID)
                    {
                        resList = resList.Where(p => p.ID != Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_MANAGER).ToList<WFResolution>();
                    }
                    else if (principal.UserData.Role == Constants.PR_VP_OF_SALES_ID)
                    {
                        resList = resList.Where(p => p.ID != Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_VP_OF_SALES).ToList<WFResolution>();
                    }
                    else if (principal.UserData.Role == Constants.PR_PRESIDENT_CEO_ID)
                    {
                        resList = resList.Where(p => p.ID != Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PRESIDENT_CEO).ToList<WFResolution>();
                    }
                    sDisplayText = isDetailPage ? "Approve" : string.Empty;
                    value += CommonFunc.ButtonWithParams("approve", "Approve", "CRM.popup('/PurchaseRequest/ApprovalResult/" + id.ToString() + "@" +
                                resList[0].ID.ToString() + "@" + principal.UserData.Role.ToString() + "', 'Approve " + Constants.PR_REQUEST_PREFIX + id.ToString() + "', 500)", sDisplayText);
                    sDisplayText = isDetailPage ? "Reject" : string.Empty;
                    value += CommonFunc.ButtonWithParams("reject", "Reject", "CRM.popup('/PurchaseRequest/ApprovalResult/" + id.ToString() + "@" + resList[1].ID.ToString() + "@" +
                                principal.UserData.Role.ToString() + "', 'Reject " + Constants.PR_REQUEST_PREFIX + id.ToString() + "', 500)", sDisplayText);

                }
                else if (principal.UserData.Role == Constants.PR_US_ACCOUNTING)
                {
                    sDisplayText = isDetailPage ? "Process" : string.Empty;
                    value += CommonFunc.ButtonWithParams("pr_review", "Process", "window.location= '/PurchaseRequest/USAccountingResult/" + id.ToString() + "'", sDisplayText);
                }
            }

            return value;
        }

        public ActionResult USAccountingResult(string id)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                #region Check permission
                bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                        id, Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return RedirectToAction("NotPermission", "Common");
                #endregion

                int purchaseId = 0;
                if (Int32.TryParse(id, out purchaseId))//check id existed in DB
                {
                    sp_GetPurchaseRequestResult obj = dao.GetPurchaseRequestByID(id);
                    if (obj != null)
                    {
                        ViewData[CommonDataKey.DEPARTMENT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", deptDao.GetDepartmentIdBySub(obj.SubDepartment));
                        ViewData[CommonDataKey.SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", obj.SubDepartment);
                        ViewData[CommonDataKey.PURCHASE_REQUEST_PRIORITY] = new SelectList(Constants.PURCHASE_REQUEST_PRIORITY,
                            "value", "text", obj.Priority.HasValue ? obj.Priority : 0);
                        ViewData[CommonDataKey.SALE_TAX_NAME] = new SelectList(Constants.SaleTax, "Value", "Text", obj.SaleTaxName);
                        //Get Resolution List when Purchasing Fill Data
                        List<WFResolution> resolutionList = resDao.GetListResolutionChangeByRole(principal.UserData.Role).OrderByDescending(q => q.ID).ToList();

                        ViewData[CommonDataKey.DDL_PR_RESOLUTION] = new SelectList(resolutionList, "ID", "Name", obj.WFResolutionID);
                        ViewData[CommonDataKey.DDL_PR_STATUS] = new SelectList(staDao.GetListStatusByResolution(obj.WFResolutionID), "ID", "Name");
                        ViewData[CommonDataKey.DDL_PR_ASSIGN] = new SelectList(dao.GetSelectedListUSAccounting(), "Value", "Text", obj.AssignID.HasValue ? obj.AssignID.Value.ToString() : string.Empty);
                        ViewData[CommonDataKey.SUB_TOTAL_ITEM] = Math.Round(dao.GetSubTotalByPurchaseID(purchaseId).Value, Constants.ROUND_NUMBER);
                        List<PurchaseInvoice> listInvoice = dao.GetPurchaseInvoice(purchaseId);
                        if (listInvoice.Count > 0)
                        {
                            ViewData[CommonDataKey.LIST_INVOICE] = listInvoice;
                        }
                        List<PurchaseItem> list = dao.GetListItemsByPurchaseID(purchaseId);
                        ViewData[CommonDataKey.LIST_PURCHASE_ITEM] = list;
                        ViewData[CommonDataKey.COUNT_PURCHASE_ITEM] = list.Count;
                        List<sp_GetListApprovalAssignResult> listApproval = dao.GetListApprovalAssign(purchaseId);
                        //Filter exception 2 Role Purchasing,SR Manager
                        listApproval = listApproval.Where(p => p.ApproverGroup != Constants.PR_PURCHASING_ID && p.ApproverGroup != Constants.PR_SR_MANAGER_ID).ToList<sp_GetListApprovalAssignResult>();
                        if (listApproval.Count > 0)
                        {
                            listApproval = listApproval.Where(p => p.ApproverGroup != Constants.PR_SR_MANAGER_ID && p.ApproverGroup != Constants.PR_PURCHASING_ID)
                                .OrderBy(p => p.ApprovalID).ToList<sp_GetListApprovalAssignResult>();
                            ViewData[CommonDataKey.LIST_APPROVAL] = listApproval;
                        }
                        return View(obj);
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                ShowMessage(msg);
            }
            return RedirectToAction("/Index");
            //var principal = HttpContext.User as AuthenticationProjectPrincipal;
            //PurchaseRequest obj = dao.GetByID(id);
            //List<WFResolution> resolutionList = resDao.GetListResolutionChangeByRole(principal.UserData.Role).OrderByDescending(q => q.ID).ToList();
            //ViewData[CommonDataKey.DDL_PR_RESOLUTION] =ViewData[CommonDataKey.DDL_PR_RESOLUTION] = new SelectList(resolutionList, "ID", "Name", obj.WFResolutionID);
            //ViewData[CommonDataKey.DDL_PR_STATUS] = new SelectList(staDao.GetListStatusByResolution(obj.WFResolutionID), "ID", "Name");
            //ViewData[CommonDataKey.DDL_PR_ASSIGN] = new SelectList(dao.GetSelectedListUSAccounting(), "Value", "Text", "");
            //return View(obj);
        }

        [HttpPost]
        public ActionResult USAccountingResult(FormCollection request)
        {
            Message msg = null;

            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;

                bool isSendMail = false;
                int prID = int.Parse(request["PurchaseID"]);
                #region Check permission
                bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                        request["PurchaseID"], Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return RedirectToAction("NotPermission", "Common");
                #endregion

                PurchaseRequest objPR = dao.GetByID(prID);
                if (objPR != null)
                {
                    #region Insert Comment
                    string contents = request["Contents"];
                    if (!string.IsNullOrEmpty(contents))
                    {
                        PurchaseComment obj = new PurchaseComment();
                        obj.RequestID = prID;
                        obj.PostTime = DateTime.Now;
                        obj.Poster = principal.UserData.UserName;
                        obj.Contents = contents;
                        prCommentDao.Insert(obj);
                    }
                    #endregion

                    int statusID = int.Parse(request["WFStatusID"]);
                    int resolutionID = int.Parse(request["WFResolutionID"]);

                    // if status is CLOSED
                    if (statusID == Constants.STATUS_CLOSE)
                    {
                        objPR.InvolveResolution += Constants.PR_ACTION_CLOSE + Constants.SEPARATE_INVOLVE_SIGN;
                        objPR.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                        objPR.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                        objPR.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                        objPR.WFResolutionID = resolutionID;

                        objPR.WFStatusID = Constants.STATUS_CLOSE;
                        isSendMail = true;
                    }
                    else
                    {
                        int assignID = int.Parse(request["Assign"]);
                        if (resolutionID == Constants.PR_RESOLUTION_TO_BE_PROCESSED)
                        {
                            if (objPR.AssignID != assignID)
                            {
                                objPR.InvolveResolution += Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                                objPR.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                                objPR.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                                objPR.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                                objPR.AssignID = assignID;
                                objPR.AssignRole = Constants.PR_US_ACCOUNTING;
                                isSendMail = true;
                            }
                        }
                        else
                        {
                            objPR.InvolveResolution += Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                            objPR.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                            objPR.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                            objPR.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                            objPR.AssignID = assignID;
                            objPR.AssignRole = Constants.PR_US_ACCOUNTING;
                            objPR.WFResolutionID = resolutionID;
                            isSendMail = true;
                        }
                    }
                    msg = dao.UpdateForUSAccouting(objPR);
                    ShowMessage(msg);
                    if (msg.MsgType == MessageType.Info && isSendMail)
                    {
                        SendPRMail(objPR.ID);
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                ShowMessage(msg);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Set assign name
        /// </summary>
        /// <param name="resolution">int</param>
        /// <param name="status">int</param>
        /// <param name="assignName">string</param>
        /// <returns>string</returns>
        private string SetAssignName(int resolution, int status, string assignName)
        {
            if ((resolution == Constants.PR_RESOLUTION_COMPLETE_ID || resolution == Constants.PR_RESOLUTION_NOT_COMPLETE ||
                resolution == Constants.PR_RESOLUTION_CANCEL) && (status == Constants.STATUS_CLOSE))
            {
                assignName = string.Empty;
            }

            return assignName;
        }

        /// <summary>
        /// Get list purchase request by filter
        /// </summary>
        /// <param name="text">string</param>
        /// <param name="department">string</param>
        /// <param name="subdepartment">string</param>
        /// <param name="requestorId">string</param>
        /// <param name="statusId">string</param>
        /// <param name="role">string</param>
        /// <returns>List<sp_GetPurchaseRequestResult></returns>
        private List<sp_GetPurchaseRequestResult> GetListByFilter(string text, string department, string subdepartment, string requestorId, string assignID,
            string resolutionId, int loginId, int loginRole,string fromDate="",string toDate="")
        {
            string textSearch = null;
            int idepartment = 0;
            int isubdepartment = 0;
            int irequestorId = 0;
            string iresolutionId = null;
            int iassignID = 0;
            if (!string.IsNullOrEmpty(text))
            {
                textSearch = text;
            }
            if (!string.IsNullOrEmpty(department))
            {
                idepartment = int.Parse(department);
            }
            if (!string.IsNullOrEmpty(subdepartment))
            {
                isubdepartment = int.Parse(subdepartment);
            }

            if (!string.IsNullOrEmpty(requestorId))
            {
                irequestorId = int.Parse(requestorId);
            }
            if (!string.IsNullOrEmpty(resolutionId))
            {
                iresolutionId = resolutionId;
            }
            if (!string.IsNullOrEmpty(assignID))
            {
                iassignID = int.Parse(assignID);
            }
            string sFromDate = fromDate == Constants.PURCHASE_REQUEST_SUBMIT_FROM_DATE_KEY ? "" : fromDate;
            string sToDate = toDate == Constants.PURCHASE_REQUEST_SUBMIT_TO_DATE_KEY ? "" : toDate;
            
            //List<sp_GetPurchaseRequestResult> list = dao.GetList(textSearch, idepartment, isubdepartment, irequestorId, iassignID, 
            //    istatusId, role, irequestorId, loginName);
            List<sp_GetPurchaseRequestResult> list = dao.GetList(textSearch, idepartment, isubdepartment, irequestorId, iassignID,
                iresolutionId, loginId, loginRole, 0, false, sFromDate,sToDate);
            
            return list;
        }

        #endregion

        #region Create Purchase Request
        /// <summary>
        /// Create purchase request
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Create()
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            if (principal.UserData.Role == Constants.PR_REQUESTOR_ID)//check user in role Requestor
            {
                ViewData[CommonDataKey.DEPARTMENT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", "");
                ViewData[CommonDataKey.SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", "");
                ViewData[CommonDataKey.PURCHASE_REQUEST_PRIORITY] = new SelectList(Constants.PURCHASE_REQUEST_PRIORITY, "value", "text");
                ViewData[CommonDataKey.SALE_TAX_NAME] = new SelectList(Constants.SaleTax, "Value", "Text", "");
                ViewData[CommonDataKey.DDL_PR_RESOLUTION] = new SelectList(resDao.GetListResolutionChangeByRole(Constants.PR_REQUESTOR_ID, false, Constants.WORK_FLOW_PURCHASE_REQUEST), "ID", "Name", Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING);
                ViewData[CommonDataKey.DDL_PR_STATUS] = new SelectList(staDao.GetListStatusByResolution(Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING), "ID", "Name");
                ViewData[CommonDataKey.DDL_PR_ASSIGN] = new SelectList(dao.GetListAssign(Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING, Constants.WORK_FLOW_PURCHASE_REQUEST),
                    "UserAdminRole", "DisplayName");
                ViewData[CommonDataKey.COUNT_PURCHASE_ITEM] = 0;
                ViewData["UserRole"] = principal.UserData.Role.ToString();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        /// <summary>
        /// Create new purchase request
        /// </summary>
        /// <param name="obj">PurchaseRequest</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PurchaseRequest obj, PurchaseComment objUI)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                #region Insert Purchase Request
                obj.BillableToClient = bool.Parse(Request["chk_BillableGroup"]);
                obj.MoneyType = int.Parse(Request["chk_MoneyTypeGroup"]);
                obj.PaymentID = int.Parse(Request["chk_Payment"]);
                string userAdminRole = Request["Assign"];
                obj.Requestor = principal.UserData.UserID;
                bool isSendMail = false;
                string[] arr = userAdminRole.Split(Constants.SEPARATE_USER_ADMIN_ID);
                int assignID = int.Parse(arr[0].Trim());
                int assignRole = int.Parse(arr[1].Trim());
                obj.AssignID = assignID;
                obj.AssignRole = assignRole;
                //obj.InvolveID = principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                //obj.InvolveRole = principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                if (obj.SaleTaxName == "0")//case select no sale tax
                {
                    obj.SaleTaxValue = 0;
                }
                else
                {
                    NumberFormatInfo numInfo = new NumberFormatInfo();
                    numInfo.CurrencySymbol = "R";
                    double saleTaxValue = double.Parse(Request["SaleTaxValue"], NumberStyles.Any, numInfo);

                    obj.SaleTaxValue = saleTaxValue;
                }

                #region set involedata
                //if (obj.WFResolutionID == Constants.PR_RESOLUTION_NEW_ID)//case Add New Request
                //{
                //    if ((assignID != obj.Requestor) && (assignRole == Constants.PR_REQUESTOR_ID))//case add new but send PR for another Requestor
                //    {
                //        obj.InvolveResolution = Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                //        obj.InvolveDate = obj.InvolveDate + DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                //        obj.InvolveID += assignID + Constants.SEPARATE_INVOLVE_SIGN;
                //        obj.InvolveRole += assignRole + Constants.SEPARATE_INVOLVE_SIGN;
                //        isSendMail = true;
                //    }
                //    else if ((assignID == obj.Requestor) && (assignRole == Constants.PR_REQUESTOR_ID))//case add new PR
                //    {
                //        obj.InvolveResolution = Constants.PR_ACTION_ADDNEW + Constants.SEPARATE_INVOLVE_SIGN;
                //        obj.InvolveDate = obj.InvolveDate + DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                //        //obj.InvolveID += assignID + Constants.SEPARATE_INVOLVE_SIGN;
                //        //obj.InvolveRole += assignRole + Constants.SEPARATE_INVOLVE_SIGN;
                //    }
                //}
                //else if (obj.WFResolutionID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING)//case send PR for Purchasing
                //{
                //    obj.InvolveResolution = Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                //    obj.InvolveDate = obj.InvolveDate + DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                //    obj.InvolveID += assignID + Constants.SEPARATE_INVOLVE_SIGN;
                //    obj.InvolveRole += assignRole + Constants.SEPARATE_INVOLVE_SIGN;
                //    isSendMail = true;
                //}
                #endregion
                SetInvoleData(ref obj, ref isSendMail, true);
                obj.Requestor = principal.UserData.UserID;
                obj.CreateDate = DateTime.Now;
                obj.UpdateDate = DateTime.Now;
                obj.CreatedBy = principal.UserData.UserName;
                obj.UpdatedBy = principal.UserData.UserName;
                obj.RequestDate = DateTime.Now;
                msg = dao.Insert(obj);
                #endregion
                #region Insert Item
                if (msg.MsgType == MessageType.Info)
                {
                    int countItem = int.Parse(Request["hidItem"]);
                    for (int i = 1; i <= countItem; i++)
                    {
                        PurchaseItem item = new PurchaseItem();
                        item.RequestID = obj.ID;
                        item.ItemName = Request["description" + i];
                        item.Quantity = Math.Round(double.Parse(Request["quantity" + i]), Constants.ROUND_NUMBER);
                        item.Price = Math.Round(double.Parse(Request["price" + i]), Constants.ROUND_NUMBER);
                        item.TotalPrice = Math.Round((item.Quantity * item.Price), Constants.ROUND_NUMBER);
                        dao.InsertItem(item);
                    }
                }
                #endregion

                #region Insert Comment
                if (msg.MsgType == MessageType.Info)
                {
                    PurchaseComment objCom = new PurchaseComment();
                    objCom.RequestID = obj.ID;
                    objCom.PostTime = DateTime.Now;
                    objCom.Poster = principal.UserData.UserName;
                    objCom.Contents = objUI.Contents;
                    string serverPath = Server.MapPath(Constants.PERFORMANCE_REVIEW_PATH);
                    Message msgCheck = CheckFileUpload();
                    if (msgCheck == null) //case sussessfully
                    {
                        int y = 0;
                        foreach (string file in Request.Files)
                        {

                            HttpPostedFileBase hpf = Request.Files[y] as HttpPostedFileBase;
                            if (!string.IsNullOrEmpty(Path.GetExtension(hpf.FileName)))
                            {
                                if (String.IsNullOrEmpty(objCom.Contents))
                                {
                                    objCom.Contents = "";
                                }
                                string strReplaceUserName = principal.UserData.UserName.Replace(".", "_");
                                string extension = Path.GetExtension(hpf.FileName);
                                string fileName = Path.GetFileNameWithoutExtension(hpf.FileName);
                                string contractName = principal.UserData.UserID + "_" + strReplaceUserName + "_" + fileName +
                                     "." + DateTime.Now.ToString(Constants.UNIQUE_TIME) + extension;
                                contractName = ConvertUtil.FormatFileName(contractName);
                                string strPath = serverPath + "\\" + contractName;
                                hpf.SaveAs(strPath);
                                objCom.Files += contractName + Constants.FILE_STRING_PREFIX;
                            }
                            y++;
                        }


                    }
                    if (objCom.Contents != null)
                    {
                        prCommentDao.Insert(objCom);
                    }
                }
                #endregion
                if (msg.MsgType == MessageType.Info && isSendMail)
                {
                    SendPRMail(obj.ID);
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return RedirectToAction("/Index");
        }
        #endregion

        #region Edit Purchase Request
        /// <summary>
        /// Edit purchase request
        /// </summary>
        /// <param name="id">id of purchase request</param>
        /// <returns>ActionResult</returns>

        public ActionResult Edit(string id)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                int purchaseId = 0;
                #region Check permission
                bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                        id, Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return RedirectToAction("NotPermission", "Common");
                #endregion
                List<PurchaseComment> listCom = prCommentDao.GetList(int.Parse(id));
                ViewData[CommonDataKey.PR_COMMENT] = listCom;
                if (listCom != null)
                {
                    ViewData[CommonDataKey.PR_COMMENT_COUNT] = listCom.Count;
                }

                if (Int32.TryParse(id, out purchaseId))//check id existed in DB
                {
                    PurchaseRequest obj = dao.GetByID(purchaseId);
                    if (obj != null)
                    {
                        ViewData[CommonDataKey.DEPARTMENT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", deptDao.GetDepartmentIdBySub(obj.SubDepartment));
                        ViewData[CommonDataKey.SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", obj.SubDepartment);
                        ViewData[CommonDataKey.PURCHASE_REQUEST_PRIORITY] = new SelectList(Constants.PURCHASE_REQUEST_PRIORITY,
                            "value", "text", obj.Priority.HasValue ? obj.Priority : 0);
                        ViewData[CommonDataKey.SALE_TAX_NAME] = new SelectList(Constants.SaleTax, "Value", "Text", obj.SaleTaxName);
                        SetResolutionData(obj);
                        ViewData[CommonDataKey.SUB_TOTAL_ITEM] = Math.Round(dao.GetSubTotalByPurchaseID(purchaseId).Value, Constants.ROUND_NUMBER).ToString();
                        //Get All Purchase Item
                        List<PurchaseItem> list = dao.GetListItemsByPurchaseID(purchaseId);
                        ViewData[CommonDataKey.LIST_PURCHASE_ITEM] = list;
                        ViewData[CommonDataKey.COUNT_PURCHASE_ITEM] = list.Count;
                        ViewData[CommonDataKey.PR_SELECTED_REQUESTOR] = obj.Requestor +
                            Constants.SEPARATE_USER_ADMIN_ID_STRING + Constants.PR_REQUESTOR_ID;
                        ViewData["UserRole"] = principal.UserData.Role.ToString();
                        return View(obj);
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                ShowMessage(msg);
            }
            return RedirectToAction("/Index");

        }

        public void SetResolutionData(PurchaseRequest obj)
        {
            if (obj == null)
                return;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            List<WFResolution> resolutionList = resDao.GetListByRoleAndResolution(obj);


            bool toGroup = false;
            if (obj.AssignRole == Constants.PR_REQUESTOR_ID)
            {
                if (obj.WFResolutionID == Constants.PR_RESOLUTION_REJECT)//case PR Reject
                {
                    resolutionList = resolutionList.Where(p => p.ID != Constants.PR_RESOLUTION_NEW_ID &&
                        p.ID != Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR).ToList<WFResolution>();
                }
                else if (obj.WFResolutionID == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR)
                {
                    resolutionList = resolutionList.Where(p => p.ID != Constants.PR_RESOLUTION_NEW_ID &&
                        p.ID != Constants.PR_RESOLUTION_REJECT).ToList<WFResolution>();
                }
                else if (obj.WFResolutionID == Constants.PR_RESOLUTION_NEW_ID)
                {
                    resolutionList = resolutionList.Where(p => p.ID != Constants.PR_RESOLUTION_REJECT
                      && p.ID != Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR &&
                      p.ID != Constants.PR_RESOLUTION_CANCEL).ToList<WFResolution>();
                }
                else
                {
                    resolutionList = resolutionList.Where(p => p.ID != Constants.PR_RESOLUTION_REJECT
                        && p.ID != Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR).ToList<WFResolution>();
                }
            }
            else if (obj.AssignRole == Constants.PR_PURCHASING_ID)
            {
                if (obj.WFResolutionID == Constants.PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING ||
                    obj.WFResolutionID == Constants.PR_RESOLUTION_TO_BE_PROCESSED)
                {

                    resolutionList = new List<WFResolution>();
                    resolutionList.Add(obj.WFResolution);
                    toGroup = true;
                }
                else if (obj.WFResolutionID == Constants.PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING_IMMEDIATELY)
                {
                    resolutionList = new List<WFResolution>();
                    resolutionList.Add(obj.WFResolution);
                    toGroup = true;
                }
                else if (obj.WFResolutionID == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING)
                {
                    resolutionList = resolutionList.Where(p => p.ID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_SR_MANAGER
                        || p.ID == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING
                        || p.ID == Constants.PR_RESOLUTION_REJECT).ToList<WFResolution>();
                }
                else
                {
                    //case Requestor send PR to Group Purchasing
                    resolutionList = resolutionList.Where(p => p.ID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING
                        || p.ID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_SR_MANAGER
                        || p.ID == Constants.PR_RESOLUTION_REJECT).ToList<WFResolution>();
                }
            }
            else
            {
                resolutionList = new List<WFResolution>();
                resolutionList.Add(obj.WFResolution);
                toGroup = true;
            }

            ViewData[CommonDataKey.TO_GROUP] = toGroup;
            ViewData[CommonDataKey.DDL_PR_RESOLUTION] = new SelectList(resolutionList, "ID", "Name", obj.WFResolutionID);
            ViewData[CommonDataKey.DDL_PR_STATUS] = new SelectList(
                staDao.GetListStatusByResolution(obj.WFResolutionID), "ID", "Name", obj.WFStatusID);

            if (toGroup)
            {
                ViewData[CommonDataKey.DDL_PR_ASSIGN] = new SelectList(userAdminDao.GetListWithRole(obj.AssignRole.Value), "value", "text",
                    obj.AssignID + Constants.SEPARATE_USER_ADMIN_ID_STRING + obj.AssignRole);
            }
            else
            {
                ViewData[CommonDataKey.DDL_PR_ASSIGN] = new SelectList(dao.GetListAssign(obj.WFResolutionID,
                    Constants.WORK_FLOW_PURCHASE_REQUEST), "UserAdminRole", "DisplayName",
                    obj.AssignID + Constants.SEPARATE_USER_ADMIN_ID_STRING + obj.AssignRole);
            }
        }

        /// <summary>
        /// Get the last item in string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetLastItem(string input, string separator)
        {
            return input.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries).Last();
        }

        private string ReplaceLastItem(string input, string newValue, string separator)
        {
            input = input.TrimEnd(separator[0]);
            input = input.Substring(0, input.LastIndexOf(separator));
            input += separator + newValue + separator;
            return input;
        }

        private void SetInvoleData(ref PurchaseRequest pr, ref bool isSendMail, bool isCreateNew)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            pr.InvolveID = pr.InvolveID ?? "";
            pr.InvolveDate = pr.InvolveDate ?? "";
            pr.InvolveResolution = pr.InvolveResolution ?? "";
            pr.InvolveRole = pr.InvolveRole ?? "";

            if (isCreateNew)
            {
                pr.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                pr.InvolveDate += DateTime.Now + Constants.SEPARATE_INVOLVE_SIGN;
                pr.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                pr.InvolveResolution += Constants.PR_ACTION_ADDNEW + Constants.SEPARATE_INVOLVE_SIGN;
                if (pr.AssignID != principal.UserData.UserID || pr.AssignRole != principal.UserData.Role)
                {
                    pr.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveDate += DateTime.Now + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveResolution += Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                }
                isSendMail = true;
            }
            else
            {
                if (pr.AssignID != principal.UserData.UserID || pr.AssignRole != principal.UserData.Role)
                {
                    pr.InvolveID += pr.AssignID + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveDate += string.Empty + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveResolution += string.Empty + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveRole += pr.AssignRole + Constants.SEPARATE_INVOLVE_SIGN;
                }

                if (pr.WFResolutionID == Constants.PR_RESOLUTION_REJECT ||
                pr.WFResolutionID == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING ||
                pr.WFResolutionID == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR)
                {
                    pr.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveDate += DateTime.Now + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveResolution += Constants.PR_ACTION_REJECT + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                    isSendMail = true;
                }
                else if (pr.WFResolutionID == Constants.PR_RESOLUTION_COMPLETE_ID ||
                    pr.WFResolutionID == Constants.PR_RESOLUTION_NOT_COMPLETE ||
                    pr.WFResolutionID == Constants.PR_RESOLUTION_CANCEL)
                {
                    pr.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveDate += DateTime.Now + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveResolution += Constants.PR_ACTION_CLOSE + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                    isSendMail = false;
                }
                else
                {
                    pr.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveDate += DateTime.Now + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveResolution += Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                    pr.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                    isSendMail = true;
                }
            }
        }

        /// <summary>
        /// Edit information of purchase request
        /// </summary>
        /// <param name="obj">PurchaseRequest</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PurchaseRequest obj, PurchaseComment objUI)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                #region Check permission
                bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                        obj.ID.ToString(), Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return RedirectToAction("NotPermission", "Common");
                #endregion

                obj.BillableToClient = bool.Parse(Request["chk_BillableGroup"]);
                obj.MoneyType = int.Parse(Request["chk_MoneyTypeGroup"]);
                obj.PaymentID = int.Parse(Request["chk_Payment"]);
                string userAdminRole = Request["Assign"];
                PurchaseRequest objDb = dao.GetByID(obj.ID);
                bool isSendMail = false;

                SetInvoleData(ref obj, ref isSendMail, false);
                if (!string.IsNullOrEmpty(userAdminRole))//check if purchase resolution is not cancel
                {
                    string[] arr = userAdminRole.Split(Constants.SEPARATE_USER_ADMIN_ID);

                    //Define when user choose field forward to
                    int newAssignID = int.Parse(arr[0].Trim());
                    int newRoleID = int.Parse(arr[1].Trim());
                    obj.AssignID = newAssignID;
                    obj.AssignRole = newRoleID;
                }

                obj.UpdatedBy = principal.UserData.UserName;
                if (obj.SaleTaxName == "0")//case select no sale tax
                {
                    obj.SaleTaxValue = 0;
                }
                else
                {
                    NumberFormatInfo numInfo = new NumberFormatInfo();
                    numInfo.CurrencySymbol = "R";
                    double saleTaxValue = double.Parse(Request["SaleTaxValue"], NumberStyles.Any, numInfo);

                    obj.SaleTaxValue = saleTaxValue;
                }
                #region Purchase Items
                //Check Purchase Item 
                List<PurchaseItem> oldItemList = new List<PurchaseItem>();
                List<PurchaseItem> newItemList = new List<PurchaseItem>();
                int countItem = int.Parse(Request["hidItem"]);
                for (int i = 1; i <= countItem; i++)
                {
                    if (!string.IsNullOrEmpty(Request["Item" + i]))
                    {
                        //check update Old Purchase Item
                        PurchaseItem oldItem = new PurchaseItem();
                        oldItem.ID = int.Parse(Request["Item" + i]);
                        oldItem.RequestID = obj.ID;
                        oldItem.ItemName = Request["description" + i];
                        oldItem.Quantity = Math.Round(double.Parse(Request["quantity" + i]), Constants.ROUND_NUMBER);
                        oldItem.Price = Math.Round(double.Parse(Request["price" + i]), Constants.ROUND_NUMBER);
                        oldItem.TotalPrice = Math.Round((oldItem.Quantity * oldItem.Price), Constants.ROUND_NUMBER);
                        oldItemList.Add(oldItem);
                    }
                    else
                    {
                        //check insert Purchase Item
                        PurchaseItem newItem = new PurchaseItem();
                        newItem.RequestID = obj.ID;
                        newItem.ItemName = Request["description" + i];
                        newItem.Quantity = Math.Round(double.Parse(Request["quantity" + i]), Constants.ROUND_NUMBER);
                        newItem.Price = Math.Round(double.Parse(Request["price" + i]), Constants.ROUND_NUMBER);
                        newItem.TotalPrice = Math.Round((newItem.Quantity * newItem.Price), Constants.ROUND_NUMBER);
                        newItemList.Add(newItem);
                    }
                }
                #endregion

                msg = dao.Update(obj, oldItemList, newItemList);

                #region Insert Comment
                if (msg.MsgType == MessageType.Info)
                {
                    PurchaseComment objCom = new PurchaseComment();
                    objCom.RequestID = obj.ID;
                    objCom.PostTime = DateTime.Now;
                    objCom.Poster = principal.UserData.UserName;
                    objCom.Contents = objUI.Contents;
                    string serverPath = Server.MapPath(Constants.PERFORMANCE_REVIEW_PATH);

                    Message msgCheck = CheckFileUpload();
                    if (msgCheck == null) //case sussessfully
                    {
                        int y = 0;
                        foreach (string file in Request.Files)
                        {

                            HttpPostedFileBase hpf = Request.Files[y] as HttpPostedFileBase;
                            if (!string.IsNullOrEmpty(Path.GetExtension(hpf.FileName)))
                            {
                                if (String.IsNullOrEmpty(objCom.Contents))
                                {
                                    objCom.Contents = "";
                                }
                                string strReplaceUserName = principal.UserData.UserName.Replace(".", "_");
                                string extension = Path.GetExtension(hpf.FileName);
                                string fileName = Path.GetFileNameWithoutExtension(hpf.FileName);
                                string contractName = principal.UserData.UserID + "_" + strReplaceUserName + "_" + fileName +
                                     "." + DateTime.Now.ToString(Constants.UNIQUE_TIME) + extension;
                                contractName = ConvertUtil.FormatFileName(contractName);
                                string strPath = serverPath + "\\" + contractName;
                                hpf.SaveAs(strPath);
                                objCom.Files += contractName + Constants.FILE_STRING_PREFIX;
                            }
                            y++;
                        }

                    }
                    else
                        msg = msgCheck;

                    if (objCom.Contents != null)
                    {
                        msg = prCommentDao.Insert(objCom);
                    }
                }
                #endregion
                // Send mail when edit successfully
                if (msg.MsgType == MessageType.Info && isSendMail)
                    SendPRMail(obj.ID);
            }

            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return GotoCallerPage();
        }
        #endregion

        #region Group Approval Result
        /// <summary>
        /// Get approval result
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ApprovalResult(string id)
        {
            Message msg = null;
            try
            {

                if (!string.IsNullOrEmpty(id))
                {
                    string[] array = id.Split(Constants.SEPARATE_USER_ADMIN_ID);
                    if (array[0] != null && array[1] != null)
                    {
                        int purchaseID = int.Parse(array[0]);
                        int resolutionID = int.Parse(array[1]);
                        int roleId = int.Parse(array[2]);
                        var principal = HttpContext.User as AuthenticationProjectPrincipal;
                        #region Check permission
                        bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                                purchaseID.ToString(), Constants.ActionType.Update, principal.UserData.Role);
                        if (!check)
                            return RedirectToAction("NotPermissionPopup", "Common");
                        #endregion

                        PurchaseRequest pr = dao.GetByID(purchaseID);
                        ViewData[CommonDataKey.PURCHASE_ID] = purchaseID.ToString();
                        ViewData[CommonDataKey.RESOLUTION_ID] = resolutionID.ToString();
                        List<sp_GetListApprovalAssignResult> listAssign = dao.GetListApprovalAssign(purchaseID);
                        sp_GetListApprovalAssignResult item = new sp_GetListApprovalAssignResult();
                        //Choose default user when purchase request automatic load in drop down list
                        if (resolutionID != Constants.PR_RESOLUTION_TO_BE_APPROVAL_REJECT)//case group approval approve
                        {
                            //Choose default user when purchase request automatic load in drop down list
                            listAssign = listAssign.Where(p => p.ApproverGroup != Constants.PR_SR_MANAGER_ID).ToList<sp_GetListApprovalAssignResult>();
                            int index = listAssign.IndexOf(listAssign.Where(p => p.ApproverGroup == roleId).FirstOrDefault<sp_GetListApprovalAssignResult>());
                            item = listAssign[index + 1];
                            ViewData[CommonDataKey.GROUP_APPROVAL_RESOLUTION] = "true";
                        }
                        else//case group approval reject then purchase requrest load Role Sr Manager
                        {
                            //item = listAssign[listAssign.Count - 2];
                            //choose Sr Manager
                            int index = listAssign.IndexOf(listAssign.Where(p => p.ApproverGroup == Constants.PR_SR_MANAGER_ID).FirstOrDefault<sp_GetListApprovalAssignResult>());
                            item = listAssign[index];
                            ViewData[CommonDataKey.GROUP_APPROVAL_RESOLUTION] = "false";
                        }
                        //create new List to bind selected List
                        List<ListItem> assign = new List<ListItem>();
                        assign.Add(new ListItem(item.DisplayName, item.UserAdminRole));
                        ViewData[CommonDataKey.DDL_PR_ASSIGN] = new SelectList(assign, "Value", "Text", item.UserAdminRole);
                        return View(pr);
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return RedirectToAction("/Index");
        }

        /// <summary>
        /// Approval result
        /// </summary>
        /// <param name="content">FormCollection</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ApprovalResult(FormCollection content)
        {
            Message msg = null;
            string id = content["RequestId"];

            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                #region Check permission
                bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                        id, Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return RedirectToAction("NotPermission", "Common");
                #endregion

                string contents = content["Contents"];

                string userAdminRole = content["Assign"];
                PurchaseRequest purchase = dao.GetByID(int.Parse(id));
                if (purchase.UpdateDate.ToString() == content["UpdateDate"])
                {
                    string[] array = (userAdminRole).Split(Constants.SEPARATE_USER_ADMIN_ID);
                    #region Purchase Request Comment
                    if (!string.IsNullOrEmpty(contents))
                    {
                        PurchaseComment obj = new PurchaseComment();
                        obj.RequestID = int.Parse(id);
                        obj.PostTime = DateTime.Now;
                        obj.Poster = principal.UserData.UserName;
                        obj.Contents = contents;
                        prCommentDao.Insert(obj);
                    }
                    #endregion
                    #region Update Purchase Request
                    int assignID = int.Parse(array[0]);
                    int assignRole = int.Parse(array[1]);
                    if (assignRole != Constants.PR_SR_MANAGER_ID)//case when group approval approve
                    {
                        if (assignRole != Constants.PR_PURCHASING_ID)//case when group approval approve then pr automatic send next approval 
                        {
                            List<sp_GetResolutionByRoleResult> resolutionList = dao.GetResolutionByRole(assignRole);
                            resolutionList = resolutionList.Where(p => p.WFResolutionID != Constants.PR_RESOLUTION_TO_BE_APPROVAL_APPROVE &&
                                p.WFResolutionID != Constants.PR_RESOLUTION_TO_BE_APPROVAL_REJECT).ToList<sp_GetResolutionByRoleResult>();
                            purchase.WFResolutionID = resolutionList[0].WFResolutionID;
                        }
                        else
                        {
                            //case when group approval approve then pr automatic send group purchasing fill data
                            List<PurchaseRequestApproval> lstPurcharseApp = dao.GetPurchaseRequestApprovalImmediately(purchase.ID);
                            if (lstPurcharseApp == null || lstPurcharseApp.Count == 0)
                                purchase.WFResolutionID = Constants.PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING;
                            else
                                purchase.WFResolutionID = Constants.PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING_IMMEDIATELY;
                        }
                        purchase.InvolveResolution += Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                    }
                    else
                    {
                        //case when group approval reject
                        purchase.WFResolutionID = Constants.PR_RESOLUTION_TO_BE_APPROVAL_REJECT;
                        purchase.InvolveResolution += Constants.PR_ACTION_REJECT + Constants.SEPARATE_INVOLVE_SIGN;
                    }

                    purchase.AssignID = assignID;
                    purchase.AssignRole = assignRole;
                    purchase.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                    purchase.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                    purchase.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                    purchase.UpdatedBy = principal.UserData.UserName;
                    msg = dao.UpdateForSetupApproval(purchase, new List<PurchaseRequestApproval>(), false);
                    #endregion
                    if (msg.MsgType == MessageType.Info)
                        SendPRMail(purchase.ID);
                }
                else
                {
                    msg = new Message(MessageConstants.E0025, MessageType.Error, "Purchase Request '" + Constants.PR_REQUEST_PREFIX + id + "'");
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return GotoCallerPage();
            //return TempData[CommonDataKey.RETURN_URL] as RedirectToRouteResult;
        }
        public ActionResult GotoCallerPage()
        {
            if (Request.Params.AllKeys.Contains(CommonDataKey.RETURN_URL))
                return Redirect(Request[CommonDataKey.RETURN_URL]);
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region Purchasing Confirm
        /// <summary>
        /// Purchasing confirm
        /// </summary>
        /// <param name="id">id of purchase request</param>
        /// <returns>ActionResult</returns>
        public ActionResult PurchasingConFirm(string id)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                #region Check permission
                bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                        id, Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return RedirectToAction("NotPermission", "Common");
                #endregion

                int purchaseId = 0;
                if (Int32.TryParse(id, out purchaseId))//case check id exitsted in DB
                {
                    PurchaseRequest obj = dao.GetByID(purchaseId);
                    if (obj != null)
                    {
                        ViewData[CommonDataKey.DEPARTMENT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", deptDao.GetDepartmentIdBySub(obj.SubDepartment));
                        ViewData[CommonDataKey.SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", obj.SubDepartment);
                        ViewData[CommonDataKey.PURCHASE_REQUEST_PRIORITY] = new SelectList(Constants.PURCHASE_REQUEST_PRIORITY,
                            "value", "text", obj.Priority.HasValue ? obj.Priority : 0);
                        ViewData[CommonDataKey.SALE_TAX_NAME] = new SelectList(Constants.SaleTax, "Value", "Text", obj.SaleTaxName);
                        //Get All Resolution List by Role
                        List<WFResolution> listResolution = resDao.GetListResolutionChangeByRole(principal.UserData.Role, true, Constants.WORK_FLOW_PURCHASE_REQUEST);
                        //filter resolution list by PR resolution
                        //case when PR reject by SR Manager
                        if (obj.WFResolutionID == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING)
                        {
                            listResolution = listResolution.Where(p => p.ID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_SR_MANAGER
                                || p.ID == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING || p.ID == Constants.PR_RESOLUTION_REJECT).ToList<WFResolution>();
                        }
                        else
                        {
                            //case Requestor send PR to Group Purchasing
                            listResolution = listResolution.Where(p => p.ID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING || p.ID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_SR_MANAGER
                               || p.ID == Constants.PR_RESOLUTION_REJECT).ToList<WFResolution>();
                        }
                        ViewData[CommonDataKey.DDL_PR_RESOLUTION] = new SelectList(listResolution, "ID", "Name", obj.WFResolutionID);
                        ViewData[CommonDataKey.DDL_PR_STATUS] = new SelectList(staDao.GetListStatusByResolution(Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING), "ID", "Name");
                        ViewData[CommonDataKey.DDL_PR_ASSIGN] = new SelectList(dao.GetListAssign(obj.WFResolutionID, Constants.WORK_FLOW_PURCHASE_REQUEST), "UserAdminRole", "DisplayName", principal.UserData.UserID + Constants.SEPARATE_USER_ADMIN_ID_STRING + Constants.PR_PURCHASING_ID.ToString());
                        List<PurchaseItem> list = dao.GetListItemsByPurchaseID(purchaseId);
                        ViewData[CommonDataKey.SUB_TOTAL_ITEM] = Math.Round(dao.GetSubTotalByPurchaseID(purchaseId).Value, Constants.ROUND_NUMBER);
                        ViewData[CommonDataKey.LIST_PURCHASE_ITEM] = list;
                        ViewData[CommonDataKey.COUNT_PURCHASE_ITEM] = list.Count;
                        ViewData[CommonDataKey.PR_SELECTED_REQUESTOR] = obj.Requestor +
                            Constants.SEPARATE_USER_ADMIN_ID_STRING + Constants.PR_REQUESTOR_ID;
                        ViewData["UserRole"] = principal.UserData.Role.ToString();
                        return View(obj);
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                ShowMessage(msg);
            }
            return RedirectToAction("/Index");
        }

        /// <summary>
        /// Purchasing confirm (post)
        /// </summary>
        /// <param name="obj">PurchaseRequest</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PurchasingConFirm(PurchaseRequest obj)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;

                #region Check permission
                bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                        obj.ID.ToString(), Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return RedirectToAction("NotPermission", "Common");
                #endregion

                #region Update Purchase Request
                obj.BillableToClient = bool.Parse(Request["chk_BillableGroup"]);
                obj.MoneyType = int.Parse(Request["chk_MoneyTypeGroup"]);
                obj.PaymentID = int.Parse(Request["chk_Payment"]);
                string userAdminRole = Request["Assign"];
                PurchaseRequest objDb = dao.GetByID(obj.ID);
                bool isSendMail = false;
                string[] arr = userAdminRole.Split(Constants.SEPARATE_USER_ADMIN_ID);
                int assignID = int.Parse(arr[0].Trim());
                int assignRole = int.Parse(arr[1].Trim());
                obj.AssignID = assignID;
                obj.AssignRole = assignRole;

                if (obj.WFResolutionID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING)//case edit
                {
                    if ((assignID != objDb.AssignID.Value) && (assignRole == Constants.PR_PURCHASING_ID))//case send PR to another Group Purchasing
                    {
                        obj.InvolveResolution += Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                        obj.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                        obj.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                        obj.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                        isSendMail = true;
                    }
                }
                else if (obj.WFResolutionID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_SR_MANAGER) //case send PR for SR Manager
                {
                    obj.InvolveResolution += Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                    obj.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                    obj.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                    obj.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                    isSendMail = true;
                }
                else if (obj.WFResolutionID == Constants.PR_RESOLUTION_REJECT)//case Reject PR
                {
                    obj.InvolveResolution += Constants.PR_ACTION_REJECT + Constants.SEPARATE_INVOLVE_SIGN;
                    obj.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                    obj.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                    obj.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                    isSendMail = true;
                }
                obj.UpdatedBy = principal.UserData.UserName;
                if (obj.SaleTaxName == "0")//case select no sale tax
                {
                    obj.SaleTaxValue = 0;
                }
                else
                {
                    NumberFormatInfo numInfo = new NumberFormatInfo();
                    numInfo.CurrencySymbol = "R";
                    double saleTaxValue = double.Parse(Request["SaleTaxValue"], NumberStyles.Any, numInfo);

                    obj.SaleTaxValue = saleTaxValue;
                }
                #endregion
                #region Update Purchase Item
                List<PurchaseItem> oldItemList = new List<PurchaseItem>();
                List<PurchaseItem> newItemList = new List<PurchaseItem>();
                int countItem = int.Parse(Request["hidItem"]);
                for (int i = 1; i <= countItem; i++)
                {
                    if (!string.IsNullOrEmpty(Request["Item" + i]))//case update old purchase item
                    {
                        PurchaseItem oldItem = new PurchaseItem();
                        oldItem.ID = int.Parse(Request["Item" + i]);
                        oldItem.RequestID = obj.ID;
                        oldItem.ItemName = Request["description" + i];
                        oldItem.Quantity = Math.Round(double.Parse(Request["quantity" + i]), Constants.ROUND_NUMBER);
                        oldItem.Price = Math.Round(double.Parse(Request["price" + i]), Constants.ROUND_NUMBER);
                        oldItem.TotalPrice = Math.Round((oldItem.Quantity * oldItem.Price), Constants.ROUND_NUMBER);
                        oldItemList.Add(oldItem);
                    }
                    else
                    {
                        //case insert new purchase item
                        PurchaseItem newItem = new PurchaseItem();
                        newItem.RequestID = obj.ID;
                        newItem.ItemName = Request["description" + i];
                        newItem.Quantity = Math.Round(double.Parse(Request["quantity" + i]), Constants.ROUND_NUMBER);
                        newItem.Price = Math.Round(double.Parse(Request["price" + i]), Constants.ROUND_NUMBER);
                        newItem.TotalPrice = Math.Round((newItem.Quantity * newItem.Price), Constants.ROUND_NUMBER);
                        newItemList.Add(newItem);
                    }
                }
                #endregion
                msg = dao.Update(obj, oldItemList, newItemList);
                if (msg.MsgType == MessageType.Info && isSendMail)
                {
                    SendPRMail(obj.ID);
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return GotoCallerPage();
        }
        #endregion

        #region Sr Manager Reject
        /// <summary>
        /// SR Manager reject
        /// </summary>
        /// <param name="id">id of purchase request</param>
        /// <returns>ActionResult</returns>
        public ActionResult SrManagerReject(string id)
        {
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                #region Check permission
                bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                        id, Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return RedirectToAction("NotPermissionPopup", "Common");
                #endregion

                PurchaseRequest pr = dao.GetByID(int.Parse(id));
                ViewData[CommonDataKey.PURCHASE_ID] = id;
                List<WFResolution> list = resDao.GetListResolutionChangeByRole(Constants.PR_SR_MANAGER_ID);
                ViewData[CommonDataKey.DDL_PR_RESOLUTION] = new SelectList(list, "ID", "Name", Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING);
                List<sp_GetListAssignByResolutionIdResult> listAssign = dao.GetListAssign(Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING, Constants.WORK_FLOW_PURCHASE_REQUEST).ToList<sp_GetListAssignByResolutionIdResult>();
                ViewData[CommonDataKey.DDL_PR_ASSIGN] = new SelectList(listAssign, "UserAdminRole", "DisplayName", "");
                ViewData[CommonDataKey.PR_SELECTED_PURCHASING] = dao.GetLastInvolveIdByRole(pr, Constants.PR_PURCHASING_ID.ToString()) +
                    Constants.SEPARATE_USER_ADMIN_ID_STRING + Constants.PR_PURCHASING_ID;
                ViewData[CommonDataKey.PR_SELECTED_REQUESTOR] = pr.Requestor +
                    Constants.SEPARATE_USER_ADMIN_ID_STRING + Constants.PR_REQUESTOR_ID;
                return View(pr);
            }
            catch
            {
                Message msg = new Message(MessageConstants.E0007, MessageType.Error);
                ShowMessage(msg);
                return RedirectToAction("/Index");
            }
        }

        /// <summary>
        /// Manager reject
        /// </summary>
        /// <param name="content">Formcollection</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SrManagerReject(FormCollection content)
        {
            Message msg = null;
            string id = content["RequestId"];
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;

                #region Check permission
                bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                        content["RequestId"], Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return RedirectToAction("NotPermission", "Common");
                #endregion

                string contents = content["Contents"];

                string userAdminRole = content["Assign"];
                PurchaseRequest purchase = dao.GetByID(int.Parse(id));
                if (purchase.UpdateDate.ToString() == content["UpdateDate"])
                {
                    if (!string.IsNullOrEmpty(userAdminRole))
                    {
                        string[] array = (userAdminRole).Split(Constants.SEPARATE_USER_ADMIN_ID);
                        #region Purchase Request Comment
                        PurchaseComment obj = new PurchaseComment();
                        obj.RequestID = int.Parse(id);
                        obj.PostTime = DateTime.Now;
                        obj.Poster = principal.UserData.UserName;
                        obj.Contents = contents;
                        prCommentDao.Insert(obj);
                        #endregion
                        #region Update Purchase Request
                        int assignId = int.Parse(array[0]);
                        int assignRole = int.Parse(array[1]);
                        int resolution = int.Parse(content["WFResolutionID"]);
                        if (resolution == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING)//case when Sr Manager reject and send to group Purchasing
                        {
                            purchase.WFResolutionID = Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING;
                        }
                        else
                        {
                            //case when Sr Manager reject and send to group Requestor
                            purchase.WFResolutionID = Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR;
                        }
                        purchase.InvolveResolution += Constants.PR_ACTION_REJECT + Constants.SEPARATE_INVOLVE_SIGN;
                        purchase.AssignID = assignId;
                        purchase.AssignRole = assignRole;
                        purchase.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                        purchase.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                        purchase.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                        purchase.UpdatedBy = principal.UserData.UserName;
                        msg = dao.UpdateForSetupApproval(purchase, new List<PurchaseRequestApproval>(), false);
                        #endregion
                        if (msg.MsgType == MessageType.Info)
                            SendPRMail(purchase.ID);
                    }
                }
                else
                {
                    msg = new Message(MessageConstants.E0025, MessageType.Error, "Purchase Request '" + Constants.PR_REQUEST_PREFIX + id + "'");
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return GotoCallerPage();
        }
        #endregion

        #region Purchasing Fill Data
        /// <summary>
        /// Fill data
        /// </summary>
        /// <param name="id">id of purchase request</param>
        /// <returns>ActionResult</returns>
        public ActionResult FillData(string id)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;

                #region Check permission
                bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                        id, Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return RedirectToAction("NotPermission", "Common");
                #endregion

                int purchaseId = 0;
                if (Int32.TryParse(id, out purchaseId))//check id existed in DB
                {
                    sp_GetPurchaseRequestResult obj = dao.GetPurchaseRequestByID(id);
                    if (obj != null)
                    {
                        ViewData[CommonDataKey.DEPARTMENT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", deptDao.GetDepartmentIdBySub(obj.SubDepartment));
                        ViewData[CommonDataKey.SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", obj.SubDepartment);
                        ViewData[CommonDataKey.PURCHASE_REQUEST_PRIORITY] = new SelectList(Constants.PURCHASE_REQUEST_PRIORITY,
                            "value", "text", obj.Priority.HasValue ? obj.Priority : 0);
                        ViewData[CommonDataKey.SALE_TAX_NAME] = new SelectList(Constants.SaleTax, "Value", "Text", obj.SaleTaxName);
                        //Get Resolution List when Purchasing Fill Data
                        List<WFResolution> resolutionList = resDao.GetListResolutionChangeByRole(principal.UserData.Role);
                        if (obj.BillableToClient)
                        {
                            resolutionList = resolutionList.Where(c => c.ID == Constants.PR_RESOLUTION_TO_BE_PROCESSED
                                   || c.ID == obj.WFResolutionID).ToList<WFResolution>();
                        }
                        //resolutionList = resolutionList.Where(c => c.ID != Constants.PR_RESOLUTION_NEW_ID
                        //       && c.ID != Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_SR_MANAGER
                        //       && c.ID != Constants.PR_RESOLUTION_REJECT && c.ID != Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING
                        //       && c.ID != Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING).ToList<WFResolution>();
                        else
                        {

                            resolutionList = resolutionList.Where(c => c.ID == obj.WFResolutionID
                                    || c.ID == Constants.PR_RESOLUTION_COMPLETE_ID
                                    || c.ID == Constants.PR_RESOLUTION_NOT_COMPLETE
                                    || c.ID == Constants.PR_RESOLUTION_COMPLETE_WAITING_CLOSE_ID // tan.tran add new 2011.06.27, new request
                                   ).ToList<WFResolution>();
                        }
                        ViewData[CommonDataKey.DDL_PR_RESOLUTION] = new SelectList(resolutionList, "ID", "Name", obj.WFResolutionID);
                        ViewData[CommonDataKey.DDL_PR_STATUS] = new SelectList(staDao.GetListStatusByResolution(obj.WFResolutionID), "ID", "Name");
                        ViewData[CommonDataKey.DDL_PR_ASSIGN] = new SelectList(dao.GetListAssign(obj.WFResolutionID, Constants.WORK_FLOW_PURCHASE_REQUEST), "UserAdminRole", "DisplayName", principal.UserData.UserID + Constants.SEPARATE_USER_ADMIN_ID_STRING + Constants.PR_PURCHASING_ID.ToString());
                        ViewData[CommonDataKey.SUB_TOTAL_ITEM] = Math.Round(dao.GetSubTotalByPurchaseID(purchaseId).Value, Constants.ROUND_NUMBER);
                        List<PurchaseInvoice> listInvoice = dao.GetPurchaseInvoice(purchaseId);
                        if (listInvoice.Count > 0)
                        {
                            ViewData[CommonDataKey.LIST_INVOICE] = listInvoice;
                        }
                        List<PurchaseItem> list = dao.GetListItemsByPurchaseID(purchaseId);
                        ViewData[CommonDataKey.LIST_PURCHASE_ITEM] = list;
                        ViewData[CommonDataKey.COUNT_PURCHASE_ITEM] = list.Count;
                        List<sp_GetListApprovalAssignResult> listApproval = dao.GetListApprovalAssign(purchaseId);
                        //Filter exception 2 Role Purchasing,SR Manager
                        listApproval = listApproval.Where(p => p.ApproverGroup != Constants.PR_PURCHASING_ID && p.ApproverGroup != Constants.PR_SR_MANAGER_ID).ToList<sp_GetListApprovalAssignResult>();
                        if (listApproval.Count > 0)
                        {
                            listApproval = listApproval.Where(p => p.ApproverGroup != Constants.PR_SR_MANAGER_ID && p.ApproverGroup != Constants.PR_PURCHASING_ID)
                                .OrderBy(p => p.ApprovalID).ToList<sp_GetListApprovalAssignResult>();
                            ViewData[CommonDataKey.LIST_APPROVAL] = listApproval;
                        }
                        return View(obj);
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                ShowMessage(msg);
            }
            return RedirectToAction("/Index");
        }

        /// <summary>
        /// Fill data (post)
        /// </summary>
        /// <param name="collection">FormCollection</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FillData(FormCollection collection)
        {
            Message msg = null;
            int id = int.Parse(collection["ReqID"]);
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;

                #region Check permission
                bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_PURCHASE_REQUEST,
                        collection["ReqID"], Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return RedirectToAction("NotPermission", "Common");
                #endregion

                PurchaseRequest objDb = dao.GetByID(id);
                if (objDb.UpdateDate.ToString() == collection["UpdateDate"])
                {
                    bool isSendMail = false;
                    #region Update PR
                    if (objDb != null)//check id existed in db
                    {
                        int resolution = 0;
                        if (!string.IsNullOrEmpty(Request["WFResolutionID"]))
                        {
                            resolution = int.Parse(Request["WFResolutionID"]);
                        }
                        string userAdminRole = Request["Assign"];
                        int status = int.Parse(Request["WFStatusID"]);
                        objDb.PurchaseAppoval = collection["PurchaseAppoval"];
                        objDb.PaymentAppoval = collection["PaymentAppoval"];
                        if (objDb.BillableToClient) // case billable to client
                        {
                            if (!string.IsNullOrEmpty(userAdminRole))//
                            {
                                if (objDb.WFResolutionID != resolution)
                                {
                                    objDb.InvolveResolution += Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                                    objDb.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                                    objDb.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                                    objDb.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                                    objDb.AssignID = int.Parse(userAdminRole);
                                    objDb.WFResolutionID = resolution;
                                    objDb.AssignRole = Constants.PR_US_ACCOUNTING;
                                    isSendMail = true;
                                }
                                else
                                {
                                    string[] array = userAdminRole.Split(Constants.SEPARATE_USER_ADMIN_ID);
                                    int assignID = int.Parse(array[0].Trim());
                                    int assignRole = int.Parse(array[1].Trim());
                                    if (objDb.AssignID != assignID)
                                    {
                                        objDb.InvolveResolution += Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                                        objDb.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                                        objDb.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                                        objDb.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                                        objDb.AssignID = assignID;
                                        objDb.AssignRole = assignRole;
                                        isSendMail = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(userAdminRole))//case when purchasing edit,or assign another purchasing
                            {
                                string[] array = userAdminRole.Split(Constants.SEPARATE_USER_ADMIN_ID);
                                int assignID = int.Parse(array[0].Trim());
                                int assignRole = int.Parse(array[1].Trim());

                                if (resolution == Constants.PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING ||
                                    resolution == Constants.PR_RESOLUTION_COMPLETE_WAITING_CLOSE_ID)//case send PR for another Purchasing
                                {
                                    if (!((assignID == principal.UserData.UserID) && (assignRole) == Constants.PR_PURCHASING_ID
                                        && resolution == objDb.WFResolutionID))
                                    {
                                        objDb.InvolveResolution += Constants.PR_ACTION_FORWARDTO + Constants.SEPARATE_INVOLVE_SIGN;
                                        objDb.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                                        objDb.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                                        objDb.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                                        objDb.AssignID = assignID;
                                        objDb.AssignRole = assignRole;
                                        objDb.WFResolutionID = resolution; // tan.tran add new 2011.06.27
                                        isSendMail = true;
                                    }
                                }
                            }
                            else
                            {
                                if (resolution == Constants.PR_RESOLUTION_NOT_COMPLETE)//case cancel PR
                                {
                                    objDb.InvolveResolution += Constants.PR_ACTION_CLOSE + Constants.SEPARATE_INVOLVE_SIGN;
                                    objDb.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                                    objDb.WFResolutionID = Constants.PR_RESOLUTION_NOT_COMPLETE;
                                    objDb.WFStatusID = status;
                                    isSendMail = true;
                                }
                                else if (resolution == Constants.PR_RESOLUTION_COMPLETE_ID)// case completed PR
                                {
                                    objDb.InvolveResolution += Constants.PR_ACTION_CLOSE + Constants.SEPARATE_INVOLVE_SIGN;
                                    objDb.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                                    objDb.WFResolutionID = Constants.PR_RESOLUTION_COMPLETE_ID;
                                    objDb.WFStatusID = status;
                                    isSendMail = true;
                                }
                            }
                        }

                    }
                    #endregion
                    #region Insert Invoice
                    List<PurchaseInvoice> listInvoice = new List<PurchaseInvoice>();
                    int rowInvoice = int.Parse(collection["hidValue"]);
                    for (int i = 1; i <= rowInvoice; i++)
                    {
                        PurchaseInvoice item = new PurchaseInvoice();
                        item.RequestID = id;
                        if (collection["ivcDate" + i] != "" && collection["ivcNumber" + i] != "" && collection["ivcValue" + i] != "")
                        {
                            string invoiceDate = collection["ivcDate" + i];
                            if (!string.IsNullOrEmpty(invoiceDate))
                            {
                                item.InvoiceDate = DateTime.Parse(invoiceDate);
                            }
                            item.InvoiceNumber = collection["ivcNumber" + i];
                            item.InvoiceValue = collection["ivcValue" + i];
                            listInvoice.Add(item);
                        }
                    }
                    #endregion
                    msg = dao.UpdateConfirm(objDb, listInvoice);
                    // Send mail if update successfully
                    if (msg.MsgType == MessageType.Info && isSendMail)
                    {
                        SendPRMail(objDb.ID);
                    }
                }
                else
                {
                    msg = new Message(MessageConstants.E0025, MessageType.Error, "Purchase Request '" + Constants.PR_REQUEST_PREFIX + id + "'");
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return GotoCallerPage();
        }
        #endregion

        #endregion

        #region Send Purchase E-mail : tan.tran 2010.12.22
        /// <summary>
        /// Send Purchase Request Mail
        /// </summary>
        /// <param name="prId">purchase request id</param>
        protected void SendPRMail(int prId)
        {
            // Get body detail
            PurchaseRequestDao purDao = new PurchaseRequestDao();
            PurchaseRequest purReq = dao.GetByID(prId);
            WFRole role = null;
            if (purReq == null)
            {
                return;
            }
            else
            {
                role = roleDao.GetByID(purReq.AssignRole.Value);
            }

            string from_email = ConfigurationManager.AppSettings["from_email"];
            string host = ConfigurationManager.AppSettings["mailserver_host"];
            string port = ConfigurationManager.AppSettings["mailserver_port"];
            string poster = "Purchase Request";
            string subject = string.Empty;
            if (purReq.WFStatusID == Constants.STATUS_CLOSE)
            {
                subject = "[CRM-PR] " + Constants.PR_REQUEST_PREFIX + purReq.ID + " has been closed";
            }
            else
            {
                if (role != null)
                    subject = "[CRM-PR] " + Constants.PR_REQUEST_PREFIX + purReq.ID + " has been forwarded to " + purReq.UserAdmin.UserName + " (" + role.Name + ")";
            }

            string body = CreateBodyOfEmail(purReq, role);

            string to_email = string.Empty;
            string cc_email = string.Empty;
            string[] arrIds = purReq.InvolveID.Split(Constants.SEPARATE_INVOLVE_CHAR);
            string[] arrRoles = purReq.InvolveRole.Split(Constants.SEPARATE_INVOLVE_CHAR);

            List<string> sendList = new List<string>();
            List<string> duplicateEmail = new List<string>();

            for (int i = 0; i < arrIds.Length - 1; i++)
            {
                //check duplicate person on user name and role.
                if (!sendList.Contains(arrIds[i] + Constants.SEPARATE_INVOLVE_SIGN + arrRoles[i]))
                {
                    sendList.Add(arrIds[i] + Constants.SEPARATE_INVOLVE_SIGN + arrRoles[i]);
                    UserAdmin_WFRole user = jrAdminDao.GetByUserAdminId(int.Parse(arrIds[i]));
                    if (user != null)
                    {
                        if (purReq.WFStatusID == Constants.STATUS_CLOSE) //If an email has "Close" status, just only send by "To" section, not cc 
                        {
                            if (!duplicateEmail.Contains(user.UserAdmin.UserName + Constants.LOGIGEAR_EMAIL_DOMAIN + ";"))
                            {
                                duplicateEmail.Add(user.UserAdmin.UserName + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                                to_email += user.UserAdmin.UserName + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                            }
                        }
                        else
                        {
                            //Just send by "To" section only to person who has been assigned, the involved others are by "CC" section.
                            if (string.IsNullOrEmpty(to_email))
                            {
                                UserAdmin_WFRole currentAssign = jrAdminDao.GetByUserAdminId(purReq.AssignID.Value);
                                if (currentAssign != null)
                                {
                                    to_email = currentAssign.UserAdmin.UserName + Constants.LOGIGEAR_EMAIL_DOMAIN;
                                    cc_email = user.UserAdmin.UserName + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                                    duplicateEmail.Add(user.UserAdmin.UserName + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                                    duplicateEmail.Add(currentAssign.UserAdmin.UserName + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                                }
                            }
                            else //make a cc list mail send.
                            {
                                if (!duplicateEmail.Contains(user.UserAdmin.UserName + Constants.LOGIGEAR_EMAIL_DOMAIN + ";"))
                                {
                                    duplicateEmail.Add(user.UserAdmin.UserName + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                                    cc_email += user.UserAdmin.UserName + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                                }
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(purReq.CCList))
            {
                string[] array = purReq.CCList.Split(';');
                foreach (string item in array)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string userAdminID = userAdminDao.GetByUserName(item).UserAdminId.ToString();
                        if (!duplicateEmail.Contains(item + Constants.LOGIGEAR_EMAIL_DOMAIN + ";"))
                        {
                            duplicateEmail.Add(item + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                            cc_email += item + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(to_email))
                WebUtils.SendMail(host, port, from_email, poster, to_email, cc_email, subject, body);
        }

        /// <summary>
        /// Create body of email
        /// </summary>
        /// <param name="purReq">PurchaseRequest</param>
        /// <param name="role">WFRole</param>
        /// <returns>string</returns>
        private string CreateBodyOfEmail(PurchaseRequest purReq, WFRole role)
        {
            string path = string.Empty;

            //load template emails by pr status            
            path = Server.MapPath("~/Views/PurchaseRequest/TemplateMail.htm");
            string content = WebUtils.ReadFile(path);

            //replace the holders on template emails.
            if (purReq != null && role != null)
            {
                // create message
                content = content.Replace(Constants.PR_REQUEST_ID_HOLDER, purReq.ID.ToString());
                content = content.Replace(Constants.PR_REQUEST_DATE_HOLDER, purReq.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                content = content.Replace(Constants.PR_EXPECTED_DATE_HOLDER, purReq.ExpectedDate.HasValue ? purReq.ExpectedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "");
                content = content.Replace(Constants.PR_REQUESTOR_HOLDER, purReq.UserAdmin.UserName);
                content = content.Replace(Constants.PR_ASSIGN_TO_HOLDER, new UserAdminDao().GetById((int)purReq.AssignID).UserName);
                content = content.Replace(Constants.PR_DEPARMENT_HOLDER, deptDao.GetDepartmentNameBySub(purReq.SubDepartment));
                content = content.Replace(Constants.PR_SUBDEPARMENT_HOLDER, purReq.Department.DepartmentName);
                content = content.Replace(Constants.PR_RESOLUTION_HOLDER, purReq.WFResolution.Name);
                content = content.Replace(Constants.PR_STATUS_HOLDER, new WFStatusDao().GetByID(purReq.WFStatusID).Name);
                content = content.Replace(Constants.PR_JUSTIFICATION_HOLDER, HttpUtility.HtmlEncode(purReq.Justification));
                content = content.Replace(Constants.PR_BILLABLE_TO_CLIENT_HOLDER, purReq.BillableToClient ? "Yes" : "No");
                string link = string.Empty;
                if (purReq.WFStatusID == Constants.STATUS_CLOSE)
                {
                    content = content.Replace(Constants.PR_FORWARD_TO_HOLDER, " has been closed");
                    content = content.Replace(Constants.PR_FORWARD_TO_NAME_HOLDER, "");
                    link = "http://" + Request["SERVER_NAME"] + ":" + Request["SERVER_PORT"] + "/PurchaseRequest" + "/Detail/" + purReq.ID;
                }
                else
                {

                    content = content.Replace(Constants.PR_FORWARD_TO_HOLDER, " has been forwarded to you");
                    content = content.Replace(Constants.PR_FORWARD_TO_NAME_HOLDER, "Forwarded To: " + new UserAdminDao().GetById((int)purReq.AssignID).UserName + " (" + purReq.WFRole.Name + " )");
                    link = "http://" + Request["SERVER_NAME"] + ":" + Request["SERVER_PORT"] + "/PurchaseRequest" + "/?role=" + purReq.AssignRole;
                }

                content = content.Replace(Constants.PR_LINK_HOLDER, link);
            }

            return content;
        }

        public ActionResult SendAnEmail(string ids, string page)
        {

            sp_GetPurchaseRequestResult purReq = dao.GetPurchaseRequestByID(ids.ToString());

            ViewData["template"] = CreateMailContent(purReq, ids);
            ViewData["Page"] = page;

            return View(purReq);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SendAnEmail(FormCollection form)
        {
            string host = ConfigurationManager.AppSettings["mailserver_host"];
            string port = ConfigurationManager.AppSettings["mailserver_port"];

            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string id = Request["ID"];
            if (!string.IsNullOrEmpty(id))
            {

                string from = CommonFunc.GetEmailByLoginName(principal.UserData.UserName);
                string fromName = CommonFunc.GetDomainUser(principal.UserData.UserName).Properties["displayName"][0].ToString();

                string[] toName = form.Get("To").Trim().Split(';');
                string toMail = "";
                foreach (string name in toName)
                {
                    if (!String.IsNullOrWhiteSpace(name))
                        toMail += CommonFunc.GetEmailByLoginName(name) + ";";
                }

                string[] ccName = form.Get("CC").Trim().Split(';');
                string ccMail = "";
                foreach (string name in ccName)
                {
                    if (!String.IsNullOrWhiteSpace(name))
                        ccMail += CommonFunc.GetEmailByLoginName(name) + ";";
                }
                string body = form.Get("body");
                bool result = WebUtils.SendMail(host, port, from, fromName, toMail, ccMail, form.Get("Subject"), body);
                Message msg = null;
                if (result)
                {
                    msg = new Message(MessageConstants.I0002, MessageType.Info);
                }
                else
                {
                    msg = new Message(MessageConstants.E0032, MessageType.Error);
                }

                ShowMessage(msg);
            }
            return RedirectToAction("/Detail/" + id);
        }
        private string CreateMailContent(sp_GetPurchaseRequestResult purReq, string ids)
        {

            string content = string.Empty;

            string tmpFilePath = Server.MapPath(Constants.PR_TEMPLATE_MAIL_PATH);

            if (System.IO.File.Exists(tmpFilePath))
            {
                content = System.IO.File.ReadAllText(tmpFilePath);

                if (purReq != null)
                {
                    // create PR infor 

                    content = content.Replace("[#Requestor]", HttpUtility.HtmlEncode(purReq.RequestorName));
                    content = content.Replace("[#Department]", HttpUtility.HtmlEncode(purReq.Department));
                    content = content.Replace("[#Sub Department]", HttpUtility.HtmlEncode(purReq.SubDepartmentName));
                    content = content.Replace("[#Request Date]", HttpUtility.HtmlEncode(purReq.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW)));
                    content = content.Replace("[#Expected Date]", HttpUtility.HtmlEncode(purReq.ExpectedDate.HasValue ? purReq.ExpectedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : ""));
                    string sPriority = String.Empty;
                    if (purReq.Priority.HasValue)
                        sPriority = Constants.PURCHASE_REQUEST_PRIORITY.Where(p => int.Parse(p.Value) == purReq.Priority.Value).FirstOrDefault().Text;

                    content = content.Replace("[#Priority]", "<b style='color:red'>" + sPriority + "</b>");
                    content = content.Replace("[#Status]", HttpUtility.HtmlEncode(purReq.ResolutionName));
                    content = content.Replace("[#Forwarded To]", HttpUtility.HtmlEncode(purReq.AssignName));
                    content = content.Replace("[#Billable To Client]", HttpUtility.HtmlEncode(purReq.BillableToClient ? "Yes" : "No"));
                    content = content.Replace("[#Justification]", HttpUtility.HtmlEncode(purReq.Justification != null ? purReq.Justification.Replace("\r\n", "<br/>") : "&nbsp;"));
                    content = content.Replace("[#Vendor]", HttpUtility.HtmlEncode(purReq.VendorName != null ? purReq.VendorName : "&nbsp;"));
                    content = content.Replace("[#Phone]", HttpUtility.HtmlEncode(purReq.VendorPhone != null ? purReq.VendorPhone : "&nbsp;"));
                    content = content.Replace("[#Email]", HttpUtility.HtmlEncode(purReq.VendorEmail != null ? purReq.VendorEmail : "&nbsp;"));
                    content = content.Replace("[#Address]", HttpUtility.HtmlEncode(purReq.VendorAddress != null ? purReq.VendorAddress : "&nbsp;"));
                    //Create item infor
                    string PaymentMethod = String.Empty;
                    if (purReq.PaymentID == Constants.TYPE_PAYMENT_CASH)
                        PaymentMethod = Constants.TYPE_PAYMENT_CASH_STRING;
                    else if (purReq.PaymentID == Constants.TYPE_PAYMENT_TRANFER)
                        PaymentMethod = Constants.TYPE_PAYMENT_TRANFER_STRING;
                    content = content.Replace("[#Payment Method]", HttpUtility.HtmlEncode(PaymentMethod));
                    content = content.Replace("[#Money Type]", HttpUtility.HtmlEncode(purReq.MoneyType == Constants.TYPE_MONEY_USD ? Constants.TYPE_MONEY_USD_STRING : Constants.TYPE_MONEY_VND_STRING));
                    //Create item rows
                    double result = 0;
                    List<PurchaseItem> itemList;
                    itemList = dao.GetPurchaseRequestItems(Convert.ToInt32(ids));
                    List<PurchaseInvoice> invoiceList = dao.GetPurchaseInvoice(Convert.ToInt32(ids));


                    if (itemList != null)
                    {
                        List<PurchaseItem> items = (List<PurchaseItem>)itemList;
                        int count = 1;
                        string strRow = String.Empty;
                        foreach (PurchaseItem item in items)
                        {
                            string str = "<tr>";
                            result += item.TotalPrice;
                            str += "<td style='padding-left:5px;padding-right:10px;min-width: 200px;'>" + count + "</td>"
                             + "<td  style='padding-left:5px;padding-right:10px;min-width: 200px;'>" + HttpUtility.HtmlEncode(item.ItemName) + "</td>"
                             + "<td  style='padding-left:5px;padding-right:10px;min-width: 200px;'>" + item.Quantity + "</td>"
                             + "<td  style='padding-left:5px;padding-right:10px;min-width: 200px;'>" + CommonFunc.FormatCurrency(Math.Round(item.Price, Constants.ROUND_NUMBER)) + "</td>"
                             + "<td  style='padding-left:5px;padding-right:10px;min-width: 200px;'>" + CommonFunc.FormatCurrency(Math.Round(item.TotalPrice, Constants.ROUND_NUMBER)) + "</td>" +
                             "</tr>";

                            strRow += str;
                            count++;
                        }
                        content = content.Replace("[#Item rows]", strRow);

                    }
                    //Create history
                    string flow = string.Empty;
                    string[] arrIds = purReq.InvolveID.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string[] arrRoles = purReq.InvolveRole.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string[] arrResolution = purReq.InvolveResolution.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string[] arrDate = purReq.InvolveDate.Split(Constants.SEPARATE_INVOLVE_CHAR);

                    int maxLength = new int[] { arrIds.Length, arrDate.Length, arrResolution.Length, arrRoles.Length }.Min();

                    for (int i = 0; i < maxLength - 1; i++)//last item is empty
                    {
                        //check duplicate person on user name and role.
                        UserAdmin userAdmin = userAdminDao.GetById(int.Parse(arrIds[i]));
                        WFRole role = roleDao.GetByID(int.Parse(arrRoles[i]));
                        flow += userAdmin.UserName + " (" + role.Name + ");" + arrResolution[i] + ";" + arrDate[i] + ",";
                    }

                    string[] array = flow.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string strRowHistory = String.Empty;
                    foreach (string item in array)
                    {
                        string[] arrayItem = item.Split(';');

                        if (arrayItem.Count() > 1)
                        {
                            strRowHistory += "<tr>";
                            string col1 = !string.IsNullOrEmpty(arrayItem[0]) ? arrayItem[0] : "&nbsp;";
                            strRowHistory += "<td >" + col1 + "</td>";
                            string col2 = !string.IsNullOrEmpty(arrayItem[1]) ? arrayItem[1] : "&nbsp;";
                            strRowHistory += "<td >" + col2 + "</td>";
                            string col3 = !string.IsNullOrEmpty(arrayItem[2]) ? Convert.ToDateTime(arrayItem[2]).ToString(Constants.DATETIME_FORMAT_FULL) : "&nbsp;";
                            strRowHistory += "<td >" + col3 + "</td></tr>";
                        }
                    }

                    strRowHistory += "<tr>";                    
                    strRowHistory += "<td>" + purReq.AssignName + "</td>";                    
                    strRowHistory += "<td></td>";
                    strRowHistory += "<td></td>";                    
                    strRowHistory += "</tr>";                    

                    content = content.Replace("[#History rows]", strRowHistory);
                    //Creata Total infor
                    content = content.Replace("[#Sub Total]", CommonFunc.FormatCurrency(Math.Round(result, Constants.ROUND_NUMBER)));
                    content = content.Replace("[#Other]", CommonFunc.FormatCurrency(Math.Round(purReq.OtherCost.Value, Constants.ROUND_NUMBER)));
                    string TaxLabel = String.Empty;
                    if (purReq.SaleTaxName == "1")
                    {
                        TaxLabel = "TAX (US)";
                    }
                    else if (purReq.SaleTaxName == "2")
                    {
                        TaxLabel = "VAT(VN)";
                    }
                    else
                    {
                        TaxLabel = "No Sale Tax";
                    }
                    content = content.Replace("[#Tax Label]", TaxLabel);
                    content = content.Replace("[#Tax Value]", CommonFunc.FormatCurrency(Math.Round(purReq.SaleTaxValue.Value, Constants.ROUND_NUMBER)));
                    content = content.Replace("[#Shipping]", CommonFunc.FormatCurrency(Math.Round(purReq.Shipping.Value, Constants.ROUND_NUMBER)));
                    content = content.Replace("[#Discount]", CommonFunc.FormatCurrency(Math.Round(purReq.Discount.Value, Constants.ROUND_NUMBER)));
                    content = content.Replace("[#Service Charge]", CommonFunc.FormatCurrency(Math.Round(purReq.ServiceCharge.Value, Constants.ROUND_NUMBER)));
                    string typeOfMoney = "";
                    if (purReq.MoneyType == Constants.TYPE_MONEY_USD)
                        typeOfMoney = Constants.TYPE_MONEY_USD_STRING;
                    else
                        typeOfMoney = Constants.TYPE_MONEY_VND_STRING;

                    content = content.Replace("[#Total]", CommonFunc.FormatCurrency(Math.Round((result + purReq.OtherCost + purReq.SaleTaxValue + purReq.Shipping + purReq.ServiceCharge - purReq.Discount).Value, Constants.ROUND_NUMBER)) + " " + typeOfMoney);
                    content = content.Replace("[#Purchase Approval]", String.IsNullOrEmpty(purReq.PurchaseAppoval) ? "&nbsp;" : HttpUtility.HtmlEncode(purReq.PurchaseAppoval));
                    content = content.Replace("[#Payment Approval]", String.IsNullOrEmpty(purReq.PaymentAppoval) ? "&nbsp;" : HttpUtility.HtmlEncode(purReq.PaymentAppoval));
                }

            }
            return content;
        }
        #endregion
        #region Request Report
        [HttpGet]
        [CrmAuthorizeAttribute(Module = Modules.PurchaseRequest, Rights = Permissions.Report)]
        public ActionResult Report()
        {
            Hashtable hashData = Session[SessionKey.PURCHASE_FILTER_REPORT] == null ? new Hashtable() : (Hashtable)Session[SessionKey.PURCHASE_FILTER_REPORT];
            
            ViewData[Constants.PURCHASE_REQUEST_KEYWORD] = hashData[Constants.PURCHASE_REQUEST_KEYWORD] == null ? Constants.PURCHASE_REQUEST : hashData[Constants.PURCHASE_REQUEST_KEYWORD];
            ViewData[Constants.PURCHASE_REQUEST_DEPARTMENT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", hashData[Constants.PURCHASE_REQUEST_DEPARTMENT] == null ?
                Constants.FIRST_ITEM_DEPARTMENT : hashData[Constants.PURCHASE_REQUEST_DEPARTMENT].ToString());
            if (hashData[Constants.PURCHASE_REQUEST_DEPARTMENT] == null)
            {
                ViewData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] == null ?
                Constants.FIRST_ITEM_SUB_DEPARTMENT : hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT].ToString());
            }
            else if (!String.IsNullOrEmpty(hashData[Constants.PURCHASE_REQUEST_DEPARTMENT].ToString()))
            {
                ViewData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] = new SelectList(deptDao.GetSubListByParent(ConvertUtil.ConvertToInt(hashData[Constants.PURCHASE_REQUEST_DEPARTMENT])), "DepartmentId", "DepartmentName", hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] == null ?
                    Constants.FIRST_ITEM_SUB_DEPARTMENT : hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT].ToString());
            }
            else
            {
                ViewData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] == null ?
                Constants.FIRST_ITEM_SUB_DEPARTMENT : hashData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT].ToString());
            }
            ViewData[Constants.PURCHASE_REQUEST_REQUESTOR_ID] = new SelectList(jrAdminDao.GetUserInPurchaseRequest(false), "UserAdminId", "UserName", hashData[Constants.PURCHASE_REQUEST_REQUESTOR_ID] == null ?
                Constants.PURCHASE_REQUEST_REQUESTOR_FIRST_ITEM : hashData[Constants.PURCHASE_REQUEST_REQUESTOR_ID].ToString());
            ViewData[Constants.PURCHASE_REQUEST_ASSIGN_ID] = new SelectList(jrAdminDao.GetUserInPurchaseRequest(true), "UserAdminId", "UserName", hashData[Constants.PURCHASE_REQUEST_ASSIGN_ID] == null ?
                Constants.PURCHASE_REQUEST_ASSIGN_FIRST_ITEM : hashData[Constants.PURCHASE_REQUEST_ASSIGN_ID].ToString());
            ViewData[Constants.PURCHASE_REQUEST_RESOLUTION_ID] = new SelectList(GetListResolutionName(), "Text", "Value", hashData[Constants.PURCHASE_REQUEST_RESOLUTION_ID] == null ?
                Constants.FIRST_ITEM_RESOLUTION : hashData[Constants.PURCHASE_REQUEST_RESOLUTION_ID].ToString());

            string fromdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString(Constants.DATETIME_FORMAT);
            string todate = DateTime.Now.ToString(Constants.DATETIME_FORMAT);

            ViewData[Constants.PURCHASE_REQUEST_REPORT_FROM_DATE] = hashData[Constants.PURCHASE_REQUEST_REPORT_FROM_DATE] == null ?
                fromdate : hashData[Constants.PURCHASE_REQUEST_REPORT_FROM_DATE];
            ViewData[Constants.PURCHASE_REQUEST_REPORT_TO_DATE] = hashData[Constants.PURCHASE_REQUEST_REPORT_TO_DATE] == null ?
                todate : hashData[Constants.PURCHASE_REQUEST_REPORT_TO_DATE];

            ViewData[Constants.PURCHASE_REQUEST_COLUMN] = hashData[Constants.PURCHASE_REQUEST_COLUMN] == null ? "ID" : hashData[Constants.PURCHASE_REQUEST_COLUMN];
            ViewData[Constants.PURCHASE_REQUEST_ORDER] = hashData[Constants.PURCHASE_REQUEST_ORDER] == null ? "desc" : hashData[Constants.PURCHASE_REQUEST_ORDER];
            ViewData[Constants.PURCHASE_REQUEST_PAGE_INDEX] = hashData[Constants.PURCHASE_REQUEST_PAGE_INDEX] == null ? "1" : hashData[Constants.PURCHASE_REQUEST_PAGE_INDEX].ToString();
            ViewData[Constants.PURCHASE_REQUEST_ROW_COUNT] = hashData[Constants.PURCHASE_REQUEST_ROW_COUNT] == null ? "20" : hashData[Constants.PURCHASE_REQUEST_ROW_COUNT].ToString();

            return View();
        }
        

        /// <summary>
        /// Get list jquery grid for report
        /// </summary>
        /// <param name="purchaseId">string</param>
        /// <param name="department">string</param>
        /// <param name="subdepartment">string</param>
        /// <param name="requestorId">string</param>
        /// <param name="statusId">string</param>
        /// <returns>ActionResult</returns>
        [ValidateInput(false)]
        public ActionResult GetListReportJQGrid(string purchaseId, string department, string subdepartment, string requestorId, string assignId, string resolutionId, string fromdate, string todate)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;

            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

            #endregion

            SetSessionFilter(purchaseId, department, subdepartment, requestorId, assignId, resolutionId, sortColumn, sortOrder, pageIndex, rowCount,"Report",fromdate,todate);

            purchaseId = purchaseId.Trim();

            if (purchaseId == Constants.PURCHASE_REQUEST)
            {
                purchaseId = "";
            }
            else
            {
                if (purchaseId.ToLower().StartsWith(Constants.PR_REQUEST_PREFIX.ToLower()))
                {
                    purchaseId = purchaseId.Substring(3);
                }

            }
            
            List<sp_GetPurchaseRequestResult> list = GetListByFilter(purchaseId, department, subdepartment, requestorId, assignId, resolutionId,
                principal.UserData.UserID, principal.UserData.Role,fromdate,todate);
            //For total
            double totalAllVND = 0;
            double totalAllUSD = 0;
            TempData["vnd_all"] = totalAllVND;
            TempData["usd_all"] = totalAllUSD;
            foreach (sp_GetPurchaseRequestResult item in list)
            { 
                setTotal(item,ref totalAllUSD,ref totalAllVND);
                TempData["vnd_all"] = totalAllVND;
                TempData["usd_all"] = totalAllUSD;
            }
            ////
            //For chart
            
            List<string> listMonth = new List<string>();
            List<double> listVndTotalOfMonth = new List<double>();
            List<double> listUsdTotalOfMonth = new List<double>();
            for (DateTime i = new DateTime(DateTime.Now.AddMonths(-12).Year, DateTime.Now.AddMonths(-12).Month, 1);
                    i <= DateTime.Now; i = i.AddMonths(1))
            {
                List<sp_GetPurchaseRequestResult> subList = list.Where(p => p.RequestDate.Year == i.Year && p.RequestDate.Month == i.Month).ToList();
                double vndTotalOfMonth = 0;
                double usdTotalOfMonth = 0;
                foreach(sp_GetPurchaseRequestResult subListItem in subList)
                {
                    setTotal(subListItem, ref usdTotalOfMonth, ref vndTotalOfMonth);
                }
                listVndTotalOfMonth.Add(vndTotalOfMonth);
                listUsdTotalOfMonth.Add(usdTotalOfMonth);
                listMonth.Add(i.ToString("MMM yy"));
            }
            TempData["vnd_of_month"] = string.Join(",", listVndTotalOfMonth);
            TempData["usd_of_month"] = string.Join(",", listUsdTotalOfMonth);
            TempData["list_of_month"] =string.Join(",", listMonth);
            //
            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = dao.Sort(list, sortColumn, sortOrder)
                                  .Skip((currentPage - 1) * rowCount)
                                   .Take(rowCount);
            double vnd=0;
            double usd=0;
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
                            m.WFStatusID != Constants.STATUS_CLOSE ? m.Priority.HasValue ? 
                                m.Priority.Value.ToString() : string.Empty : string.Empty,
                            CommonFunc.Link(m.ID.ToString(),"/PurchaseRequest/Detail/"  + m.ID.ToString() + "", "PR-" +  m.ID.ToString(),false),
                            m.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW),                                                        
                            m.ExpectedDate.HasValue ? m.ExpectedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "",
                            Server.HtmlEncode(m.RequestorName),
                            Server.HtmlEncode(m.Department),                            
                            Server.HtmlEncode(m.ResolutionName),                            
                            SetAssignName(m.WFResolutionID,m.WFStatusID,m.AssignName),  
                            Server.HtmlEncode(m.PurchaseAppoval),
                            
                            CommonFunc.Link(m.ID.ToString(), "justification", "#", CommonFunc.SubStringRoundWordToOneRow(m.Justification, 
                                Constants.PURCHASE_REQUEST_JUSTIFICATION_MAX_LENGTH)),
                            setTotal(m,ref usd,ref vnd)
                        }
                    }
                ).ToArray()
            };
            TempData["vnd"] = vnd;
            TempData["usd"] = usd;
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetChartValue()
        {
            //TODO: Do the validation

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            string test = "";
            test += TempData["vnd_of_month"].ToString();
            test += "-"+TempData["usd_of_month"].ToString();
            test += "-"+TempData["list_of_month"].ToString();
            result.Data = test;
            return result;
        } 
        public JsonResult SetSumTotalOnPage()
        {
            //TODO: Do the validation
            
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            string test = "";
            test += CommonFunc.FormatCurrency(Double.Parse(TempData["vnd"].ToString())) + " VND";
            test += "-" + CommonFunc.FormatCurrency(Double.Parse(TempData["usd"].ToString())) + " USD";
            result.Data = test;
            return result;
        }
        public JsonResult SetSumTotalAll()
        {
            //TODO: Do the validation
            
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            string test = "";
            if (TempData["vnd_all"] != null)
            {
                test += CommonFunc.FormatCurrency(Double.Parse(TempData["vnd_all"].ToString()));
            }

            if (TempData["usd_all"] != null)
            {
                test += "-" + CommonFunc.FormatCurrency(Double.Parse(TempData["usd_all"].ToString()));
            }
            result.Data = test;
            return result;
        }    
        private string setTotal(sp_GetPurchaseRequestResult purReq,ref double totalRequestUSD,ref double totalRequestVND)
        {
            double result = 0;
            List<PurchaseItem> itemList;
            itemList = dao.GetPurchaseRequestItems(Convert.ToInt32(purReq.ID));
            List<PurchaseInvoice> invoiceList = dao.GetPurchaseInvoice(Convert.ToInt32(purReq.ID));
                   

            if (itemList != null)
            {
                List<PurchaseItem> items = (List<PurchaseItem>)itemList;
                        
                foreach (PurchaseItem item in items)
                {
                    result += item.TotalPrice;
                            
                }
                        

            }
            string typeOfMoney = "";
            if (purReq.MoneyType == Constants.TYPE_MONEY_USD)
            {
                typeOfMoney = Constants.TYPE_MONEY_USD_STRING;
                totalRequestUSD += Math.Round((result + purReq.OtherCost + purReq.SaleTaxValue + purReq.Shipping + purReq.ServiceCharge - purReq.Discount).Value, Constants.ROUND_NUMBER);
            }
            else
            {
                typeOfMoney = Constants.TYPE_MONEY_VND_STRING;
                totalRequestVND += Math.Round((result + purReq.OtherCost + purReq.SaleTaxValue + purReq.Shipping + purReq.ServiceCharge - purReq.Discount).Value, Constants.ROUND_NUMBER);
            }

            return CommonFunc.FormatCurrency(Math.Round((result + purReq.OtherCost + purReq.SaleTaxValue + purReq.Shipping + purReq.ServiceCharge - purReq.Discount).Value, Constants.ROUND_NUMBER)) + " " + typeOfMoney;
        }
        #endregion

        // new code from Hung
        public JsonResult CheckOfficeStatus(string userID)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = checkOutOfOffice(ConvertUtil.ConvertToInt(userID),false,string.Empty);
            return result;
        }

        private Message checkOutOfOffice(int userID, bool isMulti, string objView)
        {
            Message msg = null;
            msg = new Message(MessageConstants.E0033, MessageType.Info, string.Empty);
            int userAdminID = ConvertUtil.ConvertToInt(userID);
            if (userAdminID != 0)
            {                
                UserConfig obj = new UserConfigDao().GetByID(userAdminID);
                if (obj != null)
                {
                    if (!obj.IsOff)
                    {
                        if (!isMulti)
                        {
                            msg = new Message(MessageConstants.E0033, MessageType.Error, obj.AutoReplyMessage);
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0005, MessageType.Error, obj.AutoReplyMessage, objView);
                        }
                    }
                }
            }
            return msg;
        }

        public JsonResult CheckMultiOfficeStatus(string Assign1, string Assign2, string Assign3, string Forward, string USAccounting)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Message msg1 = checkOutOfOffice(ConvertUtil.ConvertToInt(Assign1), true, "Assign1");
            if (msg1.MsgType == MessageType.Error)
            {
                result.Data = msg1;
                return result;
            }
            Message msg2 = checkOutOfOffice(ConvertUtil.ConvertToInt(Assign2), true, "Assign2");
            if (msg2.MsgType == MessageType.Error)
            {
                result.Data = msg2;
                return result;
            }
            Message msg3 = checkOutOfOffice(ConvertUtil.ConvertToInt(Assign3), true, "Assign3");
            if (msg3.MsgType == MessageType.Error)
            {
                result.Data = msg3;
                return result;
            }
            Message msg4 = checkOutOfOffice(ConvertUtil.ConvertToInt(Forward.Split('@')[0]), true, "Forward");
            if (msg4.MsgType == MessageType.Error)
            {
                result.Data = msg4;
                return result;
            }
            Message msg5 = checkOutOfOffice(ConvertUtil.ConvertToInt(USAccounting), true, "USAccounting");
            if (msg5.MsgType == MessageType.Error)
            {
                result.Data = msg5;
                return result;
            }
            Message msg = new Message(MessageConstants.E0033, MessageType.Info, string.Empty);
            result.Data = msg;
            return result;
        }

        public ActionResult PrintReview(string ids, string page)
        {

            sp_GetPurchaseRequestResult purReq = dao.GetPurchaseRequestByID(ids.ToString());

            ViewData["template"] = CreateReviewContent(purReq, ids);
            ViewData["Page"] = page;

            return View(purReq);
        }

        private string CreateReviewContent(sp_GetPurchaseRequestResult purReq, string ids)
        {

            string content = string.Empty;

            string tmpFilePath = Server.MapPath(Constants.PR_TEMPLATE_PRINT_PATH);

            if (System.IO.File.Exists(tmpFilePath))
            {
                content = System.IO.File.ReadAllText(tmpFilePath);

                if (purReq != null)
                {
                    // create PR infor 

                    content = content.Replace("[#Requestor]", HttpUtility.HtmlEncode(purReq.RequestorName));
                    content = content.Replace("[#Department]", HttpUtility.HtmlEncode(purReq.Department));
                    content = content.Replace("[#Sub Department]", HttpUtility.HtmlEncode(purReq.SubDepartmentName));
                    content = content.Replace("[#Request Date]", HttpUtility.HtmlEncode(purReq.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW)));
                    content = content.Replace("[#Expected Date]", HttpUtility.HtmlEncode(purReq.ExpectedDate.HasValue ? purReq.ExpectedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : ""));
                    string sPriority = String.Empty;
                    if (purReq.Priority.HasValue)
                        sPriority = Constants.PURCHASE_REQUEST_PRIORITY.Where(p => int.Parse(p.Value) == purReq.Priority.Value).FirstOrDefault().Text;

                    content = content.Replace("[#Priority]", "<b style='color:black'>" + sPriority + "</b>");
                    content = content.Replace("[#Status]", HttpUtility.HtmlEncode(purReq.ResolutionName));
                    content = content.Replace("[#Forwarded To]", HttpUtility.HtmlEncode(purReq.AssignName));
                    content = content.Replace("[#Billable To Client]", HttpUtility.HtmlEncode(purReq.BillableToClient ? "Yes" : "No"));
                    content = content.Replace("[#Justification]", HttpUtility.HtmlEncode(purReq.Justification != null ? purReq.Justification.Replace("\r\n", "<br/>") : ""));
                    content = content.Replace("[#Vendor]", HttpUtility.HtmlEncode(purReq.VendorName != null ? purReq.VendorName : ""));
                    content = content.Replace("[#Phone]", HttpUtility.HtmlEncode(purReq.VendorPhone != null ? purReq.VendorPhone : ""));
                    content = content.Replace("[#Email]", HttpUtility.HtmlEncode(purReq.VendorEmail != null ? purReq.VendorEmail : ""));
                    content = content.Replace("[#Address]", HttpUtility.HtmlEncode(purReq.VendorAddress != null ? purReq.VendorAddress : ""));
                    //Create item infor
                    string PaymentMethod = String.Empty;
                    if (purReq.PaymentID == Constants.TYPE_PAYMENT_CASH)
                        PaymentMethod = Constants.TYPE_PAYMENT_CASH_STRING;
                    else if (purReq.PaymentID == Constants.TYPE_PAYMENT_TRANFER)
                        PaymentMethod = Constants.TYPE_PAYMENT_TRANFER_STRING;
                    content = content.Replace("[#Payment Method]", HttpUtility.HtmlEncode(PaymentMethod));
                    content = content.Replace("[#Money Type]", HttpUtility.HtmlEncode(purReq.MoneyType == Constants.TYPE_MONEY_USD ? Constants.TYPE_MONEY_USD_STRING : Constants.TYPE_MONEY_VND_STRING));
                    //Create item rows
                    double result = 0;
                    List<PurchaseItem> itemList;
                    itemList = dao.GetPurchaseRequestItems(Convert.ToInt32(ids));
                    List<PurchaseInvoice> invoiceList = dao.GetPurchaseInvoice(Convert.ToInt32(ids));


                    if (itemList != null)
                    {
                        List<PurchaseItem> items = (List<PurchaseItem>)itemList;
                        int count = 1;
                        string strRow = String.Empty;
                        foreach (PurchaseItem item in items)
                        {
                            string str = "<tr>";
                            result += item.TotalPrice;
                            str += "<td style='padding-left:5px;padding-right:10px;width: 70px;border: 1px solid #cccccc'>" + count + "</td>"
                             + "<td  style='padding-left:5px;padding-right:10px;width: 300px;border: 1px solid #cccccc'>" + HttpUtility.HtmlEncode(item.ItemName) + "</td>"
                             + "<td  style='padding-left:5px;padding-right:10px;width: 70px;border: 1px solid #cccccc'>" + item.Quantity + "</td>"
                             + "<td  style='padding-left:5px;padding-right:10px;width: 180px;border: 1px solid #cccccc'>" + CommonFunc.FormatCurrency(Math.Round(item.Price, Constants.ROUND_NUMBER)) + "</td>"
                             + "<td  style='padding-left:5px;padding-right:10px;width: 180px;border: 1px solid #cccccc'>" + CommonFunc.FormatCurrency(Math.Round(item.TotalPrice, Constants.ROUND_NUMBER)) + "</td>" +
                             "</tr>";

                            strRow += str;
                            count++;
                        }
                        content = content.Replace("[#Item rows]", strRow);

                    }
                    //Create history
                    string flow = string.Empty;
                    string[] arrIds = purReq.InvolveID.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string[] arrRoles = purReq.InvolveRole.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string[] arrResolution = purReq.InvolveResolution.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string[] arrDate = purReq.InvolveDate.Split(Constants.SEPARATE_INVOLVE_CHAR);

                    int maxLength = new int[] { arrIds.Length, arrDate.Length, arrResolution.Length, arrRoles.Length }.Min();

                    for (int i = 0; i < maxLength - 1; i++)//last item is empty
                    {
                        //check duplicate person on user name and role.
                        UserAdmin userAdmin = userAdminDao.GetById(int.Parse(arrIds[i]));
                        WFRole role = roleDao.GetByID(int.Parse(arrRoles[i]));
                        flow += userAdmin.UserName + " (" + role.Name + ");" + arrResolution[i] + ";" + arrDate[i] + ",";
                    }

                    string[] array = flow.Split(Constants.SEPARATE_INVOLVE_CHAR);
                    string strRowHistory = String.Empty;
                    foreach (string item in array)
                    {
                        string[] arrayItem = item.Split(';');

                        if (arrayItem.Count() > 1)
                        {
                            strRowHistory += "<tr>";
                            string col1 = !string.IsNullOrEmpty(arrayItem[0]) ? arrayItem[0] : "";
                            strRowHistory += "<td style='border: 1px solid #cccccc;font-size: 13px'>" + col1 + "</td>";
                            string col2 = !string.IsNullOrEmpty(arrayItem[1]) ? arrayItem[1] : "";
                            strRowHistory += "<td style='border: 1px solid #cccccc;font-size: 13px'>" + col2 + "</td>";
                            string col3 = !string.IsNullOrEmpty(arrayItem[2]) ? Convert.ToDateTime(arrayItem[2]).ToString(Constants.DATETIME_FORMAT_FULL) : "";
                            strRowHistory += "<td style='border: 1px solid #cccccc;font-size: 13px'>" + col3 + "</td></tr>";
                        }
                    }

                    strRowHistory += "<tr >";
                    strRowHistory += "<td style='border: 1px solid #cccccc;font-size: 13px'>" + purReq.AssignName + "</td>";
                    strRowHistory += "<td style='border: 1px solid #cccccc;font-size: 13px'></td>";
                    strRowHistory += "<td style='border: 1px solid #cccccc;font-size: 13px's></td>";
                    strRowHistory += "</tr>";

                    content = content.Replace("[#History rows]", strRowHistory);
                    //Creata Total infor
                    content = content.Replace("[#Sub Total]", CommonFunc.FormatCurrency(Math.Round(result, Constants.ROUND_NUMBER)));
                    content = content.Replace("[#Other]", CommonFunc.FormatCurrency(Math.Round(purReq.OtherCost.Value, Constants.ROUND_NUMBER)));
                    string TaxLabel = String.Empty;
                    if (purReq.SaleTaxName == "1")
                    {
                        TaxLabel = "TAX (US)";
                    }
                    else if (purReq.SaleTaxName == "2")
                    {
                        TaxLabel = "VAT(VN)";
                    }
                    else
                    {
                        TaxLabel = "No Sale Tax";
                    }
                    content = content.Replace("[#Tax Label]", TaxLabel);
                    content = content.Replace("[#Tax Value]", CommonFunc.FormatCurrency(Math.Round(purReq.SaleTaxValue.Value, Constants.ROUND_NUMBER)));
                    content = content.Replace("[#Shipping]", CommonFunc.FormatCurrency(Math.Round(purReq.Shipping.Value, Constants.ROUND_NUMBER)));
                    content = content.Replace("[#Discount]", CommonFunc.FormatCurrency(Math.Round(purReq.Discount.Value, Constants.ROUND_NUMBER)));
                    content = content.Replace("[#Service Charge]", CommonFunc.FormatCurrency(Math.Round(purReq.ServiceCharge.Value, Constants.ROUND_NUMBER)));
                    string typeOfMoney = "";
                    if (purReq.MoneyType == Constants.TYPE_MONEY_USD)
                        typeOfMoney = Constants.TYPE_MONEY_USD_STRING;
                    else
                        typeOfMoney = Constants.TYPE_MONEY_VND_STRING;

                    content = content.Replace("[#Total]", CommonFunc.FormatCurrency(Math.Round((result + purReq.OtherCost + purReq.SaleTaxValue + purReq.Shipping + purReq.ServiceCharge - purReq.Discount).Value, Constants.ROUND_NUMBER)) + " " + typeOfMoney);
                    content = content.Replace("[#Purchase Approval]", String.IsNullOrEmpty(purReq.PurchaseAppoval) ? "" : HttpUtility.HtmlEncode(purReq.PurchaseAppoval));
                    content = content.Replace("[#Payment Approval]", String.IsNullOrEmpty(purReq.PaymentAppoval) ? "" : HttpUtility.HtmlEncode(purReq.PaymentAppoval));
                }

            }
            return content;
        }
    }
}
