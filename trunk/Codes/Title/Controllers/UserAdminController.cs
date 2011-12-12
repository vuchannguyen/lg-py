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
using System.Web.Routing;

namespace CRM.Controllers
{
    public class UserAdminController : BaseController
    {
        private UserAdminDao accDao = new UserAdminDao();
        private GroupDao groupDao = new GroupDao();
        // GET: /UserAdmin/

        [CrmAuthorizeAttribute(Module = Modules.UserAdmin, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.ACCOUNT_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.ACCOUNT_DEFAULT_VALUE];

            ViewData[Constants.ACCOUNT_NAME] = hashData[Constants.ACCOUNT_NAME] == null ? Constants.USERNAME : !string.IsNullOrEmpty((string)hashData[Constants.ACCOUNT_NAME]) ? hashData[Constants.ACCOUNT_NAME] : Constants.USERNAME;
            ViewData[Constants.ACCOUNT_GROUP_ID] = new SelectList(groupDao.GetList(), "GroupId", "Groupname", hashData[Constants.ACCOUNT_GROUP_ID] == null ? Constants.FIRST_ITEM_GROUP_NAME : hashData[Constants.ACCOUNT_GROUP_ID]);

            ViewData[Constants.ACCOUNT_COLUMN] = hashData[Constants.ACCOUNT_COLUMN] == null ? "UserName" : hashData[Constants.ACCOUNT_COLUMN];
            ViewData[Constants.ACCOUNT_ORDER] = hashData[Constants.ACCOUNT_ORDER] == null ? "asc" : hashData[Constants.ACCOUNT_ORDER];
            ViewData[Constants.ACCOUNT_PAGE_INDEX] = hashData[Constants.ACCOUNT_PAGE_INDEX] == null ? "1" : hashData[Constants.ACCOUNT_PAGE_INDEX].ToString();
            ViewData[Constants.ACCOUNT_ROW_COUNT] = hashData[Constants.ACCOUNT_ROW_COUNT] == null ? "20" : hashData[Constants.ACCOUNT_ROW_COUNT].ToString();
            
            return View();
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.ACCOUNT_DEFAULT_VALUE);
            return RedirectToAction("Index");
        }

        public JsonResult ChangeActiveStatus(int id, string isActive)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                result.Data = accDao.UpdateActiveStatus(id, bool.Parse(isActive));
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }

        [CrmAuthorizeAttribute(Module = Modules.UserAdmin, Rights = Permissions.Read)]
        public ActionResult GetListJQGrid(string name,string groupName)
        {
            
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(name, groupName, sortColumn, sortOrder, pageIndex, rowCount);

            if (name.Trim().ToLower().Equals(Constants.USERNAME.ToLower()))
            {
                name = string.Empty;
            }
            
            List<User_Group> accList = accDao.GetListUser_Group();
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(groupName))
            {
                accList = accList.Where(p => !string.IsNullOrEmpty(groupName) ? 
                    p.GroupId == int.Parse(groupName) && p.UserAdmin.UserName.ToLower().Contains(name.ToLower()) :
                    p.UserAdmin.UserName.ToLower().Contains(name.ToLower())).ToList<User_Group>();
            }
            int totalRecords = accList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = accDao.Sort(accList, sortColumn, sortOrder)
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
                            m.UserAdmin.UserName,
                            m.Group.GroupName,
                            CommonFunc.ShowActiveImage(m.IsActive, "CRM.changeActiveStatus('/UserAdmin/ChangeActiveStatus/" + 
                                m.ID +"?isActive=" + !m.IsActive + "', " + (int)MessageType.Error +")"),   
                            m.CreatedBy,                                                        
                            m.UpdatedBy,                            
                            "<input type=\"button\" class=\"icon edit\" title=\"Edit\" onclick=\"CRM.popup('/UserAdmin/Edit/" + m.ID + "', 'Update', 400)\" />"
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
     

        //
        // GET: /UserAdmin/Create
        [CrmAuthorizeAttribute(Module = Modules.UserAdmin, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(groupDao.GetList(true), "GroupId", "GroupName", "");
            return View();
        }

        //
        // POST: /UserAdmin/Create
        [CrmAuthorizeAttribute(Module = Modules.UserAdmin, Rights = Permissions.Insert, ShowInPopup = true)]
        [HttpPost]
        public ActionResult Create(FormCollection data)
        {            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string userName = data["hidUserName"];
            string groupId = data["GroupId"];
            bool isActive = (data["IsActive"] == "true");

            List<string> list_userName = CommonFunc.GetListOfUserName(userName);
            Message msg = accDao.InsertMany(list_userName, groupId, isActive, principal);
            ShowMessage(msg);            
            return RedirectToAction("Index");
        }

        //
        // GET: /UserAdmin/Edit/5
        [CrmAuthorizeAttribute(Module = Modules.UserAdmin, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Edit(int id)
        {
            User_Group user_group = accDao.GetUser_Group(id);
            ViewData["GroupId"] = new SelectList(groupDao.GetList(true), "GroupId", "GroupName", user_group.GroupId);
            return View(user_group);
        }

        //
        // POST: /UserAdmin/Edit/5
        [CrmAuthorizeAttribute(Module = Modules.UserAdmin, Rights = Permissions.Update, ShowInPopup = true)]
        [HttpPost]
        public ActionResult Edit(FormCollection data)
        {
            string id = data["ID"];
            string userName = data["UserName"];
            string groupId = data["GroupId"];
            bool isActive = (data["IsActive"] == "true");

            // TODO: Add update logic here
            var principal = HttpContext.User as AuthenticationProjectPrincipal;                                  
            Message msg = accDao.Update(id, userName, groupId, isActive, principal);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }
              
        /// <summary>
        /// Delete list by ids
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.UserAdmin, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = null;
            string[] arrDeleteUser = id.Split(',');
            if (arrDeleteUser.Contains(principal.UserData.UserID.ToString()))
            {
                msg= new Message(MessageConstants.E0022, MessageType.Error);                
            }
            else
            {
                msg = accDao.DeleteList(id);                
            }
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        #region Validate 


        #endregion


        /// <summary>
        /// Set Session to Filter
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="groupId"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilter(string accountName, string groupId,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.ACCOUNT_NAME, accountName);
            hashData.Add(Constants.ACCOUNT_GROUP_ID, groupId);
            hashData.Add(Constants.ACCOUNT_COLUMN, column);
            hashData.Add(Constants.ACCOUNT_ORDER, order);
            hashData.Add(Constants.ACCOUNT_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.ACCOUNT_ROW_COUNT, rowCount);

            Session[SessionKey.ACCOUNT_DEFAULT_VALUE] = hashData;
        }
   }
}
