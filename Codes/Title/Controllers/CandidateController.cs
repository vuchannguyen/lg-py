using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Attributes;
using System.IO;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Globalization;
namespace CRM.Controllers
{
    public class CandidateController : BaseController
    {
        #region Local Variable
        private JobTitleLevelDao levelDao = new JobTitleLevelDao();
        private CandidateDao candidateDao = new CandidateDao();
        private LocationDao locationDao = new LocationDao();
        #endregion
       
        #region "View"
        [CrmAuthorizeAttribute(Module = Modules.Candidate, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.CANDIDATE_FILTER] == null ? new Hashtable() : (Hashtable)Session[SessionKey.CANDIDATE_FILTER];
            ViewData[Constants.CANDIDATE_LIST_NAME] = hashData[Constants.CANDIDATE_LIST_NAME] == null ? Constants.CANDIDATE_NAME : !string.IsNullOrEmpty((string)hashData[Constants.CANDIDATE_LIST_NAME]) ? hashData[Constants.CANDIDATE_LIST_NAME] : Constants.CANDIDATE_NAME;
            
            ViewData[Constants.LOCATION_LIST_OFFICE] = new SelectList(locationDao.GetListOfficeAll(true, false), "ID", "Name", hashData[Constants.LOCATION_LIST_OFFICE] == null ? Constants.FIRST_ITEM_OFFICE : hashData[Constants.LOCATION_LIST_OFFICE]);

            ViewData[Constants.CANDIDATE_LIST_JOB_TITLE] = new SelectList(levelDao.GetList(), "ID", "DisplayName", hashData[Constants.CANDIDATE_LIST_JOB_TITLE] == null ? Constants.FIRST_ITEM_JOBTITLE : hashData[Constants.CANDIDATE_LIST_JOB_TITLE]);
            ViewData[Constants.CANDIDATE_LIST_FROM_DATE] = hashData[Constants.CANDIDATE_LIST_FROM_DATE] == null ? "" : hashData[Constants.CANDIDATE_LIST_FROM_DATE];
            ViewData[Constants.CANDIDATE_LIST_TO_DATE] = hashData[Constants.CANDIDATE_LIST_TO_DATE] == null ? "" : hashData[Constants.CANDIDATE_LIST_TO_DATE];
            ViewData[Constants.CANDIDATE_LIST_SOURCE] = new SelectList(candidateDao.GetListSource(), "Value", "Text", hashData[Constants.CANDIDATE_LIST_SOURCE] == null ? Constants.CANDIDATE_SOURCE : hashData[Constants.CANDIDATE_LIST_SOURCE]);
            ViewData[Constants.CANDIDATE_LIST_STATUS] = new SelectList(Constants.GetCandidateStatus, "Value", "Text", hashData[Constants.CANDIDATE_LIST_STATUS] == null ? Constants.FIRST_ITEM_STATUS : hashData[Constants.CANDIDATE_LIST_STATUS]);
            ViewData[Constants.CANDIDATE_LIST_UNIVERSITY] = new SelectList(candidateDao.GetUniversityList(""), "ID", "Name", hashData[Constants.CANDIDATE_LIST_UNIVERSITY] == null ? Constants.FIRST_ITEM_STATUS : hashData[Constants.CANDIDATE_LIST_UNIVERSITY]);
            ViewData[Constants.CANDIDATE_LIST_COLUMN] = hashData[Constants.CANDIDATE_LIST_COLUMN] == null ? "SearchDate" : hashData[Constants.CANDIDATE_LIST_COLUMN].ToString();
            ViewData[Constants.CANDIDATE_LIST_ORDER] = hashData[Constants.CANDIDATE_LIST_ORDER] == null ? "desc" : hashData[Constants.CANDIDATE_LIST_ORDER].ToString();
            ViewData[Constants.CANDIDATE_LIST_PAGE_INDEX] = hashData[Constants.CANDIDATE_LIST_PAGE_INDEX] == null ? "1" : hashData[Constants.CANDIDATE_LIST_PAGE_INDEX].ToString();
            ViewData[Constants.CANDIDATE_LIST_ROW_COUNT] = hashData[Constants.CANDIDATE_LIST_ROW_COUNT] == null ? "20" : hashData[Constants.CANDIDATE_LIST_ROW_COUNT].ToString();
            return View();
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.CANDIDATE_FILTER);
            return RedirectToAction("Index");
        }

        [CrmAuthorizeAttribute(Module = Modules.Candidate, Rights = Permissions.Read)]
        public ActionResult Detail(string id,string urlback)
        {
            Candidate candidateObj = candidateDao.GetById(id);
            List<int> intList = GetListCandidateForNavigation();
            ViewData["ListInter"] = intList;
            //duyhung.nguyen added to back to the exam list
            if (!string.IsNullOrEmpty(urlback))
            {
                ViewData["BackToExamURL"] = urlback;
            }
            var listExam = new ExamDao().GetListByCandidateId(ConvertUtil.ConvertToInt(id));
            ViewData["ExamList"] = listExam;
            return View(candidateObj);
        }

        private void SetSessionFilter(string can_name, string office, string source, string titleId, string status, string from_date, string to_date, string column, string order, int pageIndex, int rowCount, string university)
        {
            Hashtable candidateState = new Hashtable();
            candidateState.Add(Constants.CANDIDATE_LIST_NAME, can_name);
            
            candidateState.Add(Constants.LOCATION_LIST_OFFICE, office);
            
            candidateState.Add(Constants.CANDIDATE_LIST_SOURCE, source);
            candidateState.Add(Constants.CANDIDATE_LIST_JOB_TITLE, titleId);
            candidateState.Add(Constants.CANDIDATE_LIST_STATUS, status);
            candidateState.Add(Constants.CANDIDATE_LIST_FROM_DATE, from_date);
            candidateState.Add(Constants.CANDIDATE_LIST_TO_DATE, to_date);
            candidateState.Add(Constants.CANDIDATE_LIST_COLUMN, column);
            candidateState.Add(Constants.CANDIDATE_LIST_ORDER, order);
            candidateState.Add(Constants.CANDIDATE_LIST_PAGE_INDEX, pageIndex);
            candidateState.Add(Constants.CANDIDATE_LIST_ROW_COUNT, rowCount);
            candidateState.Add(Constants.CANDIDATE_LIST_UNIVERSITY, university);
            Session[SessionKey.CANDIDATE_FILTER] = candidateState;
        }
        public ActionResult GetListJQGrid(string can_name, string office, string source, string titleId, string status, string from_date, string to_date, string university)
        {

            #region JQGrid Params

            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            SetSessionFilter(can_name, office, source, titleId, status, from_date, to_date, sortColumn, sortOrder, pageIndex, rowCount, university);
            #endregion
            #region search
            string candidate_name = String.Empty;
            int source_search = 0;
            int title = 0;
            int statusId = 0;
            string from = String.Empty;
            string to = String.Empty;
            int universityId = 0;
            if (can_name != Constants.CANDIDATE_NAME)
            {
                candidate_name = can_name;
            }
            if (!string.IsNullOrEmpty(source))
            {
                source_search = int.Parse(source);
            }
            if (!string.IsNullOrEmpty(titleId))
            {
                title = int.Parse(titleId);
            }
            if (!string.IsNullOrEmpty(from_date))
            {
                from = from_date;
            }
            if (!string.IsNullOrEmpty(to_date))
            {
                to = to_date;
            }
            if (!string.IsNullOrEmpty(status))
            {
                statusId = int.Parse(status);
            }
            if (!string.IsNullOrEmpty(university))
            {
                universityId = int.Parse(university);
            }
            //parse location code
            int officeId = ConvertUtil.ConvertToInt(office);            

            #endregion
            #region GetList
            List<sp_GetCandidateResult> CandidateList = candidateDao.GetList(candidate_name, source_search, title, statusId, from, to, universityId, officeId);

            int totalRecords = CandidateList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            var finalList = candidateDao.Sort(CandidateList, sortColumn, sortOrder)
                                  .Skip((currentPage - 1) * rowCount)
                                   .Take(rowCount);
            #endregion
            
            int j = 0;
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
                                Convert.ToString(j+=1),
                                m.ID.ToString(), 
                                CommonFunc.Link(m.ID.ToString(),"/Candidate/Detail/"+m.ID,m.DisplayName,true), 
                                (m.DOB.HasValue)?m.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",
                                m.CellPhone,
                                m.Gender == Constants.MALE?"Male":"Female",
                                m.SearchDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                                m.SourceName,
                                m.Title.ToString(),
                                 m.University,
                                CommonFunc.GetCandidateStatus(m.Status),
                                CommonFunc.ButtonEdit("/Candidate/Edit/"+m.ID.ToString())+ "  "+
                                ButtonTransfer("/Interview/Create/"+m.ID.ToString(),(CandidateStatus)m.Status) 
                            }
                    }
                ).ToArray()
            };
            
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get list interview for navigation
        /// </summary>
        /// <returns>List<int></returns>
        private List<int> GetListCandidateForNavigation()
        {
            List<int> interList = null;
            
            if (Session[SessionKey.CANDIDATE_FILTER] != null)
            {
                Hashtable hashData = (Hashtable)Session[SessionKey.CANDIDATE_FILTER];
                string name = (string)hashData[Constants.CANDIDATE_LIST_NAME];
                if (name == Constants.CANDIDATE_NAME)
                {
                    name = string.Empty;
                }

                int source = (!string.IsNullOrEmpty((string)hashData[Constants.CANDIDATE_LIST_SOURCE]) ? int.Parse((string)hashData[Constants.CANDIDATE_LIST_SOURCE]) : 0);
                int position = (!string.IsNullOrEmpty((string)hashData[Constants.CANDIDATE_LIST_JOB_TITLE]) ? int.Parse((string)hashData[Constants.CANDIDATE_LIST_JOB_TITLE]) : 0);
                int status = (!string.IsNullOrEmpty((string)hashData[Constants.CANDIDATE_LIST_STATUS]) ? int.Parse((string)hashData[Constants.CANDIDATE_LIST_STATUS]) : 0);
                string from = "";
                if (!string.IsNullOrEmpty((string)hashData[Constants.CANDIDATE_LIST_FROM_DATE]))
                    from = (string)hashData[Constants.CANDIDATE_LIST_FROM_DATE];
                string to = "";
                if (!string.IsNullOrEmpty((string)hashData[Constants.CANDIDATE_LIST_TO_DATE]))
                    to = (string)hashData[Constants.CANDIDATE_LIST_TO_DATE];
                string column = !string.IsNullOrEmpty((string)hashData[Constants.CANDIDATE_LIST_COLUMN]) ?
                    (string)hashData[Constants.CANDIDATE_LIST_COLUMN] : "ID";
                string order = !string.IsNullOrEmpty((string)hashData[Constants.CANDIDATE_LIST_ORDER]) ?
                    (string)hashData[Constants.CANDIDATE_LIST_ORDER] : "desc";
                 int university = (!string.IsNullOrEmpty((string)hashData[Constants.CANDIDATE_LIST_UNIVERSITY]) ? int.Parse((string)hashData[Constants.CANDIDATE_LIST_UNIVERSITY]) : 0);
                List<sp_GetCandidateResult> list = candidateDao.GetList(name, source, position, status, from, to,university);
                var flist = candidateDao.Sort(list, column, order);
                interList = flist.Select(p => p.ID).ToList();
            }
            else
            {
                interList = candidateDao.GetList("", 0,0, 0, null, null,0).Select(p => p.ID).ToList();
            }

            return interList;
        }

        private string ButtonTransfer(string actionUrl,CandidateStatus status)
        {
            if (status == CandidateStatus.Failed || status == CandidateStatus.Available || status == CandidateStatus.Waiting)
                return "<input type=\"button\" class=\"icon transfer\" title=\"Setup an Interview\" onclick=\"window.location = '" + actionUrl + "'\" />";
            else
                return "      ";

        }
        [CrmAuthorizeAttribute(Module = Modules.Candidate, Rights = Permissions.Insert)]
        #endregion
        #region "Create"

        public ActionResult Create()
        {
            setCombobox();
            candidateDao.UpdateStatus(CandidateStatus.Failed, 388);
            return View();
        }
        private void setCombobox()
        {
            ViewData["titleId"] = new SelectList(levelDao.GetList(), "ID", "DisplayName", "");
            ViewData["Status"] = new SelectList(Constants.CandidateStatusList, "Value", "Text");
            ViewData["Gender"] = new SelectList(Constants.Gender, "Value", "Text", "");
            ViewData["SourceId"] = new SelectList(candidateDao.GetListSource(), "Value", "Text", "");
            ViewData["BranchId"] = new SelectList(locationDao.GetListBranchAll(true, false), "ID", "Name");
            ViewData["OfficeID"] = new SelectList(locationDao.GetListOfficeAll(true, false), "ID", "Name");
        }
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Candidate, Rights = Permissions.Insert)]
        public ActionResult Create(Candidate candidateObj)
        {
            Message msg = null;
            if (!String.IsNullOrEmpty(candidateObj.DOB.ToString()))
            { 
                string vnName=candidateObj.VnFirstName+candidateObj.VnMiddleName+candidateObj.VnLastName;
                string engName=candidateObj.FirstName+candidateObj.MiddleName+candidateObj.LastName;
                if (candidateDao.GetByNameAndDOB(vnName,engName,candidateObj.DOB.ToString()).Count > 0)
                {
                    msg = new Message(MessageConstants.E0048, MessageType.Error, "This candidate (the information of Name and DOB)");
                    ShowMessage(msg);
                    setCombobox();
                    candidateDao.UpdateStatus(CandidateStatus.Failed, 388);
                    return View();
                }
            }
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            // TODO: Add insert logic here
            if(!string.IsNullOrEmpty(Request["UniversityId"]))
            {
                candidateObj.UniversityId =int.Parse(Request["UniversityId"]);
            }
            candidateObj.CreateDate = DateTime.Now;
            candidateObj.UpdateDate = DateTime.Now;
            candidateObj.CreatedBy = principal.UserData.UserName;
            candidateObj.UpdatedBy = principal.UserData.UserName;
            msg = candidateDao.Inser(candidateObj);
            if (msg.MsgType != MessageType.Error)
            {
                string tempPathPhoto = Server.MapPath(Constants.UPLOAD_TEMP_PATH + candidateObj.Photograph);
                string candidatePathPhoto = Server.MapPath(Constants.PHOTO_PATH_ROOT_CANDIDATE + candidateObj.Photograph);
                if (System.IO.File.Exists(tempPathPhoto))
                {
                    System.IO.File.Move(tempPathPhoto, candidatePathPhoto);
                }
                string tempPathCV = Server.MapPath(Constants.UPLOAD_TEMP_PATH + candidateObj.CVFile);
                string candidatePathCV = Server.MapPath(Constants.CV_PATH_ROOT_CANDIDATE + candidateObj.CVFile);
                if (System.IO.File.Exists(tempPathCV))
                {
                    System.IO.File.Move(tempPathCV, candidatePathCV);
                }

                // remove filter conditions
                Session.Remove(SessionKey.CANDIDATE_FILTER);
            }

            ShowMessage(msg);
            return RedirectToAction("Index");

        }
        public JsonResult GetOfficeList(string id, string selectedValue)
        {
            JsonResult result = new JsonResult ();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            string sNewOptionTemplate = "<option value='{0}' {1}>{2}</option>";
            string sNewOptions = string.Format(sNewOptionTemplate, "", "", Constants.FIRST_ITEM);
            //string sNewOptions = "";
            try
            {
                int tmpId = CheckUtil.IsInteger(id) ? int.Parse(id) : 0;
                int tmpSelectedValue = CheckUtil.IsInteger(selectedValue) ? int.Parse(selectedValue) : 0;
                
                var officeList = locationDao.GetListOfficeByBranchId(tmpId, true, false);
                foreach (var item in officeList)
                {
                    sNewOptions += string.Format(sNewOptionTemplate, item.ID, 
                        item.ID == tmpSelectedValue ? "selected" : "", item.Name);
                }
                result.Data = sNewOptions;
            }
            catch
            {
                result.Data = "";
            }
            return result;
        }
        #endregion
        #region "Edit"

        [CrmAuthorizeAttribute(Module = Modules.Candidate, Rights = Permissions.Update)]
        public ActionResult Edit(string id)
        {
            Candidate candidateObj = candidateDao.GetById(id);
            ViewData["titleId"] = new SelectList(levelDao.GetList(), "ID", "DisplayName", candidateObj.TitleId);
            ViewData["Status"] = new SelectList(Constants.CandidateStatusList, "Value", "Text", candidateObj.Status);
            ViewData["Gender"] = new SelectList(Constants.Gender, "Value", "Text", candidateObj.Gender);
            ViewData["SourceId"] = new SelectList(candidateDao.GetListSource(), "Value", "Text", candidateObj.SourceId);
            int iBranch = candidateObj.OfficeID.HasValue ? candidateObj.Office.BranchID : 0;
            int iOffice = candidateObj.OfficeID.HasValue ? candidateObj.OfficeID.Value : 0;
            ViewData["BranchId"] = new SelectList(locationDao.GetListBranchAll(true, false), "ID", "Name", iBranch);
            ViewData["OfficeID"] = new SelectList(locationDao.GetListOfficeAll(true, false), "ID", "Name", iOffice);
            Candidate viewData = candidateDao.GetById(id);
            return View(viewData);
        }

        [CrmAuthorizeAttribute(Module = Modules.Candidate, Rights = Permissions.Update)]
        public ActionResult EditPop(string id)
        {
            Candidate candidateObj = candidateDao.GetById(id);
            ViewData["titleId"] = new SelectList(levelDao.GetList(), "ID", "DisplayName", candidateObj.TitleId);
            ViewData["Status"] = new SelectList(Constants.CandidateStatusList, "Value", "Text", candidateObj.Status);
            ViewData["Gender"] = new SelectList(Constants.Gender, "Value", "Text", candidateObj.Gender);
            ViewData["SourceId"] = new SelectList(candidateDao.GetListSource(), "Value", "Text", candidateObj.SourceId);
            Candidate viewData = candidateDao.GetById(id);
            return View(viewData);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Candidate, Rights = Permissions.Update)]
        public ActionResult Edit(Candidate candidateObj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            candidateObj.UpdatedBy = principal.UserData.UserName;
            Message msg = candidateDao.Update(candidateObj);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Candidate, Rights = Permissions.Update)]
        public ActionResult EditPop(Candidate candidateObj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            candidateObj.UpdatedBy = principal.UserData.UserName;
            Message msg = candidateDao.Update(candidateObj);
            ShowMessage(msg);
            return RedirectToAction("Detail/" + candidateObj.ID);            
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Candidate, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = candidateDao.DeleteList(id, principal.UserData.UserName);
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }
        #endregion
        #region "General Candidate"
        public ActionResult CandidateToolTip(string id)
        {
            Candidate candidateObj = candidateDao.GetById(id);
            return View(candidateObj);
        }
        #endregion
        #region ExportToExcel
        #region Demo Excel Component
        //private List<ArrayList> ConvertCandidateResultToList()
        //{

        //    List<ArrayList> listOfArray = new List<ArrayList>();
        //    List<sp_GetCandidateResult> result = candidateDao.GetList();
        //    int i = 1;
        //    foreach (sp_GetCandidateResult row in result)
        //    {
        //        ArrayList data = new ArrayList();
        //        data.Add(i);
        //        data.Add(row.DisplayName);
        //        data.Add(row.DOB);
        //        data.Add(row.CellPhone);
        //        data.Add(row.Email);
        //        data.Add(row.SearchBy);
        //        data.Add(row.SearchDate);
        //        data.Add(row.Source);
        //        data.Add(row.Title);
        //        data.Add(row.Address);
        //        data.Add(row.Note);
        //        listOfArray.Add(data);
        //        i++;
        //    }
        //    return listOfArray;

        //}

        //public JsonResult ExportToExcel()
        //{
        //    JsonResult jresult = new JsonResult();
        //    jresult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //    Export export = new Export();

        //    export.ColumLetters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
        //    export.IndexRowTemplate = 7;
        //    export.TemplateExcelFilePath = Server.MapPath(Constants.EXCEL_TEMPLATE_PATH_CANDIDATE);
        //    export.SaveAsExportFilePath = Server.MapPath(Constants.EXCEL_EXPORT_PATH_CANDIDATE);

        //    if (export.ToExcelCandidate(candidateDao.GetList()))
        //        jresult.Data = true;
        //    else
        //    {
        //        jresult.Data = false;
        //    }

        //    if (export.ToExcel(ConvertCandidateResultToList()))
        //        jresult.Data = true;
        //    else
        //    {
        //        jresult.Data = false;
        //    }



        //    return jresult;
        //}
        #endregion

        public ActionResult ExportToExcel(string can_name, string source, string titleId,string status, string from_date, string to_date,string university, string officeId)
        {
           
                #region filter
                string candidate_name = String.Empty;
                int source_search = 0;
                int title = 0;
                string from = String.Empty;
                string to = String.Empty;
                int statusId = 0;
                int universityId = 0;
                if (can_name != Constants.CANDIDATE_NAME)
                {
                    candidate_name = can_name;
                }
                if (!string.IsNullOrEmpty(source))
                {
                    source_search = int.Parse(source);
                }
                if (!string.IsNullOrEmpty(titleId))
                {
                    title = int.Parse(titleId);
                }
                if (!string.IsNullOrEmpty(from_date))
                {
                    from = from_date;
                }
                if (!string.IsNullOrEmpty(to_date))
                {
                    to = to_date;
                }
                if (!string.IsNullOrEmpty(status))
                {
                    statusId = int.Parse(status);
                }
                if (!string.IsNullOrEmpty(university))
                {
                    universityId = int.Parse(university);
                }
                #endregion

                List<sp_GetCandidateResult> CandidateList = candidateDao.GetList(candidate_name, source_search, title,statusId, 
                    from, to,universityId, ConvertUtil.ConvertToInt(officeId) );
                Hashtable hashData = (Hashtable)Session[SessionKey.CANDIDATE_FILTER];
                string column = !string.IsNullOrEmpty((string)hashData[Constants.CANDIDATE_LIST_COLUMN]) ?
                    (string)hashData[Constants.CANDIDATE_LIST_COLUMN] : "ID";
                string order = !string.IsNullOrEmpty((string)hashData[Constants.CANDIDATE_LIST_ORDER]) ?
                    (string)hashData[Constants.CANDIDATE_LIST_ORDER] : "desc";

                List<sp_GetCandidateResult> finalList = candidateDao.Sort(CandidateList, column, order).ToList<sp_GetCandidateResult>();

                ExportExcel exp = new ExportExcel();
                exp.Title = Constants.CANDIDATE_TILE_EXPORT_EXCEL;
                exp.FileName = Constants.CANDIDATE_EXPORT_EXCEL_NAME;
                exp.ColumnList = new string[] { "DisplayNameExport","VNNameExport", "DOB:Date", "CellPhone:Text", "Email", 
                "SearchDate:Date","Status:Candidate","Gender:Gender", "SourceName", "Title","University", "Address", "Note" };
                exp.HeaderExcel = new string[]{ "Full Name","VN Name", "DOB", "CellPhone", "Email", 
                "Searched Date","Status","Gender", "Source", "Title","University", "Address", "Remarks" };
                exp.List = finalList;
                exp.IsRenderNo = true;
                exp.Execute();
            return View();
        }
        #endregion
        
    }
   
}
