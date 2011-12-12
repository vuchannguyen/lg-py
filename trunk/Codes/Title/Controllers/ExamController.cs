using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Attributes;
using CRM.Controllers;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Configuration;
using ClosedXML.Excel;

namespace CRM.Controllers
{
    /// <summary>
    /// Exam Controller
    /// </summary>
    public class ExamController : BaseController
    {
        #region Local Variable

        private JobTitleLevelDao levelDao = new JobTitleLevelDao();
        private ExamDao examDao = new ExamDao();
        private CandidateExamDao canExamDao = new CandidateExamDao();
        private ExamQuestionDAO examQuestionDao = new ExamQuestionDAO();
        private CandidateDao candidateDao = new CandidateDao();
        private EmployeeDao employeeDao = new EmployeeDao();
        private ExamQuestionSectionDAO examQuestionSectionDAO = new ExamQuestionSectionDAO();
        private CandidateAnswerDao candidateAnswerDAO = new CandidateAnswerDao();
        private DepartmentDao deptDao = new DepartmentDao();

        #endregion

        #region Nguyen Duy Hung

        /// <summary>
        /// Exam List page
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.EXAM_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.EXAM_DEFAULT_VALUE];

            ViewData[Constants.EXAM_TEXT] = hashData[Constants.EXAM_TEXT] == null ? Constants.EXAM_TITLE : !string.IsNullOrEmpty((string)hashData[Constants.EXAM_TEXT]) ? hashData[Constants.EXAM_TEXT] : Constants.EXAM_TITLE;
            ViewData[Constants.EXAM_QUESTION] = new SelectList(examQuestionDao.GetList(), "ID", "Title", hashData[Constants.EXAM_QUESTION] == null ? Constants.FIRST_ITEM_EXAM_QUESTION : hashData[Constants.EXAM_QUESTION]);
            ViewData[Constants.EXAM_DATE_FROM] = hashData[Constants.EXAM_DATE_FROM] == null ? string.Empty : (string)hashData[Constants.EXAM_DATE_FROM];
            ViewData[Constants.EXAM_DATE_TO] = hashData[Constants.EXAM_DATE_TO] == null ? string.Empty : (string)hashData[Constants.EXAM_DATE_TO];

            ViewData[Constants.EXAM_COLUMN] = hashData[Constants.EXAM_COLUMN] == null ? "ExamDate" : hashData[Constants.EXAM_COLUMN];
            ViewData[Constants.EXAM_ORDER] = hashData[Constants.EXAM_ORDER] == null ? "desc" : hashData[Constants.EXAM_ORDER];
            ViewData[Constants.EXAM_PAGE_INDEX] = hashData[Constants.EXAM_PAGE_INDEX] == null ? "1" : hashData[Constants.EXAM_PAGE_INDEX].ToString();
            ViewData[Constants.EXAM_ROW_COUNT] = hashData[Constants.EXAM_ROW_COUNT] == null ? "20" : hashData[Constants.EXAM_ROW_COUNT].ToString();

            return View();
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.EXAM_DEFAULT_VALUE);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get Exam List and bind to JQGrid
        /// </summary>
        /// <param name="text"></param>
        /// <param name="examQuestionId"></param>
        /// <param name="examDateFrom"></param>
        /// <param name="examDateTo"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Read)]
        [ValidateInput(false)]
        public ActionResult GetExamListJQGrid(string text, string examQuestionId, string examDateFrom, string examDateTo)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(text, examQuestionId, examDateFrom, examDateTo, sortColumn, sortOrder, pageIndex, rowCount);

            string exTitle = null;
            int exQuestionId = 0;
            DateTime? exDateFrom = null;
            DateTime? exDateTo = null;

            //set value for search params
            if (text != Constants.EXAM_TITLE)
                exTitle = text.Trim();
            if (!string.IsNullOrEmpty(examQuestionId))
                exQuestionId = int.Parse(examQuestionId);
            if (!string.IsNullOrEmpty(examDateFrom))
                exDateFrom = DateTime.Parse(examDateFrom);
            if (!string.IsNullOrEmpty(examDateTo))
                exDateTo = DateTime.Parse(examDateTo);

            //get list
            List<sp_GetExamResult> examList = examDao.GetList(exTitle, exQuestionId, exDateFrom, exDateTo);

            //for paging
            int totalRecords = examList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            var finalList = examDao.Sort(examList, sortColumn, sortOrder)
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
                            HttpUtility.HtmlEncode(m.Title),
                            HttpUtility.HtmlEncode(m.ExamQuestionTitle),   
                            (m.ExamType == Constants.LOT_CANDIDATE_EXAM_ID ? Constants.LOT_CANDIDATE_EXAM_NAME : Constants.LOT_EMPLOYEE_EXAM_NAME),
                            m.ExamDate.ToString(Constants.DATETIME_FORMAT_VIEW),                            
                            m.MarkStatus,
                            m.ProgramingMarkStatus,
                            (examDao.IsTested(m.ID) ? string.Empty : "<input type=\"button\" class=\"icon edit\" title=\"Edit\" onclick=\"CRM.popup('/Exam/Edit/" + m.ID.ToString() + "', 'Update', 400)\" />&nbsp;") 
                            + ((m.ExamType == Constants.LOT_CANDIDATE_EXAM_ID && CommonFunc.CheckAuthorized(principal.UserData.UserID, (byte)Modules.Exam, (int)Permissions.AssignCandidate)) ? "<input type=\"button\" class=\"icon add-user\" title=\"Assign Candidate\" onclick=\"CRM.redirect('/Exam/AssignList/" + m.ID.ToString() + "')\" />&nbsp;": string.Empty) 
                            + ((m.ExamType == Constants.LOT_EMPLOYEE_EXAM_ID && CommonFunc.CheckAuthorized(principal.UserData.UserID, (byte)Modules.Exam, (int)Permissions.AssignEmployee)) ? "<input type=\"button\" class=\"icon add-user\" title=\"Assign Candidate\" onclick=\"CRM.redirect('/Exam/AssignEmployeeList/" + m.ID.ToString() + "')\" />&nbsp;": string.Empty) 
                            + (CommonFunc.CheckAuthorized(principal.UserData.UserID, (byte)Modules.Exam, (int)Permissions.Mark) ? "<input type=\"button\" class=\"icon result\" title=\"View Test Result\" onclick=\"CRM.redirect('/Exam/CandidateTestList/" + m.ID.ToString() + "')\" />" : string.Empty)                           
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Exam/Create
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create()
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            ViewData[CommonDataKey.EXAM_QUESTION_ID] = new SelectList(examQuestionDao.GetList(), "ID", "Title");
            ViewData[CommonDataKey.EXAM_TYPE] = new SelectList(examDao.GetExamTypeList(principal.UserData.UserID), "Value", "Text");
            return View();
        }

        //
        // POST: /Exam/Create        
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Insert)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(LOT_Exam exam)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            exam.CreatedBy = principal.UserData.UserName;
            exam.UpdatedBy = principal.UserData.UserName;

            // insert to db
            Message msg = examDao.Insert(exam);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        //
        // GET: /Exam/Edit/id
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Edit(int id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            LOT_Exam exam = examDao.GetByID(id);
            ViewData[CommonDataKey.EXAM_QUESTION_ID] = new SelectList(examQuestionDao.GetList(), "ID", "Title", exam.ExamQuestionID);
            ViewData[CommonDataKey.EXAM_TYPE] = new SelectList(examDao.GetExamTypeList(principal.UserData.UserID), "Value", "Text", exam.ExamType);
            return View(exam);
        }

        //
        // POST: /Exam/Edit/LOT_Exam object
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Update)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(LOT_Exam exam)
        {
            try
            {
                // TODO: Add update logic here
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                exam.UpdatedBy = principal.UserData.UserName;

                // update to db
                Message msg = examDao.Update(exam);
                ShowMessage(msg);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Delete List
        /// </summary>
        /// <param name="id">ids</param>
        /// <returns></returns>        
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = examDao.DeleteList(id, principal.UserData.UserName);
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        /// <summary>
        /// View Assigned Candidate List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.AssignCandidate, ShowAtCurrentPage = true)]
        public ActionResult AssignList(int id)
        {
            LOT_Exam exam = examDao.GetByID(id);
            ViewData[CommonDataKey.EXAM_ID] = id;
            ViewData[CommonDataKey.EXAM_TITLE] = exam.Title;
            return View();
        }

        /// <summary>
        /// View Candidate list to assign
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.AssignCandidate, ShowInPopup = true)]
        public ActionResult Assign(int id)
        {
            ViewData[Constants.CANDIDATE_LIST_JOB_TITLE] = new SelectList(levelDao.GetList(), "ID", "DisplayName");
            ViewData[Constants.CANDIDATE_LIST_SOURCE] = new SelectList(candidateDao.GetListSource(), "Value", "Text");
            ViewData[Constants.CANDIDATE_LIST_STATUS] = new SelectList(Constants.GetCandidateStatus, "Value", "Text");
            ViewData[CommonDataKey.EXAM_ID] = id.ToString();
            return View();
        }

        /// <summary>
        /// Assign Candidate to exam
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.AssignCandidate)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Assign(FormCollection collection)
        {
            string examID = collection[CommonDataKey.EXAM_ID];
            string selectedIDs = collection["AssignIDs"];
            if (!string.IsNullOrEmpty(selectedIDs) && !string.IsNullOrEmpty(examID))
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                //remove the last char which is ',' to split
                selectedIDs = selectedIDs.TrimEnd(Constants.SEPARATE_IDS_CHAR);
                //pass to array
                string[] idArr = selectedIDs.Split(Constants.SEPARATE_IDS_CHAR);

                List<LOT_Candidate_Exam> list = new List<LOT_Candidate_Exam>();
                int uniqueId = canExamDao.getlastID();
                foreach (string strId in idArr)
                {
                    int id = 0;
                    bool isInterger = Int32.TryParse(strId, out id);
                    if (isInterger)
                    {
                        uniqueId = uniqueId + 1;
                        //add to list
                        LOT_Candidate_Exam item = new LOT_Candidate_Exam();
                        item.ExamID = int.Parse(examID);
                        item.CandidateID = id;
                        item.CandidatePin = uniqueId + CommonFunc.encryptDataMd5(uniqueId + DateTime.Now.ToString(), 2);
                        item.CreatedBy = principal.UserData.UserName;
                        item.CreateDate = DateTime.Now;
                        item.UpdatedBy = principal.UserData.UserName;
                        item.UpdateDate = DateTime.Now;
                        list.Add(item);
                    }
                }

                //perform to assign
                Message msg = canExamDao.AssignCandidate(list);
                ShowMessage(msg);
            }
            return RedirectToAction("AssignList/" + collection[CommonDataKey.EXAM_ID]);
        }

        /// <summary>
        /// View employee list to assign
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.AssignEmployee, ShowInPopup = true)]
        public ActionResult AssignEmployee(int id)
        {
            ViewData[Constants.EMPLOYEE_LIST_NAME] = Constants.FULLNAME_OR_USERID;
            ViewData[Constants.EMPLOYEE_LIST_DEPARTMENT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", Constants.FIRST_ITEM_DEPARTMENT);
            ViewData[Constants.EMPLOYEE_LIST_SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", Constants.FIRST_ITEM_SUB_DEPARTMENT);
            ViewData[Constants.EMPLOYEE_LIST_JOB_TITLE] = new SelectList(levelDao.GetList(), "ID", "DisplayName", Constants.FIRST_ITEM_JOBTITLE);
            ViewData[CommonDataKey.EXAM_ID] = id.ToString();
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="name"></param>
        /// <param name="department"></param>
        /// <param name="subDepartment"></param>
        /// <param name="titleId"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.AssignEmployee, ShowInPopup = true)]
        public ActionResult GetEmployeeListJQGrid(string examId, string name, string department, string subDepartment, string titleId)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            #region search
            string userName = string.Empty;
            int subDepartmentId = 0;
            int departmentId = 0;
            int title = 0;
            if (name != Constants.FULLNAME_OR_USERID)
            {
                userName = name;
            }
            if (!string.IsNullOrEmpty(subDepartment))
            {
                subDepartmentId = int.Parse(subDepartment);
            }
            if (!string.IsNullOrEmpty(titleId))
            {
                title = int.Parse(titleId);
            }
            if (!string.IsNullOrEmpty(department))
            {
                departmentId = int.Parse(department);
            }
            #endregion

            //Get List and paging
            List<sp_GetEmployeeResult> empList = examDao.GetEmployeeByExamID(int.Parse(examId), userName, departmentId, subDepartmentId, title, Constants.EMPLOYEE_ACTIVE, Constants.RESIGNED);

            int totalRecords = empList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

            List<sp_GetEmployeeResult> finalList = employeeDao.Sort(empList, sortColumn, sortOrder)
                                  .Skip((pageIndex - 1) * rowCount)
                                   .Take(rowCount).ToList<sp_GetEmployeeResult>();
            //bind to grid
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
                            CommonFunc.Link(m.ID,"/Employee/Detail/"  + m.ID+ "",m.DisplayName,true),
                            m.TitleName,
                            m.Department,
                            m.DepartmentName,
                            m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            m.StatusName                            
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Assign employee to exam
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.AssignEmployee)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AssignEmployee(FormCollection collection)
        {
            string examID = collection[CommonDataKey.EXAM_ID];
            bool isFinished = false;
            var exam = examDao.GetByID(ConvertUtil.ConvertToInt(examID));
            if (examQuestionDao.HasOnlyVerbalSection(exam.ExamQuestionID))
                isFinished = true;
            string selectedIDs = collection["AssignIDs"];
            if (!string.IsNullOrEmpty(selectedIDs) && !string.IsNullOrEmpty(examID))
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                //remove the last char which is ',' to split
                selectedIDs = selectedIDs.TrimEnd(Constants.SEPARATE_IDS_CHAR);
                //pass to array
                string[] idArr = selectedIDs.Split(Constants.SEPARATE_IDS_CHAR);

                List<LOT_Candidate_Exam> list = new List<LOT_Candidate_Exam>();
                int uniqueId = canExamDao.getlastID();
                foreach (string strId in idArr)
                {
                    //check are all items seleted
                    if (strId != Constants.FALSE)
                    {
                        uniqueId = uniqueId + 1;
                        //add to list
                        LOT_Candidate_Exam item = new LOT_Candidate_Exam();
                        item.IsFinish = isFinished;
                        item.ExamID = int.Parse(examID);
                        item.EmployeeID = strId;
                        item.CandidatePin = uniqueId + CommonFunc.encryptDataMd5(uniqueId + DateTime.Now.ToString(), 2);
                        item.CreatedBy = principal.UserData.UserName;
                        item.CreateDate = DateTime.Now;
                        item.UpdatedBy = principal.UserData.UserName;
                        item.UpdateDate = DateTime.Now;
                        list.Add(item);
                    }
                }

                //perform to assign
                Message msg = canExamDao.AssignCandidate(list);
                ShowMessage(msg);
            }
            return RedirectToAction("AssignEmployeeList/" + collection[CommonDataKey.EXAM_ID]);
        }

        /// <summary>
        /// Get Candidate List and bind JQGrid to assign
        /// </summary>
        /// <param name="can_name"></param>
        /// <param name="source"></param>
        /// <param name="titleId"></param>
        /// <param name="status"></param>
        /// <param name="from_date"></param>
        /// <param name="to_date"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.AssignCandidate)]
        [ValidateInput(false)]
        public ActionResult GetCandidateListJQGrid(string examId, string can_name, string source, string titleId, string status, string from_date, string to_date)
        {
            if (!string.IsNullOrEmpty(examId))
            {
                #region JQGrid Params

                string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
                string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
                int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
                int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

                #endregion

                #region search
                string candidate_name = String.Empty;
                int source_search = 0;
                int title = 0;
                int statusId = 0;
                string from = String.Empty;
                string to = String.Empty;
                if (can_name != Constants.CANDIDATE_NAME)
                {
                    candidate_name = can_name;
                }
                if (!string.IsNullOrEmpty(source))
                {
                    source_search = int.Parse(source);
                }
                if (!string.IsNullOrEmpty(titleId))
                {
                    title = int.Parse(titleId);
                }
                if (!string.IsNullOrEmpty(from_date))
                {
                    from = from_date;
                }
                if (!string.IsNullOrEmpty(to_date))
                {
                    to = to_date;
                }
                if (!string.IsNullOrEmpty(status))
                {
                    statusId = int.Parse(status);
                }

                #endregion

                #region GetList

                List<sp_GetCandidateResult> CandidateList = examDao.GetCandidateListByExamID(int.Parse(examId), candidate_name, source_search, title, statusId, from, to);

                //paging
                int totalRecords = CandidateList.Count();
                int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

                var finalList = candidateDao.Sort(CandidateList, sortColumn, sortOrder)
                                      .Skip((pageIndex - 1) * rowCount)
                                       .Take(rowCount);
                #endregion

                //bind to jqGrid
                int j = 0;
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
                                Convert.ToString(j+=1),
                                m.ID.ToString(),
                                m.DisplayName, 
                                (m.DOB.HasValue)?m.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",
                                m.CellPhone,
                                m.Gender == Constants.MALE?"Male":"Female",
                                m.SearchDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                                m.SourceName,
                                m.Title.ToString(),
                                CommonFunc.GetCandidateStatus(m.Status),
                                m.Note                                
                            }
                        }
                    ).ToArray()
                };

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get Assigned Candidate List and bind to JQGrid
        /// </summary>
        /// <param name="examId"></param> 
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.AssignCandidate)]
        public ActionResult GetAssignListJQGrid(string examId)
        {
            if (!string.IsNullOrEmpty(examId))
            {
                #region JQGrid Params

                string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
                string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
                int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
                int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

                #endregion

                #region search
                #endregion

                #region GetList

                List<sp_GetCandidateExamResult> CandidateList = examDao.GetCandidateExam(int.Parse(examId));

                //paging
                int totalRecords = CandidateList.Count();
                int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

                var finalList = canExamDao.Sort(CandidateList, sortColumn, sortOrder)
                                      .Skip((pageIndex - 1) * rowCount)
                                       .Take(rowCount);
                #endregion

                //bind to grid
                int j = 0;
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
                                Convert.ToString(j+=1),
                                m.ID.ToString(), 
                                CommonFunc.Link(m.ID.ToString(),"/Candidate/Detail/?id="+m.CandidateID + "&urlback=/Exam/AssignList/" + examId,m.DisplayName,true),
                                (m.DOB.HasValue)?m.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",
                                m.CellPhone,
                                m.Gender == Constants.MALE?"Male":"Female",
                                m.SearchDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                                m.SourceName,
                                m.Title.ToString(),
                                CommonFunc.GetCandidateStatus(m.Status),
                                m.Note,                                
                                m.CandidatePin
                            }
                        }
                    ).ToArray()
                };

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
                return null;
        }

        /// <summary>
        /// View Assigned Employee List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.AssignEmployee, ShowAtCurrentPage = true)]
        public ActionResult AssignEmployeeList(int id)
        {
            LOT_Exam exam = examDao.GetByID(id);
            ViewData[CommonDataKey.EXAM_ID] = id;
            ViewData[CommonDataKey.EXAM_TITLE] = exam.Title;
            return View();
        }

        /// <summary>
        /// Get Assigned Employee List and bind to JQGrid
        /// </summary>
        /// <param name="examId"></param> 
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.AssignEmployee)]
        public ActionResult GetAssignEmployeeListJQGrid(string examId)
        {
            if (!string.IsNullOrEmpty(examId))
            {
                #region JQGrid Params

                string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
                string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
                int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
                int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

                #endregion

                #region search
                #endregion

                #region GetList

                List<sp_GetEmployeeExamResult> CandidateList = examDao.GetEmployeeExam(int.Parse(examId));

                //paging
                int totalRecords = CandidateList.Count();
                int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

                var finalList = canExamDao.SortEmployee(CandidateList, sortColumn, sortOrder)
                                      .Skip((pageIndex - 1) * rowCount)
                                       .Take(rowCount);
                #endregion

                //bind to grid
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
                            CommonFunc.Link(m.EmployeeID,"/Employee/Detail?id="  + m.EmployeeID  + "&urlback=/Exam/AssignEmployeeList/" + examId,m.DisplayName,true),
                            m.TitleName,
                            m.Department,
                            m.DepartmentName,
                            m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            m.StatusName,               
                            m.CandidatePin
                        }
                        }
                    ).ToArray()
                };

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
                return null;
        }

        /// <summary>
        /// Remove Seleted Candidate
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="examID"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.AssignCandidate)]
        public ActionResult RemoveAssignList(string ids, string examID)
        {
            Message msg = canExamDao.RemoveAssignList(ids);
            ShowMessage(msg);
            return RedirectToAction("AssignList/" + examID);
        }

        /// <summary>
        /// Remove Seleted Employee
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="examID"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.AssignEmployee)]
        public ActionResult RemoveSelectedEmployee(string ids, string examID)
        {
            Message msg = canExamDao.RemoveAssignList(ids);
            ShowMessage(msg);
            return RedirectToAction("AssignEmployeeList/" + examID);
        }

        /// <summary>
        /// View detail the Test of Candidate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CandidateTestDetail(int id)
        {
            LOT_Candidate_Exam candidateExam = canExamDao.GetByID(id);
            if (candidateExam != null)
            {
                LOT_ExamQuestion examQuestion = examQuestionDao.GetByID(candidateExam.LOT_Exam.ExamQuestionID);
                if (candidateExam.LOT_Exam.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
                {
                    ViewData[CommonDataKey.CANDIDATE_NAME] = candidateExam.Candidate.FirstName + " " + candidateExam.Candidate.MiddleName + " " + candidateExam.Candidate.LastName;
                }
                else
                {
                    ViewData[CommonDataKey.CANDIDATE_NAME] = candidateExam.Employee.FirstName + " " + candidateExam.Employee.MiddleName + " " + candidateExam.Employee.LastName;
                }
                ViewData[CommonDataKey.EXAM_ID] = candidateExam.ExamID;
                ViewData[CommonDataKey.EXAM_TITLE] = HttpUtility.HtmlEncode(candidateExam.LOT_Exam.Title);
                ViewData[CommonDataKey.CANDIDATE_EXAM] = candidateExam;
                return View(examQuestion);
            }
            return View();
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
        private void SetSessionFilter(string title, string examQuestion, string dateFrom, string dateTo,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.EXAM_NAME, title);
            hashData.Add(Constants.EXAM_QUESTION, examQuestion);
            hashData.Add(Constants.EXAM_DATE_FROM, dateFrom);
            hashData.Add(Constants.EXAM_DATE_TO, dateTo);
            hashData.Add(Constants.EXAM_COLUMN, column);
            hashData.Add(Constants.EXAM_ORDER, order);
            hashData.Add(Constants.EXAM_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.EXAM_ROW_COUNT, rowCount);

            Session[SessionKey.EXAM_DEFAULT_VALUE] = hashData;
        }

        #endregion

        #region Khai Tran

        /// <summary>
        /// View for candidate test list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Mark)]
        public ActionResult CandidateTestList(string id)
        {
            ViewData[Constants.EXAM_ID] = id;
            LOT_Exam exam = examDao.GetByID(int.Parse(id));
            if (exam != null)
            {
                ViewData[Constants.EXAM_NAME] = HttpUtility.HtmlEncode(exam.Title);
                ViewData[Constants.EXAM_DATE] = exam.ExamDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                ViewData[Constants.EXAM_LIST_QUESTION] = examQuestionSectionDAO.GetListByExamQuestionID(exam.ExamQuestionID);
            }

            return View(exam);
        }

        #region New Code For Candiate Test List
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Mark)]
        public ActionResult GetCandidateTestListJQGrid(string examId)
        {
            #region JQGrid Params

            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

            #endregion
            SetSessionFilterCandidateList(sortColumn, sortOrder);
            #region GetList
            List<sp_GetCandidateListByExamResult> candidateList = examDao.GetCandidateListByExam(int.Parse(examId), sortColumn, sortOrder);
            #endregion
            LOT_Exam exam = examDao.GetByID(int.Parse(examId));
            int totalRecords = candidateList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            var finalList = candidateList.Skip((pageIndex - 1) * rowCount).Take(rowCount).ToList();
            int index = 1;
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
                        cell = GetListSection(exam, index++, m)
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string SetAction(double totalMark, sp_GetCandidateListByExamResult candidate, LOT_Exam exam)
        {
            string result = string.Empty;
            string verbalAction = string.Empty;
            string sendMailAction = string.Empty;
            string exportExcelAction = string.Empty;
            if (!double.IsNaN(totalMark))
            {
                if (examDao.HasVerbalSection(exam.ID))
                {
                    verbalAction = CommonFunc.Button("icon edit-verbal", Constants.CTL_UPDATE_VERBAL_MARK,
                     "CRM.popup('/Exam/InputVerbalMark/" + candidate.ID + "', '" + Constants.CTL_UPDATE_VERBAL_MARK + "', 500)");
                }
                if (!string.IsNullOrEmpty(candidate.Email))
                {
                    sendMailAction = CommonFunc.Button("lot_send_email", Constants.CTL_EMAIL_TITLE_DIALOG
                                   , "CRM.popup('/Exam/SendEmailToCandidate/" + candidate.ID
                                   + "', '" + Constants.CTL_EMAIL_TITLE_DIALOG + "', " + Constants.CTL_EMAIL_DIALOG_WIDTH + ")");
                }
                exportExcelAction = CommonFunc.Button("lot_excel", Constants.CTL_EXCEL_BUTTON_TITLE
                                        , "window.location = '/Exam/ExportResultToExcel?candidateExamID=" + candidate.ID + "'");
                result = CommonFunc.Button("icon edit", Constants.CTL_UPDATE_WRITING_MARK, "CRM.popup('/Exam/InputMark/" + candidate.ID + "', 'Input Mark', 650)")
                    //+ ((examDao.CheckProgrammingSkillSection((int)m[10]) && (double)m[11] != 0) ? CommonFunc.Button("icon edit-program",Constants.CTL_UPDATE_PROGRAMING_SKILL_REMARK,"CRM.popup('/Exam/InputProgramingMark/"+ m[4].ToString() +"', 'Update Programming Mark', 650)") : string.Empty)
                  + verbalAction + sendMailAction + exportExcelAction;
            }
            return result;
        }

        private string[] GetListSection(LOT_Exam exam, int index, sp_GetCandidateListByExamResult candidate)
        {
            bool isNan = false;
            double totalMark = 0;
            double totalMaxMark = 0;
            double totalWritingMaxMark = candidateAnswerDAO.GetMaxWritingMark(candidate.ID, Constants.LOT_WRITING_SKILL_ID);
            double totalWritingMark = candidate.WritingMark != null ? candidate.WritingMark.Value : Constants.WRITTING_MARK_NULL;
            List<LOT_ExamQuestion_Section> listSection = examQuestionSectionDAO.GetListByExamQuestionID(exam.ExamQuestionID);
            List<string> str = new List<string>();
            str.Add(index.ToString());
            if (exam.ExamType != Constants.LOT_CANDIDATE_EXAM_ID)
            {
                str.Add(candidate.CandidateID);
            }
            str.Add(candidate.DisplayName);
            str.Add(candidate.Email);
            //Check Mark of Writing
            string writingMark = string.Empty;
            if (totalWritingMark != Constants.WRITTING_MARK_NULL)
            {
                writingMark = Math.Round(totalWritingMark) + "/" + totalWritingMaxMark;
            }
            str.Add(writingMark);
            foreach (LOT_ExamQuestion_Section section in listSection)
            {
                str.Add(SetResultByVerbalMarkType(section,exam,candidate,ref totalMark,ref totalMaxMark, ref isNan));
            }
            //Check Mark of Writing
            string strTotalMark = string.Empty;
            if (!isNan)
            {
                strTotalMark = Math.Round(totalMark) + "/" + (totalMaxMark);
                if (totalWritingMark != Constants.WRITTING_MARK_NULL)
                {
                    strTotalMark = Math.Round(totalMark + totalWritingMark) + "/" + (totalMaxMark + totalWritingMaxMark);
                }
            }
            str.Add(candidate.SendMail ? Constants.LOT_SENT_MAIL : string.Empty);
            str.Add(strTotalMark);
            str.Add(SetAction(totalMark, candidate, exam));
            return str.ToArray();
        }

        private void SetSessionFilterCandidateList(string sortColumn, string sortOrder)
        {
            Hashtable hash = new Hashtable();
            hash[Constants.CANDIDATELIST_SORT_ORDER] = sortOrder;
            hash[Constants.CANDIDATELIST_SORT_NAME] = sortColumn;
            Session[SessionKey.CANDIDATELIST] = hash;
        }

        public string SetResultByVerbalMarkType(LOT_ExamQuestion_Section section,LOT_Exam exam,sp_GetCandidateListByExamResult candidate,
            ref double totalMark,ref double totalMaxMark, ref bool isNan)
        {
            string result = string.Empty;
            if (section.SectionID != Constants.LOT_VERBAL_SKILL_ID)
            {
                result=examDao.CalculateMarkSection(exam, section, candidate, ref totalMark, ref totalMaxMark, ref isNan);
            }
            else
            {
                LOT_Candidate_Exam objCandidateExam = new LOT_Candidate_Exam();
                if (exam.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
                {
                    objCandidateExam = canExamDao.GetByExamAndCandidate(exam.ID, ConvertUtil.ConvertToInt(candidate.CandidateID),string.Empty);
                }
                else
                {
                    objCandidateExam = canExamDao.GetByExamAndCandidate(exam.ID, null, candidate.CandidateID);
                }
                if (objCandidateExam != null)
                {
                    if (objCandidateExam.VerbalMarkType.HasValue)
                    {
                        if (objCandidateExam.VerbalMarkType.Value == Constants.LOT_VERBAL_MARK_TYPE_LEVEL)
                        {
                            result=Constants.LOT_VERBAL_MARK_STRING + " - "
                                + (objCandidateExam.VerbalMark.HasValue ? objCandidateExam.VerbalMark.Value.ToString() : string.Empty);
                        }
                        else
                        {
                            Training_MasterEnglishType objMasterEnglishType = new TrainingEmpEnglishInfoDao().GetMasterEnglishTypeByID(objCandidateExam.VerbalMarkType.Value);
                            if (objMasterEnglishType != null)
                            {
                                result=objMasterEnglishType.Name + " - "
                                    + (objCandidateExam.VerbalMark.HasValue ? objCandidateExam.VerbalMark.Value.ToString() : string.Empty);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public ActionResult ExportCandidateToExcel(string id)
        {
            LOT_Exam exam = examDao.GetByID(ConvertUtil.ConvertToInt(id));
            string sortName = "CandidateName";
            string sortOrder = "desc";
            if (Session[SessionKey.CANDIDATELIST] != null)
            {
                Hashtable hash = (Hashtable)Session[SessionKey.CANDIDATELIST];
                sortName = (string)hash[Constants.CANDIDATELIST_SORT_NAME];
                sortOrder = (string)hash[Constants.CANDIDATELIST_SORT_ORDER];
            }
            List<sp_GetCandidateListByExamResult> candidateList = examDao.GetCandidateListByExam(exam.ID, sortName, sortOrder);
            List<LOT_ExamQuestion_Section> listSection = examQuestionSectionDAO.GetListByExamQuestionID(exam.ExamQuestionID);
            string title = "Candidate's Test List";
            var wb = new XLWorkbook();
            var worksheet = wb.Worksheets.Add(title);
            int originalColumn = 5;
            int totalColumn = 5;
            if (exam.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
            {
                totalColumn = 4;
                originalColumn = 4;
            }

            if (listSection != null)
            {
                totalColumn += listSection.Count + 2;
            }
            IXLAddress firstAdd = worksheet.Cell(1, 1).Address;
            #region Title
            worksheet.Cell(1, 1).Value = title;
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 15;
            worksheet.Range(worksheet.Cell(1, 1).Address, worksheet.Cell(1, totalColumn).Address).Merge();
            worksheet.Range(worksheet.Cell(1, 1).Address, worksheet.Cell(1, totalColumn).Address).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            #endregion
            #region Exam Name
            worksheet.Cell(2, 1).Value = "Exam Name : " + exam.Title;
            worksheet.Cell(2, 1).Style.Font.Bold = true;
            worksheet.Cell(2, 1).Style.Font.FontSize = 12;
            worksheet.Range(worksheet.Cell(2, 1).Address, worksheet.Cell(2, 4).Address).Merge();
            worksheet.Range(worksheet.Cell(2, 1).Address, worksheet.Cell(2, 4).Address).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            #endregion
            #region Exam Date
            worksheet.Cell(3, 1).Value = "Exam Date : " + exam.ExamDate.ToString(Constants.DATETIME_FORMAT_VIEW);
            worksheet.Cell(3, 1).Style.Font.Bold = true;
            worksheet.Cell(3, 1).Style.Font.FontSize = 12;
            worksheet.Range(worksheet.Cell(3, 1).Address, worksheet.Cell(3, 4).Address).Merge();
            worksheet.Range(worksheet.Cell(3, 1).Address, worksheet.Cell(3, 4).Address).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            #endregion
            #region Header
            if (exam.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
            {
                worksheet.Cell(4, 1).Value = "No";
                worksheet.Cell(4, 2).Value = "Candidate Name";
                worksheet.Cell(4, 3).Value = "Email";
                worksheet.Cell(4, 4).Value = "Writing Mark";
            }
            else
            {
                worksheet.Cell(4, 1).Value = "No";
                worksheet.Cell(4, 2).Value = "ID";
                worksheet.Cell(4, 3).Value = "Candidate Name";
                worksheet.Cell(4, 4).Value = "Email";
                worksheet.Cell(4, 5).Value = "Writing Mark";
            }
            foreach (LOT_ExamQuestion_Section item in listSection)
            {
                worksheet.Cell(4, originalColumn + 1).Value = item.LOT_Section.SectionName;
                originalColumn++;
            }
            worksheet.Cell(4, originalColumn + 1).Value = "Send Mail";
            worksheet.Cell(4, originalColumn + 2).Value = "Total Mark";
            originalColumn += 2;
            //Set Style For Header
            for (int i = 1; i <= originalColumn; i++)
            {
                worksheet.Cell(4, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(4, i).Style.Fill.BackgroundColor = XLColor.BlueGray;
                worksheet.Cell(4, i).Style.Font.FontColor = XLColor.White;
                worksheet.Cell(4, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            #endregion
            int index = 1;
            int rowBegin = 5;
            foreach (sp_GetCandidateListByExamResult candidate in candidateList)
            {
                bool isNan = false;
                int originalCell = 5;
                double totalMark = 0;
                double totalMaxMark = 0;
                double totalWritingMaxMark = candidateAnswerDAO.GetMaxWritingMark(candidate.ID, Constants.LOT_WRITING_SKILL_ID);
                double totalWritingMark = candidate.WritingMark != null ? candidate.WritingMark.Value : Constants.WRITTING_MARK_NULL;
                worksheet.Cell(rowBegin, 1).Value = index.ToString();
                worksheet.Cell(rowBegin, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                if (exam.ExamType != Constants.LOT_CANDIDATE_EXAM_ID)
                {
                    worksheet.Cell(rowBegin, 2).Value = candidate.CandidateID;
                    worksheet.Cell(rowBegin, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(rowBegin, 3).Value = candidate.DisplayName;
                    worksheet.Cell(rowBegin, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    worksheet.Cell(rowBegin, 4).Value = !string.IsNullOrEmpty(candidate.Email) ? candidate.Email : String.Empty;
                    worksheet.Cell(rowBegin, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    if (totalWritingMark != Constants.WRITTING_MARK_NULL)
                    {
                        worksheet.Cell(rowBegin, 5).Value = "'" + Math.Round(totalWritingMark).ToString() + "/" + totalWritingMaxMark.ToString();
                    }
                }
                else
                {
                    originalCell = 4;
                    worksheet.Cell(rowBegin, 2).Value = candidate.DisplayName;
                    worksheet.Cell(rowBegin, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    worksheet.Cell(rowBegin, 3).Value = candidate.Email;
                    worksheet.Cell(rowBegin, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    //Check mark of writing
                    if (totalWritingMark != Constants.WRITTING_MARK_NULL)
                    {
                        worksheet.Cell(rowBegin, 4).Value = Math.Round(totalWritingMark) + "/" + totalWritingMaxMark;
                        worksheet.Cell(rowBegin, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            ;
                    }
                }
                for (int i = 1; i <= originalColumn; i++)
                {
                    worksheet.Column(i).AdjustToContents();
                }
                foreach (LOT_ExamQuestion_Section section in listSection)
                {
                    worksheet.Cell(rowBegin, originalCell + 1).Value = SetResultByVerbalMarkType(section, exam, candidate, ref totalMark, ref totalMaxMark, ref isNan);
                    originalCell++;
                }
                //Check mark of writing
                string totalStringMark = Math.Round(totalMark) + "/" + totalMaxMark;
                if (totalWritingMark != Constants.WRITTING_MARK_NULL)
                {
                    totalStringMark = Math.Round(totalMark + totalWritingMark) + "/" + (totalMaxMark + totalWritingMaxMark);
                }
                worksheet.Cell(rowBegin, originalCell + 1).Value = candidate.SendMail ? Constants.LOT_SENT_MAIL : string.Empty;
                string strTotalMark = string.Empty;
                if (!isNan)
                {
                    strTotalMark = Math.Round(totalMark) + "/" + (totalMaxMark);
                    if (totalWritingMark != Constants.WRITTING_MARK_NULL)
                    {
                        strTotalMark = Math.Round(totalMark + totalWritingMark) + "/" + (totalMaxMark + totalWritingMaxMark);
                    }
                }
                worksheet.Cell(rowBegin, originalCell + 2).Value = strTotalMark;
                worksheet.Range(worksheet.Cell(rowBegin, 1).Address, worksheet.Cell(rowBegin, originalCell + 2).Address).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(rowBegin, 1).Address, worksheet.Cell(rowBegin, originalCell + 2).Address).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(rowBegin, 5).Address, worksheet.Cell(rowBegin, originalCell + 2).Address).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rowBegin++;
                index++;
            }

            string filepath = Server.MapPath("~/Export/") + title + DateTime.Now.Ticks.ToString();
            wb.SaveAs(filepath);
            string filename = title.Replace(" ", "_") + ".xlsx";

            return new ExcelResult { FileName = filename, Path = filepath };
        }
        #endregion

        #region Old Code
        ///// <summary>
        ///// Get CandidateTest List for JQGrid
        ///// </summary>
        ///// <param name="examId"></param>
        ///// <returns></returns>
        //[CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Mark)]
        //public ActionResult GetCandidateTestListJQGrid(string examId)
        //{
        //    if (!string.IsNullOrEmpty(examId))
        //    {
        //        #region JQGrid Params

        //        string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
        //        string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
        //        int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
        //        int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

        //        #endregion

        //        #region GetList
        //        List<sp_GetCandidateListByExamResult> candidateList = examDao.GetCandidateListByExam(int.Parse(examId), sortColumn, sortOrder);
        //        #endregion

        //        // calculate mark and get max mark for each candidate exam
        //        List<List<object>> listProcess = new List<List<object>>();
        //        for (int i = 0; i < candidateList.Count; i++)
        //        {
        //            sp_GetCandidateListByExamResult candidateExam = candidateList[i];
        //            List<object> objs = new List<object>();
        //            double totalWritingMaxMark = candidateAnswerDAO.GetMaxWritingMark(candidateExam.ID, Constants.LOT_WRITING_SKILL_ID);
        //            double totalWritingMark = candidateExam.WritingMark != null ? candidateExam.WritingMark.Value : Constants.WRITTING_MARK_NULL;
        //            //Code calcaulate Program mark
        //            //double totalProgramingMaxMark = candidateAnswerDAO.GetMaxWritingMark(candidateExam.ID, Constants.LOT_PROGRAMMING_SKILL_ID);
        //            //double totalPrograminMark = candidateExam.ProgramingMark != null ? candidateExam.ProgramingMark.Value : Constants.WRITTING_MARK_NULL;
        //            // writing mark
        //            objs.Add(totalWritingMaxMark);
        //            objs.Add(totalWritingMark);

        //            double totalMaxMark = totalWritingMaxMark;
        //            double totalMark = totalWritingMark == Constants.WRITTING_MARK_NULL ? 0 : totalWritingMark;
        //            //Code calcaulate Program mark
        //            //totalMaxMark = totalMaxMark + totalProgramingMaxMark;
        //            //totalMark = totalMark + (totalPrograminMark == Constants.WRITTING_MARK_NULL ? 0 : totalPrograminMark);
        //            double totalPrograminMark = 0;
        //            double totalProgramingMaxMark = candidateAnswerDAO.GetMaxWritingMark(candidateExam.ID, Constants.LOT_TECHNICAL_SKILL_ID);
        //            examDao.CalculateMark(candidateExam.ID, ref totalMaxMark, ref totalMark, ref totalPrograminMark);
        //            // Total mark
        //            objs.Add(totalMaxMark);
        //            objs.Add(totalMark);

        //            objs.Add(candidateExam.ID);
        //            objs.Add(candidateExam.CandidateID);
        //            objs.Add(HttpUtility.HtmlEncode(candidateExam.DisplayName));
        //            objs.Add(HttpUtility.HtmlEncode(candidateExam.Email));
        //            objs.Add(candidateExam.LoginDate);
        //            objs.Add(candidateExam.SendMail);
        //            objs.Add(candidateExam.ExamID);
        //            // programing mark
        //            objs.Add(totalProgramingMaxMark);
        //            objs.Add(totalPrograminMark);
        //            objs.Add(candidateExam.IsFinish);
        //            listProcess.Add(objs);
        //        }
        //        // sort mark
        //        if (sortColumn == "Mark")
        //        {
        //            int order = sortOrder == Constants.SORT_DESC ? -1 : 1;
        //            listProcess.Sort(
        //                 delegate(List<object> m1, List<object> m2)
        //                 { return ((double)m1[3]).CompareTo((double)m2[3]) * order; });
        //        }
        //        else if (sortColumn == "ID")
        //        {
        //            int order = sortOrder == Constants.SORT_DESC ? -1 : 1;
        //            listProcess.Sort(
        //                 delegate(List<object> m1, List<object> m2)
        //                 { return ((double)m1[4]).CompareTo((double)m2[4]) * order; });
        //        }

        //        var list = listProcess.Skip((pageIndex - 1) * rowCount).Take(rowCount);
        //        int totalRecords = candidateList.Count();
        //        int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

        //        var jsonData = ToJSONData(list, totalPages, pageIndex, totalRecords, rowCount, examId);
        //        return Json(jsonData, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// Parse list object to JSON data
        ///// </summary>
        ///// <param name="list"></param>
        ///// <param name="totalPages"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="totalRecords"></param>
        ///// <param name="rowCount"></param>
        ///// <returns></returns>
        //private object ToJSONData(IEnumerable<List<object>> list, int totalPages, int pageIndex, int totalRecords, int rowCount, string examId)
        //{
        //    // hide writing mark if it is null
        //    // m[4] => candidateExamID, m[5] => candidateID
        //    int j = (pageIndex - 1) * rowCount;
        //    var jsonData = new
        //    {
        //        total = totalPages,
        //        page = pageIndex,
        //        records = totalRecords,
        //        rowCount = rowCount,
        //        rows = (
        //            from m in list
        //            select new
        //            {
        //                i = m[4],
        //                cell = new string[] {
        //                        Convert.ToString(j+=1),
        //                        m[4].ToString(),
        //                        !(bool)m[13]?m[6].ToString():
        //                            CommonFunc.Link(m[4].ToString(),string.Format(Constants.CTL_LINK_CANDIDATE_DETAIL, m[4],examId), m[6].ToString(),true),
        //                        m[7] != null ? m[7].ToString(): string.Empty,
        //                        ((double)m[2])==0?"": Math.Round((double)m[3]) + "/" + m[2],
        //                        ((double)m[1])==Constants.WRITTING_MARK_NULL?"": Math.Round((double)m[1]) + "/" + m[0],
        //                        ((double)m[12])==Constants.WRITTING_MARK_NULL?"": Math.Round((double)m[12]) + "/" + m[11],
        //                        ((bool)m[9]) == true ? Constants.LOT_SENT_MAIL : string.Empty,
        //                        ((double)m[0]==0?"": CommonFunc.Button("icon edit",Constants.CTL_UPDATE_WRITING_MARK,"CRM.popup('/Exam/InputMark/"+ m[4].ToString() +"', 'Input Mark', 650)"))
        //                        + ((examDao.CheckProgrammingSkillSection((int)m[10]) && (double)m[11] != 0) ? CommonFunc.Button("icon edit-program",Constants.CTL_UPDATE_PROGRAMING_SKILL_REMARK,"CRM.popup('/Exam/InputProgramingMark/"+ m[4].ToString() +"', 'Update Programming Mark', 650)") : string.Empty)
        //                        + ((examDao.HasVerbalSection((int)m[10])) ? CommonFunc.Button("icon edit-verbal", Constants.CTL_UPDATE_VERBAL_MARK, 
        //                            "CRM.popup('/Exam/InputVerbalMark/"+ m[4].ToString() +"', '" + Constants.CTL_UPDATE_VERBAL_MARK + "', 500)") : string.Empty)
        //                        /*Added by Tai Nguyen on 18-Jan-2010*/
        //                        + ( ((double)m[2] == 0 || m[7] == null) ? "" : CommonFunc.Button("lot_send_email", Constants.CTL_EMAIL_TITLE_DIALOG
        //                            , "CRM.popup('/Exam/SendEmailToCandidate/"+ m[4].ToString() 
        //                            + "', '" + Constants.CTL_EMAIL_TITLE_DIALOG + "', " + Constants.CTL_EMAIL_DIALOG_WIDTH + ")") )
        //                        + ( ((double)m[2]) == 0 ? "" : CommonFunc.Button("lot_excel", Constants.CTL_EXCEL_BUTTON_TITLE
        //                            , "window.location = '/Exam/ExportResultToExcel?candidateExamID="+ m[4].ToString()+"'" ))
        //                        /*End Tai Nguyen*/
        //                    }
        //            }
        //        ).ToArray()
        //    };

        //    return jsonData;
        //}
        #endregion
        /// <summary>
        /// Export Test Result to Excel
        /// </summary>
        /// <param name="examID"></param>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Mark)]
        public void ExportResultToExcel(string candidateExamID)
        {
            if (!string.IsNullOrEmpty(candidateExamID))
            {
                LOT_Candidate_Exam can_Exam = canExamDao.GetByID(int.Parse(candidateExamID));
                if (can_Exam != null)
                {
                    double markMultipleChoice = 0, markSentenceCorrection = 0, markComprehension = 0, markListening = 0;
                    double? avgMark = null;
                    double maxMarkMultipleChoice = 0, maxMarkSentenceCorrection = 0, maxMarkComprehension = 0, maxMarkListening = 0;
                    string strToExcel = string.Empty;
                    strToExcel += "<p><b>TEST RESULT</b></p>";
                    strToExcel += "   <table width=800 style=\"border:1px solid #000\"><tr><td><table>";
                    strToExcel += "       <tr><td><b><i>Candidate:</i></b></td><td>{0}</td><td><b><i>Title:</i></b></td><td>{1}</td></tr>";
                    string canName = string.Empty;
                    string jobTitle = string.Empty;
                    //check exam for candidate or Employee and fill data
                    if (can_Exam.LOT_Exam.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
                    {
                        canName = can_Exam.Candidate.FirstName + " " + can_Exam.Candidate.MiddleName + " " + can_Exam.Candidate.LastName;
                        jobTitle = can_Exam.Candidate.JobTitleLevel.DisplayName;
                    }
                    else
                    {
                        canName = can_Exam.Employee.ID + " - " + can_Exam.Employee.FirstName + " " + can_Exam.Employee.MiddleName + " " + can_Exam.Employee.LastName;
                        jobTitle = can_Exam.Employee.JobTitleLevel.DisplayName;
                    }

                    strToExcel = string.Format(strToExcel, canName, jobTitle);

                    strToExcel += "       <tr><td>Exam Name:</td><td>" + can_Exam.LOT_Exam.Title + "</td><td></td><td></td></tr>";
                    strToExcel += "       <tr><td>Exam Date:</td><td class=\"text\" >" + can_Exam.LOT_Exam.ExamDate.ToString(Constants.DATETIME_FORMAT_VIEW) + "</td><td></td><td></td></tr>";
                    strToExcel += "       <tr><td><b>Total Mark:</b></td><td class=\"text\"><b>#TOTAL</b></td><td></td><td></td></tr></table>";

                    //fill mark data
                    double totalMark = 0;
                    double totalMaxMark = 0;
                    double mark = 0;
                    double maxMark = 0;

                    strToExcel += "<table border=1><tr><th colspan='4'>Summary</th></tr>" +
                        "<tr><th colspan=2 style=\"background-color:Gray\">Section</h><th colspan=2 style=\"background-color:Gray\">Mark</th></tr>{0}";

                    List<LOT_ExamQuestion_Section> exQuesionSectionList = examQuestionSectionDAO.GetListByExamQuestionID(can_Exam.LOT_Exam.LOT_ExamQuestion.ID);

                    foreach (LOT_ExamQuestion_Section item in exQuesionSectionList)
                    {
                        examDao.GetMark(can_Exam.ID, item.ID, ref mark, ref maxMark);

                        //check valid mark
                        if (mark != Constants.WRITTING_MARK_NULL && !double.IsNaN(mark))
                        {
                            totalMark += mark;
                            totalMaxMark += maxMark;
                        }
                        else
                        {
                            mark = 0;
                        }
                        string strSection = string.Empty;
                        //fill mark data for each section
                        switch (item.LOT_Section.ID)
                        {
                            case Constants.LOT_WRITING_SKILL_ID:
                            case Constants.LOT_CRITICAL_THINKING_ID:
                            case Constants.LOT_PROGRAMMING_SKILL_ID:
                            case Constants.LOT_TECHNICAL_SKILL_ID:
                                strSection = "       <tr><td colspan=2>" + item.LOT_Section.SectionName + "</td><td colspan=2 align=center class=\"text\">" + mark.ToString() + "/" + maxMark.ToString() + "</td></tr>";
                                strToExcel += strSection;
                                break;
                            case Constants.LOT_MULTIPLE_CHOICE_QUESTION:
                                markMultipleChoice = mark;
                                maxMarkMultipleChoice = maxMark;
                                break;
                            case Constants.LOT_SENTENCE_CORRECTION_QUESTION:
                                markSentenceCorrection = mark;
                                maxMarkSentenceCorrection = maxMark;
                                break;
                            case Constants.LOT_COMPREHENSION_SKILL_ID:
                                markComprehension = mark;
                                maxMarkComprehension = maxMark;
                                break;
                            case Constants.LOT_VERBAL_SKILL_ID:
                                string result = string.Empty;
                                if (can_Exam.VerbalMarkType.Value == Constants.LOT_VERBAL_MARK_TYPE_LEVEL)
                                {
                                    result = Constants.LOT_VERBAL_MARK_STRING + " - "
                                        + (can_Exam.VerbalMark.HasValue ? can_Exam.VerbalMark.Value.ToString() : string.Empty);
                                }
                                else
                                {
                                    Training_MasterEnglishType objMasterEnglishType = new TrainingEmpEnglishInfoDao().GetMasterEnglishTypeByID(can_Exam.VerbalMarkType.Value);
                                    result = objMasterEnglishType.Name + " - "
                                                        + (can_Exam.VerbalMark.HasValue ? can_Exam.VerbalMark.Value.ToString() : string.Empty);
                                }   
                                strSection = "       <tr><td colspan=2>" + item.LOT_Section.SectionName + "</td><td colspan=2 align=center class=\"text\">" + result
                                    + "</td></tr>";
                                strToExcel += strSection;
                                break;
                        }
                        avgMark = CommonFunc.Average(markComprehension, markListening, markMultipleChoice, markSentenceCorrection);
                        if (avgMark.HasValue)
                        {
                            strToExcel = string.Format(strToExcel,
                                "<tr><td colspan=2>SKILLS</td><td colspan='2' align='center' class=\"text\">" +
                                avgMark.Value + "/100 - Level " + CommonFunc.GetEngLishSkillLevel((int)avgMark.Value) + "</td></tr>");
                        }
                        else
                            strToExcel = string.Format(strToExcel, "");
                        int iCandidateExamId = ConvertUtil.ConvertToInt(candidateExamID);
                        var tmplist = can_Exam.LOT_CandidateAnswers.Where(p => p.CandidateExamID == iCandidateExamId &&
                            (p.LOT_ExamQuestion_Section.SectionID == Constants.LOT_WRITING_SKILL_ID ||
                            p.LOT_ExamQuestion_Section.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID) &&
                            p.LOT_ExamQuestion_Section.ExamQuestionID == can_Exam.LOT_Exam.ExamQuestionID);
                        var listAnswerWriting = tmplist.Where(p => p.LOT_ExamQuestion_Section.SectionID == Constants.LOT_WRITING_SKILL_ID);
                        var listAnswerPrograming = tmplist.Where(p => p.LOT_ExamQuestion_Section.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID);
                        string breakLine = "<br style=\"mso-data-placement:same-cell;\">";
                        string strTab = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        string strWritingAnswer = "";
                        int count = 0;
                        foreach (var answer in listAnswerWriting)
                        {
                            count++;
                            strWritingAnswer += "<b style='mso-data-placement:same-cell;'>" +
                                count + ". " + answer.LOT_Question.QuestionContent.Replace("\n", breakLine + strTab).Replace("\t", strTab) +
                                "</b>" + breakLine + breakLine;
                            strWritingAnswer += strTab + answer.Essay.Replace("\n", breakLine + strTab).Replace("\t", strTab) + breakLine + breakLine;
                        }
                        string strProgramingAnswer = "";
                        count = 0;
                        foreach (var answer in listAnswerPrograming)
                        {
                            count++;
                            strProgramingAnswer += "<b style='mso-data-placement:same-cell;'>" +
                                count + ". " + answer.LOT_Question.QuestionContent.Replace("\n", breakLine + strTab).Replace("\t", strTab) +
                                "</b>" + breakLine + breakLine;
                            strProgramingAnswer += strTab + answer.Essay.Replace("\n", breakLine + strTab).Replace("\t", strTab) + breakLine + breakLine;
                        }
                        strToExcel += "<tr><th colspan='4'>Detail</th></tr>";
                        strToExcel += "<tr><th colspan='4' align='left' style='background-color:Gray'>Writing Skill</th></tr>";
                        strToExcel += "<tr><td colspan='4' align='left'>" + strWritingAnswer + "</td></tr>";
                        //Old Code of programming skill
                        //strToExcel += "<tr><th colspan='4' align='left' style='background-color:Gray'>Programming Skill</th></tr>";
                        //strToExcel += "<tr><td colspan='4' align='left'>" + strProgramingAnswer + "</td></tr>";
                        String fileName = canName.Replace(" ", "_") + "_Result.xls";
                        String filePath = Server.MapPath("~" + Constants.UPLOAD_TEMP_PATH);
                        strToExcel += "</table></td></tr></table>";
                        strToExcel = strToExcel.Replace("#TOTAL", totalMark.ToString() + "/" + totalMaxMark.ToString());
                        WriteFile(fileName, strToExcel);
                    }
                }
            }
        }

        /// <summary>
        /// Send thank you email to candidate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Mark)]
        public ActionResult SendEmailToCandidate(string id)
        {
            int candidateExamID = int.Parse(id);
            LOT_Candidate_Exam candidateExam = examDao.GetCandidateExamById(candidateExamID);
            string tmpFilePath = Server.MapPath(Constants.HTML_EMAIL_TEMPLATE_PATH);
            string temp = string.Empty;
            if (System.IO.File.Exists(tmpFilePath))
            {

                string host = Request.Url.Host;
                string port = Request.Url.Port.ToString();
                string host_port = "";
                if (port == "80" || port == "")
                    host_port = host;
                else
                    host_port = host + ":" + port;

                temp = System.IO.File.ReadAllText(tmpFilePath);
            }
            ViewData["template"] = temp;
            return View(candidateExam);
        }

        /// <summary>
        /// Send email to candidate
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Mark)]
        public ActionResult SendEmailToCandidate(FormCollection collection)
        {
            string host = ConfigurationManager.AppSettings["mailserver_host"];
            string port = ConfigurationManager.AppSettings["mailserver_port"];

            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string id = collection.Get("ID");
            string from = Constants.CTL_SENDER_EMAIL_ADDRESS;
            string fromName = Constants.CTL_SENDER_NAME;
            Message msg = null;
            string toEmailList = collection.Get("To");
            string msgCode = CommonController.CanSendEmail(toEmailList, "");
            if (!msgCode.Equals(MessageConstants.I0002))
            {
                msg = new Message(MessageConstants.E0032, MessageType.Error);
            }
            else
            {
                string[] ccName = collection.Get("CC").Trim().Split(';');
                string ccMail = "";
                foreach (string name in ccName)
                {
                    if (!String.IsNullOrWhiteSpace(name))
                    {
                        ccMail += CommonFunc.GetEmailByLoginName(name) + ";";
                    }
                }

                msg = canExamDao.SendMailAndUpdate(id, host, port, from, fromName, toEmailList, ccMail, collection.Get("Subject"), collection.Get("body"));
            }
            ShowMessage(msg);
            return RedirectToAction(collection.Get("page"));
        }

        /// <summary>
        /// InputMark
        /// </summary>
        /// <param name="id">Candidate exam id</param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Mark)]
        public ActionResult InputMark(string id)
        {
            int canExamId = int.Parse(id);
            List<sp_GetEssayInfoByCandidateExamIDResult> writingInfos = examDao.GetEssayInfo(canExamId, Constants.LOT_WRITING_SKILL_ID);
            ViewData[CommonDataKey.CTL_WRITING_INFOS] = writingInfos;
            LOT_Candidate_Exam candidateExam = examDao.GetCandidateExamById(canExamId);

            int maxWritingMark = candidateAnswerDAO.GetMaxWritingMark(canExamId, Constants.LOT_WRITING_SKILL_ID);
            ViewData[CommonDataKey.CTL_MAX_WRITING_MARK] = maxWritingMark;

            if (candidateExam.LOT_Exam.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
            {
                if (string.IsNullOrEmpty(candidateExam.Candidate.MiddleName))
                {
                    ViewData[CommonDataKey.CTL_DISPLAY_NAME] = HttpUtility.HtmlEncode(candidateExam.Candidate.FirstName + " " + candidateExam.Candidate.LastName);
                }
                else
                {
                    ViewData[CommonDataKey.CTL_DISPLAY_NAME] = HttpUtility.HtmlEncode(candidateExam.Candidate.FirstName + " " + candidateExam.Candidate.MiddleName + " " + candidateExam.Candidate.LastName);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(candidateExam.Employee.MiddleName))
                {
                    ViewData[CommonDataKey.CTL_DISPLAY_NAME] = HttpUtility.HtmlEncode(candidateExam.Employee.FirstName + " " + candidateExam.Employee.LastName);
                }
                else
                {
                    ViewData[CommonDataKey.CTL_DISPLAY_NAME] = HttpUtility.HtmlEncode(candidateExam.Employee.FirstName + " " + candidateExam.Employee.MiddleName + " " + candidateExam.Employee.LastName);
                }
            }

            if (candidateExam.UpdateDate != null)
            {
                ViewData[CommonDataKey.CTL_UPDATE_DATE] = candidateExam.UpdateDate;
            }
            ViewData[CommonDataKey.CTL_WRITING_MARK] = candidateExam.WritingMark;
            ViewData[CommonDataKey.CTL_WRITING_COMMENT] = HttpUtility.HtmlEncode(candidateExam.WritingComment);

            return View();
        }

        /// <summary>
        /// InputMark
        /// </summary>
        /// <param name="candidateExam"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Mark)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InputMark(LOT_Candidate_Exam candidateExam)
        {
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                candidateExam.UpdatedBy = principal.UserData.UserName;
                Message msg = examDao.Update(candidateExam);
                ShowMessage(msg);
                return RedirectToAction("/CandidateTestList/" + candidateExam.ExamID);
            }
            catch (Exception)
            {
                return View();
            }
        }


        /// <summary>
        /// Input Remark for programming skill section
        /// </summary>
        /// <param name="id">Candidate exam id</param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Mark)]
        public ActionResult InputProgramingMark(string id)
        {
            int canExamId = int.Parse(id);
            List<sp_GetEssayInfoByCandidateExamIDResult> programingInfos = examDao.GetEssayInfo(canExamId, Constants.LOT_PROGRAMMING_SKILL_ID);
            ViewData[CommonDataKey.CTL_WRITING_INFOS] = programingInfos;
            LOT_Candidate_Exam candidateExam = examDao.GetCandidateExamById(canExamId);

            int maxProgramingMark = candidateAnswerDAO.GetMaxWritingMark(canExamId, Constants.LOT_PROGRAMMING_SKILL_ID);
            ViewData[CommonDataKey.CTL_MAX_PROGRAMING_MARK] = maxProgramingMark;

            if (candidateExam.LOT_Exam.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
            {
                if (string.IsNullOrEmpty(candidateExam.Candidate.MiddleName))
                {
                    ViewData[CommonDataKey.CTL_DISPLAY_NAME] = HttpUtility.HtmlEncode(candidateExam.Candidate.FirstName + " " + candidateExam.Candidate.LastName);
                }
                else
                {
                    ViewData[CommonDataKey.CTL_DISPLAY_NAME] = HttpUtility.HtmlEncode(candidateExam.Candidate.FirstName + " " + candidateExam.Candidate.MiddleName + " " + candidateExam.Candidate.LastName);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(candidateExam.Employee.MiddleName))
                {
                    ViewData[CommonDataKey.CTL_DISPLAY_NAME] = HttpUtility.HtmlEncode(candidateExam.Employee.FirstName + " " + candidateExam.Employee.LastName);
                }
                else
                {
                    ViewData[CommonDataKey.CTL_DISPLAY_NAME] = HttpUtility.HtmlEncode(candidateExam.Employee.FirstName + " " + candidateExam.Employee.MiddleName + " " + candidateExam.Employee.LastName);
                }
            }

            if (candidateExam.UpdateDate != null)
            {
                ViewData[CommonDataKey.CTL_UPDATE_DATE] = candidateExam.UpdateDate;
            }
            ViewData[CommonDataKey.CTL_PROGRAMING_MARK] = candidateExam.ProgramingMark;
            ViewData[CommonDataKey.CTL_PROGRAMING_COMMENT] = HttpUtility.HtmlEncode(candidateExam.ProgramingComment);

            return View();
        }

        /// <summary>
        /// Input Remark
        /// </summary>
        /// <param name="candidateExam"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Mark)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InputProgramingMark(LOT_Candidate_Exam candidateExam)
        {
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                candidateExam.UpdatedBy = principal.UserData.UserName;
                Message msg = examDao.UpdateProgramingMark(candidateExam);
                ShowMessage(msg);
                return RedirectToAction("/CandidateTestList/" + candidateExam.ExamID);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Mark)]
        public ActionResult InputVerbalMark(string id)
        {
            try
            {
                var candidateExam = canExamDao.GetByID(ConvertUtil.ConvertToInt(id));
                var typeList = new TrainingEmpEnglishInfoDao().GetTypeList();
                bool isLevelInput = candidateExam.VerbalMarkType == Constants.LOT_VERBAL_MARK_TYPE_LEVEL;
                typeList.Insert(0, new Training_MasterEnglishType() { ID = Constants.LOT_VERBAL_MARK_TYPE_LEVEL, Name = Constants.LOT_VERBAL_MARK_STRING });
                ViewData[CommonDataKey.LOT_VERBAL_MARK_TYPE] = new SelectList(typeList, "ID", "Name", candidateExam.VerbalMarkType);
                ViewData[CommonDataKey.LOT_VERBAL_COMMENT] = candidateExam.VerbalComment;
                ViewData[CommonDataKey.LOT_VERBAL_TESTED_BY] = candidateExam.VerbalTestedBy;
                ViewData[CommonDataKey.LOT_VERBAL_MARK] = isLevelInput ? "" : candidateExam.VerbalMark.ToString();
                ViewData[CommonDataKey.LOT_VERBAL_UPDATE_DATE] = candidateExam.UpdateDate.ToString();
                ViewData[CommonDataKey.LOT_VERBAL_LEVEL_LIST] = new SelectList(new TrainingLevelMappingDao().GetListVerbalMapping(),
                    "VerbalLevel", "VerbalLevel", isLevelInput ? candidateExam.VerbalMark.ToString() : "");
                return View();
            }
            catch
            {
                return Content(Resources.Message.E0001);
            }
        }

        /// <summary>
        /// Input Remark
        /// </summary>
        /// <param name="candidateExam"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Exam, Rights = Permissions.Mark)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InputVerbalMark()
        {
            var candidateExam = canExamDao.GetByID(ConvertUtil.ConvertToInt(Request["CandidateExamId"]));
            Message msg = null;
            try
            {
                int markType = ConvertUtil.ConvertToInt(Request[CommonDataKey.LOT_VERBAL_MARK_TYPE]);
                candidateExam.VerbalMarkType = markType;
                if (markType == Constants.LOT_VERBAL_MARK_TYPE_LEVEL)
                    candidateExam.VerbalMark = float.Parse(Request[CommonDataKey.LOT_VERBAL_LEVEL_LIST]);
                else
                    candidateExam.VerbalMark = float.Parse(Request[CommonDataKey.LOT_VERBAL_MARK]);
                candidateExam.VerbalComment = Request[CommonDataKey.LOT_VERBAL_COMMENT];
                candidateExam.UpdateDate = DateTime.Parse(Request[CommonDataKey.LOT_VERBAL_UPDATE_DATE]);
                candidateExam.VerbalTestedBy = Request[CommonDataKey.LOT_VERBAL_TESTED_BY];
                msg = examDao.UpdateVerbalMark(candidateExam);

            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0001, MessageType.Error);
                return View();
            }
            ShowMessage(msg);
            return RedirectToAction("/CandidateTestList/" + candidateExam.ExamID);
        }
        [HttpPost]
        public JsonResult CheckNameExists(string name)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Message msg = null;
            try
            {
                CommonFunc.GetDomainUser(name);
                result.Data = true;
            }
            catch
            {
                msg = new Message(MessageConstants.E0030, MessageType.Error, name);
                result.Data = msg.ToString();
            }
            //string displayName = domainUser.Properties[CommonFunc.GetDomainUserProperty(DomainUserProperty.LoginName)][0].ToString();
            //if (displayName.Equals(name))
            return result;
        }
        #endregion

        #region Huy Ly
        /// <summary>
        /// Export Candidate List to Excel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ExportCandidateListToExcel(int id)
        {
            try
            {
                LOT_Exam exam = examDao.GetByID(id);

                if (exam.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
                {
                    List<sp_GetCandidateExamResult> CandidateList = examDao.GetCandidateExam(id);

                    ExportExcel exp = new ExportExcel();
                    exp.Title = Constants.LOT_EXAM_CANDIDATE_LIST_TITLE_EXPORT_EXCEL;
                    exp.FileName = Constants.LOT_EXAM_CANDIDATE_LIST_EXPORT_EXCEL + string.Format("-{0}.xls", DateTime.Now.ToString("dd-MMM-yyyy"));
                    exp.ColumnList = new string[] { "DisplayName", "DOB:Date", "CellPhone:Text", "Gender:Gender",
                    "SearchDate:Date", "SourceName", "Title", "Status:Candidate", "CandidatePin" };
                    exp.HeaderExcel = new string[] { "Full Name", "DOB", "CellPhone", "Gender",
                    "Searched date", "Source", "Position", "Status", "Pin" };
                    exp.List = CandidateList;
                    exp.IsRenderNo = true;
                    exp.Execute();
                }
                else
                {
                    List<sp_GetEmployeeExamResult> employeeList = examDao.GetEmployeeExam(id);

                    ExportExcel exp = new ExportExcel();
                    exp.Title = Constants.LOT_EXAM_CANDIDATE_LIST_TITLE_EXPORT_EXCEL;
                    exp.FileName = Constants.LOT_EXAM_CANDIDATE_LIST_EXPORT_EXCEL + string.Format("-{0}.xls", DateTime.Now.ToString("dd-MMM-yyyy"));
                    exp.ColumnList = new string[] { "EmployeeID", "DisplayName", "TitleName", "Department", "DepartmentName",
                    "StartDate:Date", "StatusName", "CandidatePin" };
                    exp.HeaderExcel = new string[] { "ID","Full Name", "Job Title", "Department", "SubDepartment",
                    "Start Date", "Status", "Pin" };
                    exp.List = employeeList;
                    exp.IsRenderNo = false;
                    exp.Execute();
                }

                return View();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Export Candidate Pin to Excel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ExportCandidatePinToExcel(int id)
        {
            try
            {
                LOT_Exam exam = examDao.GetByID(id);
                if (exam.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
                {
                    List<sp_GetCandidateExamResult> CandidateList = examDao.GetCandidateExam(id);

                    string fileName = string.Format("{0}-{1}.xls", Constants.LOT_EXAM_CANDIDATE_PIN_EXPORT_EXCEL, DateTime.Now.ToString("dd-MMM-yyyy"));

                    using (StringWriter sw = new StringWriter())
                    {
                        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                        {
                            //Get Info of Exam
                            string examName = string.Empty;
                            string examDate = string.Empty;
                            int time = 0;
                            if (exam != null)
                            {
                                time = exam.LOT_ExamQuestion.ExamQuestionTime;
                                examName = exam.Title;
                                examDate = exam.ExamDate.ToString("dd-MMM-yyyy");
                            }

                            //  Create a table to contain content
                            HtmlTable table = new HtmlTable();

                            //  render the table into the htmlwriter
                            table.RenderControl(htw);

                            string styleColumnTitle = " style=\"width:100px; padding-left:15px\"";
                            string spacer = "&nbsp;&nbsp;";

                            string strToExcel = string.Empty;
                            strToExcel += "<p><b>Candidate PIN</b></p>";
                            string col = "<td style=width:200px>{0}<td>";
                            string temp = string.Empty;
                            string item = "";
                            item += "   <table width=100 style=\"border:1px solid #000\">";
                            item += "       <tr><td></td><td></td></tr>";
                            item += "       <tr><td{0}>Exam Name:</td><td><b>" + examName + "</b>{3}</td></tr>";
                            item += "       <tr><td{0}>Exam Date:</td><td><b>" + examDate + "</b>{3}</td></tr>";
                            item += "       <tr><td{0}>Time:</td><td><b>" + time + "</b>{3}</td></tr>";
                            item += "       <tr><td></td><td></td></tr>";
                            item += "       <tr><td{0}>Your Name:</td><td><b>{1}</b>{3}</td></tr>";
                            item += "       <tr><td{0}>Your PIN:</td><td><b>{2}</b>{3}</td></tr>";
                            item += "       <tr><td></td><td></td></tr>";
                            item += "   </table><br />";

                            int i = 0;
                            string str = string.Empty;
                            foreach (sp_GetCandidateExamResult candidate in CandidateList)
                            {
                                str = string.Format(col, string.Format(item, styleColumnTitle, candidate.DisplayName, candidate.CandidatePin, spacer));
                                if (i % 2 == 0)
                                {
                                    temp += "<tr>";
                                }

                                temp += str;

                                if (i % 2 != 0 && i == CandidateList.Count)
                                {
                                    temp += "</tr>";
                                }
                                i++;
                            }
                            strToExcel += string.Format("<table>{0}<table>", temp);
                            strToExcel += sw.ToString();

                            WriteFile(fileName, strToExcel);
                        }
                    }
                }
                else
                {
                    List<sp_GetEmployeeExamResult> employeeList = examDao.GetEmployeeExam(id);

                    string fileName = string.Format("{0}-{1}.xls", Constants.LOT_EXAM_CANDIDATE_PIN_EXPORT_EXCEL, DateTime.Now.ToString("dd-MMM-yyyy"));

                    using (StringWriter sw = new StringWriter())
                    {
                        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                        {
                            //Get Info of Exam
                            string examName = string.Empty;
                            string examDate = string.Empty;
                            int time = 0;
                            if (exam != null)
                            {
                                time = exam.LOT_ExamQuestion.ExamQuestionTime;
                                examName = exam.Title;
                                examDate = exam.ExamDate.ToString("dd-MMM-yyyy");
                            }

                            //  Create a table to contain content
                            HtmlTable table = new HtmlTable();

                            //  render the table into the htmlwriter
                            table.RenderControl(htw);

                            string styleColumnTitle = " style=\"width:100px; padding-left:15px\"";
                            string spacer = "&nbsp;&nbsp;";

                            string strToExcel = string.Empty;
                            strToExcel += "<p><b>Candidate PIN</b></p>";
                            string col = "<td style=width:200px>{0}<td>";
                            string temp = string.Empty;
                            string item = "";
                            item += "   <table width=100 style=\"border:1px solid #000\">";
                            item += "       <tr><td></td><td></td></tr>";
                            item += "       <tr><td{0}>Exam Name:</td><td><b>" + examName + "</b>{3}</td></tr>";
                            item += "       <tr><td{0}>Exam Date:</td><td><b>" + examDate + "</b>{3}</td></tr>";
                            item += "       <tr><td{0}>Time:</td><td><b>" + time + "</b>{3}</td></tr>";
                            item += "       <tr><td></td><td></td></tr>";
                            item += "       <tr><td{0}>Your Name:</td><td><b>{1}</b>{3}</td></tr>";
                            item += "       <tr><td{0}>Your PIN:</td><td><b>{2}</b>{3}</td></tr>";
                            item += "       <tr><td></td><td></td></tr>";
                            item += "   </table><br />";

                            int i = 0;
                            string str = string.Empty;
                            foreach (sp_GetEmployeeExamResult employee in employeeList)
                            {
                                str = string.Format(col, string.Format(item, styleColumnTitle, employee.DisplayName, employee.CandidatePin, spacer));
                                if (i % 2 == 0)
                                {
                                    temp += "<tr>";
                                }

                                temp += str;

                                if (i % 2 != 0 && i == employeeList.Count)
                                {
                                    temp += "</tr>";
                                }
                                i++;
                            }
                            strToExcel += string.Format("<table>{0}<table>", temp);
                            strToExcel += sw.ToString();

                            WriteFile(fileName, strToExcel);
                        }
                    }
                }

                return View();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Write file that its type is excel file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        [NonAction()]
        private void WriteFile(string fileName, string content)
        {
            try
            {
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                Response.ContentType = "application/vnd.ms-excel";

                string style = @"<style> .text { mso-number-format:\@;} </style> ";
                Response.Write(style);
                Response.Write("<meta http-equiv=Content-Type content='text/html; charset=utf-8' />");
                Response.Write(content);
                Response.End();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion
    }
}
