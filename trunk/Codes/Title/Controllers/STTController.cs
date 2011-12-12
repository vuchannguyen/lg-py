using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using System.Web.UI.WebControls;
using CRM.Library.Common;
using CRM.Library.Attributes;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI;
using System.Collections;

namespace CRM.Controllers
{
    public class STTController : BaseController
    {
        #region Variable

        private STTDao sttDao = new STTDao();
        private STTStatusDao sttStatusDao = new STTStatusDao();
        private STTResultDao sttResultDao = new STTResultDao();
        private DepartmentDao deptDao = new DepartmentDao();
        private STTRefResultDao refResultDao = new STTRefResultDao();
        private InsuranceHospitalDao hospitalDao = new InsuranceHospitalDao();
        private JobRequestDao requestDao = new JobRequestDao();
        private LocationDao locationDao = new LocationDao();
        #endregion

        #region Index
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            List<sp_GetSTTClassResult> listCls = sttDao.GetListClass();
            Hashtable hashData = Session[SessionKey.STT_FILTER] == null ? new Hashtable() : (Hashtable)Session[SessionKey.STT_FILTER];

            ViewData[Constants.STT_LIST_NAME] = hashData[Constants.STT_LIST_NAME] == null ? Constants.FULLNAME_OR_USERID : !string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_NAME]) ? hashData[Constants.STT_LIST_NAME] : Constants.FULLNAME_OR_USERID;

            ViewData[Constants.LOCATION_LIST_BRANCH] = new SelectList(locationDao.GetListBranchAll(true, false), "ID", "Name", hashData[Constants.LOCATION_LIST_BRANCH] == null ? Constants.FIRST_ITEM_BRANCH : hashData[Constants.LOCATION_LIST_BRANCH]);
            ViewData[Constants.LOCATION_LIST_OFFICE] = new SelectList(locationDao.GetListOffice(ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_BRANCH]), true, false), "ID", "Name", hashData[Constants.LOCATION_LIST_OFFICE] == null ? Constants.FIRST_ITEM_OFFICE : hashData[Constants.LOCATION_LIST_OFFICE]);
            ViewData[Constants.LOCATION_LIST_FLOOR] = new SelectList(locationDao.GetListFloor(ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_BRANCH]), ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_OFFICE]), true, false), "ID", "Name", hashData[Constants.LOCATION_LIST_FLOOR] == null ? Constants.FIRST_ITEM_FLOOR : hashData[Constants.LOCATION_LIST_FLOOR]);

            ViewData[Constants.STT_LIST_CLASS] = new SelectList(sttDao.GetListClass(), "Ids", "Ids", hashData[Constants.STT_LIST_CLASS] == null ? listCls.Count > 0 ? listCls.Last().ids : "" : hashData[Constants.STT_LIST_CLASS].ToString());
            ViewData[Constants.STT_LIST_STATUS] = new SelectList(sttStatusDao.GetList(), "Id", "Name", hashData[Constants.STT_LIST_STATUS]== null?"":hashData[Constants.STT_LIST_STATUS].ToString());
            ViewData[Constants.STT_LIST_RESULT] = new SelectList(sttResultDao.GetList(), "Id", "Name", hashData[Constants.STT_LIST_RESULT] == null?"":hashData[Constants.STT_LIST_RESULT].ToString());
            ViewData[Constants.STT_LIST_STARTDATE_FROM] = hashData[Constants.STT_LIST_STARTDATE_FROM] == null ? "" : hashData[Constants.STT_LIST_STARTDATE_FROM].ToString();
            ViewData[Constants.STT_LIST_STARTDATE_TO] = hashData[Constants.STT_LIST_STARTDATE_TO] == null ? "" : hashData[Constants.STT_LIST_STARTDATE_TO].ToString();
            ViewData[Constants.STT_LIST_FROMDATE_FROM] = hashData[Constants.STT_LIST_FROMDATE_FROM] == null ? "" : hashData[Constants.STT_LIST_FROMDATE_FROM].ToString();
            ViewData[Constants.STT_LIST_FROMDATE_TO] = hashData[Constants.STT_LIST_FROMDATE_TO] == null ? "" : hashData[Constants.STT_LIST_FROMDATE_TO].ToString();
            ViewData[Constants.STT_LIST_COLUMN] = hashData[Constants.STT_LIST_COLUMN] == null ? "ID" : hashData[Constants.STT_LIST_COLUMN].ToString();
            ViewData[Constants.STT_LIST_ORDER] = hashData[Constants.STT_LIST_ORDER]== null?"desc": hashData[Constants.STT_LIST_ORDER].ToString();
            ViewData[Constants.STT_LIST_PAGE_INDEX] = hashData[Constants.STT_LIST_PAGE_INDEX] == null ? "1" : hashData[Constants.STT_LIST_PAGE_INDEX].ToString();
            ViewData[Constants.STT_LIST_ROW_COUNT] = hashData[Constants.STT_LIST_ROW_COUNT] == null ? "20" : hashData[Constants.STT_LIST_ROW_COUNT].ToString();  
            return View();
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.STT_FILTER);
            return RedirectToAction("Index");
        }

        private void SetSessionFilter(string name, string branch, string office, string floor, string cls, string statusId, string resultId,
               string startDateBegin, string startDateEnd, string endDateBegin, string endDateEnd,string column,string order,int pageIndex,int rowCount )
        {
            Hashtable sttState = new Hashtable();
            sttState.Add(Constants.STT_LIST_NAME, name);

            sttState.Add(Constants.LOCATION_LIST_BRANCH, branch);
            sttState.Add(Constants.LOCATION_LIST_OFFICE, office);
            sttState.Add(Constants.LOCATION_LIST_FLOOR, floor);

            sttState.Add(Constants.STT_LIST_CLASS, cls);
            sttState.Add(Constants.STT_LIST_STATUS,statusId);
            sttState.Add(Constants.STT_LIST_RESULT,resultId);
            sttState.Add(Constants.STT_LIST_STARTDATE_FROM,startDateBegin);
            sttState.Add(Constants.STT_LIST_STARTDATE_TO,startDateEnd);
            sttState.Add(Constants.STT_LIST_FROMDATE_FROM,endDateBegin);
            sttState.Add(Constants.STT_LIST_FROMDATE_TO,endDateEnd);
            sttState.Add(Constants.STT_LIST_COLUMN,column);
            sttState.Add(Constants.STT_LIST_ORDER,order);
            sttState.Add(Constants.STT_LIST_PAGE_INDEX,pageIndex);
            sttState.Add(Constants.STT_LIST_ROW_COUNT,rowCount);
            Session[SessionKey.STT_FILTER] = sttState;
        }

        public ActionResult GetListJQGrid(string name, string branch, string office, string floor, string cls, string statusId, string resultId,
               string startDateBegin, string startDateEnd, string endDateBegin, string endDateEnd)
        {
            
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();            
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            SetSessionFilter(name, branch, office, floor, cls, statusId, resultId, startDateBegin, startDateEnd, endDateBegin, endDateEnd, sortColumn, sortOrder, pageIndex, rowCount); ;
            #endregion
            #region search
            DateTime? startDateFrom = null;
            DateTime? startDateTo = null;
            DateTime? endDateFrom = null;
            DateTime? endDateTo = null;
            string userName = string.Empty;
            int statusIdInt = 0;
            int resultIdInt = 0;
            if (name != Constants.FULLNAME_OR_USERID)
            {
                userName = name;
            }
            if (!string.IsNullOrEmpty(statusId))
            {
                statusIdInt = int.Parse(statusId);
            }
            if (!string.IsNullOrEmpty(resultId))
            {
                resultIdInt = int.Parse(resultId);
            }
            if (!string.IsNullOrEmpty(startDateBegin))
            {
                startDateFrom = DateTime.Parse(startDateBegin);
            }
            if (!string.IsNullOrEmpty(startDateEnd))
            {
                startDateTo = DateTime.Parse(startDateEnd);
            }
            if (!string.IsNullOrEmpty(endDateBegin))
            {
                endDateFrom = DateTime.Parse(endDateBegin);
            }
            if (!string.IsNullOrEmpty(endDateEnd))
            {
                endDateTo = DateTime.Parse(endDateEnd);
            }
            //parse location code
            string locationCode = CommonFunc.GenerateLocationCode(branch, office, floor, null);
            locationCode = string.IsNullOrEmpty(locationCode) ? null : locationCode;

            #endregion
            List<sp_GetSTTResult> sttList = sttDao.GetList(userName, statusIdInt, resultIdInt, startDateFrom, startDateTo, endDateFrom, endDateTo, cls, locationCode);
            int totalRecords = sttList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            List<sp_GetSTTResult> finalList = sttDao.Sort(sttList, sortColumn, sortOrder)
                                  .Skip((currentPage - 1) * rowCount)
                                  .Take(rowCount).ToList<sp_GetSTTResult>();

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
                            m.ID, 
                            CommonFunc.Link(m.ID,"/STT/Detail/"  + m.ID+ "",m.DisplayName,true),                          
                            m.Status,
                            m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            m.ExpectedEndDate.HasValue?m.ExpectedEndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",
                            m.Result,
                            m.Remarks,       
                            SetAction(m.STTStatusId,m.ResultId,m.ID)                
                           
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string SetAction(int status, int? result, string id)
        {
            string value = string.Empty;
            if (result.HasValue)
            {
                switch (result.Value)
                {
                    case Constants.STT_RESULT_PASS:
                        if (status == Constants.STT_STATUS_NEED_TO_PROMOTED)
                        {
                            value += CommonFunc.Button("promote", "Promoted", "CRM.popup('/STT/Promoted/" + id + "','Promoted " + id + "',400);") +
                                    CommonFunc.Button("edit", "Edit", "window.location = '/STT/Edit/" + id + "'");

                        }
                        else if (status == Constants.STT_STATUS_PROMOTED)
                        {
                            value += CommonFunc.Button("clpointer", "", "") + CommonFunc.Button("edit", "Edit", "window.location = '/STT/Edit/" + id + "'");

                        }
                        break;
                    case Constants.STT_RESULT_FAIL:

                        value += CommonFunc.Button("clpointer", "", "") + CommonFunc.Button("edit", "Edit", "window.location = '/STT/Edit/" + id + "'");
                        break;
                }
            }
            else
            {
                value += CommonFunc.Button("result", "Result", "CRM.popup('/STT/Result/" + id + "','Result " + id + "',450);") +
                         CommonFunc.Button("edit", "Edit", "window.location = '/STT/Edit/" + id + "'");
            }
            return value;
        }

        public ActionResult Navigation(string name, string id)
        {
            List<sp_GetSTTResult> sttList;
            sttList = GetListSTTForNavigation();
            string testID = string.Empty;
            int index = 0;
            string url = string.Empty;
            switch (name)
            {
                case "First":
                    testID = sttList[0].ID;
                    break;
                case "Prev":
                    index = sttList.IndexOf(sttList.Where(p => p.ID == id).FirstOrDefault<sp_GetSTTResult>());
                    if (index != 0)
                    {
                        testID = sttList[index - 1].ID;
                    }
                    else
                    {
                        testID = id;
                    }
                    break;
                case "Next":
                    index = sttList.IndexOf(sttList.Where(p => p.ID == id).FirstOrDefault<sp_GetSTTResult>());
                    if (index != sttList.Count - 1)
                    {
                        testID = sttList[index + 1].ID;
                    }
                    else
                    {
                        testID = id;
                    }
                    break;
                case "Last":
                    testID = sttList[sttList.Count - 1].ID;
                    break;
            }
            return RedirectToAction("Detail/" + testID);
        }

        private List<sp_GetSTTResult> GetListSTTForNavigation()
        {
            DateTime? startDateFrom = null;
            DateTime? startDateTo = null;
            DateTime? endDateFrom = null;
            DateTime? endDateTo = null;
            string userName = string.Empty;
            int statusIdInt = 0;
            int resultIdInt = 0;
            List<sp_GetSTTResult> sttList;
            List<sp_GetSTTClassResult> listCls = sttDao.GetListClass();
            string cls = listCls.Count > 0 ? listCls.Last().ids : "";
            string order = "ID";
            string columnName = "desc";
            if (Session[SessionKey.STT_FILTER] != null)
            {
                Hashtable hashData = (Hashtable)Session[SessionKey.STT_FILTER];
                userName = !string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_NAME]) ? (string)hashData[Constants.STT_LIST_NAME] : "";
                if (userName == Constants.FULLNAME_OR_USERID)
                {
                    userName = string.Empty;
                }
                statusIdInt = !string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_STATUS]) ? int.Parse((string)hashData[Constants.STT_LIST_STATUS]) : 0;
                resultIdInt = !string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_RESULT]) ? int.Parse((string)hashData[Constants.STT_LIST_RESULT]) : 0;
                if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_STARTDATE_FROM]))
                {
                    startDateFrom = DateTime.Parse((string)hashData[Constants.STT_LIST_STARTDATE_FROM]);
                }
                if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_STARTDATE_TO]))
                {
                    startDateTo = DateTime.Parse((string)hashData[Constants.STT_LIST_STARTDATE_TO]);
                }
                if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_FROMDATE_FROM]))
                {
                    endDateFrom = DateTime.Parse((string)hashData[Constants.STT_LIST_FROMDATE_FROM]);
                }
                if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_FROMDATE_TO]))
                {
                    endDateTo = DateTime.Parse((string)hashData[Constants.STT_LIST_FROMDATE_TO]);
                }
                if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_CLASS]))
                {
                    cls = (string)hashData[Constants.STT_LIST_CLASS];
                }
                if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_COLUMN]))
                {
                    columnName = (string)hashData[Constants.STT_LIST_COLUMN];
                }
                if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_ORDER]))
                {
                    order = (string)hashData[Constants.STT_LIST_ORDER];
                }
            }
            sttList = sttDao.GetList(userName, statusIdInt, resultIdInt, startDateFrom, startDateTo, endDateFrom, endDateTo, cls);
            sttList = sttDao.Sort(sttList, columnName, order);
            return sttList;
        } 

        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Read)]
        public ActionResult ExportToExcel()
        {
            try
            {
                #region search
                DateTime? startDateFrom = null;
                DateTime? startDateTo = null;
                DateTime? endDateFrom = null;
                DateTime? endDateTo = null;
                string locationCode = null;
                string userName = string.Empty;
                int statusIdInt = 0;
                int resultIdInt = 0;
                List<sp_GetSTTForExportResult> sttList;
                List<sp_GetSTTClassResult> listCls = sttDao.GetListClass();
                string cls = listCls.Count > 0 ? listCls.Last().ids : "";
                string order = "ID";
                string columnName = "desc";
                if (Session[SessionKey.STT_FILTER] != null)
                {
                    Hashtable hashData = (Hashtable)Session[SessionKey.STT_FILTER];
                    userName = !string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_NAME]) ? (string)hashData[Constants.STT_LIST_NAME] : "";
                    if (userName == Constants.FULLNAME_OR_USERID)
                    {
                        userName = string.Empty;
                    }
                    statusIdInt = !string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_STATUS]) ? int.Parse((string)hashData[Constants.STT_LIST_STATUS]) : 0;
                    resultIdInt = !string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_RESULT]) ? int.Parse((string)hashData[Constants.STT_LIST_RESULT]) : 0;
                    if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_STARTDATE_FROM]))
                    {
                        startDateFrom = DateTime.Parse((string)hashData[Constants.STT_LIST_STARTDATE_FROM]);
                    }
                    if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_STARTDATE_TO]))
                    {
                        startDateTo = DateTime.Parse((string)hashData[Constants.STT_LIST_STARTDATE_TO]);
                    }
                    if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_FROMDATE_FROM]))
                    {
                        endDateFrom = DateTime.Parse((string)hashData[Constants.STT_LIST_FROMDATE_FROM]);
                    }
                    if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_FROMDATE_TO]))
                    {
                        endDateTo = DateTime.Parse((string)hashData[Constants.STT_LIST_FROMDATE_TO]);
                    }
                    cls = (string)hashData[Constants.STT_LIST_CLASS];
                    if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_COLUMN]))
                    {
                        columnName = (string)hashData[Constants.STT_LIST_COLUMN];
                    }
                    if (!string.IsNullOrEmpty((string)hashData[Constants.STT_LIST_ORDER]))
                    {
                        order = (string)hashData[Constants.STT_LIST_ORDER];
                    }
                    int branch = ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_BRANCH]);
                    int office = ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_OFFICE]);
                    int floor = ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_FLOOR]);
                    //parse location code
                    locationCode = CommonFunc.GenerateLocationCode(branch.ToString(), office.ToString(), floor.ToString(), null);
                    locationCode = string.IsNullOrEmpty(locationCode) ? null : locationCode;
                }
                sttList = sttDao.GetListForExport(userName, statusIdInt, resultIdInt, startDateFrom, startDateTo, endDateFrom, endDateTo, cls, locationCode);
                sttList = sttDao.SortForExport(sttList, columnName, order);
                #endregion 
                ExportExcel exp = new ExportExcel();
                exp.Title = Constants.STT_TILE_EXPORT_EXCEL;
                exp.FileName = Constants.STT_EXPORT_EXCEL_NAME;
                exp.ColumnList = new string[] { "ID", "DisplayName", "VnDisplayName", "Status", "Result", "DOB:Date", "POB", "VnPOB", 
                    "PlaceOfOrigin", "VnPlaceOfOrigin", "Nationality", "Gender:Gender", "MarriedStatus:Married", "Degree","OtherDegree","Major", "IDNumber:text", "IssueDate:Date",
                    "IDIssueLocation", "VnIDIssueLocation", "Race", "Religion", "JR", "JRApproval", "StartDate:Date", "ExpectedEndDate:Date", "Department",
                    "SubDepartment", "Title", "LaborUnion:Labor", "LaborUnionDate:Date", "HomePhone:Text", "CellPhone:Text",
                    "Floor:Text", "ExtensionNumber:Text", "SeatCode:Text", "SkypeId", "YahooId", "PersonalEmail", "OfficeEmail",
                    "EmergencyContactName", "EmergencyContactPhone:Text", "EmergencyContactRelationship", "BankAccount", "BankName", "Remarks",
                    "PermanentAddress", "VnPermanentAddress", "TempAddress", "VnTempAddress" };
                exp.HeaderExcel = new string[] { "ID", "Full Name", "VN Name", "Status", "Result", "Date Of Birth", "Place Of Birth", "VN Place Of Birth",
                    "Place Of Origin", "VN Place Of Origin", "Nationality", "Gender", "Married Status", "Degree","Other Degree","Major", "ID Number", "Issue Date", "Issue Location",
                    "VN Issue Location", "Race", "Religion", "JR", "JR Approval", "Start Date", "Expected End Date", "Department", "Sub Department", "Job Title",
                    "LaborUnion", "Labor Union Date", "Home Phone", "Cell Phone", "Floor", "Extension Number", "SeatCode", "SkypeId", "YahooId", "PersonalEmail",
                    "OfficeEmail", "Emergency Contact Name", "Emergency Contact Phone", "Emergency Contact RelationShip", "Bank Account", "Bank Name", "Remarks",
                    "Permanent Address", "VN Permanent Address", "Temp Address", "VN Temp Address" };
                exp.List = sttList;
                exp.IsRenderNo = true;
                exp.Execute();
            }
            catch
            {
                return View();
            }
            return View();
        }
        #endregion

        #region Detail

        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Read)]
        public ActionResult Detail(string id)
        {
            STT emp = sttDao.GetById(id);
            STT_RefResult objSTT = new STTRefResultDao().GetById(id);
            if (emp != null)
            {
                if (objSTT != null)
                {
                    if (!string.IsNullOrEmpty(objSTT.Remarks))
                    {
                        ViewData["Remarks"] = objSTT.Remarks;
                    }
                    else
                    {
                        ViewData["Remarks"] = Constants.NODATA;
                    }
                    if (!string.IsNullOrEmpty(objSTT.Attachfile))
                    {
                        ViewData["FileCVName"] = CommonFunc.SplitFileName(objSTT.Attachfile, Constants.STT_RESULT_PATH, true);
                    }
                    else
                    {
                        ViewData["FileCVName"] = Constants.NODATA;
                    }
                    ViewData["EndDate"] = objSTT.EndDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                }
                List<sp_GetSTTResult> sttList;
                sttList = GetListSTTForNavigation();
                ViewData["NameDepartment"] = deptDao.GetDepartmentNameBySub(emp.DepartmentId);
                ViewData["ListSTT"] = sttList;
                return View(emp);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditPersonalInfo(string id)
        {
            STT emp = sttDao.GetById(id);
            //for Married Status dropdownlist
            ViewData["MarriedStatus"] = new SelectList(Constants.MarriedStatus, "Value", "Text", emp.MarriedStatus);

            //for Nationlity dropdownlist            
            ViewData["Nationality"] = new SelectList(Constants.Nationality, "Value", "Text", emp.Nationality);

            //for Gender dropdownlist
            ViewData["Gender"] = new SelectList(Constants.Gender, "Value", "Text", emp.Gender.ToString());

            //for employee status dropdownlist (not include Status Resigned)
            List<STT_Status> empStatusList = null;
            if (emp.ResultId.HasValue)
            {
                empStatusList = sttStatusDao.GetList();
            }
            else
            {
                empStatusList = sttStatusDao.GetListForAddNew();
            }
            ViewData["STTStatusId"] = new SelectList(empStatusList, "ID", "Name", emp.STTStatusId);
            return View(emp);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditPersonalInfo(STT emp)
        {
            // TODO: Add insert logic here            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            emp.UpdatedBy = principal.UserData.UserName;
            Message msg = sttDao.UpdatePersonalInfo(emp);
            ShowMessage(msg);
            return RedirectToAction("Detail/" + emp.ID);
        }

        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditCompanyInfo(string id)
        {
            STT emp = sttDao.GetById(id);
            ViewData["DepartmentName"] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", deptDao.GetDepartmentIdBySub(emp.DepartmentId));
            ViewData["DepartmentId"] = new SelectList(deptDao.GetSubListByParent(deptDao.GetDepartmentIdBySub(emp.DepartmentId).Value), "DepartmentId", "DepartmentName", emp.DepartmentId);
            return View(emp);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditCompanyInfo(STT emp)
        {
            // TODO: Add insert logic here            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            emp.UpdatedBy = principal.UserData.UserName;

            Message msg = null;
            int seatCodeID = ConvertUtil.ConvertToInt(CommonFunc.GetLocation(emp.LocationCode, LocationType.SeatCode));
            if (CommonFunc.IsLocationCodeValid(emp.LocationCode) && 
                locationDao.IsSeatCodeAvailableFor(seatCodeID, emp.ID, true))
                msg = sttDao.UpdateCompanyInfo(emp);
            else
                msg = new Message(MessageConstants.E0030, MessageType.Error, "Work location");
            ShowMessage(msg);
            return RedirectToAction("Detail/" + emp.ID);
        }

        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditContactInfo(string id)
        {
            STT emp = sttDao.GetById(id);
            return View(emp);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditContactInfo(STT emp)
        {
            // TODO: Add insert logic here            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            emp.UpdatedBy = principal.UserData.UserName;

            Message msg = sttDao.UpdateContactInfo(emp);
            ShowMessage(msg);
            return RedirectToAction("Detail/" + emp.ID);
        }

        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditRemark(string id)
        {
            STT emp = sttDao.GetById(id);
            return View(emp);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditRemark(STT emp)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            emp.UpdatedBy = principal.UserData.UserName;

            Message msg = sttDao.UpdateRemark(emp);
            ShowMessage(msg);
            return RedirectToAction("Detail/" + emp.ID);
        }

        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditBankAccountInfo(string id)
        {
            STT emp = sttDao.GetById(id);
            return View(emp);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditBankAccountInfo(STT emp)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            emp.UpdatedBy = principal.UserData.UserName;

            Message msg = sttDao.UpdateBankAccountInfo(emp);
            ShowMessage(msg);
            return RedirectToAction("Detail/" + emp.ID);
        }

        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditAddressInfo(string id)
        {
            STT emp = sttDao.GetById(id);
            ViewData["PermanentCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.PermanentCountry);
            ViewData["TempCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.TempCountry);
            ViewData["VnPermanentCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.VnPermanentCountry);
            ViewData["VnTempCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.VnTempCountry);
            return View(emp);
        }

        /// <summary>
        /// Load Edit Personal Info form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditAddressInfo(STT emp)
        {
            #region edit for Address
            if (emp.PermanentAddress == Constants.ADDRESS)
            {
                emp.PermanentAddress = null;
            }
            if (emp.PermanentArea == Constants.AREA)
            {
                emp.PermanentArea = null;
            }
            if (emp.PermanentCityProvince == Constants.CITYPROVINCE)
            {
                emp.PermanentCityProvince = null;
            }
            if (emp.PermanentDistrict == Constants.DISTRICT)
            {
                emp.PermanentDistrict = null;
            }
            if (emp.TempAddress == Constants.ADDRESS)
            {
                emp.TempAddress = null;
            }
            if (emp.TempArea == Constants.AREA)
            {
                emp.TempArea = null;
            }
            if (emp.TempCityProvince == Constants.CITYPROVINCE)
            {
                emp.TempCityProvince = null;
            }
            if (emp.TempDistrict == Constants.DISTRICT)
            {
                emp.TempDistrict = null;
            }
            //VN
            if (emp.VnPermanentAddress == Constants.VN_ADDRESS)
            {
                emp.VnPermanentAddress = null;
            }
            if (emp.VnPermanentArea == Constants.VN_AREA)
            {
                emp.VnPermanentArea = null;
            }
            if (emp.VnPermanentCityProvince == Constants.VN_CITYPROVINCE)
            {
                emp.VnPermanentCityProvince = null;
            }
            if (emp.VnPermanentDistrict == Constants.VN_DISTRICT)
            {
                emp.VnPermanentDistrict = null;
            }
            if (emp.VnTempAddress == Constants.VN_ADDRESS)
            {
                emp.VnTempAddress = null;
            }
            if (emp.VnTempArea == Constants.VN_AREA)
            {
                emp.VnTempArea = null;
            }
            if (emp.VnTempCityProvince == Constants.VN_CITYPROVINCE)
            {
                emp.VnTempCityProvince = null;
            }
            if (emp.VnTempDistrict == Constants.VN_DISTRICT)
            {
                emp.VnTempDistrict = null;
            }
            #endregion
            // TODO: Add insert logic here            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            emp.UpdatedBy = principal.UserData.UserName;

            Message msg = sttDao.UpdateAddressInfo(emp);
            ShowMessage(msg);
            return RedirectToAction("Detail/" + emp.ID);
        }

        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditResultInfo(string id)
        {
            STT_RefResult stt = sttDao.GetRefResultById(id);
            STT obj = sttDao.GetById(id);
            if (obj.STTStatusId == Constants.STT_STATUS_NEED_TO_PROMOTED || obj.STTStatusId == Constants.STT_STATUS_REJECTED)
            {
                ViewData["Status"] = true;
            }
            ViewData["Result"] = new SelectList(sttResultDao.GetList(), "Id", "Name", stt.ResultId);
            ViewData["STTID"] = obj.ID;
            return View(stt);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditResultInfo(STT_RefResult obj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = null;
            STT_RefResult objDb = new STTRefResultDao().GetById(obj.SttID);
            msg = CheckFileUpload();
            string oldFile = string.Empty;
            string contractFileName = string.Empty;
            if (msg == null) //case sussessfully
            {
                int y = 0;
                STT empInfo = sttDao.GetById(obj.SttID);
                string hidDeleteFile = Request["hidDeleteFile"];
                if (msg == null)
                {
                    if (objDb != null)
                    {

                        if (!string.IsNullOrEmpty(hidDeleteFile))
                        {
                            string[] arrDeleteFile = hidDeleteFile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);
                            foreach (string deleteFile in arrDeleteFile)
                            {
                                oldFile = objDb.Attachfile.TrimEnd(Constants.FILE_CHAR_PREFIX);
                                if (oldFile.Split(Constants.FILE_CHAR_PREFIX).Contains(deleteFile))
                                {
                                    objDb.Attachfile = objDb.Attachfile.Replace(deleteFile + Constants.FILE_STRING_PREFIX, "");
                                }
                            }
                        }
                    }
                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase hpf = Request.Files[y] as HttpPostedFileBase;
                        if (!string.IsNullOrEmpty(Path.GetExtension(hpf.FileName)))
                        {
                            string extension = Path.GetExtension(hpf.FileName);
                            string fileName = Path.GetFileNameWithoutExtension(hpf.FileName);
                            string strPath = Server.MapPath(Constants.STT_RESULT_PATH);
                            string newFile = empInfo.ID + "_" + empInfo.FirstName + "_" + empInfo.LastName + "_" + fileName +
                                obj.EndDate.ToString(Constants.UNIQUE_CONTRACT) + DateTime.Now.ToString(Constants.UNIQUE_TIME) + extension;
                            newFile = ConvertUtil.FormatFileName(newFile);
                            strPath = strPath + "\\" + newFile;
                            hpf.SaveAs(strPath);
                            objDb.Attachfile += newFile + Constants.FILE_STRING_PREFIX;
                        }
                        y++;
                    }
                }
                objDb.UpdatedBy = principal.UserData.UserName;
                objDb.ResultId = obj.ResultId;
                objDb.EndDate = obj.EndDate;
                objDb.UpdatedDate = DateTime.Now;
                objDb.Remarks = obj.Remarks;
                msg = refResultDao.UpdateResult(objDb);
            }
            ShowMessage(msg);
            return RedirectToAction("Detail/" + obj.SttID);
        }

        #endregion

        #region Add New,Edit STT

        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Insert)]
        public ActionResult Create()
        {
            ViewData["MarriedStatus"] = new SelectList(Constants.MarriedStatus, "Value", "Text", "");
            ViewData["Nationality"] = new SelectList(Constants.Nationality, "Value", "Text", "");
            ViewData["VnNationality"] = new SelectList(Constants.VnNationality, "Value", "Text", "");
            ViewData["Gender"] = new SelectList(Constants.Gender, "Value", "Text", "");

            List<STT_Status> empStatusList = sttStatusDao.GetListForAddNew();
            ViewData["STTStatusId"] = new SelectList(empStatusList, "ID", "Name", "");

            ViewData["DepartmentName"] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", "");
            List<ListItem> subDepartment = new List<ListItem>();
            ViewData["DepartmentId"] = new SelectList(subDepartment, "Value", "Text", "");
            return View();
        }

        #region Validate

        public JsonResult CheckIDExits(string id)
        {
            //TODO: Do the validation
            Message msg = null;
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            STT obj = sttDao.GetById(id);
            if (obj != null)
            {
                msg = new Message(MessageConstants.E0020, MessageType.Error, id, "STT");
                result.Data = msg.ToString();
            }
            else
            {
                result.Data = true;
            }
            return result;
        }

        #endregion
        //
        // POST: /STT/Create

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create(STT obj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = null;
            #region edit for Address
            if (obj.PermanentAddress == Constants.ADDRESS)
            {
                obj.PermanentAddress = null;
            }
            if (obj.PermanentArea == Constants.AREA)
            {
                obj.PermanentArea = null;
            }
            if (obj.PermanentCityProvince == Constants.CITYPROVINCE)
            {
                obj.PermanentCityProvince = null;
            }
            if (obj.PermanentDistrict == Constants.DISTRICT)
            {
                obj.PermanentDistrict = null;
            }
            if (obj.TempAddress == Constants.ADDRESS)
            {
                obj.TempAddress = null;
            }
            if (obj.TempArea == Constants.AREA)
            {
                obj.TempArea = null;
            }
            if (obj.TempCityProvince == Constants.CITYPROVINCE)
            {
                obj.TempCityProvince = null;
            }
            if (obj.TempDistrict == Constants.DISTRICT)
            {
                obj.TempDistrict = null;
            }
            //VN
            if (obj.VnPermanentAddress == Constants.VN_ADDRESS)
            {
                obj.VnPermanentAddress = null;
            }
            if (obj.VnPermanentArea == Constants.VN_AREA)
            {
                obj.VnPermanentArea = null;
            }
            if (obj.VnPermanentCityProvince == Constants.VN_CITYPROVINCE)
            {
                obj.VnPermanentCityProvince = null;
            }
            if (obj.VnPermanentDistrict == Constants.VN_DISTRICT)
            {
                obj.VnPermanentDistrict = null;
            }
            if (obj.VnTempAddress == Constants.VN_ADDRESS)
            {
                obj.VnTempAddress = null;
            }
            if (obj.VnTempArea == Constants.VN_AREA)
            {
                obj.VnTempArea = null;
            }
            if (obj.VnTempCityProvince == Constants.VN_CITYPROVINCE)
            {
                obj.VnTempCityProvince = null;
            }
            if (obj.VnTempDistrict == Constants.VN_DISTRICT)
            {
                obj.VnTempDistrict = null;
            }
            #endregion
            obj.CreateDate = DateTime.Now;
            obj.UpdateDate = DateTime.Now;
            obj.CreatedBy = principal.UserData.UserName;
            obj.UpdatedBy = principal.UserData.UserName;
            if (CommonFunc.IsLocationCodeValid(obj.LocationCode))
                msg = sttDao.Insert(obj);
            else
                msg = new Message(MessageConstants.E0030, MessageType.Error, "Work location");
            ShowMessage(msg);
            if (msg.MsgType != MessageType.Error)
            {
                string tempPathPhoto = Server.MapPath(Constants.UPLOAD_TEMP_PATH + obj.Photograph);
                string empPathPhoto = Server.MapPath(Constants.IMAGE_PATH + obj.Photograph);
                if (System.IO.File.Exists(tempPathPhoto))
                {
                    System.IO.File.Move(tempPathPhoto, empPathPhoto);
                }
                string tempPathCV = Server.MapPath(Constants.UPLOAD_TEMP_PATH + obj.CVFile);
                string empPathCV = Server.MapPath(Constants.CV_PATH + obj.CVFile);
                if (System.IO.File.Exists(tempPathCV))
                {
                    System.IO.File.Move(tempPathCV, empPathCV);
                }

                // save class of STT to session
                Hashtable sttState = new Hashtable();                
                sttState.Add(Constants.STT_LIST_CLASS, obj.ID.Split('-')[0]);
                Session[SessionKey.STT_FILTER] = sttState;
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        //
        // GET: /STT/Edit/5

        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Edit(string id)
        {
            STT emp = sttDao.GetById(id);
            ViewData["MarriedStatus"] = new SelectList(Constants.MarriedStatus, "Value", "Text", emp.MarriedStatus);

            ViewData["Nationality"] = new SelectList(Constants.Nationality, "Value", "Text", emp.Nationality);
            ViewData["PermanentCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.PermanentCountry);
            ViewData["TempCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.TempCountry);
            ViewData["VnPermanentCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.VnPermanentCountry);
            ViewData["VnTempCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.VnTempCountry);

            ViewData["Gender"] = new SelectList(Constants.Gender, "Value", "Text", emp.Gender);

            List<STT_Status> empStatusList = null;
            if (emp.ResultId.HasValue)
            {
                empStatusList = sttStatusDao.GetList();
            }
            else
            {
                empStatusList = sttStatusDao.GetListForAddNew();
            }
            ViewData["STTStatusId"] = new SelectList(empStatusList, "ID", "Name", emp.STTStatusId);

            ViewData["DepartmentName"] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", deptDao.GetDepartmentIdBySub(emp.DepartmentId));
            ViewData["DepartmentId"] = new SelectList(deptDao.GetSubListByParent(deptDao.GetDepartmentIdBySub(emp.DepartmentId).Value), "DepartmentId", "DepartmentName", emp.DepartmentId);
            return View(emp);
        }

        //
        // POST: /STT/Edit/5

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update)]
        public ActionResult Edit(STT emp)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = null;
            #region edit for Address
            if (emp.PermanentAddress == Constants.ADDRESS)
            {
                emp.PermanentAddress = null;
            }
            if (emp.PermanentArea == Constants.AREA)
            {
                emp.PermanentArea = null;
            }
            if (emp.PermanentCityProvince == Constants.CITYPROVINCE)
            {
                emp.PermanentCityProvince = null;
            }
            if (emp.PermanentDistrict == Constants.DISTRICT)
            {
                emp.PermanentDistrict = null;
            }
            if (emp.TempAddress == Constants.ADDRESS)
            {
                emp.TempAddress = null;
            }
            if (emp.TempArea == Constants.AREA)
            {
                emp.TempArea = null;
            }
            if (emp.TempCityProvince == Constants.CITYPROVINCE)
            {
                emp.TempCityProvince = null;
            }
            if (emp.TempDistrict == Constants.DISTRICT)
            {
                emp.TempDistrict = null;
            }
            //VN
            if (emp.VnPermanentAddress == Constants.VN_ADDRESS)
            {
                emp.VnPermanentAddress = null;
            }
            if (emp.VnPermanentArea == Constants.VN_AREA)
            {
                emp.VnPermanentArea = null;
            }
            if (emp.VnPermanentCityProvince == Constants.VN_CITYPROVINCE)
            {
                emp.VnPermanentCityProvince = null;
            }
            if (emp.VnPermanentDistrict == Constants.VN_DISTRICT)
            {
                emp.VnPermanentDistrict = null;
            }
            if (emp.VnTempAddress == Constants.VN_ADDRESS)
            {
                emp.VnTempAddress = null;
            }
            if (emp.VnTempArea == Constants.VN_AREA)
            {
                emp.VnTempArea = null;
            }
            if (emp.VnTempCityProvince == Constants.VN_CITYPROVINCE)
            {
                emp.VnTempCityProvince = null;
            }
            if (emp.VnTempDistrict == Constants.VN_DISTRICT)
            {
                emp.VnTempDistrict = null;
            }
            #endregion
            emp.UpdatedBy = principal.UserData.UserName;
            if (CommonFunc.IsLocationCodeValid(emp.LocationCode))
                msg = sttDao.Update(emp);
            else
                msg = new Message(MessageConstants.E0030, MessageType.Error, "Work location");
            
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Delete,ShowAtCurrentPage=true)]
        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = sttDao.DeleteList(id, principal.UserData.UserName);
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Promoted(string id)
        {
            STT emp = sttDao.GetById(id);
            STT_RefResult objRef = new STTRefResultDao().GetById(id);
            int department = deptDao.GetDepartmentIdBySub(emp.DepartmentId).Value;
            ViewData["TitleId"] = new SelectList(new JobTitleLevelDao().GetJobTitleListByDepId(department), "ID", "DisplayName", "");
            List<EmployeeStatus> empStatusList = new EmployeeStatusDao().GetList().Where(p => p.StatusId != Constants.RESIGNED).ToList<EmployeeStatus>();
            ViewData["EmpStatusId"] = new SelectList(empStatusList, "StatusId", "StatusName", "");
            ViewData["ViewEndDate"] = objRef.EndDate.ToString(Constants.DATETIME_FORMAT);
            ViewData["STTUpdateDate"] = emp.UpdateDate.ToString();
            return View(emp);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public JsonResult Promoted(Employee obj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            obj.CreatedBy = principal.UserData.UserName;
            obj.UpdatedBy = principal.UserData.UserName;
            Message msg = sttDao.UpdatePromote(Request["ID"], Request["NewID"], obj);
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = msg;
            return result;
        }

        #endregion

        #region tooltip
        public ActionResult STTToolTip(string id)
        {
            STT stt = sttDao.GetById(id);
            return View(stt);
        }
        #endregion

        #region result
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Result(string id)
        {
            STT_RefResult stt = sttDao.GetRefResultById(id);
            STT obj = sttDao.GetById(id);
            ViewData["ViewStartDate"] = obj.StartDate.ToString(Constants.DATETIME_FORMAT);
            ViewData["Result"] = new SelectList(sttResultDao.GetList(), "Id", "Name", "");
            ViewData["STTID"] = obj.ID;
            ViewData["STTUpdateDate"] = obj.UpdateDate.ToString();
            return View(stt);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Update, ShowInPopup = true)]
        public FileUploadJsonResult Result(STT_RefResult obj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            FileUploadJsonResult result = new FileUploadJsonResult();
            Message msg = null;
            string oldFile = string.Empty;
            string serverPath = Server.MapPath(Constants.STT_RESULT_PATH); 
            msg = CheckFileUpload();
            string contractFileName = string.Empty;
            if (msg == null) //case sussessfully
            {
                int y = 0;
                STT empInfo = sttDao.GetById(obj.SttID);
               
                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase hpf = Request.Files[y] as HttpPostedFileBase;
                        if (!string.IsNullOrEmpty(Path.GetExtension(hpf.FileName)))
                        {
                            string extension = Path.GetExtension(hpf.FileName);
                            string fileName = Path.GetFileNameWithoutExtension(hpf.FileName);
                            string contractName = empInfo.ID + "_" + empInfo.FirstName + "_" + empInfo.LastName + "_" + fileName +
                                obj.EndDate.ToString(Constants.UNIQUE_CONTRACT) + DateTime.Now.ToString(Constants.UNIQUE_TIME) + extension;
                            contractName = ConvertUtil.FormatFileName(contractName);
                            string strPath = serverPath + "\\" + contractName;
                            hpf.SaveAs(strPath);
                            contractFileName += contractName + Constants.FILE_STRING_PREFIX;
                        }
                        y++;
                    }
                obj.CreatedBy = principal.UserData.UserName;
                obj.UpdatedBy = principal.UserData.UserName;
                obj.Attachfile = contractFileName;
                msg = refResultDao.Insert(obj);                    
                result.Data = msg;
            }
            else
            {
                result.Data = msg;
            }
            return result;
        }

        private Message CheckFileUpload()
        {
            Message msg = null;
            bool invalidExtension = false;
            bool invalidSize = false;
            bool invalidName = false;
            string errorExtension = string.Empty;
            string errorFileName = string.Empty;
            string duplicateName = string.Empty;
            int i = 0;
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[i] as HttpPostedFileBase;
                if (hpf.ContentLength != 0)
                {
                    int maxSize = 1024 * 1024 * Constants.CV_MAX_SIZE;
                    float sizeFile = float.Parse(hpf.ContentLength.ToString("N"));
                    string extension = Path.GetExtension(hpf.FileName);
                    string[] extAllowList = Constants.CONTRACT_EXT_ALLOW.Split(',');

                    if (!extAllowList.Contains(extension.ToLower())) //check extension file is valid
                    {
                        invalidExtension = true;
                        errorExtension += extension + ",";
                        break;
                    }
                    else if (sizeFile > maxSize) //check maxlength of uploaded file
                    {
                        invalidSize = true;
                        break;
                    }
                    else if (duplicateName.Contains(Path.GetFileName(hpf.FileName)))
                    {
                        errorFileName = Path.GetFileName(hpf.FileName);
                        invalidName = true;
                        break;
                    }
                }
                i++;
                duplicateName += Path.GetFileName(hpf.FileName) + ",";
            }
            if (invalidExtension == true)
            {
                msg = new Message(MessageConstants.E0013, MessageType.Error, errorExtension.TrimEnd(','), Constants.CONTRACT_EXT_ALLOW, Constants.CV_MAX_SIZE.ToString());
            }
            else if (invalidSize == true)
            {
                msg = new Message(MessageConstants.E0012, MessageType.Error, Constants.CV_MAX_SIZE.ToString());
            }
            else if (invalidName == true)
            {
                msg = new Message(MessageConstants.E0017, MessageType.Error, errorFileName);
            }
            return msg;
        }
        #endregion

    }
}
