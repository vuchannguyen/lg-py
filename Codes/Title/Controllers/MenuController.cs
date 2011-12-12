using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Areas.Portal.Controllers;
using CRM.Library.Common;
using CRM.Models;
using System.IO;
using CRM.Library.Attributes;

namespace CRM.Controllers
{
    /// <summary>
    /// The controller og menu management page
    /// </summary>
    public class MenuController : BaseController
    {
        //
        // GET: /Menu/
        /// <summary>
        /// menuDao
        /// </summary>
        private MenuDao menuDao = new MenuDao();
        /// <summary>
        /// group permission dao
        /// </summary>
        private GroupPermissionDao gpDao = new GroupPermissionDao();
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Menu, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            return View(menuDao.GetNotChildList(null, true).ToList());
        }
        /// <summary>
        /// GET: creat menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Menu, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create(string id)
        {
            ViewData["ParentId"] = id;
            return View();
        }
        /// <summary>
        /// Check the menu for valid information
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [NonAction]
        public Message CheckValidMenu(Menu menu)
        {
            if (menu == null)
                return new Message(MessageConstants.E0020, MessageType.Error, "Menu", "Database");
            int nameMaxlength = CommonFunc.GetLengthLimit(menu, "Name");
            if(menu.Name.Length > nameMaxlength)
                return new Message(MessageConstants.E0029, MessageType.Error, "The length of \"Name\" field", nameMaxlength);
            int linkMaxLength = CommonFunc.GetLengthLimit(menu, "Link");
            if (menu.Name.Length > nameMaxlength)
                return new Message(MessageConstants.E0029, MessageType.Error, "The length of \"Link\" field", nameMaxlength);

            Menu menuDb = menuDao.GetByID(menu.ID, null);
            if (!string.IsNullOrEmpty(menu.ImageUrl) && ( menuDb == null || menu.ImageUrl != menuDb.ImageUrl))
            {
                string filePath = Server.MapPath(Constants.UPLOAD_TEMP_PATH + menu.ImageUrl);
                if (!System.IO.File.Exists(filePath))
                    return new Message(MessageConstants.E0011, MessageType.Error);
                //FileInfo fi = new FileInfo(filePath);
                //if(!Constants.MENU_PAGE_IMAGE_FORMAT_ALLOWED.Replace("*","").Split(';').Contains(fi.Extension))
                //    return new Message(MessageConstants.E0013, MessageType.Error, fi.Extension.Remove(0,1), 
                //        Constants.MENU_PAGE_IMAGE_FORMAT_ALLOWED, (int)(Constants.MENU_PAGE_IMAGE_MAX_SIZE/1024));
                //if(fi.Length > Constants.MENU_PAGE_IMAGE_MAX_SIZE)
                //    return new Message(MessageConstants.E0012, MessageType.Error, Constants.MENU_PAGE_IMAGE_MAX_SIZE + " Bytes");
            }
            return null;
        }
        /// <summary>
        /// POST: create menu
        /// </summary>
        /// <param name="menuObj"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Menu, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create(Menu menuObj)
        {
            Message msg = null;
            var principal = HttpContext.User;
            try
            {
                string imageName = Request["ServerImageName"];
                int nModule = Request.Form.AllKeys.Where(p=>p.Contains(Constants.MENU_PAGE_MODULE_ID_PREFIX)).Count();
                List<MenuPermission> menuPermissionList = new List<MenuPermission>();
                for (int i = 0; i < nModule; i++)
                {
                    MenuPermission mp = new MenuPermission();
                    mp.ModuleId = ConvertUtil.ConvertToInt(Request[Constants.MENU_PAGE_MODULE_ID_PREFIX + i]);
                    mp.PermissionId = ConvertUtil.ConvertToInt(Request[Constants.MENU_PAGE_PERMISSION_ID_PREFIX + i]);
                    if (menuPermissionList.Where(p => p.ModuleId == mp.ModuleId && p.PermissionId == mp.PermissionId).Count() == 0)
                        menuPermissionList.Add(mp);
                }

                //menuObj.ParentId = ConvertUtil.ConvertToInt(parentId);
                menuObj.CreatedBy = principal.Identity.Name;
                menuObj.UpdatedBy = principal.Identity.Name;
                menuObj.ImageUrl = string.IsNullOrEmpty(imageName) ? null : imageName;
                
                msg = CheckValidMenu(menuObj);
                if (msg == null)
                {
                    msg = menuDao.Insert(menuObj, menuPermissionList);
                    if (msg.MsgType != MessageType.Error && !string.IsNullOrEmpty(menuObj.ImageUrl))
                    {
                        MoveUploadedFile(imageName);
                    }
                    //CommonFunc.RemoveAllFile(Constants.UPLOAD_TEMP_PATH, Constants.MENU_PAGE_ICON_NAME_PREFIX + "*");
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
        /// GET: Edit menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Menu, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Edit(string id)
        {
            int menuID = ConvertUtil.ConvertToInt(id);
            Menu menu = menuDao.GetByID(menuID, null);
            var mplist = menuDao.GetMenuPermissionList(menuID);
            ViewData[CommonDataKey.MENU_PERMISSION_LIST] = mplist;
            return View(menu);
        }
        /// <summary>
        /// POST: Edit menu
        /// </summary>
        /// <param name="menuObj"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Menu, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Edit(Menu menuObj)
        {
            Message msg = null;
            var principal = HttpContext.User;
            try
            {
                string imageName = Request["ServerImageName"];
                int nModule = Request.Form.AllKeys.Where(p => p.Contains(Constants.MENU_PAGE_MODULE_ID_PREFIX)).Count();
                List<MenuPermission> menuPermissionList = new List<MenuPermission>();
                for (int i = 0; i < nModule; i++)
                {
                    MenuPermission mp = new MenuPermission();
                    mp.ModuleId = ConvertUtil.ConvertToInt(Request[Constants.MENU_PAGE_MODULE_ID_PREFIX + i]);
                    mp.PermissionId = ConvertUtil.ConvertToInt(Request[Constants.MENU_PAGE_PERMISSION_ID_PREFIX + i]);
                    if (menuPermissionList.Where(p => p.ModuleId == mp.ModuleId && p.PermissionId == mp.PermissionId).Count()==0)
                        menuPermissionList.Add(mp);
                }

                //menuObj.ParentId = ConvertUtil.ConvertToInt(parentId);
                menuObj.UpdatedBy = principal.Identity.Name;
                menuObj.ImageUrl = string.IsNullOrEmpty(imageName) ? null : imageName;

                msg = CheckValidMenu(menuObj);
                if (msg == null)
                {
                    msg = menuDao.Update(menuObj, menuPermissionList);
                    if (msg.MsgType != MessageType.Error && !string.IsNullOrEmpty(menuObj.ImageUrl))
                    {
                        MoveUploadedFile(imageName);
                    }
                    //CommonFunc.RemoveAllFile(Constants.UPLOAD_TEMP_PATH, Constants.MENU_PAGE_ICON_NAME_PREFIX + "*");
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                //msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
            }
            ShowMessage(msg);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Create the menu tree
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [CrmAuthorizeAttribute(Module = Modules.Menu, Rights = Permissions.Read, ShowInPopup = true)]
        public string CreateMenuTree()
        {
            string result = string.Empty;
            List<Menu> menuList = menuDao.GetList(null);
            List<Menu> menuNotChild = menuDao.GetNotChildList(menuList, null);
            foreach (Menu menu in menuNotChild)
            {
                result += ExtensionMethods.GetDisplayString(menuList, menu);
            }
            return result;
        }
        [NonAction]
        public void MoveUploadedFile(string fileName)
        {
            string sourcePath = Server.MapPath(Constants.UPLOAD_TEMP_PATH + fileName);
            string destPath = Server.MapPath(Constants.MENU_PAGE_ICON_FOLDER + fileName);
            if (System.IO.File.Exists(sourcePath))
                System.IO.File.Move(sourcePath, destPath);  
        }
        /// <summary>
        /// Get module list for module dropdownlist on Create menu popup window when clicking the add button
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Menu, Rights = Permissions.Read, ShowInPopup = true)]
        public JsonResult GetModuleList()
        {
            var moduleList = PermissionConstants.ListModule;
            var jsonResult = Json(new { 
                modules = moduleList.ToArray()
            }, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        /// <summary>
        /// Get permission list for permission dropdownlist on Create menu popup window when changing the 
        /// module dropdownlist value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Menu, Rights = Permissions.Read, ShowInPopup = true)]
        public JsonResult GetPermissionList(string id)
        {
            int moduleID = ConvertUtil.ConvertToInt(id);
            var permissionList = gpDao.GetPermissionByModuleId(moduleID).Select
                (p => new {   
                    p.PermissionId, 
                    PermissionConstants.ListPermission.Where(
                        q=>ConvertUtil.ConvertToInt(q.Value) == p.PermissionId).FirstOrDefault().Text
                });
            var jsonResult = Json(new
            {
                permissions = permissionList.ToArray()
            }, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        [CrmAuthorizeAttribute(Module = Modules.Menu, Rights = Permissions.Read, ShowInPopup = true)]
        public JsonResult RemoveFile(string name)
        {
            Message msg = null;
            try
            {
                if (string.IsNullOrEmpty(name))
                    msg = new Message(MessageConstants.E0030, MessageType.Error, "File name");
                string filePath = Server.MapPath(Constants.UPLOAD_TEMP_PATH + name);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Image", "deleted");
                }
                else
                    msg = new Message(MessageConstants.E0014, MessageType.Error, "delete");
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return Json(new
            {
                msg = msg
            }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// Delete the menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Menu, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public ActionResult Delete(string id)
        {
            Message msg = null;
            try
            {
                int menuId = ConvertUtil.ConvertToInt(id);
                Menu menu = menuDao.GetByID(menuId, null);
                if (menu == null)
                    msg = new Message(MessageConstants.E0005, MessageType.Error, "Selected menu", "database");
                else
                    msg = menuDao.Delete(menuId);
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);            
            JsonResult result = new JsonResult();
            return Json(msg);
        }
    }
}
