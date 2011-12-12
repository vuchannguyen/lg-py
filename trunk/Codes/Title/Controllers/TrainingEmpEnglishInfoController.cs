using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Attributes;
using CRM.Models;
using System.Collections;
using CRM.Library.Common;

namespace CRM.Controllers
{
    public class TrainingEmpEnglishInfoController : BaseController
    {

        private TrainingEmpEnglishInfoDao eeiDao = new TrainingEmpEnglishInfoDao();
        private EmployeeDao empDao = new EmployeeDao();
        
        /// <summary>
        /// List of english infomation
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.EmployeeEnglishInfo, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.TRAINING_EEI_DEFAULT_SEARCH_VALUES] == null ?
                null : (Hashtable)Session[SessionKey.TRAINING_EEI_DEFAULT_SEARCH_VALUES];
            ViewData[CommonDataKey.TRAINING_EEI_SEARCH_TEXT] = hashData == null ?
                Constants.TRAINING_EEI_TXT_KEYWORD_LABEL : (string)hashData[CommonDataKey.TRAINING_EEI_SEARCH_TEXT];

            string selectedType = hashData == null ? null : (string)hashData[CommonDataKey.TRAINING_EEI_SEARCH_TYPE];
            ViewData[CommonDataKey.TRAINING_EEI_SKILL_TYPE_LIST] = new SelectList(eeiDao.GetTypeList(), "ID","Name", selectedType);
            ViewData[CommonDataKey.TRAINING_EEI_SEARCH_SORT_COLUMN] = hashData != null ? (string)hashData[CommonDataKey.TRAINING_EEI_SEARCH_SORT_COLUMN] : "Name";
            ViewData[CommonDataKey.TRAINING_EEI_SEARCH_SORT_ORDER] = hashData != null ? (string)hashData[CommonDataKey.TRAINING_EEI_SEARCH_SORT_ORDER] : "asc";
            ViewData[CommonDataKey.TRAINING_EEI_SEARCH_PAGE_INDEX] = hashData != null ? (string)hashData[CommonDataKey.TRAINING_EEI_SEARCH_PAGE_INDEX] : "1";
            ViewData[CommonDataKey.TRAINING_EEI_SEARCH_ROW_COUNT] = hashData != null ? (string)hashData[CommonDataKey.TRAINING_EEI_SEARCH_ROW_COUNT] : "20";
            return View();
        }
        /// <summary>
        /// Set session filter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilter(string name, string type, string sortColumn, string sortOrder, string pageIndex, string rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(CommonDataKey.TRAINING_EEI_SEARCH_TEXT, name);
            hashData.Add(CommonDataKey.TRAINING_EEI_SEARCH_TYPE, type);
            hashData.Add(CommonDataKey.TRAINING_EEI_SEARCH_SORT_COLUMN, sortColumn);
            hashData.Add(CommonDataKey.TRAINING_EEI_SEARCH_SORT_ORDER, sortOrder);
            hashData.Add(CommonDataKey.TRAINING_EEI_SEARCH_PAGE_INDEX, pageIndex);
            hashData.Add(CommonDataKey.TRAINING_EEI_SEARCH_ROW_COUNT, rowCount);

            Session[SessionKey.TRAINING_EEI_DEFAULT_SEARCH_VALUES] = hashData;
        }
        /// <summary>
        /// Get list JQGrid
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.EmployeeEnglishInfo, Rights = Permissions.Read)]
        public ActionResult GetListJQGrid(string name, string type)
        {
            
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(name, type, sortColumn, sortOrder, pageIndex.ToString(), rowCount.ToString());

            if (name.Trim().ToLower().Equals(Constants.TRAINING_EEI_TXT_KEYWORD_LABEL.ToLower()))
                name = string.Empty;
            int? nType = ConvertUtil.ConvertToInt(type);
            if(nType.Value == 0)
                nType = null;
            var list = eeiDao.GetList(name, nType);
            int totalRecords = list.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            //Sort the list
            var finalList = eeiDao.Sort(list, sortColumn, sortOrder)
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
                        i = m.ID,
                        cell =
                        new string[] {
                            m.ID.ToString(),
                            m.EmployeeId,
                            m.EmployeeName,
                            m.TypeName,
                            m.Score.ToString(),
                            m.ExpireDate.HasValue ? m.ExpireDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "",
                            string.IsNullOrEmpty(m.Notes) ? "" : HttpUtility.HtmlEncode(m.Notes).Replace("\r\n","</br>"),
                            CommonFunc.Button("edit", "Edit", "CRM.popup('" + Url.Action("Edit") + "/" + m.ID + 
                            "', 'Edit English Information for " + m.EmployeeName + "', 550)")
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
             
        }
        /// <summary>
        /// Refresh filter session
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.EmployeeEnglishInfo, Rights = Permissions.Read)]
        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.TRAINING_EEI_DEFAULT_SEARCH_VALUES);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// GET:Create new item
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.EmployeeEnglishInfo, Rights = Permissions.Insert, ShowInPopup=true)]
        public ActionResult Create()
        {
            try
            {
                ViewData[CommonDataKey.TRAINING_EEI_SKILL_TYPE_LIST] = new SelectList(eeiDao.GetTypeList(), "ID", "Name");
                return View();
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        } 
        /// <summary>
        /// POST: create new item
        /// </summary>
        /// <param name="eei"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.EmployeeEnglishInfo, Rights = Permissions.Insert)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Training_EmpEnglishInfo eei)
        {
            Message msg = null;
            try
            {
                // TODO: Add insert logic here
                eei.TypeId = ConvertUtil.ConvertToInt(Request[CommonDataKey.TRAINING_EEI_SKILL_TYPE_LIST]);
                CheckValidEEI(eei, false);
                msg = eeiDao.Insert(eei);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ArgumentException))
                    msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
                else
                    msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Check if the EnglishInfo is valid
        /// </summary>
        /// <param name="eei"></param>
        /// <param name="isUpdate"></param>
        [NonAction]
        private void CheckValidEEI(Training_EmpEnglishInfo eei, bool isUpdate)
        {
            string empName = Request["EmployeeName"].ToLower();
            Employee emp = empDao.GetById(eei.EmployeeId);
            bool error=false;
            double score = 0;
            //Existing type in master data
            var typeList = eeiDao.GetTypeList();
            
            if (
                //Missing Employee name
                string.IsNullOrEmpty(empName) ||
                //Missing Employee Id
                string.IsNullOrEmpty(eei.EmployeeId) || 
                //Employee does not exist
                emp == null ||
                emp.DeleteFlag ||
                emp.EmpStatusId == Constants.RESIGNED ||
                //Employee Id and Employee name is not of the same person
                empDao.FullName(eei.EmployeeId, Constants.FullNameFormat.FirstMiddleLast).ToLower() != empName ||
                //Invalid type
                !typeList.Select(p => p.ID).Contains(eei.TypeId) ||
                
                //Score is incorrect type
                !double.TryParse(Request["Score"], out score)
                )
                error = true;
            //Existing type of employee
            var typeListEmp = eeiDao.GetListByEmpId(eei.EmployeeId).Select(p => p.TypeId);
            if (isUpdate)
            {
                var info = eeiDao.GetById(ConvertUtil.ConvertToInt(Request["eeiId"]));
                if (info == null ||
                    info.UpdateDate.ToString() != eei.UpdateDate.ToString())
                    error = true;
                else
                    typeListEmp = typeListEmp.Where(p => p != info.TypeId);
            }
            if (error) 
                throw new ArgumentException(Resources.Message.E0007);
            //var eeiDb = eeiDao.GetById(eei.ID);
            //Check if the skill of employee exists
            if (typeListEmp.Contains(eei.TypeId))
            {
                throw new ArgumentException(string.Format(Resources.Message.E0048, "The type \"" +
                    typeList.FirstOrDefault(p => p.ID == eei.TypeId).Name + "\" of employee \"" + 
                    empDao.FullName(eei.EmployeeId, Constants.FullNameFormat.FirstMiddleLast) + "\""));
            }
        }
        /// <summary>
        /// GET: Edit item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.EmployeeEnglishInfo, Rights = Permissions.Update, ShowInPopup=true)]
        public ActionResult Edit(string id)
        {
            try
            {
                var eei = eeiDao.GetById(ConvertUtil.ConvertToInt(id));
                ViewData[CommonDataKey.TRAINING_EEI_SKILL_TYPE_LIST] = new SelectList(eeiDao.GetTypeList(), "ID", "Name", eei.TypeId);
                return View(eei);
            }
            catch
            {
                return Content(Resources.Message.E0007);
            }
        }
        /// <summary>
        /// POST: edit item
        /// </summary>
        /// <param name="eei"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.EmployeeEnglishInfo, Rights = Permissions.Update)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Training_EmpEnglishInfo eei)
        {
            Message msg = null;
            try
            {
                // TODO: Add insert logic here
                
                eei.TypeId = ConvertUtil.ConvertToInt(Request[CommonDataKey.TRAINING_EEI_SKILL_TYPE_LIST]);
                CheckValidEEI(eei, true);
                msg = eeiDao.Update(eei);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ArgumentException))
                    msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
                else
                    msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Delete list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.EmployeeEnglishInfo, Rights = Permissions.Delete, ShowAtCurrentPage=true)]
        public ActionResult Delete(string id)
        {
            Message msg = null;
            try
            {
                // TODO: Add delete logic here
                var idArr = id.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
                msg = eeiDao.Delete(idArr.Select(p=>ConvertUtil.ConvertToInt(p)).ToArray());
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return RedirectToAction("Index");
        }
    }
}
