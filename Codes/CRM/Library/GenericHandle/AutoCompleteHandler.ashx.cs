using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Models;
using CRM.Library.Common;
using CRM.Areas.Portal.Models;

namespace CRM.Library.GenericHandle
{
    /// <summary>
    /// Summary description for AutoCompleteHandler
    /// </summary>
    public class AutoCompleteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string name = context.Request.QueryString["q"];
            string pageName = context.Request.QueryString["Page"];
            
            #region pageName
            switch (pageName)
            {
                case "JRAdmin":
                    //JRAdminDao jrDao = new JRAdminDao();
                    //List<sp_GetJRForAdminResult> listJRAdmin = jrDao.GetListByName(name, 0, 0);
                    //var finalListJRAdmin = listJRAdmin.Select(p => p.UserName).Distinct();
                    //foreach (string item in finalListJRAdmin)
                    //{
                    //    context.Response.Write(item + Environment.NewLine);
                    //}
                  
                    UserAdminDao userAdminDao = new UserAdminDao();
                    List<UserAdmin> listUserAdmin = userAdminDao.GetList().Where(p => p.UserName.ToLower().Contains(name)).ToList<UserAdmin>();
                    foreach (UserAdmin item in listUserAdmin)
                    {
                        Employee objEmployee = new EmployeeDao().GetByOfficeEmailInActiveList(item.UserName + Constants.PREFIX_EMAIL_LOGIGEAR);
                        string displayName = item.UserName;
                        if (objEmployee != null)
                        {
                            displayName += " - " + objEmployee.ID;
                        }
                        context.Response.Write(displayName + Environment.NewLine);
                    }
                    break;
                case "Group":
                    GroupDao groupDao = new GroupDao();
                    List<Group> listGroup = groupDao.GetListByName(name);
                    foreach (Group item in listGroup)
                    {
                        context.Response.Write(item.GroupName + Environment.NewLine);
                    }
                    break;
                case "Employee":
                    EmployeeDao empDao = new EmployeeDao();
                    int isActive = int.Parse(context.Request.QueryString["IsActive"]);
                    List<sp_GetEmployeeResult> empList = new List<sp_GetEmployeeResult>();
                    if (isActive == Constants.EMPLOYEE_ACTIVE)
                    {
                        empList = empDao.GetListByName(name, isActive, Constants.RESIGNED);
                    }
                    else if (isActive == Constants.EMPLOYEE_NOT_ACTIVE)
                    {
                        empList = empDao.GetListByName(name, isActive, Constants.RESIGNED);
                    }
                    //var finalListEmployee = empList.Select(p => p.DisplayName).Distinct();
                    foreach (sp_GetEmployeeResult item in empList)
                    {
                        context.Response.Write(item.DisplayName + " - " + item.ID + Environment.NewLine);
                    }
                    break;
                case "AssignEmployee":
                    ExamDao examDao = new ExamDao();
                    EmployeeDao employeeDao = new EmployeeDao();
                    int examID = int.Parse(context.Request.QueryString[CommonDataKey.EXAM_ID]);
                    List<sp_GetEmployeeResult> employeeList = new List<sp_GetEmployeeResult>();
                    employeeList = employeeDao.GetListByName(name, Constants.EMPLOYEE_ACTIVE, Constants.RESIGNED);
                    List<string> assignedEmployeeListID = examDao.AssignedEmployeeListID(examID);
                    employeeList = employeeList.Where(c => !assignedEmployeeListID.Contains(c.ID)).ToList<sp_GetEmployeeResult>();

                    var finalListEmp = employeeList.Select(p => p.DisplayName).Distinct();
                    foreach (string item in finalListEmp)
                    {
                        context.Response.Write(item + Environment.NewLine);
                    }

                    break;
                case "UserAdmin":
                    //UserAdminDao userAdminDao = new UserAdminDao();
                    //List<UserAdmin> listUserAdmin = userAdminDao.GetList().Where(p => p.UserName.ToLower().Contains(name)).ToList<UserAdmin>();
                    //foreach (UserAdmin item in listUserAdmin)
                    //{
                    //    context.Response.Write(item.UserName + Environment.NewLine);
                    //}
                    List<String> listUser = new List<string>();
                    System.DirectoryServices.SearchResultCollection entriesUser = CommonFunc.GetDomainUserList(name);

                    foreach (System.DirectoryServices.SearchResult searchResult in entriesUser)
                    {
                        System.DirectoryServices.DirectoryEntry entry = searchResult.GetDirectoryEntry();

                        if (entry.Properties["mail"] != null)
                        {
                            if (entry.Properties["mail"].Count > 0)
                            {

                                string displayName = entry.Properties[CommonFunc.GetDomainUserProperty(DomainUserProperty.LoginName)][0].ToString();
                                //string emailAddress = entry.Properties[CommonFunc.GetDomainUserProperty(DomainUserProperty.OutlookEmail)][0].ToString();
                                Employee objEmployee = new EmployeeDao().GetByOfficeEmailInActiveList(displayName + Constants.PREFIX_EMAIL_LOGIGEAR);
                                string item = displayName;
                                if (objEmployee != null)
                                {
                                    item += " - " + objEmployee.ID;
                                }
                                // +" &lt;" + emailAddress + "&gt;";
                                context.Response.Write(item + Environment.NewLine);
                            }
                        }
                    }
                    break;
                case "UserLogs":
                    //List<sp_LogMasterResult> logList = new LogDao().GetList("", "");
                    //var finalList = logList.Select(p => p.UserName).Distinct();

                    List<CRM.Models.Entities.MasterLogEntity> logList = new LogDao().GetQueryList("", "").ToList();
                    var finalList = logList.Select(p => p.UserName).Distinct();
                    foreach (string item in finalList)
                    {
                        context.Response.Write(item + Environment.NewLine);
                    }
                    break;

                case "STT":
                    STTDao sttDao = new STTDao();
                    List<sp_GetSTTResult> listSTT = sttDao.GetListByName(name);
                    foreach (sp_GetSTTResult item in listSTT)
                    {
                        context.Response.Write(item.DisplayName + " - " + item.ID + Environment.NewLine);
                    }
                    break;

                case "Candidate":
                    CandidateDao candidateDao = new CandidateDao();
                    List<sp_GetCandidateResult> result = candidateDao.GetList(name, 0, 0, 0, "", "", 0);
                    List<String> lstDisplayName = result.Select(p => p.DisplayName).ToList();
                    foreach (string item in lstDisplayName)
                    {
                        context.Response.Write(item + Environment.NewLine);
                    }
                    break;
                case "AssignCandidate":
                    ExamDao exDao = new ExamDao();
                    CandidateDao canDao = new CandidateDao();
                    int exID = int.Parse(context.Request.QueryString[CommonDataKey.EXAM_ID]);
                    List<sp_GetCandidateResult> canList = canDao.GetList(name, 0, 0, 0, "", "", 0);
                    List<int> assignedCandidateListID = exDao.AssignedCandidateListID(exID);
                    canList = canList.Where(c => !assignedCandidateListID.Contains(c.ID)).ToList<sp_GetCandidateResult>();
                    List<String> canListDisplayName = canList.Select(p => p.DisplayName).ToList();
                    foreach (string item in canListDisplayName)
                    {
                        context.Response.Write(item + Environment.NewLine);
                    }
                    break;
                case "JR":
                    string action = context.Request.QueryString["Action"];

                    if (string.IsNullOrEmpty(action) || action == "undefined")
                    {
                        List<sp_GetJobRequestCompleteResult> listJr = new List<sp_GetJobRequestCompleteResult>();
                        listJr = new JobRequestDao().GetJRListComplete(name, 0, 0, 0, null, 0);
                        foreach (sp_GetJobRequestCompleteResult item in listJr)
                        {
                            context.Response.Write(Constants.JOB_REQUEST_ITEM_PREFIX + item.ID + Environment.NewLine);
                        }
                    }
                    else
                    {
                        List<sp_GetJobRequestCompleteInterviewResult> listJr = new List<sp_GetJobRequestCompleteInterviewResult>();
                        name = name.Replace("j", "");
                        name = name.Replace("r", "");
                        name = name.Replace("-", "");
                        listJr = new JobRequestDao().GetJRListInterView(name, 0, 0, 0, 0);

                        foreach (sp_GetJobRequestCompleteInterviewResult item in listJr)
                        {
                            context.Response.Write(Constants.JOB_REQUEST_ITEM_PREFIX + item.ID + Environment.NewLine);
                        }
                    }

                    break;
                case "JRIndex":
                    string containsJR = Constants.JOB_REQUEST_PREFIX;
                    JobRequestItemDao jrItemDao = new JobRequestItemDao ();
                    if (containsJR.ToLower().Contains(name.ToLower()) || 
                        Constants.JOB_REQUEST_ITEM_PREFIX.ToLower().Contains(name.ToLower()))
                    {
                        name = string.Empty;
                    }
                    string assignRole = context.Request.QueryString["Role"];
                    List<sp_GetJobRequestResult> listJrIndex =
                        new JobRequestDao().GetList(name, 0, 0, 0, 0, 0, assignRole, 0);
                    foreach (sp_GetJobRequestResult item in listJrIndex)
                    {
                        context.Response.Write(Constants.JOB_REQUEST_PREFIX + item.ID + Environment.NewLine);
                        List<JobRequestItem> jrItems = jrItemDao.GetListByJrId(item.ID);
                        foreach (JobRequestItem subItem in jrItems)
                        {
                            if (subItem.ID.ToString().ToLower().Contains(name.ToLower()))
                            {
                                context.Response.Write(Constants.JOB_REQUEST_ITEM_PREFIX + subItem.ID + Environment.NewLine);
                            }
                        }
                    }
                    break;
                case "SendMail":
                    List<String> list = new List<string>();
                    System.DirectoryServices.SearchResultCollection entries = CommonFunc.GetDomainUserList(name);

                    foreach (System.DirectoryServices.SearchResult searchResult in entries)
                    {
                        System.DirectoryServices.DirectoryEntry entry = searchResult.GetDirectoryEntry();

                        if (entry.Properties["mail"] != null)
                        {
                            if (entry.Properties["mail"].Count > 0)
                            {

                                string displayName = entry.Properties[CommonFunc.GetDomainUserProperty(DomainUserProperty.LoginName)][0].ToString();
                                //string emailAddress = entry.Properties[CommonFunc.GetDomainUserProperty(DomainUserProperty.OutlookEmail)][0].ToString();
                                string item = displayName;// +" &lt;" + emailAddress + "&gt;";
                                context.Response.Write(item + Environment.NewLine);
                            }
                        }
                    }

                    break;
                case "PRIndex":
                    string containsPR = Constants.PR_REQUEST_PREFIX;
                    if (containsPR.ToLower().Contains(name))
                    {
                        name = string.Empty;
                    }
                    string roles = context.Request.QueryString["Role"];
                    string isViewAll = context.Request.QueryString["IsViewAll"];
                    int requestorId = 0;
                    string loginName = context.Request.QueryString["loginName"];
                    if (isViewAll.Equals("False") && roles == Constants.PR_REQUESTOR_ID.ToString())
                    {
                        requestorId = int.Parse(context.Request.QueryString["UserLoginId"]);
                    }

                    //List<sp_GetPurchaseRequestResult> listPrIndex = new PurchaseRequestDao().GetList(name, 0, 0, requestorId, 0, 0, roles, requestorId, loginName);
                    //List<sp_GetPurchaseRequestResult> listPrIndex = new PurchaseRequestDao().GetList(name, 0, 0, 0, 0, null, requestorId, int.Parse(roles), 0, false);
                    //chau.ly update 14.02.2012
                    List<sp_GetPurchaseRequestResult> listPrIndex = new PurchaseRequestDao().GetList(name, 0, 0, 0, 0, null, null, requestorId, int.Parse(roles), 0, false);
                    foreach (sp_GetPurchaseRequestResult item in listPrIndex)
                    {
                        context.Response.Write(Constants.PR_REQUEST_PREFIX + item.ID + Environment.NewLine);
                    }
                    break;

                case "Question":
                    QuestionDao qDao = new QuestionDao();
                    List<LOT_Question> listQuestion = qDao.GetListAutoComplete(name);
                    foreach (LOT_Question item in listQuestion)
                    {
                        context.Response.Write(CommonFunc.TruncateAroundSubText(item.QuestionContent, name) + Environment.NewLine);
                    }
                    break;
                case "Manager":
                    List<sp_GetManagerResult> listManager = new EmployeeDao().GetManager(name, 0, 0);
                    foreach (sp_GetManagerResult item in listManager)
                    {
                        context.Response.Write(item.DisplayName + Environment.NewLine);
                    }
                    break;
                case "University":
                    List<University> listUniversity = new CandidateDao().GetUniversityList("");
                    listUniversity = listUniversity.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList<University>();
                    foreach (University item in listUniversity)
                    {
                        context.Response.Write(item.Name + Environment.NewLine);
                    }
                    break;
                case "PTOAdmin":
                    List<sp_GetEmployeeManagerResult> listEmployeeManagerForAdmin = new EmployeeDao().GetEmployeeManager(name);
                    foreach (sp_GetEmployeeManagerResult item in listEmployeeManagerForAdmin)
                    {
                        string displayName = item.OfficeEmail.Replace(Constants.PREFIX_EMAIL_LOGIGEAR, "") + " - " + item.ID;
                        context.Response.Write(displayName + Environment.NewLine);
                    }
                    break;
                case "PTO_User":
                    var listEmployeeManager = new EmployeeDao().GetManager(name, 0, 0);
                    empDao = new EmployeeDao();
                    foreach (var item in listEmployeeManager)
                    {
                        context.Response.Write(empDao.FullName(item.ID, Constants.FullNameFormat.FirstMiddleLast) + " - " + item.ID + Environment.NewLine);
                    }
                    break;
                case "PTOCC":
                    EmployeeDao emplDao = new EmployeeDao();
                    List<sp_GetEmployeeResult> listEmpl = emplDao.GetListByName(name, Constants.EMPLOYEE_ACTIVE,
                                                                                Constants.RESIGNED);
                    string content = "";
                    foreach (sp_GetEmployeeResult item in listEmpl)
                    {
                        if (item.OfficeEmail != null)
                        {
                            string[] aTmp = item.OfficeEmail.Split('@');
                            content += aTmp[0] + Environment.NewLine;
                        }
                    }
                    context.Response.Write(content);
                    break;
                case "EmployeeWithID":
                    var empListWithId = new EmployeeDao().GetListByOfficeEmail(name).
                        Select(p => new { p.ID, p.OfficeEmail });
                    foreach (var item in empListWithId)
                    {
                        string displayName = item.OfficeEmail.Remove(item.OfficeEmail.IndexOf("@")) + " - " + item.ID;
                        context.Response.Write(displayName + Environment.NewLine);
                    }
                    break;
                case "ManagerWithID":
                    var mgrList = new EmployeeDao().GetEmployeeManager(name);
                    foreach (sp_GetEmployeeManagerResult item in mgrList)
                    {
                        string displayName = item.OfficeEmail.Remove(item.OfficeEmail.IndexOf("@")) + " - " + item.ID;
                        context.Response.Write(displayName + Environment.NewLine);
                    }
                    break;
                case "JobTitleLevel":
                    var jobTitleLevelList = new JobTitleLevelDao().GetList().Where(c => c.DisplayName.ToLower().Contains(name.Trim().ToLower()));
                    foreach (JobTitleLevel item in jobTitleLevelList)
                    {
                        string displayName = item.DisplayName;
                        context.Response.Write(displayName + Environment.NewLine);
                    }
                    break;
                case "EditPosition":
                    var mgrList_EP = new EmployeeDao().GetListManagerWithAllAttr(name);
                    empDao = new EmployeeDao();
                    foreach (Employee item in mgrList_EP)
                    {
                        string displayName = empDao.FullName(item.ID,
                            Constants.FullNameFormat.FirstMiddleLast) + " - " + item.ID;
                        context.Response.Write(displayName + Environment.NewLine);
                    }
                    break;
                case "Position":
                    empDao = new EmployeeDao();
                    string deptId = context.Request.QueryString["deptId"];
                    string subDeptId = context.Request.QueryString["subDeptId"];
                    string titleId = context.Request.QueryString["titleId"];                    
                    string project = context.Request.QueryString["project"];
                    string locationCode = null;
                    var positionList = empDao.GetListEmployeeAndSTT(name, ConvertUtil.ConvertToInt(deptId), ConvertUtil.ConvertToInt(subDeptId),
                        ConvertUtil.ConvertToInt(titleId),project, locationCode);
                    /*foreach (Employee item in positionList)
                    {
                        string displayName = CommonFunc.GetEmployeeFullName(item,
                            Constants.FullNameFormat.FirstMiddleLast);
                        context.Response.Write(displayName + Environment.NewLine);
                    }*/
                    foreach (sp_GetPositionResult item in positionList)
                    {
                        string displayName = item.FullName;
                        context.Response.Write(displayName + Environment.NewLine);
                    }
                    break;
                case "ManagerListPR":
                    string sStatus = context.Request.QueryString["status"];
                    string sNeed = context.Request.QueryString["need"];
                    var userName = HttpContext.Current.User.Identity.Name;

                    empDao = new EmployeeDao();
                    var prlist_Manager = empDao.GetPRList(CommonFunc.GetEmployeeByUserName(userName).ID,
                        name, sStatus, sNeed.Equals(Constants.PER_REVIEW_NEED_PR_LIST));
                    foreach (sp_GetEmployeesForPRResult item in prlist_Manager)
                    {
                        string displayName = empDao.FullName(item.ID,
                            Constants.FullNameFormat.FirstMiddleLast);
                        context.Response.Write(displayName + Environment.NewLine);
                    }
                    break;
                case "HRPR":
                    string status = context.Request.QueryString["status"];
                    string need = context.Request.QueryString["need"];

                    if (string.IsNullOrEmpty(status))
                        status = "0";
                    PerformanceReviewDao perDao = new PerformanceReviewDao();
                    var prlist = perDao.GetList(name, int.Parse(status), need.Equals(Constants.PER_REVIEW_NEED_PR_LIST), Constants.PRW_ROLE_HR_ID.ToString());
                    foreach (sp_GetEmployeesPRForHRResult item in prlist)
                    {
                        context.Response.Write(item.EmployeeName + Environment.NewLine);
                    }
                    break;
                case "PTOManager":
                    DateTime? from = null;
                    DateTime? to = null;
                    string month = context.Request.QueryString["month"];
                    string managerID = context.Request.QueryString["managerId"];
                    if (!string.IsNullOrEmpty(month))
                    {
                        try
                        {
                            // filter month is date end of month and this date dependent on configuration
                            // ex: month:Feb-2011 => from: 25-Feb-2011, to: 26-Feb-2011
                            to = DateTime.Parse(Constants.DATE_LOCK_PTO + month);
                            from = to.Value.AddMonths(-1).AddDays(1);
                        }
                        catch
                        {
                            // get mon by curent date
                            month = DateTime.Now.ToString(Constants.PTO_MANAGER_DATE_FORMAT);
                            to = DateTime.Parse(Constants.DATE_LOCK_PTO + month);
                            from = to.Value.AddMonths(-1).AddDays(1);
                        }
                    }
                    List<sp_GetPTOEmployeeListForManagerResult> listPTOUser = new PTODao().GetPTOEmpListForManager(name, 0, 0, 0, from, to, managerID,null);
                    List<string> finalListPTOUser = listPTOUser.Select(p => p.Submitter + " - " + p.EmployeeID).Distinct().ToList<string>();
                    foreach (string item in finalListPTOUser)
                    {
                        context.Response.Write(item + Environment.NewLine);
                    }
                    break;
                case "WorkFlow":
                    
                    int wfID = int.Parse(context.Request.QueryString["workflowID"]);                    
                    
                    List<sp_GetJRForAdminResult> listUserAdminRole = new JRAdminDao().GetListByName(name, wfID, 0);
                    List<string> listStringName = listUserAdminRole.Select(q => q.UserName).Distinct().ToList();
                    foreach (string item in listStringName)
                    {
                        Employee objEmployee = new EmployeeDao().GetByOfficeEmailInActiveList(item + Constants.PREFIX_EMAIL_LOGIGEAR);
                        string displayName = item;
                        if (objEmployee != null)
                        {
                            displayName += " - " + objEmployee.ID;
                        }
                        context.Response.Write(displayName + Environment.NewLine);
                    }
                    break;
                case "LocationEmployee":
                    EmployeeDao empDao1 = new EmployeeDao();
                    List<sp_GetListEmployeeSttNotSeatResult> empList1 = new List<sp_GetListEmployeeSttNotSeatResult>();

                    empList1 = empDao1.GetListEmployeeNotSeat(name);

                    foreach (sp_GetListEmployeeSttNotSeatResult item in empList1)
                    {
                        context.Response.Write(item.FullName + " - " + item.ID + Environment.NewLine);
                    }
                    break;
                case "LocationChart":
                    EmployeeDao empDao2 = new EmployeeDao();
                    
                    
                    List<sp_GetEmployeeSTTListResult> empSttList = new List<sp_GetEmployeeSTTListResult>();

                    empSttList = empDao2.GetEmpSttList(name);
                    if (context.Request.QueryString.AllKeys.Contains("hasLocation"))
                        empSttList = empSttList.Where(p=>!string.IsNullOrEmpty(p.LocationCode)).ToList();
                    foreach (sp_GetEmployeeSTTListResult item in empSttList)
                    {
                        context.Response.Write(item.FullName + " - " + item.ID + Environment.NewLine);
                    }
                    break;
                case "MoveLocation":
                    LocationDao locatinoDao = new LocationDao();
                    var listSeatCode = locatinoDao.GetListSeatCode(name, 0, 0, 0, false);
                    foreach (var item in listSeatCode)
                    {
                        context.Response.Write(item.SeatCodeName + " (" + item.FloorName + ") - "
                             + item.SeatCodeID + Environment.NewLine);
                    }
                    break;
                case "ServiceRequest":
                    var listServiceRequestIT = new UserAdminDao().GetListITHelpDesk((int)Modules.ServiceRequestAdmin, (int)Permissions.ItHelpDesk);
                    foreach (var item in listServiceRequestIT)
                    {
                        context.Response.Write(item.Name + " -" + item.ID+ Environment.NewLine);
                    }
                    break;
                case "ServiceRequestSetting":
                    List<sp_GetSR_SettingResult> listSRSetting = new SRSettingDao().GetList(name,"",0,0);
                    List<string> listUserSetting = listSRSetting.Select(q => q.UserName).Distinct().ToList();
                    foreach (var item in listUserSetting)
                    {
                        context.Response.Write(item + Environment.NewLine);
                    }
                    break;
                case "ClassPlaning":
                    int typeCourse = ConvertUtil.ConvertToInt(context.Request["TypeCourse"]);
                    int trainingStatus = ConvertUtil.ConvertToInt(context.Request["TrainingStatus"]);
                    List<sp_GetClassPlanningResult> listClassID = new TrainingCenterDao().GetList(name, 0, 0, trainingStatus, null, typeCourse);
                    List<string> listClassName = listClassID.Select(q => q.ClassID).Distinct().ToList();
                    foreach (var item in listClassName)
                    {
                        context.Response.Write(item + Environment.NewLine);
                    }
                    break;
                case "AAsset":
                    //List<A_Asset> aAssetList = new AAssetDao().GetParentAsset();
                    //foreach (var item in aAssetList)
                    //{
                    //    context.Response.Write(item.AssetId + Environment.NewLine);
                    //}
                    break;
            }
            #endregion

            string funcName = context.Request.QueryString["func"];
            #region funcName
            switch (funcName)
            { 
                case "Employee":
                    EmployeeDao empDao = new EmployeeDao();
                    //type = 1 -> user name, type = 2 -> full name (first middle last)
                    int type = ConvertUtil.ConvertToInt(context.Request.QueryString["type"]);
                    //hasSuffixId = true -> include the Employee ID in the display result
                    bool hasSuffixId = !string.IsNullOrEmpty(context.Request.QueryString["suffixId"]);
                    //idFirst = true -> display the id first then display name
                    bool idFirst = !string.IsNullOrEmpty(context.Request.QueryString["idFirst"]);
                    if (type == 1)
                    {
                        var empList = empDao.GetListByOfficeEmail(name).Where(p=>!p.DeleteFlag);
                        foreach (var item in empList)
                        {
                            string suffix = "";
                            string mainValue = "";
                            if (hasSuffixId && !idFirst)
                            {
                                suffix = " - " + item.ID;
                                mainValue = item.OfficeEmail.Remove(item.OfficeEmail.IndexOf("@"));
                            }
                            else if (hasSuffixId && idFirst)
                            {
                                suffix = " - " + item.OfficeEmail.Remove(item.OfficeEmail.IndexOf("@"));
                                mainValue = item.ID;
                            }
                            string displayName = mainValue + suffix;
                            context.Response.Write(displayName + Environment.NewLine);
                        }
                    }
                    else if (type == 2)
                    {
                        var empList = empDao.GetList(name, 0, 0, 0, 1, 0).Where(p=>p.EmpStatusId != Constants.RESIGNED);
                        foreach (var item in empList)
                        {
                            string suffix = "";
                            string mainValue = "";
                            if (hasSuffixId && !idFirst)
                            {
                                suffix = " - " + item.ID;
                                mainValue = empDao.FullName(item.ID, Constants.FullNameFormat.FirstMiddleLast);
                            }
                            else if (hasSuffixId && idFirst)
                            {
                                suffix = " - " + empDao.FullName(item.ID, Constants.FullNameFormat.FirstMiddleLast);
                                mainValue = item.ID;
                            }
                            string displayName = mainValue + suffix;
                            context.Response.Write(displayName + Environment.NewLine);
                        }
                    }
                    break;
            }
            #endregion
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}