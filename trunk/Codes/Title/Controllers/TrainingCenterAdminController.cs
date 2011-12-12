using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.Library.Common;
using System.Web.UI.WebControls;
using System.Collections;
using ClosedXML.Excel;
using CRM.Library.Attributes;

namespace CRM.Controllers 
{
    public class TrainingCenterAdminController : BaseController
    {

        #region Variables
        private TrainingClassDao classDao = new TrainingClassDao();
        private TrainingSkillTypeDao stDao = new TrainingSkillTypeDao();
        private UserAdminDao userAdminDao = new UserAdminDao();
        private TrainingAttendeeDao taDao = new TrainingAttendeeDao();
        private TrainingCenterDao trainDao = new TrainingCenterDao();
        private TrainingCourseDao courseDao = new TrainingCourseDao();
        private EmployeeDao empDao = new EmployeeDao();
        private DepartmentDao deptDao = new DepartmentDao();
        private JobTitleLevelDao levelDao = new JobTitleLevelDao();
        private TrainingEmpEnglishInfoDao englishInfoDao = new TrainingEmpEnglishInfoDao();
        private TrainingLevelMappingDao levelMappingDao = new TrainingLevelMappingDao();
        private TrainingMaterialDao materialDao = new TrainingMaterialDao();
        #endregion

        //
        // GET: /TrainingCenter/

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ProClass()
        {
            Hashtable hashData = Session[Constants.TRAINING_CENTER_ADMIN_LIST_PROFESSIONAL] == null ? new Hashtable() : (Hashtable)Session[Constants.TRAINING_CENTER_ADMIN_LIST_PROFESSIONAL];
            ViewData[Constants.TC_PROFESSIONAL_TEXT] = hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TEXT] == null ? Constants.TC_TEXT : (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TEXT];
            ViewData[Constants.TC_PROFESSIONAL_COURSE] = new SelectList(courseDao.GetList(Constants.TRAINING_CENTER_CLASS_TYPE_PRO_SKILL), "ID", "Name", ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COURSE]));
            ViewData[Constants.TC_PROFESSIONAL_TYPE] = new SelectList(stDao.GetList(), "ID", "Name", ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TYPE]));
            //int status = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_PROFESSIONAL_STATUS]);
            //ViewData[Constants.TC_PROFESSIONAL_STATUS] = new SelectList(trainDao.GetListCourseStatus(), "ID", "Name", status == 0 ? Constants.TRAINING_CENTER_COURSE_STATUS_OPEN : status);
            ViewData[Constants.TC_PROFESSIONAL_STATUS] = new SelectList(trainDao.GetListRegStatus(), "ID", "Name", Session[Constants.TRAINING_CENTER_ADMIN_LIST_PROFESSIONAL] == null ? Constants.TRAINING_CENTER_COURSE_STATUS_OPEN.ToString() : (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_STATUS]);
            ViewData[Constants.TC_PROFESSIONAL_INSTRUCTOR] = new SelectList(trainDao.GetListIntructor(), "Value", "Text", (hashData[Constants.TRAINING_CENTER_PROFESSIONAL_INSTRUCTOR] == null ? Constants.FIRST_ITEM_INTRUCTOR : (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_INSTRUCTOR]));

            ViewData[Constants.TC_PROFESSIONAL_COLUMN] = hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COLUMN] == null ? "ID" : hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COLUMN];
            ViewData[Constants.TC_PROFESSIONAL_ORDER] = hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ORDER] == null ? "desc" : hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ORDER];
            ViewData[Constants.TC_PROFESSIONAL_PAGE_INDEX] = hashData[Constants.TRAINING_CENTER_PROFESSIONAL_PAGE_INDEX] == null ? "1" : hashData[Constants.TRAINING_CENTER_PROFESSIONAL_PAGE_INDEX].ToString();
            ViewData[Constants.TC_PROFESSIONAL_ROW_COUNT] = hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ROW] == null ? "20" : hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ROW].ToString();
            return View();
        }



        /// <summary>
        /// Set Session in Professional Trainning
        /// </summary>
        /// <param name="text"></param>
        /// <param name="course"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="instructor"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionProFessionalFilterList(string text, string course, string type, string status, string instructor,
           string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_TEXT, text);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_COURSE, course);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_TYPE, type);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_STATUS, status);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_INSTRUCTOR, instructor);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_COLUMN, column);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_ORDER, order);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_ROW, rowCount);

            Session[Constants.TRAINING_CENTER_ADMIN_LIST_PROFESSIONAL] = hashData;
        }

        public ActionResult Export(string id)
        {
            List<sp_GetClassPlanningResult> list = new List<sp_GetClassPlanningResult>();
            string sortColumn = "ID";
            string sortOrder = "desc";
            Hashtable hashData = new Hashtable();
            int typeCourse = 0;
            if (Session[Constants.TRAINING_CENTER_ADMIN_LIST_PROFESSIONAL] == null && Session[Constants.TRAINING_CENTER_ADMIN_LIST_CLASS] == null)
            {
                list = trainDao.GetList(null, 0, 0, 0, null, typeCourse);
            }
            else
            {
                if (string.IsNullOrEmpty(id))
                {
                    hashData = (Hashtable)Session[Constants.TRAINING_CENTER_ADMIN_LIST_PROFESSIONAL];
                    typeCourse = Constants.TRAINING_CENTER_CLASS_TYPE_PRO_SKILL;
                }
                else
                {
                    hashData = (Hashtable)Session[Constants.TRAINING_CENTER_ADMIN_LIST_CLASS];
                    typeCourse = Constants.TRAINING_CENTER_CLASS_TYPE_ENGLISH;
                }
            }
            string text = null;
            if (hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TEXT] != null)
            {
                if ((string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TEXT] != Constants.TC_TEXT)
                {
                    text = (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TEXT];
                }
            }
            int course = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COURSE]);
            int type = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TYPE]);
            int status = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_PROFESSIONAL_STATUS]);
            string intructor = null;
            if (hashData[Constants.TRAINING_CENTER_PROFESSIONAL_INSTRUCTOR] != null)
            {
                intructor = (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_INSTRUCTOR];
            }
            sortColumn = hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COLUMN] == null ? "ID" : (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COLUMN];
            sortOrder = hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ORDER] == null ? "desc" : (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ORDER];
            list = trainDao.GetList(text, course, type, status, intructor, typeCourse);
            List<sp_GetClassPlanningResult> finalList = trainDao.Sort(list, sortColumn, sortOrder);
            string title = string.Empty;
            string[] column_it;
            string[] header_it;
            if (string.IsNullOrEmpty(id))
            {
                title = "Professional Training";
                column_it = new string[] { "ClassID:text", "CourseName:text", "SkillTypeName", "StatusName", "Duration:duration", "StartDate:date", "Instructors:text", "Attendess", "Objectives:text" };
                header_it = new string[] { "Class ID", "Course Name", "Type", "Status", "Duration", "StartDate", "Instructors", "# of Attendess", "Objectives" };
            }
            else
            {
                title = "Class Training";
                column_it = new string[] { "ClassID:text", "CourseName:text", "StatusName", "Duration:duration", "StartDate:date", "Instructors:text", "ClassTime:text", "Attendess", "Objectives:text" };
                header_it = new string[] { "Class ID", "Course Name", "Status", "Duration", "StartDate", "Instructors", "Class Time", "# of Attendess", "Objectives" };
            }
            ExportExcelAdvance exp = new ExportExcelAdvance();
            exp.Sheets = new List<CExcelSheet>{ new CExcelSheet{ Name=title, 
                                                    List= finalList,
                                                    Header = header_it, 
                                                    ColumnList= column_it,
                                                    Title = title,                
                                                    IsRenderNo = true}};
            title = title.Replace(" ", "_");
            string filepath = Server.MapPath("~/Export/") + title + DateTime.Now.Ticks.ToString();
            string filename = exp.ExportExcelMultiSheet(filepath);
            filename = title;

            return new ExcelResult { FileName = filename+".xlsx", Path = filepath };
        }

        /// <summary>
        /// Return List
        /// </summary>
        /// <param name="text"></param>
        /// <param name="course"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="instructor"></param>
        /// <returns></returns>
        public ActionResult GetListJQGrid(string text, string course, string type, string status, string intructor)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            SetSessionProFessionalFilterList(text, course, type, status, intructor, sortColumn, sortOrder, pageIndex, rowCount);

            #region search
            int pCourse = ConvertUtil.ConvertToInt(course);
            int pType = ConvertUtil.ConvertToInt(type);
            int pStatus = ConvertUtil.ConvertToInt(status);
            if (text == Constants.TC_TEXT)
            {
                text = "";
            }

            #endregion
            List<sp_GetClassPlanningResult> list = trainDao.GetList(text, pCourse, pType, pStatus, intructor, Constants.TRAINING_CENTER_CLASS_TYPE_PRO_SKILL);

            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            List<sp_GetClassPlanningResult> finalList = trainDao.Sort(list, sortColumn, sortOrder)
                                 .Skip((currentPage - 1) * rowCount)
                                  .Take(rowCount).ToList<sp_GetClassPlanningResult>();
            int index = 1;
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
                            index++.ToString(),
                            m.ID.ToString(),
                            CommonFunc.Link("lnk"+m.ID,"/TrainingCenterAdmin/ProClassDetail/"+m.ID,m.ClassID,false),
                            HttpUtility.HtmlEncode(m.CourseName),
                            m.SkillTypeName,
                           m.StatusID == Constants.TRAINING_CENTER_COURSE_STATUS_OPEN ? "<div id='row_active' class='row_active'>"+m.StatusName + "</div>" : m.StatusName,
                            (m.Duration.HasValue?m.Duration.Value.ToString():string.Empty)+ " " + Constants.TC_DURATION_PREFIX,                            
                            m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            String.IsNullOrEmpty(m.Instructors)?"":m.Instructors.TrimEnd(';'),    
                            m.Attendess.ToString(),
                            HttpUtility.HtmlEncode(m.Objectives),
                            SetAction(m.ID,m.ClassID,false)
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read)]
        public ActionResult EnglishClass()
        {
            Hashtable hashData = Session[Constants.TRAINING_CENTER_ADMIN_LIST_CLASS] == null ? new Hashtable() : (Hashtable)Session[Constants.TRAINING_CENTER_ADMIN_LIST_CLASS];
            ViewData[Constants.TC_PROFESSIONAL_TEXT] = hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TEXT] == null ? Constants.TC_TEXT : (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TEXT];
            ViewData[Constants.TC_PROFESSIONAL_COURSE] = new SelectList(courseDao.GetList(Constants.TRAINING_CENTER_CLASS_TYPE_ENGLISH), "ID", "Name", ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COURSE]));
            ViewData[Constants.TC_PROFESSIONAL_TYPE] = new SelectList(stDao.GetList(), "ID", "Name", ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TYPE]));
            //string status =(string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_STATUS];
            ViewData[Constants.TC_PROFESSIONAL_STATUS] = new SelectList(trainDao.GetListCourseStatus(), "ID", "Name", Session[Constants.TRAINING_CENTER_ADMIN_LIST_CLASS] == null ? Constants.TRAINING_CENTER_COURSE_STATUS_OPEN.ToString() : (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_STATUS]);
            ViewData[Constants.TC_PROFESSIONAL_INSTRUCTOR] = new SelectList(trainDao.GetListIntructor(), "Value", "Text", (hashData[Constants.TRAINING_CENTER_PROFESSIONAL_INSTRUCTOR] == null ? Constants.FIRST_ITEM_INTRUCTOR : (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_INSTRUCTOR]));

            ViewData[Constants.TC_PROFESSIONAL_COLUMN] = hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COLUMN] == null ? "ID" : hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COLUMN];
            ViewData[Constants.TC_PROFESSIONAL_ORDER] = hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ORDER] == null ? "desc" : hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ORDER];
            ViewData[Constants.TC_PROFESSIONAL_PAGE_INDEX] = hashData[Constants.TRAINING_CENTER_PROFESSIONAL_PAGE_INDEX] == null ? "1" : hashData[Constants.TRAINING_CENTER_PROFESSIONAL_PAGE_INDEX].ToString();
            ViewData[Constants.TC_PROFESSIONAL_ROW_COUNT] = hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ROW] == null ? "20" : hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ROW].ToString();
            return View();
        }

        /// <summary>
        /// Set Session in Class Trainning
        /// </summary>
        /// <param name="text"></param>
        /// <param name="course"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="instructor"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilterListClass(string text, string course, string type, string status, string instructor,
           string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_TEXT, text);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_COURSE, course);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_TYPE, type);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_STATUS, status);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_INSTRUCTOR, instructor);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_COLUMN, column);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_ORDER, order);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.TRAINING_CENTER_PROFESSIONAL_ROW, rowCount);

            Session[Constants.TRAINING_CENTER_ADMIN_LIST_CLASS] = hashData;
        }

        /// <summary>
        /// Return List
        /// </summary>
        /// <param name="text"></param>
        /// <param name="course"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="instructor"></param>
        /// <returns></returns>
        /// 
        [ValidateInput(false)]
        public ActionResult GetListClassJQGrid(string text, string course, string type, string status, string intructor)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            // User login

            SetSessionFilterListClass(text, course, type, status, intructor, sortColumn, sortOrder, pageIndex, rowCount);

            #region search
            int pCourse = ConvertUtil.ConvertToInt(course);
            int pType = ConvertUtil.ConvertToInt(type);
            int pStatus = ConvertUtil.ConvertToInt(status);
            if (text == Constants.TC_TEXT)
            {
                text = "";
            }

            #endregion
            List<sp_GetClassPlanningResult> list = trainDao.GetList(text, pCourse, pType, pStatus, intructor, Constants.TRAINING_CENTER_CLASS_TYPE_ENGLISH);

            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            List<sp_GetClassPlanningResult> finalList = trainDao.Sort(list, sortColumn, sortOrder)
                                 .Skip((currentPage - 1) * rowCount)
                                  .Take(rowCount).ToList<sp_GetClassPlanningResult>();
            int index = 1;
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
                            index++.ToString(),
                            m.ID.ToString(),
                            CommonFunc.Link("lnk"+m.ID,"/TrainingCenterAdmin/EngClassDetail/"+m.ID,m.ClassID,false),
                            HttpUtility.HtmlEncode(m.CourseName),
                            m.StatusID == Constants.TRAINING_CENTER_COURSE_STATUS_OPEN ? "<div id='row_active' class='row_active'>"+m.StatusName + "</div>" : m.StatusName,
                            (m.Duration.HasValue?m.Duration.Value.ToString():string.Empty)+ " " + Constants.TC_DURATION_PREFIX,                            
                            m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            String.IsNullOrEmpty(m.Instructors)?"":m.Instructors.TrimEnd(';'),    
                            m.ClassTime,
                            m.Attendess.ToString(),
                            HttpUtility.HtmlEncode(m.Objectives),
                            SetAction(m.ID,m.ClassID,true)
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string SetAction(int id, string classID, bool isEnglish)
        {
            string action = string.Empty;
            string showPopup = string.Empty;
            if (!isEnglish)
            {
                showPopup = "CRM.popup('/TrainingCenterAdmin/EditClass/" + id + "', 'Edit Professional Class - " + classID + "', '700')";
            }
            else
            {
                showPopup = "CRM.popup('/TrainingCenterAdmin/EditClass/" + id + "', 'Edit English Class - " + classID + "', '700')";
            }
            action += CommonFunc.Button("edit", "Edit", showPopup);
            action += CommonFunc.Button("attendees", "Attendees", "CRM.popup('/TrainingCenterAdmin/UpdateAttendees/" + id + "', 'Update Attendees - " + classID + "', '800')");
            return action;
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Delete)]
        public ActionResult DeleteListEngClass(string id)
        {
            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = classDao.DeleteList(id, principal.UserData.UserName);
            //ShowMessage(msg);            
            //return RedirectToAction("EnglishClass");
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Delete)]
        public ActionResult DeleteListProClass(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = classDao.DeleteList(id, principal.UserData.UserName);
            //ShowMessage(msg);
            //return RedirectToAction("ProClass");
            JsonResult result = new JsonResult();
            return Json(msg);
        }
        public ActionResult Refresh(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Session.Remove(Constants.TRAINING_CENTER_ADMIN_LIST_PROFESSIONAL);
                return RedirectToAction("ProClass");
            }
            else
            {
                Session.Remove(Constants.TRAINING_CENTER_ADMIN_LIST_CLASS);
                return RedirectToAction("EnglishClass");
            }

        }

        /// <summary>
        /// Professional class detail action
        /// </summary>
        /// <param name="id">id of class</param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read)]
        public ActionResult ProClassDetail(string id)
        {
            Training_Class tClass = classDao.GetTrainingClassById(id, Constants.TRAINING_CENTER_CLASS_TYPE_PRO_SKILL);
            if (tClass == null)
                return RedirectToAction("ProClass");

            List<sp_TC_GetListAttendeesOfClassResult> listAtt = taDao.GetListAttendessOfClass(ConvertUtil.ConvertToInt(id));
            ViewData[CommonDataKey.TRAINING_CENTER_ATTEND_LIST] = listAtt;
            ViewData[CommonDataKey.TRAINING_CENTER_COUNT_ATTEND] = listAtt != null ? listAtt.Count : 0;
            string skillType = string.Empty;
            if (tClass.Training_Course.TypeId.HasValue)
            {
                Training_SkillType type = stDao.GetById(tClass.Training_Course.TypeId.Value);
                if (type != null)
                    skillType = type.Name;
            }
            List<int> intList = GetListProClassForNavigation();
            ViewData[CommonDataKey.TRAINING_CENTER_CLASS_LIST] = intList;
            ViewData[CommonDataKey.TRAINING_CENTER_SKILL_TYPE] = skillType;

            return View(tClass);
        }
        
        #region ------------------------------------Course---------------------------------------
        /// <summary>
        /// Courses List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read)]
        public ActionResult Courses(string id)
        {
            try
            {
                int nType = ConvertUtil.ConvertToInt(id);
                if (nType == 0 ||
                    (nType != Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH &&
                    nType != Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL)
                    )
                    return RedirectToAction("Courses", new { id = Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL });
               
                var hash = Session[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE] as Hashtable;
                var filterKeyWord = Constants.TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL;
                if(hash != null && !string.IsNullOrEmpty((string)hash[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_TEXT]))
                    filterKeyWord = (string)hash[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_TEXT];
                ViewData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_TEXT] = filterKeyWord;
                ViewData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_PAGE_INDEX] = hash == null ?
                    1 : hash[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_PAGE_INDEX];
                ViewData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_ROW_COUNT] = hash == null ?
                    20 : hash[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_ROW_COUNT];
                ViewData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_COLLUMN] = hash == null ?
                    "ID" : hash[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_COLLUMN];
                ViewData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_ORDER] = hash == null ?
                    "asc" : hash[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_ORDER];
                string sType = hash == null ? "" : (string)hash[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_TYPE];
                if (nType == Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL)
                    ViewData[CommonDataKey.TRAINING_CENTER_COURSE_TYPE] = new SelectList(stDao.GetList(), "ID", "Name", sType);
                return View();
            }
            catch
            {
                return RedirectToAction("DashBoard");
            }
        }
        /// <summary>
        /// Get list of course type
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public List<ListItem> GetListCourseType()
        {
            List<ListItem> list = new List<ListItem>(){
                new ListItem("Skill Professional", Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL.ToString()),
                new ListItem("English", Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH.ToString())
            };
            return list;
        }
        /// <summary>
        /// GET: create Professional Course
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult CreateProCourse()
        {
            try
            {
                ViewData[CommonDataKey.TRAINING_CENTER_COURSE_TYPE] = new SelectList(stDao.GetList(), "ID", "Name");
                ViewData[CommonDataKey.TRAINING_CENTER_COURSE_STATUS] = new SelectList(trainDao.GetListCourseStatus(), "ID", "Name");
                return View();
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        }
        /// <summary>
        /// GET: Create English Course
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult CreateEnglishCourse()
        {
            try
            {
                ViewData[CommonDataKey.TRAINING_CENTER_COURSE_STATUS] = new SelectList(trainDao.GetListCourseStatus(), "ID", "Name");
                return View();
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        }
        /// <summary>
        /// Set filter session for admin course list
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="sortcolumn"></param>
        /// <param name="sortorder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        [NonAction]
        public void SetSessionFilterCourses(string text, string type, string sortcolumn, string sortorder, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_TEXT, text);
            hashData.Add(CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_TYPE, type);
            hashData.Add(CommonDataKey.TRAINING_ADMIN_SESSION_NAVIGATION_COURSE_TYPE, Request.Params["type"]);
            hashData.Add(CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_COLLUMN, sortcolumn);
            hashData.Add(CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_ORDER, sortorder);
            hashData.Add(CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_PAGE_INDEX, pageIndex);
            hashData.Add(CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_ROW_COUNT, rowCount);
            Session[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE] = hashData;
        }
        /// <summary>
        /// Get course list jqgrid for admin list
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="skilltype"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult GetListCourseAdminJQGrid(string type, string name, string skilltype)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            // User login
            SetSessionFilterCourses(name, skilltype, sortColumn, sortOrder, pageIndex, rowCount);
            #region search
            int? nType = ConvertUtil.ConvertToInt(type);
            if (nType == 0)
                nType = null;
            int? nSkillType = ConvertUtil.ConvertToInt(skilltype);
            if (nSkillType == 0)
                nSkillType = null;
            if (name == Constants.TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL)
                name = null;
            #endregion
            var list = courseDao.GetList(name, nType, nSkillType);

            int totalRecords = list.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            var finalList = courseDao.Sort(list, sortColumn, sortOrder).Skip((currentPage - 1) * rowCount).Take(rowCount).ToList();

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
                            CommonFunc.Link(m.ID.ToString(),"/TrainingCenterAdmin/CoursesDetail/"+m.ID,m.CourseId,false),
                            HttpUtility.HtmlEncode(m.Name),
                            m.TypeName,
                            m.StatusName,
                            m.Duration.HasValue ? m.Duration.Value.ToString() : "",
                            HttpUtility.HtmlEncode(m.Requirements),
                            String.IsNullOrEmpty(m.KeyTrainers)?"":m.KeyTrainers.TrimEnd(';'),
                            CommonFunc.Button("edit", "Edit","CRM.popup('/TrainingCenterAdmin/" + 
                                (m.TypeOfCourse == Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL ? "EditProCourse/" : "EditEnglishCourse/") + 
                                m.ID + "', 'Edit Course " + m.CourseId + "', 700)")
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        private List<int> GetListCourseForNavigation()
        {
            
            List<int> interList = null;
           
            if (Session[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE] != null)
            {
                Hashtable hashData = (Hashtable)Session[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE];
                string text = (string)hashData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_TEXT];
                if (text == Constants.TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL)
                {
                    text = string.Empty;
                }
                string sortColumn = !string.IsNullOrEmpty((string)hashData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_COLLUMN]) ?
                    (string)hashData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_COLLUMN] : "ID";
                string skilltype = !string.IsNullOrEmpty((string)hashData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_TYPE]) ?
                    (string)hashData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_TYPE] : "0";
                string coursetype = !string.IsNullOrEmpty((string)hashData[CommonDataKey.TRAINING_ADMIN_SESSION_NAVIGATION_COURSE_TYPE]) ?
                    (string)hashData[CommonDataKey.TRAINING_ADMIN_SESSION_NAVIGATION_COURSE_TYPE] : "1";
                string sortOrder = !string.IsNullOrEmpty((string)hashData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_ORDER]) ?
                    (string)hashData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_ORDER] : "desc";
                
                int? nSkillType = ConvertUtil.ConvertToInt(skilltype);
                if (nSkillType == 0)
                    nSkillType = null;
                int? nType = ConvertUtil.ConvertToInt(coursetype);
                if (nType == 0)
                    nType = 1;

                var list = courseDao.GetList(text, nType, nSkillType);
                var finalList = courseDao.Sort(list, sortColumn, sortOrder).ToList();
                interList = finalList.Select(p => p.ID).ToList();
            }
            else
            {
                interList = courseDao.GetList("", 1, null).Select(p => p.ID).ToList();
            }

            return interList;
        }
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read)]
        public ActionResult CoursesDetail(string ID)
        {
            try
            {
                Training_Course course = courseDao.GetById(ConvertUtil.ConvertToInt(ID));
                List<int> intList = GetListCourseForNavigation();
                ViewData["ListInter"] = intList;
                ViewData["type"] = course.TypeId;
                if (course == null)
                    return Content(string.Format(Resources.Message.E0005, "Course", "database"));
                Hashtable hashData = Session[SessionKey.PTO_ADMIN_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.PTO_ADMIN_DEFAULT_VALUE];

                ViewData[Constants.TC_COURSE_DETAIL_COLUMN] = hashData[Constants.TC_COURSE_DETAIL_COLUMN] == null ? "Employee" : hashData[Constants.TC_COURSE_DETAIL_COLUMN];
                ViewData[Constants.TC_COURSE_DETAIL_ORDER] = hashData[Constants.TC_COURSE_DETAIL_ORDER] == null ? "asc" : hashData[Constants.TC_COURSE_DETAIL_ORDER];
                ViewData[Constants.TC_COURSE_DETAIL_PAGE_INDEX] = hashData[Constants.TC_COURSE_DETAIL_PAGE_INDEX] == null ? "1" : hashData[Constants.TC_COURSE_DETAIL_PAGE_INDEX].ToString();
                ViewData[Constants.TC_COURSE_DETAIL_ROW_COUNT] = hashData[Constants.TC_COURSE_DETAIL_ROW_COUNT] == null ? "20" : hashData[Constants.TC_COURSE_DETAIL_ROW_COUNT].ToString();

                return View(course);
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
            
        }
        /// <summary>
        /// POST: Create course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Insert, ShowAtCurrentPage = true)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CreateCourse(Training_Course course)
        {
            Message msg = null;
            try
            {
                // TODO: Add insert logic here
                if (course.TypeOfCourse == Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL)
                    course.TypeId = ConvertUtil.ConvertToInt(Request[CommonDataKey.TRAINING_CENTER_COURSE_TYPE]);
                course.StatusId = ConvertUtil.ConvertToInt(Request[CommonDataKey.TRAINING_CENTER_COURSE_STATUS]);
                CheckValidCourse(course, false);
                msg = courseDao.Insert(course);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ArgumentException))
                    msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
                else
                    msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return RedirectToAction("Courses", new { id= course.TypeOfCourse});
        }
        /// <summary>
        /// GET: edit Professional Course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditProCourse(string id)
        {
            try
            {
                Training_Course course = courseDao.GetById(ConvertUtil.ConvertToInt(id));
                if (course == null)
                    return Content(string.Format(Resources.Message.E0005, "Course", "database"));
                ViewData[CommonDataKey.TRAINING_CENTER_COURSE_TYPE] = new SelectList(stDao.GetList(), "ID", "Name", course.TypeId);
                ViewData[CommonDataKey.TRAINING_CENTER_COURSE_STATUS] =
                    new SelectList(trainDao.GetListCourseStatus(), "ID", "Name", course.StatusId);
                return View(course);
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        }
        /// <summary>
        /// GET: Edit English Course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Update, ShowInPopup=true)]
        public ActionResult EditEnglishCourse(string id)
        {
            try
            {
                Training_Course course = courseDao.GetById(ConvertUtil.ConvertToInt(id));
                if (course == null)
                    return Content(string.Format(Resources.Message.E0005, "Course", "database"));
                ViewData[CommonDataKey.TRAINING_CENTER_COURSE_STATUS] =
                    new SelectList(trainDao.GetListCourseStatus(), "ID", "Name", course.StatusId);
                return View(course);
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        }
        /// <summary>
        /// Post: Edit Course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Update, ShowAtCurrentPage = true)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditCourse(Training_Course course)
        {
            
            Message msg = null;
            try
            {
                // TODO: Add insert logic here
                if (course.TypeOfCourse == Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL)
                    course.TypeId = ConvertUtil.ConvertToInt(Request[CommonDataKey.TRAINING_CENTER_COURSE_TYPE]);
                course.StatusId = ConvertUtil.ConvertToInt(Request[CommonDataKey.TRAINING_CENTER_COURSE_STATUS]);
                course.ID = ConvertUtil.ConvertToInt(Request["RealId"]);
                CheckValidCourse(course, true);
                msg = courseDao.Update(course);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ArgumentException))
                    msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
                else
                    msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            if (String.IsNullOrEmpty(Request["page"]))
                return RedirectToAction("Courses", new { id = course.TypeOfCourse });
            else 
                return RedirectToAction("CoursesDetail", new { id = course.ID });
        }
        /// <summary>
        /// Check for valid Course
        /// </summary>
        /// <param name="course"></param>
        /// <param name="isUpdate"></param>
        [NonAction]
        private void CheckValidCourse(Training_Course course, bool isUpdate)
        {
            var objDb = courseDao.GetById(course.CourseId);
            if (objDb != null && course.ID != objDb.ID)
                throw new ArgumentException(string.Format(Resources.Message.E0048, "Course ID \"" + course.CourseId + "\""));
            if (isUpdate)
            {
                var courseDb = courseDao.GetById(course.ID);
                if (courseDb == null)
                    throw new ArgumentException(string.Format(Resources.Message.E0005, "Course", "database"));
                else if (courseDb.UpdateDate.ToString() != course.UpdateDate.ToString())
                    throw new ArgumentException(string.Format(Resources.Message.E0025, "Course"));
            }
        }
        /// <summary>
        /// Check for valid Class
        /// </summary>
        /// <param name="tClass"></param>
        /// <param name="isUpdate"></param>
        [NonAction]
        private void CheckValidClass(Training_Class tClass, bool isUpdate)
        {
            var objDb = classDao.GetById(tClass.ClassId);
            if (objDb != null && tClass.ID != objDb.ID)
                throw new ArgumentException(string.Format(Resources.Message.E0048, "Class ID \"" + tClass.ClassId + "\""));
            if (isUpdate)
            {
                var classDb = classDao.GetById(tClass.ID);
                if (classDb == null)
                    throw new ArgumentException(string.Format(Resources.Message.E0005, "Class", "database"));
                else if (classDb.UpdateDate.ToString() != tClass.UpdateDate.ToString())
                    throw new ArgumentException(string.Format(Resources.Message.E0025, "Class"));
            }
        }
        #endregion
        /// <summary>
        /// GET: Create class
        /// </summary>
        /// <param name="type">Type of courses (Professional Skill or English)</param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Insert)]
        public ActionResult CreateClass(string type)
        {
            try
            {
                ViewData[CommonDataKey.TRAINING_CENTER_REG_STATUS] = new SelectList(trainDao.GetListRegStatus(), "ID", "Name");
                return View();
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        }
        /// <summary>
        /// Post: Create class
        /// </summary>
        /// <param name="tClass"></param>
        /// <returns></returns>
        /// 
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Insert)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CreateClass(Training_Class tClass)
        {
            Message msg = null;
            int typeOfCourse = 0;
            try
            {
                // TODO: Add insert logic here
                tClass.RegStatusId = ConvertUtil.ConvertToInt(Request[CommonDataKey.TRAINING_CENTER_REG_STATUS]);
                CheckValidClass(tClass, false);
                msg = classDao.Insert(tClass);
                typeOfCourse = courseDao.GetById(tClass.CourseId).TypeOfCourse;
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ArgumentException))
                    msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
                else
                    msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return RedirectToAction(typeOfCourse == Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH ?
                "EnglishClass" : "ProClass");
        }
        /// <summary>
        /// GET: edit class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditClass(string id)
        {
            try
            {
                Training_Class tClass = classDao.GetById(ConvertUtil.ConvertToInt(id));
                if (tClass == null)
                    return Content(string.Format(Resources.Message.E0005, "Class", "database"));
                ViewData[CommonDataKey.TRAINING_CENTER_REG_STATUS] = new SelectList(trainDao.GetListRegStatus(), "ID", "Name", tClass.RegStatusId);
                return View(tClass);
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        }
        /// <summary>
        /// GET: edit class
        /// </summary>
        /// <param name="tClass"></param>
        /// <returns></returns>
        /// 
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Update)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditClass(Training_Class tClass)
        {
            Message msg = null;
            int typeOfCourse = 0;
            try
            {
                // TODO: Add insert logic here
                tClass.RegStatusId = ConvertUtil.ConvertToInt(Request[CommonDataKey.TRAINING_CENTER_REG_STATUS]);
                CheckValidClass(tClass, true);
                msg = classDao.Update(tClass);
                typeOfCourse = courseDao.GetById(tClass.CourseId).TypeOfCourse;
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ArgumentException))
                    msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
                else
                    msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            
            if (String.IsNullOrEmpty(Request["page"]))
                return RedirectToAction(typeOfCourse == Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH ?
               "EnglishClass" : "ProClass");
            else
                return RedirectToAction(typeOfCourse == Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH ? "EngClassDetail" : "ProClassDetail", 
                    new { id = tClass.ID});
        }
        /// <summary>
        /// List of course show on popup level 2 in Create Class form
        /// </summary>
        /// <returns></returns>
        /// 
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read)]
        public ActionResult ListCourse()
        {
            ViewData[CommonDataKey.TRAINING_CENTER_COURSE_TYPE] = new SelectList(stDao.GetList(), "ID", "Name");
            return View();
        }
        /// <summary>
        /// GET:Update attendees for class
        /// </summary>
        /// <param name="id">Class ID</param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read, ShowInPopup=true)]
        public ActionResult UpdateAttendees(string id)
        {
            try
            {
                Training_Class tClass = classDao.GetById(ConvertUtil.ConvertToInt(id));
                if (tClass == null)
                    return Content(string.Format(Resources.Message.E0005, "Class", "database"));
                var attendeeList = taDao.GetListAttendessOfClass(tClass.ID);
                ViewData["ResultType"] = tClass.ResultType;
                return View(attendeeList);
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        }
        /// <summary>
        /// POST:Update attendee of Class
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.UpdateAttendee)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateAttendees(FormCollection form)
        {
            Message msg = null;
            Training_Class tClass = null;
            try
            {
                int classId = ConvertUtil.ConvertToInt(Request["classId"]);
                tClass = classDao.GetById(classId);
                if (tClass == null)
                    throw new ArgumentException(string.Format(Resources.Message.E0005, "Class", "database"));
                List<Training_Attendee> attendeelist = new List<Training_Attendee>();
                foreach (var key in form.AllKeys.Where(p => p.Contains("txtId_")))
                {
                    string controlId = key.Split('_')[1];
                    Training_Attendee newAttendee = new Training_Attendee()
                    {
                        ClassId = classId,
                        EmpId = form[key],
                        Remark = form["txtRemark_" + controlId],
                        Result = form["txtScore_" + controlId] ?? form["slScore_" + controlId],
                    };
                    attendeelist.Add(newAttendee);
                }
                msg = classDao.UpdateAttendees(classId, attendeelist);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ArgumentException))
                    msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
                else
                    msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            if (String.IsNullOrEmpty(Request["page"]))
                return RedirectToAction(tClass.Training_Course.TypeOfCourse == Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH ?
                                        "EnglishClass" : "ProClass");
            else
                return RedirectToAction(tClass.Training_Course.TypeOfCourse == Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH ? "EngClassDetail" : "ProClassDetail",
                    new { id = tClass.ID });
        }
        /// <summary>
        /// Check if employees are exist
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="arrId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckEmployeeExists(string empId, string arrId)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var ids = arrId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (ids.Contains(empId))
                result.Data = string.Format(Resources.Message.E0048, empId);
            else
            {
                Employee emp = empDao.GetById(empId, false);
                if (emp == null)
                    result.Data = string.Format(Resources.Message.E0030, empId);
                else
                    result.Data = true;
            }
            return result;
        }
        /// <summary>
        /// Get jqgrid list of courses to show on popup level 2 in Create class popup
        /// </summary>
        /// <param name="name"></param>
        /// <param name="courseType"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// 
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read)]
        [ValidateInput(false)]
        public ActionResult GetListCourseJQGrid(string name, string courseType, string type)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            if (name.Trim().ToLower().Equals(Constants.TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL.ToLower()))
                name = string.Empty;
            int? ntype = ConvertUtil.ConvertToInt(type);
            int? nCourseType = ConvertUtil.ConvertToInt(courseType);
            if (ntype == 0)
                ntype = null;
            if (nCourseType == 0)
                nCourseType = null;
            var list = courseDao.GetList(name, nCourseType, ntype);
            int totalRecords = list.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            //Sort the list
            var finalList = courseDao.Sort(list, sortColumn, sortOrder)
                                  .Skip((pageIndex - 1) * rowCount).Take(rowCount).ToList();

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
                            HttpUtility.HtmlEncode(m.CourseId),
                            CommonFunc.Link(m.ID.ToString(), "javascript:chooseCourse(\"" + HttpUtility.HtmlEncode(m.Name) + "\", " + 
                                HttpUtility.HtmlEncode(m.ID) + ");", HttpUtility.HtmlEncode(m.Name), false),
                            m.TypeName,
                            m.StatusName,
                            CommonFunc.ShowActiveImage(m.Active),               
                            String.IsNullOrEmpty(m.KeyTrainers)?"":m.KeyTrainers.TrimEnd(';')
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// English class detail action
        /// </summary>
        /// <param name="id">id of class</param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read)]
        public ActionResult EngClassDetail(string id)
        {
            Training_Class tClass = classDao.GetTrainingClassById(id, Constants.TRAINING_CENTER_CLASS_TYPE_ENGLISH);

            if (tClass == null)
                return RedirectToAction("EnglishClass");
            
            List<int> intList = GetListEngClassForNavigation();
            ViewData[CommonDataKey.TRAINING_CENTER_CLASS_LIST] = intList;            
            List<sp_TC_GetListAttendeesOfClassResult> listAtt = taDao.GetListAttendessOfClass(ConvertUtil.ConvertToInt(id));
            ViewData[CommonDataKey.TRAINING_CENTER_ATTEND_LIST] = listAtt;
            ViewData[CommonDataKey.TRAINING_CENTER_COUNT_ATTEND] = listAtt != null ? listAtt.Count : 0;
            
            return View(tClass);
        }

        /// <summary>
        /// Get pro class list for navigation
        /// </summary>
        /// <returns></returns>
        private List<int> GetListProClassForNavigation()
        {

            List<int> interList = null;

            if (Session[Constants.TRAINING_CENTER_ADMIN_LIST_PROFESSIONAL] != null)
            {
                Hashtable hashData = (Hashtable)Session[Constants.TRAINING_CENTER_ADMIN_LIST_PROFESSIONAL];
                string text = (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TEXT];
                if (text == Constants.TC_TEXT)
                {
                    text = string.Empty;
                }

                string course = (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COURSE];
                int pCourse = ConvertUtil.ConvertToInt(course);

                string type = (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TYPE];
                int pType = ConvertUtil.ConvertToInt(type);

                string status = (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_STATUS];
                int pStatus = ConvertUtil.ConvertToInt(status);

                string instructor = (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_INSTRUCTOR];

                string sortColumn = !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COLUMN]) ?
                    (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COLUMN] : "ID";
                string sortOrder = !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ORDER]) ?
                    (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ORDER] : "desc";


                var list = trainDao.GetList(text, pCourse, pType, pStatus, instructor, Constants.TRAINING_CENTER_CLASS_TYPE_PRO_SKILL);
                var finalList = trainDao.Sort(list, sortColumn, sortOrder).ToList();
                interList = finalList.Select(p => p.ID).ToList();
            }
            else
            {
                interList = trainDao.GetList("", 0, 0, 0, "", Constants.TRAINING_CENTER_CLASS_TYPE_PRO_SKILL).Select(p => p.ID).ToList();
            }

            return interList;
        }

        /// <summary>
        /// Get english class list for navigation
        /// </summary>
        /// <returns></returns>
        private List<int> GetListEngClassForNavigation()
        {

            List<int> interList = null;

            if (Session[Constants.TRAINING_CENTER_ADMIN_LIST_CLASS] != null)
            {
                Hashtable hashData = (Hashtable)Session[Constants.TRAINING_CENTER_ADMIN_LIST_CLASS];
                string text = (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TEXT];
                if (text == Constants.TC_TEXT)
                {
                    text = string.Empty;
                }
                
                string course = (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COURSE];
                int pCourse = ConvertUtil.ConvertToInt(course);

                string type = (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_TYPE];
                int pType = ConvertUtil.ConvertToInt(type);

                string status = (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_STATUS];
                int pStatus = ConvertUtil.ConvertToInt(status);

                string instructor = (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_INSTRUCTOR];

                string sortColumn = !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COLUMN]) ?
                    (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_COLUMN] : "ID";
                string sortOrder = !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ORDER]) ?
                    (string)hashData[Constants.TRAINING_CENTER_PROFESSIONAL_ORDER] : "desc";

               
                var list = trainDao.GetList(text, pCourse, pType, pStatus, instructor, Constants.TRAINING_CENTER_CLASS_TYPE_ENGLISH);
                var finalList = trainDao.Sort(list, sortColumn, sortOrder).ToList();
                interList = finalList.Select(p => p.ID).ToList();
            }
            else
            {
                interList = trainDao.GetList("", 0, 0, 0, "", Constants.TRAINING_CENTER_CLASS_TYPE_ENGLISH).Select(p => p.ID).ToList();
            }

            return interList;
        }
        #region tooltip
        public ActionResult EmployeeToolTip(string id)
        {
            Employee emp = empDao.GetById(id);
            return View(emp);
        }
        #endregion

        public ActionResult RefreshEnglishCourseAttend()
        {
            Session.Remove(SessionKey.TRAINING_CENTER_ENG_COURSE_ATTEND_FILTER);
            return RedirectToAction("EnglishCourseAttendance");
        }

        public ActionResult RefreshEnglishScoreSheet()
        {
            Session.Remove(SessionKey.TRAINING_CENTER_ENG_SCORE_SHEET_FILTER);
            return RedirectToAction("EnglishScoreSheet");
        }

        public ActionResult RefreshProfessionalCourseAttend()
        {
            Session.Remove(SessionKey.TRAINING_CENTER_PRO_COURSE_ATTEND_FILTER);
            return RedirectToAction("ProfessionalCourseAttendance");
        }

        public ActionResult EnglishCourseAttendance()
        {
            List<Training_Course> listCourse = courseDao.GetList();
            listCourse = listCourse.Where(q => q.TypeOfCourse == Constants.TRAINING_CENTER_CLASS_TYPE_ENGLISH).ToList();
            Hashtable hashData = Session[SessionKey.TRAINING_CENTER_ENG_COURSE_ATTEND_FILTER] == null ? new Hashtable() :
                (Hashtable)Session[SessionKey.TRAINING_CENTER_ENG_COURSE_ATTEND_FILTER];

            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] == null ?
                Constants.TRAINING_EEI_TXT_KEYWORD_LABEL : !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME]) ?
                hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] : Constants.TRAINING_EEI_TXT_KEYWORD_LABEL;

            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName",
                hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT] == null ? Constants.FIRST_ITEM_DEPARTMENT : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT]);
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE] = new SelectList(levelDao.GetList(), "ID", "DisplayName",
                hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE] == null ? Constants.FIRST_ITEM_JOBTITLE : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE]);
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER] = new SelectList(empDao.GetManager(null, 0, 0), "ID", "DisplayName",
                hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER] == null ? Constants.FIRST_ITEM_JOBTITLE : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER]);

            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN] == null ? "ID" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN];
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER] == null ? "desc" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER];
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX] == null ? 1 : ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX]);
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW] == null ? 20 : ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW]);

            ViewData["TextDept"] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT] == null ? "" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT];
            ViewData["TextTitle"] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE] == null ? "" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE];
            ViewData["TextManager"] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER] == null ? "" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER];

            return View(listCourse);
        }

        public ActionResult ProfessionalCourseAttendance()
        {
            List<Training_Course> listCourse = courseDao.GetList();
            listCourse = listCourse.Where(q => q.TypeOfCourse == Constants.TRAINING_CENTER_CLASS_TYPE_PRO_SKILL).ToList();
            Hashtable hashData = Session[SessionKey.TRAINING_CENTER_PRO_COURSE_ATTEND_FILTER] == null ? new Hashtable() :
                (Hashtable)Session[SessionKey.TRAINING_CENTER_PRO_COURSE_ATTEND_FILTER];

            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] == null ?
                Constants.TRAINING_EEI_TXT_KEYWORD_LABEL : !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME]) ? hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] : Constants.FULLNAME_OR_USERID;

            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName",
                hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT] == null ? Constants.FIRST_ITEM_DEPARTMENT : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT]);
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE] = new SelectList(levelDao.GetList(), "ID", "DisplayName",
                hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE] == null ? Constants.FIRST_ITEM_JOBTITLE : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE]);
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER] = new SelectList(empDao.GetManager(null, 0, 0), "ID", "DisplayName",
                hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER] == null ? Constants.FIRST_ITEM_JOBTITLE : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER]);

            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN] == null ? "ID" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN];
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER] == null ? "desc" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER];
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX] == null ? 1 : ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX]);
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW] == null ? 20 : ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW]);

            ViewData["TextDept"] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT] == null ? "" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT];
            ViewData["TextTitle"] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE] == null ? "" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE];
            ViewData["TextManager"] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER] == null ? "" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER];

            return View(listCourse);
        }

        string[] GetCourseOfEmployee(List<Training_Course> listCourse, sp_TC_GetEnglishCourseAttendanceResult employee, int type)
        {
            List<sp_TC_GetCourseEmpAttendResult> list = trainDao.GetCourseEmpAttend(type, employee.ID);
            List<string> str = new List<string>();
            str.Add(employee.ID);
            if (type == Constants.TRAINING_CENTER_CLASS_TYPE_ENGLISH)
            {
                str.Add(CommonFunc.Link(employee.ID, "empTooltip", "#", employee.FullName));
            }
            else
            {
                str.Add(employee.FullName);
            }
            str.Add(employee.TitleName);
            str.Add(employee.DepartmentName);
            str.Add(employee.ManagerName);
            foreach (Training_Course course in listCourse)
            {
                var item = list.Find(p => p.CourseId == course.ID);
                if (item != null)
                {
                    str.Add(CommonFunc.Link(item.ClassId.ToString() + "@" + employee.ID, "#",
                        item.Result != null ?(string.IsNullOrEmpty(item.Result.Trim()) ? "x" : item.Result):"", true));
                }
                else
                {
                    str.Add(string.Empty);
                }
            }
            return str.ToArray();
        }
        /// <summary>
        /// Displayed Tooltip when hover on Attended course
        /// </summary>
        /// <param name="id">Class ID (in the format: [classId]@[EmpId])</param>
        /// <returns></returns>
        public ActionResult EmpClassTooltip(string id)
        {
            try
            {
                var idArr = id.Split('@');
                Training_Class theClass = classDao.GetById(int.Parse(idArr[0]));
                ViewData["result"] = trainDao.GetTrainingResult(theClass.CourseId, idArr[1]);
                return View(theClass);
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// View Training record of employee
        /// </summary>
        /// <param name="userid">employee id</param>
        /// <param name="id">skill type</param>
        /// <returns></returns>
        /// 
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.TrainingRecord)]
        public ActionResult TrainingRecord(string userid, string id)
        {
            Employee emp = null;
            try
            {
                emp = empDao.GetById(userid);
                if (emp == null)
                    return RedirectToAction("Index");
                List<sp_TC_GetClassEmpAttendResult> listEClass = trainDao.GetListClassEmployeeAttend(Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH, ConvertUtil.ConvertToInt(userid));
                List<sp_TC_GetClassEmpAttendResult> listProfClass = trainDao.GetListClassEmployeeAttend(Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL,
                    ConvertUtil.ConvertToInt(userid), ConvertUtil.ConvertToInt(id));
                List<sp_TC_GetClassEmpNotAttendResult> listEClassNotAtt = trainDao.GetListClassEmployeeNotAttend(Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH, ConvertUtil.ConvertToInt(userid));
                List<sp_TC_GetClassEmpNotAttendResult> listProfClassNotAtt = trainDao.GetListClassEmployeeNotAttend(Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL,
                    ConvertUtil.ConvertToInt(userid), ConvertUtil.ConvertToInt(id));
                var examList = trainDao.GetListExam(userid);
                var lastSkillExam = examList.FirstOrDefault(p => p.ScoreComprehensionSkill.HasValue || p.ScoreListeningSkill.HasValue
                    || p.ScoreMultipleChoice.HasValue || p.ScoreSentenceCorrection.HasValue || p.ScoreWritingSkill.HasValue);
                var lastVerbalExam = examList.FirstOrDefault(p => p.ScoreVerbal.HasValue);

                ViewData[Constants.TC_PROFESSIONAL_TYPE] = new SelectList(stDao.GetList(), "ID", "Name", id);
                ViewData[Constants.TRAINING_CENTER_LIST_E_CLASS_ATTEND] = listEClass;
                ViewData[Constants.TRAINING_CENTER_LIST_PROF_CLASS_ATTEND] = listProfClass;
                ViewData[Constants.TRAINING_CENTER_LIST_E_CLASS_NOT_ATTEND] = listEClassNotAtt;
                ViewData[Constants.TRAINING_CENTER_LIST_PROF_CLASS_NOT_ATTEND] = listProfClassNotAtt;
                ViewData[Constants.TRAINING_CENTER_LIST_EXAM_ATTENDANCE] = examList;
                ViewData[CommonDataKey.TRAINING_CENTER_EMP_ID] = userid;
                double? avgScore = null;
                double? cerfiticationScore = null;
                if (lastSkillExam != null)
                {
                    avgScore = CommonFunc.Average(lastSkillExam.ScoreComprehensionSkill, lastSkillExam.ScoreListeningSkill,
                         lastSkillExam.ScoreMultipleChoice, lastSkillExam.ScoreSentenceCorrection, lastSkillExam.ScoreWritingSkill);
                }
                if (avgScore.HasValue)
                {
                    avgScore = (double?)Math.Floor(avgScore.Value + 0.5);
                }
                else
                {
                    avgScore = 0;
                }

                // get the english certification information
                Training_EmpEnglishInfo objScoreToeic = englishInfoDao.GetScoreToeicWithEmployee(userid);
                if (objScoreToeic != null)
                {
                    cerfiticationScore = objScoreToeic.Score;
                }
                else
                {
                    cerfiticationScore = 0;
                }
                // end get current level

                ViewData[CommonDataKey.TRAINING_CENTER_SCORE_SKILL] = avgScore > cerfiticationScore ? avgScore : cerfiticationScore;
                ViewData[CommonDataKey.TRAINING_CENTER_SCORE_VERBAL] =
                    lastVerbalExam != null && lastVerbalExam.VerbalMarkType == Constants.LOT_VERBAL_MARK_TYPE_TOEIC ?
                    lastVerbalExam.ScoreVerbal.Value.ToString() : "0";

                float? verbalLevel = CommonFunc.GetVerbalLevel(lastVerbalExam);
                ViewData[CommonDataKey.TRAINING_CENTER_LEVEL_VERBAL] = verbalLevel != null ? verbalLevel.ToString() : "0";
                string skills = "", verbals = "";
                CommonFunc.SetChartData(examList, ref skills, ref verbals);
                ViewData["Chart_Skills"] = skills;
                ViewData["Chart_Verbals"] = verbals;
                ViewData["UserID"] = userid;

            }
            catch (Exception)
            {
                Message msg = new Message(MessageConstants.I0012, MessageType.Info);
                ShowMessage(msg);
                return RedirectToAction("Index");
                
            }

            return View(emp);
        }

        /// <summary>
        /// Set filter aeguments to session for EMployee list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="department"></param>
        /// <param name="subDepartment"></param>
        /// <param name="titleId"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionEnglishCourseAttendanceFilter(string text, string title, string department, string manager,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME, text);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT, department);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE, title);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER, manager);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN, column);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER, order);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW, rowCount);

            Session[SessionKey.TRAINING_CENTER_ENG_COURSE_ATTEND_FILTER] = hashData;
        }
        private void SetSessionEnglishScoreSheetFilter(string text, string title, string department, string manager,
            string column, string order, int pageIndex, int rowCount)
            {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME, text);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT, department);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE, title);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER, manager);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN, column);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER, order);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW, rowCount);

            Session[SessionKey.TRAINING_CENTER_ENG_SCORE_SHEET_FILTER] = hashData;
        }

        private void SetSessionProfessionalCourseAttendanceFilter(string text, string title, string department, string manager,
           string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME, text);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT, department);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE, title);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER, manager);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN, column);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER, order);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW, rowCount);

            Session[SessionKey.TRAINING_CENTER_PRO_COURSE_ATTEND_FILTER] = hashData;
        }

        public ActionResult ExportEnglishCourseAttend(string id)
        {

            string name = null;
            int iTitle = 0;
            int iDepartment = 0;
            int iManager = 0;
            string sortColumn = "ID";
            string sortDirection = "desc";
            string title = "Professional Course Attendance";
            int ttype = ConvertUtil.ConvertToInt(id);
            if (ttype != Constants.TRAINING_CENTER_CLASS_TYPE_PRO_SKILL)
            {
                title = "English Course Attendance";
                if (Session[SessionKey.TRAINING_CENTER_ENG_COURSE_ATTEND_FILTER] != null)
                {
                    Hashtable hashData = (Hashtable)Session[SessionKey.TRAINING_CENTER_ENG_COURSE_ATTEND_FILTER];
                    if (hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] != null)
                    {
                        if (!string.IsNullOrEmpty((string)hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME]))
                        {
                            if ((string)hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] != Constants.TRAINING_EEI_TXT_KEYWORD_LABEL)
                            {
                                name = (string)hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME];
                            }
                        }
                    }
                    iTitle = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE]);
                    iDepartment = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT]);
                    iManager = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER]);

                    sortColumn = (string)(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN] == null ? "ID" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN]);
                    sortDirection = (string)(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER] == null ? "desc" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER]);
                }
            }
            else
            {
                if (Session[SessionKey.TRAINING_CENTER_PRO_COURSE_ATTEND_FILTER] != null)
                {
                    Hashtable hashData = (Hashtable)Session[SessionKey.TRAINING_CENTER_PRO_COURSE_ATTEND_FILTER];
                    if (hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] != null)
                    {
                        if (!string.IsNullOrEmpty((string)hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME]))
                        {
                            if ((string)hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] != Constants.TRAINING_EEI_TXT_KEYWORD_LABEL)
                            {
                                name = (string)hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME];
                            }
                        }
                    }
                    iTitle = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE]);
                    iDepartment = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT]);
                    iManager = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER]);

                    sortColumn = (string)(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN] == null ? "ID" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN]);
                    sortDirection = (string)(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER] == null ? "desc" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER]);
                }
            }

            List<Training_Course> listCourse = courseDao.GetList();

            listCourse = listCourse.Where(q => q.TypeOfCourse == ttype).ToList();
            List<sp_TC_GetEnglishCourseAttendanceResult> listEng = trainDao.GetListEnglishCourseAttendance(name, iTitle, iDepartment, iManager);
            listEng = trainDao.CourseAttend(listEng, sortColumn, sortDirection);
            var wb = new XLWorkbook();

            var worksheet = wb.Worksheets.Add(title);


            int ROW_BEGIN = 2;
            int COL_BEGIN = 1;
            int col = COL_BEGIN;
            #region Title
            IXLAddress firstAdd = worksheet.Cell(1, 1).Address;
            worksheet.Cell(1, 1).Value = title;
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 15;
            worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.White;
            worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromArgb(0x0066cc);
            #endregion
            worksheet.Cell(ROW_BEGIN, 1).Value = "Emp ID";
            worksheet.Cell(ROW_BEGIN, 2).Value = "Name";
            worksheet.Cell(ROW_BEGIN, 3).Value = "Current Title";
            worksheet.Cell(ROW_BEGIN, 4).Value = "Department";
            worksheet.Cell(ROW_BEGIN, 5).Value = "Direct Manager";
            worksheet.Cell(ROW_BEGIN, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(ROW_BEGIN, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(ROW_BEGIN, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(ROW_BEGIN, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(ROW_BEGIN, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            col = 6;
            foreach (Training_Course header in listCourse)
            {
                worksheet.Cell(ROW_BEGIN, col).Value = header.Name;
                worksheet.Cell(ROW_BEGIN, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                col++;
            }
            worksheet.Range(worksheet.Cell(ROW_BEGIN, COL_BEGIN).Address, worksheet.Cell(ROW_BEGIN, col - 1).Address).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Range(worksheet.Cell(ROW_BEGIN, COL_BEGIN).Address, worksheet.Cell(ROW_BEGIN, col - 1).Address).Style.Font.Bold = true;
            worksheet.Range(worksheet.Cell(ROW_BEGIN, COL_BEGIN).Address, worksheet.Cell(ROW_BEGIN, col - 1).Address).Style.Font.FontSize = 12;
            worksheet.Range(worksheet.Cell(ROW_BEGIN, COL_BEGIN).Address, worksheet.Cell(ROW_BEGIN, col - 1).Address).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

            IXLAddress secondAdd = worksheet.Cell(1, col - 1).Address;
            worksheet.Range(firstAdd, secondAdd).Merge();
            worksheet.Range(firstAdd, secondAdd).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            int rowData = 3;
            foreach (sp_TC_GetEnglishCourseAttendanceResult emp in listEng)
            {
                worksheet.Cell(rowData, 1).Value = emp.ID;
                worksheet.Cell(rowData, 2).Value = emp.FullName;
                worksheet.Cell(rowData, 3).Value = emp.TitleName;
                worksheet.Cell(rowData, 4).Value = emp.DepartmentName;
                worksheet.Cell(rowData, 5).Value = emp.ManagerName;
                worksheet.Cell(rowData, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(rowData, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(rowData, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(rowData, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(rowData, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                List<sp_TC_GetCourseEmpAttendResult> list = trainDao.GetCourseEmpAttend(ttype, emp.ID);
                col = 6;
                foreach (Training_Course course in listCourse)
                {
                    string result = string.Empty;
                    var item = list.Find(p => p.CourseId == course.ID);
                    if (item != null)
                    {

                        result = (item.Result != null ? item.Result : "x");
                    }
                    else
                    {
                        result = string.Empty;
                    }
                    worksheet.Cell(rowData, col).Value = result;
                    worksheet.Cell(rowData, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(rowData, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    col++;
                }

                rowData++;
            }
            string filepath = Server.MapPath("~/Export/") + title + DateTime.Now.Ticks.ToString();
            wb.SaveAs(filepath);
            string filename = title.Replace(" ", "_");

            return new ExcelResult { FileName = filename+".xlsx", Path = filepath };
        }

        public ActionResult GetListEnglishCourseAttend(string text, string title, string department, string manager)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            SetSessionEnglishCourseAttendanceFilter(text, title, department, manager, sortColumn, sortOrder, pageIndex, rowCount);
            string name = null;
            if (!text.Equals(Constants.TRAINING_EEI_TXT_KEYWORD_LABEL))
                name = text;
            int iTitle = 0;
            if (!string.IsNullOrEmpty(title))
                iTitle = ConvertUtil.ConvertToInt(title);
            int iDepartment = 0;
            if (!string.IsNullOrEmpty(department))
                iDepartment = ConvertUtil.ConvertToInt(department);
            int iManager = 0;
            if (!string.IsNullOrEmpty(manager))
                iManager = ConvertUtil.ConvertToInt(manager);
            List<Training_Course> listCourse = courseDao.GetList();
            listCourse = listCourse.Where(q => q.TypeOfCourse == Constants.TRAINING_CENTER_CLASS_TYPE_ENGLISH).ToList();
            List<sp_TC_GetEnglishCourseAttendanceResult> listEng = trainDao.GetListEnglishCourseAttendance(name, iTitle, iDepartment, iManager);
            int totalRecords = listEng.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            var finalList = trainDao.CourseAttend(listEng, sortColumn, sortOrder).Skip((pageIndex - 1) * rowCount).Take(rowCount).ToList();
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
                        cell = GetCourseOfEmployee(listCourse, m, Constants.TRAINING_CENTER_CLASS_TYPE_ENGLISH)
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListProfessionalhCourseAttend(string text, string title, string department, string manager)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            SetSessionProfessionalCourseAttendanceFilter(text, title, department, manager, sortColumn, sortOrder, pageIndex, rowCount);
            string name = null;
            if (!text.Equals(Constants.TRAINING_EEI_TXT_KEYWORD_LABEL))
                name = text;
            int iTitle = 0;
            if (!string.IsNullOrEmpty(title))
                iTitle = ConvertUtil.ConvertToInt(title);
            int iDepartment = 0;
            if (!string.IsNullOrEmpty(department))
                iDepartment = ConvertUtil.ConvertToInt(department);
            int iManager = 0;
            if (!string.IsNullOrEmpty(manager))
                iManager = ConvertUtil.ConvertToInt(manager);
            List<Training_Course> listCourse = courseDao.GetList();
            listCourse = listCourse.Where(q => q.TypeOfCourse == Constants.TRAINING_CENTER_CLASS_TYPE_PRO_SKILL).ToList();
            List<sp_TC_GetEnglishCourseAttendanceResult> listEng = trainDao.GetListEnglishCourseAttendance(name, iTitle, iDepartment, iManager);
            int totalRecords = listEng.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            var finalList = trainDao.CourseAttend(listEng, sortColumn, sortOrder).Skip((pageIndex - 1) * rowCount).Take(rowCount).ToList();
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
                        cell = GetCourseOfEmployee(listCourse, m, Constants.TRAINING_CENTER_CLASS_TYPE_PRO_SKILL)
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EnglishScoreSheet()
        {
            Hashtable hashData = Session[SessionKey.TRAINING_CENTER_ENG_SCORE_SHEET_FILTER] == null ? new Hashtable() :
                (Hashtable)Session[SessionKey.TRAINING_CENTER_ENG_SCORE_SHEET_FILTER];

            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] == null ?
                Constants.TRAINING_EEI_TXT_KEYWORD_LABEL : !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME]) ?
                hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] : Constants.TRAINING_EEI_TXT_KEYWORD_LABEL;

            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName",
                hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT] == null ? Constants.FIRST_ITEM_DEPARTMENT : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT]);
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE] = new SelectList(levelDao.GetList(), "ID", "DisplayName",
                hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE] == null ? Constants.FIRST_ITEM_JOBTITLE : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE]);
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER] = new SelectList(empDao.GetManager(null, 0, 0), "ID", "DisplayName",
                hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER] == null ? Constants.FIRST_ITEM_JOBTITLE : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER]);

            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN] == null ? "ID" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN];
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER] == null ? "desc" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER];
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX] == null ? 1 : ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX]);
            ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW] = hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW] == null ? 20 : ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW]);

            return View();
        }

        public ActionResult GetListEnglishScoreSheet(string text, string title, string department, string manager)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            SetSessionEnglishScoreSheetFilter(text, title, department, manager, sortColumn, sortOrder, pageIndex, rowCount);
            string name = null;
            if (!text.Equals(Constants.TRAINING_EEI_TXT_KEYWORD_LABEL))
                name = text;
            int iTitle = 0;
            if (!string.IsNullOrEmpty(title))
                iTitle = ConvertUtil.ConvertToInt(title);
            int iDepartment = 0;
            if (!string.IsNullOrEmpty(department))
                iDepartment = ConvertUtil.ConvertToInt(department);
            int iManager = 0;
            if (!string.IsNullOrEmpty(manager))
                iManager = ConvertUtil.ConvertToInt(manager);
            List<sp_TC_GetEnglishCourseAttendanceResult> listEng = trainDao.GetListEnglishCourseAttendance(name, iTitle, iDepartment, iManager);
            int totalRecords = listEng.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            var finalList = trainDao.CourseAttend(listEng, sortColumn, sortOrder).Skip((pageIndex - 1) * rowCount).Take(rowCount).ToList();
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
                        cell = GetEnglishScoreSheetEmployee(index++, m)
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        string[] GetEnglishScoreSheetEmployee(int index, sp_TC_GetEnglishCourseAttendanceResult employee)
        {
            var examList = trainDao.GetListExam(employee.ID);
            double? writeToeic = null;
            double? writeScore = null;
            Training_EmpEnglishInfo objScoreToeic = englishInfoDao.GetScoreToeicWithEmployee(employee.ID);
            if (objScoreToeic != null)
            {
                writeToeic = objScoreToeic.Score;
            }
            string writeEmxamDate = string.Empty;
            var lastSkillExam = examList.FirstOrDefault(p => p.ScoreComprehensionSkill.HasValue || p.ScoreListeningSkill.HasValue
                || p.ScoreMultipleChoice.HasValue || p.ScoreSentenceCorrection.HasValue || p.ScoreWritingSkill.HasValue);
            if (lastSkillExam != null)
            {
                double? avgScore = CommonFunc.Average(lastSkillExam.ScoreComprehensionSkill, lastSkillExam.ScoreListeningSkill,
                    lastSkillExam.ScoreMultipleChoice, lastSkillExam.ScoreSentenceCorrection, lastSkillExam.ScoreWritingSkill);
                writeScore = avgScore.HasValue ? Math.Floor(avgScore.Value + 0.5) : 0;
                writeEmxamDate = lastSkillExam.ExamDate.ToString(Constants.DATETIME_FORMAT_VIEW);
            }
            string verbalEmxamDate = string.Empty;
            double verbalCurrent = 0;
            double? writingCurrent = GetCurrentLevel(writeScore, writeToeic);
            string scoreVerbal = string.Empty;
            string scoreVerbalLevel = string.Empty;
            var lastVerbalExam = examList.FirstOrDefault(p => p.ScoreVerbal.HasValue);
            if (lastVerbalExam != null)
            {
                verbalEmxamDate = lastVerbalExam.ExamDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                scoreVerbal = lastVerbalExam.ScoreVerbal.HasValue && lastVerbalExam.VerbalMarkType == Constants.LOT_VERBAL_MARK_TYPE_TOEIC ? 
                    lastVerbalExam.ScoreVerbal.Value.ToString() : string.Empty;
                verbalCurrent = CommonFunc.GetEngLishVerbalevel((int)lastVerbalExam.ScoreVerbal.Value);
                var tmpLevel = CommonFunc.GetVerbalLevel(lastVerbalExam);
                scoreVerbalLevel = lastVerbalExam.ScoreVerbal.HasValue && tmpLevel != null ? tmpLevel.ToString() : string.Empty;
            }
            Training_EnglishTitleMapping objEnglishTitleMapping = levelMappingDao.GetEnglishMappingByTitleId(employee.TitleID);
            double currentMappingWrite = 0;
            double currentMappingToeic = 0;
            if (objEnglishTitleMapping != null)
            {
                currentMappingWrite = objEnglishTitleMapping.Training_SkillMapping.SkillLevel;
                currentMappingToeic = objEnglishTitleMapping.Training_VerbalMapping.VerbalLevel;
            }
            List<string> str = new List<string>();
            str.Add(index.ToString());
            str.Add(employee.ID);
            str.Add(employee.FullName);
            str.Add(employee.TitleName);
            str.Add(employee.DepartmentName);
            str.Add(employee.ManagerName);
            str.Add(writeEmxamDate);
            str.Add(writeScore == null ? "" : writeScore.ToString());
            str.Add(writeToeic == null ? "" : writeToeic.ToString());
            str.Add(writingCurrent == null ? "" : writingCurrent.ToString());
            str.Add(verbalEmxamDate);
            str.Add(scoreVerbal);
            str.Add(scoreVerbalLevel);
            str.Add(currentMappingWrite !=0 ? currentMappingWrite.ToString() : "");
            str.Add(currentMappingToeic !=0 ? currentMappingToeic.ToString() : "");
            str.Add(CompareMappingLevel(writingCurrent == null ? 0 : writingCurrent.Value, verbalCurrent, currentMappingWrite, currentMappingToeic));
            return str.ToArray();
        }

        private double? GetCurrentLevel(double? score, double? toeic)
        {
            if (score == null && toeic == null)
                return null;
            var scoreLevel = score.HasValue ? trainDao.GetEnglishLevel((int)score, false, false) : 0;
            var toeicLevel = toeic.HasValue ? trainDao.GetEnglishLevel((int)toeic, false, true) : 0;
            return scoreLevel > toeicLevel ? scoreLevel : toeicLevel;
        }

        private string CompareMappingLevel(double currentWriting, double currentVerbal, double mappingWriting, double mappingVerbal)
        {
            if (mappingWriting != 0 && mappingVerbal != 0)
                return (currentWriting >= mappingWriting && currentVerbal >= mappingVerbal) ? "Yes" : "No";
            else if (mappingWriting == 0)
                return mappingVerbal == 0 ? "" : (currentVerbal >= mappingVerbal ? "Yes" : "No");
            else
                return mappingWriting == 0 ? "" : (currentWriting >= mappingWriting ? "Yes" : "No");
        }

        public ActionResult ExportEnglishScoreSheet()
        {

            string name = null;
            int iTitle = 0;
            int iDepartment = 0;
            int iManager = 0;
            string sortColumn = "ID";
            string sortDirection = "desc";
            string title = "English Score Sheet";
            if (Session[SessionKey.TRAINING_CENTER_ENG_SCORE_SHEET_FILTER] != null)
            {
                Hashtable hashData = (Hashtable)Session[SessionKey.TRAINING_CENTER_ENG_SCORE_SHEET_FILTER];
                if (hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] != null)
                {
                    if (!string.IsNullOrEmpty((string)hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME]))
                    {
                        if ((string)hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] != Constants.TRAINING_EEI_TXT_KEYWORD_LABEL)
                        {
                            name = (string)hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME];
                        }
                    }
                }
                iTitle = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE]);
                iDepartment = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT]);
                iManager = ConvertUtil.ConvertToInt(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER]);

                sortColumn = (string)(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN] == null ? "ID" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN]);
                sortDirection = (string)(hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER] == null ? "desc" : hashData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER]);
            }
            List<sp_TC_GetEnglishCourseAttendanceResult> listEng = trainDao.GetListEnglishCourseAttendance(name, iTitle, iDepartment, iManager);
            listEng = trainDao.CourseAttend(listEng, sortColumn, sortDirection);
            var wb = new XLWorkbook();
            var worksheet = wb.Worksheets.Add(title);
            int index = 1;
            int ROW_BEGIN = 4;
            int COL_BEGIN = 1;
            int col = COL_BEGIN;
            #region Title
            IXLAddress firstAdd = worksheet.Cell(1, 1).Address;
            worksheet.Cell(1, 1).Value = title;
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 15;
            worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.White;
            worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromArgb(0x0066cc);
            worksheet.Range(worksheet.Cell(1, 1).Address, worksheet.Cell(1, 16).Address).Merge();
            worksheet.Range(worksheet.Cell(1, 1).Address, worksheet.Cell(1, 16).Address).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Range(worksheet.Cell(2, 1).Address, worksheet.Cell(3, 1).Address).Value = "No";
            worksheet.Range(worksheet.Cell(2, 1).Address, worksheet.Cell(3, 1).Address).Merge();
            worksheet.Range(worksheet.Cell(2, 1).Address, worksheet.Cell(3, 1).Address).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.Cell(2, 2).Address, worksheet.Cell(3, 2).Address).Value = "Emp ID";
            worksheet.Range(worksheet.Cell(2, 2).Address, worksheet.Cell(3, 2).Address).Merge();
            worksheet.Range(worksheet.Cell(2, 2).Address, worksheet.Cell(3, 2).Address).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.Cell(2, 3).Address, worksheet.Cell(3, 3).Address).Value = "Name";
            worksheet.Range(worksheet.Cell(2, 3).Address, worksheet.Cell(3, 3).Address).Merge();
            worksheet.Range(worksheet.Cell(2, 3).Address, worksheet.Cell(3, 3).Address).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.Cell(2, 4).Address, worksheet.Cell(3, 4).Address).Value = "Current Title";
            worksheet.Range(worksheet.Cell(2, 4).Address, worksheet.Cell(3, 4).Address).Merge();
            worksheet.Range(worksheet.Cell(2, 4).Address, worksheet.Cell(3, 4).Address).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.Cell(2, 5).Address, worksheet.Cell(3, 5).Address).Value = "Department";
            worksheet.Range(worksheet.Cell(2, 5).Address, worksheet.Cell(3, 5).Address).Merge();
            worksheet.Range(worksheet.Cell(2, 5).Address, worksheet.Cell(3, 5).Address).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.Cell(2, 6).Address, worksheet.Cell(3, 6).Address).Value = "Direct Manager";
            worksheet.Range(worksheet.Cell(2, 6).Address, worksheet.Cell(3, 6).Address).Merge();
            worksheet.Range(worksheet.Cell(2, 6).Address, worksheet.Cell(3, 6).Address).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.Cell(2, 7).Address, worksheet.Cell(2, 10).Address).Value = "Current Writing skill(Entrance)";
            worksheet.Range(worksheet.Cell(2, 7).Address, worksheet.Cell(2, 10).Address).Merge();
            worksheet.Range(worksheet.Cell(2, 7).Address, worksheet.Cell(2, 10).Address).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(3, 7).Value = "Exam Date";
            worksheet.Cell(3, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(3, 8).Value = "Score";
            worksheet.Cell(3, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(3, 9).Value = "TOEIC";
            worksheet.Cell(3, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(3, 10).Value = "Level";
            worksheet.Cell(3, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.Cell(2, 11).Address, worksheet.Cell(2, 13).Address).Value = "Current Verbal skill(Entrance)";
            worksheet.Range(worksheet.Cell(2, 11).Address, worksheet.Cell(2, 13).Address).Merge();
            worksheet.Range(worksheet.Cell(2, 11).Address, worksheet.Cell(2, 13).Address).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(3, 11).Value = "Exam Date";
            worksheet.Cell(3, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(3, 12).Value = "TOEIC";
            worksheet.Cell(3, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(3, 13).Value = "Level";
            worksheet.Cell(3, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.Cell(2, 14).Address, worksheet.Cell(2, 15).Address).Value = "Matching Scale(expected level)";
            worksheet.Range(worksheet.Cell(2, 14).Address, worksheet.Cell(2, 15).Address).Merge();
            worksheet.Range(worksheet.Cell(2, 14).Address, worksheet.Cell(2, 15).Address).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(3, 14).Value = "Writing Level";
            worksheet.Cell(3, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(3, 15).Value = "Verbal Level";
            worksheet.Cell(3, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.Cell(2, 16).Address, worksheet.Cell(3, 16).Address).Value = "Satisfication with Current Title";
            //Style For Header
            worksheet.Range(worksheet.Cell(2, 16).Address, worksheet.Cell(3, 16).Address).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.Cell(2, 16).Address, worksheet.Cell(3, 16).Address).Merge();
            worksheet.Range(worksheet.Cell(2, 1).Address, worksheet.Cell(3, 16).Address).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Range(worksheet.Cell(2, 1).Address, worksheet.Cell(3, 16).Address).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Range(worksheet.Cell(2, 1).Address, worksheet.Cell(3, 16).Address).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Range(worksheet.Cell(2, 1).Address, worksheet.Cell(3, 16).Address).Style.Font.Bold = true;
            worksheet.Range(worksheet.Cell(2, 1).Address, worksheet.Cell(3, 16).Address).Style.Font.FontSize = 12;
            worksheet.Range(worksheet.Cell(2, 1).Address, worksheet.Cell(3, 16).Address).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
            #endregion

            foreach (sp_TC_GetEnglishCourseAttendanceResult employee in listEng)
            {
                var examList = trainDao.GetListExam(employee.ID);
                double? writeToeic = null;
                double? writeScore = null;
                Training_EmpEnglishInfo objScoreToeic = englishInfoDao.GetScoreToeicWithEmployee(employee.ID);
                if (objScoreToeic != null)
                {
                    writeToeic = objScoreToeic.Score;
                }
                string writeEmxamDate = string.Empty;
                var lastSkillExam = examList.FirstOrDefault(p => p.ScoreComprehensionSkill.HasValue || p.ScoreListeningSkill.HasValue
                    || p.ScoreMultipleChoice.HasValue || p.ScoreSentenceCorrection.HasValue || p.ScoreWritingSkill.HasValue);
                if (lastSkillExam != null)
                {
                    double? avgScore = CommonFunc.Average(lastSkillExam.ScoreComprehensionSkill, lastSkillExam.ScoreListeningSkill,
                        lastSkillExam.ScoreMultipleChoice, lastSkillExam.ScoreSentenceCorrection, lastSkillExam.ScoreWritingSkill);
                    writeScore = avgScore.HasValue ? Math.Floor(avgScore.Value + 0.5) : 0;
                    writeEmxamDate = lastSkillExam.ExamDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                }
                string verbalEmxamDate = string.Empty;
                double verbalCurrent = 0;
                double? writingCurrent = GetCurrentLevel(writeScore, writeToeic);
                string scoreVerbal = string.Empty;
                string scoreVerbalLevel = string.Empty;
                var lastVerbalExam = examList.FirstOrDefault(p => p.ScoreVerbal.HasValue);
                if (lastVerbalExam != null)
                {
                    verbalEmxamDate = lastVerbalExam.ExamDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                    scoreVerbal = lastVerbalExam.ScoreVerbal.HasValue && lastVerbalExam.VerbalMarkType == Constants.LOT_VERBAL_MARK_TYPE_TOEIC ?
                        lastVerbalExam.ScoreVerbal.Value.ToString() : string.Empty;
                    verbalCurrent = CommonFunc.GetEngLishVerbalevel((int)lastVerbalExam.ScoreVerbal.Value);
                    var tmpLevel = CommonFunc.GetVerbalLevel(lastVerbalExam);
                    scoreVerbalLevel = lastVerbalExam.ScoreVerbal.HasValue && tmpLevel != null ? tmpLevel.ToString() : string.Empty;
                }
                Training_EnglishTitleMapping objEnglishTitleMapping = levelMappingDao.GetEnglishMappingByTitleId(employee.TitleID);
                double currentMappingWrite = 0;
                double currentMappingToeic = 0;
                if (objEnglishTitleMapping != null)
                {
                    currentMappingWrite = objEnglishTitleMapping.Training_SkillMapping.SkillLevel;
                    currentMappingToeic = objEnglishTitleMapping.Training_VerbalMapping.VerbalLevel;
                }

                worksheet.Cell(ROW_BEGIN, 1).Value = index.ToString();
                worksheet.Cell(ROW_BEGIN, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 2).Value = employee.ID;
                worksheet.Cell(ROW_BEGIN, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 3).Value = employee.FullName;
                worksheet.Cell(ROW_BEGIN, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 4).Value = employee.TitleName;
                worksheet.Cell(ROW_BEGIN, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 5).Value = employee.DepartmentName;
                worksheet.Cell(ROW_BEGIN, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 6).Value = employee.ManagerName;
                worksheet.Cell(ROW_BEGIN, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 7).Value = writeEmxamDate;
                worksheet.Cell(ROW_BEGIN, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 8).Value = writeScore == null ? "" : writeScore.Value.ToString();
                worksheet.Cell(ROW_BEGIN, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 9).Value = writeToeic == null ? "" : writeToeic.Value.ToString();
                worksheet.Cell(ROW_BEGIN, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 10).Value = writingCurrent == null ? "" : writingCurrent.Value.ToString();
                worksheet.Cell(ROW_BEGIN, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 11).Value = verbalEmxamDate;
                worksheet.Cell(ROW_BEGIN, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 12).Value = scoreVerbal;
                worksheet.Cell(ROW_BEGIN, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 13).Value = scoreVerbalLevel;
                worksheet.Cell(ROW_BEGIN, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 14).Value = currentMappingWrite != 0 ? currentMappingWrite.ToString() : "";
                worksheet.Cell(ROW_BEGIN, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 15).Value = currentMappingToeic != 0 ? currentMappingToeic.ToString() : "";
                worksheet.Cell(ROW_BEGIN, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(ROW_BEGIN, 16).Value = CompareMappingLevel(writingCurrent == null ? 0 : writingCurrent.Value, verbalCurrent, currentMappingWrite, currentMappingToeic);
                worksheet.Cell(ROW_BEGIN, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(ROW_BEGIN, 7).Address, worksheet.Cell(ROW_BEGIN, 16).Address).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ROW_BEGIN++;
            }
            string filepath = Server.MapPath("~/Export/") + title + DateTime.Now.Ticks.ToString();
            wb.SaveAs(filepath);
            string filename = title.Replace(" ", "_") + ".xlsx";

            return new ExcelResult { FileName = filename, Path = filepath };
        }
        /// <summary>
        /// English level mapping
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read)]
        public ActionResult EnglishLevelMapping()
        {
            return View();
        }
        /// <summary>
        /// Training Center DashBoard
        /// </summary>
        /// <returns></returns>
        /// 
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            return RedirectToAction("DashBoard");
        }
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read)]
        public ActionResult DashBoard()
        {
            return View();
        }
       /// <summary>
       /// Refresh filtersession of courses
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read, ShowAtCurrentPage=true)]
        public ActionResult RefreshCourseList(string id)
        {
            Session.Remove(CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE);
            return RedirectToAction("Courses", new { id = id});
        }
        /// <summary>
        /// Delete courses
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Delete, ShowAtCurrentPage=true)]
        public ActionResult DeleteCourseList(string id)
        {
            int typeofCourse = 0;
            Message msg = null;
            try
            {
                string[] ids = id.Split(new char[]{Constants.SEPARATE_IDS_CHAR}, StringSplitOptions.RemoveEmptyEntries);
                CheckDeleteCourses(ids);
                msg = courseDao.DeleteList(ids, ref typeofCourse);
            }
            catch(Exception ex)
            {
                if (ex.GetType() == typeof(ArgumentException))
                    msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
                else
                    msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            //ShowMessage(msg);
            //return RedirectToAction("Courses", new { id = typeofCourse });
            JsonResult result = new JsonResult();
            return Json(msg);
        }
        /// <summary>
        /// Check if courses can be deleted
        /// </summary>
        /// <param name="ids"></param>
        public void CheckDeleteCourses(string[] ids)
        {
            foreach(string id in ids)
            {
                var course = courseDao.GetById(ConvertUtil.ConvertToInt(id));
                if (course.Training_Classes.Where(p => !p.DeleteFlag).Count() > 0)
                {
                    throw new ArgumentException(string.Format(Resources.Message.E0046, "delete course \"" +
                        course.CourseId + "\"", "there're some classes in this course"));
                }
            }
        }
        /// <summary>
        /// Export Course List
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="skilltype"></param>
        /// <param name="sortOrder"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Export, ShowAtCurrentPage=true)]
        public ActionResult ExportCourseList(string type, string name, string skilltype, string sortOrder, string sortColumn)
        {
            #region search
            int? nType = ConvertUtil.ConvertToInt(type);
            if (nType == 0)
                nType = null;
            int? nSkillType = ConvertUtil.ConvertToInt(skilltype);
            if (nSkillType == 0)
                nSkillType = null;
            if (name == Constants.TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL)
                name = null;
            #endregion
            var list = courseDao.GetList(name, nType, nSkillType);
            var finalList = courseDao.Sort(list, sortColumn, sortOrder);
            string title = string.Empty;
            string[] column_it;
            string[] header_it;
            if (nType == Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH)
            {
                title = "English Courses";
                column_it = new string[] { "CourseId:text", "Name:text", "StatusName", "Duration:duration", "Requirements", "KeyTrainers"};
                header_it = new string[] { "ID", "Name", "Status", "Duration", "Requirements", "KeyTrainers" };
            }
            else
            {
                title = "Professional Skill Courses";
                column_it = new string[] { "CourseId:text", "Name:text", "TypeName","StatusName", "Duration:duration", "Requirements:text", "KeyTrainers:text" };
                header_it = new string[] { "ID", "Name", "Type","Status", "Duration", "Requirements", "KeyTrainers" };
            }
            ExportExcelAdvance exp = new ExportExcelAdvance();
            exp.Sheets = new List<CExcelSheet>{ new CExcelSheet{ Name=title, 
                                                    List= finalList,
                                                    Header = header_it, 
                                                    ColumnList= column_it,
                                                    Title = title,                
                                                    IsRenderNo = true}};
            title = title.Replace(" ", "_");
            string filepath = Server.MapPath("~/Export/") + title + DateTime.Now.Ticks.ToString() + ".xlsx";
            string filename = exp.ExportExcelMultiSheet(filepath);
            filename = title + ".xlsx";

            return new ExcelResult { FileName = filename, Path = filepath };
        }

        [CrmAuthorizeAttribute(Module = Modules.TrainingCenter, Rights = Permissions.Read)]
        public ActionResult GetSubListJQGrid(string courseId, string type)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(courseId, type, sortColumn, sortOrder, pageIndex, rowCount);

            int? nType = ConvertUtil.ConvertToInt(type);
            int? nCourseId = ConvertUtil.ConvertToInt(courseId);
            
            var list = materialDao.GetList(null, "", nType , nCourseId, "", null);
            int totalRecords = list.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            //Sort the list
            var finalList = list.Skip((pageIndex - 1) * rowCount).Take(rowCount).ToList();

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
                            m.Description != null ? CommonFunc.Link(m.ID.ToString(), "courseTooltip", "#", (m.Description.Length > 100 ?
                                HttpUtility.HtmlEncode(m.Description.Substring(0,100)) + "...": HttpUtility.HtmlEncode(m.Description))): "",                            
                            SetActionCourseDetail(m.ID)
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string SetActionCourseDetail(int id)
        {
            string action = string.Empty;
            action += CommonFunc.Button("download_cv", "Download", "CRM.downloadMaterial(" + id + ")");
            action += CommonFunc.Button("edit", "Edit", "showPopup('/TrainingMaterial/Edit/" + id +
                        "', 'Edit Material', 700)");            
            return action;
        }

        private void SetSessionFilter(string courseId, string type,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.TC_COURSE_DETAIL_COURSE, courseId);
            hashData.Add(Constants.TC_COURSE_DETAIL_TYPE, type);

            hashData.Add(Constants.TC_COURSE_DETAIL_COLUMN, column);
            hashData.Add(Constants.TC_COURSE_DETAIL_ORDER, order);
            hashData.Add(Constants.TC_COURSE_DETAIL_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.TC_COURSE_DETAIL_ROW_COUNT, rowCount);

            Session[SessionKey.PTO_ADMIN_DEFAULT_VALUE] = hashData;
        }

        public ActionResult CourseDetailTooltip(int id)
        {
            Training_Material pr = materialDao.GetByID(id);
            //string result = Server.HtmlEncode(pr.Justification);
            //return result.Replace("\\r\\n", "<br/>");
            return View(pr);
        }
        /// <summary>
        /// Displayed Tooltip when hover on emp name
        /// </summary>
        /// <param name="id">[EmpId])</param>
        /// <returns></returns>
        public ActionResult EmpEnTooltip(string id)
        {
            try
            {
                var examList = trainDao.GetListExam(id);
                var lastSkillExam = examList.FirstOrDefault(p => p.ScoreComprehensionSkill.HasValue || p.ScoreListeningSkill.HasValue
                    || p.ScoreMultipleChoice.HasValue || p.ScoreSentenceCorrection.HasValue || p.ScoreWritingSkill.HasValue);
                var lastVerbalExam = examList.FirstOrDefault(p => p.ScoreVerbal.HasValue);

                double? avgScore = null;
                if (lastSkillExam != null)
                {
                    avgScore = CommonFunc.Average(lastSkillExam.ScoreComprehensionSkill, lastSkillExam.ScoreListeningSkill,
                         lastSkillExam.ScoreMultipleChoice, lastSkillExam.ScoreSentenceCorrection, lastSkillExam.ScoreWritingSkill);
                }
                ViewData[CommonDataKey.TRAINING_CENTER_SCORE_SKILL] = avgScore.HasValue ? (double?)Math.Floor(avgScore.Value + 0.5) : 0;
                ViewData[CommonDataKey.TRAINING_CENTER_SCORE_VERBAL] =
                    lastVerbalExam != null && lastVerbalExam.VerbalMarkType == Constants.LOT_VERBAL_MARK_TYPE_TOEIC ?
                    lastVerbalExam.ScoreVerbal.Value.ToString() : "0";

                float? verbalLevel = CommonFunc.GetVerbalLevel(lastVerbalExam);
                ViewData[CommonDataKey.TRAINING_CENTER_LEVEL_VERBAL] = verbalLevel != null ? verbalLevel.ToString() : "0";
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
