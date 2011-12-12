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
    public class JRAdminController : BaseController
    {
        //
        // GET: /JRAdmin/

        #region variables

        private UserAdminDao adminDao = new UserAdminDao();
        private JRAdminDao jrAdminDao = new JRAdminDao();
        private WorkflowDao wfDao = new WorkflowDao();
        private RoleDao roleDao = new RoleDao();

        #endregion

        [CrmAuthorizeAttribute(Module = Modules.WorkflowAdmin, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.JR_ADMIN_FILTER] == null ? new Hashtable() : (Hashtable)Session[SessionKey.JR_ADMIN_FILTER];
            ViewData[Constants.JR_ADMIN_NAME] = hashData[Constants.JR_ADMIN_NAME] == null ? Constants.USERNAME : !string.IsNullOrEmpty((string)hashData[Constants.JR_ADMIN_NAME]) ? hashData[Constants.JR_ADMIN_NAME] : Constants.USERNAME;
            ViewData[Constants.JR_ADMIN_WORKFLOW] = new SelectList(wfDao.GetList(true), "ID", "Name", hashData[Constants.JR_ADMIN_WORKFLOW] == null ? Constants.FIRST_ITEM_WORKFLOW : hashData[Constants.JR_ADMIN_WORKFLOW]);
            //Added by Huy Ly - 12-May-2010
            List<WFRole> listWFRole = new List<WFRole>();

            if (hashData[Constants.JR_ADMIN_WORKFLOW] != null && !string.IsNullOrEmpty(hashData[Constants.JR_ADMIN_WORKFLOW].ToString()))
            {
                listWFRole = roleDao.GetListByWorkflow( ConvertUtil.ConvertToInt(hashData[Constants.JR_ADMIN_WORKFLOW]));
            }
            else
            {
                listWFRole = roleDao.GetList(true);
            }
            ViewData[Constants.JR_ADMIN_GROUP_NAME] = new SelectList(listWFRole, "ID", "Name", hashData[Constants.JR_ADMIN_GROUP_NAME] == null ? Constants.FIRST_ITEM_GROUP_NAME : hashData[Constants.JR_ADMIN_GROUP_NAME]);
            ViewData[Constants.JR_ADMIN_COLUMN] = hashData[Constants.JR_ADMIN_COLUMN] == null ? "ID" : hashData[Constants.JR_ADMIN_COLUMN].ToString();
            ViewData[Constants.JR_ADMIN_ORDER] = hashData[Constants.JR_ADMIN_ORDER] == null ? "desc" : hashData[Constants.JR_ADMIN_ORDER].ToString();
            ViewData[Constants.JR_ADMIN_PAGE_INDEX] = hashData[Constants.JR_ADMIN_PAGE_INDEX] == null ? "1" : hashData[Constants.JR_ADMIN_PAGE_INDEX].ToString();
            ViewData[Constants.JR_ADMIN_ROW_COUNT] = hashData[Constants.JR_ADMIN_ROW_COUNT] == null ? "20" : hashData[Constants.JR_ADMIN_ROW_COUNT].ToString();
            return View();
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.JR_ADMIN_FILTER);
            return RedirectToAction("Index");
        }

        private void SetSessionFilter(string name,string workflow, string role, string column, string order, int pageIndex, int rowCount)
        {
            Hashtable jrAdminState = new Hashtable();
            jrAdminState.Add(Constants.JR_ADMIN_NAME, name);
            jrAdminState.Add(Constants.JR_ADMIN_WORKFLOW, workflow);
            jrAdminState.Add(Constants.JR_ADMIN_GROUP_NAME, role);
            jrAdminState.Add(Constants.JR_ADMIN_COLUMN, column);
            jrAdminState.Add(Constants.JR_ADMIN_ORDER, order);
            jrAdminState.Add(Constants.JR_ADMIN_PAGE_INDEX, pageIndex);
            jrAdminState.Add(Constants.JR_ADMIN_ROW_COUNT, rowCount);
            Session[SessionKey.JR_ADMIN_FILTER] = jrAdminState;
        }
        public JsonResult ChangeActiveStatus(int id, string isActive)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                result.Data = jrAdminDao.UpdateActiveStatus(id, bool.Parse(isActive));
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }
        /// <summary>
        /// Bind Data in Grid
        /// </summary>
        /// <param name="name"></param>
        /// <param name="workflow"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult GetListJQGrid(string name, string workflow, string role)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            SetSessionFilter(name, workflow, role, sortColumn, sortOrder, pageIndex, rowCount);
            #endregion

            #region search
            string userName = "";
            int wfId = 0;
            int roleId = 0;
            if (name != Constants.USERNAME)
            {
                userName = name;
            }
            if (!string.IsNullOrEmpty(workflow))
            {
                wfId = int.Parse(workflow);
            }
            if (!string.IsNullOrEmpty(role))
            {
                roleId = int.Parse(role);
            }
            #endregion

            List<sp_GetJRForAdminResult> groupList = jrAdminDao.GetList(userName, wfId, roleId);

            int totalRecords = groupList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = jrAdminDao.Sort(groupList, sortColumn, sortOrder)
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
                        i = m.ID.ToString(),
                        cell = new string[] {
                            m.ID.ToString(),
                            m.UserName ,
                            m.wfName,
                            m.roleName,
                            m.WFRoleID.Value.ToString(),
                            CommonFunc.ShowActiveImage(m.IsActive, "CRM.changeActiveStatus('/JRAdmin/ChangeActiveStatus/" + 
                                m.ID +"?isActive=" + !m.IsActive + "', " + (int)MessageType.Error +")"),         
                            m.CreatedBy,                            
                            m.UpdatedBy,
                            "<input type=\"button\" class=\"icon edit\" title = \"Edit\" onclick=\"CRM.popup('/JRAdmin/Edit/" + m.ID.ToString() + "', 'Update', 400)\" />"
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

		// GET: /JRAdmin/Create
        [CrmAuthorizeAttribute(Module = Modules.WorkflowAdmin, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create()
        {
            ViewData["WFID"] = new SelectList(wfDao.GetList(true), "ID", "Name", "");
            ViewData["WFRoleID"] = new SelectList(new List<string>());
            return View();
        }

		// POST: /JRAdmin/Create
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.WorkflowAdmin, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create(FormCollection collection)
        {
            Message msg = null;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            // TODO: Add insert logic here
            string userName = Request["hidUserName"];
            int wfRoleID = int.Parse(Request["WFRoleID"]);
            string sIsActive = Request["IsActive"];
            bool isActive = (Request["IsActive"] != "false");

            //UserAdmin obj = adminDao.GetByUserName(userName);
            JsonResult result = CheckManyUserNameAvailable(userName);
            if ((result.Data as string) != Boolean.TrueString)
            {
                List<UserAdmin_WFRole> list = new List<UserAdmin_WFRole>();
                foreach (string name in CommonFunc.GetListOfUserName(userName))
                {
                    int userAdminId = adminDao.GetByUserName(name).UserAdminId;
                    UserAdmin_WFRole item = new UserAdmin_WFRole();

                    item.WFRoleID = wfRoleID;
                    item.IsActive = isActive;
                    item.UserAdminId = userAdminId;
                    item.CreateDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    item.CreatedBy = principal.UserData.UserName;
                    item.UpdatedBy = principal.UserData.UserName;
                    list.Add(item);    
                }
                msg = jrAdminDao.Insert(list);
                // remove filter conditions
                //Session.Remove(SessionKey.JR_ADMIN_FILTER);
            }
            else
            {
                //msg = new Message(MessageConstants.E0005, MessageType.Warning, "User " + userName, "User Admin");
                msg = result.Data as Message;
            }
            ShowMessage(msg);
            return RedirectToAction("Index");
        }
        
        //
        // GET: /JRAdmin/Edit/

        [CrmAuthorizeAttribute(Module = Modules.WorkflowAdmin, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Edit(string id)
        {
            UserAdmin_WFRole viewData = jrAdminDao.GetById(int.Parse(id));
            ViewData["WFID"] = new SelectList(wfDao.GetList(true), "ID", "Name", roleDao.GetWorkflowByRole(viewData.WFRole.ID).WFID);
            ViewData["WFRoleID"] = new SelectList(roleDao.GetList(true), "ID", "Name", viewData.WFRole.ID);
            return View(viewData);
        }

        //
        // POST: /JRAdmin/Edit/

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.WorkflowAdmin, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Edit(UserAdmin_WFRole objUI)
        {
            Message msg = null;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string userName = Request["UserName"];
            UserAdmin obj = adminDao.GetByUserName(userName);
            if (obj != null)
            {
                objUI.UserAdminId = obj.UserAdminId;
                objUI.UpdatedBy = principal.UserData.UserName;
                msg = jrAdminDao.Update(objUI);
                
            }               
            ShowMessage(msg);
            return RedirectToAction("Index"); 
        }

		 /// <summary>
        /// Delete List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.WorkflowAdmin, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public ActionResult DeleteList(string name)
        {
            name = name.TrimEnd(':');
            bool canDelete = true;
            int userAdmin = 0;
            string roleName = string.Empty;
            Message msg = null;
            string[] idArr = name.Split(':');
            int i=0;
            foreach (string userAdminId in idArr)
            {
                bool test = Int32.TryParse(userAdminId.Split(',')[0],out i);
                UserAdmin_WFRole obj = jrAdminDao.GetById(i);
                if (obj != null)
                {
                    if (jrAdminDao.hasJRAssignTo(obj.UserAdminId, int.Parse(userAdminId.Split(',')[1])))
                    {
                        roleName = new RoleDao().GetByID(int.Parse(userAdminId.Split(',')[1])).Name;
                        userAdmin = obj.UserAdminId;
                        canDelete = false;
                        break;
                    }   
                }
            }
            if (canDelete == true)
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                msg = jrAdminDao.DeleteList(name, principal.UserData.UserName);
            }
            else
            {
                UserAdminDao adminDao = new UserAdminDao();
                UserAdmin obj = adminDao.GetById(userAdmin);
                if (obj != null)
                {
                    msg = new Message(MessageConstants.E0023, MessageType.Error, "delete " + obj.UserName + " with role " + roleName, "Job Request");
                }
            }
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }             

        #region Validate

        /// <summary>
        /// Check User Name Avalaible in User Admin  
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public JsonResult CheckUserNameAvailable(string userName)
        {
            //TODO: Do the validation
            Message msg = null;           
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            UserAdminDao adminDao = new UserAdminDao();
            UserAdmin obj = adminDao.GetByUserName(userName);
            if (obj == null)
            {
                msg = new Message(MessageConstants.E0005, MessageType.Error, userName,"User Admin");
                result.Data = msg.ToString();
            }
            else
            {
                result.Data = true;
            }
            return result;
        }
        /// <summary>
        /// Check list of User Name Avalaible in User Admin  
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public JsonResult CheckManyUserNameAvailable(string userName)
        {
            //TODO: Do the validation
            Message msg = null;
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            UserAdminDao adminDao = new UserAdminDao();
            List<string> list_userName = CommonFunc.GetListOfUserName(userName);
            List<string> list_notExist = new List<string>();
            foreach (string name in list_userName)
            {
                UserAdmin obj = adminDao.GetByUserName(name);
                if (obj == null)
                    list_notExist.Add(name);
            }

            if (list_notExist.Count != 0)
            {
                msg = new Message(MessageConstants.E0005, MessageType.Error, 
                    string.Join(Constants.SEPARATE_USER_ADMIN_USERNAME, list_notExist), "User Admin");
                result.Data = msg.ToString();
            }
            else
            {
                result.Data = true;
            }
            return result;
        }

        /// <summary>
        /// Check User Name Exits
        /// </summary>
        /// <param name="displayorder"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckUserNameAndRoleExistOnCreate(FormCollection content)
        {
            //TODO: Do the validation                                      
            Message msg = null;
            if (Request.IsAjaxRequest())
            {
                string userName = content["username"];
                int roleId = int.Parse(content["roleid"]);
                //int workflow = int.Parse(content["workflow"]);
                int id = 0;
                string action = content["action"];
                //bool isActive = bool.Parse(content["IsActive"]);
                if (!string.IsNullOrEmpty(content["ID"]))
                {
                    id = int.Parse(content["ID"]);
                }
                int userAdminId = new UserAdminDao().GetByUserName(userName).UserAdminId;
                bool isDuplicated = false;
                if (action == "Update")
                {
                    isDuplicated = jrAdminDao.CheckDuplicated(id, userAdminId, roleId, true);
                }
                else
                {
                    isDuplicated = jrAdminDao.CheckDuplicated(id, userAdminId, roleId, false);
                }
                if (isDuplicated)
                {
                    msg = new Message(MessageConstants.E0010, MessageType.Error, userName);
                }
                else
                {
                    msg = new Message(MessageConstants.I0001, MessageType.Info, string.Empty, string.Empty);
                }
                
            }
            return Json(msg);
        }

        /// <summary>
        /// Check User Name Exits
        /// </summary>
        /// <param name="displayorder"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckManyUserNameAndRoleExistOnCreate(FormCollection content)
        {
            //TODO: Do the validation                                      
            Message msg = null;
            UserAdminDao userAdminDao = new UserAdminDao();
            if (Request.IsAjaxRequest())
            {
                string userName = content["username"];
                List<string> list_userName = CommonFunc.GetListOfUserName(userName);
                List<string> list_Existed = new List<string>();
                int roleId = int.Parse(content["roleid"]);
                //int workflow = int.Parse(content["workflow"]);
                int id = 0;
                string action = content["action"];
                //bool isActive = bool.Parse(content["IsActive"]);
                if (!string.IsNullOrEmpty(content["ID"]))
                {
                    id = int.Parse(content["ID"]);
                }
                foreach (string name in list_userName)
                {
                    int userAdminId = userAdminDao.GetByUserName(name).UserAdminId;
                    bool isDuplicated = false;
                    if (action == "Update")
                    {
                        isDuplicated = jrAdminDao.CheckDuplicated(id, userAdminId, roleId, true);
                    }
                    else
                    {
                        isDuplicated = jrAdminDao.CheckDuplicated(id, userAdminId, roleId, false);
                    }
                    if (isDuplicated)
                    {
                        list_Existed.Add(name);
                        //msg = new Message(MessageConstants.E0010, MessageType.Error, userName);
                    }
                    
                }
                if (list_Existed.Count != 0)
                {
                    msg = new Message(MessageConstants.E0010, MessageType.Error, 
                        string.Join(Constants.SEPARATE_USER_ADMIN_USERNAME, list_Existed));
                }
                else
                {
                    msg = new Message(MessageConstants.I0001, MessageType.Info, string.Empty, string.Empty);
                }

            }
            return Json(msg);
        }
        private bool CheckDuplicate(string userName, int workFlow, int roleId, int userAdminId,bool isUpdate)
        {
            bool isDuplicated = false;
            List<sp_GetJRForAdminResult> objList = objList = jrAdminDao.GetList(userName, workFlow, roleId);
            if (isUpdate)
            {                    
                objList = objList.Where(p => (p.UserAdminId == userAdminId) && (p.WFRoleID == roleId)).ToList<sp_GetJRForAdminResult>();
            }
            else
            {
                objList = objList.Where(p => ((p.UserAdminId == userAdminId) && (p.WFRoleID == roleId))).ToList<sp_GetJRForAdminResult>(); 
            }
            if (objList.Count > 0)
            {
                isDuplicated = true;
            }
            return isDuplicated;
        }
        #endregion

    }
}
