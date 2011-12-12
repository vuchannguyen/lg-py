using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.Library.Common;

namespace CRM.Controllers
{
    public class ModulePermissonController : BaseController
    {
        #region variable
        private ModulePermissonDao moduleDao = new ModulePermissonDao();
        #endregion
        //
        // GET: /ModulePermisson/

        public ActionResult Index()
        {
            return View();
        }

        private string GetPermissonbyModuleID(int moduleID)
        {
            string value = string.Empty;
            List<ModulePermission> list = moduleDao.GetActionByModuleID(moduleID);
            if (list.Count > 0)
            {
                foreach (ModulePermission item in list)
                {
                    value += ((Permissions)item.PermissionId).ToString() + ",";
                }
            }
            return value.TrimEnd(',');
        }

        public ActionResult GetListJQGrid()
        {
            List<ModuleData> listEnum = moduleDao.GetList();
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            int totalRecords = listEnum.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = listEnum.Skip((currentPage - 1) * rowCount).Take(rowCount);
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
                        i = index,
                        cell = new string[] {
                            m.ID.ToString(),
                            m.Name, 
                            GetPermissonbyModuleID(m.ID),
                            CommonFunc.Button("permission", "Permission", "CRM.popup('/ModulePermisson/Assign/" + m.ID + "', 'Assign Permisson', 400)")
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Assign(int id)
        {
            List<ModulePermission> list = moduleDao.GetActionByModuleID(id);
            TempData["ModuleId"] = id;
            return View(list);
        }

        [HttpPost]
        public ActionResult Assign(FormCollection collection)
        {
            List<ModulePermission> moduleList = new List<ModulePermission>();
            string listID = collection["hidID"];
            int moduleID = (int)TempData["ModuleId"];
            foreach (string name in listID.Split(','))
            {
                int id = int.Parse(name);
                var value = collection["chkActive" + id];
                if (!string.IsNullOrEmpty(value))
                {
                    bool check = bool.Parse(value.Split(',')[0].ToString());
                    if (check)
                    {
                        ModulePermission item = new ModulePermission();
                        item.ModuleId = moduleID;
                        item.PermissionId = id;
                        moduleList.Add(item);
                    }
                }
            }
            Message msg = moduleDao.Assign(moduleList, moduleID);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

    }
}
