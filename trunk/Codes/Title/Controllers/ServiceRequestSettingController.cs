using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using CRM.Library.Common;
using CRM.Models;
using CRM.Library.Attributes;

namespace CRM.Controllers
{
    public class ServiceRequestSettingController : BaseController
    {
        #region Variables
        private EmployeeDao empDao = new EmployeeDao();
        private LocationDao locationDao = new LocationDao();
        private SRSettingDao settingDao = new SRSettingDao();
        #endregion

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestAdmin, Rights = Permissions.Routing)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[Constants.SR_LIST_SETTING_INDEX] == null ? new Hashtable() : (Hashtable)Session[Constants.SR_LIST_SETTING_INDEX];

            ViewData[Constants.SR_LIST_SETTING_TEXT] = hashData[Constants.SR_LIST_SETTING_TEXT] == null ? Constants.USERNAME : !string.IsNullOrEmpty((string)hashData[Constants.SR_LIST_SETTING_TEXT]) ? hashData[Constants.SR_LIST_SETTING_TEXT] : Constants.USERNAME;
            ViewData[Constants.SR_LIST_SETTING_PROJECT] = new SelectList(empDao.GetProjectList(), hashData[Constants.SR_LIST_SETTING_PROJECT] == null ? string.Empty : (string)hashData[Constants.SR_LIST_SETTING_PROJECT]);
            ViewData[Constants.SR_LIST_SETTING_BRANCH] = new SelectList(locationDao.GetListBranchAll(true, false),"ID","Name", hashData[Constants.SR_LIST_SETTING_BRANCH] == null ? string.Empty : (string)hashData[Constants.SR_LIST_SETTING_BRANCH]);
            ViewData[Constants.SR_LIST_SETTING_OFFICE] = new SelectList(locationDao.GetListOffice(ConvertUtil.ConvertToInt(hashData[Constants.SR_LIST_SETTING_OFFICE]), true, false), "ID", "Name", hashData[Constants.SR_LIST_SETTING_OFFICE] == null ? string.Empty : hashData[Constants.SR_LIST_SETTING_OFFICE]);

            ViewData[Constants.SR_LIST_SETTING_COLUMN] = hashData[Constants.SR_LIST_SETTING_COLUMN] == null ? "ID" : hashData[Constants.SR_LIST_SETTING_COLUMN];
            ViewData[Constants.SR_LIST_SETTING_ORDER] = hashData[Constants.SR_LIST_SETTING_ORDER] == null ? "desc" : hashData[Constants.SR_LIST_SETTING_ORDER];
            ViewData[Constants.SR_LIST_SETTING_PAGE_INDEX] = hashData[Constants.SR_LIST_SETTING_PAGE_INDEX] == null ? "1" : hashData[Constants.SR_LIST_SETTING_PAGE_INDEX].ToString();
            ViewData[Constants.SR_LIST_SETTING_ROW] = hashData[Constants.SR_LIST_SETTING_ROW] == null ? "20" : hashData[Constants.SR_LIST_SETTING_ROW].ToString();
            return View();
        }

        /// <summary>
        /// Set Session of Service Request Setting
        /// </summary>
        /// <param name="text"></param>
        /// <param name="project"></param>
        /// <param name="branch"></param>
        /// <param name="office"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionServiceRequestSetting(string text,string project, string branch, string office,string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();

            hashData.Add(Constants.SR_LIST_SETTING_TEXT, text);
            hashData.Add(Constants.SR_LIST_SETTING_PROJECT, project);
            hashData.Add(Constants.SR_LIST_SETTING_BRANCH, branch);
            hashData.Add(Constants.SR_LIST_SETTING_OFFICE, office);
            hashData.Add(Constants.SR_LIST_SETTING_COLUMN, column);
            hashData.Add(Constants.SR_LIST_SETTING_ORDER, order);
            hashData.Add(Constants.SR_LIST_SETTING_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.SR_LIST_SETTING_ROW, rowCount);

            Session[Constants.SR_LIST_SETTING_INDEX] = hashData;
        }

        /// <summary>
        /// Get List Service Request Setting
        /// </summary>
        /// <param name="filterText"></param>
        /// <param name="project"></param>
        /// <param name="branch"></param>
        /// <param name="office"></param>
        /// <returns></returns>
        public ActionResult GetListJQGrid(string filterText, string project, string branch, string office)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

            #endregion

            SetSessionServiceRequestSetting(filterText, project, branch, office, sortColumn, sortOrder, pageIndex, rowCount);

            #region search
            string userName = null;
            string projectName = null;
            int branchID = 0;
            int officeID = 0;
            if (filterText != Constants.USERNAME)
            {
                userName = filterText;
            }
            if (!string.IsNullOrEmpty(branch))
            {
                branchID = ConvertUtil.ConvertToInt(branch);
            }
            if (!string.IsNullOrEmpty(office))
            {
                officeID = ConvertUtil.ConvertToInt(office);
            }
            if (!string.IsNullOrEmpty(project))
            {
                projectName = project;
            }
            #endregion

            List<sp_GetSR_SettingResult> list = settingDao.GetList(userName, projectName, branchID, officeID).OrderBy(q => q.ProjectName).ToList();

            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = settingDao.Sort(list, sortColumn, sortOrder)
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
                                m.OfficeName,
                                string.IsNullOrEmpty(m.ProjectName)?"(Default Routing)" : m.ProjectName,
                                m.UserName,
                                CommonFunc.ShowActiveImage(m.IsActive, "CRM.changeActiveStatus('/ServiceRequestSetting/ChangeActiveStatus/" + 
                                m.ID +"?isActive=" + !m.IsActive + "&updateDate="+m.UpdateDate.ToString()+"', " + (int)MessageType.Error +")"),  
                                CommonFunc.Button("edit","Edit","CRM.popup('/ServiceRequestSetting/Edit/" + m.ID + "', 'Route Setting for : " + 
                                    m.UserName +"', 550)")
                            }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Update Status of Service Request Setting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestAdmin, Rights = Permissions.Routing)]
        public JsonResult ChangeActiveStatus(int id, string isActive,string updateDate)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            SR_Setting obj = settingDao.GetByID(id);
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            try
            {
                result.Data = settingDao.UpdateActiveStatus(id, bool.Parse(isActive), updateDate, principal.UserData.UserName);
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }

        /// <summary>
        /// Create SR
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestAdmin, Rights = Permissions.Routing)]
        public ActionResult Create()
        {
            ViewData[Constants.SR_LIST_SETTING_PROJECT] = new SelectList(empDao.GetProjectList());
            ViewData[Constants.SR_LIST_SETTING_OFFICE] = new SelectList(locationDao.GetListOffice(0, true, false), "ID", "Name", string.Empty);
            return View();
        }

        /// <summary>
        /// Create SR
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestAdmin, Rights = Permissions.Routing)]
        public JsonResult Create(SR_Setting obj)
        {
            Message msg = null;
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (new UserAdminDao().GetByUserName(Request["UserAdminText"]) != null)
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                obj.CreateDate = DateTime.Now;
                obj.CreatedBy = principal.UserData.UserName;
                obj.UpdateDate = DateTime.Now;
                obj.UpdatedBy = principal.UserData.UserName;
                obj.DeleteFlag = false;
                msg = settingDao.Insert(obj);
                if (msg.MsgType == MessageType.Info)
                {
                    ShowMessage(msg);
                }
            }
            else
            {
                msg = new Message(MessageConstants.E0003, MessageType.Error, Request["UserAdminText"]);
            }
            result.Data = msg;
            return result;
        }

        /// <summary>
        /// Update Sr
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         [CrmAuthorizeAttribute(Module = Modules.ServiceRequestAdmin, Rights = Permissions.Routing)]
        public ActionResult Edit(string id)
        {
            try
            {
                SR_Setting obj = settingDao.GetByID(ConvertUtil.ConvertToInt(id));
                if (obj != null)
                {
                    ViewData[Constants.SR_LIST_SETTING_PROJECT] = new SelectList(empDao.GetProjectList(),obj.ProjectName);
                    ViewData[Constants.SR_LIST_SETTING_OFFICE] = new SelectList(locationDao.GetListOffice(0, true, false), "ID", "Name", obj.OfficeID);
                    ViewData["UpdateDate"] = obj.UpdateDate.ToString();
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
         /// Update Sr
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
         [HttpPost]
         [CrmAuthorizeAttribute(Module = Modules.ServiceRequestAdmin, Rights = Permissions.Routing)]
        public JsonResult Edit(SR_Setting obj)
        {
            Message msg = null;
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (new UserAdminDao().GetByUserName(Request["UserAdminText"]) != null)
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;                
                obj.UpdatedBy = principal.UserData.UserName;
                msg = settingDao.Update(obj);
                if (msg.MsgType == MessageType.Info)
                {
                    ShowMessage(msg);
                }
            }
            else
            {
                msg = new Message(MessageConstants.E0003, MessageType.Error, Request["UserAdminText"]);
            }
            result.Data = msg;
            return result;
        }

         /// <summary>
         /// Delete Sr
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestAdmin, Rights = Permissions.Routing)]
        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = settingDao.DeleteList(id, principal.UserData.UserName);
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        public ActionResult Refresh(string id)
        {
            Session.Remove(Constants.SR_LIST_SETTING_INDEX);
            return RedirectToAction("Index");
        }

    }
}
