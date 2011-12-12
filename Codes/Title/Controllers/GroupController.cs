using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CRM.Models.Entities;
using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Attributes;
using System.Web.Script.Serialization;

namespace CRM.Controllers
{
    public class GroupController : BaseController
    {
        private GroupDao groupDao = new GroupDao();        
		private GroupPermissionDao groupPerDao = new GroupPermissionDao();
        
        // GET: /Group/
        [CrmAuthorizeAttribute(Module = Modules.Group, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.GROUP_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.GROUP_DEFAULT_VALUE];

            ViewData[Constants.GROUP_NAME] = hashData[Constants.GROUP_NAME] == null ? Constants.GROUPNAME : !string.IsNullOrEmpty((string)hashData[Constants.GROUP_NAME]) ? hashData[Constants.GROUP_NAME] : Constants.GROUPNAME;
            ViewData[Constants.GROUP_COLUMN] = hashData[Constants.GROUP_COLUMN] == null ? "DisplayOrder" : hashData[Constants.GROUP_COLUMN];
            ViewData[Constants.GROUP_ORDER] = hashData[Constants.GROUP_ORDER] == null ? "asc" : hashData[Constants.GROUP_ORDER];
            ViewData[Constants.GROUP_PAGE_INDEX] = hashData[Constants.GROUP_PAGE_INDEX] == null ? "1" : hashData[Constants.GROUP_PAGE_INDEX].ToString();
            ViewData[Constants.GROUP_ROW_COUNT] = hashData[Constants.GROUP_ROW_COUNT] == null ? "20" : hashData[Constants.GROUP_ROW_COUNT].ToString();
            
            return View();
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.GROUP_DEFAULT_VALUE);
            return RedirectToAction("Index");
        }

        public JsonResult ChangeActiveStatus(int id, string isActive)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                result.Data = groupDao.UpdateActiveStatus(id, bool.Parse(isActive));
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }
        public ActionResult GetListJQGrid(string optionSearch)
        {
            //grid constant => duy hung

            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(optionSearch, sortColumn, sortOrder, pageIndex, rowCount);

            #region search
            string searchName = string.Empty;
            if (!string.IsNullOrEmpty(optionSearch) && !optionSearch.Equals(Constants.GROUPNAME))
            {
                searchName = optionSearch;
            }
            #endregion

            List<Group> groupList = groupDao.GetList(searchName);

            int totalRecords = groupList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = groupDao.Sort(groupList, sortColumn, sortOrder)
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
                        i = m.GroupId,
                        cell = new string[] {
                            m.GroupId.ToString(),
                            m.GroupName,
                            CommonFunc.ShowActiveImage(m.IsActive, "CRM.changeActiveStatus('/Group/ChangeActiveStatus/" + 
                                m.GroupId +"?isActive=" + !m.IsActive + "', " + (int)MessageType.Error +")"),                            
                            m.DisplayOrder.ToString(),                            
                            m.CreatedBy,                            
                            m.UpdatedBy.ToString(),
                            "<input type=\"button\" class=\"icon edit\" title=\"Edit\" onclick=\"CRM.popup('/Group/Edit/" + m.GroupId.ToString() + "', 'Update', 400)\" /> <input class=\"icon permission\" type=\"button\" title=\"Assign Permission\" onclick=\"CRM.redirect('/Group/Assign/"+m.GroupId.ToString()+"')\" />"
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

     
        // GET: /Group/Create
        [CrmAuthorizeAttribute(Module = Modules.Group, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Group/Create

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Group, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create(Group group)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            // TODO: Add insert logic here
            group.CreateDate = DateTime.Now;
            group.UpdateDate = DateTime.Now;
            group.CreatedBy = principal.UserData.UserName;
            group.UpdatedBy = principal.UserData.UserName;
            Message msg = groupDao.Insert(group);
            ShowMessage(msg);            
            return RedirectToAction("Index");
        }

        [CrmAuthorizeAttribute(Module = Modules.Group, Rights = Permissions.Update, ShowAtCurrentPage = true)]
		public ActionResult Assign(string id)
        {
            int groupId = int.Parse(id);
            ViewData["Title"] = groupDao.GetById(groupId).GroupName;
            List<ModuleModel> list = groupPerDao.GetModuleList(groupId);
            return View(list);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Group, Rights = Permissions.Update, ShowAtCurrentPage = true)]
        public ActionResult Assign(string id, FormCollection collection)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string userName = principal.UserData.UserName;
            int groupId = int.Parse(id);
            List<GroupPermission> groupList = new List<GroupPermission>();            
            foreach (var key in collection.AllKeys)
            {
                var value = collection.GetValues(key)[0];
                if (!string.IsNullOrEmpty(value))
                {
                    string[] arr = key.Split('_');
                    int moduleId = int.Parse(arr[0]);
                    int permisionId = int.Parse(arr[1]);
                    bool check = bool.Parse(value.ToString());
                    if (check)
                    {
                        GroupPermission groupPer = new GroupPermission();
                        groupPer.GroupId = groupId;
                        groupPer.ModuleId = moduleId;
                        groupPer.PermissionId = permisionId;
                        groupPer.CreatedBy = userName;
                        groupPer.CreateDate = DateTime.Now;
                        groupPer.UpdatedBy = userName;
                        groupPer.UpdateDate = DateTime.Now;
                        groupList.Add(groupPer);
                    }
                }
            }

            Message msg = groupPerDao.AssignPermission(groupId, groupList);
            ShowMessage(msg);                      
            return RedirectToAction("Index");
        }        
        
        // GET: /Group/Edit/5
        [CrmAuthorizeAttribute(Module = Modules.Group, Rights = Permissions.Update, ShowInPopup=true)]
        public ActionResult Edit(int id)
        {
            Group viewData = groupDao.GetById(id);                       
            return View(viewData);
        }

        //
        // POST: /Group/Edit/5

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Group, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Edit(Group group)
        {
            // TODO: Add update logic here
            var principal = HttpContext.User as AuthenticationProjectPrincipal;                        
            group.UpdatedBy = principal.UserData.UserName;
            Message msg = groupDao.Update(group);
            ShowMessage(msg);                     
            return RedirectToAction("Index");
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Group, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = groupDao.DeleteList(id, principal.UserData.UserName);
            //ShowMessage(msg);            
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }          

        #region Remote Validate        
        /// <summary>
        /// Check GroupName Exits
        /// </summary>
        /// <param name="displayorder"></param>
        /// <returns></returns>
        public JsonResult IsGroupNameExist(string groupName)
        {
            //TODO: Do the validation                          
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Group obj = groupDao.GetByGroupName(groupName);
            string msg = string.Format(Resources.Message.E0003, groupName);
            if (obj != null)
                result.Data = msg;
            else
                result.Data = true;
            return result;           
        }


        /// <summary>
        /// Check Order Exits
        /// </summary>
        /// <param name="displayorder"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IsGroupNameExistOnEdit(FormCollection content)
        {
            //TODO: Do the validation                                      
            Message msg = null;
            if (Request.IsAjaxRequest())
            {
                string groupName = content["groupName"];
                string id = content["id"];
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(groupName))
                {
                    Group obj = groupDao.GetByGroupName(groupName, int.Parse(id.ToString()));
                    if (obj != null)
                    {
                        msg = new Message(MessageConstants.E0003, MessageType.Error, groupName);                        
                    }
                    else
                    {
                        msg = new Message(MessageConstants.I0001, MessageType.Info, string.Empty, string.Empty);
                    }
                }
            }
            return Json(msg);
        }

        #endregion

        /// <summary>
        /// Set Session to Filter
        /// </summary>
        /// <param name="groupName"></param        
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilter(string groupName, 
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.GROUP_NAME, groupName);            
            hashData.Add(Constants.GROUP_COLUMN, column);
            hashData.Add(Constants.GROUP_ORDER, order);
            hashData.Add(Constants.GROUP_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.GROUP_ROW_COUNT, rowCount);

            Session[SessionKey.GROUP_DEFAULT_VALUE] = hashData;
        }
    }
}
