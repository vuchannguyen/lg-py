using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Common;
using System.Collections;
using CRM.Models;
using System.Web.UI.WebControls;
using CRM.Library.Attributes;

namespace CRM.Controllers
{
    public class SRCategoryController : BaseController
    {
        //
        // GET: /SRCategory/
        #region variable
        private SRCategoryDao categoryDao = new SRCategoryDao();
        #endregion
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestCategory, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.SR_CATEGORY_DEFAULT_SEARCH_VALUES] == null ?
                null : (Hashtable)Session[SessionKey.SR_CATEGORY_DEFAULT_SEARCH_VALUES];
            ViewData[CommonDataKey.SR_CATEGORY_SEARCH_TEXT] = hashData == null ? 
                Constants.SR_TXT_KEYWORD_LABEL : (string)hashData[CommonDataKey.SR_CATEGORY_SEARCH_TEXT];
            ViewData[CommonDataKey.SR_CATEGORY_SEARCH_SORT_COLUMN] = hashData != null ? (string)hashData[CommonDataKey.SR_CATEGORY_SEARCH_SORT_COLUMN] : "Name";
            ViewData[CommonDataKey.SR_CATEGORY_SEARCH_SORT_ORDER] = hashData != null ? (string)hashData[CommonDataKey.SR_CATEGORY_SEARCH_SORT_ORDER] : "asc";
            ViewData[CommonDataKey.SR_CATEGORY_SEARCH_PAGE_INDEX] = hashData != null ? (string)hashData[CommonDataKey.SR_CATEGORY_SEARCH_PAGE_INDEX] : "1";
            ViewData[CommonDataKey.SR_CATEGORY_SEARCH_ROW_COUNT] = hashData != null ? (string)hashData[CommonDataKey.SR_CATEGORY_SEARCH_ROW_COUNT] : "20";
            return View();
        }

        /// <summary>
        /// Set Session Filter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="category"></param>
        /// <param name="status"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilter(string name, string sortColumn, string sortOrder, string pageIndex, string rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(CommonDataKey.SR_CATEGORY_SEARCH_TEXT, name);
            hashData.Add(CommonDataKey.SR_CATEGORY_SEARCH_SORT_COLUMN, sortColumn);
            hashData.Add(CommonDataKey.SR_CATEGORY_SEARCH_SORT_ORDER, sortOrder);
            hashData.Add(CommonDataKey.SR_CATEGORY_SEARCH_PAGE_INDEX, pageIndex);
            hashData.Add(CommonDataKey.SR_CATEGORY_SEARCH_ROW_COUNT, rowCount);

            Session[SessionKey.SR_CATEGORY_DEFAULT_SEARCH_VALUES] = hashData;
        }
        /// <summary>
        /// Get jqgrid list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="category"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestCategory, Rights = Permissions.Read)]
        public ActionResult GetListJQGrid(string name)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(name, sortColumn, sortOrder, pageIndex.ToString(), rowCount.ToString());

            if (name.Trim().ToLower().Equals(Constants.SR_TXT_KEYWORD_LABEL.ToLower()))
                name = string.Empty;
            
            var categoryList = categoryDao.GetList(name, null, null);
            categoryList = FilterList(categoryList);
            int totalRecords = categoryList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            //Sort the list
            var finalList = categoryDao.Sort(categoryList, sortColumn, sortOrder)
                                  .Skip((pageIndex - 1) * rowCount).Take(rowCount).ToList();
            
            var jsonData = new
            {
                total = totalPages,
                page = pageIndex,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        i = m.ParentId.HasValue ? m.ParentId : m.ID,
                        cell = 
                        m.ParentId.HasValue ?
                        new string[] {
                            m.ParentId.Value.ToString(),
                            true.ToString(),
                            m.ParentName,
                            HttpUtility.HtmlEncode(m.ParentDescription ?? "").Replace("\n","<br/>"),
                            m.ParentOrder.ToString(),
                            CommonFunc.ShowActiveImage(m.IsParentActive, "CRM.changeActiveStatus('" + Url.Action("ChangeActiveStatus") + "/" +
                                m.ParentId +"?isActive=" + !m.IsParentActive + "', " + (int)MessageType.Error +")"),               
                            CommonFunc.Button("edit", "Edit", "CRM.popup('" + Url.Action("Edit") + "/" + m.ParentId + 
                            "', 'Edit Category " + m.ParentName + "', 430)")
                        }
                        : new string[] {
                            m.ID.ToString(),
                            (m.SubCount > 0).ToString(),
                            m.Name,
                            HttpUtility.HtmlEncode(m.Description ?? "").Replace("\n","<br/>"),
                            m.DisplayOrder.ToString(),
                            CommonFunc.ShowActiveImage(m.IsActive, "CRM.changeActiveStatus('" + Url.Action("ChangeActiveStatus") + "/" +
                                m.ID +"?isActive=" + !m.IsActive + "', " + (int)MessageType.Error +")"),               
                            CommonFunc.Button("edit", "Edit", "CRM.popup('" + Url.Action("Edit") + "/" + m.ID + 
                            "', 'Edit Category " + m.Name + "', 430)")
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public List<sp_GetSRCategoryResult> FilterList(List<sp_GetSRCategoryResult> list)
        {
            //Get all parent categories
            var result = list.Where(p => p.ParentId == null).ToList();
            //Get the parent ids
            var parentIds = result.Select(p=>p.ID).ToList();
            foreach (var item in list.Where(p=>p.ParentId.HasValue))
                if (!parentIds.Contains(item.ParentId.Value))
                {
                    result.Add(item);
                    parentIds.Add(item.ParentId.Value);
                }
            return result;
        }
        public ActionResult GetSubList(string id, string name)
        {
            if (string.IsNullOrEmpty(name) || name.Trim().ToLower().Equals(Constants.SR_TXT_KEYWORD_LABEL.ToLower()))
                name = string.Empty;
            var items = categoryDao.GetList(name, ConvertUtil.ConvertToInt(id), null);
            var jsonData = new
            {
                rows = (
                    from m in items
                    select new
                    {
                        i = m.ID,
                        cell = new string[] {
                            m.ID.ToString(),
                            m.Name,
                            HttpUtility.HtmlEncode(m.Description ?? "").Replace("\n","<br/>"),
                            m.DisplayOrder.ToString(),
                            CommonFunc.ShowActiveImage(m.IsActive, "CRM.changeActiveStatus('" + Url.Action("ChangeActiveStatus") + "/" +
                                m.ID +"?isActive=" + !m.IsActive + "', " + (int)MessageType.Error +")"),               
                            CommonFunc.Button("edit", "Edit", "CRM.popup('" + Url.Action("Edit") + "/" + m.ID + 
                            "', 'Edit Category " + m.Name + "', 430)")
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /SRCategory/Create
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestCategory, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create()
        {
            ViewData[CommonDataKey.SR_CATEGORY_LIST] = new SelectList(categoryDao.GetList(null, 0,true), "ID", "Name");
            return View();
        } 

        //
        // POST: /SRCategory/Create
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestCategory, Rights = Permissions.Insert, ShowInPopup = true)]
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Create(SR_Category category)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                // TODO: Add insert logic here
                string parentId = Request[CommonDataKey.SR_CATEGORY_LIST];
                if(!string.IsNullOrEmpty(parentId))
                    category.ParentId =  ConvertUtil.ConvertToInt(parentId);
                category.CreatedBy = category.UpdatedBy = HttpContext.User.Identity.Name;
                result.Data = categoryDao.Insert(category);
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }
        
        //
        // GET: /SRCategory/Edit/5
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestCategory, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Edit(string id)
        {
            SR_Category category = categoryDao.GetById(ConvertUtil.ConvertToInt( id), null);
            ViewData[CommonDataKey.SR_CATEGORY_LIST] = new SelectList(categoryDao.GetList(null, 0, true), 
                "ID", "Name", category == null ? null : category.ParentId);
            return View(category);
        }

        //
        // POST: /SRCategory/Edit/5
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestCategory, Rights = Permissions.Update, ShowInPopup = true)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(SR_Category category)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                // TODO: Add update logic here
                var objDb = categoryDao.GetById(category.ID, null);
                if (objDb.UpdateDate.ToString() != category.UpdateDate.ToString())
                    throw new ArgumentException(string.Format(Resources.Message.E0025, "Category " + objDb.Name));
                string parentId = Request[CommonDataKey.SR_CATEGORY_LIST];
                if (!string.IsNullOrEmpty(parentId))
                    category.ParentId = ConvertUtil.ConvertToInt(parentId);
                else
                    category.ParentId = null;
                category.UpdatedBy = HttpContext.User.Identity.Name;
                result.Data = categoryDao.Update(category);
            }
            catch(Exception ex)
            {
                if(ex.GetType() == typeof(ArgumentException))
                    result.Data = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
                else
                    result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }

        //
        // GET: /SRCategory/Delete/5
        private Message CheckDelete(string[] idArr)
        {
            try
            {
                foreach (string id in idArr)
                {
                    SR_Category cate = categoryDao.GetById(ConvertUtil.ConvertToInt(id), null);
                    if (cate.SR_Categories.Count > 0)
                        return new Message(MessageConstants.E0049, MessageType.Error,
                            "delete all sub categories first");
                    if(cate.SR_ServiceRequests.Count > 0)
                        return new Message(MessageConstants.E0046, MessageType.Error,
                            "delete category " + cate.Name, "it has been assigned to other Service Requests");
                }
                return null;
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);   
            }
        }
        /// <summary>
        /// Delete Categories
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestCategory, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public ActionResult Delete(string id)
        {
            Message msg = null;
            string[] idArr = id.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
            try
            {
                // TODO: Add Delete logic here
                msg = CheckDelete(idArr);
                if (msg == null)
                    msg = categoryDao.Delete(idArr);
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }
        /// <summary>
        /// Change the Active status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ServiceRequestCategory, Rights = Permissions.Update, ShowAtCurrentPage = true)]
        public JsonResult ChangeActiveStatus(int id, string isActive)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                result.Data = categoryDao.UpdateActiveStatus(id, bool.Parse(isActive));
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }
        /// <summary>
        /// Refresh the filter parameters
        /// </summary>
        /// <returns></returns>
        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.SR_CATEGORY_DEFAULT_SEARCH_VALUES);
            return RedirectToAction("Index");
        }
    }
}
