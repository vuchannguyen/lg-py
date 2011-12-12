using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

using CRM.Models.Entities;
using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Exceptions;
using CRM.Library.Attributes;
using CRM.Library.Utils;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace CRM.Controllers
{
    public class JobRequestController : BaseController
    {

        #region private variables

        private JobRequestDao dao = new JobRequestDao();
        private DepartmentDao deptDao = new DepartmentDao();
        private PositionDao posDao = new PositionDao();
        private JRAdminDao jrAdminDao = new JRAdminDao();
        private WFStatusDao wfStatusdao = new WFStatusDao();
        private JRCommentDao jrCommentDao = new JRCommentDao();
        private CommonDao commDao = new CommonDao();
        private ResolutionDao resDao = new ResolutionDao();
        private StatusDao staDao = new StatusDao();
        private UserAdminDao userAdminDao = new UserAdminDao();
        private RoleDao roleDao = new RoleDao();
        private JobTitleDao titleDao = new JobTitleDao();
        private JobRequestItemDao jrItemDao = new JobRequestItemDao();
        private GroupPermissionDao groupPerDao = new GroupPermissionDao();
        private GroupDao groupDao = new GroupDao();

        #endregion

        #region Duy Hung Nguyen

        // GET: /JobRequest/
        //[CrmAuthorizeAttribute(Module = Modules.JobRequest, Rights = Permissions.Read)]
        public ActionResult Index(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            int currentRole = 0;
            List<WFRole> list = commDao.GetRoleList(principal.UserData.UserID, Constants.WORK_FLOW_JOB_REQUEST);

            //handle case user link to this page from email
            string roleParam = Request["role"];
            if (!string.IsNullOrEmpty(roleParam))
            {
                TempData["roleId"] = roleParam;
            }

            if (TempData["roleId"] != null)
            {
                principal.UserData.Role = int.Parse(TempData["roleId"].ToString());
            }
            //End

            if (list.Count == 0) //if does
            {
                throw new ForbiddenException();
            }
            else if (principal.UserData.Role != 0 && list.Count >= 1) //Check for delete case in JRAdmin
            {
                WFRole exist = list.Where(c => c.ID == principal.UserData.Role).FirstOrDefault<WFRole>();
                if (exist == null) //if does not exist, choose the first role in list
                {
                    if (list.Count > 1)
                    {
                        //Display login role dropdownlist for choosing
                        ViewData["Role"] = new SelectList(list, "ID", "Name", "");
                    }
                    AssignRole(list[0].ID);
                    currentRole = list[0].ID;
                }
                else
                {
                    if (list.Count > 1)
                    {
                        //Display login role dropdownlist for choosing
                        ViewData["Role"] = new SelectList(list, "ID", "Name", principal.UserData.Role);
                    }

                    if (TempData["roleId"] != null)
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
                        ViewData["Role"] = new SelectList(list, "ID", "Name", "");
                    }
                    AssignRole(list[0].ID); //assign the first role in list (default)
                    currentRole = list[0].ID;
                }
            }


            Hashtable hashData = Session[Constants.JOB_REQUEST_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[Constants.JOB_REQUEST_DEFAULT_VALUE];
            if (!string.IsNullOrEmpty(id))
            {
                ViewData[Constants.JOB_REQUEST_KEYWORD] = id;
                AssignRole(Constants.HR_ID);
                currentRole = Constants.HR_ID;
            }
            else
            {
                ViewData[Constants.JOB_REQUEST_KEYWORD] = hashData[Constants.JOB_REQUEST_KEYWORD] == null ? Constants.JOB_REQUEST : hashData[Constants.JOB_REQUEST_KEYWORD];
            }

            
            ViewData[Constants.JOB_REQUEST_DEPARTMENT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", hashData[Constants.JOB_REQUEST_DEPARTMENT] == null ? Constants.FIRST_ITEM_DEPARTMENT : hashData[Constants.JOB_REQUEST_DEPARTMENT].ToString());
            ViewData[Constants.JOB_REQUEST_SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", hashData[Constants.JOB_REQUEST_SUB_DEPARTMENT] == null ? Constants.FIRST_ITEM_SUB_DEPARTMENT : hashData[Constants.JOB_REQUEST_SUB_DEPARTMENT].ToString());
            ViewData[Constants.JOB_REQUEST_POSITION_ID] = new SelectList(posDao.GetList(), "ID", "DisplayName", hashData[Constants.JOB_REQUEST_POSITION_ID] == null ? Constants.FIRST_ITEM_JOBTITLE : hashData[Constants.JOB_REQUEST_POSITION_ID].ToString());
            ViewData[Constants.JOB_REQUEST_REQUESTOR_ID] = new SelectList(jrAdminDao.GetByRequestor(), "UserAdminId", "UserName", hashData[Constants.JOB_REQUEST_REQUESTOR_ID] == null ? Constants.JOB_REQUEST_REQUESTOR_FIRST_ITEM : hashData[Constants.JOB_REQUEST_REQUESTOR_ID].ToString());
            ViewData[Constants.JOB_REQUEST_STATUS_ID] = new SelectList(wfStatusdao.GetList(), "ID", "Name", hashData[Constants.JOB_REQUEST_STATUS_ID] == null ? Constants.STATUS_OPEN.ToString() : hashData[Constants.JOB_REQUEST_STATUS_ID].ToString());
            ViewData[Constants.JOB_REQUEST_REQUEST_TYPE] = new SelectList(Constants.JR_REQUEST_TYPE, "Value", "Text", hashData[Constants.JOB_REQUEST_REQUEST_TYPE] == null ? string.Empty : hashData[Constants.JOB_REQUEST_REQUEST_TYPE].ToString());
            ViewData[Constants.JOB_REQUEST_ROLE] = principal.UserData.Role;

            ViewData[Constants.JOB_REQUEST_COLUMN] = hashData[Constants.JOB_REQUEST_COLUMN] == null ? "ID" : hashData[Constants.JOB_REQUEST_COLUMN];
            ViewData[Constants.JOB_REQUEST_ORDER] = hashData[Constants.JOB_REQUEST_ORDER] == null ? "desc" : hashData[Constants.JOB_REQUEST_ORDER];
            ViewData[Constants.JOB_REQUEST_PAGE_INDEX] = hashData[Constants.JOB_REQUEST_PAGE_INDEX] == null ? "1" : hashData[Constants.JOB_REQUEST_PAGE_INDEX].ToString();
            ViewData[Constants.JOB_REQUEST_ROW_COUNT] = hashData[Constants.JOB_REQUEST_ROW_COUNT] == null ? "20" : hashData[Constants.JOB_REQUEST_ROW_COUNT].ToString();


            return View(currentRole);
        }

        public ActionResult Refresh()
        {
            Session.Remove(Constants.JOB_REQUEST_DEFAULT_VALUE);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Read in cookie and re-assign choosing role into cookie
        /// </summary>
        /// <param name="role"></param>
        private void AssignRole(int role)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            var timeOut = 400;
            UserData ud = new UserData(principal.UserData.UserID, principal.UserData.UserName, role);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, principal.UserData.UserName, DateTime.Now, DateTime.Now.AddMinutes(timeOut), false, ud.ToXml());
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(Constants.COOKIE_CRM, encryptedTicket);
            
            Response.Cookies.Add(authCookie);
        }

        /// <summary>
        /// Set action for detail page
        /// </summary>
        /// <param name="assignRole"></param>
        /// <param name="assignID"></param>
        /// <param name="status"></param>
        /// <param name="resolutionID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string SetAction(int assignRole, int assignID, int status, int resolutionID, int id)
        { 
          var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string value = string.Empty;
            
            if(assignID == principal.UserData.UserID && assignRole == principal.UserData.Role && status != Constants.STATUS_CLOSE)
            {
                if (principal.UserData.Role == Constants.REQUESTOR_ID)
                {
                    value += CommonFunc.ButtonWithParams("edit right", "Edit", "CRM.popup('/JobRequest/Edit/" + id.ToString() + "', 'Edit - " + id.ToString() + "', 650)", "Edit");                        
                }
                else if (principal.UserData.Role == Constants.HR_ID)
                {
                    value += CommonFunc.ButtonWithParams("forward right", "Forward to", "CRM.popup('/JobRequest/ForwardTo/" + id.ToString() + "', 'Forward - " + id.ToString() + "', 500)", "Forward to");                        
                }
                else if (principal.UserData.Role == Constants.MANAGER_ID)
                {
                    List<WFResolution> resList = resDao.GetListResolutionChangeByRole(principal.UserData.Role);
                    value += CommonFunc.ButtonWithParams("approve right", "Set Up Approval", "CRM.popup('/JobRequest/ManagerResult/" + id.ToString() + "@" + resList[0].ID.ToString() +
                        "', 'Approve #JR-"  + id.ToString() + "', 500)", "Set Up Approval");
                    value += CommonFunc.ButtonWithParams("reject right", "Reject", "CRM.popup('/JobRequest/ManagerResult/" + id.ToString() + "@" + resList[1].ID.ToString() +
                        "', 'Reject #JR- " + id.ToString() + "', 500)", "Reject");
                }
            }

            return value;                
        }

        /// <summary>
        /// Set filter arguments to session
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="department"></param>
        /// <param name="subDepartment"></param>
        /// <param name="positionId"></param>
        /// <param name="requestorId"></param>
        /// <param name="statusId"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilter(string keyword, string department, string subDepartment, string positionId, string requestorId, string statusId,
            string column, string order, int pageIndex, int rowCount,string request)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.JOB_REQUEST_KEYWORD, keyword);
            hashData.Add(Constants.JOB_REQUEST_DEPARTMENT, department);
            hashData.Add(Constants.JOB_REQUEST_SUB_DEPARTMENT, subDepartment);
            hashData.Add(Constants.JOB_REQUEST_POSITION_ID, positionId);
            hashData.Add(Constants.JOB_REQUEST_REQUESTOR_ID, requestorId);
            hashData.Add(Constants.JOB_REQUEST_STATUS_ID, statusId);
            hashData.Add(Constants.JOB_REQUEST_REQUEST_TYPE, request);

            hashData.Add(Constants.JOB_REQUEST_COLUMN, column);
            hashData.Add(Constants.JOB_REQUEST_ORDER, order);
            hashData.Add(Constants.JOB_REQUEST_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.JOB_REQUEST_ROW_COUNT, rowCount);

            Session[Constants.JOB_REQUEST_DEFAULT_VALUE] = hashData;
        }

        /// <summary>
        /// Get all items for JR
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetSubList(int id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            int ind = 1;
            List<JobRequestItem> items = dao.GetJRItems(id);
            var jsonData = new
            {
                rows = (
                    from m in items
                    select new
                    {
                        i = m.ID,
                        cell = new string[] {
                            (ind++).ToString(),
                            Constants.JOB_REQUEST_ITEM_PREFIX + m.ID.ToString(),
                            m.Candidate,
                            m.EmpID,
                            m.FinalTitleId.HasValue?m.JobTitleLevel.DisplayName:m.Title,
                            (m.ActualStartDate != null && m.ActualStartDate.HasValue)?m.ActualStartDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",
                            m.JobRequestItemStatus.StatusName,
                            CommonFunc.DescyptSalary(m.ProbationSalary, principal, groupDao,userAdminDao),
                            CommonFunc.DescyptSalary(m.ContractedSalary, principal, groupDao,userAdminDao),                            
                            ((m.JobRequest.AssignID ==  principal.UserData.UserID 
                                    && m.JobRequest.AssignRole ==  principal.UserData.Role 
                                    && m.StatusID == Constants.JR_ITEM_STATUS_OPEN)? 
                                    "<input type=\"button\" class=\"icon filldata\" title = \"Fill data\" value=\"\" onclick=\"CRM.popup('/JobRequest/HRFillData/" 
                                        + m.ID + "', 'Fill data for " + Constants.JOB_REQUEST_PREFIX + m.ID.ToString() +"', 750)\" />": "")
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Bind data for Grid
        /// </summary>
        /// <param name="text"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <param name="requestorId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public ActionResult GetListJQGrid(string text, string department, string subdepartment, string positionId, string requestorId, string statusId, string request)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;

            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(text, department, subdepartment, positionId, requestorId, statusId, sortColumn, sortOrder, pageIndex, rowCount, request);
            text = text.Trim();
            if (text == Constants.JOB_REQUEST)
            {
                text = "";
            }
            else
            {
                string containsJR = Constants.JOB_REQUEST_PREFIX;
                if (containsJR.Contains(text))
                {
                    text = string.Empty;
                }
                else
                {
                    if (text.Length >= 6)
                    {
                        text = text.Substring(containsJR.Length);
                    }
                }
            }
            int role = principal.UserData.Role;
            object jsonData;

            // get data for HR
            List<sp_GetJobRequestResult> list = GetListByFilter(text, department, subdepartment, positionId, 
                requestorId, statusId, principal.UserData.UserID, principal.UserData.Role.ToString(), request);
            List<WFResolution> resList = resDao.GetListResolutionChangeByRole(principal.UserData.Role);

            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = dao.Sort(list, sortColumn, sortOrder)
                                  .Skip((currentPage - 1) * rowCount)
                                   .Take(rowCount);

            jsonData = new
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
                            CommonFunc.Link(m.ID.ToString(),"/JobRequest/Detail/"  + m.ID.ToString() + "",
                                Constants.JOB_REQUEST_PREFIX + m.ID.ToString(),
                                jrItemDao.GetListByJrId(m.ID).Count > 0 ? true : false),
                            m.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW),                            
                            m.Position,                           
                            m.Quantity.ToString(),
                            m.SubDepartment,                            
                            m.ExpectedStartDate.HasValue ? m.ExpectedStartDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "",
                            m.ResolutionName,
                            m.StatusName,     
                            m.RequestTypeId == Constants.JR_REQUEST_TYPE_NEW ? "New":"Replace",      
                            m.RequestorName,
                            SetAssignName(m.WFResolutionID,m.WFStatusID,m.AssignName),      
                            role == Constants.HR_ID?
                            ((m.AssignID ==  principal.UserData.UserID 
                                    && m.AssignRole ==  principal.UserData.Role 
                                    && m.WFStatusID != Constants.STATUS_CLOSE)? 
                                    "<input type=\"button\" class=\"icon forward\" title = \"Forward To\" onclick=\"CRM.popup('/JobRequest/ForwardTo/" + m.ID + "','Forward To', 350)\"/>":""):
                            //Authorize role,decide which function to display for each role
                            ((principal.UserData.Role == Constants.REQUESTOR_ID && m.AssignID ==  principal.UserData.UserID && m.AssignRole ==  principal.UserData.Role && m.WFStatusID != Constants.STATUS_CLOSE) ? "<input type=\"button\" class=\"icon edit\" title = \"Edit\" onclick=\"CRM.popup('/JobRequest/Edit/" + m.ID.ToString() + "', 'Update', 650)\" />" : "")                            
                            + ((principal.UserData.Role == Constants.MANAGER_ID && m.AssignID ==  principal.UserData.UserID && m.AssignRole ==  principal.UserData.Role && m.WFStatusID != Constants.STATUS_CLOSE) ? "<input type=\"button\" class=\"icon approve\" title = \"Approve\" onclick=\"CRM.popup('/JobRequest/ManagerResult/" + m.ID.ToString()+"@"+resList[0].ID.ToString()+ "', 'Approve #JR-"+m.ID.ToString()+"', 500)\" /> <input type=\"button\" class=\"icon reject\" title = \"Reject\" onclick=\"CRM.popup('/JobRequest/ManagerResult/" + m.ID.ToString()+"@"+resList[1].ID.ToString()+ "', 'Reject #JR-"+m.ID.ToString()+"', 500)\"/>": "")                            
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string SetAssignName(int resolution, int status,string assignName)
        {
            if ((resolution == Constants.RESOLUTION_COMPLETED_ID || resolution == Constants.RESOLUTION_CANCEL_ID) && (status == Constants.STATUS_CLOSE))
            {
                assignName = string.Empty;
            }
            return assignName;
        }

        //
        // GET: /JobRequest/Create

        public ActionResult Create()
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;

            #region Bind data for dropdownlist
            ViewData["DepartmentId"] = new SelectList(new List<string>());
            ViewData["DepartmentName"] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", "");
            ViewData["PositionFrom"] = new SelectList(new List<string>());
            ViewData["PositionTo"] = new SelectList(new List<string>());
            
            ViewData["WFResolutionID"] = new SelectList(resDao.GetListResolutionChangeByRole(principal.UserData.Role, false,Constants.WORK_FLOW_JOB_REQUEST), "ID", "Name", Constants.RESOLUTION_NEW_ID);
            ViewData["WFStatusID"] = new SelectList(staDao.GetListStatusByResolution(Constants.RESOLUTION_NEW_ID), "ID", "Name");
            ViewData["Assign"] = new SelectList(dao.GetListAssign(Constants.RESOLUTION_NEW_ID), "UserAdminRole", "DisplayName", principal.UserData.UserID + Constants.SEPARATE_USER_ADMIN_ID_STRING + Constants.REQUESTOR_ID.ToString());
            ViewData["FirstChoiceResolution"] = string.Empty;
            ViewData["FirstChoiceStatus"] = string.Empty;
            ViewData["FirstChoiceAssign"] = string.Empty;
            ViewData["RequestTypeId"] = new SelectList(Constants.JR_REQUEST_TYPE, "Value", "Text", string.Empty);
            #endregion

            return View();
        }

        public List<sp_GetJobRequestResult> GetListJobRequestForNavigation()
        {
            List<sp_GetJobRequestResult> listJR = null;
            string column = "ID";
            string order = "desc";            
            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;

            if (Session[Constants.JOB_REQUEST_DEFAULT_VALUE] != null)
            {
                Hashtable hashData = (Hashtable)Session[Constants.JOB_REQUEST_DEFAULT_VALUE];
                string text = (string)hashData[Constants.JOB_REQUEST_KEYWORD];
                if (text == Constants.FULLNAME_OR_USERID)
                {
                    text = string.Empty;
                }
                if (text == Constants.JOB_REQUEST)
                {
                    text = "";
                }
                else
                {
                    string containsJR = Constants.JOB_REQUEST_PREFIX;
                    if (containsJR.Contains(text))
                    {
                        text = string.Empty;
                    }
                    else
                    {
                        if (text.Length >= 6)
                        {
                            text = text.Substring(containsJR.Length);
                        }
                    }
                }
                int role = principal.UserData.Role;
                listJR = GetListByFilter(text, (string)hashData[Constants.JOB_REQUEST_DEPARTMENT], (string)hashData[Constants.JOB_REQUEST_SUB_DEPARTMENT],
                                        (string)hashData[Constants.JOB_REQUEST_POSITION_ID], (string)hashData[Constants.JOB_REQUEST_REQUESTOR_ID],
                                        (string)hashData[Constants.JOB_REQUEST_STATUS_ID], principal.UserData.UserID,
                                        principal.UserData.Role.ToString(), (string)hashData[Constants.JOB_REQUEST_REQUEST_TYPE]);
                column = !string.IsNullOrEmpty((string)hashData[Constants.JOB_REQUEST_COLUMN]) ? (string)hashData[Constants.JOB_REQUEST_COLUMN] : "ID";
                order = !string.IsNullOrEmpty((string)hashData[Constants.JOB_REQUEST_ORDER]) ? (string)hashData[Constants.JOB_REQUEST_ORDER] : "desc";

                listJR = dao.Sort(listJR, column, order);
            }
            else
            {
                listJR = dao.GetList(null, 0, 0, 0, 0, Constants.STATUS_OPEN, principal.UserData.Role.ToString(), 0);
            }

            return listJR;
        }

        /// <summary>
        /// Navigation action
        /// </summary>
        /// <param name="name">action name</param>
        /// <param name="id">job request id</param>        
        /// <returns>ActionResult</returns>
        public ActionResult Navigation(string name, string id)
        {

            List<sp_GetJobRequestResult> listJR = GetListJobRequestForNavigation();

            string testID = string.Empty;
            int index = 0;
            string url = string.Empty;
            switch (name)
            {
                case "First":
                    testID = listJR[0].ID.ToString();
                    break;
                case "Prev":
                    index = listJR.IndexOf(listJR.Where(p => p.ID.ToString() == id).FirstOrDefault<sp_GetJobRequestResult>());
                    if (index != 0)
                    {
                        testID = listJR[index - 1].ID.ToString();
                    }
                    else
                    {
                        testID = id;
                    }
                    break;
                case "Next":
                    index = listJR.IndexOf(listJR.Where(p => p.ID.ToString() == id).FirstOrDefault<sp_GetJobRequestResult>());
                    if (index != listJR.Count - 1)
                    {
                        testID = listJR[index + 1].ID.ToString();
                    }
                    else
                    {
                        testID = id;
                    }
                    break;
                case "Last":
                    testID = listJR[listJR.Count - 1].ID.ToString();
                    break;
            }
            
            url = "Detail/" + testID;
            
            return RedirectToAction(url);
        }

        ///// <summary>
        ///// Do action change role
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <returns></returns>
        //public ActionResult ChangeRole(string roleId)
        //{
        //    if (!string.IsNullOrEmpty(roleId))
        //    {
        //        int role = int.Parse(roleId);
        //        AssignRole(role);
        //    }
        //    return RedirectToAction("Index");
        //}


        // POST: /JobRequest/Create
        [HttpPost]
        public ActionResult Create(JobRequest request)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string rdmanyValue = Request["Position"];
            if (rdmanyValue != Constants.MANY_POSITION_CHECK)
                request.PositionTo = null;
            bool isHold = false;
            string userAdminRole = Request["Assign"];
            request.RequestorId = principal.UserData.UserID;
            
            if (!string.IsNullOrEmpty(userAdminRole))
            {
                string[] arr = userAdminRole.Split(Constants.SEPARATE_USER_ADMIN_ID);
                request.AssignID = int.Parse(arr[0].Trim());
                request.AssignRole = int.Parse(arr[1].Trim());
                request.InvolveID = principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                request.InvolveRole = principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                request.InvolveDate = request.InvolveDate + DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                if (request.WFResolutionID == Constants.RESOLUTION_NEW_ID)//case Add New
                {
                    if ((int.Parse(arr[0].Trim()) != request.RequestorId) && (int.Parse(arr[1].Trim())) == Constants.REQUESTOR_ID)//case add new but send JR for another requestor
                    {
                        request.InvolveResolution = Constants.JR_ACTION_ASSIGNTO + Constants.SEPARATE_INVOLVE_SIGN;
                    }
                    else if ((int.Parse(arr[0].Trim()) == request.RequestorId) && (int.Parse(arr[1].Trim())) == Constants.REQUESTOR_ID)//case add new JR
                    {
                        isHold = true;
                        request.InvolveResolution = Constants.JR_ACTION_ADDNEW + Constants.SEPARATE_INVOLVE_SIGN;
                    }
                }
                else if (request.WFResolutionID == Constants.RESOLUTION_TO_BE_APPROVED_BY_MANAGER_ID)//case send JR for Manager
                {
                    request.InvolveResolution = Constants.JR_ACTION_SUBMIT + Constants.SEPARATE_INVOLVE_SIGN;
                }
            }
            request.CreateDate = DateTime.Now;
            request.UpdateDate = DateTime.Now;
            request.CreatedBy = principal.UserData.UserName;
            request.UpdatedBy = principal.UserData.UserName;
            Message msg = dao.Insert(request);
            if ((!string.IsNullOrEmpty(userAdminRole)) && (isHold == false) && (msg.MsgType == MessageType.Info))
            {                                                                                    
                SendMail(request.ID, 0);
                Session.Remove(Constants.JOB_REQUEST_DEFAULT_VALUE);
            }
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

       
        public ActionResult Edit(int id)
        {
            
            // TODO: Add insert logic here            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region Check permission
            bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_JOB_REQUEST,
                    id.ToString(), Constants.ActionType.Update, principal.UserData.Role);
            if (!check)
                return View("../Common/NotPermission");
            #endregion

            JobRequest jobRequest = dao.GetById(id);

            #region Bind data for dropdownlist
            int department = deptDao.GetDepartmentIdBySub(jobRequest.DepartmentId).Value;
            ViewData["DepartmentId"] = new SelectList(deptDao.GetSubListByParent(department), "DepartmentId", "DepartmentName", jobRequest.DepartmentId);
            ViewData["DepartmentName"] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", department);
            ViewData["PositionFrom"] = new SelectList(posDao.GetJobTitleListByDepId(department), "ID", "DisplayName", jobRequest.PositionFrom);
            List<JobTitleLevel> posList = posDao.GetListByLevelId(jobRequest.PositionFrom);
            ViewData["PositionTo"] = new SelectList(posList, "ID", "DisplayName", jobRequest.PositionTo);
            ViewData["RequestTypeId"] = new SelectList(Constants.JR_REQUEST_TYPE, "Value", "Text", jobRequest.RequestTypeId);
            List<WFResolution> resolutionList = new List<WFResolution>();
            //Check if current resolution is reject status, load resolution list approriate to it
            if (jobRequest.WFResolutionID == Constants.RESOLUTION_REJECT_ID)
                resolutionList = resDao.GetListResolutionChangeByRole(principal.UserData.Role, true,Constants.WORK_FLOW_JOB_REQUEST);
            else
                resolutionList = resDao.GetListResolutionChangeByRole(principal.UserData.Role, false,Constants.WORK_FLOW_JOB_REQUEST);
            //Check if it is a resolution in "resolution can be changed" List, if not exist set it as a default item 
            WFResolution existItem = resolutionList.Where(c => c.ID == jobRequest.WFResolutionID).FirstOrDefault<WFResolution>();
            if (existItem == null)
            {
                WFResolution item = resDao.GetByID(jobRequest.WFResolutionID);
                if (item != null)
                {
                    ViewData["FirstChoiceResolution"] = item.Name;
                }
            }
            else
            {
                ViewData["FirstChoiceResolution"] = string.Empty;
            }
            ViewData["WFResolutionID"] = new SelectList(resolutionList, "ID", "Name", jobRequest.WFResolutionID);    
            WFResolution currentResolution = resolutionList.Where(c => c.ID == jobRequest.WFResolutionID).FirstOrDefault<WFResolution>();
            List<WFStatus> staList = staDao.GetListStatusByResolution(jobRequest.WFResolutionID);
            WFStatus status = staList.Where(c => c.ID == jobRequest.WFStatusID).FirstOrDefault<WFStatus>();         
            if (existItem == null)
            {
                WFStatus item = staDao.GetByID(jobRequest.WFStatusID);
                //set default item for status list
                if (item != null)
                {
                    ViewData["FirstChoiceStatus"] = item.Name;
                }
                ViewData["WFStatusID"] = new SelectList(new List<string>());
            }
            else
            {
                ViewData["FirstChoiceStatus"] = string.Empty;
                ViewData["WFStatusID"] = new SelectList(staList, "ID", "Name", jobRequest.WFStatusID);
            }                                                                                   
            List<sp_GetListAssignByResolutionIdResult> assignList = dao.GetListAssign(jobRequest.WFResolutionID); 
            if (existItem == null)
            {
                if (jobRequest.AssignID.HasValue && jobRequest.AssignRole.HasValue)
                {
                    UserAdmin item = userAdminDao.GetById(jobRequest.AssignID.Value);
                    WFRole role = roleDao.GetByID(jobRequest.AssignRole.Value);
                    //set default assign item for list

                    if (item != null && role != null)
                    {
                        ViewData["FirstChoiceAssign"] = item.UserName + " (" + role.Name + ")";
                    }
                    ViewData["Assign"] = new SelectList(new List<string>());
                }
                else
                {
                    ViewData["FirstChoiceAssign"] = string.Empty;
                }
            }
            else
            {
                ViewData["FirstChoiceAssign"] = string.Empty;
                ViewData["Assign"] = new SelectList(assignList, "UserAdminRole", "DisplayName", jobRequest.AssignID + "@" + jobRequest.AssignRole);
            }

            #endregion
            string name = string.Empty;
            if (!string.IsNullOrEmpty(jobRequest.CCList))
            {
                foreach (string item in jobRequest.CCList.TrimEnd(',').Split(','))
                {
                    string loginName = CommonFunc.GetLoginNameByEmail(item);
                    name += loginName + ",";
                }
            }
            ViewData["ViewCC"] = name;
            return View(jobRequest);
        }

        //
        // POST: /JobRequest/Edit/5

        [HttpPost]
        public ActionResult Edit(JobRequest request)
        {            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;

            #region Check permission
            bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_JOB_REQUEST,
                    request.ID.ToString(), Constants.ActionType.Update, principal.UserData.Role);
            if (!check)
                return View("../Common/NotPermission");
            #endregion

            string rdmanyValue = Request["Position"];
            if (rdmanyValue != Constants.MANY_POSITION_CHECK)
                request.PositionTo = null;
            bool isHold = false;
            JobRequest objDb = dao.GetById(request.ID);
            request.InvolveID += objDb.AssignID + Constants.SEPARATE_INVOLVE_SIGN;
            request.InvolveRole += objDb.AssignRole + Constants.SEPARATE_INVOLVE_SIGN;
            request.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
            string userAdminRole = Request["Assign"];
            if (!string.IsNullOrEmpty(userAdminRole))
            {
                string[] arr = userAdminRole.Split(Constants.SEPARATE_USER_ADMIN_ID);
                

                if (request.WFResolutionID == Constants.RESOLUTION_NEW_ID)
                {
                    if ((int.Parse(arr[0].Trim()) != objDb.AssignID.Value) && (int.Parse(arr[1].Trim())) == Constants.REQUESTOR_ID)
                    {
                        request.InvolveResolution += Constants.JR_ACTION_ASSIGNTO + Constants.SEPARATE_INVOLVE_SIGN;
                    }
                    else if ((int.Parse(arr[0].Trim()) == objDb.AssignID.Value) && (int.Parse(arr[1].Trim())) == Constants.REQUESTOR_ID)
                    {
                        isHold = true;
                    }
                }
                else if (request.WFResolutionID == Constants.RESOLUTION_TO_BE_APPROVED_BY_MANAGER_ID) //case send JR for manager
                {
                    request.InvolveResolution += Constants.JR_ACTION_SUBMIT + Constants.SEPARATE_INVOLVE_SIGN;
                }
                request.AssignID = int.Parse(arr[0].Trim());
                request.AssignRole = int.Parse(arr[1].Trim());
            }
            else
            {
                 if(request.WFResolutionID == Constants.RESOLUTION_CANCEL_ID)//case Close JR
                {
                    request.InvolveResolution += Constants.JR_ACTION_CANCEL + Constants.SEPARATE_INVOLVE_SIGN;
                }
            }
            request.UpdatedBy = principal.UserData.UserName;
            Message msg = dao.Update(request);                           
            if ((!string.IsNullOrEmpty(userAdminRole) || request.WFStatusID == Constants.STATUS_CLOSE) && (msg.MsgType == MessageType.Info) && (isHold == false))
            {
                SendMail(request.ID, 0);
            }
            ShowMessage(msg);
            return GotoCallerPage();
        }



        /// <summary>
        /// Delete list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        [HttpPost]
        public ActionResult DeleteList(string id)
        {
            Message msg = null;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            if (principal.UserData.Role != Constants.REQUESTOR_ID)
            {
                throw new ForbiddenException();
            }
            bool isValid = true;
            string[] idArr = id.Split(',');
            foreach (string value in idArr)
            {
                
                List<sp_GetEmployeeResult> listEmployeeActive = new EmployeeDao().GetList("", 0, 0, 0, Constants.EMPLOYEE_ACTIVE, Constants.RESIGNED).Where(p => p.JR == value).ToList<sp_GetEmployeeResult>();
                List<sp_GetEmployeeResult> listEmployeeResigned = new EmployeeDao().GetList("", 0, 0, 0, Constants.EMPLOYEE_NOT_ACTIVE, Constants.RESIGNED).Where(p => p.JR == value).ToList<sp_GetEmployeeResult>();
                List<sp_GetSTTResult> listSTT = new STTDao().GetList("",0,0,null,null,null,null,null).Where(p => (p.JR == value)).ToList<sp_GetSTTResult>();
                if (listEmployeeActive.Count > 0 || listEmployeeResigned.Count > 0 || listSTT.Count > 0)
                {
                    isValid = false;
                    msg = new Message(MessageConstants.E0006, MessageType.Error, "delete " + Constants.JOB_REQUEST_PREFIX + value, "it");
                }
            }
            if (isValid)
            {
                msg = dao.DeleteList(id, principal.UserData.UserName);
            }
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        /// <summary>
        /// Email send to involed persion in job request workflow 
        /// </summary>
        /// <param name="jobRequestId"></param>
        protected void SendMail(int jobRequestId,int jrItemId)
        {
            // Get body detail
            JobRequest jobRequest = new JobRequest();
            JobRequestItem jrItem = null;
            if (jrItemId != 0)
            {
                jrItem = jrItemDao.GetByID(jrItemId.ToString());
                jobRequest = jrItem.JobRequest;
            }
            else
            {
                jobRequest = dao.GetById(jobRequestId);
            }
            WFRole role = null;
            if (jobRequest == null)
                return;
            else
                role = roleDao.GetByID(jobRequest.AssignRole.Value);

            string from_email = ConfigurationManager.AppSettings["from_email"];
            string host = ConfigurationManager.AppSettings["mailserver_host"];
            string port = ConfigurationManager.AppSettings["mailserver_port"];
            string poster = "Job Request";
            string subject = string.Empty;
            if (jrItem!=null)
            {
                subject = "JR #" + jrItem.ID + " has been closed";
            }
            else
            {
                if (role != null)
                    subject = "JRG #" + jobRequest.ID + " has been forwarded to " + jobRequest.UserAdmin1.UserName + " (" + role.Name + ")";
            }

            string body = MessageBody(jobRequest, jrItem, role);


            string to_email = string.Empty;
            string cc_email = string.Empty;
            string[] arrIds = jobRequest.InvolveID.Split(Constants.SEPARATE_INVOLVE_CHAR);
            string[] arrRoles = jobRequest.InvolveRole.Split(Constants.SEPARATE_INVOLVE_CHAR);

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
                        if (jobRequest.WFStatusID == Constants.STATUS_CLOSE) //If an email has "Close" status, just only send by "To" section, not cc 
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
                                UserAdmin_WFRole currentAssign = jrAdminDao.GetByUserAdminId(jobRequest.AssignID.Value);
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
            if (!string.IsNullOrEmpty(jobRequest.CCList))
            {
                string[] array = jobRequest.CCList.Split(';');
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

        //Read email template
        private string MessageBody(JobRequest jobRequest, JobRequestItem jrItem, WFRole role)
        {
            string path = string.Empty;

            //load template emails by jr status
            if(jrItem == null)
                path = Server.MapPath("~/Views/JobRequest/TemplateOpen.htm");
            else if (jrItem.StatusID == Constants.JR_ITEM_STATUS_SUCCESS)
                path = Server.MapPath("~/Views/JobRequest/TemplateClose.htm");
            else
                path = Server.MapPath("~/Views/JobRequest/TemplateCancel.htm");
            
            string content = WebUtils.ReadFile(path);
           
            //replace the holders on template emails.
            if (jobRequest != null && role != null)
            {
                content = content.Replace(Constants.JR_REQUEST_ID_HOLDER, jobRequest.ID.ToString());
                content = content.Replace(Constants.JR_REQUEST_DATE_HOLDER, jobRequest.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                content = content.Replace(Constants.JR_REQUESTOR_HOLDER, jobRequest.UserAdmin.UserName);
                content = content.Replace(Constants.JR_ASSIGN_TO_HOLDER, jobRequest.UserAdmin1.UserName);
                content = content.Replace(Constants.JR_ASSIGN_TO_NAME_HOLDER, jobRequest.UserAdmin1.UserName + " (" + role.Name + ")");
                content = content.Replace(Constants.JR_DEPARMENT_HOLDER, deptDao.GetDepartmentNameBySub(jobRequest.DepartmentId));
                content = content.Replace(Constants.JR_SUBDEPARMENT_HOLDER, jobRequest.Department.DepartmentName);
                content = content.Replace(Constants.JR_QUANTITY_HOLDER, jobRequest.Quantity.ToString());

                if (jobRequest.PositionTo.HasValue)
                {
                    content = content.Replace(Constants.JR_POSITION_HOLDER, jobRequest.JobTitleLevel.DisplayName + " --> " + jobRequest.JobTitleLevel1.DisplayName);
                }
                else
                {
                    content = content.Replace(Constants.JR_POSITION_HOLDER, jobRequest.JobTitleLevel.DisplayName);
                }

                if (jobRequest.ExpectedStartDate.HasValue)
                    content = content.Replace(Constants.JR_EXPECTED_START_DATE_HOLDER, jobRequest.ExpectedStartDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW));
                else
                    content = content.Replace(Constants.JR_EXPECTED_START_DATE_HOLDER, Constants.JR_EMPTY_HOLDER);

                content = content.Replace(Constants.JR_SALARY_SUGGESTION_HOLDER, !string.IsNullOrEmpty(jobRequest.SalarySuggestion) ?
                    Constants.PRIVATE_DATA : Constants.JR_EMPTY_HOLDER);


                
                content = content.Replace(Constants.JR_JUSTIFICATION_HOLDER, !string.IsNullOrEmpty(jobRequest.Justification) ? jobRequest.Justification : Constants.JR_EMPTY_HOLDER);
                content = content.Replace(Constants.JR_RESOLUTION_HOLDER, jobRequest.WFResolution.Name);
                content = content.Replace(Constants.JR_REQUEST_TYPE_HOLDER, jobRequest.RequestTypeId == Constants.JR_REQUEST_TYPE_NEW ? "New":"Replace");
                content = content.Replace(Constants.JR_STATUS_HOLDER, jobRequest.WFStatus.Name);

                string link = string.Empty;
                if (jrItem!=null && jrItem.StatusID != Constants.JR_ITEM_STATUS_OPEN)
                {
                    content = content.Replace(Constants.JR_REQUEST_ITEM_ID_HOLDER, jrItem.ID.ToString());
                    if (jrItem.StatusID == Constants.JR_ITEM_STATUS_SUCCESS)
                    {
                        content = content.Replace(Constants.JR_REQUEST_ITEM_JOB_TITLE_HOLDER, jrItem.FinalTitleId.HasValue?jrItem.JobTitleLevel.DisplayName:jrItem.Title);
                        content = content.Replace(Constants.JR_CANDIDATE, jrItem.Candidate);
                        content = content.Replace(Constants.JR_EMP_ID, jrItem.EmpID);
                        content = content.Replace(Constants.JR_GENDER, Constants.Gender.Single(p => bool.Parse(p.Value) == jrItem.Gender).Text);
                        content = content.Replace(Constants.JR_ACTUAL_START_DATE, jrItem.ActualStartDate.HasValue ? jrItem.ActualStartDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : Constants.JR_EMPTY_HOLDER);
                        content = content.Replace(Constants.JR_PROBATION_SALARY_NOTE, !string.IsNullOrEmpty(jrItem.ProbationSalaryNote) ? jrItem.ProbationSalaryNote : Constants.JR_EMPTY_HOLDER);
                        content = content.Replace(Constants.JR_CONTRACTED_SALARY_NOTE, !string.IsNullOrEmpty(jrItem.ContractedSalaryNote) ? jrItem.ContractedSalaryNote : Constants.JR_EMPTY_HOLDER);
                        content = content.Replace(Constants.JR_PROBATION_SALARY, !string.IsNullOrEmpty(jrItem.ProbationSalary) ? Constants.PRIVATE_DATA : Constants.JR_EMPTY_HOLDER);
                        content = content.Replace(Constants.JR_CONTRACTED_SALARY, !string.IsNullOrEmpty(jrItem.ContractedSalary) ? Constants.PRIVATE_DATA : Constants.JR_EMPTY_HOLDER);
                        if (jrItem.ProbationMonths.HasValue)
                            content = content.Replace(Constants.JR_PROBATION_MONTH_HOLDER, jrItem.ProbationMonths.Value.ToString());
                        else
                            content = content.Replace(Constants.JR_PROBATION_MONTH_HOLDER, Constants.JR_EMPTY_HOLDER);
                    }
                    else
                    {
                        content = content.Replace(Constants.JR_ITEM_STATUS_HOLDER, jrItem.JobRequestItemStatus.StatusName);
                    }
                    link = "http://" + Request["SERVER_NAME"] + ":" + Request["SERVER_PORT"] + "/JobRequest" + "/Detail/" + jobRequest.ID;
                }
                else
                {
                    link = "http://" + Request["SERVER_NAME"] + ":" + Request["SERVER_PORT"] + "/JobRequest" + "/?role=" + jobRequest.AssignRole;
                }
                content = content.Replace(Constants.JR_LINK_HOLDER, link);
            }
            return content;
        }
        #endregion

        #region JR for HR by Tan Tran

        /*public ActionResult HRFillData(int id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            JobRequest viewData = dao.GetById(id);
            //ViewData["FinalTitleId"] = new SelectList(commDao.GetJobTitleList(deptDao.GetDepartmentIdBySub(viewData.DepartmentId).Value), "ID", "DisplayName", viewData.FinalTitleId);
            ViewData["WFResolutionID"] = new SelectList(resDao.GetListResolutionChangeByRole(principal.UserData.Role), "ID", "Name", "");
            ViewData["WFStatusID"] = new SelectList(new List<string>());
            ViewData["Assign"] = new SelectList(dao.GetListByRole(Constants.HR_ID), "UserAdminRole", "DisplayName", principal.UserData.UserID + Constants.SEPARATE_USER_ADMIN_ID_STRING + principal.UserData.Role);
            return View(viewData);
        }*/


        public ActionResult HRFillData(int id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region Check permission
            bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_JOB_REQUEST_ITEM,
                    id.ToString(), Constants.ActionType.Update, principal.UserData.Role);
            if (!check)
                return View("../Common/NotPermission");
            #endregion

            JobRequestItem viewData = dao.GetItemById(id);
            ViewData["Assign"] = new SelectList(dao.GetListByRole(Constants.HR_ID), "UserAdminRole", "DisplayName", principal.UserData.UserID + Constants.SEPARATE_USER_ADMIN_ID_STRING + principal.UserData.Role);

                 Employee objEmployee = new EmployeeDao().GetByJR(id.ToString());

                if (objEmployee != null)
                {
                    ViewData["ObjEmployee"] = objEmployee;
                    ViewData["WFResolutionID"] = new SelectList(resDao.GetListResolutionChangeByRole(principal.UserData.Role), "ID", "Name", Constants.RESOLUTION_COMPLETED_ID);
                    ViewData["StatusID"] = new SelectList(dao.GetJRItemStatus(), "StatusID", "StatusName", Constants.STATUS_CLOSE);
                    ViewData["FinalTitleView"] = new SelectList(commDao.GetJobTitleList(deptDao.GetDepartmentIdBySub(viewData.JobRequest.DepartmentId).Value), "ID", "DisplayName", objEmployee.TitleId);
                    return View(viewData);
                }
                else
                {
                    STT objSTT = new STTDao().GetByJR(id.ToString());
                    if (objSTT != null)
                    {
                        ViewData["ObjSTT"] = objSTT;
                        ViewData["WFResolutionID"] = new SelectList(resDao.GetListResolutionChangeByRole(principal.UserData.Role), "ID", "Name", Constants.RESOLUTION_COMPLETED_ID);
                        ViewData["StatusID"] = new SelectList(dao.GetJRItemStatus(), "StatusID", "StatusName", Constants.STATUS_CLOSE);
                        ViewData["FinalTitleId"] = Constants.STT_DEFAULT_VALUE;
                        return View(viewData);
                    }
                }
            ViewData["WFResolutionID"] = new SelectList(resDao.GetListResolutionChangeByRole(principal.UserData.Role), "ID", "Name", "");
            ViewData["StatusID"] = new SelectList(dao.GetJRItemStatus(), "StatusID", "StatusName", viewData.StatusID);
            ViewData["FinalTitleId"] = new SelectList(commDao.GetJobTitleList(deptDao.GetDepartmentIdBySub(viewData.JobRequest.DepartmentId).Value), "ID", "DisplayName", viewData.FinalTitleId);
            return View(viewData);
        }

        [HttpPost]
        public ActionResult HRFillData(JobRequestItem obj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region Check permission
            bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_JOB_REQUEST_ITEM,
                    obj.ID.ToString(), Constants.ActionType.Update, principal.UserData.Role);
            if (!check)
                return View("../Common/NotPermission");
            #endregion
            obj.UpdatedBy = principal.UserData.UserName;
            
            Message msg = dao.HRFillData(obj);
            if (msg.MsgType == MessageType.Info)
            {
                if (obj.StatusID != Constants.JR_ITEM_STATUS_OPEN)
                {
                    JobRequestItem item = dao.GetItemById(obj.ID);
                    SendMail(item.JobRequest.ID, obj.ID);
                }
            }
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        public ActionResult ForwardTo(int id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region Check permission
            bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_JOB_REQUEST,
                    id.ToString(), Constants.ActionType.Update, principal.UserData.Role);
            if (!check)
                return View("../Common/NotPermission");
            #endregion

            JobRequest jr = dao.GetById(id);

            ViewData["Assign"] = new SelectList(dao.GetListByRole(Constants.HR_ID), "UserAdminRole", "DisplayName", 
                principal.UserData.UserID + Constants.SEPARATE_USER_ADMIN_ID_STRING + principal.UserData.Role);

            return View(jr);
        }

        [HttpPost]
        public ActionResult ForwardTo(JobRequest obj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region Check permission
            bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_JOB_REQUEST,
                    obj.ID.ToString(), Constants.ActionType.Update, principal.UserData.Role);
            if (!check)
                return View("../Common/NotPermission");
            #endregion

            JobRequest objDb = dao.GetById(obj.ID);

            string userAdminRole = Request["Assign"];
            string[] array = userAdminRole.Split(Constants.SEPARATE_USER_ADMIN_ID);

            obj.InvolveResolution = objDb.InvolveResolution + Constants.JR_ACTION_ASSIGNTO + Constants.SEPARATE_INVOLVE_SIGN;
            obj.InvolveID = objDb.InvolveID + array[0].Trim() + Constants.SEPARATE_INVOLVE_SIGN;
            obj.InvolveRole = objDb.InvolveRole + array[1] + Constants.SEPARATE_INVOLVE_SIGN;
            obj.InvolveDate = objDb.InvolveDate + DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
            obj.AssignID = int.Parse(array[0]);
            obj.AssignRole = int.Parse(array[1]);
            obj.UpdatedBy = principal.UserData.UserName;

            Message msg = dao.ForwardTo(obj);

            if (msg.MsgType == MessageType.Info)
            {                
                SendMail(obj.ID, 0);
            }

            ShowMessage(msg);
            return GotoCallerPage();
        }

        #endregion

        #region hung nguyen

        public ActionResult ExportToExcel(string text, string department, string subdepartment, string positionId, string requestorId, string statusId,string request)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            if (text == Constants.JOB_REQUEST)
            {
                text = "";
            }
            string containsJR = Constants.JOB_REQUEST_PREFIX;
            if (containsJR.Contains(text))
            {
                text = string.Empty;
            }
            else
            {
                if (text.Length >= 6)
                {
                    text = text.Substring(containsJR.Length);
                }
            }
            List<sp_GetJobRequestResult> list = GetListByFilter(text, department, subdepartment, 
                positionId, requestorId, statusId, principal.UserData.UserID,
                principal.UserData.Role.ToString(), request);
            ExportExcel exp = new ExportExcel();
            exp.Title = Constants.JR_TILE_EXPORT_EXCEL;
            exp.FileName = Constants.JR_EXPORT_EXCEL_NAME;
            exp.ColumnList = new string[] { "ID:JR", "StatusName", "ResolutionName", "RequestorName", "RequestDate:Date","RequestTypeId:JR_Request", "Department", "SubDepartment", "Position", "Quantity","ExpectedStartDate:Date", "AssignName", "SalarySuggestion", "Justification" };
            exp.HeaderExcel = new string[] { "Request #", "Status", "Resolution", "Requestor", "Request Date", "Request Type", "Department", "Sub Department", "Position", "Quantity", "Expected Start Date", "Forwarded To", "Salary Suggestion", "Justification" };
            exp.List = list;
            exp.Title = "test";
            exp.IsRenderNo = true;
            exp.Execute();
            return View();
        }

        public ActionResult Detail(int id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            try
            {
                #region Combobox Role
                List<WFRole> list = commDao.GetRoleList(principal.UserData.UserID, Constants.WORK_FLOW_JOB_REQUEST);
                //Display login role dropdownlist for choosing
                if (list.Count > 1)
                {
                    ViewData["Role"] = new SelectList(list, "ID", "Name", principal.UserData.Role);
                }
                #endregion
                //For navigation
                List<sp_GetJobRequestResult> listPR = GetListJobRequestForNavigation();
                ViewData["ListJR"] = listPR;
                sp_GetJobRequestResult viewData = dao.GetDetailById(id);
                ViewData["Comment"] = jrCommentDao.GetList(id);
                if (jrCommentDao.GetList(id).Count > 0)
                {
                    ViewData["CommentCount"] = jrCommentDao.GetList(id).Count;
                }
                ViewData["ItemList"] = dao.GetJobRequestItemList(id);
                JobRequest jobRequest = dao.GetById(id);
                string flow = string.Empty;
                string[] arrIds = jobRequest.InvolveID.Split(Constants.SEPARATE_INVOLVE_CHAR);
                string[] arrRoles = jobRequest.InvolveRole.Split(Constants.SEPARATE_INVOLVE_CHAR);
                string[] arrResolution = jobRequest.InvolveResolution.Split(Constants.SEPARATE_INVOLVE_CHAR);
                string[] arrDate = jobRequest.InvolveDate.Split(Constants.SEPARATE_INVOLVE_CHAR);
                for (int i = 0; i < arrIds.Length - 1; i++)
                {
                    //check duplicate person on user name and role.
                    UserAdmin userAdmin = userAdminDao.GetById(int.Parse(arrIds[i]));
                    WFRole role = roleDao.GetByID(int.Parse(arrRoles[i]));
                    if (i == arrIds.Length - 1)  // Last Role in WorkFlow
                    {
                        flow += userAdmin.UserName + " (" + role.Name + ") ";
                    }
                    else
                    {
                        flow += userAdmin.UserName + " (" + role.Name + ");" + arrResolution[i] + ";" + arrDate[i] + ",";
                    }

                }

                ;

                ViewData["WorkFlow"] = flow + userAdminDao.GetById(jobRequest.AssignID.Value).UserName + ";;,";
                /*Added by Tai nguyen*/
                bool canViewSalary = groupDao.HasPermisionOnModule(
                    userAdminDao.GetByUserName(principal.UserData.UserName).UserAdminId,
                    (int)Permissions.Read,
                    (int)Modules.ViewSalaryInfo);
                ViewData[CommonDataKey.JR_CAN_VIEW_SALARY] = canViewSalary;
                /*End Tai nguyen*/
                ViewData[CommonDataKey.JR_ACTIONS] =
                    SetAction(viewData.AssignRole.Value, viewData.AssignID.Value, viewData.WFStatusID, viewData.WFResolutionID, viewData.ID);
                return View(viewData);
            }
            catch (Exception)
            {
                Message msg = new Message(MessageConstants.E0007, MessageType.Error);
                ShowMessage(msg);
                return RedirectToAction("Index");
            }
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

        [HttpPost]
        public ActionResult AddComment(JobRequestComment obj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            // TODO: Add insert logic here
            obj.PostTime = DateTime.Now;
            obj.Poster = principal.UserData.UserName;
            Message msg = jrCommentDao.Insert(obj);
            ShowMessage(msg);
            return RedirectToAction("Detail/" + int.Parse(obj.RequestID.ToString()));
        }
       
        public ActionResult ManagerResult(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {


                string[] array = id.Split(Constants.SEPARATE_USER_ADMIN_ID);
                if (array[0] != null && array[1] != null)
                {
                    var principal = HttpContext.User as AuthenticationProjectPrincipal;
                    #region Check permission
                    bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_JOB_REQUEST,
                            array[0], Constants.ActionType.Update, principal.UserData.Role);
                    if (!check)
                        return View("../Common/NotPermission");
                    #endregion
                    JobRequest jr = dao.GetById(int.Parse(array[0]));
                    string requestorDefault = jr.RequestorId.ToString() + Constants.SEPARATE_USER_ADMIN_ID + Constants.REQUESTOR_ID.ToString();
                    ViewData["RequestId"] = array[0];
                    ViewData["ResolutionId"] = array[1];
                    ViewData["UpdateDate"] = jr.UpdateDate.ToString();
                    ViewData["Assign"] = new SelectList(dao.GetListAssign(int.Parse(array[1])), "UserAdminRole", "DisplayName", requestorDefault);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult ManagerResult(FormCollection content)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                string contents = content["Contents"];
                string id = content["RequestId"];
                string resolutionId = content["ResolutionId"];
                string userAdminRole = content["Assign"];
                string updateDate = content["UpdateDate"];

                #region Check permission
                bool check = CommonFunc.CheckMovingRequest(principal.UserData.UserID, Constants.FlowType.FLOW_JOB_REQUEST,
                        id, Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return View("../Common/NotPermission");
                #endregion

                if (!string.IsNullOrEmpty(userAdminRole))
                {
                    string[] stringArray = (userAdminRole).Split(Constants.SEPARATE_USER_ADMIN_ID);
                    #region Job Request Comment
                    JobRequestComment obj = new JobRequestComment();
                    obj.RequestID = int.Parse(id);
                    obj.PostTime = DateTime.Now;
                    obj.Poster = principal.UserData.UserName;
                    obj.Contents = contents;
                    #endregion

                    msg = DoUpdateJobRequest(id, resolutionId, stringArray, principal.UserData.UserName, updateDate, obj);
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return GotoCallerPage();
        }

        /// <summary>
        /// Do Update Job Request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resolutionId"></param>
        /// <param name="array"></param>
        /// <param name="userName"></param>
        /// <param name="updateDate"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Message DoUpdateJobRequest(string id, string resolutionId, string[] array, string userName, string updateDate, JobRequestComment obj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = null;
            JobRequest objUi = dao.GetById(int.Parse(id));
            if (objUi != null)
            {
                if (int.Parse(resolutionId) == Constants.RESOLUTION_REJECT_ID)
                {
                    objUi.InvolveResolution += Constants.JR_ACTION_REJECT + Constants.SEPARATE_INVOLVE_SIGN;
                }
                else
                {
                    objUi.InvolveResolution += Constants.JR_ACTION_APPROVE + Constants.SEPARATE_INVOLVE_SIGN;
                }   
                objUi.WFResolutionID = int.Parse(resolutionId);
                objUi.AssignID = int.Parse(array[0]);
                objUi.AssignRole = int.Parse(array[1]);
                objUi.InvolveID += principal.UserData.UserID + Constants.SEPARATE_INVOLVE_SIGN;
                objUi.InvolveRole += principal.UserData.Role + Constants.SEPARATE_INVOLVE_SIGN;
                objUi.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                if (!string.IsNullOrEmpty(updateDate))
                {
                    objUi.UpdateDate = DateTime.Parse(updateDate);
                }
                objUi.UpdatedBy = userName;
                msg = dao.UpdateForApproval(objUi, obj);                

                if (msg.MsgType == MessageType.Info && objUi.WFResolutionID != Constants.RESOLUTION_REJECT_ID)
                {
                    // Auto generate job request items
                    dao.GenerateJobRequest(objUi.ID, principal.UserData.UserName, (int)objUi.AssignID, (int) objUi.AssignRole);
                    SendMail(objUi.ID,0);
                }
            }

            return msg;
        }

        public ActionResult GotoCallerPage()
        {
            if (Request.Params.AllKeys.Contains(CommonDataKey.RETURN_URL))
                return Redirect(Request[CommonDataKey.RETURN_URL]);
            else
                return RedirectToAction("Index");
        }
        #endregion

        private List<sp_GetJobRequestResult> GetListByFilter(string text, string department, 
            string subdepartment, string positionId, string requestorId, string statusId, 
            int assignId, string assignRole, string request)
        {
            string textSearch = null;
            int idepartment = 0;
            int isubdepartment = 0;
            int ipositionId = 0;
            int irequestorId = 0;
            int istatusId = 0;
            int requestType = 0;
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
            if (!string.IsNullOrEmpty(positionId))
            {
                ipositionId = int.Parse(positionId);
            }
            if (!string.IsNullOrEmpty(requestorId))
            {
                irequestorId = int.Parse(requestorId);
            }
            if (!string.IsNullOrEmpty(statusId))
            {
                istatusId = int.Parse(statusId);
            }
            if (!string.IsNullOrEmpty(request))
            {
                requestType = int.Parse(request);
            }

            List<sp_GetJobRequestResult> list = dao.GetList(textSearch, idepartment, isubdepartment,
                ipositionId, irequestorId, istatusId, assignRole, requestType);
            return list;
        }

        private List<sp_GetJobRequestForHRResult> GetListForHRByFilter(string text, string department, string subdepartment, string positionId, string requestorId, string statusId, string role, string request)
        {
            string textSearch = null;
            int idepartment = 0;
            int isubdepartment = 0;
            int ipositionId = 0;
            int irequestorId = 0;
            int istatusId = 0;
            int requestType = 0;
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
            if (!string.IsNullOrEmpty(positionId))
            {
                ipositionId = int.Parse(positionId);
            }
            if (!string.IsNullOrEmpty(requestorId))
            {
                irequestorId = int.Parse(requestorId);
            }
            if (!string.IsNullOrEmpty(statusId))
            {
                istatusId = int.Parse(statusId);
            }
            if (!string.IsNullOrEmpty(request))
            {
                requestType = int.Parse(request);
            }

            List<sp_GetJobRequestForHRResult> list = dao.GetListForHR(textSearch, idepartment, isubdepartment, ipositionId, irequestorId, istatusId, role, requestType);
            return list;
        }

        /// <summary>
        /// Check GroupName Exits
        /// </summary>
        /// <param name="displayorder"></param>
        /// <returns></returns>
        public JsonResult IsApprovalExist(string approval)
        {
            //TODO: Do the validation                          
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            bool isExist = dao.IsApprovalExist(approval);
            if (isExist)
            {
                string msg = string.Format(Resources.Message.E0003, approval);
                result.Data = msg;
            }
            else
                result.Data = true;
            return result;
        }

        public ActionResult CandidateTooltip(string id)
        {
            JobRequestItem jrItem = jrItemDao.GetByID(id);
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            GroupDao groupDao = new GroupDao();

            bool canViewSalary = groupDao.HasPermisionOnModule(
                userAdminDao.GetByUserName(principal.UserData.UserName).UserAdminId, 
                (int)Permissions.Read, 
                (int)Modules.ViewSalaryInfo);
            ViewData[CommonDataKey.JR_CAN_VIEW_SALARY] = canViewSalary;
            return View(jrItem);
        }
        public ActionResult DetailTooltip(int id)
        {
            JobRequest jobRequest = dao.GetById(id);
            List<JobRequestItem> listJrItem = jrItemDao.GetListByJrId(id);
            ViewData[CommonDataKey.JR_OBJECT] = jobRequest;
            return View(listJrItem);
        }

    }
}
