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

namespace CRM.Controllers
{
    public class JobTitleController : BaseController
    {
        private JobTitleDao jobTitleDao = new JobTitleDao();
        private DepartmentDao depDao = new DepartmentDao();
        //
        // GET: /JobTitle/
        [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Read)]
        public ActionResult Index()
        {            

            Hashtable hashData = Session[SessionKey.JOB_TITLE_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.JOB_TITLE_DEFAULT_VALUE];

            ViewData[Constants.JOB_TITLE_NAME] = hashData[Constants.JOB_TITLE_NAME] == null ? Constants.JOB_TITLE : !string.IsNullOrEmpty((string)hashData[Constants.JOB_TITLE_NAME]) ? hashData[Constants.JOB_TITLE_NAME] : Constants.JOB_TITLE;
            ViewData[Constants.JOB_TITLE_DEPARTMENT] = new SelectList(depDao.GetList(), "DepartmentId", "DepartmentName", hashData[Constants.JOB_TITLE_DEPARTMENT] == null ? Constants.FIRST_ITEM_DEPARTMENT : hashData[Constants.JOB_TITLE_DEPARTMENT]);

            ViewData[Constants.JOB_TITLE_COLUMN] = hashData[Constants.JOB_TITLE_COLUMN] == null ? "TitleName" : hashData[Constants.JOB_TITLE_COLUMN];
            ViewData[Constants.JOB_TITLE_ORDER] = hashData[Constants.JOB_TITLE_ORDER] == null ? "asc" : hashData[Constants.JOB_TITLE_ORDER];
            ViewData[Constants.JOB_TITLE_PAGE_INDEX] = hashData[Constants.JOB_TITLE_PAGE_INDEX] == null ? "1" : hashData[Constants.JOB_TITLE_PAGE_INDEX].ToString();
            ViewData[Constants.JOB_TITLE_ROW_COUNT] = hashData[Constants.JOB_TITLE_ROW_COUNT] == null ? "20" : hashData[Constants.JOB_TITLE_ROW_COUNT].ToString();
            
            return View();
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.JOB_TITLE_DEFAULT_VALUE);
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
        [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Read)]
        [ValidateInput(false)]
        public ActionResult GetListJQGrid(string text, string departmentId)
        {
            #region JQGrid Params

            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

            #endregion

            SetSessionFilter(text, departmentId, sortColumn, sortOrder, pageIndex, rowCount);

            List<JobTitle> list = jobTitleDao.GetList();

            //filter
            if (!string.IsNullOrEmpty(text.Trim()) && text != Constants.JOB_TITLE)
            {
                list = list.Where(c => c.JobTitleName.ToLower().Contains(text.Trim().ToLower())).ToList<JobTitle>();
            }
            if (!string.IsNullOrEmpty(departmentId))
            {
                list = list.Where(c => c.DepartmentId.ToString().Equals(departmentId)).ToList<JobTitle>();
            }

            //for paging
            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            var finalList = jobTitleDao.Sort(list, sortColumn, sortOrder)
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
                        i = m.JobTitleId,
                        cell = new string[] {
                            m.JobTitleId.ToString(),
                            HttpUtility.HtmlEncode(m.JobTitleName),
                            HttpUtility.HtmlEncode(m.Department.DepartmentName),   
                            HttpUtility.HtmlEncode(m.Description),
                            m.IsManager.ToString(),
                            "<input type=\"button\" class=\"icon edit\" title=\"Edit\" onclick=\"CRM.popup('/JobTitle/Edit/" + m.JobTitleId.ToString() + "', 'Update', 400)\" />"                            
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /JobTitle/Create
        [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Insert)]
        public ActionResult Create()
        {
            ViewData[CommonDataKey.JTL_DEPARTMENT] = new SelectList(depDao.GetList(), "DepartmentId", "DepartmentName");
            return View();
        }

        //
        // POST: /JobTitle/Create

        [HttpPost]
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Insert)]
        public ActionResult Create(JobTitle jobTitle)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            jobTitle.CreateDate = DateTime.Now;
            jobTitle.CreatedBy = principal.UserData.UserName;
            jobTitle.UpdateDate = DateTime.Now;
            jobTitle.UpdatedBy = principal.UserData.UserName;

            Message msg = jobTitleDao.Insert(jobTitle);            
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        //
        // GET: /JobTitle/Edit/5
        [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Update)]
        public ActionResult Edit(int id)
        {
            JobTitle jobTitle = jobTitleDao.GetByID(id);
            ViewData[CommonDataKey.JTL_DEPARTMENT] = new SelectList(depDao.GetList(), "DepartmentId", "DepartmentName", jobTitle.DepartmentId);
            return View(jobTitle);
        }

        //
        // POST: /JobTitle/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Update)]
        public ActionResult Edit(JobTitle jobTitle)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            jobTitle.UpdatedBy = principal.UserData.UserName;
            Message msg = jobTitleDao.Update(jobTitle);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete List
        /// </summary>
        /// <param name="id">ids</param>
        /// <returns></returns     
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Delete)]
        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = jobTitleDao.DeleteList(id, principal.UserData.UserName);
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        /// <summary>
        /// Set Session to Filter
        /// </summary>
        /// <param name="jobTitleName"></param>
        /// <param name="department"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilter(string jobTitleName, string department,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.JOB_TITLE_NAME, jobTitleName);
            hashData.Add(Constants.JOB_TITLE_DEPARTMENT, department);
            hashData.Add(Constants.JOB_TITLE_COLUMN, column);
            hashData.Add(Constants.JOB_TITLE_ORDER, order);
            hashData.Add(Constants.JOB_TITLE_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.JOB_TITLE_ROW_COUNT, rowCount);

            Session[SessionKey.JOB_TITLE_DEFAULT_VALUE] = hashData;
        }

    }
}
