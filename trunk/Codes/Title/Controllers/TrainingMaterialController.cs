using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Common;
using CRM.Models;
using CRM.Library.Attributes;

namespace CRM.Controllers
{
    public class TrainingMaterialController : BaseController
    {
        #region variables
        private string materialIndexAction = "Dashboard";
        private TrainingCourseDao courseDao = new TrainingCourseDao();
        private TrainingCenterDao tcDao = new TrainingCenterDao();
        private TrainingMaterialDao materialDao = new TrainingMaterialDao();
        #endregion
        //
        // GET: /TrainingMaterial/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /TrainingMaterial/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TrainingMaterial/Create

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Training_Material obj)
        {
            string imageName = Request["ServerImageName"];
            string fileDisplayName = Request["hidFileDisplayName"];
            obj.UploadFile = imageName;
            obj.UploadFileDisplayName = fileDisplayName;
            obj.CreatedBy = obj.UpdatedBy = principal.UserData.UserName;
            Message msg = materialDao.Insert(obj);
            if (msg.MsgType != MessageType.Error && !string.IsNullOrEmpty(obj.UploadFile))
            {
                MoveUploadedFile(imageName);
            }
            ShowMessage(msg);
            return Redirect(Request["returnUrl"] ?? "TrainingMaterial/SubList");
        }

        private void MoveUploadedFile(string fileName)
        {
            string sourcePath = Server.MapPath(Constants.UPLOAD_TEMP_PATH + fileName);
            string destPath = Server.MapPath(Constants.MATERIAL_FOLDER + fileName);
            if (System.IO.File.Exists(sourcePath))
                System.IO.File.Move(sourcePath, destPath);
        }

        private void DeleteOldFile(string fileName)
        {
            string serverPath = Server.MapPath(Constants.MATERIAL_FOLDER + fileName);
            if (System.IO.File.Exists(serverPath))
                System.IO.File.Delete(serverPath);
        }
        //
        // GET: /TrainingMaterial/Edit/5
        [CrmAuthorizeAttribute(Module = Modules.TrainingMaterial, Rights = Permissions.Update)]
        public ActionResult Edit(int id)
        {
            try
            {
                Training_Material obj = materialDao.GetByID(ConvertUtil.ConvertToInt(id));
                if (obj != null)
                {
                    return View(obj);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /TrainingMaterial/Edit/5
        [CrmAuthorizeAttribute(Module = Modules.TrainingMaterial, Rights = Permissions.Update)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Training_Material obj)
        {
            string imageName = Request["ServerImageName"];
            string fileDisplayName = Request["hidFileDisplayName"];
            obj.UploadFile = imageName;
            obj.UploadFileDisplayName = fileDisplayName;
            obj.UpdatedBy = principal.UserData.UserName;
            Training_Material objDb = materialDao.GetByID(obj.ID);
            string oldFile = objDb.UploadFile;
            Message msg = materialDao.Update(obj);
            if (msg.MsgType != MessageType.Error && !string.IsNullOrEmpty(obj.UploadFile) && obj.UploadFile != oldFile)
            {
                MoveUploadedFile(obj.UploadFile);
                DeleteOldFile(oldFile);
            }
            ShowMessage(msg);
            return Redirect(Request.UrlReferrer.ToString());
        }
        [CrmAuthorizeAttribute(Module = Modules.TrainingMaterial, Rights = Permissions.Read)]
        public ActionResult SubList(string type, string key)
        {
            try
            {
                int nType = ConvertUtil.ConvertToInt(type);
                ViewData[CommonDataKey.TRAINING_CENTER_MATERIAL_TYPE] = nType;
                if (nType == Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_PRO_COURSE)
                {
                    ViewData[CommonDataKey.TRAINING_CENTER_MATERIAL_COURSE] = new SelectList(
                        courseDao.GetList(null, Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL, null), "ID", "Name", ConvertUtil.ConvertToInt(key));
                    var course = courseDao.GetById(ConvertUtil.ConvertToInt(key));
                    ViewData[CommonDataKey.TRAINING_CENTER_SUBLIST_NAME] = course!=null? course.Name : null;
                }
                else if (nType == Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_ENGLISH_COURSE)
                {
                    ViewData[CommonDataKey.TRAINING_CENTER_MATERIAL_COURSE] = new SelectList(
                        courseDao.GetList(null, Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH, null), "ID", "Name", ConvertUtil.ConvertToInt(key));
                    var course = courseDao.GetById(ConvertUtil.ConvertToInt(key));
                    ViewData[CommonDataKey.TRAINING_CENTER_SUBLIST_NAME] = course != null ? course.Name : null;
                }
                else if (nType == Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_CATEGORY)
                {
                    ViewData[CommonDataKey.TRAINING_CENTER_SUBLIST_NAME] = string.IsNullOrEmpty(key) ? null : key ;
                    var categoryList = materialDao.GetCategoryList();
                    if(categoryList.Count>0)
                        ViewData[CommonDataKey.TRAINING_CENTER_MATERIAL_CATEGORY] = new SelectList(categoryList, key);
                    else
                        throw new Exception(string.Format(Resources.Message.E0005, "Category " + key, "system"));
                }
                else
                    throw new Exception(Resources.Message.E0007);
                return View();
            }
            catch (Exception ex)
            {
                ShowMessage(string.Format(Resources.Message.E0033, ex.Message), MessageType.Error);
                return RedirectToAction(materialIndexAction);
            }
        }
        /// <summary>
        /// Get material sublist
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingMaterial, Rights = Permissions.Read)]
        public ActionResult GetSubListJQGrid(string name, string type, string key)
        {
            #region JQGrid Params
            //string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            //string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            //SetSessionFilter(name, sortColumn, sortOrder, pageIndex.ToString(), rowCount.ToString());

            if (name.Trim().ToLower().Equals(Constants.TRAINING_CENTER_MATERIAL_TXT_KEYWORD_LABEL.ToLower()))
                name = string.Empty;
            int? nType = null;
            int? nCourseId = null;
            if (!string.IsNullOrEmpty(type) && ConvertUtil.ConvertToInt(type) != 0)
                nType = int.Parse(type);
            if (!string.IsNullOrEmpty(key) &&
                (nType == Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_PRO_COURSE ||
                nType == Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_ENGLISH_COURSE) &&
                ConvertUtil.ConvertToInt(key) != 0)
                nCourseId = int.Parse(key);
            if (string.IsNullOrEmpty(key))
                key = null;
            var list = materialDao.GetList(null, name, nType, nCourseId, key, null);
            int totalRecords = list.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            //Sort the list
            var finalList = list.Skip((pageIndex - 1) * rowCount).Take(rowCount).ToList();

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
                            m.ID.ToString(),
                            CommonFunc.ShowMaterial(m, nType.Value, list.Count, list.IndexOf(m), false)
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Update the dowload times of material
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // [CrmAuthorizeAttribute(Module = Modules.TrainingMaterial, Rights = Permissions.Read, ShowAtCurrentPage=true)]
        public JsonResult UpdateDownloadTimes(string id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                materialDao.UpdateDownloadTimes(ConvertUtil.ConvertToInt(id));
                result.Data = true;
            }
            catch
            {
                result.Data = Resources.Message.E0007;
            }
            return result;
        }
        /// <summary>
        /// Reorder material
        /// </summary>
        /// <param name="id"></param>
        /// <param name="up"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.TrainingMaterial, Rights = Permissions.Update, ShowAtCurrentPage=true)]
        public JsonResult MaterialMove(string id, string up)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                bool isUp = Convert.ToBoolean(up);
                if (ConvertUtil.ConvertToInt(id) == 0)
                    throw new Exception();
                materialDao.Move(materialDao.GetById(int.Parse(id), null), isUp);
                result.Data = true;
            }
            catch
            {
                result.Data = Resources.Message.E0007;
            }
            return result;
        }
        /// <summary>
        /// Delete material
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.TrainingMaterial, Rights = Permissions.Delete, ShowAtCurrentPage=true)]
        public ActionResult Delete(string id)
        {
            Message msg = null;
            string[] idArr = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
            try
            {
                // TODO: Add Delete logic here
                msg = materialDao.Delete(idArr);
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            //ShowMessage(msg);
            //return Redirect(Request.UrlReferrer.ToString());
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        /// <summary>
        /// Dash board
        /// </summary>
        /// <returns></returns>
        public ActionResult Dashboard()
        {

            return View();
        }

        /// <summary>
        /// Material List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MaterialList(string id)
        {
            int type = ConvertUtil.ConvertToInt(id);
            string title = string.Empty;
            
            int count = 0;
            List<sp_TC_GetMaterialListResult> list = materialDao.GetMaterialList(type);
            if (list != null && list.Count > 0)
                count = (from p in list
                         select p.amount).Sum(p => p.Value);
            switch (type)
            {
                case 1:
                    title = "Materials by Professional Courses[{0}]";
                    break;
                case 2:
                    title = "Materials by English Courses[{0}]";
                    break;
                case 3:
                    title = "Materials by Category[{0}]";
                    break;
            }
            title = string.Format(title, count);
            ViewData["type"] = type;
            ViewData["title"] = title;
            return View(list);
        }
    }
}

