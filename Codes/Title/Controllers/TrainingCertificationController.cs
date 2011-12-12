using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Attributes;
using CRM.Models;
using CRM.Library.Common;
using System.Web.UI.WebControls;

namespace CRM.Controllers
{
    public class TrainingCertificationController : BaseController
    {
        TrainingCertificationDao trainingCerDao = new TrainingCertificationDao();

        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE];

            ViewData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_COLUMN] = hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_COLUMN] == null ? "ID" : hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_COLUMN];
            ViewData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_ORDER] = hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_ORDER] == null ? "desc" : hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_ORDER];
            ViewData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_PAGE_INDEX] = hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_PAGE_INDEX] == null ? "1" : hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_PAGE_INDEX].ToString();
            ViewData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_ROW_COUNT] = hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_ROW_COUNT] == null ? "20" : hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_ROW_COUNT].ToString();
            return View();
        }
         public ActionResult GetListJQGrid(string certificationName)
         {
             #region JQGrid Params
             string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
             string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
             int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
             int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
             #endregion
             SetSessionFilter(certificationName, sortColumn, sortOrder, pageIndex, rowCount);
             string name = "";
             if (certificationName != Constants.TRAINING_CERTIFICATION_MASTER_SEARCH_NAME && !string.IsNullOrEmpty(certificationName))
             {
                 name = certificationName;
             }
             List<sp_GetTrainingCertificationResult> trainingCerList = new TrainingCertificationDao().GetTrainingCertificationList(name).ToList();

             int totalRecords = trainingCerList.Count();
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
             int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

             List<sp_GetTrainingCertificationResult> finalList = trainingCerDao.Sort(trainingCerList, sortColumn, sortOrder).Skip((currentPage - 1) * rowCount)
                                    .Take(rowCount).ToList<sp_GetTrainingCertificationResult>();

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
                             HttpUtility.HtmlEncode(m.ID.ToString()),
                             //HttpUtility.HtmlEncode(m.ID.ToString()),
                            // "<a id=" + m.ID.ToString() + "onclick=\"javascript:CRM.popup('/TrainingCertification/Edit/" + m.ID.ToString() + "', 'Update', 470)/" + m.ID.ToString() + "\">" + HttpUtility.HtmlEncode(m.ID.ToString()) + "</a>",
                             CommonFunc.Link(m.ID.ToString(),"javascript:CRM.popup('/TrainingCertification/Edit/" + m.ID.ToString() + "', 'Edit " + m.Name +" ',470);", m.ID.ToString(),true),
                             HttpUtility.HtmlEncode(m.Name),
                             HttpUtility.HtmlEncode(m.Description),
                             HttpUtility.HtmlEncode(m.CreateDate),
                             HttpUtility.HtmlEncode(m.CreatedBy),
                             HttpUtility.HtmlEncode(m.UpdateDate),
                             HttpUtility.HtmlEncode(m.UpdatedBy),
                             CommonFunc.ShowActiveImage(m.IsActive, "CRM.changeActiveStatus('/TrainingCertification/ChangeActiveStatus/" + 
                                 m.ID +"?isActive=" + !m.IsActive + "', " + (int)MessageType.Error +")"),  
                         }
                     }
                 ).ToArray()
             };
             return Json(jsonData, JsonRequestBehavior.AllowGet);
         }

        private void SetSessionFilter(string certificationName, string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.TRAINING_CERTIFICATION_MASTER_LIST_NAME, certificationName);

            hashData.Add(Constants.TRAINING_CERTIFICATION_MASTER_LIST_COLUMN, column);
            hashData.Add(Constants.TRAINING_CERTIFICATION_MASTER_LIST_ORDER, order);
            hashData.Add(Constants.TRAINING_CERTIFICATION_MASTER_LIST_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.TRAINING_CERTIFICATION_MASTER_LIST_ROW_COUNT, rowCount);

            Session[SessionKey.TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE] = hashData;
        }

        public JsonResult ChangeActiveStatus(int id, string isActive)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                result.Data = trainingCerDao.UpdateActiveStatus(id, bool.Parse(isActive));
            }
            catch
            {
                result.Data = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return result;
        }
        public ActionResult Refresh(string id)
        {
            string view = string.Empty;
            switch (id)
            {
                case "2":
                    Session.Remove(SessionKey.TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE);
                    view = "Index";
                    break;
                default:
                    Session.Remove(SessionKey.TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE);
                    view = "Index";
                    break;
            }
            return RedirectToAction(view);
        }

        public ActionResult ExportToExcel(string active)
        {
            string certificationName = null;
            string columnName = "ID";
            string order = "desc";
            var grid = new GridView();
            int isActive = int.Parse(active);
            List<sp_GetTrainingCertificationResult> trainingCerList = new List<sp_GetTrainingCertificationResult>();
            if (Session[SessionKey.TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE] != null)
            {
                Hashtable hashData = (Hashtable)Session[SessionKey.TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE];
                certificationName = (string)hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_NAME];
                if (certificationName == Constants.TRAINING_CERTIFICATION_MASTER_SEARCH_NAME)
                {
                    certificationName = "";
                }
                columnName = !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_COLUMN]) ? (string)hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_COLUMN] : "ID";
                order = !string.IsNullOrEmpty((string)hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_ORDER]) ? (string)hashData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_ORDER] : "desc";
            }
            trainingCerList = trainingCerDao.GetTrainingCertificationList(certificationName);
            trainingCerList = trainingCerDao.Sort(trainingCerList, columnName, order);
            ExportExcel exp = new ExportExcel();
            string[] column = new string[] { "ID:text", "Name:text-left", "Description:text-left", "CreateDate:date", "CreatedBy:text", "UpdateDate:date", "UpdatedBy:text", "IsActive:text"};
            string[] header = new string[] { "ID", "Certification Name", "Description", "CreateDate", "CreatedBy", "UpdateDate", "UpdatedBy", "IsActive"};

            exp.Title = Constants.TRAINING_CERTIFICATION_TITLE_EXPORT_EXCEL;
            exp.FileName = Constants.TRAINING_CERTIFICATION_EXPORT_EXCEL_NAME;
            exp.ColumnList = column;
            exp.HeaderExcel = header;

            exp.List = trainingCerList;
            exp.IsRenderNo = true;
            exp.Execute();
            return View();
        }

        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = trainingCerDao.DeleteList(id, principal.UserData.UserName);
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        public ActionResult Create()
        {
            Hashtable hashData = Session[SessionKey.TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE];
           
            return View();
        }

      
        [HttpPost]
        //[CrmAuthorizeAttribute(Module = Modules., Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create(Training_CertificationMaster data)
        {

            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            data.UpdatedBy = principal.UserData.UserName;
            data.CreatedBy = principal.UserData.UserName;
            Message msg = trainingCerDao.Insert(data);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            Training_CertificationMaster cus = trainingCerDao.GetById(id);
            Hashtable hashData = Session[SessionKey.TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE];
            
            return View(cus);
        }

        [HttpPost]
        //[CrmAuthorizeAttribute(Module = Modules., Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Edit(Training_CertificationMaster data)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            data.UpdatedBy = principal.UserData.UserName;
            Message msg = trainingCerDao.UpdateTrainingCertification(data);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

    }
}
