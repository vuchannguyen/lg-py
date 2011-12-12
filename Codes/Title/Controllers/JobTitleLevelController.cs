using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Attributes;
using System.Collections;

namespace CRM.Controllers
{
    public class JobTitleLevelController : BaseController
    {
        private JobTitleLevelDao jobTitleLevelDao = new JobTitleLevelDao();
        private JobTitleDao jobTitleDao = new JobTitleDao();
        private DepartmentDao depDao = new DepartmentDao();

        [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.JOB_TITLE_LEVEL_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.JOB_TITLE_LEVEL_DEFAULT_VALUE];

            ViewData[Constants.JOB_TITLE_LEVEL_NAME] = hashData[Constants.JOB_TITLE_LEVEL_NAME] == null ? Constants.JOB_TITLE_lEVEL : !string.IsNullOrEmpty((string)hashData[Constants.JOB_TITLE_LEVEL_NAME]) ? hashData[Constants.JOB_TITLE_LEVEL_NAME] : Constants.JOB_TITLE_lEVEL;
            ViewData[Constants.JOB_TITLE_LEVEL_SELECTION] = new SelectList(jobTitleDao.GetList(), "JobTitleId", "JobTitleName", hashData[Constants.JOB_TITLE_LEVEL_SELECTION] == null ? Constants.FIRST_ITEM_JOBTITLE : hashData[Constants.JOB_TITLE_LEVEL_SELECTION]);

            ViewData[Constants.JOB_TITLE_LEVEL_COLUMN] = hashData[Constants.JOB_TITLE_LEVEL_COLUMN] == null ? "TitleName" : hashData[Constants.JOB_TITLE_LEVEL_COLUMN];
            ViewData[Constants.JOB_TITLE_LEVEL_ORDER] = hashData[Constants.JOB_TITLE_LEVEL_ORDER] == null ? "asc" : hashData[Constants.JOB_TITLE_LEVEL_ORDER];
            ViewData[Constants.JOB_TITLE_LEVEL_PAGE_INDEX] = hashData[Constants.JOB_TITLE_LEVEL_PAGE_INDEX] == null ? "1" : hashData[Constants.JOB_TITLE_LEVEL_PAGE_INDEX].ToString();
            ViewData[Constants.JOB_TITLE_LEVEL_ROW_COUNT] = hashData[Constants.JOB_TITLE_LEVEL_ROW_COUNT] == null ? "20" : hashData[Constants.JOB_TITLE_LEVEL_ROW_COUNT].ToString();
            
            return View();
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.JOB_TITLE_LEVEL_DEFAULT_VALUE);
            return RedirectToAction("Index");
        }

        [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Read)]
        public ActionResult GetListJQGrid(string text, string jobTitleID)
        {
            #region JQGrid Params

            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

            #endregion

            SetSessionFilter(text, jobTitleID, sortColumn, sortOrder, pageIndex, rowCount);

            int jobTitle = 0;
            if (text == Constants.JOB_TITLE_lEVEL)
            {
                text = string.Empty;
            }
            if (!string.IsNullOrEmpty(jobTitleID))
            {
                jobTitle = int.Parse(jobTitleID);
            }

            List<sp_GetJobTitleLevelResult> list = jobTitleLevelDao.GetListFilter(text, jobTitle);

            //for paging
            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            List<sp_GetJobTitleLevelResult> finalList = jobTitleLevelDao.Sort(list, sortColumn, sortOrder).ToList<sp_GetJobTitleLevelResult>();
            TempData[CommonDataKey.JTL_JOBTITLE_LEVEL_LIST] = finalList;
            finalList= finalList.Skip((pageIndex - 1) * rowCount).Take(rowCount).ToList<sp_GetJobTitleLevelResult>();
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
                            HttpUtility.HtmlEncode(m.DisplayName),
                            HttpUtility.HtmlEncode(m.JobTitleName),   
                            HttpUtility.HtmlEncode(m.DepartmentName),   
                            m.IsManager.ToString(),
                            CommonFunc.ShowActiveImage(m.IsActive),
                            "<input type=\"button\" class=\"icon edit\" title=\"Edit\" onclick=\"CRM.popup('/JobTitleLevel/Edit/" + m.ID.ToString() + "', 'Update', 400)\" />"                            
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Insert)]
         public ActionResult Create()
         {
             ViewData[CommonDataKey.JTL_JOBTITLE_LEVEL] = new SelectList(Constants.JobTitleLevel, "Value", "Text", "");
             ViewData[CommonDataKey.JTL_JOBTITLE] = new SelectList(jobTitleDao.GetList(), "JobTitleId", "JobTitleName","");
             return View();
         }

         //
         // POST: /JobTitle/Create

         [HttpPost]
         [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Insert)]
         [ValidateInput(false)]
         public ActionResult Create(JobTitleLevel jobTitle)
         {
             var principal = HttpContext.User as AuthenticationProjectPrincipal;
             jobTitle.CreateDate = DateTime.Now;
             jobTitle.CreatedBy = principal.UserData.UserName;
             jobTitle.UpdateDate = DateTime.Now;
             jobTitle.UpdatedBy = principal.UserData.UserName;

             Message msg = jobTitleLevelDao.Insert(jobTitle);            
             ShowMessage(msg);
             return RedirectToAction("Index");
         }

        [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Update)]
         public ActionResult Edit(int id)
         {
             JobTitleLevel jobTitle = jobTitleLevelDao.GetById(id);
             ViewData[CommonDataKey.JTL_JOBTITLE_LEVEL] = new SelectList(Constants.JobTitleLevel, "Value", "Text", jobTitle.JobLevel);
             ViewData[CommonDataKey.JTL_JOBTITLE] = new SelectList(jobTitleDao.GetList(), "JobTitleId", "JobTitleName", jobTitle.JobTitleId);
             return View(jobTitle);
         }

         //
         // POST: /JobTitle/Edit/5

         [HttpPost]
         [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Update)]
         [ValidateInput(false)]
         public ActionResult Edit(JobTitleLevel jobTitle)
         {
             var principal = HttpContext.User as AuthenticationProjectPrincipal;
             jobTitle.UpdatedBy = principal.UserData.UserName;
             Message msg = jobTitleLevelDao.Update(jobTitle);
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
             Message msg = jobTitleLevelDao.DeleteList(id, principal.UserData.UserName);
             //ShowMessage(msg);
             //return RedirectToAction("Index");
             JsonResult result = new JsonResult();
             return Json(msg);
         }

         /// <summary>
         /// Export Job Title list to excel
         /// </summary>
         /// <param name="name"></param>
         /// <param name="year"></param>
         /// <param name="sortname"></param>
         /// <param name="sortorder"></param>
         [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Export)]
         public void ExportToExcel(string name, string year, string sortname, string sortorder)
         {
             Hashtable hashData = Session[SessionKey.JOB_TITLE_LEVEL_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.JOB_TITLE_LEVEL_DEFAULT_VALUE];
             string jobTitleName = hashData[Constants.JOB_TITLE_LEVEL_NAME] == null ? Constants.JOB_TITLE_lEVEL : !string.IsNullOrEmpty((string)hashData[Constants.JOB_TITLE_LEVEL_NAME]) ? (string)hashData[Constants.JOB_TITLE_LEVEL_NAME] : Constants.JOB_TITLE_lEVEL;
             int jobTitleId = !string.IsNullOrEmpty((string)hashData[Constants.JOB_TITLE_LEVEL_SELECTION]) ? int.Parse((string)hashData[Constants.JOB_TITLE_LEVEL_SELECTION]) : 0; 
             if (jobTitleName == Constants.JOB_TITLE_lEVEL)
             {
                 jobTitleName = string.Empty;
             }             

             List<sp_GetJobTitleLevelResult> holidayList = TempData[CommonDataKey.JTL_JOBTITLE_LEVEL_LIST] != null ?
                 (List<sp_GetJobTitleLevelResult>)TempData[CommonDataKey.JTL_JOBTITLE_LEVEL_LIST] : jobTitleLevelDao.GetListFilter(jobTitleName, jobTitleId);
             ExportExcel exp = new ExportExcel();
             exp.Title = Constants.JTL_JOB_TITLE_LEVEL_LIST;
             exp.FileName = Constants.JTL_JOB_TITLE_LEVEL_FILE_NAME;
             exp.ColumnList = new string[] { "DisplayName", "JobTitleName", "DepartmentName", "IsManager", "IsActive" };
             exp.HeaderExcel = new string[] { "Title Name", "Job Title", "Department", "Manage","Active" };
             exp.List = holidayList;
             exp.IsRenderNo = true;
             exp.Execute();
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
         private void SetSessionFilter(string jobTitleName, string jobTitleId,
             string column, string order, int pageIndex, int rowCount)
         {
             Hashtable hashData = new Hashtable();
             hashData.Add(Constants.JOB_TITLE_LEVEL_NAME, jobTitleName);
             hashData.Add(Constants.JOB_TITLE_LEVEL_SELECTION, jobTitleId);
             hashData.Add(Constants.JOB_TITLE_LEVEL_COLUMN, column);
             hashData.Add(Constants.JOB_TITLE_LEVEL_ORDER, order);
             hashData.Add(Constants.JOB_TITLE_LEVEL_PAGE_INDEX, pageIndex);
             hashData.Add(Constants.JOB_TITLE_LEVEL_ROW_COUNT, rowCount);

             Session[SessionKey.JOB_TITLE_LEVEL_DEFAULT_VALUE] = hashData;
         }
    }
}
