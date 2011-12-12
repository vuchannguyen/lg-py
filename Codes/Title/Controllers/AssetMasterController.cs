using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.Library.Common;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Collections;
using CRM.Library.Attributes;
using System.Text.RegularExpressions;

namespace CRM.Controllers
{
    public class AssetMasterController : BaseController
    {
        AssetMasterDao asDao = new AssetMasterDao();
        AssetStatusDao asStatusDao = new AssetStatusDao();
        AssetCategoryDao asCategoryDao = new AssetCategoryDao();
        //EmployeeDao emDao = new EmployeeDao();
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.ASSET_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.ASSET_DEFAULT_VALUE];
            ViewData[Constants.ASSET_LIST_NAME] = hashData[Constants.ASSET_LIST_NAME] == null ? Constants.ASSET_MASTER_ASSETID_USERNAME_USERID : !string.IsNullOrEmpty((string)hashData[Constants.ASSET_LIST_NAME]) ? hashData[Constants.ASSET_LIST_NAME] : Constants.ASSET_MASTER_ASSETID_USERNAME_USERID;
            
            ViewData[Constants.ASSET_LIST_CATEGORY] = new SelectList(asCategoryDao.GetAssetCategoryList(), "ID", "Name", hashData[Constants.ASSET_LIST_CATEGORY] == null ? string.Empty : (string)hashData[Constants.ASSET_LIST_CATEGORY]);
            ViewData[Constants.ASSET_LIST_STATUS] = new SelectList(asStatusDao.GetAssetStatusList(), "ID", "StatusName", hashData[Constants.ASSET_LIST_STATUS] == null ? string.Empty : (string)hashData[Constants.ASSET_LIST_STATUS]);
            ViewData[Constants.ASSET_LIST_EMPLOYEE_PROJECT] = new SelectList(asDao.GetEmployeeProject(), "Project", "Project", hashData[Constants.ASSET_LIST_EMPLOYEE_PROJECT] == null ? string.Empty : (string)hashData[Constants.ASSET_LIST_EMPLOYEE_PROJECT]);

            ViewData[Constants.ASSET_MASTER_LIST_COLUMN] = hashData[Constants.ASSET_MASTER_LIST_COLUMN] == null ? "ID" : hashData[Constants.ASSET_MASTER_LIST_COLUMN];
            ViewData[Constants.ASSET_MASTER_LIST_ORDER] = hashData[Constants.ASSET_MASTER_LIST_ORDER] == null ? "desc" : hashData[Constants.ASSET_MASTER_LIST_ORDER];
            ViewData[Constants.ASSET_MASTER_LIST_PAGE_INDEX] = hashData[Constants.ASSET_MASTER_LIST_PAGE_INDEX] == null ? "1" : hashData[Constants.ASSET_MASTER_LIST_PAGE_INDEX].ToString();
            ViewData[Constants.ASSET_MASTER_LIST_ROW_COUNT] = hashData[Constants.ASSET_MASTER_LIST_ROW_COUNT] == null ? "20" : hashData[Constants.ASSET_MASTER_LIST_ROW_COUNT].ToString();
            return View();
        }

        //
        // GET: /AssetMaster/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult GetListJQGrid(string searchText, string category, string status, string project)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(searchText, category, status, project, sortColumn, sortOrder, pageIndex, rowCount);

            #region search
            string userName = null;
            //int categoryId = 0;
            //int statusId = 0;
            string projectName = null;
            //int categoryId = 0;
            //int statusId = 0;
            //string projectName = string.Empty;
            if (searchText != Constants.ASSET_MASTER_ASSETID_USERNAME_USERID && !string.IsNullOrEmpty(searchText))
            {
                userName = searchText;
            }
            //if (!string.IsNullOrEmpty(category))
            //{
            //    categoryId = ConvertUtil.ConvertToInt(category);
            //}
            //if (!string.IsNullOrEmpty(status))
            //{
            //    statusId = ConvertUtil.ConvertToInt(status);
            //}
            if (!string.IsNullOrEmpty(project))
            {
                projectName = project.ToString();
            }
            int categoryId = ConvertUtil.ConvertToInt(category);
            int statusId = ConvertUtil.ConvertToInt(status);
            //string projectName = project;
            #endregion
          
            List<sp_GetAssetMasterResult> assList = asDao.GetListById(userName, categoryId, statusId, projectName).ToList();

            int totalRecords = assList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
           
            List<sp_GetAssetMasterResult> finalList = asDao.Sort(assList, sortColumn, sortOrder)
                                 .Skip((currentPage - 1) * rowCount)
                                  .Take(rowCount).ToList<sp_GetAssetMasterResult>();

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
                            m.AssetId.ToString(), 
                            m.AssetCategoryName,
                            m.DisplayName,
                            m.StatusName,
                            m.Remark,
                            CommonFunc.ShowActiveImage(m.IsActive, "CRM.changeActiveStatus('/AssetMaster/ChangeActiveStatus/" + 
                                 m.ID +"?isActive=" + !m.IsActive + "', " + (int)MessageType.Error +")"),
                            CommonFunc.ButtonEdit("AssetMaster/Edit")
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportToExcel(string active)
        {
            string searchName = null;
            string columnName = "ID";
            string order = "desc";
            int categoryId = 0;
            int statusId = 0;
            string projectId = null;
            var grid = new GridView();
            int isActive = int.Parse(active);
            List<sp_GetAssetMasterResult> asMasList = new List<sp_GetAssetMasterResult>();
            if (Session[SessionKey.ASSET_DEFAULT_VALUE] != null)
            {
                Hashtable hashData = (Hashtable)Session[SessionKey.ASSET_DEFAULT_VALUE];
                searchName = (string)hashData[Constants.ASSET_LIST_NAME];
                categoryId = !string.IsNullOrEmpty((string)hashData[Constants.ASSET_LIST_CATEGORY]) ? int.Parse((string)hashData[Constants.ASSET_LIST_CATEGORY]) : 0;
                statusId = !string.IsNullOrEmpty((string)hashData[Constants.ASSET_LIST_STATUS]) ? int.Parse((string)hashData[Constants.ASSET_LIST_STATUS]) : 0;
                projectId = (string)hashData[Constants.ASSET_LIST_EMPLOYEE_PROJECT];
               
                if (searchName == Constants.ASSET_MASTER_ASSETID_USERNAME_USERID)
                {
                    searchName = null;
                }
                if (string.IsNullOrEmpty(projectId))
                {
                    projectId = null;
                }
                columnName = !string.IsNullOrEmpty((string)hashData[Constants.ASSET_MASTER_LIST_COLUMN]) ? (string)hashData[Constants.ASSET_MASTER_LIST_COLUMN] : "ID";
                order = !string.IsNullOrEmpty((string)hashData[Constants.ASSET_MASTER_LIST_ORDER]) ? (string)hashData[Constants.ASSET_MASTER_LIST_ORDER] : "desc";
            }
            asMasList = asDao.GetList(searchName, categoryId, statusId, projectId);
            asMasList = asDao.Sort(asMasList, columnName, order);
            ExportExcel exp = new ExportExcel();
            string[] column = new string[] { "AssetId:text", "AssetCategoryName", "DisplayName:text-left", "StatusName:text-left", "Remark:text-left", "IsActive:text-left" };
            string[] header = new string[] { "ID", "Category", "UserName", "Status", "Remark", "IsActive" };

            exp.Title = Constants.ASSET_MASTER_TITLE_EXPORT_EXCEL;
            exp.FileName = Constants.ASSET_MASTER_EXPORT_EXCEL_NAME;
            exp.ColumnList = column;
            exp.HeaderExcel = header;

            exp.List = asMasList;
            exp.IsRenderNo = true;
            exp.Execute();
            return View();
        }

        private void SetSessionFilter(string searchText, string category, string status, string project,
           string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.ASSET_LIST_NAME, searchText);
            hashData.Add(Constants.ASSET_LIST_CATEGORY, category);
            hashData.Add(Constants.ASSET_LIST_STATUS, status);
            hashData.Add(Constants.ASSET_LIST_EMPLOYEE_PROJECT, project);
            
            hashData.Add(Constants.ASSET_MASTER_LIST_COLUMN, column);
            hashData.Add(Constants.ASSET_MASTER_LIST_ORDER, order);
            hashData.Add(Constants.ASSET_MASTER_LIST_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.ASSET_MASTER_LIST_ROW_COUNT, rowCount);

            Session[SessionKey.ASSET_DEFAULT_VALUE] = hashData;
        }

        public JsonResult ChangeActiveStatus(int id, string isActive)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                result.Data = asDao.UpdateActiveStatus(id, bool.Parse(isActive));
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }
		public JsonResult GetPropertyList(int categoryId)
        {
            AssetPropertyDao assPropDao = new AssetPropertyDao();
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            List<AssetProperty> assProp = assPropDao.GetByCategoryId(categoryId);
            string listAssPropIDs = string.Empty;
            foreach (AssetProperty r in assProp)
            {
                listAssPropIDs += r.ID.ToString() + Constants.ASSET_MASTER_COLUMN_SIGN + r.Name + Constants.ASSET_MASTER_COLUMN_SIGN + r.MasterData + Constants.ASSET_MASTER_ROW_SIGN;
            }
            result.Data = listAssPropIDs.TrimEnd(Constants.ASSET_MASTER_ROW_SIGN);
            return result;
        }
        public ActionResult Create()
        {
            AssetCategoryDao assetCatDao = new AssetCategoryDao();
            AssetStatusDao assetStatusDao = new AssetStatusDao();
            Hashtable hashTable = new Hashtable();
            List<AssetCategory> listCate = assetCatDao.GetList(true);
            ViewData[Constants.ASSET_MASTER_CATEGORY_LIST] = new SelectList(listCate, "id", "name",
                hashTable[Constants.ASSET_MASTER_CATEGORY_LIST] == null ? Constants.ASSET_MASTER_FIRST_ITEM_CATEGORY : hashTable[Constants.ASSET_MASTER_CATEGORY_LIST].ToString());
            List<AssetStatus> listStatus = assetStatusDao.GetAssetStatusList();
            ViewData[Constants.ASSET_MASTER_STATUS_LIST] = new SelectList(listStatus, "id", "StatusName",
                hashTable[Constants.ASSET_MASTER_STATUS_LIST] == null ? Constants.ASSET_MASTER_FIRST_ITEM_STATUS : hashTable[Constants.ASSET_MASTER_STATUS_LIST].ToString());

            return View();
        }

        [HttpPost]
        public ActionResult Create(AssetMaster assetMaster)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            assetMaster.UpdatedBy = principal.UserData.UserName;
            assetMaster.UpdateDate = DateTime.Now;
            assetMaster.CreatedBy = principal.UserData.UserName;
            assetMaster.CreateDate = DateTime.Now;

            Message msg = asDao.Insert(assetMaster);
            FileUploadJsonResult result = new FileUploadJsonResult();
            result.Data = new { msg };
            return result;
        }
  
        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = asDao.DeleteList(id, principal.UserData.UserName);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        public ActionResult Refresh(string id)
        {
            string view = string.Empty;
            switch (id)
            {
                case "2":
                    Session.Remove(SessionKey.ASSET_DEFAULT_VALUE);
                    view = "Index";
                    break;
                default:
                    Session.Remove(SessionKey.ASSET_DEFAULT_VALUE);
                    view = "Index";
                    break;
            }
            return RedirectToAction(view);
        }
    }
}
