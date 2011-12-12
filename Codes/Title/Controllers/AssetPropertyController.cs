using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using System.Collections;
using CRM.Library.Common;
using CRM.Library.Attributes;

namespace CRM.Controllers
{
    /// <summary>
    /// Asset Property Controller
    /// </summary>
    public class AssetPropertyController : BaseController
    {
        //
        // GET: /AssetProperty/
        #region Variables
        private AssetCategoryDao assetCatDao = new AssetCategoryDao();
        private AssetPropertyDao assetPropDao = new AssetPropertyDao(); 
        #endregion

        #region Methods
        public ActionResult Index()
        {
            Hashtable hashTable = Session[SessionKey.ASSET_PROPERTY] == null ? new Hashtable() : (Hashtable)Session[SessionKey.ASSET_PROPERTY];
            if (hashTable[Constants.ASSET_PROPERTY_SEARCH_TEXT] != null
                && !string.IsNullOrEmpty(hashTable[Constants.ASSET_PROPERTY_SEARCH_TEXT].ToString()))
            {
                ViewData[Constants.ASSET_PROPERTY_SEARCH_TEXT] = hashTable[Constants.ASSET_PROPERTY_SEARCH_TEXT];
            }
            else
            {
                ViewData[Constants.ASSET_PROPERTY_SEARCH_TEXT] = Constants.ASSET_PROPERTY_DEFAULT_SEARCH_TEXT;
            }

            List<AssetCategory> listCate = assetCatDao.GetList(true);
            ViewData[Constants.ASSET_PROPERTY_CATEGORY_LIST] = new SelectList(listCate, "id", "name",
                hashTable[Constants.ASSET_PROPERTY_CATEGORY_LIST] == null ? Constants.SR_LIST_CATEGORY_LABEL : hashTable[Constants.ASSET_PROPERTY_CATEGORY_LIST].ToString());
            ViewData[Constants.ASSET_PROP_LIST_COLUMN] = hashTable[Constants.ASSET_PROP_LIST_COLUMN] == null ? "ID" : hashTable[Constants.ASSET_PROP_LIST_COLUMN].ToString();
            ViewData[Constants.ASSET_PROP_LIST_ORDER] = hashTable[Constants.ASSET_PROP_LIST_ORDER] == null ? "desc" : hashTable[Constants.ASSET_PROP_LIST_ORDER].ToString();
            ViewData[Constants.ASSET_PROP_LIST_PAGE_INDEX] = hashTable[Constants.ASSET_PROP_LIST_PAGE_INDEX] == null ? "1" : hashTable[Constants.ASSET_PROP_LIST_PAGE_INDEX].ToString();
            ViewData[Constants.ASSET_PROP_LIST_ROW_COUNT] = hashTable[Constants.ASSET_PROP_LIST_ROW_COUNT] == null ? "20" : hashTable[Constants.ASSET_PROP_LIST_ROW_COUNT].ToString();

            return View();
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetProperty, Rights = Permissions.Read)]
        public ActionResult GetListJQGrid(string searchText, string categoryId)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(searchText, categoryId, sortColumn, sortOrder, pageIndex, rowCount);

            #region search
            if (searchText == Constants.ASSET_PROPERTY_DEFAULT_SEARCH_TEXT)
            {
                searchText = string.Empty;
            }
            int assetCatId = 0;
            if (!string.IsNullOrEmpty(categoryId))
            {
                assetCatId = int.Parse(categoryId);
            }
            #endregion

            List<sp_GetAssetPropertyResult> assetCatList = assetPropDao.GetList(searchText, assetCatId);

            int totalRecords = assetCatList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

            List<sp_GetAssetPropertyResult> finalList = assetPropDao.Sort(assetCatList, sortColumn, sortOrder)
                                  .Skip((pageIndex - 1) * rowCount)
                                  .Take(rowCount).ToList<sp_GetAssetPropertyResult>();

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
                            HttpUtility.HtmlEncode(m.ID.ToString()), 
                            HttpUtility.HtmlEncode(m.Name),
                            HttpUtility.HtmlEncode(m.AssetCategoryName),
                            HttpUtility.HtmlEncode(m.DisplayOrder.ToString()),
                            HttpUtility.HtmlEncode(m.CreatedBy),
                            HttpUtility.HtmlEncode(m.UpdatedBy),                            
                            CommonFunc.Button("edit", "Edit", "CRM.popup('/AssetProperty/Edit/" + m.ID + "', 'Update', 600)")
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetCategory, Rights = Permissions.Delete)]
        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = assetPropDao.DeleteList(id, principal.UserData.UserName);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetProperty, Rights = Permissions.Insert)]
        public ActionResult Create()
        {
            List<AssetCategory> listCate = assetCatDao.GetList(true);
            ViewData[Constants.ASSET_PROPERTY_CATEGORY_LIST] = new SelectList(listCate, "id", "name", Constants.SR_LIST_CATEGORY_LABEL);
            return View();
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetProperty, Rights = Permissions.Insert)]
        [HttpPost]
        public ActionResult Create(AssetProperty assCat)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            assCat.UpdatedBy = principal.UserData.UserName;
            assCat.UpdateDate = DateTime.Now;
            assCat.CreatedBy = principal.UserData.UserName;
            assCat.CreateDate = DateTime.Now;

            Message msg = assetPropDao.Insert(assCat);
            FileUploadJsonResult result = new FileUploadJsonResult();
            result.Data = new { msg };
            return result;
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetProperty, Rights = Permissions.Update)]
        public ActionResult Edit(string id)
        {
            AssetProperty assCat = assetPropDao.GetById(int.Parse(id));
            if (assCat == null)
                RedirectToAction("Index");
            List<AssetCategory> listCate = assetCatDao.GetList(true);
            ViewData[Constants.ASSET_PROPERTY_CATEGORY_LIST] = new SelectList(listCate, "id", "name", assCat.AssetCategoryId);
            return View(assCat);
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetProperty, Rights = Permissions.Update)]
        [HttpPost]
        public ActionResult Edit(AssetProperty assCat)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            assCat.UpdatedBy = principal.UserData.UserName;
            

            Message msg = assetPropDao.Update(assCat);
            FileUploadJsonResult result = new FileUploadJsonResult();
            result.Data = new { msg };
            return result;
        }

        private void SetSessionFilter(string searchText, string categoryid, string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add(Constants.ASSET_PROPERTY_SEARCH_TEXT, searchText);
            hashTable.Add(Constants.ASSET_PROPERTY_CATEGORY_LIST, searchText);
            hashTable.Add(Constants.ASSET_PROP_LIST_COLUMN, column);
            hashTable.Add(Constants.ASSET_PROP_LIST_ORDER, order);
            hashTable.Add(Constants.ASSET_PROP_LIST_PAGE_INDEX, pageIndex);
            hashTable.Add(Constants.ASSET_PROP_LIST_ROW_COUNT, rowCount);

            Session[SessionKey.ASSET_PROPERTY] = hashTable;
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.ASSET_PROPERTY);
            return RedirectToAction("Index");
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetCategory, Rights = Permissions.Export)]
        public ActionResult Export(string searchText, string categoryId)
        {
            Hashtable hashData = new Hashtable();
            hashData = (Hashtable)Session[SessionKey.ASSET_PROPERTY];
            string sortColumn = hashData[Constants.ASSET_PROP_LIST_COLUMN].ToString();
            string sortOrder = hashData[Constants.ASSET_PROP_LIST_ORDER].ToString();

            List<sp_GetAssetPropertyResult> list = new List<sp_GetAssetPropertyResult>();
            #region search
            if (searchText == Constants.ASSET_PROPERTY_DEFAULT_SEARCH_TEXT)
            {
                searchText = string.Empty;
            }
            int assetCatId = 0;
            if (!string.IsNullOrEmpty(categoryId))
            {
                assetCatId = int.Parse(categoryId);
            }
            #endregion

            list = assetPropDao.GetList(searchText, assetCatId);
            var finalList = assetPropDao.Sort(list, sortColumn, sortOrder);
            string title = string.Empty;
            string[] column_it;
            string[] header_it;

            title = "Asset Property List";
            column_it = new string[] { "ID:text", "Name:text", "AssetCategoryName:text", "MasterData:text" };
            header_it = new string[] { "ID", "Asset Property", "Category", "Master Data" };

            ExportExcelAdvance exp = new ExportExcelAdvance();
            exp.Sheets = new List<CExcelSheet>{ new CExcelSheet{ Name=title, 
                                                    List= finalList,
                                                    Header = header_it, 
                                                    ColumnList= column_it,
                                                    Title = title,                
                                                    IsRenderNo = true}};
            title = title.Replace(" ", "_");
            string filepath = Server.MapPath("~/Export/") + title + DateTime.Now.Ticks.ToString() + ".xlsx";
            string filename = exp.ExportExcelMultiSheet(filepath);
            filename = title + ".xlsx";

            return new ExcelResult { FileName = filename, Path = filepath };
        }
        #endregion
    }
}
