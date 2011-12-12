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
    public class AssetCategoryController : BaseController
    {
        private AssetCategoryDao assetCatDao = new AssetCategoryDao();

        [CrmAuthorizeAttribute(Module = Modules.AssetCategory, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashTable = Session[SessionKey.ASSET_CATEGORY] == null ?
                new Hashtable() : (Hashtable)Session[SessionKey.ASSET_CATEGORY];
            if (hashTable[Constants.ASSET_CATEGORY_SEARCH_TEXT] != null
                && !string.IsNullOrEmpty(hashTable[Constants.ASSET_CATEGORY_SEARCH_TEXT].ToString()))
            {
                ViewData[Constants.ASSET_CATEGORY_SEARCH_TEXT] = hashTable[Constants.ASSET_CATEGORY_SEARCH_TEXT];
            }
            else
            {
                ViewData[Constants.ASSET_CATEGORY_SEARCH_TEXT] = Constants.ASSET_CATEGORY_NAME_OR_DESCRIPTION;
            }
            ViewData[Constants.ASSET_CAT_LIST_COLUMN] = hashTable[Constants.ASSET_CAT_LIST_COLUMN] == null ? "ID" : hashTable[Constants.ASSET_CAT_LIST_COLUMN].ToString();
            ViewData[Constants.ASSET_CAT_LIST_ORDER] = hashTable[Constants.ASSET_CAT_LIST_ORDER] == null ? "desc" : hashTable[Constants.ASSET_CAT_LIST_ORDER].ToString();
            ViewData[Constants.ASSET_CAT_LIST_PAGE_INDEX] = hashTable[Constants.ASSET_CAT_LIST_PAGE_INDEX] == null ? "1" : hashTable[Constants.ASSET_CAT_LIST_PAGE_INDEX].ToString();
            ViewData[Constants.ASSET_CAT_LIST_ROW_COUNT] = hashTable[Constants.ASSET_CAT_LIST_ROW_COUNT] == null ? "20" : hashTable[Constants.ASSET_CAT_LIST_ROW_COUNT].ToString();
            
                        
            return View();
        }

        private void SetSessionFilter(string searchText, string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add(Constants.ASSET_CATEGORY_SEARCH_TEXT, searchText);            
            hashTable.Add(Constants.ASSET_CAT_LIST_COLUMN, column);
            hashTable.Add(Constants.ASSET_CAT_LIST_ORDER, order);
            hashTable.Add(Constants.ASSET_CAT_LIST_PAGE_INDEX, pageIndex);
            hashTable.Add(Constants.ASSET_CAT_LIST_ROW_COUNT, rowCount);

            Session[SessionKey.ASSET_CATEGORY] = hashTable;
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetCategory, Rights = Permissions.Read)]
        public ActionResult GetListJQGrid(string searchText)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(searchText, sortColumn, sortOrder, pageIndex, rowCount);

            #region search            
            if (searchText == Constants.ASSET_CATEGORY_NAME_OR_DESCRIPTION)
            {
                searchText = string.Empty;
            }
            #endregion

            List<AssetCategory> assetCatList = assetCatDao.GetList(searchText);

            int totalRecords = assetCatList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

            List<AssetCategory> finalList = assetCatDao.Sort(assetCatList, sortColumn, sortOrder)
                                  .Skip((pageIndex - 1) * rowCount)
                                  .Take(rowCount).ToList<AssetCategory>();

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
                            HttpUtility.HtmlEncode(m.Description),
                            //CommonFunc.ShowActiveImage(m.IsActive),
                            CommonFunc.ShowActiveImage(m.IsActive, "CRM.changeActiveStatus('/AssetCategory/ChangeActiveStatus/" + 
                                m.ID +"?isActive=" + !m.IsActive + "', " + (int)MessageType.Error +")"),
                            HttpUtility.HtmlEncode(m.CreatedBy),
                            HttpUtility.HtmlEncode(m.UpdatedBy),                            
                            CommonFunc.Button("edit", "Edit", "CRM.popup('/AssetCategory/Edit/" + m.ID + "', 'Update', 400)")
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeActiveStatus(int id, string isActive)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                result.Data = assetCatDao.UpdateActiveStatus(id, bool.Parse(isActive));
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetCategory, Rights = Permissions.Delete)]
        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = assetCatDao.DeleteList(id,principal.UserData.UserName );
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetCategory, Rights = Permissions.Insert)]
        public ActionResult Create()
        {
            return View();
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetCategory, Rights = Permissions.Insert)]
        [HttpPost]
        public ActionResult Create(AssetCategory assCat)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            assCat.UpdatedBy = principal.UserData.UserName;
            assCat.UpdateDate = DateTime.Now;
            assCat.CreatedBy = principal.UserData.UserName;
            assCat.CreateDate = DateTime.Now;

            Message msg = assetCatDao.Insert(assCat);
            FileUploadJsonResult result = new FileUploadJsonResult();
            result.Data = new { msg };
            return result;
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetCategory, Rights = Permissions.Update)]
        public ActionResult Edit(string id)
        {
            AssetCategory assCat = assetCatDao.GetById(int.Parse(id));
            return View(assCat);
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetCategory, Rights = Permissions.Update)]
        [HttpPost]
        public ActionResult Edit(AssetCategory assCat)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            assCat.UpdatedBy = principal.UserData.UserName;
            
            Message msg = assetCatDao.Update(assCat);
            FileUploadJsonResult result = new FileUploadJsonResult();
            result.Data = new { msg };
            return result;
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetCategory, Rights = Permissions.Read)]
        public ActionResult PropertyList()
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

        private void SetSessionFilter4Property(string searchText, string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add(Constants.ASSET_PROPERTY_SEARCH_TEXT, searchText);
            hashTable.Add(Constants.ASSET_PROP_LIST_COLUMN, column);
            hashTable.Add(Constants.ASSET_PROP_LIST_ORDER, order);
            hashTable.Add(Constants.ASSET_PROP_LIST_PAGE_INDEX, pageIndex);
            hashTable.Add(Constants.ASSET_PROP_LIST_ROW_COUNT, rowCount);

            Session[SessionKey.ASSET_PROPERTY] = hashTable;
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.ASSET_CATEGORY);
            return RedirectToAction("Index");
        }

        [CrmAuthorizeAttribute(Module = Modules.AssetCategory, Rights = Permissions.Export)]
        public ActionResult Export(string text)
        {
            Hashtable hashData = new Hashtable();
            hashData = (Hashtable)Session[SessionKey.ASSET_CATEGORY];
            string sortColumn = hashData[Constants.ASSET_CAT_LIST_COLUMN].ToString();
            string sortOrder = hashData[Constants.ASSET_CAT_LIST_ORDER].ToString();

            List<AssetCategory> list = new List<AssetCategory>();
            if (!string.IsNullOrEmpty(text.Trim()) && text != Constants.ASSET_CATEGORY_NAME_OR_DESCRIPTION)
                text = text.Trim().ToLower();
            else
                text = string.Empty;
            
            list = assetCatDao.GetList(text);
            var finalList = assetCatDao.Sort(list, sortColumn, sortOrder);
            string title = string.Empty;
            string[] column_it;
            string[] header_it;

            title = "Asset Category List";
            column_it = new string[] { "ID:text", "Name:text", "Description:text", "IsActive" };
            header_it = new string[] { "ID", "Asset Category", "Description", "IsActive" };

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
    }
}
