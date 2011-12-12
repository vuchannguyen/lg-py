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
    public class EmployeeController : BaseController
    {
        #region Variable

        private DepartmentDao deptDao = new DepartmentDao();
        private JobTitleDao titleDao = new JobTitleDao();
        private EmployeeDao empDao = new EmployeeDao();
        private EmployeeStatusDao empStatusDao = new EmployeeStatusDao();
        private PositionDao posDao = new PositionDao();
        private JobRequestDao requestDao = new JobRequestDao();
        private EmployeeDepartmentJobTitleTrackingDao trackingDao = new EmployeeDepartmentJobTitleTrackingDao();
        private JobTitleDao jobTitleDao = new JobTitleDao();
        private JobTitleLevelDao levelDao = new JobTitleLevelDao();
        private ContractRenewalDao contractDao = new ContractRenewalDao();
        private InsuranceHospitalDao hospitalDao = new InsuranceHospitalDao();
        private LocationDao locationDao = new LocationDao();
        #endregion

        #region Index
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.EMPLOYEE_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.EMPLOYEE_DEFAULT_VALUE];
            ViewData[Constants.EMPLOYEE_LIST_NAME] = hashData[Constants.EMPLOYEE_LIST_NAME] == null ? Constants.FULLNAME_OR_USERID : !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_NAME]) ? hashData[Constants.EMPLOYEE_LIST_NAME] : Constants.FULLNAME_OR_USERID;
            ViewData[Constants.EMPLOYEE_LIST_DEPARTMENT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", hashData[Constants.EMPLOYEE_LIST_DEPARTMENT] == null ? Constants.FIRST_ITEM_DEPARTMENT : hashData[Constants.EMPLOYEE_LIST_DEPARTMENT]);
            ViewData[Constants.EMPLOYEE_LIST_SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", hashData[Constants.EMPLOYEE_LIST_SUB_DEPARTMENT] == null ? Constants.FIRST_ITEM_SUB_DEPARTMENT : hashData[Constants.EMPLOYEE_LIST_SUB_DEPARTMENT]);
            ViewData[Constants.EMPLOYEE_LIST_JOB_TITLE] = new SelectList(levelDao.GetList(), "ID", "DisplayName", hashData[Constants.EMPLOYEE_LIST_JOB_TITLE] == null ? Constants.FIRST_ITEM_JOBTITLE : hashData[Constants.EMPLOYEE_LIST_JOB_TITLE]);

            ViewData[Constants.LOCATION_LIST_BRANCH] = new SelectList(locationDao.GetListBranchAll(true, false), "ID", "Name", hashData[Constants.LOCATION_LIST_BRANCH] == null ? Constants.FIRST_ITEM_BRANCH : hashData[Constants.LOCATION_LIST_BRANCH]);            
            ViewData[Constants.LOCATION_LIST_OFFICE] = new SelectList(locationDao.GetListOffice(ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_BRANCH]), true, false), "ID", "Name", hashData[Constants.LOCATION_LIST_OFFICE] == null ? Constants.FIRST_ITEM_OFFICE : hashData[Constants.LOCATION_LIST_OFFICE]);
            ViewData[Constants.LOCATION_LIST_FLOOR] = new SelectList(locationDao.GetListFloor(ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_BRANCH]), ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_OFFICE]), true, false), "ID", "Name", hashData[Constants.LOCATION_LIST_FLOOR] == null ? Constants.FIRST_ITEM_FLOOR : hashData[Constants.LOCATION_LIST_FLOOR]);

            ViewData[Constants.EMPLOYEE_LIST_COLUMN] = hashData[Constants.EMPLOYEE_LIST_COLUMN] == null ? "ID" : hashData[Constants.EMPLOYEE_LIST_COLUMN];
            ViewData[Constants.EMPLOYEE_LIST_ORDER] = hashData[Constants.EMPLOYEE_LIST_ORDER] == null ? "desc" : hashData[Constants.EMPLOYEE_LIST_ORDER];
            ViewData[Constants.EMPLOYEE_LIST_PAGE_INDEX] = hashData[Constants.EMPLOYEE_LIST_PAGE_INDEX] == null ? "1" : hashData[Constants.EMPLOYEE_LIST_PAGE_INDEX].ToString();
            ViewData[Constants.EMPLOYEE_LIST_ROW_COUNT] = hashData[Constants.EMPLOYEE_LIST_ROW_COUNT] == null ? "20" : hashData[Constants.EMPLOYEE_LIST_ROW_COUNT].ToString();

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action">
        ///     1 or null: refresh Employee Active List
        ///     2: refresh Employee Resigned List
        /// </param>
        /// <returns></returns>
        public ActionResult Refresh(string id)
        {
            string view = string.Empty;
            switch (id)
            {                
                case "2":
                    Session.Remove(SessionKey.RESIGNED_EMPLOYEE_DEFAULT_VALUE);
                    view = "EmployeeResignList";
                    break;
                default:
                    Session.Remove(SessionKey.EMPLOYEE_DEFAULT_VALUE);
                    view = "Index";
                    break;
            }
            return RedirectToAction(view);
        }

        /// <summary>
        /// Set filter aeguments to session for EMployee list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="department"></param>
        /// <param name="subDepartment"></param>
        /// <param name="titleId"></param>
        /// <param name="titleLevelId"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilter(string name, string department, string subDepartment, string titleId, string titleLevelId, string branch, string office, string floor,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.EMPLOYEE_LIST_NAME, name);
            hashData.Add(Constants.EMPLOYEE_LIST_DEPARTMENT, department);
            hashData.Add(Constants.EMPLOYEE_LIST_SUB_DEPARTMENT, subDepartment);
            hashData.Add(Constants.EMPLOYEE_LIST_JOB_TITLE, titleId);
            hashData.Add(Constants.EMPLOYEE_LIST_JOB_TITLE_LEVEL, titleLevelId);
            hashData.Add(Constants.LOCATION_LIST_BRANCH, branch);
            hashData.Add(Constants.LOCATION_LIST_OFFICE, office);
            hashData.Add(Constants.LOCATION_LIST_FLOOR, floor);
            hashData.Add(Constants.EMPLOYEE_LIST_COLUMN, column);
            hashData.Add(Constants.EMPLOYEE_LIST_ORDER, order);
            hashData.Add(Constants.EMPLOYEE_LIST_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.EMPLOYEE_LIST_ROW_COUNT, rowCount);

            Session[SessionKey.EMPLOYEE_DEFAULT_VALUE] = hashData;
        }

        /// <summary>
        /// Set filter aeguments to session for EMployee list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="department"></param>
        /// <param name="subDepartment"></param>
        /// <param name="titleId"></param>
        /// <param name="titleLevelId"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetResignSessionFilter(string name, string department, string subDepartment, string titleId, string titleLevelId, string branch, string office, string floor,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.EMPLOYEE_LIST_RESIGNED_NAME, name);
            hashData.Add(Constants.EMPLOYEE_LIST_RESIGNED_DEPARTMENT, department);
            hashData.Add(Constants.EMPLOYEE_LIST_RESIGNED_SUB_DEPARTMENT, subDepartment);
            hashData.Add(Constants.EMPLOYEE_LIST_RESIGNED_JOB_TITLE, titleId);
            hashData.Add(Constants.EMPLOYEE_LIST_RESIGNED_JOB_TITLE_LEVEL, titleLevelId);
            hashData.Add(Constants.LOCATION_LIST_BRANCH, branch);
            hashData.Add(Constants.LOCATION_LIST_OFFICE, office);
            hashData.Add(Constants.LOCATION_LIST_FLOOR, floor);
            hashData.Add(Constants.EMPLOYEE_LIST_RESIGNED_COLUMN, column);
            hashData.Add(Constants.EMPLOYEE_LIST_RESIGNED_ORDER, order);
            hashData.Add(Constants.EMPLOYEE_LIST_RESIGNED_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.EMPLOYEE_LIST_RESIGNED_ROW_COUNT, rowCount);

            Session[SessionKey.RESIGNED_EMPLOYEE_DEFAULT_VALUE] = hashData;
        }

        public ActionResult GetListJQGrid(string name, string department, string subDepartment, string titleId, string titleLevelId, string branch, string office, string floor)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(name, department, subDepartment, titleId, titleLevelId, branch, office, floor, sortColumn, sortOrder, pageIndex, rowCount);

            #region search
            string userName = string.Empty;
            int subDepartmentId = 0;
            int departmentId = 0;
            int title = 0;
            if (name != Constants.FULLNAME_OR_USERID)
            {
                userName = name;
            }
            if (!string.IsNullOrEmpty(subDepartment))
            {
                subDepartmentId = int.Parse(subDepartment);
            }
            if (!string.IsNullOrEmpty(titleId))
            {
                title = int.Parse(titleId);
            }
            if (!string.IsNullOrEmpty(titleLevelId))
            {
                title = int.Parse(titleLevelId);
            }
            if (!string.IsNullOrEmpty(department))
            {
                departmentId = int.Parse(department);
            }
            #endregion

            //parse location code
            string locationCode = CommonFunc.GenerateLocationCode(branch, office, floor, null);
            locationCode = string.IsNullOrEmpty(locationCode) ? null : locationCode;

            List<sp_GetEmployeeResult> empList = empDao.GetList(userName, departmentId, subDepartmentId, title, Constants.EMPLOYEE_ACTIVE, Constants.RESIGNED, locationCode);
            

            int totalRecords = empList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            //List<sp_GetEmployeeResult> finalList = empDao.Sort(empList, sortColumn, sortOrder)
            //                      .Skip((pageIndex - 1) * rowCount)
            //                       .Take(rowCount).ToList<sp_GetEmployeeResult>();
            List<sp_GetEmployeeResult> finalList = empDao.Sort(empList, sortColumn, sortOrder)
                                 .Skip((currentPage - 1) * rowCount)
                                  .Take(rowCount).ToList<sp_GetEmployeeResult>();

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
                            CommonFunc.Link(m.ID,"/Employee/Detail/"  + m.ID+ "",m.DisplayName,true),
                            m.TitleName,
                            m.Department,
                            m.DepartmentName,
                            m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            m.StatusName,
                            CommonFunc.Button("edit", "Edit", "window.location = '/Employee/Edit/" + m.ID.ToString() + "'") +
                            CommonFunc.Button("history", "View History", "window.location = '/Employee/History/" + m.ID.ToString() + "'") +
                            CommonFunc.Button("contract_renewal", "Contract Renewal", "window.location = '/Employee/ContractRenewal/" + m.ID.ToString() + "'") +
                            CommonFunc.Button("performance_review", "Performance Review", "window.location = '/Employee/PerformanceReview/" + m.ID.ToString() + "'") +
                            CommonFunc.Button("resign", "Resign", "CRM.popup('/Employee/Resign/" + m.ID.ToString() + "', 'Resign Employee "+m.ID.ToString()+" - "+m.DisplayName+" ', 400)")
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportToExcel(string active)
        {
            string userName = string.Empty;
            string columnName = "ID";
            string order = "desc";
            int departmentId = 0;
            int title = 0;
            int branch = 0;
            int office = 0;
            int floor = 0;
            int subDepartmentId = 0;
            var grid = new GridView();
            int isActive = int.Parse(active);
            List<sp_GetEmployeeForExportResult> empList = new List<sp_GetEmployeeForExportResult>();
            if (isActive == Constants.EMPLOYEE_ACTIVE)
            {
                if (Session[SessionKey.EMPLOYEE_DEFAULT_VALUE] != null)
                {
                    Hashtable hashData = (Hashtable)Session[SessionKey.EMPLOYEE_DEFAULT_VALUE];
                    userName = (string)hashData[Constants.EMPLOYEE_LIST_NAME];
                    if (userName == Constants.FULLNAME_OR_USERID)
                    {
                        userName = string.Empty;
                    }
                    departmentId = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_DEPARTMENT]) ? int.Parse((string)hashData[Constants.EMPLOYEE_LIST_DEPARTMENT]) : 0;
                    subDepartmentId = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_SUB_DEPARTMENT]) ? int.Parse((string)hashData[Constants.EMPLOYEE_LIST_SUB_DEPARTMENT]) : 0;
                    title = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_JOB_TITLE]) ? int.Parse((string)hashData[Constants.EMPLOYEE_LIST_JOB_TITLE]) : 0;

                    branch = ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_BRANCH]);
                    office = ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_OFFICE]);
                    floor = ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_FLOOR]);
                    
                    columnName = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_COLUMN]) ? (string)hashData[Constants.EMPLOYEE_LIST_COLUMN] : "ID";
                    order = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_ORDER]) ? (string)hashData[Constants.EMPLOYEE_LIST_ORDER] : "desc";
                }
            }
            else
            {
                if (Session[SessionKey.RESIGNED_EMPLOYEE_DEFAULT_VALUE] != null)
                {
                    Hashtable hashData = (Hashtable)Session[SessionKey.RESIGNED_EMPLOYEE_DEFAULT_VALUE];
                    userName = (string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_NAME];
                    if (userName == Constants.FULLNAME_OR_USERID)
                    {
                        userName = string.Empty;
                    }
                    departmentId = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_DEPARTMENT]) ? int.Parse((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_DEPARTMENT]) : 0;
                    subDepartmentId = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_SUB_DEPARTMENT]) ? int.Parse((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_SUB_DEPARTMENT]) : 0;
                    title = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_JOB_TITLE]) ? int.Parse((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_JOB_TITLE]) : 0;

                    branch = ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_BRANCH]);
                    office = ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_OFFICE]);
                    floor = ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_FLOOR]);

                    columnName = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_COLUMN]) ? (string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_COLUMN] : "ID";
                    order = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_ORDER]) ? (string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_ORDER] : "desc";

                }
            }

            //parse location code
            string locationCode = CommonFunc.GenerateLocationCode(branch.ToString(), office.ToString(), floor.ToString(), null);
            locationCode = string.IsNullOrEmpty(locationCode) ? null : locationCode;

            empList = empDao.GetListForExport(userName, departmentId, subDepartmentId, title, isActive, Constants.RESIGNED, locationCode);
            empList = empDao.SortForExport(empList, columnName, order);
            ExportExcel exp = new ExportExcel();
            string[] column = new string[] { "ID", "DisplayName", "VnName", "StatusName", "DOB:Date", "POB", "VnPOB", 
                    "PlaceOfOrigin", "VnPlaceOfOrigin", "Nationality", "Gender:Gender", "MarriedStatus:Married", "Degree","OtherDegree","Major", "IDNumber:text", "IssueDate:Date",
                    "IDIssueLocation", "VnIDIssueLocation", "Race", "Religion", "JR:JR", "JRApproval", "StartDate:Date", "ContractedDate:Date", "Department",
                    "DepartmentName", "TitleName","TaxID","TaxIssueDate:Date","SocialInsuranceNo:Text","Hospital" ,"LaborUnion:Labor", "LaborUnionDate:Date", "HomePhone:Text", "CellPhone:Text",
                    "Floor:Text", "ExtensionNumber:Text", "SeatCode:Text", "SkypeId", "YahooId", "PersonalEmail", "OfficeEmail",
                    "EmergencyContactName", "EmergencyContactPhone:Text", "EmergencyContactRelationship", "BankAccount:Text", "BankName", "Remarks",
                    "PermanentAddress", "VnPermanentAddress", "TempAddress", "VnTempAddress"};
            string[] header = new string[] { "ID", "Full Name", "VN Name", "Status", "Date Of Birth", "Place Of Birth", "VN Place Of Birth",
                    "Place Of Origin", "VN Place Of Origin", "Nationality", "Gender", "Married Status", "Degree","OtherDegree","Major", "IDNumber", "Issue Date", "Issue Location",
                    "VN Issue Location", "Race", "Religion", "JR", "JR Approval", "Start Date", "Contracted Date", "Department", "Sub Department", "Job Title",
                    "Tax ID","Tax Issue Date","Social Insurance No","Hospital","LaborUnion", "Labor Union Date", "Home Phone", "Cell Phone", "Floor", "Extension Number", "SeatCode", "SkypeId", "YahooId", "PersonalEmail",
                    "OfficeEmail", "Emergency Contact Name", "Emergency Contact Phone", "Emergency Contact RelationShip", "Bank Account", "Bank Name", "Remarks",
                    "Permanent Address", "VN Permanent Address", "Temp Address", "VN Temp Address" };
            if (isActive != Constants.EMPLOYEE_ACTIVE)
            {
                exp.Title = Constants.EMPLOYEE_RESIGN_TILE_EXPORT_EXCEL;
                exp.FileName = Constants.EMPLOYEE_RESIGN_EXPORT_EXCEL_NAME;
                string[] exColumn = new string[] { "ResignedDate:Date", "ResignedAllowance:Text", "ResignedReason" };
                string[] finalColumn = new string[column.Length + exColumn.Length];
                column.CopyTo(finalColumn, 0);
                exColumn.CopyTo(finalColumn, column.Length);
                string[] exHeader = new string[] { "Resigned Date", "Resigned Allowance", "Resigned Reason" };
                string[] finalHeader = new string[header.Length + exHeader.Length];
                header.CopyTo(finalHeader, 0);
                exHeader.CopyTo(finalHeader, header.Length);
                exp.ColumnList = finalColumn;
                exp.HeaderExcel = finalHeader;
            }
            else
            {
                exp.Title = Constants.EMPLOYEE_TILE_EXPORT_EXCEL;
                exp.FileName = Constants.EMPLOYEE_EXPORT_EXCEL_NAME;
                exp.ColumnList = column;
                exp.HeaderExcel = header;
            }
            exp.List = empList;
            exp.IsRenderNo = true;
            exp.Execute();
            return View();
        }

        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Resign(string id)
        {
            Employee emp = empDao.GetById(id);
            return View(emp);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Resign(FormCollection content)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string id = content["ID"];
            DateTime resignedDate = DateTime.Parse(content["txtResignedDate"]);
            Employee emp = new Employee();
            emp = empDao.GetById(id);
            if (emp != null)
            {
                emp.ResignedDate = resignedDate;
                if (!string.IsNullOrEmpty(content["txtResignedAllowance"]))
                {
                    emp.ResignedAllowance = float.Parse(content["txtResignedAllowance"]);
                }
                string context = content["txtResignedReason"].Trim();
                if (!string.IsNullOrEmpty(context))
                {
                    emp.ResignedReason = context;
                }
                emp.UpdatedBy = principal.UserData.UserName;
                emp.EmpStatusId = Constants.RESIGNED;
            }
            Message msg = empDao.UpdateForResign(emp);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        public ActionResult Navigation(string Active, string name, string id, string page)
        {
            int isActive = int.Parse(Active);
            List<sp_GetEmployeeResult> empList = GetListEmployeeForNavigation(isActive);

            string testID = string.Empty;
            int index = 0;
            string url = string.Empty;
            switch (name)
            {
                case "First":
                    testID = empList[0].ID;
                    break;
                case "Prev":
                    index = empList.IndexOf(empList.Where(p => p.ID == id).FirstOrDefault<sp_GetEmployeeResult>());
                    if (index != 0)
                    {
                        testID = empList[index - 1].ID;
                    }
                    else
                    {
                        testID = id;
                    }
                    break;
                case "Next":
                    index = empList.IndexOf(empList.Where(p => p.ID == id).FirstOrDefault<sp_GetEmployeeResult>());
                    if (index != empList.Count - 1)
                    {
                        testID = empList[index + 1].ID;
                    }
                    else
                    {
                        testID = id;
                    }
                    break;
                case "Last":
                    testID = empList[empList.Count - 1].ID;
                    break;
            }
            switch (page)
            {
                case "History":
                    url = "History/" + testID;
                    break;
                case "Detail":
                    url = "Detail/" + testID;
                    break;
            }
            return RedirectToAction(url);
        }

        #endregion

        #region Detail

        /// <summary>
        /// Go to Detail View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Read)]
        public ActionResult Detail(string id, string urlback)
        {

            Employee emp = empDao.GetById(id);
            if (emp != null)
            {
                int isActive = emp.EmpStatusId == Constants.RESIGNED ? Constants.EMPLOYEE_NOT_ACTIVE : Constants.EMPLOYEE_ACTIVE;
                List<sp_GetEmployeeResult> empList = GetListEmployeeForNavigation(isActive);
                ViewData["ListEmployee"] = empList;
                ViewData["NameDepartment"] = deptDao.GetDepartmentNameBySub(emp.DepartmentId);

                //Duy Hung Nguyen added for the case come from assign employee for exam
                if (!string.IsNullOrEmpty(urlback))
                {
                    ViewData["BackToExamURL"] = urlback;
                }

                return View(emp);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Get list employee follow Session or status
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        private List<sp_GetEmployeeResult> GetListEmployeeForNavigation(int isActive)
        {
            string name = string.Empty;
            int department = 0;
            int sub_Department = 0;
            int title = 0;
            string locationCode = null;
            string column = "ID";
            string order = "desc";            
            List<sp_GetEmployeeResult> empList = null;
            if (isActive == Constants.EMPLOYEE_ACTIVE)
            {
                if (Session[SessionKey.EMPLOYEE_DEFAULT_VALUE] != null)
                {
                    Hashtable hashData = (Hashtable)Session[SessionKey.EMPLOYEE_DEFAULT_VALUE];
                    name = (string)hashData[Constants.EMPLOYEE_LIST_NAME];
                    if (name == Constants.FULLNAME_OR_USERID)
                    {
                        name = string.Empty;
                    }
                    department = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_DEPARTMENT]) ? int.Parse((string)hashData[Constants.EMPLOYEE_LIST_DEPARTMENT]) : 0;
                    sub_Department = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_SUB_DEPARTMENT]) ? int.Parse((string)hashData[Constants.EMPLOYEE_LIST_SUB_DEPARTMENT]) : 0;
                    title = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_JOB_TITLE]) ? int.Parse((string)hashData[Constants.EMPLOYEE_LIST_JOB_TITLE]) : 0;
                    
                    string branch = ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_BRANCH]).ToString();
                    string office = ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_OFFICE]).ToString();
                    string floor = ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_FLOOR]).ToString();
                    
                    //parse location code
                    locationCode = CommonFunc.GenerateLocationCode(branch, office, floor, null);
                    locationCode = string.IsNullOrEmpty(locationCode) ? null : locationCode;
                    
                    column = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_COLUMN]) ? (string)hashData[Constants.EMPLOYEE_LIST_COLUMN] : "ID";
                    order = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_ORDER]) ? (string)hashData[Constants.EMPLOYEE_LIST_ORDER] : "desc";
                    empList = empDao.GetList(name, department, sub_Department, title, isActive, Constants.RESIGNED);
                    empList = empDao.Sort(empList, column, order);
                }
                else
                {
                    empList = empDao.GetList(name, department, sub_Department, title, isActive, Constants.RESIGNED);
                }
            }
            else
            {
                if (Session[SessionKey.RESIGNED_EMPLOYEE_DEFAULT_VALUE] != null)
                {
                    Hashtable hashData = (Hashtable)Session[SessionKey.RESIGNED_EMPLOYEE_DEFAULT_VALUE];
                    name = (string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_NAME];
                    if (name == Constants.FULLNAME_OR_USERID)
                    {
                        name = string.Empty;
                    }
                    department = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_DEPARTMENT]) ? int.Parse((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_DEPARTMENT]) : 0;
                    sub_Department = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_SUB_DEPARTMENT]) ? int.Parse((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_SUB_DEPARTMENT]) : 0;
                    title = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_JOB_TITLE]) ? int.Parse((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_JOB_TITLE]) : 0;
                    column = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_COLUMN]) ? (string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_COLUMN] : "ID";
                    order = !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_ORDER]) ? (string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_ORDER] : "desc";
                    empList = empDao.GetList(name, department, sub_Department, title, isActive, Constants.RESIGNED);
                    empList = empDao.Sort(empList, column, order);
                }
                else
                {
                    empList = empDao.GetList(name, department, sub_Department, title, isActive, Constants.RESIGNED);
                }
            }
            return empList;
        }

        /// <summary>
        /// Load Edit Personal Info form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditPersonalInfo(string id)
        {
            Employee emp = empDao.GetById(id);
            //for Married Status dropdownlist
            ViewData["MarriedStatus"] = new SelectList(Constants.MarriedStatus, "Value", "Text", emp.MarriedStatus);

            //for Nationlity dropdownlist            
            ViewData["Nationality"] = new SelectList(Constants.Nationality, "Value", "Text", emp.Nationality);

            //for Gender dropdownlist
            ViewData["Gender"] = new SelectList(Constants.Gender, "Value", "Text", emp.Gender.ToString());

            //for employee status dropdownlist (not include Status Resigned)
            List<EmployeeStatus> empStatusList = empStatusDao.GetList().Where(p => p.StatusId != Constants.RESIGNED).ToList<EmployeeStatus>();
            ViewData["EmpStatusId"] = new SelectList(empStatusList, "StatusId", "StatusName", emp.EmpStatusId);
            return View(emp);
        }

        /// <summary>
        /// Load Edit Personal Info form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditPersonalInfo(Employee emp)
        {
            // TODO: Add insert logic here            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            emp.UpdatedBy = principal.UserData.UserName;

            Message msg = empDao.UpdatePersonalInfo(emp);
            ShowMessage(msg);
            return RedirectToAction("Detail/" + emp.ID);
        }


        /// <summary>
        /// Load Edit Personal Info form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditCompanyInfo(string id)
        {
            Employee emp = empDao.GetById(id);
            ViewData["DepartmentName"] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", deptDao.GetDepartmentIdBySub(emp.DepartmentId));
            ViewData["DepartmentId"] = new SelectList(deptDao.GetSubListByParent(deptDao.GetDepartmentIdBySub(emp.DepartmentId).Value), "DepartmentId", "DepartmentName", emp.DepartmentId);
            ViewData["InsuranceHospitalID"] = new SelectList(hospitalDao.GetList(), "ID", "Name", emp.InsuranceHospitalID);
            ViewData["TitleId"] = new SelectList(posDao.GetJobTitleListByDepId(deptDao.GetDepartmentIdBySub(emp.DepartmentId).Value), "JobTitleId", "JobTitleName", emp.TitleId);
            ViewData["TitleLevelId"] = new SelectList(posDao.GetJobTitleLevelListByJobTitleId(jobTitleDao.GetJobTitleIdBySub(emp.TitleId).Value), "ID", "DisplayName", emp.JobTitleLevel.ID);
            return View(emp);
        }

        /// <summary>
        /// Load Edit Personal Info form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditCompanyInfo(Employee emp)
        {
            // TODO: Add insert logic here            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            emp.UpdatedBy = principal.UserData.UserName;
            DateTime deptDate = new DateTime();
            DateTime titleDate = new DateTime();
            DateTime hospitalDate = new DateTime();
            if (!string.IsNullOrEmpty(Request["departEffectDate"]))
            {
                deptDate = Convert.ToDateTime(Request["departEffectDate"]);

            }
            if (!string.IsNullOrEmpty(Request["titleEffectDate"]))
            {
                titleDate = Convert.ToDateTime(Request["titleEffectDate"]);
            }
            if (!string.IsNullOrEmpty(Request["titleLevelEffectDate"]))
            {
                titleDate = Convert.ToDateTime(Request["titleLevelEffectDate"]);
            }
            if (!string.IsNullOrEmpty(Request["hospitalEffectDate"]))
            {
                hospitalDate = Convert.ToDateTime(Request["hospitalEffectDate"]);
            }
            Message msg = null;
            int seatCodeID = ConvertUtil.ConvertToInt(CommonFunc.GetLocation(emp.LocationCode, LocationType.SeatCode));
            if (CommonFunc.IsLocationCodeValid(emp.LocationCode) &&
                locationDao.IsSeatCodeAvailableFor(seatCodeID, emp.ID, false))
                msg = empDao.UpdateCompanyInfo(emp, deptDate, titleDate, hospitalDate);
            else
                msg = new Message(MessageConstants.E0030, MessageType.Error, "Work location");
            
            ShowMessage(msg);
            return RedirectToAction("Detail/" + emp.ID);
        }

        /// <summary>
        /// Load Edit Personal Info form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditContactInfo(string id)
        {
            Employee emp = empDao.GetById(id);
            return View(emp);
        }

        /// <summary>
        /// Load Edit Personal Info form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditContactInfo(Employee emp)
        {
            // TODO: Add insert logic here            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            emp.UpdatedBy = principal.UserData.UserName;

            Message msg = empDao.UpdateContactInfo(emp);
            ShowMessage(msg);
            return RedirectToAction("Detail/" + emp.ID);
        }

        /// <summary>
        /// Load Edit Personal Info form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditAddressInfo(string id)
        {
            Employee emp = empDao.GetById(id);

            //for Nationlity dropdownlist            
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
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditAddressInfo(Employee emp)
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

            Message msg = empDao.UpdateAddressInfo(emp);
            ShowMessage(msg);
            return RedirectToAction("Detail/" + emp.ID);
        }

        /// <summary>
        /// Load Edit Personal Info form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditBankAccountInfo(string id)
        {
            Employee emp = empDao.GetById(id);
            return View(emp);
        }

        /// <summary>
        /// Load Edit Personal Info form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditBankAccountInfo(Employee emp)
        {
            // TODO: Add insert logic here            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            emp.UpdatedBy = principal.UserData.UserName;

            Message msg = empDao.UpdateBankAccountInfo(emp);
            ShowMessage(msg);
            return RedirectToAction("Detail/" + emp.ID);
        }


        /// <summary>
        /// Load Edit Personal Info form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditRemark(string id)
        {
            Employee emp = empDao.GetById(id);
            return View(emp);
        }

        /// <summary>
        /// Load Edit Personal Info form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditRemark(Employee emp)
        {
            // TODO: Add insert logic here            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            emp.UpdatedBy = principal.UserData.UserName;

            Message msg = empDao.UpdateRemark(emp);
            ShowMessage(msg);
            return RedirectToAction("Detail/" + emp.ID);
        }

        #endregion

        #region History

        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Read)]
        public ActionResult History(string id)
        {
            Employee emp = empDao.GetById(id);
            int isActive = emp.EmpStatusId == Constants.RESIGNED ? Constants.EMPLOYEE_NOT_ACTIVE : Constants.EMPLOYEE_ACTIVE;
            List<sp_GetEmployeeResult> empList = GetListEmployeeForNavigation(isActive);
            ViewData["ListEmployee"] = empList;
            ViewData["NameDepartment"] = deptDao.GetDepartmentNameBySub(emp.DepartmentId);

            return View(emp);
        }

        public ActionResult GetEmployeeHistory(string name, string id)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            List<EmployeeDepartmentJobTitleTracking> empList = empDao.GetListHistoryById(id);

            Employee emp = empDao.GetById(id);
            int totalRecords = empList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            List<EmployeeDepartmentJobTitleTracking> finalList = empList.Skip((currentPage - 1) * rowCount)
                                   .Take(rowCount).OrderByDescending(c => c.StartDate).ToList<EmployeeDepartmentJobTitleTracking>();

            int i = 0;
            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        cell = new string[] { 
                            m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW),                           
                                CompareValueHistory(finalList,i,Constants.DEPARTMENT),                          
                               CompareValueHistory(finalList,i,Constants.SUBDEPARTMENT),
                               CompareValueHistory(finalList,i,Constants.JOBTITLE),
                           "",
                            "",
                            i++.ToString()
                            
                        },
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string CompareValueHistory(List<EmployeeDepartmentJobTitleTracking> list, int i, string colummn)
        {
            string value = string.Empty;
            if (i < list.Count - 1)
            {
                switch (colummn)
                {
                    case Constants.DEPARTMENT:
                        if (list[i].DepartmentName != list[i + 1].DepartmentName)
                        {
                            value += "<div id='row_active' class='row_active'>" + deptDao.GetDepartmentNameBySubDepartmentName(list[i].DepartmentName) + "</div>";
                        }
                        else
                        {
                            value += deptDao.GetDepartmentNameBySubDepartmentName(list[i].DepartmentName);
                        }
                        break;
                    case Constants.SUBDEPARTMENT:
                        if (list[i].DepartmentName != list[i + 1].DepartmentName)
                        {
                            value += "<div id='row_active' class='row_active'>" + list[i].DepartmentName + "</div>";
                        }
                        else
                        {
                            value += list[i].DepartmentName;
                        }
                        break;
                    case Constants.JOBTITLE:
                        if (list[i].JobTitleName != list[i + 1].JobTitleName)
                        {
                            value += "<div id='row_active' class='row_active'>" + list[i].JobTitleName + "</div>";
                        }
                        else
                        {
                            value += list[i].JobTitleName;
                        }
                        break;
                }
            }
            else
            {
                switch (colummn)
                {
                    case Constants.DEPARTMENT:
                        value += deptDao.GetDepartmentNameBySubDepartmentName(list[i].DepartmentName);
                        break;
                    case Constants.SUBDEPARTMENT:
                        value += list[i].DepartmentName;
                        break;
                    case Constants.JOBTITLE:
                        value += list[i].JobTitleName;
                        break;
                }
            }
            return value;
        }

        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Read)]
        public ActionResult ExportForHistory(string id)
        {
            List<EmployeeDepartmentJobTitleTracking> empList = empDao.GetListHistoryById(id);
            Employee emp = empDao.GetById(id);
            #region History
            #region variable
            HtmlTable tbl_Excel = new HtmlTable();
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell colDate = new HtmlTableCell();
            HtmlTableCell colDepartment = new HtmlTableCell();
            HtmlTableCell colSubDepartment = new HtmlTableCell();
            HtmlTableCell colJobTitle = new HtmlTableCell();
            HtmlTableCell colProject = new HtmlTableCell();
            HtmlTableCell colManager = new HtmlTableCell();
            HtmlTableCell col = new HtmlTableCell();
            HtmlTableCell colHospital = new HtmlTableCell();
            #endregion
            #region Title
            col.Align = HorizontalAlign.Left.ToString();
            col.VAlign = VerticalAlign.Middle.ToString();
            col.ColSpan = 4;
            col.RowSpan = 2;
            col.Height = "15";
            col.Attributes.Add("style", Constants.ROW_HEADER);
            col.InnerHtml = "<font size=5><b>Employee History \r\n" + emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName + "</font>";
            col.NoWrap = true;
            row.Cells.Add(col);
            tbl_Excel.Rows.Add(row);
            #endregion
            #region Space
            row = new HtmlTableRow();
            tbl_Excel.Rows.Add(row);
            row = new HtmlTableRow();
            tbl_Excel.Rows.Add(row);
            row = new HtmlTableRow();
            tbl_Excel.Rows.Add(row);
            #endregion
            #region Grid Title
            row = new HtmlTableRow();
            colDate = new HtmlTableCell();
            colDepartment = new HtmlTableCell();
            colSubDepartment = new HtmlTableCell();
            colJobTitle = new HtmlTableCell();
            colProject = new HtmlTableCell();
            colManager = new HtmlTableCell();
            colDate.InnerHtml = "Date";
            colDepartment.InnerHtml = "Department";
            colSubDepartment.InnerHtml = "Sub Department";
            colJobTitle.InnerHtml = "Job Title";
            colProject.InnerHtml = "Project";
            colManager.InnerHtml = "Manager";
            row.Cells.Add(colDate);
            row.Cells.Add(colDepartment);
            row.Cells.Add(colSubDepartment);
            row.Cells.Add(colJobTitle);
            row.Cells.Add(colProject);
            row.Cells.Add(colManager);
            #region CSS for Title
            colDate.Attributes.Add("style", Constants.ROW_TITLE);
            colDepartment.Attributes.Add("style", Constants.ROW_TITLE);
            colSubDepartment.Attributes.Add("style", Constants.ROW_TITLE);
            colJobTitle.Attributes.Add("style", Constants.ROW_TITLE);
            colProject.Attributes.Add("style", Constants.ROW_TITLE);
            colManager.Attributes.Add("style", Constants.ROW_TITLE);
            tbl_Excel.Rows.Add(row);
            #endregion
            #endregion
            #region Row
            foreach (EmployeeDepartmentJobTitleTracking item in empList)
            {
                row = new HtmlTableRow();
                colDate = new HtmlTableCell();
                colDepartment = new HtmlTableCell();
                colSubDepartment = new HtmlTableCell();
                colJobTitle = new HtmlTableCell();
                colProject = new HtmlTableCell();
                colManager = new HtmlTableCell();
                colDate.InnerHtml = item.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                colDepartment.InnerHtml = deptDao.GetDepartmentNameBySubDepartmentName(item.DepartmentName);
                colSubDepartment.InnerHtml = item.DepartmentName;
                colJobTitle.InnerHtml = item.JobTitleName;
                colProject.InnerHtml = "";
                colManager.InnerHtml = "";
                colDate.Attributes.Add("style", Constants.ROW_CSS);
                colDepartment.Attributes.Add("style", Constants.ROW_CSS);
                colSubDepartment.Attributes.Add("style", Constants.ROW_CSS);
                colJobTitle.Attributes.Add("style", Constants.ROW_CSS);
                colProject.Attributes.Add("style", Constants.ROW_CSS);
                colManager.Attributes.Add("style", Constants.ROW_CSS);
                row.Cells.Add(colDate);
                row.Cells.Add(colDepartment);
                row.Cells.Add(colSubDepartment);
                row.Cells.Add(colJobTitle);
                row.Cells.Add(colProject);
                row.Cells.Add(colManager);
                tbl_Excel.Rows.Add(row);
            }
            #endregion
            #endregion
            #region Hospital
            List<EmployeeInsuranceHospitalTracking> hospitalList = hospitalDao.GetListHistoryById(id);
            if (hospitalList.Count > 0)
            {
                #region Space
                row = new HtmlTableRow();
                tbl_Excel.Rows.Add(row);
                row = new HtmlTableRow();
                tbl_Excel.Rows.Add(row);
                row = new HtmlTableRow();
                tbl_Excel.Rows.Add(row);
                #endregion
                #region Title
                colHospital.Align = HorizontalAlign.Left.ToString();
                colHospital.VAlign = VerticalAlign.Middle.ToString();
                colHospital.ColSpan = 4;
                colHospital.RowSpan = 2;
                colHospital.Height = "15";
                colHospital.Attributes.Add("style", Constants.ROW_HEADER);
                colHospital.InnerHtml = "<font size=5><b>Employee Insurance Hospital \r\n" + emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName + "</font>";
                colHospital.NoWrap = true;
                row.Cells.Add(colHospital);
                tbl_Excel.Rows.Add(row);
                #endregion
                #region Space
                row = new HtmlTableRow();
                tbl_Excel.Rows.Add(row);
                row = new HtmlTableRow();
                tbl_Excel.Rows.Add(row);
                row = new HtmlTableRow();
                tbl_Excel.Rows.Add(row);
                #endregion
                HtmlTableCell colHospitalName = new HtmlTableCell();
                colDate = new HtmlTableCell();
                colDate.InnerHtml = "Date";
                colHospitalName.InnerHtml = "Insurance Hospital";
                row.Cells.Add(colDate);
                row.Cells.Add(colHospitalName);
                colDate.Attributes.Add("style", Constants.ROW_TITLE);
                colHospitalName.Attributes.Add("style", Constants.ROW_TITLE);
                foreach (EmployeeInsuranceHospitalTracking item in hospitalList)
                {
                    row = new HtmlTableRow();
                    colDate = new HtmlTableCell();
                    colHospitalName = new HtmlTableCell();
                    colDate.InnerHtml = item.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                    colHospitalName.InnerHtml = item.InsuranceHospital.Name;
                    colDate.Attributes.Add("style", Constants.ROW_CSS);
                    colHospitalName.Attributes.Add("style", Constants.ROW_CSS);
                    row.Cells.Add(colDate);
                    row.Cells.Add(colHospitalName);
                    tbl_Excel.Rows.Add(row);
                }
            }
            #endregion
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "EmployeeHistory"));
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            tbl_Excel.RenderControl(hw);
            Response.Write("<meta http-equiv=Content-Type content='text/html; charset=utf-8' />");
            Response.Write(sw.ToString());
            Response.End();
            return View("Index");
        }

        public ActionResult GetEmployeeInsuranceHospital(string name, string id)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            List<EmployeeInsuranceHospitalTracking> hospitalList = hospitalDao.GetListHistoryById(id);

            Employee emp = empDao.GetById(id);
            int totalRecords = hospitalList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            List<EmployeeInsuranceHospitalTracking> finalList = hospitalList.Skip((currentPage - 1) * rowCount)
                                   .Take(rowCount).OrderByDescending(c => c.StartDate).ToList<EmployeeInsuranceHospitalTracking>();

            int i = 0;
            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        cell = new string[] { 
                            m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW),                           
                                CompareValueHospital(finalList,i),
                                i++.ToString()
                            
                        },
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string CompareValueHospital(List<EmployeeInsuranceHospitalTracking> list, int i)
        {
            string value = string.Empty;
            if (i < list.Count - 1)
            {

                if (list[i].InsuranceHospitalID != list[i + 1].InsuranceHospitalID)
                {
                    value += "<div id='row_active' class='row_active'>" + list[i].InsuranceHospital.Name + "</div>";
                }
                else
                {
                    value += list[i].InsuranceHospital.Name;
                }
            }
            else
            {
                value += list[i].InsuranceHospital.Name;
            }
            return value;
        }

        #endregion

        #region  Resign

        /// <summary>
        /// Delete list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>  
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public ActionResult DeleteList(string id)
        {            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = empDao.DeleteList(id, principal.UserData.UserName);
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        /// <summary>
        /// Delete list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>    
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public ActionResult DeleteResignList(string id)
        {            
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = empDao.DeleteList(id, principal.UserData.UserName);
            //ShowMessage(msg);
            //return RedirectToAction("EmployeeResignList");            
            JsonResult result = new JsonResult();
            return Json(msg);
        }

        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Read)]
        public ActionResult EmployeeResignList()
        {
           
            Hashtable hashData = Session[SessionKey.RESIGNED_EMPLOYEE_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.RESIGNED_EMPLOYEE_DEFAULT_VALUE];

            ViewData[Constants.EMPLOYEE_LIST_RESIGNED_NAME] = hashData[Constants.EMPLOYEE_LIST_RESIGNED_NAME] == null ? Constants.FULLNAME_OR_USERID : !string.IsNullOrEmpty((string)hashData[Constants.EMPLOYEE_LIST_RESIGNED_NAME]) ? hashData[Constants.EMPLOYEE_LIST_RESIGNED_NAME] : Constants.FULLNAME_OR_USERID;
            ViewData[Constants.EMPLOYEE_LIST_RESIGNED_DEPARTMENT] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", hashData[Constants.EMPLOYEE_LIST_RESIGNED_DEPARTMENT] == null ? Constants.FIRST_ITEM_DEPARTMENT : hashData[Constants.EMPLOYEE_LIST_RESIGNED_DEPARTMENT]);
            ViewData[Constants.EMPLOYEE_LIST_RESIGNED_SUB_DEPARTMENT] = new SelectList(deptDao.GetSubList(), "DepartmentId", "DepartmentName", hashData[Constants.EMPLOYEE_LIST_RESIGNED_SUB_DEPARTMENT] == null ? Constants.FIRST_ITEM_SUB_DEPARTMENT : hashData[Constants.EMPLOYEE_LIST_RESIGNED_SUB_DEPARTMENT]);
            ViewData[Constants.EMPLOYEE_LIST_RESIGNED_JOB_TITLE] = new SelectList(levelDao.GetList(), "ID", "DisplayName", hashData[Constants.EMPLOYEE_LIST_RESIGNED_JOB_TITLE] == null ? Constants.FIRST_ITEM_JOBTITLE : hashData[Constants.EMPLOYEE_LIST_RESIGNED_JOB_TITLE]);

            ViewData[Constants.LOCATION_LIST_BRANCH] = new SelectList(locationDao.GetListBranchAll(true, false), "ID", "Name", hashData[Constants.LOCATION_LIST_BRANCH] == null ? Constants.FIRST_ITEM_BRANCH : hashData[Constants.LOCATION_LIST_BRANCH]);
            ViewData[Constants.LOCATION_LIST_OFFICE] = new SelectList(locationDao.GetListOffice(ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_BRANCH]), true, false), "ID", "Name", hashData[Constants.LOCATION_LIST_OFFICE] == null ? Constants.FIRST_ITEM_OFFICE : hashData[Constants.LOCATION_LIST_OFFICE]);
            ViewData[Constants.LOCATION_LIST_FLOOR] = new SelectList(locationDao.GetListFloor(ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_BRANCH]), ConvertUtil.ConvertToInt(hashData[Constants.LOCATION_LIST_OFFICE]), true, false), "ID", "Name", hashData[Constants.LOCATION_LIST_FLOOR] == null ? Constants.FIRST_ITEM_FLOOR : hashData[Constants.LOCATION_LIST_FLOOR]);

            ViewData[Constants.EMPLOYEE_LIST_RESIGNED_COLUMN] = hashData[Constants.EMPLOYEE_LIST_RESIGNED_COLUMN] == null ? "ID" : hashData[Constants.EMPLOYEE_LIST_RESIGNED_COLUMN];
            ViewData[Constants.EMPLOYEE_LIST_RESIGNED_ORDER] = hashData[Constants.EMPLOYEE_LIST_RESIGNED_ORDER] == null ? "desc" : hashData[Constants.EMPLOYEE_LIST_RESIGNED_ORDER];
            ViewData[Constants.EMPLOYEE_LIST_RESIGNED_PAGE_INDEX] = hashData[Constants.EMPLOYEE_LIST_RESIGNED_PAGE_INDEX] == null ? "1" : hashData[Constants.EMPLOYEE_LIST_RESIGNED_PAGE_INDEX].ToString();
            ViewData[Constants.EMPLOYEE_LIST_RESIGNED_ROW_COUNT] = hashData[Constants.EMPLOYEE_LIST_RESIGNED_ROW_COUNT] == null ? "20" : hashData[Constants.EMPLOYEE_LIST_RESIGNED_ROW_COUNT].ToString();

            return View();
        }

        public ActionResult GetListResignListJQGrid(string name, string department, string subDepartment, string titleId, string titleLevelId, string branch, string office, string floor)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetResignSessionFilter(name, department, subDepartment, titleId, titleLevelId, branch, office, floor, sortColumn, sortOrder, pageIndex, rowCount);

            #region search
            string userName = string.Empty;
            int departmentId = 0;
            int title = 0;
            int subDepartmentId = 0;
            if (name != Constants.FULLNAME_OR_USERID)
            {
                userName = name;
            }
            if (!string.IsNullOrEmpty(department))
            {
                departmentId = int.Parse(department);
            }
            if (!string.IsNullOrEmpty(titleId))
            {
                title = int.Parse(titleId);
            }
            if (!string.IsNullOrEmpty(titleLevelId))
            {
                title = int.Parse(titleLevelId);
            }
            if (!string.IsNullOrEmpty(subDepartment))
            {
                subDepartmentId = int.Parse(subDepartment);
            }
            //parse location code
            string locationCode = CommonFunc.GenerateLocationCode(branch, office, floor, null);
            locationCode = string.IsNullOrEmpty(locationCode) ? null : locationCode;
            #endregion

            List<sp_GetEmployeeResult> empList = empDao.GetList(userName, departmentId, subDepartmentId, title, Constants.EMPLOYEE_NOT_ACTIVE, Constants.RESIGNED, locationCode);

            int totalRecords = empList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            List<sp_GetEmployeeResult> finalList = empDao.Sort(empList, sortColumn, sortOrder)
                                  .Skip((currentPage - 1) * rowCount)
                                   .Take(rowCount).ToList<sp_GetEmployeeResult>();

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
                            CommonFunc.Link(m.ID,"/Employee/Detail/"  + m.ID+ "",m.DisplayName,true),
                            m.TitleName,
                            m.Department,
                            m.DepartmentName,
                            m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            m.ResignedDate != null ? m.ResignedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) :"",    
                            m.ResignedReason,
                            CommonFunc.Button("re_active", "Activate Resigned Employee", "window.location = '/Employee/ReActive/" + m.ID + "'") +
                            CommonFunc.Button("edit", "Edit", "window.location = '/Employee/Edit/" + m.ID + "'") +
                            CommonFunc.Button("contract_renewal", "Contract Renewal", "window.location = '/Employee/ResignContract/" + m.ID + "'") +
                            CommonFunc.Button("history", "View History", "window.location = '/Employee/History/" + m.ID.ToString() + "'") 
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update)]
        public ActionResult ReActive(string id)
        {
            Employee emp = empDao.GetById(id);
            ViewData["MarriedStatus"] = new SelectList(Constants.MarriedStatus, "Value", "Text", emp.MarriedStatus);

            ViewData["Nationality"] = new SelectList(Constants.Nationality, "Value", "Text", emp.Nationality);
            ViewData["PermanentCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.PermanentCountry);
            ViewData["TempCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.TempCountry);
            ViewData["VnPermanentCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.VnPermanentCountry);
            ViewData["VnTempCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.VnTempCountry);

            ViewData["Gender"] = new SelectList(Constants.Gender, "Value", "Text", emp.Gender);

            List<EmployeeStatus> empStatusList = empStatusDao.GetList().Where(p => p.StatusId != Constants.RESIGNED).ToList<EmployeeStatus>();
            ViewData["EmpStatusId"] = new SelectList(empStatusList, "StatusId", "StatusName", emp.EmpStatusId);

            ViewData["DepartmentName"] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", deptDao.GetDepartmentIdBySub(emp.DepartmentId));
            ViewData["DepartmentId"] = new SelectList(deptDao.GetSubListByParent(deptDao.GetDepartmentIdBySub(emp.DepartmentId).Value), "DepartmentId", "DepartmentName", emp.DepartmentId);
            //ViewData["TitleId"] = new SelectList(posDao.GetJobTitleListByDepId(deptDao.GetDepartmentIdBySub(emp.DepartmentId).Value), "ID", "DisplayName", emp.TitleId);
            ViewData["TitleId"] = new SelectList(posDao.GetJobTitleListByDepId(deptDao.GetDepartmentIdBySub(emp.DepartmentId).Value), "JobTitleId", "JobTitleName", emp.TitleId);
            ViewData["TitleLevelId"] = new SelectList(posDao.GetJobTitleLevelListByJobTitleId(jobTitleDao.GetJobTitleIdBySub(emp.TitleId).Value), "ID", "DisplayName", emp.JobTitleLevel.ID);
            ViewData["Hospital"] = new SelectList(hospitalDao.GetList(), "ID", "Name", emp.InsuranceHospitalID);
            ViewData["ReActive"] = "ReActive";
            return View(emp);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update)]
        public ActionResult ReActive(Employee obj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            if (!string.IsNullOrEmpty(Request["NewID"]))
            {
                Employee oldEmployee = empDao.GetById(obj.ID);
                if (oldEmployee != null)
                {
                    oldEmployee.UpdatedBy = principal.UserData.UserName;
                    empDao.UpdateFlag(oldEmployee);
                }
                obj.OldEmployeeId += obj.ID + ",";
                obj.ID = Request["NewID"];
                obj.DeleteFlag = true;
            }
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
            Message msg = empDao.Insert(obj);
            if (msg.MsgType == MessageType.Info)
            {
                msg = empDao.InsertTracking(obj);
            }
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        #endregion

        #region Add New Employee

        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Insert)]
        public ActionResult Create()
        {
            ViewData["MarriedStatus"] = new SelectList(Constants.MarriedStatus, "Value", "Text", "");

            ViewData["Nationality"] = new SelectList(Constants.Nationality, "Value", "Text", "");
            ViewData["VnNationality"] = new SelectList(Constants.VnNationality, "Value", "Text", "");
            ViewData["Gender"] = new SelectList(Constants.Gender, "Value", "Text", "");

            List<EmployeeStatus> empStatusList = empStatusDao.GetList().Where(p => p.StatusId != Constants.RESIGNED).ToList<EmployeeStatus>();
            ViewData["EmpStatusId"] = new SelectList(empStatusList, "StatusId", "StatusName", "");
            List<ListItem> title = new List<ListItem>();
            ViewData["TitleId"] = new SelectList(title, "Value", "Text", "");

            List<ListItem> titleLevel = new List<ListItem>();
            ViewData["TitleLevelId"] = new SelectList(titleLevel, "Value", "Text", "");

            ViewData["DepartmentName"] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", "");
            List<ListItem> item = new List<ListItem>();
            ViewData["DepartmentID"] = new SelectList(item, "DepartmentId", "DepartmentName", "");
            IList<ListItem> subDepartment = new List<ListItem>();
            ViewData["SubDepartment"] = new SelectList(subDepartment, "Value", "Text", "");
            ViewData["Hospital"] = new SelectList(hospitalDao.GetList(), "ID", "Name", "");

            return View();
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Insert)]
        public ActionResult Create(Employee obj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
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
            Message msg = null;
            if (CommonFunc.IsLocationCodeValid(obj.LocationCode))
                msg = empDao.Insert(obj);
            else
                msg = new Message(MessageConstants.E0030, MessageType.Error, "Work location");
            if (msg.MsgType == MessageType.Info)
            {
                msg = empDao.InsertTracking(obj);
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
                    // remove filter conditions
                    Session.Remove(SessionKey.EMPLOYEE_DEFAULT_VALUE);
                }
            }
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update)]
        public ActionResult Edit(string id)
        {
            Employee emp = empDao.GetById(id);
            ViewData["MarriedStatus"] = new SelectList(Constants.MarriedStatus, "Value", "Text", emp.MarriedStatus);

            ViewData["Nationality"] = new SelectList(Constants.Nationality, "Value", "Text", emp.Nationality);
            ViewData["PermanentCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.PermanentCountry);
            ViewData["TempCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.TempCountry);
            ViewData["VnPermanentCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.VnPermanentCountry);
            ViewData["VnTempCountry"] = new SelectList(Constants.Nationality, "Value", "Text", emp.VnTempCountry);

            ViewData["Gender"] = new SelectList(Constants.Gender, "Value", "Text", emp.Gender);

            List<EmployeeStatus> empStatusList = empStatusDao.GetList();
            if (emp.EmpStatusId != Constants.RESIGNED)
            {
                empStatusList = empStatusList.Where(p => p.StatusId != Constants.RESIGNED).ToList();
            }
            ViewData["EmpStatusId"] = new SelectList(empStatusList, "StatusId", "StatusName", emp.EmpStatusId);

            ViewData["DepartmentName"] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", deptDao.GetDepartmentIdBySub(emp.DepartmentId));
            ViewData["DepartmentId"] = new SelectList(deptDao.GetSubListByParent(deptDao.GetDepartmentIdBySub(emp.DepartmentId).Value), "DepartmentId", "DepartmentName", emp.DepartmentId);
            //ViewData["TitleId"] = new SelectList(posDao.GetJobTitleListByDepId(deptDao.GetDepartmentIdBySub(emp.DepartmentId).Value), "ID", "DisplayName", emp.TitleId);
            ViewData["TitleId"] = new SelectList(posDao.GetJobTitleListByDepId(deptDao.GetDepartmentIdBySub(emp.DepartmentId).Value), "JobTitleId", "JobTitleName", emp.TitleId);
            ViewData["TitleLevelId"] = new SelectList(posDao.GetJobTitleLevelListByJobTitleId(jobTitleDao.GetJobTitleIdBySub(emp.TitleId).Value), "ID", "DisplayName", emp.JobTitleLevel.ID);
            ViewData["InsuranceHospitalID"] = new SelectList(hospitalDao.GetList(), "ID", "Name", emp.InsuranceHospitalID);
            return View(emp);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update)]
        public ActionResult Edit(Employee emp)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = null;
            string returnAction = "Index";
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
            DateTime deptDate = new DateTime();
            DateTime titleDate = new DateTime();
            DateTime hospitalDate = new DateTime();
            if (!string.IsNullOrEmpty(Request["departEffectDate"]))
            {
                deptDate = Convert.ToDateTime(Request["departEffectDate"]);

            }
            if (!string.IsNullOrEmpty(Request["titleEffectDate"]))
            {
                titleDate = Convert.ToDateTime(Request["titleEffectDate"]);
            }
            if (!string.IsNullOrEmpty(Request["titleLevelEffectDate"]))
            {
                titleDate = Convert.ToDateTime(Request["titleLevelEffectDate"]);
            }
            if (!string.IsNullOrEmpty(Request["hospitalEffectDate"]))
            {
                hospitalDate = Convert.ToDateTime(Request["hospitalEffectDate"]);
            }
            if (emp.EmpStatusId == Constants.RESIGNED)
            {
                emp.ResignedDate = DateTime.Parse(Request["Resigned_Date"]);
                returnAction = "EmployeeResignList";
            }
            if (CommonFunc.IsLocationCodeValid(emp.LocationCode))
                msg =empDao.Update(emp, deptDate, titleDate, hospitalDate);
            else
                msg = new Message(MessageConstants.E0030, MessageType.Error, "Work location");
             
            ShowMessage(msg);
            return RedirectToAction(returnAction);
        }

        #region validate
        /// <summary>
        /// Check ID Exits in Employee 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public JsonResult CheckIDExits(string id)
        {
            //TODO: Do the validation
            Message msg = null;
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Employee obj = empDao.GetById(id);
            if (obj != null)
            {
                msg = new Message(MessageConstants.E0020, MessageType.Error, id, "Employee");
                result.Data = msg.ToString();
            }
            else
            {
                result.Data = true;
            }
            return result;
        }

        public JsonResult CheckJRExits(string jr,string empID)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Message msg = null;

            List<sp_GetEmployeeResult> empList = empDao.GetList("", 0, 0, 0, Constants.EMPLOYEE_ACTIVE, Constants.RESIGNED).Where(p => p.ID != empID && p.JR != null && p.JR !=Constants.JOBREQUEST_SPECIAL_ID && p.JR.Contains(jr)).ToList<sp_GetEmployeeResult>();
            List<sp_GetSTTResult> sttList = new STTDao().GetList("", 0, 0, null, null, null, null, "").Where(p => p.ID != empID && p.JR != null && p.JR != Constants.JOBREQUEST_SPECIAL_ID && p.JR.Contains(jr)).ToList<sp_GetSTTResult>();
            if (empList.Count > 0 || sttList.Count > 0)
            {
                msg = new Message(MessageConstants.E0003, MessageType.Error, Constants.JOB_REQUEST_ITEM_PREFIX + jr);
                result.Data = msg.ToString();
            }
            else
            {
                result.Data = true;
            }
            return result;
        }

        public ActionResult CheckEmailExits(string email, string id, string newId)
        {
            Message msg = null;
            string userName = email.Split('@')[0];
            string emailOffice = string.Empty;
            Employee obj = null;
            STT objSTT = null;
            if (!string.IsNullOrEmpty(id))//case when edit 
            {
                obj = empDao.GetById(id);
                if (obj != null)
                {
                    emailOffice = obj.OfficeEmail;
                }
                else
                {
                    objSTT = new STTDao().GetById(id);
                    if (objSTT != null)
                    {
                        emailOffice = objSTT.OfficeEmail;
                    }
                }
            }
            if (string.IsNullOrEmpty(id) || emailOffice != email)
            {
                AuthenticateDao authenticateDao = new AuthenticateDao();
                bool isExitsinAD = false;
                bool isExitsinEmployee = false;
                isExitsinAD = authenticateDao.CheckExistInAD(userName);
                List<sp_GetEmployeeResult> empList = empDao.GetList("", 0, 0, 0, Constants.EMPLOYEE_ACTIVE, Constants.RESIGNED).Where(p => p.OfficeEmail != null && p.OfficeEmail.Equals(email)).ToList<sp_GetEmployeeResult>();
                List<sp_GetSTTResult> sttList = new STTDao().GetList("", 0, 0, null, null, null, null, "").Where(p => p.OfficeEmail != null && p.OfficeEmail.Equals(email)).ToList<sp_GetSTTResult>();
                if (empList.Count <= 0 && sttList.Count <= 0)
                {
                    isExitsinEmployee = true;
                }

                if (isExitsinEmployee == false)
                {
                    msg = new Message(MessageConstants.E0003, MessageType.Error, "Email " + email);
                }
                else if (isExitsinAD == false)
                {
                    msg = new Message(MessageConstants.E0005, MessageType.Error, "Email " + email, "system");
                }
            }
            return Json(msg);
        }

        public JsonResult SetJrApproval(string ids)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            JobRequest obj = requestDao.GetById(int.Parse(ids));
            //if (obj != null)
            //{
            //    result.Data = obj.Approval;
            //}
            //else
            //{
                result.Data = "";
            //}
            return result;
        }

        /// <summary>
        /// Check Department Effective Date
        /// </summary>
        /// <param name="displayorder"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckDepartEffectiveDate(string empId, string effectDate, string actionName)
        {
            //TODO: Do the validation                                      
            Message msg = null;
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(effectDate))
            {

                EmployeeDepartmentJobTitleTracking obj = trackingDao.GetLastByEmpId(empId);
                if (obj != null)
                {
                    if ((DateTime.Parse(effectDate) - obj.StartDate).Days > 0)
                    {
                        msg = new Message(MessageConstants.I0001, MessageType.Info, string.Empty, string.Empty);
                    }
                    else
                    {
                        switch (actionName)
                        {
                            case "Department":
                                msg = new Message(MessageConstants.E0015, MessageType.Error, "Department Effective Date (" + effectDate + ")", "Last Effective Date (" + obj.StartDate.ToString(Constants.DATETIME_FORMAT) + ")");
                                break;
                            case "JobTitle":
                                msg = new Message(MessageConstants.E0015, MessageType.Error, "Title Effective Date (" + effectDate + ")", "Last Effective Date (" + obj.StartDate.ToString(Constants.DATETIME_FORMAT) + ")");
                                break;
                        }

                    }
                }
                else
                {
                    msg = new Message(MessageConstants.I0001, MessageType.Info, string.Empty, string.Empty);
                }

                if (msg.MsgType == MessageType.Info)
                {
                    result.Data = true;
                }
                else
                {
                    result.Data = msg.MsgText;
                }
            }
            else
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                result.Data = msg.MsgText;
            }

            return result;
        }

        /// <summary>
        /// Check Hospital Effective Date
        /// </summary>
        /// <param name="displayorder"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckInsuranceHospitalEffectiveDate(string empId, string effectDate)
        {
            //TODO: Do the validation                                      
            Message msg = null;
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(effectDate))
            {

                EmployeeInsuranceHospitalTracking empTrack = hospitalDao.GetLastByEmpId(empId);
                if (empTrack != null)
                {
                    if ((DateTime.Parse(effectDate) - empTrack.StartDate).Days >= 0)
                    {
                        msg = new Message(MessageConstants.I0001, MessageType.Info, string.Empty, string.Empty);
                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0015, MessageType.Error, "Insurance Hospital Effective Date (" + effectDate + ")", "Last Effective Date (" + empTrack.StartDate.ToString(Constants.DATETIME_FORMAT) + ")");
                    }
                }
                else
                {
                    msg = new Message(MessageConstants.I0001, MessageType.Info, string.Empty, string.Empty);
                }

                if (msg.MsgType == MessageType.Info)
                {
                    result.Data = true;
                }
                else
                {
                    result.Data = msg.MsgText;
                }
            }
            else
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                result.Data = msg.MsgText;
            }

            return result;
        }

        #endregion

        #endregion

        #region Contract Renewal

        [CrmAuthorizeAttribute(Module = Modules.ContractRenewal, Rights = Permissions.Read)]
        public ActionResult ContractRenewal(string id)
        {
            TempData["EmployeeId"] = id;
            List<Contract> contractList = contractDao.GetList(id).Where(p => p.EndDate == null && p.ContractType == Constants.PERMANENT_CONTRACT).ToList<Contract>();
            if (contractList.Count > 0)
            {
                TempData["AddNew"] = contractList.Count;
            }

            Employee emp = empDao.GetById(id);
            ViewData["Employee"] = emp;
            return View();
        }

        [CrmAuthorizeAttribute(Module = Modules.ContractRenewal, Rights = Permissions.Read)]
        public ActionResult ResignContract(string id)
        {
            TempData["EmployeeId"] = id;
            Employee emp = empDao.GetById(id);
            ViewData["Employee"] = emp;
            return View();
        }

        public ActionResult GetContractRenewalJQGrid(string id)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            List<Contract> contractList = contractDao.GetList(id);

            int totalRecords = contractList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            DateTime dayNow = DateTime.Now;
            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in contractList
                    select new
                    {
                        i = m.ContractId,
                        cell = new string[] {
                            m.ContractId.ToString(),
                            m.ContractNumber,
                           CheckCurrentDate(m.StartDate,m.EndDate) == true ? "<div id='row_active' class='row_active'>"+m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW) + "</div>" : m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            m.StartDate.ToString(Constants.DATETIME_FORMAT),
                            m.EndDate.HasValue ?m.EndDate.Value.ToString(Constants.DATETIME_FORMAT):"",
                            m.EndDate.HasValue ?m.EndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",
                            m.ContractType1.ContractTypeName,                            
                            CheckPermissonViewContractFile()?CommonFunc.SplitFileName(m.ContractFile,Constants.CONTRACT_PATH,false):"",
                           CommonFunc.Button("edit", "Edit", "CRM.popup('/Employee/EditContract/" + m.ContractId + "', ' Update Contract ' , 550)") 
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private bool CheckPermissonViewContractFile()
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            bool isAuthorize = CommonFunc.CheckAuthorized(principal.UserData.UserID, (int)Modules.ContractRenewal, (int)Permissions.ViewContract);
            return isAuthorize;
        }

        public ActionResult GetResignContractJQGrid(string id)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            List<Contract> contractList = contractDao.GetList(id);

            int totalRecords = contractList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(contractList.Count, pageIndex, rowCount);
            DateTime dayNow = DateTime.Now;
            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in contractList
                    select new
                    {
                        i = m.ContractId,
                        cell = new string[] {
                            m.ContractId.ToString(),
                            CheckCurrentDate(m.StartDate,m.EndDate) == true ?  "<div style='color:"+Constants.ROW_ACTIVE_COLOR+";'>"+m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW) + "</div>" : "<div>" + m.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW) + "</div>",
                            m.StartDate.ToString(Constants.DATETIME_FORMAT),
                            m.EndDate.HasValue ?m.EndDate.Value.ToString(Constants.DATETIME_FORMAT):"",
                            m.EndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW),
                            m.ContractType1.ContractTypeName,
                            CommonFunc.SplitFileName(m.ContractFile,Constants.CONTRACT_PATH,false)                                    
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #region Action For Contract

        /// <summary>
        /// Set Row Is Active
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private bool CheckCurrentDate(DateTime startDate, DateTime? endDate)
        {
            bool result = false;
            if (endDate != null)
            {
                if (DateTime.Now <= endDate && DateTime.Now >= startDate)
                    result = true;
            }
            else
            {
                if (DateTime.Now >= startDate)
                    result = true;
            }
            return result;
        }

        /// <summary>
        /// Check Multi Upload
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Automatic Set End Date when Dropdownlist is selected
        /// </summary>
        /// <param name="startDateUI"></param>
        /// <param name="contractType"></param>
        /// <returns></returns>
        public JsonResult SetEndDate(string startDateUI, int contractType, string duration)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(startDateUI))
            {
                DateTime startDate = DateTime.Parse(startDateUI);
                DateTime endDate = new DateTime();
                if (duration == "0" || string.IsNullOrEmpty(duration))
                {
                    result.Data = 0;
                }
                else
                {
                    endDate = startDate.AddMonths(int.Parse(duration)).AddDays(-1);
                    result.Data = endDate.ToString(Constants.DATETIME_FORMAT);
                }
            }
            else
            {
                result.Data = false;
            }
            return result;

        }

        #region Validate
        /// <summary>
        /// Set Row End Date shoe/hide 
        /// </summary>
        /// <param name="contractTypeId"></param>
        /// <returns></returns>
        public JsonResult CheckDurationOfContract(int contractTypeId)
        {
            //TODO: Do the validation  
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            ContractType obj = contractDao.GetContractTypeById(contractTypeId);
            if (obj.Duration == null)
            {
                result.Data = false;
            }
            else
            {
                result.Data = obj.Duration;
            }
            return result;
        }

        /// <summary>
        /// Check ContracType is Exits
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="contractType"></param>
        /// <param name="contractName"></param>
        /// <returns></returns>
        public JsonResult CheckContractedType(string empId, int contractType, string contractName, string contractId)
        {
            //TODO: Do the validation
            Message msg = null;
            JsonResult result = new JsonResult();
            List<Contract> obj = new List<Contract>();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (string.IsNullOrEmpty(contractId))
            {
                obj = contractDao.GetList(empId).Where(p => p.ContractType == contractType).ToList<Contract>();
            }
            else
            {
                obj = contractDao.GetList(empId).Where(p => (p.ContractType == contractType) && (p.ContractId != int.Parse(contractId))).ToList<Contract>();
            }
            if (obj.Count > 0)
            {
                msg = new Message(MessageConstants.E0003, MessageType.Error, contractName);
                result.Data = msg.ToString();
            }
            else
            {
                result.Data = true;
            }
            return result;
        }

        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult RemoveContractCV(string cv, string id)
        {
            Message msg = null;
            Contract obj = null;
            if (Request.IsAjaxRequest())
            {
                obj = contractDao.GetById(int.Parse(id));
                if (obj != null)
                {
                    //cv = obj.ContractFile;
                    string[] array = obj.ContractFile.TrimEnd(',').Split(',');
                    if (array.Count() > 1)  //case if upload multi file
                    {
                        if (obj.ContractFile.Contains(cv))
                        {
                            obj.ContractFile = obj.ContractFile.Replace(cv + ",", "");
                        }
                        msg = contractDao.UpdateCV(obj);
                        if (msg.MsgType == MessageType.Info)
                        {
                            string strPath = Server.MapPath(Constants.CONTRACT_PATH);
                            strPath += "\\" + cv;
                            System.IO.File.Delete(strPath);
                            if (!System.IO.File.Exists(strPath))
                            {
                                msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee CV", "deleted");
                            }
                            else
                            {
                                msg = new Message(MessageConstants.E0014, MessageType.Error, "delete employee CV ");
                            }
                        }
                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0018, MessageType.Error, "Contract", 1);
                    }
                }
                else
                {
                    msg = new Message(MessageConstants.E0014, MessageType.Error, "delete employee CV ");
                }
            }
            return Json(msg);
        }

        #endregion

        #endregion

        [CrmAuthorizeAttribute(Module = Modules.ContractRenewal, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult CreateContract(string id)
        {
            Employee emp = empDao.GetById(id);
            Contract contractDate = contractDao.GetLastByEmpId(id);
            ViewData["LastEndDate"] = Constants.DATE_MIN;
            if (contractDate != null)
            {
                ViewData["LastEndDate"] = contractDate.EndDate.Value;
            }
            ViewData["ContractedDate"] = emp.StartDate.ToString(Constants.DATETIME_FORMAT);
            ViewData["EmployeeId"] = id;
            ViewData["ContractType"] = new SelectList(contractDao.GetListContractType(), "ContractTypeId", "ContractTypeName", "");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.ContractRenewal, Rights = Permissions.Insert, ShowInPopup = true)]
        public FileUploadJsonResult CreateContract(Contract obj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            FileUploadJsonResult result = new FileUploadJsonResult();
            Message msg = CheckFileUpload();
            string contractFileName = string.Empty;
            if (msg == null) //case sussessfully
            {
                int y = 0;
                Employee empInfo = empDao.GetById(obj.EmployeeId);

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[y] as HttpPostedFileBase;
                    if (!string.IsNullOrEmpty(Path.GetExtension(hpf.FileName)))
                    {
                        string extension = Path.GetExtension(hpf.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(hpf.FileName);
                        string strPath = Server.MapPath(Constants.CONTRACT_PATH);
                        string contractName = obj.EmployeeId + "_" + empInfo.FirstName + "_" + empInfo.LastName + "_" + fileName +
                            obj.StartDate.ToString(Constants.UNIQUE_CONTRACT) + DateTime.Now.ToString(Constants.UNIQUE_TIME) + extension;
                        contractName = ConvertUtil.FormatFileName(contractName);
                        strPath = strPath + "\\" + contractName;
                        hpf.SaveAs(strPath);
                        contractFileName += contractName + Constants.FILE_STRING_PREFIX;
                    }
                    y++;
                }
                obj.CreatedBy = principal.UserData.UserName;
                obj.UpdatedBy = principal.UserData.UserName;
                obj.ContractFile = contractFileName;
                msg = contractDao.Insert(obj);
                if (msg.MsgType == MessageType.Info)
                {
                    List<Contract> contractList = contractDao.GetList(obj.EmployeeId);
                    if (contractList.Count > 0)
                    {
                        foreach (Contract item in contractList)
                        {
                            if (item.EndDate <= obj.StartDate)
                            {
                                contractDao.UpdateNotification(item.ContractId);
                            }
                        }
                    }
                }
                result.Data = new { msg };
            }
            else
            {
                result.Data = new { msg };
            }
            return result;
        }

        [CrmAuthorizeAttribute(Module = Modules.ContractRenewal, Rights = Permissions.Delete)]
        public ActionResult DeleteContractList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            bool isValid = true;
            Message msg = null;
            string empId = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                id = id.TrimEnd(Constants.SEPARATE_INVOLVE_CHAR);
                string[] idArr = id.Split(Constants.SEPARATE_INVOLVE_CHAR);
                int contractID = 0;
                foreach (string contractId in idArr)
                {
                    bool canDelete = Int32.TryParse(contractId, out contractID);

                    Contract contract = contractDao.GetById(contractID);
                    if (contract != null)
                    {
                        empId = contract.EmployeeId.ToString();
                        //check contract is active
                        if (contract.EndDate.HasValue && contract.EndDate.Value >= DateTime.Now && DateTime.Now >= contract.StartDate)
                        {
                            isValid = false;
                            break;
                        }
                        else if (contract.EndDate <= DateTime.Now && contract.StartDate <= DateTime.Now)
                        {
                            isValid = false;
                            break;
                        }
                        else if (!contract.EndDate.HasValue && DateTime.Now >= contract.StartDate)
                        {
                            isValid = false;
                            break;
                        }

                        bool isComplete = true;
                        string strPath = Server.MapPath(Constants.CONTRACT_PATH);
                        foreach (string item in contract.ContractFile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX))
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                strPath += item;
                                System.IO.File.Delete(strPath);
                                if (System.IO.File.Exists(strPath))
                                {
                                    isComplete = false;
                                }
                            }
                        }
                        if (isComplete)
                        {
                            msg = new Message(MessageConstants.I0001, MessageType.Info, idArr.Count().ToString() + " Contract(s)", "deleted");
                            contract.UpdatedBy = principal.UserData.UserName;
                            contractDao.Delete(contract);
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0014, MessageType.Error, "delete contract(s)");
                        }

                    }
                }
            }
            if (!isValid)
            {
                msg = new Message(MessageConstants.E0016, MessageType.Error);
            }
            ShowMessage(msg);
            return RedirectToAction("ContractRenewal/" + empId);
        }

        [CrmAuthorizeAttribute(Module = Modules.ContractRenewal, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult EditContract(int id)
        {
            Contract obj = contractDao.GetById(id);
            Employee emp = empDao.GetById(obj.EmployeeId);
            Contract contractDate = contractDao.GetLastByEmpId(obj.EmployeeId);
            ViewData["LastEndDate"] = Constants.DATE_MIN;
            if (contractDate != null && contractDate.EndDate.HasValue)
            {
                ViewData["LastEndDate"] = contractDate.EndDate.Value;
            }
            ViewData["ContractedDate"] = emp.StartDate.ToString(Constants.DATETIME_FORMAT);
            ViewData["ContractType"] = new SelectList(contractDao.GetListContractType(), "ContractTypeId", "ContractTypeName", obj.ContractType);
            return View(obj);
        }

        [HttpPost]
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.ContractRenewal, Rights = Permissions.Update, ShowInPopup = true)]
        public FileUploadJsonResult EditContract(Contract obj)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            FileUploadJsonResult result = new FileUploadJsonResult();
            string oldFile = string.Empty;
            string serverPath = Server.MapPath(Constants.CONTRACT_PATH);
            Message msg = CheckFileUpload();
            string contractFileName = string.Empty;
            if (msg == null) //case sussessfully
            {
                Contract contract = contractDao.GetById(obj.ContractId);
                if (contract != null)
                {
                    int y = 0;
                    Employee empInfo = empDao.GetById(obj.EmployeeId);
                    string hidDeleteFile = Request["hidDeleteFile"];
                    if (!string.IsNullOrEmpty(hidDeleteFile)) //case delete file
                    {
                        string[] arrDeleteFile = hidDeleteFile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);
                        foreach (string deleteFile in arrDeleteFile)
                        {
                            oldFile = contract.ContractFile.TrimEnd(Constants.FILE_CHAR_PREFIX);
                            if (oldFile.Split(Constants.FILE_CHAR_PREFIX).Contains(deleteFile))
                            {
                                contract.ContractFile = contract.ContractFile.Replace(deleteFile + Constants.FILE_STRING_PREFIX, "");
                            }
                        }
                    }
                    if (msg == null) //case sussessfully
                    {
                        foreach (string file in Request.Files) //case add new file
                        {
                            HttpPostedFileBase hpf = Request.Files[y] as HttpPostedFileBase;
                            if(!string.IsNullOrEmpty(Path.GetExtension(hpf.FileName)))
                            {
                            string extension = Path.GetExtension(hpf.FileName);
                            string fileName = Path.GetFileNameWithoutExtension(hpf.FileName);
                            string contractName = obj.EmployeeId + "_" + empInfo.FirstName + "_" + empInfo.LastName + "_" + fileName +
                                obj.StartDate.ToString(Constants.UNIQUE_CONTRACT) + DateTime.Now.ToString(Constants.UNIQUE_TIME) + extension;
                            contractName = ConvertUtil.FormatFileName(contractName);
                            string strPath = serverPath + "\\" + contractName;
                            hpf.SaveAs(strPath);
                            contract.ContractFile += contractName + Constants.FILE_STRING_PREFIX;
                            }
                            y++;
                        }
                    }
                    contract.ContractType = obj.ContractType;
                    contract.ContractNumber = obj.ContractNumber;
                    contract.StartDate = obj.StartDate;
                    contract.EndDate = obj.EndDate;
                    contract.Comment = obj.Comment;
                    contract.UpdatedBy = principal.UserData.UserName;
                    string startDate = obj.StartDate.ToString(Constants.DATETIME_FORMAT_DB);
                    string endDate = obj.EndDate.HasValue ? obj.EndDate.Value.ToString(Constants.DATETIME_FORMAT_DB) : null;
                    bool isOverlap = contractDao.CheckOverlapDate(obj.ContractId, obj.EmployeeId, startDate, endDate);
                    if (isOverlap == false)
                    {
                        msg = contractDao.Update(contract);
                        if (msg.MsgType == MessageType.Info)
                        {
                            if (!string.IsNullOrEmpty(hidDeleteFile))
                            {
                                string[] arrDeleteFile = hidDeleteFile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);
                                foreach (string deleteFile in arrDeleteFile)
                                {
                                    string strPath = serverPath + "\\" + deleteFile;
                                    if (System.IO.File.Exists(strPath))
                                    {
                                        System.IO.File.Delete(strPath);
                                    }
                                }
                            }
                            if (contract.EndDate.HasValue)
                            {
                                Contract objLastDate = contractDao.GetLastByEmpId(contract.EmployeeId);
                                if (objLastDate.EndDate == contract.StartDate)
                                {
                                    List<Contract> contractList = contractDao.GetList(obj.EmployeeId);
                                    if (contractList.Count > 0)
                                    {
                                        foreach (Contract item in contractList)
                                        {
                                            contractDao.UpdateNotification(item.ContractId);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ContractType objContract = contractDao.GetContractTypeById(obj.ContractType);
                        msg = new Message(MessageConstants.E0019, MessageType.Error, objContract.ContractTypeName, obj.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW) + ((obj.EndDate.HasValue) ? " to " + obj.EndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : ""));
                    }
                    result.Data = new { msg };
                }
            }
            else
            {
                result.Data = new { msg };
            }
            return result;
        }

        #endregion

        #region tooltip
        public ActionResult EmployeeToolTip(string id)
        {
            Employee emp = empDao.GetById(id);
            return View(emp);
        }
        #endregion

        #region List Of Hospital

        public ActionResult ListHospital()
        {
            return View();
        }

        public ActionResult GetListHospitalGrid()
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;

            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            List<InsuranceHospital> list = hospitalDao.GetList(); ;

            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = hospitalDao.Sort(list, sortColumn, sortOrder)
                                  .Skip((currentPage - 1) * rowCount)
                                   .Take(rowCount);


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
                            CommonFunc.Link(m.ID,"javascript:ChooseHospital(\""+m.ID+"\");",m.Name,false),
                           // "<a id='"+m.ID.ToString()+"' href=\"javascript:ChooseHospital('"+m.ID.ToString()+"');\">"+ m.Name +"</a>",
                            m.Address,                            
                            m.IsPublic == true?"Yes":"No"
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Performance Review
        public ActionResult PerformanceReview(string id)
        {
            Employee obj = empDao.GetById(id);
            TempData[CommonDataKey.TEMP_EMPLOYEE_PR] = id;
            return View(obj);
        }

        public ActionResult GetListPerformanceReview()
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            string id = (string)TempData[CommonDataKey.TEMP_EMPLOYEE_PR];
            List<sp_GetPerformanceReviewListResult> list = GetListPRByEmployee(id);

            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            int index = 0;
            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in list
                    select new
                    {
                        i = m.ID,
                        cell = new string[] {
                            index == 0? "<div id='row_active' class='row_active'>"+m.ID.ToString()+"</div>":m.ID.ToString(),
                            m.ManagerName,
                            m.PRDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            m.NextReviewDate.HasValue?m.NextReviewDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):string.Empty,
                            m.StatusName,
                            m.ResolutionName,
                            (index++).ToString()
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private List<sp_GetPerformanceReviewListResult> GetListPRByEmployee(string employeeID)
        {
            List<sp_GetPerformanceReviewListResult> list = new PerformanceReviewDao().GetList(null, 0, 0)
                .Where(q => q.EmployeeID == employeeID).OrderByDescending(q => q.ID).ToList();
            return list;
        }
        #endregion
    }
}