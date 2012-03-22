using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CRM.Models
{
    public enum Role
    {
        Manager = 1
    }

    public enum Modules
    {        
        Group = 1,
        UserAdmin = 2,
        WorkflowAdmin = 3,
        Employee = 4 ,
        ContractRenewal = 5,        
        UserLog = 6,
        Hiring =7,
        STT = 8,
	    Candidate = 9,
		ViewSalaryInfo = 10,    
        ExamQuestion = 12,
        Exam = 13,
        Question = 14,
        PurchaseRequest = 15,
        AssetCategory = 16,
        AssetProperty = 17,
        AnnualHoliday =18,
        PTO_Admin = 19,
        PTO_Report = 20,
        JobTitleLevel = 21,
        UpdateDataInPortal = 22,
        JobRequest = 23,
        PerformanceReviewHR = 24,
        Menu = 25,
        ServiceRequest = 26,
        ServiceRequestCategory = 27,
        ServiceRequestReport = 28,
        ServiceRequestAdmin = 29,
        TrainingCenter = 30,
        EmployeeEnglishInfo = 31,
        TrainingMaterial = 32,
        PurchaseRequestUS = 33,
        AssetMaster = 34,
        TimeManagement = 35,
        TrainingAdminConfirmation = 36
    }

    public enum Permissions
    {                
        Read = 1,
        Insert = 2,
        Update = 3,
        Delete = 4,
        ContractNotification = 5,
        Assign = 6,
        Mark = 7,        
        ViewContract = 8,
        AssignCandidate = 9,
        AssignEmployee = 10,
        Export = 11,
        ViewAllPR = 12,
        ForceEdit = 13,
        Supervisor = 14,
        AddActivity = 15,
        DeleteActivity = 16,
        UpdateSolution = 17,
        GetApproval = 18,
        PTONotification = 19,
        WeeklyReport = 20, // ServiceRequestReport
        OpenedClosedReport = 21, // ServiceRequestReport
        RequestsActivityReport = 22, // ServiceRequestReport
        ItHelpDesk = 23, // ServiceRequestAdmin
        Routing = 24, // ServiceRequestAdmin
        Notification = 25, // ServiceRequestAdmin
        ProfessionalClass = 26,
        TrainingRecord = 27,
        UpdateAttendee = 28,
        Report = 29, // PTO Report permission
        DueDateSetting = 30, // SR Settings
        SensitiveInfo = 31
    }

    /// <summary>
    /// Permission Constants
    /// </summary>
    public class PermissionConstants
    {
        /// <summary>
        /// Get Permission List
        /// </summary>
        public static List<ListItem> ListModule
        {
            get
            {
                List<ListItem> functionList = new List<ListItem>();
                Array arrFuncs = Enum.GetValues(typeof(Modules));
                for (int i = 0; i < arrFuncs.Length; i++)
                {
                    Modules f = (Modules)arrFuncs.GetValue(i);
                    functionList.Add(new ListItem(f.ToString(), f.GetHashCode().ToString()));
                }
                return functionList;
            }
        }


        /// <summary>
        /// Get Permission List
        /// </summary>
        public static List<ListItem> ListPermission
        {
            get
            {
                List<ListItem> permissionList = new List<ListItem>();
                Array arrFuncs = Enum.GetValues(typeof(Permissions));
                for (int i = 0; i < arrFuncs.Length; i++)
                {
                    Permissions f = (Permissions)arrFuncs.GetValue(i);
                    permissionList.Add(new ListItem(f.ToString(), f.GetHashCode().ToString()));
                }
                return permissionList;
            }
        }
    }
}