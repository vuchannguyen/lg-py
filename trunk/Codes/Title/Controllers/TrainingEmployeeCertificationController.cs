using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Attributes;
using CRM.Models;
using CRM.Library.Common;
using System.Web.UI.WebControls;

namespace CRM.Controllers
{
    public class TrainingEmployeeCertificationController : BaseController
    {
        
        TrainingEmployeeCertificationDao trEmpCerDao = new TrainingEmployeeCertificationDao();
        private JobTitleDao titleDao = new JobTitleDao();
        private JobTitleLevelDao levelDao = new JobTitleLevelDao();

        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.TRAINING_EMPLOYEE_CERTIFICATION_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.TRAINING_EMPLOYEE_CERTIFICATION_DEFAULT_VALUE];
            ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_NAME] = hashData[Constants.ASSET_LIST_NAME] == null ? Constants.TRAINING_EMPLOYEE_CERTIFICATION_SEARCH_NAME : !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_NAME]) ? hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_NAME] : Constants.TRAINING_EMPLOYEE_CERTIFICATION_SEARCH_NAME;
            ViewData[Constants.EMPLOYEE_LIST_JOB_TITLE] = new SelectList(levelDao.GetList(), "DisplayName", "DisplayName", hashData[Constants.EMPLOYEE_LIST_JOB_TITLE] == null ? Constants.FIRST_ITEM_JOBTITLE : hashData[Constants.EMPLOYEE_LIST_JOB_TITLE]);
            ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_MANAGER] = new SelectList(trEmpCerDao.GetEmployeeCertificationManagerList(), "ManagerId", "ManagerName", hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_MANAGER] == null ? string.Empty : (string)hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_MANAGER]);
            ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_CERTIFICATION] = new SelectList(trEmpCerDao.GetTrainingCertificationList(), "ID", "Name", hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_CERTIFICATION] == null ? string.Empty : (string)hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_CERTIFICATION]);

            ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_COLUMN] = hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_COLUMN] == null ? "ID" : hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_COLUMN];
            ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_ORDER] = hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_ORDER] == null ? "desc" : hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_ORDER];
            ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_PAGE_INDEX] = hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_PAGE_INDEX] == null ? "1" : hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_PAGE_INDEX].ToString();
            ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_ROW_COUNT] = hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_ROW_COUNT] == null ? "20" : hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_ROW_COUNT].ToString();
            return View();
        }

        public ActionResult GetListJQGrid(string searchName, string jobTitle, string manager, string certification)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            SetSessionFilter(searchName, jobTitle, manager, certification, sortColumn, sortOrder, pageIndex, rowCount);
            #region search
            string userName = null;
            string jobTitleId = null;
            string managerId = null;
            int certificationId = 0;
            if (searchName != Constants.TRAINING_EMPLOYEE_CERTIFICATION_SEARCH_NAME)
            {
                userName = searchName;
            }
            if (!string.IsNullOrEmpty(jobTitle))
            {
                jobTitleId = jobTitle.ToString();
            }
            if (!string.IsNullOrEmpty(manager))
            {
                managerId = manager;
            }
            if (!string.IsNullOrEmpty(certification))
            {
                certificationId = int.Parse(certification);
            }
            #endregion
           
            List<sp_GetEmployeeCertificationResult> trainingEmpCerlist = new TrainingEmployeeCertificationDao().GetTrainingEmployeeCertificationList(userName, jobTitleId, managerId, certificationId).ToList();

            int totalRecords = trainingEmpCerlist.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            List<sp_GetEmployeeCertificationResult> finalList = trEmpCerDao.Sort(trainingEmpCerlist, sortColumn, sortOrder).Skip((currentPage - 1) * rowCount)
                                   .Take(rowCount).ToList<sp_GetEmployeeCertificationResult>();

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
                             HttpUtility.HtmlEncode(m.ID.ToString()),
                             HttpUtility.HtmlEncode(m.ID.ToString()),
                             HttpUtility.HtmlEncode(m.DisplayName),
                             HttpUtility.HtmlEncode(m.TitleName),
                             HttpUtility.HtmlEncode(m.ManagerName),
                             HttpUtility.HtmlEncode(m.Name),
                             HttpUtility.HtmlEncode(m.Remark),
                             //CommonFunc.Link(m.ID.ToString(),"javascript:CRM.popup('/TrainingEmployeeCertification/Edit/" + m.ID.ToString() + "', 'Edit " + m.Name +" ',470);", m.ID.ToString(),true),
                            // CommonFunc.Button("edit", "Edit", "javascript:CRM.popup('/TrainingEmployeeCertification/Edit/" + m.ID.ToString() + "', 'Edit " + m.Name +" ',470);" + m.ID.ToString() + "'")
                            "<input type=\"button\" class=\"icon edit\" title=\"Edit\" onclick=\"CRM.popup('/TrainingEmployeeCertification/Edit/" + m.EmployeeCertificationId + "', 'Update', 400)\" />"
                         }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private void SetSessionFilter(string searchName, string jobTitle, string manager, string certification, string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_NAME, searchName);
            hashData.Add(Constants.EMPLOYEE_LIST_JOB_TITLE, jobTitle);
            hashData.Add(Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_MANAGER, manager);
            hashData.Add(Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_CERTIFICATION, certification);

            hashData.Add(Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_COLUMN, column);
            hashData.Add(Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_ORDER, order);
            hashData.Add(Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_ROW_COUNT, rowCount);

            Session[SessionKey.TRAINING_EMPLOYEE_CERTIFICATION_DEFAULT_VALUE] = hashData;
        }

        public ActionResult ExportToExcel(string active)
        {
            string searchName = null;
            string columnName = "ID";
            string order = "desc";
            string jobTitleId = null;
            string managerId = null;
            int certificationId = 0;
            var grid = new GridView();
            int isActive = int.Parse(active);
            List<sp_GetEmployeeCertificationResult> trainingCerList = new List<sp_GetEmployeeCertificationResult>();
            if (Session[SessionKey.TRAINING_EMPLOYEE_CERTIFICATION_DEFAULT_VALUE] != null)
            {
                Hashtable hashData = (Hashtable)Session[SessionKey.TRAINING_EMPLOYEE_CERTIFICATION_DEFAULT_VALUE];
                searchName = (string)hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_NAME];
                jobTitleId = (string)hashData[Constants.EMPLOYEE_LIST_JOB_TITLE];
                //managerId = hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_MANAGER] == null ? string.Empty : (string)hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_MANAGER];
                managerId = (string)hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_MANAGER];
                if (string.IsNullOrEmpty(managerId))
                {
                    managerId = null;
                }
                certificationId = !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_CERTIFICATION]) ? int.Parse((string)hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_CERTIFICATION]) : 0;
                if (searchName == Constants.TRAINING_EMPLOYEE_CERTIFICATION_SEARCH_NAME)
                {
                    searchName = null;
                }
               
                columnName = !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_COLUMN]) ? (string)hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_COLUMN] : "ID";
                order = !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_ORDER]) ? (string)hashData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_ORDER] : "desc";
            }
            trainingCerList = trEmpCerDao.GetTrainingEmployeeCertificationList(searchName, jobTitleId, managerId, certificationId);
            trainingCerList = trEmpCerDao.Sort(trainingCerList, columnName, order);
            ExportExcel exp = new ExportExcel();
            string[] column = new string[] { "ID:text", "DisplayName:text-left", "TitleName:text-left", "ManagerName:text-left", "Name:text-left", "Remark:text-left" };
            string[] header = new string[] { "ID", "Name", "JobTitle", "DirectManager", "CertificationName", "Remark"};

            exp.Title = Constants.TRAINING_EMPLOYEE_CERTIFICATIONTITLE_EXPORT_EXCEL;
            exp.FileName = Constants.TRAINING_EMPLOYEE_CERTIFICATION_EXCEL_NAME;
            exp.ColumnList = column;
            exp.HeaderExcel = header;

            exp.List = trainingCerList;
            exp.IsRenderNo = true;
            exp.Execute();
            return View();
        }

        public ActionResult Create()
        {
            Hashtable hashData = Session[SessionKey.TRAINING_EMPLOYEE_CERTIFICATION_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.TRAINING_EMPLOYEE_CERTIFICATION_DEFAULT_VALUE];

            return View();
        }


        [HttpPost]
        //[CrmAuthorizeAttribute(Module = Modules., Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create(Employee_Certification data)
        {

            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = trEmpCerDao.Insert(data);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            Employee_Certification trEmpCer = trEmpCerDao.GetById(id);
            Hashtable hashData = Session[SessionKey.TRAINING_EMPLOYEE_CERTIFICATION_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.TRAINING_EMPLOYEE_CERTIFICATION_DEFAULT_VALUE];

            return View(trEmpCer);
        }

        [HttpPost]
        //[CrmAuthorizeAttribute(Module = Modules., Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Edit(Employee_Certification data)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
           // data.UpdatedBy = principal.UserData.UserName;
            Message msg = trEmpCerDao.UpdateTrainingEmployeeCertification(data);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = trEmpCerDao.DeleteList(id);
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        public ActionResult Refresh(string id)
        {
            string view = string.Empty;
            switch (id)
            {
                case "2":
                    Session.Remove(SessionKey.TRAINING_EMPLOYEE_CERTIFICATION_DEFAULT_VALUE);
                    view = "Index";
                    break;
                default:
                    Session.Remove(SessionKey.TRAINING_EMPLOYEE_CERTIFICATION_DEFAULT_VALUE);
                    view = "Index";
                    break;
            }
            return RedirectToAction(view);
        }

    }
}
