using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Common;
using CRM.Library.Exceptions;
using CRM.Library.Utils;
using CRM.Models;
using System.Collections;
using CRM.Library.Attributes;

namespace CRM.Controllers
{
    /// <summary>
    /// Performance review HR
    /// </summary>
    public class PerformanceReviewHRController : BaseController
    {
        #region Variable
        private PerformanceReviewDao perDao = new PerformanceReviewDao();
        private JobRequestDao dao = new JobRequestDao();
        private DepartmentDao depDao = new DepartmentDao();
        private EmployeeDao empDao = new EmployeeDao();
        private StatusDao statusDao = new StatusDao();
        private EformDao eformDao = new EformDao();
        private WorkflowDao wfDao = new WorkflowDao();
        private WFStatusDao wfStatusDao = new WFStatusDao();
        private ResolutionDao resDao = new ResolutionDao();
        private CommonDao commDao = new CommonDao();
        private PerformanceReviewCommentDao prCmDao = new PerformanceReviewCommentDao();
        private UserAdminDao userAdminDao = new UserAdminDao();
        private RoleDao roleDao = new RoleDao();
        #endregion

        #region Methods

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        //[CrmAuthorizeAttribute(Module = Modules.PerformanceReviewHR, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Message msg = null;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            List<WFRole> list = commDao.GetRoleList(principal.UserData.UserID, Constants.WORK_FLOW_PERFORMANCE_REVIEW);
            WFRole role = list.Where(c => c.ID == Constants.PRW_ROLE_HR_ID).FirstOrDefault();
            try
            {
                if (role != null)
                {
                    Hashtable hashData = Session[SessionKey.PERFORMANCE_REVIEW_HR_FILTER] == null ? new Hashtable() : (Hashtable)Session[SessionKey.PERFORMANCE_REVIEW_HR_FILTER];
                    ViewData[Constants.PRW_HR_LIST_NAME] = hashData[Constants.PRW_HR_LIST_NAME] == null ? Constants.PER_REVIEW_FIRST_KEY_WORD :
                        !string.IsNullOrEmpty((string)hashData[Constants.PRW_HR_LIST_NAME]) ? hashData[Constants.PRW_HR_LIST_NAME] : Constants.PER_REVIEW_FIRST_KEY_WORD;
                    ViewData[Constants.PRW_HR_LIST_NEED] = new SelectList(Constants.NeedReview, "Value", "Text",
                        hashData[Constants.PRW_HR_LIST_NEED] == null ? "" : hashData[Constants.PRW_HR_LIST_NEED].ToString());
                    ViewData[Constants.PRW_HR_LIST_STATUS] = new SelectList(wfStatusDao.GetList(), "ID", "Name",
                        hashData[Constants.PRW_HR_LIST_STATUS] == null ? "" : hashData[Constants.PRW_HR_LIST_STATUS].ToString());
                    ViewData[Constants.PRW_HR_LIST_COLUMN] = hashData[Constants.PRW_HR_LIST_COLUMN] == null ? "ID" : hashData[Constants.PRW_HR_LIST_COLUMN].ToString();
                    ViewData[Constants.PRW_HR_LIST_ORDER] = hashData[Constants.PRW_HR_LIST_ORDER] == null ? "desc" : hashData[Constants.PRW_HR_LIST_ORDER].ToString();
                    ViewData[Constants.PRW_HR_LIST_PAGE_INDEX] = hashData[Constants.PRW_HR_LIST_PAGE_INDEX] == null ? "1" : hashData[Constants.PRW_HR_LIST_PAGE_INDEX].ToString();
                    ViewData[Constants.PRW_HR_LIST_ROW_COUNT] = hashData[Constants.PRW_HR_LIST_ROW_COUNT] == null ? "20" : hashData[Constants.PRW_HR_LIST_ROW_COUNT].ToString();
                    return View();
                }
                else
                {
                    msg = new Message(MessageConstants.E0002, MessageType.Error);
                    throw new ForbiddenExceptionOnCurrentPage();
                }
            }
            catch (ForbiddenExceptionOnCurrentPage)
            {
                return RedirectToAction("../Home/Index");
            }

        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.PERFORMANCE_REVIEW_HR_FILTER);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get list JQGrid
        /// </summary>
        /// <param name="filterText">string</param>
        /// <param name="status">string</param>
        /// <param name="need">string</param>
        /// <returns>ActionResult</returns>        
        public ActionResult GetListHrJQGrid(string filterText, string status, string need)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;

            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(filterText, status, need, sortColumn, sortOrder, pageIndex, rowCount);

            filterText = filterText.Trim();
            if (filterText == Constants.PER_REVIEW_FIRST_KEY_WORD)
            {
                filterText = "";
            }
            object jsonData = null;
            // get data for HR            
            if (string.IsNullOrEmpty(status))
                status = "0";
            var list = perDao.GetList(filterText, int.Parse(status),
                need.Equals(Constants.PER_REVIEW_NEED_PR_LIST_ID), principal.UserData.Role.ToString());
            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = perDao.Sort(list, sortColumn, sortOrder)
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
                            m.ID,
                            m.EmployeeName,
                            m.Department,
                            m.ManagerName,
                            "",                                
                            ""
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Set session for filter
        /// </summary>
        /// <param name="name">keyword</param>
        /// <param name="status">status</param>
        /// <param name="need">need</param>
        /// <param name="column">sort column</param>
        /// <param name="order">sort order</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="rowCount">row count</param>
        private void SetSessionFilter(string name, string status, string need, string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.PRW_HR_LIST_NAME, name);
            hashData.Add(Constants.PRW_HR_LIST_STATUS, status);
            hashData.Add(Constants.PRW_HR_LIST_NEED, need);
            hashData.Add(Constants.PRW_HR_LIST_COLUMN, column);
            hashData.Add(Constants.PRW_HR_LIST_ORDER, order);
            hashData.Add(Constants.PRW_HR_LIST_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.PRW_HR_LIST_ROW_COUNT, rowCount);

            Session[SessionKey.PERFORMANCE_REVIEW_HR_FILTER] = hashData;
        }

        /// <summary>
        /// Export to excel
        /// </summary>
        /// <param name="filterText">string</param>
        /// <param name="status">string</param>
        /// <param name="need">string</param>
        /// <returns>ActionResult</returns>
        [NonAction]
        public ActionResult ExportToExcel(string filterText, string status, string need)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            filterText = filterText.Trim();
            if (filterText == Constants.PER_REVIEW_FIRST_KEY_WORD)
            {
                filterText = "";
            }
            if (string.IsNullOrEmpty(status))
                status = "0";
            var list = perDao.GetList(filterText, int.Parse(status),
                need.Equals(Constants.PER_REVIEW_NEED_PR_LIST), principal.UserData.Role.ToString());

            ExportExcel exp = new ExportExcel();
            exp.Title = Constants.JR_TILE_EXPORT_EXCEL;
            exp.FileName = Constants.JR_EXPORT_EXCEL_NAME;
            exp.ColumnList = new string[] { "ID:JR", "StatusName", "ResolutionName", "RequestorName", "RequestDate:Date", "RequestTypeId:JR_Request", "Department", "SubDepartment", "Position", "Quantity", "ExpectedStartDate:Date", "AssignName", "SalarySuggestion", "Justification" };
            exp.HeaderExcel = new string[] { "Request #", "Status", "Resolution", "Requestor", "Request Date", "Request Type", "Department", "Sub Department", "Position", "Quantity", "Expected Start Date", "Forwarded To", "Salary Suggestion", "Justification" };
            exp.List = list;
            exp.Title = "test";
            exp.IsRenderNo = true;
            exp.Execute();

            return View();
        }

        /// <summary>
        /// Set action 
        /// </summary>
        /// <param name="prID">string</param>
        /// <returns>string</returns>
        private string SetAction(string prID)
        {
            string action = string.Empty;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            PerformanceReview obj = perDao.GetById(prID);
            Employee emp = CommonFunc.GetEmployeeByUserName(principal.UserData.UserName);
            if (obj != null && emp != null)
            {
                if (obj.AssignID == emp.ID && obj.WFStatusID != Constants.STATUS_CLOSE && obj.AssignRole == Constants.PRW_ROLE_HR_ID)
                {
                    action = CommonFunc.Button("forward", "Do Action", "CRM.popup('/PerformanceReviewHR/ClosePR/" + prID + "', 'Do Action', 650)");
                }
            }

            return action;
        }

        /// <summary>
        /// Get list Sub grid
        /// </summary>
        /// <param name="id">id of performance review</param>
        /// <returns>ActionResult</returns>
        public ActionResult GetListHrJQSubGrid(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            List<sp_GetSubListPerformanceReviewHRResult> items = perDao.GetListPRByEmployeeId(id);

            var jsonData = new
            {
                rows = (
                    from m in items
                    select new
                    {
                        i = m.ID,
                        cell = new string[] {
                            CommonFunc.Link( m.ID, "/PerformanceReviewHr/Detail/" + m.ID, m.ID, false),
                            m.ManagerName,
                            m.PRDate.ToString(Constants.DATETIME_FORMAT),
                            m.StatusName,
                            m.ResolutionName,
                            m.AssignName + "(" + roleDao.GetByID(m.AssignRole).Name + ")",
                            SetAction(m.ID)
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Close performance review
        /// </summary>
        /// <param name="id">id of performance review</param>
        /// <returns>ActionResult</returns>
        public ActionResult ClosePR(string id)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                Employee emp = CommonFunc.GetEmployeeByUserName(principal.UserData.UserName);
                #region Check permission

                bool check = CommonFunc.CheckMovingRequest(int.Parse(emp.ID), Constants.FlowType.FLOW_PERFORMANCE_REVIEW,
                        id, Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return View("../Common/NotPermission");
                #endregion
                PerformanceReview viewData = perDao.GetById(id);

                #region Bind data for dropdownlist
                
                List<SelectListItem> list = new SelectList(resDao.GetListResolutionChangeByRole(Constants.PRW_ROLE_HR_ID), "ID", "Name", Constants.PRW_RESOLUTION_TO_BE_APPROVED).ToList();
                ViewData[CommonDataKey.PER_REVIEW_RESOLUTION] = list;
                ViewData[CommonDataKey.PER_REVIEW_STATUS] = new SelectList(statusDao.GetListStatusByResolution(viewData.WFResolutionID), "ID", "Name", viewData.WFStatusID);
                SetListAssignByRole(emp.ID);
                ViewData[CommonDataKey.PER_REVIEW_FIRST_CHOISED_STATUS] = string.Empty;
                ViewData[CommonDataKey.PER_REVIEW_FIRST_CHOISED_ASSIGN] = string.Empty;
                #endregion

                return View(viewData);
            }
            catch (Exception )
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Close performance review [post]
        /// </summary>
        /// <param name="form">FormCollection</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ClosePR(FormCollection form)
        {
            Message msg = null;
            bool isSendMail = false;
            try
            {
                int resolutionId = int.Parse(form.Get(CommonDataKey.PER_REVIEW_RESOLUTION));
                int statusId = int.Parse(form.Get(CommonDataKey.PER_REVIEW_STATUS));
                string fowardto = form.Get(CommonDataKey.PER_REVIEW_ASSIGN);
                string ID = form["ID"];
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                Employee emp = CommonFunc.GetEmployeeByUserName(principal.UserData.UserName);
                #region Check permission

                bool check = CommonFunc.CheckMovingRequest(int.Parse(emp.ID), Constants.FlowType.FLOW_PERFORMANCE_REVIEW,
                        ID, Constants.ActionType.Update, principal.UserData.Role);
                if (!check)
                    return View("../Common/NotPermission");
                #endregion

                PerformanceReview obj = perDao.GetById(ID);

                if (obj.UpdateDate.ToString() == form["UpdateDate"])
                {
                    string userAdminRole = fowardto;
                    string[] array = userAdminRole.Split(Constants.SEPARATE_USER_ADMIN_ID);
                    string contents = form["Contents"];
                    #region Performance Review Comment
                    if (!string.IsNullOrEmpty(contents))
                    {
                        PRComment objCm = new PRComment();
                        objCm.PRID = ID;
                        objCm.PostTime = DateTime.Now;
                        objCm.Poster = principal.UserData.UserName;
                        objCm.Contents = contents;
                        prCmDao.Insert(objCm);
                    }
                    #endregion

                    if (resolutionId == Constants.PRW_RESOLUTION_CANCEL)//case cancel PR
                    {
                        obj.InvolveResolution += Constants.PR_ACTION_CLOSE + Constants.SEPARATE_INVOLVE_SIGN;
                        obj.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                        obj.WFResolutionID = resolutionId;
                        obj.WFStatusID = statusId;
                        isSendMail = true;
                    }
                    else if (resolutionId == Constants.PRW_RESOLUTION_COMPLETE_ID)// case completed PR
                    {
                        obj.InvolveResolution += Constants.PR_ACTION_CLOSE + Constants.SEPARATE_INVOLVE_SIGN;
                        obj.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                        obj.WFResolutionID = resolutionId;
                        obj.WFStatusID = statusId;
                        isSendMail = true;
                    }
                    else
                    {

                        obj.InvolveResolution += Constants.JR_ACTION_ASSIGNTO + Constants.SEPARATE_INVOLVE_SIGN;
                        obj.InvolveID += array[0].Trim() + Constants.SEPARATE_INVOLVE_SIGN;
                        obj.InvolveRole += array[1] + Constants.SEPARATE_INVOLVE_SIGN;
                        obj.InvolveDate += DateTime.Now.ToString(Constants.DATETIME_FORMAT_JR) + Constants.SEPARATE_INVOLVE_SIGN;
                        obj.AssignID = array[0];
                        obj.AssignRole = int.Parse(array[1]);
                        isSendMail = true;
                    }
                    obj.UpdatedBy = principal.UserData.UserName;
                    msg = perDao.UpdatePerformanceReviewForHr(obj);

                    if (msg.MsgType == MessageType.Info && isSendMail )
                    {
                        SendPRMail(ID);
                    }
                }
                else
                {
                    msg = new Message(MessageConstants.E0025, MessageType.Error, "Performance Review '" + ID + "'");
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// View eform
        /// </summary>
        /// <param name="id">eform id</param>
        private void ViewEform(string id)
        {
            EformDao eDao = new EformDao();
            Int32 int_id = 0;
            try
            {
                int_id = int.Parse(id);
            }
            catch (FormatException)
            {
                //return RedirectToAction("Index", "Home");
            }
            ViewData["detail_list"] = eDao.GetEformDetailByEFormID(int.Parse(id));
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            ViewData["user"] = principal.UserData.UserName;
        }

        /// <summary>
        /// Show the detail of PR
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <summary>
        /// Detail
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>ActionResult</returns>
        public ActionResult Detail(string id)
        {

            PerformanceReview pr = perDao.GetById(id);
            if (pr == null)
                return RedirectToAction("Index");
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            if (CanViewDetail(pr.ID, principal.UserData.UserName))
                ViewData[CommonDataKey.IS_ACCESSIBLE] = true;
            else
                ViewData[CommonDataKey.IS_ACCESSIBLE] = false;
            ViewData["eform"] = pr.EForm.ID;
            ViewData["eformId"] = pr.EForm.MasterID;
            ViewData[CommonDataKey.PER_REVIEW_COMMENT] = perDao.GetListComment(pr.ID);
            ViewEform(pr.EFormID.ToString());
            return View(pr);
        }
        [NonAction]
        public bool CanViewDetail(string prId, string loginName)
        {
            if (string.IsNullOrEmpty(prId) || string.IsNullOrEmpty(loginName))
                return false;
            PerformanceReview pr = perDao.GetById(prId);
            var hrUserNameList = userAdminDao.GetListByRole(Constants.PRW_ROLE_HR_ID).
                Select(p => p.UserName).ToList();
            var involeIds = pr.InvolveID.Split(new string[] { Constants.SEPARATE_INVOLVE_SIGN },
                StringSplitOptions.RemoveEmptyEntries);
            if (pr != null && (   
                    //login user is HR
                    (hrUserNameList.Count != 0 && hrUserNameList.Contains(loginName)) ||
                    //login user is in invole list
                    involeIds.Contains(CommonFunc.GetEmployeeIDByUserName(loginName))
                    ))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Set list assign by role HR
        /// </summary>
        /// <param name="userId">string</param>
        private void SetListAssignByRole(string userId)
        {
            List<sp_GetListAssignByRoleResult> array = new List<sp_GetListAssignByRoleResult>();
            Employee tempEmp = null;
            
            foreach (UserAdmin userAdmin in userAdminDao.GetListByRole(Constants.PRW_ROLE_HR_ID))
            {
                tempEmp = empDao.GetByOfficeEmailInActiveList(userAdmin.UserName + Constants.LOGIGEAR_EMAIL_DOMAIN);
                
                string value = string.Format("{0}@{1}", tempEmp.ID, Constants.PRW_ROLE_HR_ID);

                sp_GetListAssignByRoleResult item = new sp_GetListAssignByRoleResult();
                item.UserAdminRole = value;
                item.DisplayName = empDao.FullName(tempEmp.ID, Constants.FullNameFormat.FirstMiddleLast);                
                array.Add(item);
            }

            ViewData[CommonDataKey.PER_REVIEW_ASSIGN] = new SelectList(array, "UserAdminRole", "DisplayName", userId + Constants.SEPARATE_USER_ADMIN_ID_STRING + Constants.PRW_ROLE_HR_ID);
            
        }

        #region Send Purchase E-mail
        /// <summary>
        /// Send Performance Review Mail
        /// </summary>
        /// <param name="prId">performance review id</param>
        protected void SendPRMail(string prId)
        {
            // Get body detail            
            PerformanceReview purReq = perDao.GetById(prId);
            WFRole role = null;
            if (purReq == null)
            {
                return;
            }
            else
            {
                role = roleDao.GetByID(purReq.AssignRole);
            }

            string from_email = ConfigurationManager.AppSettings["from_email"];
            string host = ConfigurationManager.AppSettings["mailserver_host"];
            string port = ConfigurationManager.AppSettings["mailserver_port"];
            string poster = "Performance review";
            string subject = string.Empty;
            if (purReq.WFStatusID == Constants.STATUS_CLOSE)
            {
                subject = "[CRM-PR] "  + purReq.ID + " has been closed";
            }
            else
            {
                if (role != null)
                    subject = "[CRM-PR] " + purReq.ID + " has been forwarded to " + empDao.FullName(purReq.AssignID, Constants.FullNameFormat.FirstMiddleLast) + " (" + role.Name + ")";
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
                    Employee user = empDao.GetById(arrIds[i]);
                    string userName = empDao.FullName(arrIds[i], Constants.FullNameFormat.FirstMiddleLast);
                    if (user != null)
                    {
                        if (purReq.WFStatusID == Constants.STATUS_CLOSE) //If an email has "Close" status, just only send by "To" section, not cc 
                        {
                            if (!duplicateEmail.Contains(user.OfficeEmail + ";"))
                            {
                                duplicateEmail.Add(user.OfficeEmail + ";");
                                to_email += user.OfficeEmail + ";";
                            }
                        }
                        else
                        {
                            //Just send by "To" section only to person who has been assigned, the involved others are by "CC" section.
                            if (string.IsNullOrEmpty(to_email))
                            {
                                Employee currentAssign = empDao.GetById(purReq.AssignID);
                                string assignName = empDao.FullName(purReq.AssignID, Constants.FullNameFormat.FirstMiddleLast);
                                if (currentAssign != null)
                                {
                                    to_email = currentAssign.OfficeEmail;
                                    cc_email = user.OfficeEmail + ";";
                                    duplicateEmail.Add(user.OfficeEmail + ";");
                                    duplicateEmail.Add(currentAssign.OfficeEmail + ";");
                                }
                            }
                            else //make a cc list mail send.
                            {
                                if (!duplicateEmail.Contains(user.OfficeEmail + ";"))
                                {
                                    duplicateEmail.Add(user.OfficeEmail + ";");
                                    cc_email += user.OfficeEmail + ";";
                                }
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(purReq.CCEmail))
            {
                string[] array = purReq.CCEmail.Split(';');
                foreach (string item in array)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        UserAdmin user = userAdminDao.GetByUserName(item);
                        if (user != null)
                        {
                            string userAdminID = user.UserAdminId.ToString();
                            if (!duplicateEmail.Contains(item + Constants.LOGIGEAR_EMAIL_DOMAIN + ";"))
                            {
                                duplicateEmail.Add(item + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                                cc_email += item + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                            }
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
        /// <param name="purReq">PerformanceReview</param>
        /// <param name="role">WFRole</param>
        /// <returns>string</returns>
        private string CreateBodyOfEmail(PerformanceReview purReq, WFRole role)
        {
            string path = string.Empty;

            //load template emails by pr status            
            path = Server.MapPath("~/Areas/Portal/Views/PerformanceReview/TemplateMail.htm");
            string content = WebUtils.ReadFile(path);

            //replace the holders on template emails.
            if (purReq != null && role != null)
            {
                // create message
                content = content.Replace(Constants.PR_ASSIGN_TO_HOLDER, empDao.FullName(purReq.AssignID, Constants.FullNameFormat.FirstMiddleLast));
                content = content.Replace(Constants.PR_PERFORMANCE_ID_HOLDER, purReq.ID.ToString());
                content = content.Replace(Constants.PR_EMPLOYEE_HOLDER, purReq.Employee.FirstName + " " + purReq.Employee.MiddleName + " " + purReq.Employee.LastName);
                content = content.Replace(Constants.PR_DEPARMENT_HOLDER, depDao.GetDepartmentNameBySub(purReq.Employee.DepartmentId));
                content = content.Replace(Constants.PR_SUBDEPARMENT_HOLDER, purReq.Employee.Department.DepartmentName);
                content = content.Replace(Constants.PR_TITLE_HOLDER, purReq.Employee.JobTitleLevel.DisplayName);
                Employee manager = empDao.GetById(purReq.Employee.ManagerId);
                content = content.Replace(Constants.PR_MANAGER_HOLDER, manager.FirstName + " " + manager.MiddleName + " " + manager.LastName);
                content = content.Replace(Constants.PR_RESOLUTION_HOLDER, purReq.WFResolution.Name);
                content = content.Replace(Constants.PR_STATUS_HOLDER, new WFStatusDao().GetByID(purReq.WFStatusID).Name);

                string link = string.Empty;
                if (purReq.WFStatusID == Constants.STATUS_CLOSE)
                {
                    content = content.Replace(Constants.PR_FORWARD_TO_HOLDER, " has been closed");
                    content = content.Replace(Constants.PR_FORWARD_TO_NAME_HOLDER, "");
                    link = "http://" + Request["SERVER_NAME"] + ":" + Request["SERVER_PORT"] + "/Portal/PerformanceReview" + "/Detail/" + purReq.ID;
                }
                else
                {
                    content = content.Replace(Constants.PR_FORWARD_TO_HOLDER, " has been forwarded to you");
                    content = content.Replace(Constants.PR_FORWARD_TO_NAME_HOLDER, "Forwarded To: " + empDao.FullName(purReq.AssignID, Constants.FullNameFormat.FirstMiddleLast) + " (" + purReq.WFRole.Name + " )");
                    link = "http://" + Request["SERVER_NAME"] + ":" + Request["SERVER_PORT"] + "/Portal/PerformanceReview" + "/?role=" + purReq.AssignRole;
                }

                content = content.Replace(Constants.PR_LINK_HOLDER, link);
            }

            return content;
        }

        #endregion
        #endregion
    }
}
